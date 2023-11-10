// <copyright file="ElasticSearchingFeature.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching feature class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using JetBrains.Annotations;
    using ServiceStack;

    /// <summary>An elastic searching feature.</summary>
    /// <seealso cref="IPlugin"/>
    [PublicAPI]
    public class ElasticSearchingFeature : IPlugin
    {
        /// <summary>Registers this ElasticSearchingFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
