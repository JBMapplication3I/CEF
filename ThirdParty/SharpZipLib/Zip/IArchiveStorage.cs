// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.IArchiveStorage
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>Interface for archive storage.</summary>
    public interface IArchiveStorage : IDisposable
    {
        /// <summary>Gets the update mode.</summary>
        /// <value>The update mode.</value>
        FileUpdateMode UpdateMode { get; }

        /// <summary>Convert temporary to final.</summary>
        /// <returns>IArchiveStorage converted to a temporary to final.</returns>
        Stream ConvertTemporaryToFinal();

        /// <summary>Gets temporary output.</summary>
        /// <returns>The temporary output.</returns>
        Stream GetTemporaryOutput();

        /// <summary>Makes temporary copy.</summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A Stream.</returns>
        Stream MakeTemporaryCopy(Stream stream);

        /// <summary>Opens for direct update.</summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A Stream.</returns>
        Stream OpenForDirectUpdate(Stream stream);
    }
}
