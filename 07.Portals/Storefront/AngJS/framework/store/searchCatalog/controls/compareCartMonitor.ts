/**
 * @file framework/store/searchCatalog/controls/compareCartMonitor.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Compare cart monitor class
 */
module cef.store.searchCatalog.controls {
    class SearchCatalogCompareCartController extends core.TemplatedControllerBase {
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.ISearchCatalogProductCompareService {
            return this && this.cvSearchCatalogProductCompareService;
        }
        // Constructor
        constructor(
                private readonly $state: ng.ui.IStateService, // Used by UI
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogProductCompareService: services.ISearchCatalogProductCompareService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogCompareCartMonitor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/compareCartMonitor.html", "ui"),
        controller: SearchCatalogCompareCartController,
        controllerAs: "scccCtrl"
    }));
}
