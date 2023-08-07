using System;
using System.Runtime.InteropServices;
using Vanara.PInvoke;

namespace ServiceSample
{
    [ComVisible(true)]
    public interface IServer
    {
        void RegisterPath(string path);
        void ExportFile(string path);
        void UnregisterPath(string path);
    }

    [ComVisible(true)]
    [ComDefaultInterface(typeof(IServer))]
    [Guid("7c462b89-9733-4dd6-a939-b9bc090d7651")]
    public sealed class ComServer : IServer
    {
        void IServer.ExportFile(string path)
        {
            var res = Ole32.CoImpersonateClient();
            int tung = 1;
        }

        void IServer.RegisterPath(string path)
        {
            var res = Ole32.CoImpersonateClient();
            int tung = 1;
        }

        void IServer.UnregisterPath(string path)
        {
            var res = Ole32.CoImpersonateClient();
            int tung = 1;
        }
    }
}
