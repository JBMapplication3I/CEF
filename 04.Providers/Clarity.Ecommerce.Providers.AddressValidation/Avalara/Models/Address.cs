// <copyright file="Address.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address class</summary>
#pragma warning disable 1591
namespace Clarity.Ecommerce.Providers.AddressValidation.Avalara.Models
{
    using System;
    using JetBrains.Annotations;

    /// <summary>An address.</summary>
    [PublicAPI, Serializable]
    public class Address
    {
        /// <summary>Address can be determined for tax calculation by Line1, City, Region, PostalCode, Country OR
        /// Latitude/Longitude OR TaxRegionId.</summary>
        /// <value>The address code.</value>
        public string? AddressCode { get; set; } // Input for GetTax only, not by address validation

        /// <summary>Gets or sets the line 1.</summary>
        /// <value>The line 1.</value>
        public string? Line1 { get; set; }

        /// <summary>Gets or sets the line 2.</summary>
        /// <value>The line 2.</value>
        public string? Line2 { get; set; }

        /// <summary>Gets or sets the line 3.</summary>
        /// <value>The line 3.</value>
        public string? Line3 { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        public string? City { get; set; }

        /// <summary>Gets or sets the region.</summary>
        /// <value>The region.</value>
        public string? Region { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        public string? PostalCode { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        public string? Country { get; set; }

        /// <summary>Gets or sets the county.</summary>
        /// <value>The county.</value>
        public string? County { get; set; } // Output for ValidateAddress only

        /// <summary>Gets or sets the fips code.</summary>
        /// <value>The fips code.</value>
        public string? FipsCode { get; set; } // Output for ValidateAddress only

        /// <summary>Gets or sets the carrier route.</summary>
        /// <value>The carrier route.</value>
        public string? CarrierRoute { get; set; } // Output for ValidateAddress only

        /// <summary>Gets or sets the post net.</summary>
        /// <value>The post net.</value>
        public string? PostNet { get; set; } // Output for ValidateAddress only

        /// <summary>Gets or sets the type of the address.</summary>
        /// <value>The type of the address.</value>
        public AddressType? AddressType { get; set; } // Output for ValidateAddress only

        /// <summary>Gets or sets the latitude.</summary>
        /// <value>The latitude.</value>
        public decimal? Latitude { get; set; } // Input for GetTax only

        /// <summary>Gets or sets the longitude.</summary>
        /// <value>The longitude.</value>
        public decimal? Longitude { get; set; } // Input for GetTax only

        /// <summary>Gets or sets the identifier of the tax region.</summary>
        /// <value>The identifier of the tax region.</value>
        public string? TaxRegionId { get; set; } // Input for GetTax only
    }
}
