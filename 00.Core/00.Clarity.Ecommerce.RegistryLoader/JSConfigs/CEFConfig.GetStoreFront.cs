// <copyright file="CEFConfig.GetStoreFront.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF configuration class, get Storefront CEF Config section</summary>
// ReSharper disable StringLiteralTypo, StyleCop.SA1027, TabsAndSpacesMismatch
#nullable enable
namespace Clarity.Ecommerce.JSConfigs
{
	using Models;

	public static partial class CEFConfigDictionary
	{
		/// <summary>Gets store front CEF configuration.</summary>
		/// <param name="host">The host.</param>
		/// <returns>The store front CEF configuration.</returns>
		// ReSharper disable once CyclomaticComplexity
		public static CEFActionResponse<string> GetStoreFrontCEFConfig(string? host)
		{
			var retVal = $@"/**
 * @file cef-store.ts
 * @author Copyright (c) 2018-{System.DateTime.Today.Year} clarity-ventures.com. All rights reserved
 * @desc Main configuration file with settings for storefront
 * Note: The documentation on what each value is/does is in /framework/store/_core/cefConfig.ts
 */
/* The Settings Object (will become an angular constant) */
var settings = {GetStoreFrontCEFConfigInner(host)};
/* Set up the constant */
angular.module(""cefConfig"", []).constant(""cefConfig"", settings);";
			return new(retVal, true);
		}

		/// <summary>Gets storefront CEF configuration (alternate).</summary>
		/// <param name="host">The host.</param>
		/// <returns>The storefront CEF configuration (alternate).</returns>
		public static CEFActionResponse<string> GetStoreFrontCEFConfigAlt(string? host)
		{
			var serialized = GetStoreFrontCEFConfigInner(host);
			return serialized.WrapInPassingCEFARIfNotNull();
		}

		private static string GetStoreFrontCEFConfigInner(string? host)
		{
			return "{" + $@"
debug: {StorefrontDebugMode.ToString().ToLower()},
html5Mode: {StorefrontHTML5Mode},
{GetAuthBlock()}
countryCode: ""{CountryCode}"",
routes: {{
api: {{
	host: {(APIStorefrontRouteHostUrl != null ? $"\"{APIStorefrontRouteHostUrl}\"" : "null")},
	root: {(APIStorefrontRouteRelativePath != null ? $"\"{APIStorefrontRouteRelativePath}\"" : "null")}
}},
{FormatRoute("cart", CartRouteHostUrl, CartRouteRelativePath, CartRouteHostLookupMethod, CartRouteHostLookupWhichUrl)},
{FormatRoute("catalog", CatalogRouteHostUrl, CatalogRouteRelativePath, CatalogRouteHostLookupMethod, CatalogRouteHostLookupWhichUrl)},
{FormatRoute("category", CategoryRouteHostUrl, CategoryRouteRelativePath, CategoryRouteHostLookupMethod, CategoryRouteHostLookupWhichUrl)},
{FormatRoute("checkout", CheckoutRouteHostUrl, CheckoutRouteRelativePath, CheckoutRouteHostLookupMethod, CheckoutRouteHostLookupWhichUrl)},
{FormatRoute("companyLogo", CompanyLogoRouteHostUrl, CompanyLogoRouteRelativePath, CompanyLogoRouteHostLookupMethod, CompanyLogoRouteHostLookupWhichUrl)},
{FormatRoute("connectLive", ConnectLiveRouteHostUrl, ConnectLiveRouteRelativePath, ConnectLiveRouteHostLookupMethod, ConnectLiveRouteHostLookupWhichUrl)},
{FormatRoute("contactUs", ContactUsRouteHostUrl, ContactUsRouteRelativePath, ContactUsRouteHostLookupMethod, ContactUsRouteHostLookupWhichUrl)},
{FormatRoute("dashboard", DashboardRouteHostUrl, DashboardRouteRelativePath, DashboardRouteHostLookupMethod, DashboardRouteHostLookupWhichUrl)},
{FormatRoute("forcedPasswordReset", ForcedPasswordResetRouteHostUrl, ForcedPasswordResetRouteRelativePath, ForcedPasswordResetRouteHostLookupMethod, ForcedPasswordResetRouteHostLookupWhichUrl)},
{FormatRoute("forgotPassword", ForgotPasswordRouteHostUrl, ForgotPasswordRouteRelativePath, ForgotPasswordRouteHostLookupMethod, ForgotPasswordRouteHostLookupWhichUrl)},
{FormatRoute("forgotUsername", ForgotUsernameRouteHostUrl, ForgotUsernameRouteRelativePath, ForgotUsernameRouteHostLookupMethod, ForgotUsernameRouteHostLookupWhichUrl)},
{FormatRoute("images", ImagesRootRouteHostUrl, ImagesRootRouteRelativePath, ImagesRootRouteHostLookupMethod, ImagesRootRouteHostLookupWhichUrl)},
{FormatRoute("imports", ImportsRootRouteHostUrl, ImportsRootRouteRelativePath, ImportsRootRouteHostLookupMethod, ImportsRootRouteHostLookupWhichUrl)},
{FormatRoute("login", LoginRouteHostUrl, LoginRouteRelativePath, LoginRouteHostLookupMethod, LoginRouteHostLookupWhichUrl)},
{FormatRoute("membershipRegistration", MembershipRegistrationRouteHostUrl, MembershipRegistrationRouteRelativePath, MembershipRegistrationRouteHostLookupMethod, MembershipRegistrationRouteHostLookupWhichUrl)},
{FormatRoute("myBrandAdmin", MyBrandAdminRouteHostUrl, MyBrandAdminRouteRelativePath, MyBrandAdminRouteHostLookupMethod, MyBrandAdminRouteHostLookupWhichUrl)},
{FormatRoute("myFranchiseAdmin", MyFranchiseAdminRouteHostUrl, MyFranchiseAdminRouteRelativePath, MyFranchiseAdminRouteHostLookupMethod, MyFranchiseAdminRouteHostLookupWhichUrl)},
{FormatRoute("myStoreAdmin", MyStoreAdminRouteHostUrl, MyStoreAdminRouteRelativePath, MyStoreAdminRouteHostLookupMethod, MyStoreAdminRouteHostLookupWhichUrl)},
{FormatRoute("privacy", PrivacyRouteHostUrl, PrivacyRouteRelativePath, PrivacyRouteHostLookupMethod, PrivacyRouteHostLookupWhichUrl)},
{FormatRoute("productDetail", ProductDetailRouteHostUrl, ProductDetailRouteRelativePath, ProductDetailRouteHostLookupMethod, ProductDetailRouteHostLookupWhichUrl)},
{FormatRoute("quoteCart", QuoteCartRouteHostUrl, QuoteCartRouteRelativePath, QuoteCartRouteHostLookupMethod, QuoteCartRouteHostLookupWhichUrl)},
{FormatRoute("registration", RegistrationRouteHostUrl, RegistrationRouteRelativePath, RegistrationRouteHostLookupMethod, RegistrationRouteHostLookupWhichUrl)},
{FormatRoute("site", SiteRouteHostUrl, SiteRouteRelativePath, SiteRouteHostLookupMethod, SiteRouteHostLookupWhichUrl)},
{FormatRoute("storeDetail", StoreDetailRouteHostUrl, StoreDetailRouteRelativePath, StoreDetailRouteHostLookupMethod, StoreDetailRouteHostLookupWhichUrl)},
{FormatRoute("storedFiles", StoredFilesRootRouteHostUrl, StoredFilesRootRouteRelativePath, StoredFilesRootRouteHostLookupMethod, StoredFilesRootRouteHostLookupWhichUrl)},
{FormatRoute("storeLocator", StoreLocatorRouteHostUrl, StoreLocatorRouteRelativePath, StoreLocatorRouteHostLookupMethod, StoreLocatorRouteHostLookupWhichUrl)},
{FormatRoute("submitQuote", SubmitQuoteRouteHostUrl, SubmitQuoteRouteRelativePath, SubmitQuoteRouteHostLookupMethod, SubmitQuoteRouteHostLookupWhichUrl)},
{FormatRoute("terms", TermsRouteHostUrl, TermsRouteRelativePath, TermsRouteHostLookupMethod, TermsRouteHostLookupWhichUrl)},
{FormatRoute("ui", host ?? UIRouteHostUrl, UIRouteRelativePath, UIRouteHostLookupMethod, UIRouteHostLookupWhichUrl)},
{FormatRoute("uiTemplateOverride", host ?? UITemplateOverrideRouteHostUrl, UITemplateOverrideRouteRelativePath + "/store", UITemplateOverrideRouteHostLookupMethod, UITemplateOverrideRouteHostLookupWhichUrl)},
}},
{GetCORSResourceWhiteList()}
{GetCookies()}
companyName: ""{CompanyName}"",
{GetGoogleAPI()}
{GetCatalog()}
dashboard: [
	{{
		name: ""MyDashboard"",
		titleKey: ""ui.storefront.common.MyDashboard"",
		sref: ""userDashboard.dashboard"",
		href: ""/dashboard"",
		enabled: {DashboardRouteMyDashboardEnabled.ToString().ToLower()},
		order: 10,
		icon: '<i class=""far fa-tachometer-alt-fast fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""AccountSettings"",
		titleKey: ""ui.storefront.common.AccountSetting.Plural"",
		enabled: {DashboardRouteAccountSettingsEnabled.ToString().ToLower()},
		order: 20,
		icon: '<i class=""far fa-user-circle fa-fw"" aria-hidden=""true""></i>',
		children: [
			{{
				name: ""MyProfile"",
				titleKey: ""ui.storefront.menu.miniMenu.myProfile"",
				sref: ""userDashboard.profileUser"",
				href: ""/dashboard/my-profile"",
				enabled: {DashboardRouteAccountSettingsMyProfileEnabled.ToString().ToLower()},
				order: 21
			}},
			{{
				name: ""AccountProfile"",
				titleKey: ""ui.storefront.userDashboard2.userDashboard.AccountProfile"",
				sref: ""userDashboard.profileAccount"",
				href: ""/dashboard/account-profile"",
				enabled: {DashboardRouteAccountSettingsAccountProfileEnabled.ToString().ToLower()},
				order: 22
			}},
			{{
				name: ""AddressBook"",
				titleKey: ""ui.storefront.common.AddressBook"",
				sref: ""userDashboard.addressBook"",
				href: ""/dashboard/address-book"",
				enabled: {DashboardRouteAccountSettingsAddressBookEnabled.ToString().ToLower()},
				order: 23
			}},
			{{
				name: ""Wallet"",
				titleKey: ""ui.storefront.userDashboard2.userDashboard.Wallet"",
				sref: ""userDashboard.wallet.list"",
				href: ""/dashboard/wallet"",
				enabled: {DashboardRouteAccountSettingsWalletEnabled.ToString().ToLower()},
				order: 24
			}},
		]
	}},
	{{
		name: ""Users"",
		titleKey: ""ui.storefront.common.User.Plural"",
		sref: ""userDashboard.users.list"",
		href: ""/dashboard/users"",
		reqAnyRoles: [""CEF Local Administrator"", ""CEF Affiliate Administrator""],
		enabled: {DashboardRouteUsersEnabled.ToString().ToLower()},
		order: 30,
		icon: '<i class=""far fa-users fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""Groups"",
		titleKey: ""ui.storefront.common.SalesGroup.Plural"",
		sref: ""userDashboard.salesGroups.list"",
		href: ""/dashboard/groups"",
		enabled: {DashboardRouteGroupsEnabled.ToString().ToLower()},
		order: 40,
		icon: '<i class=""far fa-object-group fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""Orders"",
		titleKey: ""ui.storefront.common.Order.Plural"",
		sref: ""userDashboard.salesOrders.list"",
		srefAlt: ""userDashboard.salesGroups.detail.order"",
		href: ""/dashboard/orders"",
		hrefAlt: ""userDashboard.salesGroups.detail.order"",
		enabled: {DashboardRouteSalesOrdersEnabled.ToString().ToLower()},
		order: 50,
		icon: '<i class=""far fa-receipt fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""Returns"",
		titleKey: ""ui.storefront.common.Return.Plural"",
		sref: ""userDashboard.salesReturns.list"",
		srefAlt: ""userDashboard.salesGroups.detail.return"",
		href: ""/dashboard/returns"",
		hrefAlt: ""userDashboard.salesGroups.detail.return"",
		enabled: {DashboardRouteSalesReturnsEnabled.ToString().ToLower()},
		order: 60,
		icon: '<i class=""far fa-box-fragile fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""Invoices"",
		titleKey: ""ui.storefront.common.Invoice.Plural"",
		sref: ""userDashboard.salesInvoices.list"",
		srefAlt: ""userDashboard.salesGroups.detail.invoice"",
		href: ""/dashboard/invoices"",
		hrefAlt: ""userDashboard.salesGroups.detail.invoice"",
		enabled: {DashboardRouteSalesInvoicesEnabled.ToString().ToLower()},
		order: 70,
		icon: '<i class=""far fa-file-invoice-dollar fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""Quotes"",
		titleKey: ""ui.storefront.common.Quote.Plural"",
		sref: ""userDashboard.salesQuotes.list"",
		srefAlt: ""userDashboard.salesGroups.detail.quote"",
		href: ""/dashboard/quotes"",
		hrefAlt: ""userDashboard.salesGroups.detail.quote"",
		enabled: {DashboardRouteSalesQuotesEnabled.ToString().ToLower()},
		order: 80,
		icon: '<i class=""far fa-quote-right fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""Samples"",
		titleKey: ""ui.storefront.menu.miniMenu.sampleRequests"",
		sref: ""userDashboard.sampleRequests.list"",
		srefAlt: ""userDashboard.salesGroups.detail.sample"",
		href: ""/dashboard/samples"",
		hrefAlt: ""userDashboard.salesGroups.detail.sample"",
		enabled: {DashboardRouteSampleRequestsEnabled.ToString().ToLower()},
		order: 100,
		icon: '<i class=""far fa-eye-dropper fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""Downloads"",
		titleKey: ""ui.storefront.userDashboard2.userDashboard.Downloads"",
		sref: ""userDashboard.downloads"",
		href: ""/dashboard/downloads"",
		enabled: {DashboardRouteDownloadsEnabled.ToString().ToLower()},
		order: 110,
		icon: '<i class=""far fa-arrow-circle-down fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""WishList"",
		titleKey: ""ui.storefront.common.WishList"",
		sref: ""userDashboard.wishList"",
		href: ""/dashboard/wish-list"",
		enabled: {DashboardRouteWishListEnabled.ToString().ToLower()},
		order: 120,
		icon: '<i class=""far fa-heart fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""Favorites"",
		titleKey: ""ui.storefront.common.FavoritesList"",
		sref: ""userDashboard.favorites"",
		href: ""/dashboard/favorites"",
		enabled: {DashboardRouteFavoritesEnabled.ToString().ToLower()},
		order: 130,
		icon: '<i class=""far fa-star fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""InStockAlerts"",
		titleKey: ""ui.storefront.userDashboard2.userDashboard.InStockAlerts"",
		sref: ""userDashboard.notifyMeList"",
		href: ""/dashboard/in-stock-alerts"",
		enabled: {DashboardRouteInStockAlertsEnabled.ToString().ToLower()},
		order: 140,
		icon: '<i class=""far fa-bell-on fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""ShoppingLists"",
		titleKey: ""ui.storefront.userDashboard2.userDashboard.ShoppingLists"",
		sref: ""userDashboard.shoppingLists.list"",
		href: ""/dashboard/shopping-lists"",
		enabled: {DashboardRouteShoppingListsEnabled.ToString().ToLower()},
		order: 150,
		icon: '<i class=""far fa-clipboard-list-check fa-fw"" aria-hidden=""true""></i>'
	}},
	{{
		name: ""OrderRequests"",
		titleKey: ""ui.storefront.cart.OrderRequest"",
		sref: ""userDashboard.orderRequests.list"",
		href: ""/dashboard/order-requests"",
		enabled: {DashboardRouteOrderRequestsEnabled.ToString().ToLower()},
		order: 150,
		icon: '<i class=""far fa-tag fa-fw"" aria-hidden=""true""></i>',
		reqAnyRoles: [""Supervisor""],
	}},
	{{
		name: ""Inbox"",
		titleKey: ""ui.storefront.userDashboard2.userDashboard.Inbox"",
		sref: ""userDashboard.inbox"",
		href: ""/dashboard/inbox"",
		enabled: {DashboardRouteInboxEnabled.ToString().ToLower()},
		order: 160,
		icon: '<i class=""far fa-inbox fa-fw"" aria-hidden=""true""></i>'
	}}{CustomDashboardRoutes ?? string.Empty}
],
{GetPurchaseBlock()}
{GetSubmitQuoteBlock()}
{GetRegisterBlock()}
{GetPersonalDetailsBlock()}
billing: {{
	enabled: {BillingEnabled.ToString().ToLower()},
	payments: {{
		enabled: {PaymentsEnabled.ToString().ToLower()}
	}}
}},
facebookPixelService: {{
	enabled: {FacebookPixelServiceEnabled.ToString().ToLower()}
}},
googleTagManager: {{
	enabled: {GoogleTagManagerEnabled.ToString().ToLower()}
}},
loginForPricing: {{
	enabled: {LoginForPricingEnabled.ToString().ToLower()},
	key: ""{LoginForPricingKey}""
}},
loginForInventory: {{
	enabled: {LoginForInventoryEnabled.ToString().ToLower()}
}},
newUserRegister: {{
	newUsersAreDefaultApproved: {NewUsersAreDefaultApproved.ToString().ToLower()},
	newUsersAreDefaultLockedOut: {NewUsersAreDefaultLockedOut.ToString().ToLower()},
}},
disableAddToCartModals: {(!AddToCartModalsEnabled).ToString().ToLower()},
tracking: {{
	enabled: {TrackingEnabled.ToString().ToLower()},
	expires: {TrackingExpiresAfterXMinutes},
}},
largeBidNotification : {{
	enabled: {LargeBidNotificationEnabled.ToString().ToLower()},
	percentage: {LargeBidNotificationPercentage}
}},
addToQuoteCartModalDisabled: {(!AddToQuoteCartModalIsEnabled).ToString().ToLower()},
ticketExchange: {{
	enabled: {CatalogTicketExchangeEnabled.ToString().ToLower()},
}},
paymentHubEnabled: {PaymentHubEnabled.ToString().ToLower()},
paymentHubHeaderEnabled:{PaymentHubHeaderEnabled.ToString().ToLower()},
searchBarDisabled:{SearchBarDisabled.ToString().ToLower()},
headerProductsDisabled:{HeaderProductsDisabled.ToString().ToLower()},
headerBotDisabled:{HeaderBotDisabled.ToString().ToLower()},
{GetFeatureSet()}
{GetStoredFilesImagesAndImports()}".Replace("\r\n", "\r\n\t") + @"
}";
		}
	}
}
