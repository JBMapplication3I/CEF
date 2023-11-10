module cef.store.searchCatalog.controls {
    class SearchCatalogFilterByRegionController extends core.TemplatedControllerBase {
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
        get regions() {
            return this.cvSearchCatalogService.activeSearchViewModel
                && this.cvSearchCatalogService.activeSearchViewModel["Regions"];
        }
        pushRegion(region: api.IndexableRegionModel): void {
            if (this.searchIsRunning) {
                return;
            }
            this.cvSearchCatalogService.pushRegion(region);
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogFilterByRegion", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            mode: "@" // "single" (default: "single")
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/filterByRegion.html", "ui"),
        controller: SearchCatalogFilterByRegionController,
        controllerAs: "scfbrCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
