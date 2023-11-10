// <copyright file="Category.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ICategory
        : INameableBase,
            IHaveSeoBase,
            IHaveOrderMinimumsBase,
            IHaveFreeShippingMinimumsBase,
            IHaveRequiresRolesBase,
            IHaveReviewsBase,
            IHaveAParentBase<Category>,
            IHaveATypeBase<CategoryType>,
            IHaveImagesBase<Category, CategoryImage, CategoryImageType>,
            IHaveStoredFilesBase<Category, CategoryFile>,
            IAmFilterableByBrand<BrandCategory>,
            IAmFilterableByFranchise<FranchiseCategory>,
            IAmFilterableByProduct<ProductCategory>,
            IAmFilterableByStore<StoreCategory>
    {
        #region Category Properties
        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets a value indicating whether this ICategory is visible.</summary>
        /// <value>True if this ICategory is visible, false if not.</value>
        bool IsVisible { get; set; }

        /// <summary>Gets or sets a value indicating whether the in menu should be included.</summary>
        /// <value>True if include in menu, false if not.</value>
        bool IncludeInMenu { get; set; }

        /// <summary>Gets or sets the header content.</summary>
        /// <value>The header content.</value>
        string? HeaderContent { get; set; }

        /// <summary>Gets or sets the sidebar content.</summary>
        /// <value>The sidebar content.</value>
        string? SidebarContent { get; set; }

        /// <summary>Gets or sets the footer content.</summary>
        /// <value>The footer content.</value>
        string? FooterContent { get; set; }

        /// <summary>Gets or sets the handling charge.</summary>
        /// <value>The handling charge.</value>
        decimal? HandlingCharge { get; set; }

        /// <summary>Gets or sets the restocking fee percent.</summary>
        /// <value>The restocking fee percent.</value>
        decimal? RestockingFeePercent { get; set; }

        /// <summary>Gets or sets the restocking fee amount.</summary>
        /// <value>The restocking fee amount.</value>
        decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the restocking fee amount currency.</summary>
        /// <value>The identifier of the restocking fee amount currency.</value>
        int? RestockingFeeAmountCurrencyID { get; set; }

        /// <summary>Gets or sets the restocking fee amount currency.</summary>
        /// <value>The restocking fee amount currency.</value>
        Currency? RestockingFeeAmountCurrency { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Categories", "Category")]
    public class Category : NameableBase, ICategory
    {
        private ICollection<Review>? reviews;
        private ICollection<Category>? children;
        private ICollection<BrandCategory>? brands;
        private ICollection<StoreCategory>? stores;
        private ICollection<CategoryImage>? images;
        private ICollection<CategoryFile>? storedFiles;
        private ICollection<ProductCategory>? products;
        private ICollection<FranchiseCategory>? franchises;
        private ICollection<Category>? minimumOrderDollarAmountBufferCategories;
        private ICollection<Category>? minimumOrderQuantityAmountBufferCategories;
        private ICollection<Manufacturer>? minimumOrderManufacturerDollarAmountBufferCategories;
        private ICollection<Manufacturer>? minimumOrderManufacturerQuantityAmountBufferCategories;
        private ICollection<Brand>? minimumOrderBrandDollarAmountBufferCategories;
        private ICollection<Brand>? minimumOrderBrandQuantityAmountBufferCategories;
        private ICollection<Franchise>? minimumOrderFranchiseDollarAmountBufferCategories;
        private ICollection<Franchise>? minimumOrderFranchiseQuantityAmountBufferCategories;
        private ICollection<Store>? minimumOrderStoreDollarAmountBufferCategories;
        private ICollection<Store>? minimumOrderStoreQuantityAmountBufferCategories;
        private ICollection<Vendor>? minimumOrderVendorDollarAmountBufferCategories;
        private ICollection<Vendor>? minimumOrderVendorQuantityAmountBufferCategories;
        private ICollection<Category>? minimumForFreeShippingDollarAmountBufferCategories;
        private ICollection<Category>? minimumForFreeShippingQuantityAmountBufferCategories;
        private ICollection<Manufacturer>? minimumForFreeShippingManufacturerDollarAmountBufferCategories;
        private ICollection<Manufacturer>? minimumForFreeShippingManufacturerQuantityAmountBufferCategories;
        private ICollection<Brand>? minimumForFreeShippingBrandDollarAmountBufferCategories;
        private ICollection<Brand>? minimumForFreeShippingBrandQuantityAmountBufferCategories;
        private ICollection<Franchise>? minimumForFreeShippingFranchiseDollarAmountBufferCategories;
        private ICollection<Franchise>? minimumForFreeShippingFranchiseQuantityAmountBufferCategories;
        private ICollection<Store>? minimumForFreeShippingStoreDollarAmountBufferCategories;
        private ICollection<Store>? minimumForFreeShippingStoreQuantityAmountBufferCategories;
        private ICollection<Vendor>? minimumForFreeShippingVendorDollarAmountBufferCategories;
        private ICollection<Vendor>? minimumForFreeShippingVendorQuantityAmountBufferCategories;

        public Category()
        {
            // IHaveAParentBase
            children = new HashSet<Category>();
            // IHaveStoredFilesBase
            storedFiles = new HashSet<CategoryFile>();
            // IHaveImagesBase
            images = new HashSet<CategoryImage>();
            // IHaveReviewsBase
            reviews = new HashSet<Review>();
            // IAmFilterableByBrandsBase
            brands = new HashSet<BrandCategory>();
            // IAmFilterableByFranchisesBase
            franchises = new HashSet<FranchiseCategory>();
            // IAmFilterableByProductsBase
            products = new HashSet<ProductCategory>();
            // IAmFilterableByStoreBase
            stores = new HashSet<StoreCategory>();
            // Category Properties
            minimumOrderDollarAmountBufferCategories = new HashSet<Category>();
            minimumOrderQuantityAmountBufferCategories = new HashSet<Category>();
            minimumOrderManufacturerDollarAmountBufferCategories = new HashSet<Manufacturer>();
            minimumOrderManufacturerQuantityAmountBufferCategories = new HashSet<Manufacturer>();
            minimumOrderBrandDollarAmountBufferCategories = new HashSet<Brand>();
            minimumOrderBrandQuantityAmountBufferCategories = new HashSet<Brand>();
            minimumOrderFranchiseDollarAmountBufferCategories = new HashSet<Franchise>();
            minimumOrderFranchiseQuantityAmountBufferCategories = new HashSet<Franchise>();
            minimumOrderStoreDollarAmountBufferCategories = new HashSet<Store>();
            minimumOrderStoreQuantityAmountBufferCategories = new HashSet<Store>();
            minimumOrderVendorDollarAmountBufferCategories = new HashSet<Vendor>();
            minimumOrderVendorQuantityAmountBufferCategories = new HashSet<Vendor>();
            minimumForFreeShippingDollarAmountBufferCategories = new HashSet<Category>();
            minimumForFreeShippingQuantityAmountBufferCategories = new HashSet<Category>();
            minimumForFreeShippingManufacturerDollarAmountBufferCategories = new HashSet<Manufacturer>();
            minimumForFreeShippingManufacturerQuantityAmountBufferCategories = new HashSet<Manufacturer>();
            minimumForFreeShippingBrandDollarAmountBufferCategories = new HashSet<Brand>();
            minimumForFreeShippingBrandQuantityAmountBufferCategories = new HashSet<Brand>();
            minimumForFreeShippingFranchiseDollarAmountBufferCategories = new HashSet<Franchise>();
            minimumForFreeShippingFranchiseQuantityAmountBufferCategories = new HashSet<Franchise>();
            minimumForFreeShippingStoreDollarAmountBufferCategories = new HashSet<Store>();
            minimumForFreeShippingStoreQuantityAmountBufferCategories = new HashSet<Store>();
            minimumForFreeShippingVendorDollarAmountBufferCategories = new HashSet<Vendor>();
            minimumForFreeShippingVendorQuantityAmountBufferCategories = new HashSet<Vendor>();
        }

        #region HaveAParentBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Parent)), DefaultValue(null)] // Foreign Key handled in modelBuilder
        public int? ParentID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(Children)), DefaultValue(null), JsonIgnore]
        public virtual Category? Parent { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Category>? Children { get => children; set => children = value; }
        #endregion

        #region IHaveSeoBase Properties
        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DontMapOutWithListing]
        public string? SeoKeywords { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false)]
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        [StringLength(75), StringIsUnicode(false), DontMapOutWithListing]
        public string? SeoPageTitle { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DontMapOutWithListing]
        public string? SeoDescription { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DontMapOutWithListing]
        public string? SeoMetaData { get; set; }
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
        public bool MinimumOrderDollarAmountOverrideFeeIsPercent { get; set; }

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
        public bool MinimumOrderQuantityAmountOverrideFeeIsPercent { get; set; }

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
        [InverseProperty(nameof(MinimumOrderDollarAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumOrderDollarAmountBufferCategory { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(MinimumOrderQuantityAmountBufferCategories)), DefaultValue(null), JsonIgnore]
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
        [InverseProperty(nameof(MinimumForFreeShippingDollarAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(MinimumForFreeShippingQuantityAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public int? MinimumForFreeShippingQuantityAmountBufferCategoryID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
        #endregion

        #region IHaveATypeBase<CategoryType>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type))]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        public virtual CategoryType? Type { get; set; }
        #endregion

        #region IAmFilterableByBrand Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandCategory>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByCategory Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<StoreCategory>? Stores { get => stores; set => stores = value; }
        #endregion

        #region IAmFilterableByFranchise Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<FranchiseCategory>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByProduct Properties
        /// <inheritdoc/>
        [DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductCategory>? Products { get => products; set => products = value; }
        #endregion

        #region IHaveRequiresRolesBase Properties
        /// <inheritdoc/>
        [StringLength(512), DefaultValue(null)]
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

        #region IHaveReviewsBase Properties
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Review>? Reviews { get => reviews; set => reviews = value; }
        #endregion

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CategoryImage>? Images { get => images; set => images = value; }
        #endregion

        #region HaveStoredFilesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CategoryFile>? StoredFiles { get => storedFiles; set => storedFiles = value; }
        #endregion

        #region Category Properties
        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), Index]
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsVisible { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IncludeInMenu { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? HeaderContent { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? SidebarContent { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? FooterContent { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? HandlingCharge { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? RestockingFeePercent { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(RestockingFeeAmountCurrency)), DefaultValue(null)]
        public int? RestockingFeeAmountCurrencyID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Currency? RestockingFeeAmountCurrency { get; set; }
        #endregion

        #region Associated Objects
        // Note: These are only here to satisfy relationship requirements, they should never be used

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Category>? MinimumOrderDollarAmountBufferCategories { get => minimumOrderDollarAmountBufferCategories; set => minimumOrderDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Category>? MinimumOrderQuantityAmountBufferCategories { get => minimumOrderQuantityAmountBufferCategories; set => minimumOrderQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Manufacturer>? ManufacturerMinimumOrderDollarAmountBufferCategories { get => minimumOrderManufacturerDollarAmountBufferCategories; set => minimumOrderManufacturerDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Manufacturer>? ManufacturerMinimumOrderQuantityAmountBufferCategories { get => minimumOrderManufacturerQuantityAmountBufferCategories; set => minimumOrderManufacturerQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Brand>? BrandMinimumOrderDollarAmountBufferCategories { get => minimumOrderBrandDollarAmountBufferCategories; set => minimumOrderBrandDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Brand>? BrandMinimumOrderQuantityAmountBufferCategories { get => minimumOrderBrandQuantityAmountBufferCategories; set => minimumOrderBrandQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Franchise>? FranchiseMinimumOrderDollarAmountBufferCategories { get => minimumOrderFranchiseDollarAmountBufferCategories; set => minimumOrderFranchiseDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Franchise>? FranchiseMinimumOrderQuantityAmountBufferCategories { get => minimumOrderFranchiseQuantityAmountBufferCategories; set => minimumOrderFranchiseQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Store>? StoreMinimumOrderDollarAmountBufferCategories { get => minimumOrderStoreDollarAmountBufferCategories; set => minimumOrderStoreDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Store>? StoreMinimumOrderQuantityAmountBufferCategories { get => minimumOrderStoreQuantityAmountBufferCategories; set => minimumOrderStoreQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Vendor>? VendorMinimumOrderDollarAmountBufferCategories { get => minimumOrderVendorDollarAmountBufferCategories; set => minimumOrderVendorDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Vendor>? VendorMinimumOrderQuantityAmountBufferCategories { get => minimumOrderVendorQuantityAmountBufferCategories; set => minimumOrderVendorQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Category>? MinimumForFreeShippingDollarAmountBufferCategories { get => minimumForFreeShippingDollarAmountBufferCategories; set => minimumForFreeShippingDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Category>? MinimumForFreeShippingQuantityAmountBufferCategories { get => minimumForFreeShippingQuantityAmountBufferCategories; set => minimumForFreeShippingQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Manufacturer>? ManufacturerMinimumForFreeShippingDollarAmountBufferCategories { get => minimumForFreeShippingManufacturerDollarAmountBufferCategories; set => minimumForFreeShippingManufacturerDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Manufacturer>? ManufacturerMinimumForFreeShippingQuantityAmountBufferCategories { get => minimumForFreeShippingManufacturerQuantityAmountBufferCategories; set => minimumForFreeShippingManufacturerQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Brand>? BrandMinimumForFreeShippingDollarAmountBufferCategories { get => minimumForFreeShippingBrandDollarAmountBufferCategories; set => minimumForFreeShippingBrandDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Brand>? BrandMinimumForFreeShippingQuantityAmountBufferCategories { get => minimumForFreeShippingBrandQuantityAmountBufferCategories; set => minimumForFreeShippingBrandQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Franchise>? FranchiseMinimumForFreeShippingDollarAmountBufferCategories { get => minimumForFreeShippingFranchiseDollarAmountBufferCategories; set => minimumForFreeShippingFranchiseDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Franchise>? FranchiseMinimumForFreeShippingQuantityAmountBufferCategories { get => minimumForFreeShippingFranchiseQuantityAmountBufferCategories; set => minimumForFreeShippingFranchiseQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Store>? StoreMinimumForFreeShippingDollarAmountBufferCategories { get => minimumForFreeShippingStoreDollarAmountBufferCategories; set => minimumForFreeShippingStoreDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Store>? StoreMinimumForFreeShippingQuantityAmountBufferCategories { get => minimumForFreeShippingStoreQuantityAmountBufferCategories; set => minimumForFreeShippingStoreQuantityAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Vendor>? VendorMinimumForFreeShippingDollarAmountBufferCategories { get => minimumForFreeShippingVendorDollarAmountBufferCategories; set => minimumForFreeShippingVendorDollarAmountBufferCategories = value; }

        /// <summary>Gets or sets the categories the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer categories.</value>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Vendor>? VendorMinimumForFreeShippingQuantityAmountBufferCategories { get => minimumForFreeShippingVendorQuantityAmountBufferCategories; set => minimumForFreeShippingVendorQuantityAmountBufferCategories = value; }
        #endregion
    }
}
