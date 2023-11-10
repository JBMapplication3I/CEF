/**
 * @file framework/store/stores/storeListings.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Store Listings directive class
 */
module cef.store.stores {
    class StoreListingsController extends core.TemplatedControllerBase {
        // Filters
        numRows: number;
        page: number;
        stateName: string;
        activeTab: "All" | "Active" | "Available";


        // Properties
        franchise: api.FranchiseModel;
        franchiseStores: api.StoreModel[] = [];
        size: number;
        countyIDAndFranchise: any = {};
        searchTerm: string;
        state: api.StateModel;
        stores: api.StoreModel[];
        counties: api.DistrictModel[];
        filteredCounties: api.DistrictModel[];
        regionID: number;
        numPages: number;
        numRowsPerPage: number[] = [1, 3, 5, 7];
        currentPage: number = 1;
        // Functions
        getMinCountyIndex(): number {
            return (this.page - 1) * this.numRows;
        }
        getMaxCountyIndex(): number {
            return ((this.page - 1) * this.numRows) + this.numRows;
        }
        getStateAndCounties(): void {
            if (!this.stateName) {
                return;
            }
            this.setRunning();
            this.cvApi.geography.GetRegionByName({ Name: this.stateName })
                .then((res) => {
                    if (!res || !res.data) {
                        console.log(this.stateName + " region not found");
                        return;
                    }
                    this.state = res.data;
                    // TEST
                    // this.getFranchiseByRegion(this.state.ID);
                    this.getCountiesByRegion();
                    this.finishRunning();
                })
                .catch(reason => this.finishRunning(true, reason));
        }
        getStore(id: number): void {
            this.setRunning();
            this.cvApi.stores.GetStoreByID(id)
                .then((res) => {
                    if (!res || !res.data) {
                        console.log("Store could not be found");
                        return;
                    }
                    this.franchiseStores.push(res.data);
                    this.finishRunning();
                })
                .catch(err => this.finishRunning(true, err));
        }
        getStoresInCounty(id: number): void {
            if (!this.state || !this.state.ID) {
                return;
            }
            this.setRunning();
            this.cvApi.stores.GetStores({ DistrictID: id })
                .then((res) => {
                    if (!res
                        || !res.data
                        || !res.data.Results
                        || !res.data.Results.length) {
                        console.log("No stores found in this district");
                        return;
                    }
                    this.stores = res.data.Results;
                    this.finishRunning();
                    this.stores.forEach((store) => {
                        this.getStore(store.ID);
                    })
                })
        }
        getOperatedByCounty(county: api.DistrictModel): string {
            let organization = "-"
            if (!this.franchiseStores || !this.franchiseStores.length) {
                return organization
            }
            this.franchiseStores.forEach((store) => {
                const storeHasCounty = store.StoreDistricts.filter((district) => district.ID === county.ID).length > 0;
                if (storeHasCounty) {
                    organization = store.SerializableAttributes.Organization.Value;
                }
            })
            return organization;
        }
        getCountiesByRegion(): void {
            if (!this.state) {
                return;
            }
            this.setRunning();
            let dto: api.GetDistrictsDto = {
                RegionID: this.state.ID,
                Paging: <api.Paging>{
                    StartIndex: this.currentPage,
                    Size: this.size
                }
            };
            this.cvApi.geography.GetDistricts(dto)
                .then((res) => {
                    if (!res
                        || !res.data
                        || !res.data.Results
                        || !res.data.Results.length) {
                        console.log("No districts found in this region");
                        return;
                    }
                    this.counties = res.data.Results;
                    this.numPages = res.data.TotalPages;
                    this.finishRunning();
                    this.counties.forEach((county) => {
                        this.getFranchiseByDistrict(county.ID);
                    });
                    this.filterCounties();
                })
                .catch(reason => this.finishRunning(true, reason));
        }
        getFranchiseByDistrict(countyID: number): void {
            if (!countyID) {
                return;
            }
            this.setRunning();
            this.cvApi.franchises.GetFranchises({ StoreAnyDistrictID: countyID })
                .then((res) => {
                    if (!res
                        || !res.data
                        || !res.data.Results
                        || !res.data.Results.length) {
                        console.log("No franchise found for CountyID: " + countyID);
                        return;
                    }
                    this.countyIDAndFranchise[countyID] = res.data.Results[0].Name;
                    console.log(this.countyIDAndFranchise)
                    this.filterCounties();
                    this.finishRunning();
                })
                .catch(err => this.finishRunning(true, err));
        }
        getFranchiseByRegion(regionID: number): void {
            // Need district and region id to be included in this endpoint or make new endpoint so we dont have to
            // make separate calls. Too many calls.
            this.setRunning();
            this.cvApi.franchises.GetFranchises({ StoreRegionID: regionID })
                .then((res) => {
                    if (!res
                        || !res.data
                        || !res.data.Results
                        || !res.data.Results.length) {
                        console.log("No franchise found for RegionID: " + regionID);
                        return;
                    }
                    console.log("FranchiseByRegion: ", res.data.Results);
                    this.finishRunning();
                })
                .catch(err => {
                    console.error(err)
                    this.finishRunning(true, err)
                });
        }
        filtersSelected(tab: "All" | "Active" | "Available", page?: number): void {
            this.numPages = Math.ceil(this.counties.length / this.numRows);
            const isNotAlreadyOnPage = window.location.href.toLowerCase().indexOf(this.cefConfig.routes.catalog.root.toLowerCase()) === -1;
            const params = {
                state: this.stateName,
                countiesToShow: tab,
                size: this.numRows,
                page: page ? page : this.page
            };
            if (isNotAlreadyOnPage) {
                this.activeTab = tab ? tab : this.activeTab;
                this.size = this.numRows;
                this.page = page ? page : this.page;
                this.filterCounties();
                this.$filter("goToCORSLink")("/Store-Listings", "site", "primary", false, params);
                return;
            }
            this.$state.go("/Store-Listings", params);
        }
        filterCounties(): void {
            if (!this.counties) {
                return;
            }
            this.filteredCounties = this.counties.filter((county) => {
                return this.activeTab === "Active"
                    ? this.countyIDAndFranchise[county.ID] && this.countyIDAndFranchise[county.ID].length
                    : this.activeTab === "Available"
                        ? !this.countyIDAndFranchise[county.ID] || !this.countyIDAndFranchise[county.ID].length
                        : this.counties
            });
        }
        showCounty($index, countyID: number): boolean {
            if (!this.filteredCounties) {
                return false;
            }
            return this.filteredCounties.filter((county) => {
                return countyID === county.ID;
            }).length > 0
                && $index >= this.getMinCountyIndex()
                && $index < this.getMaxCountyIndex();
        }
        // Events
        load() {
            this.getStateAndCounties();
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
            let countiesToShow = this.$location.search().countiesToShow;
            let size = parseInt(this.$location.search().size);
            let page = parseInt(this.$location.search().page);
            this.stateName = this.$location.search().state;
            this.activeTab = countiesToShow ? countiesToShow : 'All';
            this.size = size ? size : this.numRowsPerPage[0];
            this.page = page ? page : 1;
            this.load();
        }
    }

    cefApp.directive("cefStoreListings", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/stores/storeListings.html", "ui"),
        controller: StoreListingsController,
        controllerAs: "storeListingsCtrl",
        bindToController: true
    }));
}
