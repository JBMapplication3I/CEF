using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Counter1
    {
        public string Key { get; set; } = null!;
        public int Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
