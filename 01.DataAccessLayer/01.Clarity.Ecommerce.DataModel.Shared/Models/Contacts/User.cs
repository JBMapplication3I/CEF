// <copyright file="User.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for user.</summary>
    public interface IUser
        : IHaveNotesBase,
            IHaveATypeBase<UserType>,
            IHaveAStatusBase<UserStatus>,
            IHaveAContactBase,
            IAmFilterableByBrand<BrandUser>,
            IAmFilterableByFranchise<FranchiseUser>,
            IAmFilterableByStore<StoreUser>,
            IHaveImagesBase<User, UserImage, UserImageType>,
            IHaveStoredFilesBase<User, UserFile>,
            IHaveReviewsBase
    {
        #region ASP.NET Identity Properties
        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserName { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        string? Email { get; set; }

        /// <summary>Gets or sets a value indicating whether the email confirmed.</summary>
        /// <value>True if email confirmed, false if not.</value>
        bool EmailConfirmed { get; set; }

        /// <summary>Gets or sets the password hash.</summary>
        /// <value>The password hash.</value>
        string? PasswordHash { get; set; }

        /// <summary>Gets or sets the security stamp.</summary>
        /// <value>The security stamp.</value>
        string? SecurityStamp { get; set; }

        /// <summary>Gets or sets the phone number.</summary>
        /// <value>The phone number.</value>
        string? PhoneNumber { get; set; }

        /// <summary>Gets or sets a value indicating whether the phone number confirmed.</summary>
        /// <value>True if phone number confirmed, false if not.</value>
        bool PhoneNumberConfirmed { get; set; }

        /// <summary>Gets or sets a value indicating whether the two factor is enabled.</summary>
        /// <value>True if two factor enabled, false if not.</value>
        bool TwoFactorEnabled { get; set; }

        /// <summary>Gets or sets the Date/Time of the lockout end date UTC.</summary>
        /// <value>The lockout end date UTC.</value>
        DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>Gets or sets a value indicating whether the lockout is enabled.</summary>
        /// <value>True if lockout enabled, false if not.</value>
        bool LockoutEnabled { get; set; }

        /// <summary>Gets or sets the number of access failed.</summary>
        /// <value>The number of access failed.</value>
        int AccessFailedCount { get; set; }
        #endregion

        #region Extended Identity Properties
        /// <summary>Gets or sets a value indicating whether the user is approved.</summary>
        /// <value>The is approved.</value>
        bool IsApproved { get; set; }

        /// <summary>Gets or sets a value indicating whether the require password change on next login.</summary>
        /// <value>True if require password change on next login, false if not.</value>
        bool RequirePasswordChangeOnNextLogin { get; set; }
        #endregion

        #region User Properties
        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the user's date of birth.</summary>
        /// <value>The date of birth.</value>
        DateTime? DateOfBirth { get; set; }

        /// <summary>Gets or sets the user's gender.</summary>
        /// <value>The user's gender.</value>
        string? Gender { get; set; }

        /// <summary>Gets or sets a value indicating whether SMS is allowed.</summary>
        /// <value>True if SMS is allowed, false if not.</value>
        bool IsSMSAllowed { get; set; }

        /// <summary>Gets or sets a value indicating whether this object use automatic pay.</summary>
        /// <value>True if use automatic pay, false if not.</value>
        bool UseAutoPay { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        Account? Account { get; set; }

        /// <summary>Gets or sets the identifier of the preferred store.</summary>
        /// <value>The identifier of the preferred store.</value>
        int? PreferredStoreID { get; set; }

        /// <summary>Gets or sets the preferred store.</summary>
        /// <value>The preferred store.</value>
        Store? PreferredStore { get; set; }

        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        Currency? Currency { get; set; }

        /// <summary>Gets or sets the identifier of the language.</summary>
        /// <value>The identifier of the language.</value>
        int? LanguageID { get; set; }

        /// <summary>Gets or sets the language.</summary>
        /// <value>The language.</value>
        Language? Language { get; set; }

        /// <summary>Gets or sets the identifier of the user online status.</summary>
        /// <value>The identifier of the user online status.</value>
        int? UserOnlineStatusID { get; set; }

        /// <summary>Gets or sets the user online status.</summary>
        /// <value>The user online status.</value>
        UserOnlineStatus? UserOnlineStatus { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Newtonsoft.Json;

    /// <summary>An user.</summary>
    /// <seealso cref="IdentityUser{TKey,UserLogin,RoleUser,UserClaim}"/>
    /// <seealso cref="Interfaces.DataModel.IUser"/>
    [SqlSchema("Contacts", "User")]
    public class User
        : IdentityUser<int, UserLogin, RoleUser, UserClaim>,
            Interfaces.DataModel.IUser
    {
        private ICollection<FavoriteCategory>? favoriteCategories;
        private ICollection<FavoriteVendor>? favoriteVendors;
        private ICollection<FavoriteManufacturer>? favoriteManufacturers;
        private ICollection<FavoriteStore>? favoriteStores;
        private ICollection<ReferralCode>? referralCodes;
        private ICollection<UserProductType>? userProductTypes;
        private ICollection<SalesInvoice>? salesInvoices;
        private ICollection<SalesQuote>? salesQuotes;
        private ICollection<SalesOrder>? salesOrders;
        private ICollection<Subscription>? subscriptions;
        private ICollection<Wallet>? wallets;
        private ICollection<Message>? sentMessages;
        private ICollection<MessageRecipient>? receivedMessages;
        private ICollection<MessageAttachment>? messageAttachmentsCreated;
        private ICollection<MessageAttachment>? messageAttachmentsUpdated;
        private ICollection<ConversationUser>? conversationUsers;
        private ICollection<Note>? notes;
        private ICollection<Note>? notesCreated;
        private ICollection<Note>? notesUpdated;
        private ICollection<Review>? reviewsSubmitted;
        private ICollection<Review>? reviewsApproved;
        private ICollection<UserEventAttendance>? userEventAttendances;
        private ICollection<DiscountCode>? discountCodes;
        private ICollection<Review>? reviews;
        private ICollection<BrandUser>? brands;
        private ICollection<FranchiseUser>? franchises;
        private ICollection<StoreUser>? stores;
        private ICollection<UserImage>? images;
        private ICollection<UserFile>? storedFiles;

        /// <summary>Initializes a new instance of the <see cref="User"/> class.</summary>
        public User()
        {
            // IHaveNotesBase
            notes = new HashSet<Note>();
            // IHaveImagesBase
            images = new HashSet<UserImage>();
            // IHaveStoredFiles
            storedFiles = new HashSet<UserFile>();
            // IAmFilterableByStore<T>
            stores = new HashSet<StoreUser>();
            // IAmFilterableByBrand<T>
            brands = new HashSet<BrandUser>();
            // IAmFilterableByFranchise<T>
            franchises = new HashSet<FranchiseUser>();
            // ASP.NET Identity Properties
            // Claims = new HashSet<UserClaim>();
            // Logins = new HashSet<UserLogin>();
            // Roles = new HashSet<RoleUser>();
            // User Properties
            salesInvoices = new HashSet<SalesInvoice>();
            notesCreated = new HashSet<Note>();
            notesUpdated = new HashSet<Note>();
            salesQuotes = new HashSet<SalesQuote>();
            reviewsSubmitted = new HashSet<Review>();
            reviewsApproved = new HashSet<Review>();
            salesOrders = new HashSet<SalesOrder>();
            reviews = new HashSet<Review>();
            subscriptions = new HashSet<Subscription>();
            wallets = new HashSet<Wallet>();
            sentMessages = new HashSet<Message>();
            receivedMessages = new HashSet<MessageRecipient>();
            messageAttachmentsCreated = new HashSet<MessageAttachment>();
            messageAttachmentsUpdated = new HashSet<MessageAttachment>();
            conversationUsers = new HashSet<ConversationUser>();
            favoriteCategories = new HashSet<FavoriteCategory>();
            favoriteManufacturers = new HashSet<FavoriteManufacturer>();
            favoriteStores = new HashSet<FavoriteStore>();
            favoriteVendors = new HashSet<FavoriteVendor>();
            userEventAttendances = new HashSet<UserEventAttendance>();
            referralCodes = new HashSet<ReferralCode>();
            discountCodes = new HashSet<DiscountCode>();
            userProductTypes = new HashSet<UserProductType>();
        }

        #region Base Properties
        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        public override int Id { get => ID; set => ID = value; }

        /// <inheritdoc/>
        [Key, Index, DatabaseGenerated(DatabaseGeneratedOption.Identity), DefaultValue(0)]
        public int ID { get; set; }

        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(false), Index, DefaultValue(null)]
        public string? CustomKey { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ Index]
        public DateTime CreatedDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ Index, DefaultValue(null), DontMapOutWithListing]
        public DateTime? UpdatedDate { get; set; }

        /// <inheritdoc/>
        [Index, DefaultValue(true)]
        public bool Active { get; set; } = true;

        /// <inheritdoc/>
        [Index, DefaultValue(null)]
        public long? Hash { get; set; }

        /// <inheritdoc/>
        [DefaultValue("{}")]
        public string? JsonAttributes { get; set; } = "{}";

        /// <inheritdoc/>
        /// <remarks>This value is read-only as it just deserializes the <see cref="JsonAttributes"/> property.</remarks>
        [NotMapped, ReadOnly(true), JsonIgnore]
        public SerializableAttributesDictionary SerializableAttributes => JsonAttributes.DeserializeAttributesDictionary();
        #endregion

        #region IHaveATypeBase<UserType>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual UserType? Type { get; set; }
        #endregion

        #region IHaveAStatusBase<UserStatus>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual UserStatus? Status { get; set; }
        #endregion

        #region IHaveAContactBase
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Contact)), DefaultValue(0)]
        public int ContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Contact? Contact { get; set; }
        #endregion

        #region IAmFilterableByBrand Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandUser>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByFranchise Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<FranchiseUser>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByStore Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<StoreUser>? Stores { get => stores; set => stores = value; }
        #endregion

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<UserImage>? Images { get => images; set => images = value; }
        #endregion

        #region HaveStoredFilesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<UserFile>? StoredFiles { get => storedFiles; set => storedFiles = value; }
        #endregion

        #region ASP.NET Identity Properties
        /// <inheritdoc cref="Interfaces.DataModel.IUser.UserName"/>
        [StringIsUnicode(false), Index(IsUnique = true), DefaultValue(null), StringLength(256)]
        public override string? UserName { get; set; }

        /// <inheritdoc cref="Interfaces.DataModel.IUser.Email"/>
        [StringIsUnicode(false), DefaultValue(null)]
        public override string? Email { get; set; }

        /// <inheritdoc cref="Interfaces.DataModel.IUser.PasswordHash"/>
        [StringLength(100), DontMapInWithMappingLayer, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public override string? PasswordHash { get; set; }

        /// <inheritdoc cref="Interfaces.DataModel.IUser.SecurityStamp"/>
        [StringLength(100), DefaultValue(null)]
        public override string? SecurityStamp { get; set; }

        /// <inheritdoc cref="Interfaces.DataModel.IUser.PhoneNumber"/>
        [StringLength(25), StringIsUnicode(false), DefaultValue(null)]
        public override string? PhoneNumber { get; set; }

        /// <inheritdoc cref="Interfaces.DataModel.IUser.LockoutEndDateUtc"/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public override DateTime? LockoutEndDateUtc { get; set; }

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool IsApproved { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool RequirePasswordChangeOnNextLogin { get; set; }
        #endregion

        #region User Properties
        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(true), DefaultValue(null)]
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? DateOfBirth { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false), DefaultValue(null)]
        public string? Gender { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsSMSAllowed { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool UseAutoPay { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Account)), DefaultValue(null)]
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Account? Account { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(PreferredStore)), DefaultValue(null)]
        public int? PreferredStoreID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual Store? PreferredStore { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Currency)), DefaultValue(null)]
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Currency? Currency { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Language)), DefaultValue(null)]
        public int? LanguageID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Language? Language { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(UserOnlineStatus)), DefaultValue(null)]
        public int? UserOnlineStatusID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual UserOnlineStatus? UserOnlineStatus { get; set; }
        #endregion

        #region Associated Objects
        #region Don't Map These Out
        // ReSharper disable UnusedMember.Global
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<FavoriteCategory>? FavoriteCategories { get => favoriteCategories; set => favoriteCategories = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<FavoriteVendor>? FavoriteVendors { get => favoriteVendors; set => favoriteVendors = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<FavoriteManufacturer>? FavoriteManufacturers { get => favoriteManufacturers; set => favoriteManufacturers = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<FavoriteStore>? FavoriteStores { get => favoriteStores; set => favoriteStores = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<ReferralCode>? ReferralCodes { get => referralCodes; set => referralCodes = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<UserProductType>? UserProductTypes { get => userProductTypes; set => userProductTypes = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesInvoice>? SalesInvoices { get => salesInvoices; set => salesInvoices = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesQuote>? SalesQuotes { get => salesQuotes; set => salesQuotes = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrder>? SalesOrders { get => salesOrders; set => salesOrders = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Subscription>? Subscriptions { get => subscriptions; set => subscriptions = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Message>? SentMessages { get => sentMessages; set => sentMessages = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<MessageRecipient>? ReceivedMessages { get => receivedMessages; set => receivedMessages = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<ConversationUser>? ConversationUsers { get => conversationUsers; set => conversationUsers = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<DiscountCode>? DiscountCodes { get => discountCodes; set => discountCodes = value; }
        // ReSharper restore UnusedMember.Global

        // ReSharper disable UnusedMember.Global
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Wallet>? Wallets { get => wallets; set => wallets = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<MessageAttachment>? MessageAttachmentsCreated { get => messageAttachmentsCreated; set => messageAttachmentsCreated = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<MessageAttachment>? MessageAttachmentsUpdated { get => messageAttachmentsUpdated; set => messageAttachmentsUpdated = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? NotesCreated { get => notesCreated; set => notesCreated = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? NotesUpdated { get => notesUpdated; set => notesUpdated = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Review>? ReviewsSubmitted { get => reviewsSubmitted; set => reviewsSubmitted = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Review>? ReviewsApproved { get => reviewsApproved; set => reviewsApproved = value; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<UserEventAttendance>? UserEventAttendances { get => userEventAttendances; set => userEventAttendances = value; }

        // ReSharper restore UnusedMember.Global
        #region IHaveNoteBase Properties
        /// <inheritdoc/>
        [DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region IHaveReviewsBase Properties
        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Review>? Reviews { get => reviews; set => reviews = value; }
        #endregion
        #endregion
        #endregion

        #region ICloneable
        /// <inheritdoc/>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Converts this Base to a hash-able string.</summary>
        /// <returns>This Base as a string.</returns>
        public virtual string ToHashableString()
        {
            throw new NotImplementedException();
            /*
            var builder = new System.Text.StringBuilder();
            // Base
            builder.Append("A: ").Append(Active).AppendLine();
            builder.Append("J: ").AppendLine(SerializableAttributes.SerializeAttributesDictionary());
            // Return
            return builder.ToString();
            */
        }
        #endregion
    }
}
