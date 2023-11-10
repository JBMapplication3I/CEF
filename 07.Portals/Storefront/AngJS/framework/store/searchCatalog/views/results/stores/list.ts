module cef.store.searchCatalog.views.results {
    class SearchCatalogStoreResultsListController extends SearchCatalogResultsControllerBase {
        // Bound Scope Properties
        // <See inherited>
        // Convenience Redirects
        // <See inherited>
        // Properties
        // <None>
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig, // Used by UI
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService,
                private readonly $location: ng.ILocationService,
                private readonly subdomain: string,
                private readonly $cookies: ng.cookies.ICookiesService,
                protected readonly $scope: ng.IScope,
                private readonly $filter: ng.IFilterService) {
            super(cefConfig, cvAuthenticationService, cvSearchCatalogService);
        }

        private getCookiesOptions(): ng.cookies.ICookiesOptions {
            return <ng.cookies.ICookiesOptions>{
                path: "/",
                domain: this.cefConfig.useSubDomainForCookies || !this.subdomain
                    ? this.$location.host()
                    : this.$location.host().replace(this.subdomain, "")
            };
        }

        setSelectedStore(storeID: string): void {
            this.$cookies.put("cefSelectedStoreID", storeID, this.getCookiesOptions());
            let builtPath = this.cefConfig.routes.catalog.root + ":" + "searchCatalog.products.results.both";
            let params = {
                'term': null,
                categoriesAny: null,
                categoriesAll: null,
                pricingRanges: null,
                attributesAny: null,
                attributesAll: null,
                storeId: storeID
            };
            this.$filter("goToCORSLink")(builtPath, "site", "primary", false, params);
        }
    }

    cefApp.directive("cefSearchCatalogStoreResultsList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            thumbWidth: "=?",
            thumbHeight: "=?",
            nameRows: "=?",
            hideStock: "=?",
            hideSku: "=?",
            hideShortDesc: "=?",
            shortDescRows: "=?",
            pricingDisplayStyle: "=?",
            quickAdd: "=?",
            actionButtonView: "=?",
            hideFavoritesList: "=?",
            hideWishList: "=?",
            hideNotifyMe: "=?",
            hideCompare: "=?",
            hideIcons: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/views/results/stores/list.html", "ui"),
        controller: SearchCatalogStoreResultsListController,
        controllerAs: "scstlCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
