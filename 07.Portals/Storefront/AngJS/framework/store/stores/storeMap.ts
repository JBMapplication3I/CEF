/**
 * @file framework/store/stores/storeMap.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Store Listings directive class
 */
module cef.store.stores {
    class StoreMapController extends core.TemplatedControllerBase {
        // Properties
        sports: api.FranchiseCategoryModel[] = [];
        selectedSport: api.FranchiseCategoryModel;
        stateName: string;
        states: any;
        selectedState: any;
        county: string;
        // Functions
        getRegionCode(): void {
            this.setRunning();
            this.cvApi.geography.GetRegions()
                .then((res) => {
                    this.states = res.data.Results;
                    console.log(res);
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
            this.$state.go("/Store-Listings", params);
        }
        getSports(): void {
            this.setRunning();
            this.cvApi.franchises.GetFranchiseCategories()
                .then((res) => {
                    if (!res.data || !res.data.Results || !res.data.Results.length) {
                        return;
                    }
                    this.sports = res.data.Results;
                    console.log(this.sports);
                })
        }
        // Events
        load() {
            this.getRegionCode();
            this.getSports();
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $state: ng.ui.IStateService,
                private readonly cvApi: api.ICEFAPI,
                private readonly $window: ng.IWindowService,
                private readonly $location: ng.ILocationService,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
            //console.log(this.$location.search("county"));

            console.log("STORE MAP DIRECTIVE");
            const { state, county } = this.$location.search();
            this.stateName = state;
            this.county = county;
            console.log(this.county);
            this.load();
        }
    }

    cefApp.directive("cefStoreMap", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/stores/storeMap.html", "ui"),
        controller: StoreMapController,
        controllerAs: "storeMapCtrl",
        bindToController: true
    }));
}
