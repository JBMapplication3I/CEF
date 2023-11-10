using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class JobParameter
    {
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
        public long JobId { get; set; }

        public virtual Job Job { get; set; } = null!;
    }
}
