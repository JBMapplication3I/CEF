/**
 * @file framework/store/searchCatalog/searchCatalog.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Search catalog class
 */
module cef.store.searchCatalog {
    class SearchCatalogController extends core.TemplatedControllerBase {
        // Properties
        currentStore: api.StoreModel;
        states: any;
        // <None>
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        getCurrentStore(): void {
            if (!this.$stateParams.storeId || !this.service.allStores || !this.service.allStores.length) { return; }
            for (let i = 0; i < this.service.allStores.length; i++) {
                if (this.service.allStores[i].ID === this.$stateParams.storeId) {
                    this.currentStore = this.service.allStores[i];
                    break;
                }
            }
        }

        getRegionCode(): void {
            this.setRunning();
            this.cvApi.geography.GetRegions({ Active: true })
                .then((res) => {
                    this.states = res.data.Results;
                })
                .catch(err => this.finishRunning(true, err));
        }

        stateSelected($event: any): void {
            const isNotAlreadyOnPage = window.location.href.toLowerCase().indexOf(this.cefConfig.routes.catalog.root.toLowerCase()) === -1;
            const params = {
                state: $event.target.attributes.title.nodeValue
            };
            if (isNotAlreadyOnPage) {
                this.$filter("goToCORSLink")("/Store-Listings", "site", "primary", false, params);
                return;
            }

            this.$filter("goToCORSLink")("/Store-Listings", "site", "primary", false, params);
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $state: ng.ui.IStateService,
                private readonly $stateParams: ng.ui.IStateParamsService, // Used by HTML
                private readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
            this.$rootScope.$on(this.cvServiceStrings.events.stores.ready, () => this.getCurrentStore());
        }
    }

    cefApp.directive("cefSearchCatalog", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/searchCatalog.html", "ui"),
        controller: SearchCatalogController,
        controllerAs: "searchCatalogCtrl",
        bindToController: true
    }));
}
