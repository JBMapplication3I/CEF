// <copyright file="CheckoutProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Checkout Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using Interfaces.Models;
    using Interfaces.Providers.Checkouts;
    using Lamar;
    using Models;
#if PAYPAL
    using PayPalInt;
#endif
    using SingleOrder;
    using TargetOrder;

    /// <summary>The Checkout provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class CheckoutProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="CheckoutProviderRegistry"/> class.</summary>
        public CheckoutProviderRegistry()
        {
            For<ICheckoutResult>().Use<CheckoutResult>();
#if PAYPAL
            // NOTE: We can't have two at the same time with checkout providers, may need to put PayPal on a separate interface
            Use<PayPalCheckoutProvider>().Singleton().For<ICheckoutProviderBase>();
#endif
            if (TargetOrderCheckoutProviderConfig.IsValid(false))
            {
                Use<TargetOrderCheckoutProvider>().Singleton().For<ICheckoutProviderBase>();
                return;
            }
            if (SingleOrderCheckoutProviderConfig.IsValid(false))
            {
                Use<SingleOrderCheckoutProvider>().Singleton().For<ICheckoutProviderBase>();
                return;
            }
            // Assign default
            Use<SingleOrderCheckoutProvider>().Singleton().For<ICheckoutProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using Interfaces.Models;
    using Interfaces.Providers.Checkouts;
    using Models;
#if PAYPAL
    using PayPalInt;
#endif
    using SingleOrder;
    using StructureMap;
    using StructureMap.Pipeline;
    using TargetOrder;

    /// <summary>The Checkout provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class CheckoutProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="CheckoutProviderRegistry"/> class.</summary>
        public CheckoutProviderRegistry()
        {
            For<ICheckoutResult>().Use<CheckoutResult>();
#if PAYPAL
            For<ICheckoutProviderBase>(new SingletonLifecycle()).Add<PayPalCheckoutProvider>();
#endif
            var found = false;
            if (TargetOrderCheckoutProviderConfig.IsValid(false))
            {
                For<ICheckoutProviderBase>(new SingletonLifecycle()).Add<TargetOrderCheckoutProvider>();
                found = true;
            }
            if (!found && SingleOrderCheckoutProviderConfig.IsValid(false))
            {
                For<ICheckoutProviderBase>(new SingletonLifecycle()).Add<SingleOrderCheckoutProvider>();
            }
            if (found)
            {
                return;
            }
            // Assign default
            For<ICheckoutProviderBase>(new SingletonLifecycle()).Add<SingleOrderCheckoutProvider>();
        }
    }
}
#endif
