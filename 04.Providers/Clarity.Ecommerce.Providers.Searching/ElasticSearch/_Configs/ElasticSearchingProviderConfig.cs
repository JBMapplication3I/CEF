// <copyright file="ElasticSearchingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;

    /// <summary>An elastic searching provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static partial class ElasticSearchingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="ElasticSearchingProviderConfig" /> class.</summary>
        static ElasticSearchingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
        {
            return ProviderConfig.CheckIsEnabledBySettings<ElasticSearchingProvider>() || isDefaultAndActivated;
        }
    }
}
