using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SalesReturnEventType
    {
        public SalesReturnEventType()
        {
            SalesReturnEvents = new HashSet<SalesReturnEvent>();
        }

        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public int? SortOrder { get; set; }
        public string? TranslationKey { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual ICollection<SalesReturnEvent> SalesReturnEvents { get; set; }
    }
}
