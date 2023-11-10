module cef.store.searchCatalog.controls {
    class SearchCatalogAlertsErrorBadSearchController extends core.TemplatedControllerBase {
        // Convenience Points (reduces HTML size)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private cvSearchCatalogService: services.SearchCatalogService) { // Used by HTML
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogAlertsErrorBadSearch", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/alertsErrorBadSearch.html", "ui"),
        controller: SearchCatalogAlertsErrorBadSearchController,
        controllerAs: "scaebsCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
