// <copyright file="RateQuoteModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the rate quote model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the rate quote.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IRateQuoteModel"/>
    public partial class RateQuoteModel
    {
        #region RateQuote Properties
        /// <inheritdoc/>
        public DateTime? EstimatedDeliveryDate { get; set; }

        /// <inheritdoc/>
        public DateTime? TargetShippingDate { get; set; }

        /// <inheritdoc/>
        public decimal? Rate { get; set; }

        /// <inheritdoc/>
        public long? CartHash { get; set; }

        /// <inheritdoc/>
        public DateTime? RateTimestamp { get; set; }

        /// <inheritdoc/>
        public bool Selected { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int ShipCarrierMethodID { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierMethodKey { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierMethodName { get; set; }

        /// <inheritdoc cref="IRateQuoteModel.ShipCarrierMethod "/>
        public ShipCarrierMethodModel? ShipCarrierMethod { get; set; }

        /// <inheritdoc/>
        IShipCarrierMethodModel? IRateQuoteModel.ShipCarrierMethod { get => ShipCarrierMethod; set => ShipCarrierMethod = (ShipCarrierMethodModel?)value; }

        /// <inheritdoc/>
        public int? CartID { get; set; }

        /// <inheritdoc/>
        public int? SampleRequestID { get; set; }

        /// <inheritdoc/>
        public int? SalesOrderID { get; set; }

        /// <inheritdoc/>
        public int? SalesQuoteID { get; set; }

        /// <inheritdoc/>
        public int? SalesInvoiceID { get; set; }

        /// <inheritdoc/>
        public int? SalesReturnID { get; set; }

        /// <inheritdoc/>
        public int? PurchaseOrderID { get; set; }
        #endregion

        #region Convenience Properties
        /// <inheritdoc/>
        public int ShipCarrierID { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierKey { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierName { get; set; }
        #endregion
    }
}
