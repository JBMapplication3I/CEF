// <copyright file="ServiceRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the service registry class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using ServiceStack.Configuration;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>A service registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class ServiceRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="ServiceRegistry"/> class.</summary>
        public ServiceRegistry()
        {
            For<IAppSettings>(new SingletonLifecycle()).Use<AppSettings>();
        }
    }
}
