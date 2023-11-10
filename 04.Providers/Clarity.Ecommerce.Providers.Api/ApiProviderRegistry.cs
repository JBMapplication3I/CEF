// <copyright file="ApiProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the API Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Api
{
    using Basic;
    using Interfaces.Providers.Api;
    using Lamar;

    /// <summary>The API provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class ApiProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="ApiProviderRegistry"/> class.</summary>
        public ApiProviderRegistry()
        {
            Use<BasicApiProvider>().Singleton().For<IApiProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Api
{
    using Basic;
    using Interfaces.Providers.Api;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The API provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class ApiProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="ApiProviderRegistry"/> class.</summary>
        public ApiProviderRegistry()
        {
            For<IApiProviderBase>(new SingletonLifecycle()).Add<BasicApiProvider>();
        }
    }
}
#endif
