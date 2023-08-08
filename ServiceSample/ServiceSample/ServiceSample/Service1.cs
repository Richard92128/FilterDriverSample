using NLog;
using System;
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

namespace ServiceSample
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private CancellationTokenSource _lock = new CancellationTokenSource();
        private Thread _thread = null;

        private static EventLogQuery _query = new EventLogQuery("Security", PathType.LogName, "*[EventData[Data[@Name='AccessMask']='0x10000']]");
        private EventLogWatcher _eventLog = new EventLogWatcher(_query);

        //private NamedPipeServerStream _inoutGate = null;

        private void _InitializeSvr()
        {
            var logger = LogManager.GetCurrentClassLogger();

            _thread = new Thread(() =>
            {
                var IdentSystem = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
                var IdentEveryOne = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
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
                    string path;
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

                        path = Encoding.Unicode.GetString(stream.GetBuffer(), 0, totalLen);
                        logger.Debug("Get path = {0}", path);

                        bool _isValidPath = false;
                        _inoutGate.RunAsClient(() =>
                        {
                            // check permisson here
                            _isValidPath = true;
                        });

                        var input = Encoding.Unicode.GetBytes("OK");
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

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            _Initialize();
        }

        private void EventLog_EventRecordWritten(object sender, EventRecordWrittenEventArgs e)
        {
            int tung = 1;
        }

        private void _DeInitialize()
        {
            var logger = LogManager.GetCurrentClassLogger();

            _lock.Cancel();

            _thread.Join();
            
            _eventLog.Enabled = false;
        }

        protected override void OnStop()
        {
            base.OnStop();
            _DeInitialize();
        }
    }
}
