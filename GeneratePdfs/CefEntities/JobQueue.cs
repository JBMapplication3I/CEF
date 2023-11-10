using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class JobQueue
    {
        public int Id { get; set; }
        public string Queue { get; set; } = null!;
        public DateTime? FetchedAt { get; set; }
        public long JobId { get; set; }

        public virtual Job Job { get; set; } = null!;
    }
}
