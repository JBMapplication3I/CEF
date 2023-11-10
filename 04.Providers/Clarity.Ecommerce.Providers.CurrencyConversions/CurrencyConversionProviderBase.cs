// <copyright file="CurrencyConversionProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the currency conversion provider base class</summary>
namespace Clarity.Ecommerce.Providers.CurrencyConversions
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.CurrencyConversions;

    /// <summary>A currency conversion provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ICurrencyConversionsProviderBase"/>
    public abstract class CurrencyConversionProviderBase : ProviderBase, ICurrencyConversionsProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.CurrencyConversions;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<double> ConvertAsync(
            string keyA, string keyB, double value, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<double> ConvertAsync(
            string keyA, string keyB, double value, DateTime onDate, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<double> ConvertAsync(
            IProductModel product, IAccountModel account, double value, string? contextProfileName);
    }
}
