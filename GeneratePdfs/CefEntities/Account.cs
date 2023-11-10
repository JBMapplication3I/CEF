using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Account
    {
        public Account()
        {
            AccountAssociationMasters = new HashSet<AccountAssociation>();
            AccountAssociationSlaves = new HashSet<AccountAssociation>();
            AccountContacts = new HashSet<AccountContact>();
            AccountCurrencies = new HashSet<AccountCurrency>();
            AccountFiles = new HashSet<AccountFile>();
            AccountImages = new HashSet<AccountImage>();
            AccountPricePoints = new HashSet<AccountPricePoint>();
            AccountProducts = new HashSet<AccountProduct>();
            AccountUsageBalances = new HashSet<AccountUsageBalance>();
            AccountUserRoles = new HashSet<AccountUserRole>();
            AdAccounts = new HashSet<AdAccount>();
            BrandAccounts = new HashSet<BrandAccount>();
            Calendars = new HashSet<Calendar>();
            Carts = new HashSet<Cart>();
            Contractors = new HashSet<Contractor>();
            DiscountAccounts = new HashSet<DiscountAccount>();
            FranchiseAccounts = new HashSet<FranchiseAccount>();
            Notes = new HashSet<Note>();
            PriceRuleAccounts = new HashSet<PriceRuleAccount>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SalesGroups = new HashSet<SalesGroup>();
            SalesInvoices = new HashSet<SalesInvoice>();
            SalesOrders = new HashSet<SalesOrder>();
            SalesQuotes = new HashSet<SalesQuote>();
            SalesReturns = new HashSet<SalesReturn>();
            SampleRequests = new HashSet<SampleRequest>();
            StoreAccounts = new HashSet<StoreAccount>();
            Subscriptions = new HashSet<Subscription>();
            Users = new HashSet<User>();
            VendorAccounts = new HashSet<VendorAccount>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? JsonAttributes { get; set; }
        public bool IsTaxable { get; set; }
        public string? TaxExemptionNo { get; set; }
        public string? TaxEntityUseCode { get; set; }
        public bool IsOnHold { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public long? Hash { get; set; }
        public decimal? Credit { get; set; }
        public int? CreditCurrencyId { get; set; }
        public string? Token { get; set; }
        public string? SageId { get; set; }

        public virtual Currency? CreditCurrency { get; set; }
        public virtual AccountStatus Status { get; set; } = null!;
        public virtual AccountType Type { get; set; } = null!;
        public virtual ICollection<AccountAssociation> AccountAssociationMasters { get; set; }
        public virtual ICollection<AccountAssociation> AccountAssociationSlaves { get; set; }
        public virtual ICollection<AccountContact> AccountContacts { get; set; }
        public virtual ICollection<AccountCurrency> AccountCurrencies { get; set; }
        public virtual ICollection<AccountFile> AccountFiles { get; set; }
        public virtual ICollection<AccountImage> AccountImages { get; set; }
        public virtual ICollection<AccountPricePoint> AccountPricePoints { get; set; }
        public virtual ICollection<AccountProduct> AccountProducts { get; set; }
        public virtual ICollection<AccountUsageBalance> AccountUsageBalances { get; set; }
        public virtual ICollection<AccountUserRole> AccountUserRoles { get; set; }
        public virtual ICollection<AdAccount> AdAccounts { get; set; }
        public virtual ICollection<BrandAccount> BrandAccounts { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Contractor> Contractors { get; set; }
        public virtual ICollection<DiscountAccount> DiscountAccounts { get; set; }
        public virtual ICollection<FranchiseAccount> FranchiseAccounts { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<PriceRuleAccount> PriceRuleAccounts { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SalesGroup> SalesGroups { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        public virtual ICollection<SalesQuote> SalesQuotes { get; set; }
        public virtual ICollection<SalesReturn> SalesReturns { get; set; }
        public virtual ICollection<SampleRequest> SampleRequests { get; set; }
        public virtual ICollection<StoreAccount> StoreAccounts { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<VendorAccount> VendorAccounts { get; set; }
    }
}
