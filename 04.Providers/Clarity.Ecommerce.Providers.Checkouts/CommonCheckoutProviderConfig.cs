// <copyright file="CommonCheckoutProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the common checkout provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using System.ComponentModel;
    using JetBrains.Annotations;
    using JSConfigs;

    /// <summary>A checkout provider configuration class that contains the settings common to each provider.</summary>
    [PublicAPI, GeneratesAppSettings]
    public static class CommonCheckoutProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="CommonCheckoutProviderConfig" /> class.</summary>
        static CommonCheckoutProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(CommonCheckoutProviderConfig));
        }

        /// <summary>Gets the payment process.</summary>
        /// <value>The payment process.</value>
        public static Enums.PaymentProcessMode PaymentProcess { get; }
            = CEFConfigDictionary.PaymentsProcess;

        /// <summary>Gets a value indicating whether the intentionally no payment provider is set.</summary>
        /// <value>True if intentionally no payment provider, false if not.</value>
        [AppSettingsKey("NoPaymentProvider"),
         DefaultValue(false)]
        internal static bool IntentionallyNoPaymentProvider
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(CommonCheckoutProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(CommonCheckoutProviderConfig));
        }

        /// <summary>Gets a value indicating whether this CheckoutProviderBase is payoneer payment provider
        /// available.</summary>
        /// <value>True if payoneer payment provider available, false if not.</value>
        [AppSettingsKey("PayoneerPaymentsProvider"),
         DefaultValue(false)]
        internal static bool PayoneerPaymentProviderAvailable
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(CommonCheckoutProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(CommonCheckoutProviderConfig));
        }

        /// <summary>Gets a value indicating whether the require ship option.</summary>
        /// <value>True if require ship option, false if not.</value>
        internal static bool RequireShipOption { get; }
            = CEFConfigDictionary.PurchaseRequireShipOption;

        /// <summary>Gets a value indicating whether to add address to the address book if possible.</summary>
        /// <value>True if add addresses to book, false if not.</value>
        internal static bool AddAddressesToBook { get; }
            = CEFConfigDictionary.PurchaseAddAddressesToBook;

        /// <summary>Gets a value indicating whether the override and force ship to home option if no ship option
        /// selected.</summary>
        /// <value>True if override and force ship to home option if no ship option selected, false if not.</value>
        internal static bool OverrideAndForceShipToHomeOptionIfNoShipOptionSelected { get; }
            = CEFConfigDictionary.PurchaseOverrideAndForceShipToHomeOptionIfNoShipOptionSelected;

        /// <summary>Gets a value indicating whether the override and force no ship to option if when ship option
        /// selected.</summary>
        /// <value>True if override and force no ship to option if when ship option selected, false if not.</value>
        internal static bool OverrideAndForceNoShipToOptionIfWhenShipOptionSelected { get; }
            = CEFConfigDictionary.PurchaseOverrideAndForceNoShipToOptionIfWhenShipOptionSelected;
    }
}
