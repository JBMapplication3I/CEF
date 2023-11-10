module cef.store.searchCatalog.controls {
    class SearchCatalogFilterByDistrictController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        mode: string;
        limitAttrsTo: number;
        limitOptionsTo: number;
        sortAttributes: string[];
        // Properties
        showOptions = new cefalt.store.Dictionary<boolean>();
        isOpen: boolean;
        // Convenience Points (reduces HTML size)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        get searchIsRunning(): boolean {
            return this.cvSearchCatalogService.searchIsRunning;
        }
        get districts() {
            return this.cvSearchCatalogService.activeSearchViewModel
                && this.cvSearchCatalogService.activeSearchViewModel["Districts"];
        }
        pushDistrict(district: api.IndexableDistrictModel): void {
            if (this.searchIsRunning) {
                return;
            }
            this.cvSearchCatalogService.pushDistrict(district);
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogFilterByDistrict", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            mode: "@" // "single" (default: "single")
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/filterByDistrict.html", "ui"),
        controller: SearchCatalogFilterByDistrictController,
        controllerAs: "scfbdCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
