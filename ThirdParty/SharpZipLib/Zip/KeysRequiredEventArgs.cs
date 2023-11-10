// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.KeysRequiredEventArgs
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;

    /// <summary>Additional information for keys required events.</summary>
    /// <seealso cref="EventArgs" />
    public class KeysRequiredEventArgs : EventArgs
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.KeysRequiredEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="name">The name.</param>
        public KeysRequiredEventArgs(string name)
        {
            FileName = name;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.KeysRequiredEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="name">    The name.</param>
        /// <param name="keyValue">The key value.</param>
        public KeysRequiredEventArgs(string name, byte[] keyValue)
        {
            FileName = name;
            Key = keyValue;
        }

        /// <summary>Gets the filename of the file.</summary>
        /// <value>The name of the file.</value>
        public string FileName { get; }

        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        public byte[] Key { get; set; }
    }
}
