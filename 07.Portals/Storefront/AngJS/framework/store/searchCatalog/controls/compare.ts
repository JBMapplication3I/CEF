/**
 * @file framework/store/searchCatalog/controls/compare.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Compare controller
 */
module cef.store.searchCatalog.controls {
    class CompareProductsController extends core.TemplatedControllerBase {
        // Bound Scope properties
        // Convenience Points (reduces HTML size)
        get service(): services.ISearchCatalogProductCompareService {
            return this && this.cvSearchCatalogProductCompareService;
        }
        // Functions
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvSearchCatalogProductCompareService: services.ISearchCatalogProductCompareService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefCompareProducts", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        replace: true,
        scope: { },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/compare.html", "ui"),
        controller: CompareProductsController,
        controllerAs: "compareCtrl",
        bindToController: true
    }));
}
