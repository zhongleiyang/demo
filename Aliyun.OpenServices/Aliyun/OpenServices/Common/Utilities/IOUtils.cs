namespace Aliyun.OpenServices.Common.Utilities
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal static class IOUtils
    {
        private const int _bufferSize = 0x1000;

        public static void WriteTo(this Stream src, Stream dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException("dest");
            }
            byte[] buffer = new byte[0x1000];
            int bytesRead = 0;
            while ((bytesRead = src.Read(buffer, 0, buffer.Length)) > 0)
            {
                dest.Write(buffer, 0, bytesRead);
            }
            dest.Flush();
        }
    }
}

