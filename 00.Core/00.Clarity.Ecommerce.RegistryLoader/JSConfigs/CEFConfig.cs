// <copyright file="CEFConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF Config class</summary>
// ReSharper disable InconsistentNaming, StyleCop.SA1300, StyleCop.SA1302, UnusedMember.Global
// ReSharper disable StyleCop.SA1402
#pragma warning disable SA1201, SA1300, SA1302, SA1516, SA1600, SA1602, SA1604, SA1649
#pragma warning disable IDE1006, 1591
#nullable disable
namespace Clarity.Ecommerce.JSConfigs
{
    using System.Collections.Generic;

    public class CEFConfig
    {
        public bool debug { get; set; }
        public string authProvider { get; set; }
        public string authProviderAuthorizeUrl { get; set; }
        public string authProviderLogoutUrl { get; set; }
        public string authProviderClientId { get; set; }
        public string authProviderRedirectUri { get; set; }
        public string authProviderScope { get; set; }
        public string dateFormat { get; set; }
        public bool showDNNButton { get; set; }
        public bool showStorefrontButton { get; set; }
        public string countryCode { get; set; }
        public string apiKey { get; set; }
        public string[] corsResourceWhiteList { get; set; }
        public bool useSubDomainForCookies { get; set; }
        public int usePartialSubDomainForCookiesRootSegmentCount { get; set; }
        public bool requireSecureForCookies { get; set; }
        public UrlConfig api { get; set; }
        public UrlConfig ui { get; set; }
        public UrlConfig uiTemplateOverride { get; set; }
        public UrlConfig site { get; set; }
        public UrlConfig terms { get; set; }
        public UrlConfig privacy { get; set; }
        public UrlConfig contactUs { get; set; }
        public UrlConfig login { get; set; }
        public UrlConfig registration { get; set; }
        public UrlConfig forcedPasswordReset { get; set; }
        public UrlConfig forgotPassword { get; set; }
        public UrlConfig forgotUsername_siteSettings { get; set; }
        public string forgotUsername_email { get; set; }
        public UrlConfig membershipRegistration { get; set; }
        public UrlConfig myBrandAdmin { get; set; }
        public UrlConfig myStoreAdmin { get; set; }
        public UrlConfig myVendorAdmin { get; set; }
        public UrlConfig connectLive { get; set; }
        public UrlConfig admin { get; set; }
        public UrlConfig reporting { get; set; }
        public UrlConfig scheduler { get; set; }
        public UrlConfig connect { get; set; }
        public UrlConfig companyLogo { get; set; }
        public bool google_maps_enabled { get; set; }
        public string google_maps_apiKey { get; set; }
        public string google_apiKey { get; set; }
        public string google_apiClientKey { get; set; }
        public string companyName { get; set; }
        public string productDetailUrlFragment { get; set; }
        public string dashboardUrlFragment { get; set; }
        public DashboardSettings[] dashboard { get; set; }
        public string catalog_root { get; set; }
        public string[] catalog_extraRoots { get; set; }
        public int catalog_showCategoriesForLevelsUpTo { get; set; }
        public int catalog_defaultPageSize { get; set; }
        public string catalog_defaultFormat { get; set; }
        public string catalog_defaultSort { get; set; }
        public bool catalog_onlyApplyStoreToFilterByUI { get; set; }
        public bool catalog_displayImages { get; set; }
        public bool catalog_getFullAssocs { get; set; }
        public CheckoutConfig checkout { get; set; }
        public PurchaseConfig purchase { get; set; }
        public RegistrationConfig register { get; set; }
        public PersonalDetailsDisplay personalDetailsDisplay { get; set; }
        public bool billing_enabled { get; set; }
        public bool billing_payments_enabled { get; set; }
        public bool miniCart_enabled { get; set; }
        public bool usePhonePrefixLookups_enabled { get; set; }
        public bool facebookPixelService_enabled { get; set; }
        public bool googleTagManager_enabled { get; set; }
        public bool loginForPricing_enabled { get; set; }
        public string loginForPricing_key { get; set; }
        public bool loginForInventory_enabled { get; set; }
        public bool disableAddToCartModals { get; set; }
        public FeatureSet featureSet { get; set; }
        public UrlConfig storedFiles_root { get; set; }
        public string storedFiles_suffix { get; set; }
        public string storedFiles_accounts { get; set; }
        public string storedFiles_calendarEvents { get; set; }
        public string storedFiles_carts { get; set; }
        public string storedFiles_categories { get; set; }
        public string storedFiles_emailQueueAttachments { get; set; }
        public string storedFiles_messageAttachments { get; set; }
        public string storedFiles_products { get; set; }
        public string storedFiles_purchaseOrders { get; set; }
        public string storedFiles_salesInvoices { get; set; }
        public string storedFiles_salesOrders { get; set; }
        public string storedFiles_salesQuotes { get; set; }
        public string storedFiles_salesReturns { get; set; }
        public string storedFiles_sampleRequests { get; set; }
        public string storedFiles_users { get; set; }
        public UrlConfig images_root { get; set; }
        public string images_suffix { get; set; }
        public string images_accounts { get; set; }
        public string images_ads { get; set; }
        public string images_brands { get; set; }
        public string images_calendarEvents { get; set; }
        public string images_categories { get; set; }
        public string images_countries { get; set; }
        public string images_currencies { get; set; }
        public string images_languages { get; set; }
        public string images_manufacturers { get; set; }
        public string images_products { get; set; }
        public string images_regions { get; set; }
        public string images_stores { get; set; }
        public string images_users { get; set; }
        public string images_vendors { get; set; }
        public UrlConfig imports_root { get; set; }
        public string imports_suffix { get; set; }
        public string imports_excels { get; set; }
        public string imports_products { get; set; }
        public string imports_productPricePoints { get; set; }
        public string imports_salesQuotes { get; set; }
        public string imports_users { get; set; }
    }

    public class UrlHostConfig
    {
        public string type { get; set; }
        public string whichUrl { get; set; }
    }

    public class UrlConfig
    {
        public string host { get; set; }
        public string root { get; set; }
        public UrlHostConfig hostIsProvidedByLookup { get; set; } // | bool;
    }

    public enum CheckoutModes
    {
        Single = 0, // the default
        Targets = 1,
    }

    public class CEFConfigCartType
    {
        public string type { get; set; }
    }

    public class CheckoutStore
    {
        public bool showShipToStoreOption { get; set; }
        public bool showInStorePickupOption { get; set; }
    }

    public class PaymentSection
    {
        public bool creditCard { get; set; }
        public bool invoice { get; set; }
        public bool payPal { get; set; }
    }

    public class TemplateSection
    {
        public bool active { get; set; }
        public bool complete { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public bool show { get; set; }
        public string headingDetailsURL { get; set; }
        public int position { get; set; }
        public string templateURL { get; set; }
        public string continueText { get; set; }
        public bool showButton { get; set; }
        public TemplateSection[] children { get; set; }
    }

    public class CheckoutConfigFlags
    {
        public bool createAccount { get; set; }
    }

    public class CheckoutConfig
    {
        public CheckoutConfigFlags flags { get; set; }
        public string root { get; set; }
        public bool useRecentlyUsedAddresses { get; set; }
        public CheckoutModes mode { get; set; }
        public bool dontAllowCreateAccount { get; set; }
        public bool stepEnterByClick { get; set; }
        public bool usernameIsEmail { get; set; }
        public string defaultPaymentMethod { get; set; }
        public PaymentSection paymentOptions { get; set; }
        public CEFConfigCartType cart { get; set; }
        public CheckoutStore store { get; set; }
        public TemplateSection[] sections { get; set; }
        public string finalActionButtonText { get; set; }
        public PersonalDetailsDisplay personalDetailsDisplay { get; set; }
        public bool specialInstructions { get; set; }
    }

    public class PurchaseStepConfig
    {
        public bool show { get; set; }
        public bool showButton { get; set; }
        public string name { get; set; }
        public string titleKey { get; set; }
        public string continueTextKey { get; set; }
        public string templateURL { get; set; }
        public int order { get; set; }
    }

    public class RegistrationStepConfig
    {
        public bool show { get; set; }
        public bool showButton { get; set; }
        public string name { get; set; }
        public string titleKey { get; set; }
        public string continueTextKey { get; set; }
        public string templateURL { get; set; }
        public int order { get; set; }
    }

    public class Uplifts
    {
        public decimal percent { get; set; }
        public decimal amount { get; set; }
    }

    public class PurchasePaymentMethodConfig
    {
        public string name { get; set; }
        public string titleKey { get; set; }
        public int order { get; set; }
        public string templateURL { get; set; }
        public Uplifts uplifts { get; set; }
    }

    public class PurchaseConfig
    {
        public bool allowGuest { get; set; }
        public Dictionary<string, PurchaseStepConfig> sections { get; set; }
        public Dictionary<string, PurchasePaymentMethodConfig> paymentMethods { get; set; }
        public bool showSpecialInstructions { get; set; }
    }

    public class RegistrationConfig
    {
        public Dictionary<string, RegistrationStepConfig> sections { get; set; }
    }

    public class PersonalDetailsDisplay
    {
        public bool hideAddressBookKeys { get; set; }
        public bool hideAddressBookFirstName { get; set; }
        public bool hideAddressBookLastName { get; set; }
        public bool hideAddressBookEmail { get; set; }
        public bool hideAddressBookPhone { get; set; }
        public bool hideAddressBookFax { get; set; }
        public bool hideBillingFirstName { get; set; }
        public bool hideBillingLastName { get; set; }
        public bool hideBillingEmail { get; set; }
        public bool hideBillingPhone { get; set; }
        public bool hideBillingFax { get; set; }
        public bool hideShippingFirstName { get; set; }
        public bool hideShippingLastName { get; set; }
        public bool hideShippingEmail { get; set; }
        public bool hideShippingPhone { get; set; }
        public bool hideShippingFax { get; set; }
    }

    public class DashboardSettings
    {
        public string name { get; set; }
        public string title { get; set; }
        public string titleKey { get; set; }
        public string sref { get; set; }
        public bool enabled { get; set; }
        public string icon { get; set; }
        public DashboardSettings[] children { get; set; }
        public int order { get; set; }
        public string[] reqAnyRoles { get; set; }
        public string[] reqAnyPerms { get; set; }
    }

    public class PaymentConfig
    {
        public bool enabled { get; set; }
        public Uplifts uplifts { get; set; }
    }

    public class PricingConfig
    {
        public bool enabled { get; set; }
        public SimpleEnablableFeature priceRules { get; set; }
        public SimpleEnablableFeature pricePoints { get; set; }
        public SimpleEnablableFeature msrp { get; set; }
        public SimpleEnablableFeature reduction { get; set; }
    }

    public class TaxesConfig
    {
        public bool enabled { get; set; }
        public SimpleEnablableFeature avalara { get; set; }
    }

    public class PurchasingConfig
    {
        public SimpleEnablableFeature availabilityDates { get; set; }
        public SimpleEnablableFeature documentRequired { get; set; }
        public SimpleEnablableFeature documentRequired_override { get; set; }
        public SimpleEnablableFeature minMax { get; set; }
        public SimpleEnablableFeature minMax_after { get; set; }
        public SimpleEnablableFeature minOrder { get; set; }
        public SimpleEnablableFeature roleRequiredToPurchase { get; set; }
        public SimpleEnablableFeature roleRequiredToSee { get; set; }
        public SimpleEnablableFeature mustPurchaseInMultiplesOf { get; set; }
        public SimpleEnablableFeature mustPurchaseInMultiplesOf_override { get; set; }
    }

    public class ShippingRatesConfig
    {
        public bool enabled { get; set; }
        public SimpleEnablableFeature estimator { get; set; }
        public SimpleEnablableFeature flat { get; set; }
        public bool productsCanBeFreeShipping { get; set; }
    }

    public class SplitShippingConfig
    {
        public bool enabled { get; set; }
        public bool onlyAllowOneDestination { get; set; }
    }

    public class ShippingConfig
    {
        public bool enabled { get; set; }
        public SimpleEnablableFeature carrierAccounts { get; set; }
        public SimpleEnablableFeature events { get; set; }
        public SimpleEnablableFeature packages { get; set; }
        public SimpleEnablableFeature masterPacks { get; set; }
        public SimpleEnablableFeature pallets { get; set; }
        public SimpleEnablableFeature shipToStore { get; set; }
        public SimpleEnablableFeature inStorePickup { get; set; }
        public ShippingRatesConfig rates { get; set; }
        public SimpleEnablableFeature restrictions { get; set; }
        public SplitShippingConfig splitShipping { get; set; }
        public SimpleEnablableFeature leadTimes { get; set; }
        public SimpleEnablableFeature handlingFees { get; set; }
    }

    public class InventoryPreSaleMaxPerProductAccountConfig
    {
        public bool enabled { get; set; }
        public SimpleEnablableFeature after { get; set; }
    }

    public class InventoryPreSaleConfig
    {
        public bool enabled { get; set; }
        public SimpleEnablableFeature maxPerProductGlobal { get; set; }
        public InventoryPreSaleMaxPerProductAccountConfig maxPerProductPerAccount { get; set; }
    }

    public class InventoryBackOrderMaxPerProductAccountConfig
    {
        public bool enabled { get; set; }
        public SimpleEnablableFeature after { get; set; }
    }

    public class InventoryBackOrderConfig
    {
        public bool enabled { get; set; }
        public SimpleEnablableFeature maxPerProductGlobal { get; set; }
        public InventoryBackOrderMaxPerProductAccountConfig maxPerProductPerAccount { get; set; }
    }

    public class InventoryAdvancedConfig
    {
        public bool enabled { get; set; }
        public bool countOnlyThisStoresWarehouseStock { get; set; }
    }

    public class InventoryConfig
    {
        public bool enabled { get; set; }
        public InventoryBackOrderConfig backOrder { get; set; }
        public InventoryPreSaleConfig preSale { get; set; }
        public InventoryAdvancedConfig advanced { get; set; }
    }

    public class SimpleEnablableFeature
    {
        public bool enabled { get; set; }
    }

    public class AddressBookFeatureSet
    {
        public bool enabled { get; set; }
        public bool dashboardCanAddAddresses { get; set; }
        public bool allowMakeThisMyNewDefaultBillingInCheckout { get; set; }
        public bool allowMakeThisMyNewDefaultBillingInDashboard { get; set; }
        public bool allowMakeThisMyNewDefaultShippingInCheckout { get; set; }
        public bool allowMakeThisMyNewDefaultShippingInDashboard { get; set; }
    }

    public class CartsFeatureSet
    {
        public bool enabled { get; set; }
        public string cartUrlFragment { get; set; }
        public SimpleEnablableFeature compare { get; set; }
        public SimpleEnablableFeature favoritesList { get; set; }
        public SimpleEnablableFeature notifyMeWhenInStock { get; set; }
        public SimpleEnablableFeature shoppingLists { get; set; }
        public SimpleEnablableFeature wishList { get; set; }
        public SimpleEnablableFeature serviceDebug { get; set; }
    }

    public class CategoriesFeatureSet
    {
        public bool enabled { get; set; }
        public string categoryUrlFragment { get; set; }
    }

    public class FeatureSet
    {
        public SimpleEnablableFeature ads { get; set; }
        public AddressBookFeatureSet addressBook { get; set; }
        public SimpleEnablableFeature badges { get; set; }
        public SimpleEnablableFeature brands { get; set; }
        public SimpleEnablableFeature calendarEvents { get; set; }
        public CartsFeatureSet carts { get; set; }
        public CategoriesFeatureSet categories { get; set; }
        public SimpleEnablableFeature chat { get; set; }
        public SimpleEnablableFeature contacts_phonePrefixLookups { get; set; }
        public SimpleEnablableFeature discounts { get; set; }
        public bool emails_enabled { get; set; }
        public SimpleEnablableFeature emails_splitTemplates { get; set; }
        public InventoryConfig inventory { get; set; }
        public bool languages_enabled { get; set; }
        public string languages_default { get; set; }
        public SimpleEnablableFeature login { get; set; }
        public SimpleEnablableFeature manufacturers { get; set; }
        public SimpleEnablableFeature multiCurrency { get; set; }
        public SimpleEnablableFeature nonProductFavorites { get; set; }
        public bool payments_enabled { get; set; }
        public SimpleEnablableFeature payments_memberships { get; set; }
        public PaymentConfig payments_methods_creditCard { get; set; }
        public PaymentConfig payments_methods_eCheck { get; set; }
        public PaymentConfig payments_methods_credit { get; set; }
        public SimpleEnablableFeature payments_subscriptions { get; set; }
        public bool payments_wallet_enabled { get; set; }
        public SimpleEnablableFeature payments_wallet_creditCard { get; set; }
        public SimpleEnablableFeature payments_wallet_eCheck { get; set; }
        public PricingConfig pricing { get; set; }
        public SimpleEnablableFeature products_categoryAttributes { get; set; }
        public SimpleEnablableFeature products_futureImports { get; set; }
        public SimpleEnablableFeature products_notifications { get; set; }
        public SimpleEnablableFeature products_restrictions { get; set; }
        public SimpleEnablableFeature products_storedFiles { get; set; }
        public SimpleEnablableFeature profanityFilter { get; set; }
        public bool profile_enabled { get; set; }
        public SimpleEnablableFeature profile_images { get; set; }
        public SimpleEnablableFeature profile_storedFiles { get; set; }
        public bool purchaseOrders_enabled { get; set; }
        public bool purchaseOrders_hasIntegratedKeys { get; set; }
        public PurchasingConfig purchasing { get; set; }
        public SimpleEnablableFeature referralRegistrations { get; set; }
        public bool registration_usernameIsEmail { get; set; }
        public bool registration_addressBookIsRequired { get; set; }
        public bool registration_walletIsRequired { get; set; }
        public SimpleEnablableFeature reporting { get; set; }
        public SimpleEnablableFeature reviews { get; set; }
        public bool salesGroups_enabled { get; set; }
        public bool salesGroups_hasIntegratedKeys { get; set; }
        public bool salesInvoices_enabled { get; set; }
        public bool salesInvoices_hasIntegratedKeys { get; set; }
        public bool salesOrders_enabled { get; set; }
        public bool salesOrders_hasIntegratedKeys { get; set; }
        public bool salesQuotes_enabled { get; set; }
        public bool salesQuotes_hasIntegratedKeys { get; set; }
        public bool salesQuotes_includeQuantity { get; set; }
        public string salesQuotes_quoteCartUrlFragment { get; set; }
        public bool salesReturns_enabled { get; set; }
        public bool salesReturns_hasIntegratedKeys { get; set; }
        public bool sampleRequests_enabled { get; set; }
        public bool sampleRequests_hasIntegratedKeys { get; set; }
        public ShippingConfig shipping { get; set; }
        public bool stores_enabled { get; set; }
        public SimpleEnablableFeature stores_myStoreAdmin { get; set; }
        public SimpleEnablableFeature stores_myBrandAdmin { get; set; }
        public SimpleEnablableFeature stores_siteDomains { get; set; }
        public SimpleEnablableFeature stores_socialProviders { get; set; }
        public string stores_storeDetailUrlFragment { get; set; }
        public string stores_storeLocatorUrlFragment { get; set; }
        public TaxesConfig taxes { get; set; }
        public bool tracking_enabled { get; set; }
        public int tracking_expires { get; set; }
        public SimpleEnablableFeature vendors { get; set; }
    }
}
