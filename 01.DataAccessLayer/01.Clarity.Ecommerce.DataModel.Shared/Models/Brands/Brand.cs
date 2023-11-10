// <copyright file="Brand.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IBrand
        : INameableBase,
            IHaveOrderMinimumsBase,
            IHaveFreeShippingMinimumsBase,
            IHaveNotesBase,
            IHaveImagesBase<Brand, BrandImage, BrandImageType>,
            IAmFilterableByAccount<BrandAccount>,
            IAmFilterableByFranchise<BrandFranchise>,
            IAmFilterableByCategory<BrandCategory>,
            IAmFilterableByProduct<BrandProduct>,
            IAmFilterableByStore<BrandStore>,
            IAmFilterableByUser<BrandUser>
    {
        #region Associated Objects
        /// <summary>Gets or sets the brand currencies.</summary>
        /// <value>The brand currencies.</value>
        ICollection<BrandCurrency>? BrandCurrencies { get; set; }

        /// <summary>Gets or sets the brand inventory locations.</summary>
        /// <value>The brand inventory locations.</value>
        ICollection<BrandInventoryLocation>? BrandInventoryLocations { get; set; }

        /// <summary>Gets or sets the brand languages.</summary>
        /// <value>The brand languages.</value>
        ICollection<BrandLanguage>? BrandLanguages { get; set; }

        /// <summary>Gets or sets the brand site domains.</summary>
        /// <value>The brand site domains.</value>
        ICollection<BrandSiteDomain>? BrandSiteDomains { get; set; }
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

    [SqlSchema("Brands", "Brand")]
    public class Brand : NameableBase, IBrand
    {
        private ICollection<Note>? notes;
        private ICollection<BrandUser>? users;
        private ICollection<BrandImage>? images;
        private ICollection<BrandStore>? stores;
        private ICollection<BrandAccount>? accounts;
        private ICollection<BrandProduct>? products;
        private ICollection<BrandCategory>? categories;
        private ICollection<BrandFranchise>? franchises;

        private ICollection<BrandCurrency>? brandCurrencies;
        private ICollection<BrandInventoryLocation>? brandInventoryLocations;
        private ICollection<BrandLanguage>? brandLanguages;
        private ICollection<BrandSiteDomain>? brandSiteDomains;

        public Brand()
        {
            // IHaveNotesBase Properties
            notes = new HashSet<Note>();
            // IHaveImagesBase Properties
            images = new HashSet<BrandImage>();
            // IAmFilterableByAccount
            accounts = new HashSet<BrandAccount>();
            // IAmFilterableByFranchise
            franchises = new HashSet<BrandFranchise>();
            // IAmFilterableByProduct
            products = new HashSet<BrandProduct>();
            // IAmFilterableByUser
            users = new HashSet<BrandUser>();
            // IAmFilterableByStore
            stores = new HashSet<BrandStore>();
            // IAmFilterableByCategory
            categories = new HashSet<BrandCategory>();
            // Brand Properties
            brandCurrencies = new HashSet<BrandCurrency>();
            brandInventoryLocations = new HashSet<BrandInventoryLocation>();
            brandLanguages = new HashSet<BrandLanguage>();
            brandSiteDomains = new HashSet<BrandSiteDomain>();
        }

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<BrandImage>? Images { get => images; set => images = value; }
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
        public virtual ICollection<BrandAccount>? Accounts { get => accounts; set => accounts = value; }
        #endregion

        #region IAmFilterableByCategory
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandCategory>? Categories { get => categories; set => categories = value; }
        #endregion

        #region IAmFilterableByFranchise
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<BrandFranchise>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandProduct>? Products { get => products; set => products = value; }
        #endregion

        #region IAmFilterableByStore
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<BrandStore>? Stores { get => stores; set => stores = value; }
        #endregion

        #region IAmFilterableByUser
        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandUser>? Users { get => users; set => users = value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandCurrency>? BrandCurrencies { get => brandCurrencies; set => brandCurrencies = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<BrandInventoryLocation>? BrandInventoryLocations { get => brandInventoryLocations; set => brandInventoryLocations = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandLanguage>? BrandLanguages { get => brandLanguages; set => brandLanguages = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandSiteDomain>? BrandSiteDomains { get => brandSiteDomains; set => brandSiteDomains = value; }
        #endregion
    }
}
