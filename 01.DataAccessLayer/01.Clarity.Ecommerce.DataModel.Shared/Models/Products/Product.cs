// <copyright file="Product.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for product.</summary>
    public interface IProduct
        : INameableBase,
            IHaveSeoBase,
            IHaveAStatusBase<ProductStatus>,
            IHaveATypeBase<ProductType>,
            IAmFilterableByAccount<AccountProduct>,
            IAmFilterableByBrand<BrandProduct>,
            IAmFilterableByCategory<ProductCategory>,
            IAmFilterableByFranchise<FranchiseProduct>,
            IAmFilterableByManufacturer<ManufacturerProduct>,
            IAmFilterableByStore<StoreProduct>,
            IAmFilterableByVendor<VendorProduct>,
            IHaveRequiresRolesBase,
            IHaveReviewsBase,
            IHaveNullableDimensions,
            IHaveImagesBase<Product, ProductImage, ProductImageType>,
            IHaveStoredFilesBase<Product, ProductFile>
    {
        #region Flags/Toggles
        /// <summary>Gets or sets a value indicating whether this IProduct is visible.</summary>
        /// <value>True if this IProduct is visible, false if not.</value>
        bool IsVisible { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProduct is discontinued.</summary>
        /// <value>True if this IProduct is discontinued, false if not.</value>
        bool IsDiscontinued { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProduct is eligible for return.</summary>
        /// <value>True if this IProduct is eligible for return, false if not.</value>
        bool IsEligibleForReturn { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProduct is taxable.</summary>
        /// <value>True if this IProduct is taxable, false if not.</value>
        bool IsTaxable { get; set; }

        /// <summary>Gets or sets a value indicating whether we allow back order.</summary>
        /// <value>True if allow back order, false if not.</value>
        bool AllowBackOrder { get; set; }

        /// <summary>Gets or sets a value indicating whether we allow pre-sale.</summary>
        /// <value>True if allow pre-sale, false if not.</value>
        bool AllowPreSale { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProduct is unlimited stock.</summary>
        /// <value>True if this IProduct is unlimited stock, false if not.</value>
        bool IsUnlimitedStock { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProduct is free shipping.</summary>
        /// <value>True if this IProduct is free shipping, false if not.</value>
        bool IsFreeShipping { get; set; }

        /// <summary>Gets or sets a value indicating whether the nothing to ship.</summary>
        /// <value>True if nothing to ship, false if not.</value>
        bool NothingToShip { get; set; }

        /// <summary>Gets or sets a value indicating whether the drop ship only.</summary>
        /// <value>True if drop ship only, false if not.</value>
        bool DropShipOnly { get; set; }

        /// <summary>Gets or sets a value indicating whether the shipping lead time is calendar days.</summary>
        /// <value>True if shipping lead time is calendar days, false if not.</value>
        bool ShippingLeadTimeIsCalendarDays { get; set; }
        #endregion

        #region Descriptors
        /// <summary>Gets or sets HCPC Code..</summary>
        /// <value>The HCPC Code.</value>
        string? HCPCCode { get; set; }

        /// <summary>Gets or sets information describing the short.</summary>
        /// <value>Information describing the short.</value>
        string? ShortDescription { get; set; }

        /// <summary>Gets or sets the manufacturer part number.</summary>
        /// <value>The manufacturer part number.</value>
        string? ManufacturerPartNumber { get; set; }

        /// <summary>Gets or sets the name of the brand.</summary>
        /// <value>The name of the brand.</value>
        string? BrandName { get; set; }

        /// <summary>Gets or sets the tax code.</summary>
        /// <value>The tax code.</value>
        string? TaxCode { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the index synonyms.</summary>
        /// <value>The index synonyms.</value>
        string? IndexSynonyms { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }
        #endregion

        #region Pricing & Fees
        /// <summary>Gets or sets the price base.</summary>
        /// <value>The price base.</value>
        decimal? PriceBase { get; set; }

        /// <summary>Gets or sets the price MSRP.</summary>
        /// <value>The price MSRP.</value>
        decimal? PriceMsrp { get; set; }

        /// <summary>Gets or sets the price reduction.</summary>
        /// <value>The price reduction.</value>
        decimal? PriceReduction { get; set; }

        /// <summary>Gets or sets the price sale.</summary>
        /// <value>The price sale.</value>
        decimal? PriceSale { get; set; }

        /// <summary>Gets or sets the handling charge.</summary>
        /// <value>The handling charge.</value>
        decimal? HandlingCharge { get; set; }

        /// <summary>Gets or sets the flat shipping charge.</summary>
        /// <value>The flat shipping charge.</value>
        decimal? FlatShippingCharge { get; set; }

        /// <summary>Gets or sets the restocking fee percent.</summary>
        /// <value>The restocking fee percent.</value>
        decimal? RestockingFeePercent { get; set; }

        /// <summary>Gets or sets the restocking fee amount.</summary>
        /// <value>The restocking fee amount.</value>
        decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Availability, Stock, Shipping Requirements
        /// <summary>Gets or sets the available start date.</summary>
        /// <value>The available start date.</value>
        DateTime? AvailableStartDate { get; set; }

        /// <summary>Gets or sets the available end date.</summary>
        /// <value>The available end date.</value>
        DateTime? AvailableEndDate { get; set; }

        /// <summary>Gets or sets the pre-sell end date.</summary>
        /// <value>The pre-sell end date.</value>
        DateTime? PreSellEndDate { get; set; }

        /// <summary>Gets or sets the stock quantity.</summary>
        /// <value>The stock quantity.</value>
        decimal? StockQuantity { get; set; }

        /// <summary>Gets or sets the stock quantity allocated.</summary>
        /// <value>The stock quantity allocated.</value>
        decimal? StockQuantityAllocated { get; set; }

        /// <summary>Gets or sets the stock quantity pre-sold.</summary>
        /// <value>The stock quantity pre-sold.</value>
        decimal? StockQuantityPreSold { get; set; }

        /// <summary>Gets or sets the quantity per master pack.</summary>
        /// <value>The quantity per master pack.</value>
        decimal? QuantityPerMasterPack { get; set; }

        /// <summary>Gets or sets the quantity master pack per layer.</summary>
        /// <value>The quantity master pack per layer.</value>
        decimal? QuantityMasterPackPerLayer { get; set; }

        /// <summary>Gets or sets the quantity master pack layers per pallet.</summary>
        /// <value>The quantity master pack layers per pallet.</value>
        decimal? QuantityMasterPackLayersPerPallet { get; set; }

        /// <summary>Gets or sets the quantity master pack per pallet.</summary>
        /// <value>The quantity master pack per pallet.</value>
        decimal? QuantityMasterPackPerPallet { get; set; }

        /// <summary>Gets or sets the quantity per layer.</summary>
        /// <value>The quantity per layer.</value>
        decimal? QuantityPerLayer { get; set; }

        /// <summary>Gets or sets the quantity layers per pallet.</summary>
        /// <value>The quantity layers per pallet.</value>
        decimal? QuantityLayersPerPallet { get; set; }

        /// <summary>Gets or sets the quantity per pallet.</summary>
        /// <value>The quantity per pallet.</value>
        decimal? QuantityPerPallet { get; set; }

        /// <summary>Gets or sets the kit base quantity price multiplier.</summary>
        /// <value>The kit base quantity price multiplier.</value>
        decimal? KitBaseQuantityPriceMultiplier { get; set; }

        /// <summary>Gets or sets the shipping lead time days.</summary>
        /// <value>The shipping lead time days.</value>
        int? ShippingLeadTimeDays { get; set; }
        #endregion

        #region Min/Max Purchase Per Order
        /// <summary>Gets or sets the minimum purchase quantity.</summary>
        /// <value>The minimum purchase quantity.</value>
        decimal? MinimumPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the minimum purchase quantity if past purchased.</summary>
        /// <value>The minimum purchase quantity if past purchased.</value>
        decimal? MinimumPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the maximum purchase quantity.</summary>
        /// <value>The maximum purchase quantity.</value>
        decimal? MaximumPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the maximum purchase quantity if past purchased.</summary>
        /// <value>The maximum purchase quantity if past purchased.</value>
        decimal? MaximumPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the maximum back order purchase quantity.</summary>
        /// <value>The maximum back order purchase quantity.</value>
        decimal? MaximumBackOrderPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the maximum back order purchase quantity if past purchased.</summary>
        /// <value>The maximum back order purchase quantity if past purchased.</value>
        decimal? MaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the maximum back order purchase quantity global.</summary>
        /// <value>The maximum back order purchase quantity global.</value>
        decimal? MaximumBackOrderPurchaseQuantityGlobal { get; set; }

        /// <summary>Gets or sets the maximum pre-purchase quantity.</summary>
        /// <value>The maximum pre-purchase quantity.</value>
        decimal? MaximumPrePurchaseQuantity { get; set; }

        /// <summary>Gets or sets the maximum pre-purchase quantity if past purchased.</summary>
        /// <value>The maximum pre-purchase quantity if past purchased.</value>
        decimal? MaximumPrePurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the maximum pre purchase quantity global.</summary>
        /// <value>The maximum pre purchase quantity global.</value>
        decimal? MaximumPrePurchaseQuantityGlobal { get; set; }
        #endregion

        #region Required Document
        /// <summary>Gets or sets the document required for purchase.</summary>
        /// <value>The document required for purchase.</value>
        string? DocumentRequiredForPurchase { get; set; }

        /// <summary>Gets or sets a message describing the document required for purchase missing warning.</summary>
        /// <value>A message describing the document required for purchase missing warning.</value>
        string? DocumentRequiredForPurchaseMissingWarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the document required for purchase expired warning.</summary>
        /// <value>A message describing the document required for purchase expired warning.</value>
        string? DocumentRequiredForPurchaseExpiredWarningMessage { get; set; }

        /// <summary>Gets or sets the document required for purchase override fee.</summary>
        /// <value>The document required for purchase override fee.</value>
        decimal? DocumentRequiredForPurchaseOverrideFee { get; set; }

        /// <summary>Gets or sets a value indicating whether the document required for purchase override fee is percent.</summary>
        /// <value>True if document required for purchase override fee is percent, false if not.</value>
        bool DocumentRequiredForPurchaseOverrideFeeIsPercent { get; set; }

        /// <summary>Gets or sets a message describing the document required for purchase override fee warning.</summary>
        /// <value>A message describing the document required for purchase override fee warning.</value>
        string? DocumentRequiredForPurchaseOverrideFeeWarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the document required for purchase override fee accepted.</summary>
        /// <value>A message describing the document required for purchase override fee accepted.</value>
        string? DocumentRequiredForPurchaseOverrideFeeAcceptedMessage { get; set; }
        #endregion

        #region Must Purchase In Multiples Of
        /// <summary>Gets or sets the must purchase in multiples of amount.</summary>
        /// <value>The must purchase in multiples of amount.</value>
        decimal? MustPurchaseInMultiplesOfAmount { get; set; }

        /// <summary>Gets or sets a message describing the must purchase in multiples of amount warning.</summary>
        /// <value>A message describing the must purchase in multiples of amount warning.</value>
        string? MustPurchaseInMultiplesOfAmountWarningMessage { get; set; }

        /// <summary>Gets or sets the must purchase in multiples of amount override fee.</summary>
        /// <value>The must purchase in multiples of amount override fee.</value>
        decimal? MustPurchaseInMultiplesOfAmountOverrideFee { get; set; }

        /// <summary>Gets or sets a value indicating whether the must purchase in multiples of amount override fee is
        /// percent.</summary>
        /// <value>True if we must purchase in multiples of amount override fee is percent, false if not.</value>
        bool MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent { get; set; }

        /// <summary>Gets or sets a message describing the must purchase in multiples of amount override fee warning.</summary>
        /// <value>A message describing the must purchase in multiples of amount override fee warning.</value>
        string? MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the must purchase in multiples of amount override fee accepted.</summary>
        /// <value>A message describing the must purchase in multiples of amount override fee accepted.</value>
        string? MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage { get; set; }
        #endregion

        #region Analytics filled data
        /// <summary>Gets or sets the total number of purchased amount.</summary>
        /// <value>The total number of purchased amount.</value>
        decimal? TotalPurchasedAmount { get; set; }

        /// <summary>Gets or sets the total number of purchased amount currency identifier.</summary>
        /// <value>The total number of purchased amount currency identifier.</value>
        int? TotalPurchasedAmountCurrencyID { get; set; }

        /// <summary>Gets or sets the total number of purchased amount currency.</summary>
        /// <value>The total number of purchased amount currency.</value>
        Currency? TotalPurchasedAmountCurrency { get; set; }

        /// <summary>Gets or sets the total number of purchased quantity.</summary>
        /// <value>The total number of purchased quantity.</value>
        decimal? TotalPurchasedQuantity { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the package.</summary>
        /// <value>The identifier of the package.</value>
        int? PackageID { get; set; }

        /// <summary>Gets or sets the package.</summary>
        /// <value>The package.</value>
        Package? Package { get; set; }

        /// <summary>Gets or sets the identifier of the master pack.</summary>
        /// <value>The identifier of the master pack.</value>
        int? MasterPackID { get; set; }

        /// <summary>Gets or sets the master pack.</summary>
        /// <value>The master pack.</value>
        Package? MasterPack { get; set; }

        /// <summary>Gets or sets the identifier of the pallet.</summary>
        /// <value>The identifier of the pallet.</value>
        int? PalletID { get; set; }

        /// <summary>Gets or sets the pallet.</summary>
        /// <value>The pallet.</value>
        Package? Pallet { get; set; }

        /// <summary>Gets or sets the identifier of the restocking fee amount currency.</summary>
        /// <value>The identifier of the restocking fee amount currency.</value>
        int? RestockingFeeAmountCurrencyID { get; set; }

        /// <summary>Gets or sets the restocking fee amount currency.</summary>
        /// <value>The restocking fee amount currency.</value>
        Currency? RestockingFeeAmountCurrency { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the cart items.</summary>
        /// <value>The cart items.</value>
        ICollection<CartItem>? CartItems { get; set; }

        /// <summary>Gets or sets the product associations.</summary>
        /// <value>The product associations.</value>
        ICollection<ProductAssociation>? ProductAssociations { get; set; }

        /// <summary>Gets or sets the products associated with.</summary>
        /// <value>The products associated with.</value>
        ICollection<ProductAssociation>? ProductsAssociatedWith { get; set; }

        /// <summary>Gets or sets the product inventory location sections.</summary>
        /// <value>The product inventory location sections.</value>
        ICollection<ProductInventoryLocationSection>? ProductInventoryLocationSections { get; set; }

        /// <summary>Gets or sets the product membership levels.</summary>
        /// <value>The product membership levels.</value>
        ICollection<ProductMembershipLevel>? ProductMembershipLevels { get; set; }

        /// <summary>Gets or sets the product downloads.</summary>
        /// <value>The product downloads.</value>
        ICollection<ProductDownload>? ProductDownloads { get; set; }

        /// <summary>Gets or sets the product price points.</summary>
        /// <value>The product price points.</value>
        ICollection<ProductPricePoint>? ProductPricePoints { get; set; }

        /// <summary>Gets or sets the product ship carrier methods.</summary>
        /// <value>The product ship carrier methods.</value>
        ICollection<ProductShipCarrierMethod>? ProductShipCarrierMethods { get; set; }

        /// <summary>Gets or sets the discount products.</summary>
        /// <value>The discount products.</value>
        ICollection<DiscountProduct>? DiscountProducts { get; set; }

        /// <summary>Gets or sets the sales order items.</summary>
        /// <value>The sales order items.</value>
        ICollection<SalesOrderItem>? SalesOrderItems { get; set; }

        /// <summary>Gets or sets the sales return items.</summary>
        /// <value>The sales return items.</value>
        ICollection<SalesReturnItem>? SalesReturnItems { get; set; }

        /// <summary>Gets or sets a list of types of the product subscriptions.</summary>
        /// <value>A list of types of the product subscriptions.</value>
        ICollection<ProductSubscriptionType>? ProductSubscriptionTypes { get; set; }

        /// <summary>Gets or sets the product restrictions.</summary>
        /// <value>The product restrictions.</value>
        ICollection<ProductRestriction>? ProductRestrictions { get; set; }

        /// <summary>Gets or sets the product notifications.</summary>
        /// <value>The product notifications.</value>
        ICollection<ProductNotification>? ProductNotifications { get; set; }
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
    using System.Linq;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Products", "Product")]
    public class Product : NameableBase, IProduct
    {
        private ICollection<AccountProduct>? accounts;
        private ICollection<BrandProduct>? brands;
        private ICollection<ProductCategory>? categories;
        private ICollection<FranchiseProduct>? franchises;
        private ICollection<ManufacturerProduct>? manufacturers;
        private ICollection<StoreProduct>? stores;
        private ICollection<VendorProduct>? vendors;
        private ICollection<Review>? reviews;
        private ICollection<ProductFile>? storedFiles;
        private ICollection<ProductImage>? images;
        private ICollection<ProductAssociation>? productAssociations;
        private ICollection<ProductAssociation>? productsAssociatedWith;
        private ICollection<ProductPricePoint>? productPricePoints;
        private ICollection<ProductShipCarrierMethod>? productShipCarrierMethods;
        private ICollection<ProductInventoryLocationSection>? pils;
        private ICollection<ProductMembershipLevel>? productMembershipLevels;
        private ICollection<ProductDownload>? productDownloads;
        private ICollection<ProductSubscriptionType>? productSubscriptionTypes;
        private ICollection<ProductRestriction>? productRestrictions;
        private ICollection<ProductNotification>? productNotifications;
        private ICollection<CartItem>? cartItems;
        private ICollection<SalesOrderItem>? salesOrderItems;
        private ICollection<SalesReturnItem>? salesReturnItems;
        private ICollection<DiscountProduct>? discountProducts;

        public Product()
        {
            // IAmFilterableByAccount
            accounts = new HashSet<AccountProduct>();
            // IAmFilterableByBrand
            brands = new HashSet<BrandProduct>();
            // IAmFilterableByCategory
            categories = new HashSet<ProductCategory>();
            // IAmFilterableByFranchise
            franchises = new HashSet<FranchiseProduct>();
            // IAmFilterableByManufacturer
            manufacturers = new HashSet<ManufacturerProduct>();
            // IAmFilterableByStore
            stores = new HashSet<StoreProduct>();
            // IAmFilterableByVendor
            vendors = new HashSet<VendorProduct>();
            // IHaveStoredFiles
            storedFiles = new HashSet<ProductFile>();
            // IHaveImages
            images = new HashSet<ProductImage>();
            // IHaveReviews
            reviews = new HashSet<Review>();
            // Product
            cartItems = new HashSet<CartItem>();
            discountProducts = new HashSet<DiscountProduct>();
            productAssociations = new HashSet<ProductAssociation>();
            productsAssociatedWith = new HashSet<ProductAssociation>();
            pils = new HashSet<ProductInventoryLocationSection>();
            productMembershipLevels = new HashSet<ProductMembershipLevel>();
            productDownloads = new HashSet<ProductDownload>();
            productPricePoints = new HashSet<ProductPricePoint>();
            productShipCarrierMethods = new HashSet<ProductShipCarrierMethod>();
            productRestrictions = new HashSet<ProductRestriction>();
            productNotifications = new HashSet<ProductNotification>();
            productSubscriptionTypes = new HashSet<ProductSubscriptionType>();
            salesOrderItems = new HashSet<SalesOrderItem>();
            salesReturnItems = new HashSet<SalesReturnItem>();
        }

        #region IHaveAStatusBase<ProductStatus>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual ProductStatus? Status { get; set; }
        #endregion

        #region IHaveATypeBase<ProductType>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual ProductType? Type { get; set; }
        #endregion

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

        #region IHaveRequiresRolesBase Properties
        /// <inheritdoc/>
        [StringLength(2048), DefaultValue(null)]
        public string? RequiresRoles { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        public List<string> RequiresRolesList => RequiresRoles?.Split(',').Select(s => s.Trim()).ToList()
            ?? new List<string>();

        /// <inheritdoc/>
        [StringLength(512), DefaultValue(null)]
        public string? RequiresRolesAlt { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore]
        public List<string> RequiresRolesListAlt => RequiresRolesAlt?.Split(',').Select(s => s.Trim()).ToList()
            ?? new List<string>();
        #endregion

        #region IHaveNullableDimensions
        /// <inheritdoc/>
        [DecimalPrecision(18, 4)]
        public decimal? Weight { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false)]
        public string? WeightUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4)]
        public decimal? Width { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false)]
        public string? WidthUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4)]
        public decimal? Depth { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false)]
        public string? DepthUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4)]
        public decimal? Height { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false)]
        public string? HeightUnitOfMeasure { get; set; }
        #endregion

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductImage>? Images { get => images; set => images = value; }
        #endregion

        #region HaveStoredFilesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductFile>? StoredFiles { get => storedFiles; set => storedFiles = value; }
        #endregion

        #region IAmFilterableByAccount
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<AccountProduct>? Accounts { get => accounts; set => accounts = value; }
        #endregion

        #region IAmFilterableByBrand
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandProduct>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByCategory
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductCategory>? Categories { get => categories; set => categories = value; }
        #endregion

        #region IAmFilterableByFranchise
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<FranchiseProduct>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByManufacturer
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<ManufacturerProduct>? Manufacturers { get => manufacturers; set => manufacturers = value; }
        #endregion

        #region IAmFilterableByStore
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<StoreProduct>? Stores { get => stores; set => stores = value; }
        #endregion

        #region IAmFilterableByVendor
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<VendorProduct>? Vendors { get => vendors; set => vendors = value; }
        #endregion

        #region Flags/Toggles
        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool IsVisible { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsDiscontinued { get; set; }

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool IsEligibleForReturn { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool IsTaxable { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool AllowBackOrder { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool AllowPreSale { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsUnlimitedStock { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsFreeShipping { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool NothingToShip { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool DropShipOnly { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool ShippingLeadTimeIsCalendarDays { get; set; }
        #endregion

        #region Descriptors
        /// <inheritdoc/>
        [StringLength(255), StringIsUnicode(false), DefaultValue(null)]
        public string? HCPCCode { get; set; }

        /// <inheritdoc/>
        [StringLength(255), StringIsUnicode(false), DefaultValue(null)]
        public string? ShortDescription { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false), DefaultValue(null)]
        public string? ManufacturerPartNumber { get; set; }

        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(false), DefaultValue(null)]
        public string? BrandName { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false), DefaultValue(null)]
        public string? TaxCode { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false), DefaultValue(null)]
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? IndexSynonyms { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }
        #endregion

        #region Pricing & Fees
        /// <inheritdoc/>
        /// <remarks>Mapping handled by pricing service.</remarks>
        [DontMapInEver, DontMapOutEver, DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PriceBase { get; set; }

        /// <inheritdoc/>
        /// <remarks>Mapping handled by pricing service.</remarks>
        [DontMapInEver, DontMapOutEver, DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PriceMsrp { get; set; }

        /// <inheritdoc/>
        /// <remarks>Mapping handled by pricing service.</remarks>
        [DontMapInEver, DontMapOutEver, DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PriceReduction { get; set; }

        /// <inheritdoc/>
        /// <remarks>Mapping handled by pricing service.</remarks>
        [DontMapInEver, DontMapOutEver, DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PriceSale { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? HandlingCharge { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? FlatShippingCharge { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? RestockingFeePercent { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Availability, Stock, Shipping Requirements
        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? AvailableStartDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? AvailableEndDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? PreSellEndDate { get; set; }

        /// <inheritdoc/>
        /// <remarks>Mapping handled by inventory service.</remarks>
        [DontMapInEver, DontMapOutEver, DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? StockQuantity { get; set; }

        /// <inheritdoc/>
        /// <remarks>Mapping handled by inventory service.</remarks>
        [DontMapInEver, DontMapOutEver, DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? StockQuantityAllocated { get; set; }

        /// <inheritdoc/>
        /// <remarks>Mapping handled by inventory service.</remarks>
        [DontMapInEver, DontMapOutEver, DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? StockQuantityPreSold { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityPerMasterPack { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityMasterPackPerLayer { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityMasterPackLayersPerPallet { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityMasterPackPerPallet { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityPerLayer { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityLayersPerPallet { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityPerPallet { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? KitBaseQuantityPriceMultiplier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ShippingLeadTimeDays { get; set; }
        #endregion

        #region Min/Max Purchase Per Order
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaximumPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaximumPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaximumBackOrderPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaximumBackOrderPurchaseQuantityGlobal { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaximumPrePurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaximumPrePurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaximumPrePurchaseQuantityGlobal { get; set; }
        #endregion

        #region Required Document
        /// <inheritdoc/>
        [StringLength(128), DefaultValue(null)]
        public string? DocumentRequiredForPurchase { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? DocumentRequiredForPurchaseMissingWarningMessage { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? DocumentRequiredForPurchaseExpiredWarningMessage { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? DocumentRequiredForPurchaseOverrideFee { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool DocumentRequiredForPurchaseOverrideFeeIsPercent { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? DocumentRequiredForPurchaseOverrideFeeWarningMessage { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? DocumentRequiredForPurchaseOverrideFeeAcceptedMessage { get; set; }
        #endregion

        #region Must Purchase In Multiples Of
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MustPurchaseInMultiplesOfAmount { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MustPurchaseInMultiplesOfAmountWarningMessage { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MustPurchaseInMultiplesOfAmountOverrideFee { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage { get; set; }
        #endregion

        #region Analytics filled data
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? TotalPurchasedAmount { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(TotalPurchasedAmountCurrency)), DefaultValue(null)]
        public int? TotalPurchasedAmountCurrencyID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Currency? TotalPurchasedAmountCurrency { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? TotalPurchasedQuantity { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Package)), DefaultValue(null)]
        public int? PackageID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Package? Package { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(MasterPack)), DefaultValue(null)]
        public int? MasterPackID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Package? MasterPack { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Pallet)), DefaultValue(null)]
        public int? PalletID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Package? Pallet { get; set; }

        /// <inheritdoc/>
        // ForeignKey is handled in the model builder for cascading
        [DefaultValue(null)]
        public int? RestockingFeeAmountCurrencyID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Currency? RestockingFeeAmountCurrency { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductAssociation>? ProductAssociations { get => productAssociations; set => productAssociations = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductAssociation>? ProductsAssociatedWith { get => productsAssociatedWith; set => productsAssociatedWith = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<ProductMembershipLevel>? ProductMembershipLevels { get => productMembershipLevels; set => productMembershipLevels = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<ProductDownload>? ProductDownloads { get => productDownloads; set => productDownloads = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductShipCarrierMethod>? ProductShipCarrierMethods { get => productShipCarrierMethods; set => productShipCarrierMethods = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<ProductSubscriptionType>? ProductSubscriptionTypes { get => productSubscriptionTypes; set => productSubscriptionTypes = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductRestriction>? ProductRestrictions { get => productRestrictions; set => productRestrictions = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductNotification>? ProductNotifications { get => productNotifications; set => productNotifications = value; }

        #region Don't map these out
        #region IHaveReviewsBase<ProductReview, Product> Properties
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Review>? Reviews { get => reviews; set => reviews = value; }
        #endregion

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductInventoryLocationSection>? ProductInventoryLocationSections { get => pils; set => pils = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductPricePoint>? ProductPricePoints { get => productPricePoints; set => productPricePoints = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<CartItem>? CartItems { get => cartItems; set => cartItems = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrderItem>? SalesOrderItems { get => salesOrderItems; set => salesOrderItems = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesReturnItem>? SalesReturnItems { get => salesReturnItems; set => salesReturnItems = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<DiscountProduct>? DiscountProducts { get => discountProducts; set => discountProducts = value; }
        #endregion
        #endregion
    }
}
