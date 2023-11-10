module cef.store.searchCatalog.controls {
    class SearchCatalogResultsPerPageController extends core.TemplatedControllerBase {
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Properties from Scope
        defaultValue: number;
        perPageViewType: string;
        hideLabel: boolean;
        perPageChoices: Array<number>;
        // Properties
        private get searchViewModel() {
            return this.service.activeSearchViewModel;
        }
        get currentChoice(): number {
            return this.searchViewModel && this.searchViewModel.Form.PageSize
                || this.defaultValue
                || 9;
        }
        set currentChoice(newValue: number) {
            this.searchViewModel.Form.PageSize = newValue;
        }
        get choices(): Array<number> {
            return this.perPageChoices || [9, 18, 27];
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogResultsPerPage", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { perPageViewType: "@", defaultValue: "=", hideLabel: "@", perPageChoices: "=?" },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/resultsPerPageSelector.html", "ui"),
        controller: SearchCatalogResultsPerPageController,
        controllerAs: "scrppCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
