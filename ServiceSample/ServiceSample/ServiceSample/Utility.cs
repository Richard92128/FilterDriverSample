using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSample
{
    public static class Setting
    {
        public static readonly int _lenBuffer = 2;
        public static readonly int _inoutMaxBufferLen = 128;
    }
    public static class Utility
    {
        
    }

    public class ObservingTarget
    {
        public readonly string _path;
        public readonly SecurityIdentifier _user;
        public readonly object _lock = new object();
        public ObservingTarget(string path, SecurityIdentifier user)
        {
            _path = path;
            _user = user;
        }
    }
}
