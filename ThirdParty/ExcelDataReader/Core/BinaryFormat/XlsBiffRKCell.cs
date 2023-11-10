// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffRKCell
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>The XLS biff rk cell.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffBlankCell"/>
    internal class XlsBiffRKCell : XlsBiffBlankCell
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffRKCell"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffRKCell(byte[] bytes, uint offset, ExcelBinaryReader reader) : base(bytes, offset, reader) { }

        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        public double Value => NumFromRK(ReadUInt32(6));

        /// <summary>Number from rk.</summary>
        /// <param name="rk">The rk.</param>
        /// <returns>The total number of from rk.</returns>
        public static double NumFromRK(uint rk)
        {
            var num = ((int)rk & 2) != 2
                ? Helpers.Int64BitsToDouble((long)(rk & 4294967292U) << 32)
                : (int)(rk >> 2) | (((int)rk & int.MinValue) == 0 ? 0 : -1073741824);
            if (((int)rk & 1) == 1)
            {
                num /= 100.0;
            }
            return num;
        }
    }
}
