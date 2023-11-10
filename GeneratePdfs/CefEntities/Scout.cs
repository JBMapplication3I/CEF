using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Scout
    {
        public Scout()
        {
            ScoutCategories = new HashSet<ScoutCategory>();
        }

        public int Id { get; set; }
        public DateTime? YearMin { get; set; }
        public DateTime? YearMax { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public decimal? HoursUsedMin { get; set; }
        public decimal? HoursUsedMax { get; set; }
        public decimal? DistanceUsedMin { get; set; }
        public decimal? DistanceUsedMax { get; set; }
        public string? DistanceUnitOfMeasure { get; set; }
        public int CreatedByUserId { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual User CreatedByUser { get; set; } = null!;
        public virtual ICollection<ScoutCategory> ScoutCategories { get; set; }
    }
}
