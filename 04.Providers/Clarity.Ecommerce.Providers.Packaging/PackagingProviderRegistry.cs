// <copyright file="PackagingProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Packaging Provider StructureMap 4 Registry to associate the interfaces with
// their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Packaging
{
    using Basic;
    using DynamicsAx;
    using Interfaces.Providers;
    using Interfaces.Providers.Packaging;
    using Lamar;
    using Models;

    /// <summary>The Packaging provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class PackagingProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="PackagingProviderRegistry"/> class.</summary>
        public PackagingProviderRegistry()
        {
            For<IProviderShipment>().Use<ProviderShipment>();
            if (DynamicsAXPackagingProviderConfig.IsValid(false))
            {
                Use<DynamicsAXPackagingProvider>().Singleton().For<IPackagingProviderBase>();
                return;
            }
            if (BasicPackagingProviderConfig.IsValid(false))
            {
                Use<BasicPackagingProvider>().Singleton().For<IPackagingProviderBase>();
                return;
            }
            // Assign default
            Use<BasicPackagingProvider>().Singleton().For<IPackagingProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Packaging
{
    using Basic;
    using DynamicsAx;
    using Interfaces.Providers;
    using Interfaces.Providers.Packaging;
    using Models;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The Packaging provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class PackagingProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="PackagingProviderRegistry"/> class.</summary>
        public PackagingProviderRegistry()
        {
            For<IProviderShipment>().Use<ProviderShipment>();
            var found = false;
            if (DynamicsAXPackagingProviderConfig.IsValid(false))
            {
                For<IPackagingProviderBase>(new SingletonLifecycle()).Add<DynamicsAXPackagingProvider>();
                found = true;
            }
            if (BasicPackagingProviderConfig.IsValid(false))
            {
                For<IPackagingProviderBase>(new SingletonLifecycle()).Add<BasicPackagingProvider>();
                found = true;
            }
            if (found)
            {
                return;
            }
            // Assign default
            For<IPackagingProviderBase>(new SingletonLifecycle()).Add<BasicPackagingProvider>();
        }
    }
}
#endif
