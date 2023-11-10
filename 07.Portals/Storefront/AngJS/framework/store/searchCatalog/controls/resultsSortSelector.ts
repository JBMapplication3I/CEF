module cef.store.searchCatalog.controls {
    class SearchCatalogResultsSortController extends core.TemplatedControllerBase {
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Properties
        private get searchViewModel() {
            return this.service.activeSearchViewModel;
        }
        get currentChoice(): any {
            return this.searchViewModel && this.searchViewModel.Form.Sort
                || this.defaultValue as any
                || api.SearchSort.Relevance.toString();
        }
        set currentChoice(newValue: any) {
            this.searchViewModel.Form.Sort = newValue as any;
        }
        get choices(): Array<string> {
            return [
                services.SearchCatalogService.stateAu,
                services.SearchCatalogService.stateCa,
                services.SearchCatalogService.stateFr,
                services.SearchCatalogService.stateMa,
                services.SearchCatalogService.stateSt,
                services.SearchCatalogService.stateVe,
            ].indexOf(this.service.activeStateRoot) >= 0
                ? [
                    "Relevance",
                    "Recent",
                    "NameAscending",
                    "NameDescending",
                ]
                : [
                    "Relevance",
                    "Recent",
                    "NameAscending",
                    "NameDescending",
                    "PricingAscending",
                    "PricingDescending",
                ];
        }
        defaultValue: api.SearchSort | string; // From scope
        sortViewType: string; // From scope
        hideLabel: boolean; // From scope
        // Functions
        // <None>
        // Constructor
        constructor(
                private readonly $state: ng.ui.IStateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogResultsSort", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { sortViewType: "@", defaultValue: "@", hideLabel: "@" },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/resultsSortSelector.html", "ui"),
        controller: SearchCatalogResultsSortController,
        controllerAs: "scrsCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
