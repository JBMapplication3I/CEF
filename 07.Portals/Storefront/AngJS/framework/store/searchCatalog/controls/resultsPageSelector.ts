/**
 * @file framework/store/searchCatalog/controls/resultsPageSelector.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Results page selector class
 */
module cef.store.searchCatalog.controls {
    class SearchCatalogResultsPageController extends core.TemplatedControllerBase {
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Bound Scope Properties
        defaultValue: number;
        pageViewType: string;
        hideLabel: boolean;
        // Properties
        private get searchViewModel(): any {
            return this.service.activeSearchViewModel;
        }
        get currentChoice(): number {
            return (this.searchViewModel && this.searchViewModel.Form.Page || this.defaultValue || 1) - 1;
        }
        set currentChoice(newValue: number) {
            this.searchViewModel.Form.Page = newValue + 1;
        }
        // Data
        get totalCount(): number { return this.searchViewModel && this.searchViewModel.Total; }
        get pageSize(): number { return this.searchViewModel && this.searchViewModel.Form.PageSize; }
        set pageSize(value: number) { this.searchViewModel.Form.PageSize = value; this.resetCaches(); }
        get pageSetSize(): number { return this.searchViewModel && this.searchViewModel.Form.PageSetSize; }
        set pageSetSize(value: number) { this.searchViewModel.Form.PageSetSize = value; this.resetCaches(); }
        get currentPageFrom(): number { return this.skip() + 1; }
        get currentPageTo(): number {
            const unlimitedTo = (this.skip() + (this.pageSize <= 0 ? 0 : this.pageSize));
            return this.searchViewModel && this.searchViewModel.Total < unlimitedTo
                ? this.searchViewModel.Total
                : unlimitedTo;
        }
        get showingLabelContent(): string {
            return this.searchViewModel && this.searchViewModel.Total <= 0
                ? this.$translate.instant("ui.storefront.common.NoResults")
                : this.$translate.instant("ui.storefront.pagination.ShowingXToYOfZ.Template", {
                    min: this.$filter("number")(this.currentPageFrom, 0),
                    max: this.$filter("number")(this.currentPageTo, 0),
                    total: this.$filter("number")(this.searchViewModel && this.searchViewModel.Total || 0, 0)
                });
        }
        pageSetCurrent = 0;
        // Cache Management
        cachedPagesInSets: Array<Array<number>> = null;
        resetCachedPagesInSets = (): void => {
            this.cachedPagesInSets = null;
        }
        resetCaches = (): void => {
            this.resetCachedPagesInSets(); this.goToFirstPage();
        }
        // Calculate
        maxPageSet = (): number => {
            return Math.max(0, this.pagesInSets.length - 1);
        }
        skip = (): number => {
            return (this.currentChoice * (this.pageSize <= 0 ? 0 : this.pageSize));
        }
        maxPage = (): number => {
            return this.pageSize <= 0
                ? 0
                : Math.ceil((this.searchViewModel && this.searchViewModel.Total || 1) / this.pageSize);
        }
        get pagesInSets(): Array<Array<number>> {
            if (this.cachedPagesInSets && this.cachedPagesInSets.length > 0) {
                return this.cachedPagesInSets;
            }
            const sets = new Array<Array<number>>();
            let currentSet = 0;
            for (let i = 0; i < this.maxPage(); i++) {
                if (!sets[currentSet]) { sets[currentSet] = []; }
                sets[currentSet].push(i);
                if (sets[currentSet].length >= this.pageSetSize) {
                    currentSet++;
                }
            }
            this.cachedPagesInSets = sets;
            return this.cachedPagesInSets;
        }
        getFirstPageInPageSet = (): number => {
             return this.pagesInSets[this.pageSetCurrent][0];
        }
        getLastPageInPageSet = (): number => {
             return this.pagesInSets[this.pageSetCurrent][this.pagesInSets[this.pageSetCurrent].length-1];
        }
        // Navigate
        goToFirstPage = () => {
            this.currentChoice = 0;
            this.pageSetCurrent = 0;
        }
        goToPreviousPage = () => {
            if (this.currentChoice <= 0) {
                return;
            }
            this.currentChoice--;
            if (this.currentChoice < this.getFirstPageInPageSet()) {
                this.goToPreviousPageSet();
            }
        }
        goToNextPage = () => {
            if (this.currentChoice >= this.maxPage()) {
                return;
            }
            this.currentChoice++;
            if (this.currentChoice > this.getLastPageInPageSet()) {
                this.goToNextPageSet();
            }
        }
        goToLastPage = () => {
            this.pageSetCurrent = this.maxPageSet();
            this.goToLastPageInPageSet();
        }
        // Jumping Page Sets
        goToFirstPageInPageSet = () => {
            this.currentChoice = this.getFirstPageInPageSet();
        }
        goToLastPageInPageSet = () => {
            this.currentChoice = this.getLastPageInPageSet();
        }
        goToPreviousPageSet = () => {
            if (this.pageSetCurrent <= 0) {
                return;
            }
            this.pageSetCurrent--;
            this.goToLastPageInPageSet();
        }
        goToNextPageSet = () => {
            if (this.pageSetCurrent >= this.maxPageSet()) {
                return;
            }
            this.pageSetCurrent++;
            this.goToFirstPageInPageSet();
        }
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $filter: ng.IFilterService,
                private readonly $state: ng.ui.IStateService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.catalog.change, (event: ng.IAngularEvent, ...args: any[]) => {
                if (args[0] === "PageSize") {
                    this.resetCaches();
                }
            });
            const unbind2 = $scope.$watch(() => this.totalCount, (newValue, oldValue/*, scope*/) => {
                if (newValue !== oldValue) {
                    this.resetCaches();
                }
            });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            })
        }
    }

    cefApp.directive("cefSearchCatalogResultsPage", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { pageViewType: "@", defaultValue: "@", hideLabel: "@" },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/resultsPageSelector.html", "ui"),
        controller: SearchCatalogResultsPageController,
        controllerAs: "scrpCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
