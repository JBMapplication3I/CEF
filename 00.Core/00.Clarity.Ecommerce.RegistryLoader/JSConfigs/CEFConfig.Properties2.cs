// <copyright file="CEFConfig.Properties2.cs" company="clarity-ventures.com">
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
        #region Cookies
        /// <summary>For enforcing cookies to be sharable by not including the left-most sub-domain (or some other
        /// combination value).</summary>
        /// <value>The cookies domain.</value>
        [AppSettingsKey("Clarity.API.Cookies.Domain"),
         DefaultValue(null)]
        public static string CookiesDomain
        {
            get => TryGet(out string asValue)
                ? asValue
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                : throw new System.Configuration.ConfigurationErrorsException(
                    "Clarity.API.Cookies.Domain is required in appSettings.config");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the cookies file.</summary>
        /// <value>The full pathname of the cookies file.</value>
        [AppSettingsKey("Clarity.API.Cookies.Path"),
         DefaultValue("/")]
        public static string CookiesPath
        {
            get => TryGet(out string asValue) ? asValue : "/";
            private set => TrySet(value);
        }

        /// <summary>When true, will not remove the subdomain from the domain when setting cookies.</summary>
        /// <value>True if cookies use sub domain, false if not.</value>
        /// <example>shop.mysite.com will stay as shop.mysite.com.</example>
        [AppSettingsKey("Clarity.API.Cookies.UseSubDomain"),
         DefaultValue(true)]
        public static bool CookiesUseSubDomain
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>The number of segments to consider shared (counting from the right). If there are more segments in
        /// the domain than this count, they will be ignored in cookie domain paths for sharing.</summary>
        /// <value>The number of cookies use sub domain segments.</value>
        /// <remarks>Only applicable if <see cref="CookiesUseSubDomain" /> is false.</remarks>
        /// <example>
        /// * with a value of 2, shop.mysite.com will become .mysite.com
        /// * with a value of 2, sub.shop.mysite.com will become .mysite.com
        /// * with a value of 3, sub.shop.mysite.com will become shop.mysite.com.
        /// </example>
        [AppSettingsKey("Clarity.API.Cookies.UseSubDomain.SegmentCount"),
         DefaultValue(2),
         MutuallyExclusiveWith(nameof(CookiesUseSubDomain))]
        public static int CookiesUseSubDomainSegmentCount
        {
            get => TryGet(out int asValue) ? asValue : 2;
            private set => TrySet(value);
        }

        /// <summary>If true, every request validates every cookie is set to Secure Only and returns 403 if any fail this
        /// check.</summary>
        /// <value>True if cookies require secure, false if not.</value>
        [AppSettingsKey("Clarity.API.Cookies.RequireSecure"),
         DefaultValue(false)]
        public static bool CookiesRequireSecure
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the cookies require secure ignored cef.</summary>
        /// <value>The cookies require secure ignored cef.</value>
        [AppSettingsKey("Clarity.API.Cookies.RequireSecure.IgnoredCEFCookies"),
         DefaultValue("cefCoords,cefGeo,cefLocationRequested,CURRENCY_KEY,NG_TRANSLATE_LANG_KEY,ss-id,ss-pid,ss-opt,x-uaid,_ga"),
         SplitOn(new[] { ',' })]
        public static string[] CookiesRequireSecureIgnoredCEF
        {
            get => TryGet(out string[] asValue) ? asValue : Array.Empty<string>();
            private set => TrySet(value);
        }

        /// <summary>If true, every request validates every cookie is set to HTTP Only and returns 403 if any fail this
        /// check.</summary>
        /// <value>True if cookies require HTTP only, false if not.</value>
        [AppSettingsKey("Clarity.API.Cookies.RequireHTTPOnly"),
         DefaultValue(false)]
        public static bool CookiesRequireHTTPOnly
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the cookies require HTTP only ignored cef.</summary>
        /// <value>The cookies require HTTP only ignored cef.</value>
        [AppSettingsKey("Clarity.API.Cookies.RequireHTTPOnly.IgnoredCEFCookies"),
         DefaultValue("cefCoords,cefGeo,cefLocationRequested,CURRENCY_KEY,NG_TRANSLATE_LANG_KEY,ss-id,ss-pid,ss-opt,x-uaid"),
         SplitOn(new[] { ',' }),
         DependsOn(nameof(CookiesRequireHTTPOnly))]
        public static string[] CookiesRequireHTTPOnlyIgnoredCEF
        {
            get => CookiesRequireHTTPOnly
                ? TryGet(out string[] asValue) ? asValue : Array.Empty<string>()
                : Array.Empty<string>();
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the cookies validate authentication values every request.</summary>
        /// <value>True if cookies validate authentication values every request, false if not.</value>
        [AppSettingsKey("Clarity.API.Cookies.ValidateAuthValuesEveryRequest"),
         DefaultValue(true),
         DependsOn(nameof(LoginEnabled))]
        public static bool CookiesValidateAuthValuesEveryRequest
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the cookies ignored non cef.</summary>
        /// <value>The cookies ignored non cef.</value>
        [AppSettingsKey("Clarity.API.Cookies.IgnoredNonCEFCookies"),
         DefaultValue(".ASPXANONYMOUS,dnn_IsMobile,language,us_lang"),
         SplitOn(new[] { ',' })]
        public static string[]? CookiesIgnoredNonCEF
        {
            get => TryGet(out string[]? asValue) ? asValue : null;
            private set => TrySet(value);
        }
        #endregion

        #region Dashboard
        /// <summary>Turn off/on dashboard features.</summary>
        /// <value>The dashboard page configuration.</value>
        [DependsOn(nameof(MyDashboardEnabled))]
        public static DashboardPageConfig[] DashboardPageConfig
        {
            get => MyDashboardEnabled
                ? TryGet<DashboardPageConfig[]>(out var asValue)
                    ? asValue
                    : Array.Empty<DashboardPageConfig>()
                : Array.Empty<DashboardPageConfig>();
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether my dashboard is enabled.</summary>
        /// <value>True if my dashboard enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.MyDashboard.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled))]
        public static bool MyDashboardEnabled
        {
            get => LoginEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route my dashboard is enabled.</summary>
        /// <value>True if dashboard route my dashboard enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.MyDashboard.Enabled"),
            DefaultValue(true),
            DependsOn(
                nameof(MyDashboardEnabled),
                nameof(SalesOrdersEnabled),
                nameof(SalesQuotesEnabled),
                nameof(SalesInvoicesEnabled))]
        public static bool DashboardRouteMyDashboardEnabled
        {
            get => MyDashboardEnabled && (SalesOrdersEnabled || SalesQuotesEnabled || SalesInvoicesEnabled)
                ? TryGet(out bool asValue) ? asValue : true
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route account settings is enabled.</summary>
        /// <value>True if dashboard route account settings enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.AccountSettings.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(MyDashboardEnabled))]
        public static bool DashboardRouteAccountSettingsEnabled
        {
            get => MyDashboardEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route account settings my profile is enabled.</summary>
        /// <value>True if dashboard route account settings my profile enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.AccountSettings.MyProfile.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(MyDashboardEnabled))]
        public static bool DashboardRouteAccountSettingsMyProfileEnabled
        {
            get => MyDashboardEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route account settings account profile is enabled.</summary>
        /// <value>True if dashboard route account settings account profile enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.AccountSettings.AccountProfile.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(MyDashboardEnabled))]
        public static bool DashboardRouteAccountSettingsAccountProfileEnabled
        {
            get => MyDashboardEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route account settings address book is enabled.</summary>
        /// <value>True if dashboard route account settings address book enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.AccountSettings.AddressBook.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(MyDashboardEnabled), nameof(AddressBookEnabled))]
        public static bool DashboardRouteAccountSettingsAddressBookEnabled
        {
            get => MyDashboardEnabled && AddressBookEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route account settings wallet is enabled.</summary>
        /// <value>True if dashboard route account settings wallet enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.AccountSettings.Wallet.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(MyDashboardEnabled), nameof(PaymentsWalletEnabled))]
        public static bool DashboardRouteAccountSettingsWalletEnabled
        {
            get => MyDashboardEnabled && PaymentsWalletEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route users is enabled.</summary>
        /// <value>True if dashboard route users enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.Users.Enabled"),
                DefaultValue(false),
                DependsOn(nameof(MyDashboardEnabled))]
        public static bool DashboardRouteUsersEnabled
        {
            get => MyDashboardEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the user record stored files is enabled.</summary>
        /// <value>True if user record stored files enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Users.StoredFiles.Enabled"),
            DefaultValue(false)]
        public static bool UserStoredFilesEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route groups is enabled.</summary>
        /// <value>True if dashboard route groups enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.Groups.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled), nameof(SalesGroupsEnabled))]
        public static bool DashboardRouteGroupsEnabled
        {
            get => MyDashboardEnabled && SalesGroupsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route sales orders is enabled.</summary>
        /// <value>True if dashboard route sales orders enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.SalesOrders.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(MyDashboardEnabled), nameof(SalesOrdersEnabled))]
        public static bool DashboardRouteSalesOrdersEnabled
        {
            get => MyDashboardEnabled && SalesOrdersEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        // Sales Invoices Route: See CEFConfig.Properties.SalesInvoices.cs

        // Sales Quotes Route: See CEFConfig.Properties.SalesQuotes.cs

        // Sales Returns Route: See CEFConfig.Properties.SalesReturns.cs

        /// <summary>Gets a value indicating whether the dashboard route subscriptions is enabled.</summary>
        /// <value>True if dashboard route subscriptions enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.Subscriptions.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled))]
        public static bool DashboardRouteSubscriptionsEnabled
        {
            get => MyDashboardEnabled && PaymentsWithSubscriptionsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route sample requests is enabled.</summary>
        /// <value>True if dashboard route sample requests enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.SampleRequests.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled), nameof(SampleRequestsEnabled))]
        public static bool DashboardRouteSampleRequestsEnabled
        {
            get => MyDashboardEnabled && SampleRequestsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route downloads is enabled.</summary>
        /// <value>True if dashboard route downloads enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.Downloads.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled), nameof(ProductStoredFilesEnabled))]
        public static bool DashboardRouteDownloadsEnabled
        {
            get => MyDashboardEnabled && ProductStoredFilesEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route wish list is enabled.</summary>
        /// <value>True if dashboard route wish list enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.WishList.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled), nameof(CartsWishListEnabled))]
        public static bool DashboardRouteWishListEnabled
        {
            get => MyDashboardEnabled && CartsWishListEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route favorites is enabled.</summary>
        /// <value>True if dashboard route favorites enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.Favorites.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled), nameof(CartsFavoritesListEnabled))]
        public static bool DashboardRouteFavoritesEnabled
        {
            get => MyDashboardEnabled && CartsFavoritesListEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route in stock alerts is enabled.</summary>
        /// <value>True if dashboard route in stock alerts enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.InStockAlerts.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled), nameof(CartsNotifyMeWhenInStockListEnabled))]
        public static bool DashboardRouteInStockAlertsEnabled
        {
            get => MyDashboardEnabled && CartsNotifyMeWhenInStockListEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route shopping lists is enabled.</summary>
        /// <value>True if dashboard route shopping lists enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.ShoppingLists.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled), nameof(CartsShoppingListsEnabled))]
        public static bool DashboardRouteShoppingListsEnabled
        {
            get => MyDashboardEnabled && CartsShoppingListsEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route order requests is enabled.</summary>
        /// <value>True if dashboard route order requests enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.OrderRequests.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled), nameof(CartsOrderRequestsEnabled))]
        public static bool DashboardRouteOrderRequestsEnabled
        {
            get => MyDashboardEnabled && CartsOrderRequestsEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the dashboard route inbox is enabled.</summary>
        /// <value>True if dashboard route inbox enabled, false if not.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.Inbox.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(MyDashboardEnabled), nameof(MessagingEnabled))]
        public static bool DashboardRouteInboxEnabled
        {
            get => MyDashboardEnabled && MessagingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the custom dashboard routes.</summary>
        /// <value>The custom dashboard routes.</value>
        [AppSettingsKey("Clarity.Dashboard.Route.CustomRoutes"),
            DefaultValue(""),
            DependsOn(nameof(MyDashboardEnabled))]
        public static string? CustomDashboardRoutes
        {
            get => MyDashboardEnabled ? TryGet(out string? asValue) ? asValue : string.Empty : string.Empty;
            private set => TrySet(value);
        }

        #region My Profile
        /// <summary>Gets a value indicating whether my profile images is enabled.</summary>
        /// <value>True if my profile images enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Profile.Images.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(DashboardRouteAccountSettingsMyProfileEnabled))]
        public static bool MyProfileImagesEnabled
        {
            get => DashboardRouteAccountSettingsMyProfileEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether my profile stored files is enabled.</summary>
        /// <value>True if my profile stored files enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Profile.StoredFiles.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(DashboardRouteAccountSettingsMyProfileEnabled))]
        public static bool MyProfileStoredFilesEnabled
        {
            get => DashboardRouteAccountSettingsMyProfileEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Account Profile
        /// <summary>Gets a value indicating whether the account profile images is enabled.</summary>
        /// <value>True if account profile images enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.AccountProfile.Images.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(DashboardRouteAccountSettingsAccountProfileEnabled))]
        public static bool AccountProfileImagesEnabled
        {
            get => DashboardRouteAccountSettingsAccountProfileEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the account profile stored files is enabled.</summary>
        /// <value>True if account profile stored files enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.AccountProfile.StoredFiles.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(DashboardRouteAccountSettingsAccountProfileEnabled))]
        public static bool AccountProfileStoredFilesEnabled
        {
            get => DashboardRouteAccountSettingsAccountProfileEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }
        #endregion
        #endregion

        #region Discounts
        /// <summary>Gets a value indicating whether the discounts is enabled.</summary>
        /// <value>True if discounts enabled, false if not.</value>
        [AppSettingsKey("Clarity.Discounts.Enabled"),
            DefaultValue(true)]
        public static bool DiscountsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region Emails
        /// <summary>Gets a value indicating whether the emails is enabled.</summary>
        /// <value>True if emails enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Emails.Enabled"),
            DefaultValue(true)]
        public static bool EmailsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the emails with split templates is enabled.</summary>
        /// <value>True if emails with split templates enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Emails.SplitTemplates.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(EmailsEnabled))]
        public static bool EmailsWithSplitTemplatesEnabled
        {
            get => EmailsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the email forgot username subject.</summary>
        /// <value>The email forgot username subject.</value>
        [AppSettingsKey("Clarity.Authentication.ForgotUsername.Subject"),
            DefaultValue("Your {{CompanyName}} Username"),
            DependsOn(nameof(EmailsEnabled))]
        public static string EmailForgotUsernameSubject
        {
            get => EmailsEnabled
                ? TryGet(out string asValue) ? asValue : "Your {{CompanyName}} Username"
                : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets the full pathname of the email forgot username body template file.</summary>
        /// <value>The full pathname of the email forgot username body template file.</value>
        [AppSettingsKey("Clarity.Authentication.ForgotUsername.BodyTemplatePath"),
            DefaultValue("ForgotUsername.html"),
            DependsOn(nameof(EmailsEnabled))]
        public static string EmailForgotUsernameBodyTemplatePath
        {
            get => EmailsEnabled ? TryGet(out string asValue) ? asValue : "ForgotUsername.html" : string.Empty;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the email notifications messaging email copies is enabled.</summary>
        /// <value>True if email notifications messaging email copies enabled, false if not.</value>
        [AppSettingsKey("Clarity.Notifications.Messaging.EnableEmailCopies"),
            DefaultValue(false),
            DependsOn(nameof(EmailsEnabled))]
        public static bool EmailNotificationsMessagingEmailCopiesEnabled
        {
            get => EmailsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the email notifications sales order to customer by email on save.</summary>
        /// <value>True if email notifications sales order to customer by email on save, false if not.</value>
        [AppSettingsKey("Clarity.Notifications.SalesOrders.Customer.ByEmailOnSave"),
            DefaultValue(false),
            DependsOn(nameof(EmailsEnabled))]
        public static bool EmailNotificationsSalesOrderToCustomerByEmailOnSave
        {
            get => EmailsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the email defaults back office email address.</summary>
        /// <value>The email defaults back office email address.</value>
        [AppSettingsKey("Clarity.Emails.Defaults.BackOfficeEmail"),
            DefaultValue(null),
            DependsOn(nameof(EmailsEnabled))]
        public static string? EmailDefaultsBackOfficeEmailAddress
        {
            get => EmailsEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the email notifications user profile updated to backoffice by email on save.</summary>
        /// <value>True if email notifications user profile updated to backoffice by email on save, false if not.</value>
        [AppSettingsKey("Clarity.Notifications.UserProfile.BackOffice.ByEmailOnSave"),
            DefaultValue(false),
            DependsOn(nameof(EmailsEnabled))]
        public static bool EmailNotificationsUserProfileUpdatedToBackOfficeByEmailOnSave
        {
            get => EmailsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the email defaults attachment location.</summary>
        /// <value>The email defaults attachment location.</value>
        [AppSettingsKey("Clarity.Emails.Defaults.AttachmentLocation"),
            DefaultValue(null),
            DependsOn(nameof(EmailsEnabled))]
        public static string? EmailDefaultsAttachmentLocation
        {
            get => EmailsEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the forgot username email from.</summary>
        /// <value>The forgot username email from.</value>
        [AppSettingsKey("Clarity.ForgotUsername.EmailFrom"),
            DefaultValue("no-reply@claritydemos.com"),
            DependsOn(nameof(LoginEnabled))]
        public static string? ForgotUsernameEmailFrom
        {
            get => LoginEnabled ? TryGet(out string? asValue) ? asValue : "no-reply@claritydemos.com" : null;
            private set => TrySet(value);
        }
        #endregion

        #region Favorites (Other than Products)
        /// <summary>Gets a value indicating whether the non product favorites is enabled.</summary>
        /// <value>True if non product favorites enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.NonProductFavorites.Enabled"),
         DefaultValue(false)]
        public static bool NonProductFavoritesEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region Franchises
        /// <summary>Gets a value indicating whether the brands is enabled.</summary>
        /// <value>True if brands enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Franchises.Enabled"),
         DefaultValue(false)]
        public static bool FranchisesEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the franchises site domains is enabled.</summary>
        /// <value>True if franchises site domains enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Franchises.SiteDomains.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(FranchisesEnabled))]
        public static bool FranchisesSiteDomainsEnabled
        {
            get => FranchisesEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Geography
        /// <summary>Gets a value indicating whether the do automatic update latitude longitude.</summary>
        /// <value>True if do automatic update latitude longitude, false if not.</value>
        [AppSettingsKey("Clarity.Addresses.DoAutoUpdateLatitudeLongitude"),
         DefaultValue(false)]
        public static bool DoAutoUpdateLatitudeLongitude
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region Google
        /// <summary>The Google Maps API Key.</summary>
        /// <value>The google maps API key.</value>
        [AppSettingsKey("Clarity.Google.Maps.APIKey"),
         DefaultValue("AIzaSyDgmQtEU6ODdXWW1mlliVNqFuwGGeBRoQU")]
        public static string GoogleMapsAPIKey
        {
            get => TryGet(out string asValue) ? asValue : "AIzaSyDgmQtEU6ODdXWW1mlliVNqFuwGGeBRoQU";
            private set => TrySet(value);
        }

        /// <summary>The Google general API Key.</summary>
        /// <value>The google API key.</value>
        [AppSettingsKey("Clarity.Google.APIKey"),
         DefaultValue("AIzaSyBVTDAWycjSqP_tGKLSXO66_K7JSoYF5VQ")]
        public static string GoogleAPIKey
        {
            get => TryGet(out string asValue) ? asValue : "AIzaSyBVTDAWycjSqP_tGKLSXO66_K7JSoYF5VQ";
            private set => TrySet(value);
        }

        /// <summary>The Google API Client ID/Key/Secret.</summary>
        /// <value>The Google API client key.</value>
        [AppSettingsKey("Clarity.Google.APIClientKey"),
         DefaultValue("585747101560-6ccfcfcpg89hoq560qtj8f5eklb0mi4l.apps.googleusercontent.com")]
        public static string GoogleAPIClientKey
        {
            get => TryGet(out string asValue)
                ? asValue
                : "585747101560-6ccfcfcpg89hoq560qtj8f5eklb0mi4l.apps.googleusercontent.com";
            private set => TrySet(value);
        }
        #endregion

        #region Inventory
        /// <summary>Gets a value indicating whether the inventory is enabled.</summary>
        /// <value>True if inventory enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.Enabled"),
            DefaultValue(true)]
        public static bool InventoryEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the inventory back order is enabled.</summary>
        /// <value>True if inventory back order enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.BackOrder.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(InventoryEnabled))]
        public static bool InventoryBackOrderEnabled
        {
            get => InventoryEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the inventory back order maximum per product global is enabled.</summary>
        /// <value>True if inventory back order maximum per product global enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.BackOrder.MaxPerProduct.Global.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(InventoryBackOrderEnabled))]
        public static bool InventoryBackOrderMaxPerProductGlobalEnabled
        {
            get => InventoryBackOrderEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the inventory back order maximum per product per account is
        /// enabled.</summary>
        /// <value>True if inventory back order maximum per product per account enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.BackOrder.MaxPerProduct.PerAccount.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(InventoryBackOrderEnabled))]
        public static bool InventoryBackOrderMaxPerProductPerAccountEnabled
        {
            get => InventoryBackOrderEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the inventory back order maximum per product per account after is
        /// enabled.</summary>
        /// <value>True if inventory back order maximum per product per account after enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.BackOrder.MaxPerProduct.PerAccount.After.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(InventoryBackOrderMaxPerProductPerAccountEnabled))]
        public static bool InventoryBackOrderMaxPerProductPerAccountAfterEnabled
        {
            get => InventoryBackOrderMaxPerProductPerAccountEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the inventory pre sale is enabled.</summary>
        /// <value>True if inventory pre sale enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.PreSale.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(InventoryEnabled))]
        public static bool InventoryPreSaleEnabled
        {
            get => InventoryEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the inventory pre sale maximum per product global is enabled.</summary>
        /// <value>True if inventory pre sale maximum per product global enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.PreSale.MaxPerProduct.Global.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(InventoryPreSaleEnabled))]
        public static bool InventoryPreSaleMaxPerProductGlobalEnabled
        {
            get => InventoryPreSaleEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the inventory pre sale maximum per product per account is
        /// enabled.</summary>
        /// <value>True if inventory pre sale maximum per product per account enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.PreSale.MaxPerProduct.PerAccount.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(InventoryPreSaleEnabled))]
        public static bool InventoryPreSaleMaxPerProductPerAccountEnabled
        {
            get => InventoryPreSaleEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the inventory pre sale maximum per product per account after is
        /// enabled.</summary>
        /// <value>True if inventory pre sale maximum per product per account after enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.PreSale.MaxPerProduct.PerAccount.After.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(InventoryPreSaleMaxPerProductPerAccountEnabled))]
        public static bool InventoryPreSaleMaxPerProductPerAccountAfterEnabled
        {
            get => InventoryPreSaleMaxPerProductPerAccountEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the inventory advanced is enabled.</summary>
        /// <value>True if inventory advanced enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.Advanced.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(InventoryEnabled))]
        public static bool InventoryAdvancedEnabled
        {
            get => InventoryEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the login for inventory is enabled.</summary>
        /// <value>True if login for inventory enabled, false if not.</value>
        [AppSettingsKey("Clarity.Inventory.LoginForInventory.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(InventoryEnabled), nameof(LoginEnabled))]
        public static bool LoginForInventoryEnabled
        {
            get => LoginEnabled && InventoryEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether unlimited inventory is enabled.</summary>
        /// <value>True if unlimited inventory is enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Inventory.Unlimited.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(InventoryEnabled))]
        public static bool UnlimitedInventoryEnabled
        {
            get => InventoryEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether or not to get the closest warehouse with stock.</summary>
        /// <value>True if get closest warehouse with stock, false if not.</value>
        [AppSettingsKey("Clarity.Inventory.Advanced.GetClosestWarehouseWithStock"),
            DefaultValue(false),
            DependsOn(nameof(InventoryAdvancedEnabled))]
        public static bool GetClosestWarehouseWithStock
        {
            get => InventoryAdvancedEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the warehouse priority matrix JSON.</summary>
        /// <value>The warehouse priority matrix JSON.</value>
        [AppSettingsKey("Clarity.Inventory.Advanced.ClosestWarehouseWithStock.RegionServesRegionsListJson"),
            DefaultValue(null),
            DependsOn(nameof(InventoryAdvancedEnabled), nameof(GetClosestWarehouseWithStock))]
        public static string? WarehouseRegionServesRegionsListJson
        {
            get => GetClosestWarehouseWithStock ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the warehouse priority matrix JSON.</summary>
        /// <value>The warehouse priority matrix JSON.</value>
        [AppSettingsKey("Clarity.Inventory.Advanced.ClosestWarehouseWithStock.PriorityListByRegionJson"),
            DefaultValue(null),
            DependsOn(nameof(InventoryAdvancedEnabled), nameof(GetClosestWarehouseWithStock))]
        public static string? WarehousePriorityListByRegionMatrixJSON
        {
            get => GetClosestWarehouseWithStock ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the inventory count only this stores warehouse stock.</summary>
        /// <value>True if inventory count only this stores warehouse stock, false if not.</value>
        [AppSettingsKey("Clarity.Inventory.CountOnlyThisStoresWarehouseStock"),
            DefaultValue(false),
            DependsOn(nameof(StoresEnabled), nameof(InventoryAdvancedEnabled))]
        public static bool InventoryCountOnlyThisStoresWarehouseStock
        {
            get => StoresEnabled && InventoryAdvancedEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Language
        /// <summary>Gets a value indicating whether the languages is enabled.</summary>
        /// <value>True if languages enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Languages.Enabled"),
         DefaultValue(false)]
        public static bool LanguagesEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>The default language to use before the user selects their own.</summary>
        /// <value>The default language.</value>
        [AppSettingsKey("Clarity.Globalization.Language.Default"),
         DefaultValue("en_US")]
        public static string DefaultLanguage
        {
            get => TryGet(out string asValue) ? asValue : "en_US";
            private set => TrySet(value);
        }
        #endregion

        #region Logger
        /// <summary>Gets the name of the logger application.</summary>
        /// <value>The name of the logger application.</value>
        [AppSettingsKey("Clarity.Logger.ApplicationName"),
         DefaultValue("Clarity eCommerce Framework Application")]
        public static string LoggerApplicationName
        {
            get => TryGet(out string asValue) ? asValue : "Clarity eCommerce Framework Application";
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the logger email alerts on.</summary>
        /// <value>True if logger email alerts on, false if not.</value>
        [AppSettingsKey("Clarity.Logger.EmailAlertsOn"),
         DefaultValue(false)]
        public static bool LoggerEmailAlertsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the logger email alerts on error only.</summary>
        /// <value>True if logger email alerts on error only, false if not.</value>
        [AppSettingsKey("Clarity.Logger.EmailAlertsOnErrorOnly"),
         DefaultValue(true),
         DependsOn(nameof(LoggerEmailAlertsEnabled))]
        public static bool LoggerEmailAlertsOnErrorOnly
        {
            get => LoggerEmailAlertsEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the logger email use ssl or TLS.</summary>
        /// <value>True if logger email use ssl or tls, false if not.</value>
        [AppSettingsKey("Clarity.Logger.EmailUseSSLOrTLS"),
         DefaultValue(false),
         DependsOn(nameof(LoggerEmailAlertsEnabled))]
        public static bool LoggerEmailUseSSLOrTLS
        {
            get => LoggerEmailAlertsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the logger email SMTP host.</summary>
        /// <value>The logger email SMTP host.</value>
        [AppSettingsKey("Clarity.Logger.EmailSMTPHost"),
         DefaultValue("10.10.30.82"),
         DependsOn(nameof(LoggerEmailAlertsEnabled))]
        public static string? LoggerEmailSMTPHost
        {
            get => LoggerEmailAlertsEnabled ? TryGet(out string? asValue) ? asValue : "10.10.30.82" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the logger email SMTP port.</summary>
        /// <value>The logger email SMTP port.</value>
        [AppSettingsKey("Clarity.Logger.EmailSMTPPort"),
         DefaultValue(25),
         DependsOn(nameof(LoggerEmailAlertsEnabled))]
        public static int LoggerEmailSMTPPort
        {
            get => LoggerEmailAlertsEnabled ? TryGet(out int asValue) ? asValue : 25 : 0;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the logger email authenticate.</summary>
        /// <value>True if logger email authenticate, false if not.</value>
        [AppSettingsKey("Clarity.Logger.EmailAuthenticate"),
         DefaultValue(false),
         DependsOn(nameof(LoggerEmailAlertsEnabled))]
        public static bool LoggerEmailAuthenticate
        {
            get => LoggerEmailAlertsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the logger email username.</summary>
        /// <value>The logger email username.</value>
        [AppSettingsKey("Clarity.Logger.EmailUsername"),
         DefaultValue(null),
         DependsOn(nameof(LoggerEmailAlertsEnabled))]
        public static string? LoggerEmailUsername
        {
            get => LoggerEmailAlertsEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the logger email password.</summary>
        /// <value>The logger email password.</value>
        [AppSettingsKey("Clarity.Logger.EmailPassword"),
         DefaultValue(null),
         DependsOn(nameof(LoggerEmailAlertsEnabled))]
        public static string? LoggerEmailPassword
        {
            get => LoggerEmailAlertsEnabled ? TryGet(out string? asValue) ? asValue : null : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the logger email from addresses.</summary>
        /// <value>The logger email from addresses.</value>
        [AppSettingsKey("Clarity.Logger.EmailFromAddresses"),
         DefaultValue("no-reply@www.claritydemos.com"),
         DependsOn(nameof(LoggerEmailAlertsEnabled))]
        public static string? LoggerEmailFromAddresses
        {
            get => LoggerEmailAlertsEnabled
                ? TryGet(out string? asValue) ? asValue : "no-reply@www.claritydemos.com"
                : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the logger email recipient addresses.</summary>
        /// <value>The logger email recipient addresses.</value>
        [AppSettingsKey("Clarity.Logger.EmailRecipientAddresses"),
         DefaultValue("alerts@www.claritydemos.com"),
         DependsOn(nameof(LoggerEmailAlertsEnabled))]
        public static string? LoggerEmailRecipientAddresses
        {
            get => LoggerEmailAlertsEnabled
                ? TryGet(out string? asValue) ? asValue : "alerts@www.claritydemos.com"
                : null;
            private set => TrySet(value);
        }
        #endregion

        #region Manufacturers
        /// <summary>Gets a value indicating whether the manufacturers is enabled.</summary>
        /// <value>True if manufacturers enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Manufacturers.Enabled"),
         DefaultValue(false)]
        public static bool ManufacturersEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the manufacturers do restrictions by minimum maximum (hard/soft stops).</summary>
        /// <value>True if manufacturers do restrictions by minimum maximum, false if not (hard/soft stops).</value>
        [AppSettingsKey("Clarity.Carts.Validation.DoManufacturerRestrictionsByMinMax"),
         DefaultValue(false)]
        public static bool ManufacturersDoRestrictionsByMinMax
        {
            get => ManufacturersEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }
        #endregion

        #region Memberships/Subscriptions
        /// <summary>Gets a value indicating whether the payments with memberships is enabled.</summary>
        /// <value>True if payments with memberships enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Payments.Memberships.Enabled"),
         DefaultValue(false)]
        public static bool PaymentsWithMembershipsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments with subscriptions is enabled.</summary>
        /// <value>True if payments with subscriptions enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Payments.Subscriptions.Enabled"),
         DefaultValue(false)]
        public static bool PaymentsWithSubscriptionsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the memberships renewal period before.</summary>
        /// <value>The memberships renewal period before.</value>
        [AppSettingsKey("Clarity.Memberships.Renewal.Period.AllowedUpToXDaysBefore"),
         DefaultValue(30)]
        public static int MembershipsRenewalPeriodBefore
        {
            get => TryGet(out int asValue) ? asValue : 30;
            private set => TrySet(value);
        }

        /// <summary>Gets the memberships renewal period after.</summary>
        /// <value>The memberships renewal period after.</value>
        [AppSettingsKey("Clarity.Memberships.Renewal.Period.AllowedUpToXDaysAfter"),
         DefaultValue(15)]
        public static int MembershipsRenewalPeriodAfter
        {
            get => TryGet(out int asValue) ? asValue : 15;
            private set => TrySet(value);
        }

        /// <summary>Gets the memberships upgrade period blackout.</summary>
        /// <value>The memberships upgrade period blackout.</value>
        [AppSettingsKey("Clarity.Memberships.Upgrade.Period.BlackoutXDaysBefore"),
         DefaultValue(30)]
        public static int MembershipsUpgradePeriodBlackout
        {
            get => TryGet(out int asValue) ? asValue : 30;
            private set => TrySet(value);
        }
        #endregion

        #region MultiCurrency
        /// <summary>Gets a value indicating whether the multi currency is enabled.</summary>
        /// <value>True if multi currency enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.MultiCurrency.Enabled"),
         DefaultValue(false)]
        public static bool MultiCurrencyEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the default currency.</summary>
        /// <value>The default currency.</value>
        [AppSettingsKey("Clarity.Globalization.Currency.Default"),
         DefaultValue("USD")]
        public static string DefaultCurrency
        {
            get => TryGet(out string asValue) ? asValue : "USD";
            private set => TrySet(value);
        }
        #endregion

        #region Ordering
        /// <summary>Gets a value indicating whether order limits enforced is enabled.</summary>
        /// <value>True if enforce order limits enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesOrders.EnforceOrderLimits.Enabled"),
         DefaultValue(false)]
        public static bool EnforceOrderLimits
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether product licensing enforced is enabled.</summary>
        /// <value>True if enforce product licensing enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesOrders.EnforceProductLicensingInCart.Enabled"),
         DefaultValue(false)]
        public static bool EnforceProductLicensingInCart
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales orders is enabled.</summary>
        /// <value>True if sales orders enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesOrders.Enabled"),
         DefaultValue(true)]
        public static bool SalesOrdersEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales orders has integrated keys.</summary>
        /// <value>True if sales orders has integrated keys, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.SalesOrders.HasIntegratedKeys"),
         DefaultValue(false),
         DependsOn(nameof(SalesOrdersEnabled))]
        public static bool SalesOrdersHasIntegratedKeys
        {
            get => SalesOrdersEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the sales order off hold is enabled.</summary>
        /// <value>True if sales order off hold enabled, false if not.</value>
        [AppSettingsKey("Clarity.Ordering.OffHold.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(SalesOrdersEnabled))]
        public static bool SalesOrderOffHoldEnabled
        {
            get => SalesOrdersEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the URL extension resource for sales orders in the fusion API.</summary>
        /// <value>The URL extension resource for sales orders.</value>
        /// <remarks>Sets the URL extension resource for sales orders.</remarks>
        [AppSettingsKey("Clarity.Ordering.UseCustomPriceConversionForCartItems"),
         DefaultValue(false)]
        public static bool UseCustomPriceConversionForCartItems
        {
            get => CEFConfigDictionary.TryGet(out bool asValue) ? asValue : false;
            private set => CEFConfigDictionary.TrySet(value);
        }
        #endregion

        #region Payments
        /// <summary>Gets a value indicating whether the payments is enabled.</summary>
        /// <value>True if payments enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Payments.Enabled"),
            DefaultValue(true)]
        public static bool PaymentsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Site-wide activation of wallet UI and functionality.</summary>
        /// <value>True if payments wallet enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Payments.Wallet.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(LoginEnabled), nameof(PaymentsEnabled))]
        public static bool PaymentsWalletEnabled
        {
            get => LoginEnabled && PaymentsEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments wallet required in registration.</summary>
        /// <value>True if payments wallet required in registration, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Payments.Wallet.RequiredInRegistration"),
            DefaultValue(false),
            DependsOn(nameof(PaymentsWalletEnabled))]
        public static bool PaymentsWalletRequiredInRegistration
        {
            get => PaymentsWalletEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments wallet credit card is enabled.</summary>
        /// <value>True if payments wallet credit card enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Payments.Wallet.CreditCard.Enabled"),
            DefaultValue(true),
            DependsOn(nameof(PaymentsWalletEnabled))]
        public static bool PaymentsWalletCreditCardEnabled
        {
            get => PaymentsWalletEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the payments wallet echeck is enabled.</summary>
        /// <value>True if payments wallet echeck enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Payments.Wallet.eCheck.Enabled"),
            DefaultValue(false),
            DependsOn(nameof(PaymentsWalletEnabled))]
        public static bool PaymentsWalletEcheckEnabled
        {
            get => PaymentsWalletEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments provider mode.</summary>
        /// <value>The payments provider mode.</value>
        [AppSettingsKey("Clarity.Payments.ProviderMode"),
            DefaultValue(Enums.PaymentProviderMode.Testing),
            DependsOn(nameof(PaymentsEnabled))]
        public static Enums.PaymentProviderMode PaymentsProviderMode
        {
            get => PaymentsEnabled
                ? TryGet(out Enums.PaymentProviderMode asValue) ? asValue : Enums.PaymentProviderMode.Testing
                : Enums.PaymentProviderMode.Testing;
            private set => TrySet(value);
        }

        /// <summary>Gets the payments process.</summary>
        /// <value>The payments process.</value>
        [AppSettingsKey("Clarity.Payments.Process"),
            DefaultValue(Enums.PaymentProcessMode.AuthorizeAndCapture),
            DependsOn(nameof(PaymentsEnabled))]
        public static Enums.PaymentProcessMode PaymentsProcess
        {
            get => PaymentsEnabled
                ? TryGet(out Enums.PaymentProcessMode asValue) ? asValue : Enums.PaymentProcessMode.AuthorizeAndCapture
                : Enums.PaymentProcessMode.AuthorizeAndCapture;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether to use a provider to get wallet.</summary>
        /// <value>True if use provider get wallet, false if not.</value>
        [AppSettingsKey("Clarity.Payments.UseProviderGetWallet"),
            DefaultValue(false)]
        public static bool UseProviderGetWallet
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the name of the wallet account number serializable attribute.</summary>
        /// <value>The name of the wallet account number serializable attribute.</value>
        [AppSettingsKey("Clarity.Payments.Wallet.AccountNumberSerializableAttributeName"),
            DefaultValue("EvoAccountNumber"),
            DependsOn(nameof(UseProviderGetWallet))]
        public static string? WalletAccountNumberSerializableAttributeName
        {
            get => UseProviderGetWallet ? TryGet(out string? asValue) ? asValue : "EvoAccountNumber" : null;
            private set => TrySet(value);
        }
        #endregion

        #region Payment Hub
        /// <summary>Gets a value indicating whether this is a payment hub instance.</summary>
        /// <value>True if this is a payment hub instance, false if not.</value>
        [AppSettingsKey("Clarity.PaymentHub.PaymentHubEnabled"),
         DefaultValue(false)]
        public static bool PaymentHubEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        /// <summary>Gets a value indicating whether to show the search bar.</summary>
        /// <value>True if hide the search bar, false if not.</value>
        [AppSettingsKey("Clarity.PaymentHub.SearchBarDisabled"),
         DefaultValue(false)]
        public static bool SearchBarDisabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        /// <summary>Gets a value indicating whether to show the products dropdown in the header.</summary>
        /// <value>True if hide the products dropdown in the header, false if not.</value>
        [AppSettingsKey("Clarity.PaymentHub.HeaderProductsDisabled"),
         DefaultValue(false)]
        public static bool HeaderProductsDisabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        /// <summary>Gets a value indicating whether to show the header bot content.</summary>
        /// <value>True if hide the header bot content, false if not.</value>
        [AppSettingsKey("Clarity.PaymentHub.HeaderBotDisabled"),
         DefaultValue(false)]
        public static bool HeaderBotDisabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        /// <summary>Gets a value indicating whether to show the payment hub header.</summary>
        /// <value>True if show the payment hub header, false if not.</value>
        [AppSettingsKey("Clarity.PaymentHub.PaymentHubHeaderEnabled"),
         DefaultValue(false)]
        public static bool PaymentHubHeaderEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion

        #region Pricing
        /// <summary>Gets a value indicating whether the pricing is enabled.</summary>
        /// <value>True if pricing enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Pricing.Enabled"),
         DefaultValue(true)]
        public static bool PricingEnabled
        {
            get => TryGet(out bool asValue) ? asValue : true;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the login for pricing is enabled.</summary>
        /// <value>True if login for pricing enabled, false if not.</value>
        [AppSettingsKey("Clarity.Pricing.LoginForPricing.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(LoginEnabled), nameof(PricingEnabled))]
        public static bool LoginForPricingEnabled
        {
            get => LoginEnabled && PricingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the login for pricing key.</summary>
        /// <value>The login for pricing key.</value>
        [AppSettingsKey("Clarity.Login.LoginForPricing.Key"),
         DefaultValue("ui.storefront.searchCatalog.loginToViewPricing"),
         DependsOn(nameof(LoginForPricingEnabled))]
        public static string? LoginForPricingKey
        {
            get => LoginForPricingEnabled
                ? TryGet(out string? asValue)
                    ? asValue
                    : "ui.storefront.searchCatalog.loginToViewPricing"
                : null;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the pricing msrp is enabled.</summary>
        /// <value>True if pricing msrp enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Pricing.Msrp.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(PricingEnabled))]
        public static bool PricingMsrpEnabled
        {
            get => PricingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the pricing reduction is enabled.</summary>
        /// <value>True if pricing reduction enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Pricing.Reduction.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(PricingEnabled))]
        public static bool PricingReductionEnabled
        {
            get => PricingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the pricing price rules is enabled.</summary>
        /// <value>True if pricing price rules enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Pricing.PriceRules.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(PricingEnabled))]
        public static bool PricingPriceRulesEnabled
        {
            get => PricingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the pricing tiered pricing is enabled.</summary>
        /// <value>True if pricing tiered pricing enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Pricing.TieredPricing.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(PricingEnabled))]
        public static bool PricingTieredPricingEnabled
        {
            get => PricingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets the pricing provider tiered default markup rate.</summary>
        /// <value>The pricing provider tiered default markup rate.</value>
        [AppSettingsKey("Clarity.Modes.TieredPricingMode.DefaultMarkupRate"),
         DefaultValue(0),
         DependsOn(nameof(PricingTieredPricingEnabled))]
        public static decimal PricingProviderTieredDefaultMarkupRate
        {
            get => PricingTieredPricingEnabled ? TryGet(out decimal asValue) ? asValue : 0m : 0m;
            private set => TrySet(value);
        }

        /// <summary>Gets the pricing provider tiered default unit of measure.</summary>
        /// <value>The pricing provider tiered default unit of measure.</value>
        [AppSettingsKey("Clarity.Modes.TieredPricingMode.DefaultUnitOfMeasure"),
         DefaultValue("EACH"),
         DependsOn(nameof(PricingTieredPricingEnabled))]
        public static string? PricingProviderTieredDefaultUnitOfMeasure
        {
            get => PricingTieredPricingEnabled ? TryGet(out string? asValue) ? asValue : "EACH" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the pricing provider tiered default price point key.</summary>
        /// <value>The pricing provider tiered default price point key.</value>
        [AppSettingsKey("Clarity.Modes.TieredPricingMode.DefaultPricePointKey"),
         DefaultValue("WEB"),
         DependsOn(nameof(PricingTieredPricingEnabled))]
        public static string? PricingProviderTieredDefaultPricePointKey
        {
            get => PricingTieredPricingEnabled ? TryGet(out string? asValue) ? asValue : "WEB" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets the pricing provider tiered default currency key.</summary>
        /// <value>The pricing provider tiered default currency key.</value>
        [AppSettingsKey("Clarity.Modes.TieredPricingMode.DefaultCurrencyKey"),
         DefaultValue("USD"),
         DependsOn(nameof(PricingTieredPricingEnabled))]
        public static string? PricingProviderTieredDefaultCurrencyKey
        {
            get => PricingTieredPricingEnabled ? TryGet(out string? asValue) ? asValue : "USD" : null;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the pricing tiered pricing is enabled.</summary>
        /// <value>True if pricing tiered pricing enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Pricing.FlatWithStoreOverridePricingMode.Enabled"),
         DefaultValue(false),
         DependsOn(nameof(PricingEnabled))]
        public static bool PricingFlatWithStoreOverridePricingEnabled
        {
            get => PricingEnabled ? TryGet(out bool asValue) ? asValue : false : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the pricing provider flat with store override pricing mode require
        /// inventory to override.</summary>
        /// <value>True if pricing provider flat with store override pricing mode require inventory to override, false
        /// if not.</value>
        [AppSettingsKey("Clarity.Modes.FlatWithStoreOverridePricingMode.RequireInventoryToOverride"),
         DefaultValue(false)]
        public static bool PricingProviderFlatWithStoreFranchiseOrBrandOverridePricingModeRequireInventoryToOverride
        {
            get => PricingFlatWithStoreOverridePricingEnabled
                ? TryGet(out bool asValue) ? asValue : false
                : false;
            private set => TrySet(value);
        }
        #endregion

        #region Products
        /// <summary>Enable filtering of products by account roles.</summary>
        /// <value>True if filtering catalog by account roles, false if not.</value>
        [AppSettingsKey("Clarity.Featureset.Products.FilterByAccountRoles"),
            DefaultValue(false)]
        public static bool FilterByAccountRoles
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether some products require licenses for purchase.</summary>
        /// <value>True if LicenseRequiredforProductPurchase enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Products.LicenseRequiredforProductPurchase"),
         DefaultValue(false)]
        public static bool LicenseRequiredforProductPurchase
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the product notifications is enabled.</summary>
        /// <value>True if product notifications enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Products.Notifications.Enabled"),
         DefaultValue(false)]
        public static bool ProductNotificationsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the product restrictions is enabled.</summary>
        /// <value>True if product restrictions enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Products.Restrictions.Enabled"),
         DefaultValue(false)]
        public static bool ProductRestrictionsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the product future imports is enabled.</summary>
        /// <value>True if product future imports enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Products.FutureImports.Enabled"),
         DefaultValue(false)]
        public static bool ProductFutureImportsEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the product stored files is enabled.</summary>
        /// <value>True if product stored files enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Products.StoredFiles.Enabled"),
         DefaultValue(false)]
        public static bool ProductStoredFilesEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether the product category attributes is enabled.</summary>
        /// <value>True if product category attributes enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Products.CategoryAttributes.Enabled"),
         DefaultValue(true),
         DependsOn(nameof(CategoriesEnabled))]
        public static bool ProductCategoryAttributesEnabled
        {
            get => CategoriesEnabled ? TryGet(out bool asValue) ? asValue : true : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether product role restrictions is overridden.</summary>
        /// <value>True if override product role restrictions, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Products.OverrideProductRoleRestrictions"),
         DefaultValue(false)]
        public static bool OverrideProductRoleRestrictions
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }

        /// <summary>Gets a value indicating whether to override is unlimited stock.</summary>
        /// <value>True if override product role restrictions, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.Products.OverrideProductIsUnlimitedStock"),
         DefaultValue(null)]
        public static bool? OverrideProductIsUnlimitedStock
        {
            get => TryGet(out bool asValue) ? asValue : null;
            private set => TrySet(value);
        }
        #endregion

        #region Profanity Filter
        /// <summary>Gets a value indicating whether the profanity filter is enabled.</summary>
        /// <value>True if profanity filter enabled, false if not.</value>
        [AppSettingsKey("Clarity.FeatureSet.ProfanityFilter.Enabled"),
         DefaultValue(false)]
        public static bool ProfanityFilterEnabled
        {
            get => TryGet(out bool asValue) ? asValue : false;
            private set => TrySet(value);
        }
        #endregion
    }
}
