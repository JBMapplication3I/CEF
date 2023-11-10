// <copyright file="IRateQuoteModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRateQuoteModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for rate quote model.</summary>
    public partial interface IRateQuoteModel
    {
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

        #region Related Objects
        /// <summary>Gets or sets the identifier of the ship carrier method.</summary>
        /// <value>The identifier of the ship carrier method.</value>
        int ShipCarrierMethodID { get; set; }

        /// <summary>Gets or sets the ship carrier method key.</summary>
        /// <value>The ship carrier method key.</value>
        string? ShipCarrierMethodKey { get; set; }

        /// <summary>Gets or sets the name of the ship carrier method.</summary>
        /// <value>The name of the ship carrier method.</value>
        string? ShipCarrierMethodName { get; set; }

        /// <summary>Gets or sets the ship carrier method.</summary>
        /// <value>The ship carrier method.</value>
        IShipCarrierMethodModel? ShipCarrierMethod { get; set; }

        /// <summary>Gets or sets the identifier of the ship carrier.</summary>
        /// <value>The identifier of the ship carrier.</value>
        int ShipCarrierID { get; set; }

        /// <summary>Gets or sets the ship carrier key.</summary>
        /// <value>The ship carrier key.</value>
        string? ShipCarrierKey { get; set; }

        /// <summary>Gets or sets the name of the ship carrier.</summary>
        /// <value>The name of the ship carrier.</value>
        string? ShipCarrierName { get; set; }

        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        int? CartID { get; set; }

        /// <summary>Gets or sets the identifier of the sample request.</summary>
        /// <value>The identifier of the sample request.</value>
        int? SampleRequestID { get; set; }

        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int? SalesOrderID { get; set; }

        /// <summary>Gets or sets the identifier of the sales invoice.</summary>
        /// <value>The identifier of the sales invoice.</value>
        int? SalesInvoiceID { get; set; }

        /// <summary>Gets or sets the identifier of the sales quote.</summary>
        /// <value>The identifier of the sales quote.</value>
        int? SalesQuoteID { get; set; }

        /// <summary>Gets or sets the identifier of the sales return.</summary>
        /// <value>The identifier of the sales return.</value>
        int? SalesReturnID { get; set; }

        /// <summary>Gets or sets the identifier of the purchase order.</summary>
        /// <value>The identifier of the purchase order.</value>
        int? PurchaseOrderID { get; set; }
        #endregion
    }
}
