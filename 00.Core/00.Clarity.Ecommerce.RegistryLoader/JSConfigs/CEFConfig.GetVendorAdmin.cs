// <copyright file="CEFConfig.GetVendorAdmin.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF configuration class, Get Vendor Admin CEF Config section</summary>
// ReSharper disable StringLiteralTypo, StyleCop.SA1027, TabsAndSpacesMismatch
#pragma warning disable SA1027 // Use tabs correctly
namespace Clarity.Ecommerce.JSConfigs
{
	using Models;
	using Newtonsoft.Json;

	public static partial class CEFConfigDictionary
	{
		/// <summary>Gets vendor admin CEF configuration.</summary>
		/// <returns>The vendor admin CEF configuration.</returns>
		public static CEFActionResponse<string> GetVendorAdminCEFConfig()
		{
			var retVal = $@"/**
 * @file cef-vendor-admin.ts
 * @author Copyright (c) 2020-{System.DateTime.Today.Year} clarity-ventures.com. All rights reserved
 * @desc Main configuration file with settings for stovendorre administrator SPA
 * Note: The documentation on what each value is/does is in /framework/storeAdmin/_core/cefConfig.ts
 */
/* The Settings Object (will become an angular constant) */
var settings = {GetVendorAdminCEFConfigInner()};
angular.module(""cefConfig"", []).constant(""cefConfig"", settings);";
			return new(retVal, true);
		}

		/// <summary>Gets vendor admin CEF configuration (alternate).</summary>
		/// <returns>The vendor admin CEF configuration (alternate).</returns>
		public static CEFActionResponse<CEFConfig> GetVendorAdminCEFConfigAlt()
		{
			var serialized = GetVendorAdminCEFConfigInner();
			var deserialized = JsonConvert.DeserializeObject<CEFConfig>(serialized!);
			return deserialized.WrapInPassingCEFARIfNotNull();
		}

		private static string GetVendorAdminCEFConfigInner()
		{
			return "{" + $@"
html5Mode: {VendorAdminHTML5Mode},
{GetAuthBlock()}
countryCode: ""{CountryCode}"",
routes: {{
api: {{
	host: {(APIVendorAdminRouteHostUrl != null ? $"\"{APIVendorAdminRouteHostUrl}\"" : "null")},
	root: {(APIVendorAdminRouteRelativePath != null ? $"\"{APIVendorAdminRouteRelativePath}\"" : "null")}
}},
{FormatRoute("ui", UIRouteHostUrl, UIRouteRelativePath, UIRouteHostLookupMethod, UIRouteHostLookupWhichUrl)},
{FormatRoute("uiTemplateOverride", UITemplateOverrideRouteHostUrl, UITemplateOverrideRouteRelativePath + "/vendorAdmin", UITemplateOverrideRouteHostLookupMethod, UITemplateOverrideRouteHostLookupWhichUrl)},
{FormatRoute("site", SiteRouteHostUrl, SiteRouteRelativePath, SiteRouteHostLookupMethod, SiteRouteHostLookupWhichUrl)},
{FormatRoute("terms", TermsRouteHostUrl, TermsRouteRelativePath, TermsRouteHostLookupMethod, TermsRouteHostLookupWhichUrl)},
{FormatRoute("privacy", PrivacyRouteHostUrl, PrivacyRouteRelativePath, PrivacyRouteHostLookupMethod, PrivacyRouteHostLookupWhichUrl)},
{FormatRoute("contactUs", ContactUsRouteHostUrl, ContactUsRouteRelativePath, ContactUsRouteHostLookupMethod, ContactUsRouteHostLookupWhichUrl)},
{FormatRoute("companyLogo", CompanyLogoRouteHostUrl, CompanyLogoRouteRelativePath, CompanyLogoRouteHostLookupMethod, CompanyLogoRouteHostLookupWhichUrl)},
{FormatRoute("login", LoginRouteHostUrl, LoginRouteRelativePath, LoginRouteHostLookupMethod, LoginRouteHostLookupWhichUrl)},
{FormatRoute("registration", RegistrationRouteHostUrl, RegistrationRouteRelativePath, RegistrationRouteHostLookupMethod, RegistrationRouteHostLookupWhichUrl)},
{FormatRoute("forgotPassword", ForgotPasswordRouteHostUrl, ForgotPasswordRouteRelativePath, ForgotPasswordRouteHostLookupMethod, ForgotPasswordRouteHostLookupWhichUrl)},
{FormatRoute("forgotUsername", ForgotUsernameRouteHostUrl, ForgotUsernameRouteRelativePath, ForgotUsernameRouteHostLookupMethod, ForgotUsernameRouteHostLookupWhichUrl)},
{FormatRoute("myVendorAdmin", MyVendorAdminRouteHostUrl, MyVendorAdminRouteRelativePath, MyVendorAdminRouteHostLookupMethod, MyVendorAdminRouteHostLookupWhichUrl)},
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
