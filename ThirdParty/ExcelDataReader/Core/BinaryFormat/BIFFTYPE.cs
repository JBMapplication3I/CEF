// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.BIFFTYPE
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>Values that represent bifftypes.</summary>
    internal enum BIFFTYPE : ushort
    {
        WorkbookGlobals = 5,

        /// <summary>An enum constant representing the VB module option.</summary>
        VBModule = 6,

        /// <summary>0x0010.</summary>
        Worksheet = 16,

        /// <summary>0x0020.</summary>
        Chart = 32,

        /// <summary>0x0040.</summary>
        v4MacroSheet = 64,

        /// <summary>0x0100.</summary>
        v4WorkbookGlobals = 256,
    }
}
