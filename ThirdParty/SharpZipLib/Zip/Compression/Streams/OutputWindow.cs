// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Streams.OutputWindow
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
    using System;

    /// <summary>Form for viewing the output.</summary>
    public class OutputWindow
    {
        /// <summary>The window mask.</summary>
        private const int WindowMask = 32767;

        /// <summary>Size of the window.</summary>
        private const int WindowSize = 32768;

        /// <summary>The window.</summary>
        private readonly byte[] window = new byte[32768];

        /// <summary>The window end.</summary>
        private int windowEnd;

        /// <summary>The window filled.</summary>
        private int windowFilled;

        /// <summary>Copies the dictionary.</summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="offset">    The offset.</param>
        /// <param name="length">    The length.</param>
        public void CopyDict(byte[] dictionary, int offset, int length)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }
            if (windowFilled > 0)
            {
                throw new InvalidOperationException();
            }
            if (length > 32768)
            {
                offset += length - 32768;
                length = 32768;
            }
            Array.Copy(dictionary, offset, window, 0, length);
            windowEnd = length & short.MaxValue;
        }

        /// <summary>Copies the output.</summary>
        /// <param name="output">The output.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="len">   The length.</param>
        /// <returns>An int.</returns>
        public int CopyOutput(byte[] output, int offset, int len)
        {
            var num1 = windowEnd;
            if (len > windowFilled)
            {
                len = windowFilled;
            }
            else
            {
                num1 = (windowEnd - windowFilled + len) & short.MaxValue;
            }
            var num2 = len;
            var length = len - num1;
            if (length > 0)
            {
                Array.Copy(window, 32768 - length, output, offset, length);
                offset += length;
                len = num1;
            }
            Array.Copy(window, num1 - len, output, offset, len);
            windowFilled -= num2;
            if (windowFilled < 0)
            {
                throw new InvalidOperationException();
            }
            return num2;
        }

        /// <summary>Copies the stored.</summary>
        /// <param name="input"> The input.</param>
        /// <param name="length">The length.</param>
        /// <returns>An int.</returns>
        public int CopyStored(StreamManipulator input, int length)
        {
            length = Math.Min(Math.Min(length, 32768 - windowFilled), input.AvailableBytes);
            var length1 = 32768 - windowEnd;
            int num;
            if (length > length1)
            {
                num = input.CopyBytes(window, windowEnd, length1);
                if (num == length1)
                {
                    num += input.CopyBytes(window, 0, length - length1);
                }
            }
            else
            {
                num = input.CopyBytes(window, windowEnd, length);
            }
            windowEnd = (windowEnd + num) & short.MaxValue;
            windowFilled += num;
            return num;
        }

        /// <summary>Gets the available.</summary>
        /// <returns>The available.</returns>
        public int GetAvailable()
        {
            return windowFilled;
        }

        /// <summary>Gets free space.</summary>
        /// <returns>The free space.</returns>
        public int GetFreeSpace()
        {
            return 32768 - windowFilled;
        }

        /// <summary>Repeats.</summary>
        /// <param name="length">  The length.</param>
        /// <param name="distance">The distance.</param>
        public void Repeat(int length, int distance)
        {
            if ((windowFilled += length) > 32768)
            {
                throw new InvalidOperationException("Window full");
            }
            var num1 = (windowEnd - distance) & short.MaxValue;
            var num2 = 32768 - length;
            if (num1 <= num2 && windowEnd < num2)
            {
                if (length <= distance)
                {
                    Array.Copy(window, num1, window, windowEnd, length);
                    windowEnd += length;
                }
                else
                {
                    while (length-- > 0)
                    {
                        window[windowEnd++] = window[num1++];
                    }
                }
            }
            else
            {
                SlowRepeat(num1, length, distance);
            }
        }

        /// <summary>Resets this OutputWindow.</summary>
        public void Reset()
        {
            windowFilled = windowEnd = 0;
        }

        /// <summary>Writes.</summary>
        /// <param name="value">The value to write.</param>
        public void Write(int value)
        {
            if (windowFilled++ == 32768)
            {
                throw new InvalidOperationException("Window full");
            }
            window[windowEnd++] = (byte)value;
            windowEnd &= short.MaxValue;
        }

        /// <summary>Slow repeat.</summary>
        /// <param name="repStart">The rep start.</param>
        /// <param name="length">  The length.</param>
        /// <param name="distance">The distance.</param>
        private void SlowRepeat(int repStart, int length, int distance)
        {
            while (length-- > 0)
            {
                window[windowEnd++] = window[repStart++];
                windowEnd &= short.MaxValue;
                repStart &= short.MaxValue;
            }
        }
    }
}
