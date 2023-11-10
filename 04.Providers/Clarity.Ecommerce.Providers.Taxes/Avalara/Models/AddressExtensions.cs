// <copyright file="AddressExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address extensions class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>The address extensions.</summary>
    public static class AddressExtensions
    {
        /// <summary>An IAddressModel extension method that converts this AddressExtensions to an avalara address.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="address">    The address to act on.</param>
        /// <param name="addressCode">The address code.</param>
        /// <returns>The given data converted to the Address.</returns>
        public static Address ToAvalaraAddress(this IAddressModel? address, string addressCode)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }
            return new()
            {
                AddressCode = addressCode,
                Line1 = address.Street1,
                Line2 = address.Street2,
                City = address.City,
                Region = address.RegionCode ?? address.Region?.Code,
                Country = address.CountryCode == "USA" ? "US" : address.CountryCode,
                PostalCode = address.PostalCode,
            };
        }

        /// <summary>An IAddressModel extension method that query if 'addressModel' is valid full.</summary>
        /// <param name="addressModel">The addressModel to act on.</param>
        /// <returns>True if valid full, false if not.</returns>
        public static bool IsValidFull(this IAddressModel? addressModel)
        {
            if (string.IsNullOrWhiteSpace(addressModel?.City))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(addressModel!.Street1))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(addressModel.PostalCode))
            {
                return false;
            }
            ////if (string.IsNullOrWhiteSpace(addressModel.RegionName)) { return false; }
            return !string.IsNullOrWhiteSpace(addressModel.CountryCode);
        }
    }
}
