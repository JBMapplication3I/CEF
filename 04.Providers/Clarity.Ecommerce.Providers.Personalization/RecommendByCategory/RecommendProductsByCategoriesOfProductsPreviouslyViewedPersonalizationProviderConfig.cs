﻿// <copyright file="RecommendProductsByCategoriesOfProductsPreviouslyViewedPersonalizationProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the recommend products by categories of products previously viewed personalization provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Personalization.RecommendByCategory
{
    using Interfaces.Providers;

    /// <summary>A recommend products by categories of products previously viewed personalization provider
    /// configuration.</summary>
    internal static class RecommendProductsByCategoriesOfProductsPreviouslyViewedPersonalizationProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<RecommendProductsByCategoriesOfProductsPreviouslyViewedPersonalizationProvider>() || isDefaultAndActivated;
    }
}
