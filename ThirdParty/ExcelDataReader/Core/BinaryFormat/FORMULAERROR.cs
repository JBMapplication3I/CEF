// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.FORMULAERROR
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>Values that represent formulaerrors.</summary>
    internal enum FORMULAERROR : byte
    {
        NULL = 0,

        DIV0 = 7,

        /// <summary>0x0F.</summary>
        VALUE = 15,

        /// <summary>0x17.</summary>
        REF = 23,

        /// <summary>0x1D.</summary>
        NAME = 29,

        /// <summary>0x24.</summary>
        NUM = 36,

        /// <summary>0x2A.</summary>
        NA = 42,
    }
}
