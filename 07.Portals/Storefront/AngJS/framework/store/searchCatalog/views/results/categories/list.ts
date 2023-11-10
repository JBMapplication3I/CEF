module cef.store.searchCatalog.views.results {
    class SearchCatalogCategoryResultsListController extends SearchCatalogResultsControllerBase {
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
                protected readonly $scope: ng.IScope) {
            super(cefConfig, cvAuthenticationService, cvSearchCatalogService);
        }
    }

    cefApp.directive("cefSearchCatalogCategoryResultsList", ($filter: ng.IFilterService): ng.IDirective => ({
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
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/views/results/categories/list.html", "ui"),
        controller: SearchCatalogCategoryResultsListController,
        controllerAs: "sccalCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
