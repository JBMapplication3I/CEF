module cef.admin.core {
    /**
     *  { type: "Store", whichUrl: "primary" }, // Provided by Store Lookup using the SiteDomain.Url property value
     *  { type: "Store", whichUrl: "alternate-1" }, // Provided by Store Lookup using the SiteDomain.AlternateUrl1 property value
     *  { type: "Store", whichUrl: "alternate-2" }, // Provided by Store Lookup using the SiteDomain.AlternateUrl2 property value
     *  { type: "Store", whichUrl: "alternate-3" }, // Provided by Store Lookup using the SiteDomain.AlternateUrl3 property value
     *  { type: "Brand", whichUrl: "primary" }, // Provided by Brand Lookup using the SiteDomain.Url property value
     *  { type: "Brand", whichUrl: "alternate-1" }, // Provided by Brand Lookup using the SiteDomain.AlternateUrl1 property value
     *  { type: "Brand", whichUrl: "alternate-2" }, // Provided by Brand Lookup using the SiteDomain.AlternateUrl2 property value
     *  { type: "Brand", whichUrl: "alternate-3" }, // Provided by Brand Lookup using the SiteDomain.AlternateUrl3 property value
     */
    export interface IUrlHostConfig {
        type: string;
        whichUrl: string;
    }

    export interface IUrlConfig {
        /**
         * The domain where the area is hosted. This can be provided by the API via Brand or Store lookup
         * by Site Domain (except the API area itself). Leave null to use the current domain.
         * @warning Do not end with an '/'
         * @memberof IUrlConfig
         */
        host: string;
        /**
         * The relative path to the area. This can be provided by the API via Brand or Store lookup by Site
         * Domain (except the API area itself). Leave null to use the root or look up via Brand or Store. This
         * should be relative to the host or return value of the Brand or Store lookup by Site Domain if set.
         * @warning Do not end with an '/'
         * @memberof IUrlConfig
         */
        root: string;
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
        hostIsProvidedByLookup: IUrlHostConfig | boolean;
    }

    export enum CheckoutModes {
        Single = 0, // the default
        Targets = 1
    }

    export interface CartType {
        type: string;
    }

    export interface CheckoutStore {
        showShipToStoreOption: boolean;
        showInStorePickupOption: boolean;
    }

    export interface PaymentSection {
        creditCard: boolean;
        invoice: boolean;
        payPal: boolean;
    }

    export interface TemplateSection {
        active: boolean;
        complete: boolean;
        name: string;
        title: string;
        show: boolean;
        headingDetailsURL: string;
        position: number;
        templateURL: string;
        continueText: string;
        showButton: boolean;
        children: TemplateSection[];
    }

    export interface CheckoutConfig {
        /**
         * Use flags to set default values within checkout
         * @memberof CheckoutConfig
         */
        flags: {
            /**
             * Set the state of create account checkbox default.
             * @default true
             * @type {boolean}
             * @memberof CheckoutConfig.flags
             */
            createAccount: boolean;
        };
        /**
         * The Relative URL to the Checkout page
         * Do not leave a trailing slash
         * @default "/Checkout"
         * @type {string}
         * @memberof CheckoutConfig
         */
        root: string;
        /**
         * When populating the UI with Billing and Shipping details, re-use the
         * last order's information when possible
         * Note: Only possible when using Single and Split (not Targets) checkouts
         * @default false
         * @type {boolean}
         * @memberof CheckoutConfig
         */
        useRecentlyUsedAddresses: boolean;
        /**
         * Which endpoint to call when completing checkout
         * @example Single = 0, Targets = 1
         * @default 0 Single
         * @type {CheckoutModes}
         * @memberof CheckoutConfig
         */
        mode: CheckoutModes;
        dontAllowCreateAccount: boolean;
        stepEnterByClick: boolean;
        usernameIsEmail: boolean;
        defaultPaymentMethod: string;
        paymentOptions: PaymentSection;
        cart: CartType;
        store: CheckoutStore;
        sections: TemplateSection[];
        finalActionButtonText: string;
        personalDetailsDisplay: IPersonalDetailsDisplay;
        specialInstructions: boolean;
    };

    export interface IPurchaseStepConfig {
        show: boolean;
        showButton: boolean;
        name: string;
        titleKey: string;
        continueTextKey: string;
        templateURL: string;
        order: number;
    }

    export interface IRegistrationStepConfig {
        show: boolean;
        showButton: boolean;
        name: string;
        titleKey: string;
        continueTextKey: string;
        templateURL: string;
        order: number;
    }

    export interface IPurchasePaymentMethodConfig {
        name: string;
        titleKey: string;
        order: number;
        templateURL: string;
        /**
         * Uplifts can be positive or negative
         * @default null
         * @type {{ useGreater: boolean, percent: number, amount: number }}
         */
        uplifts: {
            /**
             * When both are available, use the greater total charge.
             * (Percentage could result in a value less than the flat fee.)
             * @example
             *  false // use both
             *  true // use the greater fee
             * @type boolean
             */
            useGreater: boolean;
             /**
             * A percentage uplift
             * @example
             *  0.03 // 3% increase
             *  -0.03 // 3% decrease
             * @default null
             * @type number
             */
            percent: number;
            /**
             * An amount uplift
             * @example
             *  5.00 // $5.00 increase
             *  -5.00 // $5.00 decrease
             * @default null
             * @type number
             */
            amount: number;
        };
    }

    export interface IPurchaseConfig {
        allowGuest: boolean;
        sections: { [name: string]: IPurchaseStepConfig };
        paymentMethods: { [name: string]: IPurchasePaymentMethodConfig };
        showSpecialInstructions: boolean;
    }

    export interface IRegistrationConfig {
        sections: { [name: string]: IRegistrationStepConfig };
    }

    export interface IPersonalDetailsDisplay {
        /**
         * Address Book hide keys
         * @default undefined
         * @type {boolean}
         */
        hideAddressBookKeys: boolean;
        /**
         * Address Book hide first name
         * @default undefined
         * @type {boolean}
         */
        hideAddressBookFirstName: boolean;
        /**
         * Address Book hide first name
         * @default undefined
         * @type {boolean}
         */
        hideAddressBookLastName: boolean;
        /**
         * Address Book hide last name
         * @default undefined
         * @type {boolean}
         */
        hideAddressBookEmail: boolean;
        /**
         * Address Book hide phone
         * @default undefined
         * @type {boolean}
         */
        hideAddressBookPhone: boolean;
        /**
         * Address Book hide fax
         * @default undefined
         * @type {boolean}
         */
        hideAddressBookFax: boolean;
        /**
         * Hide billing first name
         * @default undefined
         * @type {boolean}
         */
        hideBillingFirstName: boolean;
        /**
         * Hide billing first name
         * @default undefined
         * @type {boolean}
         */
        hideBillingLastName: boolean;
        /**
         * Hide billing last name
         * @default undefined
         * @type {boolean}
         */
        hideBillingEmail: boolean;
        /**
         * Hide billing phone
         * @default undefined
         * @type {boolean}
         */
        hideBillingPhone: boolean;
        /**
         * Hide billing fax
         * @default undefined
         * @type {boolean}
         */
        hideBillingFax: boolean;
        /**
         * Hide shipping first name
         * @default undefined
         * @type {boolean}
         */
        hideShippingFirstName: boolean;
        /**
         * Hide shipping last name
         * @default undefined
         * @type {boolean}
         */
        hideShippingLastName: boolean;
        /**
         * Hide shipping email
         * @default undefined
         * @type {boolean}
         */
        hideShippingEmail: boolean;
        /**
         * Hide shipping phone
         * @default undefined
         * @type {boolean}
         */
        hideShippingPhone: boolean;
        /**
         * Hide shipping fax
         * @default undefined
         * @type {boolean}
         */
        hideShippingFax: boolean;
    }

    export interface IDashboardSettings {
        /**
         * The name to be injected to ID's and Name's of elements (no spaces)
         * @type {string}
         * @memberof IDashboardSettings
         */
        name: string;
        /**
         * The title's current translated value (will be set by the UI, don't put in cefConfig)
         * @type {string}
         * @memberof IDashboardSettings
         */
        title: string;
        /**
         * The title's translation key
         * @type {string}
         * @memberof IDashboardSettings
         */
        titleKey: string;
        /**
         * The name of the UI Router state for this section
         * @type {string}
         * @memberof IDashboardSettings
         */
        sref: string;
        /**
         * An alternate sref that should be considered for this section to be 'active'
         * @type {string}
         * @memberof IDashboardSettings
         */
        srefAlt?: string;
        /**
         * When enabled, the section will show. When disabled, the section will
         * not be generated into the menus
         * @type {boolean}
         * @memberof IDashboardSettings
         */
        enabled: boolean;
        /**
         * The SVG image content for the icon
         * @type {string}
         * @memberof IDashboardSettings
         */
        icon: string;
        /**
         * The array of child sections (if any)
         * @type {Array<IDashboardSettings>}
         * @memberof IDashboardSettings
         */
        children: Array<IDashboardSettings>;
        /**
         * The sort order for display
         * @type {number}
         * @memberof IDashboardSettings
         */
        order: number;
        /**
         * Requires the user to have at least one of these roles to be displayed
         * @type {Array<string>}
         * @memberof IDashboardSettings
         */
        reqAnyRoles: Array<string>;
        /**
         * Requires the user to have at least one of these permissions to be displayed
         * @type {Array<string>}
         * @memberof IDashboardSettings
         */
        reqAnyPerms: Array<string>;
    }

    export interface IPaymentConfig {
        enabled: boolean;
        /**
         * Uplifts can be positive or negative
         * @default null
         * @type {{ useGreater: boolean, percent: number, amount: number }}
         */
        uplifts: {
            /**
             * When both are available, use the greater total charge.
             * (Percentage could result in a value less than the flat fee.)
             * @example
             *  false // use both
             *  true // use the greater fee
             * @type boolean
             */
            useGreater: boolean;
            /**
             * A percentage uplift
             * @example
             *  0.03 // 3% increase
             *  -0.03 // 3% decrease
             * @default null
             * @type number
             */
            percent: number;
            /**
             * An amount uplift
             * @example
             *  5.00 // $5.00 increase
             *  -5.00 // $5.00 decrease
             * @default null
             * @type number
             */
            amount: number;
        };
    }

    export interface ISimpleEnablableFeature {
        enabled: boolean;
    }

    export interface IFeatureSet {
        ads: ISimpleEnablableFeature;
        /**
         * Site-wide activation of address book UI and functionality
         * @default true
         * @type {boolean}
         */
        addressBook: {
            enabled: boolean;
            dashboardCanAddAddresses: boolean;
            allowMakeThisMyNewDefaultBillingInCheckout: boolean;
            allowMakeThisMyNewDefaultBillingInDashboard: boolean;
            allowMakeThisMyNewDefaultShippingInCheckout: boolean;
            allowMakeThisMyNewDefaultShippingInDashboard: boolean;
        };
        badges: ISimpleEnablableFeature;
        brands: ISimpleEnablableFeature;
        calendarEvents: ISimpleEnablableFeature;
        carts: {
            enabled: boolean;
            compare: ISimpleEnablableFeature;
            favoritesList: ISimpleEnablableFeature;
            notifyMeWhenInStock: ISimpleEnablableFeature;
            shoppingLists: ISimpleEnablableFeature;
            wishList: ISimpleEnablableFeature;
            /**
             * Enabled debug lines in the angular cart service
             * @type {ISimpleEnablableFeature}
             */
            serviceDebug: ISimpleEnablableFeature;
        };
        categories: {
            enabled: boolean;
            minMax: boolean;
        };
        chat: ISimpleEnablableFeature;
        contacts: {
            phonePrefixLookups: ISimpleEnablableFeature;
        };
        discounts: ISimpleEnablableFeature;
        emails: {
            enabled: boolean;
            splitTemplates: ISimpleEnablableFeature;
        };
        inventory: {
            enabled: boolean;
            backOrder: {
                enabled: boolean;
                maxPerProductGlobal: ISimpleEnablableFeature;
                maxPerProductPerAccount: {
                    enabled: boolean;
                    after: ISimpleEnablableFeature;
                };
            };
            preSale: {
                enabled: boolean;
                maxPerProductGlobal: ISimpleEnablableFeature;
                maxPerProductPerAccount: {
                    enabled: boolean;
                    after: ISimpleEnablableFeature;
                };
            };
            advanced: {
                enabled: boolean;
                /**
                 * When counting inventory data for display, sum all locations
                 * together (false) or limit to only this store (true)
                 * @default false
                 * @type {boolean}
                 */
                countOnlyThisStoresWarehouseStock: boolean;
            };
            unlimited: {
                enabled: boolean;
            };
        };
        languages: {
            enabled: boolean;
            /**
             * The default language to use before the user selects their own
             * @default "en_US"
             * @type {string}
             * @memberof CefConfig
             */
            default: string;
        };
        login: {
            enabled: boolean;
            emailForForgot: string;
        };
        manufacturers: {
            enabled: boolean;
            minMax: boolean;
        };
        multiCurrency: ISimpleEnablableFeature;
        nonProductFavorites: ISimpleEnablableFeature;
        payments: {
            enabled: boolean;
            memberships: ISimpleEnablableFeature;
            methods: {
                creditCard: IPaymentConfig;
                eCheck: IPaymentConfig;
                credit: IPaymentConfig;
            };
            subscriptions: ISimpleEnablableFeature;
            wallet: {
                /**
                 * Site-wide activation of wallet UI and functionality
                 * @default true
                 * @type {boolean}
                 */
                enabled: boolean;
                creditCard: ISimpleEnablableFeature;
                eCheck: ISimpleEnablableFeature;
            };
        };
        pricing: {
            enabled: boolean;
            priceRules: ISimpleEnablableFeature;
            pricePoints: ISimpleEnablableFeature;
            msrp: ISimpleEnablableFeature;
            reduction: ISimpleEnablableFeature;
        };
        products: {
            categoryAttributes: ISimpleEnablableFeature;
            futureImports: ISimpleEnablableFeature;
            notifications: ISimpleEnablableFeature;
            restrictions: ISimpleEnablableFeature;
            storedFiles: ISimpleEnablableFeature;
        };
        profanityFilter: ISimpleEnablableFeature;
        profile: {
            enabled: boolean;
            images: ISimpleEnablableFeature;
            storedFiles: ISimpleEnablableFeature;
        };
        purchaseOrders: {
            enabled: boolean;
            hasIntegratedKeys: boolean;
        };
        purchasing: {
            availabilityDates: ISimpleEnablableFeature;
            documentRequired: {
                enabled: boolean;
                override: ISimpleEnablableFeature;
            };
            minMax: {
                enabled: boolean;
                after: ISimpleEnablableFeature;
            };
            minOrder: {
                enabled: boolean;
            };
            roleRequiredToPurchase: ISimpleEnablableFeature;
            roleRequiredToSee: ISimpleEnablableFeature;
        };
        referralRegistrations: ISimpleEnablableFeature;
        registration: {
            usernameIsEmail: boolean;
            addressBookIsRequired: boolean;
            walletIsRequired: boolean;
        };
        reporting: ISimpleEnablableFeature;
        reviews: ISimpleEnablableFeature;
        salesGroups: {
            enabled: boolean;
            hasIntegratedKeys: boolean;
        };
        salesInvoices: {
            enabled: boolean;
            hasIntegratedKeys: boolean;
            lateFees: {
                day: number;
                amount: number;
                kind: string;
            }[];
            canPayViaUserDashboard: {
                single: {
                    enabled: false;
                    partial: false;
                };
                multiple: {
                    enabled: false;
                    partial: false;
                };
            };
            canPayViaCSR: {
                single: {
                    enabled: false;
                    partial: false;
                };
                multiple: {
                    enabled: false;
                    partial: false;
                };
            };
        };
        salesOrders: {
            enabled: boolean;
            hasIntegratedKeys: boolean;
        };
        salesQuotes: {
            enabled: boolean;
            hasIntegratedKeys: boolean;
            /**
             * Include the Quantities in valuation of quotes (adds extra columns to the UI and import/exports)
             * @default true
             * @type {boolean}
             */
            includeQuantity: boolean;
        };
        salesReturns: {
            enabled: boolean;
            hasIntegratedKeys: boolean;
        };
        sampleRequests: {
            enabled: boolean;
            hasIntegratedKeys: boolean;
        };
        shipping: {
            enabled: boolean;
            carrierAccounts: ISimpleEnablableFeature;
            events: ISimpleEnablableFeature;
            packages: ISimpleEnablableFeature;
            masterPacks: ISimpleEnablableFeature;
            pallets: ISimpleEnablableFeature;
            shipToStore: ISimpleEnablableFeature;
            inStorePickup: ISimpleEnablableFeature;
            rates: {
                enabled: boolean;
                estimator: ISimpleEnablableFeature;
                flat: ISimpleEnablableFeature;
                productsCanBeFreeShipping: boolean;
            };
            restrictions: ISimpleEnablableFeature;
            splitShipping: {
                enabled: boolean;
                onlyAllowOneDestination: boolean;
            };
        };
        stores: {
            /**
             * Site-wide activation of stores UI and functionality
             * @default true
             * @type {boolean}
             */
            enabled: boolean;
            myStoreAdmin: ISimpleEnablableFeature;
            myBrandAdmin: ISimpleEnablableFeature;
            siteDomains: ISimpleEnablableFeature;
            socialProviders: ISimpleEnablableFeature;
            minMax: boolean;
        };
        taxes: {
            enabled: boolean;
            avalara: ISimpleEnablableFeature;
        };
        tracking: {
            /**
             * When enabled, the tracking snippet will generate visit tracking information
             * @default false
             * @type {boolean}
             */
            enabled: boolean;
            /**
             * How long the visit cookies last before expiration (in minutes)
             * @default 120
             * @type {number}
             */
            expires: number;
        };
        vendors: {
            enabled: boolean;
            minMax: boolean;
        };
    }

    export interface CefConfig {
        /**
         * Debug mode shows extra debugging information in templates to make it easier
         * to troubleshoot complex issues.
         * @default false
         * @type {boolean}
         * @memberof CefConfig
         */
        debug: boolean;
        /**
         * Required for Angular UI Router, do not change
         * @type {{ enabled: boolean, requireBase: boolean } | boolean}
         * @memberof CefConfig
         */
        html5Mode: { enabled: boolean; requireBase: boolean; } | boolean;
        /**
         * What authentication method should be used when hitting the API
         * Possible Values: dotnetnuke, identity, openid
         * @warning As of v4.6, no provider should be used except identity without consulting Clarity Management
         * @default "identity"
         * @type {string}
         * @memberof CefConfig
         */
        authProvider: string;
        authProviderMFAEnabled: boolean,
        authProviderMFAByEmailEnabled: boolean,
        authProviderMFABySMSEnabled: boolean,
        authProviderAuthorizeUrl: string;
        authProviderLogoutUrl: string;
        authProviderClientId: string;
        authProviderRedirectUri: string;
        authProviderScope: string;
        dateFormat: string;
        showDNNButton: boolean;
        showStorefrontButton: boolean;
        /**
         * The default country code
         * If this is not set, default 'getRegions' calls will not run
         * @default "USA"
         * @type {string}
         * @memberof CefConfig
         */
        countryCode: string;
        apiKey: string;
        /**
         * @example
         * // "self" is already included
         * // Don't use any localhost with or without port numbers as it confuses Chrome
         * // Allow loading from our assets domain. Notice the difference between * and **
         * // Space added to prevent comment closing only
         *   "http://some.subdomain.website.com/ **",
         *   "https://some.subdomain.website.com/ **",
         *   "http://shop.my-website.com/ **",
         *   "https://shop.my-website.com/ **",
         *   "http://*.webdev.us/ **",
         *   "http://*.webdev.* / **"
         * @default []
         * @type {string[]}
         * @memberof CefConfig
         */
        corsResourceWhiteList: string[];
        /**
         * When true, will not remove the subdomain from the domain when setting cookies
         * @example shop.mysite.com will stay as shop.mysite.com
         * @default false
         * @type {boolean}
         * @memberof CefConfig
         */
        useSubDomainForCookies: boolean;
        /**
         * The number of segments to consider shared (counting from the right).
         * If there are more segments in the domain than this count, they will be
         * ignored in cookie domain paths for sharing.
         * @note Only applicable if {@see useSubDomainForCookies} is false.
         * @example
         * // with a value of 2, shop.mysite.com will become .mysite.com
         * // with a value of 2, sub.shop.mysite.com will become .mysite.com
         * // with a value of 3, sub.shop.mysite.com will become shop.mysite.com
         * @default 2
         * @type {number}
         * @memberof CefConfig
         */
        usePartialSubDomainForCookiesRootSegmentCount: number;
        /**
         * If set, will state if the cookie is marked secure or not based on the
         * boolean value
         * @default null
         * @type {boolean}
         * @memberof CefConfig
         */
        requireSecureForCookies: boolean;
        routes: {
            api: IUrlConfig;
            ui: IUrlConfig;
            uiTemplateOverride: IUrlConfig;
            site: IUrlConfig;
            terms: IUrlConfig;
            privacy: IUrlConfig;
            contactUs: IUrlConfig;
            login: IUrlConfig;
            registration: IUrlConfig;
            forcedPasswordReset: IUrlConfig;
            forgotPassword: IUrlConfig;
            forgotUsername: IUrlConfig;
            membershipRegistration: IUrlConfig;
            myBrandAdmin: IUrlConfig;
            myStoreAdmin: IUrlConfig;
            myVendorAdmin: IUrlConfig;
            connectLive: IUrlConfig;
            admin: IUrlConfig;
            reporting: IUrlConfig;
            scheduler: IUrlConfig;
            connect: IUrlConfig;
            companyLogo: IUrlConfig;
            productDetail: IUrlConfig;
            catalog: IUrlConfig;
            category: IUrlConfig;
            storeDetail: IUrlConfig;
            storeLocator: IUrlConfig;
            dashboard: IUrlConfig;
            storedFiles: IUrlConfig;
            images: IUrlConfig;
            imports: IUrlConfig;
        };
        google: {
            maps: {
                enabled: boolean;
                /**
                 * The Google Maps API Key
                 * @type {string}
                 * @memberof CefConfig
                 */
                apiKey: string;
            };
            /**
             * The Google general API Key
             * @type {string}
             * @memberof CefConfig
             */
            apiKey: string;
            /**
             * The Google API Client ID/Key/Secret
             * @type {string}
             * @memberof CefConfig
             */
            apiClientKey: string;
        };
        /**
         * The name of the company to display in SEO URLs and pages like Registration
         * @default "Clarity eCommerce Demo"
         * @type {string}
         * @memberof CefConfig
         */
        companyName: string;
        /**
         * Turn off/on dashboard features
         * @type {IDashboardSettings[]}
         * @memberof CefConfig
         */
        dashboard: Array<IDashboardSettings>;
        catalog: {
            /**
             * Search Catalog: How many levels of depth into Categories to show before we can display products
             * @default 1
             * @type {number}
             */
            showCategoriesForLevelsUpTo: number;
            /**
             * Search Catalog: The default page size to start with
             * @default 9
             * @type {number}
             */
            defaultPageSize: number;
            /**
             * Search Catalog: The default format (layout) to start with
             * @default "grid"
             * @type {string}
             */
            defaultFormat: string;
            /**
             * Search Catalog: The default Sort method to start with
             * @default "Relevance"
             * @type {string}
             */
            defaultSort: string;
            /**
             * Search Catalog: Apply the Store ID to the search only if the user has selected it via UI.
             * When false, Store ID will always be forced onto the search from the user's selected Store
             * @default true
             * @type {boolean}
             */
            onlyApplyStoreToFilterByUI: boolean;
            /**
             * Search Catalog: Sets the visibility setting for category landing page images.
             * @default true
             * @type {boolean}
             */
            displayImages: boolean;
            /**
             * Search Catalog: If true, any associations on products returned from the search will have
             * their data loaded to the {@link services.cvProductService} for use.
             * @warning This is a performance intensive action if there are many associations (such as
             * variants) on each product.
             * @default false
             * @type {boolean}
             */
            getFullAssocs: boolean;
        };
        checkout: CheckoutConfig;
        purchase: IPurchaseConfig;
        register: IRegistrationConfig;
        personalDetailsDisplay: IPersonalDetailsDisplay;
        ticketExchange: {
            enabled: boolean;
        }
        billing: {
            enabled: boolean;
            payments: {
                enabled: boolean;
            };
        };
        miniCart: {
            enabled: boolean;
        };
        usePhonePrefixLookups: {
            enabled: boolean;
        };
        facebookPixelService: {
            enabled: boolean;
        };
        googleTagManager: {
            enabled: boolean;
        };
        loginForPricing: {
            enabled: boolean;
            key: string;
        };
        loginForInventory: {
            enabled: boolean;
        };
        /**
         * Site-wide disabling of all modals that result from adding an item to the cart
         * @default false
         * @type {boolean}
         */
        disableAddToCartModals: boolean;
        featureSet: IFeatureSet;
        storedFiles: {
            suffix: string;
            accounts: string;
            calendarEvents: string;
            carts: string;
            categories: string;
            emailQueueAttachments: string;
            messageAttachments: string;
            products: string;
            purchaseOrders: string;
            salesInvoices: string;
            salesOrders: string;
            salesQuotes: string;
            salesReturns: string;
            sampleRequests: string;
            users: string;
        };
        images: {
            suffix: string;
            accounts: string;
            ads: string;
            brands: string;
            calendarEvents: string;
            categories: string;
            countries: string;
            currencies: string;
            languages: string;
            manufacturers: string;
            products: string;
            regions: string;
            stores: string;
            users: string;
            vendors: string;
        };
        imports: {
            suffix: string;
            excels: string;
            products: string;
            productPricePoints: string;
            salesQuotes: string;
            users: string;
        };
    }
}
