module cef.store.core {
    export class ServerSidePaging<
            TRecord extends api.BaseModel,
            TPagedRecord extends api.PagedResultsBase<TRecord>> {
        // Data
        protected _totalCount = 0;
        protected _filteredCount = null;
        protected _quickFilter: string = "";
        protected _pageSize = 8;
        protected _data: { [page: number]: TRecord[] } = { };
        protected _dataUnpaged: TRecord[] = [];
        protected _currentPage = 0;
        protected _cachedFilteredData: { [page: number]: TRecord[] } = null;
        protected _cachedAllPages: number[] = null;
        pagesLoaded: number[] = [];
        get totalCount(): number {
            return this._totalCount;
        }
        get filteredCount(): number {
            if (!this._filteredCount) {
                this.filteredData; // Fills the value
            }
            return this._filteredCount;
        }
        get pageSize(): number {
            return this._pageSize;
        }
        set pageSize(value: number) {
            this._pageSize = value;
            this.pagesLoaded = [];
            this.resetCaches();
            this.resetAll();
            this.search();
        }
        get quickFilter(): string {
            return this._quickFilter;
        }
        set quickFilter(value: string) {
            this._quickFilter = value;
            this.pagesLoaded = [];
            this.resetCaches();
            this.search();
        }
        get dataUnpaged(): TRecord[] {
            return this._dataUnpaged;
        }
        get data(): { [page: number]: TRecord[] } {
            return this._data;
        }
        set data(value: { [page: number]: TRecord[] }) {
            if (value) {
                let ttlCount = 0;
                const unpaged = [];
                this.pagesLoaded = [];
                Object.keys(value).forEach(page => {
                    ttlCount = ttlCount + value[Number(page)].length;
                    value[Number(page)].forEach(i => {
                        i["__page"] = Number(page);
                        unpaged.push(i);
                    });
                    this.pagesLoaded.push(Number(page));
                });
                this._data = value;
                this._dataUnpaged = unpaged;
                this._totalCount = ttlCount;
            } else {
                this._data = { };
                this._dataUnpaged = [];
                this.pagesLoaded = [];
                this._totalCount = 0;
            }
            this.resetCaches();
        }
        get currentPageFrom(): number {
            return this.skip() + 1;
        }
        get currentPageTo(): number {
            const unlimitedTo = this.skip() + this.pageSize;
            return this.filteredCount < unlimitedTo
                ? this.filteredCount
                : unlimitedTo;
        }
        get showingLabelContent(): string {
            // NOTE: Translations are an issue due to use in both admin and storefront
            return this.filteredCount <= 0
                ? "No Results"
                : this.$filter
                    && `Showing ${this.$filter("number")(this.currentPageFrom, 0)} to ${
                            this.$filter("number")(this.currentPageTo, 0)} of ${
                                this.$filter("number")(this.filteredCount, 0)}`;
        }
        get currentPage(): number {
            if (angular.isUndefined(this._currentPage)) {
                this._currentPage = 0;
            }
            return this._currentPage;
        }
        set currentPage(newValue: number) {
            const newValueChecked = Math.max(0, Math.min(newValue || 0, this.maxPage() - 1)); // -1 to make it zero based
            if (newValueChecked !== 0 && this._currentPage === newValueChecked) {
                return;
            }
            this._currentPage = newValueChecked;
            this.search().then(() => { /* Do Nothing*/ });
        }
        // Cache Management
        resetCachedPages(): void {
            this._cachedAllPages = null;
        }
        resetCachedFilteredData(): void {
            this._cachedFilteredData = null;
            this._filteredCount = 0;
        }
        resetCaches(): void {
            this.resetCachedFilteredData();
            this.resetCachedPages();
            this._currentPage = this.currentPage;
        }
        resetAll(): void {
            this.data = { };
        }
        // Calculate
        setCurrentPageIfDifferent(newValue: number): void {
            if (angular.isUndefined(newValue)) {
                newValue = 0;
            }
            if (this.currentPage === newValue) {
                return;
            }
            this.currentPage = newValue;
        }
        skip(): number {
            return this.currentPage * this.pageSize;
        }
        maxPage(): number {
            return Math.ceil(this.filteredCount / this.pageSize);
        }
        /**
         * @deprecated Use `pages()` instead. Here for compatibility with
         * the WithSets version of this class
         */
        allPages(): number[] {
            if (this._cachedAllPages) {
                return this._cachedAllPages;
            }
            this.pages(); // Populates the data
            return this._cachedAllPages;
        }
        pages(): number[] {
            if (this._cachedAllPages) {
                return this._cachedAllPages;
            }
            const all = new Array<number>();
            for (let i = 0; i < this.maxPage(); i++) {
                all.push(i);
            }
            this._cachedAllPages = all;
            return this._cachedAllPages;
        }
        get filteredData(): { [page: number]: TRecord[] } {
            if (this._cachedFilteredData) {
                return this._cachedFilteredData;
            }
            if (!this.data || !this.$filter) {
                this._filteredCount = 0;
                this._cachedFilteredData = { [0]: [] };
                return this._cachedFilteredData;
            }
            if (!this.quickFilter) {
                this._cachedFilteredData = this._data;
                this._filteredCount = this._totalCount;
                return this._cachedFilteredData;
            }
            const rawFiltered = this.$filter("filter")(this._dataUnpaged, { "$": this.quickFilter });
            if (!rawFiltered.length) {
                this._filteredCount = 0;
                this._cachedFilteredData = { [0]: [] };
                return this._cachedFilteredData;
            }
            const paged: { [page: number]: TRecord[] } = { };
            let pageIndex = 0;
            rawFiltered.forEach(i => {
                if (!paged[pageIndex]) {
                    paged[pageIndex] = [];
                }
                if (paged[pageIndex].length >= this._pageSize) {
                    pageIndex++;
                }
                if (!paged[pageIndex]) {
                    paged[pageIndex] = [];
                }
                i["__filteredPage"] = pageIndex;
                paged[pageIndex].push(i);
            });
            this._cachedFilteredData = paged;
            this._filteredCount = this._totalCount = rawFiltered.length;
            return this._cachedFilteredData;
        }
        // Navigate
        goToFirstPage = () => {
            this.setCurrentPageIfDifferent(0);
        }
        goToPreviousPage = () => {
            this.setCurrentPageIfDifferent(Math.max(0, this.currentPage - 1));
        }
        goToNextPage = () => {
            this.setCurrentPageIfDifferent(Math.min(this.maxPage(), this.currentPage + 1));
        }
        goToLastPage = () => {
            this.setCurrentPageIfDifferent(this.maxPage() - 1); // -1 to make it zero based
        }
        // Functions
        search(): ng.IPromise<TRecord[]> {
            if (angular.isUndefined(this._currentPage)) {
                return this.$q.reject("No current page");
            }
            if (this.pagesLoaded.indexOf(this._currentPage) !== -1) {
                return this.$q.resolve(this._data[this._currentPage]);
            }
            return this.$q((resolve, reject) => {
                if (angular.isFunction(this.waitCondition)) {
                    const wait = this.waitCondition();
                    if (wait) {
                        reject("Waiting");
                        return;
                    }
                }
                let dto = <api.BaseSearchModel>{
                    Paging: <api.Paging>{
                        StartIndex: this._currentPage + 1, // Skip
                        Size: (this.pageSize || 8) // Take
                    }
                }
                if (angular.isFunction(this.searchParamsToMerge)) {
                    const toMerge = this.searchParamsToMerge();
                    if (toMerge && Object.keys(toMerge).length > 0) {
                        dto = angular.merge(dto, toMerge);
                    }
                }
                if (this.quickFilter) {
                    dto[this.searchParameterName || "IDOrCustomKeyOrName"] = this.quickFilter;
                }
                this.searchCall(dto).then(r => {
                    if (!this._data) {
                        this._data = [];
                    }
                    if (!this.data[this._currentPage]) {
                        this._data[this._currentPage] = [];
                    }
                    r.data.Results.forEach(i => {
                        i["__page"] = this._currentPage;
                        this._data[this._currentPage].push(i);
                        this.dataUnpaged.push(i);
                    });
                    this._data[this._currentPage] = _.uniqBy(this._data[this._currentPage], x => x.ID).sort(x => x.ID);
                    this._dataUnpaged = _.uniqBy(this._dataUnpaged, x => x.ID).sort(x => x.ID);
                    this.pagesLoaded.push(this._currentPage);
                    this._totalCount = r.data.TotalCount;
                    this.resetCaches();
                    this.filteredData;
                    resolve(this._data);
                    if (this.$rootScope) {
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.paging.searchComplete,
                            this.name,
                            this._data[this._currentPage]);
                    }
                }).catch(reject);
            });
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                readonly $scope: ng.IScope,
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly searchCall:
                    (searchModel: api.BaseSearchModel) => ng.IHttpPromise<TPagedRecord>,
                size: number,
                private readonly name: string,
                private readonly searchParameterName: string = "IDOrCustomKeyOrName",
                private readonly searchParamsToMerge: () => object = () => { return { Active: true, AsListing: true }; },
                private readonly waitCondition: () => boolean = null) {
            this._pageSize = size;
            this._currentPage = 0;
            this.search();
            const unbind1 = $scope.$watch(() => angular.toJson(searchParamsToMerge()), () => this.search());
            $scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }
}
