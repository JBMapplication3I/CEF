module cef.store.searchCatalog.controls {
    class SearchCatalogFilterByTypeController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        mode: string;
        limitAttrsTo: number;
        limitOptionsTo: number;
        sortAttributes: string[];
        // Properties
        showOptions = new cefalt.store.Dictionary<boolean>();
        isOpen: boolean;
        get searchIsRunning(): boolean {
            return this.cvSearchCatalogService.searchIsRunning;
        }
        get types() {
            return this.cvSearchCatalogService.activeSearchViewModel
                && this.cvSearchCatalogService.activeSearchViewModel.Types;
        }
        pushType(type: api.IndexableTypeModel): void {
            if (this.searchIsRunning) {
                return;
            }
            this.cvSearchCatalogService.pushType(type);
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogFilterByType", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            mode: "@" // "single" (default: "single")
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/filterByType.html", "ui"),
        controller: SearchCatalogFilterByTypeController,
        controllerAs: "scfbtCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
