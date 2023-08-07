using NLog;
using System;
using System.ServiceProcess;

namespace ServiceSample
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            LogManager.Setup().LoadConfiguration(builder => {
                builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "D:\\ServiceLog\\TestService.txt");
            });

            Vanara.PInvoke.Ole32.SOLE_AUTHENTICATION_LIST sOLE_AUTHENTICATION_LIST;
            sOLE_AUTHENTICATION_LIST.cAuthInfo = 0;
            sOLE_AUTHENTICATION_LIST.aAuthInfo = IntPtr.Zero;
            Vanara.PInvoke.Ole32.SOLE_AUTHENTICATION_SERVICE[] sOLE_AUTHENTICATION_SERVICEs = null;

            var res = Vanara.PInvoke.Ole32.CoInitializeSecurity(IntPtr.Zero, -1, sOLE_AUTHENTICATION_SERVICEs, IntPtr.Zero,
                        Vanara.PInvoke.Rpc.RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_NONE, Vanara.PInvoke.Rpc.RPC_C_IMP_LEVEL.RPC_C_IMP_LEVEL_IMPERSONATE,
                        sOLE_AUTHENTICATION_LIST, Vanara.PInvoke.Ole32.EOLE_AUTHENTICATION_CAPABILITIES.EOAC_NONE, IntPtr.Zero);
            LogManager.GetCurrentClassLogger().Debug(string.Format("CoInitializeSecurity return {0}", res.ToString()));

            if (Environment.UserInteractive)
            {
                // debug
                var service = new Service1();
                service._Initialize();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
