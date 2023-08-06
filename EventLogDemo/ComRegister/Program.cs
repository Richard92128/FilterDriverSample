using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.Win32;
using ComTypes = System.Runtime.InteropServices.ComTypes;

namespace ComRegister
{
    internal class Program
    {
        //public enum REGKIND
        //{
        //    REGKIND_DEFAULT = 0,
        //    REGKIND_REGISTER = 1,
        //    REGKIND_NONE = 2
        //}

        //[DllImport("OleAut32", CharSet = CharSet.Unicode, ExactSpelling = true)]
        //public static extern int LoadTypeLibEx(
        //        [In, MarshalAs(UnmanagedType.LPWStr)] string fileName,
        //        REGKIND regKind,
        //        out ComTypes.ITypeLib typeLib);
        //[DllImport("OleAut32")]
        //public static extern int UnRegisterTypeLib(
        //        ref Guid id,
        //        short majorVersion,
        //        short minorVersion,
        //        int lcid,
        //        ComTypes.SYSKIND sysKind);

        public const string ServerClass = "AF080472-F173-4D9D-8BE7-435776617347";
        public static readonly Guid ServerClassGuid = Guid.Parse(ServerClass);
        public static readonly string LocalServer32 = @"SOFTWARE\Classes\CLSID\{0:B}\LocalServer32";
        private static readonly string exePath = Path.Combine(AppContext.BaseDirectory, "EventLogDemo.exe");
        private static readonly string tlbPath = Path.Combine(AppContext.BaseDirectory, "ComInterface.tlb");
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string regCommandMaybe = args[0];
                if (regCommandMaybe.Equals("/regserver", StringComparison.OrdinalIgnoreCase) || regCommandMaybe.Equals("-regserver", StringComparison.OrdinalIgnoreCase))
                {
                    LocalServer.Register(ServerClassGuid, exePath, tlbPath);
                }
                else if (regCommandMaybe.Equals("/unregserver", StringComparison.OrdinalIgnoreCase) || regCommandMaybe.Equals("-unregserver", StringComparison.OrdinalIgnoreCase))
                {
                    LocalServer.Unregister(ServerClassGuid, tlbPath);
                }
            }

        }
    }
}
