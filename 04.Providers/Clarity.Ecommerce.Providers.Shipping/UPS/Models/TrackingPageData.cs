// <copyright file="TrackingPageData.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tracking page data class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.UPS.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>A tracking page data.</summary>
    [JetBrains.Annotations.PublicAPI]
    public class TrackingPageData
    {
        /// <summary>Gets or sets the name of the product.</summary>
        /// <value>The name of the product.</value>
        public string? ProductName { get; set; }

        /// <summary>Gets or sets the product code.</summary>
        /// <value>The product code.</value>
        public string? ProductCode { get; set; }

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }

        /// <summary>Gets or sets the sell price.</summary>
        /// <value>The sell price.</value>
        public decimal SellPrice { get; set; }

        /// <summary>Gets or sets the order date.</summary>
        /// <value>The order date.</value>
        public DateTime OrderDate { get; set; }

        /// <summary>Gets or sets the order number.</summary>
        /// <value>The order number.</value>
        public string? OrderNumber { get; set; }

        /// <summary>Gets or sets the carrier.</summary>
        /// <value>The carrier.</value>
        public string? Carrier { get; set; }

        /// <summary>Gets or sets the carrier email.</summary>
        /// <value>The carrier email.</value>
        public string? CarrierEmail { get; set; }

        /// <summary>Gets or sets the delivery method.</summary>
        /// <value>The delivery method.</value>
        public string? DeliveryMethod { get; set; }

        /// <summary>Gets or sets the published rate.</summary>
        /// <value>The published rate.</value>
        public decimal PublishedRate { get; set; }

        /// <summary>Gets or sets the negotiated rate.</summary>
        /// <value>The negotiated rate.</value>
        public decimal NegotiatedRate { get; set; }

        /// <summary>Gets or sets the estimated delivery date.</summary>
        /// <value>The estimated delivery date.</value>
        public DateTime? EstimatedDeliveryDate { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        public string? TrackingNumber { get; set; }

        /// <summary>Gets or sets the name of the shipper.</summary>
        /// <value>The name of the shipper.</value>
        public string? ShipperName { get; set; }

        /// <summary>Gets or sets the shipper address.</summary>
        /// <value>The shipper address.</value>
        public string? ShipperAddress { get; set; }

        /// <summary>Gets or sets the shipper city state zip.</summary>
        /// <value>The shipper city state zip.</value>
        public string? ShipperCityStateZip { get; set; }

        /// <summary>Gets or sets the shipper phone.</summary>
        /// <value>The shipper phone.</value>
        public string? ShipperPhone { get; set; }

        /// <summary>Gets or sets the shipper email.</summary>
        /// <value>The shipper email.</value>
        public string? ShipperEmail { get; set; }

        /// <summary>Gets or sets the name of the ship to.</summary>
        /// <value>The name of the ship to.</value>
        public string? ShipToName { get; set; }

        /// <summary>Gets or sets the ship to address.</summary>
        /// <value>The ship to address.</value>
        public string? ShipToAddress { get; set; }

        /// <summary>Gets or sets the ship to city state zip.</summary>
        /// <value>The ship to city state zip.</value>
        public string? ShipToCityStateZip { get; set; }

        /// <summary>Gets or sets the ship to phone.</summary>
        /// <value>The ship to phone.</value>
        public string? ShipToPhone { get; set; }

        /// <summary>Gets or sets the ship to email.</summary>
        /// <value>The ship to email.</value>
        public string? ShipToEmail { get; set; }

        /// <summary>Gets or sets the progress.</summary>
        /// <value>The progress.</value>
        public List<TrackingPageProgress>? Progress { get; set; }

        /// <summary>Gets or sets the reference number 1.</summary>
        /// <value>The reference number 1.</value>
        public string? ReferenceNumber1 { get; set; }

        /// <summary>Gets or sets the reference number 2.</summary>
        /// <value>The reference number 2.</value>
        public string? ReferenceNumber2 { get; set; }

        /// <summary>Gets or sets the reference number 3.</summary>
        /// <value>The reference number 3.</value>
        public string? ReferenceNumber3 { get; set; }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public string? Status { get; set; }

        /// <summary>Gets or sets the status raw.</summary>
        /// <value>The status raw.</value>
        public string? StatusRaw { get; set; }

        /// <summary>Gets or sets the ship date.</summary>
        /// <value>The ship date.</value>
        public DateTime? ShipDate { get; set; }

        /// <summary>Gets or sets the last point of scan.</summary>
        /// <value>The last point of scan.</value>
        public string? LastPointOfScan { get; set; }

        /// <summary>Gets or sets the Destination for the.</summary>
        /// <value>The destination.</value>
        public string? Destination { get; set; }

        /// <summary>Gets or sets the company.</summary>
        /// <value>The company.</value>
        public string? Company { get; set; }

        /// <summary>Gets or sets the identifier of the carrier.</summary>
        /// <value>The identifier of the carrier.</value>
        public int CarrierID { get; set; }

        /// <summary>Gets or sets the name of the event.</summary>
        /// <value>The name of the event.</value>
        public string? EventName { get; set; }

        /// <summary>Gets or sets a value indicating whether this TrackingPageData is variance.</summary>
        /// <value>True if this TrackingPageData is variance, false if not.</value>
        public bool IsVariance { get; set; }

        /// <summary>Gets or sets a value indicating whether this TrackingPageData is delayed.</summary>
        /// <value>True if this TrackingPageData is delayed, false if not.</value>
        public bool IsDelayed { get; set; }

        /// <summary>Gets or sets the Date/Time of the tracking last update.</summary>
        /// <value>The tracking last update.</value>
        public DateTime? TrackingLastUpdate { get; set; }

        /// <summary>Gets or sets a value indicating whether the tracking cached.</summary>
        /// <value>True if tracking cached, false if not.</value>
        public bool TrackingCached { get; set; }

        /// <summary>Gets or sets the tracking original estimated delivery date.</summary>
        /// <value>The tracking original estimated delivery date.</value>
        public DateTime? TrackingOriginalEstimatedDeliveryDate { get; set; }
    }
}
