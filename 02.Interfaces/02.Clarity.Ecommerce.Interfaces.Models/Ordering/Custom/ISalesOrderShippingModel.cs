// <copyright file="ISalesOrderShippingModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesOrderShippingModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for sales order shipping model.</summary>
    public interface ISalesOrderShippingModel : IBaseModel
    {
        /// <summary>Gets or sets the name of the carrier.</summary>
        /// <value>The name of the carrier.</value>
        string? CarrierName { get; set; }

        /// <summary>Gets or sets the identifier of the carrier.</summary>
        /// <value>The identifier of the carrier.</value>
        int? CarrierID { get; set; }

        /// <summary>Gets or sets the name of the carrier method.</summary>
        /// <value>The name of the carrier method.</value>
        string? CarrierMethodName { get; set; }

        /// <summary>Gets or sets the identifier of the carrier method.</summary>
        /// <value>The identifier of the carrier method.</value>
        int? CarrierMethodID { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        string? TrackingNumber { get; set; }

        /// <summary>Gets or sets the estimated delivery date.</summary>
        /// <value>The estimated delivery date.</value>
        System.DateTime? EstimatedDeliveryDate { get; set; }

        /// <summary>Gets or sets the arrival date.</summary>
        /// <value>The arrival date.</value>
        System.DateTime? ArrivalDate { get; set; }
    }
}
