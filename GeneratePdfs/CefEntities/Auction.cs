using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Auction
    {
        public Auction()
        {
            AuctionCategories = new HashSet<AuctionCategory>();
            BrandAuctions = new HashSet<BrandAuction>();
            FranchiseAuctions = new HashSet<FranchiseAuction>();
            Listings = new HashSet<Listing>();
            Lots = new HashSet<Lot>();
            StoreAuctions = new HashSet<StoreAuction>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int? ContactId { get; set; }
        public DateTime? OpensAt { get; set; }
        public DateTime? ClosesAt { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Contact? Contact { get; set; }
        public virtual AuctionStatus Status { get; set; } = null!;
        public virtual AuctionType Type { get; set; } = null!;
        public virtual ICollection<AuctionCategory> AuctionCategories { get; set; }
        public virtual ICollection<BrandAuction> BrandAuctions { get; set; }
        public virtual ICollection<FranchiseAuction> FranchiseAuctions { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<StoreAuction> StoreAuctions { get; set; }
    }
}
