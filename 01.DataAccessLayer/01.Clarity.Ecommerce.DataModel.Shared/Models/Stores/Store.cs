// <copyright file="Store.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IStore
        : INameableBase,
            IHaveSeoBase,
            IHaveOrderMinimumsBase,
            IHaveFreeShippingMinimumsBase,
            IHaveNotesBase,
            IHaveANullableContactBase,
            IHaveATypeBase<StoreType>,
            IHaveImagesBase<Store, StoreImage, StoreImageType>,
            IHaveReviewsBase,
            IAmFilterableByAccount<StoreAccount>,
            IAmFilterableByBrand<BrandStore>,
            IAmFilterableByCategory<StoreCategory>,
            IAmFilterableByFranchise<FranchiseStore>,
            IAmFilterableByManufacturer<StoreManufacturer>,
            IAmFilterableByProduct<StoreProduct>,
            IAmFilterableByUser<StoreUser>,
            IAmFilterableByVendor<StoreVendor>
    {
        #region Store Properties
        /// <summary>Gets or sets the slogan.</summary>
        /// <value>The slogan.</value>
        string? Slogan { get; set; }

        /// <summary>Gets or sets the mission statement.</summary>
        /// <value>The mission statement.</value>
        string? MissionStatement { get; set; }

        /// <summary>Gets or sets the about.</summary>
        /// <value>The about.</value>
        string? About { get; set; }

        /// <summary>Gets or sets the overview.</summary>
        /// <value>The overview.</value>
        string? Overview { get; set; }

        /// <summary>Gets or sets URL of the external.</summary>
        /// <value>The external URL.</value>
        string? ExternalUrl { get; set; }

        /// <summary>Gets or sets the identifier of the operating hours time zone.</summary>
        /// <value>The identifier of the operating hours time zone.</value>
        string? OperatingHoursTimeZoneId { get; set; }

        /// <summary>Gets or sets the operating hours monday start.</summary>
        /// <value>The operating hours monday start.</value>
        decimal? OperatingHoursMondayStart { get; set; }

        /// <summary>Gets or sets the operating hours monday end.</summary>
        /// <value>The operating hours monday end.</value>
        decimal? OperatingHoursMondayEnd { get; set; }

        /// <summary>Gets or sets the operating hours Tuesday start.</summary>
        /// <value>The operating hours Tuesday start.</value>
        decimal? OperatingHoursTuesdayStart { get; set; }

        /// <summary>Gets or sets the operating hours Tuesday end.</summary>
        /// <value>The operating hours Tuesday end.</value>
        decimal? OperatingHoursTuesdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours wednesday start.</summary>
        /// <value>The operating hours wednesday start.</value>
        decimal? OperatingHoursWednesdayStart { get; set; }

        /// <summary>Gets or sets the operating hours wednesday end.</summary>
        /// <value>The operating hours wednesday end.</value>
        decimal? OperatingHoursWednesdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours thursday start.</summary>
        /// <value>The operating hours thursday start.</value>
        decimal? OperatingHoursThursdayStart { get; set; }

        /// <summary>Gets or sets the operating hours thursday end.</summary>
        /// <value>The operating hours thursday end.</value>
        decimal? OperatingHoursThursdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours friday start.</summary>
        /// <value>The operating hours friday start.</value>
        decimal? OperatingHoursFridayStart { get; set; }

        /// <summary>Gets or sets the operating hours friday end.</summary>
        /// <value>The operating hours friday end.</value>
        decimal? OperatingHoursFridayEnd { get; set; }

        /// <summary>Gets or sets the operating hours saturday start.</summary>
        /// <value>The operating hours saturday start.</value>
        decimal? OperatingHoursSaturdayStart { get; set; }

        /// <summary>Gets or sets the operating hours saturday end.</summary>
        /// <value>The operating hours saturday end.</value>
        decimal? OperatingHoursSaturdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours sunday start.</summary>
        /// <value>The operating hours sunday start.</value>
        decimal? OperatingHoursSundayStart { get; set; }

        /// <summary>Gets or sets the operating hours sunday end.</summary>
        /// <value>The operating hours sunday end.</value>
        decimal? OperatingHoursSundayEnd { get; set; }

        /// <summary>Gets or sets the operating hours closed statement.</summary>
        /// <value>The operating hours closed statement.</value>
        string? OperatingHoursClosedStatement { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime? EndDate { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets the DisplayInStorefront.</summary>
        /// <value>The DisplayInStorefront.</value>
        bool? DisplayInStorefront { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the language.</summary>
        /// <value>The identifier of the language.</value>
        int? LanguageID { get; set; }

        /// <summary>Gets or sets the language.</summary>
        /// <value>The language.</value>
        Language? Language { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the store badges.</summary>
        /// <value>The store badges.</value>
        ICollection<StoreBadge>? StoreBadges { get; set; }

        /// <summary>Gets or sets the store contacts.</summary>
        /// <value>The store contacts.</value>
        ICollection<StoreContact>? StoreContacts { get; set; }

        /// <summary>Gets or sets the store inventory locations.</summary>
        /// <value>The store inventory locations.</value>
        ICollection<StoreInventoryLocation>? StoreInventoryLocations { get; set; }

        /// <summary>Gets or sets the store subscriptions.</summary>
        /// <value>The store subscriptions.</value>
        ICollection<StoreSubscription>? StoreSubscriptions { get; set; }

        /// <summary>Gets or sets the store countries.</summary>
        /// <value>The store countries.</value>
        ICollection<StoreCountry>? StoreCountries { get; set; }

        /// <summary>Gets or sets the store regions.</summary>
        /// <value>The store regions.</value>
        ICollection<StoreRegion>? StoreRegions { get; set; }

        /// <summary>Gets or sets the store districts.</summary>
        /// <value>The store districts.</value>
        ICollection<StoreDistrict>? StoreDistricts { get; set; }
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
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Stores", "Store")]
    public class Store : NameableBase, IStore
    {
        private ICollection<Note>? notes;
        private ICollection<Review>? reviews;
        private ICollection<StoreUser>? users;
        private ICollection<BrandStore>? brands;
        private ICollection<FranchiseStore>? franchises;
        private ICollection<StoreImage>? images;
        private ICollection<StoreVendor>? vendors;
        private ICollection<StoreAccount>? accounts;
        private ICollection<StoreProduct>? products;
        private ICollection<StoreCategory>? categories;
        private ICollection<StoreManufacturer>? manufacturers;

        private ICollection<StoreBadge>? storeBadges;
        private ICollection<StoreContact>? storeContacts;
        private ICollection<StoreSubscription>? storeSubscriptions;
        private ICollection<StoreCountry>? storeCountries;
        private ICollection<StoreRegion>? storeRegions;
        private ICollection<StoreDistrict>? storeDistricts;
        private ICollection<StoreInventoryLocation>? storeInventoryLocations;

        public Store()
        {
            // IHaveNotesBase Properties
            notes = new HashSet<Note>();
            // IHaveImagesBase Properties
            images = new HashSet<StoreImage>();
            // IHaveReviewsBase Properties
            reviews = new HashSet<Review>();
            // IAmFilterableByAccount
            accounts = new HashSet<StoreAccount>();
            // IAmFilterableByProduct
            products = new HashSet<StoreProduct>();
            // IAmFilterableByVendor
            vendors = new HashSet<StoreVendor>();
            // IAmFilterableByManufacturer
            manufacturers = new HashSet<StoreManufacturer>();
            // IAmFilterableByUser
            users = new HashSet<StoreUser>();
            // IAmFilterableByBrand
            brands = new HashSet<BrandStore>();
            // IAmFilterableByFranchise
            franchises = new HashSet<FranchiseStore>();
            // IAmFilterableByCategory
            categories = new HashSet<StoreCategory>();
            // Store Properties
            storeBadges = new HashSet<StoreBadge>();
            storeContacts = new HashSet<StoreContact>();
            storeInventoryLocations = new HashSet<StoreInventoryLocation>();
            storeSubscriptions = new HashSet<StoreSubscription>();
            storeCountries = new HashSet<StoreCountry>();
            storeRegions = new HashSet<StoreRegion>();
            storeDistricts = new HashSet<StoreDistrict>();
        }

        #region IHaveSeoBase Properties
        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoKeywords { get; set; }

        /// <inheritdoc/>
        [StringLength(75), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoPageTitle { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoDescription { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public string? SeoMetaData { get; set; }
        #endregion

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual StoreType? Type { get; set; }
        #endregion

        #region IHaveANullableContact Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Contact)), DefaultValue(null)]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual Contact? Contact { get; set; }
        #endregion

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreImage>? Images { get => images; set => images = value; }
        #endregion

        #region IHaveReviewsBase<StoreReview, Store> Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Review>? Reviews { get => reviews; set => reviews = value; }
        #endregion

        #region IHaveOrderMinimumsBase Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumOrderDollarAmount { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumOrderDollarAmountAfter { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumOrderDollarAmountWarningMessage { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumOrderDollarAmountOverrideFee { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool MinimumOrderDollarAmountOverrideFeeIsPercent { get; set; } = false;

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumOrderDollarAmountOverrideFeeWarningMessage { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumOrderDollarAmountOverrideFeeAcceptedMessage { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumOrderQuantityAmount { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumOrderQuantityAmountAfter { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumOrderQuantityAmountWarningMessage { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumOrderQuantityAmountOverrideFee { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool MinimumOrderQuantityAmountOverrideFeeIsPercent { get; set; } = false;

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumOrderQuantityAmountOverrideFeeWarningMessage { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumOrderQuantityAmountOverrideFeeAcceptedMessage { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(MinimumOrderDollarAmountBufferProduct)), DefaultValue(null)]
        public int? MinimumOrderDollarAmountBufferProductID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Product? MinimumOrderDollarAmountBufferProduct { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(MinimumOrderQuantityAmountBufferProduct)), DefaultValue(null)]
        public int? MinimumOrderQuantityAmountBufferProductID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Product? MinimumOrderQuantityAmountBufferProduct { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(MinimumOrderDollarAmountBufferCategory)), DefaultValue(null)]
        public int? MinimumOrderDollarAmountBufferCategoryID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(Category.StoreMinimumOrderDollarAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumOrderDollarAmountBufferCategory { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(Category.StoreMinimumOrderQuantityAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public int? MinimumOrderQuantityAmountBufferCategoryID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumOrderQuantityAmountBufferCategory { get; set; }
        #endregion

        #region IHaveFreeShippingMinimumsBase
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumForFreeShippingDollarAmount { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumForFreeShippingDollarAmountAfter { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumForFreeShippingDollarAmountWarningMessage { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumForFreeShippingQuantityAmount { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumForFreeShippingQuantityAmountAfter { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumForFreeShippingQuantityAmountWarningMessage { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(MinimumForFreeShippingDollarAmountBufferProduct)), DefaultValue(null)]
        public int? MinimumForFreeShippingDollarAmountBufferProductID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Product? MinimumForFreeShippingDollarAmountBufferProduct { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(MinimumForFreeShippingQuantityAmountBufferProduct)), DefaultValue(null)]
        public int? MinimumForFreeShippingQuantityAmountBufferProductID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Product? MinimumForFreeShippingQuantityAmountBufferProduct { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(MinimumForFreeShippingDollarAmountBufferCategory)), DefaultValue(null)]
        public int? MinimumForFreeShippingDollarAmountBufferCategoryID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(Category.StoreMinimumForFreeShippingDollarAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(Category.StoreMinimumForFreeShippingQuantityAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public int? MinimumForFreeShippingQuantityAmountBufferCategoryID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
        #endregion

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreProduct>? Products { get => products; set => products = value; }
        #endregion

        #region IAmFilterableByAccount
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreAccount>? Accounts { get => accounts; set => accounts = value; }
        #endregion

        #region IAmFilterableByUser
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreUser>? Users { get => users; set => users = value; }
        #endregion

        #region IAmFilterableByVendor
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreVendor>? Vendors { get => vendors; set => vendors = value; }
        #endregion

        #region IAmFilterableByCategory
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreCategory>? Categories { get => categories; set => categories = value; }
        #endregion

        #region IAmFilterableByBrand
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandStore>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByFranchise
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseStore>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByManufacturer
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreManufacturer>? Manufacturers { get => manufacturers; set => manufacturers = value; }
        #endregion

        #region Store Properties
        /// <inheritdoc/>
        [StringLength(1024), StringIsUnicode(true)]
        public string? Slogan { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), StringIsUnicode(true)]
        public string? MissionStatement { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true)]
        public string? About { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true)]
        public string? Overview { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false)]
        public string? ExternalUrl { get; set; }

        /// <inheritdoc/>
        [StringLength(55), StringIsUnicode(true)]
        public string? OperatingHoursTimeZoneId { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursMondayStart { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursMondayEnd { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursTuesdayStart { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursTuesdayEnd { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursWednesdayStart { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursWednesdayEnd { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursThursdayStart { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursThursdayEnd { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursFridayStart { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursFridayEnd { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursSaturdayStart { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursSaturdayEnd { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursSundayStart { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? OperatingHoursSundayEnd { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(true)]
        public string? OperatingHoursClosedStatement { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? StartDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? EndDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool? DisplayInStorefront { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Language))]
        public int? LanguageID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Language? Language { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreBadge>? StoreBadges { get => storeBadges; set => storeBadges = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreContact>? StoreContacts { get => storeContacts; set => storeContacts = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreInventoryLocation>? StoreInventoryLocations { get => storeInventoryLocations; set => storeInventoryLocations = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreSubscription>? StoreSubscriptions { get => storeSubscriptions; set => storeSubscriptions = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreCountry>? StoreCountries { get => storeCountries; set => storeCountries = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreRegion>? StoreRegions { get => storeRegions; set => storeRegions = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreDistrict>? StoreDistricts { get => storeDistricts; set => storeDistricts = value; }
        #endregion
    }
}
