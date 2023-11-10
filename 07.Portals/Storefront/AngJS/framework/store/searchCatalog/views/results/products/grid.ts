module cef.store.searchCatalog.views.results {
    class SearchCatalogProductResultsGridController extends SearchCatalogResultsControllerBase {
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
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig, cvAuthenticationService, cvSearchCatalogService);
        }
    }

    cefApp.directive("cefSearchCatalogProductResultsGrid", ($filter: ng.IFilterService): ng.IDirective => ({
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
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/views/results/products/grid.html", "ui"),
        controller: SearchCatalogProductResultsGridController,
        controllerAs: "scprgCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
