// <copyright file="PaymentsProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payments provider registry class</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Payments
{
#if AUTHORIZENET
    using Authorize;
#endif
    using BNG;
    using BraintreePayments;
    using BridgePay;
    using CardConnect;
#if CYBERSOURCE
    using CyberSource;
#endif
    using EVO;
    using Interfaces.Providers.Payments;
    using Lamar;
#if LITLE
    using LitleShip;
#endif
    using Mock;
    using OrbitalPaymentech;
    using Payeezy;
    using PayeezyAPI;
    using Payoneer;
    using PayPalPayflowPro;
    using PayTrace;
    using Sage;
    using StripeInt;
    using UnitedTranzaction;

    /// <summary>The payments provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class PaymentsProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="PaymentsProviderRegistry"/> class.</summary>
        public PaymentsProviderRegistry()
        {
            For<IPaymentResponse>().Use<PaymentResponse>();
            For<IPaymentWalletResponse>().Use<PaymentWalletResponse>();
            For<ISubscriptionPaymentModel>().Use<SubscriptionPaymentModel>();
            For<IPayPalPayflowProPaymentsProviderExtensions>().Use<PayPalPayflowProPaymentsProviderExtensions>();
            ////For<IEvoPaymentProviderExtensions>().Use<EvoPaymentProviderExtensions>();
#if AUTHORIZENET
            if (AuthorizePaymentsProviderConfig.IsValid(false))
            {
                Use<AuthorizePaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
#endif
            if (BNGPaymentsProviderConfig.IsValid(false))
            {
                Use<BNGPaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (BridgePayPaymentsProviderConfig.IsValid(false))
            {
                Use<BridgePayPaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (CardConnectPaymentsProviderConfig.IsValid(false))
            {
                Use<CardConnectPaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
#if CYBERSOURCE
            if (CyberSourcePaymentsProviderConfig.IsValid(false))
            {
                Use<CyberSourcePaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
#endif
            if (EvoPaymentProviderConfig.IsValid(false))
            {
                Use<EvoPaymentProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
#if LITLE
            if (LitlePaymentsProviderConfig.IsValid(false))
            {
                Use<LitlePaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
#endif
            if (OrbitalPaymentechPaymentsProviderConfig.IsValid(false))
            {
                Use<OrbitalPaymentechPaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (PayeezyPaymentsProviderConfig.IsValid(false))
            {
                Use<PayeezyPaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (PayeezyAPIPaymentsProviderConfig.IsValid(false))
            {
                Use<PayeezyAPIPaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (PayoneerPaymentsProviderConfig.IsValid(false))
            {
                Use<PayoneerPaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (PayPalPayflowProPaymentsProviderConfig.IsValid(false))
            {
                Use<PayPalPayflowProPaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (PayTracePaymentsProviderConfig.IsValid(false))
            {
                Use<PayTracePaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (SagePaymentsProviderConfig.IsValid(false))
            {
                Use<SagePaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (StripePaymentsProviderConfig.IsValid(false))
            {
                Use<StripePaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (UnitedTranzactionPaymentsProviderConfig.IsValid(false))
            {
                Use<UnitedTranzactionPaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            if (BraintreePaymentsProviderConfig.IsValid(false))
            {
                Use<BraintreePaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
                return;
            }
            // Assign default
            Use<MockPaymentsProvider>().Singleton().For<IPaymentsProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Payments
{
#if AUTHORIZENET
    using Authorize;
#endif
    using BNG;
    using BraintreePayments;
    using BridgePay;
    using CardConnect;
#if CYBERSOURCE
    using CyberSource;
#endif
    using EVO;
    using Interfaces.Providers.Payments;
#if LITLE
    using LitleShip;
#endif
    using Mock;
    using OrbitalPaymentech;
    using Payeezy;
    using PayeezyAPI;
    using Payoneer;
    using PayPalPayflowPro;
    using PayTrace;
    using Sage;
    using StripeInt;
    using StructureMap;
    using StructureMap.Pipeline;
    using UnitedTranzaction;

    /// <summary>The payments provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class PaymentsProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="PaymentsProviderRegistry"/> class.</summary>
        public PaymentsProviderRegistry()
        {
            For<IPaymentResponse>().Use<PaymentResponse>();
            For<IPaymentWalletResponse>().Use<PaymentWalletResponse>();
            For<ISubscriptionPaymentModel>().Use<SubscriptionPaymentModel>();
            For<IPayPalPayflowProPaymentsProviderExtensions>().Use<PayPalPayflowProPaymentsProviderExtensions>();
            ////For<IEvoPaymentProviderExtensions>().Use<EvoPaymentProviderExtensions>();
            var found = false;
#if AUTHORIZENET
            if (AuthorizePaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<AuthorizePaymentsProvider>();
                found = true;
            }
#endif
            if (BNGPaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<BNGPaymentsProvider>();
                found = true;
            }
            if (BridgePayPaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<BridgePayPaymentsProvider>();
                found = true;
            }
            if (CardConnectPaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<CardConnectPaymentsProvider>();
                found = true;
            }
#if CYBERSOURCE
            if (CyberSourcePaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<CyberSourcePaymentsProvider>();
                found = true;
            }
#endif
            if (EvoPaymentProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<EvoPaymentProvider>();
                found = true;
            }
#if LITLE
            if (LitlePaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<LitlePaymentsProvider>();
                found = true;
            }
#endif
            if (OrbitalPaymentechPaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<OrbitalPaymentechPaymentsProvider>();
                found = true;
            }
            if (PayeezyPaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<PayeezyPaymentsProvider>();
                found = true;
            }
            if (PayeezyAPIPaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<PayeezyAPIPaymentsProvider>();
                found = true;
            }
            if (PayoneerPaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<PayoneerPaymentsProvider>();
                found = true;
            }
            if (PayPalPayflowProPaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<PayPalPayflowProPaymentsProvider>();
                found = true;
            }
            if (PayTracePaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<PayTracePaymentsProvider>();
                found = true;
            }
            if (SagePaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<SagePaymentsProvider>();
                found = true;
            }
            if (StripePaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<StripePaymentsProvider>();
                found = true;
            }
            if (UnitedTranzactionPaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<UnitedTranzactionPaymentsProvider>();
                found = true;
            }
            if (BraintreePaymentsProviderConfig.IsValid(false))
            {
                For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<BraintreePaymentsProvider>();
                found = true;
            }
            if (found)
            {
                return;
            }
            // Assign default
            For<IPaymentsProviderBase>(new SingletonLifecycle()).Add<MockPaymentsProvider>();
        }
    }
}
#endif
