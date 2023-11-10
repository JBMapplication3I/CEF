// <copyright file="PricingFactorySettings.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pricing factory settings class</summary>
namespace Clarity.Ecommerce.Providers.Pricing
{
    using Interfaces.Providers.Pricing;
    using JSConfigs;
    using Utilities;

    /// <summary>A pricing factory settings.</summary>
    /// <seealso cref="IPricingFactorySettings"/>
    public class PricingFactorySettings : IPricingFactorySettings
    {
        /// <summary>The default price point key.</summary>
        private string? defaultPricePointKey;

        /// <summary>The default unit of measure.</summary>
        private string? defaultUnitOfMeasure;

        /// <summary>The default currency key.</summary>
        private string? defaultCurrencyKey;

        /// <summary>The default markup rate.</summary>
        private decimal? defaultMarkupRate;

        /// <inheritdoc/>
        public virtual string DefaultPricePointKey => (Contract.CheckValidKey(defaultPricePointKey)
            ? defaultPricePointKey
            : defaultPricePointKey = CEFConfigDictionary.PricingProviderTieredDefaultPricePointKey)!;

        /// <inheritdoc/>
        public virtual string DefaultUnitOfMeasure => (Contract.CheckValidKey(defaultUnitOfMeasure)
            ? defaultUnitOfMeasure
            : defaultUnitOfMeasure = CEFConfigDictionary.PricingProviderTieredDefaultUnitOfMeasure)!;

        /// <inheritdoc/>
        public virtual string DefaultCurrencyKey => (Contract.CheckValidKey(defaultCurrencyKey)
            ? defaultCurrencyKey
            : defaultCurrencyKey = CEFConfigDictionary.PricingProviderTieredDefaultCurrencyKey)!;

        /// <inheritdoc/>
        public virtual decimal DefaultMarkupRate
        {
            get
            {
                if (defaultMarkupRate.HasValue)
                {
                    return defaultMarkupRate.Value;
                }
                if (CEFConfigDictionary.PricingProviderTieredDefaultMarkupRate != 0m)
                {
                    defaultMarkupRate = CEFConfigDictionary.PricingProviderTieredDefaultMarkupRate;
                }
                if (defaultMarkupRate is null or < 0)
                {
                    defaultMarkupRate = 0m;
                }
                return defaultMarkupRate.Value;
            }
        }
    }
}
