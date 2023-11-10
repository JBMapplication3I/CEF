using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class GeneralAttributePredefinedOption
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string Value { get; set; } = null!;
        public string? UofM { get; set; }
        public int? SortOrder { get; set; }
        public int AttributeId { get; set; }

        public virtual GeneralAttribute Attribute { get; set; } = null!;
    }
}
