module cef.store.searchCatalog.controls {
    class SearchCatalogAlertsInfoNoResultsController extends core.TemplatedControllerBase {
        // Convenience Points (reduces HTML size)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        private searched: boolean;
        // Functions
        shouldShow(): boolean {
            return !this.service.searchIsRunning
                && this.searched
                && this.service.activeSearchViewModel
                && angular.isDefined(this.service.activeSearchViewModel.Total)
                && this.service.activeSearchViewModel.Total <= 0;
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.catalog.searchComplete,
                () => this.searched = true);
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefSearchCatalogAlertsInfoNoResults", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/alertsInfoNoResults.html", "ui"),
        controller: SearchCatalogAlertsInfoNoResultsController,
        controllerAs: "scainrCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
