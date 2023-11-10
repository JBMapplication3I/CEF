// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.STGTY
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>Values that represent stgties.</summary>
    internal enum STGTY : byte
    {
        /// <summary>An enum constant representing the stgty invalid option.</summary>
        STGTY_INVALID,


        /// <summary>An enum constant representing the stgty storage option.</summary>
        STGTY_STORAGE,


        /// <summary>An enum constant representing the stgty stream option.</summary>
        STGTY_STREAM,


        /// <summary>An enum constant representing the stgty Location kB ytes option.</summary>
        STGTY_LOCKBYTES,


        /// <summary>An enum constant representing the stgty property option.</summary>
        STGTY_PROPERTY,


        /// <summary>An enum constant representing the stgty root option.</summary>
        STGTY_ROOT,
    }
}
