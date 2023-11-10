using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Bid
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public decimal? MaxBid { get; set; }
        public decimal? CurrentBid { get; set; }
        public int? LotId { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public bool Won { get; set; }
        public int? ListingId { get; set; }

        public virtual Listing? Listing { get; set; }
        public virtual Lot? Lot { get; set; }
        public virtual BidStatus Status { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
