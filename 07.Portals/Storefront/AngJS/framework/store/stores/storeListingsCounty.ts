/**
 * @file framework/store/stores/storeListingsCounty.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Store Listings directive class
 */
 module cef.store.stores {
    interface IStoreListingsCounty {
        store: api.StoreModel;
        category: string;
    }

    class StoreListingsCountyController extends core.TemplatedControllerBase {
        // Filters
        selectedTeamFilter: string;
        selectedSportFilter: string;
        selectedCityFilter: string;
        selectedOrganizationFilter: string;
        selectedStatusFilter: string;
        sports: string[] = [];
        cities: string[] = [];
        organizations: string[] = [];
        statuses: string[] = ['Open', 'Closed'];
        orderBy: string = "Name";
        teamIsAsc: boolean = true;
        sportIsAsc: boolean = true;
        cityIsAsc: boolean = true;
        orgIsAsc: boolean = true;
        statusIsAsc: boolean = true;
        page: number;

        // URL Params
        stateName: string;
        countyName: string;
        paramTeam: string;
        paramSport: string;
        paramCity: string;
        paramOrganization: string;
        paramStatus: string;

        // Properties
        franchise: api.FranchiseModel;
        franchiseStores: api.StoreModel[] = [];
        stores: api.StoreModel[];
        sortedStores: api.StoreModel[] = [];
        state: api.StateModel;
        county: api.DistrictModel;
        users: api.UserModel[] = [];
        kind: string = "store";
        numPages: number;
        numRowsPerPage: number[] = [1, 3, 5, 7];
        // Functions
        fillFilters(store: api.StoreModel) {
            let organization = store.SerializableAttributes.Organization.Value;
            let status = store.SerializableAttributes.Status.Value;
            let city = store.Contact.Address.City;
            let categories = store.Categories;
            if (this.organizations.indexOf(organization) === -1) this.organizations.push(organization);
            if (this.statuses.indexOf(status) === -1) this.statuses.push(status);
            if (this.cities.indexOf(city) === -1) this.cities.push(city);
            categories.forEach((category) => {
                if (this.sports.indexOf(category.SlaveName) === -1) this.sports.push(category.SlaveName);
            });
        }
        /* changeOrderBy(filter: string) {
            this.orderBy = filter;
            this.sortStores();
            this.isAsc = !this.isAsc;
        } */
        /* sortStoresByFilter(store1: api.StoreModel, store2: api.StoreModel): number {
            console.log(this.isAsc);
            if (this.orderBy === "Sport") {
                return store1.Categories[0].SlaveName > store2.Categories[0].SlaveName ? 1 : -1;
            }
            if (this.orderBy === "City") {
                return store1.Contact.Address.City > store2.Contact.Address.City ? 1 : -1;
            }
            if (this.orderBy === "Organization") {
                return store1.SerializableAttributes.Organization.Value > store1.SerializableAttributes.Organization.Value ? 1 : -1;
            }
            if (this.orderBy === "Status") {
                return store1.SerializableAttributes.Status.Value > store1.SerializableAttributes.Status.Value ? 1 : -1;
            }
            return store1.Name > store2.Name ? 1 : -1;
        } */
        sortStores(sortBy?: string): void {
            if (!this.franchiseStores || !this.franchiseStores.length) {
                return;
            }
            switch (sortBy) {
                case "Team": {
                    if (this.franchiseStores.length === 1) { return; }
                    this.sortedStores = this.franchiseStores.sort((store1, store2): number => {
                        const team1 = store1.Name;
                        const team2 = store2.Name;
                        return this.teamIsAsc ? (team1 > team2 ? 1 : -1) : (team1 < team2 ? 1 : -1);
                    });
                    this.teamIsAsc = !this.teamIsAsc;
                    break;
                }
                case "Sport": {
                    if (this.franchiseStores.length === 1) { return; }
                    this.sortedStores = this.franchiseStores.sort((store1, store2): number => {
                        const sport1 = store1.Categories[0].SlaveName;
                        const sport2 = store2.Categories[0].SlaveName;
                        if (sport1 === sport2) {
                            return store1.Name > store2.Name ? 1 : -1;
                        }
                        return this.sportIsAsc ? (sport1 > sport2 ? 1 : -1) : (sport1 < sport2 ? 1 : -1);
                    });
                    this.sportIsAsc = !this.sportIsAsc;
                    break;
                }
                case "City": {
                    if (this.franchiseStores.length === 1) { return; }
                    this.sortedStores = this.franchiseStores.sort((store1, store2): number => {
                        const city1 = store1.Contact.Address.City;
                        const city2 = store2.Contact.Address.City;
                        if (city1 === city2) {
                            return store1.Name > store2.Name ? 1 : -1;
                        }
                        return this.cityIsAsc ? (city1 > city2 ? 1 : -1) : (city1 < city2 ? 1 : -1);
                    });
                    this.cityIsAsc = !this.cityIsAsc;
                    break;
                }
                case "Organization": {
                    if (this.franchiseStores.length === 1) { return; }
                    this.sortedStores = this.franchiseStores.sort((store1, store2): number => {
                        const org1 = store1.SerializableAttributes.Organization.Value;
                        const org2 = store2.SerializableAttributes.Organization.Value;
                        if (org1 === org2) {
                            return store1.Name > store2.Name ? 1 : -1;
                        }
                        return this.orgIsAsc ? (org1 > org2 ? 1 : -1) : (org1 < org2 ? 1 : -1);
                    });
                    this.orgIsAsc = !this.orgIsAsc;
                    break;
                };
                case "Status": {
                    if (this.franchiseStores.length === 1) { return; }
                    this.sortedStores = this.franchiseStores.sort((store1, store2): number => {
                        const status1 = store1.SerializableAttributes.Status.Value;
                        const status2 = store2.SerializableAttributes.Status.Value;
                        if (status1 === status2) {
                            return store1.Name > store2.Name ? 1 : -1;
                        }
                        return this.statusIsAsc ? (status1 > status2 ? 1 : -1) : (status1 < status2 ? 1 : -1);
                    });
                    this.statusIsAsc = !this.statusIsAsc;
                    break;
                }
                default: {
                    this.sortedStores = this.franchiseStores.sort((store1, store2): number => {
                        const status1 = store1.SerializableAttributes.Status.Value;
                        const status2 = store2.SerializableAttributes.Status.Value;
                        if (status1 === status2 && status1 === "Open") {
                            return store1.Name > store2.Name ? 1 : -1;
                        }
                        return status1 === "Open" ? -1 : 1;
                    });
                }
            }
        }
        getStores(promises: ng.IPromise<any>[]): void {
            this.setRunning();
            this.$q.all(promises).then(results => {
                if (!results || !results.length) { return; }
                results.forEach(r => {
                    if (!r || !r["data"]) { return; }
                    let store = r["data"];
                    let storeInDistrict = store.StoreDistricts.find(district => {
                        return district.SlaveID === this.county.ID;
                    })
                    if (storeInDistrict) {
                        this.franchiseStores.push(store);
                        this.fillFilters(store);
                    }
                })
                this.finishRunning();
                this.sortStores();
            })
            .catch(err => this.finishRunning(true, err));
        }
        getDistrict(): any {
            this.setRunning();
            this.cvApi.geography.GetDistrictByName({ Name: this.countyName })
                .then((res) => {
                    if (!res || !res.data) {
                        return;
                    }
                    this.county = res.data;
                    this.finishRunning();
                    this.getFranchiseByDistrict();
                })
                .catch(err => this.finishRunning(true, err));
        }
        getUser(id: number): void {
            this.setRunning();
            this.cvApi.contacts.GetUserByID(id)
                .then((res) => {
                    if (!res || !res.data) {
                        return;
                    }
                    this.users.push(res.data);
                    this.finishRunning();
                })
                .catch(err => this.finishRunning(true, err));
        }
        getRegionCode(): void {
            this.setRunning();
            this.cvApi.geography.GetRegions({ CustomKey: this.stateName })
                .then((res) => {
                    if (!res
                        || !res.data
                        || !res.data.Results
                        || !res.data.Results.length) {
                        return;
                    }
                    this.state = res.data.Results[0];
                    this.finishRunning();
                    this.getDistrict();
                })
                .catch(err => this.finishRunning(true, err));
        }
        getFranchise(id: number): void {
            this.setRunning();
            this.cvApi.franchises.GetFranchiseByID(id)
                .then((res) => {
                    if (!res || !res.data) {
                        return;
                    }
                    this.franchise = res.data;
                    let promises = [];
                    this.franchise.Stores.forEach((store) => {
                        promises.push(this.cvApi.stores.GetStoreByID(store.SlaveID))
                    });
                    this.getStores(promises)
                    this.franchise.Users.forEach((user) => {
                        this.getUser(user.SlaveID);
                    });
                    this.finishRunning();
                })
                .catch(err => this.finishRunning(true, err));
        }
        getFranchiseByDistrict(): void {
            if (!this.county || !this.county.ID) {
                return;
            }
            this.setRunning();
            this.cvApi.franchises.GetFranchises({ StoreAnyDistrictID: this.county.ID })
                .then((res) => {
                    if (!res
                        || !res.data
                        || !res.data.Results
                        || !res.data.Results.length) {
                        return;
                    }
                    this.getFranchise(res.data.Results[0].ID);
                    this.finishRunning();
                })
                .catch(err => this.finishRunning(true, err));
        }
        filtersSelected(): void {
            const isNotAlreadyOnPage = window.location.href.toLowerCase().indexOf(this.cefConfig.routes.catalog.root.toLowerCase()) === -1;
            const params = {
                state: this.stateName,
                county: this.countyName,
                team: this.selectedTeamFilter,
                sport: this.selectedSportFilter,
                city: this.selectedCityFilter,
                organization: this.selectedOrganizationFilter,
                status: this.selectedStatusFilter
            };
            if (isNotAlreadyOnPage) {
                this.$filter("goToCORSLink")("/Store-Listings", "site", "primary", false, params);
                return;
            }
            this.$state.go("/Store-Listings", params);
        }
        // Events
        load() {
            this.getRegionCode();
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $state: ng.ui.IStateService,
                private readonly cvApi: api.ICEFAPI,
                private readonly $window: ng.IWindowService,
                private readonly cvSearchCatalogService: services.SearchCatalogService,
                private readonly $location: ng.ILocationService,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
            this.stateName = this.$location.search().state;
            this.countyName = this.$location.search().county;
            this.paramTeam = this.$location.search().team;
            this.paramSport =this.$location.search().sport;
            this.paramCity = this.$location.search().city;
            this.paramOrganization = this.$location.search().organization;
            this.paramStatus = this.$location.search().status;
            let page = this.$location.search().page;
            this.page = page ? page : 1;
            console.log("paramTeam:", this.paramTeam);
            console.log("paramSport:", this.paramSport);
            console.log("paramCity:", this.paramCity);
            console.log("paramOrganization:", this.paramOrganization);
            console.log("paramStatus:", this.paramStatus);
            this.load();
        }
    }

    cefApp.directive("cefStoreListingsCounty", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/stores/storeListingsCounty.html", "ui"),
        controller: StoreListingsCountyController,
        controllerAs: "storeListingsCountyCtrl",
        bindToController: true
    }));
}
