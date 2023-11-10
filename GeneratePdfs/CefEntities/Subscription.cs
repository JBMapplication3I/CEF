using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Subscription
    {
        public Subscription()
        {
            AdZoneAccesses = new HashSet<AdZoneAccess>();
            StoreSubscriptions = new HashSet<StoreSubscription>();
            SubscriptionHistories = new HashSet<SubscriptionHistory>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime StartsOn { get; set; }
        public DateTime? EndsOn { get; set; }
        public int BillingPeriodsTotal { get; set; }
        public int BillingPeriodsPaid { get; set; }
        public DateTime? LastPaidDate { get; set; }
        public decimal Fee { get; set; }
        public string? Memo { get; set; }
        public bool AutoRenew { get; set; }
        public bool CanUpgrade { get; set; }
        public decimal? CreditUponUpgrade { get; set; }
        public int RepeatTypeId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public int? UserId { get; set; }
        public int? AccountId { get; set; }
        public long? Hash { get; set; }
        public int? SalesInvoiceId { get; set; }
        public string? JsonAttributes { get; set; }
        public int? ProductMembershipLevelId { get; set; }
        public bool IsAutoRefill { get; set; }
        public int? SalesGroupId { get; set; }
        public int? ProductSubscriptionTypeId { get; set; }
        public int? SalesOrderId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ProductMembershipLevel? ProductMembershipLevel { get; set; }
        public virtual ProductSubscriptionType? ProductSubscriptionType { get; set; }
        public virtual RepeatType RepeatType { get; set; } = null!;
        public virtual SalesGroup? SalesGroup { get; set; }
        public virtual SalesInvoice? SalesInvoice { get; set; }
        public virtual SalesOrder? SalesOrder { get; set; }
        public virtual SubscriptionStatus Status { get; set; } = null!;
        public virtual SubscriptionType Type { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<AdZoneAccess> AdZoneAccesses { get; set; }
        public virtual ICollection<StoreSubscription> StoreSubscriptions { get; set; }
        public virtual ICollection<SubscriptionHistory> SubscriptionHistories { get; set; }
    }
}
