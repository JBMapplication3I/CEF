// <copyright file="CEFConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
// ReSharper disable InconsistentNaming, MissingXmlDoc
#pragma warning disable IDE1006
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class CEFConfig
    {
        public bool debug { get; set; }

        public string? authProvider { get; set; }

        public string? authProviderAuthorizeUrl { get; set; }

        public string? authProviderLogoutUrl { get; set; }

        public string? authProviderClientId { get; set; }

        public string? authProviderRedirectUri { get; set; }

        public string? authProviderScope { get; set; }

        public string? dateFormat { get; set; }

        public bool showDNNButton { get; set; }

        public bool showStorefrontButton { get; set; }

        public string? countryCode { get; set; }

        public string? apiKey { get; set; }

        public string[]? corsResourceWhiteList { get; set; }

        public bool useSubDomainForCookies { get; set; }

        public int usePartialSubDomainForCookiesRootSegmentCount { get; set; }

        public bool requireSecureForCookies { get; set; }

        public UrlConfig? api { get; set; }

        public UrlConfig? ui { get; set; }

        public UrlConfig? uiTemplateOverride { get; set; }

        public UrlConfig? site { get; set; }

        public UrlConfig? terms { get; set; }

        public UrlConfig? privacy { get; set; }

        public UrlConfig? contactUs { get; set; }

        public UrlConfig? login { get; set; }

        public UrlConfig? registration { get; set; }

        public UrlConfig? forcedPasswordReset { get; set; }

        public UrlConfig? forgotPassword { get; set; }

        public UrlConfig? forgotUsername_siteSettings { get; set; }

        public string? forgotUsername_email { get; set; }

        public UrlConfig? membershipRegistration { get; set; }

        public UrlConfig? myBrandAdmin { get; set; }

        public UrlConfig? myStoreAdmin { get; set; }

        public UrlConfig? myVendorAdmin { get; set; }

        public UrlConfig? connectLive { get; set; }

        public UrlConfig? admin { get; set; }

        public UrlConfig? reporting { get; set; }

        public UrlConfig? scheduler { get; set; }

        public UrlConfig? connect { get; set; }

        public UrlConfig? companyLogo { get; set; }

        public bool google_maps_enabled { get; set; }

        public string? google_maps_apiKey { get; set; }

        public string? google_apiKey { get; set; }

        public string? google_apiClientKey { get; set; }

        public string? companyName { get; set; }

        public string? productDetailUrlFragment { get; set; }

        public string? dashboardUrlFragment { get; set; }

        public DashboardSettings[]? dashboard { get; set; }

        public string? catalog_root { get; set; }

        public string[]? catalog_extraRoots { get; set; }

        public int catalog_showCategoriesForLevelsUpTo { get; set; }

        public int catalog_defaultPageSize { get; set; }

        public string? catalog_defaultFormat { get; set; }

        public string? catalog_defaultSort { get; set; }

        public bool catalog_onlyApplyStoreToFilterByUI { get; set; }

        public bool catalog_displayImages { get; set; }

        public bool catalog_getFullAssocs { get; set; }

        public CheckoutConfig? checkout { get; set; }

        public PurchaseConfig? purchase { get; set; }

        public RegistrationConfig? register { get; set; }

        public PersonalDetailsDisplay? personalDetailsDisplay { get; set; }

        public bool billing_enabled { get; set; }

        public bool billing_payments_enabled { get; set; }

        public bool miniCart_enabled { get; set; }

        public bool usePhonePrefixLookups_enabled { get; set; }

        public bool facebookPixelService_enabled { get; set; }

        public bool googleTagManager_enabled { get; set; }

        public bool loginForPricing_enabled { get; set; }

        public string? loginForPricing_key { get; set; }

        public bool loginForInventory_enabled { get; set; }

        public bool disableAddToCartModals { get; set; }

        public FeatureSet? featureSet { get; set; }

        public UrlConfig? storedFiles_root { get; set; }

        public string? storedFiles_suffix { get; set; }

        public string? storedFiles_accounts { get; set; }

        public string? storedFiles_calendarEvents { get; set; }

        public string? storedFiles_carts { get; set; }

        public string? storedFiles_categories { get; set; }

        public string? storedFiles_emailQueueAttachments { get; set; }

        public string? storedFiles_messageAttachments { get; set; }

        public string? storedFiles_products { get; set; }

        public string? storedFiles_purchaseOrders { get; set; }

        public string? storedFiles_salesInvoices { get; set; }

        public string? storedFiles_salesOrders { get; set; }

        public string? storedFiles_salesQuotes { get; set; }

        public string? storedFiles_salesReturns { get; set; }

        public string? storedFiles_sampleRequests { get; set; }

        public string? storedFiles_users { get; set; }

        public UrlConfig? images_root { get; set; }

        public string? images_suffix { get; set; }

        public string? images_accounts { get; set; }

        public string? images_ads { get; set; }

        public string? images_brands { get; set; }

        public string? images_calendarEvents { get; set; }

        public string? images_categories { get; set; }

        public string? images_countries { get; set; }

        public string? images_currencies { get; set; }

        public string? images_languages { get; set; }

        public string? images_manufacturers { get; set; }

        public string? images_products { get; set; }

        public string? images_regions { get; set; }

        public string? images_stores { get; set; }

        public string? images_users { get; set; }

        public string? images_vendors { get; set; }

        public UrlConfig? imports_root { get; set; }

        public string? imports_suffix { get; set; }

        public string? imports_excels { get; set; }

        public string? imports_products { get; set; }

        public string? imports_productPricePoints { get; set; }

        public string? imports_salesQuotes { get; set; }

        public string? imports_users { get; set; }
    }
}
