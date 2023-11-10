// <copyright file="InventoryProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Inventory provider registry class</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Inventory
{
    using Flat;
    using Interfaces.Models;
    using Interfaces.Providers.Inventory;
    using Lamar;
    using Models;
    using No;
    using PILS;

    /// <summary>The Inventory provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class InventoryProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="InventoryProviderRegistry"/> class.</summary>
        public InventoryProviderRegistry()
        {
            For<ICalculatedInventory>().Use<CalculatedInventory>();
            if (PILSInventoryProviderConfig.IsValid(false))
            {
                Use<PILSInventoryProvider>().Singleton().For<IInventoryProviderBase>();
                return;
            }
            if (FlatInventoryProviderConfig.IsValid(false))
            {
                Use<FlatInventoryProvider>().Singleton().For<IInventoryProviderBase>();
                return;
            }
            if (NoInventoryProviderConfig.IsValid(false))
            {
                Use<NoInventoryProvider>().Singleton().For<IInventoryProviderBase>();
                return;
            }
            // Assign Default
            Use<FlatInventoryProvider>().Singleton().For<IInventoryProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Inventory
{
    using Flat;
    using Interfaces.Models;
    using Interfaces.Providers.Inventory;
    using Models;
    using No;
    using PILS;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The Inventory provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class InventoryProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="InventoryProviderRegistry"/> class.</summary>
        public InventoryProviderRegistry()
        {
            For<ICalculatedInventory>().Use<CalculatedInventory>();
            var found = false;
            if (PILSInventoryProviderConfig.IsValid(false))
            {
                For<IInventoryProviderBase>(new SingletonLifecycle()).Add<PILSInventoryProvider>();
                found = true;
            }
            if (FlatInventoryProviderConfig.IsValid(false))
            {
                For<IInventoryProviderBase>(new SingletonLifecycle()).Add<FlatInventoryProvider>();
                found = true;
            }
            if (NoInventoryProviderConfig.IsValid(false))
            {
                For<IInventoryProviderBase>(new SingletonLifecycle()).Add<NoInventoryProvider>();
                found = true;
            }
            if (found)
            {
                return;
            }
            // Assign Default
            For<IInventoryProviderBase>(new SingletonLifecycle()).Add<FlatInventoryProvider>();
        }
    }
}
#endif
