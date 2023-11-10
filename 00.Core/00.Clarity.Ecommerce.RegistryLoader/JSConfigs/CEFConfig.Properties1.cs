// <copyright file="CEFConfig.Properties1.cs" company="clarity-ventures.com">
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
        #region Other
        /// <summary>Gets the name of the API.</summary>
        /// <value>The name of the API.</value>
        /// <remarks>Sets the name of the API that ServiceStack launches itself as.</remarks>
        [AppSettingsKey("APIName"),
         DefaultValue("Clarity eCommerce Platform API")]
        public static string APIName
        {
            get => TryGet(out string asValue) ? asValue : "Clarity eCommerce Platform API";
            private set => TrySet(value);
        }

        /// <summary>Gets the name of the API.</summary>
        /// <value>The name of the API.</value>
        /// <remarks>Sets the name of the API that ServiceStack launches itself as.</remarks>
        [AppSettingsKey("Clarity.Ordering.SearchIncludeNullForSalesGroupAsMaster"),
         DefaultValue(null)]
        public static bool? SearchIncludeNullForSalesGroupAsMaster
        {
            get => TryGet(out bool? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the name of the API.</summary>
        /// <value>The name of the API.</value>
        /// <remarks>Sets the name of the API that ServiceStack launches itself as.</remarks>
        [AppSettingsKey("Clarity.Ordering.SearchIncludeNullForSalesGroupAsSub"),
         DefaultValue(null)]
        public static bool? SearchIncludeNullForSalesGroupAsSub
        {
            get => TryGet(out bool? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the name of the API.</summary>
        /// <value>The name of the API.</value>
        /// <remarks>Sets the name of the API that ServiceStack launches itself as.</remarks>
        [AppSettingsKey("Clarity.Ordering.SearchMasterOrdersOnly"),
         DefaultValue(null)]
        public static bool? SearchMasterOrdersOnly
        {
            get => TryGet(out bool? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the name of the API.</summary>
        /// <value>The name of the API.</value>
        /// <remarks>Sets the name of the API that ServiceStack launches itself as.</remarks>
        [AppSettingsKey("Clarity.Ordering.SearchIncludeNullForShippingID"),
         DefaultValue(null)]
        public static bool? SearchIncludeNullForShippingID
        {
            get => TryGet(out bool? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the plugins folder.</summary>
        /// <value>The full pathname of the plugins folder.</value>
        [AppSettingsKey("Clarity.Providers.PluginsPath"),
#if NET5_0_OR_GREATER
         DefaultValue(@"{CEF_RootPath}\Plugins5")]
#else
         DefaultValue(@"{CEF_RootPath}\Plugins")]
#endif
        public static string PluginsPath
        {
            get => TryGet(out string asValue) ? asValue : @"{CEF_RootPath}\Plugins";
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the clients folder.</summary>
        /// <value>The full pathname of the clients folder.</value>
        [AppSettingsKey("Clarity.Providers.ClientsPath"),
#if NET5_0_OR_GREATER
         DefaultValue(@"{CEF_RootPath}\ClientPlugins5")]
#else
         DefaultValue(@"{CEF_RootPath}\ClientPlugins")]
#endif
        public static string ClientsPath
        {
            get => TryGet(out string asValue) ? asValue : @"{CEF_RootPath}\ClientPlugins";
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the plugins folder.</summary>
        /// <value>The full pathname of the plugins folder.</value>
        [AppSettingsKey("Clarity.Providers.SchedulerPath"),
#if NET5_0_OR_GREATER
         DefaultValue(@"{CEF_RootPath}\07.Portals\Scheduler\Scheduler.Net5\bin")]
#else
         DefaultValue(@"{CEF_RootPath}\07.Portals\Scheduler\Scheduler\bin")]
#endif
        public static string ScheduledTasksPath
        {
            get => TryGet(out string asValue) ? asValue : @"{CEF_RootPath}\07.Portals\Scheduler\Scheduler\bin";
            private set => TrySet(value);
        }

        /// <summary>The name of the company to display in SEO URLs and pages like Registration.</summary>
        /// <value>The name of the company.</value>
        [AppSettingsKey("Clarity.CompanyName"),
         DefaultValue("Clarity eCommerce Demo")]
        public static string CompanyName
        {
            get => TryGet(out string asValue) ? asValue : "Clarity eCommerce Demo";
            private set => TrySet(value);
        }

        /// <summary>Gets the main contact phone # of the company.</summary>
        /// <value>The main contact phone # of the company.</value>
        [AppSettingsKey("Clarity.CompanyPhone"),
            DefaultValue(null)]
        public static string? CompanyPhone
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the main contact email of the company.</summary>
        /// <value>The main contact email of the company.</value>
        [AppSettingsKey("Clarity.CompanyEmail"),
            DefaultValue(null)]
        public static string? CompanyEmail
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>The default country code.</summary>
        /// <value>The country code.</value>
        /// <remarks>If this is not set, default 'getRegions' calls will not run.</remarks>
        [AppSettingsKey("Clarity.Globalization.CountryCode.Default"),
         DefaultValue("USA")] // Confirmed it's USA, not US 2020-09-24
        public static string CountryCode
        {
            get => TryGet(out string asValue) ? asValue : "USA";
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the log every request.</summary>
        /// <value>True if log every request, false if not.</value>
        [AppSettingsKey("Clarity.Logger.LogEveryRequest"),
         DefaultValue(false)]
        public static bool LogEveryRequest
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the API disable metadata.</summary>
        /// <value>True if API disable metadata, false if not.</value>
        [AppSettingsKey("Clarity.API.DisableMetadata"),
         DefaultValue(false)]
        public static bool APIDisableMetadata
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the API use UTC.</summary>
        /// <value>True if API use utc, false if not.</value>
        [AppSettingsKey("Clarity.API.UseUTC"),
         DefaultValue(false)]
        public static bool APIUseUTC
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>For CORS: Have to include each of: no-prefix, http and https prefixes.</summary>
        /// <example>
        /// develop.claritydemos.com http://develop.claritydemos.com https://develop.claritydemos.com .
        /// </example>
        /// <value>The origin white-list.</value>
        [AppSettingsKey("Clarity.API.Requests.OriginsWhiteList"),
         DefaultValue(new string[0]),
         SplitOn(new[] { ' ' })]
        public static string[] ServiceStackOriginsWhiteList
        {
            get => TryGet<string[]>(out var asValue) ? asValue : Array.Empty<string>();
            private set => TrySet(value);
        }

        /// <summary>Web locations where resources like CSS and JS files can be loaded from (a white list).</summary>
        /// <example>
        /// // "self" is already included
        /// // Don't use any localhost with or without port numbers as it confuses Chrome
        /// // Allow loading from our assets domain. Notice the difference between * and **
        /// "http://some.subdomain.website.com/**",
        /// "https://some.subdomain.website.com/**",
        /// "http://shop.my-website.com/**",
        /// "https://shop.my-website.com/**",
        /// "http://*.webdev.us/**",
        /// "http://*.webdev.*/**" .
        /// </example>
        /// <value>The origin white-list.</value>
        [AppSettingsKey("Clarity.CORS.ResourceWhiteList"),
         DefaultValue(new string[0]),
         SplitOn(new[] { ' ' })]
        public static string[] CORSResourceWhiteList
        {
            get => TryGet<string[]>(out var asValue) ? asValue : Array.Empty<string>();
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the storefront debug mode.</summary>
        /// <value>True if storefront debug mode, false if not.</value>
        [AppSettingsKey("Clarity.Admin.DebugMode"),
         DefaultValue(false)]
        public static bool AdminDebugMode
        {
            get => TryGet<bool>(out var asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the storefront debug mode.</summary>
        /// <value>True if storefront debug mode, false if not.</value>
        [AppSettingsKey("Clarity.Storefront.DebugMode"),
         DefaultValue(false)]
        public static bool StorefrontDebugMode
        {
            get => TryGet<bool>(out var asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the storefront HTML 5 mode.</summary>
        /// <value>The storefront HTML 5 mode.</value>
        [AppSettingsKey("Clarity.Storefront.HTML5Mode"),
         DefaultValue("{ enabled: false, requireBase: false }")]
        public static string StorefrontHTML5Mode
        {
            get => TryGet<string>(out var asValue) ? asValue : "{ enabled: false, requireBase: false }";
            private set => TrySet(value);
        }

        /// <summary>Gets the admin HTML 5 mode.</summary>
        /// <value>The admin HTML 5 mode.</value>
        [AppSettingsKey("Clarity.Admin.HTML5Mode"),
         DefaultValue("true")]
        public static string AdminHTML5Mode
        {
            get => TryGet<string>(out var asValue) ? asValue : "true";
            private set => TrySet(value);
        }

        /// <summary>Gets the store admin HTML 5 mode.</summary>
        /// <value>The store admin HTML 5 mode.</value>
        [AppSettingsKey("Clarity.StoreAdmin.HTML5Mode"),
         DefaultValue("{ enabled: false, requireBase: false }")]
        public static string StoreAdminHTML5Mode
        {
            get => TryGet<string>(out var asValue) ? asValue : "{ enabled: false, requireBase: false }";
            private set => TrySet(value);
        }

        /// <summary>Gets the brand admin HTML 5 mode.</summary>
        /// <value>The brand admin HTML 5 mode.</value>
        [AppSettingsKey("Clarity.BrandAdmin.HTML5Mode"),
         DefaultValue("{ enabled: false, requireBase: false }")]
        public static string BrandAdminHTML5Mode
        {
            get => TryGet<string>(out var asValue) ? asValue : "{ enabled: false, requireBase: false }";
            private set => TrySet(value);
        }

        /// <summary>Gets the vendor admin HTML 5 mode.</summary>
        /// <value>The vendor admin HTML 5 mode.</value>
        [AppSettingsKey("Clarity.VendorAdmin.HTML5Mode"),
         DefaultValue("true")]
        public static string VendorAdminHTML5Mode
        {
            get => TryGet<string>(out var asValue) ? asValue : "true";
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the admin show storefront button.</summary>
        /// <value>True if admin show storefront button, false if not.</value>
        [AppSettingsKey("Clarity.Admin.MenuButtons.Storefront.Show"),
         DefaultValue(true)]
        public static bool AdminShowStorefrontButton
        {
            get => TryGet<bool>(out var asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the admin show DotNetNuke button.</summary>
        /// <value>True if admin show DotNetNuke button, false if not.</value>
        [AppSettingsKey("Clarity.Admin.MenuButtons.DNN.Show"),
         DefaultValue(true),
         Unused]
        public static bool AdminShowDNNButton
        {
            get => TryGet<bool>(out var asValue) ? asValue : true;
            private set => TrySet(value);
        }
        #endregion

        #region API Requests
        /// <summary>If true, every request that has a StoreID as part of it (based on an internal interface) will
        /// determine the appropriate store ID to place on it and do so, overriding any value that came over the wire.</summary>
        /// <value>True if API requests validate store, false if not.</value>
        [AppSettingsKey("Clarity.API.Requests.ValidateStore"),
         DefaultValue(false),
         DependsOn(nameof(StoresEnabled))]
        public static bool APIRequestsValidateStore
        {
            get => StoresEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>If true, every request that has a FranchiseID as part of it (based on an internal interface) will
        /// determine the appropriate franchise ID to place on it and do so, overriding any value that came over the wire.</summary>
        /// <value>True if API requests validate franchise, false if not.</value>
        [AppSettingsKey("Clarity.API.Requests.ValidateFranchise"),
         DefaultValue(false),
         DependsOn(nameof(FranchisesEnabled))]
        public static bool APIRequestsValidateFranchise
        {
            get => FranchisesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>If true, every request that has a BrandID as part of it (based on an internal interface) will
        /// determine the appropriate brand ID to place on it and do so, overriding any value that came over the wire.</summary>
        /// <value>True if API requests validate brand, false if not.</value>
        [AppSettingsKey("Clarity.API.Requests.ValidateBrand"),
         DefaultValue(false),
         DependsOn(nameof(BrandsEnabled))]
        public static bool APIRequestsValidateBrand
        {
            get => BrandsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>If true, every request that has a ManufacturerID as part of it (based on an internal interface) will
        /// determine the appropriate manufacturer ID to place on it and do so, overriding any value that came over the wire.</summary>
        /// <value>True if API requests validate manufacturer, false if not.</value>
        [AppSettingsKey("Clarity.API.Requests.ValidateManufacturer"),
         DefaultValue(false),
         DependsOn(nameof(ManufacturersEnabled))]
        public static bool APIRequestsValidateManufacturer
        {
            get => ManufacturersEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>If true, every request that has a VendorID as part of it (based on an internal interface) will
        /// determine the appropriate vendor ID to place on it and do so, overriding any value that came over the wire.</summary>
        /// <value>True if API requests validate vendor, false if not.</value>
        [AppSettingsKey("Clarity.API.Requests.ValidateVendor"),
         DefaultValue(false),
         DependsOn(nameof(VendorsEnabled))]
        public static bool APIRequestsValidateVendor
        {
            get => VendorsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>If true, every request validates that is was sent over HTTPS, and returns 403 if it fails this check.</summary>
        /// <value>True if API requests require https, false if not.</value>
        [AppSettingsKey("Clarity.API.Requests.RequireHTTPS"),
         DefaultValue(false)]
        public static bool APIRequestsRequireHTTPS
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the API requests always very by referer.</summary>
        /// <value>True if API requests always very by referer, false if not.</value>
        [AppSettingsKey("Clarity.API.Requests.AlwaysVeryByReferer"),
         DefaultValue(false)]
        public static bool APIRequestsAlwaysVeryByReferer
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region Address Book
        /// <summary>Gets a value indicating whether the dashboard add addresses to book.</summary>
        /// <value>True if dashboard add addresses to book, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.AddAddressesToBook"),
            DefaultValue(true),
            DependsOn(nameof(AddressBookEnabled))]
        public static bool DashboardAddAddressesToBook
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Site-wide activation of address book UI and functionality.</summary>
        /// <value>True if address book enabled, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled))]
        public static bool AddressBookEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Address Book hide custom keys.</summary>
        /// <value>True if address book hide keys, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.Inputs.HideKeys"),
            DefaultValue(false),
            DependsOn(nameof(AddressBookEnabled))]
        public static bool AddressBookHideKeys
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Address Book hide first name.</summary>
        /// <value>True if address book hide first name, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.Inputs.HideFirstName"),
            DefaultValue(false),
            DependsOn(nameof(AddressBookEnabled))]
        public static bool AddressBookHideFirstName
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Address Book hide first name.</summary>
        /// <value>True if address book hide last name, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.Inputs.HideLastName"),
            DefaultValue(false),
            DependsOn(nameof(AddressBookEnabled))]
        public static bool AddressBookHideLastName
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Address Book hide last name.</summary>
        /// <value>True if address book hide email, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.Inputs.HideEmail"),
            DefaultValue(false),
            DependsOn(nameof(AddressBookEnabled))]
        public static bool AddressBookHideEmail
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Address Book hide phone.</summary>
        /// <value>True if address book hide phone, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.Inputs.HidePhone"),
            DefaultValue(false),
            DependsOn(nameof(AddressBookEnabled))]
        public static bool AddressBookHidePhone
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Address Book hide fax.</summary>
        /// <value>True if address book hide fax, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.Inputs.HideFax"),
            DefaultValue(false),
            DependsOn(nameof(AddressBookEnabled))]
        public static bool AddressBookHideFax
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the address book allow make this my new default billing in
        /// checkout.</summary>
        /// <value>True if address book allow make this my new default billing in checkout, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.AllowMakeThisMyNewDefault.Billing.InCheckout"),
            DefaultValue(false),
            DependsOn(nameof(AddressBookEnabled))]
        public static bool AddressBookAllowMakeThisMyNewDefaultBillingInCheckout
        {
            get => AddressBookEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the address book allow make this my new default billing in
        /// dashboard.</summary>
        /// <value>True if address book allow make this my new default billing in dashboard, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.AllowMakeThisMyNewDefault.Billing.InDashboard"),
            DefaultValue(true),
            DependsOn(nameof(AddressBookEnabled), nameof(PurchasePanesBillingEnabled))]
        public static bool AddressBookAllowMakeThisMyNewDefaultBillingInDashboard
        {
            get => AddressBookEnabled && PurchasePanesBillingEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the address book allow make this my new default shipping in
        /// checkout.</summary>
        /// <value>True if address book allow make this my new default shipping in checkout, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.AllowMakeThisMyNewDefault.Shipping.InCheckout"),
            DefaultValue(false),
            DependsOn(nameof(AddressBookEnabled), nameof(ShippingEnabled), nameof(PurchasePanesShippingEnabled)),
            MutuallyExclusiveWith(nameof(SplitShippingEnabled))]
        public static bool AddressBookAllowMakeThisMyNewDefaultShippingInCheckout
        {
            get => AddressBookEnabled && ShippingEnabled && !SplitShippingEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the address book allow make this my new default shipping in
        /// dashboard.</summary>
        /// <value>True if address book allow make this my new default shipping in dashboard, false if not.</value>
        [AppSettingsKey("Clarity.AddressBook.AllowMakeThisMyNewDefault.Shipping.InDashboard"),
            DefaultValue(true),
            DependsOn(nameof(AddressBookEnabled), nameof(ShippingEnabled))]
        public static bool AddressBookAllowMakeThisMyNewDefaultShippingInDashboard
        {
            get => AddressBookEnabled && ShippingEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }
        #endregion

        #region Advertising
        /// <summary>Gets a value indicating whether the ads is enabled.</summary>
        /// <value>True if ads enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Ads.Enabled"),
         DefaultValue(false)]
        public static bool AdsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region Affiliates
        /// <summary>Gets a value indicating whether the affiliates is enabled.</summary>
        /// <value>True if affiliates enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Affiliates.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(LoginEnabled))]
        public static bool AffiliatesEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether roles are required to access associated accounts.</summary>
        /// <value>True if no roles are required for accessing associated accounts, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Affiliates.RoleRequiredForAssociatedAccountSelection"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled))]
        public static bool RoleRequiredForAssociatedAccountSelection
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Auctions
        /// <summary>Gets a value indicating whether auctions are enabled.</summary>
        /// <value>True if auctions are enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Auctions.Enabled"),
            DefaultValue(false)]
        public static bool AuctionsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value for the Global Bid Percentage.</summary>
        /// <value>The global bid percentage.</value>
        [AppSettingsKey("Clarity.Auctions.GlobalBidPercentage"),
            DefaultValue(null)]
        public static decimal? PercentageOfGreatestBid
        {
            get => TryGet(out decimal? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether an alert will be shown if a new bid is much higher than the current bid.</summary>
        /// <value>True if large bid notifications are enabled, false if not.</value>
        [AppSettingsKey("Clarity.Auctions.LargeBidNotification.Enabled"),
            DefaultValue(false)]
        public static bool LargeBidNotificationEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value setting the percentage over the current bid that will trigger an alert.</summary>
        /// <value>The percentage over the current bid.</value>
        [AppSettingsKey("Clarity.Auctions.LargeBidNotification.Percentage"),
            DefaultValue(null)]
        public static decimal? LargeBidNotificationPercentage
        {
            get => TryGet(out decimal? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        #endregion

        #region SignalR
        /// <summary>Gets a value indicating whether signalR is enabled.</summary>
        /// <value>True if signalR is enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SignalR.Enabled"),
            DefaultValue(false)]
        public static bool SignalREnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }
        #endregion

        #region Auth
        /// <summary>Gets a value indicating whether the login is enabled.</summary>
        /// <value>True if login enabled, false if not.</value>
        [AppSettingsKey("Clarity.Login.Enabled"),
            DefaultValue(true)]
        public static bool LoginEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets the authentication providers.</summary>
        /// <value>The authentication providers.</value>
        /// <example>
        /// identity<br/>
        /// dnnsso<br/>
        /// tokenized<br/>
        /// openid<br/>
        /// openid,identity .
        /// </example>
        /// <default>identity.</default>
        [AppSettingsKey("Clarity.API.Auth.Providers"),
            DefaultValue("identity")]
        public static string AuthProviders
        {
            get => LoginEnabled ? TryGet(out string asValue) ? asValue : "identity" : "identity";
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the authentication provider password require digit.</summary>
        /// <value>True if authentication provider password require digit, false if not.</value>
        [AppSettingsKey("Clarity.API.Auth.Passwords.RequireDigit"),
            DefaultValue(false),
            Unused]
        public static bool AuthProviderPasswordRequireDigit
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the length of the authentication provider password require.</summary>
        /// <value>The length of the authentication provider password require.</value>
        [AppSettingsKey("Clarity.API.Auth.Passwords.RequireLength"),
            DefaultValue(6),
            Unused]
        public static int AuthProviderPasswordRequireLength
        {
            get => TryGet(out int asValue) ? asValue : 6;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the authentication provider password require lowercase.</summary>
        /// <value>True if authentication provider password require lowercase, false if not.</value>
        [AppSettingsKey("Clarity.API.Auth.Passwords.RequireLowercase"),
            DefaultValue(false),
            Unused]
        public static bool AuthProviderPasswordRequireLowercase
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the authentication provider password require uppercase.</summary>
        /// <value>True if authentication provider password require uppercase, false if not.</value>
        [AppSettingsKey("Clarity.API.Auth.Passwords.RequireUppercase"),
            DefaultValue(false),
            Unused]
        public static bool AuthProviderPasswordRequireUppercase
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the authentication provider password require non letter or digit.</summary>
        /// <value>True if authentication provider password require non letter or digit, false if not.</value>
        [AppSettingsKey("Clarity.API.Auth.Passwords.RequireNonLetterOrDigit"),
            DefaultValue(false),
            Unused]
        public static bool AuthProviderPasswordRequireNonLetterOrDigit
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the authentication provider username is email.</summary>
        /// <value>True if authentication provider username is email, false if not.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.Identity.UsernameIsEmail"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled))]
        public static bool AuthProviderUsernameIsEmail
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the authentication provider username is email.</summary>
        /// <value>True if authentication provider username is email, false if not.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.Email.UseSpecialCharInEmail"),
            DefaultValue(true)]
        public static bool UseSpecialCharInEmail
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether two factor authentication is enabled.</summary>
        /// <value>True if TwoFactor enabled, false if not.</value>
        [AppSettingsKey("Clarity.Login.TwoFactor.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled))]
        public static bool TwoFactorEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the two factor forced.</summary>
        /// <value>True if two factor forced, false if not.</value>
        [AppSettingsKey("Clarity.Login.TwoFactor.Forced.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(TwoFactorEnabled))]
        public static bool TwoFactorForced
        {
            get => TwoFactorEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the two factor by email is enabled.</summary>
        /// <value>True if two factor by email enabled, false if not.</value>
        [AppSettingsKey("Clarity.Login.TwoFactor.ByEmail.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(TwoFactorEnabled))]
        public static bool TwoFactorByEmailEnabled
        {
            get => TwoFactorEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the two factor by SMS is enabled.</summary>
        /// <value>True if two factor by SMS enabled, false if not.</value>
        [AppSettingsKey("Clarity.Login.TwoFactor.BySMS.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(TwoFactorEnabled))]
        public static bool TwoFactorBySMSEnabled
        {
            get => TwoFactorEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the two factor by email subject.</summary>
        /// <value>The two factor by email subject.</value>
        [AppSettingsKey("Clarity.Login.TwoFactor.ByEmail.Subject"),
            DefaultValue(null),
            DependsOn(nameof(TwoFactorByEmailEnabled))]
        public static string? TwoFactorByEmailSubject
        {
            get => TwoFactorByEmailEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the two factor by email body.</summary>
        /// <value>The two factor by email body.</value>
        [AppSettingsKey("Clarity.Login.TwoFactor.ByEmail.Body"),
            DefaultValue(null),
            DependsOn(nameof(TwoFactorByEmailEnabled))]
        public static string? TwoFactorByEmailBody
        {
            get => TwoFactorByEmailEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the two factor by SMS body.</summary>
        /// <value>The two factor by SMS body.</value>
        [AppSettingsKey("Clarity.Login.TwoFactor.BySMS.Body"),
            DefaultValue(null),
            DependsOn(nameof(TwoFactorBySMSEnabled))]
        public static string? TwoFactorBySMSBody
        {
            get => TwoFactorBySMSEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        #region OpenID
        /// <summary>Gets URL of the authentication provider authorize.</summary>
        /// <value>The authentication provider authorize URL.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.AuthorizeUrl"),
         DefaultValue(null),
         DependsOn("OpenIDAuthProvider")]
        public static string? AuthProviderAuthorizeUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the identifier of the authentication provider client.</summary>
        /// <value>The identifier of the authentication provider client.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.ClientID"),
         DefaultValue(null),
         DependsOn("OpenIDAuthProvider")]
        public static string? AuthProviderClientId
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the authentication provider client secret.</summary>
        /// <value>The authentication provider client secret.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.ClientSecret"),
         DefaultValue(null),
         DependsOn("OpenIDAuthProvider")]
        public static string? AuthProviderClientSecret
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the authentication provider discovery.</summary>
        /// <value>The authentication provider discovery URL.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.DiscoveryUrl"),
         DefaultValue(null),
         DependsOn("OpenIDAuthProvider")]
        public static string? AuthProviderDiscoveryUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the name of the authentication provider email claim.</summary>
        /// <value>The name of the authentication provider email claim.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.EmailClaimName"),
         DefaultValue(null),
         DependsOn("OpenIDAuthProvider")]
        public static string? AuthProviderEmailClaimName
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the authentication provider issuer.</summary>
        /// <value>The authentication provider issuer.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.Issuer"),
         DefaultValue(null),
         DependsOn("OpenIDAuthProvider")]
        public static string? AuthProviderIssuer
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the authentication provider logout.</summary>
        /// <value>The authentication provider logout URL.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.LogoutUrl"),
         DefaultValue(null)]
        public static string? AuthProviderLogoutUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets URI of the authentication provider redirect.</summary>
        /// <value>The authentication provider redirect URI.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.RedirectUri"),
         DefaultValue(null),
         DependsOn("OpenIDAuthProvider")]
        public static string? AuthProviderRedirectUri
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the authentication provider scope.</summary>
        /// <value>The authentication provider scope.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.Scope"),
         DefaultValue(null),
         DependsOn("OpenIDAuthProvider")]
        public static string? AuthProviderScope
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the authentication provider token.</summary>
        /// <value>The authentication provider token URL.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.TokenUrl"),
         DefaultValue(null),
         DependsOn("OpenIDAuthProvider")]
        public static string? AuthProviderTokenUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets URL of the authentication provider user information.</summary>
        /// <value>The authentication provider user information URL.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.OpenID.UserInfoUrl"),
         DefaultValue(null),
         DependsOn("OpenIDAuthProvider")]
        public static string? AuthProviderUserInfoUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }
        #endregion

        /// <summary>Gets URL of the authentication provider tokenized alternative check.</summary>
        /// <value>The authentication provider tokenized alternative check URL.</value>
        [AppSettingsKey("Clarity.API.Auth.Providers.Tokenized.AlternativeCheckUrl"),
            DefaultValue(null),
            DependsOn("TokenizedAuthProvider")]
        public static string? AuthProviderTokenizedAlternativeCheckUrl
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the user approval token.</summary>
        /// <value>The user approval token.</value>
        [AppSettingsKey("Clarity.API.Auth.User.Approval.Token"),
            DefaultValue(null),
            MutuallyExclusiveWith(nameof(NewUsersAreDefaultApproved), nameof(LoginEnabled))]
        public static string? UserApprovalToken
        {
            get => !NewUsersAreDefaultApproved && LoginEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the invitation token.</summary>
        /// <value>The invitation token.</value>
        [AppSettingsKey("Clarity.API.Auth.Invitation.Token"),
            DefaultValue(null),
            DependsOn(nameof(LoginEnabled))]
        public static string? InvitationToken
        {
            get => LoginEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the re captcha secret.</summary>
        /// <value>The re captcha secret.</value>
        [AppSettingsKey("Clarity.API.Auth.ReCaptchaSecret"),
            DefaultValue(null),
            DependsOn(nameof(LoginEnabled))]
        public static string? ReCaptchaSecret
        {
            get => LoginEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the create referral code during registration.</summary>
        /// <value>True if create referral code during registration, false if not.</value>
        [AppSettingsKey("Clarity.API.Auth.CreateReferralCodeDuringRegistration"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled))]
        public static bool CreateReferralCodeDuringRegistration
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the new users are default active.</summary>
        /// <value>True if new users are default active, false if not.</value>
        [AppSettingsKey("Clarity.UserRegistration.Active"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled))]
        public static bool NewUsersAreDefaultActive
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the new users are default approved.</summary>
        /// <value>True if new users are default approved, false if not.</value>
        [AppSettingsKey("Clarity.UserRegistration.IsApproved"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled))]
        public static bool NewUsersAreDefaultApproved
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the new users are default locked out.</summary>
        /// <value>True if new users are default locked out, false if not.</value>
        [AppSettingsKey("Clarity.UserRegistration.LockoutEnabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled))]
        public static bool NewUsersAreDefaultLockedOut
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the new users are default locked out.</summary>
        /// <value>True if new users are default locked out, false if not.</value>
        [AppSettingsKey("Clarity.UserRegistration.CustomUserApproval"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled))]
        public static bool CustomUserApproval
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the new users gain default role.</summary>
        /// <value>True if new users gain default role, false if not.</value>
        [AppSettingsKey("Clarity.UserRegistration.AddDefaultRole"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled))]
        public static bool NewUsersGainDefaultRole
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the default user role.</summary>
        /// <value>The default user role.</value>
        [AppSettingsKey("Clarity.UserRegistration.DefaultRole"),
            DefaultValue("CEF User"),
            DependsOn(nameof(NewUsersGainDefaultRole))]
        public static string? DefaultUserRole
        {
            get => NewUsersGainDefaultRole ? TryGet(out string? asValue) ? asValue : "CEF User" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the new users allow lookup existing account on registration.</summary>
        /// <value>True if new users allow lookup existing account on registration, false if not.</value>
        [AppSettingsKey("Clarity.UserRegistration.AllowLookupExistingAccount"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled))]
        public static bool NewUsersAllowLookupExistingAccountOnRegistration
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the new users allow lookup existing account on registration.</summary>
        /// <value>True if new users allow lookup existing account on registration, false if not.</value>
        [AppSettingsKey("Clarity.UserRegistration.ShouldQueueEmailOnNewUserCreated.BackOffice"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled))]
        public static bool ShouldQueueEmailOnNewUserCreatedBackOffice
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether we should queue password reset email on set user as active.</summary>
        /// <value>True if we should queue password reset email on set user as active, false if not.</value>
        [AppSettingsKey("Clarity.Users.ShouldQueuePasswordResetEmailOnSetUserAsActive"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled))]
        public static bool ShouldQueuePasswordResetEmailOnSetUserAsActive
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether new users are required to verify email before logging in.</summary>
        /// <value>True if email verification is required, false if not.</value>
        [AppSettingsKey("Clarity.Users.RequireEmailVerificationForNewUsers"),
            DefaultValue(false)]
        public static bool RequireEmailVerificationForNewUsers
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region AWS
        /// <summary>Gets the uploads the ws credential profile.</summary>
        /// <value>The uploads the ws credential profile.</value>
        [AppSettingsKey("Clarity.Uploads.AWS.CredentialProfile"),
         DefaultValue(null),
         Unused,
         DependsOn("AWSFilesProvider")]
        public static string? UploadsAWSCredentialProfile
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the identifier of the uploads the ws default access key.</summary>
        /// <value>The identifier of the uploads the ws default access key.</value>
        [AppSettingsKey("Clarity.Uploads.AWS.DefaultAccessKeyId"),
         DefaultValue(null),
         DependsOn("AWSFilesProvider")]
        public static string? UploadsAWSDefaultAccessKeyId
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the uploads the ws default bucket.</summary>
        /// <value>A Bucket for uploads the ws default data.</value>
        [AppSettingsKey("Clarity.Uploads.AWS.DefaultBucket"),
         DefaultValue(null),
         DependsOn("AWSFilesProvider")]
        public static string? UploadsAWSDefaultBucket
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the uploads the ws default canned a cl.</summary>
        /// <value>The uploads the ws default canned a cl.</value>
        [AppSettingsKey("Clarity.Uploads.AWS.DefaultCannedACL"),
         DefaultValue(null),
         DependsOn("AWSFilesProvider")]
        public static string? UploadsAWSDefaultCannedACL
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the pathname of the uploads the ws default folder.</summary>
        /// <value>The pathname of the uploads the ws default folder.</value>
        [AppSettingsKey("Clarity.Uploads.AWS.DefaultFolder"),
         DefaultValue(null),
         DependsOn("AWSFilesProvider")]
        public static string? UploadsAWSDefaultFolder
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the uploads the ws default profile.</summary>
        /// <value>The uploads the ws default profile.</value>
        [AppSettingsKey("Clarity.Uploads.AWS.DefaultProfile"),
         DefaultValue(null),
         DependsOn("AWSFilesProvider")]
        public static string? UploadsAWSDefaultProfile
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the uploads the ws default region endpoint.</summary>
        /// <value>The uploads the ws default region endpoint.</value>
        [AppSettingsKey("Clarity.Uploads.AWS.DefaultRegionEndpoint"),
         DefaultValue(null),
         DependsOn("AWSFilesProvider")]
        public static string? UploadsAWSDefaultRegionEndpoint
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the uploads the ws default secret access key.</summary>
        /// <value>The uploads the ws default secret access key.</value>
        [AppSettingsKey("Clarity.Uploads.AWS.DefaultSecretAccessKey"),
         DefaultValue(null),
         DependsOn("AWSFilesProvider")]
        public static string? UploadsAWSDefaultSecretAccessKey
        {
            get => TryGet(out string? asValue) ? asValue : null;
            private set => TrySet(value);
        }
        #endregion

        #region Badges
        /// <summary>Gets a value indicating whether the badges is enabled.</summary>
        /// <value>True if badges enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Badges.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(StoresEnabled))]
        public static bool BadgesEnabled
        {
            get => StoresEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Brands
        /// <summary>Gets a value indicating whether the brands is enabled.</summary>
        /// <value>True if brands enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Brands.Enabled"),
         DefaultValue(false)]
        public static bool BrandsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether products should be filtered by brand when searching.</summary>
        /// <value>True if RemoveBrandCatalogSearchFilter enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Brands.RemoveBrandCatalogSearchFilter"),
         DefaultValue(false)]
        public static bool RemoveBrandCatalogSearchFilter
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the brands site domains is enabled.</summary>
        /// <value>True if brands site domains enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Brands.SiteDomains.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(BrandsEnabled))]
        public static bool BrandsSiteDomainsEnabled
        {
            get => BrandsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Calendar Events
        /// <summary>Gets a value indicating whether the calendar events is enabled.</summary>
        /// <value>True if calendar events enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.CalendarEvents.Enabled"),
         DefaultValue(false)]
        public static bool CalendarEventsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the calendar events change package limit in days.</summary>
        /// <value>The calendar events change package limit in days.</value>
        [AppSettingsKey("Clarity.CalendarEvents.ChangePackageLimitInDays"),
         DefaultValue(30),
         DependsOn(nameof(CalendarEventsEnabled))]
        public static int CalendarEventsChangePackageLimitInDays
        {
            get => CalendarEventsEnabled ? TryGet(out int asValue) ? asValue : 30 : 0;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the user event attendances send group leader emails on create.</summary>
        /// <value>True if user event attendances send group leader emails on create, false if not.</value>
        [AppSettingsKey("Clarity.UserEventAttendances.SendGroupLeaderEmailsOnCreate"),
         DefaultValue(false),
         DependsOn(nameof(CalendarEventsEnabled))]
        public static bool UserEventAttendancesSendGroupLeaderEmailsOnCreate
        {
            get => CalendarEventsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Cart Validator
        /// <summary>Gets a value indicating whether the cart validation do product restrictions.</summary>
        /// <value>True if cart validation do product restrictions, false if not.</value>
        [AppSettingsKey("Clarity.CartValidation.DoProductRestrictions"),
         DefaultValue(false),
         DependsOn(nameof(CartsEnabled))]
        public static bool CartValidationDoProductRestrictions
        {
            get => CartsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the cart validation limits products to a single store.</summary>
        /// <value>True if cart validation single store, false if not.</value>
        [AppSettingsKey("Clarity.CartValidation.SingleStoreInCartEnabled"),
         DefaultValue(false),
         DependsOn(nameof(CartsEnabled))]
        public static bool CartValidationSingleStoreInCartEnabled
        {
            get => CartsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the maximum allowed number of items in a cart.</summary>
        /// <value>True if cart contains maximum number of items or fewer, false if not.</value>
        [AppSettingsKey("Clarity.CartValidation.MaximumCartItems"),
         DefaultValue(9),
         DependsOn(nameof(CartsEnabled))]
        public static int CartValidationMaximumCartItems
        {
            get => CartsEnabled ? TryGet(out int asValue) ? asValue : 9 : 9;
            private set => TrySet(value);
        }

        /// <summary>Gets the cart validation product restrictions keys.</summary>
        /// <value>The cart validation product restrictions keys.</value>
        [AppSettingsKey("Clarity.CartValidation.ProductRestrictionsKeys"),
         DefaultValue(null),
         DependsOn(nameof(CartValidationDoProductRestrictions))]
        public static string? CartValidationProductRestrictionsKeys
        {
            get => CartValidationDoProductRestrictions ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }
        #endregion

        #region Carts
        /// <summary>Gets a value indicating whether the carts is enabled.</summary>
        /// <value>True if carts enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.Enabled"),
            DefaultValue(true)]
        public static bool CartsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the carts angular service debugging is enabled.</summary>
        /// <value>True if carts angular service debugging enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.NgServiceDebugging.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(CartsEnabled))]
        public static bool CartsAngularServiceDebuggingEnabled
        {
            get => CartsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Site-wide enabling of all modals that result from adding an item to the cart.</summary>
        /// <value>True if add to cart modals enabled, false if not.</value>
        [AppSettingsKey("Clarity.UI.AddToCartModals.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(CartsEnabled))]
        public static bool AddToCartModalsEnabled
        {
            get => CartsEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the confirm add to quote cart modal should be shown.</summary>
        /// <value>True if confirm modal should be shown, false if not.</value>
        [AppSettingsKey("Clarity.UI.AddToQuoteCartModal.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(SalesQuotesEnabled))]
        public static bool AddToQuoteCartModalIsEnabled
        {
            get => SalesQuotesEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the carts wish list is enabled.</summary>
        /// <value>True if carts wish list enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.WishList.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(CartsEnabled), nameof(MyDashboardEnabled))]
        public static bool CartsWishListEnabled
        {
            get => LoginEnabled && CartsEnabled && MyDashboardEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the carts favorites list is enabled.</summary>
        /// <value>True if carts favorites list enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.FavoritesList.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(CartsEnabled), nameof(MyDashboardEnabled))]
        public static bool CartsFavoritesListEnabled
        {
            get => LoginEnabled && CartsEnabled && MyDashboardEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the carts notify me when in stock list is enabled.</summary>
        /// <value>True if carts notify me when in stock list enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.NotifyMeWhenInStock.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(CartsEnabled), nameof(InventoryEnabled))]
        public static bool CartsNotifyMeWhenInStockListEnabled
        {
            get => LoginEnabled && CartsEnabled && InventoryEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the carts shopping lists is enabled.</summary>
        /// <value>True if carts shopping lists enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.ShoppingLists.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(CartsEnabled), nameof(MyDashboardEnabled))]
        public static bool CartsShoppingListsEnabled
        {
            get => LoginEnabled && CartsEnabled && MyDashboardEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether cart types should be generated with a unique Guid for its key and name.</summary>
        /// <value>True if generate and assign Guid for cart type key and name, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.ShoppingLists.GenerateAndAssignGuidForCartTypeKeyAndName"),
            DefaultValue(false),
            DependsOn(nameof(CartsShoppingListsEnabled))]
        public static bool GenerateAndAssignGuidForCartTypeKeyAndName
        {
            get => CartsShoppingListsEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether cart types should be generated with a unique Guid for its key and name.</summary>
        /// <value>True if generate and assign Guid for cart type key and name, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.ShoppingLists.AssignNameToAndShowDisplayName"),
            DefaultValue(false),
            DependsOn(nameof(CartsShoppingListsEnabled))]
        public static bool AssignNameToAndShowDisplayName
        {
            get => CartsShoppingListsEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the carts order requests is enabled.</summary>
        /// <value>True if carts order requests enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.OrderRequests.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(LoginEnabled), nameof(CartsEnabled), nameof(MyDashboardEnabled))]
        public static bool CartsOrderRequestsEnabled
        {
            get => LoginEnabled && CartsEnabled && MyDashboardEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the carts compare is enabled.</summary>
        /// <value>True if carts compare enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.Compare.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(CartsEnabled))]
        public static bool CartsCompareEnabled
        {
            get => CartsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the explode kits added to cart.</summary>
        /// <value>True if explode kits added to cart, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Carts.ExplodeKitsAddedToCart"),
            DefaultValue(false),
            DependsOn(nameof(CartsEnabled))]
        public static bool ExplodeKitsAddedToCart
        {
            get => CartsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Catalog
        /// <summary>Search Catalog: How many levels of depth into Categories to show before we can display products.</summary>
        /// <value>The catalog show product categories for levels up to x coordinate.</value>
        [AppSettingsKey("Clarity.Catalog.ShowProductCategoriesForLevelsUpToX"),
         DefaultValue(0),
         DependsOn(nameof(CategoriesEnabled)),
         MutuallyExclusiveWith("The separated Categories landing pages for DNN")]
        public static int CatalogShowProductCategoriesForLevelsUpToX
        {
            get => CategoriesEnabled ? TryGet(out int asValue) ? asValue : 0 : 0;
            private set => TrySet(value);
        }

        /// <summary>Search Catalog: The default page size to start with.</summary>
        /// <value>The size of the catalog default page.</value>
        [AppSettingsKey("Clarity.Catalog.DefaultPageSize"),
         DefaultValue(9)]
        public static int CatalogDefaultPageSize
        {
            get => TryGet(out int asValue) ? asValue : 9;
            private set => TrySet(value);
        }

        /// <summary>Search Catalog: The default format (layout) to start with.</summary>
        /// <value>The catalog default format.</value>
        [AppSettingsKey("Clarity.Catalog.DefaultFormat"),
         DefaultValue("grid")]
        public static string CatalogDefaultFormat
        {
            get => TryGet(out string asValue) ? asValue : "grid";
            private set => TrySet(value);
        }

        /// <summary>Search Catalog: The default Sort method to start with.</summary>
        /// <value>The catalog default sort.</value>
        [AppSettingsKey("Clarity.Catalog.DefaultSort"),
         DefaultValue("Relevance")]
        public static string CatalogDefaultSort
        {
            get => TryGet(out string asValue) ? asValue : "Relevance";
            private set => TrySet(value);
        }

        /// <summary>Search Catalog: Apply the Store ID to the search only if the user has selected it via UI. When false,
        /// Store ID will always be forced onto the search from the user's selected Store.</summary>
        /// <value>True if catalog only apply store to filter by user interface, false if not.</value>
        [AppSettingsKey("Clarity.Catalog.OnlyApplyStoreToFilterByUI"),
         DefaultValue(true),
         DependsOn(nameof(StoresEnabled))]
        public static bool CatalogOnlyApplyStoreToFilterByUI
        {
            get => StoresEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Search Catalog: Sets the visibility setting for category landing page images.</summary>
        /// <value>True if catalog display images, false if not.</value>
        [AppSettingsKey("Clarity.Catalog.DisplayImages"),
         DefaultValue(true)]
        public static bool CatalogDisplayImages
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Search Catalog: If true, any associations on products returned from the search will have their data
        /// loaded to the services.cvProductService for use.</summary>
        /// <remarks>Warning! This is a performance intensive action if there are many associations (such as variants) on
        /// each product.</remarks>
        /// <value>True if catalog get full associated products information, false if not.</value>
        [AppSettingsKey("Clarity.Catalog.GetFullAssociatedProductsInfo"),
         DefaultValue(false)]
        public static bool CatalogGetFullAssociatedProductsInfo
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Search Catalog: Enables ticket exchange information on the search Catalog.</summary>
        /// <value>True if catalog ticket exchange, false if not.</value>
        [AppSettingsKey("Clarity.Catalog.TicketExchange"),
         DefaultValue(false)]
        public static bool CatalogTicketExchangeEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Search Catalog: Limits the number of top-level categories displayed in the catalog filter accordion
        /// on the catalog.</summary>
        /// <value>The maximum number of top-level categories to display in the catalog filter.</value>
        [AppSettingsKey("Clarity.Catalog.MaxTopLevelCategoriesInFilter"),
         DefaultValue(20)]
        public static int MaxTopLevelCategoriesInCatalogFilter
        {
            get => TryGet(out int asValue) ? asValue : 20;
            private set => TrySet(value);
        }
        #endregion

        #region Categories
        /// <summary>Gets a value indicating whether the categories is enabled.</summary>
        /// <value>True if categories enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Categories.Enabled"),
            DefaultValue(true)]
        public static bool CategoriesEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the categories do restrictions by minimum maximum (hard/soft stops).</summary>
        /// <value>True if categories do restrictions by minimum maximum, false if not (hard/soft stops).</value>
        [AppSettingsKey("Clarity.Carts.Validation.DoCategoryRestrictionsByMinMax"),
            DefaultValue(false)]
        public static bool CategoriesDoRestrictionsByMinMax
        {
            get => CategoriesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Charges and Fees
        /// <summary>Gets the charges handling for non 0 cost or weight orders.</summary>
        /// <value>The charges handling for non 0 cost or weight orders.</value>
        [AppSettingsKey("Clarity.Charges.HandlingForNon0CostOrWeightOrders"),
         DefaultValue(null),
         DependsOn(nameof(ShippingEnabled))]
        public static decimal? ChargesHandlingForNon0CostOrWeightOrders
        {
            get => ShippingEnabled ? TryGet(out decimal? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }
        #endregion

        #region Chat/Messaging
        /// <summary>Gets a value indicating whether the chat is enabled.</summary>
        /// <value>True if chat enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Chat.Enabled"),
         DefaultValue(false)]
        public static bool ChatEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the messaging is enabled.</summary>
        /// <value>True if messaging enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Messaging.Enabled"),
         DefaultValue(false)]
        public static bool MessagingEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion
    }
}
