// <copyright file="Feature.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice handlers feature class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers
{
    using ServiceStack;

    /// <summary>A sales invoice handler feature.</summary>
    /// <seealso cref="IPlugin"/>
    [JetBrains.Annotations.PublicAPI]
    public class SalesInvoiceHandlersFeature : IPlugin
    {
        /// <summary>Registers this SalesInvoiceHandlersFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
