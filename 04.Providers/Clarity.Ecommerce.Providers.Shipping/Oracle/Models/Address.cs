// <copyright file="Address.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Oracle.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>An address.</summary>
    public class Address
    {
        /// <summary>Gets or sets the street 1.</summary>
        /// <value>The street 1.</value>
        public string? Street1 { get; set; }

        /// <summary>Gets or sets the street 2.</summary>
        /// <value>The street 2.</value>
        public string? Street2 { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        public string? City { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        public string? PostalCode { get; set; }

        /// <summary>Gets or sets the name of the country.</summary>
        /// <value>The name of the country.</value>
        public string? CountryName { get; set; }

        /// <summary>Gets or sets the name of the region.</summary>
        /// <value>The name of the region.</value>
        public string? RegionName { get; set; }

        /// <summary>Gets or sets a value indicating whether the active.</summary>
        /// <value>True if active, false if not.</value>
        public bool Active { get; set; }

        /// <summary>Gets or sets the created date.</summary>
        /// <value>The created date.</value>
        public DateTime? CreatedDate { get; set; }

        /// <summary>Gets or sets the serializable attributes.</summary>
        /// <value>The serializable attributes.</value>
        public SerializableAttributesDictionary? SerializableAttributes { get; set; }
    }
}
