module cef.store.stores {
    class SellerDirectoryController extends core.TemplatedControllerBase {
        // Properties
        storeList: Array<api.StoreModel> = [];
        pageSizes = [8, 12, 16, 24, 28, 32];
        basePaging: api.Paging = {
            StartIndex: 1,
            Size: this.pageSizes[0]
        };
        basePagingResults: api.StorePagedResults = {
            Results: [],
            CurrentCount: 0,
            CurrentPage: 1,
            TotalCount: 0,
            TotalPages: 1
        };
        queryParams = {};
        querySorts: Array<api.Sort> = [];
        paging: api.Paging;
        pagingResults: api.StorePagedResults;
        filters = {};
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.resetPaging();
            this.getStores();
        }

        onPagingChange(): void {
            this.paging.StartIndex = this.pagingResults.CurrentPage;
            this.getStores();
        };

        getStores(): void {
            const newQuery = (Object as any).assign({}, this.queryParams, { Active: true, AsListing: true, Paging: this.paging, Sorts: this.querySorts }) as api.GetStoresDto;
            this.cvApi.stores.GetStores(newQuery).then(r => {
                this.storeList = r.data.Results;
                this.pagingResults = r.data;
            });
        }

        setQuery(params): void {
            if (!angular.isObject(params)) { return; }
            Object.keys(params).forEach((key) => {
                if (angular.isArray(params[key]) && this.queryParams[key] && angular.isArray(this.queryParams[key])) {
                    this.queryParams[key] = this.queryParams[key].concat(params[key]);
                } else {
                    this.queryParams = (Object as any).assign(this.queryParams, params);
                }
            });
        };

        removeQuery(key: number | string): boolean {
            let removed: boolean = false;
            if (key && this.queryParams[key]) {
                delete this.queryParams[key];
                removed = true;
            }
            return removed;
        }

        removeQueryArrayItem(key: number | string, value: any): boolean {
            let removed = false;
            if (key && value && this.queryParams[key] && angular.isArray(this.queryParams[key])) {
                const arr = this.queryParams[key];
                arr.splice(arr.indexOf(value), 1);
                removed = true;
            }
            return removed;
        }

        resetQuery(): void {
            this.queryParams = {};
        }

        doSearch(params): void {
            this.resetPaging();
            this.setQuery(params);
            this.getStores();
        };

        clearSearch(key, value): void {
            const didRemove = key && value
                ? this.removeQueryArrayItem(key, value) || this.removeQuery(key)
                : false;
            if (didRemove) {
                this.resetPaging();
                this.getStores();
            }
        }

        resetPaging(): void {
            this.paging = angular.copy(this.basePaging);
            this.pagingResults = angular.copy(this.basePagingResults);
        };

        addFilter(name: string, params: any): any {
            ////if (angular.isObject(params)) {
            ////    const f = this.filterFactory(name, params);
            ////    f.availableSubscribers = this.filters;
            ////    this.filters[name] = f;
            ////    return f;
            ////}
            return null;
        }
    }

    cefApp.directive("cefSellerDirectory", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/stores/sellerDirectory.html", "ui"),
        controller: SellerDirectoryController,
        controllerAs: "sellerDirectoryCtrl"
    }));

    export interface ISellerDirectoryGenericFilterScope extends ng.IScope {
        filterObj: any;
        searchFn: string; // A stores.GetStoreTypes
        searchFnArgs?: { }; // { CountryID: 1002 }
        slug: string; // A type
        name: string; // A Type
        mergeAs: string; // A TypeID
        mergeAsArray?: boolean; // A true
    }

    cefApp.directive("cefSellerDirectoryGenericFilter", (cvApi: api.ICEFAPI) => ({
        restrict: "EA",
        require: "^cefSellerDirectory",
        scope: {
            searchFn: "@", // A stores.GetStoreTypes
            searchFnArgs: "=?", // { CountryID: 1002 }
            slug: "@", // A type
            name: "@", // A Type
            mergeAs: "@", // A TypeID
            mergeAsArray: "@?" // A true
        },
        template: `<label class="control-label" for="{{name}}">{{name}}</label> <div filter-dropdown="filterObj" filter-placeholder="Select a(n) {{name}}"></div>`,
        link($scope: ISellerDirectoryGenericFilterScope, el, attrs, ctrl: any) {
            let lastVal: number;
            let fn: (args?: { }) => ng.IHttpPromise<api.PagedResultsBase<api.NameableBaseModel>> = cvApi[$scope.searchFn];
            if (!angular.isFunction(fn) && $scope.searchFn.indexOf(".") > 0) {
                const split = $scope.searchFn.split(".");
                if (split.length > 1) {
                    fn = cvApi[split[0]][split[1]];
                }
            }
            if (!angular.isFunction(fn)) {
                throw "searchFn is required and must match a CEF service endpoint name";
            }
            ($scope.searchFnArgs ? fn($scope.searchFnArgs) : fn()).then(response => {
                $scope.filterObj = ctrl.addFilter($scope.slug, {
                    optionsList: response.data.Results.map(item => {
                        return { Name: item.Name, Value: item.ID, data: item };
                    }),
                    updateCallback: (val, obj) => {
                        if (angular.isObject(obj)) {
                            lastVal = obj.data.ID;
                            const obj2 = {};
                            if ($scope.mergeAsArray) {
                                obj2[$scope.mergeAs] = [obj.data.ID];
                            } else {
                                obj2[$scope.mergeAs] = obj.data.ID;
                            }
                            ctrl.doSearch(obj2);
                        } else {
                            ctrl.clearSearch($scope.mergeAs, lastVal);
                        }
                    }
                });
            });
        }
    }));
}
