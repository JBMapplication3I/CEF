// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.FATMARKERS
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>Values that represent fatmarkers.</summary>
    internal enum FATMARKERS : uint
    {
        /// <summary>0xFFFFFFFC.</summary>
        FAT_DifSector = 4294967292,


        /// <summary>0xFFFFFFFD.</summary>
        FAT_FatSector = 4294967293,


        /// <summary>0xFFFFFFFE.</summary>
        FAT_EndOfChain = 4294967294,


        /// <summary>0xFFFFFFFF.</summary>
        FAT_FreeSpace = 4294967295,
    }
}
