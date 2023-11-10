// <copyright file="Feature.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return handlers feature class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers
{
    using ServiceStack;

    /// <summary>A sales return handler feature.</summary>
    /// <seealso cref="IPlugin"/>
    [JetBrains.Annotations.PublicAPI]
    public class SalesReturnHandlersFeature : IPlugin
    {
        /// <summary>Registers this SalesReturnHandlersFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
