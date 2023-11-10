// <copyright file="CEFConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF Config class</summary>
// ReSharper disable StyleCop.SA1300
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
// ReSharper disable MissingXmlDoc
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.Collections.Generic;

    /**
     * { type: "Store", whichUrl: "primary" }, // Provided by Store Lookup using the SiteDomain.Url property value
     * { type: "Store", whichUrl: "alternate-1" }, // Provided by Store Lookup using the SiteDomain.AlternateUrl1 property value
     * { type: "Store", whichUrl: "alternate-2" }, // Provided by Store Lookup using the SiteDomain.AlternateUrl2 property value
     * { type: "Store", whichUrl: "alternate-3" }, // Provided by Store Lookup using the SiteDomain.AlternateUrl3 property value
     * { type: "Brand", whichUrl: "primary" }, // Provided by Brand Lookup using the SiteDomain.Url property value
     * { type: "Brand", whichUrl: "alternate-1" }, // Provided by Brand Lookup using the SiteDomain.AlternateUrl1 property value
     * { type: "Brand", whichUrl: "alternate-2" }, // Provided by Brand Lookup using the SiteDomain.AlternateUrl2 property value
     * { type: "Brand", whichUrl: "alternate-3" }, // Provided by Brand Lookup using the SiteDomain.AlternateUrl3 property value
     */
    public interface IUrlHostConfig
    {
        public string type { get; set; }

        public string whichUrl { get; set; }
    }

    public interface IUrlConfig
    {
        /**
         * The domain where the area is hosted. This can be provided by the API via Brand or Store lookup
         * by Site Domain (except the API area itself). Leave null to use the current domain.
         * @warning Do not end with an '/'
         * @memberof IUrlConfig
         */
        public string host { get; set; }
        /**
         * The relative path to the area. This can be provided by the API via Brand or Store lookup by Site
         * Domain (except the API area itself). Leave null to use the root or look up via Brand or Store. This
         * should be relative to the host or return value of the Brand or Store lookup by Site Domain if set.
         * @warning Do not end with an '/'
         * @memberof IUrlConfig
         */
        public string root { get; set; }
        /**
         * When set to "Brand", the UI Host Domain will be provided by the first active site domain on the Brand.
         * When set to "Store", the UI Host Domain will be provided by the first active site domain on the Store.
         * When set to false, the UI Host Domain wil be provided by however host is set.
         * @warning This setting overrides ui.host when set to "Brand" or "Store"
         * @example
         * // Not provided by lookup
         * false
         * // Provided by Store Lookup using the SiteDomain.Url
         * { type: "Store", whichUrl: "primary" }
         * // Provided by Store Lookup using SiteDomain.AlternateUrl1
         * { type: "Store", whichUrl: "alternate-1" }
         * // Provided by Store Lookup using SiteDomain.AlternateUrl2
         * { type: "Store", whichUrl: "alternate-2" }
         * // Provided by Store Lookup using SiteDomain.AlternateUrl3
         * { type: "Store", whichUrl: "alternate-3" }
         * // Provided by Brand Lookup using SiteDomain.Url
         * { type: "Brand", whichUrl: "primary" }
         * // Provided by Brand Lookup using SiteDomain.AlternateUrl1
         * { type: "Brand", whichUrl: "alternate-1" }
         * // Provided by Brand Lookup using SiteDomain.AlternateUrl2
         * { type: "Brand", whichUrl: "alternate-2" }
         * // Provided by Brand Lookup using SiteDomain.AlternateUrl3
         * { type: "Brand", whichUrl: "alternate-3" }
         * @memberof IUrlConfig
         */
        public IUrlHostConfig hostIsProvidedByLookup { get; set; } // | bool;
    }

    public enum CheckoutModes
    {
        Single = 0, // the default
        Targets = 1,
    }

    public interface CartType
    {
        public string type { get; set; }
    }

    public interface ICheckoutConfigFlags
    {
        /**
         * Set the state of create account checkbox default.
         * @default true
         * @type {bool}
         * @memberof CheckoutConfig.flags
         */
        public bool createAccount { get; set; }
    }

    public interface IPurchaseStepConfig
    {
        public bool show { get; set; }
        public bool showButton { get; set; }
        public string name { get; set; }
        public string titleKey { get; set; }
        public string continueTextKey { get; set; }
        public string templateURL { get; set; }
        public int order { get; set; }
    }

    public interface IRegistrationStepConfig
    {
        public bool show { get; set; }
        public bool showButton { get; set; }
        public string name { get; set; }
        public string titleKey { get; set; }
        public string continueTextKey { get; set; }
        public string templateURL { get; set; }
        public int order { get; set; }
    }

    public interface IUplifts
    {
        /**
         * A percentage uplift
         * @example
         *  0.03 // 3% increase
         *  -0.03 // 3% decrease
         * @default null
         * @type decimal
         */
        public decimal percent { get; set; }
        /**
         * An amount uplift
         * @example
         *  5.00 // $5.00 increase
         *  -5.00 // $5.00 decrease
         * @default null
         * @type decimal
         */
        public decimal amount { get; set; }
    }

    public interface IPurchasePaymentMethodConfig
    {
        public string name { get; set; }
        public string titleKey { get; set; }
        public int order { get; set; }
        public string templateURL { get; set; }
        /**
         * Uplifts can be positive or negative
         * @default null
         * @type {{ percent: decimal, amount: decimal }}
         */
        public IUplifts uplifts { get; set; }
    }

    public interface IPurchaseConfig
    {
        public bool allowGuest { get; set; }
        public Dictionary<string, IPurchaseStepConfig> sections { get; set; }
        public Dictionary<string, IPurchasePaymentMethodConfig> paymentMethods { get; set; }
        public bool showSpecialInstructions { get; set; }
    }

    public interface IRegistrationConfig
    {
        public Dictionary<string, IRegistrationStepConfig> sections { get; set; }
    }

    public interface IPersonalDetailsDisplay
    {
        /**
         * Address Book hide keys
         * @default undefined
         * @type {bool}
         */
        public bool hideAddressBookKeys { get; set; }
        /**
         * Address Book hide first name
         * @default undefined
         * @type {bool}
         */
        public bool hideAddressBookFirstName { get; set; }
        /**
         * Address Book hide first name
         * @default undefined
         * @type {bool}
         */
        public bool hideAddressBookLastName { get; set; }
        /**
         * Address Book hide last name
         * @default undefined
         * @type {bool}
         */
        public bool hideAddressBookEmail { get; set; }
        /**
         * Address Book hide phone
         * @default undefined
         * @type {bool}
         */
        public bool hideAddressBookPhone { get; set; }
        /**
         * Address Book hide fax
         * @default undefined
         * @type {bool}
         */
        public bool hideAddressBookFax { get; set; }
        /**
         * Hide billing first name
         * @default undefined
         * @type {bool}
         */
        public bool hideBillingFirstName { get; set; }
        /**
         * Hide billing first name
         * @default undefined
         * @type {bool}
         */
        public bool hideBillingLastName { get; set; }
        /**
         * Hide billing last name
         * @default undefined
         * @type {bool}
         */
        public bool hideBillingEmail { get; set; }
        /**
         * Hide billing phone
         * @default undefined
         * @type {bool}
         */
        public bool hideBillingPhone { get; set; }
        /**
         * Hide billing fax
         * @default undefined
         * @type {bool}
         */
        public bool hideBillingFax { get; set; }
        /**
         * Hide shipping first name
         * @default undefined
         * @type {bool}
         */
        public bool hideShippingFirstName { get; set; }
        /**
         * Hide shipping last name
         * @default undefined
         * @type {bool}
         */
        public bool hideShippingLastName { get; set; }
        /**
         * Hide shipping email
         * @default undefined
         * @type {bool}
         */
        public bool hideShippingEmail { get; set; }
        /**
         * Hide shipping phone
         * @default undefined
         * @type {bool}
         */
        public bool hideShippingPhone { get; set; }
        /**
         * Hide shipping fax
         * @default undefined
         * @type {bool}
         */
        public bool hideShippingFax { get; set; }
    }

    public interface IDashboardSettings
    {
        /**
         * The name to be injected to ID's and Name's of elements (no spaces)
         * @type {string}
         * @memberof IDashboardSettings
         */
        public string name { get; set; }
        /**
         * The title's current translated value (will be set by the UI, don't put in cefConfig)
         * @type {string}
         * @memberof IDashboardSettings
         */
        public string title { get; set; }
        /**
         * The title's translation key
         * @type {string}
         * @memberof IDashboardSettings
         */
        public string titleKey { get; set; }
        /**
         * The name of the UI Router state for this section
         * @type {string}
         * @memberof IDashboardSettings
         */
        public string sref { get; set; }
        /**
         * When enabled, the section will show. When disabled, the section will
         * not be generated into the menus
         * @type {bool}
         * @memberof IDashboardSettings
         */
        public bool enabled { get; set; }
        /**
         * The SVG image content for the icon
         * @type {string}
         * @memberof IDashboardSettings
         */
        public string icon { get; set; }
        /**
         * The array of child sections (if any)
         * @type {Array{IDashboardSettings}}
         * @memberof IDashboardSettings
         */
        public IDashboardSettings[] children { get; set; }
        /**
         * The sort order for display
         * @type {int}
         * @memberof IDashboardSettings
         */
        public int order { get; set; }
        /**
         * Requires the user to have at least one of these roles to be displayed
         * @type {Array{string}}
         * @memberof IDashboardSettings
         */
        public string[] reqAnyRoles { get; set; }
        /**
         * Requires the user to have at least one of these permissions to be displayed
         * @type {Array{string}}
         * @memberof IDashboardSettings
         */
        public string[] reqAnyPerms { get; set; }
    }

    public interface IPaymentConfig
    {
        public bool enabled { get; set; }
        /**
         * Uplifts can be positive or negative
         * @default null
         * @type {{ percent: decimal, amount: decimal }}
         */
        public IUplifts uplifts { get; set; }
    }

    public class IPricingConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public ISimpleEnablableFeature? priceRules { get; set; }
        public ISimpleEnablableFeature? pricePoints { get; set; }
        public ISimpleEnablableFeature? msrp { get; set; }
        public ISimpleEnablableFeature? reduction { get; set; }
    }

    public class ITaxesConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public ISimpleEnablableFeature? avalara { get; set; }
    }

    public class IPurchasingConfig
    {
        public ISimpleEnablableFeature? availabilityDates { get; set; }
        public ISimpleEnablableFeature? documentRequired { get; set; }
        public ISimpleEnablableFeature? documentRequired_override { get; set; }
        public ISimpleEnablableFeature? minMax { get; set; }
        public ISimpleEnablableFeature? minMax_after { get; set; }
        public ISimpleEnablableFeature? minOrder { get; set; }
        public ISimpleEnablableFeature? roleRequiredToPurchase { get; set; }
        public ISimpleEnablableFeature? roleRequiredToSee { get; set; }
        public ISimpleEnablableFeature? mustPurchaseInMultiplesOf { get; set; }
        public ISimpleEnablableFeature? mustPurchaseInMultiplesOf_override { get; set; }
    }

    public class IShippingRatesConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public ISimpleEnablableFeature? estimator { get; set; }
        public ISimpleEnablableFeature? flat { get; set; }
        public bool productsCanBeFreeShipping { get; set; }
    }

    public class ISplitShippingConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public bool onlyAllowOneDestination { get; set; }
    }

    public class IShippingConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public ISimpleEnablableFeature? carrierAccounts { get; set; }
        public ISimpleEnablableFeature? events { get; set; }
        public ISimpleEnablableFeature? packages { get; set; }
        public ISimpleEnablableFeature? masterPacks { get; set; }
        public ISimpleEnablableFeature? pallets { get; set; }
        public ISimpleEnablableFeature? shipToStore { get; set; }
        public ISimpleEnablableFeature? inStorePickup { get; set; }
        public IShippingRatesConfig? rates { get; set; }
        public ISimpleEnablableFeature? restrictions { get; set; }
        public ISplitShippingConfig? splitShipping { get; set; }
        public ISplitShippingConfig? handlingFees { get; set; }
    }

    public class IInventoryPreSaleMaxPerProductAccountConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public ISimpleEnablableFeature? after { get; set; }
    }

    public class IInventoryPreSaleConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public ISimpleEnablableFeature? maxPerProductGlobal { get; set; }
        public IInventoryPreSaleMaxPerProductAccountConfig? maxPerProductPerAccount { get; set; }
    }

    public class IInventoryBackOrderMaxPerProductAccountConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public ISimpleEnablableFeature? after { get; set; }
    }

    public class IInventoryBackOrderConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public ISimpleEnablableFeature? maxPerProductGlobal { get; set; }
        public IInventoryBackOrderMaxPerProductAccountConfig? maxPerProductPerAccount { get; set; }
    }

    public class IInventoryAdvancedConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public bool countOnlyThisStoresWarehouseStock { get; set; }
    }


    public class IInventoryConfig : ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
        public ISimpleEnablableFeature? carrierAccounts { get; set; }
        public ISimpleEnablableFeature? events { get; set; }
        public ISimpleEnablableFeature? packages { get; set; }
        public ISimpleEnablableFeature? masterPacks { get; set; }
        public ISimpleEnablableFeature? pallets { get; set; }
        public ISimpleEnablableFeature? shipToStore { get; set; }
        public ISimpleEnablableFeature? inStorePickup { get; set; }
        public IShippingRatesConfig? rates { get; set; }
        public ISimpleEnablableFeature? restrictions { get; set; }
        public ISplitShippingConfig? splitShipping { get; set; }
        public ISplitShippingConfig? handlingFees { get; set; }
    }

    public interface ISimpleEnablableFeature
    {
        public bool enabled { get; set; }
    }

    public interface IAddressBookFeatureSet
    {
        public bool enabled { get; set; }
        public bool dashboardCanAddAddresses { get; set; }
        public bool allowMakeThisMyNewDefaultBillingInCheckout { get; set; }
        public bool allowMakeThisMyNewDefaultBillingInDashboard { get; set; }
        public bool allowMakeThisMyNewDefaultShippingInCheckout { get; set; }
        public bool allowMakeThisMyNewDefaultShippingInDashboard { get; set; }
    }

    public interface ICartsFeatureSet
    {
        public bool enabled { get; set; }
        /**
         * The Relative URL to the Cart page
         * Do not leave a trailing slash
         * @default "/Cart"
         * @type {string}
         * @memberof CefConfig
         */
        public string cartUrlFragment { get; set; }
        public ISimpleEnablableFeature compare { get; set; }
        public ISimpleEnablableFeature favoritesList { get; set; }
        public ISimpleEnablableFeature notifyMeWhenInStock { get; set; }
        public ISimpleEnablableFeature shoppingLists { get; set; }
        public ISimpleEnablableFeature wishList { get; set; }
        /**
         * Enabled debug lines in the angular cart service
         * @type {ISimpleEnablableFeature}
         */
        public ISimpleEnablableFeature serviceDebug { get; set; }
    }

    public interface ICategoriesFeatureSet
    {
        public bool enabled { get; set; }
        /**
         * The Relative URL to the Category page before the SEO URL is appended
         * Do not leave a trailing slash
         * @default "/Category"
         * @type {string}
         * @memberof CefConfig
         */
        public string categoryUrlFragment { get; set; }
    }

    public interface IFeatureSet
    {
        public ISimpleEnablableFeature ads { get; set; }
        /**
         * Site-wide activation of address book UI and functionality
         * @default true
         * @type {bool}
         */
        public IAddressBookFeatureSet addressBook { get; set; }
        public ISimpleEnablableFeature badges { get; set; }
        public ISimpleEnablableFeature brands { get; set; }
        public ISimpleEnablableFeature calendarEvents { get; set; }
        public ICartsFeatureSet carts { get; set; }
        public ICategoriesFeatureSet categories { get; set; }
        public ISimpleEnablableFeature chat { get; set; }
        public ISimpleEnablableFeature contacts_phonePrefixLookups { get; set; }
        public ISimpleEnablableFeature discounts { get; set; }
        public bool emails_enabled { get; set; }
        public ISimpleEnablableFeature emails_splitTemplates { get; set; }
        public bool inventory_enabled { get; set; }
        public bool inventory_backOrder_enabled { get; set; }
        public ISimpleEnablableFeature inventory_backOrder_maxPerProductGlobal { get; set; }
        public bool inventory_backOrder_maxPerProductPerAccount_enabled { get; set; }
        public ISimpleEnablableFeature inventory_backOrder_maxPerProductPerAccount_after { get; set; }
        public bool inventory_preSale_enabled { get; set; }
        public ISimpleEnablableFeature inventory_preSale_maxPerProductGlobal { get; set; }
        public bool inventory_preSale_maxPerProductPerAccount_enabled { get; set; }
        public ISimpleEnablableFeature inventory_preSale_maxPerProductPerAccount_after { get; set; }
        public bool inventory_advanced_enabled { get; set; }
        /**
         * When counting inventory data for display, sum all locations
         * together (false) or limit to only this store (true)
         * @default false
         * @type {bool}
         */
        public bool inventory_advanced_countOnlyThisStoresWarehouseStock { get; set; }
        public bool languages_enabled { get; set; }
        /**
         * The default language to use before the user selects their own
         * @default "en_US"
         * @type {string}
         * @memberof CefConfig
         */
        public string languages_default { get; set; }
        public ISimpleEnablableFeature login { get; set; }
        public ISimpleEnablableFeature manufacturers { get; set; }
        public ISimpleEnablableFeature multiCurrency { get; set; }
        public ISimpleEnablableFeature nonProductFavorites { get; set; }
        public bool payments_enabled { get; set; }
        public ISimpleEnablableFeature payments_memberships { get; set; }
        public IPaymentConfig payments_methods_creditCard { get; set; }
        public IPaymentConfig payments_methods_eCheck { get; set; }
        public IPaymentConfig payments_methods_credit { get; set; }
        public ISimpleEnablableFeature payments_subscriptions { get; set; }
        /**
         * Site-wide activation of wallet UI and functionality
         * @default true
         * @type {bool}
         */
        public bool payments_wallet_enabled { get; set; }
        public ISimpleEnablableFeature payments_wallet_creditCard { get; set; }
        public ISimpleEnablableFeature payments_wallet_eCheck { get; set; }
        public IPricingConfig pricing { get; set; }
        public ISimpleEnablableFeature products_categoryAttributes { get; set; }
        public ISimpleEnablableFeature products_futureImports { get; set; }
        public ISimpleEnablableFeature products_notifications { get; set; }
        public ISimpleEnablableFeature products_restrictions { get; set; }
        public ISimpleEnablableFeature products_storedFiles { get; set; }
        public ISimpleEnablableFeature profanityFilter { get; set; }
        public bool profile_enabled { get; set; }
        public ISimpleEnablableFeature profile_images { get; set; }
        public ISimpleEnablableFeature profile_storedFiles { get; set; }
        public bool purchaseOrders_enabled { get; set; }
        public bool purchaseOrders_hasIntegratedKeys { get; set; }
        public ISimpleEnablableFeature purchasing_availabilityDates { get; set; }
        public bool purchasing_documentRequired_enabled { get; set; }
        public ISimpleEnablableFeature purchasing_documentRequired_override { get; set; }
        public bool purchasing_minMax_enabled { get; set; }
        public ISimpleEnablableFeature purchasing_minMax_after { get; set; }
        public bool purchasing_minOrder_enabled { get; set; }
        public ISimpleEnablableFeature purchasing_roleRequiredToPurchase { get; set; }
        public ISimpleEnablableFeature purchasing_roleRequiredToSee { get; set; }
        public ISimpleEnablableFeature referralRegistrations { get; set; }
        public bool registration_usernameIsEmail { get; set; }
        public bool registration_addressBookIsRequired { get; set; }
        public bool registration_walletIsRequired { get; set; }
        public ISimpleEnablableFeature reporting { get; set; }
        public ISimpleEnablableFeature reviews { get; set; }
        public bool salesGroups_enabled { get; set; }
        public bool salesGroups_hasIntegratedKeys { get; set; }
        public bool salesInvoices_enabled { get; set; }
        public bool salesInvoices_hasIntegratedKeys { get; set; }
        public bool salesOrders_enabled { get; set; }
        public bool salesOrders_hasIntegratedKeys { get; set; }
        public bool salesQuotes_enabled { get; set; }
        public bool salesQuotes_hasIntegratedKeys { get; set; }
        /**
         * Include the Quantities in valuation of quotes (adds extra columns to the UI and import/exports)
         * @default true
         * @type {bool}
         */
        public bool salesQuotes_includeQuantity { get; set; }
        /**
         * The Relative URL to the Quote Cart page. Note we are pointing at the regular
         * Cart page to host both controls by default, but this option lets us specify
         * a separate page if a client override needs it.
         * Do not leave a trailing slash
         * @default "/Quote"
         * @type {string}
         * @memberof CefConfig
         */
        public string salesQuotes_quoteCartUrlFragment { get; set; }
        public bool salesReturns_enabled { get; set; }
        public bool salesReturns_hasIntegratedKeys { get; set; }
        public bool sampleRequests_enabled { get; set; }
        public bool sampleRequests_hasIntegratedKeys { get; set; }
        public bool shipping_enabled { get; set; }
        public ISimpleEnablableFeature shipping_carrierAccounts { get; set; }
        public ISimpleEnablableFeature shipping_events { get; set; }
        public ISimpleEnablableFeature shipping_packages { get; set; }
        public ISimpleEnablableFeature shipping_masterPacks { get; set; }
        public ISimpleEnablableFeature shipping_pallets { get; set; }
        public ISimpleEnablableFeature shipping_shipToStore { get; set; }
        public ISimpleEnablableFeature shipping_inStorePickup { get; set; }
        public bool shipping_rates_enabled { get; set; }
        public ISimpleEnablableFeature shipping_rates_estimator { get; set; }
        public ISimpleEnablableFeature shipping_rates_flat { get; set; }
        public bool shipping_rates_productsCanBeFreeShipping { get; set; }
        public ISimpleEnablableFeature shipping_restrictions { get; set; }
        public bool shipping_splitShipping_enabled { get; set; }
        public bool shipping_splitShipping_onlyAllowOneDestination { get; set; }
        /**
         * Site-wide activation of stores UI and functionality
         * @default true
         * @type {bool}
         */
        public bool stores_enabled { get; set; }
        public ISimpleEnablableFeature stores_myStoreAdmin { get; set; }
        public ISimpleEnablableFeature stores_myBrandAdmin { get; set; }
        public ISimpleEnablableFeature stores_siteDomains { get; set; }
        public ISimpleEnablableFeature stores_socialProviders { get; set; }
        /**
         * The Relative URL to the Store Detail page before the SEO URL is appended
         * Do not leave a trailing slash
         * @default "/Store"
         * @type {string}
         * @memberof CefConfig
         */
        public string stores_storeDetailUrlFragment { get; set; }
        /**
         * The Relative URL to the Store Locator page
         * Do not leave a trailing slash
         * @default "/Store-Locator"
         * @type {string}
         * @memberof CefConfig
         */
        public string stores_storeLocatorUrlFragment { get; set; }
        public ITaxesConfig taxes { get; set; }
        /**
         * When enabled, the tracking snippet will generate visit tracking information
         * @default false
         * @type {bool}
         */
        public bool tracking_enabled { get; set; }
        /**
         * How long the visit cookies last before expiration (in minutes)
         * @default 120
         * @type {int}
         */
        public int tracking_expires { get; set; }
        public ISimpleEnablableFeature vendors { get; set; }
    }
}
