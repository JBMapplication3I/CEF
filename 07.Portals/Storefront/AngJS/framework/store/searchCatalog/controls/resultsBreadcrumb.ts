module cef.store.searchCatalog.controls {
    class SearchCatalogResultsBreadcrumbController extends core.TemplatedControllerBase {
        // Properties
        routingDisabled: boolean = false;
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Functions
        goToState(state: string | ng.ui.IState): void {
            this.routingDisabled = true;
            this.$state.transitionTo(state as ng.ui.IState);
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $state: ng.ui.IStateService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSearchCatalogService: services.SearchCatalogService) { // Used by UI
            super(cefConfig);
            const unbind1 = $scope.$watch(
                () => this.$state && this.$state.current && this.$state.current.url,
                (newValue: string) => {
                    this.cvSearchCatalogService.breadcrumb.compare = newValue == "/Compare";
                });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefSearchCatalogResultsBreadcrumb", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/resultsBreadcrumb.html", "ui"),
        controller: SearchCatalogResultsBreadcrumbController,
        controllerAs: "scrbCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
