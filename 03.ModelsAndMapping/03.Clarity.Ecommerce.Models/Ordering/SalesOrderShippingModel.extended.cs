// <copyright file="SalesOrderShippingModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order shipping model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the sales order shipping.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="ISalesOrderShippingModel"/>
    public class SalesOrderShippingModel : BaseModel, ISalesOrderShippingModel
    {
        /// <inheritdoc/>
        public string? CarrierName { get; set; }

        /// <inheritdoc/>
        public int? CarrierID { get; set; }

        /// <inheritdoc/>
        public string? CarrierMethodName { get; set; }

        /// <inheritdoc/>
        public int? CarrierMethodID { get; set; }

        /// <inheritdoc/>
        public string? TrackingNumber { get; set; }

        /// <inheritdoc/>
        public DateTime? EstimatedDeliveryDate { get; set; }

        /// <inheritdoc/>
        public DateTime? ArrivalDate { get; set; }
    }
}
