// <copyright file="Feature.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample request handlers feature class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers
{
    using ServiceStack;

    /// <summary>A sample request handler feature.</summary>
    /// <seealso cref="IPlugin"/>
    [JetBrains.Annotations.PublicAPI]
    public class SampleRequestHandlersFeature : IPlugin
    {
        /// <summary>Registers this SampleRequestHandlersFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
