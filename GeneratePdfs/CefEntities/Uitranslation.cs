using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Uitranslation
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Locale { get; set; }
        public string? Value { get; set; }
        public int UiKeyId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Uikey UiKey { get; set; } = null!;
    }
}
