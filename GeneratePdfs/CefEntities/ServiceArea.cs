using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ServiceArea
    {
        public int Id { get; set; }
        public decimal? Radius { get; set; }
        public int ContractorId { get; set; }
        public int AddressId { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Contractor Contractor { get; set; } = null!;
    }
}
