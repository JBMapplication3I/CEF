// <copyright file="CEFConfig.Properties3.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF configuration class.</summary>
// ReSharper disable StyleCop.SA1202, StyleCop.SA1300, StyleCop.SA1303, StyleCop.SA1516, StyleCop.SA1602
#nullable enable
namespace Clarity.Ecommerce.JSConfigs
{
    using System;
    using System.ComponentModel;

    /// <summary>Dictionary of CEF configurations.</summary>
    public static partial class CEFConfigDictionary
    {
        #region Purchase Orders
        /// <summary>Gets a value indicating whether the purchase orders is enabled.</summary>
        /// <value>True if purchase orders enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.PurchaseOrders.Enabled"),
         DefaultValue(false)]
        public static bool PurchaseOrdersEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchase orders has integrated keys.</summary>
        /// <value>True if purchase orders has integrated keys, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.PurchaseOrders.HasIntegratedKeys"),
         DefaultValue(false),
         DependsOn(nameof(PurchaseOrdersEnabled))]
        public static bool PurchaseOrdersHasIntegratedKeys
        {
            get => PurchaseOrdersEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Purchasing
        /// <summary>Gets a value indicating whether the purchasing minimum maximum is enabled.</summary>
        /// <value>True if purchasing minimum maximum enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Purchasing.MinMax.Enabled"),
         DefaultValue(false)]
        public static bool PurchasingMinMaxEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchasing minimum maximum after is enabled.</summary>
        /// <value>True if purchasing minimum maximum after enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Purchasing.MinMax.After.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(LoginEnabled), nameof(PurchasingMinMaxEnabled))]
        public static bool PurchasingMinMaxAfterEnabled
        {
            get => LoginEnabled && PurchasingMinMaxEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchasing availability dates is enabled.</summary>
        /// <value>True if purchasing availability dates enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Purchasing.AvailabilityDates.Enabled"),
         DefaultValue(false)]
        public static bool PurchasingAvailabilityDatesEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchasing role required to purchase is enabled.</summary>
        /// <value>True if purchasing role required to purchase enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Purchasing.RoleRequiredToPurchase.Enabled"),
         DefaultValue(false)]
        public static bool PurchasingRoleRequiredToPurchaseEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchasing role required to see is enabled.</summary>
        /// <value>True if purchasing role required to see enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Purchasing.RoleRequiredToSee.Enabled"),
         DefaultValue(false)]
        public static bool PurchasingRoleRequiredToSeeEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchasing document required is enabled.</summary>
        /// <value>True if purchasing document required enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Purchasing.DocumentRequired.Enabled"),
         DefaultValue(false)]
        public static bool PurchasingDocumentRequiredEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchasing document required override is enabled.</summary>
        /// <value>True if purchasing document required override enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Purchasing.DocumentRequired.Override.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(PurchasingDocumentRequiredEnabled))]
        public static bool PurchasingDocumentRequiredOverrideEnabled
        {
            get => PurchasingDocumentRequiredEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchasing must purchase in multiples of is enabled.</summary>
        /// <value>True if purchasing must purchase in multiples of enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Purchasing.MustPurchaseInMultiplesOf.Enabled"),
         DefaultValue(false)]
        public static bool PurchasingMustPurchaseInMultiplesOfEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the purchasing must purchase in multiples of override is enabled.</summary>
        /// <value>True if purchasing must purchase in multiples of override enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Purchasing.MustPurchaseInMultiplesOf.Override.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(PurchasingMustPurchaseInMultiplesOfEnabled))]
        public static bool PurchasingMustPurchaseInMultiplesOfOverrideEnabled
        {
            get => PurchasingMustPurchaseInMultiplesOfEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the special instructions field in shipping is enabled.</summary>
        /// <value>True if purchasing must purchase in multiples of enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Purchasing.ShippingShowSpecialInstructions.Enabled"),
         DefaultValue(false)]
        public static bool PurchaseShippingShowSpecialInstructions
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region Redis
        /// <summary>NOTE: This should never be false.</summary>
        /// <value>True if caching redis enabled, false if not.</value>
        [AppSettingsKey("Clarity.Caching.Redis.Enabled"),
         DefaultValue(true)]
        public static bool CachingRedisEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets the caching timeout time span.</summary>
        /// <value>The caching timeout time span.</value>
        [AppSettingsKey("Clarity.Caching.TimeoutTimeSpan"),
         DefaultValue("01:00:00")]
        public static TimeSpan CachingTimeoutTimeSpan
        {
            get
            {
                var retVal = TryGet(out TimeSpan asValue) ? asValue : new(01, 00, 00);
                ////if (retVal == null)
                ////{
                ////    // Didn't save a value
                ////    retVal = new TimeSpan(01, 00, 00);
                ////}
                if (retVal.CompareTo(new(00, 00, 00)) == 0)
                {
                    // Saved a bad value, should never be permanent
                    retVal = new(01, 00, 00);
                }
                if (retVal.TotalDays > 1d)
                {
                    // Saved a bad value, should never be permanent
                    retVal = new(01, 00, 00);
                }
                return retVal;
            }
            private set => TrySet(value);
        }

        /// <summary>Gets URI of the caching redis host.</summary>
        /// <value>The caching redis host URI.</value>
        [AppSettingsKey("Clarity.Caching.Redis.Host.Uri"),
         DefaultValue("localhost"),
         DependsOn(nameof(CachingRedisEnabled))]
        public static string CachingRedisHostUri
        {
            get => CachingRedisEnabled ? TryGet(out string asValue) ? asValue : "localhost" : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the caching redis host port.</summary>
        /// <value>The caching redis host port.</value>
        [AppSettingsKey("Clarity.Caching.Redis.Host.Port"),
         DefaultValue(6379),
         DependsOn(nameof(CachingRedisEnabled))]
        public static int? CachingRedisHostPort
        {
            get => CachingRedisEnabled ? TryGet(out int asValue) ? asValue : 6379 : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the caching redis username.</summary>
        /// <value>The caching redis username.</value>
        [AppSettingsKey("Clarity.Caching.Redis.Username"),
         DefaultValue(null),
         DependsOn(nameof(CachingRedisEnabled))]
        public static string? CachingRedisUsername
        {
            get => CachingRedisEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the caching redis password.</summary>
        /// <value>The caching redis password.</value>
        [AppSettingsKey("Clarity.Caching.Redis.Password"),
         DefaultValue(null),
         DependsOn(nameof(CachingRedisEnabled))]
        public static string? CachingRedisPassword
        {
            get => CachingRedisEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the caching redis required ssl.</summary>
        /// <value>True if caching redis required ssl, false if not.</value>
        [AppSettingsKey("Clarity.Caching.Redis.RequireSSL"),
         DefaultValue(false),
         DependsOn(nameof(CachingRedisEnabled))]
        public static bool CachingRedisRequiredSSL
        {
            get => CachingRedisEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the caching redis abort connect.</summary>
        /// <value>True if caching redis abort connect, false if not.</value>
        [AppSettingsKey("Clarity.Caching.Redis.AbortConnect"),
         DefaultValue(false),
         DependsOn(nameof(CachingRedisEnabled))]
        public static bool CachingRedisAbortConnect
        {
            get => CachingRedisEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }
        #endregion

        #region Referral Registrations
        /// <summary>Gets a value indicating whether the referral registrations is enabled.</summary>
        /// <value>True if referral registrations enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.ReferralRegistrations.Enabled"),
         DefaultValue(false)]
        public static bool ReferralRegistrationsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region Registration
        #region Basic Info Pane
        /// <summary>Gets a value indicating whether the registration panes basic information is enabled.</summary>
        /// <value>True if registration panes basic information enabled, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.BasicInfo.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled))]
        public static bool RegistrationPanesBasicInfoEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes basic information position.</summary>
        /// <value>The registration panes basic information position.</value>
        [AppSettingsKey("Clarity.Registration.Panes.BasicInfo.Position"),
         DefaultValue(0),
         DependsOn(nameof(RegistrationPanesBasicInfoEnabled))]
        public static int RegistrationPanesBasicInfoPosition
        {
            get => RegistrationPanesBasicInfoEnabled ? TryGet(out int asValue) ? asValue : 0 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes basic information title.</summary>
        /// <value>The registration panes basic information title.</value>
        [AppSettingsKey("Clarity.Registration.Panes.BasicInfo.Title"),
         DefaultValue("ui.storefront.registration.BasicInformation"),
         DependsOn(nameof(RegistrationPanesBasicInfoEnabled))]
        public static string RegistrationPanesBasicInfoTitle
        {
            get => RegistrationPanesBasicInfoEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.registration.BasicInformation"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes basic information continue text.</summary>
        /// <value>The registration panes basic information continue text.</value>
        [AppSettingsKey("Clarity.Registration.Panes.BasicInfo.ContinueText"),
         DefaultValue("ui.storefront.registration.ContinueToBasicInformation"),
         DependsOn(nameof(RegistrationPanesBasicInfoEnabled))]
        public static string RegistrationPanesBasicInfoContinueText
        {
            get => RegistrationPanesBasicInfoEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.registration.ContinueToBasicInformation"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the registration panes basic information show button.</summary>
        /// <value>True if registration panes basic information show button, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.BasicInfo.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(RegistrationPanesBasicInfoEnabled))]
        public static bool RegistrationPanesBasicInfoShowButton
        {
            get => RegistrationPanesBasicInfoEnabled
                ? TryGet(out bool asValue) ? asValue : true
                : false;
            private set => TrySet(value);
        }
        #endregion

        #region Address Book Pane
        /// <summary>Gets a value indicating whether the address book required in registration.</summary>
        /// <value>True if address book required in registration, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.RequiredInRegistration"),
         DefaultValue(true),
         DependsOn(nameof(AddressBookEnabled))]
        public static bool AddressBookRequiredInRegistration
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the registration panes address book is enabled.</summary>
        /// <value>True if registration panes address book enabled, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.AddressBook.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled))]
        public static bool RegistrationPanesAddressBookEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes address book position.</summary>
        /// <value>The registration panes address book position.</value>
        [AppSettingsKey("Clarity.Registration.Panes.AddressBook.Position"),
         DefaultValue(1),
         DependsOn(nameof(RegistrationPanesAddressBookEnabled))]
        public static int RegistrationPanesAddressBookPosition
        {
            get => RegistrationPanesAddressBookEnabled ? TryGet(out int asValue) ? asValue : 1 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes address book title.</summary>
        /// <value>The registration panes address book title.</value>
        [AppSettingsKey("Clarity.Registration.Panes.AddressBook.Title"),
         DefaultValue("ui.storefront.common.AddressBook"),
         DependsOn(nameof(RegistrationPanesAddressBookEnabled))]
        public static string RegistrationPanesAddressBookTitle
        {
            get => RegistrationPanesAddressBookEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.common.AddressBook"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes address book continue text.</summary>
        /// <value>The registration panes address book continue text.</value>
        [AppSettingsKey("Clarity.Registration.Panes.AddressBook.ContinueText"),
         DefaultValue("ui.storefront.registration.ContinueToAddressBook"),
         DependsOn(nameof(RegistrationPanesAddressBookEnabled))]
        public static string RegistrationPanesAddressBookContinueText
        {
            get => RegistrationPanesAddressBookEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.registration.ContinueToAddressBook"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the registration panes address book show button.</summary>
        /// <value>True if registration panes address book show button, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.AddressBook.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(RegistrationPanesAddressBookEnabled))]
        public static bool RegistrationPanesAddressBookShowButton
        {
            get => RegistrationPanesAddressBookEnabled
                ? TryGet(out bool asValue) ? asValue : true
                : false;
            private set => TrySet(value);
        }
        #endregion

        #region Wallet Pane
        /// <summary>Gets a value indicating whether the registration panes wallet is enabled.</summary>
        /// <value>True if registration panes wallet enabled, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Wallet.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(LoginEnabled))]
        public static bool RegistrationPanesWalletEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes wallet position.</summary>
        /// <value>The registration panes wallet position.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Wallet.Position"),
         DefaultValue(2),
         DependsOn(nameof(RegistrationPanesWalletEnabled))]
        public static int RegistrationPanesWalletPosition
        {
            get => RegistrationPanesWalletEnabled ? TryGet(out int asValue) ? asValue : 2 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes wallet title.</summary>
        /// <value>The registration panes wallet title.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Wallet.Title"),
         DefaultValue("ui.storefront.userDashboard2.userDashboard.Wallet"),
         DependsOn(nameof(RegistrationPanesWalletEnabled))]
        public static string RegistrationPanesWalletTitle
        {
            get => RegistrationPanesWalletEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.userDashboard2.userDashboard.Wallet"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes wallet continue text.</summary>
        /// <value>The registration panes wallet continue text.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Wallet.ContinueText"),
         DefaultValue("ui.storefront.registration.ContinueToWallet"),
         DependsOn(nameof(RegistrationPanesWalletEnabled))]
        public static string RegistrationPanesWalletContinueText
        {
            get => RegistrationPanesWalletEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.registration.ContinueToWallet"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the registration panes wallet show button.</summary>
        /// <value>True if registration panes wallet show button, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Wallet.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(RegistrationPanesWalletEnabled))]
        public static bool RegistrationPanesWalletShowButton
        {
            get => RegistrationPanesWalletEnabled
                ? TryGet(out bool asValue) ? asValue : true
                : false;
            private set => TrySet(value);
        }
        #endregion

        #region Custom Pane
        /// <summary>Gets a value indicating whether the registration panes custom is enabled.</summary>
        /// <value>True if registration panes custom enabled, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Custom.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(LoginEnabled))]
        public static bool RegistrationPanesCustomEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes custom position.</summary>
        /// <value>The registration panes custom position.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Custom.Position"),
         DefaultValue(3),
         DependsOn(nameof(RegistrationPanesCustomEnabled))]
        public static int RegistrationPanesCustomPosition
        {
            get => RegistrationPanesCustomEnabled ? TryGet(out int asValue) ? asValue : 3 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes custom title.</summary>
        /// <value>The registration panes custom title.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Custom.Title"),
         DefaultValue("Custom"),
         DependsOn(nameof(RegistrationPanesCustomEnabled))]
        public static string RegistrationPanesCustomTitle
        {
            get => RegistrationPanesCustomEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "Custom"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes custom continue text.</summary>
        /// <value>The registration panes custom continue text.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Custom.ContinueText"),
         DefaultValue("Custom"),
         DependsOn(nameof(RegistrationPanesCustomEnabled))]
        public static string RegistrationPanesCustomContinueText
        {
            get => RegistrationPanesCustomEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "Custom"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the registration panes custom show button.</summary>
        /// <value>True if registration panes custom show button, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Custom.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(RegistrationPanesCustomEnabled))]
        public static bool RegistrationPanesCustomShowButton
        {
            get => RegistrationPanesCustomEnabled
                ? TryGet(out bool asValue) ? asValue : true
                : false;
            private set => TrySet(value);
        }
        #endregion

        #region Confirmation Pane
        /// <summary>Gets a value indicating whether the registration panes confirmation is enabled.</summary>
        /// <value>True if registration panes confirmation enabled, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Confirmation.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled))]
        public static bool RegistrationPanesConfirmationEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes confirmation position.</summary>
        /// <value>The registration panes confirmation position.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Confirmation.Position"),
         DefaultValue(4),
         DependsOn(nameof(RegistrationPanesConfirmationEnabled))]
        public static int RegistrationPanesConfirmationPosition
        {
            get => RegistrationPanesConfirmationEnabled ? TryGet(out int asValue) ? asValue : 4 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes confirmation title.</summary>
        /// <value>The registration panes confirmation title.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Confirmation.Title"),
         DefaultValue("ui.storefront.checkout.checkoutPanels.Confirmation"),
         DependsOn(nameof(RegistrationPanesConfirmationEnabled))]
        public static string RegistrationPanesConfirmationTitle
        {
            get => RegistrationPanesConfirmationEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.checkout.checkoutPanels.Confirmation"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes confirmation continue text.</summary>
        /// <value>The registration panes confirmation continue text.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Confirmation.ContinueText"),
         DefaultValue("ui.storefront.registration.ContinueToConfirmation"),
         DependsOn(nameof(RegistrationPanesConfirmationEnabled))]
        public static string RegistrationPanesConfirmationContinueText
        {
            get => RegistrationPanesConfirmationEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.registration.ContinueToConfirmation"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the registration panes confirmation show button.</summary>
        /// <value>True if registration panes confirmation show button, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Confirmation.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(RegistrationPanesConfirmationEnabled))]
        public static bool RegistrationPanesConfirmationShowButton
        {
            get => RegistrationPanesConfirmationEnabled
                ? TryGet(out bool asValue) ? asValue : true
                : false;
            private set => TrySet(value);
        }
        #endregion

        #region Completed Pane
        /// <summary>Gets a value indicating whether the registration panes completed is enabled.</summary>
        /// <value>True if registration panes completed enabled, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Completed.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled))]
        public static bool RegistrationPanesCompletedEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes completed position.</summary>
        /// <value>The registration panes completed position.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Completed.Position"),
         DefaultValue(5),
         DependsOn(nameof(RegistrationPanesCompletedEnabled))]
        public static int RegistrationPanesCompletedPosition
        {
            get => RegistrationPanesCompletedEnabled ? TryGet(out int asValue) ? asValue : 5 : -1;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes completed title.</summary>
        /// <value>The registration panes completed title.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Completed.Title"),
         DefaultValue("ui.storefront.registration.RegistrationComplete"),
         DependsOn(nameof(RegistrationPanesCompletedEnabled))]
        public static string RegistrationPanesCompletedTitle
        {
            get => RegistrationPanesCompletedEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.registration.RegistrationComplete"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the registration panes completed continue text.</summary>
        /// <value>The registration panes completed continue text.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Completed.ContinueText"),
         DefaultValue("ui.storefront.memberships.membershipRegistration.completeRegistration"),
         DependsOn(nameof(RegistrationPanesCompletedEnabled))]
        public static string RegistrationPanesCompletedContinueText
        {
            get => RegistrationPanesCompletedEnabled
                ? TryGet(out string asValue)
                    ? asValue
                    : "ui.storefront.memberships.membershipRegistration.completeRegistration"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the registration panes completed show button.</summary>
        /// <value>True if registration panes completed show button, false if not.</value>
        [AppSettingsKey("Clarity.Registration.Panes.Completed.ShowButton"),
         DefaultValue(true),
         DependsOn(nameof(RegistrationPanesCompletedEnabled))]
        public static bool RegistrationPanesCompletedShowButton
        {
            get => RegistrationPanesCompletedEnabled
                ? TryGet(out bool asValue) ? asValue : true
                : false;
            private set => TrySet(value);
        }
        #endregion
        #endregion

        #region Reporting
        /// <summary>Gets a value indicating whether the reporting is enabled.</summary>
        /// <value>True if reporting enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Reporting.Enabled"),
         DefaultValue(false)]
        public static bool ReportingEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the DevExpress reporting is enabled.</summary>
        /// <value>True if reporting enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Reporting.DevExpress.Enabled"),
         DefaultValue(false)]
        public static bool ReportingDevExpressEnabled
        {
            get => ReportingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the SyncFusion reporting is enabled.</summary>
        /// <value>True if reporting enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Reporting.SyncFusion.Enabled"),
         DefaultValue(false)]
        public static bool ReportingSyncFusionEnabled
        {
            get => ReportingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Reviews
        /// <summary>Gets a value indicating whether the reviews is enabled.</summary>
        /// <value>True if reviews enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Reviews.Enabled"),
            DefaultValue(true)]
        public static bool ReviewsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion
    }
}
