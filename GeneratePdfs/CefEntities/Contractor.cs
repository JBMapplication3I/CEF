using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Contractor
    {
        public Contractor()
        {
            ServiceAreas = new HashSet<ServiceArea>();
        }

        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? UserId { get; set; }
        public int? StoreId { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Store? Store { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<ServiceArea> ServiceAreas { get; set; }
    }
}
