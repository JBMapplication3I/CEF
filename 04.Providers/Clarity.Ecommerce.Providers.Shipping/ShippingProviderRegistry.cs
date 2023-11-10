// <copyright file="ShippingProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Shipping Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Shipping
{
    using Combined;
    using FedEx;
    using FlatRate;
    using InStorePickup;
    using Interfaces.Providers.Shipping;
    using Lamar;
    using Loomis;
    using PercentageOfSubtotalAndTargetDate;
    using ShipToStore;
    using TNT;
    using UPS;
    using USPS;
    using YRC;
    using Zone;

    /// <summary>The Shipping provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class ShippingProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="ShippingProviderRegistry"/> class.</summary>
        public ShippingProviderRegistry()
        {
            if (CombinedShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<CombinedShippingProvider>().Singleton();
            }
            if (FedExShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<FedExShippingProvider>().Singleton();
            }
            if (FlatRateShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<FlatRateShippingProvider>().Singleton();
            }
            if (InStorePickupShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<InStorePickupShippingProvider>().Singleton();
            }
            if (LoomisShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<LoomisShippingProvider>().Singleton();
            }
            if (PercentageOfSubtotalAndTargetDateShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<PercentageOfSubtotalAndTargetDateShippingProvider>().Singleton();
            }
            if (ShipToStoreShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<ShipToStoreShippingProvider>().Singleton();
            }
            if (TNTShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<TNTShippingProvider>().Singleton();
            }
            if (UPSShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<UPSShippingProvider>().Singleton();
            }
            if (USPSShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<USPSShippingProvider>().Singleton();
            }
            if (YRCShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<YRCShippingProvider>().Singleton();
            }
            if (ZoneRateShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>().Add<ZoneRateShippingProvider>().Singleton();
            }
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Shipping
{
    using Combined;
    using FedEx;
    using FlatRate;
    using InStorePickup;
    using Interfaces.Providers.Shipping;
    using Loomis;
    using PercentageOfSubtotalAndTargetDate;
    using ShipToStore;
    using StructureMap;
    using StructureMap.Pipeline;
    using TNT;
    using UPS;
    using USPS;
    using YRC;
    using Zone;

    /// <summary>The Shipping provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class ShippingProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="ShippingProviderRegistry"/> class.</summary>
        public ShippingProviderRegistry()
        {
            if (CombinedShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<CombinedShippingProvider>();
            }
            if (FedExShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<FedExShippingProvider>();
            }
            if (FlatRateShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<FlatRateShippingProvider>();
            }
            if (InStorePickupShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<InStorePickupShippingProvider>();
            }
            if (LoomisShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<LoomisShippingProvider>();
            }
            if (PercentageOfSubtotalAndTargetDateShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<PercentageOfSubtotalAndTargetDateShippingProvider>();
            }
            if (ShipToStoreShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<ShipToStoreShippingProvider>();
            }
            if (TNTShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<TNTShippingProvider>();
            }
            if (UPSShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<UPSShippingProvider>();
            }
            if (USPSShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<USPSShippingProvider>();
            }
            if (YRCShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<YRCShippingProvider>();
            }
            if (ZoneRateShippingProviderConfig.IsValid(false))
            {
                For<IShippingProviderBase>(new SingletonLifecycle()).Add<ZoneRateShippingProvider>();
            }
        }
    }
}
#endif
