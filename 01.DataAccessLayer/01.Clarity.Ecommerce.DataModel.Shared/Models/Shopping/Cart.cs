// <copyright file="Cart.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface ICart
        : ISalesCollectionBase<Cart,
                CartStatus,
                CartType,
                CartItem,
                AppliedCartDiscount,
                CartState,
                CartFile,
                CartContact,
                CartEvent,
                CartEventType>,
            IHaveNotesBase
    {
        #region Cart Properties
        /// <summary>Gets or sets the identifier of the session.</summary>
        /// <value>The identifier of the session.</value>
        Guid? SessionID { get; set; }

        /// <summary>Gets or sets the requested ship date.</summary>
        /// <value>The requested ship date.</value>
        DateTime? RequestedShipDate { get; set; }

        /// <summary>Gets or sets the subtotal shipping modifier.</summary>
        /// <value>The subtotal shipping modifier.</value>
        decimal? SubtotalShippingModifier { get; set; }

        /// <summary>Gets or sets the subtotal shipping modifier mode.</summary>
        /// <value>The subtotal shipping modifier mode.</value>
        int? SubtotalShippingModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal taxes modifier.</summary>
        /// <value>The subtotal taxes modifier.</value>
        decimal? SubtotalTaxesModifier { get; set; }

        /// <summary>Gets or sets the subtotal taxes modifier mode.</summary>
        /// <value>The subtotal taxes modifier mode.</value>
        int? SubtotalTaxesModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal fees modifier.</summary>
        /// <value>The subtotal fees modifier.</value>
        decimal? SubtotalFeesModifier { get; set; }

        /// <summary>Gets or sets the subtotal fees modifier mode.</summary>
        /// <value>The subtotal fees modifier mode.</value>
        int? SubtotalFeesModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal handling modifier.</summary>
        /// <value>The subtotal handling modifier.</value>
        decimal? SubtotalHandlingModifier { get; set; }

        /// <summary>Gets or sets the subtotal handling modifier mode.</summary>
        /// <value>The subtotal handling modifier mode.</value>
        int? SubtotalHandlingModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal discounts modifier.</summary>
        /// <value>The subtotal discounts modifier.</value>
        decimal? SubtotalDiscountsModifier { get; set; }

        /// <summary>Gets or sets the subtotal discounts modifier mode.</summary>
        /// <value>The subtotal discounts modifier mode.</value>
        int? SubtotalDiscountsModifierMode { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the shipment.</summary>
        /// <value>The identifier of the shipment.</value>
        int? ShipmentID { get; set; }

        /// <summary>Gets or sets the shipment.</summary>
        /// <value>The shipment.</value>
        Shipment? Shipment { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Shopping", "Cart")]
    public class Cart
        : SalesCollectionBase<Cart,
              CartStatus,
              CartType,
              CartItem,
              AppliedCartDiscount,
              CartState,
              CartFile,
              CartContact,
              CartEvent,
              CartEventType>,
            ICart
    {
        private ICollection<Note>? notes;

        public Cart()
        {
            // HaveNotesBase
            notes = new HashSet<Note>();
        }

        #region Overrides Added for the Cluster Index
        /// <inheritdoc/>
        [DefaultValue(true),
         Index, Index("Unq_ByCartClusterRequirements", IsUnique = true, IsClustered = false, Order = 0)]
        public override bool Active { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(null),
         Index, Index("Unq_ByCartClusterRequirements", IsUnique = true, IsClustered = false, Order = 1)]
        public Guid? SessionID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0),
         Index, Index("Unq_ByCartClusterRequirements", IsUnique = true, IsClustered = false, Order = 2)]
        public override int TypeID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(null),
         Index, Index("Unq_ByCartClusterRequirements", IsUnique = true, IsClustered = false, Order = 3)]
        public override int? UserID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Account)), DefaultValue(null),
         Index, Index("Unq_ByCartClusterRequirements", IsUnique = true, IsClustered = false, Order = 4)]
        public override int? AccountID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null),
         Index, Index("Unq_ByCartClusterRequirements", IsUnique = true, IsClustered = false, Order = 5)]
        public override int? BrandID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Franchise)), DefaultValue(null),
         Index, Index("Unq_ByCartClusterRequirements", IsUnique = true, IsClustered = false, Order = 6)]
        public override int? FranchiseID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null),
         Index, Index("Unq_ByCartClusterRequirements", IsUnique = true, IsClustered = false, Order = 7)]
        public override int? StoreID { get; set; }
        #endregion

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region Cart Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? SubtotalShippingModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SubtotalShippingModifierMode { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? SubtotalTaxesModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SubtotalTaxesModifierMode { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? SubtotalFeesModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SubtotalFeesModifierMode { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? SubtotalHandlingModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SubtotalHandlingModifierMode { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? SubtotalDiscountsModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SubtotalDiscountsModifierMode { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        [DefaultValue(null)]
        public DateTime? RequestedShipDate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Shipment)), DefaultValue(null)]
        public int? ShipmentID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Shipment? Shipment { get; set; }
        #endregion
    }
}
