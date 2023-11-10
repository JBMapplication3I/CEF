// <copyright file="SalesItemBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    /// <summary>A sales collection base.</summary>
    /// <typeparam name="TMaster">   Type of the master entity.</typeparam>
    /// <typeparam name="TSalesItem">Type of the sales item entity.</typeparam>
    /// <typeparam name="TDiscount"> Type of the discount entity.</typeparam>
    /// <typeparam name="TTarget">   Type of the target entity.</typeparam>
    /// <seealso cref="NameableBase"/>
    /// <seealso cref="ISalesItemBase{TSalesItem,TDiscount,TTarget}"/>
    public abstract class SalesItemBase<TMaster, TSalesItem, TDiscount, TTarget>
        : NameableBase,
            ISalesItemBase<TSalesItem, TDiscount, TTarget>
        where TMaster : ISalesCollectionBase
        where TSalesItem : IHaveAppliedDiscountsBase<TSalesItem, TDiscount>
        where TDiscount : IAppliedDiscountBase<TSalesItem, TDiscount>
        where TTarget : ISalesItemTargetBase
    {
        private ICollection<TDiscount>? discounts;
        private ICollection<TTarget>? targets;
        private ICollection<Note>? notes;

        /// <summary>Initializes a new instance of the <see cref="SalesItemBase{TMaster, TSalesItem, TDiscount, TTarget}"/> class.</summary>
        protected SalesItemBase()
        {
            // SalesItemBase Properties
            discounts = new HashSet<TDiscount>();
            targets = new HashSet<TTarget>();
            notes = new HashSet<Note>();
        }

        #region IAmFilterableByNullableProduct Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Product)), DefaultValue(null)]
        public int? ProductID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), AllowMapInWithRelateWorkflowsButDontAutoGenerate, JsonIgnore]
        public virtual Product? Product { get; set; }
        #endregion

        #region IAmFilterableByNullableUser Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(null)]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }
        #endregion

        #region SalesItemBase Properties
        /// <inheritdoc/>
        [StringLength(1000), StringIsUnicode(false), DefaultValue(null)]
        public string? Sku { get; set; }

        /// <inheritdoc/>
        [StringLength(0100), StringIsUnicode(false), DefaultValue(null)]
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), StringIsUnicode(false), DefaultValue(null)]
        public string? ForceUniqueLineItemKey { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0000)]
        public decimal Quantity { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityBackOrdered { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityPreSold { get; set; }

        /// <inheritdoc/>
        [NotMapped]
        public decimal TotalQuantity => Quantity + (QuantityBackOrdered ?? 0m) + (QuantityPreSold ?? 0m);

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0000)]
        public decimal UnitCorePrice { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? UnitSoldPrice { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? UnitCorePriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? UnitSoldPriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        [StringLength(500), StringIsUnicode(false), DefaultValue(null)]
        public string? Status { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual TMaster? Master { get; set; }

        // Note: This is only for searching
        [NotMapped, JsonIgnore]
        ISalesCollectionBase? ISalesItemBase<TSalesItem, TDiscount, TTarget>.Master
        {
            get => Master;
            set => Master = (TMaster?)value;
        }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(OriginalCurrency)), DefaultValue(null)]
        public int? OriginalCurrencyID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Currency? OriginalCurrency { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SellingCurrency)), DefaultValue(null)]
        public int? SellingCurrencyID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Currency? SellingCurrency { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<TDiscount>? Discounts { get => discounts; set => discounts = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<TTarget>? Targets { get => targets; set => targets = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion
        #endregion
    }
}
