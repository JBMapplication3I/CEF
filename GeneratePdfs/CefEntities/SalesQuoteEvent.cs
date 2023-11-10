using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SalesQuoteEvent
    {
        public int Id { get; set; }
        public int? OldStateId { get; set; }
        public int? NewStateId { get; set; }
        public int? OldStatusId { get; set; }
        public int? NewStatusId { get; set; }
        public int? OldTypeId { get; set; }
        public int? NewTypeId { get; set; }
        public long? OldHash { get; set; }
        public long? NewHash { get; set; }
        public string? OldRecordSerialized { get; set; }
        public string? NewRecordSerialized { get; set; }
        public int TypeId { get; set; }
        public int MasterId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual SalesQuote Master { get; set; } = null!;
        public virtual SalesQuoteEventType Type { get; set; } = null!;
    }
}
