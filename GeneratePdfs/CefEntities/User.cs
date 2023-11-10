using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class User
    {
        public User()
        {
            Answers = new HashSet<Answer>();
            Bids = new HashSet<Bid>();
            BrandUsers = new HashSet<BrandUser>();
            Campaigns = new HashSet<Campaign>();
            CartItems = new HashSet<CartItem>();
            CartTypes = new HashSet<CartType>();
            Carts = new HashSet<Cart>();
            Contractors = new HashSet<Contractor>();
            ConversationUsers = new HashSet<ConversationUser>();
            DiscountCodes = new HashSet<DiscountCode>();
            DiscountUsers = new HashSet<DiscountUser>();
            EmailQueueAttachmentCreatedByUsers = new HashSet<EmailQueueAttachment>();
            EmailQueueAttachmentUpdatedByUsers = new HashSet<EmailQueueAttachment>();
            Events = new HashSet<Event>();
            FavoriteCategories = new HashSet<FavoriteCategory>();
            FavoriteManufacturers = new HashSet<FavoriteManufacturer>();
            FavoriteShipCarriers = new HashSet<FavoriteShipCarrier>();
            FavoriteStores = new HashSet<FavoriteStore>();
            FavoriteVendors = new HashSet<FavoriteVendor>();
            FranchiseUsers = new HashSet<FranchiseUser>();
            GroupUsers = new HashSet<GroupUser>();
            Groups = new HashSet<Group>();
            InventoryLocationUsers = new HashSet<InventoryLocationUser>();
            Iporganizations = new HashSet<Iporganization>();
            MessageAttachmentCreatedByUsers = new HashSet<MessageAttachment>();
            MessageAttachmentUpdatedByUsers = new HashSet<MessageAttachment>();
            MessageRecipients = new HashSet<MessageRecipient>();
            Messages = new HashSet<Message>();
            NoteCreatedByUsers = new HashSet<Note>();
            NoteUpdatedByUsers = new HashSet<Note>();
            NoteUsers = new HashSet<Note>();
            PageViews = new HashSet<PageView>();
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            RecordVersions = new HashSet<RecordVersion>();
            ReferralCodes = new HashSet<ReferralCode>();
            Reports = new HashSet<Report>();
            ReviewApprovedByUsers = new HashSet<Review>();
            ReviewSubmittedByUsers = new HashSet<Review>();
            ReviewUsers = new HashSet<Review>();
            RoleUsers = new HashSet<RoleUser>();
            SalesInvoiceItems = new HashSet<SalesInvoiceItem>();
            SalesInvoices = new HashSet<SalesInvoice>();
            SalesOrderItems = new HashSet<SalesOrderItem>();
            SalesOrders = new HashSet<SalesOrder>();
            SalesQuoteItems = new HashSet<SalesQuoteItem>();
            SalesQuotes = new HashSet<SalesQuote>();
            SalesReturnItems = new HashSet<SalesReturnItem>();
            SalesReturns = new HashSet<SalesReturn>();
            SampleRequestItems = new HashSet<SampleRequestItem>();
            SampleRequests = new HashSet<SampleRequest>();
            Scouts = new HashSet<Scout>();
            StoreUsers = new HashSet<StoreUser>();
            Subscriptions = new HashSet<Subscription>();
            UserClaims = new HashSet<UserClaim>();
            UserEventAttendances = new HashSet<UserEventAttendance>();
            UserFiles = new HashSet<UserFile>();
            UserImages = new HashSet<UserImage>();
            UserLogins = new HashSet<UserLogin>();
            UserProductTypes = new HashSet<UserProductType>();
            UserSupportRequests = new HashSet<UserSupportRequest>();
            Visitors = new HashSet<Visitor>();
            Visits = new HashSet<Visit>();
            Wallets = new HashSet<Wallet>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? JsonAttributes { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int ContactId { get; set; }
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public string? DisplayName { get; set; }
        public int? AccountId { get; set; }
        public int? PreferredStoreId { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int? CurrencyId { get; set; }
        public int? LanguageId { get; set; }
        public bool IsApproved { get; set; }
        public long? Hash { get; set; }
        public int? UserOnlineStatusId { get; set; }
        public bool RequirePasswordChangeOnNextLogin { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public bool IsSmsallowed { get; set; }
        public bool UseAutoPay { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Contact Contact { get; set; } = null!;
        public virtual Currency? Currency { get; set; }
        public virtual Language? Language { get; set; }
        public virtual Store? PreferredStore { get; set; }
        public virtual UserStatus Status { get; set; } = null!;
        public virtual UserType Type { get; set; } = null!;
        public virtual UserOnlineStatus? UserOnlineStatus { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<BrandUser> BrandUsers { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<CartType> CartTypes { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Contractor> Contractors { get; set; }
        public virtual ICollection<ConversationUser> ConversationUsers { get; set; }
        public virtual ICollection<DiscountCode> DiscountCodes { get; set; }
        public virtual ICollection<DiscountUser> DiscountUsers { get; set; }
        public virtual ICollection<EmailQueueAttachment> EmailQueueAttachmentCreatedByUsers { get; set; }
        public virtual ICollection<EmailQueueAttachment> EmailQueueAttachmentUpdatedByUsers { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<FavoriteCategory> FavoriteCategories { get; set; }
        public virtual ICollection<FavoriteManufacturer> FavoriteManufacturers { get; set; }
        public virtual ICollection<FavoriteShipCarrier> FavoriteShipCarriers { get; set; }
        public virtual ICollection<FavoriteStore> FavoriteStores { get; set; }
        public virtual ICollection<FavoriteVendor> FavoriteVendors { get; set; }
        public virtual ICollection<FranchiseUser> FranchiseUsers { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<InventoryLocationUser> InventoryLocationUsers { get; set; }
        public virtual ICollection<Iporganization> Iporganizations { get; set; }
        public virtual ICollection<MessageAttachment> MessageAttachmentCreatedByUsers { get; set; }
        public virtual ICollection<MessageAttachment> MessageAttachmentUpdatedByUsers { get; set; }
        public virtual ICollection<MessageRecipient> MessageRecipients { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Note> NoteCreatedByUsers { get; set; }
        public virtual ICollection<Note> NoteUpdatedByUsers { get; set; }
        public virtual ICollection<Note> NoteUsers { get; set; }
        public virtual ICollection<PageView> PageViews { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<RecordVersion> RecordVersions { get; set; }
        public virtual ICollection<ReferralCode> ReferralCodes { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Review> ReviewApprovedByUsers { get; set; }
        public virtual ICollection<Review> ReviewSubmittedByUsers { get; set; }
        public virtual ICollection<Review> ReviewUsers { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        public virtual ICollection<SalesInvoiceItem> SalesInvoiceItems { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        public virtual ICollection<SalesQuoteItem> SalesQuoteItems { get; set; }
        public virtual ICollection<SalesQuote> SalesQuotes { get; set; }
        public virtual ICollection<SalesReturnItem> SalesReturnItems { get; set; }
        public virtual ICollection<SalesReturn> SalesReturns { get; set; }
        public virtual ICollection<SampleRequestItem> SampleRequestItems { get; set; }
        public virtual ICollection<SampleRequest> SampleRequests { get; set; }
        public virtual ICollection<Scout> Scouts { get; set; }
        public virtual ICollection<StoreUser> StoreUsers { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserEventAttendance> UserEventAttendances { get; set; }
        public virtual ICollection<UserFile> UserFiles { get; set; }
        public virtual ICollection<UserImage> UserImages { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserProductType> UserProductTypes { get; set; }
        public virtual ICollection<UserSupportRequest> UserSupportRequests { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
