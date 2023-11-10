module cef.store.searchCatalog.views.results {
    class SearchCatalogProductResultsListController extends SearchCatalogResultsControllerBase {
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
                protected readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig, cvAuthenticationService, cvSearchCatalogService);
        }
    }

    cefApp.directive("cefSearchCatalogProductResultsList", ($filter: ng.IFilterService): ng.IDirective => ({
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
            hideIcons: "=?",
            hideAttrs: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/views/results/products/list.html", "ui"),
        controller: SearchCatalogProductResultsListController,
        controllerAs: "scprlCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
