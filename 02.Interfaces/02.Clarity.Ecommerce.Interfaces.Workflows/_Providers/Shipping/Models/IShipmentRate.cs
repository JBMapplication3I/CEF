// <copyright file="IShipmentRate.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IShipmentRate interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Shipping
{
    using System;

    /// <summary>Interface for shipment rate.</summary>
    public interface IShipmentRate
    {
        /// <summary>Gets or sets the name of the carrier.</summary>
        /// <value>The name of the carrier.</value>
        string? CarrierName { get; set; }

        /// <summary>Gets or sets the option code.</summary>
        /// <value>The option code.</value>
        string? OptionCode { get; set; }

        /// <summary>Gets or sets the name of the option.</summary>
        /// <value>The name of the option.</value>
        string? OptionName { get; set; }

        /// <summary>Gets or sets the name of the full option.</summary>
        /// <value>The name of the full option.</value>
        string? FullOptionName { get; set; }

        /// <summary>Gets or sets the Date/Time of the estimated arrival.</summary>
        /// <value>The estimated arrival.</value>
        DateTime? EstimatedArrival { get; set; }

        /// <summary>Gets or sets the Date/Time of the estimated arrival maximum.</summary>
        /// <value>The estimated arrival maximum.</value>
        DateTime? EstimatedArrivalMax { get; set; }

        /// <summary>Gets or sets the Date/Time of the target shipping date.</summary>
        /// <value>The target shipping date.</value>
        DateTime? TargetShipping { get; set; }

        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        /// <summary>Gets or sets the applied accessorials.</summary>
        /// <value>The applied accessorials.</value>
        string[]? AppliedAccessorials { get; set; }

        /// <summary>Gets or sets the delivery day of week.</summary>
        /// <value>The delivery day of week.</value>
        string? DeliveryDayOfWeek { get; set; }

        /// <summary>Gets or sets the signature option.</summary>
        /// <value>The signature option.</value>
        string? SignatureOption { get; set; }
    }
}
