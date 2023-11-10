using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Report
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ResultsData { get; set; }
        public int RunByUserId { get; set; }
        public int TypeId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual User RunByUser { get; set; } = null!;
        public virtual ReportType Type { get; set; } = null!;
    }
}
