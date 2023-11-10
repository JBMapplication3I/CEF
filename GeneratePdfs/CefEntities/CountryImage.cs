using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class CountryImage
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? SortOrder { get; set; }
        public string? DisplayName { get; set; }
        public string? SeoTitle { get; set; }
        public string? Author { get; set; }
        public DateTime? MediaDate { get; set; }
        public string? Copyright { get; set; }
        public string? Location { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsPrimary { get; set; }
        public int? OriginalWidth { get; set; }
        public int? OriginalHeight { get; set; }
        public string? OriginalFileFormat { get; set; }
        public string? OriginalFileName { get; set; }
        public bool OriginalIsStoredInDb { get; set; }
        public byte[]? OriginalBytes { get; set; }
        public int? ThumbnailWidth { get; set; }
        public int? ThumbnailHeight { get; set; }
        public string? ThumbnailFileFormat { get; set; }
        public string? ThumbnailFileName { get; set; }
        public bool ThumbnailIsStoredInDb { get; set; }
        public byte[]? ThumbnailBytes { get; set; }
        public int? MasterId { get; set; }
        public int TypeId { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Country? Master { get; set; }
        public virtual CountryImageType Type { get; set; } = null!;
    }
}
