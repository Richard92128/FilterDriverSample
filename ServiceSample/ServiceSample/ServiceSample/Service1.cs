using NLog;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using static Vanara.PInvoke.AdvApi32;

namespace ServiceSample
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private static Semaphore _lock = new Semaphore(0, 1);
        private static EventLogQuery query = new EventLogQuery("Security", PathType.LogName, "*[EventData[Data[@Name='AccessMask']='0x10000']]");

        private Thread _thread = new Thread(_InitializeComSvr);
        private EventLogWatcher eventLog = new EventLogWatcher(query);
        public static void _InitializeComSvr(object _obj)
        {
            Service1 svr = (Service1)_obj;

            RegistrationServices rs = new RegistrationServices();
            int cookie = rs.RegisterTypeForComClients(typeof(ComServer), RegistrationClassContext.LocalServer, RegistrationConnectionType.MultipleUse);

            _lock.WaitOne();

            rs.UnregisterTypeForComClients(cookie);
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
                app.StartInfo.Arguments = "/set /subcategory:\"File System\" /failure:disable /success:disable";
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

            
            eventLog.EventRecordWritten += EventLog_EventRecordWritten;
            //eventLog.Enabled = true;

            _thread.Start(this);
            //_InitializeComSvr(this);
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

        protected override void OnStop()
        {
            base.OnStop();

            _lock.Release();
            _thread.Join();

            eventLog.Enabled = false;
        }
    }
}
