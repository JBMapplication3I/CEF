// <copyright file="Feature.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote handlers feature class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers
{
    using ServiceStack;

    /// <summary>A sales quote handler feature.</summary>
    /// <seealso cref="IPlugin"/>
    [JetBrains.Annotations.PublicAPI]
    public class SalesQuoteHandlersFeature : IPlugin
    {
        /// <summary>Registers this SalesQuoteHandlersFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
