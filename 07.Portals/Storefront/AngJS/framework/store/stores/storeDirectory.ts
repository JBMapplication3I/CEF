module cef.store.stores {
    class StoreDirectory extends core.TemplatedControllerBase {
        transclude: boolean;
        templateUrl: string;

        hasCompanyLogo = false;
        hasSalesRep = false;
        companyLogoFilename = "";
        salesRepFilename = "";

        seourl: string;
        stores: api.StoreModel[] = [];
        user: api.UserModel;
        roleUser: api.RoleForUserModel[] = [];
        languages: api.CategoryModel[] = [];
        regions: api.RegionModel[] = [];
        countries: api.CountryModel[] = [];
        categories: api.CategoryModel[] = [];
        categoriesGrouped: api.CategoryModel[] = [];
        types: api.TypeModel[] = [];

        storesMaxCount: api.StoreModel[] = [];
        storeCount = 100;

        storeImages = [];
        filter = {
            language: null,
            region: null,
            category: null,
            country: null,
            type: null,
            text: null,
            city: null
        };

        load(): void {
            this.getFilterValues();
            this.getStores();
        }

        getCategoriesGrouped(): void {
            this.categoriesGrouped.forEach((el, index/*, array*/) => {
                if (typeof el.ParentID === "undefined" && el.ParentID === null) { return; }
                const parent = _.find(this.categories, cat => cat.ID === el.ParentID);
                this.categoriesGrouped[index].Parent = parent ? parent : null;
            });
        }

        getStores(): void {
            // Get Stores
            this.cvApi.stores.GetStores({ Active: true, AsListing: true }).then(r => {
                // Get Full Store Detail
                this.storesMaxCount = r.data.Results.slice(0, this.storeCount);
                _(this.storesMaxCount).forEach(store => {
                    if (!store || !store.ID || store.ID <= 0) { return; }
                    this.cvApi.stores.GetStoreByID(store.ID)
                        .then(r => this.stores.push(r.data))
                        .catch(result => this.consoleLog(result));
                });
            }).catch(result => this.consoleLog(result));
        }

        filterStores(): void {
            this.stores.forEach((value, index) => {
                // Set store active to false if doesn't pass all filter criteria - begin with active = true
                var store = this.stores[index];
                var filter = this.filter;
                store.Active = true;
                // Region
                if (store.Active && filter.region && filter.region !== "") { store.Active = _.findIndex(store.StoreContacts, item => { return item.Slave.Address.RegionID === filter.region }) > -1 }
                // Type
                if (store.Active && filter.type && filter.type !== "") { if (store.Type.ID !== filter.type) {store.Active = false; } }
                // Country
                if (store.Active && filter.country && filter.country !== "") { if (store.Contact.Address.Country.ID !== filter.country) { store.Active = false; } }
                // City
                if (store.Active && filter.city && filter.city !== undefined && filter.city !== "" && store.Contact.Address.City !== undefined) {
                    if (store.Contact.Address.City.toLowerCase().indexOf(filter.city.toLowerCase()) < 0) { store.Active = false; }
                }
                // Quick Finder
                if (store.Active && filter.text && filter.text !== undefined && filter.text !== "" && store.Name !== undefined) {
                    var active = false;
                    if (!active && store.Name !== undefined && (store.Name.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.About !== undefined && (store.About.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.Overview !== undefined && (store.Overview.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.Description !== undefined && (store.Description.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.Slogan !== undefined && (store.Slogan.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.MissionStatement !== undefined && (store.MissionStatement.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.Contact.Address.Street1 !== undefined && (store.Contact.Address.Street1.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.Contact.Address.Street2 !== undefined && (store.Contact.Address.Street2.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.Contact.Address.City !== undefined && (store.Contact.Address.City.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.Contact.Address.Country !== undefined && store.Contact.Address.Country.Name !== undefined && (store.Contact.Address.Country.Name.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.Contact.Address.Region !== undefined && store.Contact.Address.Region.Name !== undefined &&  (store.Contact.Address.Region.Name.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.Contact.FirstName !== undefined && (store.Contact.FirstName.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    if (!active && store.Contact.LastName !== undefined && (store.Contact.LastName.toLowerCase().indexOf(filter.text.toLowerCase()) >= 0)) { active = true }
                    store.Active = active;
                }
            });
        }

        getFilterValues(): void {
            this.cvRegionService.search({ Active: true, AsListing: true, Sorts: [{ field: "Name", order: 0, dir: "asc" }] }).then(r =>
                this.regions = r.length > 0
                    ? r.filter(el => (el.CustomKey ? (el.CustomKey.toLowerCase().indexOf("region") > -1) : null))
                    : null)
                .catch(reason => this.consoleLog(reason));
            this.cvCountryService.search({ Active: true, AsListing: true, Sorts: [{ field: "Name", order: 0, dir: "asc" }] }).then(c =>
                this.countries = c.length > 0
                    ? c.filter(el => (el.CustomKey ? (el.CustomKey.toLowerCase().indexOf("africa") > -1) : null))
                    : null)
                .catch(reason => this.consoleLog(reason));
            this.cvApi.categories.GetCategories({
                Active: true,
                AsListing: true,
                IncludeChildrenInResults: false
            }).then(r => {
                this.categories = r.data.Results;
                this.categories = this.categories.length > 0
                    ? this.categories.filter(el => (el.TypeID ? (el.TypeID === 1002) : null))
                    : null;
                this.categoriesGrouped = this.categories.length > 0
                    ? this.categories.filter(el => (el.TypeID ? (typeof el.ParentID !== "undefined") : null))
                    : null;
                this.getCategoriesGrouped();
                this.languages = r.data.Results;
                this.languages = this.languages.length > 0
                    ? this.languages.filter(el => (el.TypeID ? (el.TypeID === 2) : null))
                    : null;
            }).catch(reason => this.consoleLog(reason));
            this.cvApi.stores.GetStoreTypes({ Active: true, AsListing: true })
                .then(r => this.types = r.data.Results)
                .catch(reason => this.consoleLog(reason));
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvCountryService: services.ICountryService,
                private readonly cvRegionService: services.IRegionService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefStoreDirectory", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/stores/storeDirectory.html", "ui"),
        controller: StoreDirectory,
        controllerAs: "storeDirectory"
    }));
}
