using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EventLogDemo
{
    [ComVisible(true)]
    [Guid("AF080472-F173-4D9D-8BE7-435776617347")]
    [ComDefaultInterface(typeof(IServer))]
    public sealed class DemoServer : IServer
    {
        double IServer.ComputePi(string input)
        {
            Trace.WriteLine($"Running {nameof(DemoServer)}.{nameof(IServer.ComputePi)} input = {input}");

            return 3.14d;
        }
    }
}
