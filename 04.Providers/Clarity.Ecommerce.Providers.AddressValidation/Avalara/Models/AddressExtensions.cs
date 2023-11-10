// <copyright file="AddressExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address extensions class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.Avalara.Models
{
    using Interfaces.Models;
    using Utilities;

    /// <summary>The address extensions.</summary>
    public static class AddressExtensions
    {
        /// <summary>An <seealso cref="IAddressModel" /> extension method that converts the address to an Avalara address.</summary>
        /// <param name="address">The address to act on.</param>
        /// <returns>address as the Address.</returns>
        public static Address ToAvalaraAddress(this IAddressModel address)
        {
            Contract.RequiresNotNull(address);
            return new()
            {
                AddressCode = "002", ////address.ID?.ToString() ?? "02",
                Line1 = address.Street1,
                Line2 = address.Street2,
                Line3 = address.Street3,
                City = address.City,
                Region = address.RegionCode,
                Country = address.CountryCode == "USA" ? "US" : address.CountryCode,
                PostalCode = address.PostalCode,
            };
        }
    }
}
