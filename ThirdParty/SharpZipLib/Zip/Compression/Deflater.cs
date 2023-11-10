// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Deflater
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;

    /// <summary>A deflater.</summary>
    public class Deflater
    {
        /// <summary>The best compression.</summary>
        public const int BEST_COMPRESSION = 9;

        /// <summary>The best speed.</summary>
        public const int BEST_SPEED = 1;

        /// <summary>The default compression.</summary>
        public const int DEFAULT_COMPRESSION = -1;

        /// <summary>The deflated.</summary>
        public const int DEFLATED = 8;

        /// <summary>The no compression.</summary>
        public const int NO_COMPRESSION = 0;

        /// <summary>State of the busy.</summary>
        private const int BUSY_STATE = 16;

        /// <summary>State of the closed.</summary>
        private const int CLOSED_STATE = 127;

        /// <summary>State of the finished.</summary>
        private const int FINISHED_STATE = 30;

        /// <summary>State of the finishing.</summary>
        private const int FINISHING_STATE = 28;

        /// <summary>State of the flushing.</summary>
        private const int FLUSHING_STATE = 20;

        /// <summary>State of the initialise.</summary>
        private const int INIT_STATE = 0;

        /// <summary>The is finishing.</summary>
        private const int IS_FINISHING = 8;

        /// <summary>The is flushing.</summary>
        private const int IS_FLUSHING = 4;

        /// <summary>The is setdict.</summary>
        private const int IS_SETDICT = 1;

        /// <summary>State of the setdict.</summary>
        private const int SETDICT_STATE = 1;

        /// <summary>The engine.</summary>
        private readonly DeflaterEngine engine;

        /// <summary>The level.</summary>
        private int level;

        /// <summary>True to no zlib header or footer.</summary>
        private readonly bool noZlibHeaderOrFooter;

        /// <summary>The pending.</summary>
        private readonly DeflaterPending pending;

        /// <summary>The state.</summary>
        private int state;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Deflater" />
        ///     class.
        /// </summary>
        public Deflater() : this(-1, false) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Deflater" />
        ///     class.
        /// </summary>
        /// <param name="level">The level.</param>
        public Deflater(int level) : this(level, false) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Deflater" />
        ///     class.
        /// </summary>
        /// <param name="level">               The level.</param>
        /// <param name="noZlibHeaderOrFooter">True to no zlib header or footer.</param>
        public Deflater(int level, bool noZlibHeaderOrFooter)
        {
            if (level == -1)
            {
                level = 6;
            }
            else if (level < 0 || level > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }
            pending = new DeflaterPending();
            engine = new DeflaterEngine(pending);
            this.noZlibHeaderOrFooter = noZlibHeaderOrFooter;
            SetStrategy(DeflateStrategy.Default);
            SetLevel(level);
            Reset();
        }

        /// <summary>Gets the adler.</summary>
        /// <value>The adler.</value>
        public int Adler => engine.Adler;

        /// <summary>Gets a value indicating whether this Deflater is finished.</summary>
        /// <value>True if this Deflater is finished, false if not.</value>
        public bool IsFinished => state == 30 && pending.IsFlushed;

        /// <summary>Gets a value indicating whether this Deflater is needing input.</summary>
        /// <value>True if this Deflater is needing input, false if not.</value>
        public bool IsNeedingInput => engine.NeedsInput();

        /// <summary>Gets the total number of in.</summary>
        /// <value>The total number of in.</value>
        public long TotalIn => engine.TotalIn;

        /// <summary>Gets the total number of out.</summary>
        /// <value>The total number of out.</value>
        public long TotalOut { get; private set; }

        /// <summary>Deflates the given output.</summary>
        /// <param name="output">The output.</param>
        /// <returns>An int.</returns>
        public int Deflate(byte[] output)
        {
            return Deflate(output, 0, output.Length);
        }

        /// <summary>Deflates.</summary>
        /// <param name="output">The output.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>An int.</returns>
        public int Deflate(byte[] output, int offset, int length)
        {
            var num1 = length;
            if (state == sbyte.MaxValue)
            {
                throw new InvalidOperationException("Deflater closed");
            }
            if (state < 16)
            {
                var num2 = 30720;
                var num3 = (level - 1) >> 1;
                if (num3 < 0 || num3 > 3)
                {
                    num3 = 3;
                }
                var num4 = num2 | (num3 << 6);
                if ((state & 1) != 0)
                {
                    num4 |= 32;
                }
                pending.WriteShortMSB(num4 + (31 - num4 % 31));
                if ((state & 1) != 0)
                {
                    var adler = engine.Adler;
                    engine.ResetAdler();
                    pending.WriteShortMSB(adler >> 16);
                    pending.WriteShortMSB(adler & ushort.MaxValue);
                }
                state = 16 | (state & 12);
            }
            while (true)
            {
                do
                {
                    do
                    {
                        var num2 = pending.Flush(output, offset, length);
                        offset += num2;
                        TotalOut += num2;
                        length -= num2;
                        if (length == 0 || state == 30)
                        {
                            goto label_24;
                        }
                    }
                    while (engine.Deflate((state & 4) != 0, (state & 8) != 0));
                    if (state == 16)
                    {
                        return num1 - length;
                    }
                    if (state == 20)
                    {
                        if (level != 0)
                        {
                            for (var index = 8 + (-pending.BitCount & 7); index > 0; index -= 10)
                            {
                                pending.WriteBits(2, 10);
                            }
                        }
                        state = 16;
                    }
                }
                while (state != 28);
                pending.AlignToByte();
                if (!noZlibHeaderOrFooter)
                {
                    var adler = engine.Adler;
                    pending.WriteShortMSB(adler >> 16);
                    pending.WriteShortMSB(adler & ushort.MaxValue);
                }
                state = 30;
            }
        label_24:
            return num1 - length;
        }

        /// <summary>Finishes this Deflater.</summary>
        public void Finish()
        {
            state |= 12;
        }

        /// <summary>Flushes this Deflater.</summary>
        public void Flush()
        {
            state |= 4;
        }

        /// <summary>Gets the level.</summary>
        /// <returns>The level.</returns>
        public int GetLevel()
        {
            return level;
        }

        /// <summary>Resets this Deflater.</summary>
        public void Reset()
        {
            state = noZlibHeaderOrFooter ? 16 : 0;
            TotalOut = 0L;
            pending.Reset();
            engine.Reset();
        }

        /// <summary>Sets a dictionary.</summary>
        /// <param name="dictionary">The dictionary.</param>
        public void SetDictionary(byte[] dictionary)
        {
            SetDictionary(dictionary, 0, dictionary.Length);
        }

        /// <summary>Sets a dictionary.</summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="index">     Zero-based index of the.</param>
        /// <param name="count">     Number of.</param>
        public void SetDictionary(byte[] dictionary, int index, int count)
        {
            state = state == 0 ? 1 : throw new InvalidOperationException();
            engine.SetDictionary(dictionary, index, count);
        }

        /// <summary>Sets an input.</summary>
        /// <param name="input">The input.</param>
        public void SetInput(byte[] input)
        {
            SetInput(input, 0, input.Length);
        }

        /// <summary>Sets an input.</summary>
        /// <param name="input"> The input.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        public void SetInput(byte[] input, int offset, int count)
        {
            if ((state & 8) != 0)
            {
                throw new InvalidOperationException("Finish() already called");
            }
            engine.SetInput(input, offset, count);
        }

        /// <summary>Sets a level.</summary>
        /// <param name="level">The level.</param>
        public void SetLevel(int level)
        {
            if (level == -1)
            {
                level = 6;
            }
            else if (level < 0 || level > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }
            if (this.level == level)
            {
                return;
            }
            this.level = level;
            engine.SetLevel(level);
        }

        /// <summary>Sets a strategy.</summary>
        /// <param name="strategy">The strategy.</param>
        public void SetStrategy(DeflateStrategy strategy)
        {
            engine.Strategy = strategy;
        }
    }
}
