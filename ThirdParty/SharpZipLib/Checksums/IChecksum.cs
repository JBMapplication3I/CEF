// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Checksums.IChecksum
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Checksums
{
    /// <summary>Interface for checksum.</summary>
    public interface IChecksum
    {
        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        long Value { get; }

        /// <summary>Resets this IChecksum.</summary>
        void Reset();

        /// <summary>Updates the given value.</summary>
        /// <param name="value">The value.</param>
        void Update(int value);

        /// <summary>Updates the given buffer.</summary>
        /// <param name="buffer">The buffer.</param>
        void Update(byte[] buffer);

        /// <summary>Updates this IChecksum.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        void Update(byte[] buffer, int offset, int count);
    }
}