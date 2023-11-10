// <copyright file="TaxAddress.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax address class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;

    /// <summary>(Serializable)a tax address.</summary>
    [Serializable]
    public class TaxAddress // Result object
    {
        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        public string? Address { get; set; }

        /// <summary>Gets or sets the address code.</summary>
        /// <value>The address code.</value>
        public string? AddressCode { get; set; }

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

        /// <summary>Gets or sets the latitude.</summary>
        /// <value>The latitude.</value>
        public decimal Latitude { get; set; }

        /// <summary>Gets or sets the longitude.</summary>
        /// <value>The longitude.</value>
        public decimal Longitude { get; set; }

        /// <summary>Gets or sets the identifier of the tax region.</summary>
        /// <value>The identifier of the tax region.</value>
        public string? TaxRegionId { get; set; }

        /// <summary>Gets or sets the juris code.</summary>
        /// <value>The juris code.</value>
        public string? JurisCode { get; set; }
    }
}
