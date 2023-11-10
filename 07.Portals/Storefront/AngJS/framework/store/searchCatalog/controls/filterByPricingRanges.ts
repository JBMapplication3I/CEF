module cef.store.searchCatalog.controls {
    class SearchCatalogFilterByPricingRangesController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        mode: string;
        // Properties
        get searchIsRunning(): boolean {
            return this && this.cvSearchCatalogService.searchIsRunning;
        }
        // Convenience Points (reduces HTML size)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Functions
        pricingRangeHandler(): void {
            if (this.searchIsRunning) {
                return;
            }
            this.cvSearchCatalogService.clearRangeKeyPr(this.mode);
            var pricing: number[] = $.map($('.range-wrapper input#pricing-range'),
                elem => (
                    [elem.valueLow !== undefined ? elem.valueLow : elem.min,
                    elem.valueHigh !== undefined ? elem.valueHigh : elem.max]));
            if (angular.isDefined(pricing[0]) && angular.isDefined(pricing[1])) {
                this.cvSearchCatalogService.pushRangeKeyPr(
                    this.mode,
                    `$${pricing[0]} - ${pricing[1]}`);
            }
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogFilterByPricingRanges", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { mode: "@" },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/filterByPricingRanges.html", "ui"),
        controller: SearchCatalogFilterByPricingRangesController,
        controllerAs: "scfbprCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
