module cef.store.userDashboard.controls {
    interface IDownloadHistoryScope extends ng.IScope {
        type: string;
        downloadDetailUrl: string;
        paginate?: boolean;
        defaultPageSize?: number;
        pageChoices?: number;
        showFilters?: boolean;
    }

    interface DownloadSearchResultModel {
        ID: number;
        DownloadDateTime: Date;
    }

    class DownloadHistoryController extends core.TemplatedControllerBase {
        // Properties
        private _filterID: number;
        private _startDate: Date;
        private _endDate: Date;
        private _page = 1;
        get type(): string { return this.$scope.type; }
        get detailUrl(): string { return this.$scope.downloadDetailUrl; }
        get filters(): boolean { return this.$scope.showFilters || false; }
        get paginates(): boolean { return this.$scope.paginate || false; }
        get defaultPageSize(): number { return this.$scope.defaultPageSize || 10; }
        get pageChoices(): number { return this.$scope.pageChoices || 5; }
        pageSize: number;
        status = "searching";
        allDownloads: DownloadSearchResultModel[] = [];
        filteredDownloads: DownloadSearchResultModel[] = [];
        downloads: DownloadSearchResultModel[] = [];
        gridFields = [
            { field: "ID", title: "ID", width: "0", template: '<a ng-click="downloadHistory.view(dataItem.ID)">{{dataItem.ID}}</a>', hidden: true },
            { field: "DownloadDateTime", width: "15%", title: "Date", template: '{{dataItem.CreatedDate | convertJSONDate | date:"yyyy-MM-dd"}}' },
            { field: "Total", title: "Total", width: "20%", template: "{{dataItem.Totals.Total | currency}}" }
        ];

        load(): void {
            if (this.type !== "user" && this.type !== "account") {
                throw new Error("The type attribute must be 'user' or 'account'.");
            }
            this.status = "searching";
            this.cvAuthenticationService.preAuth().finally(() => {
                (this.type === "user" ? this.getCurrentUserDownloads() : this.getCurrentAccountDownloads())
                    .success(downloads => {
                        this.allDownloads = downloads;
                        this.status = this.allDownloads.length > 0 ? "results" : "noResults";
                        this.update();
                    });
            });
        }

        getCurrentUserDownloads() { return { success: (f: Function) => { f([]); } }; };
        getCurrentAccountDownloads() { return { success: (f: Function) => { f([]); } }; };

        get filterID(): number { return this._filterID; }
        set filterID(id: number) { this._filterID = id; this.update(); }

        get startDate(): Date { return this._startDate; }
        set startDate(date: Date) { this._startDate = date; this.update(); }

        get endDate(): Date { return this._endDate; }
        set endDate(date: Date) { this._endDate = date; this.update(); }

        // TODO: Replace this paging with the extracted one in the core folder
        get page(): number { return this._page; }
        set page(n: number) { this._page = n; this.update(); }

        update() {
            this.filteredDownloads = this.filter(this.allDownloads);
            this.downloads = this.paginate(this.filteredDownloads);
        }

        filter(downloads: DownloadSearchResultModel[]): DownloadSearchResultModel[] {
            if (this.filterID != null) {
                downloads = _.filter(downloads,(download) => download.ID.toString().startsWith(this.filterID.toString()));
            }
            if (this.startDate != null) {
                downloads = _.filter(downloads,(download) => core.Utility.convertJSONDate(download.DownloadDateTime.toString()) >= this.startDate);
            }
            if (this.endDate != null) {
                downloads = _.filter(downloads,(download) => core.Utility.convertJSONDate(download.DownloadDateTime.toString()) <= this.endDate);
            }
            return downloads;
        }

        paginate(downloads: DownloadSearchResultModel[]): DownloadSearchResultModel[] {
            if (this.paginates) {
                return downloads.slice((this.page - 1) * this.pageSize, this.page * this.pageSize);
            }
            return downloads;
        }

        pageRange() {
            let start = this.page - Math.floor(this.pageChoices / 2);
            let end = this.page + Math.ceil(this.pageChoices / 2 - 1);
            if (start < 1) {
                start = 1;
                end = Math.min(this.pageChoices, this.numPages());
            } else if (end > this.numPages()) {
                start = Math.max(1, this.numPages() - this.pageChoices + 1);
                end = this.numPages();
            }
            return _.range(start, end + 1);
        }

        hasPrevious(): boolean { return this.page > 1; }
        hasNext(): boolean { return this.page < this.numPages(); }

        first() { this.page = 1; }
        previous() { this.page = Math.max(this.page - 1, 1); }
        next() { this.page = Math.min(this.page + 1, Math.max(1, this.numPages())); }
        last() { this.page = Math.max(1, this.numPages()); }

        numPages(): number { return Math.ceil(this.filteredDownloads.length / this.pageSize); }

        firstInPage(): number {
            if (this.filteredDownloads.length === 0) {
                return 0;
            }
            return this.paginates ? (this.page - 1) * this.pageSize + 1 : 1;
        }

        lastInPage(): number {
            if (this.paginates) {
                if (this.page * this.pageSize <= this.filteredDownloads.length) {
                    return this.page * this.pageSize;
                }
                return this.filteredDownloads.length;
            }
            return this.filteredDownloads.length;
        }

        totalItems(): number { return this.filteredDownloads.length; }

        view(id: number) {
            core.Utility.joinPaths("/", this.detailUrl, id.toString());
        }
        // Constructor
        constructor(
                private readonly $scope: IDownloadHistoryScope,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefDownloadHistory", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { type: "@", downloadDetailUrl: "@", paginate: "=?", defaultPageSize: "=?", pageChoices: "=?", showFilters: "=?" },
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/downloadHistory.html", "ui"),
        controller: DownloadHistoryController,
        controllerAs: "downloadHistory"
    }));
}
