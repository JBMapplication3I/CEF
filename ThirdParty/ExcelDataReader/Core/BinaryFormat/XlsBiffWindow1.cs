// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffWindow1
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;

    /// <summary>The XLS biff window 1.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffWindow1 : XlsBiffRecord
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffWindow1"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffWindow1(byte[] bytes, uint offset, ExcelBinaryReader reader) : base(bytes, offset, reader) { }

        /// <summary>A bit-field of flags for specifying window 1 options.</summary>
        [Flags]
        public enum Window1Flags : ushort
        {
            /// <summary>A binary constant representing the hidden flag.</summary>
            Hidden = 1,


            /// <summary>A binary constant representing the minimized flag.</summary>
            Minimized = 2,


            /// <summary>A binary constant representing the scroll visible flag.</summary>
            HScrollVisible = 8,


            /// <summary>0x0010.</summary>
            VScrollVisible = 16,


            /// <summary>0x0020.</summary>
            WorkbookTabs = 32,
        }

        /// <summary>Gets the active tab.</summary>
        /// <value>The active tab.</value>
        public ushort ActiveTab => ReadUInt16(10);

        /// <summary>Gets the first visible tab.</summary>
        /// <value>The first visible tab.</value>
        public ushort FirstVisibleTab => ReadUInt16(12);

        /// <summary>Gets the flags.</summary>
        /// <value>The flags.</value>
        public Window1Flags Flags => (Window1Flags)ReadUInt16(8);

        /// <summary>Gets the height.</summary>
        /// <value>The height.</value>
        public ushort Height => ReadUInt16(6);

        /// <summary>Gets the left.</summary>
        /// <value>The left.</value>
        public ushort Left => ReadUInt16(0);

        /// <summary>Gets the number of selected tabs.</summary>
        /// <value>The number of selected tabs.</value>
        public ushort SelectedTabCount => ReadUInt16(14);

        /// <summary>Gets the tab ratio.</summary>
        /// <value>The tab ratio.</value>
        public ushort TabRatio => ReadUInt16(16);

        /// <summary>Gets the top.</summary>
        /// <value>The top.</value>
        public ushort Top => ReadUInt16(2);

        /// <summary>Gets the width.</summary>
        /// <value>The width.</value>
        public ushort Width => ReadUInt16(4);
    }
}
