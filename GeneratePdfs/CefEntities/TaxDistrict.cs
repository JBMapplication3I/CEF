using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class TaxDistrict
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public int DistrictId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual District District { get; set; } = null!;
    }
}
