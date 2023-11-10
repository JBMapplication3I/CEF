// <copyright file="RateByAttributesShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Rate By Attributes shipping provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.RateByAttributes
{
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;

    /// <summary>A Rate By Attributes shipping provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class RateByAttributesShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="RateByAttributesShippingProviderConfig" /> class.</summary>
        static RateByAttributesShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(RateByAttributesShippingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<RateByAttributesShippingProvider>() || isDefaultAndActivated;
    }
}
