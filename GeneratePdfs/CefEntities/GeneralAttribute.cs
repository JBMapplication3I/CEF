using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class GeneralAttribute
    {
        public GeneralAttribute()
        {
            GeneralAttributePredefinedOptions = new HashSet<GeneralAttributePredefinedOption>();
        }

        public int Id { get; set; }
        public string CustomKey { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsFilter { get; set; }
        public bool IsPredefined { get; set; }
        public bool IsMarkup { get; set; }
        public int TypeId { get; set; }
        public string? DisplayName { get; set; }
        public int? SortOrder { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public bool HideFromStorefront { get; set; }
        public bool IsComparable { get; set; }
        public bool IsTab { get; set; }
        public bool HideFromSuppliers { get; set; }
        public bool HideFromProductDetailView { get; set; }
        public bool HideFromCatalogViews { get; set; }
        public int? AttributeTabId { get; set; }
        public int? AttributeGroupId { get; set; }
        public string? TranslationKey { get; set; }

        public virtual AttributeGroup? AttributeGroup { get; set; }
        public virtual AttributeTab? AttributeTab { get; set; }
        public virtual AttributeType Type { get; set; } = null!;
        public virtual ICollection<GeneralAttributePredefinedOption> GeneralAttributePredefinedOptions { get; set; }
    }
}
