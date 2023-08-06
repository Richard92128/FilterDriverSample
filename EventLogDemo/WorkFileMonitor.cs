using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Threading;

namespace EventLogDemo
{
    public class WorkFileMonitor
    {
        public WorkFileMonitor()
        {
           
        }
        ~WorkFileMonitor()
        {

        }

        private Semaphore _lock = new Semaphore(0, 1);

        public void Start()
        {
            var query = new EventLogQuery("Security", PathType.LogName, "*[EventData[Data[@Name='AccessMask']='0x10000']]");
            var eventLog = new EventLogWatcher(query);
            eventLog.EventRecordWritten += EventLog_EventRecordWritten;
            eventLog.Enabled = true;


            RegistrationServices rs = new RegistrationServices();
            int cookie = rs.RegisterTypeForComClients(typeof(DemoServer), RegistrationClassContext.LocalServer, RegistrationConnectionType.MultipleUse);

            _lock.WaitOne();

            rs.UnregisterTypeForComClients(cookie);
        }

        private static void EventLog_EventRecordWritten(object sender, EventRecordWrittenEventArgs e)
        {
            var str = e.EventRecord.ToXml();
            var format = e.EventRecord.FormatDescription();
            var key = e.EventRecord.KeywordsDisplayNames;
            int tung = 1;
        }

    }
}
