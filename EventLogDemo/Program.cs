using System;

namespace EventLogDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var instance = new WorkFileMonitor();
            instance.Start();
        }
    }
}
