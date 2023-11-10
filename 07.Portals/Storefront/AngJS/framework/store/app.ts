module cef.store {
    export var cefApp = angular.module("cef.store", [
        "cefConfig",
        "angular-intro",
        "angularMoment",
        "angularPayments",
        "cefStoreTemplates",
        "credit-cards",
        "door3.css",
        "duScroll",
        "hl.sticky",
        "kendo.directives",
        "mgo-angular-wizard",
        "ngAnimate",
        "ngCookies",
        // "ngDialog",
        "ngMessages",
        "ngSanitize",
        "angular-bind-html-compile",
        "pascalprecht.translate",
        "ui.bootstrap",
        "ui.router.title",
        "ui.router",
        "ui.toggle",
        "uiGmapgoogle-maps",
        "usersnapLogging",
        "validation.match"
    ]);

    cefApp.config(($compileProvider: ng.ICompileProvider, cefConfig: core.CefConfig) => {
        $compileProvider.debugInfoEnabled(true || !cefConfig.debug);
        // Call angular.reloadWithDebugInfo(); from the console to enable debug after-the-fact
    });

    // Providers
    cefApp.constant("cvServiceStrings", services.serviceStrings);
    cefApp.provider("cvApi", api.CEFAPIProvider);

    // Services
    cefApp.service("errorInterceptor", core.ErrorInterceptor);

    cefApp.factory("$translateCookieStorageCustom", core.translations.translateCookieStorageCustomFactoryFn);
    cefApp.factory("subdomain", core.subdomainFactoryFn);
    cefApp.factory("CacheFactoryService", core.cacheFactoryServiceFactoryFn);
    cefApp.factory("cvPromiseFactory", factories.cvPromiseFactoryFn);

    cefApp.service("cvAddressBookService", services.AddressBookService);
    cefApp.service("cvAdService", services.AdService);
    cefApp.service("cvAttributeService", services.AttributeService);
    cefApp.service("cvAuctionService", services.AuctionService);
    cefApp.service("cvAuthenticationService", services.AuthenticationService);
    cefApp.service("cvCartService", services.CartService);
    cefApp.service("cvCategoryService", services.CategoryService);
    cefApp.service("cvChatService", services.ChatService);
    cefApp.service("cvCountryService", services.CountryService);
    cefApp.service("cvCreateBulkOrderService", services.CreateBulkOrderService)
    cefApp.service("cvCurrencyService", services.CurrencyService);
    cefApp.service("cvCurrentBrandService", services.CurrentBrandService);
    cefApp.service("cvCurrentStoreService", services.CurrentStoreService);
    cefApp.service("cvFormValidationService", services.FormValidationService);
    cefApp.service("cvFranchiseService", services.FranchiseService);
    cefApp.service("cvInventoryService", services.InventoryService);
    cefApp.service("cvLanguageService", services.LanguageService);
    cefApp.service("cvLocationUtilityService", services.cvLocationUtilityServiceFn);
    cefApp.service("cvLotService", services.LotService);
    cefApp.service("cvManufacturerService", services.ManufacturerService);
    cefApp.service("cvPricingService", services.PricingService);
    cefApp.service("cvProductReviewsService", services.ProductReviewsService)
    cefApp.service("cvProductService", services.ProductService);
    cefApp.service("cvPurchaseService", services.PurchaseService);
    cefApp.service("cvQuoteService", services.QuoteService);
    cefApp.service("cvRegionService", services.RegionService);
    cefApp.service("cvRegistrationService", services.RegistrationService);
    cefApp.service("cvRestrictedRegionCheckService", services.RestrictedRegionCheckService);
    cefApp.service("cvSearchCatalogProductCompareService", services.SearchCatalogProductCompareService);
    cefApp.service("cvSearchCatalogService", services.SearchCatalogService);
    cefApp.service("cvSecurityService", services.SecurityService);
    cefApp.service("cvStatesService", services.StatesService);
    cefApp.service("cvStatusesService", services.StatusesService);
    cefApp.service("cvStoreLocationService", services.StoreLocationService);
    cefApp.service("cvStoreService", services.StoreService);
    cefApp.service("cvSubmitQuoteService", services.SubmitQuoteService);
    cefApp.service("cvTypesService", services.TypesService);
    cefApp.service("cvUserLocationService", services.cvUserLocationServiceFn);
    cefApp.service("cvVendorService", services.VendorService);
    cefApp.service("cvViewStateService", services.ViewStateService);
    cefApp.service("cvWalletService", services.WalletService);
    cefApp.service("cvFacebookPixelService", services.FacebookPixelService);
    cefApp.service("cvGoogleTagManagerService", services.GoogleTagManagerService);

    cefApp.factory("cvContactFactory", factories.cvContactFactoryFn);
    cefApp.factory("cvLocationFactory", factories.cvLocationFactoryFn);
    cefApp.factory("cvMessagingFactory", factories.cvMessagingFactoryFn);

    // Filters
    cefApp.filter("globalizedCurrency", core.globalizedCurrencyFilterFn);
    cefApp.filter("decamelize", core.decamelizeFilterFn);
    cefApp.filter("camelCaseToHuman", core.camelCaseToHumanFilterFn);
    cefApp.filter("checkIfNotEmpty", core.checkIfNotEmptyFilterFn);
    cefApp.filter("encode", () => window.encodeURIComponent);
    cefApp.filter("decodeHtml", core.decodeHtmlFilterFn);
    cefApp.filter("dec2hex", core.dec2hexFilterFn);
    cefApp.filter("hex2char", core.hex2charFilterFn);
    cefApp.filter("modulo", core.moduloFilterFn);
    cefApp.filter("max", core.maxFilterFn);
    cefApp.filter("min", core.minFilterFn);
    cefApp.filter("boolNorm", core.boolNormFilterFn);
    cefApp.filter("nthElement", core.nthElementFilterFn);
    cefApp.filter("numberToTime", core.numberToTimeFilterFn);
    cefApp.filter("objectKeysLimitTo", core.objectKeysLimitToFilterFn);
    cefApp.filter("objectKeysFilter", core.objectKeysFilterFilterFn);
    cefApp.filter("toArray", core.toArrayFilterFn);
    cefApp.filter("tel", core.telFilterFn);
    cefApp.filter("trustedHtml", core.trustedHtmlFilterFn);
    cefApp.filter("zeroPadNumber", core.zeroPadNumberFilterFn);
    cefApp.filter("convertJSONDate", core.convertJsonDateFilterFn);
    cefApp.filter("limitToEllipses", core.limitToEllipsesFilterFn);
    cefApp.filter("modifiedValue", core.modifiedValueFilterFn);
    cefApp.filter("groupBy", core.groupByFilterFn);
    cefApp.filter("flatGroupBy", core.flatGroupByFilterFn);
    cefApp.filter("sumBy", core.sumByFilterFn);
    cefApp.filter("statusIDToText", core.statusIDToTextFilterFn);
    cefApp.filter("typeIDToText", core.typeIDToTextFilterFn);
    cefApp.filter("stateIDToText", core.stateIDToTextFilterFn);
    cefApp.filter("splitShippingGroupTitle", core.splitShippingGroupTitleFilterFn);
    cefApp.filter("attributeOrder", core.attributeOrderFilterFn);
    cefApp.filter("uniqueArray", core.uniqueArrayValueFn);
    cefApp.filter("removeUrlHash", core.removeUrlHashFn);
    cefApp.filter ("abs", core.absValueFilterFn);
    cefApp.filter ("unique", core.uniqueValueFilterFn);
    cefApp.filter ("splitOnSpace", core.splitBySpaceValueFilterFn);
    cefApp.filter ("roundDown", core.roundDown);
    cefApp.filter ("bracketWrap", core.bracketWrapFn);
    cefApp.filter ("splitByComma", core.splitByCommaValueFilterFn);

    // Filters specific to Storefront
    cefApp.filter("checkIfJsonAttributeFromPdc", getJsonAttributeFromPdcFilter => (input, attributeName) => {
        if (typeof input !== "object" && typeof attributeName !== "string") {
            return false;
        }
        var attributeValue = getJsonAttributeFromPdcFilter(input, attributeName);
        if (typeof attributeValue === "string" && attributeValue.length > 0) {
            return true;
        }
        return false;
    });
    cefApp.filter("checkIfJsonAttributeFromPdc", getJsonAttributeFromPdcFilter => (input, attributeName) => {
        if (typeof input !== "object" && typeof attributeName !== "string") {
            return false;
        }
        var attributeValue = getJsonAttributeFromPdcFilter(input, attributeName);
        if (typeof attributeValue === "string" && attributeValue.length > 0) {
            return true;
        }
        return false;
    });
    cefApp.filter("checkIfValuesInJsonAttributesAreEmpty", hideThoseDashesAndOtherThingsFilter => (input, attributeNames) => {
        if (!Array.isArray(attributeNames) && (typeof input !== "object")) {
            return false;
        }
        for (let arrayIndex = 0, arrayLength = attributeNames.length; arrayIndex < arrayLength; arrayIndex++) {
            const attributevaluestring = hideThoseDashesAndOtherThingsFilter(input[attributeNames[arrayIndex]]);
            if (typeof attributevaluestring === "string" && attributevaluestring.length > 0) {
                return true;
            }
        }
        return false;
    });
    cefApp.filter("getFirstAttributeFromPdc", getAttributeFromPdcFilter => (input, attributeNameArray) => {
        for (let i = 0; i < attributeNameArray.length; i++) {
            const value = getAttributeFromPdcFilter(input, attributeNameArray[i]);
            if (typeof value === "string" && value.length > 0) {
                return value;
            }
        }
        return "";
    });
    cefApp.filter("getFirstJsonAttributeFromPdc", getJsonAttributeFromPdcFilter => (input, attributeNameArray) => {
        for (let i = 0; i < attributeNameArray.length; i++) {
            const value = getJsonAttributeFromPdcFilter(input, attributeNameArray[i]);
            if (typeof value === "string" && value.length > 0) {
                return value;
            }
        }
        return "";
    });
    cefApp.filter("getFirstJsonAttributeFromPdcIncludingUofM", getJsonAttributeFromPdcIncludingUofMFilter => (input, attributeNameArray) => {
        for (let i = 0; i < attributeNameArray.length; i++) {
            const value = getJsonAttributeFromPdcIncludingUofMFilter(input, attributeNameArray[i]);
            if (typeof value === "string" && value.length > 0) {
                return value;
            }
        }
        return "";
    });
    cefApp.filter("getJsonAttributeFromPdc", hideThoseDashesAndOtherThingsFilter => (input, attributeName) => {
        if (!input || typeof input !== "object" && typeof attributeName !== "string") {
            return input;
        }
        if (input.product) {
            input = input.product;
        } else if (input.Product) {
            input = input.Product;
        }
        if (!input || !input.SerializableAttributes) {
            return ""; // No Attributes
        }
        if (!input.SerializableAttributes[attributeName]) {
            return ""; // Not found
        }
        return hideThoseDashesAndOtherThingsFilter(input.SerializableAttributes[attributeName].Value);
    });
    cefApp.filter("getJsonAttributeFromPdc", hideThoseDashesAndOtherThingsFilter => (input, attributeName) => {
        if (!input || typeof input !== "object" && typeof attributeName !== "string") {
            return input;
        }
        if (input.product) {
            input = input.product;
        } else if (input.Product) {
            input = input.Product;
        }
        if (!input || !input.SerializableAttributes) {
            return ""; // No Attributes
        }
        if (!input.SerializableAttributes[attributeName]) {
            return ""; // Not found
        }
        return hideThoseDashesAndOtherThingsFilter(
            input.SerializableAttributes[attributeName].Value +
            (input.SerializableAttributes[attributeName].UofM
                ? ` ${input.SerializableAttributes[attributeName].UofM}`
                : ""));
    });
    cefApp.filter("jsonAttributeHasMoreThanOneOption", () => obj => {
        if (!obj) { return obj; }
        const keys = { };
        for (let key in obj) {
            if (!obj.hasOwnProperty(key)) { continue; }
            if (obj[key].length <= 1) { continue; }
            keys[key] = obj[key];
        }
        return keys;
    });
    cefApp.filter("getAttr", () => (input, aKey: string): string => { // Don't use types if your models aren't consistent!
        input = input || [];
        var useLowerCase: boolean;
        if (input.length) {
            useLowerCase = input[0].hasOwnProperty("name");
        }
        var out = "";
        var inThere = _.find(input, () => (useLowerCase ? { name: aKey } as any : { Name: aKey } as any));
        if (inThere && inThere[useLowerCase ? "value" : "Value"]) { out = inThere[useLowerCase ? "value" : "Value"]; }
        return out;
    });
    cefApp.filter("cleanSearchCategoryName", () => (cat: api.CategoryModel): string => {
        if (!cat) { return null; }
        if (cat.DisplayName) {
            return cat.DisplayName;
        }
        let name: string = cat["Key"] || cat.Name;
        if (name.indexOf("|") !== -1) {
            name = name.split("|")[0];
        }
        name = name.replace(/\//, " / ").replace(/\s{2,}/, " ");
        return name;
    });
    cefApp.filter("acToDropdownItem", (cefConfig: core.CefConfig) => (accountContact: api.AccountContactModel): string => {
        const noSlave = angular.isUndefined(accountContact.Slave);
        if (noSlave) {
            throw new Error("acToDropdownItem No contact data, invalid setup");
        }
        const hasName = (accountContact.Slave.Address.CustomKey || accountContact.Slave.CustomKey)
            || accountContact.CustomKey;
        return (hasName && !cefConfig.personalDetailsDisplay.hideAddressBookKeys ? (hasName.toLocaleUpperCase() + ": ") : "")
            + accountContact.Slave.Address.Street1
            /* + (accountContact.IsBilling
                ? ' (Billing)'
                : (accountContact.IsPrimary
                    ? ' (Default Shipping)'
                    : ' (Shipping)'))*/;
    });
    cefApp.filter("cToDropdownItem", (cefConfig: core.CefConfig) => (contact: api.ContactModel): string => {
        const noContact = angular.isUndefined(contact);
        if (noContact) {
            throw new Error("cToDropdownItem No contact data, invalid setup");
        }
        if (contact.ID === -1) {
            // Add a New Address
            return contact.CustomKey;
        }
        const noAddress = angular.isUndefined(contact.Address);
        if (noAddress) {
            throw new Error("cToDropdownItem No contact data, invalid setup");
        }
        const hasName = contact.Address.CustomKey || contact.CustomKey;
        if (contact.Address.Street2) {
            return (hasName && !cefConfig.personalDetailsDisplay.hideAddressBookKeys ? (hasName.toLocaleUpperCase() + ": ") : "") +
                contact.Address.Street1 + " " +
                contact.Address.Street2 + " " +
                contact.Address.City + ", " +
                contact.Address.RegionName + ", " +
                contact.Address.PostalCode
        }
        return (hasName && !cefConfig.personalDetailsDisplay.hideAddressBookKeys ? (hasName.toLocaleUpperCase() + ": ") : "") +
            contact.Address.Street1 + " " +
            contact.Address.City + ", " +
            contact.Address.RegionName + ", " +
            contact.Address.PostalCode/* +
            (accountContact.IsBilling
                ? ' (Billing)'
                : (accountContact.IsPrimary
                    ? ' (Default Shipping)'
                    : ' (Shipping)'))*/;
    });

    // CORS
    cefApp.filter("corsLinkRootInner", core.corsLinkRootInnerFn);
    cefApp.filter("corsLinkRoot", core.corsLinkRootInnerFn);
    cefApp.filter("corsLink", core.corsLinkFn);
    cefApp.filter("corsProductLink", core.corsProductLinkFn);
    cefApp.filter("corsStoreLink", core.corsStoreLinkFn);
    cefApp.filter("corsImageLink", core.corsImageLinkFn);
    cefApp.filter("corsStoredFilesLink", core.corsStoredFilesLinkFn);
    cefApp.filter("corsImportsLink", core.corsImportsLinkFn);
    cefApp.filter("goToCORSLink", core.goToCORSLinkFn);

    // Directives
    cefApp.directive("cvGrid", core.cvGridDirectiveFn);
    cefApp.directive("cefCompiler", core.cefCompilerFn);
    cefApp.directive("uiSrefIf", core.uiRouterPlugins.uiSrefIfFn);
    cefApp.directive("uiSrefPlus", core.uiRouterPlugins.uiSrefPlusDirectiveFn);
    cefApp.directive("ngEnter", core.ngEnterDirectiveFn);
    cefApp.directive("convertToNumber", core.convertToNumberDirectiveFn);

    // Directives specifics to Storefront
    cefApp.directive("scrollTo", (
        $location: ng.ILocationService,
        $anchorScroll: ng.IAnchorScrollService,
        cvServiceStrings: services.IServiceStrings
        ): ng.IDirective => ({
        link: function(scope, element, attrs) {
            element.bind("click", event => {
                event.stopPropagation();
                scope.$on(cvServiceStrings.events.$state.locationChangeStart, ev => ev.preventDefault());
                var location = attrs["scrollTo"];
                $location.hash(location);
                $anchorScroll();
            });
        }
    }));

    // Controllers
    cefApp.controller("genericCtrl", core.GenericController);
    cefApp.controller("SearchTypeController", core.SearchTypeController);
    cefApp.controller("StoreSearchController", core.StoreSearchController);

    function $HttpParamSerializerNoQuotesProvider(): ng.IServiceProvider {
        function forEachSorted(obj, iterator, context = undefined) {
            const keys = Object.keys(obj).sort();
            for (let i = 0; i < keys.length; i++) {
                iterator.call(context, obj[keys[i]], keys[i]);
            }
            return keys;
        }
        function encodeUriQuery(val: string, pctEncodeSpaces = false): string {
            return encodeURIComponent(val)
                .replace(/%40/gi, "@")
                .replace(/%3A/gi, ":")
                .replace(/%24/g, "$")
                .replace(/%2C/gi, ",")
                .replace(/%3B/gi, ";")
                .replace(/%22/gi, "") // Double-quotes to nothing
                .replace(/%20/g, (pctEncodeSpaces ? "%20" : "+"));
        }
        function serializeValue(v: any): string {
            if (angular.isObject(v)) {
                return angular.isDate(v) ? v.toISOString() : angular.toJson(v);
            }
            return v;
        }
        /**
         * @ngdoc service
         * @name $httpParamSerializer
         * @description
         *
         * Default {@link $http `$http`} params serializer that converts objects to strings
         * according to the following rules:
         *
         * * `{'foo': 'bar'}` results in `foo=bar`
         * * `{'foo': Date.now()}` results in `foo=2015-04-01T09%3A50%3A49.262Z` (`toISOString()` and encoded representation of a Date object)
         * * `{'foo': ['bar', 'baz']}` results in `foo=bar&foo=baz` (repeated key for each array element)
         * * `{'foo': {'bar':'baz'}}` results in `foo=%7Bbar%3Abaz%7D"` (stringified and encoded representation of an object, but double-quotes stripped)
         *
         * Note that serializer will sort the request parameters alphabetically.
         */
        this.$get = function() {
            return function ngParamSerializer(params: any): string {
                if (!params) {
                    return "";
                }
                var parts = [];
                forEachSorted(params, (value, key) => {
                    if (value === null || angular.isUndefined(value)) {
                        return;
                    }
                    if (angular.isArray(value)) {
                        angular.forEach(value, (v, k) => {
                            parts.push(encodeUriQuery(key)  + "=" + encodeUriQuery(serializeValue(v)));
                        });
                        return;
                    }
                    parts.push(encodeUriQuery(key) + "=" + encodeUriQuery(serializeValue(value)));
                });
                return parts
                    .join("&")
                    // .replace(/,/, "%60") // This causes multiple parsing issues
                    ;
            };
        };
        return this;
    }
    cefApp.provider("$httpParamSerializerNoQuotes", $HttpParamSerializerNoQuotesProvider);

    // Configs
    cefApp.config((
        $locationProvider: ng.ILocationProvider,
        $httpProvider: ng.IHttpProvider,
        $sceDelegateProvider: ng.ISCEDelegateProvider,
        cefConfig: core.CefConfig
    ) => {
        $locationProvider.html5Mode(cefConfig.html5Mode as boolean);
        $locationProvider.hashPrefix("!");
        $httpProvider.interceptors.push("errorInterceptor");
        // Added for CORS start
        $httpProvider.defaults.withCredentials = true;
        $httpProvider.defaults.headers.common["Content-Type"] = "application/json";
        $httpProvider.defaults.headers.common["Vary"] = "Origin";
        delete $httpProvider.defaults.headers.common["X-Requested-With"];
        $httpProvider.defaults.paramSerializer = "$httpParamSerializerNoQuotes";
        $httpProvider.defaults.timeout = 10 * 1000;
        // Allow loading from our assets domain(s). Notice the difference between * and **. More can be added to the config.
        var arr = cefConfig.corsResourceWhiteList;
        // Allow loading from same domain
        arr.push("self");
        $sceDelegateProvider.resourceUrlWhitelist(arr);
        // Added for CORS end
    });

    cefApp.run(($http: ng.IHttpService, $filter: ng.IFilterService, $rootScope: ng.IRootScopeService, cefConfig: core.CefConfig, cvServiceStrings: services.IServiceStrings, $location: ng.ILocationService): void => {
        $rootScope.cefConfig = cefConfig;
        if (!cefConfig.featureSet.brands.enabled) {
            return;
        }
        const ems: api.BrandModel = {
            BrandSiteDomains: [
                {
                    MasterName: "ems",
                    SlaveName: "ems",
                    Slave: {
                        Url: "https://ems-jbm-test.jandbmedical.com",
                        Name: "ems",
                        ID: 1,
                        CustomKey: "ems",
                        Active: true,
                        CreatedDate: new Date("2022-04-28T12:52:54.1000000")
                    },
                    MasterID: 3,
                    MasterKey: "ems",
                    SlaveID: 1,
                    SlaveKey: "ems",
                    ID: 1,
                    Active: true,
                    CreatedDate: new Date("2022-04-28T12:53:30.1490000")
                }
            ],
            MinimumOrderDollarAmountOverrideFeeIsPercent: false,
            MinimumOrderQuantityAmountOverrideFeeIsPercent: false,
            Name: "ems",
            ID: 3,
            CustomKey: "ems",
            Active: true,
            CreatedDate: new Date("2022-04-28T12:44:20.7250000"),
            UpdatedDate: new Date("2023-07-25T21:08:44.6366045")
        }
        const medsurg: api.BrandModel = {
            BrandSiteDomains: [
                {
                    MasterName: "medsurg",
                    SlaveName: "medsurg",
                    Slave: {
                        Url: "https://medsurg-jbm-test.jandbmedical.com",
                        Name: "medsurg",
                        ID: 2,
                        CustomKey: "medsurg",
                        Active: true,
                        CreatedDate: new Date("2022-04-28T13:31:14.7380000")
                    },
                    MasterID: 4,
                    MasterKey: "medsurg",
                    SlaveID: 2,
                    SlaveKey: "medsurg",
                    ID: 2,
                    Active: true,
                    CreatedDate: new Date("2022-04-28T13:32:06.1540000")
                }
            ],
            MinimumOrderDollarAmountOverrideFeeIsPercent: false,
            MinimumOrderQuantityAmountOverrideFeeIsPercent: false,
            Name: "medsurg",
            ID: 4,
            CustomKey: "medsurg",
            Active: true,
            CreatedDate: new Date("2022-04-28T13:30:42.7920000"),
            UpdatedDate: new Date("2023-07-26T13:44:29.2356357")
        }
        let brandToUse: api.BrandModel;
        switch ($location.host()) {
            case 'ems-jbm-test.jandbmedical.com': {
                brandToUse  = ems;
                break;
            }
            default:
            case 'medsurg-jbm-test.jandbmedical.com': {
                brandToUse = medsurg;
                break;
            }
        }
        // $http<api.CEFActionResponseT<api.BrandModel>>({
        //     url: $filter("corsLink")("/Brands/Brand/Current", "api", "primary"),
        //     method: "GET",
        // }).then(r => {
        //     if (!r || !r.data || !r.data.Result
        //         || !r.data.Result.BrandSiteDomains
        //         || r.data.Result.BrandSiteDomains.length <= 0) {
        //         // We failed to get data
        //         return;
        //     }
            $rootScope["globalBrandSiteDomainID"] = brandToUse.BrandSiteDomains[0].SlaveID;
            $rootScope["globalBrand"] = brandToUse;
            $rootScope["globalBrandID"] = brandToUse.ID;
            if (angular.isDefined(brandToUse.BrandSiteDomains[0].Slave)) {
                $rootScope["globalBrandSiteDomain"] = brandToUse.BrandSiteDomains[0].Slave;
                $rootScope.$broadcast(cvServiceStrings.events.brands.globalBrandSiteDomainPopulated);
                return;
            }
            $http<api.SiteDomainModel>({
                url: $filter("corsLink")("/Stores/SiteDomain/ID/" + $rootScope["globalBrandSiteDomainID"], "api", "primary"),
                method: "GET",
            }).then(r => {
                if (!r || !r.data) {
                    // We failed to get data
                    return;
                }
                $rootScope["globalBrandSiteDomain"] = r.data;
                $rootScope.$broadcast(cvServiceStrings.events.brands.globalBrandSiteDomainPopulated);
            });
        });
    // });
    /** This hack helps get around typescript interfering with DI */
    cefApp.run($injector => (window as any).$injector = $injector);
    cefApp.config((uiGmapGoogleMapApiProvider, cefConfig: core.CefConfig) => {
        // if (cefConfig.google.maps.enabled) {
            uiGmapGoogleMapApiProvider.configure({ key: cefConfig.google.maps.apiKey, preventLoad: true });
            // Use preventLoad: true with "uiGmapMapScriptLoader" which is a service that has a .load() function to pull in the google maps script
        // }
    });

    // Translations
    cefApp.run(($translateCookieStorageCustom, subdomain: string, cefConfig: core.CefConfig) => {
        const options = <ng.cookies.ICookiesOptions>{
            path: "/", // Stay at the relative root instead of being page specific
            /* expires: never, */
            domain: cefConfig.useSubDomainForCookies || !subdomain
                ? window.location.host
                : window.location.host.replace(subdomain, "") // Allow use in multiple sub-domains
        };
        if (cefConfig.requireSecureForCookies) {
            options.secure = true;
        }
        $translateCookieStorageCustom.setDefaultOptions(options);
    });
    cefApp.config(($translateProvider: ng.translate.ITranslateProvider, cefConfig: core.CefConfig) => {
        $translateProvider.useSanitizeValueStrategy(null);
        $translateProvider.preferredLanguage(cefConfig.featureSet.languages.default);
        $translateProvider.fallbackLanguage(cefConfig.featureSet.languages.default);
        $translateProvider.useLoader("$translateAsyncPartialLoaderStore", {
            KeyStartsWith: "ui.storefront.",
            urlTemplate: "lang={lang}&part={part}"
        });
        $translateProvider.useStorage("$translateCookieStorageCustom");
    });
    cefApp.run(($translate: ng.translate.ITranslateService,
            $translateAsyncPartialLoaderStore: ng.translate.ITranslatePartialLoaderService,
            cefConfig: core.CefConfig) => {
        function addCurrentLanguagePart() {
            const lang = $translate.use() || cefConfig.featureSet.languages.default;
            $translateAsyncPartialLoaderStore.addPart(lang, 1, "lang={lang}&part={part}");
            $translate.refresh(lang);
        }
        if ($translate.isReady()) {
            addCurrentLanguagePart();
            return;
        }
        $translate.onReady().then(() => addCurrentLanguagePart());
    });

    export interface IModalComposeStateParams {
        subject?: string;
        msg?: string;
        contacts: Array<number>;
    }

    // UI Router States
    cefApp.config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $titleProvider: ng.ui.ITitleProvider,
        cefConfig: core.CefConfig
    ) => {
        const fullInner = urlConfig => {
            if (!urlConfig.host && !urlConfig.root) {
                // We don't have a static url or root path to apply
                return "";
            }
            if (!urlConfig.host && urlConfig.root) {
                // We don't have a static url, but we do have a root path to apply
                return urlConfig.root;
            }
            if (urlConfig.host && !urlConfig.root) {
                // We don't have a root path, but we do have a static url to apply
                return urlConfig.host;
            }
            // We have both a root path and a static url to apply
            return urlConfig.host + urlConfig.root;
        };
        const root = fullInner(cefConfig.routes.ui) + "/framework/store";
        const getCatSettings = () => cefConfig.catalog
            ? cefConfig.catalog
            : {
                defaultFormat: cefConfig.catalog.defaultFormat || "grid",
                defaultPageSize: cefConfig.catalog.defaultPageSize || 9,
                defaultSort: cefConfig.catalog.defaultSort || "Relevance",
                showCategoriesForLevelsUpTo: cefConfig.catalog.showCategoriesForLevelsUpTo || 1
            };
        const getDefaultCatalogRoute = (which: string = "p") => {
            const catSettings = getCatSettings();
            return (which === "s" ? true : catSettings.showCategoriesForLevelsUpTo === 0)
                ? `/c/${which}/Results`
                    + `/Format/${catSettings.defaultFormat ? catSettings.defaultFormat : "grid"}`
                    + `/Page/1`
                    + `/Size/${catSettings.defaultPageSize ? catSettings.defaultPageSize : 9}`
                    + `/Sort/${catSettings.defaultSort ? catSettings.defaultSort : "Relevance"}`
                : `/c/${which}/Categories/Level-1`;
        }
        const isCatalogUrl = () => {
            let catalogUrls = [/*...cefConfig.catalog.extraRoots*/];
            //// catalogUrls.push("/searchcatalog");
            //// catalogUrls.push("/search-catalog");
            catalogUrls.push(cefConfig.routes.catalog.root);
            const lowerCasePath = window.location.pathname.toLowerCase();
            for (let i = 0; i < catalogUrls.length; i++) {
                if (lowerCasePath.startsWith(catalogUrls[i].toLowerCase())) {
                    return true;
                }
            }
            return false;
        };
        const bigWhen = () => {
            /* TODO: Encapsulate this in a setting
            if (window.location.pathname.toLowerCase() === '/') {
                return "/Sign-In";
            }
            */
            if (cefConfig.paymentHubEnabled && window.location.pathname === "/") {
                return "/db" + cefConfig.routes.dashboard.root;
            }
            if (window.location.pathname.toLowerCase().startsWith(cefConfig.routes.dashboard.root.toLowerCase())) {
                return "/db" + cefConfig.routes.dashboard.root;
            }
            if (window.location.pathname.toLowerCase().startsWith(cefConfig.routes.category.root.toLowerCase())) {
                return "/cat";
            }
            if (window.location.pathname.toLowerCase().startsWith("/my-store")) {
                return "/ms/Dashboard";
            }
            if (window.location.pathname.toLowerCase().startsWith("/my-brand")) {
                return "/mb/Dashboard";
            }
            if (isCatalogUrl()) {
                return getDefaultCatalogRoute("p");
            }
            return "";
        };
        $urlRouterProvider
            .when("", bigWhen)
            .when("/", bigWhen)
            .when("/db",              "/db" + cefConfig.routes.dashboard.root)
            .when("/db/db" + cefConfig.routes.dashboard.root, "/db" + cefConfig.routes.dashboard.root)
            .when("/ms",              "/ms/Dashboard")
            .when("/mb",              "/mb/Dashboard")
            .when("/c",               () => getDefaultCatalogRoute("p"))
            .when("/c/Results",       () => getDefaultCatalogRoute("p"))
            .when("/c/p",             () => getDefaultCatalogRoute("p"))
            .when("/c/p/Results",     () => getDefaultCatalogRoute("p"))
            .when("/c/c",             () => getDefaultCatalogRoute("c"))
            .when("/c/c/Results",     () => getDefaultCatalogRoute("c"))
            .when("/c/s",             () => getDefaultCatalogRoute("s"))
            .when("/c/s/Results",     () => getDefaultCatalogRoute("s"))
            .when("/c/a",             () => getDefaultCatalogRoute("a"))
            .when("/c/a/Results",     () => getDefaultCatalogRoute("a"))
            .when("/c/l",             () => getDefaultCatalogRoute("l"))
            .when("/c/l/Results",     () => getDefaultCatalogRoute("l"))
            .when("/c/o",             () => getDefaultCatalogRoute("o"))
            .when("/c/o/Results",     () => getDefaultCatalogRoute("o"))
            .when("/c/m",             () => getDefaultCatalogRoute("m"))
            .when("/c/m/Results",     () => getDefaultCatalogRoute("m"))
            .when("/c/v",             () => getDefaultCatalogRoute("v"))
            .when("/c/v/Results",     () => getDefaultCatalogRoute("v"))
            ;
        $titleProvider.documentTitle(($rootScope: ng.IRootScopeService, $translate: ng.translate.ITranslateService, $q: ng.IQService) =>
            $q((resolve, reject) => {
                if ($rootScope.$breadcrumbs && $rootScope.$breadcrumbs.length > 0) {
                    resolve(`${$rootScope.$breadcrumbs.map(x => x.translatedTitle || x.title).join(" > ")} - ${cefConfig.companyName}`);
                    return;
                }
                if ($rootScope.$title) {
                    $translate($rootScope.$title)
                        .then(translated => resolve(`${translated} - ${cefConfig.companyName}`))
                        .catch(reason => reject(reason));
                    return;
                }
                resolve(`${cefConfig.companyName}`);
            })
        );
        generateStatesForDashboard($stateProvider, root, cefConfig);
        generateStatesForCatalogs($stateProvider, root, cefConfig);
    });

    function generateStatesForDashboard($stateProvider: ng.ui.IStateProvider, root: string, cefConfig: core.CefConfig): void {
        $stateProvider
            // ==== Begin Home ====
            // Home
            /* TODO: Encapsulate this in a setting
            .state("home", <ng.ui.IState>{
                url: "/Sign-In",
                views: { main: { template: '<div class="d-none" cef-login return-url="userDashboard.dashboard" after-login-action-mode="3"></div>' } }
            })
            */
            // ==== End Home ====
            // ==== Begin Login/Registration ====
            // Sign In
            // Register
            // Forgot Username
            // Forgot Password
            //     Return
            // ==== End Login/Registration ====
            // ==== Begin Dashboard ====
            .state("userDashboard", <ng.ui.IState>{ resolve: { },
                abstract: true, nomenu: true,
                url: "/db", title: "My Profile",
                views: { main: { template: `<div cef-user-dashboard class="col-12"></div>` } }
            })
                .state("userDashboard.dashboard", <ng.ui.IState>{
                    resolve: { $title: () => "ui.storefront.common.MyDashboard" },
                    icon: "far fa-tachometer-alt-fast",
                    url: "/Dashboard", parent: "userDashboard", hasOverrideTitleInTemplate: true,
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/dashboard.html" } }
                })
                .state("userDashboard.profileUser", <ng.ui.IState>{
                    resolve: { $title: () => "ui.storefront.menu.miniMenu.myProfile" },
                    url: "/My-Profile", parent: "userDashboard", hasOverrideTitleInTemplate: true,
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/profile.user.html" } }
                })
                .state("userDashboard.profileAccount", <ng.ui.IState>{
                    resolve: { $title: () => "ui.storefront.userDashboard2.userDashboard.AccountProfile" },
                    url: "/Account-Profile", parent: "userDashboard", hasOverrideTitleInTemplate: true,
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/profile.account.html" } }
                })
                .state("userDashboard.addressBook", <ng.ui.IState>{
                    resolve: { $title: () => "ui.storefront.common.AddressBook" },
                    url: "/Address-Book", parent: "userDashboard",
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/addressBook.html" } }
                })
                    .state("userDashboard.addressBook.list", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.AddressBook" },
                        url: "/List", nomenu: true, parent: "userDashboard.addressBook",
                        views: { book: { templateUrl: root + "/userDashboard/views/addressBook.html" } }
                    })
                    .state("userDashboard.addressBook.editor", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.AddressBook" },
                        url: "/Edit/:ID", nomenu: true, parent: "userDashboard.addressBook",
                        params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                        views: { book: { templateUrl: root + "/userDashboard/views/addressBookEditor.html" } }
                    })
                .state("userDashboard.chats", <ng.ui.IState>{ resolve: { },
                    url: "/Chats", parent: "userDashboard",
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/chats.html" } }
                })
                .state("userDashboard.payments", <ng.ui.IState>{ resolve: { },
                    url: "/Payments", parent: "userDashboard",
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/payments.html" } }
                })
                .state("userDashboard.wallet", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true,
                    url: "/Wallet", parent: "userDashboard",
                    views: { dashboard: { template: `<div class="row"><div ui-view="wallet" class="col-12"></div></div>` } }
                })
                    .state("userDashboard.wallet.list", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.userDashboard2.userDashboard.Wallet" },
                        url: "/List", parent: "userDashboard.wallet",
                        views: { wallet: { templateUrl: root + "/userDashboard/views/wallet.html" } }
                    })
                .state("userDashboard.users", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true, parent: "userDashboard",
                    url: "/Users",
                    views: { dashboard: { template: `<div class="row"><div ui-view="users" class="col-12"></div></div>` } }
                })
                    .state("userDashboard.users.list", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.User.Plural" },
                        url: "/List", parent: "userDashboard.users",
                        views: { users: { templateUrl: root + "/userDashboard/views/users.html" } }
                    })
                    .state("userDashboard.users.detail", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.User.UserDetail" },
                        url: "/Detail/:ID", nomenu: true, parent: "userDashboard.users", hasOverrideTitleInTemplate: true,
                        params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                        views: { users: { templateUrl: root + "/userDashboard/views/userDetail.html" } }
                    })
                    .state("userDashboard.users.import", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.ImportUser.Plural" },
                        url: "/Import", nomenu: true, parent: "userDashboard.users", hasOverrideTitleInTemplate: true,
                        views: { users: { templateUrl: root + "/userDashboard/views/usersImport.html" } }
                    })
                    .state("userDashboard.users.createBulkOrder", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.userDashboard2.controls.users.CreateBulkOrder" },
                        url: "/New-Bulk-Order", nomenu: true, parent: "userDashboard.users",
                        views: { users: { templateUrl: root + "/userDashboard/views/usersNewBulkOrder.html" } }
                    })
                .state("userDashboard.subscriptions", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true, parent: "userDashboard",
                    url: "/Subscriptions",
                    views: { dashboard: { template: `<div class="row"><div ui-view="subscriptions" class="col-12"></div></div>` } }
                    // views: { dashboard: { templateUrl: root + "/userDashboard/views/subscriptions.html" } }
                })
                    .state("userDashboard.subscriptions.list", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.userDashboard2.userDashboard.Subscriptions" },
                        url: "/List", parent: "userDashboard.subscriptions",
                        views: { subscriptions: { templateUrl: root + "/userDashboard/views/subscriptions.html" } }
                    })
                    .state("userDashboard.subscriptions.detail", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.userDashboard2.userDashboard.SubscriptionsDetail" },
                        url: "/Detail/:ID", nomenu: true, parent: "userDashboard.subscriptions",
                        params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                        views: { subscriptions: { templateUrl: root + "/userDashboard/views/subscriptionDetail.html" } }
                    })
                .state("userDashboard.salesGroups", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true, parent: "userDashboard",
                    icon: "far fa-object-group",
                    url: "/Sales-Groups",
                    views: { dashboard: { template: `<div class="row"><div ui-view="salesGroups" class="col-12"></div></div>` } }
                })
                    .state("userDashboard.salesGroups.list", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.SalesGroup" },
                        url: "/List", parent: "userDashboard.salesGroups",
                        views: { salesGroups: { templateUrl: root + "/userDashboard/views/salesGroups.html" } }
                    })
                    .state("userDashboard.salesGroups.detail", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.userDashboard2.controls.sales.GroupDetail" },
                        url: "/Detail/:GroupID", nomenu: true, parent: "userDashboard.salesGroups", hasOverrideTitleInTemplate: true,
                        params: <ng.ui.RawParams>{ GroupID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                        views: { salesGroups: { templateUrl: root + "/userDashboard/views/salesGroupDetail.html" } }
                    })
                        .state("userDashboard.salesGroups.detail.quote", <ng.ui.IState>{
                            resolve: { $title: () => "ui.storefront.userDashboard2.controls.sales.GroupDetail" },
                            url: "/Quote/:ID", nomenu: true, parent: "userDashboard.salesGroups.detail", hasOverrideTitleInTemplate: true,
                            params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                            views: { content: { templateUrl: root + "/userDashboard/views/salesGroupDetail.quote.html" } }
                        })
                        .state("userDashboard.salesGroups.detail.sample", <ng.ui.IState>{
                            resolve: { $title: () => "ui.storefront.userDashboard2.controls.sales.GroupDetail" },
                            url: "/Sample/:ID", nomenu: true, parent: "userDashboard.salesGroups.detail", hasOverrideTitleInTemplate: true,
                            params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                            views: { content: { templateUrl: root + "/userDashboard/views/salesGroupDetail.sample.html" } }
                        })
                        .state("userDashboard.salesGroups.detail.order", <ng.ui.IState>{
                            resolve: { $title: () => "ui.storefront.userDashboard2.controls.sales.GroupDetail" },
                            url: "/Order/:ID", nomenu: true, parent: "userDashboard.salesGroups.detail", hasOverrideTitleInTemplate: true,
                            params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                            views: { content: { templateUrl: root + "/userDashboard/views/salesGroupDetail.order.html" } }
                        })
                        .state("userDashboard.salesGroups.detail.invoice", <ng.ui.IState>{
                            resolve: { $title: () => "ui.storefront.userDashboard2.controls.sales.GroupDetail" },
                            url: "/Invoice/:ID", nomenu: true, parent: "userDashboard.salesGroups.detail", hasOverrideTitleInTemplate: true,
                            params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                            views: { content: { templateUrl: root + "/userDashboard/views/salesGroupDetail.invoice.html" } }
                        })
                        .state("userDashboard.salesGroups.detail.return", <ng.ui.IState>{
                            resolve: { $title: () => "ui.storefront.userDashboard2.controls.sales.GroupDetail" },
                            url: "/Return/:ID", nomenu: true, parent: "userDashboard.salesGroups.detail", hasOverrideTitleInTemplate: true,
                            params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                            views: { content: { templateUrl: root + "/userDashboard/views/salesGroupDetail.return.html" } }
                        })
                        .state("userDashboard.salesGroups.detail.shipping", <ng.ui.IState>{
                            resolve: { $title: () => "ui.storefront.userDashboard2.controls.sales.GroupDetail" },
                            url: "/Shipping/:ID", nomenu: true, parent: "userDashboard.salesGroups.detail", hasOverrideTitleInTemplate: true,
                            params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                            views: { content: { templateUrl: root + "/userDashboard/views/salesGroupDetail.shipping.html" } }
                        })
                .state("userDashboard.salesOrders", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true, parent: "userDashboard",
                    icon: "far fa-receipt",
                    url: "/Sales-Orders",
                    views: { dashboard: { template: `<div class="row"><div ui-view="salesOrders" class="col-12"></div></div>` } }
                })
                    .state("userDashboard.salesOrders.list", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.Order.Plural" },
                        url: "/List", parent: "userDashboard.salesOrders",
                        views: { salesOrders: { templateUrl: root + "/userDashboard/views/salesOrders.html" } }
                    })
                    .state("userDashboard.salesOrders.detail", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.userDashboard2.userDashboard.SalesOrdersDetail" },
                        url: "/Detail/:ID", nomenu: true, parent: "userDashboard.salesOrders",
                        params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                        views: { salesOrders: { templateUrl: root + "/userDashboard/views/salesOrderDetail.html" } }
                    })
                .state("userDashboard.salesReturns", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true,
                    icon: "far fa-box-fragile",
                    url: "/Sales-Returns", parent: "userDashboard",
                    views: { dashboard: { template: `<div class="row"><div ui-view="salesReturns" class="col-12"></div></div>` } }
                })
                    .state("userDashboard.salesReturns.list", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.Return.Plural"},
                        url: "/List", parent: "userDashboard.salesReturns",
                        views: { salesReturns: { templateUrl: root + "/userDashboard/views/salesReturns.html" } }
                    })
                    .state("userDashboard.salesReturns.detail", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.menu.miniMenu.salesReturnDetail" },
                        url: "/Detail/:ID", nomenu: true, parent: "userDashboard.salesReturns",
                        views: { salesReturns: { templateUrl: root + "/userDashboard/views/salesReturnDetail.html" } }
                    })
                    .state("userDashboard.salesReturns.new", <ng.ui.IState>{ resolve: { },
                        url: "/New/:ID", nomenu: true, parent: "userDashboard.salesReturns",
                        views: { salesReturns: { templateUrl: root + "/userDashboard/views/salesReturnNew.html" } }
                    })
                    .state("userDashboard.salesReturns.print", <ng.ui.IState>{ resolve: { },
                        url: "/Print/:ID", nomenu: true, parent: "userDashboard.salesReturns", hasOverrideTitleInTemplate: true,
                        views: { salesReturns: { templateUrl: root + "/userDashboard/views/salesReturnPrint.html" } }
                    })
                .state("userDashboard.salesInvoices", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true,
                    icon: "far fa-file-invoice-dollar",
                    url: "/Sales-Invoices", parent: "userDashboard",
                    views: { dashboard: { template: `<div class="row"><div ui-view="salesInvoices" class="col-12"></div></div>` } }
                })
                    .state("userDashboard.salesInvoices.list", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.Invoice.Plural" },
                        url: "/List", parent: "userDashboard.salesInvoices",
                        views: { salesInvoices: { templateUrl: root + "/userDashboard/views/salesInvoices.html" } }
                    })
                    .state("userDashboard.salesInvoices.detail", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.Invoice" },
                        url: "/Detail/:ID", nomenu: true, parent: "userDashboard.salesInvoices", hasOverrideTitleInTemplate: true,
                        params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                        views: { salesInvoices: { templateUrl: root + "/userDashboard/views/salesInvoiceDetail.html" } }
                    })
                .state("userDashboard.salesQuotes", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true, parent: "userDashboard",
                    icon: "far fa-quote-right",
                    url: "/Sales-Quotes",
                    views: { dashboard: { template: `<div class="row"><div ui-view="salesQuotes" class="col-12"></div></div>` } }
                })
                    .state("userDashboard.salesQuotes.list", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.Quote.Plural" },
                        url: "/List", parent: "userDashboard.salesQuotes",
                        views: { salesQuotes: { templateUrl: root + "/userDashboard/views/salesQuotes.html" } }
                    })
                    .state("userDashboard.salesQuotes.detail", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.common.Quote.QuoteDetail" },
                        url: "/Detail/:ID", nomenu: true, parent: "userDashboard.salesQuotes",
                        params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                        views: { salesQuotes: { templateUrl: root + "/userDashboard/views/salesQuoteDetail.html" } }
                    })
                .state("userDashboard.sampleRequests", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true, parent: "userDashboard",
                    icon: "far fa-eye-dropper",
                    url: "/Sample-Requests",
                    views: { dashboard: { template: `<div class="row"><div ui-view="sampleRequests" class="col-12"></div></div>` } }
                })
                    .state("userDashboard.sampleRequests.list", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.userDashboard2.controls.adminSiteMenu2.SampleRequest.Plural" },
                        url: "/List", parent: "userDashboard.sampleRequests",
                        views: { sampleRequests: { templateUrl: root + "/userDashboard/views/sampleRequests.html" } }
                    })
                    .state("userDashboard.sampleRequests.detail", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.userDashboard2.controls.adminSiteMenu2.SampleRequestDetail" },
                        url: "/Detail/:ID", nomenu: true, parent: "userDashboard.sampleRequests", hasOverrideTitleInTemplate: true,
                        params: <ng.ui.RawParams>{ ID: <ng.ui.ParamDeclaration>{ type: "int", squash: false } },
                        views: { sampleRequests: { templateUrl: root + "/userDashboard/views/sampleRequestDetail.html" } }
                    })
                .state("userDashboard.downloads", <ng.ui.IState>{
                    resolve: { $title: () => "ui.storefront.menu.miniMenu.Downloads" },
                    icon: "far fa-arrow-circle-down",
                    url: "/Downloads", parent: "userDashboard",
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/downloads.html" } }
                })
                .state("userDashboard.wishList", <ng.ui.IState>{
                    resolve: { $title: () => "ui.storefront.common.WishList" },
                    icon: "far fa-heart",
                    url: "/Wish-List", parent: "userDashboard",
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/wishList.html" } }
                })
                .state("userDashboard.notifyMeList", <ng.ui.IState>{
                    resolve: { $title: () => "ui.storefront.menu.miniMenu.inStockAlerts" },
                    icon: "far fa-bell-on",
                    url: "/Notify-Me-List", parent: "userDashboard",
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/notifyMeList.html" } }
                })
                .state("userDashboard.favorites", <ng.ui.IState>{
                    resolve: { $title: () => "ui.storefront.storeDashboard.storeFavorites.Favorites" },
                    icon: "far fa-star",
                    url: "/Favorites", parent: "userDashboard",
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/favoritesList.html" } }
                })
                .state("userDashboard.shoppingLists", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true,
                    icon: "far fa-clipboard-list-check",
                    url: "/Shopping-Lists", parent: "userDashboard",
                    views: { dashboard: { template: `<div class="row"><div ui-view="shoppingLists" class="col-12"></div></div>` } }
                })
                    .state("userDashboard.shoppingLists.list", <ng.ui.IState>{
                        resolve: {  $title: () => "ui.storefront.userDashboard2.userDashboard.ShoppingLists" },
                        url: "/List", parent: "userDashboard.shoppingLists",
                        views: { shoppingLists: { templateUrl: root + "/userDashboard/views/shoppingLists.html" } }
                    })
                    .state("userDashboard.shoppingLists.detail", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.menu.miniMenu.MyShoppingList" },
                        url: "/Detail/:Name", parent: "userDashboard.shoppingLists", hasOverrideTitleInTemplate: true,
                        views: { shoppingLists: { templateUrl: root + "/userDashboard/views/shoppingListDetail.html" } }
                    })
                .state("userDashboard.orderRequests", <ng.ui.IState>{ resolve: { },
                    abstract: true, nomenu: true,
                    icon: "far fa-tag",
                    url: "/Order-Requests", parent: "userDashboard",
                    views: { dashboard: { template: `<div class="row"><div ui-view="orderRequests" class="col-12"></div></div>` } }
                })
                    .state("userDashboard.orderRequests.list", <ng.ui.IState>{
                        resolve: {  $title: () => "ui.storefront.userDashboard2.userDashboard.OrderRequests" },
                        url: "/List", parent: "userDashboard.orderRequests",
                        views: { orderRequests: { templateUrl: root + "/userDashboard/views/orderRequests.html" } }
                    })
                    .state("userDashboard.orderRequests.detail", <ng.ui.IState>{
                        resolve: { $title: () => "ui.storefront.menu.miniMenu.MyOrderRequest" },
                        url: "/Detail/:Name", parent: "userDashboard.orderRequests", hasOverrideTitleInTemplate: true,
                        views: { orderRequests: { templateUrl: root + "/userDashboard/views/orderRequestDetail.html" } }
                    })
                .state("userDashboard.chatHub", <ng.ui.IState>{
                    resolve: { $title: () => "ui.storefront.storeDashboard.storeMenuSidebar.MessageHub" },
                    url: "/Message-Hub", parent: "userDashboard",
                    views: { dashboard: { templateUrl: root + "/userDashboard/views/chatHub.html" } }
                })
                .state("userDashboard.inbox", <ng.ui.IState>{ resolve: { },
                    icon: "far fa-inbox",
                    url: "/Inbox", parent: "userDashboard",
                    views: { dashboard: { template: `<cef-messaging></cef-messaging>` } }
                })
                    .state("userDashboard.inbox.folder", <ng.ui.IState>{ resolve: { },
                        url: "/Folder/:folder", parent: "userDashboard.inbox",
                        title: "Inbox",
                        params: { box: { } },
                        template: "<cef-message-box></cef-message-box>"
                    })
                    .state("userDashboard.inbox.read", <ng.ui.IState>{ resolve: { },
                        url: "/Read/:id", parent: "userDashboard.inbox",
                        params: <ng.ui.RawParams>{ id: <ng.ui.ParamDeclaration>{ type: "int", squash: false },  msg: { value: null } },
                        template: "<cef-message></cef-message>"
                    })
                    .state("userDashboard.inbox.reply", <ng.ui.IState>{
                        url: "/Reply/:id", parent: "userDashboard.inbox",
                        params: <ng.ui.RawParams>{ id: <ng.ui.ParamDeclaration>{ type: "int", squash: false },  msg: { value: null } },
                        template: '<cef-message mode="reply"></cef-message>',
                        resolve: { msg: $stateParams => $stateParams.msg.reply() }
                    })
                    .state("userDashboard.inbox.forward", <ng.ui.IState>{
                        url: "/Forward/:id", parent: "userDashboard.inbox",
                        params: <ng.ui.RawParams>{ id: <ng.ui.ParamDeclaration>{ type: "int", squash: false },  msg: { value: null } },
                        template: '<cef-message mode="forward"></cef-message>',
                        resolve: { msg: $stateParams => $stateParams.msg.forward() }
                    })
                    .state("userDashboard.inbox.compose", <ng.ui.IState>{ resolve: { },
                        url: "/Compose", parent: "userDashboard.inbox",
                        params: { msg: { value: null } },
                        template: '<cef-message mode="compose"></cef-message>'
                    })
                    .state("userDashboard.inbox.delete", <ng.ui.IState>{ resolve: { },
                        url: "/Delete/:id", parent: "userDashboard.inbox",
                        params: <ng.ui.RawParams>{ id: <ng.ui.ParamDeclaration>{ type: "int", squash: false },  msg: { value: null } },
                        onEnter: ($uibModal: ng.ui.bootstrap.IModalService, $stateParams: ng.ui.IStateParamsService, $state: ng.ui.IStateService) => {
                            if (!($stateParams as any).id || !($stateParams as any).msg) {
                                $state.go("userDashboard.inbox.folder", { folder: "inbox" });
                                return;
                            }
                            $uibModal.open({
                                templateUrl: /*$filter("corsLink")(*/"/framework/store/messaging/messaging-delete-modal.html"/*, "ui")*/,
                                controller: ($scope, $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, $stateParams: ng.ui.IStateParamsService) => {
                                    $scope.stateParams = $stateParams;
                                    $scope.confirm = () => { $uibModalInstance.close(true); };
                                    $scope.closeThisDialog = () => { $uibModalInstance.dismiss(false); }
                                }
                            }).result.then(result => {
                                if (result) {
                                    ($stateParams as any).msg.deactivate().then(() => {
                                        $state.go("userDashboard.inbox.deleted");
                                    });
                                }
                            }).finally(() => $state.go("userDashboard.inbox.folder", { folder: "inbox" }));
                        }
                    })
                    .state("userDashboard.inbox.deleted", <ng.ui.IState>{ resolve: { },
                        parent: "userDashboard.inbox",
                        template: '<cef-message mode="deleted"></cef-message>'
                    })
                    .state("userDashboard.inbox.modalCompose", <ng.ui.IState>{ resolve: { },
                        url: "/Contact",
                        /* params: { msg: { value: null }, contacts: [], subject: "" }, */
                        params: <ng.ui.RawParams>{ // Use IModalComposeStateParams when you need type safety
                            subject: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
                            msg: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
                            contacts: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, array: true, isOptional: true },
                        },
                        onEnter: (
                            $uibModal: ng.ui.bootstrap.IModalService,
                            $stateParams: IModalComposeStateParams,
                            cvLoginModalFactory: user.ILoginModalFactory) => {
                            // Use the login modal service to open the modal, will auto-resolve if already logged in
                            cvLoginModalFactory(() => null, null, true, false).then(() => {
                                $uibModal.open({
                                    templateUrl: root + "/messaging/messaging-contact-modal.html",
                                    backdrop: "static",
                                    resolve: {
                                        subject: $stateParams.subject,
                                        msg: $stateParams.msg,
                                        contacts: $stateParams.contacts
                                    },
                                    controller: "cefContactModal",
                                    controllerAs: "modalComposeCtrl"
                                }).result.then(() => {
                                }).finally(() => {
                                });
                            });
                        }
                    });
    }

    function generateStatesForCatalogs($stateProvider: ng.ui.IStateProvider, root: string, cefConfig: core.CefConfig): void {
        // === Begin Abstract Categories ===
        $stateProvider
            .state("categories", <ng.ui.IState>{ resolve: { },
                abstract:true, nomenu: true,
                url: "/cat", title: "Categories",
                views: { main: { template: "<cef-categories-view></cef-categories-view>" } }
            })
                .state("categories.level", <ng.ui.IState>{ resolve: { },
                    title: "Categories Inner", nomenu: true,
                    url: "/:category", parent: "categories",
                    params: <ng.ui.RawParams>{
                        category: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true }
                    },
                    views: { main: { template: "<cef-categories-view></cef-categories-view>" } }
                });
        function genBaseCatalogParams(params: ng.ui.RawParams = { }): ng.ui.RawParams {
            // Use ng.ui.IStateParamsServiceForSearchCatalog for type safety when referencing. Not using here as this is the default values format
            return angular.merge(params, <ng.ui.RawParams>{
                format: <ng.ui.ParamDeclaration>{ type: "string", value: cefConfig && cefConfig.catalog.defaultFormat, squash: false, isOptional: false },
                page: <ng.ui.ParamDeclaration>{ type: "int", value: 1, squash: false, isOptional: false },
                size: <ng.ui.ParamDeclaration>{ type: "int", value: cefConfig && cefConfig.catalog.defaultPageSize, squash: false, isOptional: false },
                sort: <ng.ui.ParamDeclaration>{ type: "string", value: cefConfig && cefConfig.catalog.defaultSort, squash: false, isOptional: false },
                term: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
            });
        }
        function appendAttributesToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                attributesAny: <ng.ui.ParamDeclaration>{ type: "json", value: null, squash: true, array: true, isOptional: true },
                attributesAll: <ng.ui.ParamDeclaration>{ type: "json", value: null, squash: true, array: false, isOptional: true },
            });
        }
        function appendCategoryToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                category: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
                categoriesAny: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, array: true, isOptional: true },
                categoriesAll: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, array: true, isOptional: true },
            });
        }
        function appendCityToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                city: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
            });
        }
        function appendDistrictToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                districtId: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
                districtIdsAny: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, array: true, isOptional: true },
                districtIdsAll: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, array: true, isOptional: true },
            });
        }
        function appendManufacturerToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                manufacturerId: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, isOptional: true },
                manufacturerIdsAny: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, array: true, isOptional: true },
                manufacturerIdsAll: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, array: true, isOptional: true },
            });
        }
        function appendNameToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                name: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
            });
        }
        function appendPricingToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                pricingRanges: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, array: true, isOptional: true },
            });
        }
        function appendProductToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                productId: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, isOptional: true },
                productIdsAny: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, array: true, isOptional: true },
                productIdsAll: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, array: true, isOptional: true },
            });
        }
        function appendRatingsToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                ratingRanges: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, array: true, isOptional: true },
            });
        }
        function appendRegionToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                regionId: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
                regionIdsAny: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, array: true, isOptional: true },
            });
        }
        function appendStoreToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                storeId: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, isOptional: true },
                storeIdsAny: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, array: true, isOptional: true },
                storeIdsAll: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, array: true, isOptional: true },
            });
        }
        function appendTypeToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                typeId: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, isOptional: true },
                typeIdsAny: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, array: true, isOptional: true },
            });
        }
        function appendVendorToParams(params: ng.ui.RawParams): ng.ui.RawParams {
            return angular.merge(params, {
                vendorId: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, isOptional: true },
                vendorIdsAny: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, array: true, isOptional: true },
                vendorIdsAll: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, array: true, isOptional: true },
            });
        }
        function genView(name: string): { [name: string]: ng.ui.IState; } {
            const retVal = {
                searchCatalogResultsView: {
                    templateProvider: ($templateCache: ng.ITemplateCacheService, $q: ng.IQService, $http: ng.IHttpService) => $q((resolve, reject) => {
                        var path = root + "/searchCatalog/views/results/" + name + "/both.html";
                        var cached = $templateCache.get(path);
                        if (cached) {
                            resolve(cached);
                            return;
                        }
                        $http.get(path).then(r => resolve(r.data)).catch(reject);
                    })
                }
            };
            return retVal;
        }
        const qBase = "/Format/:format/Page/:page/Size/:size/Sort/:sort?term";
        const qAt = "&attributesAny&attributesAll";
        const qCa = "&category&categoriesAny&categoriesAll";
        const qCi = "&city";
        const qDi = "&districtId&districtIdsAny&districtIdsAll";
        const qMa = "&manufacturerId&manufacturerIdsAny&manufacturerIdsAll";
        const qNa = "&name";
        const qPi = "&pricingRanges";
        const qPr = "&productId&productIdsAny&productIdsAll";
        const qRa = "&ratingRanges";
        const qRe = "&regionId&regionIdsAny";
        const qSt = "&storeId&storeIdsAny&storeIdsAll";
        const qTy = "&typeId&typeIdsAny";
        const qVe = "&vendorId&vendorIdsAny&vendorIdsAll";
        function genResultsBaseState(name: string): ng.ui.IState {
            const views = { };
            views[name] = { templateUrl: root + "/searchCatalog/views/results-" + name + ".html" }
            return <ng.ui.IState>{
                resolve: { },
                abstract: true,
                nomenu: true,
                url: "/Results",
                parent: "searchCatalog." + name,
                views: views
            };
        }
        function genCompareState(name: string): ng.ui.IState {
            const views = { };
            views[name] = { templateUrl: root + "/searchCatalog/views/compare-" + name + ".html" }
            return  <ng.ui.IState>{
                resolve: { },
                url: "/Compare",
                title: "Compare " + name,
                nomenu: true,
                parent: "searchCatalog." + name,
                views: views
            };
        }
        function genKindBaseState(name: string, urlChar: string): ng.ui.IState {
            return <ng.ui.IState>{
                resolve: { },
                abstract: true,
                nomenu: true,
                url: "/" + urlChar,
                title: name + "Catalog",
                parent: "searchCatalog",
                views: { searchCatalog: { template: `<div ui-view="${name}"></div>` } }
            };
        }
        function genCatViews(name: string): ng.ui.IStateProvider {
            const views1 = { };
            views1[name] = { templateUrl: root + "/searchCatalog/views/categories.html" };
            const views2 = { };
            views2["searchCatalog" + name + "Categories"] = { templateUrl: root + "/searchCatalog/views/categories/level1-" + name + ".html" };
            const views3 = { };
            views3["searchCatalog" + name + "Categories"] = { templateUrl: root + "/searchCatalog/views/categories/level2-" + name + ".html" };
            const views4 = { };
            views4["searchCatalog" + name + "Categories"] = { templateUrl: root + "/searchCatalog/views/categories/level3-" + name + ".html" };
            return $stateProvider
                .state("searchCatalog." + name + ".categories", <ng.ui.IState>{
                    resolve: { },
                    abstract: true, nomenu: true,
                    url: "/Categories", parent: "searchCatalog." + name,
                    views: views1
                })
                .state("searchCatalog." + name + ".categories.level1", <ng.ui.IState>{
                    resolve: { },
                    title: "Categories (Top Level)",
                    nomenu: true,
                    url: "/Level-1",
                    parent: "searchCatalog." + name + ".categories",
                    views: views2
                })
                .state("searchCatalog." + name + ".categories.level2", <ng.ui.IState>{
                    resolve: { },
                    title: "Sub-Categories",
                    nomenu: true,
                    url: "/Level-2/:category",
                    parent: "searchCatalog." + name + ".categories",
                    params: <ng.ui.RawParams>{
                        category: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: false, isOptional: false }
                    },
                    views: views3
                })
                .state("searchCatalog." + name + ".categories.level3", <ng.ui.IState>{
                    resolve: { },
                    title: "Sub-Sub-Categories",
                    nomenu: true,
                    url: "/Level-3/:parentCategory/:category",
                    parent: "searchCatalog." + name + ".categories",
                    params: <ng.ui.RawParams>{
                        parentCategory: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: false, isOptional: false },
                        category: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: false, isOptional: false }
                    },
                    views: views4
                });
        }
        // ==== End Abstract Categories ====
        $stateProvider
        // ==== Begin Search Catalog ====
            .state("searchCatalog", <ng.ui.IState>{ resolve: { },
                abstract: true, nomenu: true,
                url: "/c", title: "Search Catalog",
                views: { main: { template: "<cef-search-catalog></cef-search-catalog>" } }
            })
                .state("searchCatalog.auctions", genKindBaseState("auctions", "a"))
                .state("searchCatalog.auctions.results", genResultsBaseState("auctions"))
                .state("searchCatalog.auctions.results.both", <ng.ui.IState>{
                    resolve: { },
                    title: "Results", nomenu: true, parent: "searchCatalog.auctions.results",
                    url: qBase + qAt + qCa + qMa + qPi + qPr + qRa + qSt + qVe,
                    params: appendAttributesToParams(
                        appendCategoryToParams(
                        appendManufacturerToParams(
                        appendPricingToParams(
                        appendProductToParams(
                        appendRatingsToParams(
                        appendStoreToParams(
                        appendVendorToParams(
                        genBaseCatalogParams()
                        )))))))),
                    views: genView("auctions")
                })
                .state("searchCatalog.auctions.compare", genCompareState("auctions"))
                .state("searchCatalog.categories", genKindBaseState("categories", "c"))
                .state("searchCatalog.categories.results", genResultsBaseState("categories"))
                .state("searchCatalog.categories.results.both", <ng.ui.IState>{
                    resolve: { },
                    title: "Results", nomenu: true, parent: "searchCatalog.categories.results",
                    url: qBase + qAt + qCa + qMa + qPi + qPr + qRa + qSt + qVe,
                    params: appendAttributesToParams(
                        appendCategoryToParams(
                        appendManufacturerToParams(
                        appendPricingToParams(
                        appendProductToParams(
                        appendRatingsToParams(
                        appendStoreToParams(
                        appendVendorToParams(
                        genBaseCatalogParams()
                        )))))))),
                    views: genView("categories")
                })
                .state("searchCatalog.categories.compare", genCompareState("categories"))
                .state("searchCatalog.listings", genKindBaseState("listings", "l"))
                .state("searchCatalog.listings.results", genResultsBaseState("listings"))
                .state("searchCatalog.listings.results.both", <ng.ui.IState>{
                    resolve: { },
                    title: "Results", nomenu: true, parent: "searchCatalog.listings.results",
                    url: qBase + qAt + qCa + qMa + qPi + qPr + qRa + qSt + qVe,
                    params: appendAttributesToParams(
                        appendCategoryToParams(
                        appendManufacturerToParams(
                        appendPricingToParams(
                        appendProductToParams(
                        appendRatingsToParams(
                        appendStoreToParams(
                        appendVendorToParams(
                        genBaseCatalogParams()
                        )))))))),
                    views: genView("listings")
                })
                .state("searchCatalog.listings.compare", genCompareState("listings"))
                .state("searchCatalog.lots", genKindBaseState("lots", "o"))
                .state("searchCatalog.lots.results", genResultsBaseState("lots"))
                .state("searchCatalog.lots.results.both", <ng.ui.IState>{
                    resolve: { },
                    title: "Results", nomenu: true, parent: "searchCatalog.lots.results",
                    url: qBase + qAt + qCa + qMa + qPi + qPr + qRa + qSt + qVe,
                    params: appendAttributesToParams(
                        appendCategoryToParams(
                        appendManufacturerToParams(
                        appendPricingToParams(
                        appendProductToParams(
                        appendRatingsToParams(
                        appendStoreToParams(
                        appendVendorToParams(
                        genBaseCatalogParams()
                        )))))))),
                    views: genView("lots")
                })
                .state("searchCatalog.lots.compare", genCompareState("lots"))
                .state("searchCatalog.manufacturers", genKindBaseState("manufacturers", "m"))
                .state("searchCatalog.manufacturers.results", genResultsBaseState("manufacturers"))
                .state("searchCatalog.manufacturers.results.both", <ng.ui.IState>{
                    resolve: { },
                    title: "Results", nomenu: true, parent: "searchCatalog.manufacturers.results",
                    url: qBase + qAt + qCa + qPi + qPr + qRa + qSt + qVe,
                    params: appendAttributesToParams(
                        appendCategoryToParams(
                        //appendManufacturerToParams(
                        appendPricingToParams(
                        appendProductToParams(
                        appendRatingsToParams(
                        appendStoreToParams(
                        appendVendorToParams(
                        genBaseCatalogParams()
                        )))))/*)*/)),
                    views: genView("manufacturers")
                })
                .state("searchCatalog.manufacturers.compare", genCompareState("manufacturers"))
                .state("searchCatalog.products", genKindBaseState("products", "p"))
                .state("searchCatalog.products.results", genResultsBaseState("products"))
                .state("searchCatalog.products.results.both", <ng.ui.IState>{
                    resolve: { },
                    title: "Results", nomenu: true, parent: "searchCatalog.products.results",
                    url: qBase + qAt + "&brandName" + qCa + "&filterByCurrentAccountRoles" + "&onHand" + qMa + qPi + qRa + qSt + qTy + qVe,
                    params: appendAttributesToParams(
                        appendCategoryToParams(
                        appendManufacturerToParams(
                        appendPricingToParams(
                        //appendProductToParams(
                        appendRatingsToParams(
                        appendStoreToParams(
                        appendTypeToParams(
                        appendVendorToParams(
                        genBaseCatalogParams(<ng.ui.RawParams>{
                            brandName: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
                            filterByCurrentAccountRoles: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
                            onHand: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: true, isOptional: true },
                        }))))/*)*/))))),
                    views: genView("products")
                })
                .state("searchCatalog.products.compare", genCompareState("products"))
                .state("searchCatalog.stores", genKindBaseState("stores", "s"))
                .state("searchCatalog.stores.results", genResultsBaseState("stores"))
                .state("searchCatalog.stores.results.both", <ng.ui.IState>{
                    resolve: { },
                    title: "Results", nomenu: true, parent: "searchCatalog.stores.results",
                    url: qBase + qAt + qCa + qCi + qDi + qMa + qNa + qPi + qPr + qRa + qRe + qTy + qVe,
                    params: appendAttributesToParams(
                        appendCategoryToParams(
                        appendCityToParams(
                        appendDistrictToParams(
                        appendManufacturerToParams(
                        appendNameToParams(
                        appendPricingToParams(
                        appendProductToParams(
                        appendRatingsToParams(
                        appendRegionToParams(
                        appendTypeToParams(
                        appendVendorToParams(
                        genBaseCatalogParams()
                        )))))))))))),
                    views: genView("stores")
                })
                .state("searchCatalog.stores.compare", genCompareState("stores"))
                .state("searchCatalog.vendors", genKindBaseState("vendors", "v"))
                .state("searchCatalog.vendors.results", genResultsBaseState("vendors"))
                .state("searchCatalog.vendors.results.both", <ng.ui.IState>{
                    resolve: { },
                    title: "Results", nomenu: true, parent: "searchCatalog.vendors.results",
                    url: qBase + qAt + qCa + qMa + qPi + qPr + qRa + qSt,
                    params: appendAttributesToParams(
                        appendCategoryToParams(
                        appendManufacturerToParams(
                        appendPricingToParams(
                        appendProductToParams(
                        appendRatingsToParams(
                        appendStoreToParams(
                        /*appendVendorToParams(*/
                        genBaseCatalogParams()
                        /*)*/))))))),
                    views: genView("vendors")
                })
                .state("searchCatalog.vendors.compare", genCompareState("vendors"))
            genCatViews("auctions");
            genCatViews("listings");
            genCatViews("lots");
            genCatViews("products");
            genCatViews("stores");
            // ==== End Search Catalog ====
        ;
    }
}

// These are added to satisfy interface requirements in build, they are not actually used by the storefront app

module cef.storeAdmin.services {
    export interface ICurrentStoreAdminService { }
}

/* Testing CLS performance
let cls = 0;
new PerformanceObserver(entryList => {
    for (const entry of entryList.getEntries()) {
        if (!entry["hadRecentInput"]) {
            cls += entry["value"];
            console.log("Current CLS value:", cls, entry);
        }
    }
}).observe({ type: "layout-shift", buffered: true });
*/
