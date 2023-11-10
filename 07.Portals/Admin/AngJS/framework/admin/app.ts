module cef.admin {
    export var adminApp = angular.module("cef.admin", [
        "cefConfig",
        "angular-loading-bar", // == cfpLoadingBar
        ////"angularMoment",
        "angularPayments",
        "cefAdminTemplates",
        "credit-cards",
        ////"door3.css",
        "kendo.directives",
        ////"localytics.directives",
        "mega-menu",
        ////"ngAnimate",
        "ngCookies",
        ////"ngDialog",
        "ngMessages",
        ////"ngSanitize",
        "angular-bind-html-compile",
        "pascalprecht.translate",
        "ui.bootstrap",
        "ui.router.title",
        "ui.router",
        "ui.toggle",
        "usersnapLogging"
    ]);

    // Providers
    adminApp.constant("cvServiceStrings", services.serviceStrings);
    adminApp.provider("cvApi", api.CEFAPIProvider);

    // Services
    adminApp.service("errorInterceptor", core.ErrorInterceptor);

    adminApp.factory("$translateCookieStorageCustom", core.translations.translateCookieStorageCustomFactoryFn);
    adminApp.factory("subdomain", core.subdomainFactoryFn);
    adminApp.factory("CacheFactoryService", core.cacheFactoryServiceFactoryFn);

    adminApp.service("cvAuthenticationService", services.AuthenticationService);
    adminApp.service("cvAddressBookService", services.AddressBookService);
    adminApp.service("cvCartService", services.CartService);
    adminApp.service("cvCountryService", services.CountryService);
    adminApp.service("cvCurrencyService", services.CurrencyService);
    adminApp.service("cvFormValidationService", services.FormValidationService);
    adminApp.service("cvInventoryService", services.InventoryService);
    adminApp.service("cvLanguageService", services.LanguageService);
    adminApp.service("cvPurchaseService", services.PurchaseService);
    adminApp.service("cvRegionService", services.RegionService);
    adminApp.service("cvRestrictedRegionCheckService", services.RestrictedRegionCheckService);
    adminApp.service("cvSecurityService", services.SecurityService);
    adminApp.service("cvStatesService", services.StatesService);
    adminApp.service("cvStatusesService", services.StatusesService);
    adminApp.service("cvTypesService", services.TypesService);
    adminApp.service("cvWalletService", services.WalletService);
    adminApp.service("cvViewStateService", services.ViewStateService);

    adminApp.factory("cvContactFactory", factories.cvContactFactoryFn);

    // Filters
    adminApp.filter("globalizedCurrency", core.globalizedCurrencyFilterFn);
    adminApp.filter("decamelize", core.decamelizeFilterFn);
    adminApp.filter("camelCaseToHuman", core.camelCaseToHumanFilterFn);
    adminApp.filter("checkIfNotEmpty", core.checkIfNotEmptyFilterFn);
    adminApp.filter("encode", () => window.encodeURIComponent);
    adminApp.filter("dec2hex", core.dec2hexFilterFn);
    adminApp.filter("hex2char", core.hex2charFilterFn);
    adminApp.filter("modulo", core.moduloFilterFn);
    adminApp.filter("max", core.maxFilterFn);
    adminApp.filter("min", core.minFilterFn);
    adminApp.filter("boolNorm", core.boolNormFilterFn);
    adminApp.filter("nthElement", core.nthElementFilterFn);
    adminApp.filter("numberToTime", core.numberToTimeFilterFn);
    adminApp.filter("objectKeysLimitTo", core.objectKeysLimitToFilterFn);
    adminApp.filter("objectKeysFilter", core.objectKeysFilterFilterFn);
    adminApp.filter("toArray", core.toArrayFilterFn);
    adminApp.filter("tel", core.telFilterFn);
    adminApp.filter("trustedHtml", core.trustedHtmlFilterFn);
    adminApp.filter("zeroPadNumber", core.zeroPadNumberFilterFn);
    adminApp.filter("convertJSONDate", core.convertJsonDateFilterFn);
    adminApp.filter("limitToEllipses", core.limitToEllipsesFilterFn);
    adminApp.filter("modifiedValue", core.modifiedValueFilterFn);
    adminApp.filter("groupBy", core.groupByFilterFn);
    adminApp.filter("flatGroupBy", core.flatGroupByFilterFn);
    adminApp.filter("sumBy", core.sumByFilterFn);
    adminApp.filter("statusIDToText", core.statusIDToTextFilterFn);
    adminApp.filter("typeIDToText", core.typeIDToTextFilterFn);
    adminApp.filter("stateIDToText", core.stateIDToTextFilterFn);
    adminApp.filter("splitShippingGroupTitle", core.splitShippingGroupTitleFilterFn);

    adminApp.filter("acToDropdownItem", (cefConfig: core.CefConfig) => (accountContact: api.AccountContactModel): string => {
        const noContact = angular.isUndefined(accountContact.Slave);
        const noSlave = angular.isUndefined(accountContact.Slave);
        if (noContact && noSlave) {
            throw new Error("acToDropdownItem No contact data, invalid setup");
        }
        const useSlave = noContact;
        const hasName = (useSlave
            ? (accountContact.Slave.Address.CustomKey || accountContact.Slave.CustomKey)
            : (accountContact.Slave.Address.CustomKey || accountContact.Slave.CustomKey))
            || accountContact.CustomKey;
        return (hasName && !cefConfig.personalDetailsDisplay.hideAddressBookKeys ? (hasName.toLocaleUpperCase() + ': ') : '') +
            (useSlave ? accountContact.Slave.Address.Street1 : accountContact.Slave.Address.Street1)/* +
            (accountContact.IsBilling
                ? ' (Billing)'
                : (accountContact.IsPrimary
                    ? ' (Default Shipping)'
                    : ' (Shipping)'))*/;
    });
    adminApp.filter("cToDropdownItem", (cefConfig: core.CefConfig) => (contact: api.ContactModel): string => {
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
        return (hasName && !cefConfig.personalDetailsDisplay.hideAddressBookKeys ? (hasName.toLocaleUpperCase() + ': ') : '') +
            contact.Address.Street1/* +
            (accountContact.IsBilling
                ? ' (Billing)'
                : (accountContact.IsPrimary
                    ? ' (Default Shipping)'
                    : ' (Shipping)'))*/;
    });

    // CORS
    adminApp.filter("corsLinkRootInner", core.corsLinkRootInnerFn);
    adminApp.filter("corsLinkRoot", core.corsLinkRootInnerFn);
    adminApp.filter("corsLink", core.corsLinkFn);
    adminApp.filter("corsProductLink", core.corsProductLinkFn);
    adminApp.filter("corsStoreLink", core.corsStoreLinkFn);
    adminApp.filter("corsImageLink", core.corsImageLinkFn);
    adminApp.filter("corsStoredFilesLink", core.corsStoredFilesLinkFn);
    adminApp.filter("corsImportsLink", core.corsImportsLinkFn);
    adminApp.filter("goToCORSLink", core.goToCORSLinkFn);

    // Directives
    adminApp.directive("cvGrid", core.cvGridDirectiveFn);
    adminApp.directive("cefCompiler", core.cefCompilerFn);
    adminApp.directive("uiSrefIf", core.uiRouterPlugins.uiSrefIfFn);
    adminApp.directive("uiSrefPlus", core.uiRouterPlugins.uiSrefPlusDirectiveFn);
    adminApp.directive("ngEnter", core.ngEnterDirectiveFn);
    adminApp.directive("convertToNumber", core.convertToNumberDirectiveFn);
    adminApp.directive("cvWidget", core.cvWidgetDirectiveFn);
    adminApp.directive("widgetMenuItem", core.cvWidgetMenuItemDirectiveFn);
    adminApp.directive("cvPage", core.cvPageDirectiveFn);

    // Configs
    adminApp.run(() => {
        // Make sure there's a base tag for proper router operation.
        var tagExists = $("head base");
        if (tagExists.length) { tagExists.remove(); }
        $("head").prepend($(`<base href="/" />`));
    });

    function $HttpParamSerializerNoQuotesProvider(): ng.IServiceProvider {
        function forEachSorted(obj, iterator, context = undefined) {
            var keys = Object.keys(obj).sort();
            for (var i = 0; i < keys.length; i++) {
                iterator.call(context, obj[keys[i]], keys[i]);
            }
            return keys;
        }
        function encodeUriQuery(val, pctEncodeSpaces = false) {
            return encodeURIComponent(val)
                .replace(/%40/gi, "@")
                .replace(/%3A/gi, ":")
                .replace(/%24/g, "$")
                .replace(/%2C/gi, ",")
                .replace(/%3B/gi, ";")
                .replace(/%22/gi, "") // Double-quotes to nothing
                .replace(/%20/g, (pctEncodeSpaces ? "%20" : "+"));
        }
        function serializeValue(v) {
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
            return function ngParamSerializer(params) {
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
                return parts.join("&");
            };
        };
        return this;
    }
    adminApp.provider("$httpParamSerializerNoQuotes", $HttpParamSerializerNoQuotesProvider)

    adminApp.config((
            $locationProvider: ng.ILocationProvider,
            $httpProvider: ng.IHttpProvider,
            $sceDelegateProvider: ng.ISCEDelegateProvider,
            cfpLoadingBarProvider,
            cefConfig: core.CefConfig) => {
        $locationProvider.html5Mode(cefConfig.html5Mode as boolean);
        $locationProvider.hashPrefix("!");
        $httpProvider.interceptors.push("errorInterceptor");
        // Added for CORS start
        $httpProvider.defaults.withCredentials = true;
        $httpProvider.defaults.headers.common["Content-Type"] = "application/json";
        $httpProvider.defaults.headers.common["Vary"] = "Origin";
        delete $httpProvider.defaults.headers.common["X-Requested-With"];
        $httpProvider.defaults.paramSerializer = "$httpParamSerializerNoQuotes";
        // Allow loading from our assets domain(s). Notice the difference between * and **. More can be added to the config.
        var arr = cefConfig.corsResourceWhiteList;
        // Allow loading from same domain
        arr.push("self");
        $sceDelegateProvider.resourceUrlWhitelist(arr);
        // Added for CORS end
        cfpLoadingBarProvider.includeSpinner = false;
    });
    /** This hack helps get around typescript interfering with DI */
    adminApp.run($injector => (window as any).$injector = $injector);

    adminApp.run(($http: ng.IHttpService, $filter: ng.IFilterService, $rootScope: ng.IRootScopeService, cefConfig: core.CefConfig): void => {
        if (!cefConfig.featureSet.stores.enabled) {
            return;
        }
        $http<api.CEFActionResponseT<api.BrandModel>>({
            url: $filter("corsLink")("/Brands/Brand/Current", "api", "primary"),
            method: "GET",
        }).then(r => {
            if (!r || !r.data || !r.data.Result
                || !r.data.Result.BrandSiteDomains
                || r.data.Result.BrandSiteDomains.length <= 0) {
                // We failed to get data
                return;
            }
            $rootScope["globalBrandSiteDomainID"] = r.data.Result.BrandSiteDomains[0].SlaveID;
            $http<api.SiteDomainModel>({
                url: $filter("corsLink")("/Stores/SiteDomain/ID/" + $rootScope["globalBrandSiteDomainID"], "api", "primary"),
                method: "GET",
            }).then(r => {
                if (!r || !r.data) {
                    // We failed to get data
                    return;
                }
                $rootScope["globalBrandSiteDomain"] = r.data;
            });
        });
    });

    // Translations
    adminApp.run(($rootScope: ng.IRootScopeService, $translateCookieStorageCustom, subdomain: string, cefConfig: core.CefConfig) => {
        $rootScope["cefConfig"] = cefConfig;
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
    adminApp.config(($translateProvider: ng.translate.ITranslateProvider, cefConfig: core.CefConfig) => {
        $translateProvider.useSanitizeValueStrategy(null);
        $translateProvider.preferredLanguage(cefConfig.featureSet.languages.default);
        $translateProvider.fallbackLanguage(cefConfig.featureSet.languages.default);
        $translateProvider.useLoader("$translateAsyncPartialLoaderAdmin", {
            KeyStartsWith: "ui.admin.",
            urlTemplate: "lang={lang}&part={part}"
        });
        $translateProvider.useStorage("$translateCookieStorageCustom");
    });
    adminApp.run(($translate: ng.translate.ITranslateService,
            $translateAsyncPartialLoaderAdmin: ng.translate.ITranslatePartialLoaderService,
            cefConfig: core.CefConfig) => {
        function addCurrentLanguagePart() {
            const lang = $translate.use() || cefConfig.featureSet.languages.default;
            $translateAsyncPartialLoaderAdmin.addPart(lang, 1, "lang={lang}&part={part}");
            $translate.refresh(lang);
        }
        if ($translate.isReady()) {
            addCurrentLanguagePart();
            return;
        }
        $translate.onReady().then(() => addCurrentLanguagePart());
    });

    // UI Router States
    adminApp.config((
            $urlMatcherFactoryProvider,
            $stateProvider: ng.ui.IStateProvider,
            $urlRouterProvider: ng.ui.IUrlRouterProvider,
            $titleProvider: ng.ui.ITitleProvider,
            cefConfig: core.CefConfig) => {
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
        const templateRoot = fullInner(cefConfig.routes.ui) + "/framework/admin"; // <UI>/framework/admin
        let rootPath = fullInner(cefConfig.routes.admin); // /Admin/Clarity-Ecommerce-Admin
        if (rootPath === window.location.protocol + "//" + window.location.host) {
            rootPath = rootPath.replace(window.location.protocol + "//" + window.location.host, "/");
            if (rootPath === "") {
                // Have to have a value
                rootPath = "/";
            } else if (rootPath.startsWith("//")) {
                // Don't start with double slash if we accidentally set that
                rootPath = rootPath.replace("//", "/");
            }
        }
        const altRootPath = rootPath === "/" ? "" : rootPath;
        $urlMatcherFactoryProvider.caseInsensitive(true);
        if ("/" !== rootPath) {
            $urlRouterProvider.when("/", rootPath);
        }
        $urlRouterProvider.when(rootPath + "/", rootPath);
        $urlRouterProvider.otherwise($injector => $injector.get("$state").go("error.FourOhFour"));
        $titleProvider.documentTitle(($rootScope: ng.IRootScopeService, $translate: ng.translate.ITranslateService, $q: ng.IQService) =>
            $q((resolve, reject) => {
                if ($rootScope.$breadcrumbs && $rootScope.$breadcrumbs.length > 0) {
                    resolve(`${$rootScope.$breadcrumbs.map(x => x.translatedTitle || x.title).join(" > ")} - ${cefConfig.companyName}`);
                    return;
                }
                if ($rootScope.$title) {
                    $translate($rootScope.$title).then(translated => resolve(`${translated} - ${cefConfig.companyName}`),
                               result => resolve(`${result} - ${cefConfig.companyName}`))
                        .catch(reason => resolve(`${reason} - ${cefConfig.companyName}`));
                    return;
                }
                resolve(`${cefConfig.companyName}`);
            })
        );
        const makeContainerState = (name: string, permission: RegExp | string, outerView: string, url: string, translationKey: string): void => {
            const viewObj = {};
            viewObj["page"] = { template: `<div ui-view="${outerView || name}" class="full-height"></div>`, };
            $stateProvider.state(name, <ng.ui.IState>{
                resolve: { $title: () => translationKey },
                abstract: true,
                nomenu: true,
                requiresPermission: permission,
                url: altRootPath + url,
                views: viewObj
            });
        };
        const makeSectionState = (
                rootStateName: string,
                name: string,
                permission: RegExp | string,
                outerView: string,
                innerView: string,
                url: string,
                translationKey: string,
                overrideSettings: ng.ui.IState)
                : void => {
            const viewObj = {};
            viewObj[outerView || rootStateName] = {
                template: `<div ui-view="${innerView || name}" class="full-height"></div>`,
            };
            const state = <ng.ui.IState>{
                resolve: { $title: () => translationKey },
                abstract: true,
                nomenu: true,
                requiresPermission: permission,
                url: url,
                views: viewObj
            }
            if (overrideSettings && Object.keys(overrideSettings).length) {
                angular.merge(state, overrideSettings);
            }
            $stateProvider.state(`${rootStateName}.${name}`, state);
        };
        const makeListState = (
                rootStateName: string,
                name: string,
                permission: RegExp | string,
                innerView: string,
                overrideSettings: ng.ui.IState)
                : void => {
            const viewObj = {};
            viewObj[innerView || name] = {
                templateUrl: `${templateRoot}/views/${rootStateName}/${name}.list.html`,
            };
            const state = <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.common.Search" },
                requiresPermission: permission,
                url: "/List",
                views: viewObj
            };
            if (overrideSettings && Object.keys(overrideSettings).length) {
                angular.merge(state, overrideSettings);
            }
            $stateProvider.state(`${rootStateName}.${name}.list`, state);
        };
        const makeDetailState = (
                rootStateName: string,
                name: string,
                permission: RegExp | string,
                innerView: string,
                overrideSettings: ng.ui.IState)
                : void => {
            var viewObj = {};
            viewObj[innerView || name] = {
                templateUrl: `${templateRoot}/views/${rootStateName}/${name}.detail.html`,
            };
            const state = <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.common.NumberSymbolXDetail.Template" },
                nomenu: true,
                requiresPermission: permission,
                url: "/Detail/:ID",
                views: viewObj
            };
            if (overrideSettings && Object.keys(overrideSettings).length) {
                angular.merge(state, overrideSettings);
            }
            $stateProvider.state(`${rootStateName}.${name}.detail`, state);
        };
        const makeSetOfStates = (
                rootStateName: string,
                name: string,
                permission: RegExp | string,
                outerView: string,
                innerView: string,
                url: string,
                translationKey: string,
                overrideSettings?: { section?: ng.ui.IState, list?: ng.ui.IState, detail?: ng.ui.IState, })
                : void => {
            makeSectionState(rootStateName, name, permission, outerView, innerView, url, translationKey, overrideSettings && overrideSettings.section);
            makeListState(   rootStateName, name, permission, innerView,                                 overrideSettings && overrideSettings.list);
            makeDetailState( rootStateName, name, permission, innerView,                                 overrideSettings && overrideSettings.detail);
        };
        $stateProvider
            .state("home", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.states.Home" },
                requiresPermission: "Admin.Home",
                url: rootPath,
                views: { page: { templateUrl: templateRoot + "/views/home.html", } }
            })
            .state("login", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.states.Login" },
                url: altRootPath + "/Login",
                views: { page: { templateUrl: templateRoot + "/views/login.html" } }
            })
            .state("error", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.common.Error" },
                abstract: true,
                nomenu: true,
                url: altRootPath + "/Error",
                views: { page: { template: `<div ui-view="error" class="full-height"></div>` } }
            })
                .state("error.FourOhFour", <ng.ui.IState>{
                    resolve: { $title: () => "ui.admin.states.Error.404PageNotFound" },
                    url: "/404",
                    views: { error: { templateUrl: templateRoot + "/views/error.404.html" } }
                })
        ;
        // Sales area states
        makeContainerState("sales", /^Admin\.Sales\.[A-Za-z]+$/, null, "/Sales", "ui.admin.common.Sale.Plural");
        makeSetOfStates("sales",     "salesGroups",          "Admin.Sales.SalesGroups",            null, null,          "/Groups",                 "ui.admin.common.SalesGroup.Plural");
        makeSetOfStates("sales",     "salesOrders",          "Admin.Sales.SalesOrders",            null, null,          "/Orders",                 "ui.admin.common.Order.Plural");
        makeSetOfStates("sales",     "salesReturns",         "Admin.Sales.SalesReturns",           null, null,          "/Returns",                "ui.admin.common.SalesReturn.Plural");
        makeSetOfStates("sales",     "salesInvoices",        "Admin.Sales.SalesInvoices",          null, null,          "/Invoices",               "ui.admin.common.Invoice.Plural");
        makeSetOfStates("sales",     "salesQuotes",          "Admin.Sales.SalesQuotes",            null, null,          "/Quotes",                 "ui.admin.common.Quote.Plural");
        makeSetOfStates("sales",     "sampleRequests",       "Admin.Sales.SampleRequests",         null, null,          "/Sample-Requests",        "ui.admin.views.home.SampleRequests");
        makeSetOfStates("sales",     "purchaseOrders",       "Admin.Sales.PurchaseOrders",         null, null,          "/Purchase-Orders",        "ui.admin.common.PurchaseOrder.Plural");
        $stateProvider
            /*.state("sales.salesOrders.new", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.common.OrderActions.CreateNew" },
                nomenu: true,
                requiresPermission: "Ordering.SalesOrder.Create",
                url: "/New",
                views: { salesOrders: { templateUrl: templateRoot + "/views/sales/salesOrders.new.html", } },
            })*/
            .state("sales.salesOrders.create", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.common.OrderActions.CreateNew" },
                nomenu: true,
                requiresPermission: "Ordering.SalesOrder.Create",
                url: "/Create",
                views: { salesOrders: { templateUrl: templateRoot + "/views/sales/salesOrders.create.html", } },
            })
            .state("sales.salesReturns.new", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.controls.sales.salesReturnDetail.CreateNewReturn" },
                nomenu: true,
                requiresPermission: "Returning.SalesReturn.Create",
                url: "/New/:ID",
                views: { salesReturns: { templateUrl: templateRoot + "/views/sales/salesReturns.new.html", } },
            })
            .state("sales.salesQuotes.import", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.common.Import" },
                nomenu: true,
                requiresPermission: /^Quoting\.SalesQuote\.(Create|Update)$/,
                url: "/Import",
                views: { salesQuotes: { templateUrl: templateRoot + "/views/sales/salesQuotes.import.html", } },
            })
            .state("sales.salesQuotes.respondAsSupplier", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.controls.sales.salesQuoteResponseWizard.RespondingToSalesQuoteAsASupplier" },
                nomenu: true,
                requiresPermission: /^Quoting\.SalesQuote\.(Create|Update)$/,
                url: "/Respond-As-Supplier/:OriginalSalesQuoteID",
                views: { salesQuotes: { templateUrl: templateRoot + "/views/sales/salesQuotes.respondAsSupplier.html", } },
            })
            .state("sales.salesQuotes.reviewSupplierResponse", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.controls.sales.salesQuoteResponseWizard.ReviewSupplierResponse" },
                nomenu: true,
                requiresPermission: /^Quoting\.SalesQuote\.(Create|Update)$/,
                url: "/Review-Supplier-Response/:ResponseSalesQuoteID",
                views: { salesQuotes: { templateUrl: templateRoot + "/views/sales/salesQuotes.reviewSupplierResponse.html", } },
            })
        ;
        // Accounts area states
        makeContainerState("accounts", /^Admin\.Accounts\.[A-Za-z]+$/, "accounts", "/Accounts", "ui.admin.common.Account.Plural");
        makeSetOfStates("accounts",  "accounts",             "Admin.Accounts.Accounts",            null, "accountsSub", "/Accounts",               "ui.admin.common.Account.Plural");
        makeSetOfStates("accounts",  "users",                "Admin.Accounts.Users",               null, "accountsSub", "/Users",                  "ui.admin.common.User.Plural");
        makeSetOfStates("accounts",  "roles",                "Admin.Accounts.Roles",               null, "accountsSub", "/Roles",                  "ui.admin.common.Role.Plural");
        makeSetOfStates("accounts",  "discounts",            "Admin.Accounts.Discounts",           null, "accountsSub", "/Discounts",              "ui.admin.common.Discount.Plural");
        makeSetOfStates("accounts",  "reviews",              "Admin.Accounts.Reviews",             null, "accountsSub", "/Reviews",                "ui.admin.common.Review.Plural");
        makeSetOfStates("accounts",  "badges",               "Admin.Accounts.Badges",              null, "accountsSub", "/Badges",                 "ui.admin.common.Badge.Plural");
        makeSetOfStates("accounts",  "brands",               "Admin.Accounts.Brands",              null, "accountsSub", "/Brands",                 "ui.admin.common.Brand.Plural");
        makeSetOfStates("accounts",  "stores",               "Admin.Accounts.Stores",              null, "accountsSub", "/Stores",                 "ui.admin.common.Store.Plural");
        makeSetOfStates("accounts",  "siteDomains",          "Admin.Accounts.SiteDomains",         null, "accountsSub", "/Site-Domains",           "ui.admin.common.SiteDomain.Plural");
        makeSetOfStates("accounts",  "socialProviders",      "Admin.Accounts.SocialProviders",     null, "accountsSub", "/Social-Providers",       "ui.admin.common.SocialProvider.Plural");
        makeSetOfStates("accounts",  "priceRules",           "Admin.Accounts.PriceRules",          null, "accountsSub", "/Price-Rules",            "ui.admin.common.PriceRule.Plural");
        makeSetOfStates("accounts",  "ticketRules",          "Admin.Accounts.TicketRules",         null, "accountsSub", "/Ticket-Rules",           "ui.admin.common.TicketRule.Plural");
        // Inventory area states
        makeContainerState("inventory", /^Admin\.Inventory\.[A-Za-z]+$/, null, "/Inventory", "ui.admin.common.Inventory");
        makeSetOfStates("inventory", "products",             "Admin.Inventory.Products",           null, null,          "/Products",               "ui.admin.common.Product.Plural", {
            detail: {
                url: "/Detail/:ID?versionID",
                params: <ng.ui.RawParams>{
                    ID: <ng.ui.ParamDeclaration>{ type: "string", value: null, squash: false, isOptional: false, array: false },
                    versionID: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, isOptional: true, array: false }
                }
            }
        });
        makeSetOfStates("inventory", "warehouses",           "Admin.Inventory.Warehouses",         null, null,          "/Warehouses",             "ui.admin.common.Warehouse.Plural");
        makeSetOfStates("inventory", "vendors",              "Admin.Inventory.Vendors",            null, null,          "/Vendors",                "ui.admin.common.Vendor.Plural");
        makeSetOfStates("inventory", "manufacturers",        "Admin.Inventory.Manufacturers",      null, null,          "/Manufacturers",          "ui.admin.views.home.Manufacturers");
        makeSetOfStates("inventory", "categories",           "Admin.Inventory.Categories",         null, null,          "/Categories",             "ui.admin.common.Category.Plural");
        makeSetOfStates("inventory", "attributes",           "Admin.Inventory.Attributes",         null, null,          "/Attributes",             "ui.admin.common.Attribute.Plural");
        $stateProvider
            .state("inventory.products.import", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.common.Import" },
                nomenu: true,
                requiresPermission: /^Products\.Product\.(Create|Update)$/,
                url: "/Import",
                views: { products: { templateUrl: templateRoot + "/views/inventory/products.import.html", } },
            })
            .state("inventory.products.newFromSupplier", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.controls.inventory.newProductFromSupplier.NewProductFromSupplier" },
                nomenu: true,
                requiresPermission: /^Products\.Product\.(Create|Update)$/,
                url: "/New?vendorID&storeID",
                params: <ng.ui.RawParams>{
                    vendorID: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, isOptional: true, array: false },
                    storeID: <ng.ui.ParamDeclaration>{ type: "int", value: null, squash: true, isOptional: true, array: false }
                },
                views: { products: { templateUrl: templateRoot + "/views/inventory/products.newFromSupplier.html", } },
            })
        ;
        // Shipments area states
        makeContainerState("shipments", /^Admin\.Shipping\.[A-Za-z]+$/, null, "/Shipments", "ui.admin.views.home.Shipments");
        makeSetOfStates("shipments", "carrierAccounts",      "Admin.Shipping.CarrierAccounts",    null, null,           "/Carrier-Accounts",       "ui.admin.views.home.CarrierAccounts");
        makeSetOfStates("shipments", "shippingPackages",     "Admin.Shipping.Packages",           null, null,           "/Shipping-Packages",      "ui.admin.views.home.ShippingPackages");
        makeSetOfStates("shipments", "shippingDiscounts",    "Admin.Shipping.Packages",           null, null,           "/Shipping-Discounts",     "ui.admin.views.home.ShippingDiscounts", { detail: { url: '/Detail/:OptionName' }  });
        // System area states
        makeContainerState("system", /^Admin\.System\.[A-Za-z]+$/, null, "/System", "ui.admin.common.System");
        makeSetOfStates("system",    "settings",             "Admin.System.Settings",             null, null,           "/Settings",               "ui.admin.common.Setting.Plural");
        makeSetOfStates("system",    "emailQueues",          "Admin.System.EmailQueues",          null, null,           "/Email-Queues",           "ui.admin.views.home.EmailQueues");
        makeSetOfStates("system",    "emailTemplates",       "Admin.System.EmailTemplates",       null, null,           "/Email-Templates",        "ui.admin.views.home.EmailTemplates");
        makeSetOfStates("system",    "currencies",           "Admin.System.Currencies",           null, null,           "/Currencies",             "ui.admin.views.home.Currencies");
        makeSetOfStates("system",    "languages",            "Admin.System.Languages",            null, null,           "/Languages",              "ui.admin.common.Language.Plural");
        makeSetOfStates("system",    "logs",                 "Admin.System.SystemLogs",           null, null,           "/Logs",                   "ui.admin.views.home.Logs");
        makeSetOfStates("system",    "uiKeys",               "Admin.System.UiKeys",               null, null,           "/UI-Keys"   ,             "ui.admin.views.home.UIKeys");
        makeSetOfStates("system",    "uiTranslations",       "Admin.System.UiTranslations",       null, null,           "/UI-Translations",        "ui.admin.views.home.UITranslations");
        makeSetOfStates("system",    "importExportMappings", "Admin.System.ImportExportMappings", null, null,           "/Import-Export-Mappings", "ui.admin.views.home.ImportExportMappings");
        $stateProvider
            .state("system.avalara", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.common.AvalaraSettings" },
                url: "/Avalara",
                views: { system: { templateUrl: templateRoot + "/views/system/settings.avalara.html", } },
            })
            .state("system.apiReference", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.common.APIReference" },
                requiresPermission: "Admin.System.APIReference",
                url: "/API-Reference",
                views: { system: { templateUrl: templateRoot + "/views/system/APIReference.html", } },
            })
            .state("system.siteMaintenance", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.views.system.SiteMaintenance" },
                requiresPermission: "Admin.System.SiteMaintenance",
                url: "/Site-Maintenance",
                views: { system: { templateUrl: templateRoot + "/views/system/siteMaintenance.html", } },
            })
            .state("system.scheduledTasks", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.common.ScheduledTask.Plural" },
                requiresPermission: "Admin.System.ScheduledTasks",
                url: "/Scheduled-Tasks",
                views: { system: { templateUrl: templateRoot + "/views/system/scheduledTasks.html", } },
            })
            .state("system.connect", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.views.home.ClarityConnect" },
                requiresPermission: "Admin.System.ClarityConnect",
                url: "/Clarity-Connect",
                views: { system: { templateUrl: templateRoot + "/views/system/clarityConnect.html", } },
            })

            .state("system.reporting", <ng.ui.IState>{
                resolve: { $title: () => "ui.admin.views.home.Reporting" },
                abstract: true,
                nomenu: true,
                requiresPermission: /^Admin\.System\.Reports\.(?:Loader|Designer)$/,
                url: "/Reporting",
                views: { system: { template: `<div ui-view="reporting" class="full-height"></div>` } },
            })
                .state("system.reporting.designer", <ng.ui.IState>{
                    resolve: { $title: () => "ui.admin.common.Designer" },
                    requiresPermission: "Admin.System.Reports.Designer",
                    url: "/Designer",
                    views: { reporting: { templateUrl: templateRoot + "/views/system/reporting.designer.html", } },
                })
                .state("system.reporting.loader", <ng.ui.IState>{
                    resolve: { $title: () => "ui.admin.common.Loader" },
                    requiresPermission: "Admin.System.Reports.Loader",
                    url: "/Loader",
                    views: { reporting: { templateUrl: templateRoot + "/views/system/reporting.loader.html", } },
                })
                .state("system.reporting.sfloader", <ng.ui.IState>{
                    resolve: { $title: () => "ui.admin.common.Loader" },
                    requiresPermission: "Admin.System.Reports.Loader",
                    url: "/SF-Loader",
                    views: { reporting: { templateUrl: templateRoot + "/views/system/reporting.sfloader.html", } },
                })
        ;
        // Type/Status/State area states (the sets are loaded by a T4 file elsewhere)
        makeContainerState("types",    /^Admin\.Types\.[A-Za-z]+$/,    null, "/Types",    "ui.admin.common.Type.Plural");
        makeContainerState("statuses", /^Admin\.Statuses\.[A-Za-z]+$/, null, "/Statuses", "ui.admin.common.Status.Plural");
        makeContainerState("states",   /^Admin\.States\.[A-Za-z]+$/,   null, "/States",   "ui.admin.common.State.Plural");
    });
    /** Use permissions on states to prevent them from being loaded if current user doesn't have the permission */
    adminApp.run((
            $rootScope: ng.IRootScopeService,
            $state: ng.ui.IStateService,
            cvAuthenticationService: services.IAuthenticationService,
            cvSecurityService: services.ISecurityService,
            cvServiceStrings: services.IServiceStrings) => {
        $rootScope.$on(cvServiceStrings.events.$state.changeStart, (event, toState: ng.ui.IState/*, toParams, fromState: ng.ui.IState, fromParams*/) => {
            toState.resolve["guardStateChange"] = ($q: ng.IQService) => $q((resolve, reject) => {
                if (toState.name === "login") {
                    resolve();
                    return;
                }
                cvAuthenticationService.preAuth().finally(() => {
                    // Not logged in, go to login
                    if (!cvAuthenticationService.isAuthenticated()) {
                        event.preventDefault();
                        $state.go("login");
                        reject();
                        return;
                    }
                    // Cant find the page, go to 404
                    if (!$state.get(toState)) {
                        event.preventDefault();
                        $state.go("error.404");
                        reject();
                        return;
                    }
                    // If state has a permission requirement, validate it before going
                    if (angular.isDefined(toState.requiresPermission)) {
                        cvSecurityService.hasPermissionPromise(`${toState.requiresPermission}`)
                            .then(has => {
                                if (!has) {
                                    // Not validated successfully, go to login
                                    $state.go("login");
                                    reject();
                                    return;
                                }
                                resolve();
                            }).catch(reason => {
                                // Not validated successfully, go to login
                                $state.go("login");
                                reject(reason);
                            });
                        return;
                    }
                    resolve();
                });
            });
        });
    });
}

// These are added to satisfy interface requirements in build, they are not actually used by the admin app

module cef.store.services {
    export interface ICartService { }
    export interface ICurrentBrandService { }
    export interface ICurrentStoreService { }
    export interface ISearchCatalogProductCompareService { }
    export interface IStoreLocationService { }
    export interface cvLoginModalService {
        (callback: (...args: any[]) => void, returnUrl?: string, reloadPage?: boolean, noReturn?: boolean): ng.IPromise<boolean>;
    }
}

module cef.storeAdmin.services {
    export interface ICurrentStoreAdminService { }
}
