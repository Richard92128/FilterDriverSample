using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Vanara.Extensions;
using static Vanara.PInvoke.Ole32;

namespace ServiceSample
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private static readonly string _rootApp = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\WorkDeletedFileMornitor";
        private static readonly string _logDestination = _rootApp + "\\Logs";
        private static readonly string _logFileName = _logDestination + "\\LogDemo.txt"; // dummy file name
        private static readonly string _configFilePath = _rootApp + "\\AppConfig.json";

        private CancellationTokenSource _lock = new CancellationTokenSource();
        private Thread _thread = null;

        private static EventLogQuery _query = new EventLogQuery("Security", PathType.LogName, "*[EventData[Data[@Name='AccessMask']='0x10000']]");
        private EventLogWatcher _eventLog = new EventLogWatcher(_query);

        private object _observingTargetLock = new object();
        private ObservingTarget _observingTarget = null; // dummy, I will implement concurence config manager for it later

        private bool _SetAuditPermission(string path, bool isAdd)
        {
            try
            {
                var att = File.GetAttributes(path);
                bool isFolder = (att & FileAttributes.Directory) == FileAttributes.Directory;
                var iden = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                var auditRule = new FileSystemAuditRule(iden, FileSystemRights.Delete, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AuditFlags.Success | AuditFlags.Failure);

                if (isFolder)
                {
                    var fileSecurity = Directory.GetAccessControl(path);

                    if (isAdd)
                    {
                        fileSecurity.SetAuditRule(auditRule);
                    }
                    else
                    {
                        if (!fileSecurity.RemoveAuditRule(auditRule))
                        {
                            return false;
                        }
                    }
                    Directory.SetAccessControl(path, fileSecurity);
                }
                else
                {
                    var fileSecurity = File.GetAccessControl(path);

                    if (isAdd)
                    {
                        fileSecurity.SetAuditRule(auditRule);
                    }
                    else
                    {
                        if (!fileSecurity.RemoveAuditRule(auditRule))
                        {
                            return false;
                        }
                    }
                    File.SetAccessControl(path, fileSecurity);
                }
            }
            catch 
            {
                return false;
            }
            return true;
        }

        private bool _executeCommand(string command, string path, SecurityIdentifier callerIden)
        {
            if (command == "RegisterFolderPath")
            {
                if (!_SetAuditPermission(path, true))
                {
                    return false;
                }
                else
                {
                    lock (_observingTargetLock)
                    {
                        _observingTarget = new ObservingTarget(path, callerIden);
                    }
                    
                }
            }
            else if (command == "UnregisterFolderPath")
            {
                if (!_SetAuditPermission(path, false))
                {
                    return false;
                }
                else
                {
                    lock (_observingTargetLock)
                    {
                        _observingTarget = null;
                    }
                }
            }
            else if(command == "ExportFolderPath")
            {
                // this operation can be conducted in another thread
                try
                {
                    // get log file name that matchs with the file path from configuration
                    // implement
                    // end session

                    //var att = File.GetAttributes(path);
                    //if((att & FileAttributes.Directory) != FileAttributes.Directory)
                    //{
                    //    return false;
                    //}

                    object loc = null;
                    lock (_observingTargetLock)
                    {
                        if(_observingTarget != null)
                        {
                            loc = _observingTarget._lock;
                        }
                    }

                    if(loc == null)
                    {
                        return false;
                    }

                    lock (loc)
                    {

                        string sourcepath = _logFileName; // dummy file name

                        using (var fileStrm = new FileStream(sourcepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                        using (var dest = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            int bufferSize = 1024;
                            dest.SetLength(fileStrm.Length);
                            int bytesRead = -1;
                            byte[] bytes = new byte[bufferSize];

                            while ((bytesRead = fileStrm.Read(bytes, 0, bufferSize)) > 0)
                            {
                                dest.Write(bytes, 0, bytesRead);
                            }
                            dest.Flush();
                        }

                        var fileSecurity = File.GetAccessControl(path);
                        fileSecurity.AddAccessRule(new FileSystemAccessRule(callerIden, FileSystemRights.FullControl, AccessControlType.Allow));
                        File.SetAccessControl(path, fileSecurity);
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        private void _InitializeSvr()
        {
            var logger = LogManager.GetCurrentClassLogger();

            _thread = new Thread(() =>
            {
                // install connecting permission, loading the users that were permited to connect pipe from the config file.
                var IdentSystem = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
                var IdentEveryOne = new SecurityIdentifier(WellKnownSidType.WorldSid, null); // dummy
                var pipeSecurity = new PipeSecurity();
                pipeSecurity.AddAccessRule(new PipeAccessRule(IdentSystem,
                    PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance,
                    AccessControlType.Allow));
                pipeSecurity.AddAccessRule(new PipeAccessRule(IdentEveryOne,
                    PipeAccessRights.ReadWrite,
                    AccessControlType.Allow));

                var _inoutGate = new NamedPipeServerStream("WorkMornitorDeletedFileGate", PipeDirection.InOut, 1,
                                PipeTransmissionMode.Message, PipeOptions.Asynchronous, Setting._inoutMaxBufferLen, Setting._inoutMaxBufferLen, pipeSecurity);

                while (!_lock.Token.IsCancellationRequested)
                {
                    string retVal = "OK";
                    string[] funcCall;
                    string path;
                    string command;
                    SecurityIdentifier callerIden = null;
                    try
                    {
                        var _await = _inoutGate.WaitForConnectionAsync(_lock.Token);
                        _await.Wait();

                        var stream = new MemoryStream();
                        int totalLen = 0;
                        do
                        {
                            byte[] buffer = new byte[Setting._inoutMaxBufferLen];
                            int readed = _inoutGate.Read(buffer, 0, buffer.Length);
                            totalLen += readed;
                            stream.Write(buffer, 0, readed);
                        }
                        while (!_inoutGate.IsMessageComplete);
                        funcCall = Encoding.Unicode.GetString(stream.GetBuffer(), 0, totalLen).Split('>');

                        if (funcCall != null && funcCall.Length == 2)
                        {
                            command = funcCall[0];
                            path = funcCall[1];
                            stream.Dispose();
                            logger.Debug("Get path = {0}", path);

                            bool _isValidPath = false;
                            _inoutGate.RunAsClient(() =>
                            {
                                logger.Debug("Impersonate: {0}", WindowsIdentity.GetCurrent().User.Value);

                                //We have a System Object (maybe a file), and then we check permission on it.
                                //If it does not have permission to read the System Object, then do return here.
                                //Thus, we can register all users that are able to change audit rules by giving them permission to read the System Object.

                                // implement here

                                //end session

                                FileAttributes att;
                                try
                                {
                                    att = File.GetAttributes(path);
                                    //if ((att & FileAttributes.Directory) != FileAttributes.Directory) return;
                                }
                                catch (Exception e)
                                {
                                    logger.Debug("ERROR: {0}", e.Message);
                                    return;
                                }
                                FileSecurity fileSecurity = File.GetAccessControl(path);
                                IdentityReference sid = fileSecurity.GetOwner(typeof(SecurityIdentifier));
                                if (sid.Equals(WindowsIdentity.GetCurrent().User))
                                {
                                    path = Path.GetFullPath(path);
                                    callerIden = new SecurityIdentifier(WindowsIdentity.GetCurrent().User.Value);
                                    _isValidPath = true;
                                }
                            });
                            
                            if (!_isValidPath
                                    || !_executeCommand(command, path, callerIden))
                            {
                                retVal = "NG";
                            }
                        }
                        else
                        {
                            retVal = "NG";
                        }

                        var input = Encoding.Unicode.GetBytes(retVal);
                        _inoutGate.Write(input, 0, input.Length);
                        _inoutGate.WaitForPipeDrain();
                        _inoutGate.Disconnect();
                    }
                    // Catch the IOException that is raised if the pipe is broken
                    // or disconnected.
                    catch (Exception e)
                    {
                        logger.Debug("ERROR: {0}", e.Message);
                    }
                }

                _inoutGate.Close();
            });
            _thread.IsBackground = true;
            _thread.Start();
            
        }

        private void EventLog_EventRecordWritten(object sender, EventRecordWrittenEventArgs e)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(e.EventRecord.ToXml());
            var eventData = xmlDoc.FirstChild.ChildNodes[1];
            var type = eventData.GetType();
            string filePath = "";
            foreach (XmlElement node in eventData)
            {
                if (node.Attributes.GetNamedItem("Name").InnerText == "ObjectName")
                {
                    filePath = node.InnerText; break;
                }
            }

            string path = "";
            SecurityIdentifier secur = null;
            object loc = null;
            lock (_observingTargetLock)
            {
                if(_observingTarget != null)
                {
                    path = _observingTarget._path;
                    secur = new SecurityIdentifier(_observingTarget._user.Value);
                    loc = _observingTarget._lock;
                }
            }

            if(!path.EndsWith("\\"))
            {
                path += "\\";
            }

            if(string.IsNullOrEmpty(path) 
                || string.IsNullOrEmpty(filePath)
                || !filePath.StartsWith(path))
            {
                return;
            }

            if (loc == null) return;

            lock (loc)
            {
                if(!Directory.Exists(_rootApp)) 
                {
                    Directory.CreateDirectory(_rootApp);
                }
                if (!Directory.Exists(_logDestination))
                {
                    Directory.CreateDirectory(_logDestination);
                }
                if (!File.Exists(_logFileName))
                {
                    var strm = File.Create(_logFileName);
                    strm.Close();
                    var fileSecurity = File.GetAccessControl(_logFileName);
                    fileSecurity.AddAccessRule(new FileSystemAccessRule(secur, FileSystemRights.FullControl, AccessControlType.Allow));
                    File.SetAccessControl(_logFileName, fileSecurity);
                }
                using (var fileStrm = new FileStream(_logFileName, FileMode.Append, FileAccess.Write, FileShare.Write))
                {
                    filePath += "\r\n";
                    byte[] buffer = Encoding.Unicode.GetBytes(filePath);
                    fileStrm.Write(buffer, 0, buffer.Length);
                    fileStrm.Flush();
                }
            }
        }
        public void _Initialize()
        {
            var logger = LogManager.GetCurrentClassLogger();

            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            logger.Debug(string.Format("Is Admin Elevate {0}", isElevated.ToString()));

            using (var app = new Process())
            {
                app.StartInfo.FileName = "auditpol.exe";
                app.StartInfo.Arguments = "/set /subcategory:\"File System\" /failure:enable /success:enable";
                app.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                app.StartInfo.Verb = "runas";
                app.EnableRaisingEvents = true;
                app.StartInfo.RedirectStandardOutput = true;
                app.StartInfo.RedirectStandardError = true;
                // Must not set true to execute PowerShell command
                app.StartInfo.UseShellExecute = false;
                app.Start();
                string outputString;
                using (var o = app.StandardOutput)
                {
                    outputString = o.ReadToEndAsync().Result;
                }
                logger.Debug(string.Format("auditpol res {0}", outputString));
            }

            
            _eventLog.EventRecordWritten += EventLog_EventRecordWritten;
            _eventLog.Enabled = true;


            _InitializeSvr();
        }

        private void _DeInitialize()
        {
            var logger = LogManager.GetCurrentClassLogger();

            _lock.Cancel();

            _thread.Join();

            _eventLog.Enabled = false;
        }



        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            _Initialize();
        }

        protected override void OnStop()
        {
            base.OnStop();
            _DeInitialize();
        }
    }
}
