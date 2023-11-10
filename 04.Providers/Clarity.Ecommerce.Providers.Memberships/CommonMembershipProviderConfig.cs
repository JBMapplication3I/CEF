// <copyright file="CommonMembershipProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the common membership provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Memberships
{
    using System.ComponentModel;
    using JetBrains.Annotations;
    using JSConfigs;

    /// <summary>A common membership provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class CommonMembershipProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="CommonMembershipProviderConfig" /> class.</summary>
        static CommonMembershipProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(CommonMembershipProviderConfig));
        }

        /// <summary>Gets the payment process.</summary>
        /// <value>The payment process.</value>
        [AppSettingsKey("Clarity.Payments.PaymentProcess"),
         DefaultValue("AuthorizeAndCapture")]
        internal static string PaymentProcess
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(CommonMembershipProviderConfig)) ? asValue : "AuthorizeAndCapture";
            private set => CEFConfigDictionary.TrySet(value, typeof(CommonMembershipProviderConfig));
        }

        /// <summary>Gets the renewal period before.</summary>
        /// <value>The renewal period before.</value>
        [AppSettingsKey("Clarity.Memberships.RenewalPeriod.AllowedUpToXDaysBefore"),
         DefaultValue(30)]
        internal static int RenewalPeriodBefore
        {
            get => CEFConfigDictionary.TryGet(out int asValue, typeof(CommonMembershipProviderConfig)) ? asValue : 30;
            private set => CEFConfigDictionary.TrySet(value, typeof(CommonMembershipProviderConfig));
        }

        /// <summary>Gets the renewal period after.</summary>
        /// <value>The renewal period after.</value>
        [AppSettingsKey("Clarity.Memberships.RenewalPeriod.AllowedUpToXDaysAfter"),
         DefaultValue(15)]
        internal static int RenewalPeriodAfter
        {
            get => CEFConfigDictionary.TryGet(out int asValue, typeof(CommonMembershipProviderConfig)) ? asValue : 15;
            private set => CEFConfigDictionary.TrySet(value, typeof(CommonMembershipProviderConfig));
        }

        /// <summary>Gets the upgrade period blackout.</summary>
        /// <value>The upgrade period blackout.</value>
        [AppSettingsKey("Clarity.Memberships.UpgradePeriod.BlackoutXDaysBefore"),
         DefaultValue(30)]
        internal static int UpgradePeriodBlackout
        {
            get => CEFConfigDictionary.TryGet(out int asValue, typeof(CommonMembershipProviderConfig)) ? asValue : 30;
            private set => CEFConfigDictionary.TrySet(value, typeof(CommonMembershipProviderConfig));
        }
    }
}
