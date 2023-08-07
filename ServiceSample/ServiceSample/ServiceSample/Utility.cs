using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
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
        //public static string ReadStringFromPipe(Stream pipe)
        //{
        //    if (pipe.CanRead)
        //    {
        //        // Get string len
        //        var len = new byte[Setting._lenBuffer];
        //        int actualLen = 0;
        //        var actualLenBuff = pipe.Read(len, 0, Setting._lenBuffer);
        //        //if (actualLenBuff < Setting._lenBuffer)
        //        //{
        //        //    return "";
        //        //}
        //        //actualLen = (len[1] << 8) | len[0];

        //        //var streamEncoding = new UnicodeEncoding();
        //        //var buff = new byte[actualLen];
        //        //actualLenBuff = pipe.Read(len, 0, actualLen);
        //        //if (actualLenBuff < actualLen)
        //        //{
        //        //    return "";
        //        //}
        //        //return streamEncoding.GetString(buff);

        //        var streamEncoding = new UnicodeEncoding();
        //        return streamEncoding.GetString(len);

        //    }
        //    return "";
        //}
    }
}
