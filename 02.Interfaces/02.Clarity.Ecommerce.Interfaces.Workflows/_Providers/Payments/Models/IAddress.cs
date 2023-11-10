// <copyright file="IAddress.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAddress interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    /// <summary>Interface for address model.</summary>
    public interface IAddress
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }

        /// <summary>Gets or sets the street address.</summary>
        /// <value>The street address.</value>
        string? StreetAddress { get; set; }

        /// <summary>Gets or sets the street address 2.</summary>
        /// <value>The street address 2.</value>
        string? StreetAddress2 { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        string? City { get; set; }

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        string? State { get; set; }

        /// <summary>Gets or sets the zip.</summary>
        /// <value>The zip.</value>
        string? Zip { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        string? Country { get; set; }
    }
}
