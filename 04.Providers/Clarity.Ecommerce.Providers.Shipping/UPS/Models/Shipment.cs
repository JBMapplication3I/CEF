// <copyright file="Shipment.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.UPS.Models
{
    using System;

    /// <summary>A shipment.</summary>
    public class Shipment
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }

        /// <summary>Gets or sets the name of the subscription event.</summary>
        /// <value>The name of the subscription event.</value>
        public string? SubscriptionEventName { get; set; }

        /// <summary>Gets or sets the subscription event number.</summary>
        /// <value>The subscription event number.</value>
        public string? SubscriptionEventNumber { get; set; }

        /// <summary>Gets or sets the filename of the subscription file.</summary>
        /// <value>The filename of the subscription file.</value>
        public string? SubscriptionFileName { get; set; }

        /// <summary>Gets or sets the package reference number.</summary>
        /// <value>The package reference number.</value>
        public string? PackageReferenceNumber { get; set; }

        /// <summary>Gets or sets the shipment reference number.</summary>
        /// <value>The shipment reference number.</value>
        public string? ShipmentReferenceNumber { get; set; }

        /// <summary>Gets or sets the shipper number.</summary>
        /// <value>The shipper number.</value>
        public string? ShipperNumber { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        public string? TrackingNumber { get; set; }

        /// <summary>Gets or sets the date time.</summary>
        /// <value>The date time.</value>
        public DateTime DateTime { get; set; }
    }
}
