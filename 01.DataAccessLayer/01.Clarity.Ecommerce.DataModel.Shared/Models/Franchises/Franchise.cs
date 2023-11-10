// <copyright file="Franchise.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for franchise.</summary>
    public interface IFranchise
        : INameableBase,
            IHaveOrderMinimumsBase,
            IHaveFreeShippingMinimumsBase,
            IHaveATypeBase<FranchiseType>,
            IHaveNotesBase,
            IHaveImagesBase<Franchise, FranchiseImage, FranchiseImageType>,
            IAmFilterableByAccount<FranchiseAccount>,
            IAmFilterableByBrand<BrandFranchise>,
            IAmFilterableByCategory<FranchiseCategory>,
            IAmFilterableByManufacturer<FranchiseManufacturer>,
            IAmFilterableByProduct<FranchiseProduct>,
            IAmFilterableByStore<FranchiseStore>,
            IAmFilterableByUser<FranchiseUser>,
            IAmFilterableByVendor<FranchiseVendor>
    {
        #region Associated Objects
        /// <summary>Gets or sets the franchise currencies.</summary>
        /// <value>The franchise currencies.</value>
        ICollection<FranchiseCurrency>? FranchiseCurrencies { get; set; }

        /// <summary>Gets or sets the franchise inventory locations.</summary>
        /// <value>The franchise inventory locations.</value>
        ICollection<FranchiseInventoryLocation>? FranchiseInventoryLocations { get; set; }

        /// <summary>Gets or sets the franchise languages.</summary>
        /// <value>The franchise languages.</value>
        ICollection<FranchiseLanguage>? FranchiseLanguages { get; set; }

        /// <summary>Gets or sets the franchise site domains.</summary>
        /// <value>The franchise site domains.</value>
        ICollection<FranchiseSiteDomain>? FranchiseSiteDomains { get; set; }

        /// <summary>Gets or sets the franchise countries.</summary>
        /// <value>The franchise countries.</value>
        ICollection<FranchiseCountry>? FranchiseCountries { get; set; }

        /// <summary>Gets or sets the franchise regions.</summary>
        /// <value>The franchise regions.</value>
        ICollection<FranchiseRegion>? FranchiseRegions { get; set; }

        /// <summary>Gets or sets the franchise districts.</summary>
        /// <value>The franchise districts.</value>
        ICollection<FranchiseDistrict>? FranchiseDistricts { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Franchises", "Franchise")]
    public class Franchise : NameableBase, IFranchise
    {
        private ICollection<Note>? notes;
        private ICollection<FranchiseUser>? users;
        private ICollection<FranchiseImage>? images;
        private ICollection<BrandFranchise>? brands;
        private ICollection<FranchiseStore>? stores;
        private ICollection<FranchiseVendor>? vendors;
        private ICollection<FranchiseAccount>? accounts;
        private ICollection<FranchiseProduct>? products;
        private ICollection<FranchiseCategory>? categories;
        private ICollection<FranchiseManufacturer>? manufacturers;

        private ICollection<FranchiseCurrency>? franchiseCurrencies;
        private ICollection<FranchiseInventoryLocation>? franchiseInventoryLocations;
        private ICollection<FranchiseLanguage>? franchiseLanguages;
        private ICollection<FranchiseSiteDomain>? franchiseSiteDomains;
        private ICollection<FranchiseCountry>? franchiseCountries;
        private ICollection<FranchiseRegion>? franchiseRegions;
        private ICollection<FranchiseDistrict>? franchiseDistricts;

        public Franchise()
        {
            // IHaveNotesBase Properties
            notes = new HashSet<Note>();
            // IHaveImagesBase Properties
            images = new HashSet<FranchiseImage>();
            // IAmFilterableByAccount
            accounts = new HashSet<FranchiseAccount>();
            // IAmFilterableByBrand
            brands = new HashSet<BrandFranchise>();
            // IAmFilterableByCategory
            categories = new HashSet<FranchiseCategory>();
            // IAmFilterableByManufacturer
            manufacturers = new HashSet<FranchiseManufacturer>();
            // IAmFilterableByProduct
            products = new HashSet<FranchiseProduct>();
            // IAmFilterableByStore
            stores = new HashSet<FranchiseStore>();
            // IAmFilterableByUser
            users = new HashSet<FranchiseUser>();
            // IAmFilterableByVendor
            vendors = new HashSet<FranchiseVendor>();
            // Franchise Properties
            franchiseCurrencies = new HashSet<FranchiseCurrency>();
            franchiseInventoryLocations = new HashSet<FranchiseInventoryLocation>();
            franchiseLanguages = new HashSet<FranchiseLanguage>();
            franchiseSiteDomains = new HashSet<FranchiseSiteDomain>();
            franchiseCountries = new HashSet<FranchiseCountry>();
            franchiseRegions = new HashSet<FranchiseRegion>();
            franchiseDistricts = new HashSet<FranchiseDistrict>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual FranchiseType? Type { get; set; }
        #endregion

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseImage>? Images { get => images; set => images = value; }
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

        #region IAmFilterableByAccount
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseAccount>? Accounts { get => accounts; set => accounts = value; }
        #endregion

        #region IAmFilterableByBrand
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandFranchise>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByCategory
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseCategory>? Categories { get => categories; set => categories = value; }
        #endregion

        #region IAmFilterableByManufacturer
        /// <inheritdoc/>
        [DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseManufacturer>? Manufacturers { get => manufacturers; set => manufacturers = value; }
        #endregion

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseProduct>? Products { get => products; set => products = value; }
        #endregion

        #region IAmFilterableByStore
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseStore>? Stores { get => stores; set => stores = value; }
        #endregion

        #region IAmFilterableByUser
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseUser>? Users { get => users; set => users = value; }
        #endregion

        #region IAmFilterableByVendor
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseVendor>? Vendors { get => vendors; set => vendors = value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseCurrency>? FranchiseCurrencies { get => franchiseCurrencies; set => franchiseCurrencies = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseInventoryLocation>? FranchiseInventoryLocations { get => franchiseInventoryLocations; set => franchiseInventoryLocations = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseLanguage>? FranchiseLanguages { get => franchiseLanguages; set => franchiseLanguages = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseSiteDomain>? FranchiseSiteDomains { get => franchiseSiteDomains; set => franchiseSiteDomains = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseCountry>? FranchiseCountries { get => franchiseCountries; set => franchiseCountries = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseRegion>? FranchiseRegions { get => franchiseRegions; set => franchiseRegions = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseDistrict>? FranchiseDistricts { get => franchiseDistricts; set => franchiseDistricts = value; }
        #endregion
    }
}
