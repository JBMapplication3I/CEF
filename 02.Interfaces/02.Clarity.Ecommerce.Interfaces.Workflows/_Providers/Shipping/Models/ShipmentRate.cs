// <copyright file="ShipmentRate.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment rate class</summary>
namespace Clarity.Ecommerce.Providers.Shipping
{
    using System;
    using Interfaces.Providers.Shipping;

    /// <summary>A shipment rate.</summary>
    /// <seealso cref="IShipmentRate"/>
    public class ShipmentRate : IShipmentRate
    {
        /// <inheritdoc/>
        public string? CarrierName { get; set; }

        /// <inheritdoc/>
        public string? FullOptionName { get; set; }

        /// <inheritdoc/>
        public string? OptionCode { get; set; }

        /// <inheritdoc/>
        public string? OptionName { get; set; }

        /// <inheritdoc/>
        public DateTime? EstimatedArrival { get; set; }

        /// <inheritdoc/>
        public DateTime? EstimatedArrivalMax { get; set; }

        /// <inheritdoc/>
        public DateTime? TargetShipping { get; set; }

        /// <inheritdoc/>
        public decimal Rate { get; set; }

        /// <inheritdoc/>
        public string[]? AppliedAccessorials { get; set; }

        /// <inheritdoc/>
        public string? DeliveryDayOfWeek { get; set; }

        /// <inheritdoc/>
        public string? SignatureOption { get; set; }
    }
}
