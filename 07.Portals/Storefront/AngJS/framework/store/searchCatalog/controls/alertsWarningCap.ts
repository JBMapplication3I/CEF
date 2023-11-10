module cef.store.searchCatalog.controls {
    class SearchCatalogAlertsWarningCapController extends core.TemplatedControllerBase {
        // Convenience Points (reduces HTML size)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Functions
        shouldShow(): boolean {
            return !this.service.searchIsRunning
                && this.service.activeSearchViewModel
                && this.service.activeSearchViewModel.Total >= 10000;
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogAlertsWarningCap", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/alertsWarningCap.html", "ui"),
        controller: SearchCatalogAlertsWarningCapController,
        controllerAs: "scawcCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
