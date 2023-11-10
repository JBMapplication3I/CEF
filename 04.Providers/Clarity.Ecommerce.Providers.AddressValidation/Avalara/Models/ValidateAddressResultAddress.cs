// <copyright file="ValidateAddressResultAddress.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the validate address result address class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.Avalara.Models
{
    using JetBrains.Annotations;

    /// <summary>A validate address result address.</summary>
    [PublicAPI]
    public class ValidateAddressResultAddress
    {
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

        /// <summary>Gets or sets the county.</summary>
        /// <value>The county.</value>
        public string? County { get; set; }

        /// <summary>Gets or sets the region.</summary>
        /// <value>The region.</value>
        public string? Region { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        public string? Country { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        public string? PostalCode { get; set; }

        /// <summary>Gets or sets the type of the address.</summary>
        /// <value>The type of the address.</value>
        public string? AddressType { get; set; }

        /// <summary>Gets or sets the fips code.</summary>
        /// <value>The fips code.</value>
        public string? FipsCode { get; set; }

        /// <summary>Gets or sets the carrier route.</summary>
        /// <value>The carrier route.</value>
        public string? CarrierRoute { get; set; }

        /// <summary>Gets or sets the post net.</summary>
        /// <value>The post net.</value>
        public string? PostNet { get; set; }
    }
}
