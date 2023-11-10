/**
 * @file externalSearchBox.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc External Search Box directive class
 */
module cef.store.searchCatalog.controls {
    class SearchCatalogExternalSearchBoxController extends core.TemplatedControllerBase {
        // Properties
        kind = "products";
        // Convenience Properties (redirects to reduce HTML size)
        get service(): services.SearchCatalogService {
            return this.cvSearchCatalogService;
        }
        // Functions
        applyQueryTerm(): void {
            event.preventDefault();
            event.stopPropagation();
            const stateName = `searchCatalog.${this.kind}.results.both`;
            const params: ng.ui.IStateParamsServiceForSearchCatalog = {
                format: this.cefConfig.catalog.defaultFormat,
                page: 1,
                size: this.cefConfig.catalog.defaultPageSize,
                sort: this.cefConfig.catalog.defaultSort,
                term: this.cvSearchCatalogService.queryTerm,
                category: null,
                attribute: null,
                attributesAll: null,
                attributesAny: null,
                pricingRanges: null,
                ratingRanges: null
            };
            if (window.location.href.toLowerCase().indexOf(this.cefConfig.routes.catalog.root.toLowerCase()) === -1) {
                this.$filter("goToCORSLink")("SearchCatalogState:" + stateName, "catalog", "primary", false, params);
                return;
            }
            this.$state.go(stateName, params);
        }
        goToProduct(suggestResultItem: api.SuggestResultBase){
            if (!suggestResultItem.SeoUrl) {
                return;
            }
            this.$filter("goToCORSLink")("/Product/" + suggestResultItem.SeoUrl, "site", "primary", false, null);
        }
        // Return url for category
        categorySearch(category): string {
            const stateName = `searchCatalog.${this.kind}.results.both`;
            const params: ng.ui.IStateParamsServiceForSearchCatalog = {
                format: this.cefConfig.catalog.defaultFormat,
                page: 1,
                size: this.cefConfig.catalog.defaultPageSize,
                sort: this.cefConfig.catalog.defaultSort,
                term: this.cvSearchCatalogService.queryTerm,
                category: category
            };
            return this.$state.href(stateName, params);
        }
        // Events
        inputKeyPress(event: JQueryKeyEventObject): void { // Angular returns this type of object per their docs
            if (event.key !== "Enter") { return; } // Only do anything if it was the enter key
            if (!this.cvSearchCatalogService.queryTerm) { return; } // And only if the user actually entered something
            event.preventDefault();
            event.stopPropagation();
            this.applyQueryTerm();
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $state: ng.ui.IStateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService,
                private readonly cvStoreLocationService: services.IStoreLocationService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogExternalSearchBox", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/externalSearchBox.html", "ui"),
        controller: SearchCatalogExternalSearchBoxController,
        controllerAs: "scesbCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
