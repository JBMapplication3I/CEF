using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Campaign
    {
        public Campaign()
        {
            CampaignAds = new HashSet<CampaignAd>();
            Events = new HashSet<Event>();
            PageViews = new HashSet<PageView>();
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public string? JsonAttributes { get; set; }
        public DateTime? ProposedStart { get; set; }
        public DateTime? ProposedEnd { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? ActualEnd { get; set; }
        public decimal? BudgetedCost { get; set; }
        public decimal? OtherCost { get; set; }
        public decimal? ExpectedRevenue { get; set; }
        public decimal? TotalActualCost { get; set; }
        public decimal? TotalCampaignActivityActualCost { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string? CodeName { get; set; }
        public string? PromotionCodeName { get; set; }
        public string? Message { get; set; }
        public string? Objective { get; set; }
        public int? ExpectedResponse { get; set; }
        public int? UtcconversionTimeZoneCode { get; set; }
        public bool? IsTemplate { get; set; }
        public int? CreatedByUserId { get; set; }
        public long? Hash { get; set; }

        public virtual User? CreatedByUser { get; set; }
        public virtual CampaignStatus Status { get; set; } = null!;
        public virtual CampaignType Type { get; set; } = null!;
        public virtual ICollection<CampaignAd> CampaignAds { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<PageView> PageViews { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
