// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.StreamUtils
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    using System;
    using System.IO;

    /// <summary>A stream utilities. This class cannot be inherited.</summary>
    public sealed class StreamUtils
    {
        /// <summary>
        ///     Prevents a default instance of the ICSharpCode.SharpZipLib.Core.StreamUtils class from being
        ///     created.
        /// </summary>
        private StreamUtils() { }

        /// <summary>Copies this StreamUtils.</summary>
        /// <param name="source">     Another instance to copy.</param>
        /// <param name="destination">Destination for the.</param>
        /// <param name="buffer">     The buffer.</param>
        public static void Copy(Stream source, Stream destination, byte[] buffer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (buffer.Length < 128)
            {
                throw new ArgumentException("Buffer is too small", nameof(buffer));
            }

            var flag = true;
            while (flag)
            {
                var count = source.Read(buffer, 0, buffer.Length);
                if (count > 0)
                {
                    destination.Write(buffer, 0, count);
                }
                else
                {
                    destination.Flush();
                    flag = false;
                }
            }
        }

        /// <summary>Copies this StreamUtils.</summary>
        /// <param name="source">         Another instance to copy.</param>
        /// <param name="destination">    Destination for the.</param>
        /// <param name="buffer">         The buffer.</param>
        /// <param name="progressHandler">The progress handler.</param>
        /// <param name="updateInterval"> The update interval.</param>
        /// <param name="sender">         Source of the event.</param>
        /// <param name="name">           The name.</param>
        public static void Copy(
            Stream source,
            Stream destination,
            byte[] buffer,
            ProgressHandler progressHandler,
            TimeSpan updateInterval,
            object sender,
            string name)
        {
            Copy(source, destination, buffer, progressHandler, updateInterval, sender, name, -1L);
        }

        /// <summary>Copies this StreamUtils.</summary>
        /// <param name="source">         Another instance to copy.</param>
        /// <param name="destination">    Destination for the.</param>
        /// <param name="buffer">         The buffer.</param>
        /// <param name="progressHandler">The progress handler.</param>
        /// <param name="updateInterval"> The update interval.</param>
        /// <param name="sender">         Source of the event.</param>
        /// <param name="name">           The name.</param>
        /// <param name="fixedTarget">    The fixed target.</param>
        public static void Copy(
            Stream source,
            Stream destination,
            byte[] buffer,
            ProgressHandler progressHandler,
            TimeSpan updateInterval,
            object sender,
            string name,
            long fixedTarget)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (buffer.Length < 128)
            {
                throw new ArgumentException("Buffer is too small", nameof(buffer));
            }

            if (progressHandler == null)
            {
                throw new ArgumentNullException(nameof(progressHandler));
            }

            var flag1 = true;
            var now = DateTime.Now;
            long processed = 0;
            long target = 0;
            if (fixedTarget >= 0L)
            {
                target = fixedTarget;
            }
            else if (source.CanSeek)
            {
                target = source.Length - source.Position;
            }

            var e1 = new ProgressEventArgs(name, processed, target);
            progressHandler(sender, e1);
            var flag2 = true;
            while (flag1)
            {
                var count = source.Read(buffer, 0, buffer.Length);
                if (count > 0)
                {
                    processed += count;
                    flag2 = false;
                    destination.Write(buffer, 0, count);
                }
                else
                {
                    destination.Flush();
                    flag1 = false;
                }

                if (DateTime.Now - now > updateInterval)
                {
                    flag2 = true;
                    now = DateTime.Now;
                    var e2 = new ProgressEventArgs(name, processed, target);
                    progressHandler(sender, e2);
                    flag1 = e2.ContinueRunning;
                }
            }

            if (flag2)
            {
                return;
            }

            var e3 = new ProgressEventArgs(name, processed, target);
            progressHandler(sender, e3);
        }

        /// <summary>Reads a fully.</summary>
        /// <param name="stream">The stream.</param>
        /// <param name="buffer">The buffer.</param>
        public static void ReadFully(Stream stream, byte[] buffer)
        {
            ReadFully(stream, buffer, 0, buffer.Length);
        }

        /// <summary>Reads a fully.</summary>
        /// <param name="stream">The stream.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        public static void ReadFully(Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0 || offset > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (count < 0 || offset + count > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            int num;
            for (; count > 0; count -= num)
            {
                num = stream.Read(buffer, offset, count);
                if (num <= 0)
                {
                    throw new EndOfStreamException();
                }

                offset += num;
            }
        }
    }
}