// <copyright file="ICurrencyConversionsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICurrencyConversionsProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.CurrencyConversions
{
    using System;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for currency conversions provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface ICurrencyConversionsProviderBase : IProviderBase
    {
        /// <summary>Converts a decimal value from a starting currency to a new currency.</summary>
        /// <param name="keyA">              The starting currency key.</param>
        /// <param name="keyB">              The ending currency key.</param>
        /// <param name="value">             The value.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The converted currency value.</returns>
        Task<double> ConvertAsync(string keyA, string keyB, double value, string? contextProfileName);

        /// <summary>Converts a decimal value from a starting currency to a new currency.</summary>
        /// <param name="keyA">              The starting currency key.</param>
        /// <param name="keyB">              The ending currency key.</param>
        /// <param name="value">             The value.</param>
        /// <param name="onDate">            The on date.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The converted currency value.</returns>
        Task<double> ConvertAsync(string keyA, string keyB, double value, DateTime onDate, string? contextProfileName);

        /// <summary>Converts the cost of a product to a Ticket Value based on Product and Account Pricing Rules.</summary>
        /// <param name="product">           The product.</param>
        /// <param name="account">           The account.</param>
        /// <param name="value">             The value.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The converted currency value.</returns>
        Task<double> ConvertAsync(IProductModel product, IAccountModel account, double value, string? contextProfileName);
    }
}
