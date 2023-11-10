// <copyright file="CacheProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Cache Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Caching
{
    using Interfaces.Providers.Caching;
    using Lamar;
    using Redis;

    /// <summary>The Cache provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class CacheProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="CacheProviderRegistry"/> class.</summary>
        public CacheProviderRegistry()
        {
            // Assign default
            Use<RedisCacheProvider>().Singleton().For<ICacheProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Caching
{
    using Interfaces.Providers.Caching;
    using Redis;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The Cache provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class CacheProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="CacheProviderRegistry"/> class.</summary>
        public CacheProviderRegistry()
        {
            // Assign default
            For<ICacheProviderBase>(new SingletonLifecycle()).Add<RedisCacheProvider>();
        }
    }
}
#endif
