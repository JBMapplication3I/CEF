using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GeneratePdfs.CefEntities
{
    public partial class ClarityEcommerceEntities : DbContext
    {
        public ClarityEcommerceEntities()
        {
        }

        public ClarityEcommerceEntities(DbContextOptions<ClarityEcommerceEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountAssociation> AccountAssociations { get; set; } = null!;
        public virtual DbSet<AccountAssociationType> AccountAssociationTypes { get; set; } = null!;
        public virtual DbSet<AccountContact> AccountContacts { get; set; } = null!;
        public virtual DbSet<AccountCurrency> AccountCurrencies { get; set; } = null!;
        public virtual DbSet<AccountFile> AccountFiles { get; set; } = null!;
        public virtual DbSet<AccountImage> AccountImages { get; set; } = null!;
        public virtual DbSet<AccountImageType> AccountImageTypes { get; set; } = null!;
        public virtual DbSet<AccountPricePoint> AccountPricePoints { get; set; } = null!;
        public virtual DbSet<AccountProduct> AccountProducts { get; set; } = null!;
        public virtual DbSet<AccountProductType> AccountProductTypes { get; set; } = null!;
        public virtual DbSet<AccountStatus> AccountStatuses { get; set; } = null!;
        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<AccountUsageBalance> AccountUsageBalances { get; set; } = null!;
        public virtual DbSet<AccountUserRole> AccountUserRoles { get; set; } = null!;
        public virtual DbSet<Ad> Ads { get; set; } = null!;
        public virtual DbSet<AdAccount> AdAccounts { get; set; } = null!;
        public virtual DbSet<AdBrand> AdBrands { get; set; } = null!;
        public virtual DbSet<AdFranchise> AdFranchises { get; set; } = null!;
        public virtual DbSet<AdImage> AdImages { get; set; } = null!;
        public virtual DbSet<AdImageType> AdImageTypes { get; set; } = null!;
        public virtual DbSet<AdStatus> AdStatuses { get; set; } = null!;
        public virtual DbSet<AdStore> AdStores { get; set; } = null!;
        public virtual DbSet<AdType> AdTypes { get; set; } = null!;
        public virtual DbSet<AdZone> AdZones { get; set; } = null!;
        public virtual DbSet<AdZoneAccess> AdZoneAccesses { get; set; } = null!;
        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; } = null!;
        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; } = null!;
        public virtual DbSet<AppointmentType> AppointmentTypes { get; set; } = null!;
        public virtual DbSet<AttributeGroup> AttributeGroups { get; set; } = null!;
        public virtual DbSet<AttributeTab> AttributeTabs { get; set; } = null!;
        public virtual DbSet<AttributeType> AttributeTypes { get; set; } = null!;
        public virtual DbSet<Auction> Auctions { get; set; } = null!;
        public virtual DbSet<AuctionCategory> AuctionCategories { get; set; } = null!;
        public virtual DbSet<AuctionStatus> AuctionStatuses { get; set; } = null!;
        public virtual DbSet<AuctionType> AuctionTypes { get; set; } = null!;
        public virtual DbSet<Badge> Badges { get; set; } = null!;
        public virtual DbSet<BadgeImage> BadgeImages { get; set; } = null!;
        public virtual DbSet<BadgeImageType> BadgeImageTypes { get; set; } = null!;
        public virtual DbSet<BadgeType> BadgeTypes { get; set; } = null!;
        public virtual DbSet<Bid> Bids { get; set; } = null!;
        public virtual DbSet<BidStatus> BidStatuses { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<BrandAccount> BrandAccounts { get; set; } = null!;
        public virtual DbSet<BrandAuction> BrandAuctions { get; set; } = null!;
        public virtual DbSet<BrandCategory> BrandCategories { get; set; } = null!;
        public virtual DbSet<BrandCurrency> BrandCurrencies { get; set; } = null!;
        public virtual DbSet<BrandFranchise> BrandFranchises { get; set; } = null!;
        public virtual DbSet<BrandImage> BrandImages { get; set; } = null!;
        public virtual DbSet<BrandImageType> BrandImageTypes { get; set; } = null!;
        public virtual DbSet<BrandInventoryLocation> BrandInventoryLocations { get; set; } = null!;
        public virtual DbSet<BrandInventoryLocationType> BrandInventoryLocationTypes { get; set; } = null!;
        public virtual DbSet<BrandLanguage> BrandLanguages { get; set; } = null!;
        public virtual DbSet<BrandManufacturer> BrandManufacturers { get; set; } = null!;
        public virtual DbSet<BrandProduct> BrandProducts { get; set; } = null!;
        public virtual DbSet<BrandSiteDomain> BrandSiteDomains { get; set; } = null!;
        public virtual DbSet<BrandStore> BrandStores { get; set; } = null!;
        public virtual DbSet<BrandUser> BrandUsers { get; set; } = null!;
        public virtual DbSet<BrandVendor> BrandVendors { get; set; } = null!;
        public virtual DbSet<Calendar> Calendars { get; set; } = null!;
        public virtual DbSet<CalendarAppointment> CalendarAppointments { get; set; } = null!;
        public virtual DbSet<CalendarEvent> CalendarEvents { get; set; } = null!;
        public virtual DbSet<CalendarEventDetail> CalendarEventDetails { get; set; } = null!;
        public virtual DbSet<CalendarEventFile> CalendarEventFiles { get; set; } = null!;
        public virtual DbSet<CalendarEventImage> CalendarEventImages { get; set; } = null!;
        public virtual DbSet<CalendarEventImageType> CalendarEventImageTypes { get; set; } = null!;
        public virtual DbSet<CalendarEventProduct> CalendarEventProducts { get; set; } = null!;
        public virtual DbSet<CalendarEventStatus> CalendarEventStatuses { get; set; } = null!;
        public virtual DbSet<CalendarEventType> CalendarEventTypes { get; set; } = null!;
        public virtual DbSet<Campaign> Campaigns { get; set; } = null!;
        public virtual DbSet<CampaignAd> CampaignAds { get; set; } = null!;
        public virtual DbSet<CampaignStatus> CampaignStatuses { get; set; } = null!;
        public virtual DbSet<CampaignType> CampaignTypes { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<CartContact> CartContacts { get; set; } = null!;
        public virtual DbSet<CartDiscount> CartDiscounts { get; set; } = null!;
        public virtual DbSet<CartEvent> CartEvents { get; set; } = null!;
        public virtual DbSet<CartEventType> CartEventTypes { get; set; } = null!;
        public virtual DbSet<CartFile> CartFiles { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<CartItemDiscount> CartItemDiscounts { get; set; } = null!;
        public virtual DbSet<CartItemTarget> CartItemTargets { get; set; } = null!;
        public virtual DbSet<CartState> CartStates { get; set; } = null!;
        public virtual DbSet<CartStatus> CartStatuses { get; set; } = null!;
        public virtual DbSet<CartType> CartTypes { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryFile> CategoryFiles { get; set; } = null!;
        public virtual DbSet<CategoryImage> CategoryImages { get; set; } = null!;
        public virtual DbSet<CategoryImageType> CategoryImageTypes { get; set; } = null!;
        public virtual DbSet<CategoryType> CategoryTypes { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<ContactImage> ContactImages { get; set; } = null!;
        public virtual DbSet<ContactImageType> ContactImageTypes { get; set; } = null!;
        public virtual DbSet<ContactType> ContactTypes { get; set; } = null!;
        public virtual DbSet<Contractor> Contractors { get; set; } = null!;
        public virtual DbSet<Conversation> Conversations { get; set; } = null!;
        public virtual DbSet<ConversationUser> ConversationUsers { get; set; } = null!;
        public virtual DbSet<Counter> Counters { get; set; } = null!;
        public virtual DbSet<Counter1> Counters1 { get; set; } = null!;
        public virtual DbSet<CounterLog> CounterLogs { get; set; } = null!;
        public virtual DbSet<CounterLogType> CounterLogTypes { get; set; } = null!;
        public virtual DbSet<CounterType> CounterTypes { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<CountryCurrency> CountryCurrencies { get; set; } = null!;
        public virtual DbSet<CountryImage> CountryImages { get; set; } = null!;
        public virtual DbSet<CountryImageType> CountryImageTypes { get; set; } = null!;
        public virtual DbSet<CountryLanguage> CountryLanguages { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<CurrencyConversion> CurrencyConversions { get; set; } = null!;
        public virtual DbSet<CurrencyImage> CurrencyImages { get; set; } = null!;
        public virtual DbSet<CurrencyImageType> CurrencyImageTypes { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<DiscountAccount> DiscountAccounts { get; set; } = null!;
        public virtual DbSet<DiscountAccountType> DiscountAccountTypes { get; set; } = null!;
        public virtual DbSet<DiscountBrand> DiscountBrands { get; set; } = null!;
        public virtual DbSet<DiscountCategory> DiscountCategories { get; set; } = null!;
        public virtual DbSet<DiscountCode> DiscountCodes { get; set; } = null!;
        public virtual DbSet<DiscountCountry> DiscountCountries { get; set; } = null!;
        public virtual DbSet<DiscountFranchise> DiscountFranchises { get; set; } = null!;
        public virtual DbSet<DiscountManufacturer> DiscountManufacturers { get; set; } = null!;
        public virtual DbSet<DiscountProduct> DiscountProducts { get; set; } = null!;
        public virtual DbSet<DiscountProductType> DiscountProductTypes { get; set; } = null!;
        public virtual DbSet<DiscountShipCarrierMethod> DiscountShipCarrierMethods { get; set; } = null!;
        public virtual DbSet<DiscountStore> DiscountStores { get; set; } = null!;
        public virtual DbSet<DiscountUser> DiscountUsers { get; set; } = null!;
        public virtual DbSet<DiscountUserRole> DiscountUserRoles { get; set; } = null!;
        public virtual DbSet<DiscountVendor> DiscountVendors { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<DistrictCurrency> DistrictCurrencies { get; set; } = null!;
        public virtual DbSet<DistrictImage> DistrictImages { get; set; } = null!;
        public virtual DbSet<DistrictImageType> DistrictImageTypes { get; set; } = null!;
        public virtual DbSet<DistrictLanguage> DistrictLanguages { get; set; } = null!;
        public virtual DbSet<EmailQueue> EmailQueues { get; set; } = null!;
        public virtual DbSet<EmailQueueAttachment> EmailQueueAttachments { get; set; } = null!;
        public virtual DbSet<EmailStatus> EmailStatuses { get; set; } = null!;
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; } = null!;
        public virtual DbSet<EmailType> EmailTypes { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventStatus> EventStatuses { get; set; } = null!;
        public virtual DbSet<EventType> EventTypes { get; set; } = null!;
        public virtual DbSet<FavoriteCategory> FavoriteCategories { get; set; } = null!;
        public virtual DbSet<FavoriteManufacturer> FavoriteManufacturers { get; set; } = null!;
        public virtual DbSet<FavoriteShipCarrier> FavoriteShipCarriers { get; set; } = null!;
        public virtual DbSet<FavoriteStore> FavoriteStores { get; set; } = null!;
        public virtual DbSet<FavoriteVendor> FavoriteVendors { get; set; } = null!;
        public virtual DbSet<Franchise> Franchises { get; set; } = null!;
        public virtual DbSet<FranchiseAccount> FranchiseAccounts { get; set; } = null!;
        public virtual DbSet<FranchiseAuction> FranchiseAuctions { get; set; } = null!;
        public virtual DbSet<FranchiseCategory> FranchiseCategories { get; set; } = null!;
        public virtual DbSet<FranchiseCountry> FranchiseCountries { get; set; } = null!;
        public virtual DbSet<FranchiseCurrency> FranchiseCurrencies { get; set; } = null!;
        public virtual DbSet<FranchiseDistrict> FranchiseDistricts { get; set; } = null!;
        public virtual DbSet<FranchiseImage> FranchiseImages { get; set; } = null!;
        public virtual DbSet<FranchiseImageType> FranchiseImageTypes { get; set; } = null!;
        public virtual DbSet<FranchiseInventoryLocation> FranchiseInventoryLocations { get; set; } = null!;
        public virtual DbSet<FranchiseInventoryLocationType> FranchiseInventoryLocationTypes { get; set; } = null!;
        public virtual DbSet<FranchiseLanguage> FranchiseLanguages { get; set; } = null!;
        public virtual DbSet<FranchiseManufacturer> FranchiseManufacturers { get; set; } = null!;
        public virtual DbSet<FranchiseProduct> FranchiseProducts { get; set; } = null!;
        public virtual DbSet<FranchiseRegion> FranchiseRegions { get; set; } = null!;
        public virtual DbSet<FranchiseSiteDomain> FranchiseSiteDomains { get; set; } = null!;
        public virtual DbSet<FranchiseStore> FranchiseStores { get; set; } = null!;
        public virtual DbSet<FranchiseType> FranchiseTypes { get; set; } = null!;
        public virtual DbSet<FranchiseUser> FranchiseUsers { get; set; } = null!;
        public virtual DbSet<FranchiseVendor> FranchiseVendors { get; set; } = null!;
        public virtual DbSet<FutureImport> FutureImports { get; set; } = null!;
        public virtual DbSet<FutureImportStatus> FutureImportStatuses { get; set; } = null!;
        public virtual DbSet<GeneralAttribute> GeneralAttributes { get; set; } = null!;
        public virtual DbSet<GeneralAttributePredefinedOption> GeneralAttributePredefinedOptions { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupStatus> GroupStatuses { get; set; } = null!;
        public virtual DbSet<GroupType> GroupTypes { get; set; } = null!;
        public virtual DbSet<GroupUser> GroupUsers { get; set; } = null!;
        public virtual DbSet<Hash> Hashes { get; set; } = null!;
        public virtual DbSet<HistoricalAddressValidation> HistoricalAddressValidations { get; set; } = null!;
        public virtual DbSet<HistoricalCurrencyRate> HistoricalCurrencyRates { get; set; } = null!;
        public virtual DbSet<HistoricalTaxRate> HistoricalTaxRates { get; set; } = null!;
        public virtual DbSet<ImportExportMapping> ImportExportMappings { get; set; } = null!;
        public virtual DbSet<InventoryLocation> InventoryLocations { get; set; } = null!;
        public virtual DbSet<InventoryLocationRegion> InventoryLocationRegions { get; set; } = null!;
        public virtual DbSet<InventoryLocationSection> InventoryLocationSections { get; set; } = null!;
        public virtual DbSet<InventoryLocationUser> InventoryLocationUsers { get; set; } = null!;
        public virtual DbSet<Iporganization> Iporganizations { get; set; } = null!;
        public virtual DbSet<IporganizationStatus> IporganizationStatuses { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobParameter> JobParameters { get; set; } = null!;
        public virtual DbSet<JobQueue> JobQueues { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<LanguageImage> LanguageImages { get; set; } = null!;
        public virtual DbSet<LanguageImageType> LanguageImageTypes { get; set; } = null!;
        public virtual DbSet<List> Lists { get; set; } = null!;
        public virtual DbSet<Listing> Listings { get; set; } = null!;
        public virtual DbSet<ListingCategory> ListingCategories { get; set; } = null!;
        public virtual DbSet<ListingStatus> ListingStatuses { get; set; } = null!;
        public virtual DbSet<ListingType> ListingTypes { get; set; } = null!;
        public virtual DbSet<Lot> Lots { get; set; } = null!;
        public virtual DbSet<LotCategory> LotCategories { get; set; } = null!;
        public virtual DbSet<LotStatus> LotStatuses { get; set; } = null!;
        public virtual DbSet<LotType> LotTypes { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<ManufacturerImage> ManufacturerImages { get; set; } = null!;
        public virtual DbSet<ManufacturerImageType> ManufacturerImageTypes { get; set; } = null!;
        public virtual DbSet<ManufacturerProduct> ManufacturerProducts { get; set; } = null!;
        public virtual DbSet<ManufacturerType> ManufacturerTypes { get; set; } = null!;
        public virtual DbSet<Membership> Memberships { get; set; } = null!;
        public virtual DbSet<MembershipAdZoneAccess> MembershipAdZoneAccesses { get; set; } = null!;
        public virtual DbSet<MembershipAdZoneAccessByLevel> MembershipAdZoneAccessByLevels { get; set; } = null!;
        public virtual DbSet<MembershipLevel> MembershipLevels { get; set; } = null!;
        public virtual DbSet<MembershipRepeatType> MembershipRepeatTypes { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MessageAttachment> MessageAttachments { get; set; } = null!;
        public virtual DbSet<MessageRecipient> MessageRecipients { get; set; } = null!;
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<NoteType> NoteTypes { get; set; } = null!;
        public virtual DbSet<Package> Packages { get; set; } = null!;
        public virtual DbSet<PackageType> PackageTypes { get; set; } = null!;
        public virtual DbSet<PageView> PageViews { get; set; } = null!;
        public virtual DbSet<PageViewEvent> PageViewEvents { get; set; } = null!;
        public virtual DbSet<PageViewStatus> PageViewStatuses { get; set; } = null!;
        public virtual DbSet<PageViewType> PageViewTypes { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; } = null!;
        public virtual DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PhonePrefixLookup> PhonePrefixLookups { get; set; } = null!;
        public virtual DbSet<PricePoint> PricePoints { get; set; } = null!;
        public virtual DbSet<PriceRounding> PriceRoundings { get; set; } = null!;
        public virtual DbSet<PriceRule> PriceRules { get; set; } = null!;
        public virtual DbSet<PriceRuleAccount> PriceRuleAccounts { get; set; } = null!;
        public virtual DbSet<PriceRuleAccountType> PriceRuleAccountTypes { get; set; } = null!;
        public virtual DbSet<PriceRuleBrand> PriceRuleBrands { get; set; } = null!;
        public virtual DbSet<PriceRuleCategory> PriceRuleCategories { get; set; } = null!;
        public virtual DbSet<PriceRuleCountry> PriceRuleCountries { get; set; } = null!;
        public virtual DbSet<PriceRuleManufacturer> PriceRuleManufacturers { get; set; } = null!;
        public virtual DbSet<PriceRuleProduct> PriceRuleProducts { get; set; } = null!;
        public virtual DbSet<PriceRuleProductType> PriceRuleProductTypes { get; set; } = null!;
        public virtual DbSet<PriceRuleStore> PriceRuleStores { get; set; } = null!;
        public virtual DbSet<PriceRuleUserRole> PriceRuleUserRoles { get; set; } = null!;
        public virtual DbSet<PriceRuleVendor> PriceRuleVendors { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductAssociation> ProductAssociations { get; set; } = null!;
        public virtual DbSet<ProductAssociationType> ProductAssociationTypes { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductDownload> ProductDownloads { get; set; } = null!;
        public virtual DbSet<ProductDownloadType> ProductDownloadTypes { get; set; } = null!;
        public virtual DbSet<ProductFile> ProductFiles { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductImageType> ProductImageTypes { get; set; } = null!;
        public virtual DbSet<ProductInventoryLocationSection> ProductInventoryLocationSections { get; set; } = null!;
        public virtual DbSet<ProductMembershipLevel> ProductMembershipLevels { get; set; } = null!;
        public virtual DbSet<ProductNotification> ProductNotifications { get; set; } = null!;
        public virtual DbSet<ProductPricePoint> ProductPricePoints { get; set; } = null!;
        public virtual DbSet<ProductRestriction> ProductRestrictions { get; set; } = null!;
        public virtual DbSet<ProductShipCarrierMethod> ProductShipCarrierMethods { get; set; } = null!;
        public virtual DbSet<ProductStatus> ProductStatuses { get; set; } = null!;
        public virtual DbSet<ProductSubscriptionType> ProductSubscriptionTypes { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<ProfanityFilter> ProfanityFilters { get; set; } = null!;
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; } = null!;
        public virtual DbSet<PurchaseOrderContact> PurchaseOrderContacts { get; set; } = null!;
        public virtual DbSet<PurchaseOrderDiscount> PurchaseOrderDiscounts { get; set; } = null!;
        public virtual DbSet<PurchaseOrderEvent> PurchaseOrderEvents { get; set; } = null!;
        public virtual DbSet<PurchaseOrderEventType> PurchaseOrderEventTypes { get; set; } = null!;
        public virtual DbSet<PurchaseOrderFile> PurchaseOrderFiles { get; set; } = null!;
        public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; } = null!;
        public virtual DbSet<PurchaseOrderItemDiscount> PurchaseOrderItemDiscounts { get; set; } = null!;
        public virtual DbSet<PurchaseOrderItemTarget> PurchaseOrderItemTargets { get; set; } = null!;
        public virtual DbSet<PurchaseOrderState> PurchaseOrderStates { get; set; } = null!;
        public virtual DbSet<PurchaseOrderStatus> PurchaseOrderStatuses { get; set; } = null!;
        public virtual DbSet<PurchaseOrderType> PurchaseOrderTypes { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; } = null!;
        public virtual DbSet<QuestionType> QuestionTypes { get; set; } = null!;
        public virtual DbSet<RateQuote> RateQuotes { get; set; } = null!;
        public virtual DbSet<RecordVersion> RecordVersions { get; set; } = null!;
        public virtual DbSet<RecordVersionType> RecordVersionTypes { get; set; } = null!;
        public virtual DbSet<ReferralCode> ReferralCodes { get; set; } = null!;
        public virtual DbSet<ReferralCodeStatus> ReferralCodeStatuses { get; set; } = null!;
        public virtual DbSet<ReferralCodeType> ReferralCodeTypes { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<RegionCurrency> RegionCurrencies { get; set; } = null!;
        public virtual DbSet<RegionImage> RegionImages { get; set; } = null!;
        public virtual DbSet<RegionImageType> RegionImageTypes { get; set; } = null!;
        public virtual DbSet<RegionLanguage> RegionLanguages { get; set; } = null!;
        public virtual DbSet<RepeatType> RepeatTypes { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportType> ReportTypes { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<ReviewType> ReviewTypes { get; set; } = null!;
        public virtual DbSet<RoleUser> RoleUsers { get; set; } = null!;
        public virtual DbSet<SalesGroup> SalesGroups { get; set; } = null!;
        public virtual DbSet<SalesInvoice> SalesInvoices { get; set; } = null!;
        public virtual DbSet<SalesInvoiceContact> SalesInvoiceContacts { get; set; } = null!;
        public virtual DbSet<SalesInvoiceDiscount> SalesInvoiceDiscounts { get; set; } = null!;
        public virtual DbSet<SalesInvoiceEvent> SalesInvoiceEvents { get; set; } = null!;
        public virtual DbSet<SalesInvoiceEventType> SalesInvoiceEventTypes { get; set; } = null!;
        public virtual DbSet<SalesInvoiceFile> SalesInvoiceFiles { get; set; } = null!;
        public virtual DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; } = null!;
        public virtual DbSet<SalesInvoiceItemDiscount> SalesInvoiceItemDiscounts { get; set; } = null!;
        public virtual DbSet<SalesInvoiceItemTarget> SalesInvoiceItemTargets { get; set; } = null!;
        public virtual DbSet<SalesInvoicePayment> SalesInvoicePayments { get; set; } = null!;
        public virtual DbSet<SalesInvoiceState> SalesInvoiceStates { get; set; } = null!;
        public virtual DbSet<SalesInvoiceStatus> SalesInvoiceStatuses { get; set; } = null!;
        public virtual DbSet<SalesInvoiceType> SalesInvoiceTypes { get; set; } = null!;
        public virtual DbSet<SalesItemTargetType> SalesItemTargetTypes { get; set; } = null!;
        public virtual DbSet<SalesOrder> SalesOrders { get; set; } = null!;
        public virtual DbSet<SalesOrderContact> SalesOrderContacts { get; set; } = null!;
        public virtual DbSet<SalesOrderDiscount> SalesOrderDiscounts { get; set; } = null!;
        public virtual DbSet<SalesOrderEvent> SalesOrderEvents { get; set; } = null!;
        public virtual DbSet<SalesOrderEventType> SalesOrderEventTypes { get; set; } = null!;
        public virtual DbSet<SalesOrderFile> SalesOrderFiles { get; set; } = null!;
        public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; } = null!;
        public virtual DbSet<SalesOrderItemDiscount> SalesOrderItemDiscounts { get; set; } = null!;
        public virtual DbSet<SalesOrderItemTarget> SalesOrderItemTargets { get; set; } = null!;
        public virtual DbSet<SalesOrderPayment> SalesOrderPayments { get; set; } = null!;
        public virtual DbSet<SalesOrderPurchaseOrder> SalesOrderPurchaseOrders { get; set; } = null!;
        public virtual DbSet<SalesOrderSalesInvoice> SalesOrderSalesInvoices { get; set; } = null!;
        public virtual DbSet<SalesOrderState> SalesOrderStates { get; set; } = null!;
        public virtual DbSet<SalesOrderStatus> SalesOrderStatuses { get; set; } = null!;
        public virtual DbSet<SalesOrderType> SalesOrderTypes { get; set; } = null!;
        public virtual DbSet<SalesQuote> SalesQuotes { get; set; } = null!;
        public virtual DbSet<SalesQuoteCategory> SalesQuoteCategories { get; set; } = null!;
        public virtual DbSet<SalesQuoteContact> SalesQuoteContacts { get; set; } = null!;
        public virtual DbSet<SalesQuoteDiscount> SalesQuoteDiscounts { get; set; } = null!;
        public virtual DbSet<SalesQuoteEvent> SalesQuoteEvents { get; set; } = null!;
        public virtual DbSet<SalesQuoteEventType> SalesQuoteEventTypes { get; set; } = null!;
        public virtual DbSet<SalesQuoteFile> SalesQuoteFiles { get; set; } = null!;
        public virtual DbSet<SalesQuoteItem> SalesQuoteItems { get; set; } = null!;
        public virtual DbSet<SalesQuoteItemDiscount> SalesQuoteItemDiscounts { get; set; } = null!;
        public virtual DbSet<SalesQuoteItemTarget> SalesQuoteItemTargets { get; set; } = null!;
        public virtual DbSet<SalesQuoteSalesOrder> SalesQuoteSalesOrders { get; set; } = null!;
        public virtual DbSet<SalesQuoteState> SalesQuoteStates { get; set; } = null!;
        public virtual DbSet<SalesQuoteStatus> SalesQuoteStatuses { get; set; } = null!;
        public virtual DbSet<SalesQuoteType> SalesQuoteTypes { get; set; } = null!;
        public virtual DbSet<SalesReturn> SalesReturns { get; set; } = null!;
        public virtual DbSet<SalesReturnContact> SalesReturnContacts { get; set; } = null!;
        public virtual DbSet<SalesReturnDiscount> SalesReturnDiscounts { get; set; } = null!;
        public virtual DbSet<SalesReturnEvent> SalesReturnEvents { get; set; } = null!;
        public virtual DbSet<SalesReturnEventType> SalesReturnEventTypes { get; set; } = null!;
        public virtual DbSet<SalesReturnFile> SalesReturnFiles { get; set; } = null!;
        public virtual DbSet<SalesReturnItem> SalesReturnItems { get; set; } = null!;
        public virtual DbSet<SalesReturnItemDiscount> SalesReturnItemDiscounts { get; set; } = null!;
        public virtual DbSet<SalesReturnItemTarget> SalesReturnItemTargets { get; set; } = null!;
        public virtual DbSet<SalesReturnPayment> SalesReturnPayments { get; set; } = null!;
        public virtual DbSet<SalesReturnReason> SalesReturnReasons { get; set; } = null!;
        public virtual DbSet<SalesReturnSalesOrder> SalesReturnSalesOrders { get; set; } = null!;
        public virtual DbSet<SalesReturnState> SalesReturnStates { get; set; } = null!;
        public virtual DbSet<SalesReturnStatus> SalesReturnStatuses { get; set; } = null!;
        public virtual DbSet<SalesReturnType> SalesReturnTypes { get; set; } = null!;
        public virtual DbSet<SampleRequest> SampleRequests { get; set; } = null!;
        public virtual DbSet<SampleRequestContact> SampleRequestContacts { get; set; } = null!;
        public virtual DbSet<SampleRequestDiscount> SampleRequestDiscounts { get; set; } = null!;
        public virtual DbSet<SampleRequestEvent> SampleRequestEvents { get; set; } = null!;
        public virtual DbSet<SampleRequestEventType> SampleRequestEventTypes { get; set; } = null!;
        public virtual DbSet<SampleRequestFile> SampleRequestFiles { get; set; } = null!;
        public virtual DbSet<SampleRequestItem> SampleRequestItems { get; set; } = null!;
        public virtual DbSet<SampleRequestItemDiscount> SampleRequestItemDiscounts { get; set; } = null!;
        public virtual DbSet<SampleRequestItemTarget> SampleRequestItemTargets { get; set; } = null!;
        public virtual DbSet<SampleRequestState> SampleRequestStates { get; set; } = null!;
        public virtual DbSet<SampleRequestStatus> SampleRequestStatuses { get; set; } = null!;
        public virtual DbSet<SampleRequestType> SampleRequestTypes { get; set; } = null!;
        public virtual DbSet<ScheduledJobConfiguration> ScheduledJobConfigurations { get; set; } = null!;
        public virtual DbSet<ScheduledJobConfigurationSetting> ScheduledJobConfigurationSettings { get; set; } = null!;
        public virtual DbSet<Schema> Schemas { get; set; } = null!;
        public virtual DbSet<Scout> Scouts { get; set; } = null!;
        public virtual DbSet<ScoutCategory> ScoutCategories { get; set; } = null!;
        public virtual DbSet<ScoutCategoryType> ScoutCategoryTypes { get; set; } = null!;
        public virtual DbSet<Server> Servers { get; set; } = null!;
        public virtual DbSet<ServiceArea> ServiceAreas { get; set; } = null!;
        public virtual DbSet<Set> Sets { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<SettingGroup> SettingGroups { get; set; } = null!;
        public virtual DbSet<SettingType> SettingTypes { get; set; } = null!;
        public virtual DbSet<ShipCarrier> ShipCarriers { get; set; } = null!;
        public virtual DbSet<ShipCarrierMethod> ShipCarrierMethods { get; set; } = null!;
        public virtual DbSet<Shipment> Shipments { get; set; } = null!;
        public virtual DbSet<ShipmentEvent> ShipmentEvents { get; set; } = null!;
        public virtual DbSet<ShipmentStatus> ShipmentStatuses { get; set; } = null!;
        public virtual DbSet<ShipmentType> ShipmentTypes { get; set; } = null!;
        public virtual DbSet<SiteDomain> SiteDomains { get; set; } = null!;
        public virtual DbSet<SiteDomainSocialProvider> SiteDomainSocialProviders { get; set; } = null!;
        public virtual DbSet<SocialProvider> SocialProviders { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreAccount> StoreAccounts { get; set; } = null!;
        public virtual DbSet<StoreAuction> StoreAuctions { get; set; } = null!;
        public virtual DbSet<StoreBadge> StoreBadges { get; set; } = null!;
        public virtual DbSet<StoreCategory> StoreCategories { get; set; } = null!;
        public virtual DbSet<StoreContact> StoreContacts { get; set; } = null!;
        public virtual DbSet<StoreCountry> StoreCountries { get; set; } = null!;
        public virtual DbSet<StoreDistrict> StoreDistricts { get; set; } = null!;
        public virtual DbSet<StoreImage> StoreImages { get; set; } = null!;
        public virtual DbSet<StoreImageType> StoreImageTypes { get; set; } = null!;
        public virtual DbSet<StoreInventoryLocation> StoreInventoryLocations { get; set; } = null!;
        public virtual DbSet<StoreInventoryLocationType> StoreInventoryLocationTypes { get; set; } = null!;
        public virtual DbSet<StoreManufacturer> StoreManufacturers { get; set; } = null!;
        public virtual DbSet<StoreProduct> StoreProducts { get; set; } = null!;
        public virtual DbSet<StoreRegion> StoreRegions { get; set; } = null!;
        public virtual DbSet<StoreSubscription> StoreSubscriptions { get; set; } = null!;
        public virtual DbSet<StoreType> StoreTypes { get; set; } = null!;
        public virtual DbSet<StoreUser> StoreUsers { get; set; } = null!;
        public virtual DbSet<StoreVendor> StoreVendors { get; set; } = null!;
        public virtual DbSet<StoredFile> StoredFiles { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<SubscriptionHistory> SubscriptionHistories { get; set; } = null!;
        public virtual DbSet<SubscriptionStatus> SubscriptionStatuses { get; set; } = null!;
        public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; } = null!;
        public virtual DbSet<SubscriptionTypeRepeatType> SubscriptionTypeRepeatTypes { get; set; } = null!;
        public virtual DbSet<SystemLog> SystemLogs { get; set; } = null!;
        public virtual DbSet<TaxCountry> TaxCountries { get; set; } = null!;
        public virtual DbSet<TaxDistrict> TaxDistricts { get; set; } = null!;
        public virtual DbSet<TaxRegion> TaxRegions { get; set; } = null!;
        public virtual DbSet<Uikey> Uikeys { get; set; } = null!;
        public virtual DbSet<Uitranslation> Uitranslations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserClaim> UserClaims { get; set; } = null!;
        public virtual DbSet<UserEventAttendance> UserEventAttendances { get; set; } = null!;
        public virtual DbSet<UserEventAttendanceType> UserEventAttendanceTypes { get; set; } = null!;
        public virtual DbSet<UserFile> UserFiles { get; set; } = null!;
        public virtual DbSet<UserImage> UserImages { get; set; } = null!;
        public virtual DbSet<UserImageType> UserImageTypes { get; set; } = null!;
        public virtual DbSet<UserLogin> UserLogins { get; set; } = null!;
        public virtual DbSet<UserOnlineStatus> UserOnlineStatuses { get; set; } = null!;
        public virtual DbSet<UserProductType> UserProductTypes { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserStatus> UserStatuses { get; set; } = null!;
        public virtual DbSet<UserSupportRequest> UserSupportRequests { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;
        public virtual DbSet<VendorAccount> VendorAccounts { get; set; } = null!;
        public virtual DbSet<VendorImage> VendorImages { get; set; } = null!;
        public virtual DbSet<VendorImageType> VendorImageTypes { get; set; } = null!;
        public virtual DbSet<VendorManufacturer> VendorManufacturers { get; set; } = null!;
        public virtual DbSet<VendorProduct> VendorProducts { get; set; } = null!;
        public virtual DbSet<VendorType> VendorTypes { get; set; } = null!;
        public virtual DbSet<Visit> Visits { get; set; } = null!;
        public virtual DbSet<VisitStatus> VisitStatuses { get; set; } = null!;
        public virtual DbSet<Visitor> Visitors { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<ZipCode> ZipCodes { get; set; } = null!;
        public virtual DbSet<Zone> Zones { get; set; } = null!;
        public virtual DbSet<ZoneStatus> ZoneStatuses { get; set; } = null!;
        public virtual DbSet<ZoneType> ZoneTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQL2019;Initial Catalog=ATHLT_CEF_QA;User Id=sqllogin;Password=!7WKjh8#BvWXg^mSj9nJATQMWQNyP#YWqrQC;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CreditCurrencyId, "IX_CreditCurrencyID");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Credit).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CreditCurrencyId).HasColumnName("CreditCurrencyID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SageId)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("SageID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TaxEntityUseCode)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TaxExemptionNo)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.CreditCurrency)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CreditCurrencyId)
                    .HasConstraintName("FK_Accounts.Account_Currencies.Currency_CreditCurrencyID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Accounts.Account_Accounts.AccountStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Accounts.Account_Accounts.AccountType_TypeID");
            });

            modelBuilder.Entity<AccountAssociation>(entity =>
            {
                entity.ToTable("AccountAssociation", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AccountAssociationMasters)
                    .HasForeignKey(d => d.MasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts.AccountAssociation_Accounts.Account_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AccountAssociationSlaves)
                    .HasForeignKey(d => d.SlaveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts.AccountAssociation_Accounts.Account_SlaveID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AccountAssociations)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Accounts.AccountAssociation_Accounts.AccountAssociationType_TypeID");
            });

            modelBuilder.Entity<AccountAssociationType>(entity =>
            {
                entity.ToTable("AccountAssociationType", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountContact>(entity =>
            {
                entity.ToTable("AccountContact", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.TransmittedToErp).HasColumnName("TransmittedToERP");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AccountContacts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Accounts.AccountContact_Accounts.Account_AccountID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AccountContacts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Accounts.AccountContact_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<AccountCurrency>(entity =>
            {
                entity.ToTable("AccountCurrency", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CustomName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CustomTranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OverrideHtmlCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OverrideHtmlDecimalCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OverrideHtmlSeparatorCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OverrideRawCharacter).HasMaxLength(5);

                entity.Property(e => e.OverrideRawDecimalCharacter).HasMaxLength(5);

                entity.Property(e => e.OverrideRawSeparatorCharacter).HasMaxLength(5);

                entity.Property(e => e.OverrideUnicodeSymbolValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AccountCurrencies)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Accounts.AccountCurrency_Accounts.Account_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AccountCurrencies)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Accounts.AccountCurrency_Currencies.Currency_SlaveID");
            });

            modelBuilder.Entity<AccountFile>(entity =>
            {
                entity.ToTable("AccountFile", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AccountFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Accounts.AccountFile_Accounts.Account_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AccountFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Accounts.AccountFile_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<AccountImage>(entity =>
            {
                entity.ToTable("AccountImage", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AccountImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Accounts.AccountImage_Accounts.Account_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AccountImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Accounts.AccountImage_Accounts.AccountImageType_TypeID");
            });

            modelBuilder.Entity<AccountImageType>(entity =>
            {
                entity.ToTable("AccountImageType", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountPricePoint>(entity =>
            {
                entity.ToTable("AccountPricePoint", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AccountPricePoints)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Accounts.AccountPricePoint_Accounts.Account_AccountID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AccountPricePoints)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Accounts.AccountPricePoint_Pricing.PricePoint_PricePointID");
            });

            modelBuilder.Entity<AccountProduct>(entity =>
            {
                entity.ToTable("AccountProduct", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AccountProducts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Accounts.AccountProduct_Accounts.Account_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AccountProducts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Accounts.AccountProduct_Products.Product_SlaveID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AccountProducts)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Accounts.AccountProduct_Accounts.AccountProductType_TypeID");
            });

            modelBuilder.Entity<AccountProductType>(entity =>
            {
                entity.ToTable("AccountProductType", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountStatus>(entity =>
            {
                entity.ToTable("AccountStatus", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountType", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.AccountTypes)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Accounts.AccountType_Stores.Store_StoreID");
            });

            modelBuilder.Entity<AccountUsageBalance>(entity =>
            {
                entity.ToTable("AccountUsageBalance", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AccountUsageBalances)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Accounts.AccountUsageBalance_Accounts.Account_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AccountUsageBalances)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Accounts.AccountUsageBalance_Products.Product_SlaveID");
            });

            modelBuilder.Entity<AccountUserRole>(entity =>
            {
                entity.ToTable("AccountUserRole", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AccountUserRoles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Accounts.AccountUserRole_Accounts.Account_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AccountUserRoles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Accounts.AccountUserRole_Contacts.UserRole_SlaveID");
            });

            modelBuilder.Entity<Ad>(entity =>
            {
                entity.ToTable("Ad", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ClickCounterId, "IX_ClickCounterID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.ImpressionCounterId, "IX_ImpressionCounterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Caption)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ClickCounterId).HasColumnName("ClickCounterID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ImpressionCounterId).HasColumnName("ImpressionCounterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TargetUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false)
                    .HasColumnName("TargetURL");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.ClickCounter)
                    .WithMany(p => p.AdClickCounters)
                    .HasForeignKey(d => d.ClickCounterId)
                    .HasConstraintName("FK_Advertising.Ad_Counters.Counter_ClickCounterID");

                entity.HasOne(d => d.ImpressionCounter)
                    .WithMany(p => p.AdImpressionCounters)
                    .HasForeignKey(d => d.ImpressionCounterId)
                    .HasConstraintName("FK_Advertising.Ad_Counters.Counter_ImpressionCounterID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Advertising.Ad_Advertising.AdStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Advertising.Ad_Advertising.AdType_TypeID");
            });

            modelBuilder.Entity<AdAccount>(entity =>
            {
                entity.ToTable("AdAccount", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AdAccounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Advertising.AdAccount_Advertising.Ad_AdID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AdAccounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Advertising.AdAccount_Accounts.Account_AccountID");
            });

            modelBuilder.Entity<AdBrand>(entity =>
            {
                entity.ToTable("AdBrand", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AdBrands)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Advertising.AdBrand_Advertising.Ad_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AdBrands)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Advertising.AdBrand_Brands.Brand_SlaveID");
            });

            modelBuilder.Entity<AdFranchise>(entity =>
            {
                entity.ToTable("AdFranchise", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AdFranchises)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Advertising.AdFranchise_Advertising.Ad_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AdFranchises)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Advertising.AdFranchise_Franchises.Franchise_SlaveID");
            });

            modelBuilder.Entity<AdImage>(entity =>
            {
                entity.ToTable("AdImage", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AdImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Advertising.AdImageNew_Advertising.Ad_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AdImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Advertising.AdImageNew_Advertising.AdImageType_TypeID");
            });

            modelBuilder.Entity<AdImageType>(entity =>
            {
                entity.ToTable("AdImageType", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AdStatus>(entity =>
            {
                entity.ToTable("AdStatus", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AdStore>(entity =>
            {
                entity.ToTable("AdStore", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AdStores)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Advertising.AdStore_Advertising.Ad_AdID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AdStores)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Advertising.AdStore_Stores.Store_StoreID");
            });

            modelBuilder.Entity<AdType>(entity =>
            {
                entity.ToTable("AdType", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AdZone>(entity =>
            {
                entity.ToTable("AdZone", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AdZoneAccessId, "IX_AdZoneAccessID");

                entity.HasIndex(e => e.ClickCounterId, "IX_ClickCounterID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.ImpressionCounterId, "IX_ImpressionCounterID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdZoneAccessId).HasColumnName("AdZoneAccessID");

                entity.Property(e => e.ClickCounterId).HasColumnName("ClickCounterID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ImpressionCounterId).HasColumnName("ImpressionCounterID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.AdZoneAccess)
                    .WithMany(p => p.AdZones)
                    .HasForeignKey(d => d.AdZoneAccessId)
                    .HasConstraintName("FK_Advertising.AdZone_Advertising.AdZoneAccess_AdZoneAccessID");

                entity.HasOne(d => d.ClickCounter)
                    .WithMany(p => p.AdZoneClickCounters)
                    .HasForeignKey(d => d.ClickCounterId)
                    .HasConstraintName("FK_Advertising.AdZone_Counters.Counter_ClickCounterID");

                entity.HasOne(d => d.ImpressionCounter)
                    .WithMany(p => p.AdZoneImpressionCounters)
                    .HasForeignKey(d => d.ImpressionCounterId)
                    .HasConstraintName("FK_Advertising.AdZone_Counters.Counter_ImpressionCounterID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AdZones)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Advertising.AdZone_Advertising.Ad_AdID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AdZones)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Advertising.AdZone_Advertising.Zone_ZoneID");
            });

            modelBuilder.Entity<AdZoneAccess>(entity =>
            {
                entity.ToTable("AdZoneAccess", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ClickCounterId, "IX_ClickCounterID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.ImpressionCounterId, "IX_ImpressionCounterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SubscriptionId, "IX_SubscriptionID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.ZoneId, "IX_ZoneID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClickCounterId).HasColumnName("ClickCounterID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ImpressionCounterId).HasColumnName("ImpressionCounterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");

                entity.Property(e => e.ZoneId).HasColumnName("ZoneID");

                entity.HasOne(d => d.ClickCounter)
                    .WithMany(p => p.AdZoneAccessClickCounters)
                    .HasForeignKey(d => d.ClickCounterId)
                    .HasConstraintName("FK_Advertising.AdZoneAccess_Counters.Counter_ClickCounterID");

                entity.HasOne(d => d.ImpressionCounter)
                    .WithMany(p => p.AdZoneAccessImpressionCounters)
                    .HasForeignKey(d => d.ImpressionCounterId)
                    .HasConstraintName("FK_Advertising.AdZoneAccess_Counters.Counter_ImpressionCounterID");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.AdZoneAccesses)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("FK_Advertising.AdZoneAccess_Payments.Subscription_SubscriptionID");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.AdZoneAccesses)
                    .HasForeignKey(d => d.ZoneId)
                    .HasConstraintName("FK_Advertising.AdZoneAccess_Advertising.Zone_ZoneID");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CountryId, "IX_CountryID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.RegionId, "IX_RegionID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CountryCustom).HasMaxLength(100);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode).HasMaxLength(50);

                entity.Property(e => e.RegionCustom).HasMaxLength(100);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.Street1).HasMaxLength(255);

                entity.Property(e => e.Street2).HasMaxLength(255);

                entity.Property(e => e.Street3).HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Geography.Address_Geography.Country_CountryID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Geography.Address_Geography.Region_RegionID");
            });

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_Hangfire.AggregatedCounter");

                entity.ToTable("AggregatedCounter", "Hangfire");

                entity.HasIndex(e => e.ExpireAt, "[IX_HangFire_AggregatedCounter_ExpireAt]");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer", "Questionnaire");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.OptionId, "IX_OptionID");

                entity.HasIndex(e => e.QuestionId, "IX_QuestionID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questionnaire.Answer_Questionnaire.QuestionOption_OptionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questionnaire.Answer_Questionnaire.Question_QuestionID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Questionnaire.Answer_Contacts.User_UserID");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment", "Scheduling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SalesOrderId, "IX_SalesOrderID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.SalesOrder)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.SalesOrderId)
                    .HasConstraintName("FK_Scheduling.Appointment_Ordering.SalesOrder_SalesOrderID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Scheduling.Appointment_Scheduling.AppointmentStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Scheduling.Appointment_Scheduling.AppointmentType_TypeID");
            });

            modelBuilder.Entity<AppointmentStatus>(entity =>
            {
                entity.ToTable("AppointmentStatus", "Scheduling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppointmentType>(entity =>
            {
                entity.ToTable("AppointmentType", "Scheduling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttributeGroup>(entity =>
            {
                entity.ToTable("AttributeGroup", "Attributes");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttributeTab>(entity =>
            {
                entity.ToTable("AttributeTab", "Attributes");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttributeType>(entity =>
            {
                entity.ToTable("AttributeType", "Attributes");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Auction>(entity =>
            {
                entity.ToTable("Auction", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Auctions.Auction_Contacts.Contact_ContactID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Auctions.Auction_Auctions.AuctionStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Auctions)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Auctions.Auction_Auctions.AuctionType_TypeID");
            });

            modelBuilder.Entity<AuctionCategory>(entity =>
            {
                entity.ToTable("AuctionCategory", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.AuctionCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Auctions.AuctionCategory_Auctions.Auction_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.AuctionCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Auctions.AuctionCategory_Categories.Category_SlaveID");
            });

            modelBuilder.Entity<AuctionStatus>(entity =>
            {
                entity.ToTable("AuctionStatus", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuctionType>(entity =>
            {
                entity.ToTable("AuctionType", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Badge>(entity =>
            {
                entity.ToTable("Badge", "Badges");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Badges)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Stores.Badge_Stores.BadgeType_TypeID");
            });

            modelBuilder.Entity<BadgeImage>(entity =>
            {
                entity.ToTable("BadgeImage", "Badges");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BadgeImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.BadgeImage_Stores.Badge_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.BadgeImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Stores.BadgeImage_Stores.BadgeImageType_TypeID");
            });

            modelBuilder.Entity<BadgeImageType>(entity =>
            {
                entity.ToTable("BadgeImageType", "Badges");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BadgeType>(entity =>
            {
                entity.ToTable("BadgeType", "Badges");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.ToTable("Bid", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.ListingId, "IX_ListingID");

                entity.HasIndex(e => e.LotId, "IX_LotID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentBid).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ListingId).HasColumnName("ListingID");

                entity.Property(e => e.LotId).HasColumnName("LotID");

                entity.Property(e => e.MaxBid).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.ListingId)
                    .HasConstraintName("FK_Auctions.Bid_Auctions.Listing_ListingID");

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.LotId)
                    .HasConstraintName("FK_Auctions.Bid_Auctions.Lot_LotID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Auctions.Bid_Auctions.BidStatus_StatusID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Auctions.Bid_Contacts.User_UserID");
            });

            modelBuilder.Entity<BidStatus>(entity =>
            {
                entity.ToTable("BidStatus", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId, "IX_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferProductId, "IX_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId, "IX_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferProductId, "IX_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferCategoryId, "IX_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferProductId, "IX_MinimumOrderDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferCategoryId, "IX_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferProductId, "IX_MinimumOrderQuantityAmountBufferProductID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MinimumForFreeShippingDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferProductId).HasColumnName("MinimumForFreeShippingDollarAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferProductId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountBufferCategoryId).HasColumnName("MinimumOrderDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderDollarAmountBufferProductId).HasColumnName("MinimumOrderDollarAmountBufferProductID");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferCategoryId).HasColumnName("MinimumOrderQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferProductId).HasColumnName("MinimumOrderQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferCategory)
                    .WithMany(p => p.BrandMinimumForFreeShippingDollarAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Brands.Brand_Categories.Category_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferProduct)
                    .WithMany(p => p.BrandMinimumForFreeShippingDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferProductId)
                    .HasConstraintName("FK_Brands.Brand_Products.Product_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferCategory)
                    .WithMany(p => p.BrandMinimumForFreeShippingQuantityAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Brands.Brand_Categories.Category_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferProduct)
                    .WithMany(p => p.BrandMinimumForFreeShippingQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Brands.Brand_Products.Product_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferCategory)
                    .WithMany(p => p.BrandMinimumOrderDollarAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Brands.Brand_Categories.Category_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferProduct)
                    .WithMany(p => p.BrandMinimumOrderDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferProductId)
                    .HasConstraintName("FK_Brands.Brand_Products.Product_MinimumOrderDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferCategory)
                    .WithMany(p => p.BrandMinimumOrderQuantityAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Brands.Brand_Categories.Category_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferProduct)
                    .WithMany(p => p.BrandMinimumOrderQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Brands.Brand_Products.Product_MinimumOrderQuantityAmountBufferProductID");
            });

            modelBuilder.Entity<BrandAccount>(entity =>
            {
                entity.ToTable("BrandAccount", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.PricePointId, "IX_PricePointID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.PricePointId).HasColumnName("PricePointID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandAccounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Brands.BrandAccount_Brands.Brand_MasterID");

                entity.HasOne(d => d.PricePoint)
                    .WithMany(p => p.BrandAccounts)
                    .HasForeignKey(d => d.PricePointId)
                    .HasConstraintName("FK_Brands.BrandAccount_Pricing.PricePoint_PricePointID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandAccounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Brands.BrandAccount_Accounts.Account_SlaveID");
            });

            modelBuilder.Entity<BrandAuction>(entity =>
            {
                entity.ToTable("BrandAuction", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandAuctions)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Auctions.BrandAuction_Brands.Brand_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandAuctions)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Auctions.BrandAuction_Auctions.Auction_SlaveID");
            });

            modelBuilder.Entity<BrandCategory>(entity =>
            {
                entity.ToTable("BrandCategory", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Brands.BrandCategory_Brands.Brand_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Brands.BrandCategory_Categories.Category_SlaveID");
            });

            modelBuilder.Entity<BrandCurrency>(entity =>
            {
                entity.ToTable("BrandCurrency", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CustomName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CustomTranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OverrideHtmlCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OverrideHtmlDecimalCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OverrideHtmlSeparatorCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OverrideRawCharacter).HasMaxLength(5);

                entity.Property(e => e.OverrideRawDecimalCharacter).HasMaxLength(5);

                entity.Property(e => e.OverrideRawSeparatorCharacter).HasMaxLength(5);

                entity.Property(e => e.OverrideUnicodeSymbolValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandCurrencies)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Brands.BrandCurrency_Brands.Brand_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandCurrencies)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Brands.BrandCurrency_Currencies.Currency_SlaveID");
            });

            modelBuilder.Entity<BrandFranchise>(entity =>
            {
                entity.ToTable("BrandFranchise", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandFranchises)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Brands.BrandFranchise_Brands.Brand_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandFranchises)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Brands.BrandFranchise_Franchises.Franchise_SlaveID");
            });

            modelBuilder.Entity<BrandImage>(entity =>
            {
                entity.ToTable("BrandImage", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.BrandImage_Stores.Brand_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.BrandImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Stores.BrandImage_Stores.BrandImageType_TypeID");
            });

            modelBuilder.Entity<BrandImageType>(entity =>
            {
                entity.ToTable("BrandImageType", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BrandInventoryLocation>(entity =>
            {
                entity.ToTable("BrandInventoryLocation", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandInventoryLocations)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Brands.BrandInventoryLocation_Brands.Brand_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandInventoryLocations)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Brands.BrandInventoryLocation_Inventory.InventoryLocation_SlaveID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.BrandInventoryLocations)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Brands.BrandInventoryLocation_Brands.BrandInventoryLocationType_TypeID");
            });

            modelBuilder.Entity<BrandInventoryLocationType>(entity =>
            {
                entity.ToTable("BrandInventoryLocationType", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BrandLanguage>(entity =>
            {
                entity.ToTable("BrandLanguage", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.OverrideLocale, "IX_OverrideLocale");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OverrideIso63912002)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("OverrideISO639_1_2002");

                entity.Property(e => e.OverrideIso63921998)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("OverrideISO639_2_1998");

                entity.Property(e => e.OverrideIso63932007)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("OverrideISO639_3_2007");

                entity.Property(e => e.OverrideIso63952008)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("OverrideISO639_5_2008");

                entity.Property(e => e.OverrideLocale).HasMaxLength(128);

                entity.Property(e => e.OverrideUnicodeName).HasMaxLength(1024);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandLanguages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Brands.BrandLanguage_Brands.Brand_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandLanguages)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Brands.BrandLanguage_Globalization.Language_SlaveID");
            });

            modelBuilder.Entity<BrandManufacturer>(entity =>
            {
                entity.ToTable("BrandManufacturer", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandManufacturers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Brands.BrandManufacturer_Brands.Brand_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandManufacturers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Brands.BrandManufacturer_Manufacturers.Manufacturer_SlaveID");
            });

            modelBuilder.Entity<BrandProduct>(entity =>
            {
                entity.ToTable("BrandProduct", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.PriceBase).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceMsrp).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceReduction).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceSale).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandProducts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Brands.BrandProduct_Brands.Brand_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandProducts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Brands.BrandProduct_Products.Product_SlaveID");
            });

            modelBuilder.Entity<BrandSiteDomain>(entity =>
            {
                entity.ToTable("BrandSiteDomain", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandSiteDomains)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.BrandSiteDomain_Stores.Brand_BrandID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandSiteDomains)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.BrandSiteDomain_Stores.SiteDomain_SiteDomainID");
            });

            modelBuilder.Entity<BrandStore>(entity =>
            {
                entity.ToTable("BrandStore", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandStores)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.BrandStore_Stores.Brand_BrandID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandStores)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.BrandStore_Stores.Store_StoreID");
            });

            modelBuilder.Entity<BrandUser>(entity =>
            {
                entity.ToTable("BrandUser", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandUsers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Brands.BrandUser_Brands.Brand_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandUsers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Brands.BrandUser_Contacts.User_SlaveID");
            });

            modelBuilder.Entity<BrandVendor>(entity =>
            {
                entity.ToTable("BrandVendor", "Brands");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.BrandVendors)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Brands.BrandVendor_Brands.Brand_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.BrandVendors)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Brands.BrandVendor_Vendors.Vendor_SlaveID");
            });

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.ToTable("Calendar", "Scheduling");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FridayHoursEnd).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FridayHoursStart).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MondayHoursEnd).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MondayHoursStart).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SaturdayHoursEnd).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SaturdayHoursStart).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SundayHoursEnd).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SundayHoursStart).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ThursdayHoursEnd).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ThursdayHoursStart).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TuesdayHoursEnd).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TuesdayHoursStart).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WednesdayHoursEnd).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WednesdayHoursStart).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Calendars)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Scheduling.Calendar_Accounts.Account_AccountID");
            });

            modelBuilder.Entity<CalendarAppointment>(entity =>
            {
                entity.ToTable("CalendarAppointment", "Scheduling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CalendarAppointments)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Scheduling.CalendarAppointment_Scheduling.Calendar_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CalendarAppointments)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Scheduling.CalendarAppointment_Scheduling.Appointment_SlaveID");
            });

            modelBuilder.Entity<CalendarEvent>(entity =>
            {
                entity.ToTable("CalendarEvent", "CalendarEvents");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.GroupId, "IX_GroupID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EventDurationUnitOfMeasure)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RecurrenceString)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.CalendarEvents)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEvent_Contacts.Contact_ContactID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.CalendarEvents)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEvent_Groups.Group_GroupID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CalendarEvents)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEvent_CalendarEvents.CalendarEventStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CalendarEvents)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEvent_CalendarEvents.CalendarEventType_TypeID");
            });

            modelBuilder.Entity<CalendarEventDetail>(entity =>
            {
                entity.ToTable("CalendarEventDetail", "CalendarEvents");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CalendarEventId, "IX_CalendarEventID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CalendarEventId).HasColumnName("CalendarEventID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.CalendarEvent)
                    .WithMany(p => p.CalendarEventDetails)
                    .HasForeignKey(d => d.CalendarEventId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEventDetail_CalendarEvents.CalendarEvent_CalendarEventID");
            });

            modelBuilder.Entity<CalendarEventFile>(entity =>
            {
                entity.ToTable("CalendarEventFile", "CalendarEvents");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CalendarEventFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEventFileNew_CalendarEvents.CalendarEvent_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CalendarEventFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEventFileNew_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<CalendarEventImage>(entity =>
            {
                entity.ToTable("CalendarEventImage", "CalendarEvents");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CalendarEventImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEventImage_CalendarEvents.CalendarEvent_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CalendarEventImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEventImage_CalendarEvents.CalendarEventImageType_TypeID");
            });

            modelBuilder.Entity<CalendarEventImageType>(entity =>
            {
                entity.ToTable("CalendarEventImageType", "CalendarEvents");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CalendarEventProduct>(entity =>
            {
                entity.ToTable("CalendarEventProducts", "CalendarEvents");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CalendarEventProducts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEventProducts_CalendarEvents.CalendarEvent_CalendarEventID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CalendarEventProducts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_CalendarEvents.CalendarEventProducts_Products.Product_ProductID");
            });

            modelBuilder.Entity<CalendarEventStatus>(entity =>
            {
                entity.ToTable("CalendarEventStatus", "CalendarEvents");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CalendarEventType>(entity =>
            {
                entity.ToTable("CalendarEventType", "CalendarEvents");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("Campaign", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedByUserId, "IX_CreatedByUserID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BudgetedCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CodeName).HasMaxLength(32);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ExchangeRate).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.ExpectedRevenue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Message).HasMaxLength(256);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OtherCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PromotionCodeName).HasMaxLength(128);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TotalActualCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalCampaignActivityActualCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UtcconversionTimeZoneCode).HasColumnName("UTCConversionTimeZoneCode");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .HasConstraintName("FK_Tracking.Campaign_Contacts.User_CreatedByUserID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Tracking.Campaign_Tracking.CampaignStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Tracking.Campaign_Tracking.CampaignType_TypeID");
            });

            modelBuilder.Entity<CampaignAd>(entity =>
            {
                entity.ToTable("CampaignAd", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CampaignAds)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Tracking.CampaignAd_Tracking.Campaign_CampaignID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CampaignAds)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Tracking.CampaignAd_Advertising.Ad_AdID");
            });

            modelBuilder.Entity<CampaignStatus>(entity =>
            {
                entity.ToTable("CampaignStatus", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CampaignType>(entity =>
            {
                entity.ToTable("CampaignType", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.CampaignTypes)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Tracking.CampaignType_Brands.Brand_BrandID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.CampaignTypes)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Tracking.CampaignType_Stores.Store_StoreID");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart", "Shopping");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BillingContactId, "IX_BillingContactID");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FranchiseId, "IX_FranchiseID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.SessionId, "IX_SessionID");

                entity.HasIndex(e => e.ShipmentId, "IX_ShipmentID");

                entity.HasIndex(e => e.ShippingContactId, "IX_ShippingContactID");

                entity.HasIndex(e => e.StateId, "IX_StateID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.HasIndex(e => new { e.Active, e.SessionId, e.TypeId, e.UserId, e.AccountId, e.BrandId, e.FranchiseId, e.StoreId }, "Unq_ByCartClusterRequirements")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BillingContactId).HasColumnName("BillingContactID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseId).HasColumnName("FranchiseID");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");

                entity.Property(e => e.ShippingContactId).HasColumnName("ShippingContactID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.SubtotalDiscounts).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalDiscountsModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalFees).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalFeesModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalHandling).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalHandlingModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalItems).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalShipping).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalShippingModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalTaxes).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalTaxesModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Shopping.Cart_Accounts.Account_AccountID");

                entity.HasOne(d => d.BillingContact)
                    .WithMany(p => p.CartBillingContacts)
                    .HasForeignKey(d => d.BillingContactId)
                    .HasConstraintName("FK_Shopping.Cart_Contacts.Contact_BillingContactID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Shopping.Cart_Brands.Brand_BrandID");

                entity.HasOne(d => d.Franchise)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.FranchiseId)
                    .HasConstraintName("FK_Shopping.Cart_Franchises.Franchise_FranchiseID");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ShipmentId)
                    .HasConstraintName("FK_Shopping.Cart_Shipping.Shipment_ShipmentID");

                entity.HasOne(d => d.ShippingContact)
                    .WithMany(p => p.CartShippingContacts)
                    .HasForeignKey(d => d.ShippingContactId)
                    .HasConstraintName("FK_Shopping.Cart_Contacts.Contact_ShippingContactID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Shopping.Cart_Shopping.CartState_StateID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Shopping.Cart_Shopping.CartStatus_StatusID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Shopping.Cart_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Shopping.Cart_Shopping.CartType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Shopping.Cart_Contacts.User_UserID");
            });

            modelBuilder.Entity<CartContact>(entity =>
            {
                entity.ToTable("CartContact", "Shopping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SampleRequestId, "IX_SampleRequest_ID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SampleRequestId).HasColumnName("SampleRequest_ID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CartContacts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Shopping.CartContact_Shopping.Cart_CartID");

                entity.HasOne(d => d.SampleRequest)
                    .WithMany(p => p.CartContacts)
                    .HasForeignKey(d => d.SampleRequestId)
                    .HasConstraintName("FK_Shopping.CartContact_Sampling.SampleRequest_SampleRequest_ID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CartContacts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Shopping.CartContact_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<CartDiscount>(entity =>
            {
                entity.ToTable("CartDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CartDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.CartDiscounts_Shopping.Cart_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CartDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.CartDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<CartEvent>(entity =>
            {
                entity.ToTable("CartEvent", "Shopping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NewStateId).HasColumnName("NewStateID");

                entity.Property(e => e.NewStatusId).HasColumnName("NewStatusID");

                entity.Property(e => e.NewTypeId).HasColumnName("NewTypeID");

                entity.Property(e => e.OldStateId).HasColumnName("OldStateID");

                entity.Property(e => e.OldStatusId).HasColumnName("OldStatusID");

                entity.Property(e => e.OldTypeId).HasColumnName("OldTypeID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CartEvents)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Shopping.CartEvent_Shopping.Cart_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CartEvents)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Shopping.CartEvent_Shopping.CartEventType_TypeID");
            });

            modelBuilder.Entity<CartEventType>(entity =>
            {
                entity.ToTable("CartEventType", "Shopping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CartFile>(entity =>
            {
                entity.ToTable("CartFile", "Shopping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CartFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Shopping.CartFileNew_Shopping.Cart_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CartFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Shopping.CartFileNew_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("CartItem", "Shopping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.OriginalCurrencyId, "IX_OriginalCurrencyID");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.SellingCurrencyId, "IX_SellingCurrencyID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ForceUniqueLineItemKey)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalCurrencyId).HasColumnName("OriginalCurrencyID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityBackOrdered).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPreSold).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SellingCurrencyId).HasColumnName("SellingCurrencyID");

                entity.Property(e => e.Sku)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCorePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitCorePriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitSoldPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitSoldPriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitSoldPriceModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Shopping.CartItem_Shopping.Cart_MasterID");

                entity.HasOne(d => d.OriginalCurrency)
                    .WithMany(p => p.CartItemOriginalCurrencies)
                    .HasForeignKey(d => d.OriginalCurrencyId)
                    .HasConstraintName("FK_Shopping.CartItem_Currencies.Currency_OriginalCurrencyID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Shopping.CartItem_Products.Product_ProductID");

                entity.HasOne(d => d.SellingCurrency)
                    .WithMany(p => p.CartItemSellingCurrencies)
                    .HasForeignKey(d => d.SellingCurrencyId)
                    .HasConstraintName("FK_Shopping.CartItem_Currencies.Currency_SellingCurrencyID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Shopping.CartItem_Contacts.User_UserID");
            });

            modelBuilder.Entity<CartItemDiscount>(entity =>
            {
                entity.ToTable("CartItemDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CartItemDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.CartItemDiscounts_Shopping.CartItem_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CartItemDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.CartItemDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<CartItemTarget>(entity =>
            {
                entity.ToTable("CartItemTarget", "Shopping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandProductId, "IX_BrandProductID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DestinationContactId, "IX_DestinationContactID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.OriginProductInventoryLocationSectionId, "IX_OriginProductInventoryLocationSectionID");

                entity.HasIndex(e => e.OriginStoreProductId, "IX_OriginStoreProductID");

                entity.HasIndex(e => e.OriginVendorProductId, "IX_OriginVendorProductID");

                entity.HasIndex(e => e.SelectedRateQuoteId, "IX_SelectedRateQuoteID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandProductId).HasColumnName("BrandProductID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationContactId).HasColumnName("DestinationContactID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OriginProductInventoryLocationSectionId).HasColumnName("OriginProductInventoryLocationSectionID");

                entity.Property(e => e.OriginStoreProductId).HasColumnName("OriginStoreProductID");

                entity.Property(e => e.OriginVendorProductId).HasColumnName("OriginVendorProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SelectedRateQuoteId).HasColumnName("SelectedRateQuoteID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.BrandProduct)
                    .WithMany(p => p.CartItemTargets)
                    .HasForeignKey(d => d.BrandProductId)
                    .HasConstraintName("FK_Shopping.CartItemTarget_Brands.BrandProduct_BrandProductID");

                entity.HasOne(d => d.DestinationContact)
                    .WithMany(p => p.CartItemTargets)
                    .HasForeignKey(d => d.DestinationContactId)
                    .HasConstraintName("FK_Shopping.CartItemTarget_Contacts.Contact_DestinationContactID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CartItemTargets)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Shopping.CartItemTarget_Shopping.CartItem_MasterID");

                entity.HasOne(d => d.OriginProductInventoryLocationSection)
                    .WithMany(p => p.CartItemTargets)
                    .HasForeignKey(d => d.OriginProductInventoryLocationSectionId)
                    .HasConstraintName("FK_Shopping.CartItemTarget_Products.ProductInventoryLocationSection_OriginProductInventoryLocationSectionID");

                entity.HasOne(d => d.OriginStoreProduct)
                    .WithMany(p => p.CartItemTargets)
                    .HasForeignKey(d => d.OriginStoreProductId)
                    .HasConstraintName("FK_Shopping.CartItemTarget_Stores.StoreProduct_OriginStoreProductID");

                entity.HasOne(d => d.OriginVendorProduct)
                    .WithMany(p => p.CartItemTargets)
                    .HasForeignKey(d => d.OriginVendorProductId)
                    .HasConstraintName("FK_Shopping.CartItemTarget_Vendors.VendorProduct_OriginVendorProductID");

                entity.HasOne(d => d.SelectedRateQuote)
                    .WithMany(p => p.CartItemTargets)
                    .HasForeignKey(d => d.SelectedRateQuoteId)
                    .HasConstraintName("FK_Shopping.CartItemTarget_Shipping.RateQuote_SelectedRateQuoteID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CartItemTargets)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Shopping.CartItemTarget_Sales.SalesItemTargetType_TypeID");
            });

            modelBuilder.Entity<CartState>(entity =>
            {
                entity.ToTable("CartState", "Shopping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CartStatus>(entity =>
            {
                entity.ToTable("CartStatus", "Shopping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CartType>(entity =>
            {
                entity.ToTable("CartType", "Shopping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedByUserId, "IX_CreatedByUserID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.CartTypes)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Shopping.CartType_Brands.Brand_BrandID");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.CartTypes)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .HasConstraintName("FK_Shopping.CartType_Contacts.User_CreatedByUserID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.CartTypes)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Shopping.CartType_Stores.Store_StoreID");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "Categories");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId, "IX_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferProductId, "IX_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId, "IX_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferProductId, "IX_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferCategoryId, "IX_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferProductId, "IX_MinimumOrderDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferCategoryId, "IX_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferProductId, "IX_MinimumOrderQuantityAmountBufferProductID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.ParentId, "IX_ParentID");

                entity.HasIndex(e => e.RestockingFeeAmountCurrencyId, "IX_RestockingFeeAmountCurrencyID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.HandlingCharge).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferProductId).HasColumnName("MinimumForFreeShippingDollarAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferProductId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountBufferCategoryId).HasColumnName("MinimumOrderDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderDollarAmountBufferProductId).HasColumnName("MinimumOrderDollarAmountBufferProductID");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferCategoryId).HasColumnName("MinimumOrderQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferProductId).HasColumnName("MinimumOrderQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.RequiresRoles).HasMaxLength(512);

                entity.Property(e => e.RequiresRolesAlt).HasMaxLength(512);

                entity.Property(e => e.RestockingFeeAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RestockingFeeAmountCurrencyId).HasColumnName("RestockingFeeAmountCurrencyID");

                entity.Property(e => e.RestockingFeePercent).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferCategory)
                    .WithMany(p => p.InverseMinimumForFreeShippingDollarAmountBufferCategory)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Categories.Category_Categories.Category_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferProduct)
                    .WithMany(p => p.CategoryMinimumForFreeShippingDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferProductId)
                    .HasConstraintName("FK_Categories.Category_Products.Product_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferCategory)
                    .WithMany(p => p.InverseMinimumForFreeShippingQuantityAmountBufferCategory)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Categories.Category_Categories.Category_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferProduct)
                    .WithMany(p => p.CategoryMinimumForFreeShippingQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Categories.Category_Products.Product_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferCategory)
                    .WithMany(p => p.InverseMinimumOrderDollarAmountBufferCategory)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Categories.Category_Categories.Category_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferProduct)
                    .WithMany(p => p.CategoryMinimumOrderDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferProductId)
                    .HasConstraintName("FK_Categories.Category_Products.Product_MinimumOrderDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferCategory)
                    .WithMany(p => p.InverseMinimumOrderQuantityAmountBufferCategory)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Categories.Category_Categories.Category_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferProduct)
                    .WithMany(p => p.CategoryMinimumOrderQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Categories.Category_Products.Product_MinimumOrderQuantityAmountBufferProductID");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Categories.Category_Categories.Category_ParentID");

                entity.HasOne(d => d.RestockingFeeAmountCurrency)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.RestockingFeeAmountCurrencyId)
                    .HasConstraintName("FK_Categories.Category_Currencies.Currency_RestockingFeeAmountCurrencyID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Categories.Category_Categories.CategoryType_TypeID");
            });

            modelBuilder.Entity<CategoryFile>(entity =>
            {
                entity.ToTable("CategoryFile", "Categories");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CategoryFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Categories.CategoryFileNew_Categories.Category_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CategoryFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Categories.CategoryFileNew_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<CategoryImage>(entity =>
            {
                entity.ToTable("CategoryImage", "Categories");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CategoryImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Categories.CategoryImageNew_Categories.Category_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CategoryImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Categories.CategoryImageNew_Categories.CategoryImageType_TypeID");
            });

            modelBuilder.Entity<CategoryImageType>(entity =>
            {
                entity.ToTable("CategoryImageType", "Categories");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoryType>(entity =>
            {
                entity.ToTable("CategoryType", "Categories");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AddressId, "IX_AddressID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Email1)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Fax1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(300);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.Phone1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Website1)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Contacts.Contact_Geography.Address_AddressID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Contacts.Contact_Contacts.ContactType_TypeID");
            });

            modelBuilder.Entity<ContactImage>(entity =>
            {
                entity.ToTable("ContactImage", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ContactImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Contacts.ContactImage_Contacts.Contact_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ContactImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Contacts.ContactImage_Contacts.ContactImageType_TypeID");
            });

            modelBuilder.Entity<ContactImageType>(entity =>
            {
                entity.ToTable("ContactImageType", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.ToTable("ContactType", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contractor>(entity =>
            {
                entity.ToTable("Contractor", "Accounts");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Contractors)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Accounts.Contractor_Accounts.Account_AccountID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Contractors)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Accounts.Contractor_Stores.Store_StoreID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Contractors)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Accounts.Contractor_Contacts.User_UserID");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversation", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Conversations)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Messaging.Conversation_Brands.Brand_BrandID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Conversations)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Messaging.Conversation_Stores.Store_StoreID");
            });

            modelBuilder.Entity<ConversationUser>(entity =>
            {
                entity.ToTable("ConversationUser", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ConversationUsers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Messaging.ConversationUser_Messaging.Conversation_ConversationID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ConversationUsers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Messaging.ConversationUser_Contacts.User_UserID");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.ToTable("Counter", "Counters");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Counters)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Counters.Counter_Counters.CounterType_TypeID");
            });

            modelBuilder.Entity<Counter1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Counter", "Hangfire");

                entity.HasIndex(e => e.Key, "CX_HangFire_Counter")
                    .IsClustered();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key).HasMaxLength(100);
            });

            modelBuilder.Entity<CounterLog>(entity =>
            {
                entity.ToTable("CounterLog", "Counters");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CounterId, "IX_CounterID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CounterId).HasColumnName("CounterID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Counter)
                    .WithMany(p => p.CounterLogs)
                    .HasForeignKey(d => d.CounterId)
                    .HasConstraintName("FK_Counters.CounterLog_Counters.Counter_CounterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CounterLogs)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Counters.CounterLog_Counters.CounterLogType_TypeID");
            });

            modelBuilder.Entity<CounterLogType>(entity =>
            {
                entity.ToTable("CounterLogType", "Counters");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CounterType>(entity =>
            {
                entity.ToTable("CounterType", "Counters");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Iso3166alpha2)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ISO3166Alpha2");

                entity.Property(e => e.Iso3166alpha3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ISO3166Alpha3");

                entity.Property(e => e.Iso3166numeric).HasColumnName("ISO3166Numeric");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PhonePrefix)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneRegEx)
                    .HasMaxLength(512)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CountryCurrency>(entity =>
            {
                entity.ToTable("CountryCurrency", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CountryCurrencies)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Geography.CountryCurrency_Geography.Country_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CountryCurrencies)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Geography.CountryCurrency_Currencies.Currency_CurrencyID");
            });

            modelBuilder.Entity<CountryImage>(entity =>
            {
                entity.ToTable("CountryImage", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CountryImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Geography.CountryImage_Geography.Country_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CountryImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Geography.CountryImage_Geography.CountryImageType_TypeID");
            });

            modelBuilder.Entity<CountryImageType>(entity =>
            {
                entity.ToTable("CountryImageType", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CountryLanguage>(entity =>
            {
                entity.ToTable("CountryLanguage", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CountryLanguages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Geography.CountryLanguage_Geography.Country_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.CountryLanguages)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Geography.CountryLanguage_Globalization.Language_LanguageID");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency", "Currencies");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.HtmlCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.HtmlDecimalCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.HtmlSeparatorCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Iso4217alpha)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ISO4217Alpha");

                entity.Property(e => e.Iso4217numeric).HasColumnName("ISO4217Numeric");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RawCharacter).HasMaxLength(5);

                entity.Property(e => e.RawDecimalCharacter).HasMaxLength(5);

                entity.Property(e => e.RawSeparatorCharacter).HasMaxLength(5);

                entity.Property(e => e.UnicodeSymbolValue).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<CurrencyConversion>(entity =>
            {
                entity.ToTable("CurrencyConversion", "Currencies");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.EndingCurrencyId, "IX_EndingCurrencyID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.StartingCurrencyId, "IX_StartingCurrencyID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.EndingCurrencyId).HasColumnName("EndingCurrencyID");

                entity.Property(e => e.Rate).HasColumnType("decimal(24, 20)");

                entity.Property(e => e.StartingCurrencyId).HasColumnName("StartingCurrencyID");

                entity.HasOne(d => d.EndingCurrency)
                    .WithMany(p => p.CurrencyConversionEndingCurrencies)
                    .HasForeignKey(d => d.EndingCurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Currencies.CurrencyConversion_Currencies.Currency_EndingCurrencyID");

                entity.HasOne(d => d.StartingCurrency)
                    .WithMany(p => p.CurrencyConversionStartingCurrencies)
                    .HasForeignKey(d => d.StartingCurrencyId)
                    .HasConstraintName("FK_Currencies.CurrencyConversion_Currencies.Currency_StartingCurrencyID");
            });

            modelBuilder.Entity<CurrencyImage>(entity =>
            {
                entity.ToTable("CurrencyImage", "Currencies");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.CurrencyImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Currencies.CurrencyImage_Currencies.Currency_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CurrencyImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Currencies.CurrencyImage_Currencies.CurrencyImageType_TypeID");
            });

            modelBuilder.Entity<CurrencyImageType>(entity =>
            {
                entity.ToTable("CurrencyImageType", "Currencies");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discount", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BuyXvalue)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("BuyXValue");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DiscountTypeId).HasColumnName("DiscountTypeID");

                entity.Property(e => e.GetYvalue)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("GetYValue");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ThresholdAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<DiscountAccount>(entity =>
            {
                entity.ToTable("DiscountAccount", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountAccounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountAccount_Discounts.Discount_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountAccounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountAccount_Accounts.Account_SlaveID");
            });

            modelBuilder.Entity<DiscountAccountType>(entity =>
            {
                entity.ToTable("DiscountAccountType", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountAccountTypes)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountAccountType_Discounts.Discount_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountAccountTypes)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountAccountType_Accounts.AccountType_SlaveID");
            });

            modelBuilder.Entity<DiscountBrand>(entity =>
            {
                entity.ToTable("DiscountBrands", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountBrands)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountBrands_Discounts.Discount_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountBrands)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountBrands_Brands.Brand_SlaveID");
            });

            modelBuilder.Entity<DiscountCategory>(entity =>
            {
                entity.ToTable("DiscountCategories", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountCategories_Discounts.Discount_DiscountID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountCategories_Categories.Category_CategoryID");
            });

            modelBuilder.Entity<DiscountCode>(entity =>
            {
                entity.ToTable("DiscountCode", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DiscountId, "IX_DiscountID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.DiscountCodes)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK_Discounts.DiscountCode_Discounts.Discount_DiscountID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DiscountCodes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Discounts.DiscountCode_Contacts.User_UserID");
            });

            modelBuilder.Entity<DiscountCountry>(entity =>
            {
                entity.ToTable("DiscountCountry", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountCountries)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountCountry_Discounts.Discount_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountCountries)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountCountry_Geography.Country_SlaveID");
            });

            modelBuilder.Entity<DiscountFranchise>(entity =>
            {
                entity.ToTable("DiscountFranchises", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountFranchises)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountFranchises_Discounts.Discount_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountFranchises)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountFranchises_Franchises.Franchise_SlaveID");
            });

            modelBuilder.Entity<DiscountManufacturer>(entity =>
            {
                entity.ToTable("DiscountManufacturer", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountManufacturers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountManufacturer_Discounts.Discount_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountManufacturers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountManufacturer_Manufacturers.Manufacturer_SlaveID");
            });

            modelBuilder.Entity<DiscountProduct>(entity =>
            {
                entity.ToTable("DiscountProducts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountProducts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountProducts_Discounts.Discount_DiscountID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountProducts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountProducts_Products.Product_ProductID");
            });

            modelBuilder.Entity<DiscountProductType>(entity =>
            {
                entity.ToTable("DiscountProductType", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountProductTypes)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountProductType_Discounts.Discount_DiscountID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountProductTypes)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountProductType_Products.ProductType_ProductTypeID");
            });

            modelBuilder.Entity<DiscountShipCarrierMethod>(entity =>
            {
                entity.ToTable("DiscountShipCarrierMethods", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountShipCarrierMethods)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountShipCarrierMethods_Discounts.Discount_DiscountID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountShipCarrierMethods)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountShipCarrierMethods_Shipping.ShipCarrierMethod_ShipCarrierMethodID");
            });

            modelBuilder.Entity<DiscountStore>(entity =>
            {
                entity.ToTable("DiscountStores", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountStores)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountStores_Discounts.Discount_DiscountID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountStores)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountStores_Stores.Store_StoreID");
            });

            modelBuilder.Entity<DiscountUser>(entity =>
            {
                entity.ToTable("DiscountUser", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountUsers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountUser_Discounts.Discount_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountUsers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountUser_Contacts.User_SlaveID");
            });

            modelBuilder.Entity<DiscountUserRole>(entity =>
            {
                entity.ToTable("DiscountUserRole", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountUserRoles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountUserRole_Discounts.Discount_MasterID");
            });

            modelBuilder.Entity<DiscountVendor>(entity =>
            {
                entity.ToTable("DiscountVendor", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DiscountVendors)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.DiscountVendor_Discounts.Discount_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DiscountVendors)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.DiscountVendor_Vendors.Vendor_SlaveID");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CountryId, "IX_CountryID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.RegionId, "IX_RegionID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Geography.District_Geography.Country_CountryID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Geography.District_Geography.Region_RegionID");
            });

            modelBuilder.Entity<DistrictCurrency>(entity =>
            {
                entity.ToTable("DistrictCurrency", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DistrictCurrencies)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Geography.DistrictCurrency_Geography.District_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DistrictCurrencies)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Geography.DistrictCurrency_Currencies.Currency_SlaveID");
            });

            modelBuilder.Entity<DistrictImage>(entity =>
            {
                entity.ToTable("DistrictImage", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DistrictImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Geography.DistrictImage_Geography.District_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.DistrictImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Geography.DistrictImage_Geography.DistrictImageType_TypeID");
            });

            modelBuilder.Entity<DistrictImageType>(entity =>
            {
                entity.ToTable("DistrictImageType", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DistrictLanguage>(entity =>
            {
                entity.ToTable("DistrictLanguage", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DistrictLanguages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Geography.DistrictLanguage_Geography.District_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.DistrictLanguages)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Geography.DistrictLanguage_Globalization.Language_SlaveID");
            });

            modelBuilder.Entity<EmailQueue>(entity =>
            {
                entity.ToTable("EmailQueue", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.EmailTemplateId, "IX_EmailTemplateID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MessageRecipientId, "IX_MessageRecipientID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressFrom)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.AddressesBcc)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.AddressesCc)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.AddressesTo)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.EmailTemplateId).HasColumnName("EmailTemplateID");

                entity.Property(e => e.MessageRecipientId).HasColumnName("MessageRecipientID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Subject).HasMaxLength(255);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.EmailTemplate)
                    .WithMany(p => p.EmailQueues)
                    .HasForeignKey(d => d.EmailTemplateId)
                    .HasConstraintName("FK_Messaging.EmailQueue_Messaging.EmailTemplate_EmailTemplateID");

                entity.HasOne(d => d.MessageRecipient)
                    .WithMany(p => p.EmailQueues)
                    .HasForeignKey(d => d.MessageRecipientId)
                    .HasConstraintName("FK_Messaging.EmailQueue_Messaging.MessageRecipient_MessageRecipientID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.EmailQueues)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Messaging.EmailQueue_Messaging.EmailStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.EmailQueues)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Messaging.EmailQueue_Messaging.EmailType_TypeID");
            });

            modelBuilder.Entity<EmailQueueAttachment>(entity =>
            {
                entity.ToTable("EmailQueueAttachment", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedByUserId, "IX_CreatedByUserID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedByUserId, "IX_UpdatedByUserID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.UpdatedByUserId).HasColumnName("UpdatedByUserID");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.EmailQueueAttachmentCreatedByUsers)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .HasConstraintName("FK_Messaging.EmailQueueAttachment_Contacts.User_CreatedByUserID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.EmailQueueAttachments)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Messaging.EmailQueueAttachment_Messaging.EmailQueue_EmailQueueID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.EmailQueueAttachments)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Messaging.EmailQueueAttachment_Media.StoredFile_StoredFileID");

                entity.HasOne(d => d.UpdatedByUser)
                    .WithMany(p => p.EmailQueueAttachmentUpdatedByUsers)
                    .HasForeignKey(d => d.UpdatedByUserId)
                    .HasConstraintName("FK_Messaging.EmailQueueAttachment_Contacts.User_UpdatedByUserID");
            });

            modelBuilder.Entity<EmailStatus>(entity =>
            {
                entity.ToTable("EmailStatus", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.ToTable("EmailTemplate", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Subject).HasMaxLength(255);
            });

            modelBuilder.Entity<EmailType>(entity =>
            {
                entity.ToTable("EmailType", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AddressId, "IX_AddressID");

                entity.HasIndex(e => e.CampaignId, "IX_CampaignID");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.IporganizationId, "IX_IPOrganizationID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SiteDomainId, "IX_SiteDomainID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.HasIndex(e => e.VisitId, "IX_VisitID");

                entity.HasIndex(e => e.VisitorId, "IX_VisitorID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Browser)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EntryPage)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ExitPage)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Flash)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ipaddress)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IPAddress");

                entity.Property(e => e.IporganizationId).HasColumnName("IPOrganizationID");

                entity.Property(e => e.Keywords).HasMaxLength(100);

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OperatingSystem)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PartitionKey)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Referrer)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ReferringHost)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RowKey).HasMaxLength(50);

                entity.Property(e => e.SiteDomainId).HasColumnName("SiteDomainID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Time)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VisitId).HasColumnName("VisitID");

                entity.Property(e => e.VisitorId).HasColumnName("VisitorID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Tracking.Event_Geography.Address_AddressID");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK_Tracking.Event_Tracking.Campaign_CampaignID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Tracking.Event_Contacts.Contact_ContactID");

                entity.HasOne(d => d.Iporganization)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.IporganizationId)
                    .HasConstraintName("FK_Tracking.Event_Tracking.IPOrganization_IPOrganizationID");

                entity.HasOne(d => d.SiteDomain)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.SiteDomainId)
                    .HasConstraintName("FK_Tracking.Event_Stores.SiteDomain_SiteDomainID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Tracking.Event_Tracking.EventStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Tracking.Event_Tracking.EventType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tracking.Event_Contacts.User_UserID");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.VisitId)
                    .HasConstraintName("FK_Tracking.Event_Tracking.Visit_VisitID");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.VisitorId)
                    .HasConstraintName("FK_Tracking.Event_Tracking.Visitor_VisitorID");
            });

            modelBuilder.Entity<EventStatus>(entity =>
            {
                entity.ToTable("EventStatus", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.ToTable("EventType", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FavoriteCategory>(entity =>
            {
                entity.ToTable("FavoriteCategory", "Favorites");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FavoriteCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Favorites.FavoriteCategory_Contacts.User_UserID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FavoriteCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Favorites.FavoriteCategory_Categories.Category_FavoriteID");
            });

            modelBuilder.Entity<FavoriteManufacturer>(entity =>
            {
                entity.ToTable("FavoriteManufacturer", "Favorites");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FavoriteManufacturers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Favorites.FavoriteManufacturer_Contacts.User_UserID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FavoriteManufacturers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Favorites.FavoriteManufacturer_Manufacturers.Manufacturer_FavoriteID");
            });

            modelBuilder.Entity<FavoriteShipCarrier>(entity =>
            {
                entity.ToTable("FavoriteShipCarrier", "Favorites");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FavoriteShipCarriers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Favorites.FavoriteShipCarrier_Contacts.User_UserID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FavoriteShipCarriers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Favorites.FavoriteShipCarrier_Shipping.ShipCarrier_FavoriteID");
            });

            modelBuilder.Entity<FavoriteStore>(entity =>
            {
                entity.ToTable("FavoriteStore", "Favorites");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FavoriteStores)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Favorites.FavoriteStore_Contacts.User_UserID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FavoriteStores)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Favorites.FavoriteStore_Stores.Store_FavoriteID");
            });

            modelBuilder.Entity<FavoriteVendor>(entity =>
            {
                entity.ToTable("FavoriteVendor", "Favorites");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FavoriteVendors)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Favorites.FavoriteVendor_Contacts.User_UserID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FavoriteVendors)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Favorites.FavoriteVendor_Vendors.Vendor_FavoriteID");
            });

            modelBuilder.Entity<Franchise>(entity =>
            {
                entity.ToTable("Franchise", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId, "IX_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferProductId, "IX_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId, "IX_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferProductId, "IX_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferCategoryId, "IX_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferProductId, "IX_MinimumOrderDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferCategoryId, "IX_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferProductId, "IX_MinimumOrderQuantityAmountBufferProductID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MinimumForFreeShippingDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferProductId).HasColumnName("MinimumForFreeShippingDollarAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferProductId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountBufferCategoryId).HasColumnName("MinimumOrderDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderDollarAmountBufferProductId).HasColumnName("MinimumOrderDollarAmountBufferProductID");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferCategoryId).HasColumnName("MinimumOrderQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferProductId).HasColumnName("MinimumOrderQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferCategory)
                    .WithMany(p => p.FranchiseMinimumForFreeShippingDollarAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Franchises.Franchise_Categories.Category_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferProduct)
                    .WithMany(p => p.FranchiseMinimumForFreeShippingDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferProductId)
                    .HasConstraintName("FK_Franchises.Franchise_Products.Product_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferCategory)
                    .WithMany(p => p.FranchiseMinimumForFreeShippingQuantityAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Franchises.Franchise_Categories.Category_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferProduct)
                    .WithMany(p => p.FranchiseMinimumForFreeShippingQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Franchises.Franchise_Products.Product_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferCategory)
                    .WithMany(p => p.FranchiseMinimumOrderDollarAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Franchises.Franchise_Categories.Category_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferProduct)
                    .WithMany(p => p.FranchiseMinimumOrderDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferProductId)
                    .HasConstraintName("FK_Franchises.Franchise_Products.Product_MinimumOrderDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferCategory)
                    .WithMany(p => p.FranchiseMinimumOrderQuantityAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Franchises.Franchise_Categories.Category_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferProduct)
                    .WithMany(p => p.FranchiseMinimumOrderQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Franchises.Franchise_Products.Product_MinimumOrderQuantityAmountBufferProductID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Franchises)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Franchises.Franchise_Franchises.FranchiseType_TypeID");
            });

            modelBuilder.Entity<FranchiseAccount>(entity =>
            {
                entity.ToTable("FranchiseAccount", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.PricePointId, "IX_PricePointID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.PricePointId).HasColumnName("PricePointID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseAccounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseAccount_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.PricePoint)
                    .WithMany(p => p.FranchiseAccounts)
                    .HasForeignKey(d => d.PricePointId)
                    .HasConstraintName("FK_Franchises.FranchiseAccount_Pricing.PricePoint_PricePointID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseAccounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseAccount_Accounts.Account_SlaveID");
            });

            modelBuilder.Entity<FranchiseAuction>(entity =>
            {
                entity.ToTable("FranchiseAuction", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseAuctions)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Auctions.FranchiseAuction_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseAuctions)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Auctions.FranchiseAuction_Auctions.Auction_SlaveID");
            });

            modelBuilder.Entity<FranchiseCategory>(entity =>
            {
                entity.ToTable("FranchiseCategory", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseCategory_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseCategory_Categories.Category_SlaveID");
            });

            modelBuilder.Entity<FranchiseCountry>(entity =>
            {
                entity.ToTable("FranchiseCountry", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseCountries)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseCountry_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseCountries)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseCountry_Geography.Country_SlaveID");
            });

            modelBuilder.Entity<FranchiseCurrency>(entity =>
            {
                entity.ToTable("FranchiseCurrency", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CustomName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CustomTranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OverrideHtmlCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OverrideHtmlDecimalCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OverrideHtmlSeparatorCharacterCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OverrideRawCharacter).HasMaxLength(5);

                entity.Property(e => e.OverrideRawDecimalCharacter).HasMaxLength(5);

                entity.Property(e => e.OverrideRawSeparatorCharacter).HasMaxLength(5);

                entity.Property(e => e.OverrideUnicodeSymbolValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseCurrencies)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseCurrency_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseCurrencies)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseCurrency_Currencies.Currency_SlaveID");
            });

            modelBuilder.Entity<FranchiseDistrict>(entity =>
            {
                entity.ToTable("FranchiseDistrict", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseDistricts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseDistrict_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseDistricts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseDistrict_Geography.District_SlaveID");
            });

            modelBuilder.Entity<FranchiseImage>(entity =>
            {
                entity.ToTable("FranchiseImage", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseImage_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.FranchiseImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Franchises.FranchiseImage_Franchises.FranchiseImageType_TypeID");
            });

            modelBuilder.Entity<FranchiseImageType>(entity =>
            {
                entity.ToTable("FranchiseImageType", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FranchiseInventoryLocation>(entity =>
            {
                entity.ToTable("FranchiseInventoryLocation", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseInventoryLocations)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseInventoryLocation_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseInventoryLocations)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseInventoryLocation_Inventory.InventoryLocation_SlaveID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.FranchiseInventoryLocations)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Franchises.FranchiseInventoryLocation_Franchises.FranchiseInventoryLocationType_TypeID");
            });

            modelBuilder.Entity<FranchiseInventoryLocationType>(entity =>
            {
                entity.ToTable("FranchiseInventoryLocationType", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FranchiseLanguage>(entity =>
            {
                entity.ToTable("FranchiseLanguage", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.OverrideLocale, "IX_OverrideLocale");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OverrideIso63912002)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("OverrideISO639_1_2002");

                entity.Property(e => e.OverrideIso63921998)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("OverrideISO639_2_1998");

                entity.Property(e => e.OverrideIso63932007)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("OverrideISO639_3_2007");

                entity.Property(e => e.OverrideIso63952008)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("OverrideISO639_5_2008");

                entity.Property(e => e.OverrideLocale).HasMaxLength(128);

                entity.Property(e => e.OverrideUnicodeName).HasMaxLength(1024);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseLanguages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseLanguage_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseLanguages)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseLanguage_Globalization.Language_SlaveID");
            });

            modelBuilder.Entity<FranchiseManufacturer>(entity =>
            {
                entity.ToTable("FranchiseManufacturer", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseManufacturers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseManufacturer_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseManufacturers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseManufacturer_Manufacturers.Manufacturer_SlaveID");
            });

            modelBuilder.Entity<FranchiseProduct>(entity =>
            {
                entity.ToTable("FranchiseProduct", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.PriceBase).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceMsrp).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceReduction).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceSale).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseProducts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseProduct_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseProducts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseProduct_Products.Product_SlaveID");
            });

            modelBuilder.Entity<FranchiseRegion>(entity =>
            {
                entity.ToTable("FranchiseRegion", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseRegions)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseRegion_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseRegions)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseRegion_Geography.Region_SlaveID");
            });

            modelBuilder.Entity<FranchiseSiteDomain>(entity =>
            {
                entity.ToTable("FranchiseSiteDomain", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseSiteDomains)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseSiteDomain_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseSiteDomains)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseSiteDomain_Stores.SiteDomain_SlaveID");
            });

            modelBuilder.Entity<FranchiseStore>(entity =>
            {
                entity.ToTable("FranchiseStore", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseStores)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseStore_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseStores)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseStore_Stores.Store_SlaveID");
            });

            modelBuilder.Entity<FranchiseType>(entity =>
            {
                entity.ToTable("FranchiseType", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FranchiseUser>(entity =>
            {
                entity.ToTable("FranchiseUser", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseUsers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseUser_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseUsers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseUser_Contacts.User_SlaveID");
            });

            modelBuilder.Entity<FranchiseVendor>(entity =>
            {
                entity.ToTable("FranchiseVendor", "Franchises");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.FranchiseVendors)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Franchises.FranchiseVendor_Franchises.Franchise_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.FranchiseVendors)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Franchises.FranchiseVendor_Vendors.Vendor_SlaveID");
            });

            modelBuilder.Entity<FutureImport>(entity =>
            {
                entity.ToTable("FutureImport", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FileName, "IX_FileName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.RunImportAt, "IX_RunImportAt");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.VendorId, "IX_VendorID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileName)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.FutureImports)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Products.FutureImport_Products.FutureImportStatus_StatusID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.FutureImports)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Products.FutureImport_Stores.Store_StoreID");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.FutureImports)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_Products.FutureImport_Vendors.Vendor_VendorID");
            });

            modelBuilder.Entity<FutureImportStatus>(entity =>
            {
                entity.ToTable("FutureImportStatus", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GeneralAttribute>(entity =>
            {
                entity.ToTable("GeneralAttribute", "Attributes");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AttributeGroupId, "IX_AttributeGroupID");

                entity.HasIndex(e => e.AttributeTabId, "IX_AttributeTabID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey")
                    .IsUnique();

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AttributeGroupId).HasColumnName("AttributeGroupID");

                entity.Property(e => e.AttributeTabId).HasColumnName("AttributeTabID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.AttributeGroup)
                    .WithMany(p => p.GeneralAttributes)
                    .HasForeignKey(d => d.AttributeGroupId)
                    .HasConstraintName("FK_Attributes.GeneralAttribute_Attributes.AttributeGroup_AttributeGroupID");

                entity.HasOne(d => d.AttributeTab)
                    .WithMany(p => p.GeneralAttributes)
                    .HasForeignKey(d => d.AttributeTabId)
                    .HasConstraintName("FK_Attributes.GeneralAttribute_Attributes.AttributeTab_AttributeTabID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.GeneralAttributes)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Attributes.GeneralAttribute_Attributes.AttributeType_TypeID");
            });

            modelBuilder.Entity<GeneralAttributePredefinedOption>(entity =>
            {
                entity.ToTable("GeneralAttributePredefinedOption", "Attributes");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AttributeId, "IX_AttributeID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AttributeId).HasColumnName("AttributeID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UofM)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.GeneralAttributePredefinedOptions)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_Attributes.GeneralAttributePredefinedOption_Attributes.GeneralAttribute_AttributeID");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group", "Groups");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.GroupOwnerId, "IX_GroupOwnerID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.ParentId, "IX_ParentID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.GroupOwnerId).HasColumnName("GroupOwnerID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.GroupOwner)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.GroupOwnerId)
                    .HasConstraintName("FK_Groups.Group_Contacts.User_GroupOwnerID");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Groups.Group_Groups.Group_ParentID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Groups.Group_Groups.GroupStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Groups.Group_Groups.GroupType_TypeID");
            });

            modelBuilder.Entity<GroupStatus>(entity =>
            {
                entity.ToTable("GroupStatus", "Groups");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GroupType>(entity =>
            {
                entity.ToTable("GroupType", "Groups");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GroupUser>(entity =>
            {
                entity.ToTable("GroupUser", "Groups");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Groups.GroupUser_Groups.Group_GroupID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Groups.GroupUser_Contacts.User_UserID");
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_Hangfire.Hash");

                entity.ToTable("Hash", "Hangfire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt");

                entity.HasIndex(e => e.Key, "IX_HangFire_Hash_Key");

                entity.HasIndex(e => new { e.Key, e.Field }, "UX_HangFire_Hash_Key_Field")
                    .IsUnique();

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<HistoricalAddressValidation>(entity =>
            {
                entity.ToTable("HistoricalAddressValidation", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Provider)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HistoricalCurrencyRate>(entity =>
            {
                entity.ToTable("HistoricalCurrencyRate", "Currencies");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.EndingCurrencyId, "IX_EndingCurrencyID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.StartingCurrencyId, "IX_StartingCurrencyID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.EndingCurrencyId).HasColumnName("EndingCurrencyID");

                entity.Property(e => e.Rate).HasColumnType("decimal(24, 20)");

                entity.Property(e => e.StartingCurrencyId).HasColumnName("StartingCurrencyID");

                entity.HasOne(d => d.EndingCurrency)
                    .WithMany(p => p.HistoricalCurrencyRateEndingCurrencies)
                    .HasForeignKey(d => d.EndingCurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Currencies.HistoricalCurrencyRate_Currencies.Currency_EndingCurrencyID");

                entity.HasOne(d => d.StartingCurrency)
                    .WithMany(p => p.HistoricalCurrencyRateStartingCurrencies)
                    .HasForeignKey(d => d.StartingCurrencyId)
                    .HasConstraintName("FK_Currencies.HistoricalCurrencyRate_Currencies.Currency_StartingCurrencyID");
            });

            modelBuilder.Entity<HistoricalTaxRate>(entity =>
            {
                entity.ToTable("HistoricalTaxRate", "Tax");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryLevelRate).HasColumnType("decimal(7, 6)");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictLevelRate).HasColumnType("decimal(7, 6)");

                entity.Property(e => e.Provider)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(7, 6)");

                entity.Property(e => e.RegionLevelRate).HasColumnType("decimal(7, 6)");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(7, 6)");

                entity.Property(e => e.TotalTax).HasColumnType("decimal(7, 6)");

                entity.Property(e => e.TotalTaxCalculated).HasColumnType("decimal(7, 6)");

                entity.Property(e => e.TotalTaxable).HasColumnType("decimal(7, 6)");
            });

            modelBuilder.Entity<ImportExportMapping>(entity =>
            {
                entity.ToTable("ImportExportMapping", "System");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InventoryLocation>(entity =>
            {
                entity.ToTable("InventoryLocation", "Inventory");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.InventoryLocations)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Inventory.InventoryLocation_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<InventoryLocationRegion>(entity =>
            {
                entity.ToTable("InventoryLocationRegion", "Inventory");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.InventoryLocationRegions)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Inventory.InventoryLocationRegion_Inventory.InventoryLocation_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.InventoryLocationRegions)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Inventory.InventoryLocationRegion_Geography.Region_SlaveID");
            });

            modelBuilder.Entity<InventoryLocationSection>(entity =>
            {
                entity.ToTable("InventoryLocationSection", "Inventory");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.InventoryLocationId, "IX_InventoryLocationID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.InventoryLocationId).HasColumnName("InventoryLocationID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.InventoryLocation)
                    .WithMany(p => p.InventoryLocationSections)
                    .HasForeignKey(d => d.InventoryLocationId)
                    .HasConstraintName("FK_Inventory.InventoryLocationSection_Inventory.InventoryLocation_InventoryLocationID");
            });

            modelBuilder.Entity<InventoryLocationUser>(entity =>
            {
                entity.ToTable("InventoryLocationUser", "Inventory");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.InventoryLocationUsers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Inventory.InventoryLocationUser_Inventory.InventoryLocation_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.InventoryLocationUsers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Inventory.InventoryLocationUser_Contacts.User_SlaveID");
            });

            modelBuilder.Entity<Iporganization>(entity =>
            {
                entity.ToTable("IPOrganization", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AddressId, "IX_AddressID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.PrimaryUserId, "IX_PrimaryUserID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Ipaddress)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IPAddress");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryUserId).HasColumnName("PrimaryUserID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.VisitorKey).HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Iporganizations)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Tracking.IPOrganization_Geography.Address_AddressID");

                entity.HasOne(d => d.PrimaryUser)
                    .WithMany(p => p.Iporganizations)
                    .HasForeignKey(d => d.PrimaryUserId)
                    .HasConstraintName("FK_Tracking.IPOrganization_Contacts.User_PrimaryUserID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Iporganizations)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Tracking.IPOrganization_Tracking.IPOrganizationStatus_StatusID");
            });

            modelBuilder.Entity<IporganizationStatus>(entity =>
            {
                entity.ToTable("IPOrganizationStatus", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "Hangfire");

                entity.HasIndex(e => e.ExpireAt, "IX_ExpireAt");

                entity.HasIndex(e => e.Id, "IX_Id");

                entity.HasIndex(e => e.StateName, "IX_StateName");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_Hangfire.JobParameter");

                entity.ToTable("JobParameter", "Hangfire");

                entity.HasIndex(e => new { e.JobId, e.Name }, "IX_HangFire_JobParameter_JobIdAndName");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameters)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_Hangfire.JobParameter_Hangfire.Job_JobId");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.ToTable("JobQueue", "Hangfire");

                entity.HasIndex(e => new { e.Queue, e.Id }, "IX_HangFire_JobQueue_QueueAndId");

                entity.HasIndex(e => e.Id, "IX_Id");

                entity.HasIndex(e => e.JobId, "IX_JobId");

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobQueues)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_Hangfire.JobQueue_Hangfire.Job_JobId");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language", "Globalization");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Locale, "IX_Locale");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Iso63912002)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ISO639_1_2002");

                entity.Property(e => e.Iso63921998)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ISO639_2_1998");

                entity.Property(e => e.Iso63932007)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ISO639_3_2007");

                entity.Property(e => e.Iso63952008)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ISO639_5_2008");

                entity.Property(e => e.Locale).HasMaxLength(128);

                entity.Property(e => e.UnicodeName).HasMaxLength(1024);
            });

            modelBuilder.Entity<LanguageImage>(entity =>
            {
                entity.ToTable("LanguageImage", "Globalization");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.LanguageImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Globalization.LanguageImage_Globalization.Language_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.LanguageImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Globalization.LanguageImage_Globalization.LanguageImageType_TypeID");
            });

            modelBuilder.Entity<LanguageImageType>(entity =>
            {
                entity.ToTable("LanguageImageType", "Globalization");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.ToTable("List", "Hangfire");

                entity.HasIndex(e => e.ExpireAt, "IX_ExpireAt");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key).HasMaxLength(100);
            });

            modelBuilder.Entity<Listing>(entity =>
            {
                entity.ToTable("Listing", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AuctionId, "IX_AuctionID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.PickupLocationId, "IX_PickupLocationID");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuctionId).HasColumnName("AuctionID");

                entity.Property(e => e.BiddingReserve).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PickupLocationId).HasColumnName("PickupLocationID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("FK_Auctions.Listing_Auctions.Auction_AuctionID");

                entity.HasOne(d => d.PickupLocation)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.PickupLocationId)
                    .HasConstraintName("FK_Auctions.Listing_Contacts.Contact_PickupLocationID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Auctions.Listing_Products.Product_ProductID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Auctions.Listing_Auctions.ListingStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Auctions.Listing_Auctions.ListingType_TypeID");
            });

            modelBuilder.Entity<ListingCategory>(entity =>
            {
                entity.ToTable("ListingCategory", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ListingCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Auctions.ListingCategory_Auctions.Listing_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ListingCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Auctions.ListingCategory_Categories.Category_SlaveID");
            });

            modelBuilder.Entity<ListingStatus>(entity =>
            {
                entity.ToTable("ListingStatus", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ListingType>(entity =>
            {
                entity.ToTable("ListingType", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lot>(entity =>
            {
                entity.ToTable("Lot", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AuctionId, "IX_AuctionID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.PickupLocationId, "IX_PickupLocationID");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuctionId).HasColumnName("AuctionID");

                entity.Property(e => e.BiddingReserve).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PickupLocationId).HasColumnName("PickupLocationID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.QuantityAvailable).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantitySold).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.Lots)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("FK_Auctions.Lot_Auctions.Auction_AuctionID");

                entity.HasOne(d => d.PickupLocation)
                    .WithMany(p => p.Lots)
                    .HasForeignKey(d => d.PickupLocationId)
                    .HasConstraintName("FK_Auctions.Lot_Contacts.Contact_PickupLocationID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Lots)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Auctions.Lot_Products.Product_ProductID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Lots)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Auctions.Lot_Auctions.LotStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Lots)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Auctions.Lot_Auctions.LotType_TypeID");
            });

            modelBuilder.Entity<LotCategory>(entity =>
            {
                entity.ToTable("LotCategory", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.LotCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Auctions.LotCategory_Auctions.Lot_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.LotCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Auctions.LotCategory_Categories.Category_SlaveID");
            });

            modelBuilder.Entity<LotStatus>(entity =>
            {
                entity.ToTable("LotStatus", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LotType>(entity =>
            {
                entity.ToTable("LotType", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("Manufacturer", "Manufacturers");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId, "IX_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferProductId, "IX_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId, "IX_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferProductId, "IX_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferCategoryId, "IX_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferProductId, "IX_MinimumOrderDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferCategoryId, "IX_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferProductId, "IX_MinimumOrderQuantityAmountBufferProductID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MinimumForFreeShippingDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferProductId).HasColumnName("MinimumForFreeShippingDollarAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferProductId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountBufferCategoryId).HasColumnName("MinimumOrderDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderDollarAmountBufferProductId).HasColumnName("MinimumOrderDollarAmountBufferProductID");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferCategoryId).HasColumnName("MinimumOrderQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferProductId).HasColumnName("MinimumOrderQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Manufacturers)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Manufacturers.Manufacturer_Contacts.Contact_ContactID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferCategory)
                    .WithMany(p => p.ManufacturerMinimumForFreeShippingDollarAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Manufacturers.Manufacturer_Categories.Category_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferProduct)
                    .WithMany(p => p.ManufacturerMinimumForFreeShippingDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferProductId)
                    .HasConstraintName("FK_Manufacturers.Manufacturer_Products.Product_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferCategory)
                    .WithMany(p => p.ManufacturerMinimumForFreeShippingQuantityAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Manufacturers.Manufacturer_Categories.Category_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferProduct)
                    .WithMany(p => p.ManufacturerMinimumForFreeShippingQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Manufacturers.Manufacturer_Products.Product_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferCategory)
                    .WithMany(p => p.ManufacturerMinimumOrderDollarAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Manufacturers.Manufacturer_Categories.Category_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferProduct)
                    .WithMany(p => p.ManufacturerMinimumOrderDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferProductId)
                    .HasConstraintName("FK_Manufacturers.Manufacturer_Products.Product_MinimumOrderDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferCategory)
                    .WithMany(p => p.ManufacturerMinimumOrderQuantityAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Manufacturers.Manufacturer_Categories.Category_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferProduct)
                    .WithMany(p => p.ManufacturerMinimumOrderQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Manufacturers.Manufacturer_Products.Product_MinimumOrderQuantityAmountBufferProductID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Manufacturers)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Manufacturers.Manufacturer_Manufacturers.ManufacturerType_TypeID");
            });

            modelBuilder.Entity<ManufacturerImage>(entity =>
            {
                entity.ToTable("ManufacturerImage", "Manufacturers");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ManufacturerImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Manufacturers.ManufacturerImage_Manufacturers.Manufacturer_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ManufacturerImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Manufacturers.ManufacturerImage_Manufacturers.ManufacturerImageType_TypeID");
            });

            modelBuilder.Entity<ManufacturerImageType>(entity =>
            {
                entity.ToTable("ManufacturerImageType", "Manufacturers");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ManufacturerProduct>(entity =>
            {
                entity.ToTable("ManufacturerProduct", "Manufacturers");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ManufacturerProducts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Manufacturers.ManufacturerProduct_Manufacturers.Manufacturer_ManufacturerID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ManufacturerProducts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Manufacturers.ManufacturerProduct_Products.Product_ProductID");
            });

            modelBuilder.Entity<ManufacturerType>(entity =>
            {
                entity.ToTable("ManufacturerType", "Manufacturers");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.ToTable("Membership", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MembershipAdZoneAccess>(entity =>
            {
                entity.ToTable("MembershipAdZoneAccess", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.MembershipAdZoneAccesses)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Payments.MembershipAdZoneAccess_Payments.Membership_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.MembershipAdZoneAccesses)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Payments.MembershipAdZoneAccess_Advertising.AdZoneAccess_SlaveID");
            });

            modelBuilder.Entity<MembershipAdZoneAccessByLevel>(entity =>
            {
                entity.ToTable("MembershipAdZoneAccessByLevel", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.MembershipAdZoneAccessByLevels)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Payments.MembershipAdZoneAccessByLevel_Payments.MembershipAdZoneAccess_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.MembershipAdZoneAccessByLevels)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Payments.MembershipAdZoneAccessByLevel_Payments.MembershipLevel_SlaveID");
            });

            modelBuilder.Entity<MembershipLevel>(entity =>
            {
                entity.ToTable("MembershipLevel", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MembershipId, "IX_MembershipID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.MembershipId).HasColumnName("MembershipID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RolesApplied)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.MembershipLevels)
                    .HasForeignKey(d => d.MembershipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments.MembershipLevel_Payments.Membership_MembershipID");
            });

            modelBuilder.Entity<MembershipRepeatType>(entity =>
            {
                entity.ToTable("MembershipRepeatType", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.MembershipRepeatTypes)
                    .HasForeignKey(d => d.MasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments.MembershipRepeatType_Payments.Membership_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.MembershipRepeatTypes)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Payments.MembershipRepeatType_Payments.RepeatType_SlaveID");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.ConversationId, "IX_ConversationID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.SentByUserId, "IX_SentByUserID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.Context).HasMaxLength(256);

                entity.Property(e => e.ConversationId).HasColumnName("ConversationID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SentByUserId).HasColumnName("SentByUserID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Subject).HasMaxLength(255);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Messaging.Message_Brands.Brand_BrandID");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationId)
                    .HasConstraintName("FK_Messaging.Message_Messaging.Conversation_ConversationID");

                entity.HasOne(d => d.SentByUser)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.SentByUserId)
                    .HasConstraintName("FK_Messaging.Message_Contacts.User_SentByUserID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Messaging.Message_Stores.Store_StoreID");
            });

            modelBuilder.Entity<MessageAttachment>(entity =>
            {
                entity.ToTable("MessageAttachment", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedByUserId, "IX_CreatedByUserID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedByUserId, "IX_UpdatedByUserID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.UpdatedByUserId).HasColumnName("UpdatedByUserID");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.MessageAttachmentCreatedByUsers)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messaging.MessageAttachment_Contacts.User_CreatedByUserID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.MessageAttachments)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Messaging.MessageAttachment_Messaging.Message_MessageID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.MessageAttachments)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Messaging.MessageAttachment_Media.StoredFile_StoredFileID");

                entity.HasOne(d => d.UpdatedByUser)
                    .WithMany(p => p.MessageAttachmentUpdatedByUsers)
                    .HasForeignKey(d => d.UpdatedByUserId)
                    .HasConstraintName("FK_Messaging.MessageAttachment_Contacts.User_UpdatedByUserID");
            });

            modelBuilder.Entity<MessageRecipient>(entity =>
            {
                entity.ToTable("MessageRecipient", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.GroupId, "IX_GroupID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.MessageRecipients)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Messaging.MessageRecipient_Groups.Group_GroupID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.MessageRecipients)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Messaging.MessageRecipient_Messaging.Message_MessageID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.MessageRecipients)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Messaging.MessageRecipient_Contacts.User_ToUserID");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Note", "System");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CartId, "IX_CartID");

                entity.HasIndex(e => e.CartItemId, "IX_CartItemID");

                entity.HasIndex(e => e.CreatedByUserId, "IX_CreatedByUserID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FranchiseId, "IX_FranchiseID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.ManufacturerId, "IX_ManufacturerID");

                entity.HasIndex(e => e.PurchaseOrderId, "IX_PurchaseOrderID");

                entity.HasIndex(e => e.PurchaseOrderItemId, "IX_PurchaseOrderItemID");

                entity.HasIndex(e => e.SalesGroupId, "IX_SalesGroupID");

                entity.HasIndex(e => e.SalesInvoiceId, "IX_SalesInvoiceID");

                entity.HasIndex(e => e.SalesInvoiceItemId, "IX_SalesInvoiceItemID");

                entity.HasIndex(e => e.SalesOrderId, "IX_SalesOrderID");

                entity.HasIndex(e => e.SalesOrderItemId, "IX_SalesOrderItemID");

                entity.HasIndex(e => e.SalesQuoteId, "IX_SalesQuoteID");

                entity.HasIndex(e => e.SalesQuoteItemId, "IX_SalesQuoteItemID");

                entity.HasIndex(e => e.SalesReturnId, "IX_SalesReturnID");

                entity.HasIndex(e => e.SalesReturnItemId, "IX_SalesReturnItemID");

                entity.HasIndex(e => e.SampleRequestId, "IX_SampleRequestID");

                entity.HasIndex(e => e.SampleRequestItemId, "IX_SampleRequestItemID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedByUserId, "IX_UpdatedByUserID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.HasIndex(e => e.VendorId, "IX_VendorID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.CartItemId).HasColumnName("CartItemID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseId).HasColumnName("FranchiseID");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.Note1)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("Note");

                entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");

                entity.Property(e => e.PurchaseOrderItemId).HasColumnName("PurchaseOrderItemID");

                entity.Property(e => e.SalesGroupId).HasColumnName("SalesGroupID");

                entity.Property(e => e.SalesInvoiceId).HasColumnName("SalesInvoiceID");

                entity.Property(e => e.SalesInvoiceItemId).HasColumnName("SalesInvoiceItemID");

                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");

                entity.Property(e => e.SalesOrderItemId).HasColumnName("SalesOrderItemID");

                entity.Property(e => e.SalesQuoteId).HasColumnName("SalesQuoteID");

                entity.Property(e => e.SalesQuoteItemId).HasColumnName("SalesQuoteItemID");

                entity.Property(e => e.SalesReturnId).HasColumnName("SalesReturnID");

                entity.Property(e => e.SalesReturnItemId).HasColumnName("SalesReturnItemID");

                entity.Property(e => e.SampleRequestId).HasColumnName("SampleRequestID");

                entity.Property(e => e.SampleRequestItemId).HasColumnName("SampleRequestItemID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UpdatedByUserId).HasColumnName("UpdatedByUserID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_System.Note_Accounts.Account_AccountID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_System.Note_Brands.Brand_BrandID");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK_System.Note_Shopping.Cart_CartID");

                entity.HasOne(d => d.CartItem)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.CartItemId)
                    .HasConstraintName("FK_System.Note_Shopping.CartItem_CartItemID");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.NoteCreatedByUsers)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .HasConstraintName("FK_System.Note_Contacts.User_CreatedByUserID");

                entity.HasOne(d => d.Franchise)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.FranchiseId)
                    .HasConstraintName("FK_System.Note_Franchises.Franchise_FranchiseID");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("FK_System.Note_Manufacturers.Manufacturer_ManufacturerID");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .HasConstraintName("FK_System.Note_Purchasing.PurchaseOrder_PurchaseOrderID");

                entity.HasOne(d => d.PurchaseOrderItem)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.PurchaseOrderItemId)
                    .HasConstraintName("FK_System.Note_Purchasing.PurchaseOrderItem_PurchaseOrderItemID");

                entity.HasOne(d => d.SalesGroup)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SalesGroupId)
                    .HasConstraintName("FK_System.Note_Sales.SalesGroup_SalesGroupID");

                entity.HasOne(d => d.SalesInvoice)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SalesInvoiceId)
                    .HasConstraintName("FK_System.Note_Invoicing.SalesInvoice_SalesInvoiceID");

                entity.HasOne(d => d.SalesInvoiceItem)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SalesInvoiceItemId)
                    .HasConstraintName("FK_System.Note_Invoicing.SalesInvoiceItem_SalesInvoiceItemID");

                entity.HasOne(d => d.SalesOrder)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SalesOrderId)
                    .HasConstraintName("FK_System.Note_Ordering.SalesOrder_SalesOrderID");

                entity.HasOne(d => d.SalesOrderItem)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SalesOrderItemId)
                    .HasConstraintName("FK_System.Note_Ordering.SalesOrderItem_SalesOrderItemID");

                entity.HasOne(d => d.SalesQuote)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SalesQuoteId)
                    .HasConstraintName("FK_System.Note_Quoting.SalesQuote_SalesQuoteID");

                entity.HasOne(d => d.SalesQuoteItem)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SalesQuoteItemId)
                    .HasConstraintName("FK_System.Note_Quoting.SalesQuoteItem_SalesQuoteItemID");

                entity.HasOne(d => d.SalesReturn)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SalesReturnId)
                    .HasConstraintName("FK_System.Note_Returning.SalesReturn_SalesReturnID");

                entity.HasOne(d => d.SalesReturnItem)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SalesReturnItemId)
                    .HasConstraintName("FK_System.Note_Returning.SalesReturnItem_SalesReturnItemID");

                entity.HasOne(d => d.SampleRequest)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SampleRequestId)
                    .HasConstraintName("FK_System.Note_Sampling.SampleRequest_SampleRequestID");

                entity.HasOne(d => d.SampleRequestItem)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.SampleRequestItemId)
                    .HasConstraintName("FK_System.Note_Sampling.SampleRequestItem_SampleRequestItemID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_System.Note_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_System.Note_System.NoteType_TypeID");

                entity.HasOne(d => d.UpdatedByUser)
                    .WithMany(p => p.NoteUpdatedByUsers)
                    .HasForeignKey(d => d.UpdatedByUserId)
                    .HasConstraintName("FK_System.Note_Contacts.User_UpdatedByUserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NoteUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_System.Note_Contacts.User_UserID");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.NotesNavigation)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_System.Note_Vendors.Vendor_VendorID");
            });

            modelBuilder.Entity<NoteType>(entity =>
            {
                entity.ToTable("NoteType", "System");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("Package", "Shipping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Depth).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DepthUnitOfMeasure)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DimensionalWeight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DimensionalWeightUnitOfMeasure)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Height).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.HeightUnitOfMeasure)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.WeightUnitOfMeasure)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Width).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.WidthUnitOfMeasure)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Packages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Shipping.Package_Shipping.PackageType_TypeID");
            });

            modelBuilder.Entity<PackageType>(entity =>
            {
                entity.ToTable("PackageType", "Shipping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PageView>(entity =>
            {
                entity.ToTable("PageView", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AddressId, "IX_AddressID");

                entity.HasIndex(e => e.CampaignId, "IX_CampaignID");

                entity.HasIndex(e => e.CategoryId, "IX_CategoryID");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.IporganizationId, "IX_IPOrganizationID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.SiteDomainId, "IX_SiteDomainID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.HasIndex(e => e.VisitorId, "IX_VisitorID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Browser)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EntryPage)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ExitPage)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Flash)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ipaddress)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IPAddress");

                entity.Property(e => e.IporganizationId).HasColumnName("IPOrganizationID");

                entity.Property(e => e.Keywords).HasMaxLength(100);

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OperatingSystem)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PartitionKey)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Referrer)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ReferringHost)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RowKey).HasMaxLength(50);

                entity.Property(e => e.SiteDomainId).HasColumnName("SiteDomainID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Time)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Uri)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("URI");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ViewedOn).HasColumnType("datetime");

                entity.Property(e => e.VisitKey).HasMaxLength(50);

                entity.Property(e => e.VisitorId).HasColumnName("VisitorID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Tracking.PageView_Geography.Address_AddressID");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK_Tracking.PageView_Tracking.Campaign_CampaignID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Tracking.PageView_Categories.Category_CategoryID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Tracking.PageView_Contacts.Contact_ContactID");

                entity.HasOne(d => d.Iporganization)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.IporganizationId)
                    .HasConstraintName("FK_Tracking.PageView_Tracking.IPOrganization_IPOrganizationID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Tracking.PageView_Products.Product_ProductID");

                entity.HasOne(d => d.SiteDomain)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.SiteDomainId)
                    .HasConstraintName("FK_Tracking.PageView_Stores.SiteDomain_SiteDomainID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Tracking.PageView_Tracking.PageViewStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Tracking.PageView_Tracking.PageViewType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tracking.PageView_Contacts.User_UserID");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.PageViews)
                    .HasForeignKey(d => d.VisitorId)
                    .HasConstraintName("FK_Tracking.PageView_Tracking.Visitor_VisitorID");
            });

            modelBuilder.Entity<PageViewEvent>(entity =>
            {
                entity.ToTable("PageViewEvent", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PageViewEvents)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Tracking.PageViewEvent_Tracking.PageView_PageViewID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PageViewEvents)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Tracking.PageViewEvent_Tracking.Event_EventID");
            });

            modelBuilder.Entity<PageViewStatus>(entity =>
            {
                entity.ToTable("PageViewStatus", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PageViewType>(entity =>
            {
                entity.ToTable("PageViewType", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BillingContactId, "IX_BillingContactID");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CurrencyId, "IX_CurrencyID");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.PaymentMethodId, "IX_PaymentMethodID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountNumberLast4).HasMaxLength(4);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.AuthCode).HasMaxLength(100);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BillingContactId).HasColumnName("BillingContactID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CardMask).HasMaxLength(50);

                entity.Property(e => e.CardTypeId).HasColumnName("CardTypeID");

                entity.Property(e => e.CheckNumber).HasMaxLength(8);

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Cvv)
                    .HasMaxLength(50)
                    .HasColumnName("CVV");

                entity.Property(e => e.ExternalCustomerId)
                    .HasMaxLength(100)
                    .HasColumnName("ExternalCustomerID");

                entity.Property(e => e.ExternalPaymentId)
                    .HasMaxLength(100)
                    .HasColumnName("ExternalPaymentID");

                entity.Property(e => e.Last4CardDigits).HasMaxLength(4);

                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");

                entity.Property(e => e.ReferenceNo).HasMaxLength(100);

                entity.Property(e => e.RoutingNumberLast4).HasMaxLength(4);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TransactionNumber).HasMaxLength(100);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.BillingContact)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BillingContactId)
                    .HasConstraintName("FK_Payments.Payment_Contacts.Contact_BillingContactID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Payments.Payment_Brands.Brand_BrandID");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Payments.Payment_Currencies.Currency_CurrencyID");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK_Payments.Payment_Payments.PaymentMethod_PaymentMethodID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Payments.Payment_Payments.PaymentStatus_StatusID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Payments.Payment_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Payments.Payment_Payments.PaymentType_TypeID");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.ToTable("PaymentStatus", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentType", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission", "Contacts");

                entity.HasIndex(e => e.Id, "IX_Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PhonePrefixLookup>(entity =>
            {
                entity.ToTable("PhonePrefixLookup", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CountryId, "IX_CountryID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Prefix, "IX_Prefix");

                entity.HasIndex(e => e.RegionId, "IX_RegionID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityName).HasMaxLength(255);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Prefix).HasMaxLength(20);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.TimeZone).HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PhonePrefixLookups)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Geography.PhonePrefixLookup_Geography.Country_CountryID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.PhonePrefixLookups)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Geography.PhonePrefixLookup_Geography.Region_RegionID");
            });

            modelBuilder.Entity<PricePoint>(entity =>
            {
                entity.ToTable("PricePoint", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PriceRounding>(entity =>
            {
                entity.ToTable("PriceRounding", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrencyKey)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PricePointKey)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductKey)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PriceRule>(entity =>
            {
                entity.ToTable("PriceRule", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CurrencyId, "IX_CurrencyID");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MaxQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PriceAdjustment).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.PriceRules)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Pricing.PriceRule_Currencies.Currency_CurrencyID");
            });

            modelBuilder.Entity<PriceRuleAccount>(entity =>
            {
                entity.ToTable("PriceRuleAccount", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PriceRuleAccounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Pricing.PriceRuleAccount_Pricing.PriceRule_PriceRuleID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PriceRuleAccounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Pricing.PriceRuleAccount_Accounts.Account_AccountID");
            });

            modelBuilder.Entity<PriceRuleAccountType>(entity =>
            {
                entity.ToTable("PriceRuleAccountType", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PriceRuleAccountTypes)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Pricing.PriceRuleAccountType_Pricing.PriceRule_PriceRuleID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PriceRuleAccountTypes)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Pricing.PriceRuleAccountType_Accounts.AccountType_AccountTypeID");
            });

            modelBuilder.Entity<PriceRuleBrand>(entity =>
            {
                entity.ToTable("PriceRuleBrand", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PriceRuleBrands)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Pricing.PriceRuleBrand_Pricing.PriceRule_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PriceRuleBrands)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Pricing.PriceRuleBrand_Brands.Brand_SlaveID");
            });

            modelBuilder.Entity<PriceRuleCategory>(entity =>
            {
                entity.ToTable("PriceRuleCategory", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PriceRuleCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Pricing.PriceRuleCategory_Pricing.PriceRule_PriceRuleID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PriceRuleCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Pricing.PriceRuleCategory_Categories.Category_CategoryID");
            });

            modelBuilder.Entity<PriceRuleCountry>(entity =>
            {
                entity.ToTable("PriceRuleCountry", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PriceRuleCountries)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Pricing.PriceRuleCountry_Pricing.PriceRule_PriceRuleID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PriceRuleCountries)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Pricing.PriceRuleCountry_Geography.Country_CountryID");
            });

            modelBuilder.Entity<PriceRuleManufacturer>(entity =>
            {
                entity.ToTable("PriceRuleManufacturer", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PriceRuleManufacturers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Pricing.PriceRuleManufacturer_Pricing.PriceRule_PriceRuleID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PriceRuleManufacturers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Pricing.PriceRuleManufacturer_Manufacturers.Manufacturer_ManufacturerID");
            });

            modelBuilder.Entity<PriceRuleProduct>(entity =>
            {
                entity.ToTable("PriceRuleProduct", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OverrideBasePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.OverrideSalePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PriceRuleProducts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Pricing.PriceRuleProduct_Pricing.PriceRule_PriceRuleID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PriceRuleProducts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Pricing.PriceRuleProduct_Products.Product_ProductID");
            });

            modelBuilder.Entity<PriceRuleProductType>(entity =>
            {
                entity.ToTable("PriceRuleProductType", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PriceRuleProductTypes)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Pricing.PriceRuleProductType_Pricing.PriceRule_PriceRuleID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PriceRuleProductTypes)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Pricing.PriceRuleProductType_Products.ProductType_ProductTypeID");
            });

            modelBuilder.Entity<PriceRuleStore>(entity =>
            {
                entity.ToTable("PriceRuleStore", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PriceRuleStores)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Pricing.PriceRuleStore_Pricing.PriceRule_PriceRuleID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PriceRuleStores)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Pricing.PriceRuleStore_Stores.Store_StoreID");
            });

            modelBuilder.Entity<PriceRuleUserRole>(entity =>
            {
                entity.ToTable("PriceRuleUserRole", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.PriceRuleId, "IX_PriceRuleID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PriceRuleId).HasColumnName("PriceRuleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.PriceRule)
                    .WithMany(p => p.PriceRuleUserRoles)
                    .HasForeignKey(d => d.PriceRuleId)
                    .HasConstraintName("FK_Pricing.PriceRuleUserRole_Pricing.PriceRule_PriceRuleID");
            });

            modelBuilder.Entity<PriceRuleVendor>(entity =>
            {
                entity.ToTable("PriceRuleVendor", "Pricing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PriceRuleVendors)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Pricing.PriceRuleVendor_Pricing.PriceRule_PriceRuleID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PriceRuleVendors)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Pricing.PriceRuleVendor_Vendors.Vendor_VendorID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterPackId, "IX_MasterPackID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.PackageId, "IX_PackageID");

                entity.HasIndex(e => e.PalletId, "IX_PalletID");

                entity.HasIndex(e => e.RestockingFeeAmountCurrencyId, "IX_RestockingFeeAmountCurrencyID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TotalPurchasedAmountCurrencyId, "IX_TotalPurchasedAmountCurrencyID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Depth).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DepthUnitOfMeasure)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DocumentRequiredForPurchase).HasMaxLength(128);

                entity.Property(e => e.DocumentRequiredForPurchaseExpiredWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.DocumentRequiredForPurchaseMissingWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.DocumentRequiredForPurchaseOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.DocumentRequiredForPurchaseOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.FlatShippingCharge).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.HandlingCharge).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Height).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.HeightUnitOfMeasure)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.IndexSynonyms)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.KitBaseQuantityPriceMultiplier).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ManufacturerPartNumber)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.MasterPackId).HasColumnName("MasterPackID");

                entity.Property(e => e.MaximumBackOrderPurchaseQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MaximumBackOrderPurchaseQuantityGlobal).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MaximumBackOrderPurchaseQuantityIfPastPurchased).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MaximumPrePurchaseQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MaximumPrePurchaseQuantityGlobal).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MaximumPrePurchaseQuantityIfPastPurchased).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MaximumPurchaseQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MaximumPurchaseQuantityIfPastPurchased).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumPurchaseQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumPurchaseQuantityIfPastPurchased).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MustPurchaseInMultiplesOfAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MustPurchaseInMultiplesOfAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MustPurchaseInMultiplesOfAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.PalletId).HasColumnName("PalletID");

                entity.Property(e => e.PriceBase).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceMsrp).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceReduction).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceSale).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityLayersPerPallet).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityMasterPackLayersPerPallet).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityMasterPackPerLayer).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityMasterPackPerPallet).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPerLayer).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPerMasterPack).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPerPallet).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RequiresRoles).HasMaxLength(512);

                entity.Property(e => e.RequiresRolesAlt).HasMaxLength(512);

                entity.Property(e => e.RestockingFeeAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RestockingFeeAmountCurrencyId).HasColumnName("RestockingFeeAmountCurrencyID");

                entity.Property(e => e.RestockingFeePercent).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StockQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.StockQuantityAllocated).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.StockQuantityPreSold).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxCode)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPurchasedAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalPurchasedAmountCurrencyId).HasColumnName("TotalPurchasedAmountCurrencyID");

                entity.Property(e => e.TotalPurchasedQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.WeightUnitOfMeasure)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Width).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.WidthUnitOfMeasure)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.MasterPack)
                    .WithMany(p => p.ProductMasterPacks)
                    .HasForeignKey(d => d.MasterPackId)
                    .HasConstraintName("FK_Products.Product_Shipping.Package_MasterPackID");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.ProductPackages)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK_Products.Product_Shipping.Package_PackageID");

                entity.HasOne(d => d.Pallet)
                    .WithMany(p => p.ProductPallets)
                    .HasForeignKey(d => d.PalletId)
                    .HasConstraintName("FK_Products.Product_Shipping.Package_PalletID");

                entity.HasOne(d => d.RestockingFeeAmountCurrency)
                    .WithMany(p => p.ProductRestockingFeeAmountCurrencies)
                    .HasForeignKey(d => d.RestockingFeeAmountCurrencyId)
                    .HasConstraintName("FK_Products.Product_Currencies.Currency_RestockingFeeAmountCurrencyID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Products.Product_Products.ProductStatus_StatusID");

                entity.HasOne(d => d.TotalPurchasedAmountCurrency)
                    .WithMany(p => p.ProductTotalPurchasedAmountCurrencies)
                    .HasForeignKey(d => d.TotalPurchasedAmountCurrencyId)
                    .HasConstraintName("FK_Products.Product_Currencies.Currency_TotalPurchasedAmountCurrencyID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Products.Product_Products.ProductType_TypeID");
            });

            modelBuilder.Entity<ProductAssociation>(entity =>
            {
                entity.ToTable("ProductAssociation", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ProductAssociations)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Products.ProductAssociation_Brands.Brand_BrandID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ProductAssociationMasters)
                    .HasForeignKey(d => d.MasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products.ProductAssociation_Products.Product_PrimaryProductID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ProductAssociationSlaves)
                    .HasForeignKey(d => d.SlaveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products.ProductAssociation_Products.Product_AssociatedProductID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProductAssociations)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Products.ProductAssociation_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ProductAssociations)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Products.ProductAssociation_Products.ProductAssociationType_TypeID");
            });

            modelBuilder.Entity<ProductAssociationType>(entity =>
            {
                entity.ToTable("ProductAssociationType", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Products.ProductCategory_Products.Product_ProductID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Products.ProductCategory_Categories.Category_CategoryID");
            });

            modelBuilder.Entity<ProductDownload>(entity =>
            {
                entity.ToTable("ProductDownload", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AbsoluteUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RelativeUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDownloads)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Products.ProductDownload_Products.Product_ProductID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ProductDownloads)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Products.ProductDownload_Products.ProductDownloadType_TypeID");
            });

            modelBuilder.Entity<ProductDownloadType>(entity =>
            {
                entity.ToTable("ProductDownloadType", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductFile>(entity =>
            {
                entity.ToTable("ProductFile", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ProductFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Products.ProductFileNew_Products.Product_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ProductFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Products.ProductFileNew_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImage", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Products.ProductImageNew_Products.Product_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Products.ProductImageNew_Products.ProductImageType_TypeID");
            });

            modelBuilder.Entity<ProductImageType>(entity =>
            {
                entity.ToTable("ProductImageType", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductInventoryLocationSection>(entity =>
            {
                entity.ToTable("ProductInventoryLocationSection", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityAllocated).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityBroken).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPreSold).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ProductInventoryLocationSections)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Products.ProductInventoryLocationSection_Products.Product_ProductID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ProductInventoryLocationSections)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Products.ProductInventoryLocationSection_Inventory.InventoryLocationSection_InventoryLocationSectionID");
            });

            modelBuilder.Entity<ProductMembershipLevel>(entity =>
            {
                entity.ToTable("ProductMembershipLevel", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.MembershipRepeatTypeId, "IX_MembershipRepeatTypeID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MembershipRepeatTypeId).HasColumnName("MembershipRepeatTypeID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ProductMembershipLevels)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Products.ProductMembershipLevel_Products.Product_MasterID");

                entity.HasOne(d => d.MembershipRepeatType)
                    .WithMany(p => p.ProductMembershipLevels)
                    .HasForeignKey(d => d.MembershipRepeatTypeId)
                    .HasConstraintName("FK_Products.ProductMembershipLevel_Payments.MembershipRepeatType_MembershipRepeatTypeID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ProductMembershipLevels)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Products.ProductMembershipLevel_Payments.MembershipLevel_SlaveID");
            });

            modelBuilder.Entity<ProductNotification>(entity =>
            {
                entity.ToTable("ProductNotification", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductNotifications)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Messaging.ProductNotification_Products.Product_ProductID");
            });

            modelBuilder.Entity<ProductPricePoint>(entity =>
            {
                entity.ToTable("ProductPricePoint", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CurrencyId, "IX_CurrencyID");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FranchiseId, "IX_FranchiseID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.PriceRoundingId, "IX_PriceRoundingID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseId).HasColumnName("FranchiseID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MaxQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PercentDiscount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceRoundingId).HasColumnName("PriceRoundingID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UnitOfMeasure).HasMaxLength(10);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ProductPricePoints)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Products.ProductPricePoint_Brands.Brand_BrandID");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ProductPricePoints)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Products.ProductPricePoint_Currencies.Currency_CurrencyID");

                entity.HasOne(d => d.Franchise)
                    .WithMany(p => p.ProductPricePoints)
                    .HasForeignKey(d => d.FranchiseId)
                    .HasConstraintName("FK_Products.ProductPricePoint_Franchises.Franchise_FranchiseID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ProductPricePoints)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Products.ProductPricePoint_Products.Product_ProductID");

                entity.HasOne(d => d.PriceRounding)
                    .WithMany(p => p.ProductPricePoints)
                    .HasForeignKey(d => d.PriceRoundingId)
                    .HasConstraintName("FK_Products.ProductPricePoint_Pricing.PriceRounding_PriceRoundingID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ProductPricePoints)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Products.ProductPricePoint_Pricing.PricePoint_PricePointID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProductPricePoints)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Products.ProductPricePoint_Stores.Store_StoreID");
            });

            modelBuilder.Entity<ProductRestriction>(entity =>
            {
                entity.ToTable("ProductRestriction", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.OverrideWithAccountTypeId, "IX_OverrideWithAccountTypeID");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.RestrictionsApplyToCountryId, "IX_RestrictionsApplyToCountryID");

                entity.HasIndex(e => e.RestrictionsApplyToRegionId, "IX_RestrictionsApplyToRegionID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.OverrideWithAccountTypeId).HasColumnName("OverrideWithAccountTypeID");

                entity.Property(e => e.OverrideWithRoles)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RestrictionsApplyToCity)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.RestrictionsApplyToCountryId).HasColumnName("RestrictionsApplyToCountryID");

                entity.Property(e => e.RestrictionsApplyToPostalCode)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.RestrictionsApplyToRegionId).HasColumnName("RestrictionsApplyToRegionID");

                entity.HasOne(d => d.OverrideWithAccountType)
                    .WithMany(p => p.ProductRestrictions)
                    .HasForeignKey(d => d.OverrideWithAccountTypeId)
                    .HasConstraintName("FK_Products.ProductRestriction_Accounts.AccountType_OverrideWithAccountTypeID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductRestrictions)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Products.ProductRestriction_Products.Product_ProductID");

                entity.HasOne(d => d.RestrictionsApplyToCountry)
                    .WithMany(p => p.ProductRestrictions)
                    .HasForeignKey(d => d.RestrictionsApplyToCountryId)
                    .HasConstraintName("FK_Products.ProductRestriction_Geography.Country_RestrictionsApplyToCountryID");

                entity.HasOne(d => d.RestrictionsApplyToRegion)
                    .WithMany(p => p.ProductRestrictions)
                    .HasForeignKey(d => d.RestrictionsApplyToRegionId)
                    .HasConstraintName("FK_Products.ProductRestriction_Geography.Region_RestrictionsApplyToRegionID");
            });

            modelBuilder.Entity<ProductShipCarrierMethod>(entity =>
            {
                entity.ToTable("ProductShipCarrierMethod", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CurrencyId, "IX_CurrencyID");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MaxQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinQuantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UnitOfMeasure).HasMaxLength(10);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ProductShipCarrierMethods)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Products.ProductShipCarrierMethod_Brands.Brand_BrandID");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ProductShipCarrierMethods)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Products.ProductShipCarrierMethod_Currencies.Currency_CurrencyID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ProductShipCarrierMethods)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Products.ProductShipCarrierMethod_Products.Product_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ProductShipCarrierMethods)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Products.ProductShipCarrierMethod_Shipping.ShipCarrierMethod_SlaveID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProductShipCarrierMethods)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Products.ProductShipCarrierMethod_Stores.Store_StoreID");
            });

            modelBuilder.Entity<ProductStatus>(entity =>
            {
                entity.ToTable("ProductStatus", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductSubscriptionType>(entity =>
            {
                entity.ToTable("ProductSubscriptionType", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.SubscriptionTypeRepeatTypeId, "IX_SubscriptionTypeRepeatTypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.SubscriptionTypeRepeatTypeId).HasColumnName("SubscriptionTypeRepeatTypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ProductSubscriptionTypes)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Products.ProductSubscriptionType_Products.Product_ProductID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ProductSubscriptionTypes)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Products.ProductSubscriptionType_Payments.SubscriptionType_SubscriptionTypeID");

                entity.HasOne(d => d.SubscriptionTypeRepeatType)
                    .WithMany(p => p.ProductSubscriptionTypes)
                    .HasForeignKey(d => d.SubscriptionTypeRepeatTypeId)
                    .HasConstraintName("FK_Products.ProductSubscriptionType_Payments.SubscriptionTypeRepeatType_SubscriptionTypeRepeatTypeID");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType", "Products");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProfanityFilter>(entity =>
            {
                entity.ToTable("ProfanityFilter", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.ToTable("PurchaseOrder", "Purchasing");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BillingContactId, "IX_BillingContactID");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FranchiseId, "IX_FranchiseID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.InventoryLocationId, "IX_InventoryLocationID");

                entity.HasIndex(e => e.SalesGroupId, "IX_SalesGroupID");

                entity.HasIndex(e => e.ShipCarrierId, "IX_ShipCarrierID");

                entity.HasIndex(e => e.ShippingContactId, "IX_ShippingContactID");

                entity.HasIndex(e => e.StateId, "IX_StateID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.HasIndex(e => e.VendorId, "IX_VendorID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BillingContactId).HasColumnName("BillingContactID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseId).HasColumnName("FranchiseID");

                entity.Property(e => e.InventoryLocationId).HasColumnName("InventoryLocationID");

                entity.Property(e => e.SalesGroupId).HasColumnName("SalesGroupID");

                entity.Property(e => e.ShipCarrierId).HasColumnName("ShipCarrierID");

                entity.Property(e => e.ShippingContactId).HasColumnName("ShippingContactID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.SubtotalDiscounts).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalFees).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalHandling).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalItems).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalShipping).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalTaxes).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TrackingNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Accounts.Account_AccountID");

                entity.HasOne(d => d.BillingContact)
                    .WithMany(p => p.PurchaseOrderBillingContacts)
                    .HasForeignKey(d => d.BillingContactId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Contacts.Contact_BillingContactID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Brands.Brand_BrandID");

                entity.HasOne(d => d.Franchise)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.FranchiseId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Franchises.Franchise_FranchiseID");

                entity.HasOne(d => d.InventoryLocation)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.InventoryLocationId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Inventory.InventoryLocation_InventoryLocationID");

                entity.HasOne(d => d.SalesGroup)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.SalesGroupId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Sales.SalesGroup_SalesGroupID");

                entity.HasOne(d => d.ShipCarrier)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.ShipCarrierId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Shipping.ShipCarrier_ShipCarrierID");

                entity.HasOne(d => d.ShippingContact)
                    .WithMany(p => p.PurchaseOrderShippingContacts)
                    .HasForeignKey(d => d.ShippingContactId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Contacts.Contact_ShippingContactID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Purchasing.PurchaseOrderState_StateID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Purchasing.PurchaseOrderStatus_StatusID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Purchasing.PurchaseOrderType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Contacts.User_UserID");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrder_Vendors.Vendor_VendorID");
            });

            modelBuilder.Entity<PurchaseOrderContact>(entity =>
            {
                entity.ToTable("PurchaseOrderContact", "Purchasing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PurchaseOrderContacts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderContact_Purchasing.PurchaseOrder_PurchaseOrderID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PurchaseOrderContacts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderContact_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<PurchaseOrderDiscount>(entity =>
            {
                entity.ToTable("PurchaseOrderDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PurchaseOrderDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.PurchaseOrderDiscounts_Purchasing.PurchaseOrder_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PurchaseOrderDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.PurchaseOrderDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<PurchaseOrderEvent>(entity =>
            {
                entity.ToTable("PurchaseOrderEvent", "Purchasing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NewStateId).HasColumnName("NewStateID");

                entity.Property(e => e.NewStatusId).HasColumnName("NewStatusID");

                entity.Property(e => e.NewTypeId).HasColumnName("NewTypeID");

                entity.Property(e => e.OldStateId).HasColumnName("OldStateID");

                entity.Property(e => e.OldStatusId).HasColumnName("OldStatusID");

                entity.Property(e => e.OldTypeId).HasColumnName("OldTypeID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PurchaseOrderEvents)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderEvent_Purchasing.PurchaseOrder_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.PurchaseOrderEvents)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderEvent_Purchasing.PurchaseOrderEventType_TypeID");
            });

            modelBuilder.Entity<PurchaseOrderEventType>(entity =>
            {
                entity.ToTable("PurchaseOrderEventType", "Purchasing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PurchaseOrderFile>(entity =>
            {
                entity.ToTable("PurchaseOrderFile", "Purchasing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PurchaseOrderFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderFileNew_Purchasing.PurchaseOrder_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PurchaseOrderFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderFileNew_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<PurchaseOrderItem>(entity =>
            {
                entity.ToTable("PurchaseOrderItem", "Purchasing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.OriginalCurrencyId, "IX_OriginalCurrencyID");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.SellingCurrencyId, "IX_SellingCurrencyID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ForceUniqueLineItemKey)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalCurrencyId).HasColumnName("OriginalCurrencyID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityBackOrdered).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPreSold).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SellingCurrencyId).HasColumnName("SellingCurrencyID");

                entity.Property(e => e.Sku)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCorePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitCorePriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitSoldPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitSoldPriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PurchaseOrderItems)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItem_Purchasing.PurchaseOrder_MasterID");

                entity.HasOne(d => d.OriginalCurrency)
                    .WithMany(p => p.PurchaseOrderItemOriginalCurrencies)
                    .HasForeignKey(d => d.OriginalCurrencyId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItem_Currencies.Currency_OriginalCurrencyID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseOrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItem_Products.Product_ProductID");

                entity.HasOne(d => d.SellingCurrency)
                    .WithMany(p => p.PurchaseOrderItemSellingCurrencies)
                    .HasForeignKey(d => d.SellingCurrencyId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItem_Currencies.Currency_SellingCurrencyID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PurchaseOrderItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItem_Contacts.User_UserID");
            });

            modelBuilder.Entity<PurchaseOrderItemDiscount>(entity =>
            {
                entity.ToTable("PurchaseOrderItemDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PurchaseOrderItemDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.PurchaseOrderItemDiscounts_Purchasing.PurchaseOrderItem_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.PurchaseOrderItemDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.PurchaseOrderItemDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<PurchaseOrderItemTarget>(entity =>
            {
                entity.ToTable("PurchaseOrderItemTarget", "Purchasing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandProductId, "IX_BrandProductID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DestinationContactId, "IX_DestinationContactID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.OriginProductInventoryLocationSectionId, "IX_OriginProductInventoryLocationSectionID");

                entity.HasIndex(e => e.OriginStoreProductId, "IX_OriginStoreProductID");

                entity.HasIndex(e => e.OriginVendorProductId, "IX_OriginVendorProductID");

                entity.HasIndex(e => e.SelectedRateQuoteId, "IX_SelectedRateQuoteID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandProductId).HasColumnName("BrandProductID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationContactId).HasColumnName("DestinationContactID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OriginProductInventoryLocationSectionId).HasColumnName("OriginProductInventoryLocationSectionID");

                entity.Property(e => e.OriginStoreProductId).HasColumnName("OriginStoreProductID");

                entity.Property(e => e.OriginVendorProductId).HasColumnName("OriginVendorProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SelectedRateQuoteId).HasColumnName("SelectedRateQuoteID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.BrandProduct)
                    .WithMany(p => p.PurchaseOrderItemTargets)
                    .HasForeignKey(d => d.BrandProductId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItemTarget_Brands.BrandProduct_BrandProductID");

                entity.HasOne(d => d.DestinationContact)
                    .WithMany(p => p.PurchaseOrderItemTargets)
                    .HasForeignKey(d => d.DestinationContactId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItemTarget_Contacts.Contact_DestinationContactID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.PurchaseOrderItemTargets)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItemTarget_Purchasing.PurchaseOrderItem_MasterID");

                entity.HasOne(d => d.OriginProductInventoryLocationSection)
                    .WithMany(p => p.PurchaseOrderItemTargets)
                    .HasForeignKey(d => d.OriginProductInventoryLocationSectionId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItemTarget_Products.ProductInventoryLocationSection_OriginProductInventoryLocationSectionID");

                entity.HasOne(d => d.OriginStoreProduct)
                    .WithMany(p => p.PurchaseOrderItemTargets)
                    .HasForeignKey(d => d.OriginStoreProductId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItemTarget_Stores.StoreProduct_OriginStoreProductID");

                entity.HasOne(d => d.OriginVendorProduct)
                    .WithMany(p => p.PurchaseOrderItemTargets)
                    .HasForeignKey(d => d.OriginVendorProductId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItemTarget_Vendors.VendorProduct_OriginVendorProductID");

                entity.HasOne(d => d.SelectedRateQuote)
                    .WithMany(p => p.PurchaseOrderItemTargets)
                    .HasForeignKey(d => d.SelectedRateQuoteId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItemTarget_Shipping.RateQuote_SelectedRateQuoteID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.PurchaseOrderItemTargets)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Purchasing.PurchaseOrderItemTarget_Sales.SalesItemTargetType_TypeID");
            });

            modelBuilder.Entity<PurchaseOrderState>(entity =>
            {
                entity.ToTable("PurchaseOrderState", "Purchasing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PurchaseOrderStatus>(entity =>
            {
                entity.ToTable("PurchaseOrderStatus", "Purchasing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PurchaseOrderType>(entity =>
            {
                entity.ToTable("PurchaseOrderType", "Purchasing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question", "Questionnaire");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.NextQuestionId, "IX_NextQuestionID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.NextQuestionId).HasColumnName("NextQuestionID");

                entity.Property(e => e.QuestionTranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Questionnaire.Question_Brands.Brand_BrandID");

                entity.HasOne(d => d.NextQuestion)
                    .WithMany(p => p.InverseNextQuestion)
                    .HasForeignKey(d => d.NextQuestionId)
                    .HasConstraintName("FK_Questionnaire.Question_Questionnaire.Question_NextQuestionID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Questionnaire.Question_Questionnaire.QuestionType_TypeID");
            });

            modelBuilder.Entity<QuestionOption>(entity =>
            {
                entity.ToTable("QuestionOption", "Questionnaire");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FollowUpQuestionId, "IX_FollowUpQuestionID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.QuestionId, "IX_QuestionID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FollowUpQuestionId).HasColumnName("FollowUpQuestionID");

                entity.Property(e => e.OptionTranslationKey)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.FollowUpQuestion)
                    .WithMany(p => p.QuestionOptionFollowUpQuestions)
                    .HasForeignKey(d => d.FollowUpQuestionId)
                    .HasConstraintName("FK_Questionnaire.QuestionOption_Questionnaire.Question_FollowUpQuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOptionQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Questionnaire.QuestionOption_Questionnaire.Question_QuestionID");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.ToTable("QuestionType", "Questionnaire");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RateQuote>(entity =>
            {
                entity.ToTable("RateQuote", "Shipping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CartId, "IX_CartID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.PurchaseOrderId, "IX_PurchaseOrderID");

                entity.HasIndex(e => e.SalesInvoiceId, "IX_SalesInvoiceID");

                entity.HasIndex(e => e.SalesOrderId, "IX_SalesOrderID");

                entity.HasIndex(e => e.SalesQuoteId, "IX_SalesQuoteID");

                entity.HasIndex(e => e.SalesReturnId, "IX_SalesReturnID");

                entity.HasIndex(e => e.SampleRequestId, "IX_SampleRequestID");

                entity.HasIndex(e => e.ShipCarrierMethodId, "IX_ShipCarrierMethodID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SalesInvoiceId).HasColumnName("SalesInvoiceID");

                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");

                entity.Property(e => e.SalesQuoteId).HasColumnName("SalesQuoteID");

                entity.Property(e => e.SalesReturnId).HasColumnName("SalesReturnID");

                entity.Property(e => e.SampleRequestId).HasColumnName("SampleRequestID");

                entity.Property(e => e.ShipCarrierMethodId).HasColumnName("ShipCarrierMethodID");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.RateQuotes)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK_Shipping.RateQuote_Shopping.Cart_CartID");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.RateQuotes)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .HasConstraintName("FK_Shipping.RateQuote_Purchasing.PurchaseOrder_PurchaseOrderID");

                entity.HasOne(d => d.SalesInvoice)
                    .WithMany(p => p.RateQuotes)
                    .HasForeignKey(d => d.SalesInvoiceId)
                    .HasConstraintName("FK_Shipping.RateQuote_Invoicing.SalesInvoice_SalesInvoiceID");

                entity.HasOne(d => d.SalesOrder)
                    .WithMany(p => p.RateQuotes)
                    .HasForeignKey(d => d.SalesOrderId)
                    .HasConstraintName("FK_Shipping.RateQuote_Ordering.SalesOrder_SalesOrderID");

                entity.HasOne(d => d.SalesQuote)
                    .WithMany(p => p.RateQuotes)
                    .HasForeignKey(d => d.SalesQuoteId)
                    .HasConstraintName("FK_Shipping.RateQuote_Quoting.SalesQuote_SalesQuoteID");

                entity.HasOne(d => d.SalesReturn)
                    .WithMany(p => p.RateQuotes)
                    .HasForeignKey(d => d.SalesReturnId)
                    .HasConstraintName("FK_Shipping.RateQuote_Returning.SalesReturn_SalesReturnID");

                entity.HasOne(d => d.SampleRequest)
                    .WithMany(p => p.RateQuotes)
                    .HasForeignKey(d => d.SampleRequestId)
                    .HasConstraintName("FK_Shipping.RateQuote_Sampling.SampleRequest_SampleRequestID");

                entity.HasOne(d => d.ShipCarrierMethod)
                    .WithMany(p => p.RateQuotes)
                    .HasForeignKey(d => d.ShipCarrierMethodId)
                    .HasConstraintName("FK_Shipping.RateQuote_Shipping.ShipCarrierMethod_ShipCarrierMethodID");
            });

            modelBuilder.Entity<RecordVersion>(entity =>
            {
                entity.ToTable("RecordVersion", "System");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.PublishedByUserId, "IX_PublishedByUserID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PublishedByUserId).HasColumnName("PublishedByUserID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.RecordVersions)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_System.RecordVersion_Brands.Brand_BrandID");

                entity.HasOne(d => d.PublishedByUser)
                    .WithMany(p => p.RecordVersions)
                    .HasForeignKey(d => d.PublishedByUserId)
                    .HasConstraintName("FK_System.RecordVersion_Contacts.User_PublishedByUserID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.RecordVersions)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_System.RecordVersion_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.RecordVersions)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_System.RecordVersion_System.RecordVersionType_TypeID");
            });

            modelBuilder.Entity<RecordVersionType>(entity =>
            {
                entity.ToTable("RecordVersionType", "System");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReferralCode>(entity =>
            {
                entity.ToTable("ReferralCode", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ReferralCodes)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Contacts.ReferralCode_Contacts.ReferralCodeStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ReferralCodes)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Contacts.ReferralCode_Contacts.ReferralCodeType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReferralCodes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Contacts.ReferralCode_Contacts.User_UserID");
            });

            modelBuilder.Entity<ReferralCodeStatus>(entity =>
            {
                entity.ToTable("ReferralCodeStatus", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReferralCodeType>(entity =>
            {
                entity.ToTable("ReferralCodeType", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CountryId, "IX_CountryID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Iso31661)
                    .HasMaxLength(10)
                    .HasColumnName("ISO31661");

                entity.Property(e => e.Iso31662)
                    .HasMaxLength(10)
                    .HasColumnName("ISO31662");

                entity.Property(e => e.Iso3166alpha2)
                    .HasMaxLength(10)
                    .HasColumnName("ISO3166Alpha2");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Regions)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Geography.Region_Geography.Country_CountryID");
            });

            modelBuilder.Entity<RegionCurrency>(entity =>
            {
                entity.ToTable("RegionCurrency", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.RegionCurrencies)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Geography.RegionCurrency_Geography.Region_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.RegionCurrencies)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Geography.RegionCurrency_Currencies.Currency_CurrencyID");
            });

            modelBuilder.Entity<RegionImage>(entity =>
            {
                entity.ToTable("RegionImage", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.RegionImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Geography.RegionImage_Geography.Region_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.RegionImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Geography.RegionImage_Geography.RegionImageType_TypeID");
            });

            modelBuilder.Entity<RegionImageType>(entity =>
            {
                entity.ToTable("RegionImageType", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RegionLanguage>(entity =>
            {
                entity.ToTable("RegionLanguage", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.RegionLanguages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Geography.RegionLanguage_Geography.Region_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.RegionLanguages)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Geography.RegionLanguage_Globalization.Language_LanguageID");
            });

            modelBuilder.Entity<RepeatType>(entity =>
            {
                entity.ToTable("RepeatType", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Reports", "Reporting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.RunByUserId, "IX_RunByUserID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RunByUserId).HasColumnName("RunByUserID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.RunByUser)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.RunByUserId)
                    .HasConstraintName("FK_Reporting.Reports_Contacts.User_RunByUserID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Reporting.Reports_Reporting.ReportTypes_TypeID");
            });

            modelBuilder.Entity<ReportType>(entity =>
            {
                entity.ToTable("ReportTypes", "Reporting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review", "Reviews");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ApprovedByUserId, "IX_ApprovedByUserID");

                entity.HasIndex(e => e.CategoryId, "IX_CategoryID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.ManufacturerId, "IX_ManufacturerID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.SubmittedByUserId, "IX_SubmittedByUserID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.HasIndex(e => e.VendorId, "IX_VendorID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedByUserId).HasColumnName("ApprovedByUserID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.SubmittedByUserId).HasColumnName("SubmittedByUserID");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.HasOne(d => d.ApprovedByUser)
                    .WithMany(p => p.ReviewApprovedByUsers)
                    .HasForeignKey(d => d.ApprovedByUserId)
                    .HasConstraintName("FK_Reviews.Review_Contacts.User_ApprovedByUserID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Reviews.Review_Categories.Category_CategoryID");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("FK_Reviews.Review_Manufacturers.Manufacturer_ManufacturerID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Reviews.Review_Products.Product_ProductID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Reviews.Review_Stores.Store_StoreID");

                entity.HasOne(d => d.SubmittedByUser)
                    .WithMany(p => p.ReviewSubmittedByUsers)
                    .HasForeignKey(d => d.SubmittedByUserId)
                    .HasConstraintName("FK_Reviews.Review_Contacts.User_SubmittedByUserID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Reviews.Review_Reviews.ReviewType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReviewUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Reviews.Review_Contacts.User_UserID");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_Reviews.Review_Vendors.Vendor_VendorID");
            });

            modelBuilder.Entity<ReviewType>(entity =>
            {
                entity.ToTable("ReviewType", "Reviews");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.UserId })
                    .HasName("PK_Contacts.RoleUser");

                entity.ToTable("RoleUser", "Contacts");

                entity.HasIndex(e => e.GroupId, "IX_GroupID");

                entity.HasIndex(e => e.RoleId, "IX_RoleId");

                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Contacts.RoleUser_Groups.Group_GroupID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contacts.RoleUser_Contacts.UserRole_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contacts.RoleUser_Contacts.User_UserId");
            });

            modelBuilder.Entity<SalesGroup>(entity =>
            {
                entity.ToTable("SalesGroup", "Sales");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BillingContactId, "IX_BillingContactID");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BillingContactId).HasColumnName("BillingContactID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SalesGroups)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Sales.SalesGroup_Accounts.Account_AccountID");

                entity.HasOne(d => d.BillingContact)
                    .WithMany(p => p.SalesGroups)
                    .HasForeignKey(d => d.BillingContactId)
                    .HasConstraintName("FK_Sales.SalesGroup_Contacts.Contact_BillingContactID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.SalesGroups)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Sales.SalesGroup_Brands.Brand_BrandID");
            });

            modelBuilder.Entity<SalesInvoice>(entity =>
            {
                entity.ToTable("SalesInvoice", "Invoicing");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BillingContactId, "IX_BillingContactID");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FranchiseId, "IX_FranchiseID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.SalesGroupId, "IX_SalesGroupID");

                entity.HasIndex(e => e.ShippingContactId, "IX_ShippingContactID");

                entity.HasIndex(e => e.StateId, "IX_StateID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BalanceDue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BillingContactId).HasColumnName("BillingContactID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseId).HasColumnName("FranchiseID");

                entity.Property(e => e.SalesGroupId).HasColumnName("SalesGroupID");

                entity.Property(e => e.ShippingContactId).HasColumnName("ShippingContactID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.SubtotalDiscounts).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalFees).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalHandling).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalItems).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalShipping).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalTaxes).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Accounts.Account_AccountID");

                entity.HasOne(d => d.BillingContact)
                    .WithMany(p => p.SalesInvoiceBillingContacts)
                    .HasForeignKey(d => d.BillingContactId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Contacts.Contact_BillingContactID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Brands.Brand_BrandID");

                entity.HasOne(d => d.Franchise)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.FranchiseId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Franchises.Franchise_FranchiseID");

                entity.HasOne(d => d.SalesGroup)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.SalesGroupId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Sales.SalesGroup_SalesGroupID");

                entity.HasOne(d => d.ShippingContact)
                    .WithMany(p => p.SalesInvoiceShippingContacts)
                    .HasForeignKey(d => d.ShippingContactId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Contacts.Contact_ShippingContactID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Invoicing.SalesInvoiceState_StateID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Invoicing.SalesInvoiceStatus_StatusID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Invoicing.SalesInvoiceType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Invoicing.SalesInvoice_Contacts.User_UserID");
            });

            modelBuilder.Entity<SalesInvoiceContact>(entity =>
            {
                entity.ToTable("SalesInvoiceContact", "Invoicing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesInvoiceContacts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceContact_Invoicing.SalesInvoice_SalesInvoiceID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesInvoiceContacts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceContact_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<SalesInvoiceDiscount>(entity =>
            {
                entity.ToTable("SalesInvoiceDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesInvoiceDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.SalesInvoiceDiscounts_Invoicing.SalesInvoice_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesInvoiceDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.SalesInvoiceDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<SalesInvoiceEvent>(entity =>
            {
                entity.ToTable("SalesInvoiceEvent", "Invoicing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NewBalanceDue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.NewStateId).HasColumnName("NewStateID");

                entity.Property(e => e.NewStatusId).HasColumnName("NewStatusID");

                entity.Property(e => e.NewTypeId).HasColumnName("NewTypeID");

                entity.Property(e => e.OldBalanceDue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.OldStateId).HasColumnName("OldStateID");

                entity.Property(e => e.OldStatusId).HasColumnName("OldStatusID");

                entity.Property(e => e.OldTypeId).HasColumnName("OldTypeID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesInvoiceEvents)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceEvent_Invoicing.SalesInvoice_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesInvoiceEvents)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceEvent_Invoicing.SalesInvoiceEventType_TypeID");
            });

            modelBuilder.Entity<SalesInvoiceEventType>(entity =>
            {
                entity.ToTable("SalesInvoiceEventType", "Invoicing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesInvoiceFile>(entity =>
            {
                entity.ToTable("SalesInvoiceFile", "Invoicing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesInvoiceFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceFileNew_Invoicing.SalesInvoice_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesInvoiceFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceFileNew_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<SalesInvoiceItem>(entity =>
            {
                entity.ToTable("SalesInvoiceItem", "Invoicing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.OriginalCurrencyId, "IX_OriginalCurrencyID");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.SellingCurrencyId, "IX_SellingCurrencyID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ForceUniqueLineItemKey)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalCurrencyId).HasColumnName("OriginalCurrencyID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityBackOrdered).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPreSold).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SellingCurrencyId).HasColumnName("SellingCurrencyID");

                entity.Property(e => e.Sku)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCorePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitCorePriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitSoldPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitSoldPriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesInvoiceItems)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItem_Invoicing.SalesInvoice_MasterID");

                entity.HasOne(d => d.OriginalCurrency)
                    .WithMany(p => p.SalesInvoiceItemOriginalCurrencies)
                    .HasForeignKey(d => d.OriginalCurrencyId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItem_Currencies.Currency_OriginalCurrencyID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesInvoiceItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItem_Products.Product_ProductID");

                entity.HasOne(d => d.SellingCurrency)
                    .WithMany(p => p.SalesInvoiceItemSellingCurrencies)
                    .HasForeignKey(d => d.SellingCurrencyId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItem_Currencies.Currency_SellingCurrencyID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SalesInvoiceItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItem_Contacts.User_UserID");
            });

            modelBuilder.Entity<SalesInvoiceItemDiscount>(entity =>
            {
                entity.ToTable("SalesInvoiceItemDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesInvoiceItemDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.SalesInvoiceItemDiscounts_Invoicing.SalesInvoiceItem_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesInvoiceItemDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.SalesInvoiceItemDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<SalesInvoiceItemTarget>(entity =>
            {
                entity.ToTable("SalesInvoiceItemTarget", "Invoicing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandProductId, "IX_BrandProductID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DestinationContactId, "IX_DestinationContactID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.OriginProductInventoryLocationSectionId, "IX_OriginProductInventoryLocationSectionID");

                entity.HasIndex(e => e.OriginStoreProductId, "IX_OriginStoreProductID");

                entity.HasIndex(e => e.OriginVendorProductId, "IX_OriginVendorProductID");

                entity.HasIndex(e => e.SelectedRateQuoteId, "IX_SelectedRateQuoteID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandProductId).HasColumnName("BrandProductID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationContactId).HasColumnName("DestinationContactID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OriginProductInventoryLocationSectionId).HasColumnName("OriginProductInventoryLocationSectionID");

                entity.Property(e => e.OriginStoreProductId).HasColumnName("OriginStoreProductID");

                entity.Property(e => e.OriginVendorProductId).HasColumnName("OriginVendorProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SelectedRateQuoteId).HasColumnName("SelectedRateQuoteID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.BrandProduct)
                    .WithMany(p => p.SalesInvoiceItemTargets)
                    .HasForeignKey(d => d.BrandProductId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItemTarget_Brands.BrandProduct_BrandProductID");

                entity.HasOne(d => d.DestinationContact)
                    .WithMany(p => p.SalesInvoiceItemTargets)
                    .HasForeignKey(d => d.DestinationContactId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItemTarget_Contacts.Contact_DestinationContactID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesInvoiceItemTargets)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItemTarget_Invoicing.SalesInvoiceItem_MasterID");

                entity.HasOne(d => d.OriginProductInventoryLocationSection)
                    .WithMany(p => p.SalesInvoiceItemTargets)
                    .HasForeignKey(d => d.OriginProductInventoryLocationSectionId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItemTarget_Products.ProductInventoryLocationSection_OriginProductInventoryLocationSectionID");

                entity.HasOne(d => d.OriginStoreProduct)
                    .WithMany(p => p.SalesInvoiceItemTargets)
                    .HasForeignKey(d => d.OriginStoreProductId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItemTarget_Stores.StoreProduct_OriginStoreProductID");

                entity.HasOne(d => d.OriginVendorProduct)
                    .WithMany(p => p.SalesInvoiceItemTargets)
                    .HasForeignKey(d => d.OriginVendorProductId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItemTarget_Vendors.VendorProduct_OriginVendorProductID");

                entity.HasOne(d => d.SelectedRateQuote)
                    .WithMany(p => p.SalesInvoiceItemTargets)
                    .HasForeignKey(d => d.SelectedRateQuoteId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItemTarget_Shipping.RateQuote_SelectedRateQuoteID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesInvoiceItemTargets)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Invoicing.SalesInvoiceItemTarget_Sales.SalesItemTargetType_TypeID");
            });

            modelBuilder.Entity<SalesInvoicePayment>(entity =>
            {
                entity.ToTable("SalesInvoicePayment", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesInvoicePayments)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Payments.SalesInvoicePayment_Invoicing.SalesInvoice_SalesInvoiceID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesInvoicePayments)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Payments.SalesInvoicePayment_Payments.Payment_PaymentID");
            });

            modelBuilder.Entity<SalesInvoiceState>(entity =>
            {
                entity.ToTable("SalesInvoiceState", "Invoicing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesInvoiceStatus>(entity =>
            {
                entity.ToTable("SalesInvoiceStatus", "Invoicing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesInvoiceType>(entity =>
            {
                entity.ToTable("SalesInvoiceType", "Invoicing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesItemTargetType>(entity =>
            {
                entity.ToTable("SalesItemTargetType", "Sales");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.ToTable("SalesOrder", "Ordering");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BillingContactId, "IX_BillingContactID");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FranchiseId, "IX_FranchiseID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.InventoryLocationId, "IX_InventoryLocationID");

                entity.HasIndex(e => e.SalesGroupAsSubId, "IX_SalesGroupAsSubID");

                entity.HasIndex(e => e.ShippingContactId, "IX_ShippingContactID");

                entity.HasIndex(e => e.StateId, "IX_StateID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BalanceDue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BillingContactId).HasColumnName("BillingContactID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseId).HasColumnName("FranchiseID");

                entity.Property(e => e.InventoryLocationId).HasColumnName("InventoryLocationID");

                entity.Property(e => e.PaymentTransactionId)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("PaymentTransactionID");

                entity.Property(e => e.PurchaseOrderNumber).HasMaxLength(100);

                entity.Property(e => e.SalesGroupAsMasterId).HasColumnName("SalesGroupAsMasterID");

                entity.Property(e => e.SalesGroupAsSubId).HasColumnName("SalesGroupAsSubID");

                entity.Property(e => e.ShippingContactId).HasColumnName("ShippingContactID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.SubtotalDiscounts).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalFees).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalHandling).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalItems).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalShipping).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalTaxes).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxTransactionId)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("TaxTransactionID");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Accounts.Account_AccountID");

                entity.HasOne(d => d.BillingContact)
                    .WithMany(p => p.SalesOrderBillingContacts)
                    .HasForeignKey(d => d.BillingContactId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Contacts.Contact_BillingContactID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Brands.Brand_BrandID");

                entity.HasOne(d => d.Franchise)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.FranchiseId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Franchises.Franchise_FranchiseID");

                entity.HasOne(d => d.InventoryLocation)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.InventoryLocationId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Inventory.InventoryLocation_InventoryLocationID");

                entity.HasOne(d => d.SalesGroupAsMaster)
                    .WithMany(p => p.SalesOrderSalesGroupAsMasters)
                    .HasForeignKey(d => d.SalesGroupAsMasterId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Sales.SalesGroup_SalesGroupAsMasterID");

                entity.HasOne(d => d.SalesGroupAsSub)
                    .WithMany(p => p.SalesOrderSalesGroupAsSubs)
                    .HasForeignKey(d => d.SalesGroupAsSubId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Sales.SalesGroup_SalesGroupAsSubID");

                entity.HasOne(d => d.ShippingContact)
                    .WithMany(p => p.SalesOrderShippingContacts)
                    .HasForeignKey(d => d.ShippingContactId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Contacts.Contact_ShippingContactID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Ordering.SalesOrderState_StateID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Ordering.SalesOrderStatus_StatusID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Ordering.SalesOrderType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Ordering.SalesOrder_Contacts.User_UserID");
            });

            modelBuilder.Entity<SalesOrderContact>(entity =>
            {
                entity.ToTable("SalesOrderContact", "Ordering");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesOrderContacts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Ordering.SalesOrderContact_Ordering.SalesOrder_SalesOrderID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesOrderContacts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Ordering.SalesOrderContact_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<SalesOrderDiscount>(entity =>
            {
                entity.ToTable("SalesOrderDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesOrderDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.SalesOrderDiscounts_Ordering.SalesOrder_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesOrderDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.SalesOrderDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<SalesOrderEvent>(entity =>
            {
                entity.ToTable("SalesOrderEvent", "Ordering");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NewBalanceDue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.NewStateId).HasColumnName("NewStateID");

                entity.Property(e => e.NewStatusId).HasColumnName("NewStatusID");

                entity.Property(e => e.NewTypeId).HasColumnName("NewTypeID");

                entity.Property(e => e.OldBalanceDue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.OldStateId).HasColumnName("OldStateID");

                entity.Property(e => e.OldStatusId).HasColumnName("OldStatusID");

                entity.Property(e => e.OldTypeId).HasColumnName("OldTypeID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesOrderEvents)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Ordering.SalesOrderEvent_Ordering.SalesOrder_SalesOrderID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesOrderEvents)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Ordering.SalesOrderEvent_Ordering.SalesOrderEventType_TypeID");
            });

            modelBuilder.Entity<SalesOrderEventType>(entity =>
            {
                entity.ToTable("SalesOrderEventType", "Ordering");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesOrderFile>(entity =>
            {
                entity.ToTable("SalesOrderFile", "Ordering");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesOrderFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Ordering.SalesOrderFileNew_Ordering.SalesOrder_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesOrderFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Ordering.SalesOrderFileNew_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<SalesOrderItem>(entity =>
            {
                entity.ToTable("SalesOrderItem", "Ordering");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.OriginalCurrencyId, "IX_OriginalCurrencyID");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.SellingCurrencyId, "IX_SellingCurrencyID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ForceUniqueLineItemKey)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalCurrencyId).HasColumnName("OriginalCurrencyID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityBackOrdered).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPreSold).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SellingCurrencyId).HasColumnName("SellingCurrencyID");

                entity.Property(e => e.Sku)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCorePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitCorePriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitSoldPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitSoldPriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesOrderItems)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Ordering.SalesOrderItem_Ordering.SalesOrder_MasterID");

                entity.HasOne(d => d.OriginalCurrency)
                    .WithMany(p => p.SalesOrderItemOriginalCurrencies)
                    .HasForeignKey(d => d.OriginalCurrencyId)
                    .HasConstraintName("FK_Ordering.SalesOrderItem_Currencies.Currency_OriginalCurrencyID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesOrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Ordering.SalesOrderItem_Products.Product_ProductID");

                entity.HasOne(d => d.SellingCurrency)
                    .WithMany(p => p.SalesOrderItemSellingCurrencies)
                    .HasForeignKey(d => d.SellingCurrencyId)
                    .HasConstraintName("FK_Ordering.SalesOrderItem_Currencies.Currency_SellingCurrencyID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SalesOrderItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Ordering.SalesOrderItem_Contacts.User_UserID");
            });

            modelBuilder.Entity<SalesOrderItemDiscount>(entity =>
            {
                entity.ToTable("SalesOrderItemDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesOrderItemDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.SalesOrderItemDiscounts_Ordering.SalesOrderItem_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesOrderItemDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.SalesOrderItemDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<SalesOrderItemTarget>(entity =>
            {
                entity.ToTable("SalesOrderItemTarget", "Ordering");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandProductId, "IX_BrandProductID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DestinationContactId, "IX_DestinationContactID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.OriginProductInventoryLocationSectionId, "IX_OriginProductInventoryLocationSectionID");

                entity.HasIndex(e => e.OriginStoreProductId, "IX_OriginStoreProductID");

                entity.HasIndex(e => e.OriginVendorProductId, "IX_OriginVendorProductID");

                entity.HasIndex(e => e.SelectedRateQuoteId, "IX_SelectedRateQuoteID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandProductId).HasColumnName("BrandProductID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationContactId).HasColumnName("DestinationContactID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OriginProductInventoryLocationSectionId).HasColumnName("OriginProductInventoryLocationSectionID");

                entity.Property(e => e.OriginStoreProductId).HasColumnName("OriginStoreProductID");

                entity.Property(e => e.OriginVendorProductId).HasColumnName("OriginVendorProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SelectedRateQuoteId).HasColumnName("SelectedRateQuoteID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.BrandProduct)
                    .WithMany(p => p.SalesOrderItemTargets)
                    .HasForeignKey(d => d.BrandProductId)
                    .HasConstraintName("FK_Ordering.SalesOrderItemTarget_Brands.BrandProduct_BrandProductID");

                entity.HasOne(d => d.DestinationContact)
                    .WithMany(p => p.SalesOrderItemTargets)
                    .HasForeignKey(d => d.DestinationContactId)
                    .HasConstraintName("FK_Ordering.SalesOrderItemTarget_Contacts.Contact_DestinationContactID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesOrderItemTargets)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Ordering.SalesOrderItemTarget_Ordering.SalesOrderItem_MasterID");

                entity.HasOne(d => d.OriginProductInventoryLocationSection)
                    .WithMany(p => p.SalesOrderItemTargets)
                    .HasForeignKey(d => d.OriginProductInventoryLocationSectionId)
                    .HasConstraintName("FK_Ordering.SalesOrderItemTarget_Products.ProductInventoryLocationSection_OriginProductInventoryLocationSectionID");

                entity.HasOne(d => d.OriginStoreProduct)
                    .WithMany(p => p.SalesOrderItemTargets)
                    .HasForeignKey(d => d.OriginStoreProductId)
                    .HasConstraintName("FK_Ordering.SalesOrderItemTarget_Stores.StoreProduct_OriginStoreProductID");

                entity.HasOne(d => d.OriginVendorProduct)
                    .WithMany(p => p.SalesOrderItemTargets)
                    .HasForeignKey(d => d.OriginVendorProductId)
                    .HasConstraintName("FK_Ordering.SalesOrderItemTarget_Vendors.VendorProduct_OriginVendorProductID");

                entity.HasOne(d => d.SelectedRateQuote)
                    .WithMany(p => p.SalesOrderItemTargets)
                    .HasForeignKey(d => d.SelectedRateQuoteId)
                    .HasConstraintName("FK_Ordering.SalesOrderItemTarget_Shipping.RateQuote_SelectedRateQuoteID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesOrderItemTargets)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Ordering.SalesOrderItemTarget_Sales.SalesItemTargetType_TypeID");
            });

            modelBuilder.Entity<SalesOrderPayment>(entity =>
            {
                entity.ToTable("SalesOrderPayment", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesOrderPayments)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Payments.SalesOrderPayment_Ordering.SalesOrder_SalesOrderID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesOrderPayments)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Payments.SalesOrderPayment_Payments.Payment_PaymentID");
            });

            modelBuilder.Entity<SalesOrderPurchaseOrder>(entity =>
            {
                entity.ToTable("SalesOrderPurchaseOrder", "Purchasing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesOrderPurchaseOrders)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Purchasing.SalesOrderPurchaseOrder_Ordering.SalesOrder_SalesOrderID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesOrderPurchaseOrders)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Purchasing.SalesOrderPurchaseOrder_Purchasing.PurchaseOrder_PurchaseOrderID");
            });

            modelBuilder.Entity<SalesOrderSalesInvoice>(entity =>
            {
                entity.ToTable("SalesOrderSalesInvoice", "Invoicing");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesOrderSalesInvoices)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Invoicing.SalesOrderSalesInvoice_Ordering.SalesOrder_SalesOrderID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesOrderSalesInvoices)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Invoicing.SalesOrderSalesInvoice_Invoicing.SalesInvoice_SalesInvoiceID");
            });

            modelBuilder.Entity<SalesOrderState>(entity =>
            {
                entity.ToTable("SalesOrderState", "Ordering");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesOrderStatus>(entity =>
            {
                entity.ToTable("SalesOrderStatus", "Ordering");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesOrderType>(entity =>
            {
                entity.ToTable("SalesOrderType", "Ordering");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesQuote>(entity =>
            {
                entity.ToTable("SalesQuote", "Quoting");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BillingContactId, "IX_BillingContactID");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FranchiseId, "IX_FranchiseID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.ResponseAsStoreId, "IX_ResponseAsStoreID");

                entity.HasIndex(e => e.ResponseAsVendorId, "IX_ResponseAsVendorID");

                entity.HasIndex(e => e.SalesGroupAsRequestMasterId, "IX_SalesGroupAsRequestMasterID");

                entity.HasIndex(e => e.SalesGroupAsRequestSubId, "IX_SalesGroupAsRequestSubID");

                entity.HasIndex(e => e.SalesGroupAsResponseMasterId, "IX_SalesGroupAsResponseMasterID");

                entity.HasIndex(e => e.SalesGroupAsResponseSubId, "IX_SalesGroupAsResponseSubID");

                entity.HasIndex(e => e.ShippingContactId, "IX_ShippingContactID");

                entity.HasIndex(e => e.StateId, "IX_StateID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BillingContactId).HasColumnName("BillingContactID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseId).HasColumnName("FranchiseID");

                entity.Property(e => e.ResponseAsStoreId).HasColumnName("ResponseAsStoreID");

                entity.Property(e => e.ResponseAsVendorId).HasColumnName("ResponseAsVendorID");

                entity.Property(e => e.SalesGroupAsRequestMasterId).HasColumnName("SalesGroupAsRequestMasterID");

                entity.Property(e => e.SalesGroupAsRequestSubId).HasColumnName("SalesGroupAsRequestSubID");

                entity.Property(e => e.SalesGroupAsResponseMasterId).HasColumnName("SalesGroupAsResponseMasterID");

                entity.Property(e => e.SalesGroupAsResponseSubId).HasColumnName("SalesGroupAsResponseSubID");

                entity.Property(e => e.ShippingContactId).HasColumnName("ShippingContactID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.SubtotalDiscounts).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalDiscountsModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalFees).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalFeesModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalHandling).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalHandlingModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalItems).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalShipping).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalShippingModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalTaxes).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalTaxesModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SalesQuotes)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Accounts.Account_AccountID");

                entity.HasOne(d => d.BillingContact)
                    .WithMany(p => p.SalesQuoteBillingContacts)
                    .HasForeignKey(d => d.BillingContactId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Contacts.Contact_BillingContactID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.SalesQuotes)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Brands.Brand_BrandID");

                entity.HasOne(d => d.Franchise)
                    .WithMany(p => p.SalesQuotes)
                    .HasForeignKey(d => d.FranchiseId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Franchises.Franchise_FranchiseID");

                entity.HasOne(d => d.ResponseAsStore)
                    .WithMany(p => p.SalesQuoteResponseAsStores)
                    .HasForeignKey(d => d.ResponseAsStoreId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Stores.Store_ResponseAsStoreID");

                entity.HasOne(d => d.ResponseAsVendor)
                    .WithMany(p => p.SalesQuotes)
                    .HasForeignKey(d => d.ResponseAsVendorId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Vendors.Vendor_ResponseAsVendorID");

                entity.HasOne(d => d.SalesGroupAsRequestMaster)
                    .WithMany(p => p.SalesQuoteSalesGroupAsRequestMasters)
                    .HasForeignKey(d => d.SalesGroupAsRequestMasterId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Sales.SalesGroup_SalesGroupAsMasterID");

                entity.HasOne(d => d.SalesGroupAsRequestSub)
                    .WithMany(p => p.SalesQuoteSalesGroupAsRequestSubs)
                    .HasForeignKey(d => d.SalesGroupAsRequestSubId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Sales.SalesGroup_SalesGroupAsRequestSubID");

                entity.HasOne(d => d.SalesGroupAsResponseMaster)
                    .WithMany(p => p.SalesQuoteSalesGroupAsResponseMasters)
                    .HasForeignKey(d => d.SalesGroupAsResponseMasterId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Sales.SalesGroup_SalesGroupAsResponseID");

                entity.HasOne(d => d.SalesGroupAsResponseSub)
                    .WithMany(p => p.SalesQuoteSalesGroupAsResponseSubs)
                    .HasForeignKey(d => d.SalesGroupAsResponseSubId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Sales.SalesGroup_SalesGroupAsResponseSubID");

                entity.HasOne(d => d.ShippingContact)
                    .WithMany(p => p.SalesQuoteShippingContacts)
                    .HasForeignKey(d => d.ShippingContactId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Contacts.Contact_ShippingContactID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.SalesQuotes)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Quoting.SalesQuoteState_StateID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.SalesQuotes)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Quoting.SalesQuoteStatus_StatusID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SalesQuoteStores)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesQuotes)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Quoting.SalesQuoteType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SalesQuotes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Quoting.SalesQuote_Contacts.User_UserID");
            });

            modelBuilder.Entity<SalesQuoteCategory>(entity =>
            {
                entity.ToTable("SalesQuoteCategory", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesQuoteCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Quoting.SalesQuoteCategory_Quoting.SalesQuote_SalesQuoteID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesQuoteCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Quoting.SalesQuoteCategory_Categories.Category_CategoryID");
            });

            modelBuilder.Entity<SalesQuoteContact>(entity =>
            {
                entity.ToTable("SalesQuoteContact", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesQuoteContacts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Quoting.SalesQuoteContact_Quoting.SalesQuote_SalesQuoteID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesQuoteContacts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Quoting.SalesQuoteContact_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<SalesQuoteDiscount>(entity =>
            {
                entity.ToTable("SalesQuoteDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesQuoteDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.SalesQuoteDiscounts_Quoting.SalesQuote_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesQuoteDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.SalesQuoteDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<SalesQuoteEvent>(entity =>
            {
                entity.ToTable("SalesQuoteEvent", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NewStateId).HasColumnName("NewStateID");

                entity.Property(e => e.NewStatusId).HasColumnName("NewStatusID");

                entity.Property(e => e.NewTypeId).HasColumnName("NewTypeID");

                entity.Property(e => e.OldStateId).HasColumnName("OldStateID");

                entity.Property(e => e.OldStatusId).HasColumnName("OldStatusID");

                entity.Property(e => e.OldTypeId).HasColumnName("OldTypeID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesQuoteEvents)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Quoting.SalesQuoteEvent_Quoting.SalesQuote_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesQuoteEvents)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Quoting.SalesQuoteEvent_Quoting.SalesQuoteEventType_TypeID");
            });

            modelBuilder.Entity<SalesQuoteEventType>(entity =>
            {
                entity.ToTable("SalesQuoteEventType", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesQuoteFile>(entity =>
            {
                entity.ToTable("SalesQuoteFile", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesQuoteFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Quoting.SalesQuoteFileNew_Quoting.SalesQuote_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesQuoteFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Quoting.SalesQuoteFileNew_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<SalesQuoteItem>(entity =>
            {
                entity.ToTable("SalesQuoteItem", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.OriginalCurrencyId, "IX_OriginalCurrencyID");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.SellingCurrencyId, "IX_SellingCurrencyID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ForceUniqueLineItemKey)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalCurrencyId).HasColumnName("OriginalCurrencyID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityBackOrdered).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPreSold).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SellingCurrencyId).HasColumnName("SellingCurrencyID");

                entity.Property(e => e.Sku)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCorePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitCorePriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitSoldPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitSoldPriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitSoldPriceModifier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesQuoteItems)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItem_Quoting.SalesQuote_MasterID");

                entity.HasOne(d => d.OriginalCurrency)
                    .WithMany(p => p.SalesQuoteItemOriginalCurrencies)
                    .HasForeignKey(d => d.OriginalCurrencyId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItem_Currencies.Currency_OriginalCurrencyID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesQuoteItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItem_Products.Product_ProductID");

                entity.HasOne(d => d.SellingCurrency)
                    .WithMany(p => p.SalesQuoteItemSellingCurrencies)
                    .HasForeignKey(d => d.SellingCurrencyId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItem_Currencies.Currency_SellingCurrencyID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SalesQuoteItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItem_Contacts.User_UserID");
            });

            modelBuilder.Entity<SalesQuoteItemDiscount>(entity =>
            {
                entity.ToTable("SalesQuoteItemDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesQuoteItemDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.QuoteItemDiscounts_Quoting.SalesQuoteItem_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesQuoteItemDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.QuoteItemDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<SalesQuoteItemTarget>(entity =>
            {
                entity.ToTable("SalesQuoteItemTarget", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandProductId, "IX_BrandProductID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DestinationContactId, "IX_DestinationContactID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.OriginProductInventoryLocationSectionId, "IX_OriginProductInventoryLocationSectionID");

                entity.HasIndex(e => e.OriginStoreProductId, "IX_OriginStoreProductID");

                entity.HasIndex(e => e.OriginVendorProductId, "IX_OriginVendorProductID");

                entity.HasIndex(e => e.SelectedRateQuoteId, "IX_SelectedRateQuoteID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandProductId).HasColumnName("BrandProductID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationContactId).HasColumnName("DestinationContactID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OriginProductInventoryLocationSectionId).HasColumnName("OriginProductInventoryLocationSectionID");

                entity.Property(e => e.OriginStoreProductId).HasColumnName("OriginStoreProductID");

                entity.Property(e => e.OriginVendorProductId).HasColumnName("OriginVendorProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SelectedRateQuoteId).HasColumnName("SelectedRateQuoteID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.BrandProduct)
                    .WithMany(p => p.SalesQuoteItemTargets)
                    .HasForeignKey(d => d.BrandProductId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItemTarget_Brands.BrandProduct_BrandProductID");

                entity.HasOne(d => d.DestinationContact)
                    .WithMany(p => p.SalesQuoteItemTargets)
                    .HasForeignKey(d => d.DestinationContactId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItemTarget_Contacts.Contact_DestinationContactID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesQuoteItemTargets)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItemTarget_Quoting.SalesQuoteItem_MasterID");

                entity.HasOne(d => d.OriginProductInventoryLocationSection)
                    .WithMany(p => p.SalesQuoteItemTargets)
                    .HasForeignKey(d => d.OriginProductInventoryLocationSectionId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItemTarget_Products.ProductInventoryLocationSection_OriginProductInventoryLocationSectionID");

                entity.HasOne(d => d.OriginStoreProduct)
                    .WithMany(p => p.SalesQuoteItemTargets)
                    .HasForeignKey(d => d.OriginStoreProductId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItemTarget_Stores.StoreProduct_OriginStoreProductID");

                entity.HasOne(d => d.OriginVendorProduct)
                    .WithMany(p => p.SalesQuoteItemTargets)
                    .HasForeignKey(d => d.OriginVendorProductId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItemTarget_Vendors.VendorProduct_OriginVendorProductID");

                entity.HasOne(d => d.SelectedRateQuote)
                    .WithMany(p => p.SalesQuoteItemTargets)
                    .HasForeignKey(d => d.SelectedRateQuoteId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItemTarget_Shipping.RateQuote_SelectedRateQuoteID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesQuoteItemTargets)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Quoting.SalesQuoteItemTarget_Sales.SalesItemTargetType_TypeID");
            });

            modelBuilder.Entity<SalesQuoteSalesOrder>(entity =>
            {
                entity.ToTable("SalesQuoteSalesOrder", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesQuoteSalesOrders)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Quoting.SalesQuoteSalesOrder_Quoting.SalesQuote_SalesQuoteID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesQuoteSalesOrders)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Quoting.SalesQuoteSalesOrder_Ordering.SalesOrder_SalesOrderID");
            });

            modelBuilder.Entity<SalesQuoteState>(entity =>
            {
                entity.ToTable("SalesQuoteState", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesQuoteStatus>(entity =>
            {
                entity.ToTable("SalesQuoteStatus", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesQuoteType>(entity =>
            {
                entity.ToTable("SalesQuoteType", "Quoting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesReturn>(entity =>
            {
                entity.ToTable("SalesReturn", "Returning");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BillingContactId, "IX_BillingContactID");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FranchiseId, "IX_FranchiseID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.SalesGroupId, "IX_SalesGroupID");

                entity.HasIndex(e => e.ShippingContactId, "IX_ShippingContactID");

                entity.HasIndex(e => e.StateId, "IX_StateID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BalanceDue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.BillingContactId).HasColumnName("BillingContactID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseId).HasColumnName("FranchiseID");

                entity.Property(e => e.PurchaseOrderNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RefundAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RefundTransactionId)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("RefundTransactionID");

                entity.Property(e => e.SalesGroupId).HasColumnName("SalesGroupID");

                entity.Property(e => e.ShippingContactId).HasColumnName("ShippingContactID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.SubtotalDiscounts).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalFees).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalHandling).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalItems).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalShipping).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalTaxes).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxTransactionId)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("TaxTransactionID");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TrackingNumber)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Returning.SalesReturn_Accounts.Account_AccountID");

                entity.HasOne(d => d.BillingContact)
                    .WithMany(p => p.SalesReturnBillingContacts)
                    .HasForeignKey(d => d.BillingContactId)
                    .HasConstraintName("FK_Returning.SalesReturn_Contacts.Contact_BillingContactID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Returning.SalesReturn_Brands.Brand_BrandID");

                entity.HasOne(d => d.Franchise)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.FranchiseId)
                    .HasConstraintName("FK_Returning.SalesReturn_Franchises.Franchise_FranchiseID");

                entity.HasOne(d => d.SalesGroup)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.SalesGroupId)
                    .HasConstraintName("FK_Returning.SalesReturn_Sales.SalesGroup_SalesGroupID");

                entity.HasOne(d => d.ShippingContact)
                    .WithMany(p => p.SalesReturnShippingContacts)
                    .HasForeignKey(d => d.ShippingContactId)
                    .HasConstraintName("FK_Returning.SalesReturn_Contacts.Contact_ShippingContactID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Returning.SalesReturn_Returning.SalesReturnState_StateID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Returning.SalesReturn_Returning.SalesReturnStatus_StatusID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Returning.SalesReturn_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Returning.SalesReturn_Returning.SalesReturnType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Returning.SalesReturn_Contacts.User_UserID");
            });

            modelBuilder.Entity<SalesReturnContact>(entity =>
            {
                entity.ToTable("SalesReturnContact", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesReturnContacts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Returning.SalesReturnContact_Returning.SalesReturn_SalesReturnID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesReturnContacts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Returning.SalesReturnContact_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<SalesReturnDiscount>(entity =>
            {
                entity.ToTable("SalesReturnDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesReturnDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.SalesReturnDiscounts_Returning.SalesReturn_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesReturnDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.SalesReturnDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<SalesReturnEvent>(entity =>
            {
                entity.ToTable("SalesReturnEvent", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NewStateId).HasColumnName("NewStateID");

                entity.Property(e => e.NewStatusId).HasColumnName("NewStatusID");

                entity.Property(e => e.NewTypeId).HasColumnName("NewTypeID");

                entity.Property(e => e.OldStateId).HasColumnName("OldStateID");

                entity.Property(e => e.OldStatusId).HasColumnName("OldStatusID");

                entity.Property(e => e.OldTypeId).HasColumnName("OldTypeID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesReturnEvents)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Returning.SalesReturnEvent_Returning.SalesReturn_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesReturnEvents)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Returning.SalesReturnEvent_Returning.SalesReturnEventType_TypeID");
            });

            modelBuilder.Entity<SalesReturnEventType>(entity =>
            {
                entity.ToTable("SalesReturnEventType", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesReturnFile>(entity =>
            {
                entity.ToTable("SalesReturnFile", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesReturnFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Returning.SalesReturnFileNew_Returning.SalesReturn_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesReturnFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Returning.SalesReturnFileNew_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<SalesReturnItem>(entity =>
            {
                entity.ToTable("SalesReturnItem", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.OriginalCurrencyId, "IX_OriginalCurrencyID");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.SalesReturnReasonId, "IX_SalesReturnReasonID");

                entity.HasIndex(e => e.SellingCurrencyId, "IX_SellingCurrencyID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ForceUniqueLineItemKey)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalCurrencyId).HasColumnName("OriginalCurrencyID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityBackOrdered).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPreSold).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RestockingFeeAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SalesReturnReasonId).HasColumnName("SalesReturnReasonID");

                entity.Property(e => e.SellingCurrencyId).HasColumnName("SellingCurrencyID");

                entity.Property(e => e.Sku)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCorePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitCorePriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitSoldPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitSoldPriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesReturnItems)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Returning.SalesReturnItem_Returning.SalesReturn_MasterID");

                entity.HasOne(d => d.OriginalCurrency)
                    .WithMany(p => p.SalesReturnItemOriginalCurrencies)
                    .HasForeignKey(d => d.OriginalCurrencyId)
                    .HasConstraintName("FK_Returning.SalesReturnItem_Currencies.Currency_OriginalCurrencyID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesReturnItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Returning.SalesReturnItem_Products.Product_ProductID");

                entity.HasOne(d => d.SalesReturnReason)
                    .WithMany(p => p.SalesReturnItems)
                    .HasForeignKey(d => d.SalesReturnReasonId)
                    .HasConstraintName("FK_Returning.SalesReturnItem_Returning.SalesReturnReason_SalesReturnReasonID");

                entity.HasOne(d => d.SellingCurrency)
                    .WithMany(p => p.SalesReturnItemSellingCurrencies)
                    .HasForeignKey(d => d.SellingCurrencyId)
                    .HasConstraintName("FK_Returning.SalesReturnItem_Currencies.Currency_SellingCurrencyID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SalesReturnItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Returning.SalesReturnItem_Contacts.User_UserID");
            });

            modelBuilder.Entity<SalesReturnItemDiscount>(entity =>
            {
                entity.ToTable("SalesReturnItemDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesReturnItemDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.SalesReturnItemDiscounts_Returning.SalesReturnItem_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesReturnItemDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.SalesReturnItemDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<SalesReturnItemTarget>(entity =>
            {
                entity.ToTable("SalesReturnItemTarget", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandProductId, "IX_BrandProductID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DestinationContactId, "IX_DestinationContactID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.OriginProductInventoryLocationSectionId, "IX_OriginProductInventoryLocationSectionID");

                entity.HasIndex(e => e.OriginStoreProductId, "IX_OriginStoreProductID");

                entity.HasIndex(e => e.OriginVendorProductId, "IX_OriginVendorProductID");

                entity.HasIndex(e => e.SelectedRateQuoteId, "IX_SelectedRateQuoteID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandProductId).HasColumnName("BrandProductID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationContactId).HasColumnName("DestinationContactID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OriginProductInventoryLocationSectionId).HasColumnName("OriginProductInventoryLocationSectionID");

                entity.Property(e => e.OriginStoreProductId).HasColumnName("OriginStoreProductID");

                entity.Property(e => e.OriginVendorProductId).HasColumnName("OriginVendorProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SelectedRateQuoteId).HasColumnName("SelectedRateQuoteID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.BrandProduct)
                    .WithMany(p => p.SalesReturnItemTargets)
                    .HasForeignKey(d => d.BrandProductId)
                    .HasConstraintName("FK_Returning.SalesReturnItemTarget_Brands.BrandProduct_BrandProductID");

                entity.HasOne(d => d.DestinationContact)
                    .WithMany(p => p.SalesReturnItemTargets)
                    .HasForeignKey(d => d.DestinationContactId)
                    .HasConstraintName("FK_Returning.SalesReturnItemTarget_Contacts.Contact_DestinationContactID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesReturnItemTargets)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Returning.SalesReturnItemTarget_Returning.SalesReturnItem_MasterID");

                entity.HasOne(d => d.OriginProductInventoryLocationSection)
                    .WithMany(p => p.SalesReturnItemTargets)
                    .HasForeignKey(d => d.OriginProductInventoryLocationSectionId)
                    .HasConstraintName("FK_Returning.SalesReturnItemTarget_Products.ProductInventoryLocationSection_OriginProductInventoryLocationSectionID");

                entity.HasOne(d => d.OriginStoreProduct)
                    .WithMany(p => p.SalesReturnItemTargets)
                    .HasForeignKey(d => d.OriginStoreProductId)
                    .HasConstraintName("FK_Returning.SalesReturnItemTarget_Stores.StoreProduct_OriginStoreProductID");

                entity.HasOne(d => d.OriginVendorProduct)
                    .WithMany(p => p.SalesReturnItemTargets)
                    .HasForeignKey(d => d.OriginVendorProductId)
                    .HasConstraintName("FK_Returning.SalesReturnItemTarget_Vendors.VendorProduct_OriginVendorProductID");

                entity.HasOne(d => d.SelectedRateQuote)
                    .WithMany(p => p.SalesReturnItemTargets)
                    .HasForeignKey(d => d.SelectedRateQuoteId)
                    .HasConstraintName("FK_Returning.SalesReturnItemTarget_Shipping.RateQuote_SelectedRateQuoteID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SalesReturnItemTargets)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Returning.SalesReturnItemTarget_Sales.SalesItemTargetType_TypeID");
            });

            modelBuilder.Entity<SalesReturnPayment>(entity =>
            {
                entity.ToTable("SalesReturnPayment", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesReturnPayments)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Payments.SalesReturnPayment_Returning.SalesReturn_SalesReturnID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesReturnPayments)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Payments.SalesReturnPayment_Payments.Payment_PaymentID");
            });

            modelBuilder.Entity<SalesReturnReason>(entity =>
            {
                entity.ToTable("SalesReturnReason", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.RestockingFeeAmountCurrencyId, "IX_RestockingFeeAmountCurrencyID");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RestockingFeeAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RestockingFeeAmountCurrencyId).HasColumnName("RestockingFeeAmountCurrencyID");

                entity.Property(e => e.RestockingFeePercent).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.RestockingFeeAmountCurrency)
                    .WithMany(p => p.SalesReturnReasons)
                    .HasForeignKey(d => d.RestockingFeeAmountCurrencyId)
                    .HasConstraintName("FK_Returning.SalesReturnReason_Currencies.Currency_RestockingFeeAmountCurrencyID");
            });

            modelBuilder.Entity<SalesReturnSalesOrder>(entity =>
            {
                entity.ToTable("SalesReturnSalesOrder", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SalesReturnSalesOrders)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Returning.SalesReturnSalesOrder_Returning.SalesReturn_SalesReturnID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SalesReturnSalesOrders)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Returning.SalesReturnSalesOrder_Ordering.SalesOrder_SalesOrderID");
            });

            modelBuilder.Entity<SalesReturnState>(entity =>
            {
                entity.ToTable("SalesReturnState", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesReturnStatus>(entity =>
            {
                entity.ToTable("SalesReturnStatus", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalesReturnType>(entity =>
            {
                entity.ToTable("SalesReturnType", "Returning");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SampleRequest>(entity =>
            {
                entity.ToTable("SampleRequest", "Sampling");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BillingContactId, "IX_BillingContactID");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.FranchiseId, "IX_FranchiseID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.SalesGroupId, "IX_SalesGroupID");

                entity.HasIndex(e => e.ShippingContactId, "IX_ShippingContactID");

                entity.HasIndex(e => e.StateId, "IX_StateID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BillingContactId).HasColumnName("BillingContactID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseId).HasColumnName("FranchiseID");

                entity.Property(e => e.SalesGroupId).HasColumnName("SalesGroupID");

                entity.Property(e => e.ShippingContactId).HasColumnName("ShippingContactID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.SubtotalDiscounts).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalFees).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalHandling).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalItems).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalShipping).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubtotalTaxes).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SampleRequests)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Accounts.Account_AccountID");

                entity.HasOne(d => d.BillingContact)
                    .WithMany(p => p.SampleRequestBillingContacts)
                    .HasForeignKey(d => d.BillingContactId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Contacts.Contact_BillingContactID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.SampleRequests)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Brands.Brand_BrandID");

                entity.HasOne(d => d.Franchise)
                    .WithMany(p => p.SampleRequests)
                    .HasForeignKey(d => d.FranchiseId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Franchises.Franchise_FranchiseID");

                entity.HasOne(d => d.SalesGroup)
                    .WithMany(p => p.SampleRequests)
                    .HasForeignKey(d => d.SalesGroupId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Sales.SalesGroup_SalesGroupID");

                entity.HasOne(d => d.ShippingContact)
                    .WithMany(p => p.SampleRequestShippingContacts)
                    .HasForeignKey(d => d.ShippingContactId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Contacts.Contact_ShippingContactID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.SampleRequests)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Sampling.SampleRequestState_StateID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.SampleRequests)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Sampling.SampleRequestStatus_StatusID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SampleRequests)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SampleRequests)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Sampling.SampleRequestType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SampleRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Sampling.SampleRequest_Contacts.User_UserID");
            });

            modelBuilder.Entity<SampleRequestContact>(entity =>
            {
                entity.ToTable("SampleRequestContact", "Sampling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SampleRequestContacts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Sampling.SampleRequestContact_Sampling.SampleRequest_SampleRequestID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SampleRequestContacts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Sampling.SampleRequestContact_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<SampleRequestDiscount>(entity =>
            {
                entity.ToTable("SampleRequestDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SampleRequestDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.SampleRequestDiscounts_Sampling.SampleRequest_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SampleRequestDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.SampleRequestDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<SampleRequestEvent>(entity =>
            {
                entity.ToTable("SampleRequestEvent", "Sampling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NewStateId).HasColumnName("NewStateID");

                entity.Property(e => e.NewStatusId).HasColumnName("NewStatusID");

                entity.Property(e => e.NewTypeId).HasColumnName("NewTypeID");

                entity.Property(e => e.OldStateId).HasColumnName("OldStateID");

                entity.Property(e => e.OldStatusId).HasColumnName("OldStatusID");

                entity.Property(e => e.OldTypeId).HasColumnName("OldTypeID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SampleRequestEvents)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Sampling.SampleRequestEvent_Sampling.SampleRequest_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SampleRequestEvents)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Sampling.SampleRequestEvent_Sampling.SampleRequestEventType_TypeID");
            });

            modelBuilder.Entity<SampleRequestEventType>(entity =>
            {
                entity.ToTable("SampleRequestEventType", "Sampling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SampleRequestFile>(entity =>
            {
                entity.ToTable("SampleRequestFile", "Sampling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SampleRequestFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Sampling.SampleRequestFileNew_Sampling.SampleRequest_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SampleRequestFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Sampling.SampleRequestFileNew_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<SampleRequestItem>(entity =>
            {
                entity.ToTable("SampleRequestItem", "Sampling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.OriginalCurrencyId, "IX_OriginalCurrencyID");

                entity.HasIndex(e => e.ProductId, "IX_ProductID");

                entity.HasIndex(e => e.SellingCurrencyId, "IX_SellingCurrencyID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ForceUniqueLineItemKey)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalCurrencyId).HasColumnName("OriginalCurrencyID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityBackOrdered).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuantityPreSold).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SellingCurrencyId).HasColumnName("SellingCurrencyID");

                entity.Property(e => e.Sku)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCorePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitCorePriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitSoldPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UnitSoldPriceInSellingCurrency).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SampleRequestItems)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Sampling.SampleRequestItem_Sampling.SampleRequest_MasterID");

                entity.HasOne(d => d.OriginalCurrency)
                    .WithMany(p => p.SampleRequestItemOriginalCurrencies)
                    .HasForeignKey(d => d.OriginalCurrencyId)
                    .HasConstraintName("FK_Sampling.SampleRequestItem_Currencies.Currency_OriginalCurrencyID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SampleRequestItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Sampling.SampleRequestItem_Products.Product_ProductID");

                entity.HasOne(d => d.SellingCurrency)
                    .WithMany(p => p.SampleRequestItemSellingCurrencies)
                    .HasForeignKey(d => d.SellingCurrencyId)
                    .HasConstraintName("FK_Sampling.SampleRequestItem_Currencies.Currency_SellingCurrencyID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SampleRequestItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Sampling.SampleRequestItem_Contacts.User_UserID");
            });

            modelBuilder.Entity<SampleRequestItemDiscount>(entity =>
            {
                entity.ToTable("SampleRequestItemDiscounts", "Discounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SampleRequestItemDiscounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Discounts.SampleRequestItemDiscounts_Sampling.SampleRequestItem_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SampleRequestItemDiscounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Discounts.SampleRequestItemDiscounts_Discounts.Discount_DiscountID");
            });

            modelBuilder.Entity<SampleRequestItemTarget>(entity =>
            {
                entity.ToTable("SampleRequestItemTarget", "Sampling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandProductId, "IX_BrandProductID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DestinationContactId, "IX_DestinationContactID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.OriginProductInventoryLocationSectionId, "IX_OriginProductInventoryLocationSectionID");

                entity.HasIndex(e => e.OriginStoreProductId, "IX_OriginStoreProductID");

                entity.HasIndex(e => e.OriginVendorProductId, "IX_OriginVendorProductID");

                entity.HasIndex(e => e.SelectedRateQuoteId, "IX_SelectedRateQuoteID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandProductId).HasColumnName("BrandProductID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationContactId).HasColumnName("DestinationContactID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.OriginProductInventoryLocationSectionId).HasColumnName("OriginProductInventoryLocationSectionID");

                entity.Property(e => e.OriginStoreProductId).HasColumnName("OriginStoreProductID");

                entity.Property(e => e.OriginVendorProductId).HasColumnName("OriginVendorProductID");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SelectedRateQuoteId).HasColumnName("SelectedRateQuoteID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.BrandProduct)
                    .WithMany(p => p.SampleRequestItemTargets)
                    .HasForeignKey(d => d.BrandProductId)
                    .HasConstraintName("FK_Sampling.SampleRequestItemTarget_Brands.BrandProduct_BrandProductID");

                entity.HasOne(d => d.DestinationContact)
                    .WithMany(p => p.SampleRequestItemTargets)
                    .HasForeignKey(d => d.DestinationContactId)
                    .HasConstraintName("FK_Sampling.SampleRequestItemTarget_Contacts.Contact_DestinationContactID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SampleRequestItemTargets)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Sampling.SampleRequestItemTarget_Sampling.SampleRequestItem_MasterID");

                entity.HasOne(d => d.OriginProductInventoryLocationSection)
                    .WithMany(p => p.SampleRequestItemTargets)
                    .HasForeignKey(d => d.OriginProductInventoryLocationSectionId)
                    .HasConstraintName("FK_Sampling.SampleRequestItemTarget_Products.ProductInventoryLocationSection_OriginProductInventoryLocationSectionID");

                entity.HasOne(d => d.OriginStoreProduct)
                    .WithMany(p => p.SampleRequestItemTargets)
                    .HasForeignKey(d => d.OriginStoreProductId)
                    .HasConstraintName("FK_Sampling.SampleRequestItemTarget_Stores.StoreProduct_OriginStoreProductID");

                entity.HasOne(d => d.OriginVendorProduct)
                    .WithMany(p => p.SampleRequestItemTargets)
                    .HasForeignKey(d => d.OriginVendorProductId)
                    .HasConstraintName("FK_Sampling.SampleRequestItemTarget_Vendors.VendorProduct_OriginVendorProductID");

                entity.HasOne(d => d.SelectedRateQuote)
                    .WithMany(p => p.SampleRequestItemTargets)
                    .HasForeignKey(d => d.SelectedRateQuoteId)
                    .HasConstraintName("FK_Sampling.SampleRequestItemTarget_Shipping.RateQuote_SelectedRateQuoteID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SampleRequestItemTargets)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Sampling.SampleRequestItemTarget_Sales.SalesItemTargetType_TypeID");
            });

            modelBuilder.Entity<SampleRequestState>(entity =>
            {
                entity.ToTable("SampleRequestState", "Sampling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SampleRequestStatus>(entity =>
            {
                entity.ToTable("SampleRequestStatus", "Sampling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SampleRequestType>(entity =>
            {
                entity.ToTable("SampleRequestType", "Sampling");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ScheduledJobConfiguration>(entity =>
            {
                entity.ToTable("ScheduledJobConfiguration", "Hangfire");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.NotificationTemplateId, "IX_NotificationTemplateID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationTemplateId).HasColumnName("NotificationTemplateID");

                entity.HasOne(d => d.NotificationTemplate)
                    .WithMany(p => p.ScheduledJobConfigurations)
                    .HasForeignKey(d => d.NotificationTemplateId)
                    .HasConstraintName("FK_Hangfire.ScheduledJobConfiguration_Messaging.EmailTemplate_NotificationTemplateID");
            });

            modelBuilder.Entity<ScheduledJobConfigurationSetting>(entity =>
            {
                entity.ToTable("ScheduledJobConfigurationSetting", "Hangfire");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ScheduledJobConfigurationSettings)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Hangfire.ScheduledJobConfigurationSetting_Hangfire.ScheduledJobConfiguration_ScheduledJobConfigurationID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ScheduledJobConfigurationSettings)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Hangfire.ScheduledJobConfigurationSetting_System.Setting_SettingID");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_Hangfire.Schema");

                entity.ToTable("Schema", "Hangfire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Scout>(entity =>
            {
                entity.ToTable("Scout", "Scouting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedByUserId, "IX_CreatedByUserID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DistanceUnitOfMeasure)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DistanceUsedMax).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DistanceUsedMin).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.HoursUsedMax).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.HoursUsedMin).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceMax).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceMin).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Scouts)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .HasConstraintName("FK_Scouting.Scout_Contacts.User_CreatedByUserID");
            });

            modelBuilder.Entity<ScoutCategory>(entity =>
            {
                entity.ToTable("ScoutCategory", "Scouting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.ScoutCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Scouting.ScoutCategory_Scouting.Scout_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.ScoutCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Scouting.ScoutCategory_Categories.Category_SlaveID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ScoutCategories)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Scouting.ScoutCategory_Scouting.ScoutCategoryType_TypeID");
            });

            modelBuilder.Entity<ScoutCategoryType>(entity =>
            {
                entity.ToTable("ScoutCategoryType", "Scouting");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "Hangfire");

                entity.HasIndex(e => e.LastHeartbeat, "IX_HangFire_Server_LastHeartbeat");

                entity.Property(e => e.Id).HasMaxLength(100);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<ServiceArea>(entity =>
            {
                entity.ToTable("ServiceArea", "Accounts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AddressId, "IX_AddressID");

                entity.HasIndex(e => e.ContractorId, "IX_ContractorID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.ContractorId).HasColumnName("ContractorID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Radius).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.ServiceAreas)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Accounts.ServiceArea_Geography.Address_AddressID");

                entity.HasOne(d => d.Contractor)
                    .WithMany(p => p.ServiceAreas)
                    .HasForeignKey(d => d.ContractorId)
                    .HasConstraintName("FK_Accounts.ServiceArea_Accounts.Contractor_ContractorID");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_Hangfire.Set");

                entity.ToTable("Set", "Hangfire");

                entity.HasIndex(e => e.ExpireAt, "IX_ExpireAt");

                entity.HasIndex(e => new { e.Key, e.Score }, "IX_HangFire_Set_Score");

                entity.HasIndex(e => e.Key, "IX_Key");

                entity.HasIndex(e => new { e.Key, e.Value }, "UX_HangFire_Set_KeyAndValue");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting", "System");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.SettingGroupId, "IX_SettingGroupID");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SettingGroupId).HasColumnName("SettingGroupID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Settings)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_System.Setting_Brands.Brand_BrandID");

                entity.HasOne(d => d.SettingGroup)
                    .WithMany(p => p.Settings)
                    .HasForeignKey(d => d.SettingGroupId)
                    .HasConstraintName("FK_System.Setting_System.SettingGroup_SettingGroupID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Settings)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_System.Setting_Stores.Store_StoreID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Settings)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_System.Setting_System.SettingType_TypeID");
            });

            modelBuilder.Entity<SettingGroup>(entity =>
            {
                entity.ToTable("SettingGroup", "System");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SettingType>(entity =>
            {
                entity.ToTable("SettingType", "System");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShipCarrier>(entity =>
            {
                entity.ToTable("ShipCarrier", "Shipping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountNumber).HasMaxLength(128);

                entity.Property(e => e.Authentication).HasMaxLength(128);

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EncryptedPassword).HasMaxLength(1024);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PointOfContact)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.SalesRep).HasMaxLength(128);

                entity.Property(e => e.Username).HasMaxLength(75);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ShipCarriers)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Shipping.ShipCarrier_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<ShipCarrierMethod>(entity =>
            {
                entity.ToTable("ShipCarrierMethod", "Shipping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.ShipCarrierId, "IX_ShipCarrierID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ShipCarrierId).HasColumnName("ShipCarrierID");

                entity.HasOne(d => d.ShipCarrier)
                    .WithMany(p => p.ShipCarrierMethods)
                    .HasForeignKey(d => d.ShipCarrierId)
                    .HasConstraintName("FK_Shipping.ShipCarrierMethod_Shipping.ShipCarrier_ShipCarrierID");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipment", "Shipping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DestinationContactId, "IX_DestinationContactID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.InventoryLocationSectionId, "IX_InventoryLocationSectionID");

                entity.HasIndex(e => e.OriginContactId, "IX_OriginContactID");

                entity.HasIndex(e => e.ShipCarrierId, "IX_ShipCarrierID");

                entity.HasIndex(e => e.ShipCarrierMethodId, "IX_ShipCarrierMethodID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.VendorId, "IX_VendorID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Destination)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DestinationContactId).HasColumnName("DestinationContactID");

                entity.Property(e => e.InventoryLocationSectionId).HasColumnName("InventoryLocationSectionID");

                entity.Property(e => e.NegotiatedRate).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.OriginContactId).HasColumnName("OriginContactID");

                entity.Property(e => e.PublishedRate).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Reference1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Reference2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Reference3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShipCarrierId).HasColumnName("ShipCarrierID");

                entity.Property(e => e.ShipCarrierMethodId).HasColumnName("ShipCarrierMethodID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TrackingNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.HasOne(d => d.DestinationContact)
                    .WithMany(p => p.ShipmentDestinationContacts)
                    .HasForeignKey(d => d.DestinationContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipping.Shipment_Contacts.Contact_DestinationContactID");

                entity.HasOne(d => d.InventoryLocationSection)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.InventoryLocationSectionId)
                    .HasConstraintName("FK_Shipping.Shipment_Inventory.InventoryLocationSection_InventoryLocationSectionID");

                entity.HasOne(d => d.OriginContact)
                    .WithMany(p => p.ShipmentOriginContacts)
                    .HasForeignKey(d => d.OriginContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipping.Shipment_Contacts.Contact_OriginContactID");

                entity.HasOne(d => d.ShipCarrier)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.ShipCarrierId)
                    .HasConstraintName("FK_Shipping.Shipment_Shipping.ShipCarrier_ShipCarrierID");

                entity.HasOne(d => d.ShipCarrierMethod)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.ShipCarrierMethodId)
                    .HasConstraintName("FK_Shipping.Shipment_Shipping.ShipCarrierMethod_ShipCarrierMethodID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Shipping.Shipment_Shipping.ShipmentStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Shipping.Shipment_Shipping.ShipmentType_TypeID");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_Shipping.Shipment_Vendors.Vendor_VendorID");
            });

            modelBuilder.Entity<ShipmentEvent>(entity =>
            {
                entity.ToTable("ShipmentEvent", "Shipping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AddressId, "IX_AddressID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.ShipmentId, "IX_ShipmentID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.ShipmentEvents)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Shipping.ShipmentEvent_Geography.Address_AddressID");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.ShipmentEvents)
                    .HasForeignKey(d => d.ShipmentId)
                    .HasConstraintName("FK_Shipping.ShipmentEvent_Shipping.Shipment_ShipmentID");
            });

            modelBuilder.Entity<ShipmentStatus>(entity =>
            {
                entity.ToTable("ShipmentStatus", "Shipping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShipmentType>(entity =>
            {
                entity.ToTable("ShipmentType", "Shipping");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SiteDomain>(entity =>
            {
                entity.ToTable("SiteDomain", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AlternateUrl1)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.AlternateUrl2)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.AlternateUrl3)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.CatalogContent).IsUnicode(false);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FooterContent).IsUnicode(false);

                entity.Property(e => e.HeaderContent).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SideBarContent).IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(512)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SiteDomainSocialProvider>(entity =>
            {
                entity.ToTable("SiteDomainSocialProvider", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.UrlValues).IsUnicode(false);

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SiteDomainSocialProviders)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.SiteDomainSocialProvider_Stores.SiteDomain_SiteDomainID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SiteDomainSocialProviders)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.SiteDomainSocialProvider_Stores.SocialProvider_SocialProviderID");
            });

            modelBuilder.Entity<SocialProvider>(entity =>
            {
                entity.ToTable("SocialProvider", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.UrlFormat)
                    .HasMaxLength(1024)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State", "Hangfire");

                entity.HasIndex(e => e.JobId, "IX_JobId");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_Hangfire.State_Hangfire.Job_JobId");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.LanguageId, "IX_LanguageID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId, "IX_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferProductId, "IX_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId, "IX_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferProductId, "IX_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferCategoryId, "IX_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferProductId, "IX_MinimumOrderDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferCategoryId, "IX_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferProductId, "IX_MinimumOrderQuantityAmountBufferProductID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ExternalUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.LanguageId).HasColumnName("LanguageID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferProductId).HasColumnName("MinimumForFreeShippingDollarAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferProductId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountBufferCategoryId).HasColumnName("MinimumOrderDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderDollarAmountBufferProductId).HasColumnName("MinimumOrderDollarAmountBufferProductID");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferCategoryId).HasColumnName("MinimumOrderQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferProductId).HasColumnName("MinimumOrderQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MissionStatement).HasMaxLength(1024);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OperatingHoursClosedStatement).HasMaxLength(256);

                entity.Property(e => e.OperatingHoursFridayEnd).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursFridayStart).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursMondayEnd).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursMondayStart).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursSaturdayEnd).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursSaturdayStart).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursSundayEnd).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursSundayStart).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursThursdayEnd).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursThursdayStart).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursTimeZoneId).HasMaxLength(55);

                entity.Property(e => e.OperatingHoursTuesdayEnd).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursTuesdayStart).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursWednesdayEnd).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.OperatingHoursWednesdayStart).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Slogan).HasMaxLength(1024);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stores.Store_Contacts.Contact_ContactID");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Stores.Store_Globalization.Language_LanguageID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferCategory)
                    .WithMany(p => p.StoreMinimumForFreeShippingDollarAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Stores.Store_Categories.Category_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferProduct)
                    .WithMany(p => p.StoreMinimumForFreeShippingDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferProductId)
                    .HasConstraintName("FK_Stores.Store_Products.Product_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferCategory)
                    .WithMany(p => p.StoreMinimumForFreeShippingQuantityAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Stores.Store_Categories.Category_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferProduct)
                    .WithMany(p => p.StoreMinimumForFreeShippingQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Stores.Store_Products.Product_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferCategory)
                    .WithMany(p => p.StoreMinimumOrderDollarAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Stores.Store_Categories.Category_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferProduct)
                    .WithMany(p => p.StoreMinimumOrderDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferProductId)
                    .HasConstraintName("FK_Stores.Store_Products.Product_MinimumOrderDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferCategory)
                    .WithMany(p => p.StoreMinimumOrderQuantityAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Stores.Store_Categories.Category_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferProduct)
                    .WithMany(p => p.StoreMinimumOrderQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Stores.Store_Products.Product_MinimumOrderQuantityAmountBufferProductID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Stores.Store_Stores.StoreType_TypeID");
            });

            modelBuilder.Entity<StoreAccount>(entity =>
            {
                entity.ToTable("StoreAccount", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.PricePointId, "IX_PricePointID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.PricePointId).HasColumnName("PricePointID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreAccounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreAccount_Stores.Store_StoreID");

                entity.HasOne(d => d.PricePoint)
                    .WithMany(p => p.StoreAccounts)
                    .HasForeignKey(d => d.PricePointId)
                    .HasConstraintName("FK_Stores.StoreAccount_Pricing.PricePoint_PricePointID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreAccounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreAccount_Accounts.Account_AccountID");
            });

            modelBuilder.Entity<StoreAuction>(entity =>
            {
                entity.ToTable("StoreAuction", "Auctions");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreAuctions)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Auctions.StoreAuction_Stores.Store_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreAuctions)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Auctions.StoreAuction_Auctions.Auction_SlaveID");
            });

            modelBuilder.Entity<StoreBadge>(entity =>
            {
                entity.ToTable("StoreBadge", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreBadges)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreBadge_Stores.Store_StoreID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreBadges)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreBadge_Stores.Badge_BadgeID");
            });

            modelBuilder.Entity<StoreCategory>(entity =>
            {
                entity.ToTable("StoreCategory", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreCategories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreCategory_Stores.Store_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreCategories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreCategory_Categories.Category_SlaveID");
            });

            modelBuilder.Entity<StoreContact>(entity =>
            {
                entity.ToTable("StoreContact", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreContacts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreContact_Stores.Store_StoreID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreContacts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreContact_Contacts.Contact_ContactID");
            });

            modelBuilder.Entity<StoreCountry>(entity =>
            {
                entity.ToTable("StoreCountry", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreCountries)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreCountry_Stores.Store_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreCountries)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreCountry_Geography.Country_SlaveID");
            });

            modelBuilder.Entity<StoreDistrict>(entity =>
            {
                entity.ToTable("StoreDistrict", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreDistricts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreDistrict_Stores.Store_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreDistricts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreDistrict_Geography.District_SlaveID");
            });

            modelBuilder.Entity<StoreImage>(entity =>
            {
                entity.ToTable("StoreImage", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreImageNew_Stores.Store_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.StoreImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Stores.StoreImageNew_Stores.StoreImageType_TypeID");
            });

            modelBuilder.Entity<StoreImageType>(entity =>
            {
                entity.ToTable("StoreImageType", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StoreInventoryLocation>(entity =>
            {
                entity.ToTable("StoreInventoryLocation", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreInventoryLocations)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreInventoryLocation_Stores.Store_StoreID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreInventoryLocations)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreInventoryLocation_Inventory.InventoryLocation_InventoryLocationID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.StoreInventoryLocations)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Stores.StoreInventoryLocation_Stores.StoreInventoryLocationType_TypeID");
            });

            modelBuilder.Entity<StoreInventoryLocationType>(entity =>
            {
                entity.ToTable("StoreInventoryLocationType", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StoreManufacturer>(entity =>
            {
                entity.ToTable("StoreManufacturer", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreManufacturers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreManufacturer_Stores.Store_StoreID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreManufacturers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreManufacturer_Manufacturers.Manufacturer_ManufacturerID");
            });

            modelBuilder.Entity<StoreProduct>(entity =>
            {
                entity.ToTable("StoreProduct", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.PriceBase).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceMsrp).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceReduction).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PriceSale).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreProducts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreProduct_Stores.Store_StoreID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreProducts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreProduct_Products.Product_ProductID");
            });

            modelBuilder.Entity<StoreRegion>(entity =>
            {
                entity.ToTable("StoreRegion", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreRegions)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreRegion_Stores.Store_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreRegions)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreRegion_Geography.Region_SlaveID");
            });

            modelBuilder.Entity<StoreSubscription>(entity =>
            {
                entity.ToTable("StoreSubscription", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreSubscriptions)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreSubscription_Stores.Store_StoreID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreSubscriptions)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreSubscription_Payments.Subscription_SubscriptionID");
            });

            modelBuilder.Entity<StoreType>(entity =>
            {
                entity.ToTable("StoreType", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StoreUser>(entity =>
            {
                entity.ToTable("StoreUser", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreUsers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreUser_Stores.Store_StoreID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreUsers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreUser_Contacts.User_UserID");
            });

            modelBuilder.Entity<StoreVendor>(entity =>
            {
                entity.ToTable("StoreVendor", "Stores");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.StoreVendors)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Stores.StoreVendor_Stores.Store_StoreID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.StoreVendors)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Stores.StoreVendor_Vendors.Vendor_VendorID");
            });

            modelBuilder.Entity<StoredFile>(entity =>
            {
                entity.ToTable("StoredFile", "Media");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Author).HasMaxLength(512);

                entity.Property(e => e.Copyright).HasMaxLength(512);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName).HasMaxLength(128);

                entity.Property(e => e.FileFormat).HasMaxLength(512);

                entity.Property(e => e.FileName).HasMaxLength(512);

                entity.Property(e => e.IsStoredInDb).HasColumnName("IsStoredInDB");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoTitle).HasMaxLength(75);
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscription", "Payments");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.ProductMembershipLevelId, "IX_ProductMembershipLevelID");

                entity.HasIndex(e => e.ProductSubscriptionTypeId, "IX_ProductSubscriptionTypeID");

                entity.HasIndex(e => e.RepeatTypeId, "IX_RepeatTypeID");

                entity.HasIndex(e => e.SalesGroupId, "IX_SalesGroupID");

                entity.HasIndex(e => e.SalesInvoiceId, "IX_SalesInvoiceID");

                entity.HasIndex(e => e.SalesOrderId, "IX_SalesOrderID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CreditUponUpgrade).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Fee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Memo)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ProductMembershipLevelId).HasColumnName("ProductMembershipLevelID");

                entity.Property(e => e.ProductSubscriptionTypeId).HasColumnName("ProductSubscriptionTypeID");

                entity.Property(e => e.RepeatTypeId).HasColumnName("RepeatTypeID");

                entity.Property(e => e.SalesGroupId).HasColumnName("SalesGroupID");

                entity.Property(e => e.SalesInvoiceId).HasColumnName("SalesInvoiceID");

                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Payments.Subscription_Accounts.Account_AccountID");

                entity.HasOne(d => d.ProductMembershipLevel)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.ProductMembershipLevelId)
                    .HasConstraintName("FK_Payments.Subscription_Products.ProductMembershipLevel_ProductMembershipLevelID");

                entity.HasOne(d => d.ProductSubscriptionType)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.ProductSubscriptionTypeId)
                    .HasConstraintName("FK_Payments.Subscription_Products.ProductSubscriptionType_ProductSubscriptionTypeID");

                entity.HasOne(d => d.RepeatType)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.RepeatTypeId)
                    .HasConstraintName("FK_Payments.Subscription_Payments.RepeatType_RepeatTypeID");

                entity.HasOne(d => d.SalesGroup)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.SalesGroupId)
                    .HasConstraintName("FK_Payments.Subscription_Sales.SalesGroup_SalesGroupID");

                entity.HasOne(d => d.SalesInvoice)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.SalesInvoiceId)
                    .HasConstraintName("FK_Payments.Subscription_Invoicing.SalesInvoice_SalesInvoiceID");

                entity.HasOne(d => d.SalesOrder)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.SalesOrderId)
                    .HasConstraintName("FK_Payments.Subscription_Ordering.SalesOrder_SalesOrderID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Payments.Subscription_Payments.SubscriptionStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Payments.Subscription_Payments.SubscriptionType_TypeID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Payments.Subscription_Contacts.User_UserID");
            });

            modelBuilder.Entity<SubscriptionHistory>(entity =>
            {
                entity.ToTable("SubscriptionHistory", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Memo)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SubscriptionHistories)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Payments.SubscriptionHistory_Payments.Subscription_SubscriptionID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SubscriptionHistories)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Payments.SubscriptionHistory_Payments.Payment_PaymentID");
            });

            modelBuilder.Entity<SubscriptionStatus>(entity =>
            {
                entity.ToTable("SubscriptionStatus", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubscriptionType>(entity =>
            {
                entity.ToTable("SubscriptionType", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubscriptionTypeRepeatType>(entity =>
            {
                entity.ToTable("SubscriptionTypeRepeatType", "Payments");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.SubscriptionTypeRepeatTypes)
                    .HasForeignKey(d => d.MasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments.SubscriptionTypeRepeatType_Payments.SubscriptionType_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.SubscriptionTypeRepeatTypes)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Payments.SubscriptionTypeRepeatType_Payments.RepeatType_SlaveID");
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.ToTable("SystemLog", "System");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.BrandId, "IX_BrandID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.StoreId, "IX_StoreID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DataId).HasColumnName("DataID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.SystemLogs)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_System.SystemLog_Brands.Brand_BrandID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SystemLogs)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_System.SystemLog_Stores.Store_StoreID");
            });

            modelBuilder.Entity<TaxCountry>(entity =>
            {
                entity.ToTable("TaxCountry", "Tax");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CountryId, "IX_CountryID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("numeric(7, 6)");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TaxCountries)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Tax.TaxCountry_Geography.Country_CountryID");
            });

            modelBuilder.Entity<TaxDistrict>(entity =>
            {
                entity.ToTable("TaxDistrict", "Tax");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DistrictId, "IX_DistrictID");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.TaxDistricts)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Tax.TaxDistrict_Geography.District_DistrictID");
            });

            modelBuilder.Entity<TaxRegion>(entity =>
            {
                entity.ToTable("TaxRegion", "Tax");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.RegionId, "IX_RegionID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("numeric(7, 6)");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.TaxRegions)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Tax.TaxRegion_Geography.Region_RegionID");
            });

            modelBuilder.Entity<Uikey>(entity =>
            {
                entity.ToTable("UIKey", "Globalization");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Uitranslation>(entity =>
            {
                entity.ToTable("UITranslation", "Globalization");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UiKeyId, "IX_UiKeyID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Locale).HasMaxLength(1024);

                entity.Property(e => e.UiKeyId).HasColumnName("UiKeyID");

                entity.Property(e => e.Value).HasMaxLength(1024);

                entity.HasOne(d => d.UiKey)
                    .WithMany(p => p.Uitranslations)
                    .HasForeignKey(d => d.UiKeyId)
                    .HasConstraintName("FK_Globalization.UITranslation_Globalization.UIKey_UiKeyID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Contacts");

                entity.HasIndex(e => e.AccountId, "IX_AccountID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CurrencyId, "IX_CurrencyID");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.LanguageId, "IX_LanguageID");

                entity.HasIndex(e => e.PreferredStoreId, "IX_PreferredStoreID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserName, "IX_UserName")
                    .IsUnique();

                entity.HasIndex(e => e.UserOnlineStatusId, "IX_UserOnlineStatusID");

                entity.HasIndex(e => e.UserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName).HasMaxLength(128);

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.IsSmsallowed).HasColumnName("IsSMSAllowed");

                entity.Property(e => e.LanguageId).HasColumnName("LanguageID");

                entity.Property(e => e.PasswordHash).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PreferredStoreId).HasColumnName("PreferredStoreID");

                entity.Property(e => e.SecurityStamp).HasMaxLength(100);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UserOnlineStatusId).HasColumnName("UserOnlineStatusID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Contacts.User_Accounts.Account_AccountID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Contacts.User_Contacts.Contact_ContactID");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Contacts.User_Currencies.Currency_CurrencyID");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Contacts.User_Globalization.Language_LanguageID");

                entity.HasOne(d => d.PreferredStore)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PreferredStoreId)
                    .HasConstraintName("FK_Contacts.User_Stores.Store_PreferredStoreID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Contacts.User_Contacts.UserStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Contacts.User_Contacts.UserType_TypeID");

                entity.HasOne(d => d.UserOnlineStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserOnlineStatusId)
                    .HasConstraintName("FK_Contacts.User_Contacts.UserOnlineStatus_UserOnlineStatusID");
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.ToTable("UserClaim", "Contacts");

                entity.HasIndex(e => e.Id, "IX_Id");

                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contacts.UserClaim_Contacts.User_UserId");
            });

            modelBuilder.Entity<UserEventAttendance>(entity =>
            {
                entity.ToTable("UserEventAttendance", "CalendarEvents");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Date, "IX_Date");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.UserEventAttendances)
                    .HasForeignKey(d => d.MasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalendarEvents.UserEventAttendance_CalendarEvents.CalendarEvent_CalendarEventID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.UserEventAttendances)
                    .HasForeignKey(d => d.SlaveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalendarEvents.UserEventAttendance_Contacts.User_UserID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.UserEventAttendances)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_CalendarEvents.UserEventAttendance_CalendarEvents.UserEventAttendanceType_TypeID");
            });

            modelBuilder.Entity<UserEventAttendanceType>(entity =>
            {
                entity.ToTable("UserEventAttendanceType", "CalendarEvents");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserFile>(entity =>
            {
                entity.ToTable("UserFile", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FileAccessTypeId).HasColumnName("FileAccessTypeID");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoMetaData)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SeoPageTitle)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SeoUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.UserFiles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Contacts.UserFile_Contacts.User_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.UserFiles)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Contacts.UserFile_Media.StoredFile_SlaveID");
            });

            modelBuilder.Entity<UserImage>(entity =>
            {
                entity.ToTable("UserImage", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.UserImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Contacts.UserImage_Contacts.User_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.UserImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Contacts.UserImage_Contacts.UserImageType_TypeID");
            });

            modelBuilder.Entity<UserImageType>(entity =>
            {
                entity.ToTable("UserImageType", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.ProviderKey })
                    .HasName("PK_Contacts.UserLogin");

                entity.ToTable("UserLogin", "Contacts");

                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.LoginProvider)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contacts.UserLogin_Contacts.User_UserId");
            });

            modelBuilder.Entity<UserOnlineStatus>(entity =>
            {
                entity.ToTable("UserOnlineStatus", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserProductType>(entity =>
            {
                entity.ToTable("UserProductType", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.UserProductTypes)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Contacts.UserProductType_Contacts.User_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.UserProductTypes)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Contacts.UserProductType_Products.ProductType_SlaveID");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole", "Contacts");

                entity.HasIndex(e => e.Name, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasMany(d => d.Permissions)
                    .WithMany(p => p.Roles)
                    .UsingEntity<Dictionary<string, object>>(
                        "RolePermission",
                        l => l.HasOne<Permission>().WithMany().HasForeignKey("PermissionId").HasConstraintName("FK_Contacts.RolePermission_Contacts.Permission_PermissionId"),
                        r => r.HasOne<UserRole>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_Contacts.RolePermission_Contacts.UserRole_RoleId"),
                        j =>
                        {
                            j.HasKey("RoleId", "PermissionId").HasName("PK_Contacts.RolePermission");

                            j.ToTable("RolePermission", "Contacts");

                            j.HasIndex(new[] { "PermissionId" }, "IX_PermissionId");

                            j.HasIndex(new[] { "RoleId" }, "IX_RoleId");
                        });
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("UserStatus", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserSupportRequest>(entity =>
            {
                entity.ToTable("UserSupportRequest", "Messaging");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthKey)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSupportRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Messaging.UserSupportRequest_Contacts.User_UserID");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType", "Contacts");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendor", "Vendors");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId, "IX_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingDollarAmountBufferProductId, "IX_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId, "IX_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumForFreeShippingQuantityAmountBufferProductId, "IX_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferCategoryId, "IX_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderDollarAmountBufferProductId, "IX_MinimumOrderDollarAmountBufferProductID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferCategoryId, "IX_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasIndex(e => e.MinimumOrderQuantityAmountBufferProductId, "IX_MinimumOrderQuantityAmountBufferProductID");

                entity.HasIndex(e => e.MustResetPassword, "IX_MustResetPassword");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.PasswordHash, "IX_PasswordHash");

                entity.HasIndex(e => e.SecurityToken, "IX_SecurityToken");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserName, "IX_UserName");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultDiscount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EmailSubject)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.MinimumForFreeShippingDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountBufferProductId).HasColumnName("MinimumForFreeShippingDollarAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferCategoryId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountBufferProductId).HasColumnName("MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumForFreeShippingQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountBufferCategoryId).HasColumnName("MinimumOrderDollarAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderDollarAmountBufferProductId).HasColumnName("MinimumOrderDollarAmountBufferProductID");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderDollarAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountAfter).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferCategoryId).HasColumnName("MinimumOrderQuantityAmountBufferCategoryID");

                entity.Property(e => e.MinimumOrderQuantityAmountBufferProductId).HasColumnName("MinimumOrderQuantityAmountBufferProductID");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFee).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeAcceptedMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountOverrideFeeWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.MinimumOrderQuantityAmountWarningMessage).HasMaxLength(1024);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.PasswordHash).HasMaxLength(128);

                entity.Property(e => e.RecommendedPurchaseOrderDollarAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SecurityToken).HasMaxLength(128);

                entity.Property(e => e.SendMethod)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShipTo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SignBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Terms)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserName).HasMaxLength(128);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Vendors)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Vendors.Vendor_Contacts.Contact_ContactID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferCategory)
                    .WithMany(p => p.VendorMinimumForFreeShippingDollarAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Vendors.Vendor_Categories.Category_MinimumForFreeShippingDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingDollarAmountBufferProduct)
                    .WithMany(p => p.VendorMinimumForFreeShippingDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingDollarAmountBufferProductId)
                    .HasConstraintName("FK_Vendors.Vendor_Products.Product_MinimumForFreeShippingDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferCategory)
                    .WithMany(p => p.VendorMinimumForFreeShippingQuantityAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Vendors.Vendor_Categories.Category_MinimumForFreeShippingQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumForFreeShippingQuantityAmountBufferProduct)
                    .WithMany(p => p.VendorMinimumForFreeShippingQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumForFreeShippingQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Vendors.Vendor_Products.Product_MinimumForFreeShippingQuantityAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferCategory)
                    .WithMany(p => p.VendorMinimumOrderDollarAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferCategoryId)
                    .HasConstraintName("FK_Vendors.Vendor_Categories.Category_MinimumOrderDollarAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderDollarAmountBufferProduct)
                    .WithMany(p => p.VendorMinimumOrderDollarAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderDollarAmountBufferProductId)
                    .HasConstraintName("FK_Vendors.Vendor_Products.Product_MinimumOrderDollarAmountBufferProductID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferCategory)
                    .WithMany(p => p.VendorMinimumOrderQuantityAmountBufferCategories)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferCategoryId)
                    .HasConstraintName("FK_Vendors.Vendor_Categories.Category_MinimumOrderQuantityAmountBufferCategoryID");

                entity.HasOne(d => d.MinimumOrderQuantityAmountBufferProduct)
                    .WithMany(p => p.VendorMinimumOrderQuantityAmountBufferProducts)
                    .HasForeignKey(d => d.MinimumOrderQuantityAmountBufferProductId)
                    .HasConstraintName("FK_Vendors.Vendor_Products.Product_MinimumOrderQuantityAmountBufferProductID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Vendors)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Vendors.Vendor_Vendors.VendorType_TypeID");
            });

            modelBuilder.Entity<VendorAccount>(entity =>
            {
                entity.ToTable("VendorAccount", "Vendors");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.VendorAccounts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Vendors.VendorAccount_Vendors.Vendor_MasterID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.VendorAccounts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Vendors.VendorAccount_Accounts.Account_SlaveID");
            });

            modelBuilder.Entity<VendorImage>(entity =>
            {
                entity.ToTable("VendorImage", "Vendors");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.MediaDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalIsStoredInDb).HasColumnName("OriginalIsStoredInDB");

                entity.Property(e => e.ThumbnailIsStoredInDb).HasColumnName("ThumbnailIsStoredInDB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.VendorImages)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Vendors.VendorImage_Vendors.Vendor_MasterID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.VendorImages)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Vendors.VendorImage_Vendors.VendorImageType_TypeID");
            });

            modelBuilder.Entity<VendorImageType>(entity =>
            {
                entity.ToTable("VendorImageType", "Vendors");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VendorManufacturer>(entity =>
            {
                entity.ToTable("VendorManufacturer", "Vendors");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.VendorManufacturers)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Vendors.VendorManufacturer_Vendors.Vendor_VendorID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.VendorManufacturers)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Vendors.VendorManufacturer_Manufacturers.Manufacturer_ManufacturerID");
            });

            modelBuilder.Entity<VendorProduct>(entity =>
            {
                entity.ToTable("VendorProduct", "Vendors");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.MasterId, "IX_MasterID");

                entity.HasIndex(e => e.SlaveId, "IX_SlaveID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActualCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Bin)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CostMultiplier).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ListedPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MasterId).HasColumnName("MasterID");

                entity.Property(e => e.SlaveId).HasColumnName("SlaveID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.VendorProducts)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_Vendors.VendorProduct_Vendors.Vendor_VendorID");

                entity.HasOne(d => d.Slave)
                    .WithMany(p => p.VendorProducts)
                    .HasForeignKey(d => d.SlaveId)
                    .HasConstraintName("FK_Vendors.VendorProduct_Products.Product_ProductID");
            });

            modelBuilder.Entity<VendorType>(entity =>
            {
                entity.ToTable("VendorType", "Vendors");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.ToTable("Visit", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AddressId, "IX_AddressID");

                entity.HasIndex(e => e.CampaignId, "IX_CampaignID");

                entity.HasIndex(e => e.ContactId, "IX_ContactID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.IporganizationId, "IX_IPOrganizationID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SiteDomainId, "IX_SiteDomainID");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.HasIndex(e => e.VisitorId, "IX_VisitorID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Browser)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EntryPage)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ExitPage)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Flash)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ipaddress)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IPAddress");

                entity.Property(e => e.IporganizationId).HasColumnName("IPOrganizationID");

                entity.Property(e => e.Keywords).HasMaxLength(100);

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OperatingSystem)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PartitionKey)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Referrer)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ReferringHost)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RowKey).HasMaxLength(50);

                entity.Property(e => e.SiteDomainId).HasColumnName("SiteDomainID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Time)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VisitorId).HasColumnName("VisitorID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Tracking.Visit_Geography.Address_AddressID");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK_Tracking.Visit_Tracking.Campaign_CampaignID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Tracking.Visit_Contacts.Contact_ContactID");

                entity.HasOne(d => d.Iporganization)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.IporganizationId)
                    .HasConstraintName("FK_Tracking.Visit_Tracking.IPOrganization_IPOrganizationID");

                entity.HasOne(d => d.SiteDomain)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.SiteDomainId)
                    .HasConstraintName("FK_Tracking.Visit_Stores.SiteDomain_SiteDomainID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Tracking.Visit_Tracking.VisitStatus_StatusID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tracking.Visit_Contacts.User_UserID");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.VisitorId)
                    .HasConstraintName("FK_Tracking.Visit_Tracking.Visitor_VisitorID");
            });

            modelBuilder.Entity<VisitStatus>(entity =>
            {
                entity.ToTable("VisitStatus", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.ToTable("Visitor", "Tracking");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.AddressId, "IX_AddressID");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.IporganizationId, "IX_IPOrganizationID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Ipaddress)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IPAddress");

                entity.Property(e => e.IporganizationId).HasColumnName("IPOrganizationID");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Visitors)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Tracking.Visitor_Geography.Address_AddressID");

                entity.HasOne(d => d.Iporganization)
                    .WithMany(p => p.Visitors)
                    .HasForeignKey(d => d.IporganizationId)
                    .HasConstraintName("FK_Tracking.Visitor_Tracking.IPOrganization_IPOrganizationID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Visitors)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tracking.Visitor_Contacts.User_UserID");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet", "Payments");

                entity.HasIndex(e => e.AccountContactId, "IX_AccountContactID");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CurrencyId, "IX_CurrencyID");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.UserId, "IX_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountContactId).HasColumnName("AccountContactID");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CardHolderName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CardType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RoutingNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.AccountContact)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.AccountContactId)
                    .HasConstraintName("FK_Payments.Wallet_Accounts.AccountContact_AccountContactID");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Payments.Wallet_Currencies.Currency_CurrencyID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Payments.Wallet_Contacts.User_UserID");
            });

            modelBuilder.Entity<ZipCode>(entity =>
            {
                entity.ToTable("ZipCode", "Geography");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.HasIndex(e => e.ZipCode1, "IX_ZipCode");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AreaCode).HasMaxLength(255);

                entity.Property(e => e.CityName).HasMaxLength(255);

                entity.Property(e => e.CityType).HasMaxLength(255);

                entity.Property(e => e.CountyFips).HasColumnName("CountyFIPS");

                entity.Property(e => e.CountyName).HasMaxLength(255);

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Dst)
                    .HasMaxLength(255)
                    .HasColumnName("DST");

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Msacode).HasColumnName("MSACode");

                entity.Property(e => e.StateAbbreviation).HasMaxLength(255);

                entity.Property(e => e.StateFips).HasColumnName("StateFIPS");

                entity.Property(e => e.StateName).HasMaxLength(255);

                entity.Property(e => e.TimeZone).HasMaxLength(255);

                entity.Property(e => e.Utc).HasColumnName("UTC");

                entity.Property(e => e.ZipCode1)
                    .HasMaxLength(20)
                    .HasColumnName("ZipCode");

                entity.Property(e => e.ZipType).HasMaxLength(255);
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.ToTable("Zone", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.StatusId, "IX_StatusID");

                entity.HasIndex(e => e.TypeId, "IX_TypeID");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Zones)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Advertising.Zone_Advertising.ZoneStatus_StatusID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Zones)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Advertising.Zone_Advertising.ZoneType_TypeID");
            });

            modelBuilder.Entity<ZoneStatus>(entity =>
            {
                entity.ToTable("ZoneStatus", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ZoneType>(entity =>
            {
                entity.ToTable("ZoneType", "Advertising");

                entity.HasIndex(e => e.Active, "IX_Active");

                entity.HasIndex(e => e.CreatedDate, "IX_CreatedDate");

                entity.HasIndex(e => e.CustomKey, "IX_CustomKey");

                entity.HasIndex(e => e.DisplayName, "IX_DisplayName");

                entity.HasIndex(e => e.Hash, "IX_Hash");

                entity.HasIndex(e => e.Id, "IX_ID");

                entity.HasIndex(e => e.Name, "IX_Name");

                entity.HasIndex(e => e.SortOrder, "IX_SortOrder");

                entity.HasIndex(e => e.UpdatedDate, "IX_UpdatedDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
