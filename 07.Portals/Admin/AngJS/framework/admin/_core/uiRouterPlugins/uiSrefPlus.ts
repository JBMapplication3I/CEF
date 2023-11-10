module cef.admin.core.uiRouterPlugins {
    export interface IUISetStateRefScope extends ng.IScope {
        /**
         * [Optional] The root pathname that must be applied before the state can be entered
         * @type {string}
         * @default null
         * @example uisrp-root="/Catalog"
         * @example uisrp-root="/Dashboard"
         * @example uisrp-root="{{ctrl.cefConfig.catalog.root}}"
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

    export const uiSrefPlusDirectiveFn = (): ng.IDirective => ({
        restrict: "EA",
        scope: {
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
        controller(
                $element: ng.IAugmentedJQuery,
                $filter: ng.IFilterService,
                $window: ng.IWindowService,
                $location: ng.ILocationService,
                cefConfig: core.CefConfig) {
            let builtPath = "";
            if (Boolean(this.uisrpIsHome)) {
                builtPath = builtPath + (cefConfig.routes.site.root || "/");
            } else if (Boolean(this.uisrpIsAuthRedirect)) {
                this.uisrpArea = "login";
                builtPath = builtPath + "#!?returnUrl=[ReturnUrl]";
            } else if (Boolean(this.uisrpIsDashboard)) {
                if (!this.uisrpState) {
                    throw Error("A Dashboard State is required for Dashboard links");
                }
                builtPath = builtPath + cefConfig.routes.dashboard.root;
            } else if (Boolean(this.uisrpIsStoreAdmin)) {
                if (!this.uisrpState) {
                    throw Error("A Store Admin State is required for Store Admin links");
                }
                this.uisrpArea = "myStoreAdmin";
                builtPath = builtPath + (cefConfig.routes.myStoreAdmin.root || "");
            } else if (Boolean(this.uisrpIsBrandAdmin)) {
                if (!this.uisrpState) {
                    throw Error("A Brand Admin State is required for Brand Admin links");
                }
                this.uisrpArea = "myBrandAdmin";
                builtPath = builtPath + (cefConfig.routes.myBrandAdmin.root || "");
            } else if (Boolean(this.uisrpIsVendorAdmin)) {
                if (!this.uisrpState) {
                    throw Error("A Vendor Admin State is required for Vendor Admin links");
                }
                this.uisrpArea = "myVendorAdmin";
                builtPath = builtPath + (cefConfig.routes.myVendorAdmin.root || "");
            } else if (Boolean(this.uisrpIsCatalog)) {
                if (!this.uisrpState) {
                    throw Error("A Search Catalog State is required for Catalog links");
                }
                builtPath = builtPath + cefConfig.routes.catalog.root;
            } else if (Boolean(this.uisrpIsProduct)) {
                if (!this.uisrpPath) {
                    throw Error("An SEO URL is required for Product Detail links");
                }
                builtPath = builtPath + cefConfig.routes.productDetail.root;
            } else if (Boolean(this.uisrpIsStore)) {
                if (!this.uisrpPath) {
                    throw Error("An SEO URL is required for Store Detail links");
                }
                builtPath = builtPath + cefConfig.routes.storeDetail.root;
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
                const o = angular.fromJson(this.uisrpQueryParams);
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
            $element.removeAttr("href");
            $element.removeAttr("ui-sref");
            if ($element.is("button")) {
                $element.on("click", _ =>
                    $window.location.href = $filter("corsLink")(
                        builtPath,
                        this.uisrpArea || "site",
                        this.uisrpWhichUrl || "primary",
                        this.uisrpNoCache || false,
                        this.uisrpParams || null));
            } else {
                $element.attr("href", $filter("corsLink")(
                    builtPath,
                    this.uisrpArea || "site",
                    this.uisrpWhichUrl || "primary",
                    this.uisrpNoCache || false,
                    this.uisrpParams || null));
            }
        },
        controllerAs: "setStateCtrl",
        bindToController: true
    });
}
