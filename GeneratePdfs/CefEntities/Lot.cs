using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Lot
    {
        public Lot()
        {
            Bids = new HashSet<Bid>();
            LotCategories = new HashSet<LotCategory>();
        }

        public int Id { get; set; }
        public int StatusId { get; set; }
        public int ProductId { get; set; }
        public bool BuyNowAvailable { get; set; }
        public decimal? BiddingReserve { get; set; }
        public bool NoShow { get; set; }
        public DateTime? PickupTime { get; set; }
        public int AuctionId { get; set; }
        public int? PickupLocationId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int TypeId { get; set; }
        public bool PreventBuyMultiple { get; set; }
        public decimal? QuantityAvailable { get; set; }
        public decimal? QuantitySold { get; set; }

        public virtual Auction Auction { get; set; } = null!;
        public virtual Contact? PickupLocation { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual LotStatus Status { get; set; } = null!;
        public virtual LotType Type { get; set; } = null!;
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<LotCategory> LotCategories { get; set; }
    }
}
