// <copyright file="ProxyProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Proxy Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Proxy
{
    using ForwardProxy;
    using Interfaces.Providers.Proxy;
    using Lamar;
    using NoProxy;

    /// <summary>The Proxy provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class ProxyProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="ProxyProviderRegistry"/> class.</summary>
        public ProxyProviderRegistry()
        {
            if (ForwardProxyProviderConfig.IsValid(false))
            {
                Use<ForwardProxyProvider>().Singleton().For<IProxyProviderBase>();
                return;
            }
            Use<NoProxyProvider>().Singleton().For<IProxyProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Proxy
{
    using ForwardProxy;
    using Interfaces.Providers.Proxy;
    using NoProxy;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The Proxy provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class ProxyProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="ProxyProviderRegistry"/> class.</summary>
        public ProxyProviderRegistry()
        {
            if (ForwardProxyProviderConfig.IsValid(false))
            {
                For<IProxyProviderBase>(new SingletonLifecycle()).Add<ForwardProxyProvider>();
                return;
            }
            For<IProxyProviderBase>(new SingletonLifecycle()).Add<NoProxyProvider>();
        }
    }
}
#endif
