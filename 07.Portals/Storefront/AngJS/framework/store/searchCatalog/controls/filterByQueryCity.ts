module cef.store.searchCatalog.controls {
    class SearchCatalogFilterByQueryCityController extends core.TemplatedControllerBase {
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        get form(): api.ProductCatalogSearchForm {
            return this && this.service.activeSearchViewModel.Form;
        }
        // Properties
        kind: string = "stores";
        // Functions
        setSearchForm(): void {
            this.service.setSearchForm(this.kind);
        }
        // Events
        inputKeyPress(event: JQueryKeyEventObject): void {
            // event: Angular returns this type of object per their docs
            // Only do anything if it was the enter key
            if (event.key !== "Enter") {
                return;
            }
            // And only if the user actually entered something
            if (!this.cvSearchCatalogService.queryTerm && !this.cvSearchCatalogService.queryCity) {
                return;
            }
            event.preventDefault();
            event.stopPropagation();
            this.service.setSearchForm(this.kind);
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $state: ng.ui.IStateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogFilterByQueryCity", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            kind: "=?"            // 'products', 'stores', etc
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/filterByQueryCity.html", "ui"),
        controller: SearchCatalogFilterByQueryCityController,
        controllerAs: "scfbqcCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
