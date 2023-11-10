// <copyright file="RateQuote.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the rate quote class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IRateQuote : INameableBase
    {
        #region RateQuote Properties
        /// <summary>Gets or sets the estimated delivery date.</summary>
        /// <value>The estimated delivery date.</value>
        DateTime? EstimatedDeliveryDate { get; set; }

        /// <summary>Gets or sets the target shipping date.</summary>
        /// <value>The target shipping date.</value>
        DateTime? TargetShippingDate { get; set; }

        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal? Rate { get; set; }

        /// <summary>Gets or sets the cart hash.</summary>
        /// <value>The cart hash.</value>
        long? CartHash { get; set; }

        /// <summary>Gets or sets the Date/Time of the rate timestamp.</summary>
        /// <value>The rate timestamp.</value>
        DateTime? RateTimestamp { get; set; }

        /// <summary>Gets or sets a value indicating whether the selected.</summary>
        /// <value>True if selected, false if not.</value>
        bool Selected { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the ship carrier method.</summary>
        /// <value>The identifier of the ship carrier method.</value>
        int ShipCarrierMethodID { get; set; }

        /// <summary>Gets or sets the ship carrier method.</summary>
        /// <value>The ship carrier method.</value>
        ShipCarrierMethod? ShipCarrierMethod { get; set; }

        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        int? CartID { get; set; }

        /// <summary>Gets or sets the identifier of the sample request.</summary>
        /// <value>The identifier of the sample request.</value>
        int? SampleRequestID { get; set; }

        /// <summary>Gets or sets the identifier of the sales quote.</summary>
        /// <value>The identifier of the sales quote.</value>
        int? SalesQuoteID { get; set; }

        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int? SalesOrderID { get; set; }

        /// <summary>Gets or sets the identifier of the purchase order.</summary>
        /// <value>The identifier of the purchase order.</value>
        int? PurchaseOrderID { get; set; }

        /// <summary>Gets or sets the identifier of the sales invoice.</summary>
        /// <value>The identifier of the sales invoice.</value>
        int? SalesInvoiceID { get; set; }

        /// <summary>Gets or sets the identifier of the sales return.</summary>
        /// <value>The identifier of the sales return.</value>
        int? SalesReturnID { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Shipping", "RateQuote")]
    public class RateQuote : NameableBase, IRateQuote
    {
        #region RateQuote Properties
        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? EstimatedDeliveryDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? TargetShippingDate { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? Rate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public long? CartHash { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? RateTimestamp { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool Selected { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ShipCarrierMethod)), DefaultValue(0)]
        public int ShipCarrierMethodID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null)]
        public virtual ShipCarrierMethod? ShipCarrierMethod { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Cart)), DefaultValue(null)]
        public int? CartID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SampleRequest)), DefaultValue(null)]
        public int? SampleRequestID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesQuote)), DefaultValue(null)]
        public int? SalesQuoteID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesOrder)), DefaultValue(null)]
        public int? SalesOrderID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(PurchaseOrder)), DefaultValue(null)]
        public int? PurchaseOrderID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesInvoice)), DefaultValue(null)]
        public int? SalesInvoiceID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesReturn)), DefaultValue(null)]
        public int? SalesReturnID { get; set; }

        #region Don't map these
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Cart? Cart { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SampleRequest? SampleRequest { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesQuote? SalesQuote { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesOrder? SalesOrder { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual PurchaseOrder? PurchaseOrder { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesInvoice? SalesInvoice { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesReturn? SalesReturn { get; set; }
        #endregion
        #endregion
    }
}
