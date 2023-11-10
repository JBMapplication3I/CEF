module cef.store.core.uiRouterPlugins {
    export interface IUISetStateRefScope extends ng.IScope {
        /**
         * [Optional] When set with a positive integer, delay building the url into the UI
         * until after the specified time in milliseconds has elapsed
         * @type {number}
         * @default null
         * @example uisrp-delay="500"
         */
        uisrpDelay?: number;
        /**
         * [Optional] The root pathname that must be applied before the state can be entered
         * @type {string}
         * @default null
         * @example uisrp-root="/Catalog"
         * @example uisrp-root="/Dashboard"
         * @example uisrp-root="{{ctrl.cefConfig.routes.catalog.root}}"
         */
        uisrpRoot?: string;
        /**
         * [Optional] The state to enter
         * @type {string}
         * @default null
         * @example uisrp-state="searchCatalog.products.results.grid"
         * @example uisrp-state="userDashboard.salesOrders.detail"
         */
        uisrpState?: string;
        /**
         * [Optional] The state parameters object to pass to the state
         * @type {{ [param: string]: any }}
         * @default null
         * @example uisrp-params="{ category: 'Movies' }"
         * @example uisrp-params="{ ID: 999 }"
         */
        uisrpParams?: { [param: string]: any };
        /**
         * [Optional] The query parameters object to pass to the url
         * @type {{ [param: string]: any }}
         * @default null
         * @example uisrp-query-params="{ category: 'Movies' }" becomes ?category=Movies
         * @example uisrp-query-params="{ ID: 999, Preview: 64 }" becomes ?ID=999&Preview=64
         */
        uisrpQueryParams?: { [param: string]: any };
        /**
         * [Optional] The relative path to load in
         * @param {string}
         * @default null
         * @example '/Cart'
         * @example {{product.SeoUrl}}
         */
        uisrpPath?: string;
        /**
         * [Optional] Tell the relative path to load as query param
         * @type {boolean}
         * @default false
         */
        uisrpPathAsQuery?: boolean;
        /**
         * [Optional] Which url to use (primary, alternate-1, alternate-2 or alternate-3)
         * @type {string}
         * @default 'primary'
         */
        uisrpWhichUrl?: string;
        /**
         * [Optional] Which area to use (api, ui, site, admin, login, reporting, scheduler)
         * @type {string}
         * @default 'site'
         */
        uisrpArea?: string;
        /**
         * [Optional] When true, add noCache query string parameter with a unix timestamp
         * @type {boolean}
         * @default false
         */
        uisrpNoCache?: boolean;
        /**
         * [Optional] When true, ignore everything else and just go to the site home page
         * from the core config
         * Cannot use in conjunction with most other values
         * @type {boolean}
         * @default false
         */
        uisrpIsHome?: boolean;
        /**
         * [Optional] When true, go to the Sign In page location from the core config and
         * return to this URL
         * Cannot use in conjunction with most other values
         * @type {boolean}
         * @default false
         */
        uisrpIsAuthRedirect?: boolean;
        /**
         * [Optional] When true, the root is the catalog root url fragment from cefConfig
         * Cannot use in conjunction with {@see uisrpIsProduct} or {@see uisrpIsStore}
         * Requires {@see uisrpState} and it should be populated with a catalog state
         * Optionally add {@see uisrpParams} for the specified state
         * @type {boolean}
         * @default false
         */
        uisrpIsCatalog?: boolean;
        /**
         * [Optional] When true, the root is the product detail url fragment from cefConfig
         * Cannot use in conjunction with {@see uisrpIsCatalog} or {@see uisrpIsStore}
         * Requires {@see uisrpPath} and it should be populated with an SEO URL
         * @type {boolean}
         * @default false
         */
        uisrpIsProduct?: boolean;
        /**
         * [Optional] When true, the root is the store detail url fragment from cefConfig
         * Cannot use in conjunction with {@see uisrpIsCatalog} or {@see uisrpIsProduct}
         * Requires {@see uisrpPath} and it should be populated with an SEO URL
         * @type {boolean}
         * @default false
         */
        uisrpIsStore?: boolean;
        /**
         * [Optional] When true, the root is the dashboard url fragment from cefConfig
         * Cannot use in conjunction with {@see uisrpIsCatalog} or {@see uisrpIsProduct}
         * Requires {@see uisrpState} and it should be populated with the name of the
         * Dashboard state to load
         * @type {boolean}
         * @default false
         */
        uisrpIsDashboard?: boolean;
    }

    class UISREFController {
        // Bound Scope Properties
        uisrpDelay?: number;
        uisrpRoot?: string;
        uisrpState?: string;
        uisrpParams?: { [param: string]: any };
        uisrpQueryParams?: { [param: string]: any } | string;
        uisrpPath?: string;
        uisrpPathAsQuery?: boolean;
        uisrpWhichUrl?: string;
        uisrpArea?: string;
        uisrpNoCache?: boolean;
        uisrpIsHome?: boolean;
        uisrpIsAuthRedirect?: boolean;
        uisrpIsCatalog?: boolean;
        uisrpIsProduct?: boolean;
        uisrpIsStore?: boolean;
        uisrpIsDashboard?: boolean;
        // Functions
        build(): void {
            let builtPath = "";
            if (Boolean(this.uisrpIsHome)) {
                builtPath = builtPath + (this.cefConfig.routes.site.root || "/");
            } else if (Boolean(this.uisrpIsAuthRedirect)) {
                this.uisrpArea = "login";
                builtPath = builtPath + "#!?returnUrl=[ReturnUrl]";
            } else if (Boolean(this.uisrpIsDashboard)) {
                if (!this.uisrpState) {
                    //throw Error("A Dashboard State is required for Dashboard links");
                    return;
                }
                builtPath = builtPath + this.cefConfig.routes.dashboard.root;
            } else if (Boolean(this.uisrpIsCatalog)) {
                if (!this.uisrpState) {
                    //throw Error("A Search Catalog State is required for Catalog links");
                    return;
                }
                builtPath = builtPath + this.cefConfig.routes.catalog.root;
            } else if (Boolean(this.uisrpIsProduct)) {
                if (!this.uisrpPath) {
                    //throw Error("An SEO URL is required for Product Detail links")
                    return;
                }
                builtPath = builtPath + this.cefConfig.routes.productDetail.root;
            } else if (Boolean(this.uisrpIsStore)) {
                if (!this.uisrpPath) {
                    //throw Error("An SEO URL is required for Store Detail links");
                    return;
                }
                builtPath = builtPath + this.cefConfig.routes.storeDetail.root;
            } else if (this.uisrpRoot) {
                // This could be a relative or absolute path to use at the front of the url we are trying to go to
                builtPath = builtPath + this.uisrpRoot;
            }
            if (this.uisrpPath) {
                // We have a relative path to follow after the root
                if (this.uisrpPathAsQuery) {
                    builtPath = builtPath + "?path=" + this.uisrpPath;
                } else {
                    builtPath = builtPath + (this.uisrpPath.startsWith("/") ? "" : "/") + this.uisrpPath;
                }
            }
            if (this.uisrpState) {
                // We need the parser to find the state
                builtPath = builtPath + ":" + this.uisrpState;
            }
            if (this.uisrpQueryParams) {
                const o = angular.fromJson(this.uisrpQueryParams as string);
                const toParams = (obj: { [param: string]: string | number }): string => {
                    var str = [];
                    for (var p in obj) {
                        if (obj.hasOwnProperty(p)) {
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                        }
                    }
                    return str.join("&");
                };
                const converted = toParams(o);
                if (converted) {
                    builtPath = builtPath + "?" + converted;
                }
            }
            this.$element.removeAttr("href");
            this.$element.removeAttr("ui-sref");
            if (this.$element.is("button")) {
                this.$element.on("click", _ =>
                    this.$window.location.href = this.$filter("corsLink")(
                        builtPath,
                        this.uisrpArea || "site",
                        this.uisrpWhichUrl || "primary",
                        this.uisrpNoCache || false,
                        this.uisrpParams || null));
            } else {
                this.$element.attr("href", this.$filter("corsLink")(
                    builtPath,
                    this.uisrpArea || "site",
                    this.uisrpWhichUrl || "primary",
                    this.uisrpNoCache || false,
                    this.uisrpParams || null));
            }
        }
        load(): void {
            if (Number(this.uisrpDelay) > 0) {
                this.$timeout(() => this.build(), Number(this.uisrpDelay));
                return;
            }
            if (this.cefConfig.featureSet.brands.enabled) {
                this.setupWatchers();
                return;
            }
            this.build();
        }
        setupWatchers(): void {
            if (angular.isDefined(this.$rootScope["globalBrandSiteDomain"])) {
                return this.build();
            }
            if (angular.isDefined(this.$rootScope["globalBrandSiteDomainID"])) {
                return this.build();
            }
            let unbind1 = this.$rootScope.$on(this.cvServiceStrings.events.brands.globalBrandSiteDomainPopulated, () => this.build());
            this.$rootScope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $element: ng.IAugmentedJQuery,
                private readonly $filter: ng.IFilterService,
                private readonly $window: ng.IWindowService,
                private readonly $timeout: ng.ITimeoutService,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cefConfig: core.CefConfig) {
            this.load();
        }
    }

    export const uiSrefPlusDirectiveFn = (): ng.IDirective => ({
        restrict: "EA",
        scope: {
            uisrpDelay: "=?",
            uisrpRoot: "@?",
            uisrpState: "@",
            uisrpParams: "@?",
            uisrpQueryParams: "=?",
            uisrpPath: "@?",
            uisrpPathAsQuery: "@?",
            uisrpWhichUrl: "@?",
            uisrpArea: "@?",
            uisrpNoCache: "@?",
            uisrpIsHome: "@?",
            uisrpIsAuthRedirect: "@?",
            uisrpIsCatalog: "@?",
            uisrpIsProduct: "@?",
            uisrpIsStore: "@?",
            uisrpIsDashboard: "@?",
            uisrpIsStoreAdmin: "@?",
            uisrpIsBrandAdmin: "@?",
            uisrpIsVendorAdmin: "@?"
        },
        controller: UISREFController,
        controllerAs: "setStateCtrl",
        bindToController: true
    });
}
