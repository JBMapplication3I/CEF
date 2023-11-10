// <copyright file="PersonalizationProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Personalization provider registry class</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Personalization
{
    using Interfaces.Providers.Personalization;
    using Lamar;
    using RecommendByCategory;

    /// <summary>The Personalization provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class PersonalizationProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="PersonalizationProviderRegistry"/> class.</summary>
        public PersonalizationProviderRegistry()
        {
            if (RecommendProductsByCategoriesOfProductsPreviouslyViewedPersonalizationProviderConfig.IsValid(false))
            {
                Use<RecommendProductsByCategoriesOfProductsPreviouslyViewedPersonalizationProvider>().Singleton().For<IPersonalizationProviderBase>();
            }
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Personalization
{
    using Interfaces.Providers.Personalization;
    using RecommendByCategory;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The Personalization provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class PersonalizationProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="PersonalizationProviderRegistry"/> class.</summary>
        public PersonalizationProviderRegistry()
        {
            if (RecommendProductsByCategoriesOfProductsPreviouslyViewedPersonalizationProviderConfig.IsValid(false))
            {
                For<IPersonalizationProviderBase>(new SingletonLifecycle()).Add<RecommendProductsByCategoriesOfProductsPreviouslyViewedPersonalizationProvider>();
            }
        }
    }
}
#endif
