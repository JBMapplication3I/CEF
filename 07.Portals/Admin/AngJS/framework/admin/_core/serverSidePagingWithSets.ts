module cef.admin.core {
    export class ServerSidePagingWithSets<
            TRecord extends api.BaseModel,
            TPagedRecord extends api.PagedResultsBase<TRecord>> {
        // Data
        protected _totalCount = 0;
        protected _filteredCount = null;
        protected _quickFilter: string = "";
        protected _pageSize = 8;
        protected _pageSetSize = 3;
        protected _data: TRecord[] = [];
        protected _currentPage = 0;
        get totalCount(): number { return this._totalCount; }
        get filteredCount(): number {
            if (!this._filteredCount) { this.filteredData; } // Fills the value
            return this._filteredCount;
        }
        get pageSize(): number { return this._pageSize; }
        set pageSize(value: number) { this._pageSize = value; this.pagesLoaded = []; this.resetCaches(); this.search(); }
        get pageSetSize(): number { return this._pageSetSize; }
        set pageSetSize(value: number) { this._pageSetSize = value; this.pagesLoaded = []; this.resetCaches(); this.search(); }
        get quickFilter(): string { return this._quickFilter; }
        set quickFilter(value: string) { this._quickFilter = value; this.pagesLoaded = []; this.resetCaches(); this.search(); }
        get data(): TRecord[] { return this._data; }
        set data(value: TRecord[]) {
            this._data = value;
            this._totalCount = (value || []).length;
            this.resetCaches();
        }
        get currentPageFrom(): number { return this.skip() + 1; }
        get currentPageTo(): number {
            const unlimitedTo = (this.skip() + this.pageSize);
            return this.filteredCount < unlimitedTo ? this.filteredCount : unlimitedTo;
        }
        get showingLabelContent(): string {
            // NOTE: Translations are an issue due to use in both admin and storefront
            return this.filteredCount <= 0
                ? "No Results"
                : this.$filter && `Showing ${this.$filter("number")(this.currentPageFrom, 0)} to ${this.$filter("number")(this.currentPageTo, 0)} of ${this.$filter("number")(this.filteredCount, 0)}`;
        }
        get currentPage() {
            if (angular.isUndefined(this._currentPage)) {
                this._currentPage = 0;
            }
            return this._currentPage;
        }
        set currentPage(newValue: number) {
            const newValueChecked = Math.max(0, Math.min(newValue || 0, this.maxPage()));
            if (newValueChecked !== 0 && this._currentPage === newValueChecked) { return; }
            this._currentPage = newValueChecked;
            this.search().then(() => {
                if (!this.cachedPagesInSets || !this.cachedFilteredData) { return; } // Still in setup
                if (this.currentPage < this.getFirstPageInPageSet()) {
                    while (this.currentPage < this.getFirstPageInPageSet()
                           && this.pageSetCurrent > 0) {
                        this.goToPreviousPageSet();
                    }
                }
                if (this.currentPage > this.getLastPageInPageSet()) {
                    while (this.currentPage > this.getLastPageInPageSet()
                           && this.pageSetCurrent < this.maxPageSet()) {
                        this.goToNextPageSet();
                    }
                }
            });
        }
        pageSetCurrent = 0;
        // Cache Management
        cachedFilteredData: TRecord[] = null;
        cachedPagesInSets: number[][] = null;
        cachedAllPages: number[] = null;
        resetCachedPagesInSets(): void { this.cachedAllPages = null; this.cachedPagesInSets = null; }
        resetCachedFilteredData(): void { this.cachedFilteredData = null; this._filteredCount = 0; }
        resetCaches(): void {
            this.resetCachedFilteredData();
            this.resetCachedPagesInSets();
            this._currentPage = this.currentPage;
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
        maxPageSet(): number { return Math.max(0, this.pagesInSets().length - 1); }
        skip(): number { return this.currentPage * this.pageSize; }
        maxPage(): number { return Math.ceil(this.filteredCount / this.pageSize); }
        allPages(): number[] {
            if (this.cachedAllPages) { return this.cachedAllPages; }
            this.pagesInSets();
            return this.cachedAllPages;
        }
        pagesInSets(): number[][] {
            if (this.cachedPagesInSets) { return this.cachedPagesInSets; }
            const sets = new Array<Array<number>>();
            const all = new Array<number>();
            let currentSet = 0;
            for (let i = 0; i < this.maxPage(); i++) {
                if (!sets[currentSet]) { sets[currentSet] = []; }
                sets[currentSet].push(i);
                all.push(i);
                if (sets[currentSet].length >= this.pageSetSize) {
                    currentSet++;
                }
            }
            this.cachedPagesInSets = sets;
            this.cachedAllPages = all;
            return this.cachedPagesInSets;
        }
        getFirstPageInPageSet(): number {
            return this.pagesInSets()
                && this.pagesInSets()[this.pageSetCurrent]
                && this.pagesInSets()[this.pageSetCurrent][0];
        }
        getLastPageInPageSet(): number {
            return this.pagesInSets()
                && this.pagesInSets()[this.pageSetCurrent]
                && this.pagesInSets()[this.pageSetCurrent][this.pagesInSets()[this.pageSetCurrent].length-1];
        }
        // Navigate
        goToFirstPage = () => {
            this.pageSetCurrent = 0;
            this.setCurrentPageIfDifferent(0);
        }
        goToPreviousPage = () => {
            this.setCurrentPageIfDifferent(Math.max(0, this.currentPage - 1));
        }
        goToNextPage = () => {
            this.setCurrentPageIfDifferent(Math.min(this.maxPage(), this.currentPage + 1));
        }
        goToLastPage = () => {
            this.pageSetCurrent = this.maxPageSet();
            this.goToLastPageInPageSet();
        }
        // Jumping Page Sets
        goToFirstPageInPageSet = () => {
            this.setCurrentPageIfDifferent(this.getFirstPageInPageSet());
        }
        goToLastPageInPageSet = () => {
            this.setCurrentPageIfDifferent(this.getLastPageInPageSet());
        }
        goToPreviousPageSet = () => {
            this.pageSetCurrent = Math.min(this.pageSetCurrent - 1, this.maxPageSet());
            this.goToLastPageInPageSet();
        }
        goToNextPageSet = () => {
            this.pageSetCurrent = Math.min(this.pageSetCurrent + 1, this.maxPageSet());
            this.goToFirstPageInPageSet();
        }
        get filteredData(): TRecord[] {
            if (this.cachedFilteredData) { return this.cachedFilteredData; }
            this.cachedFilteredData = !this._data
                ? []
                : !this.quickFilter
                    ? this._data
                    : this.$filter && this.$filter("filter")(this._data, { "$": this.quickFilter });
            this._filteredCount = this._totalCount;
            return this.cachedFilteredData;
        }
        // Functions
        pagesLoaded: number[] = [];
        search(): ng.IPromise<TRecord[]> {
            if (angular.isUndefined(this._currentPage)) {
                return this.$q.reject("No current page");
            }
            if (this.pagesLoaded.indexOf(this._currentPage) !== -1) {
                return this.$q.resolve(this._data);
            }
            return this.$q((resolve, reject) => {
                const dto = <api.BaseSearchModel>{
                    Paging: <api.Paging>{
                        StartIndex: this._currentPage + 1, // Skip
                        Size: (this.pageSize || 8) // Take
                    }
                }
                if (this.quickFilter) {
                    dto[this.searchParameterName || "IDOrCustomKeyOrName"] = this.quickFilter;
                }
                this.searchCall(dto).then(r => {
                    if (!this._data) {
                        this._data = [];
                    }
                    this._data.push(...r.data.Results);
                    this._data = _.uniqBy(this._data, x => x.ID).sort(x => x.ID);
                    this.pagesLoaded.push(this._currentPage);
                    this._totalCount = r.data.TotalCount;
                    this.resetCaches();
                    this.filteredData;
                    resolve(this._data);
                    if (this.$rootScope) {
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.paging.searchComplete,
                            this.name,
                            r.data.Results);
                    }
                }).catch(reject);
            });
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly searchCall:
                    (searchModel: api.BaseSearchModel) => ng.IHttpPromise<TPagedRecord>,
                size: number,
                setSize: number,
                private readonly name: string,
                private readonly searchParameterName: string = "IDOrCustomKeyOrName") {
            this._pageSize = size;
            this._pageSetSize = setSize;
            this._currentPage = 0;
            this.search();
        }
    }
}
