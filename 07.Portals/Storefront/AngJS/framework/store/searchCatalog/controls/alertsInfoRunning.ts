module cef.store.searchCatalog.controls {
    class SearchCatalogAlertsInfoRunningController extends core.TemplatedControllerBase {
        // Convenience Points (reduces HTML size)
        private searched: boolean;
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService,
                readonly cvServiceStrings: services.IServiceStrings,) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.catalog.searchComplete,
                () => this.searched = true);
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefSearchCatalogAlertsInfoRunning", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/alertsInfoRunning.html", "ui"),
        controller: SearchCatalogAlertsInfoRunningController,
        controllerAs: "scairCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
