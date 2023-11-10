// <copyright file="ValidateAddressRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the validate address request class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.Avalara.Models
{
    using Interfaces.Models;
    using JetBrains.Annotations;

    /// <summary>A validate address request.</summary>
    [PublicAPI]
    public class ValidateAddressRequest
    {
        /// <summary>Initializes a new instance of the <see cref="ValidateAddressRequest"/> class.</summary>
        /// <param name="address">The address.</param>
        public ValidateAddressRequest(IAddressModel address)
        {
            Line1 = address.Street1;
            Line2 = address.Street2;
            Line3 = address.Street3;
            City = address.City;
            Region = address.RegionCode;
            Country = address.CountryCode;
            PostalCode = address.PostalCode;
        }

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

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        public string? Country { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        public string? PostalCode { get; set; }
    }
}
