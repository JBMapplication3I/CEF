module cef.store.searchCatalog.controls {
    class SearchCatalogFilterByQueryTermController extends core.TemplatedControllerBase {
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        get form(): api.ProductCatalogSearchForm {
            return this && this.service.activeSearchViewModel.Form;
        }
        // Properties
        kind = "products";
        // Functions
        applyQueryTerm(): void {
            const stateName = `searchCatalog.${this.kind}.results.both`;
            const params: ng.ui.IStateParamsServiceForSearchCatalog = {
                format: this.cefConfig.catalog.defaultFormat,
                page: 1,
                size: this.cefConfig.catalog.defaultPageSize,
                sort: this.cefConfig.catalog.defaultSort,
                term: this.cvSearchCatalogService.queryTerm,
                category: null
            };
            // TODO: This logic should be moved into the goToCORSLink if not already there
            if (window.location.href.toLowerCase().indexOf(this.cefConfig.routes.catalog.root.toLowerCase()) === -1) {
                this.$filter("goToCORSLink")("SearchCatalogState:" + stateName, "catalog", "primary", false, params);
                return;
            }
            this.$state.go(stateName, params);
        }
        // Events
        inputKeyPress(event: JQueryKeyEventObject): void {
            // event: Angular returns this type of object per their docs
            // Only do anything if it was the enter key
            if (event.key !== "Enter") {
                return;
            }
            // And only if the user actually entered something
            if (!this.cvSearchCatalogService.queryTerm) {
                return;
            }
            event.preventDefault();
            event.stopPropagation();
            this.applyQueryTerm();
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

    cefApp.directive("cefSearchCatalogFilterByQueryTerm", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/filterByQueryTerm.html", "ui"),
        controller: SearchCatalogFilterByQueryTermController,
        controllerAs: "scfbqtCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
