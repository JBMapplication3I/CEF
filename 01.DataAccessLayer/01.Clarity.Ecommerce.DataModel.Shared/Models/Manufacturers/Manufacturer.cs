// <copyright file="Manufacturer.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the manufacturer class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IManufacturer
        : INameableBase,
            IHaveOrderMinimumsBase,
            IHaveFreeShippingMinimumsBase,
            IHaveNotesBase,
            IHaveANullableContactBase,
            IHaveATypeBase<ManufacturerType>,
            IHaveImagesBase<Manufacturer, ManufacturerImage, ManufacturerImageType>,
            IHaveReviewsBase,
            IAmFilterableByBrand<BrandManufacturer>,
            IAmFilterableByFranchise<FranchiseManufacturer>,
            IAmFilterableByProduct<ManufacturerProduct>,
            IAmFilterableByStore<StoreManufacturer>,
            IAmFilterableByVendor<VendorManufacturer>
    {
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

    [SqlSchema("Manufacturers", "Manufacturer")]
    public class Manufacturer : NameableBase, IManufacturer
    {
        private ICollection<ManufacturerImage>? images;
        private ICollection<ManufacturerProduct>? products;
        private ICollection<Note>? notes;
        private ICollection<Review>? reviews;
        private ICollection<BrandManufacturer>? brands;
        private ICollection<FranchiseManufacturer>? franchises;
        private ICollection<StoreManufacturer>? stores;
        private ICollection<VendorManufacturer>? vendors;

        public Manufacturer()
        {
            // IHaveNotesBase Properties
            notes = new HashSet<Note>();
            // IHaveImagesBase Properties
            images = new HashSet<ManufacturerImage>();
            // IHaveReviewsBase Properties
            reviews = new HashSet<Review>();
            // IAmFilterableByBrand Properties
            brands = new HashSet<BrandManufacturer>();
            // IAmFilterableByFranchise Properties
            franchises = new HashSet<FranchiseManufacturer>();
            // IAmFilterableByProduct Properties
            products = new HashSet<ManufacturerProduct>();
            // IAmFilterableByStore Properties
            stores = new HashSet<StoreManufacturer>();
            // IAmFilterableByVendor Properties
            vendors = new HashSet<VendorManufacturer>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual ManufacturerType? Type { get; set; }
        #endregion

        #region IHaveANullableContactBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Contact)), DefaultValue(null)]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
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
        public virtual ICollection<ManufacturerImage>? Images { get => images; set => images = value; }
        #endregion

        #region IHaveReviewsBase<ManufacturerReview, Manufacturer> Properties
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Review>? Reviews { get => reviews; set => reviews = value; }
        #endregion

        #region IHaveOrderMinimumsBase Properties
        /// <inheritdoc/>
        [StringLength(1024), DefaultValue(null), DontMapOutWithListing]
        public string? MinimumOrderDollarAmountWarningMessage { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumOrderDollarAmount { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinimumOrderDollarAmountAfter { get; set; }

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
        [InverseProperty(nameof(Category.ManufacturerMinimumOrderDollarAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumOrderDollarAmountBufferCategory { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(Category.ManufacturerMinimumOrderQuantityAmountBufferCategories)), DefaultValue(null), JsonIgnore]
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
        [InverseProperty(nameof(Category.ManufacturerMinimumForFreeShippingDollarAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(Category.ManufacturerMinimumForFreeShippingQuantityAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public int? MinimumForFreeShippingQuantityAmountBufferCategoryID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
        #endregion

        #region IAmFilterableByBrand Properties
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandManufacturer>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByFranchise Properties
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseManufacturer>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByProduct Properties
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<ManufacturerProduct>? Products { get => products; set => products = value; }
        #endregion

        #region IAmFilterableByStore Properties
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreManufacturer>? Stores { get => stores; set => stores = value; }
        #endregion

        #region IAmFilterableByVendor Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<VendorManufacturer>? Vendors { get => vendors; set => vendors = value; }
        #endregion
    }
}
