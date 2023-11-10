// <copyright file="CEFConfig.GetManufacturerAdmin.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF configuration class, Get Manufacturer Admin CEF Config section</summary>
// ReSharper disable StringLiteralTypo, StyleCop.SA1027, TabsAndSpacesMismatch
#pragma warning disable SA1027 // Use tabs correctly
namespace Clarity.Ecommerce.JSConfigs
{
	using Models;
	using Newtonsoft.Json;

	public static partial class CEFConfigDictionary
	{
		/// <summary>Gets brand admin CEF configuration.</summary>
		/// <returns>The brand admin CEF configuration.</returns>
		public static CEFActionResponse<string> GetManufacturerAdminCEFConfig()
		{
			var retVal = $@"/**
 * @file cef-brand-admin.ts
 * @author Copyright (c) 2018-{System.DateTime.Today.Year} clarity-ventures.com. All rights reserved
 * @desc Main configuration file with settings for brand administrator SPA
 * Note: The documentation on what each value is/does is in /framework/storeAdmin/_core/cefConfig.ts
 */
/* The Settings Object (will become an angular constant) */
var settings = {GetManufacturerAdminCEFConfigInner()};
angular.module(""cefConfig"", []).constant(""cefConfig"", settings);";
			return new(retVal, true);
		}

		/// <summary>Gets brand admin CEF configuration (alternate).</summary>
		/// <returns>The brand admin CEF configuration (alternate).</returns>
		public static CEFActionResponse<CEFConfig> GetManufacturerAdminCEFConfigAlt()
		{
			var serialized = GetManufacturerAdminCEFConfigInner();
			var deserialized = JsonConvert.DeserializeObject<CEFConfig>(serialized!);
			return deserialized.WrapInPassingCEFARIfNotNull();
		}

		private static string GetManufacturerAdminCEFConfigInner()
		{
			return "{" + $@"
{GetAuthBlock()}
countryCode: ""{CountryCode}"",
routes: {{
api: {{
	host: {(APIManufacturerAdminRouteHostUrl != null ? $"\"{APIManufacturerAdminRouteHostUrl}\"" : "null")},
	root: {(APIManufacturerAdminRouteRelativePath != null ? $"\"{APIManufacturerAdminRouteRelativePath}\"" : "null")}
}},
{FormatRoute("ui", UIRouteHostUrl, UIRouteRelativePath, UIRouteHostLookupMethod, UIRouteHostLookupWhichUrl)},
{FormatRoute("uiTemplateOverride", UITemplateOverrideRouteHostUrl, UITemplateOverrideRouteRelativePath + "/brandAdmin", UITemplateOverrideRouteHostLookupMethod, UITemplateOverrideRouteHostLookupWhichUrl)},
{FormatRoute("site", SiteRouteHostUrl, SiteRouteRelativePath, SiteRouteHostLookupMethod, SiteRouteHostLookupWhichUrl)},
{FormatRoute("terms", TermsRouteHostUrl, TermsRouteRelativePath, TermsRouteHostLookupMethod, TermsRouteHostLookupWhichUrl)},
{FormatRoute("privacy", PrivacyRouteHostUrl, PrivacyRouteRelativePath, PrivacyRouteHostLookupMethod, PrivacyRouteHostLookupWhichUrl)},
{FormatRoute("contactUs", ContactUsRouteHostUrl, ContactUsRouteRelativePath, ContactUsRouteHostLookupMethod, ContactUsRouteHostLookupWhichUrl)},
{FormatRoute("companyLogo", CompanyLogoRouteHostUrl, CompanyLogoRouteRelativePath, CompanyLogoRouteHostLookupMethod, CompanyLogoRouteHostLookupWhichUrl)},
{FormatRoute("login", LoginRouteHostUrl, LoginRouteRelativePath, LoginRouteHostLookupMethod, LoginRouteHostLookupWhichUrl)},
{FormatRoute("registration", RegistrationRouteHostUrl, RegistrationRouteRelativePath, RegistrationRouteHostLookupMethod, RegistrationRouteHostLookupWhichUrl)},
{FormatRoute("forgotPassword", ForgotPasswordRouteHostUrl, ForgotPasswordRouteRelativePath, ForgotPasswordRouteHostLookupMethod, ForgotPasswordRouteHostLookupWhichUrl)},
{FormatRoute("forgotUsername", ForgotUsernameRouteHostUrl, ForgotUsernameRouteRelativePath, ForgotUsernameRouteHostLookupMethod, ForgotUsernameRouteHostLookupWhichUrl)},
{FormatRoute("membershipRegistration", MembershipRegistrationRouteHostUrl, MembershipRegistrationRouteRelativePath, MembershipRegistrationRouteHostLookupMethod, MembershipRegistrationRouteHostLookupWhichUrl)},
{FormatRoute("myBrandAdmin", MyBrandAdminRouteHostUrl, MyBrandAdminRouteRelativePath, MyBrandAdminRouteHostLookupMethod, MyBrandAdminRouteHostLookupWhichUrl)},
{FormatRoute("myManufacturerAdmin", MyManufacturerAdminRouteHostUrl, MyManufacturerAdminRouteRelativePath, MyManufacturerAdminRouteHostLookupMethod, MyManufacturerAdminRouteHostLookupWhichUrl)},
{FormatRoute("myStoreAdmin", MyStoreAdminRouteHostUrl, MyStoreAdminRouteRelativePath, MyStoreAdminRouteHostLookupMethod, MyStoreAdminRouteHostLookupWhichUrl)},
{FormatRoute("productDetail", ProductDetailRouteHostUrl, ProductDetailRouteRelativePath, ProductDetailRouteHostLookupMethod, ProductDetailRouteHostLookupWhichUrl)},
{FormatRoute("storeDetail", StoreDetailRouteHostUrl, StoreDetailRouteRelativePath, StoreDetailRouteHostLookupMethod, StoreDetailRouteHostLookupWhichUrl)},
{FormatRoute("storeLocator", StoreLocatorRouteHostUrl, StoreLocatorRouteRelativePath, StoreLocatorRouteHostLookupMethod, StoreLocatorRouteHostLookupWhichUrl)},
{FormatRoute("dashboard", DashboardRouteHostUrl, DashboardRouteRelativePath, DashboardRouteHostLookupMethod, DashboardRouteHostLookupWhichUrl)},
{FormatRoute("cart", CartRouteHostUrl, CartRouteRelativePath, CartRouteHostLookupMethod, CartRouteHostLookupWhichUrl)},
{FormatRoute("quoteCart", QuoteCartRouteHostUrl, QuoteCartRouteRelativePath, QuoteCartRouteHostLookupMethod, QuoteCartRouteHostLookupWhichUrl)},
{FormatRoute("imports", ImportsRootRouteHostUrl, ImportsRootRouteRelativePath, ImportsRootRouteHostLookupMethod, ImportsRootRouteHostLookupWhichUrl)},
{FormatRoute("images", ImagesRootRouteHostUrl, ImagesRootRouteRelativePath, ImagesRootRouteHostLookupMethod, ImagesRootRouteHostLookupWhichUrl)},
{FormatRoute("storedFiles", StoredFilesRootRouteHostUrl, StoredFilesRootRouteRelativePath, StoredFilesRootRouteHostLookupMethod, StoredFilesRootRouteHostLookupWhichUrl)},
}},
{GetCORSResourceWhiteList()}
{GetCookies()}
companyName: ""{CompanyName}"",
{GetGoogleAPI()}
{GetCatalog()}
{GetFeatureSet()}
{GetStoredFilesImagesAndImports()}".Replace("\r\n", "\r\n\t") + @"
}";
		}
	}
}
