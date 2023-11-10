module cef.store.stores {
    class StoreDirectoryAll extends core.TemplatedControllerBase {
        // Properties
        hasCompanyLogo = false;
        hasSalesRep = false;
        companyLogoFilename = "";
        salesRepFilename = "";

        seourl: string;
        stores: api.StoreModel[] = [];
        products: api.ProductModel[] = [];
        previewArray = [];
        user: api.UserModel;
        roleUser: api.RoleForUserModel[] = [];
        languages: api.CategoryModel[] = [];
        regions: api.RegionModel[] = [];
        countries: api.CountryModel[] = [];
        categories: api.CategoryModel[] = [];
        types: api.TypeModel[] = [];
        storeImages = [];
        byName = "(select By first)";
        findOptions: string[] = ["Sellers", "Dealers / Distributors", "Manufacturers", "Service Providers", "Suppliers", "Traders", "Products"];
        byOptions: string[] = ["Region", "Country", /*"Industry",*/ "Category"];
        tertiaryOptions = [];
        sellerTypeId: number = null;
        dealerTypeId: number = null;
        manufacturerTypeId: number = null;
        serviceproviderTypeId: number = null;
        lastRequestTime: number = 0;
        lastRequestTimeoutPromise = null;
        lastRequestPromise = null;  // the http request
        filter = {
            language: null,
            region: null,
            category: null,
            country: null,
            type: null,
            text: null,
            city: null,
            bySelectedOption: null,
            findSelectedOption: null,
            tertiaryOptionSelected: null,
        };
        // Constructor
        constructor(
                private readonly $timeout: ng.ITimeoutService,
                private readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvCountryService: services.ICountryService,
                private readonly cvRegionService: services.IRegionService) {
            super(cefConfig);
            this.load();
        }

        load(): void {
            this.getFilterValues();
            // this.getStores();
        }

        // TODO: I'm probably going to eventually move these two functions into filterStores() so that I can use closures for caching and fix timing issues
        doSomethingWithErrors = (errorMessage): void => {
            this.consoleLog(errorMessage);
        }

        doSomethingWithStores = (result: api.StorePagedResults): void => {
            this.previewArray = [];
            result.Results.forEach((store) => {
                this.previewArray.push({
                    Name: store.Name,
                    //LogoUrl: !store.LogoImageLibrary ? "" : store.LogoImageLibrary.SeoUrl,  // TODO: We can't test this until one of the stores actually has a logo associated
                    LogoUrl: "https://placeholdit.imgix.net/~text?txtsize=5&txt=30%C3%9730&w=30&h=30",
                    ExternalUrl: store.ExternalUrl,
                    // TODO: XXXXXXXXXXXXXXX The SeoUrl is currently not populated ?!?  Why not?  What do I need to do to get the values there?
                    SeoUrl: store.SeoUrl,
                });
            });
        }

        doSomethingWithProducts = (result: api.ProductPagedResults): void => {
            this.previewArray = [];
            result.Results.forEach((product) => {
                var logoUrl = "https://placeholdit.imgix.net/~text?txtsize=5&txt=30%C3%9730&w=30&h=30";
                if (product.PrimaryImageFileName) {
                    logoUrl = this.$filter("corsImageLink")(product.PrimaryImageFileName);
                }
                this.previewArray.push({
                    Name: product.Name,
                    LogoUrl: logoUrl,
                    SeoUrl: `Product/${product.SeoUrl}`,
                });
            });
        }

        filterStores = (): void => {
            // TODO: timing issues
            var currenttime = (new Date()).getTime();
            if (currenttime < 100 + this.lastRequestTime) {
                if (this.lastRequestTimeoutPromise != null) {
                    return; // already scheduled
                }
                this.lastRequestTimeoutPromise = this.$timeout(this.filterStores, 100 + currenttime - this.lastRequestTime );
                return;
            }
            if (this.lastRequestTimeoutPromise != null) {
                if (this.lastRequestTimeoutPromise.cancel) {
                    this.lastRequestTimeoutPromise.cancel();
                }
                this.lastRequestTimeoutPromise = null;
            }
            if (this.lastRequestPromise != null) {
                if (this.lastRequestPromise.cancel) {
                    this.lastRequestPromise.cancel();
                }
                this.lastRequestPromise = null;
            }
            this.lastRequestTime = currenttime;
            this.previewArray = []; // clear the preview
            const filterOptionsDto: api.GetStoresDto = { Active: true, AsListing: true, Paging: { Size: 9, StartIndex: 1 } };
            const productFilterOptionsDto: api.GetProductsDto = { Active: true, AsListing: true, Paging: { Size: 9, StartIndex: 1 } };
            switch (this.filter.bySelectedOption) {
                case "Region": {
                    ////filterOptionsDto.StoreContactRegionId = this.filter.tertiaryOptionSelected;
                    ////productFilterOptionsDto.StoreContactRegionID = this.filter.tertiaryOptionSelected;
                    filterOptionsDto.StoreContactRegionID = this.filter.tertiaryOptionSelected;
                    ////productFilterOptionsDto.StoreRegionID = this.filter.tertiaryOptionSelected;
                    break;
                }
                case "Country": {
                    filterOptionsDto.CountryID = this.filter.tertiaryOptionSelected;
                    ////productFilterOptionsDto.StoreCountryID = this.filter.tertiaryOptionSelected;
                    break;
                }
                case "Industry": {
                    break;
                }
                case "Category": {
                    ////filterOptionsDto.CategoryID = this.filter.tertiaryOptionSelected;
                    ////productFilterOptionsDto.StoreCategoryID = this.filter.tertiaryOptionSelected;
                    break;
                }
            }
            switch (this.filter.findSelectedOption) {
                case "Sellers": {
                    filterOptionsDto.TypeID = this.sellerTypeId;
                    this.lastRequestPromise = this.cvApi.stores.GetStores(filterOptionsDto).success(this.doSomethingWithStores).error(this.doSomethingWithErrors);
                    break;
                }
                case "Dealers / Distributors": {
                    filterOptionsDto.TypeID = this.dealerTypeId;
                    this.lastRequestPromise = this.cvApi.stores.GetStores(filterOptionsDto).success(this.doSomethingWithStores).error(this.doSomethingWithErrors);
                    break;
                }
                case "Manufacturers": {
                    filterOptionsDto.TypeID = this.manufacturerTypeId;
                    this.lastRequestPromise = this.cvApi.stores.GetStores(filterOptionsDto).success(this.doSomethingWithStores).error(this.doSomethingWithErrors);
                    break;
                }
                case "Products": {
                    //this.cvApi.product.GetProducts(productFilterOptionsDto).success(this.doSomethingWithProducts).error(this.doSomethingWithErrors);
                    //this.lastRequestPromise = this.cvApi.product.GetProducts(productFilterOptionsDto).success(this.doSomethingWithProducts).error(this.doSomethingWithErrors);
                    this.lastRequestPromise = this.cvApi.products.GetProducts(productFilterOptionsDto).success(this.doSomethingWithProducts).error(this.doSomethingWithErrors);
                    break;
                }
                case "Services": {
                    break;
                }
            }
        }

        getFilterValues = (): void => {
            this.cvRegionService.search({ Active: true, AsListing: true, Sorts: [{ field: "Name", order: 0, dir: "asc" }] }).then(r =>
                this.regions = r.length > 0
                    ? r.filter(region => (region.CustomKey ? (region.CustomKey.toLowerCase().indexOf("region") > -1) : null))
                    : null)
                .catch(reason => this.consoleLog(reason));
            this.cvCountryService.search({ Active: true, AsListing: true, Sorts: [{ field: "Name", order: 0, dir: "asc" }] }).then(c =>
                this.countries = c.length > 0
                    ? c.filter(country => (country.CustomKey ? (country.CustomKey.toLowerCase().indexOf("africa") > -1) : null))
                    : null)
                .catch(reason => this.consoleLog(reason));
            this.cvApi.categories.GetCategories({
                Active: true,
                AsListing: true,
                IncludeChildrenInResults: false
            }).then(categoriesResults => {
                this.categories = categoriesResults.data.Results;
                this.categories = this.categories.length > 0 ? this.categories.filter(category => (category.TypeID ? (category.TypeID === 1) : null)) : null;
                this.languages = categoriesResults.data.Results;
                this.languages = this.languages.length > 0 ? this.languages.filter(language => (language.TypeID ? (language.TypeID === 2) : null)) : null;
            }).catch(reason => this.consoleLog(reason));
            this.cvApi.stores.GetStoreTypes({ Active: true, AsListing: true }).success((storeTypeResult) => {
                this.types = storeTypeResult.Results;
                for (let i = 0; i < this.types.length; i++) {
                    if ("Seller" === this.types[i].Name) {
                        this.sellerTypeId = this.types[i].ID;
                    }
                    if ("Dealer / Distributor" === this.types[i].Name ) {
                        this.dealerTypeId = this.types[i].ID;
                    }
                    if ("Manufacturer" === this.types[i].Name ) {
                        this.manufacturerTypeId = this.types[i].ID;
                    }
                    if ("Service Provider" === this.types[i].Name ) {
                        this.serviceproviderTypeId = this.types[i].ID;
                    }
                    /* TODO ..............  Supplier  and then Trader....
                    if (this.types[i].Name = 'Seller') {
                        this.xxxxxxxxxxxx = this.types[i].ID;
                    }
                    if (this.types[i].Name = 'trader') {
                        this.xxxxxxxxxxxx = this.types[i].ID;
                    }
                    */
                }
            }).error((errorMessage) => { this.consoleLog(errorMessage) });
        }

        changeByOption = (): void => {
            var selected = this.filter.bySelectedOption;
            this.byName = selected;
            switch (selected) {
                case "Region": {
                    this.tertiaryOptions = this.regions;
                    break;
                }
                case "Country": {
                    this.tertiaryOptions = this.countries;
                    break;
                }
                case "Industry": {
                    this.tertiaryOptions = this.types;
                    break;
                }
                case "Category": {
                    this.tertiaryOptions = this.categories;
                    break;
                }
            }
        }
    }

    cefApp.directive("cefStoreDirectoryAll", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/stores/storeDirectoryAll.html", "ui"),
        controller: StoreDirectoryAll,
        controllerAs: "storeDirectoryAll"
    }));
}
