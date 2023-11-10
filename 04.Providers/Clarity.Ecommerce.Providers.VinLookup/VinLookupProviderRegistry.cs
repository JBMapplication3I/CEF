// <copyright file="VinLookupProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the VinLookup Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.VinLookup
{
    using Interfaces.Providers.VinLookup;
    using Lamar;
    using Vincario;

    /// <summary>The VinLookup provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class VinLookupProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="VinLookupProviderRegistry"/> class.</summary>
        public VinLookupProviderRegistry()
        {
            Use<VincarioVinLookupProvider>().Singleton().For<IVinLookupProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.VinLookup
{
    using Interfaces.Providers.VinLookup;
    using StructureMap;
    using StructureMap.Pipeline;
    using Vincario;

    /// <summary>The VinLookup provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class VinLookupProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="VinLookupProviderRegistry"/> class.</summary>
        public VinLookupProviderRegistry()
        {
            For<IVinLookupProviderBase>(new SingletonLifecycle()).Add<VincarioVinLookupProvider>();
        }
    }
}
#endif
