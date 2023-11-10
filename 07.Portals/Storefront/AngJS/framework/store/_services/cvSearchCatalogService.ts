/**
 * @file framework/store/_services/cvSearchCatalogService.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Search catalog service class
 */
module cef.store.services {
    export interface ISearchCatalogBreadcrumb {
        category1Key: string;
        category1Name?: string;
        category1DisplayName?: string;
        category1SeoUrl?: string;
        category2Key: string;
        category2Name?: string;
        category2DisplayName?: string;
        category2SeoUrl?: string;
        category3Key: string;
        category3Name?: string;
        category3DisplayName?: string;
        category3SeoUrl?: string;
        category4Key: string;
        category4Name?: string;
        category4DisplayName?: string;
        category4SeoUrl?: string;
        category5Key: string;
        category5Name?: string;
        category5DisplayName?: string;
        category5SeoUrl?: string;
        category6Key: string;
        category6Name?: string;
        category6DisplayName?: string;
        category6SeoUrl?: string;
        category7Key: string;
        category7Name?: string;
        category7DisplayName?: string;
        category7SeoUrl?: string;
        search: boolean;
        compare: boolean;
        storeName?: string;
        midTitle?: string;
    }
    export interface IAnalyzeCategoryResultCallbacks {
        updateBreadcrumb: () => void;
        updateSeoUrl: () => void;
        goToOtherState: (params?) => void;
    }

    export class SearchCatalogService {
        consoleDebug(...args: any[]): void {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]): void {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        static readonly stateAu = "searchCatalog.auctions";
        static readonly stateCa = "searchCatalog.categories";
        static readonly stateFr = "searchCatalog.franchises";
        static readonly stateLo = "searchCatalog.lots";
        static readonly stateMa = "searchCatalog.manufacturers";
        static readonly statePr = "searchCatalog.products";
        static readonly stateSt = "searchCatalog.stores";
        static readonly stateVe = "searchCatalog.vendors";
        // Stored Data
        searchViewModelAu: api.AuctionSearchViewModel;
        searchViewModelCa: api.CategorySearchViewModel;
        searchViewModelFr: api.FranchiseSearchViewModel;
        searchViewModelLo: api.LotSearchViewModel;
        searchViewModelMa: api.ManufacturerSearchViewModel;
        searchViewModelPr: api.ProductSearchViewModel;
        searchViewModelSt: api.StoreSearchViewModel;
        searchViewModelVe: api.VendorSearchViewModel;
        get activeStateRoot(): string {
            return this.$state.includes(SearchCatalogService.stateAu)
                ? SearchCatalogService.stateAu
                : this.$state.includes(SearchCatalogService.stateCa)
                    ? SearchCatalogService.stateCa
                    : this.$state.includes(SearchCatalogService.stateFr)
                        ? SearchCatalogService.stateFr
                        : this.$state.includes(SearchCatalogService.stateLo)
                            ? SearchCatalogService.stateLo
                            : this.$state.includes(SearchCatalogService.stateMa)
                                ? SearchCatalogService.stateMa
                                : this.$state.includes(SearchCatalogService.stateSt)
                                    ? SearchCatalogService.stateSt
                                    : this.$state.includes(SearchCatalogService.stateVe)
                                        ? SearchCatalogService.stateVe
                                        : SearchCatalogService.statePr;
        }
        get activeSearchViewModel(): api.SearchViewModelBase<api.SearchFormBase, api.IndexableModelBase> {
            return this.$state.includes(SearchCatalogService.stateAu)
                ? this.searchViewModelAu
                : this.$state.includes(SearchCatalogService.stateCa)
                    ? this.searchViewModelCa
                    : this.$state.includes(SearchCatalogService.stateFr)
                        ? this.searchViewModelFr
                        : this.$state.includes(SearchCatalogService.stateLo)
                            ? this.searchViewModelLo
                            : this.$state.includes(SearchCatalogService.stateMa)
                                ? this.searchViewModelMa
                                : this.$state.includes(SearchCatalogService.stateSt)
                                    ? this.searchViewModelSt
                                    : this.$state.includes(SearchCatalogService.stateVe)
                                        ? this.searchViewModelVe
                                        : this.searchViewModelPr;
        }
        getSearchViewModel(kind: string): api.SearchViewModelBase<api.SearchFormBase, api.IndexableModelBase> {
            switch (kind) {
                case "auctions": { return this.searchViewModelAu; }
                case "categories": { return this.searchViewModelCa; }
                case "franchises": { return this.searchViewModelFr; }
                case "lots": { return this.searchViewModelLo; }
                case "manufacturers": { return this.searchViewModelMa; }
                case "products": { return this.searchViewModelPr; }
                case "stores": { return this.searchViewModelSt; }
                case "vendors": { return this.searchViewModelVe; }
                default: { return undefined; }
            }
        }
        setSearchViewModel(newModel: api.SearchViewModelBase<api.SearchFormBase, api.IndexableModelBase>, kind: string): void {
            switch (kind) {
                case "auctions": { this.searchViewModelAu = newModel as any; break; }
                case "categories": { this.searchViewModelCa = newModel as any; break; }
                case "franchises": { this.searchViewModelFr = newModel as any; break; }
                case "lots": { this.searchViewModelLo = newModel as any; break; }
                case "manufacturers": { this.searchViewModelMa = newModel as any; break; }
                case "products": { this.searchViewModelPr = newModel as any; break; }
                case "stores": { this.searchViewModelSt = newModel as any; break; }
                case "vendors": { this.searchViewModelVe = newModel as any; break; }
            }
        }
        lastUsedSearchFormAu: string;
        lastUsedSearchFormCa: string;
        lastUsedSearchFormFr: string;
        lastUsedSearchFormLo: string;
        lastUsedSearchFormMa: string;
        lastUsedSearchFormPr: string;
        lastUsedSearchFormSt: string;
        lastUsedSearchFormVe: string;
        setSearchForm(kind: string): void {
            const stateName = `searchCatalog.${kind}.results.both`;
            const params = this.convertFormToStateParams();
            // TODO: This logic should be moved into the goToCORSLink if not already there
            if (window.location.href.toLowerCase().indexOf(this.cefConfig.routes.catalog.root.toLowerCase()) === -1) {
                this.$filter("goToCORSLink")("SearchCatalogState:" + stateName, "catalog", "primary", false, params);
                return;
            }
            this.$state.go(stateName, params);
        }
        getLastUsedSearchViewModel(kind: string): string {
            switch (kind) {
                case "auctions": { return this.lastUsedSearchFormAu; }
                case "categories": { return this.lastUsedSearchFormCa; }
                case "franchises": { return this.lastUsedSearchFormFr; }
                case "lots": { return this.lastUsedSearchFormLo; }
                case "manufacturers": { return this.lastUsedSearchFormMa; }
                case "products": { return this.lastUsedSearchFormPr; }
                case "stores": { return this.lastUsedSearchFormSt; }
                case "vendors": { return this.lastUsedSearchFormVe; }
                default: { return undefined; }
            }
        }
        getServiceForKind(kind: string): any {
            switch (kind) {
                case "auctions": { return this.cvAuctionService; }
                case "categories": { return this.cvCategoryService; }
                case "franchises": { return this.cvFranchiseService; }
                case "lots": { return this.cvLotService; }
                case "manufacturers": { return this.cvManufacturerService; }
                case "products": { return this.cvProductService; }
                case "stores": { return this.cvStoreService; }
                case "vendors": { return this.cvVendorService; }
                default: { return undefined; }
            }
        }
        getCachedByAllCategories(id: number) {
            return _.find(this.allCategories, x => x.ID == id);
        }
        doCurrentRefreshPromise(): ng.IPromise<any> {
            return this.$state.includes(SearchCatalogService.stateAu)
                ? this.refreshPromiseAu()
                : this.$state.includes(SearchCatalogService.stateCa)
                    ? this.refreshPromiseCa()
                    : this.$state.includes(SearchCatalogService.stateFr)
                        ? this.refreshPromiseFr()
                        : this.$state.includes(SearchCatalogService.stateLo)
                            ? this.refreshPromiseLo()
                            : this.$state.includes(SearchCatalogService.stateMa)
                                ? this.refreshPromiseMa()
                                : this.$state.includes(SearchCatalogService.stateSt)
                                    ? this.refreshPromiseSt()
                                    : this.$state.includes(SearchCatalogService.stateVe)
                                        ? this.refreshPromiseVe()
                                        : this.refreshPromisePr();
        }
        _regions: api.RegionModel[] = null;
        selectedRegionName: string;
        setRegion(event, regionId): void {
            let selectedRegion = null;
            let regionCustomKey = null;
            if (event) {
                regionCustomKey = event.target.getAttribute("title");
            }
            if (!this._regions) {
                this.cvApi.geography.GetRegions().then(r => {
                    if (r && r.data) {
                        this._regions = r.data.Results;
                        selectedRegion = this._regions.filter(x => x.ID == regionId || x.CustomKey == regionCustomKey)[0];
                        this.selectedRegionName = selectedRegion.Name;
                        this.activeSearchViewModel.Form.RegionID = selectedRegion.ID;
                        this.setSearchForm("stores");
                    }
                });
            } else {
                selectedRegion = this._regions.filter(x => x.ID == regionId || x.CustomKey == regionCustomKey)[0];
                this.selectedRegionName = selectedRegion.Name;
                this.activeSearchViewModel.Form.RegionID = selectedRegion.ID;
                this.setSearchForm("stores");
            }
        }
        _suspendRefresh = false;
        get suspendRefresh(): boolean { return this._suspendRefresh; }
        set suspendRefresh(newValue: boolean) { this._suspendRefresh = newValue; }
        searchIsRunning = false;
        lastSearchFailed = false;
        routerIsRunning = false;
        selectedCategory: api.CategoryModel = <api.CategoryModel>null;
        forcedCategory: api.CategoryModel = <api.CategoryModel>null;
        catalogBannerImageFileName: string;
        /**
         * The query term as the user is typing it. Stored separately so the search only fires when the user is ready
         * (this value is copied to the form variable at that time). Note that as this value changes, Suggestions are
         * requested from the server.
         * @public
         */
        queryTerm: string = null;
        /**
         * The query city as the user is typing it. Stored separately so the search only fires when the user is ready
         * (this value is copied to the form variable at that time).
         * @public
         */
        queryCity: string = null;
        /**
         * The query name as the user is typing it. Stored separately so the search only fires when the user is ready
         * (this value is copied to the form variable at that time).
         * @public
         */
        queryName: string = null;
        /**
         * The type ID the user has selected via UI.
         * @public
         */
        filterByTypeID: number = null;
        /**
         * When not forcing a store ID onto the search by user's selected store (via cefConfig), the store ID the user
         * has selected via UI.
         * @public
         */
        filterByStoreID: number = null;
        /**
         * The list of suggestions returned from the server in relation to the @see {queryTerm}
         * @public
         */
        suggestions: Array<api.SuggestResultBase> = [];
        /**
         * When the service is actively requesting suggestions from the server, this will be true.
         * @public
         */
        suggestionsIsRunning = false;
        /**
         * The results per page choices, pulled from the current page format's assigned values.
         * @public
         */
        resultsPerPageChoices: Array<number>;
        /**
         * The results page format choices, pulled from the registered formats' list of names.
         * @public
         */
        resultsPageFormatChoices: Array<string>;
        /**
         * The results page format choices' icons, pulled from the registered formats' icon value.
         * @public
         */
        resultsPageFormatChoiceIcons: cefalt.store.Dictionary<string>;
        breadcrumb: ISearchCatalogBreadcrumb = {
            category1Key: null,
            category1Name: null,
            category1DisplayName: null,
            category1SeoUrl: null,
            category2Key: null,
            category2Name: null,
            category2DisplayName: null,
            category2SeoUrl: null,
            category3Key: null,
            category3Name: null,
            category3DisplayName: null,
            category3SeoUrl: null,
            category4Key: null,
            category4Name: null,
            category4DisplayName: null,
            category4SeoUrl: null,
            category5Key: null,
            category5Name: null,
            category5DisplayName: null,
            category5SeoUrl: null,
            category6Key: null,
            category6Name: null,
            category6DisplayName: null,
            category6SeoUrl: null,
            category7Key: null,
            category7Name: null,
            category7DisplayName: null,
            category7SeoUrl: null,
            search: false,
            compare: false,
            midTitle: null
        };
        allCategories: Array<api.CategoryModel>;
        allAttributes: Array<api.GeneralAttributeModel>;
        allStores: Array<api.StoreModel>;
        hasSelectedStore = false;
        userSelectedStore: api.StoreModel;
        firstRun: boolean;
        currentCategoryName: string;
        currentTypeName: string;
        getCategoriesDefer: { [form: string]: ng.IDeferred<IAnalyzeCategoryResultCallbacks> };
        // Functions
        private load(cefConfig: core.CefConfig) : void {
            this.firstRun = true;
            const unbindWatchAu = this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form, () => { if (this.searchViewModelAu && this.searchViewModelAu.Form && this.$state.includes(SearchCatalogService.stateAu)) { this.initializeWatchersAu(); unbindWatchAu(); } });
            const unbindWatchCa = this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form, () => { if (this.searchViewModelCa && this.searchViewModelCa.Form && this.$state.includes(SearchCatalogService.stateCa)) { this.initializeWatchersCa(); unbindWatchCa(); } });
            const unbindWatchFr = this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form, () => { if (this.searchViewModelFr && this.searchViewModelFr.Form && this.$state.includes(SearchCatalogService.stateFr)) { this.initializeWatchersFr(); unbindWatchFr(); } });
            const unbindWatchLo = this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form, () => { if (this.searchViewModelLo && this.searchViewModelLo.Form && this.$state.includes(SearchCatalogService.stateLo)) { this.initializeWatchersLo(); unbindWatchLo(); } });
            const unbindWatchMa = this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form, () => { if (this.searchViewModelMa && this.searchViewModelMa.Form && this.$state.includes(SearchCatalogService.stateMa)) { this.initializeWatchersMa(); unbindWatchMa(); } });
            const unbindWatchPr = this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form, () => { if (this.searchViewModelPr && this.searchViewModelPr.Form && this.$state.includes(SearchCatalogService.statePr)) { this.initializeWatchersPr(); unbindWatchPr(); } });
            const unbindWatchSt = this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form, () => { if (this.searchViewModelSt && this.searchViewModelSt.Form && this.$state.includes(SearchCatalogService.stateSt)) { this.initializeWatchersSt(); unbindWatchSt(); } });
            const unbindWatchVe = this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form, () => { if (this.searchViewModelVe && this.searchViewModelVe.Form && this.$state.includes(SearchCatalogService.stateVe)) { this.initializeWatchersVe(); unbindWatchVe(); } });
            const searchViewModelBase = <api.SearchViewModelBase<api.SearchFormBase, api.IndexableModelBase>>{
                Total: 0,
                TotalPages: 0,
                ServerError: null,
                DebugInformation: null,
                IsValid: false,
                Documents: [],
                ElapsedMilliseconds: 0,
                Form: <api.SearchFormBase>{
                    Page: 1,
                    PageSize: this.cefConfig.catalog.defaultPageSize,
                    PageSetSize: 5,
                    PageFormat: this.cefConfig.catalog.defaultFormat,
                    Sort: this.cefConfig.catalog.defaultSort as any
                },
                Attributes: null,
                BrandIDs: null,
                BrandNames: null,
                CategoriesTree: null,
                FranchiseIDs: null,
                ManufacturerIDs: null,
                RatingRanges: null,
                PricingRanges: null,
                ProductIDs: null,
                ResultIDs: null,
                StoreIDs: null,
                VendorIDs: null,
                Districts: null,
                Regions: null,
                Types: null
            };
            this.searchViewModelAu = angular.copy(searchViewModelBase, <api.AuctionSearchViewModel>{ }) as any;
            this.searchViewModelCa = angular.copy(searchViewModelBase, <api.CategorySearchViewModel>{ }) as any;
            this.searchViewModelLo = angular.copy(searchViewModelBase, <api.LotSearchViewModel>{ }) as any;
            this.searchViewModelMa = angular.copy(searchViewModelBase, <api.ManufacturerSearchViewModel>{ }) as any;
            this.searchViewModelPr = angular.copy(searchViewModelBase, <api.ProductSearchViewModel>{ }) as any;
            this.searchViewModelSt = angular.copy(searchViewModelBase, <api.StoreSearchViewModel>{ }) as any;
            this.searchViewModelVe = angular.copy(searchViewModelBase, <api.VendorSearchViewModel>{ }) as any;
            this.$rootScope.$on(this.cvServiceStrings.events.catalog.categoryTreeReady,
                ($event: ng.IAngularEvent, categoriesArray: api.CategoryModel[]) => {
                    if (!this.activeSearchViewModel
                        || !this.activeSearchViewModel.CategoriesTree
                        || !this.activeSearchViewModel.CategoriesTree.Children
                        || !this.activeSearchViewModel.CategoriesTree.Children.length
                        || !categoriesArray
                        || !categoriesArray.length) {
                        // Skip if no data on one side or the other
                        return;
                    }
                    this.activeSearchViewModel.CategoriesTree.Children = this.mapCategoryFields(
                        this.activeSearchViewModel.CategoriesTree.Children,
                        categoriesArray);
                    this.currentCategoryName = this.mapCurrentCategoryName(
                        this.activeSearchViewModel.Form.Category,
                        categoriesArray);
                    this.currentTypeName = this.mapTypeIDToTypeName(this.activeSearchViewModel.Form.TypeID);
                });
            this.$rootScope.$on(this.cvServiceStrings.events.stores.selectionUpdate,
                (/*event: ng.IAngularEvent, ...args: any[]*/) => {
                    ////this.consoleDebug(`selectionUpdate detected);
                    ////if (!this.suspendRefresh) {
                        this.doCurrentRefreshPromise();
                    ////}
                });
            this.$rootScope.$on(this.cvServiceStrings.events.catalog.change,
                (/*event: ng.IAngularEvent, ...args: any[]*/) => {
                    ////this.consoleDebug(`searchCatalogChange detected for property '${args[0]}' new value: [${args[1]}] old value: [${args[2]}]`);
                    if (!this.suspendRefresh) {
                        this.doCurrentRefreshPromise();
                    }
                });
            this.$rootScope.$on(this.cvServiceStrings.events.$state.notFound, () => this.routerIsRunning = false);
            this.$rootScope.$on(this.cvServiceStrings.events.$state.changeError, () => this.routerIsRunning = false);
            this.$rootScope.$on(this.cvServiceStrings.events.$state.changeStart,
                (
                    /*
                    event: ng.IAngularEvent,
                    toState: ng.ui.IState,
                    toParams: ng.ui.IStateParamsServiceForSearchCatalog,
                    fromState: ng.ui.IState,
                    fromParams: ng.ui.IStateParamsServiceForSearchCatalog,
                    options: ng.ui.IStateOptions
                    */
                ) => {
                    if (!this.isCatalogUrl(cefConfig)) {
                        //// Console.trace("State change started but not related to the search catalog because we are not non the correct page, ignoring (leaving action unaffected)");
                        return; // Not related to search catalog
                    }
                    /*
                    const existingParams = this.convertFormToStateParams();
                    const cleanToParams = this.cleanParams(toParams);
                    if (JSON.stringify(cleanToParams) === JSON.stringify(existingParams)) {
                        // Same data, ignore
                        //// Console.trace("$stateChangeStart: Didn't reload search catalog state because state change was detected but it was the same form data");
                        event.preventDefault();
                        return;
                    }
                    */
                    this.routerIsRunning = true;
                });
            this.$rootScope.$on(this.cvServiceStrings.events.$state.changeSuccess, (
                event: ng.IAngularEvent,
                toState: ng.ui.IState,
                toParams: ng.ui.IStateParamsServiceForSearchCatalog,
                fromState: ng.ui.IState,
                fromParams: ng.ui.IStateParamsServiceForSearchCatalog
            ) => {
                this.routerIsRunning = false;
                ////this.consoleDebug(this.cvServiceStrings.events.$state.changeSuccess);
                // Object literals that get passed through ui-router get wrapped in an array. Gotta unwrap them.
                ((props: string[]): void => {
                    props.forEach(prop => {
                        if (angular.isArray(toParams[prop]) && toParams[prop].length) {
                            toParams[prop] = toParams[prop][0];
                        }
                    });
                })(["attributesAll", "attributesAny", "categoriesAll", "categoriesAny", "storeIdsAll", "storeIdsAny", "typeIdsAny", "districtIdsAll", "districtIdsAny", "regionIdsAny"]);
                // Clean the results
                const existingParams = this.convertFormToStateParams();
                const cleanToParams = this.cleanParams(toParams);
                const cleanParamsString = angular.toJson(Object.keys(cleanToParams).sort().reduce((r, k) => (r[k] = cleanToParams[k], r), {}));
                const existingParamsString = angular.toJson(Object.keys(existingParams).sort().reduce((r, k) => (r[k] = existingParams[k], r), {}));
                if (cleanParamsString === existingParamsString && !this.firstRun) {
                    // Same data, ignore
                    //// Console.trace("$stateChangeSuccess: Didn't reload search catalog state because state change was detected but it was the same form data");
                    return;
                }
                ////this.consoleDebug(cleanToParams);
                ////this.consoleDebug(existingParams);
                ////this.consoleDebug(`$stateChangeSuccess: to______: ${cleanParamsString}`);
                ////this.consoleDebug(`$stateChangeSuccess: existing: ${existingParamsString}`);
                this.firstRun = false;
                if (this.searchIsRunning && this.routerIsRunning) {
                    // State change succeeded with different form params but search and router are both already running, preventing form change
                    ////this.consoleDebug("search and router are both already running, preventing form change");
                    event.preventDefault();
                    this.$state.transitionTo(fromState, fromParams, <ng.ui.IStateOptions>{ reload: false, notify: false });
                    return;
                } else if (this.searchIsRunning) {
                    // State change succeeded with different form params but search is already running, preventing form change
                    ////this.consoleDebug("search is already running, preventing form change");
                    event.preventDefault();
                    this.$state.transitionTo(fromState, fromParams, <ng.ui.IStateOptions>{ reload: false, notify: false });
                    return;
                } else if (this.routerIsRunning) {
                    // State change succeeded with different form params but router is already running, preventing form change
                    ////this.consoleDebug("router is already running, preventing form change");
                    event.preventDefault();
                    this.$state.transitionTo(fromState, fromParams, <ng.ui.IStateOptions>{ reload: false, notify: false });
                    return;
                }/* else {
                    // State change succeeded with different form params and neither search or router are running, letting the form change occur
                    this.consoleDebug("neither search or router are running, letting the form change occur");
                }*/
                //// Console.trace("$stateChangeSuccess: ============= applying state params to form because they are different (start) ===========");
                this.applyStateParamsToForm(cleanToParams, toState);
                //// Console.trace("$stateChangeSuccess: ============= applying state params to form because they are different (end) ===========");
                //// Console.trace("$stateChangeSuccess: ============= refreshing the data because we just applied the form data from the state params (start) ===========");
                if (this.cefConfig.featureSet.brands.enabled) {
                    if (angular.isDefined(this.$rootScope["globalBrandID"])) {
                        this.doCurrentRefreshPromise();
                        return;
                    }
                    let unbind1 = this.$rootScope.$on(this.cvServiceStrings.events.brands.globalBrandSiteDomainPopulated, () => this.doCurrentRefreshPromise());
                    this.$rootScope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                        if (angular.isFunction(unbind1)) { unbind1(); }
                    });
                    return;
                }
                this.doCurrentRefreshPromise();
                //// Console.trace("$stateChangeSuccess: ============= refreshing the data because we just applied the form data from the state params (end) ===========");
            });
            this.$rootScope.$on(this.cvServiceStrings.events.attributes.ready, (_$event: ng.IAngularEvent, allAttributes: api.GeneralAttributeModel[]) => {
                this.activeSearchViewModel.Attributes = this.mapAttributeFields(this.activeSearchViewModel.Attributes, allAttributes);
            });
            this.cvApi.attributes.GetGeneralAttributes({ Active: true, AsListing: true, HideFromStorefront: false }).then(r => {
                this.allAttributes = r.data.Results;
                this.$rootScope.$broadcast(this.cvServiceStrings.events.attributes.ready, this.allAttributes);
            });
            this.cvApi.stores.GetStores({ Active: true, AsListing: true }).then(r => {
                this.allStores = r.data.Results;
                this.$rootScope.$broadcast(this.cvServiceStrings.events.stores.ready, this.allStores);
            });
        }

        private isSeoCatalogUrl(): boolean {
            //// if (!this.cefConfig.catalog.extraRoots || !this.cefConfig.catalog.extraRoots.length) {
                return false;
            //// }
            //// const catalogUrls = [...this.cefConfig.catalog.extraRoots];
            //// const lowerCasePath = window.location.pathname.toLowerCase();
            //// for (let i = 0; i < catalogUrls.length; i++) {
            ////     if (lowerCasePath.startsWith(catalogUrls[i].toLowerCase())) {
            ////         return true;
            ////     }
            //// }
            //// return false;
        }

        private isCatalogUrl(cefConfig: core.CefConfig): boolean {
            const catalogUrls = [/*...cefConfig.catalog.extraRoots, "/searchcatalog", "/search-catalog",*/ cefConfig.routes.catalog.root];
            const lowerCasePath = window.location.pathname.toLowerCase();
            for (let i = 0; i < catalogUrls.length; i++) {
                if (lowerCasePath.startsWith(catalogUrls[i].toLowerCase())) {
                    return true;
                }
            }
            return false;
        }

        private mapCategoryFields(arr: api.AggregateTree[], categories: api.CategoryModel[]): api.AggregateTree[] {
            if (!arr) { return []; }
            return arr.map((cat) => {
                categories.forEach(category => {
                    if (cat.Key === (category.Name + "|" + category.CustomKey)) {
                        if (category.DisplayName) {
                            cat.DisplayName = category.DisplayName;
                        }
                        if (category.SortOrder) {
                            cat.SortOrder = category.SortOrder;
                        }
                        return true;
                    }
                    return false;
                });
                if (cat.Children) {
                    this.mapCategoryFields(cat.Children, categories);
                }
                return cat;
            });
        }

        private mapAttributeFields(
            arr,
            attributes: api.GeneralAttributeModel[]
        ): Array<api.KeyValuePair<string,Array<api.KeyValuePair<string,number>>>> {
            if (!arr) {
                return [];
            }
            return arr.map(attr => {
                attributes.forEach(attribute => {
                    if (attr.Key === attribute.CustomKey) {
                        if (attribute.SortOrder) {
                            attr.SortOrder = attribute.SortOrder;
                        }
                        if (attribute.DisplayName) {
                            attr.DisplayName = attribute.DisplayName;
                        }
                        return true;
                    }
                    return false;
                });
                return attr;
            });
        }

        private mapCurrentCategoryName(key: string, categories: api.CategoryModel[]): string {
            if (!key) { return null; }
            let name = "";
            categories.forEach(category => {
                if (key === (category.Name + "|" + category.CustomKey) && category.DisplayName) {
                    name = category.DisplayName;
                }
            });
            if (name === "") {
                name = this.toTitleCase(key.split("|")[0]
                    .replace(/\//, " / ")
                    .replace(/\//, " / ")
                    .replace(/  /, " ")
                    .replace(/  /, " ")
                    .replace(/  /, " "));
            }
            return name;
        }

        private convertFormToStateParams(): ng.ui.IStateParamsServiceForSearchCatalog {
            const existingParams = <ng.ui.IStateParamsServiceForSearchCatalog>{
                format: this.activeSearchViewModel.Form.PageFormat || this.cefConfig.catalog.defaultFormat,
                page: this.activeSearchViewModel.Form.Page || 1,
                size: this.activeSearchViewModel.Form.PageSize || this.cefConfig.catalog.defaultPageSize,
                sort: this.activeSearchViewModel.Form.Sort as any as string || this.cefConfig.catalog.defaultSort,
                term: this.activeSearchViewModel.Form.Query,
                //
                attributesAny: this.activeSearchViewModel.Form.AttributesAny,
                attributesAll: this.activeSearchViewModel.Form.AttributesAll,
                brandName: this.activeSearchViewModel.Form["BrandName"],
                category: this.activeSearchViewModel.Form.Category,
                categoriesAny: this.activeSearchViewModel.Form.CategoriesAny,
                categoriesAll: this.activeSearchViewModel.Form.CategoriesAll,
                city: this.queryCity,
                districtId: this.activeSearchViewModel.Form.DistrictID,
                districtIdsAny: this.activeSearchViewModel.Form.DistrictIDsAny,
                districtIdsAll: this.activeSearchViewModel.Form.DistrictIDsAll,
                filterByCurrentAccountRoles: this.activeSearchViewModel.Form["FilterByCurrentAccountRoles"],
                franchiseId: this.activeSearchViewModel.Form.FranchiseID,
                franchiseIdsAny: this.activeSearchViewModel.Form.FranchiseIDsAny,
                franchiseIdsAll: this.activeSearchViewModel.Form.FranchiseIDsAll,
                manufacturerId: this.activeSearchViewModel.Form.ManufacturerID,
                manufacturerIdsAny: this.activeSearchViewModel.Form.ManufacturerIDsAny,
                manufacturerIdsAll: this.activeSearchViewModel.Form.ManufacturerIDsAll,
                name: this.queryName,
                onHand: this.activeSearchViewModel.Form.OnHand,
                pricingRanges: this.activeSearchViewModel.Form.PricingRanges,
                productId: this.activeSearchViewModel.Form.ProductID,
                productIdsAny: this.activeSearchViewModel.Form.ProductIDsAny,
                productIdsAll: this.activeSearchViewModel.Form.ProductIDsAll,
                ratingRanges: this.activeSearchViewModel.Form.RatingRanges,
                regionId: this.activeSearchViewModel.Form.RegionID,
                regionIdsAny: this.activeSearchViewModel.Form.RegionIDsAny,
                storeId: this.activeSearchViewModel.Form.StoreID
                    ? this.activeSearchViewModel.Form.StoreID
                    : this.cefConfig.catalog.onlyApplyStoreToFilterByUI
                        ? this.filterByStoreID
                        : this.cvStoreLocationService.getUsersSelectedStore()
                            && this.cvStoreLocationService.getUsersSelectedStore().ID,
                storeIdsAny: this.activeSearchViewModel.Form.StoreIDsAny,
                storeIdsAll: this.activeSearchViewModel.Form.StoreIDsAll,
                typeId: this.activeSearchViewModel.Form.TypeID,
                typeIdsAny: this.activeSearchViewModel.Form.TypeIDsAny,
                vendorId: this.activeSearchViewModel.Form.VendorID,
                vendorIdsAny: this.activeSearchViewModel.Form.VendorIDsAny,
                vendorIdsAll: this.activeSearchViewModel.Form.VendorIDsAll
            };
            if (existingParams.category
                && existingParams.category.split("|")
                && existingParams.category.split("|")[0]) {
                this.getCategoryByKey(existingParams.category.split("|")[1]);
            }
            return this.cleanParams(existingParams);
        }

        private cleanForm(): void {
            this.suspendRefresh = true;
            if (angular.isDefined(this.activeSearchViewModel)) {
                this.cleanFormInner(this.activeSearchViewModel.Form);
            }
            this.suspendRefresh = false;
        }

        private cleanFormInner(form): any {
            if (angular.isUndefined(form)) { return form; }
            if (angular.isUndefined(form.PageSize)) { form.PageSize = this.cefConfig.catalog.defaultPageSize; }
            if (angular.isUndefined(form.Page)) { form.Page = 1; }
            if (angular.isUndefined(form.PageFormat)) { form.PageFormat = this.cefConfig.catalog.defaultFormat; }
            if (angular.isUndefined(form.Sort)) { form.Sort = this.cefConfig.catalog.defaultSort as any; }
            if (angular.isDefined(form["__type"])) { delete form["__type"]; }
            return form;
        }

        private cleanParams(params: ng.ui.IStateParamsServiceForSearchCatalog): ng.ui.IStateParamsServiceForSearchCatalog {
            if (!params.format) { params.format = this.cefConfig.catalog.defaultFormat; }
            if (!params.page) { params.page = 1; }
            if (!params.size) { params.size = this.cefConfig.catalog.defaultPageSize; }
            if (!params.sort) { params.sort = this.cefConfig.catalog.defaultSort; }
            if (!params.term) { delete params.term; }
            if (!params["parentCategory"]) { delete params["parentCategory"]; }
            if (!params.attributesAll || Object.keys(params.attributesAll).length <= 0) { delete params.attributesAll; }
            if (!params.attributesAny || Object.keys(params.attributesAny).length <= 0) { delete params.attributesAny; }
            if (!params.brandName) { delete params.brandName; }
            if (!params.category) { delete params.category; }
            if (!params.categoriesAny || params.categoriesAny.length <= 0) { delete params.categoriesAny; }
            if (!params.categoriesAll || params.categoriesAll.length <= 0) { delete params.categoriesAll; }
            if (!params.city) { delete params.city; }
            if (!params.districtId) { delete params.districtId; }
            if (!params.districtIdsAny || params.districtIdsAny.length <= 0) { delete params.districtIdsAny; }
            if (!params.districtIdsAll || params.districtIdsAll.length <= 0) { delete params.districtIdsAll; }
            if (!params.filterByCurrentAccountRoles) { delete params.filterByCurrentAccountRoles; }
            if (!params.franchiseId) { delete params.franchiseId; }
            if (!params.franchiseIdsAny || params.franchiseIdsAny.length <= 0) { delete params.franchiseIdsAny; }
            if (!params.franchiseIdsAll || params.franchiseIdsAll.length <= 0) { delete params.franchiseIdsAll; }
            if (!params.manufacturerId) { delete params.manufacturerId; }
            if (!params.manufacturerIdsAny || params.manufacturerIdsAny.length <= 0) { delete params.manufacturerIdsAny; }
            if (!params.manufacturerIdsAll || params.manufacturerIdsAll.length <= 0) { delete params.manufacturerIdsAll; }
            if (!params.name) { delete params.name; }
            if (!params.onHand) { delete params.onHand; }
            if (!params.pricingRanges || params.pricingRanges.length <= 0) { delete params.pricingRanges; }
            if (!params.productId) { delete params.productId; }
            if (!params.productIdsAny || params.productIdsAny.length <= 0) { delete params.productIdsAny; }
            if (!params.productIdsAll || params.productIdsAll.length <= 0) { delete params.productIdsAll; }
            if (!params.ratingRanges || params.ratingRanges.length <= 0) { delete params.ratingRanges; }
            if (!params.regionId) { delete params.regionId; }
            if (!params.regionIdsAny || params.regionIdsAny.length <= 0) { delete params.regionIdsAny; }
            if (!params.storeId) { delete params.storeId; }
            if (!params.storeIdsAny || params.storeIdsAny.length <= 0) { delete params.storeIdsAny; }
            if (!params.storeIdsAll || params.storeIdsAll.length <= 0) { delete params.storeIdsAll; }
            if (!params.typeId) { delete params.typeId; }
            if (!params.typeIdsAny || params.typeIdsAny.length <= 0) { delete params.typeIdsAny; }
            if (!params.vendorId) { delete params.vendorId; }
            if (!params.vendorIdsAny || params.vendorIdsAny.length <= 0) { delete params.vendorIdsAny; }
            if (!params.vendorIdsAll || params.vendorIdsAll.length <= 0) { delete params.vendorIdsAll; }
            return params;
        }

        private cleanCategoryTree(categoryTree: api.AggregateTree, allCategories: api.CategoryModel[]): api.AggregateTree {
            if (categoryTree
                && categoryTree.HasChildren
                && categoryTree.Children
                && categoryTree.Children.length) {
                categoryTree.Children = categoryTree.Children
                    .filter(c => this.allCategories
                        .filter(cm => cm.Name + "|" + cm.CustomKey === c.Key && cm.IsVisible && cm.IncludeInMenu).length);
                categoryTree.Children.forEach(c => this.cleanCategoryTree(c, allCategories));
            }
            return categoryTree;
        }

        private applyStateParamsToForm(toParams: ng.ui.IStateParamsServiceForSearchCatalog, toState: ng.ui.IState): void {
            this.suspendRefresh = true;
            // Read the state parameters and apply them to the form
            this.activeSearchViewModel.Form.PageFormat = toParams.format;
            this.activeSearchViewModel.Form.Page = toParams.page;
            this.activeSearchViewModel.Form.PageSize = toParams.size;
            this.activeSearchViewModel.Form.Sort = toParams.sort as any;
            if (toParams.term) { this.activeSearchViewModel.Form.Query = toParams.term; this.queryTerm = toParams.term; } else { delete this.activeSearchViewModel.Form.Query; this.queryTerm = null; }
            if (toParams.attributesAny && Object.keys(toParams.attributesAny).length > 0) { this.activeSearchViewModel.Form.AttributesAny = toParams.attributesAny; } else { delete this.activeSearchViewModel.Form.AttributesAny; }
            if (toParams.attributesAll && Object.keys(toParams.attributesAll).length > 0) { this.activeSearchViewModel.Form.AttributesAll = toParams.attributesAll; } else { delete this.activeSearchViewModel.Form.AttributesAll; }
            if (toParams.brandName) { this.activeSearchViewModel.Form["BrandName"] = toParams.brandName; } else { delete this.activeSearchViewModel.Form["BrandName"]; }
            if (toParams.category) { this.activeSearchViewModel.Form.Category = toParams.category; } else { delete this.activeSearchViewModel.Form.Category; }
            if (toParams.categoriesAny && toParams.categoriesAny.length > 0) { this.activeSearchViewModel.Form.CategoriesAny = toParams.categoriesAny; } else { delete this.activeSearchViewModel.Form.CategoriesAny; }
            if (toParams.categoriesAll && toParams.categoriesAll.length > 0) { this.activeSearchViewModel.Form.CategoriesAll = toParams.categoriesAll; } else { delete this.activeSearchViewModel.Form.CategoriesAll; }
            if (toParams.city) { this.activeSearchViewModel.Form["City"] = toParams.city; this.queryCity = toParams.city; } else { delete this.activeSearchViewModel.Form["City"]; this.queryCity = null; }
            if (toParams.districtId) { this.activeSearchViewModel.Form.DistrictID = toParams.districtId; } else { delete this.activeSearchViewModel.Form.DistrictID; }
            if (toParams.districtIdsAny && toParams.districtIdsAny.length > 0) { this.activeSearchViewModel.Form.DistrictIDsAny = toParams.districtIdsAny; } else { delete this.activeSearchViewModel.Form.DistrictIDsAny; }
            if (toParams.districtIdsAll && toParams.districtIdsAll.length > 0) { this.activeSearchViewModel.Form.DistrictIDsAll = toParams.districtIdsAll; } else { delete this.activeSearchViewModel.Form.DistrictIDsAll; }
            if (toParams.filterByCurrentAccountRoles) { this.activeSearchViewModel.Form["FilterByCurrentAccountRoles"] = toParams.filterByCurrentAccountRoles; } else { delete this.activeSearchViewModel.Form["FilterByCurrentAccountRoles"]; }
            if (toParams.manufacturerId) { this.activeSearchViewModel.Form.ManufacturerID = toParams.manufacturerId; }
            if (toParams.manufacturerIdsAny && toParams.manufacturerIdsAny.length > 0) { this.activeSearchViewModel.Form.ManufacturerIDsAny = toParams.manufacturerIdsAny; } else { delete this.activeSearchViewModel.Form.ManufacturerIDsAny; }
            if (toParams.manufacturerIdsAll && toParams.manufacturerIdsAll.length > 0) { this.activeSearchViewModel.Form.ManufacturerIDsAll = toParams.manufacturerIdsAll; } else { delete this.activeSearchViewModel.Form.ManufacturerIDsAll; }
            if (toParams.name) { this.activeSearchViewModel.Form["Name"] = toParams.name; this.queryName = toParams.name; } else { delete this.activeSearchViewModel.Form["Name"]; this.queryName = null; }
            if (toParams.onHand) { this.activeSearchViewModel.Form.OnHand = toParams.onHand; } else { delete this.activeSearchViewModel.Form.OnHand; }
            if (toParams.pricingRanges && toParams.pricingRanges.length > 0) { this.activeSearchViewModel.Form.PricingRanges = toParams.pricingRanges; } else { delete this.activeSearchViewModel.Form.PricingRanges; }
            if (toParams.productId) { this.activeSearchViewModel.Form.ProductID = toParams.productId; }
            if (toParams.productIdsAny && toParams.productIdsAny.length > 0) { this.activeSearchViewModel.Form.ProductIDsAny = toParams.productIdsAny; } else { delete this.activeSearchViewModel.Form.ProductIDsAny; }
            if (toParams.productIdsAll && toParams.productIdsAll.length > 0) { this.activeSearchViewModel.Form.ProductIDsAll = toParams.productIdsAll; } else { delete this.activeSearchViewModel.Form.ProductIDsAll; }
            if (toParams.ratingRanges && toParams.ratingRanges.length > 0) { this.activeSearchViewModel.Form.RatingRanges = toParams.ratingRanges; } else { delete this.activeSearchViewModel.Form.RatingRanges; }
            if (toParams.regionId) { this.activeSearchViewModel.Form.RegionID = toParams.regionId; this.setRegion(null, toParams.regionId); } else { delete this.activeSearchViewModel.Form.RegionID; }
            if (toParams.regionIdsAny && toParams.regionIdsAny.length > 0) { this.activeSearchViewModel.Form.RegionIDsAny = toParams.regionIdsAny; } else { delete this.activeSearchViewModel.Form.RegionIDsAny; }
            if (toParams.storeId) { this.activeSearchViewModel.Form.StoreID = toParams.storeId; }
            else if (!this.cefConfig.catalog.onlyApplyStoreToFilterByUI && this.cvStoreLocationService.getUsersSelectedStore()) { this.activeSearchViewModel.Form.StoreID = this.cvStoreLocationService.getUsersSelectedStore().ID; }
            else { delete this.activeSearchViewModel.Form.StoreID; }
            if (toParams.storeIdsAny && toParams.storeIdsAny.length > 0) { this.activeSearchViewModel.Form.StoreIDsAny = toParams.storeIdsAny; } else { delete this.activeSearchViewModel.Form.StoreIDsAny; }
            if (toParams.storeIdsAll && toParams.storeIdsAll.length > 0) { this.activeSearchViewModel.Form.StoreIDsAll = toParams.storeIdsAll; } else { delete this.activeSearchViewModel.Form.StoreIDsAll; }
            if (toParams.typeId) { this.activeSearchViewModel.Form.TypeID = toParams.typeId; } else { delete this.activeSearchViewModel.Form.TypeID; }
            if (toParams.typeIdsAny && toParams.typeIdsAny.length > 0) { this.activeSearchViewModel.Form.TypeIDsAny = toParams.typeIdsAny; } else { delete this.activeSearchViewModel.Form.TypeIDsAny; }
            if (toParams.vendorId) { this.activeSearchViewModel.Form.VendorID = toParams.vendorId; }
            if (toParams.vendorIdsAny && toParams.vendorIdsAny.length > 0) { this.activeSearchViewModel.Form.VendorIDsAny = toParams.vendorIdsAny; } else { delete this.activeSearchViewModel.Form.VendorIDsAny; }
            if (toParams.vendorIdsAll && toParams.vendorIdsAll.length > 0) { this.activeSearchViewModel.Form.VendorIDsAll = toParams.vendorIdsAll; } else { delete this.activeSearchViewModel.Form.VendorIDsAll; }
            this.suspendRefresh = false;
        }

        private initializeWatchersAu(): void {
            // Read the state parameters and apply them to the form
            this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current);
            // Watch for changes from this point forward
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.Sort,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Sort",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.Page,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Page",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.PageFormat,     (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageFormat",     newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.PageSize,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSize",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.PageSetSize,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSetSize",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.Query,          (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Query",          newValue, oldValue); this.queryTerm = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.Category,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Category",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.FranchiseID,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseID",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.ManufacturerID, (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerID", newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.StoreID,        (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreID",        newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.TypeID,         (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeID",         newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelAu && this.searchViewModelAu.Form.VendorID,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorID",       newValue, oldValue); } });
            /* Compared Arrays/Dictionaries are always "different" because of memory addresses even if the
             * contents are the same, so turn it into JSON strings and compare the strings instead, which
             * will match on "same contents" */
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.AttributesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.AttributesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.CategoriesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.CategoriesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.FranchiseIDsAny,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAny",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.FranchiseIDsAll,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAll",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.ManufacturerIDsAny, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAny", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.ManufacturerIDsAll, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAll", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.ProductIDsAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.ProductIDsAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.RatingRanges,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RatingRanges",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.StoreIDsAny,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAny",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.StoreIDsAll,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAll",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.TypeIDsAny,         (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeIDsAny",         newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.VendorIDsAny,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAny",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelAu.Form.VendorIDsAll,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAll",       newValue, oldValue); } });
            //
            this.suspendRefresh = false;
            // Start up the search using the state parameters that were loaded
            this.refreshPromiseAu();
        }

        private initializeWatchersCa(): void {
            // Read the state parameters and apply them to the form
            this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current);
            // Watch for changes from this point forward
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.Sort,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Sort",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.Page,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Page",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.PageFormat,     (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageFormat",     newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.PageSize,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSize",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.PageSetSize,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSetSize",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.Query,          (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Query",          newValue, oldValue); this.queryTerm = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.Category,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Category",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.FranchiseID,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseID",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.ManufacturerID, (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerID", newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.StoreID,        (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreID",        newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.TypeID,         (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeID",         newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelCa && this.searchViewModelCa.Form.VendorID,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorID",       newValue, oldValue); } });
            /* Compared Arrays/Dictionaries are always "different" because of memory addresses even if the
             * contents are the same, so turn it into JSON strings and compare the strings instead, which
             * will match on "same contents" */
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.AttributesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.AttributesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.CategoriesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.CategoriesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.FranchiseIDsAny,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAny",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.FranchiseIDsAll,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAll",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.ManufacturerIDsAny, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAny", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.ManufacturerIDsAll, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAll", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.ProductIDsAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.ProductIDsAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.RatingRanges,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RatingRanges",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.StoreIDsAny,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAny",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.StoreIDsAll,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAll",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.TypeIDsAny,         (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeIDsAny",         newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.VendorIDsAny,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAny",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelCa.Form.VendorIDsAll,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAll",       newValue, oldValue); } });
            //
            this.suspendRefresh = false;
            // Start up the search using the state parameters that were loaded
            this.refreshPromiseCa();
        }

        private initializeWatchersFr(): void {
            // Read the state parameters and apply them to the form
            this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current);
            // Watch for changes from this point forward
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.Sort,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Sort",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.Page,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Page",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.PageFormat,     (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageFormat",     newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.PageSize,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSize",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.PageSetSize,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSetSize",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.Query,          (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Query",          newValue, oldValue); this.queryTerm = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.Category,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Category",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form["City"],           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "City",           newValue, oldValue); this.queryCity = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.DistrictID,     (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "DistrictID",     newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.FranchiseID,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseID",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.ManufacturerID, (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerID", newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form["Name"],           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Name",           newValue, oldValue); this.queryName = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.RegionID,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RegionID",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.StoreID,        (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreID",        newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.TypeID,         (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeID",         newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelFr && this.searchViewModelFr.Form.VendorID,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorID",       newValue, oldValue); } });
            /* Compared Arrays/Dictionaries are always "different" because of memory addresses even if the
             * contents are the same, so turn it into JSON strings and compare the strings instead, which
             * will match on "same contents" */
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.AttributesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.AttributesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.CategoriesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.CategoriesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.DistrictIDsAny,     (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "DistrictIDsAny",     newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.DistrictIDsAll,     (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "DistrictIDsAll",     newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.FranchiseIDsAny,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAny",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.FranchiseIDsAll,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAll",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.ManufacturerIDsAny, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAny", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.ManufacturerIDsAll, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAll", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.ProductIDsAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.ProductIDsAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.RatingRanges,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RatingRanges",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.RegionIDsAny,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RegionIDsAny",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.StoreIDsAny,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAny",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.StoreIDsAll,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAll",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.TypeIDsAny,         (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeIDsAny",         newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.VendorIDsAny,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAny",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelFr.Form.VendorIDsAll,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAll",       newValue, oldValue); } });
            //
            this.suspendRefresh = false;
            // Start up the search using the state parameters that were loaded
            this.refreshPromiseFr();
        }

        private initializeWatchersLo(): void {
            // Read the state parameters and apply them to the form
            this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current);
            // Watch for changes from this point forward
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.Sort,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Sort",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.Page,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Page",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.PageFormat,     (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageFormat",     newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.PageSize,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSize",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.PageSetSize,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSetSize",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.Query,          (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Query",          newValue, oldValue); this.queryTerm = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.Category,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Category",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.FranchiseID,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseID",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.ManufacturerID, (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerID", newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.StoreID,        (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreID",        newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.TypeID,         (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeID",         newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelLo && this.searchViewModelLo.Form.VendorID,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorID",       newValue, oldValue); } });
            /* Compared Arrays/Dictionaries are always "different" because of memory addresses even if the
             * contents are the same, so turn it into JSON strings and compare the strings instead, which
             * will match on "same contents" */
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.AttributesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.AttributesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.CategoriesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.CategoriesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.FranchiseIDsAny,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAny",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.FranchiseIDsAll,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAll",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.ManufacturerIDsAny, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAny", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.ManufacturerIDsAll, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAll", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.ProductIDsAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.ProductIDsAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.RatingRanges,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RatingRanges",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.StoreIDsAny,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAny",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.StoreIDsAll,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAll",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.TypeIDsAny,         (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeIDsAny",         newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.VendorIDsAny,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAny",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelLo.Form.VendorIDsAll,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAll",       newValue, oldValue); } });
            //
            this.suspendRefresh = false;
            // Start up the search using the state parameters that were loaded
            this.refreshPromiseLo();
        }

        private initializeWatchersMa(): void {
            // Read the state parameters and apply them to the form
            this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current);
            // Watch for changes from this point forward
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.Sort,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Sort",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.Page,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Page",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.PageFormat,     (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageFormat",     newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.PageSize,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSize",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.PageSetSize,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSetSize",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.Query,          (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Query",          newValue, oldValue); this.queryTerm = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.Category,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Category",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.FranchiseID,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseID",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.ManufacturerID, (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerID", newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.StoreID,        (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreID",        newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.TypeID,         (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeID",         newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelMa && this.searchViewModelMa.Form.VendorID,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorID",       newValue, oldValue); } });
            /* Compared Arrays/Dictionaries are always "different" because of memory addresses even if the
             * contents are the same, so turn it into JSON strings and compare the strings instead, which
             * will match on "same contents" */
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.AttributesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.AttributesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.CategoriesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.CategoriesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.FranchiseIDsAny,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAny",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.FranchiseIDsAll,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAll",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.ManufacturerIDsAny, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAny", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.ManufacturerIDsAll, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAll", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.ProductIDsAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.ProductIDsAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.RatingRanges,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RatingRanges",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.StoreIDsAny,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAny",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.StoreIDsAll,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAll",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.TypeIDsAny,         (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeIDsAny",         newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.VendorIDsAny,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAny",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelMa.Form.VendorIDsAll,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAll",       newValue, oldValue); } });
            //
            this.suspendRefresh = false;
            // Start up the search using the state parameters that were loaded
            this.refreshPromiseMa();
        }

        private initializeWatchersPr(): void {
            // Read the state parameters and apply them to the form
            this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current);
            // Watch for changes from this point forward
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.Sort,                        (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Sort",                        newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.Page,                        (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Page",                        newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.PageFormat,                  (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageFormat",                  newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.PageSize,                    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSize",                    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.PageSetSize,                 (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSetSize",                 newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.Query,                       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Query",                       newValue, oldValue); this.queryTerm = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.Category,                    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Category",                    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.BrandName,                   (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "BrandName",                   newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.FilterByCurrentAccountRoles, (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FilterByCurrentAccountRoles", newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.FranchiseID,                 (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseID",                 newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.ManufacturerID,              (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerID",              newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.OnHand,                      (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "OnHand",                      newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.StoreID,                     (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreID",                     newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.TypeID,                      (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeID",                      newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelPr && this.searchViewModelPr.Form.VendorID,                    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorID",                    newValue, oldValue); } });
            this.$rootScope.$on(this.cvServiceStrings.events.stores.selectionUpdate, () => this.cvStoreLocationService.getUserSelectedStore().then(result => !this.cefConfig.catalog.onlyApplyStoreToFilterByUI && (this.searchViewModelPr.Form.StoreID = result.ID)));
            this.$rootScope.$on(this.cvServiceStrings.events.stores.cleared, () => !this.cefConfig.catalog.onlyApplyStoreToFilterByUI && (this.searchViewModelPr.Form.StoreID = null));
            /* Compared Arrays/Dictionaries are always "different" because of memory addresses even if the
             * contents are the same, so turn it into JSON strings and compare the strings instead, which
             * will match on "same contents" */
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.AttributesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.AttributesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.CategoriesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.CategoriesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.FranchiseIDsAny,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAny",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.FranchiseIDsAll,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAll",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.ManufacturerIDsAny, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAny", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.ManufacturerIDsAll, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAll", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.RatingRanges,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RatingRanges",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.PricingRanges,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PricingRanges",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.StoreIDsAny,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAny",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.StoreIDsAll,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAll",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.TypeIDsAny,         (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeIDsAny",         newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.VendorIDsAny,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAny",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelPr.Form.VendorIDsAll,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAll",       newValue, oldValue); } });
            //
            this.suspendRefresh = false;
            // Start up the search using the state parameters that were loaded
            if (this.isSeoCatalogUrl()) {
                this.getForcedCategoryPromise(window.location.pathname)
                    .finally(() => this.refreshPromisePr());
            } else {
                this.refreshPromisePr();
            }
        }

        private initializeWatchersSt(): void {
            // Read the state parameters and apply them to the form
            this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current);
            // Watch for changes from this point forward
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.Sort,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Sort",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.Page,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Page",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.PageFormat,     (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageFormat",     newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.PageSize,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSize",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.PageSetSize,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSetSize",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.Query,          (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Query",          newValue, oldValue); this.queryTerm = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.Category,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Category",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form["City"],           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "City",           newValue, oldValue); this.queryCity = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.DistrictID,     (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "DistrictID",     newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.FranchiseID,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseID",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.ManufacturerID, (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerID", newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form["Name"],           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Name",           newValue, oldValue); this.queryName = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.RegionID,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RegionID",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.TypeID,         (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeID",         newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelSt && this.searchViewModelSt.Form.VendorID,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorID",       newValue, oldValue); } });
            /* Compared Arrays/Dictionaries are always "different" because of memory addresses even if the
             * contents are the same, so turn it into JSON strings and compare the strings instead, which
             * will match on "same contents" */
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.AttributesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.AttributesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.CategoriesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.CategoriesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.DistrictIDsAny,     (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "DistrictIDsAny",     newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.DistrictIDsAll,     (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "DistrictIDsAll",     newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.FranchiseIDsAny,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAny",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.FranchiseIDsAll,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAll",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.ManufacturerIDsAny, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAny", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.ManufacturerIDsAll, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAll", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.ProductIDsAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.ProductIDsAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.RatingRanges,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RatingRanges",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.RegionIDsAny,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RegionIDsAny",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.TypeIDsAny,         (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeIDsAny",         newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.VendorIDsAny,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAny",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelSt.Form.VendorIDsAll,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "VendorIDsAll",       newValue, oldValue); } });
            //
            this.suspendRefresh = false;
            // Start up the search using the state parameters that were loaded
            this.refreshPromiseSt();
        }

        private initializeWatchersVe(): void {
            // Read the state parameters and apply them to the form
            this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current);
            // Watch for changes from this point forward
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.Sort,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Sort",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.Page,           (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Page",           newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.PageFormat,     (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageFormat",     newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.PageSize,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSize",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.PageSetSize,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "PageSetSize",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.Query,          (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Query",          newValue, oldValue); this.queryTerm = newValue; } });
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.Category,       (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "Category",       newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.FranchiseID,    (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseID",    newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.ManufacturerID, (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerID", newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.StoreID,        (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreID",        newValue, oldValue); } });
            this.$rootScope.$watch(() => this.searchViewModelVe && this.searchViewModelVe.Form.TypeID,         (newValue, oldValue) => { if (newValue !== oldValue) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeID",         newValue, oldValue); } });
            /* Compared Arrays/Dictionaries are always "different" because of memory addresses even if the
             * contents are the same, so turn it into JSON strings and compare the strings instead, which
             * will match on "same contents" */
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.AttributesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.AttributesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "AttributesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.CategoriesAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.CategoriesAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "CategoriesAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.FranchiseIDsAny,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAny",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.FranchiseIDsAll,    (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "FranchiseIDsAll",    newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.ManufacturerIDsAny, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAny", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.ManufacturerIDsAll, (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ManufacturerIDsAll", newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.ProductIDsAny,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAny",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.ProductIDsAll,      (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "ProductIDsAll",      newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.RatingRanges,       (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "RatingRanges",       newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.StoreIDsAny,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAny",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.StoreIDsAll,        (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "StoreIDsAll",        newValue, oldValue); } });
            this.$rootScope.$watchCollection(() => this.searchViewModelVe.Form.TypeIDsAny,         (newValue, oldValue) => { if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) { this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "TypeIDsAny",         newValue, oldValue); } });
            //
            this.suspendRefresh = false;
            // Start up the search using the state parameters that were loaded
            this.refreshPromiseVe();
        }

        private constructSeoUrl(): string {
            let constructed = "";
            if (!this.breadcrumb.category1SeoUrl) { return constructed; }
            constructed += `/${this.breadcrumb.category1SeoUrl}`;
            if (!this.breadcrumb.category2SeoUrl) { return constructed; }
            constructed += `/${this.breadcrumb.category2SeoUrl}`;
            if (!this.breadcrumb.category3SeoUrl) { return constructed; }
            constructed += `/${this.breadcrumb.category3SeoUrl}`;
            if (!this.breadcrumb.category4SeoUrl) { return constructed; }
            constructed += `/${this.breadcrumb.category4SeoUrl}`;
            if (!this.breadcrumb.category5SeoUrl) { return constructed; }
            constructed += `/${this.breadcrumb.category5SeoUrl}`;
            if (!this.breadcrumb.category6SeoUrl) { return constructed; }
            constructed += `/${this.breadcrumb.category6SeoUrl}`;
            if (!this.breadcrumb.category7SeoUrl) { return constructed; }
            constructed += `/${this.breadcrumb.category7SeoUrl}`;
            return constructed;
        }

        private constructTitleFromBreadcrumb(): ng.IPromise<string> {
            return this.$translate("ui.storefront.product.catalog.productCatalog.Catalog").then(r => {
                let constructed = this.cefConfig.companyName + " > " + r;
                if (!this.breadcrumb.category1Name) { return constructed; }
                if (this.breadcrumb.category1DisplayName) {
                    constructed += ` > ${this.breadcrumb.category1DisplayName}`;
                } else {
                    constructed += ` > ${this.toTitleCase(this.breadcrumb.category1Name.split("|")[0])}`;
                }
                if (!this.breadcrumb.category2Name) { return constructed; }
                if (this.breadcrumb.category2DisplayName) {
                    constructed += ` > ${this.breadcrumb.category2DisplayName}`;
                } else {
                    constructed += ` > ${this.toTitleCase(this.breadcrumb.category2Name.split("|")[0])}`;
                }
                if (!this.breadcrumb.category3Name) { return constructed; }
                if (this.breadcrumb.category3DisplayName) {
                    constructed += ` > ${this.breadcrumb.category3DisplayName}`;
                } else {
                    constructed += ` > ${this.toTitleCase(this.breadcrumb.category3Name.split("|")[0])}`;
                }
                if (!this.breadcrumb.category4Name) { return constructed; }
                if (this.breadcrumb.category4DisplayName) {
                    constructed += ` > ${this.breadcrumb.category4DisplayName}`;
                } else {
                    constructed += ` > ${this.toTitleCase(this.breadcrumb.category4Name.split("|")[0])}`;
                }
                if (!this.breadcrumb.category5Name) { return constructed; }
                if (this.breadcrumb.category5DisplayName) {
                    constructed += ` > ${this.breadcrumb.category5DisplayName}`;
                } else {
                    constructed += ` > ${this.toTitleCase(this.breadcrumb.category5Name.split("|")[0])}`;
                }
                if (!this.breadcrumb.category6Name) { return constructed; }
                if (this.breadcrumb.category6DisplayName) {
                    constructed += ` > ${this.breadcrumb.category6DisplayName}`;
                } else {
                    constructed += ` > ${this.toTitleCase(this.breadcrumb.category6Name.split("|")[0])}`;
                }
                if (!this.breadcrumb.category7Name) { return constructed; }
                if (this.breadcrumb.category7DisplayName) {
                    constructed += ` > ${this.breadcrumb.category7DisplayName}`;
                } else {
                    constructed += ` > ${this.toTitleCase(this.breadcrumb.category7Name.split("|")[0])}`;
                }
                return constructed;
            });
        }

        private toTitleCase(source: string): string {
            return source.replace(/\w*/g, txt => txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase());
        }

        updateSeoUrlAu(): void { this.updateSeoUrlInner(SearchCatalogService.stateAu, this.searchViewModelAu, "Catalog"  ); }
        updateSeoUrlCa(): void { this.updateSeoUrlInner(SearchCatalogService.stateCa, this.searchViewModelCa, "Directory"); }
        updateSeoUrlFr(): void { this.updateSeoUrlInner(SearchCatalogService.stateFr, this.searchViewModelFr, "Directory"); }
        updateSeoUrlLo(): void { this.updateSeoUrlInner(SearchCatalogService.stateLo, this.searchViewModelLo, "Catalog"  ); }
        updateSeoUrlMa(): void { this.updateSeoUrlInner(SearchCatalogService.stateMa, this.searchViewModelMa, "Directory"); }
        updateSeoUrlPr(): void { this.updateSeoUrlInner(SearchCatalogService.statePr, this.searchViewModelPr, "Catalog"  ); }
        updateSeoUrlSt(): void { this.updateSeoUrlInner(SearchCatalogService.stateSt, this.searchViewModelSt, "Directory"); }
        updateSeoUrlVe(): void { this.updateSeoUrlInner(SearchCatalogService.stateVe, this.searchViewModelVe, "Directory"); }

        private updateSeoUrlInner(stateName: string, searchViewModel: api.SearchViewModelBase<api.SearchFormBase, api.IndexableModelBase>, midTitle: string = "Catalog"): void {
            if (!this.$state.includes(stateName + ".results")) {
                // Don't do anything
                return;
            }
            this.breadcrumb.midTitle = midTitle;
            if (!searchViewModel.Form.Query && !searchViewModel.Form.Category) {
                if (window.document.title !== `${this.cefConfig.companyName} > ${midTitle}`) {
                    // Push/replace state can't set title in several browsers, have to set it manually
                    this.$window.document.title = `${this.cefConfig.companyName} > ${midTitle}`;
                }
                return;
            }
            if (searchViewModel.Form.Query && !searchViewModel.Form.Category) {
                // Don't do anything
                const searchTitle = `${this.cefConfig.companyName} > ${midTitle} > Search: "${searchViewModel.Form.Query}"`;
                if (window.document.title !== searchTitle) {
                    // Push/replace state can't set title in several browsers, have to set it manually
                    this.$window.document.title = searchTitle;
                }
                return;
            }
            if (searchViewModel.Form.Query && searchViewModel.Form.Category) {
                // Don't do anything
                this.constructTitleFromBreadcrumb().then(r => {
                    const searchTitle = `${r} > ${midTitle} > Search: "${searchViewModel.Form.Query}"`;
                    if (window.document.title !== searchTitle) {
                        // Push/replace state can't set title in several browsers, have to set it manually
                        this.$window.document.title = searchTitle;
                    }
                });
                return;
            }
            this.constructTitleFromBreadcrumb().then(r => {
                if (window.document.title !== r) {
                    // Push/replace state can't set title in several browsers, have to set it manually
                    this.$window.document.title = r;
                }
            });
        }

        getCategoryBreadcrumbName($event: any, category: number): string {
            if (category === undefined && typeof $event === "number") {
                category = $event
            };
            if (this.breadcrumb["category" + category.toString() + "DisplayName"]) {
                return this.breadcrumb["category" + category.toString() + "DisplayName"];
            }
            return this.toTitleCase(this.breadcrumb["category" + category.toString() + "Name"]);
        }

        getCategoryLevelLimitByKind(kind: string): number {
            return kind === "manufacturers" || kind === "vendors"
                ? 0
                : this.cefConfig.catalog.showCategoriesForLevelsUpTo;
        }
        getCategories(): ng.IPromise<api.CategoryPagedResults> {
            const pageSize = 10;
            let page = 1;
            let result: {Results: api.CategoryModel[]}= {Results: []};
            let totalCount = 0;
            // Do first promise, to get totalcount
            return this.$q((resolve, reject) => {
                this.cvApi.categories.GetCategories({
                    Active: true,
                    AsListing: true,
                    Paging: {
                        Size: pageSize,
                        StartIndex: page,
                    },
                    IncludeInMenu: true,
                    IncludeChildrenInResults: true,
                }).then(res => {
                    if (!res.data?.TotalCount) {
                        reject();
                    }
                    totalCount = res.data.TotalCount;
                    totalCount -= pageSize;
                    result.Results.push(...res.data.Results);
                }).then(() => {
                    let promises: ng.IHttpPromise<api.CategoryPagedResults>[] = [];
                    while (totalCount > 0) {
                        promises.push(this.cvApi.categories.GetCategories({
                            Active: true,
                            AsListing: true,
                            Paging: {
                                Size: pageSize,
                                StartIndex: ++page,
                            },
                            IncludeInMenu: true,
                            IncludeChildrenInResults: true
                        }))
                        totalCount -= pageSize;
                    }
                    this.$q.all(promises).then(results => {
                        const toReturn = results.reduce((accu: {Results: api.CategoryModel[]}, each: ng.IHttpPromiseCallbackArg<api.CategoryPagedResults>) => {
                            accu.Results = [...accu.Results, ...each.data.Results]
                            return accu;
                        }, result);
                        resolve(toReturn);
                    })
                });
            })
        }
        analyzeSingleCategoryPromise(kind: string, form: api.SearchFormBase, fromGetCategoriesDefer = false): ng.IPromise<IAnalyzeCategoryResultCallbacks> {
            const formAsJson = `${kind}|${angular.toJson(form)}`;
            if (!this.getCategoriesDefer) {
                this.getCategoriesDefer = {};
            }
            if (!fromGetCategoriesDefer
                && this.getCategoriesDefer[formAsJson]
                && this.getCategoriesDefer[formAsJson].promise) {
                return this.getCategoriesDefer[formAsJson].promise;
            }
            if (!this.allCategories || this.allCategories.length <= 0) {
                // Categories must be present to set up SeoUrls, call the data and then try this again
                this.getCategoriesDefer[formAsJson] = this.$q.defer<IAnalyzeCategoryResultCallbacks>();
                this.getCategories().then(r => {
                    if (!r || !r.Results || !r.Results.length) {
                        this.getCategoriesDefer[formAsJson].reject("No categories returned from server");
                        delete this.getCategoriesDefer[formAsJson];
                        return;
                    }
                    this.allCategories = r.Results;
                    if (this.getCategoriesDefer[formAsJson]) {
                        this.getCategoriesDefer[formAsJson].resolve(this.analyzeSingleCategoryPromise(kind, form, true));
                        delete this.getCategoriesDefer[formAsJson];
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.categoryTreeReady, this.allCategories);
                });
                return this.getCategoriesDefer[formAsJson].promise;
            }
            // Determine the depth of the currently selected category, this will tell us which category view to show, if any
            if (form.Query) {
                // We have a search term, which means we have to search all categories
                return this.$q.resolve(<IAnalyzeCategoryResultCallbacks>{
                    updateBreadcrumb: (): void => {
                        this.breadcrumb = {
                            category1Key: null,
                            category2Key: null,
                            category3Key: null,
                            category4Key: null,
                            category5Key: null,
                            category6Key: null,
                            category7Key: null,
                            search: true,
                            compare: false,
                            storeName: this.cvStoreLocationService.getUsersSelectedStore()
                                && this.cvStoreLocationService.getUsersSelectedStore().Name
                        };
                    },
                    updateSeoUrl: (): void => {
                        switch (kind) {
                            case "auctions": { this.updateSeoUrlAu(); break; }
                            case "categories": { this.updateSeoUrlCa(); break; }
                            case "franchises": { this.updateSeoUrlFr(); break; }
                            case "manufacturers": { this.updateSeoUrlMa(); break; }
                            case "stores": { this.updateSeoUrlSt(); break; }
                            case "vendors": { this.updateSeoUrlVe(); break; }
                            default: { this.updateSeoUrlPr(); break; }
                        }
                    },
                    goToOtherState: (params): void => {
                        if (!this.$state.is(`searchCatalog.${kind}.results.both`, params)) {
                            this.$state.transitionTo(`searchCatalog.${kind}.results.both`, params, true);
                        }
                    }
                });
            }
            if (!form["Category"]) {
                // No (valid) category selected, show top level
                return this.$q.resolve(<IAnalyzeCategoryResultCallbacks>{
                    updateBreadcrumb: (): void => {
                        this.breadcrumb = {
                            category1Key: null,
                            category2Key: null,
                            category3Key: null,
                            category4Key: null,
                            category5Key: null,
                            category6Key: null,
                            category7Key: null,
                            search: false,
                            compare: false,
                            storeName: this.cvStoreLocationService.getUsersSelectedStore()
                                && this.cvStoreLocationService.getUsersSelectedStore().Name
                        };
                    },
                    updateSeoUrl: (): void => {
                        switch (kind) {
                            case "auctions": { this.updateSeoUrlAu(); break; }
                            case "categories": { this.updateSeoUrlCa(); break; }
                            case "franchises": { this.updateSeoUrlFr(); break; }
                            case "manufacturers": { this.updateSeoUrlMa(); break; }
                            case "stores": { this.updateSeoUrlSt(); break; }
                            case "vendors": { this.updateSeoUrlVe(); break; }
                            default: { this.updateSeoUrlPr(); break; }
                        }
                    },
                    goToOtherState: (params): void => {
                        if (this.getCategoryLevelLimitByKind(kind) >= 1) {
                            if (!this.$state.is(`searchCatalog.${kind}.categories.level1`)) {
                                this.$state.transitionTo(`searchCatalog.${kind}.categories.level1`, null, true);
                            }
                        } else {
                            if (!this.$state.is(`searchCatalog.${kind}.results.both`, params)) {
                                this.$state.transitionTo(`searchCatalog.${kind}.results.both`, params, true);
                            }
                        }
                    }
                });
            }
            const tempCatFulls: Array<api.CategoryModel> = [];
            let tempCatFull = _.find(this.allCategories,
                (x: api.CategoryModel) => x.CustomKey === form["Category"]
                    || x.Name + "|" + x.CustomKey === form["Category"]);
            if (tempCatFull) {
                tempCatFulls.push(tempCatFull);
            }
            do {
                try {
                    tempCatFull = _.find(this.allCategories, (x: api.CategoryModel) => x.ID === tempCatFull.ParentID);
                } catch (ex) {
                    // Do Nothing
                }
                if (tempCatFull) {
                    tempCatFulls.push(tempCatFull);
                }
            } while (tempCatFull && tempCatFull.ParentID)
            switch (tempCatFulls.length) {
                case 0: {
                    // No (valid) category selected, show top level
                    return this.$q.resolve(<IAnalyzeCategoryResultCallbacks>{
                        updateBreadcrumb: (): void => {
                            this.breadcrumb = {
                                category1Key: null,
                                category2Key: null,
                                category3Key: null,
                                category4Key: null,
                                category5Key: null,
                                category6Key: null,
                                category7Key: null,
                                search: false,
                                compare: false,
                                storeName: this.cvStoreLocationService.getUsersSelectedStore()
                                    && this.cvStoreLocationService.getUsersSelectedStore().Name
                            };
                        },
                        updateSeoUrl: (): void => {
                            switch (kind) {
                                case "auctions": { this.updateSeoUrlAu(); break; }
                                case "categories": { this.updateSeoUrlCa(); break; }
                                case "franchises": { this.updateSeoUrlFr(); break; }
                                case "manufacturers": { this.updateSeoUrlMa(); break; }
                                case "stores": { this.updateSeoUrlSt(); break; }
                                case "vendors": { this.updateSeoUrlVe(); break; }
                                default: { this.updateSeoUrlPr(); break; }
                            }
                        },
                        goToOtherState: (params): void => {
                            if (this.getCategoryLevelLimitByKind(kind) >= 1) {
                                if (!this.$state.is(`searchCatalog.${kind}.categories.level1`)) {
                                    this.$state.transitionTo(`searchCatalog.${kind}.categories.level1`, null, true);
                                }
                            } else {
                                if (!this.$state.is(`searchCatalog.${kind}.results.both`, params)) {
                                    this.$state.transitionTo(`searchCatalog.${kind}.results.both`, params, true);
                                }
                            }
                        }
                    });
                }
                case 1: {
                    const category1CatFull = tempCatFulls[0];
                    let category1CatKey = null;
                    if (kind !== "stores" && this.activeSearchViewModel.CategoriesTree) {
                        const category1Cat = _.find(
                            this.activeSearchViewModel.CategoriesTree.Children,
                            x => x.Key === tempCatFulls[0].CustomKey || x.Key === tempCatFulls[0].Name + "|" + tempCatFulls[0].CustomKey);
                        category1CatKey = category1Cat ? category1Cat.Key : "";
                    }
                    // Top Level is selected, but nothing lower
                    return this.$q.resolve(<IAnalyzeCategoryResultCallbacks>{
                        updateBreadcrumb: (): void => {
                            this.breadcrumb = {
                                category1Key: category1CatKey,
                                category1Name: category1CatFull.Name,
                                category1DisplayName: category1CatFull.DisplayName,
                                category1SeoUrl: category1CatFull.SeoUrl,
                                category2Key: null,
                                category3Key: null,
                                category4Key: null,
                                category5Key: null,
                                category6Key: null,
                                category7Key: null,
                                search: false,
                                compare: false,
                                storeName: this.cvStoreLocationService.getUsersSelectedStore()
                                    && this.cvStoreLocationService.getUsersSelectedStore().Name
                            };
                        },
                        updateSeoUrl: (): void => {
                            switch (kind) {
                                case "auctions": { this.updateSeoUrlAu(); break; }
                                case "categories": { this.updateSeoUrlCa(); break; }
                                case "franchises": { this.updateSeoUrlFr(); break; }
                                case "manufacturers": { this.updateSeoUrlMa(); break; }
                                case "stores": { this.updateSeoUrlSt(); break; }
                                case "vendors": { this.updateSeoUrlVe(); break; }
                                default: { this.updateSeoUrlPr(); break; }
                            }
                        },
                        goToOtherState: (params): void => {
                            if (this.getCategoryLevelLimitByKind(kind) >= 2) {
                                if (!this.$state.is(`searchCatalog.${kind}.categories.level2`, params)) {
                                    this.$state.transitionTo(`searchCatalog.${kind}.categories.level2`, params, true);
                                }
                                return;
                            }
                            if (!this.$state.is(`searchCatalog.${kind}.results.both`, params)) {
                                this.$state.transitionTo(`searchCatalog.${kind}.results.both`, params, true);
                            }
                        }
                    });
                }
                case 2: {
                    const category1CatFull = tempCatFulls[1];
                    let category1Cat: api.AggregateTree = null;
                    let category1CatKey: string = null;
                    const category2CatFull = tempCatFulls[0];
                    let category2CatKey: string = null;
                    if (kind !== "stores" && this.activeSearchViewModel.CategoriesTree) {
                        category1Cat = _.find(
                            this.activeSearchViewModel.CategoriesTree.Children,
                            x => x.Key === category1CatFull.CustomKey || x.Key === category1CatFull.Name + "|" + category1CatFull.CustomKey);
                        if (category1Cat) {
                            category1CatKey = category1Cat.Key;
                            if (category1Cat.Children) {
                                category2CatKey = _.find(
                                    category1Cat.Children,
                                    x => x.Key === category2CatFull.CustomKey || x.Key === category2CatFull.Name + "|" + category2CatFull.CustomKey).Key;
                            }
                        }
                    }
                    // Top Level is selected, but nothing lower
                    return this.$q.resolve(<IAnalyzeCategoryResultCallbacks>{
                        updateBreadcrumb: (): void => {
                            this.breadcrumb = {
                                category1Key: category1CatKey,
                                category1Name: category1CatFull.Name,
                                category1DisplayName: category1CatFull.DisplayName,
                                category1SeoUrl: category1CatFull.SeoUrl,
                                category2Key: category2CatKey,
                                category2Name: category2CatFull.Name,
                                category2DisplayName: category2CatFull.DisplayName,
                                category2SeoUrl: category2CatFull.SeoUrl,
                                category3Key: null,
                                category4Key: null,
                                category5Key: null,
                                category6Key: null,
                                category7Key: null,
                                search: false,
                                compare: false,
                                storeName: this.cvStoreLocationService.getUsersSelectedStore()
                                    && this.cvStoreLocationService.getUsersSelectedStore().Name
                            };
                        },
                        updateSeoUrl: (): void => {
                            switch (kind) {
                                case "auctions": { this.updateSeoUrlAu(); break; }
                                case "categories": { this.updateSeoUrlCa(); break; }
                                case "franchises": { this.updateSeoUrlFr(); break; }
                                case "manufacturers": { this.updateSeoUrlMa(); break; }
                                case "stores": { this.updateSeoUrlSt(); break; }
                                case "vendors": { this.updateSeoUrlVe(); break; }
                                default: { this.updateSeoUrlPr(); break; }
                            }
                        },
                        goToOtherState: (params): void => {
                            if (this.getCategoryLevelLimitByKind(kind) >= 3) {
                                if (!this.$state.is(`searchCatalog.${kind}.categories.level3`, params)) {
                                    this.$state.transitionTo(`searchCatalog.${kind}.categories.level3`, params);
                                }
                                return;
                            }
                            if (!this.$state.is(`searchCatalog.${kind}.results.both`, params)) {
                                this.$state.transitionTo(`searchCatalog.${kind}.results.both`, params);
                            }
                        }
                    });
                }
                case 3: {
                    const category1CatFull = tempCatFulls[2];
                    let category1Cat: api.AggregateTree = null;
                    let category1CatKey: string = null;
                    const category2CatFull = tempCatFulls[1];
                    let category2Cat: api.AggregateTree = null;
                    let category2CatKey: string = null;
                    const category3CatFull = tempCatFulls[0];
                    let category3CatKey: string = null;
                    if (kind !== "stores" && this.activeSearchViewModel.CategoriesTree) {
                        category1Cat = _.find(
                            this.activeSearchViewModel.CategoriesTree.Children,
                            x => x.Key === tempCatFulls[2].CustomKey || x.Key === tempCatFulls[2].Name + "|" + tempCatFulls[2].CustomKey);
                        if (category1Cat) {
                            category1CatKey = category1Cat.Key;
                            if (category1Cat.Children) {
                                category2Cat = _.find(
                                    category1Cat.Children,
                                    x => x.Key === tempCatFulls[1].CustomKey || x.Key === tempCatFulls[1].Name + "|" + tempCatFulls[1].CustomKey);
                                if (category2Cat) {
                                    category2CatKey = category2Cat.Key;
                                    if (category2Cat.Children) {
                                        category3CatKey = _.find(
                                            category2Cat.Children,
                                            x => x.Key === tempCatFulls[0].CustomKey || x.Key === tempCatFulls[0].Name + "|" + tempCatFulls[0].CustomKey).Key;
                                    }
                                }
                            }
                        }
                    }
                    // Top Level is selected, but nothing lower
                    return this.$q.resolve(<IAnalyzeCategoryResultCallbacks>{
                        updateBreadcrumb: (): void => {
                            this.breadcrumb = {
                                category1Key: category1CatKey,
                                category1Name: category1CatFull.Name,
                                category1DisplayName: category1CatFull.DisplayName,
                                category1SeoUrl: category1CatFull.SeoUrl,
                                category2Key: category2CatKey,
                                category2Name: category2CatFull.Name,
                                category2DisplayName: category2CatFull.DisplayName,
                                category2SeoUrl: category2CatFull.SeoUrl,
                                category3Key: category3CatKey,
                                category3Name: category3CatFull.Name,
                                category3DisplayName: category3CatFull.DisplayName,
                                category3SeoUrl: category3CatFull.SeoUrl,
                                category4Key: null,
                                category5Key: null,
                                category6Key: null,
                                category7Key: null,
                                search: false,
                                compare: false,
                                storeName: this.cvStoreLocationService.getUsersSelectedStore()
                                    && this.cvStoreLocationService.getUsersSelectedStore().Name
                            };
                        },
                        updateSeoUrl: (): void => {
                            switch (kind) {
                                case "auctions": { this.updateSeoUrlAu(); break; }
                                case "categories": { this.updateSeoUrlCa(); break; }
                                case "franchises": { this.updateSeoUrlFr(); break; }
                                case "manufacturers": { this.updateSeoUrlMa(); break; }
                                case "stores": { this.updateSeoUrlSt(); break; }
                                case "vendors": { this.updateSeoUrlVe(); break; }
                                default: { this.updateSeoUrlPr(); break; }
                            }
                        },
                        goToOtherState: (params): void => {
                            if (!this.$state.is(`searchCatalog.${kind}.results.both`, params)) {
                                this.$state.transitionTo(`searchCatalog.${kind}.results.both`, params);
                            }
                        }
                    });
                }
                case 4: {
                    const category1CatFull = tempCatFulls[3];
                    let category1Cat: api.AggregateTree = null;
                    let category1CatKey: string = null;
                    const category2CatFull = tempCatFulls[2];
                    let category2Cat: api.AggregateTree = null;
                    let category2CatKey: string = null;
                    const category3CatFull = tempCatFulls[1];
                    let category3Cat: api.AggregateTree = null;
                    let category3CatKey: string = null;
                    const category4CatFull = tempCatFulls[0];
                    let category4CatKey: string = null;
                    if (kind !== "stores" && this.activeSearchViewModel.CategoriesTree) {
                        category1Cat = _.find(
                            this.activeSearchViewModel.CategoriesTree.Children,
                            x => x.Key === tempCatFulls[3].CustomKey || x.Key === tempCatFulls[3].Name + "|" + tempCatFulls[3].CustomKey);
                        if (category1Cat) {
                            category1CatKey = category1Cat.Key;
                            if (category1Cat.Children) {
                                category2Cat = _.find(
                                    category1Cat.Children,
                                    x => x.Key === tempCatFulls[2].CustomKey || x.Key === tempCatFulls[2].Name + "|" + tempCatFulls[2].CustomKey);
                                if (category2Cat) {
                                    category2CatKey = category2Cat.Key;
                                    if (category2Cat.Children) {
                                        category3Cat = _.find(
                                            category2Cat.Children,
                                            x => x.Key === tempCatFulls[1].CustomKey || x.Key === tempCatFulls[1].Name + "|" + tempCatFulls[1].CustomKey);
                                        if (category3Cat) {
                                            category3CatKey = category3Cat.Key;
                                            if (category3Cat.Children) {
                                                category4CatKey = _.find(
                                                    category3Cat.Children,
                                                    x => x.Key === tempCatFulls[0].CustomKey || x.Key === tempCatFulls[0].Name + "|" + tempCatFulls[0].CustomKey).Key;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // Top Level is selected, but nothing lower
                    return this.$q.resolve(<IAnalyzeCategoryResultCallbacks>{
                        updateBreadcrumb: (): void => {
                            this.breadcrumb = {
                                category1Key: category1CatKey,
                                category1Name: category1CatFull.Name,
                                category1DisplayName: category1CatFull.DisplayName,
                                category1SeoUrl: category1CatFull.SeoUrl,
                                category2Key: category2CatKey,
                                category2Name: category2CatFull.Name,
                                category2DisplayName: category2CatFull.DisplayName,
                                category2SeoUrl: category2CatFull.SeoUrl,
                                category3Key: category3CatKey,
                                category3Name: category3CatFull.Name,
                                category3DisplayName: category3CatFull.DisplayName,
                                category3SeoUrl: category3CatFull.SeoUrl,
                                category4Key: category4CatKey,
                                category4Name: category4CatFull.Name,
                                category4DisplayName: category4CatFull.DisplayName,
                                category4SeoUrl: category4CatFull.SeoUrl,
                                category5Key: null,
                                category6Key: null,
                                category7Key: null,
                                search: false,
                                compare: false,
                                storeName: this.cvStoreLocationService.getUsersSelectedStore()
                                    && this.cvStoreLocationService.getUsersSelectedStore().Name
                            };
                        },
                        updateSeoUrl: (): void => {
                            switch (kind) {
                                case "auctions": { this.updateSeoUrlAu(); break; }
                                case "categories": { this.updateSeoUrlCa(); break; }
                                case "franchises": { this.updateSeoUrlFr(); break; }
                                case "manufacturers": { this.updateSeoUrlMa(); break; }
                                case "stores": { this.updateSeoUrlSt(); break; }
                                case "vendors": { this.updateSeoUrlVe(); break; }
                                default: { this.updateSeoUrlPr(); break; }
                            }
                        },
                        goToOtherState: (params): void => {
                            if (!this.$state.is(`searchCatalog.${kind}.results.both`, params)) {
                                this.$state.transitionTo(`searchCatalog.${kind}.results.both`, params);
                            }
                        }
                    });
                }
                case 5: {
                    const category1CatFull = tempCatFulls[4];
                    let category1Cat: api.AggregateTree = null;
                    let category1CatKey: string = null;
                    const category2CatFull = tempCatFulls[3];
                    let category2Cat: api.AggregateTree = null;
                    let category2CatKey: string = null;
                    const category3CatFull = tempCatFulls[2];
                    let category3Cat: api.AggregateTree = null;
                    let category3CatKey: string = null;
                    const category4CatFull = tempCatFulls[1];
                    let category4CatKey: string = null;
                    let category4Cat: api.AggregateTree = null;
                    const category5CatFull = tempCatFulls[0];
                    let category5CatKey: string = null;
                    if (kind !== "stores" && this.activeSearchViewModel.CategoriesTree) {
                        category1Cat = _.find(
                            this.activeSearchViewModel.CategoriesTree.Children,
                            x => x.Key === tempCatFulls[4].CustomKey || x.Key === tempCatFulls[4].Name + "|" + tempCatFulls[4].CustomKey);
                        if (category1Cat) {
                            category1CatKey = category1Cat.Key;
                            if (category1Cat.Children) {
                                category2Cat = _.find(
                                    category1Cat.Children,
                                    x => x.Key === tempCatFulls[3].CustomKey || x.Key === tempCatFulls[3].Name + "|" + tempCatFulls[3].CustomKey);
                                if (category2Cat) {
                                    category2CatKey = category2Cat.Key;
                                    if (category2Cat.Children) {
                                        category3Cat = _.find(
                                            category2Cat.Children,
                                            x => x.Key === tempCatFulls[2].CustomKey || x.Key === tempCatFulls[2].Name + "|" + tempCatFulls[2].CustomKey);
                                        if (category3Cat) {
                                            category3CatKey = category3Cat.Key;
                                            if (category3Cat.Children) {
                                                category4Cat = _.find(
                                                    category3Cat.Children,
                                                    x => x.Key === tempCatFulls[1].CustomKey || x.Key === tempCatFulls[1].Name + "|" + tempCatFulls[1].CustomKey);
                                                if (category4Cat) {
                                                    category4CatKey = category4Cat.Key;
                                                    if (category4Cat.Children) {
                                                        category5CatKey = _.find(
                                                            category4Cat.Children,
                                                            x => x.Key === tempCatFulls[0].CustomKey || x.Key === tempCatFulls[0].Name + "|" + tempCatFulls[0].CustomKey).Key;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // Top Level is selected, but nothing lower
                    return this.$q.resolve(<IAnalyzeCategoryResultCallbacks>{
                        updateBreadcrumb: (): void => {
                            this.breadcrumb = {
                                category1Key: category1CatKey,
                                category1Name: category1CatFull.Name,
                                category1DisplayName: category1CatFull.DisplayName,
                                category1SeoUrl: category1CatFull.SeoUrl,
                                category2Key: category2CatKey,
                                category2Name: category2CatFull.Name,
                                category2DisplayName: category2CatFull.DisplayName,
                                category2SeoUrl: category2CatFull.SeoUrl,
                                category3Key: category3CatKey,
                                category3Name: category3CatFull.Name,
                                category3DisplayName: category3CatFull.DisplayName,
                                category3SeoUrl: category3CatFull.SeoUrl,
                                category4Key: category4CatKey,
                                category4Name: category4CatFull.Name,
                                category4DisplayName: category4CatFull.DisplayName,
                                category4SeoUrl: category4CatFull.SeoUrl,
                                category5Key: category5CatKey,
                                category5Name: category5CatFull.Name,
                                category5DisplayName: category5CatFull.DisplayName,
                                category5SeoUrl: category5CatFull.SeoUrl,
                                category6Key: null,
                                category7Key: null,
                                search: false,
                                compare: false,
                                storeName: this.cvStoreLocationService.getUsersSelectedStore() && this.cvStoreLocationService.getUsersSelectedStore().Name
                            };
                        },
                        updateSeoUrl: (): void => {
                            switch (kind) {
                                case "auctions": { this.updateSeoUrlAu(); break; }
                                case "categories": { this.updateSeoUrlCa(); break; }
                                case "franchises": { this.updateSeoUrlFr(); break; }
                                case "manufacturers": { this.updateSeoUrlMa(); break; }
                                case "stores": { this.updateSeoUrlSt(); break; }
                                case "vendors": { this.updateSeoUrlVe(); break; }
                                default: { this.updateSeoUrlPr(); break; }
                            }
                        },
                        goToOtherState: (params): void => {
                            if (!this.$state.is(`searchCatalog.${kind}.results.both`, params)) {
                                this.$state.transitionTo(`searchCatalog.${kind}.results.both`, params);
                            }
                        }
                    });
                }
                case 6: {
                    const category1CatFull = tempCatFulls[5];
                    let category1Cat: api.AggregateTree = null;
                    let category1CatKey: string = null;
                    const category2CatFull = tempCatFulls[4];
                    let category2Cat: api.AggregateTree = null;
                    let category2CatKey: string = null;
                    const category3CatFull = tempCatFulls[3];
                    let category3Cat: api.AggregateTree = null;
                    let category3CatKey: string = null;
                    const category4CatFull = tempCatFulls[2];
                    let category4CatKey: string = null;
                    let category4Cat: api.AggregateTree = null;
                    const category5CatFull = tempCatFulls[1];
                    let category5CatKey: string = null;
                    let category5Cat: api.AggregateTree = null;
                    const category6CatFull = tempCatFulls[0];
                    let category6CatKey: string = null;
                    if (kind !== "stores" && this.activeSearchViewModel.CategoriesTree) {
                        category1Cat = _.find(
                            this.activeSearchViewModel.CategoriesTree.Children,
                            x => x.Key === tempCatFulls[5].CustomKey || x.Key === tempCatFulls[5].Name + "|" + tempCatFulls[5].CustomKey);
                        if (category1Cat) {
                            category1CatKey = category1Cat.Key;
                            if (category1Cat.Children) {
                                category2Cat = _.find(
                                    category1Cat.Children,
                                    x => x.Key === tempCatFulls[4].CustomKey || x.Key === tempCatFulls[4].Name + "|" + tempCatFulls[4].CustomKey);
                                if (category2Cat) {
                                    category2CatKey = category2Cat.Key;
                                    if (category2Cat.Children) {
                                        category3Cat = _.find(
                                            category2Cat.Children,
                                            x => x.Key === tempCatFulls[3].CustomKey || x.Key === tempCatFulls[3].Name + "|" + tempCatFulls[3].CustomKey);
                                        if (category3Cat) {
                                            category3CatKey = category3Cat.Key;
                                            if (category3Cat.Children) {
                                                category4Cat = _.find(
                                                    category3Cat.Children,
                                                    x => x.Key === tempCatFulls[2].CustomKey || x.Key === tempCatFulls[2].Name + "|" + tempCatFulls[2].CustomKey);
                                                if (category4Cat) {
                                                    category4CatKey = category4Cat.Key;
                                                    if (category4Cat.Children) {
                                                        category5Cat = _.find(
                                                            category4Cat.Children,
                                                            x => x.Key === tempCatFulls[1].CustomKey || x.Key === tempCatFulls[1].Name + "|" + tempCatFulls[1].CustomKey);
                                                        if (category5Cat) {
                                                            category5CatKey = category5Cat.Key;
                                                            if (category5Cat.Children) {
                                                                category6CatKey = _.find(
                                                                    category5Cat.Children,
                                                                    x => x.Key === tempCatFulls[0].CustomKey || x.Key === tempCatFulls[0].Name + "|" + tempCatFulls[0].CustomKey).Key;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // Top Level is selected, but nothing lower
                    return this.$q.resolve(<IAnalyzeCategoryResultCallbacks>{
                        updateBreadcrumb: (): void => {
                            this.breadcrumb = {
                                category1Key: category1CatKey,
                                category1Name: category1CatFull.Name,
                                category1DisplayName: category1CatFull.DisplayName,
                                category1SeoUrl: category1CatFull.SeoUrl,
                                category2Key: category2CatKey,
                                category2Name: category2CatFull.Name,
                                category2DisplayName: category2CatFull.DisplayName,
                                category2SeoUrl: category2CatFull.SeoUrl,
                                category3Key: category3CatKey,
                                category3Name: category3CatFull.Name,
                                category3DisplayName: category3CatFull.DisplayName,
                                category3SeoUrl: category3CatFull.SeoUrl,
                                category4Key: category4CatKey,
                                category4Name: category4CatFull.Name,
                                category4DisplayName: category4CatFull.DisplayName,
                                category4SeoUrl: category4CatFull.SeoUrl,
                                category5Key: category5CatKey,
                                category5Name: category5CatFull.Name,
                                category5DisplayName: category5CatFull.DisplayName,
                                category5SeoUrl: category5CatFull.SeoUrl,
                                category6Key: category6CatKey,
                                category6Name: category6CatFull.Name,
                                category6DisplayName: category6CatFull.DisplayName,
                                category6SeoUrl: category6CatFull.SeoUrl,
                                category7Key: null,
                                search: false,
                                compare: false,
                                storeName: this.cvStoreLocationService.getUsersSelectedStore()
                                    && this.cvStoreLocationService.getUsersSelectedStore().Name
                            };
                        },
                        updateSeoUrl: (): void => {
                            switch (kind) {
                                case "auctions": { this.updateSeoUrlAu(); break; }
                                case "categories": { this.updateSeoUrlCa(); break; }
                                case "franchises": { this.updateSeoUrlFr(); break; }
                                case "manufacturers": { this.updateSeoUrlMa(); break; }
                                case "stores": { this.updateSeoUrlSt(); break; }
                                case "vendors": { this.updateSeoUrlVe(); break; }
                                default: { this.updateSeoUrlPr(); break; }
                            }
                        },
                        goToOtherState: (params): void => {
                            if (!this.$state.is(`searchCatalog.${kind}.results.both`, params)) {
                                this.$state.transitionTo(`searchCatalog.${kind}.results.both`, params);
                            }
                        }
                    });
                }
                case 7: {
                    const category1CatFull = tempCatFulls[6];
                    let category1Cat: api.AggregateTree = null;
                    let category1CatKey: string = null;
                    const category2CatFull = tempCatFulls[5];
                    let category2Cat: api.AggregateTree = null;
                    let category2CatKey: string = null;
                    const category3CatFull = tempCatFulls[4];
                    let category3Cat: api.AggregateTree = null;
                    let category3CatKey: string = null;
                    const category4CatFull = tempCatFulls[3];
                    let category4CatKey: string = null;
                    let category4Cat: api.AggregateTree = null;
                    const category5CatFull = tempCatFulls[2];
                    let category5CatKey: string = null;
                    let category5Cat: api.AggregateTree = null;
                    const category6CatFull = tempCatFulls[1];
                    let category6CatKey: string = null;
                    let category6Cat: api.AggregateTree = null;
                    const category7CatFull = tempCatFulls[0];
                    let category7CatKey: string = null;
                    if (kind !== "stores" && this.activeSearchViewModel.CategoriesTree) {
                        category1Cat = _.find(
                            this.activeSearchViewModel.CategoriesTree.Children,
                            x => x.Key === tempCatFulls[6].CustomKey || x.Key === tempCatFulls[6].Name + "|" + tempCatFulls[6].CustomKey);
                        if (category1Cat) {
                            category1CatKey = category1Cat.Key;
                            if (category1Cat.Children) {
                                category2Cat = _.find(
                                    category1Cat.Children,
                                    x => x.Key === tempCatFulls[5].CustomKey || x.Key === tempCatFulls[5].Name + "|" + tempCatFulls[5].CustomKey);
                                if (category2Cat) {
                                    category2CatKey = category2Cat.Key;
                                    if (category2Cat.Children) {
                                        category3Cat = _.find(
                                            category2Cat.Children,
                                            x => x.Key === tempCatFulls[4].CustomKey || x.Key === tempCatFulls[4].Name + "|" + tempCatFulls[4].CustomKey);
                                        if (category3Cat) {
                                            category3CatKey = category3Cat.Key;
                                            if (category3Cat.Children) {
                                                category4Cat = _.find(
                                                    category3Cat.Children,
                                                    x => x.Key === tempCatFulls[3].CustomKey || x.Key === tempCatFulls[3].Name + "|" + tempCatFulls[3].CustomKey);
                                                if (category4Cat) {
                                                    category4CatKey = category4Cat.Key;
                                                    if (category4Cat.Children) {
                                                        category5Cat = _.find(
                                                            category4Cat.Children,
                                                            x => x.Key === tempCatFulls[2].CustomKey || x.Key === tempCatFulls[2].Name + "|" + tempCatFulls[2].CustomKey);
                                                        if (category5Cat) {
                                                            category5CatKey = category5Cat.Key;
                                                            if (category5Cat.Children) {
                                                                category6Cat = _.find(
                                                                    category5Cat.Children,
                                                                    x => x.Key === tempCatFulls[1].CustomKey || x.Key === tempCatFulls[1].Name + "|" + tempCatFulls[1].CustomKey);
                                                                if (category6Cat) {
                                                                    category6CatKey = category6Cat.Key;
                                                                    if (category6Cat.Children) {
                                                                        category7CatKey = _.find(
                                                                            category6Cat.Children,
                                                                            x => x.Key === tempCatFulls[0].CustomKey || x.Key === tempCatFulls[0].Name + "|" + tempCatFulls[0].CustomKey).Key;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // Top Level is selected, but nothing lower
                    return this.$q.resolve(<IAnalyzeCategoryResultCallbacks>{
                        updateBreadcrumb: (): void => {
                            this.breadcrumb = {
                                category1Key: category1CatKey,
                                category1Name: category1CatFull.Name,
                                category1DisplayName: category1CatFull.DisplayName,
                                category1SeoUrl: category1CatFull.SeoUrl,
                                category2Key: category2CatKey,
                                category2Name: category2CatFull.Name,
                                category2DisplayName: category2CatFull.DisplayName,
                                category2SeoUrl: category2CatFull.SeoUrl,
                                category3Key: category3CatKey,
                                category3Name: category3CatFull.Name,
                                category3DisplayName: category3CatFull.DisplayName,
                                category3SeoUrl: category3CatFull.SeoUrl,
                                category4Key: category4CatKey,
                                category4Name: category4CatFull.Name,
                                category4DisplayName: category4CatFull.DisplayName,
                                category4SeoUrl: category4CatFull.SeoUrl,
                                category5Key: category5CatKey,
                                category5Name: category5CatFull.Name,
                                category5DisplayName: category5CatFull.DisplayName,
                                category5SeoUrl: category5CatFull.SeoUrl,
                                category6Key: category6CatKey,
                                category6Name: category6CatFull.Name,
                                category6DisplayName: category6CatFull.DisplayName,
                                category6SeoUrl: category6CatFull.SeoUrl,
                                category7Key: category7CatKey,
                                category7Name: category7CatFull.Name,
                                category7DisplayName: category7CatFull.DisplayName,
                                category7SeoUrl: category7CatFull.SeoUrl,
                                search: false,
                                compare: false,
                                storeName: this.cvStoreLocationService.getUsersSelectedStore()
                                    && this.cvStoreLocationService.getUsersSelectedStore().Name
                            };
                        },
                        updateSeoUrl: (): void => {
                            switch (kind) {
                                case "auctions": { this.updateSeoUrlAu(); break; }
                                case "categories": { this.updateSeoUrlCa(); break; }
                                case "franchises": { this.updateSeoUrlFr(); break; }
                                case "manufacturers": { this.updateSeoUrlMa(); break; }
                                case "stores": { this.updateSeoUrlSt(); break; }
                                case "vendors": { this.updateSeoUrlVe(); break; }
                                default: { this.updateSeoUrlPr(); break; }
                            }
                        },
                        goToOtherState: (params): void => {
                            if (!this.$state.is(`searchCatalog.${kind}.results.both`, params)) {
                                this.$state.transitionTo(`searchCatalog.${kind}.results.both`, params);
                            }
                        }
                    });
                }
            }
            return this.$q.reject("invalid number of categories to parse through");
        }

        getCategoryByKey(key: string): void {
            if (this.selectedCategory && this.selectedCategory.CustomKey === key) {
                return;
            }
            this.cvApi.categories.GetCategoryByKey({
                Key: key,
                ExcludeProductCategories: true
            }).then(r => {
                this.selectedCategory = r.data;
                const found = this.selectedCategory?.Images?.find(x => x.TypeKey === "Catalog Banner" && x.OriginalFileName !== null);
                if (found) {
                    this.catalogBannerImageFileName = found.OriginalFileName;
                }
            });
        }

        getForcedCategoryPromise(pathname: string): ng.IPromise<api.CategoryModel> {
            return this.$q<api.CategoryModel>((resolve, reject) => {
                this.cvApi.categories.GetCategoryBySeoUrl({
                    SeoUrl: pathname,
                    ExcludeProductCategories: true
                }).then(r => {
                    if (r.data
                        && r.data.SerializableAttributes
                        && r.data.SerializableAttributes["IsForcedCategory"]
                        && r.data.SerializableAttributes["IsForcedCategory"].Value
                        && r.data.SerializableAttributes["IsForcedCategory"].Value == "True") {
                        this.forcedCategory = r.data;
                    }
                    resolve(this.forcedCategory);
                }).catch(reason => {
                    this.consoleDebug(reason)
                    reject(reason);
                });
            });
        }

        refreshPromiseAu(): ng.IPromise<api.AuctionSearchViewModel> {
            if (!this.$state.includes("searchCatalog") || this.$state.is("searchCatalog.products.compare")) {
                if (this.searchIsRunning) {
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                }
                return this.$q.reject("Not on the directory page, don't redirect into it");
            }
            this.lastSearchFailed = false;
            this.cleanForm();
            const params = this.convertFormToStateParams();
            const stringifiedForm = JSON.stringify(params);
            return this.$q((resolve, reject) => {
                const fullReject = (reason): void => {
                    this.lastSearchFailed = true;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    reject(reason);
                };
                const fullResolve = (localSearchViewModel: api.AuctionSearchViewModel): void => {
                    this.lastSearchFailed = false;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    this.searchViewModelAu = localSearchViewModel;
                    resolve(this.searchViewModelAu);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.searchComplete);
                    this.searchViewModelAu.CategoriesTree = this.cleanCategoryTree(
                        this.searchViewModelAu.CategoriesTree,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.categoryTreeReady,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.attributes.ready,
                        this.allAttributes);
                };
                if (this.lastUsedSearchFormAu === stringifiedForm) {
                    /* Auctions doesn't have a compare
                    if (this.$state.is(SearchCatalogService.stateAu + ".compare")) {
                        this.breadcrumb.compare = true;
                        this.analyzeSingleCategoryPromise("stores", this.searchViewModelAu.Form)
                            .then(() => fullResolve(this.searchViewModelAu)) // Don't go anywhere
                            .catch(fullReject); // TODO: Error State
                        return;
                    }
                    */
                    this.breadcrumb.compare = false;
                    this.analyzeSingleCategoryPromise("auctions", this.searchViewModelAu.Form).then(callbacks => {
                        callbacks.goToOtherState(params); // Go to the state
                        callbacks.updateBreadcrumb();
                        callbacks.updateSeoUrl();
                        this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current)
                        fullResolve(this.searchViewModelAu);
                    }).catch(fullReject); // TODO: Error State
                    return;
                }
                // Note the form as we use it for the search
                this.lastUsedSearchFormAu = stringifiedForm;
                // Run the search
                this.searchIsRunning = true;
                // Handle Forced Category
                if (this.forcedCategory) {
                    this.searchViewModelPr.Form.ForcedCategory
                        = this.forcedCategory.DisplayName + "|" + this.forcedCategory.CustomKey
                }
                this.cvApi.providers.SearchAuctionCatalogWithProvider(this.searchViewModelAu.Form).then(r => {
                    // Check to make sure we didn't change the form between the start of this call and when it resolved
                    const cleanResult = this.cleanFormInner(r.data.Form);
                    const cleanExisting = this.cleanFormInner(this.searchViewModelAu.Form);
                    const cleanResultString = angular.toJson(Object.keys(cleanResult).sort().reduce((r, k) => (r[k] = cleanResult[k], r), {}));
                    const cleanExistingString = angular.toJson(Object.keys(cleanExisting).sort().reduce((r, k) => (r[k] = cleanExisting[k], r), {}));
                    if (cleanExistingString !== cleanResultString) {
                        // It isn't the same, don't assign stuff
                        //return; // BUG: Sometimes this breaks the search, need to get reproducible states
                    }
                    const localSearchViewModel = r.data;
                    localSearchViewModel.Form = cleanResult;
                    // Append category info for the catalog if available
                    if (localSearchViewModel.Form
                        && localSearchViewModel.Form.Category
                        && localSearchViewModel.Form.Category.split("|")[1]) {
                        this.getCategoryByKey(localSearchViewModel.Form.Category.split("|")[1]);
                    }
                    if (!localSearchViewModel.Total || localSearchViewModel.Total <= 0) {
                        // A message about no results will show
                        this.breadcrumb.compare = false;
                        this.analyzeSingleCategoryPromise("auctions", localSearchViewModel.Form).then(callbacks => {
                            callbacks.goToOtherState(params);
                            callbacks.updateBreadcrumb();
                            callbacks.updateSeoUrl();
                            fullResolve(localSearchViewModel);
                        }).catch(fullReject); // TODO: Error State
                        return;
                    }
                    ////this.cvAuctionService.bulkGet(localSearchViewModel.ResultIDs).then(auctions => {
                        ////if (localSearchViewModel.Form.Sort.toString() === "Relevance") {
                            ////auctions.forEach(x => x["Score"] = localSearchViewModel.HitsMetaDataHitScores[x.ID]);
                        ////}
                        ////localSearchViewModel["WithMaps"] = auctions;
                        ////// Append inventory info for the catalog
                        ////this.cvStoreLocationService.getUserSelectedStore().then(store => {
                            ////this.hasSelectedStore = true;
                            ////this.userSelectedStore = store;
                        ////}).finally(() => {
                            /* Auctions don't have a compare
                            if (this.$state.is(SearchCatalogService.stateAu + ".compare")) {
                                // Don't go anywhere
                                this.breadcrumb.compare = true;
                                fullResolve(localSearchViewModel);
                                return;
                            }
                            */
                            this.breadcrumb.compare = false;
                            this.analyzeSingleCategoryPromise("auctions", localSearchViewModel.Form).then(callbacks => {
                                callbacks.goToOtherState(params); // Go to the other state
                                callbacks.updateBreadcrumb();
                                callbacks.updateSeoUrl();
                                fullResolve(localSearchViewModel);
                            }).catch(fullReject); // TODO: Error State
                        ////});
                    ////}).catch(fullReject); // TODO: Error State
                }).catch(fullReject); // TODO: Error State
            });
        }

        refreshPromiseCa(): ng.IPromise<api.CategorySearchViewModel> {
            if (!this.$state.includes("searchCatalog")) {
                if (this.searchIsRunning) {
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                }
                return this.$q.reject("Not on the directory page, don't redirect into it");
            }
            if (angular.isUndefined(this.resultsPageFormatChoices)) {
                this.resultsPageFormatChoices = ["grid", "list"];
            }
            this.lastSearchFailed = false;
            this.cleanForm();
            const params = this.convertFormToStateParams();
            const stringifiedForm = JSON.stringify(params);
            return this.$q((resolve, reject) => {
                const fullReject = (reason): void => {
                    this.lastSearchFailed = true;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    reject(reason);
                };
                const fullResolve = (localSearchViewModel: api.CategorySearchViewModel): void => {
                    this.lastSearchFailed = false;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    this.searchViewModelCa = localSearchViewModel;
                    resolve(this.searchViewModelCa);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.searchComplete);
                    this.searchViewModelCa.CategoriesTree = this.cleanCategoryTree(
                        this.searchViewModelCa.CategoriesTree,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.categoryTreeReady,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.attributes.ready,
                        this.allAttributes);
                };
                if (this.lastUsedSearchFormCa === stringifiedForm) {
                    /* Categories doesn't have a compare
                    if (this.$state.is(SearchCatalogService.stateCa + ".compare")) {
                        this.breadcrumb.compare = true;
                        this.analyzeSingleCategoryPromise("stores", this.searchViewModelCa.Form)
                            .then(() => fullResolve(this.searchViewModelCa)) // Don't go anywhere
                            .catch(fullReject); // TODO: Error State
                        return;
                    }
                    */
                    this.breadcrumb.compare = false;
                    this.analyzeSingleCategoryPromise("categories", this.searchViewModelCa.Form).then(callbacks => {
                        callbacks.goToOtherState(params); // Go to the state
                        callbacks.updateBreadcrumb();
                        callbacks.updateSeoUrl();
                        this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current)
                        fullResolve(this.searchViewModelCa);
                    }).catch(fullReject); // TODO: Error State
                    return;
                }
                // Note the form as we use it for the search
                this.lastUsedSearchFormCa = stringifiedForm;
                // Run the search
                this.searchIsRunning = true;
                // Handle Forced Category
                if (this.forcedCategory) {
                    this.searchViewModelPr.Form.ForcedCategory
                        = this.forcedCategory.DisplayName + "|" + this.forcedCategory.CustomKey
                }
                this.cvApi.providers.SearchCategoryCatalogWithProvider(this.searchViewModelCa.Form).then(r => {
                    // Check to make sure we didn't change the form between the start of this call and when it resolved
                    const cleanResult = this.cleanFormInner(r.data.Form);
                    const cleanExisting = this.cleanFormInner(this.searchViewModelCa.Form);
                    const cleanResultString = angular.toJson(Object.keys(cleanResult).sort().reduce((r, k) => (r[k] = cleanResult[k], r), {}));
                    const cleanExistingString = angular.toJson(Object.keys(cleanExisting).sort().reduce((r, k) => (r[k] = cleanExisting[k], r), {}));
                    if (cleanExistingString !== cleanResultString) {
                        // It isn't the same, don't assign stuff
                        //return; // BUG: Sometimes this breaks the search, need to get reproducible states
                    }
                    const localSearchViewModel = r.data;
                    localSearchViewModel.Form = cleanResult;
                    // Append category info for the catalog if available
                    if (localSearchViewModel.Form
                        && localSearchViewModel.Form.Category
                        && localSearchViewModel.Form.Category.split("|")[1]) {
                        this.getCategoryByKey(localSearchViewModel.Form.Category.split("|")[1]);
                    }
                    if (!localSearchViewModel.Total || localSearchViewModel.Total <= 0) {
                        // A message about no results will show
                        this.breadcrumb.compare = false;
                        this.analyzeSingleCategoryPromise("categories", localSearchViewModel.Form).then(callbacks => {
                            callbacks.goToOtherState(params);
                            callbacks.updateBreadcrumb();
                            callbacks.updateSeoUrl();
                            fullResolve(localSearchViewModel);
                        }).catch(fullReject); // TODO: Error State
                        return;
                    }
                    ////this.cvCategoryService.bulkGet(localSearchViewModel.ResultIDs).then(categories => {
                        ////if (localSearchViewModel.Form.Sort.toString() === "Relevance") {
                            ////categories.forEach(x => x["Score"] = localSearchViewModel.HitsMetaDataHitScores[x.ID]);
                        ////}
                        ////localSearchViewModel["WithMaps"] = categories;
                        ////// Append inventory info for the catalog
                        ////this.cvStoreLocationService.getUserSelectedStore().then(store => {
                            ////this.hasSelectedStore = true;
                            ////this.userSelectedStore = store;
                        ////}).finally(() => {
                            /* Categories don't have a compare
                            if (this.$state.is(SearchCatalogService.stateCa + ".compare")) {
                                // Don't go anywhere
                                this.breadcrumb.compare = true;
                                fullResolve(localSearchViewModel);
                                return;
                            }
                            */
                            this.breadcrumb.compare = false;
                            this.analyzeSingleCategoryPromise("categories", localSearchViewModel.Form).then(callbacks => {
                                callbacks.goToOtherState(params); // Go to the other state
                                callbacks.updateBreadcrumb();
                                callbacks.updateSeoUrl();
                                fullResolve(localSearchViewModel);
                            }).catch(fullReject); // TODO: Error State
                        ////});
                    ////}).catch(fullReject); // TODO: Error State
                }).catch(fullReject); // TODO: Error State
            });
        }

        refreshPromiseFr(): ng.IPromise<api.FranchiseSearchViewModel> {
            if (!this.$state.includes("searchCatalog")) {
                if (this.searchIsRunning) {
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                }
                return this.$q.reject("Not on the directory page, don't redirect into it");
            }
            this.lastSearchFailed = false;
            this.cleanForm();
            const params = this.convertFormToStateParams();
            const stringifiedForm = JSON.stringify(params);
            return this.$q((resolve, reject) => {
                const fullReject = (reason): void => {
                    this.lastSearchFailed = true;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    reject(reason);
                };
                const fullResolve = (localSearchViewModel: api.FranchiseSearchViewModel): void => {
                    this.lastSearchFailed = false;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    this.searchViewModelFr = localSearchViewModel;
                    resolve(this.searchViewModelFr);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.searchComplete);
                    this.searchViewModelFr.CategoriesTree = this.cleanCategoryTree(
                        this.searchViewModelFr.CategoriesTree,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.categoryTreeReady,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.attributes.ready,
                        this.allAttributes);
                };
                if (this.lastUsedSearchFormFr === stringifiedForm) {
                    /* Franchises doesn't have a compare
                    if (this.$state.is(SearchCatalogService.stateFr + ".compare")) {
                        this.breadcrumb.compare = true;
                        this.analyzeSingleCategoryPromise("stores", this.searchViewModelFr.Form)
                            .then(() => fullResolve(this.searchViewModelFr)) // Don't go anywhere
                            .catch(fullReject); // TODO: Error State
                        return;
                    }
                    */
                    this.breadcrumb.compare = false;
                    this.analyzeSingleCategoryPromise("franchises", this.searchViewModelFr.Form).then(callbacks => {
                        callbacks.goToOtherState(params); // Go to the state
                        callbacks.updateBreadcrumb();
                        callbacks.updateSeoUrl();
                        this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current)
                        fullResolve(this.searchViewModelFr);
                    }).catch(fullReject); // TODO: Error State
                    return;
                }
                // Note the form as we use it for the search
                this.lastUsedSearchFormFr = stringifiedForm;
                // Run the search
                this.searchIsRunning = true;
                // Handle Forced Category
                if (this.forcedCategory) {
                    this.searchViewModelFr.Form.ForcedCategory
                        = this.forcedCategory.DisplayName + "|" + this.forcedCategory.CustomKey
                }
                this.cvApi.providers.SearchFranchiseCatalogWithProvider(this.searchViewModelFr.Form).then(r => {
                    // Check to make sure we didn't change the form between the start of this call and when it resolved
                    const cleanResult = this.cleanFormInner(r.data.Form);
                    const cleanExisting = this.cleanFormInner(this.searchViewModelFr.Form);
                    const cleanResultString = angular.toJson(Object.keys(cleanResult).sort().reduce((r, k) => (r[k] = cleanResult[k], r), {}));
                    const cleanExistingString = angular.toJson(Object.keys(cleanExisting).sort().reduce((r, k) => (r[k] = cleanExisting[k], r), {}));
                    if (cleanExistingString !== cleanResultString) {
                        // It isn't the same, don't assign stuff
                        //return; // BUG: Sometimes this breaks the search, need to get reproducible states
                    }
                    const localSearchViewModel = r.data;
                    localSearchViewModel.Form = cleanResult;
                    // Append category info for the catalog if available
                    if (localSearchViewModel.Form
                        && localSearchViewModel.Form.Category
                        && localSearchViewModel.Form.Category.split("|")[1]) {
                        this.getCategoryByKey(localSearchViewModel.Form.Category.split("|")[1]);
                    }
                    if (!localSearchViewModel.Total || localSearchViewModel.Total <= 0) {
                        // A message about no results will show
                        this.breadcrumb.compare = false;
                        this.analyzeSingleCategoryPromise("franchises", localSearchViewModel.Form).then(callbacks => {
                            callbacks.goToOtherState(params);
                            callbacks.updateBreadcrumb();
                            callbacks.updateSeoUrl();
                            fullResolve(localSearchViewModel);
                        }).catch(fullReject); // TODO: Error State
                        return;
                    }
                    ////this.cvFranchiseService.bulkGet(localSearchViewModel.ResultIDs).then(franchises => {
                        ////if (localSearchViewModel.Form.Sort.toString() === "Relevance") {
                            ////franchises.forEach(x => x["Score"] = localSearchViewModel.HitsMetaDataHitScores[x.ID]);
                        ////}
                        ////localSearchViewModel["WithMaps"] = franchises;
                        ////// Append inventory info for the catalog
                        ////this.cvStoreLocationService.getUserSelectedStore().then(store => {
                            ////this.hasSelectedStore = true;
                            ////this.userSelectedStore = store;
                        ////}).finally(() => {
                            /* Franchises don't have a compare
                            if (this.$state.is(SearchCatalogService.stateFr + ".compare")) {
                                // Don't go anywhere
                                this.breadcrumb.compare = true;
                                fullResolve(localSearchViewModel);
                                return;
                            }
                            */
                            this.breadcrumb.compare = false;
                            this.analyzeSingleCategoryPromise("franchises", localSearchViewModel.Form).then(callbacks => {
                                callbacks.goToOtherState(params); // Go to the other state
                                callbacks.updateBreadcrumb();
                                callbacks.updateSeoUrl();
                                fullResolve(localSearchViewModel);
                            }).catch(fullReject); // TODO: Error State
                        ////});
                    ////}).catch(fullReject); // TODO: Error State
                }).catch(fullReject); // TODO: Error State
            });
        }

        refreshPromiseLo(): ng.IPromise<api.LotSearchViewModel> {
            if (!this.$state.includes("searchCatalog")) {
                if (this.searchIsRunning) {
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                }
                return this.$q.reject("Not on the directory page, don't redirect into it");
            }
            this.lastSearchFailed = false;
            this.cleanForm();
            const params = this.convertFormToStateParams();
            const stringifiedForm = JSON.stringify(params);
            return this.$q((resolve, reject) => {
                const fullReject = (reason): void => {
                    this.lastSearchFailed = true;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    reject(reason);
                };
                const fullResolve = (localSearchViewModel: api.LotSearchViewModel): void => {
                    this.lastSearchFailed = false;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    this.searchViewModelLo = localSearchViewModel;
                    resolve(this.searchViewModelLo);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.searchComplete);
                    this.searchViewModelLo.CategoriesTree = this.cleanCategoryTree(
                        this.searchViewModelLo.CategoriesTree,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.categoryTreeReady,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.attributes.ready,
                        this.allAttributes);
                };
                if (this.lastUsedSearchFormLo === stringifiedForm) {
                    /* Lots doesn't have a compare
                    if (this.$state.is(SearchCatalogService.stateLo + ".compare")) {
                        this.breadcrumb.compare = true;
                        this.analyzeSingleCategoryPromise("stores", this.searchViewModelLo.Form)
                            .then(() => fullResolve(this.searchViewModelLo)) // Don't go anywhere
                            .catch(fullReject); // TODO: Error State
                        return;
                    }
                    */
                    this.breadcrumb.compare = false;
                    this.analyzeSingleCategoryPromise("lots", this.searchViewModelLo.Form).then(callbacks => {
                        callbacks.goToOtherState(params); // Go to the state
                        callbacks.updateBreadcrumb();
                        callbacks.updateSeoUrl();
                        this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current)
                        fullResolve(this.searchViewModelLo);
                    }).catch(fullReject); // TODO: Error State
                    return;
                }
                // Note the form as we use it for the search
                this.lastUsedSearchFormLo = stringifiedForm;
                // Run the search
                this.searchIsRunning = true;
                // Handle Forced Category
                if (this.forcedCategory) {
                    this.searchViewModelPr.Form.ForcedCategory
                        = this.forcedCategory.DisplayName + "|" + this.forcedCategory.CustomKey
                }
                this.cvApi.providers.SearchLotCatalogWithProvider(this.searchViewModelLo.Form).then(r => {
                    // Check to make sure we didn't change the form between the start of this call and when it resolved
                    const cleanResult = this.cleanFormInner(r.data.Form);
                    const cleanExisting = this.cleanFormInner(this.searchViewModelLo.Form);
                    const cleanResultString = angular.toJson(Object.keys(cleanResult).sort().reduce((r, k) => (r[k] = cleanResult[k], r), {}));
                    const cleanExistingString = angular.toJson(Object.keys(cleanExisting).sort().reduce((r, k) => (r[k] = cleanExisting[k], r), {}));
                    if (cleanExistingString !== cleanResultString) {
                        // It isn't the same, don't assign stuff
                        //return; // BUG: Sometimes this breaks the search, need to get reproducible states
                    }
                    const localSearchViewModel = r.data;
                    localSearchViewModel.Form = cleanResult;
                    // Append category info for the catalog if available
                    if (localSearchViewModel.Form
                        && localSearchViewModel.Form.Category
                        && localSearchViewModel.Form.Category.split("|")[1]) {
                        this.getCategoryByKey(localSearchViewModel.Form.Category.split("|")[1]);
                    }
                    if (!localSearchViewModel.Total || localSearchViewModel.Total <= 0) {
                        // A message about no results will show
                        this.breadcrumb.compare = false;
                        this.analyzeSingleCategoryPromise("lots", localSearchViewModel.Form).then(callbacks => {
                            callbacks.goToOtherState(params);
                            callbacks.updateBreadcrumb();
                            callbacks.updateSeoUrl();
                            fullResolve(localSearchViewModel);
                        }).catch(fullReject); // TODO: Error State
                        return;
                    }
                    ////this.cvLotService.bulkGet(localSearchViewModel.ResultIDs).then(lots => {
                        ////if (localSearchViewModel.Form.Sort.toString() === "Relevance") {
                            ////lots.forEach(x => x["Score"] = localSearchViewModel.HitsMetaDataHitScores[x.ID]);
                        ////}
                        ////localSearchViewModel["WithMaps"] = lots;
                        ////// Append inventory info for the catalog
                        ////this.cvStoreLocationService.getUserSelectedStore().then(store => {
                            ////this.hasSelectedStore = true;
                            ////this.userSelectedStore = store;
                        ////}).finally(() => {
                            /* Lots don't have a compare
                            if (this.$state.is(SearchCatalogService.stateLo + ".compare")) {
                                // Don't go anywhere
                                this.breadcrumb.compare = true;
                                fullResolve(localSearchViewModel);
                                return;
                            }
                            */
                            this.breadcrumb.compare = false;
                            this.analyzeSingleCategoryPromise("lots", localSearchViewModel.Form).then(callbacks => {
                                callbacks.goToOtherState(params); // Go to the other state
                                callbacks.updateBreadcrumb();
                                callbacks.updateSeoUrl();
                                fullResolve(localSearchViewModel);
                            }).catch(fullReject); // TODO: Error State
                        ////});
                    ////}).catch(fullReject); // TODO: Error State
                }).catch(fullReject); // TODO: Error State
            });
        }

        refreshPromiseMa(): ng.IPromise<api.ManufacturerSearchViewModel> {
            if (!this.$state.includes("searchCatalog")) {
                if (this.searchIsRunning) {
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                }
                return this.$q.reject("Not on the directory page, don't redirect into it");
            }
            this.lastSearchFailed = false;
            this.cleanForm();
            const params = this.convertFormToStateParams();
            const stringifiedForm = JSON.stringify(params);
            return this.$q((resolve, reject) => {
                const fullReject = (reason): void => {
                    this.lastSearchFailed = true;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    reject(reason);
                };
                const fullResolve = (localSearchViewModel: api.ManufacturerSearchViewModel): void => {
                    this.lastSearchFailed = false;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    this.searchViewModelMa = localSearchViewModel;
                    resolve(this.searchViewModelMa);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.searchComplete);
                    this.searchViewModelMa.CategoriesTree = this.cleanCategoryTree(
                        this.searchViewModelMa.CategoriesTree,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.categoryTreeReady,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.attributes.ready,
                        this.allAttributes);
                };
                if (this.lastUsedSearchFormMa === stringifiedForm) {
                    /* Manufacturers doesn't have a compare
                    if (this.$state.is(SearchCatalogService.stateMa + ".compare")) {
                        this.breadcrumb.compare = true;
                        this.analyzeSingleCategoryPromise("stores", this.searchViewModelMa.Form)
                            .then(() => fullResolve(this.searchViewModelMa)) // Don't go anywhere
                            .catch(fullReject); // TODO: Error State
                        return;
                    }
                    */
                    this.breadcrumb.compare = false;
                    this.analyzeSingleCategoryPromise("manufacturers", this.searchViewModelMa.Form).then(callbacks => {
                        callbacks.goToOtherState(params); // Go to the state
                        callbacks.updateBreadcrumb();
                        callbacks.updateSeoUrl();
                        this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current)
                        fullResolve(this.searchViewModelMa);
                    }).catch(fullReject); // TODO: Error State
                    return;
                }
                // Note the form as we use it for the search
                this.lastUsedSearchFormMa = stringifiedForm;
                // Run the search
                this.searchIsRunning = true;
                // Handle Forced Category
                if (this.forcedCategory) {
                    this.searchViewModelPr.Form.ForcedCategory
                        = this.forcedCategory.DisplayName + "|" + this.forcedCategory.CustomKey
                }
                this.cvApi.providers.SearchManufacturerCatalogWithProvider(this.searchViewModelMa.Form).then(r => {
                    // Check to make sure we didn't change the form between the start of this call and when it resolved
                    const cleanResult = this.cleanFormInner(r.data.Form);
                    const cleanExisting = this.cleanFormInner(this.searchViewModelMa.Form);
                    const cleanResultString = angular.toJson(Object.keys(cleanResult).sort().reduce((r, k) => (r[k] = cleanResult[k], r), {}));
                    const cleanExistingString = angular.toJson(Object.keys(cleanExisting).sort().reduce((r, k) => (r[k] = cleanExisting[k], r), {}));
                    if (cleanExistingString !== cleanResultString) {
                        // It isn't the same, don't assign stuff
                        //return; // BUG: Sometimes this breaks the search, need to get reproducible states
                    }
                    const localSearchViewModel = r.data;
                    localSearchViewModel.Form = cleanResult;
                    // Append category info for the catalog if available
                    if (localSearchViewModel.Form
                        && localSearchViewModel.Form.Category
                        && localSearchViewModel.Form.Category.split("|")[1]) {
                        this.getCategoryByKey(localSearchViewModel.Form.Category.split("|")[1]);
                    }
                    if (!localSearchViewModel.Total || localSearchViewModel.Total <= 0) {
                        // A message about no results will show
                        this.breadcrumb.compare = false;
                        this.analyzeSingleCategoryPromise("manufacturers", localSearchViewModel.Form).then(callbacks => {
                            callbacks.goToOtherState(params);
                            callbacks.updateBreadcrumb();
                            callbacks.updateSeoUrl();
                            fullResolve(localSearchViewModel);
                        }).catch(fullReject); // TODO: Error State
                        return;
                    }
                    ////this.cvManufacturerService.bulkGet(localSearchViewModel.ResultIDs).then(manufacturers => {
                        ////if (localSearchViewModel.Form.Sort.toString() === "Relevance") {
                            ////manufacturers.forEach(x => x["Score"] = localSearchViewModel.HitsMetaDataHitScores[x.ID]);
                        ////}
                        ////localSearchViewModel["WithMaps"] = manufacturers;
                        ////// Append inventory info for the catalog
                        ////this.cvStoreLocationService.getUserSelectedStore().then(store => {
                            ////this.hasSelectedStore = true;
                            ////this.userSelectedStore = store;
                        ////}).finally(() => {
                            /* Manufacturers don't have a compare
                            if (this.$state.is(SearchCatalogService.stateMa + ".compare")) {
                                // Don't go anywhere
                                this.breadcrumb.compare = true;
                                fullResolve(localSearchViewModel);
                                return;
                            }
                            */
                            this.breadcrumb.compare = false;
                            this.analyzeSingleCategoryPromise("manufacturers", localSearchViewModel.Form).then(callbacks => {
                                callbacks.goToOtherState(params); // Go to the other state
                                callbacks.updateBreadcrumb();
                                callbacks.updateSeoUrl();
                                fullResolve(localSearchViewModel);
                            }).catch(fullReject); // TODO: Error State
                        ////});
                    ////}).catch(fullReject); // TODO: Error State
                }).catch(fullReject); // TODO: Error State
            });
        }

        refreshPromisePr(): ng.IPromise<api.ProductSearchViewModel> {
            if (!this.$state.includes("searchCatalog")) {
                if (this.searchIsRunning) {
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                }
                return this.$q.reject("Not on the catalog page, don't redirect into it");
            }
            this.lastSearchFailed = false;
            this.cleanForm();
            const params = this.convertFormToStateParams();
            const stringifiedForm = angular.toJson(params);
            return this.$q((resolve, reject) => {
                const fullReject = (reason): void => {
                    this.lastSearchFailed = true;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    reject(reason);
                };
                const fullResolve = (localSearchViewModel: api.ProductSearchViewModel): void => {
                    this.lastSearchFailed = false;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    this.searchViewModelPr = localSearchViewModel;
                    resolve(this.searchViewModelPr);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.searchComplete);
                    this.searchViewModelPr.CategoriesTree = this.cleanCategoryTree(
                        this.searchViewModelPr.CategoriesTree,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.categoryTreeReady,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.attributes.ready,
                        this.allAttributes);
                };
                if (this.lastUsedSearchFormPr === stringifiedForm) {
                    if (this.$state.is(SearchCatalogService.statePr + ".compare")) {
                        this.breadcrumb.compare = true;
                        this.analyzeSingleCategoryPromise("products", this.searchViewModelPr.Form)
                            .then(() => fullResolve(this.searchViewModelPr)) // Don't go anywhere
                            .catch(fullReject); // TODO: Error State
                        return;
                    }
                    this.breadcrumb.compare = false;
                    this.analyzeSingleCategoryPromise("products", this.searchViewModelPr.Form)
                        .then(callbacks => {
                            callbacks.goToOtherState(params); // Go to the state
                            callbacks.updateBreadcrumb();
                            callbacks.updateSeoUrl();
                            this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current)
                            fullResolve(this.searchViewModelPr);
                        }).catch(fullReject); // TODO: Error State
                    return;
                }
                // Note the form as we use it for the search
                this.lastUsedSearchFormPr = stringifiedForm;
                // Run the search
                this.searchIsRunning = true;
                // Handle Forced Category
                if (this.forcedCategory) {
                    this.searchViewModelPr.Form.ForcedCategory
                        = this.forcedCategory.DisplayName + "|" + this.forcedCategory.CustomKey
                }
                if (this.cefConfig.featureSet.brands.enabled && !this.cefConfig.featureSet.brands.removeBrandCatalogSearchFilter) {
                    this.searchViewModelPr.Form.BrandID = this.$rootScope["globalBrandID"];
                }
                this.cvApi.providers.SearchProductCatalogWithProvider(this.searchViewModelPr.Form).then(r => {
                    // Check to make sure we didn't change the form between the start of this call and when it resolved
                    const cleanResult = this.cleanFormInner(r.data.Form);
                    const cleanExisting = this.cleanFormInner(this.searchViewModelPr.Form);
                    const cleanResultString = angular.toJson(Object.keys(cleanResult).sort().reduce((r, k) => (r[k] = cleanResult[k], r), {}));
                    const cleanExistingString = angular.toJson(Object.keys(cleanExisting).sort().reduce((r, k) => (r[k] = cleanExisting[k], r), {}));
                    if (cleanExistingString !== cleanResultString) {
                        // It isn't the same, don't assign stuff
                        // return; // BUG: Sometimes this breaks the search, need to get reproducible states
                    }
                    const localSearchViewModel = r.data;
                    if (r.data.TotalPages < r.data.Form.Page) {
                        this.searchViewModelPr.Form.Page = 1;
                        resolve(this.refreshPromisePr());
                        return;
                    }
                    localSearchViewModel.Form = cleanResult;
                    // Append category info for the catalog if available
                    if (localSearchViewModel.Form
                        && localSearchViewModel.Form.Category
                        && localSearchViewModel.Form.Category.split("|")[1]) {
                        this.getCategoryByKey(localSearchViewModel.Form.Category.split("|")[1]);
                    }
                    if (!localSearchViewModel.Total || localSearchViewModel.Total <= 0) {
                        // A message about no results will show
                        this.breadcrumb.compare = false;
                        this.analyzeSingleCategoryPromise("products", localSearchViewModel.Form)
                            .then(callbacks => {
                                callbacks.goToOtherState(params);
                                callbacks.updateBreadcrumb();
                                callbacks.updateSeoUrl();
                                fullResolve(localSearchViewModel);
                            }).catch(fullReject); // TODO: Error State
                        return;
                    }
                    // Load product data in the background, operate the rest of the catalog with the known result ids
                    // We don't need to get data here
                    /*
                    this.cvProductService.bulkGet(localSearchViewModel.ResultIDs).then(products => {
                        if (localSearchViewModel.Form.Sort.toString() === "Relevance") {
                            products.forEach(x => x["Score"] = localSearchViewModel.HitsMetaDataHitScores[x.ID]);
                        }
                        ////localSearchViewModel["WithMaps"] = products;
                    });////.catch(fullReject); // TODO: Error State
                    */
                    // Load selected store data in the background, don't wait
                    this.cvStoreLocationService.getUserSelectedStore().then(store => {
                        this.hasSelectedStore = true;
                        this.userSelectedStore = store;
                    });
                    if (this.$state.is(SearchCatalogService.statePr + ".compare")) {
                        // Don't go anywhere
                        this.breadcrumb.compare = true;
                        fullResolve(localSearchViewModel);
                        return;
                    }
                    this.breadcrumb.compare = false;
                    this.analyzeSingleCategoryPromise("products", localSearchViewModel.Form).then(callbacks => {
                        callbacks.goToOtherState(params); // Go to the other state
                        callbacks.updateBreadcrumb();
                        callbacks.updateSeoUrl();
                        fullResolve(localSearchViewModel);
                    }).catch(fullReject); // TODO: Error State
            }).catch(fullReject); // TODO: Error State
            });
        }

        refreshPromiseSt(): ng.IPromise<api.StoreSearchViewModel> {
            if (!this.$state.includes("searchCatalog")) {
                if (this.searchIsRunning) {
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                }
                return this.$q.reject("Not on the directory page, don't redirect into it");
            }
            this.lastSearchFailed = false;
            this.cleanForm();
            const params = this.convertFormToStateParams();
            const stringifiedForm = JSON.stringify(params);
            return this.$q((resolve, reject) => {
                const fullReject = (reason): void => {
                    this.lastSearchFailed = true;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    reject(reason);
                };
                const fullResolve = (localSearchViewModel: api.StoreSearchViewModel): void => {
                    this.lastSearchFailed = false;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    this.searchViewModelSt = localSearchViewModel;
                    resolve(this.searchViewModelSt);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.searchComplete);
                    this.searchViewModelSt.CategoriesTree = this.cleanCategoryTree(
                        this.searchViewModelSt.CategoriesTree,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.categoryTreeReady,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.attributes.ready,
                        this.allAttributes);
                };
                if (this.lastUsedSearchFormSt === stringifiedForm) {
                    /* Stores doesn't have a compare
                    if (this.$state.is(SearchCatalogService.stateSt + ".compare")) {
                        this.breadcrumb.compare = true;
                        this.analyzeSingleCategoryPromise("stores", this.searchViewModelSt.Form)
                            .then(() => fullResolve(this.searchViewModelSt)) // Don't go anywhere
                            .catch(fullReject); // TODO: Error State
                        return;
                    }
                    */
                    this.breadcrumb.compare = false;
                    this.analyzeSingleCategoryPromise("stores", this.searchViewModelSt.Form)
                        .then(callbacks => {
                            callbacks.goToOtherState(params); // Go to the state
                            callbacks.updateBreadcrumb();
                            callbacks.updateSeoUrl();
                            this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current)
                            fullResolve(this.searchViewModelSt);
                        }).catch(fullReject); // TODO: Error State
                    return;
                }
                // Note the form as we use it for the search
                this.lastUsedSearchFormSt = stringifiedForm;
                // Run the search
                this.searchIsRunning = true;
                // Handle Forced Category
                if (this.forcedCategory) {
                    this.searchViewModelPr.Form.ForcedCategory
                        = this.forcedCategory.DisplayName + "|" + this.forcedCategory.CustomKey
                }
                this.cvApi.providers.SearchStoreCatalogWithProvider(this.searchViewModelSt.Form).then(r => {
                    // Check to make sure we didn't change the form between the start of this call and when it resolved
                    const cleanResult = this.cleanFormInner(r.data.Form);
                    const cleanExisting = this.cleanFormInner(this.searchViewModelSt.Form);
                    const cleanResultString = angular.toJson(Object.keys(cleanResult).sort().reduce((r, k) => (r[k] = cleanResult[k], r), {}));
                    const cleanExistingString = angular.toJson(Object.keys(cleanExisting).sort().reduce((r, k) => (r[k] = cleanExisting[k], r), {}));
                    if (cleanExistingString !== cleanResultString) {
                        // It isn't the same, don't assign stuff
                        //return; // BUG: Sometimes this breaks the search, need to get reproducible states
                    }
                    const localSearchViewModel = r.data;
                    localSearchViewModel.Form = cleanResult;
                    // Append category info for the catalog if available
                    if (localSearchViewModel.Form
                        && localSearchViewModel.Form.Category
                        && localSearchViewModel.Form.Category.split("|")[1]) {
                        this.getCategoryByKey(localSearchViewModel.Form.Category.split("|")[1]);
                    }
                    if (!localSearchViewModel.Total || localSearchViewModel.Total <= 0) {
                        // A message about no results will show
                        this.breadcrumb.compare = false;
                        this.analyzeSingleCategoryPromise("stores", localSearchViewModel.Form)
                            .then(callbacks => {
                                callbacks.goToOtherState(params);
                                callbacks.updateBreadcrumb();
                                callbacks.updateSeoUrl();
                                fullResolve(localSearchViewModel);
                            }).catch(fullReject); // TODO: Error State
                        return;
                    }
                    ////this.cvStoreService.bulkGet(localSearchViewModel.ResultIDs).then(stores => {
                        ////if (localSearchViewModel.Form.Sort.toString() === "Relevance") {
                            ////stores.forEach(x => x["Score"] = localSearchViewModel.HitsMetaDataHitScores[x.ID]);
                        ////}
                        ////localSearchViewModel["WithMaps"] = stores;
                        ////// Append inventory info for the catalog
                        ////this.cvStoreLocationService.getUserSelectedStore().then(store => {
                            ////this.hasSelectedStore = true;
                            ////this.userSelectedStore = store;
                        ////}).finally(() => {
                            /* Stores don't have a compare
                            if (this.$state.is(SearchCatalogService.stateSt + ".compare")) {
                                // Don't go anywhere
                                this.breadcrumb.compare = true;
                                fullResolve(localSearchViewModel);
                                return;
                            }
                            */
                            this.breadcrumb.compare = false;
                            this.analyzeSingleCategoryPromise("stores", localSearchViewModel.Form).then(callbacks => {
                                callbacks.goToOtherState(params); // Go to the other state
                                callbacks.updateBreadcrumb();
                                callbacks.updateSeoUrl();
                                fullResolve(localSearchViewModel);
                            }).catch(fullReject); // TODO: Error State
                        ////});
                    ////}).catch(fullReject); // TODO: Error State
                }).catch(fullReject); // TODO: Error State
            });
        }

        refreshPromiseVe(): ng.IPromise<api.VendorSearchViewModel> {
            if (!this.$state.includes("searchCatalog")) {
                if (this.searchIsRunning) {
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                }
                return this.$q.reject("Not on the directory page, don't redirect into it");
            }
            const kind = "vendors";
            this.lastSearchFailed = false;
            this.cleanForm();
            const params = this.convertFormToStateParams();
            const stringifiedForm = JSON.stringify(params);
            return this.$q((resolve, reject) => {
                const fullReject = (reason): void => {
                    this.lastSearchFailed = true;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    reject(reason);
                };
                const fullResolve = (localSearchViewModel: api.VendorSearchViewModel): void => {
                    this.lastSearchFailed = false;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    this.searchViewModelVe = localSearchViewModel;
                    resolve(this.searchViewModelVe);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.searchComplete);
                    this.searchViewModelVe.CategoriesTree = this.cleanCategoryTree(
                        this.searchViewModelVe.CategoriesTree,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.categoryTreeReady,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.attributes.ready,
                        this.allAttributes);
                };
                if (this.lastUsedSearchFormVe === stringifiedForm) {
                    /* Vendors doesn't have a compare
                    if (this.$state.is(SearchCatalogService.stateVe + ".compare")) {
                        this.breadcrumb.compare = true;
                        this.analyzeSingleCategoryPromise(kind, this.searchViewModelVe.Form)
                            .then(() => fullResolve(this.searchViewModelVe)) // Don't go anywhere
                            .catch(fullReject); // TODO: Error State
                        return;
                    }
                    */
                    this.breadcrumb.compare = false;
                    this.analyzeSingleCategoryPromise(kind, this.searchViewModelVe.Form).then(callbacks => {
                        callbacks.goToOtherState(params); // Go to the state
                        callbacks.updateBreadcrumb();
                        callbacks.updateSeoUrl();
                        this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current)
                        fullResolve(this.searchViewModelVe);
                    }).catch(fullReject); // TODO: Error State
                    return;
                }
                // Note the form as we use it for the search
                this.lastUsedSearchFormVe = stringifiedForm;
                // Run the search
                this.searchIsRunning = true;
                // Handle Forced Category
                if (this.forcedCategory) {
                    this.searchViewModelPr.Form.ForcedCategory
                        = this.forcedCategory.DisplayName + "|" + this.forcedCategory.CustomKey
                }
                this.cvApi.providers.SearchVendorCatalogWithProvider(this.searchViewModelVe.Form).then(r => {
                    // Check to make sure we didn't change the form between the start of this call and when it resolved
                    const cleanResult = this.cleanFormInner(r.data.Form);
                    const cleanExisting = this.cleanFormInner(this.searchViewModelVe.Form);
                    const cleanResultString = angular.toJson(Object.keys(cleanResult).sort().reduce((r, k) => (r[k] = cleanResult[k], r), {}));
                    const cleanExistingString = angular.toJson(Object.keys(cleanExisting).sort().reduce((r, k) => (r[k] = cleanExisting[k], r), {}));
                    if (cleanExistingString !== cleanResultString) {
                        // It isn't the same, don't assign stuff
                        //return; // BUG: Sometimes this breaks the search, need to get reproducible states
                    }
                    const localSearchViewModel = r.data;
                    localSearchViewModel.Form = cleanResult;
                    // Append category info for the catalog if available
                    if (localSearchViewModel.Form
                        && localSearchViewModel.Form.Category
                        && localSearchViewModel.Form.Category.split("|")[1]) {
                        this.getCategoryByKey(localSearchViewModel.Form.Category.split("|")[1]);
                    }
                    if (!localSearchViewModel.Total || localSearchViewModel.Total <= 0) {
                        // A message about no results will show
                        this.breadcrumb.compare = false;
                        this.analyzeSingleCategoryPromise(kind, localSearchViewModel.Form).then(callbacks => {
                            callbacks.goToOtherState(params);
                            callbacks.updateBreadcrumb();
                            callbacks.updateSeoUrl();
                            fullResolve(localSearchViewModel);
                        }).catch(fullReject); // TODO: Error State
                        return;
                    }
                    this.cvVendorService.bulkGet(localSearchViewModel.ResultIDs).then(vendors => {
                        if (localSearchViewModel.Form.Sort.toString() === "Relevance") {
                            vendors.forEach(x => x["Score"] = localSearchViewModel.HitsMetaDataHitScores[x.ID]);
                        }
                        ////localSearchViewModel["WithMaps"] = vendors;
                        ////// Append inventory info for the catalog
                        ////this.cvStoreLocationService.getUserSelectedStore().then(store => {
                        ////    this.hasSelectedStore = true;
                        ////    this.userSelectedStore = store;
                        ////}).finally(() => {
                            /* Vendors don't have a compare
                            if (this.$state.is(SearchCatalogService.stateVe + ".compare")) {
                                // Don't go anywhere
                                this.breadcrumb.compare = true;
                                fullResolve(localSearchViewModel);
                                return;
                            }
                            */
                            this.breadcrumb.compare = false;
                            this.analyzeSingleCategoryPromise(kind, localSearchViewModel.Form).then(callbacks => {
                                callbacks.goToOtherState(params); // Go to the other state
                                callbacks.updateBreadcrumb();
                                callbacks.updateSeoUrl();
                                fullResolve(localSearchViewModel);
                            }).catch(fullReject); // TODO: Error State
                        ////});
                    }).catch(fullReject); // TODO: Error State
                }).catch(fullReject); // TODO: Error State
            });
        }

        private refreshPromiseInner<TSVM extends api.SearchViewModelBase<api.SearchFormBase, api.IndexableModelBase>>(kind: string): ng.IPromise<TSVM> {
            if (!this.$state.includes("searchCatalog")) {
                if (this.searchIsRunning) {
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                }
                return this.$q.reject("Not on the directory page, don't redirect into it");
            }
            this.lastSearchFailed = false;
            this.cleanForm();
            const params = this.convertFormToStateParams();
            const stringifiedForm = JSON.stringify(params);
            return this.$q((resolve, reject) => {
                const fullReject = (reason: any): void => {
                    this.lastSearchFailed = true;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    reject(reason);
                };
                const fullResolve = (localSearchViewModel: TSVM): void => {
                    this.lastSearchFailed = false;
                    this.searchIsRunning = false;
                    this.$anchorScroll("top");
                    this.setSearchViewModel(localSearchViewModel, kind);
                    resolve(this.getSearchViewModel(kind));
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.searchComplete);
                    this.getSearchViewModel(kind).CategoriesTree = this.cleanCategoryTree(
                        this.getSearchViewModel(kind).CategoriesTree,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.categoryTreeReady,
                        this.allCategories);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.attributes.ready,
                        this.allAttributes);
                };
                if (this.lastUsedSearchFormVe === stringifiedForm) {
                    /* Only products have a compare view
                    if (this.$state.is(SearchCatalogService.statePr + ".compare")) {
                        this.breadcrumb.compare = true;
                        this.analyzeSingleCategoryPromise(kind, this.getSearchViewModel(kind).Form)
                            .then(() => fullResolve(this.getSearchViewModel(kind))) // Don't go anywhere
                            .catch(fullReject); // TODO: Error State
                        return;
                    }
                    */
                    this.breadcrumb.compare = false;
                    this.analyzeSingleCategoryPromise(kind, this.getSearchViewModel(kind).Form).then(callbacks => {
                        callbacks.goToOtherState(params); // Go to the state
                        callbacks.updateBreadcrumb();
                        callbacks.updateSeoUrl();
                        this.applyStateParamsToForm(this.cleanParams(this.$stateParams), this.$state.current)
                        fullResolve(this.getSearchViewModel(kind) as TSVM);
                    }).catch(fullReject); // TODO: Error State
                    return;
                }
                // Note the form as we use it for the search
                this.lastUsedSearchFormVe = stringifiedForm;
                // Run the search
                this.searchIsRunning = true;
                // Handle Forced Category
                if (this.forcedCategory) {
                    this.searchViewModelPr.Form.ForcedCategory
                        = this.forcedCategory.Name + "|" + this.forcedCategory.CustomKey
                }
                this.cvApi.providers.SearchVendorCatalogWithProvider(this.getSearchViewModel(kind).Form).then(r => {
                    // Check to make sure we didn't change the form between the start of this call and when it resolved
                    const cleanResult = this.cleanFormInner(r.data.Form);
                    const cleanExisting = this.cleanFormInner(this.getSearchViewModel(kind).Form);
                    const cleanResultString = angular.toJson(Object.keys(cleanResult).sort().reduce((r, k) => (r[k] = cleanResult[k], r), {}));
                    const cleanExistingString = angular.toJson(Object.keys(cleanExisting).sort().reduce((r, k) => (r[k] = cleanExisting[k], r), {}));
                    if (cleanExistingString !== cleanResultString) {
                        // It isn't the same, don't assign stuff
                        //return; // BUG: Sometimes this breaks the search, need to get reproducible states
                    }
                    const localSearchViewModel = r.data;
                    localSearchViewModel.Form = cleanResult;
                    // Append category info for the catalog if available
                    if (localSearchViewModel.Form
                        && localSearchViewModel.Form.Category
                        && localSearchViewModel.Form.Category.split("|")[1]) {
                        this.getCategoryByKey(localSearchViewModel.Form.Category.split("|")[1]);
                    }
                    if (!localSearchViewModel.Total || localSearchViewModel.Total <= 0) {
                        // A message about no results will show
                        this.breadcrumb.compare = false;
                        this.analyzeSingleCategoryPromise(kind, localSearchViewModel.Form).then(callbacks => {
                            callbacks.goToOtherState(params);
                            callbacks.updateBreadcrumb();
                            callbacks.updateSeoUrl();
                            fullResolve(localSearchViewModel as TSVM);
                        }).catch(fullReject); // TODO: Error State
                        return;
                    }
                    this.cvVendorService.bulkGet(localSearchViewModel.ResultIDs).then(records => {
                        if (localSearchViewModel.Form.Sort.toString() === "Relevance") {
                            records.forEach(x => x["Score"] = localSearchViewModel.HitsMetaDataHitScores[x.ID]);
                        }
                        ////localSearchViewModel["WithMaps"] = records;
                        ////// Only Products have inventory info to append
                        ////this.cvStoreLocationService.getUserSelectedStore().then(store => {
                        ////    this.hasSelectedStore = true;
                        ////    this.userSelectedStore = store;
                        ////}).finally(() => {
                            /* Only products have a compare view
                            if (this.$state.is(SearchCatalogService.statePr + ".compare")) {
                                // Don't go anywhere
                                this.breadcrumb.compare = true;
                                fullResolve(localSearchViewModel);
                                return;
                            }
                            */
                            this.breadcrumb.compare = false;
                            this.analyzeSingleCategoryPromise(kind, localSearchViewModel.Form).then(callbacks => {
                                callbacks.goToOtherState(params); // Go to the other state
                                callbacks.updateBreadcrumb();
                                callbacks.updateSeoUrl();
                                fullResolve(localSearchViewModel as TSVM);
                            }).catch(fullReject); // TODO: Error State
                        ////});
                    }).catch(fullReject); // TODO: Error State
                }).catch(fullReject); // TODO: Error State
            });
        }

        getSuggestionsPromise(): ng.IPromise<api.SuggestResultBase[]> {
            if (!this.queryTerm || !this.queryTerm.trim()) {
                this.suggestions = [];
                this.suggestionsIsRunning = false;
                return this.$q.resolve([]);
            }
            return this.$q((resolve, reject) => {
                const dto = <api.SuggestProductCatalogWithProviderDto>{
                    Query: this.queryTerm,
                    Page: 1,
                    PageSize: 8,
                    PageSetSize: 1,
                    Sort: api.SearchSort.Relevance,
                    StoreID: this.activeSearchViewModel.Form.StoreID
                        || this.cefConfig.catalog.onlyApplyStoreToFilterByUI
                        ? this.filterByStoreID
                        : this.cvStoreLocationService.getUsersSelectedStore()
                            && this.cvStoreLocationService.getUsersSelectedStore().ID,
                };
                this.suggestionsIsRunning = true;
                this.cvApi.providers.SuggestProductCatalogWithProvider(dto).then(r => {
                    this.suggestions = r.data;
                    this.suggestionsIsRunning = false;
                    resolve(this.suggestions);
                }).catch(reject)
                .finally(() => this.suggestionsIsRunning = false);
            });
        }

        pushRangeKeyPr(mode: string, rangeKey: string): void {
            if (!rangeKey) { return; }
            if (!this.activeSearchViewModel.Form.PricingRanges) {
                this.activeSearchViewModel.Form.PricingRanges = [];
            } else if (this.activeSearchViewModel.Form.PricingRanges.indexOf(rangeKey) !== -1) {
                return;
            }
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.PricingRanges.push(rangeKey);
        }

        popRangeKeyPr(mode: string, rangeKey: string): void {
            if (!rangeKey) { return; }
            const index = this.activeSearchViewModel.Form.PricingRanges.indexOf(rangeKey);
            if (index === -1) { return; }
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.PricingRanges.splice(index, 1);
        }

        clearRangeKeyPr(mode: string): void {
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.PricingRanges = [];
        }

        pushRangeKeyRa(mode: string, rangeKey: string): void {
            if (!rangeKey) { return; }
            if (!this.activeSearchViewModel.Form.RatingRanges) {
                this.activeSearchViewModel.Form.RatingRanges = [];
            } else if (this.activeSearchViewModel.Form.RatingRanges.indexOf(rangeKey) !== -1) {
                return;
            }
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.RatingRanges.push(rangeKey);
        }

        popRangeKeyRa(mode: string, rangeKey: string): void {
            if (!rangeKey) { return; }
            const index = this.activeSearchViewModel.Form.RatingRanges.indexOf(rangeKey);
            if (index === -1) { return; }
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.RatingRanges.splice(index, 1);
        }

        clearRangeKeyRa(mode: string): void {
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.RatingRanges = [];
        }

        pushAttributeOptionKey(mode: string, attribute: string, option: string): void {
            if (!attribute || !option) { return; }
            const theMode = mode || "multi-all";
            switch (theMode) {
                case "multi-any": {
                    if (!this.activeSearchViewModel.Form.AttributesAny) {
                        this.activeSearchViewModel.Form.AttributesAny = new cefalt.store.Dictionary<string[]>();
                    }
                    if (!this.activeSearchViewModel.Form.AttributesAny[attribute]) {
                        this.activeSearchViewModel.Form.AttributesAny[attribute] = [];
                    } else if (this.activeSearchViewModel.Form.AttributesAny[attribute].indexOf(option) !== -1) {
                        return;
                    }
                    this.searchIsRunning = true;
                    this.activeSearchViewModel.Form.AttributesAny[attribute].push(option);
                    break;
                }
                case "multi-all": {
                    if (!this.activeSearchViewModel.Form.AttributesAll) {
                        this.activeSearchViewModel.Form.AttributesAll = new cefalt.store.Dictionary<string[]>();
                    }
                    if (!this.activeSearchViewModel.Form.AttributesAll[attribute]) {
                        this.activeSearchViewModel.Form.AttributesAll[attribute] = [];
                    } else if (this.activeSearchViewModel.Form.AttributesAll[attribute].indexOf(option) !== -1) {
                        return;
                    }
                    this.searchIsRunning = true;
                    this.activeSearchViewModel.Form.AttributesAll[attribute].push(option);
                    break;
                }
            }
        }

        popAttributeOptionKey(mode: string, attribute: string, option: string): void {
            if (!attribute || !option) { return; }
            const theMode = mode || "multi-all";
            switch (theMode) {
                case "multi-any": {
                    this.searchIsRunning = true;
                    if (!this.activeSearchViewModel.Form.AttributesAny
                        || !this.activeSearchViewModel.Form.AttributesAny[attribute]) {
                        return;
                    }
                    const index = this.activeSearchViewModel.Form.AttributesAny[attribute].indexOf(option);
                    if (index === -1) { return; }
                    this.activeSearchViewModel.Form.AttributesAny[attribute].splice(index, 1);
                    if (this.activeSearchViewModel.Form.AttributesAny[attribute].length <= 0) {
                        // List is empty, remove the parent
                        delete this.activeSearchViewModel.Form.AttributesAny[attribute];
                    }
                    break;
                }
                case "multi-all": {
                    this.searchIsRunning = true;
                    if (!this.activeSearchViewModel.Form.AttributesAll
                        || !this.activeSearchViewModel.Form.AttributesAll[attribute]) {
                        return;
                    }
                    const index = this.activeSearchViewModel.Form.AttributesAll[attribute].indexOf(option);
                    if (index === -1) { return; }
                    this.activeSearchViewModel.Form.AttributesAll[attribute].splice(index, 1);
                    if (this.activeSearchViewModel.Form.AttributesAll[attribute].length <= 0) {
                        // List is empty, remove the parent
                        delete this.activeSearchViewModel.Form.AttributesAll[attribute];
                    }
                    break;
                }
            }
        }

        clearAttributeOption(mode: string, attribute: string): void {
            if (!attribute) { return; }
            const theMode = mode || "multi-all"
            switch (theMode) {
                case "multi-any": {
                    if (!this.activeSearchViewModel.Form.AttributesAny
                        || !this.activeSearchViewModel.Form.AttributesAny[attribute]) {
                        return;
                    }
                    this.searchIsRunning = true;
                    this.activeSearchViewModel.Form.AttributesAny[attribute] = [];
                    if (this.activeSearchViewModel.Form.AttributesAny[attribute].length <= 0) {
                        // List is empty, remove the parent
                        delete this.activeSearchViewModel.Form.AttributesAny[attribute];
                    }
                    this.searchIsRunning = false;
                    break;
                }
                case "multi-all": {
                    if (!this.activeSearchViewModel.Form.AttributesAll
                        || !this.activeSearchViewModel.Form.AttributesAll[attribute]) {
                        return;
                    }
                    this.searchIsRunning = true;
                    this.activeSearchViewModel.Form.AttributesAll[attribute] = [];
                    if (this.activeSearchViewModel.Form.AttributesAll[attribute].length <= 0) {
                        // List is empty, remove the parent
                        delete this.activeSearchViewModel.Form.AttributesAll[attribute];
                    }
                    this.searchIsRunning = true;
                    break;
                }
            }
        }

        pushBrandName(brandName: string): void {
            if (!brandName) { return; }
                if (this.activeSearchViewModel.Form["BrandName"] === brandName) {
                    return;
                }
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form["BrandName"] = brandName;
        }

        popBrandName(): void {
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form["BrandName"] = undefined;
        }

        pushType(type: api.IndexableTypeModel): void {
            if (!type) { return; }
            if (this.activeSearchViewModel.Form.TypeID === type.ID) {
                return;
            }
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.TypeID = type.ID;
        }

        popType(): void {
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.TypeID = undefined;
        }

        pushDistrict(district: api.IndexableDistrictModel): void {
            if (!district) { return; }
            if (this.activeSearchViewModel.Form.DistrictID === district.ID) {
                return;
            }
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.DistrictID = district.ID;
        }

        popDistrict(): void {
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.DistrictID = undefined;
        }

        pushRegion(region: api.IndexableRegionModel): void {
            if (!region) { return; }
            if (this.activeSearchViewModel.Form.RegionID === region.ID) {
                return;
            }
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.RegionID = region.ID;
        }

        popRegion(): void {
            this.searchIsRunning = true;
            this.activeSearchViewModel.Form.RegionID = undefined;
        }

        pushCategory(mode: string, category: string): void {
            if (!category) { return; }
            const theMode = mode || "single";
            switch (theMode) {
                case "single": {
                    if (this.activeSearchViewModel.Form.Category === category) {
                        return;
                    }
                    this.searchIsRunning = true;
                    this.activeSearchViewModel.Form.Page = 1;
                    this.activeSearchViewModel.Form.Category = category;
                    break;
                }
                case "multi-any": {
                    if (!this.activeSearchViewModel.Form.CategoriesAny) {
                        this.activeSearchViewModel.Form.CategoriesAny = [];
                    } else if (this.activeSearchViewModel.Form.CategoriesAny.indexOf(category) !== -1) {
                        return;
                    }
                    this.searchIsRunning = true;
                    this.activeSearchViewModel.Form.CategoriesAny.push(category);
                    break;
                }
                case "multi-all": {
                    if (!this.activeSearchViewModel.Form.CategoriesAll) {
                        this.activeSearchViewModel.Form.CategoriesAll = [];
                    } else if (this.activeSearchViewModel.Form.CategoriesAll.indexOf(category) !== -1) {
                        return;
                    }
                    this.searchIsRunning = true;
                    this.activeSearchViewModel.Form.CategoriesAll.push(category);
                    break;
                }
            }
        }

        popCategory(mode: string, category: string = undefined): void {
            this.searchIsRunning = true;
            const theMode = mode || "single";
            switch (theMode) {
                case "single": {
                    if (angular.isUndefined(this.activeSearchViewModel.Form.Category)) {
                        return;
                    }
                    this.searchIsRunning = true;
                    this.activeSearchViewModel.Form.Page = 1;
                    this.activeSearchViewModel.Form.Category = undefined;
                    break;
                }
                case "multi-any": {
                    if (!category) { return; }
                    const indexAny = this.activeSearchViewModel.Form.CategoriesAny.indexOf(category);
                    if (indexAny === -1) { return; }
                    this.searchIsRunning = true;
                    this.activeSearchViewModel.Form.CategoriesAny.splice(indexAny, 1);
                    break;
                }
                case "multi-all": {
                    if (!category) { return; }
                    const indexAll = this.activeSearchViewModel.Form.CategoriesAll.indexOf(category);
                    if (indexAll === -1) { return; }
                    this.searchIsRunning = true;
                    this.activeSearchViewModel.Form.CategoriesAll.splice(indexAll, 1);
                    break;
                }
            }
        }

        clearAllFilters(): void {
            this.suspendRefresh = true;
            this.activeSearchViewModel.Form.AttributesAny = new cefalt.store.Dictionary<Array<string>>();
            this.activeSearchViewModel.Form.AttributesAll = new cefalt.store.Dictionary<Array<string>>();
            this.activeSearchViewModel.Form.BrandIDsAny = undefined;
            this.activeSearchViewModel.Form.BrandIDsAll = undefined;
            this.activeSearchViewModel.Form.BrandID = undefined;
            this.activeSearchViewModel.Form["BrandName"] = undefined;
            this.activeSearchViewModel.Form.CategoriesAny = undefined;
            this.activeSearchViewModel.Form.CategoriesAll = undefined;
            this.activeSearchViewModel.Form.Category = undefined;
            this.activeSearchViewModel.Form["City"] = undefined;
            this.activeSearchViewModel.Form["FilterByCurrentAccountRoles"] = undefined;
            this.activeSearchViewModel.Form.FranchiseIDsAny = undefined;
            this.activeSearchViewModel.Form.FranchiseIDsAll = undefined;
            this.activeSearchViewModel.Form.FranchiseID = undefined;
            this.activeSearchViewModel.Form.ManufacturerIDsAny = undefined;
            this.activeSearchViewModel.Form.ManufacturerIDsAll = undefined;
            this.activeSearchViewModel.Form.ManufacturerID = undefined;
            this.activeSearchViewModel.Form.OnHand = undefined;
            this.activeSearchViewModel.Form["Name"] = undefined;
            this.activeSearchViewModel.Form.PricingRanges = undefined;
            this.activeSearchViewModel.Form.ProductIDsAny = undefined;
            this.activeSearchViewModel.Form.ProductIDsAll = undefined;
            this.activeSearchViewModel.Form.ProductID = undefined;
            this.activeSearchViewModel.Form.Query = undefined;
            this.activeSearchViewModel.Form.RatingRanges = undefined;
            this.activeSearchViewModel.Form.StoreIDsAny = undefined;
            this.activeSearchViewModel.Form.StoreIDsAll = undefined;
            this.activeSearchViewModel.Form.StoreID = undefined;
            this.activeSearchViewModel.Form.TypeIDsAny = undefined;
            this.activeSearchViewModel.Form.TypeID = undefined;
            this.activeSearchViewModel.Form.VendorIDsAny = undefined;
            this.activeSearchViewModel.Form.VendorIDsAll = undefined;
            this.activeSearchViewModel.Form.VendorID = undefined;
            this.activeSearchViewModel.Form.DistrictIDsAny = undefined;
            this.activeSearchViewModel.Form.DistrictIDsAll = undefined;
            this.activeSearchViewModel.Form.DistrictID = undefined;
            this.activeSearchViewModel.Form.RegionIDsAny = undefined;
            this.activeSearchViewModel.Form.RegionID = undefined;
            this.suspendRefresh = false;
            this.$rootScope.$broadcast(this.cvServiceStrings.events.catalog.change, "All", "Reset");
        }

        countAppliedFilters(): number {
            let appliedFilters = 0;
            if (this.activeSearchViewModel.Form) {
                let objSize = o => Object.keys(o).length;
                if (this.activeSearchViewModel.Form.AttributesAll) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.AttributesAll);
                }
                if (this.activeSearchViewModel.Form.AttributesAny) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.AttributesAny);
                }
                if (this.activeSearchViewModel.Form.CategoriesAll) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.CategoriesAll);
                }
                if (this.activeSearchViewModel.Form.CategoriesAny) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.CategoriesAny);
                }
                if (!this.activeSearchViewModel.Form.CategoriesAll
                    && !this.activeSearchViewModel.Form.CategoriesAny
                    && this.activeSearchViewModel.Form.Category) {
                    appliedFilters++;
                }
                if (this.activeSearchViewModel.Form.ManufacturerIDsAll) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.ManufacturerIDsAll);
                }
                if (this.activeSearchViewModel.Form.ManufacturerIDsAny) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.ManufacturerIDsAny);
                }
                if (!this.activeSearchViewModel.Form.ManufacturerIDsAll
                    && !this.activeSearchViewModel.Form.ManufacturerIDsAny
                    && this.activeSearchViewModel.Form.ManufacturerID) {
                    appliedFilters++;
                }
                if (this.activeSearchViewModel.Form.PricingRanges) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.PricingRanges);
                }
                if (this.activeSearchViewModel.Form.ProductIDsAll) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.ProductIDsAll);
                }
                if (this.activeSearchViewModel.Form.ProductIDsAny) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.ProductIDsAny);
                }
                if (!this.activeSearchViewModel.Form.ProductIDsAll
                    && !this.activeSearchViewModel.Form.ProductIDsAny
                    && this.activeSearchViewModel.Form.ProductID) {
                    appliedFilters++;
                }
                if (this.activeSearchViewModel.Form.Query) {
                    appliedFilters++
                }
                if (this.activeSearchViewModel.Form.RatingRanges) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.RatingRanges);
                }
                if (this.activeSearchViewModel.Form.StoreIDsAll) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.StoreIDsAll);
                }
                if (this.activeSearchViewModel.Form.StoreIDsAny) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.StoreIDsAny);
                }
                if (!this.activeSearchViewModel.Form.StoreIDsAll
                    && !this.activeSearchViewModel.Form.StoreIDsAny
                    && this.activeSearchViewModel.Form.StoreID) {
                    appliedFilters++;
                }
                if (this.activeSearchViewModel.Form.VendorIDsAll) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.VendorIDsAll);
                }
                if (this.activeSearchViewModel.Form.VendorIDsAny) {
                    appliedFilters += objSize(this.activeSearchViewModel.Form.VendorIDsAny);
                }
                if (!this.activeSearchViewModel.Form.VendorIDsAll
                    && !this.activeSearchViewModel.Form.VendorIDsAny
                    && this.activeSearchViewModel.Form.VendorID) {
                    appliedFilters++;
                }
            }
            return appliedFilters;
        }

        pricingSortAsc = (x: /*api.LotModel |*/ number): string | number | boolean => {
            if (angular.isUndefined(x) || x === null) { return null; }
            let product = this.cvProductService.getCached({ id: x });
            if (product == null) {
                // product is loading
                return false;
            }
            if (!angular.isFunction(product.readPrices)) {
                this.cvPricingService.factoryAssign(product);
            }
            if (angular.isFunction(product.readPrices)) {
                return product.readPrices().haveSale ? product.readPrices().sale : product.readPrices().base;
            }
            return product.Name;
        }

        pricingSortDesc = (x: /*api.LotModel |*/ number): string | number | boolean => {
            if (angular.isUndefined(x) || x === null) { return null; }
            let product = this.cvProductService.getCached({ id: x });
            if (product == null) {
                // product is loading
                return false;
            }
            if (!angular.isFunction(product.readPrices)) {
                this.cvPricingService.factoryAssign(product);
            }
            if (angular.isFunction(product.readPrices)) {
                return (product.readPrices().haveSale ? product.readPrices().sale : product.readPrices().base) * -1;
            }
            return product.Name;
        }

        ratingSortAsc = (x: number): string | number | boolean => {
            if (angular.isUndefined(x) || x === null) { return null; }
            let product = this.cvProductService.getCached({ id: x });
            if (product == null) {
                // product is loading
                return false;
            }
            if (!angular.isFunction(product.readRatings)) {
                this.cvProductReviewsService.factoryAssign(product);
            }
            if (angular.isFunction(product.readRatings)) {
                return product.readRatings().value;
            }
            return product.Name;
        }

        ratingSortDesc = (x: number): string | number | boolean => {
            if (angular.isUndefined(x) || x === null) { return null; }
            let product = this.cvProductService.getCached({ id: x });
            if (product == null) {
                // product is loading
                return false;
            }
            if (!angular.isFunction(product.readRatings)) {
                this.cvProductReviewsService.factoryAssign(product);
            }
            if (angular.isFunction(product.readRatings)) {
                return product.readRatings().value * -1;
            }
            return product.Name;
        }

        definedSort = (x: number): string | number | boolean => {
            if (angular.isUndefined(x) || x === null) { return null; }
            let product = this.cvProductService.getCached({ id: x });
            if (product == null) {
                return false;
            }
            if (!angular.isFunction(product.readSortOrder)) {
                this.cvProductService.factoryAssign(product);
            }
            if (angular.isFunction(product.readSortOrder)) {
                return product.readSortOrder().order;
            }
            return product.Name;
        }

        getSorter(): string | string[] | Function {
            if (!this.activeSearchViewModel) {
                return "Name";
            }
            switch (this.activeSearchViewModel.Form.Sort as any as string) {
                case "Relevance": { return ["-Score", "Name"]; }
                case "Popular": { return ["TotalPurchasedQuantity","Name"]; }
                case "Recent": { return ["-CreatedDate","Name"]; }
                case "NameAscending": { return "Name"; }
                case "NameDescending": { return "-Name"; }
                case "PricingAscending": { return this.pricingSortAsc; }
                case "PricingDescending": { return this.pricingSortDesc; }
                case "RatingAscending": { return this.ratingSortAsc; }
                case "RatingDescending": { return this.ratingSortDesc; }
                case "Defined": { return this.definedSort; }
                case "ClosingSoon": { return ["ClosesAt","Name"]; }
            }
            return "Name";
        }

        mapTypeIDToTypeName(id: number): string {
            if (!id || !this.activeSearchViewModel.Types) {
                return null;
            }
            let name = "";
            Object.keys(this.activeSearchViewModel.Types).forEach(key => {
                if (id === this.activeSearchViewModel.Types[key].ID) {
                    name = key;
                }
            });
            if (name === "") {
                name = "N/A";
            }
            return name;
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $rootScope: ng.IRootScopeService,
                readonly $location: ng.ILocationService,
                private readonly $window: ng.IWindowService,
                private readonly $q: ng.IQService,
                private readonly $state: ng.ui.IStateService,
                private readonly $stateParams: ng.ui.IStateParamsServiceForSearchCatalog,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $anchorScroll: ng.IAnchorScrollService,
                private readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvAuctionService: services.IAuctionService,
                private readonly cvCategoryService: services.ICategoryService,
                private readonly cvFranchiseService: services.IFranchiseService,
                private readonly cvLotService: services.ILotService,
                private readonly cvManufacturerService: services.IManufacturerService,
                private readonly cvProductService: services.IProductService,
                private readonly cvStoreService: services.IStoreService,
                private readonly cvVendorService: services.IVendorService,
                private readonly cvStoreLocationService: services.IStoreLocationService,
                private readonly cvInventoryService: services.IInventoryService,
                private readonly cvPricingService: services.IPricingService,
                private readonly cvProductReviewsService: services.IProductReviewsService) {
            $rootScope.$location = $location;
            if (this.cefConfig.featureSet.stores.enabled) {
                this.cvStoreLocationService.getUserSelectedStore()
                    .finally(() => this.load(cefConfig));
            } else {
                this.load(cefConfig);
            }
        }
    }
}
