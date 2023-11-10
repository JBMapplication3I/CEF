// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.BZip2.BZip2
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.BZip2
{
    using System;
    using System.IO;

    using ICSharpCode.SharpZipLib.Core;

    /// <summary>A zip 2.</summary>
    public static class BZip2
    {
        /// <summary>Compress.</summary>
        /// <param name="inStream">     Stream to read data from.</param>
        /// <param name="outStream">    Stream to write data to.</param>
        /// <param name="isStreamOwner">True if this BZip2 is stream owner.</param>
        /// <param name="level">        The level.</param>
        public static void Compress(Stream inStream, Stream outStream, bool isStreamOwner, int level)
        {
            if (inStream == null || outStream == null)
            {
                throw new Exception("Null Stream");
            }

            try
            {
                using var bZip2OutputStream = new BZip2OutputStream(outStream, level);
                bZip2OutputStream.IsStreamOwner = isStreamOwner;
                StreamUtils.Copy(inStream, bZip2OutputStream, new byte[4096]);
            }
            finally
            {
                if (isStreamOwner)
                {
                    inStream.Close();
                }
            }
        }

        /// <summary>Decompress this BZip2.</summary>
        /// <param name="inStream">     Stream to read data from.</param>
        /// <param name="outStream">    Stream to write data to.</param>
        /// <param name="isStreamOwner">True if this BZip2 is stream owner.</param>
        public static void Decompress(Stream inStream, Stream outStream, bool isStreamOwner)
        {
            if (inStream == null || outStream == null)
            {
                throw new Exception("Null Stream");
            }

            try
            {
                using var bZip2InputStream = new BZip2InputStream(inStream);
                bZip2InputStream.IsStreamOwner = isStreamOwner;
                StreamUtils.Copy(bZip2InputStream, outStream, new byte[4096]);
            }
            finally
            {
                if (isStreamOwner)
                {
                    outStream.Close();
                }
            }
        }
    }
}