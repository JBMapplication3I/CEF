// <copyright file="Vendor.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IVendor
        : INameableBase,
            IHaveOrderMinimumsBase,
            IHaveFreeShippingMinimumsBase,
            IHaveNotesBase,
            IHaveANullableContactBase,
            IHaveATypeBase<VendorType>,
            IHaveImagesBase<Vendor, VendorImage, VendorImageType>,
            IHaveReviewsBase,
            IAmFilterableByAccount<VendorAccount>,
            IAmFilterableByBrand<BrandVendor>,
            IAmFilterableByFranchise<FranchiseVendor>,
            IAmFilterableByManufacturer<VendorManufacturer>,
            IAmFilterableByProduct<VendorProduct>,
            IAmFilterableByStore<StoreVendor>
    {
        #region Vendor Properties
        /// <summary>Gets or sets the notes 1.</summary>
        /// <value>The notes 1.</value>
        string? Notes1 { get; set; }

        /// <summary>Gets or sets the account number.</summary>
        /// <value>The account number.</value>
        string? AccountNumber { get; set; }

        /// <summary>Gets or sets the terms.</summary>
        /// <value>The terms.</value>
        string? Terms { get; set; }

        /// <summary>Gets or sets the term notes.</summary>
        /// <value>The term notes.</value>
        string? TermNotes { get; set; }

        /// <summary>Gets or sets the send method.</summary>
        /// <value>The send method.</value>
        string? SendMethod { get; set; }

        /// <summary>Gets or sets the email subject.</summary>
        /// <value>The email subject.</value>
        string? EmailSubject { get; set; }

        /// <summary>Gets or sets the ship to.</summary>
        /// <value>The ship to.</value>
        string? ShipTo { get; set; }

        /// <summary>Gets or sets the ship via notes.</summary>
        /// <value>The ship via notes.</value>
        string? ShipViaNotes { get; set; }

        /// <summary>Gets or sets who sign this IVendor.</summary>
        /// <value>Describes who sign this IVendor.</value>
        string? SignBy { get; set; }

        /// <summary>Gets or sets the default discount.</summary>
        /// <value>The default discount.</value>
        decimal? DefaultDiscount { get; set; }

        /// <summary>Gets or sets a value indicating whether we allow drop ship.</summary>
        /// <value>True if allow drop ship, false if not.</value>
        bool AllowDropShip { get; set; }

        /// <summary>Gets or sets the recommended purchase order dollar amount.</summary>
        /// <value>The recommended purchase order dollar amount.</value>
        decimal? RecommendedPurchaseOrderDollarAmount { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserName { get; set; }

        /// <summary>Gets or sets the password hash.</summary>
        /// <value>The password hash.</value>
        string? PasswordHash { get; set; }

        /// <summary>Gets or sets the security token.</summary>
        /// <value>The security token.</value>
        string? SecurityToken { get; set; }

        /// <summary>Gets or sets a value indicating whether the must reset password.</summary>
        /// <value>True if we must reset password, false if not.</value>
        bool MustResetPassword { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the purchase orders.</summary>
        /// <value>The purchase orders.</value>
        ICollection<PurchaseOrder>? PurchaseOrders { get; set; }

        /// <summary>Gets or sets the shipments.</summary>
        /// <value>The shipments.</value>
        ICollection<Shipment>? Shipments { get; set; }
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

    [SqlSchema("Vendors", "Vendor")]
    public class Vendor : NameableBase, IVendor
    {
        private ICollection<Note>? notes;
        private ICollection<BrandVendor>? brands;
        private ICollection<FranchiseVendor>? franchises;
        private ICollection<Review>? reviews;
        private ICollection<VendorImage>? images;
        private ICollection<VendorManufacturer>? manufacturers;
        private ICollection<VendorProduct>? products;
        private ICollection<StoreVendor>? stores;
        private ICollection<PurchaseOrder>? purchaseOrders;
        private ICollection<Shipment>? shipments;
        private ICollection<VendorAccount>? accounts;

        public Vendor()
        {
            // IHaveNotesBase Properties
            notes = new HashSet<Note>();
            // IHaveImagesBase Properties
            images = new HashSet<VendorImage>();
            // IHaveReviewsBase Properties
            reviews = new HashSet<Review>();
            // IAmFilterableByBrand Properties
            brands = new HashSet<BrandVendor>();
            // IAmFilterableByFranchise Properties
            franchises = new HashSet<FranchiseVendor>();
            // IAmFilterableByStore Properties
            stores = new HashSet<StoreVendor>();
            // IAmFilterableByProduct Properties
            products = new HashSet<VendorProduct>();
            // IAmFilterableByManufacturer Properties
            manufacturers = new HashSet<VendorManufacturer>();
            // IAmFilterableByAccount Properties
            accounts = new HashSet<VendorAccount>();
            // Vendor Properties
            purchaseOrders = new HashSet<PurchaseOrder>();
            shipments = new HashSet<Shipment>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual VendorType? Type { get; set; }
        #endregion

        #region IHaveANullableContactBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Contact)), DefaultValue(null)]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Contact? Contact { get; set; }
        #endregion

        #region IHaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<VendorImage>? Images { get => images; set => images = value; }
        #endregion

        #region IHaveReviewsBase<VendorReview, Vendor> Properties
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
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
        [InverseProperty(nameof(Category.VendorMinimumOrderDollarAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumOrderDollarAmountBufferCategory { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(Category.VendorMinimumOrderQuantityAmountBufferCategories)), DefaultValue(null), JsonIgnore]
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
        [InverseProperty(nameof(Category.VendorMinimumForFreeShippingDollarAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(Category.VendorMinimumForFreeShippingQuantityAmountBufferCategories)), DefaultValue(null), JsonIgnore]
        public int? MinimumForFreeShippingQuantityAmountBufferCategoryID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Category? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
        #endregion

        #region IAmFilterableByAccount Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<VendorAccount>? Accounts { get => accounts; set => accounts = value; }
        #endregion

        #region IAmFilterableByBrand Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandVendor>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByFranchise Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<FranchiseVendor>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByManufacturer Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<VendorManufacturer>? Manufacturers { get => manufacturers; set => manufacturers = value; }
        #endregion

        #region IAmFilterableByProduct Properties
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<VendorProduct>? Products { get => products; set => products = value; }
        #endregion

        #region IAmFilterableByStore Properties
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreVendor>? Stores { get => stores; set => stores = value; }
        #endregion

        #region Vendor Properties
        /// <inheritdoc/>
        [StringIsUnicode(false), Column("Notes"), DefaultValue(null)]
        public string? Notes1 { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(100), DefaultValue(null)]
        public string? AccountNumber { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(100), DefaultValue(null)]
        public string? Terms { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? TermNotes { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(100), DefaultValue(null)]
        public string? SendMethod { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(300), DefaultValue(null)]
        public string? EmailSubject { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(100), DefaultValue(null)]
        public string? ShipTo { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ShipViaNotes { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(100), DefaultValue(null)]
        public string? SignBy { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool AllowDropShip { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? DefaultDiscount { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? RecommendedPurchaseOrderDollarAmount { get; set; }

        /// <inheritdoc/>
        [StringLength(128), Index, DefaultValue(null)]
        public string? UserName { get; set; }

        /// <inheritdoc/>
        [StringLength(128), Index, DefaultValue(null)]
        public string? PasswordHash { get; set; }

        /// <inheritdoc/>
        [StringLength(128), Index, DefaultValue(null)]
        public string? SecurityToken { get; set; }

        /// <inheritdoc/>
        [Index, DefaultValue(false)]
        public bool MustResetPassword { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Shipment>? Shipments { get => shipments; set => shipments = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<PurchaseOrder>? PurchaseOrders { get => purchaseOrders; set => purchaseOrders = value; }
        #endregion
    }
}
