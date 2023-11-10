// <copyright file="CEFConfig.GetAdmin.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF configuration class, Get Admin CEF Config section</summary>
// ReSharper disable StringLiteralTypo, StyleCop.SA1027, TabsAndSpacesMismatch
#pragma warning disable SA1027 // Use tabs correctly
namespace Clarity.Ecommerce.JSConfigs
{
	public static partial class CEFConfigDictionary
	{
		/// <summary>Gets admin CEF configuration.</summary>
		/// <returns>The admin CEF configuration.</returns>
		public static Models.CEFActionResponse<string> GetAdminCEFConfig()
		{
			var retVal = $@"/**
 * @file /framework/configurations/cef-admin.ts
 * @author Copyright (c) 2018-{System.DateTime.Today.Year} clarity-ventures.com. All rights reserved
 * @desc Main configuration file with settings for Admin SPA
 * Note: The documentation on what each value is/does is in /framework/admin/_core/cefConfig.ts
 */
/* The Settings Object (will become an angular constant) */
var settings = {{" + $@"
debug: {AdminDebugMode.ToString().ToLower()},
html5Mode: {AdminHTML5Mode},
{GetAuthBlock()}
countryCode: ""{CountryCode}"",
routes: {{
api: {{
	host: {(APIAdminRouteHostUrl != null ? $"\"{APIAdminRouteHostUrl}\"" : "null")},
	root: {(APIAdminRouteRelativePath != null ? $"\"{APIAdminRouteRelativePath}\"" : "null")}
}},
{FormatRoute("admin", AdminRouteHostUrl, AdminRouteRelativePath, AdminRouteHostLookupMethod, AdminRouteHostLookupWhichUrl)},
{FormatRoute("connect", ConnectRouteHostUrl, ConnectRouteRelativePath, ConnectRouteHostLookupMethod, ConnectRouteHostLookupWhichUrl)},
{FormatRoute("images", ImagesRootRouteHostUrl, ImagesRootRouteRelativePath, ImagesRootRouteHostLookupMethod, ImagesRootRouteHostLookupWhichUrl)},
{FormatRoute("imports", ImportsRootRouteHostUrl, ImportsRootRouteRelativePath, ImportsRootRouteHostLookupMethod, ImportsRootRouteHostLookupWhichUrl)},
{FormatRoute("productDetail", ProductDetailRouteHostUrl, ProductDetailRouteRelativePath, ProductDetailRouteHostLookupMethod, ProductDetailRouteHostLookupWhichUrl)},
{FormatRoute("reporting", ReportingRouteHostUrl, ReportingRouteRelativePath, ReportingRouteHostLookupMethod, ReportingRouteHostLookupWhichUrl)},
{FormatRoute("scheduler", SchedulerRouteHostUrl, SchedulerRouteRelativePath, SchedulerRouteHostLookupMethod, SchedulerRouteHostLookupWhichUrl)},
{FormatRoute("site", SiteRouteHostUrl, SiteRouteRelativePath, SiteRouteHostLookupMethod, SiteRouteHostLookupWhichUrl)},
{FormatRoute("storeDetail", StoreDetailRouteHostUrl, StoreDetailRouteRelativePath, StoreDetailRouteHostLookupMethod, StoreDetailRouteHostLookupWhichUrl)},
{FormatRoute("storedFiles", StoredFilesRootRouteHostUrl, StoredFilesRootRouteRelativePath, StoredFilesRootRouteHostLookupMethod, StoredFilesRootRouteHostLookupWhichUrl)},
{FormatRoute("ui", UIAdminRouteHostUrl, UIAdminRouteRelativePath, UIAdminRouteHostLookupMethod, UIAdminRouteHostLookupWhichUrl)},
{FormatRoute("uiTemplateOverride", UIAdminTemplateOverrideRouteHostUrl, UIAdminTemplateOverrideRouteRelativePath + "/admin", UIAdminTemplateOverrideRouteHostLookupMethod, UIAdminTemplateOverrideRouteHostLookupWhichUrl)},
}},
{GetCORSResourceWhiteList()}
{GetCookies()}
companyName: ""{CompanyName}"",
showStorefrontButton: {AdminShowStorefrontButton.ToString().ToLower()},
{GetGoogleAPI()}
{GetPurchaseBlock().Replace("/framework/store/purchasing", "/framework/admin/controls/sales/purchasing").Replace("ui.storefront.", "ui.admin.")}
{GetSubmitQuoteBlock()}
{GetPersonalDetailsBlock()}
billing: {{
	enabled: {BillingEnabled.ToString().ToLower()},
	payments: {{
		enabled: {PaymentsEnabled.ToString().ToLower()}
	}}
}},
usePhonePrefixLookups: {{
	enabled: {PhonePrefixLookupsEnabled.ToString().ToLower()}
}},
{GetFeatureSet()}
{GetStoredFilesImagesAndImports()}".Replace("\r\n", "\r\n\t") + @"
};
/* Set up the constant */
angular.module(""cefConfig"", []).constant(""cefConfig"", settings);";
			return new(retVal, true);
		}
	}
}
