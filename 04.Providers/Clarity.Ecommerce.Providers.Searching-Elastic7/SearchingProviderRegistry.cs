// <copyright file="SearchingProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Searching provider registry class</summary>
namespace Clarity.Ecommerce.Providers.Searching
{
    using ElasticSearch;
    using Interfaces.Providers.Searching;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The Searching provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SearchingProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="SearchingProviderRegistry"/> class.</summary>
        public SearchingProviderRegistry()
        {
            For<ISearchingProviderBase>(new SingletonLifecycle()).Add<ElasticSearchingProvider>();
        }
    }
}
