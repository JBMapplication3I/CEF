/**
 * @file framework/store/searchCatalog/controls/resultsLoadMore.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Results page load more class
 */
module cef.store.searchCatalog.controls {
    class SearchCatalogLoadMoreController extends core.TemplatedControllerBase {
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Properties
        private loadMorePages: number = 0;
        private hasNoResults: boolean;
        // Cache Management
        load(): void {
            // attach scroll event
            $(window).on("scroll", () => {
                if (($(window).scrollTop() + $(window).innerHeight()) >= $(document).height() - 100) {
                    this.loadMore();
                }
            });
        }
        loadMore(): void {
            if (this.viewState.running) {
                this.consoleLog("Search is currently running, returning.");
                return;
            }
            if (this.hasNoResults) {
                this.consoleLog("No results.");
                return;
            }
            this.loadMoreInner();
        }
        private loadMoreInner(): void {
            this.loadMorePages++;
            this.consoleLog("Current searchViewModelPr\n", this.service.searchViewModelPr);
            this.setRunning();
            // TODO@JTG: Replace this with a call to the cvSearchCatalogService so it calls the appropriate kind
            this.cvApi.providers.SearchProductCatalogWithProvider({
                ...this.service.searchViewModelPr.Form,
                Page: this.service.searchViewModelPr.Form.Page + this.loadMorePages
            }).then(searchResult => {
                const localSearchViewModel = searchResult.data;
                this.consoleLog("SUCCESS on searchCatalogWithProvider\n", localSearchViewModel);
                this.cvProductService.bulkGet(localSearchViewModel.ResultIDs).then(bulkGetResult => {
                    // Append bulkGetResult to existing searchModel withMaps
                    ////this.service.searchViewModelPr["WithMaps"].push(...bulkGetResult);
                    this.service.searchViewModelPr.ResultIDs.push(...localSearchViewModel.ResultIDs);
                    this.consoleLog("SUCCESS on bulkGet, add withMaps\n", this.service.searchViewModelPr);
                    this.finishRunning();
                }).catch(err => {
                    this.consoleError("failed on bulkGet\n", err);
                    this.hasNoResults = true;
                    this.finishRunning(true, err);
                });
            }).catch(err => {
                this.consoleError("failed on searchProductCatalogWithProvider\n", err);
                this.finishRunning(true, err);
            });
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvProductService: services.ProductService,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
            this.load();
            const unbind1 = $scope.$on(cvServiceStrings.events.catalog.change, () => {
                this.loadMorePages = 0;
                this.hasNoResults = false;
            });
            $scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefSearchCatalogLoadMore", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { pageViewType: "@", defaultValue: "@", hideLabel: "@" },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/resultsLoadMore.html", "ui"),
        controller: SearchCatalogLoadMoreController,
        controllerAs: "sclmCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
