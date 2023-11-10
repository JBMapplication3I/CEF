/**
 * @file framework/admin/_core/grid.ts
 * @author Copyright (c) 2014-2023 clarity-ventures.com. All rights reserved.
 * @desc CV Grid control, a wrapper around Kendo Grids that makes them
 * buildable entirely with HTML in a common format that talks to the CEF API
 */

/**
 * This function is required in the global scope so that Kendo can find it
 * properly with one of it's internal calls
 * @param {*} value - The string value to encode
 * @returns {string} The encoded string result
 */
function encodeAdmin(value) {
    if (typeof value === "string") {
        return value.replace(/"/g, '\\"').replace(/'/g, "\\'");
    }
    return value;
}

module cef.admin.core {
    interface ICVGridColumnDefinition extends kendo.ui.GridColumn {
        // Kendo GridColumn provides the following properties:
        /*aggregates?: any;
        //attributes?: any;
        //columns?: any;
        //command?: GridColumnCommandItem[];
        //encoded?: boolean;
        //field?: string;
        //filterable?: GridColumnFilterable;
        //footerTemplate?: string | Function;
        //format?: string;
        //groupable?: boolean;
        //groupHeaderTemplate?: string | Function;
        //groupFooterTemplate?: string | Function;
        //headerAttributes?: any;
        //headerTemplate?: string | Function;
        //hidden?: boolean;
        //locked?: boolean;
        //lockable?: boolean;
        //minScreenWidth?: number;
        //sortable?: GridColumnSortable;
        //template?: string | Function;
        //title?: string;
        //width?: string | number;
        //values?: any;
        //menu?: boolean;*/

        // Disabling ours as we are inheriting now
        /*field?: string;
        //title?: string;
        //hidden?: boolean;
        //format?: string;
        //width?: string | number;
        //filterable?: any; */

        // Overriding for our purposes
        template?: string | ((item: any) => string);

        // Our custom columns
        titleKey?: string;
        type?: string;
        cellClass?: string;
        cellNgClass?: string;
        headerClass?: string;
        headerNgClass?: string;
        optional?: boolean;
        // Filter Settings
        filterField?: string;
        filterOrder?: number;
        filterType?: string;
        filterColspan?: number;
        filterMin?: number | Date;
        filterMax?: number | Date;
        filterStep?: number | string;
        filterOptions?: Array<IOption>;
        filterValue?: string;
        filterClass?: string;
        onOptionRepeat?: (filterListener?: IFilterListener) => void;
        optionRepeatParams?: IRepeatParam[];
        optionScopeListeners?: string[];
        $q?: ng.IQService;
        autoCompleteParameterPropertyName?: string;
        updateAutoCompleteList?: ($viewValue: string, $q: ng.IQService) => ng.IPromise<IOption[]>;
        onAutoCompleteSelect?: ($item, $model: api.BaseModel, $label, $event: ng.IAngularEvent) => void;
        emitName?: string;
        preloadStatesKind?: string;
        preloadStatusesKind?: string;
        preloadTypesKind?: string;
        if?: string; // to eval
        repeat?: string; // to eval
        // Group By Settings
        groupByField?: string;
        serverGroupByField?: string;
        groupBy?: number;
        // Sort By Settings
        sortByField?: string;
        sortBy?: number;
        sortByDirection?: string;
        // Commands
        templateButtons?: any[];
    }
    interface IRepeatParam { param: string; value: any; conditionString: string; hasOppositeValue: boolean; oppositeValue: any; }
    interface IOption { value?: string; text?: string; selected?: boolean; sortOrder?: number; }
    interface IGroupBy { field: string, order: number, sortOrder?: number, dir?: string }
    interface ISortBy extends kendo.data.DataSourceSortItem { field: string, order: number, dir?: string }
    interface IFilterListener { listener: string; field: string; passAs?: string; }

    interface IGridScope extends ng.IScope {
        search(): void;
        resetSearch(): void;
        doEmit(name: string, colNum: number): void;
        navigate(path: string, $event): void;
        deactivate(id: number): void;
        reactivate(id: number): void;
        delete(id: number): void;
        /* gridOptions: kendo.data.DataSourceOptions; */
        gridOptions: kendo.ui.GridOptions;
        columnDefinition: string;
        gridDataSource: kendo.data.DataSource;
        dataSource: any;

        onDeactivate: () => void;
        onReactivate: () => void;
        onDelete: () => void;
        onSearch: (searchParams: any) => any;

        // To help with debugging:
        onSearchLiteral: string;
        onDeactivateLiteral: string;
        onReactivateLiteral: string;
        onDeleteLiteral: string;
        onRowNgClassLiteral: string;
        onRowGroupNgClassLiteral: string;
        rowClass: string;
        rowGroupClass: string;

        searchParams: { [field: string]: string };
        filterListeners: Array<IFilterListener>;

        partialReset?: boolean;
        subController?: any;
        autoColumnWidths?: boolean;
        hasPermissionsBase: boolean;
        permissionsBase: string;
        noInitialRun: boolean;
        noRecordsKey: string;
        noRecordsKeyWithNoInitialRun: string;
        gridColumns: ICVGridColumnDefinition[];
        fullRowTemplate: string;
        fullRowAltTemplate: string;
        dateColumns: string[];
        filterColumns: ICVGridColumnDefinition[];
        filterSide: boolean;
        dontUseFullHeight: boolean;
        forceShowLabels: boolean;
        labelsOnTop: boolean;
        groupable: boolean;
        groupBy: IGroupBy[];
        sortable: boolean;
        sortBy: ISortBy[];
        hardcodedParams: { [field: string]: string | object };
        editUrl: string;
        editState: string;
        detailUrl: string;
        detailState: string;
        idField: string;
        currentPage: number;
        pageSize: number;
        defaultPageSize: number;
        serverPaging: boolean;
        serverFiltering: boolean;
        serverGrouping: boolean;
        serverSorting: boolean;
        serverAggregates: boolean;
        activeField: string;
        onFilterFieldValues: (field, search) => string[];
        filterFieldValues: (field: string, search: string) => string[];
        onFilterChange: (field, value) => void;
        onDataBinding: (event: kendo.ui.GridDataBindingEvent) => void;
        onChange: (event: kendo.ui.GridChangeEvent) => void;
        columnTemplate: string;
    }

    class CVGridController extends core.TemplatedControllerBase {
        // Properties
        commandCell: { [id: number]: ng.IAugmentedJQuery } = { };
        gridDataSource: kendo.data.DataSource;
        dataSource: any;
        buildingGrid: boolean = true;
        gettingData: boolean;
        onSearchLiteral: string;
        onDeactivateLiteral: string;
        onReactivateLiteral: string;
        onDeleteLiteral: string;
        onFilterFieldValuesLiteral: string;
        onRowNgClassLiteral: string;
        onRowGroupNgClassLiteral: string;
        rowClass: string;
        rowGroupClass: string;
        searchParams: { [field: string]: string };
        filterListeners: Array<IFilterListener>;
        refreshEvent?: string;
        partialReset?: boolean;
        subController?: any;
        autoColumnWidths?: boolean;
        hasPermissionsBase: boolean;
        permissionsBase: string;
        noInitialRun: boolean;
        noRecordsKey: string;
        noRecordsKeyWithNoInitialRun: string;
        gridColumns: ICVGridColumnDefinition[];
        buttonIndex: string;
        fullRowTemplate: string;
        fullRowAltTemplate: string;
        dateColumns: string[];
        filterColumns: ICVGridColumnDefinition[];
        filterSide: boolean;
        dontUseFullHeight: boolean;
        forceShowLabels: boolean;
        labelsOnTop: boolean;
        groupable: boolean;
        groupBy: IGroupBy[];
        sortable: boolean;
        sortBy: ISortBy[];
        hardcodedParams: { [field: string]: string | object };
        editUrl: string;
        editState: string;
        detailUrl: string;
        detailState: string;
        idField: string;
        currentPage: number;
        pageSize: number;
        defaultPageSize: number;
        serverPaging: boolean;
        serverFiltering: boolean;
        serverGrouping: boolean;
        serverSorting: boolean;
        serverAggregates: boolean;
        activeField: string;
        //gridOptions: kendo.data.DataSourceOptions;
        gridOptions: kendo.ui.GridOptions;
        columnTemplate: string;
        lastQueryParams: string;
        dataItems: { [id: number]: any } = { };
        currentMenuDataItem: any = null;
        autoCols = ["id", "boolean", "input", "autocomplete", "select"];
        isAutoCol(type: string): boolean {
            if (!type) {
                return false;
            }
            return this.autoCols.indexOf(type.toLowerCase()) !== -1;
        }
        cols = ["minmaxnumber", "minmaxdate"];
        isCol(type: string): boolean {
            if (!type) {
                return false;
            }
            return this.cols.indexOf(type.toLowerCase()) !== -1;
        }
        // Functions
        // NOTE: This must remain an arrow function
        search = (event: JQueryKeyEventObject = null, caller: string = null, filterField: string = null): void => {
            if (this.buildingGrid) { return; } // Don't run before we are ready
            if (event && event.key) {
                if (event.key !== "Enter") { return; } // Only do anything if it was the enter key
                event.preventDefault();
                event.stopPropagation();
                if (filterField !== null && event.target && event.target["value"]) {
                    if (event.target["value"] !== this.searchParams[filterField]) {
                        // wait a bit for the form to catch up
                        this.$timeout(() => this.search(event, caller, filterField), 250);
                        return;
                    }
                }
            }
            this.gettingData = true;
            const fnOnQuery = this.onSearch();
            if (fnOnQuery == null) {
                console.warn(`cv-grid: Unable to find a value for on-query='${this.onSearchLiteral}'.`);
                this.gettingData = false;
                return;
            }
            let datasource: any;
            if (angular.isFunction(fnOnQuery)) {
                // Combine the search parameters with any hardcoded ones
                const queryParams = _.clone(this.searchParams || { });
                _.merge(queryParams, this.hardcodedParams || { });
                // Caller Data
                if (caller) {
                    _.merge(queryParams, { "__caller": caller });
                } else {
                    _.merge(queryParams, { "__caller": "None" });
                }
                // Grouping Data
                if (this.groupBy) {
                    this.groupBy = _.sortBy(this.groupBy, x => x.order);
                    _.merge(queryParams, { Groupings: this.groupBy });
                }
                // Sorting Data
                if (this.sortBy) {
                    let tempSortBy = this.sortBy;
                    if (this.groupBy) {
                        _.reverse(this.groupBy).forEach(x => {
                            if (_.some(tempSortBy, y => y.field === x.field)) {
                                return;
                            }
                            tempSortBy.unshift(x);
                        });
                    }
                    this.sortBy = _.sortBy(tempSortBy, x => x.order);
                    _.merge(queryParams, { Sorts: this.sortBy });
                }
                // Paging Data
                if (this.serverPaging) {
                    const copyQueryParams1 = _.cloneDeep(queryParams);
                    delete copyQueryParams1.Paging;
                    delete copyQueryParams1.__caller;
                    if ((this.lastQueryParams === undefined
                        || this.lastQueryParams !== angular.toJson(copyQueryParams1)
                        && this.currentPage !== 1)) {
                        // Reset to page one because we have different filters
                        this.currentPage = 1;
                    }
                    const copyQueryParams2 = _.cloneDeep(queryParams);
                    delete copyQueryParams2.Paging;
                    delete copyQueryParams2.__caller;
                    this.lastQueryParams = angular.toJson(copyQueryParams2);
                    const pagingData = <api.Paging>{
                        StartIndex: this.currentPage, // StartIndex is the index of the "page", not the specific record
                        Size: (this.gridOptions.pageable as kendo.ui.GridPageable).pageSize // Take
                    };
                    _.merge(queryParams, { Paging: pagingData });
                }
                if (queryParams.JsonAttributes) {
                    // Remove json filters with empty strings
                    queryParams.JsonAttributes = _.pickBy(queryParams.JsonAttributes, (value, __) => {
                        // filter out all empty strings, undefined, null, etc. ensure array still has length to send in query
                        return value.filter(Boolean).length;
                    });
                }
                // Run it
                datasource = fnOnQuery(queryParams);
            } else {
                datasource = fnOnQuery; // Not actually a function - they may have bound raw data which is fine.
            }
            // Promise?
            if (datasource == null) {
                console.error("datasource was null");
                this.gettingData = false;
                return;
            }
            // Simple data, not a full kendo datasource
            if (!datasource.then) {
                this.dataSource = datasource;
                const simpleDataSource = new kendo.data.DataSource({
                    pageSize: (this.gridOptions.pageable as kendo.ui.GridPageable).pageSize,
                    serverPaging: this.serverPaging,
                    serverSorting: this.serverSorting,
                    /* serverGrouping: this.serverGrouping,
                     * serverFiltering: this.serverFiltering,
                     * serverAggregates: this.serverAggregates,*/
                    data: this.dataSource,
                    schema: {
                        data: data => {
                            if (data && data.Result) {
                                if (data.Result.Results) {
                                    return data.Result.Results;
                                }
                                return data.Result;
                            }
                            if (data && data.Results) {
                                return data.Results;
                            }
                            return data;
                        },
                        total: "TotalCount"
                    }
                });
                // Add the Group Settings
                if (this.groupBy.length > 0) {
                    this.groupBy = _.sortBy(this.groupBy, x => x.order);
                    simpleDataSource.group(this.groupBy);
                }
                // Add the Sort Settings
                if (this.sortBy.length > 0) {
                    let tempSortBy = this.sortBy;
                    if (this.groupBy) {
                        _.reverse(this.groupBy).forEach(x => {
                            if (_.some(tempSortBy, y => y.field === x.field)) {
                                return;
                            }
                            tempSortBy.unshift(x);
                        });
                    }
                    this.sortBy = _.sortBy(tempSortBy, x => x.order);
                    simpleDataSource.sort(this.sortBy);
                }
                this.gridDataSource = simpleDataSource;
                this.gettingData = false;
                return;
            }
            // Full kendo datasource: ask the server for the new dataset
            datasource.then((r: any) => {
                // Deserialize any JSON dates (field: 'date' in our column definition) into javascript date objects
                const processResultDates = (value: any): void => {
                    this.dateColumns.forEach(field => {
                        if (typeof value[field] !== "string") {
                            return;
                        }
                        const d = new Date(value[field]);
                        // This would make it ignore time zone offsets, but there are other things
                        // in the way of this functioning
                        //d.setTime(d.getTime() + d.getTimezoneOffset() * 60 * 1000);
                        value[field] = d;
                    });
                }
                if (r && r.data) {
                    r = r.data;
                }
                if (this.dateColumns.length > 0) {
                    if (angular.isArray(r && r.Result && r.Result.Results)) {
                        r.Result.Results.forEach(processResultDates);
                    } else if (angular.isArray(r && r.Results)) {
                        r.Results.forEach(processResultDates);
                    } else if (angular.isArray(r && r.Result)) {
                        r.Result.forEach(processResultDates);
                    } else if (angular.isArray(r)) {
                        r.forEach(processResultDates);
                    }
                }
                const advancedDataSource = new kendo.data.DataSource({
                    pageSize: (this.gridOptions.pageable as kendo.ui.GridPageable).pageSize,
                    serverPaging: this.serverPaging,
                    serverSorting: this.serverSorting,
                    /* serverGrouping: this.serverGrouping,
                     * serverFiltering: $this.serverFiltering,
                     * serverAggregates: this.serverAggregates, */
                    data: r,
                    page: this.currentPage,
                    schema: {
                        data: data => {
                            if (data && data.Result) {
                                if (data.Result.Results) {
                                    return data.Result.Results;
                                }
                                return data.Result;
                            }
                            if (data && data.Results) {
                                return data.Results;
                            }
                            return data;
                        },
                        total: "TotalCount"
                    }
                });
                // Add the Group Settings to the results
                if (this.groupBy.length > 0) {
                    this.groupBy =  _.sortBy(this.groupBy, x => x.order);
                    advancedDataSource.group(this.groupBy);
                }
                // Add the Sort Settings to the results
                if (this.sortBy.length > 0) {
                    let tempSortBy = this.sortBy;
                    if (this.groupBy) {
                        _.reverse(this.groupBy).forEach(x => {
                            if (_.some(tempSortBy, y => y.field === x.field)) {
                                return;
                            }
                            tempSortBy.unshift(x);
                        });
                    }
                    this.sortBy = _.sortBy(tempSortBy, x => x.order);
                    advancedDataSource.sort(this.sortBy);
                }
                this.dataSource = r;
                this.gridDataSource = advancedDataSource;
                this.gettingData = false;
            });
        }
        // NOTE: This must remain an arrow function
        onSearch: (searchParams?: any) => any; // Bound by Scope
        // NOTE: This must remain an arrow function
        watchSorts = (newValue: api.Sort[], oldValue: api.Sort[]/*, scope*/): void => {
            const orderedNewValue = newValue ? _.sortBy(newValue, x => x.order) : undefined;
            const orderedOldValue = oldValue ? _.sortBy(oldValue, x => x.order) : undefined;
            const newValueJson = newValue ? angular.toJson(orderedNewValue) : "undefined";
            const oldValueJson = oldValue ? angular.toJson(orderedOldValue) : "undefined";
            if (newValueJson !== oldValueJson) {
                this.currentPage = 1;
                this.search(null, `watchSorts.diff(\r\n${newValueJson}\r\n${oldValueJson}\r\n)`);
                return;
            }
            if (!newValue) {
                return;
            }
            let changed = false;
            for (let i = 0; i < orderedNewValue.length; i++) {
                if (orderedNewValue[i].dir !== orderedOldValue[i].dir
                    || orderedNewValue[i].field !== orderedOldValue[i].field
                    || orderedNewValue[i].order !== orderedOldValue[i].order) {
                    changed = true;
                    return;
                }
            }
            if (changed) {
                this.currentPage = 1;
                this.search(null, "watchSorts.changed");
            }
        }
        // NOTE: This must remain an arrow function
        resetSearch = (): void => {
            this.searchParams = {};
            if (!this.partialReset) {
                this.currentPage = 1;
                this.search(null, "resetSearch");
            }
        }
        // NOTE: This must remain an arrow function
        doEmit = (name: string, colNum: number): void  => {
            if (name == null || name === "") { return; }
            this.$rootScope.$broadcast(name, colNum);
        }
        // NOTE: This must remain an arrow function
        navigate = (path: string, $event): void  => {
            let el = $event.target as Element;
            if (el.localName === "i") { el = el.parentElement; } // Get the Parent Button instead
            const ngLinkHref = el.getAttribute("href");
            this.$location.path(ngLinkHref.substring(1)); // Skip the . at the beginning
        }
        // NOTE: This must remain an arrow function
        deactivate = (id: number): void  => {
            this.cvConfirmModalFactory(this.$translate("ui.admin.grid.ConfirmDeactivate.Message")).then(result => {
                if (!result) { return; }
                const fnOnDeactivate = this.onDeactivate();
                if (fnOnDeactivate == null) {
                    throw new Error(`Unable to evaluate '${this.onDeactivateLiteral}'.`);
                }
                fnOnDeactivate(id).then(() => this.search(null, "deactivate"));
            });
        }
        // NOTE: This must remain an arrow function
        onDeactivate: () => (id: number) => ng.IPromise<void>;
        // NOTE: This must remain an arrow function
        reactivate = (id: number): void => {
            this.cvConfirmModalFactory(this.$translate("ui.admin.grid.ConfirmReactivate.Message")).then(result => {
                if (!result) { return; }
                const fnOnReactivate = this.onReactivate();
                if (fnOnReactivate == null) {
                    throw new Error(`Unable to evaluate '${this.onReactivateLiteral}'.`);
                }
                fnOnReactivate(id).then(() => this.search(null, "reactivate"));
            });
        }
        // NOTE: This must remain an arrow function
        onReactivate: () => (id: number) => ng.IPromise<void>;
        // NOTE: This must remain an arrow function
        onDelete: () => (id: number) => ng.IPromise<void>;
        // NOTE: This must remain an arrow function
        delete = (id: number): void => {
            this.cvConfirmModalFactory(this.$translate("ui.admin.grid.ConfirmDelete.Message")).then(result => {
                if (!result) { return; }
                const fnOnDelete = this.onDelete();
                if (fnOnDelete == null) {
                    throw new Error(`Unable to evaluate '${this.onDeleteLiteral}'.`);
                }
                fnOnDelete(id).then(() => this.search(null, "delete"));
            });
        }
        // NOTE: This must remain an arrow function
        filterFieldValues = (field: string, search: string): string[] => {
            const fnOnFilterFieldValues = this.onFilterFieldValues();
            return !fnOnFilterFieldValues ? [] : fnOnFilterFieldValues(field, search);
        }
        // NOTE: This must remain an arrow function
        onFilterFieldValues: () => (field, search) => string[];
        // NOTE: This must remain an arrow function
        onFilterChange = (field, value): void => {
            this.currentPage = 1;
            this.search(null, `onFilterChange("${field}", "${value}")`);
        }
        // NOTE: This must remain an arrow function
        onChange = (event: kendo.ui.GridChangeEvent): void => {
            this.currentPage = event.sender.pager.dataSource.page();
        }
        // NOTE: This must remain an arrow function
        onDataBinding = (event: kendo.ui.GridDataBindingEvent): boolean => {
            if (event.items.length === 0) { return true; }
            if (event && event.items) {
                // expose subController as a callable function to dataItems
                for (let i = 0; i < event.items.length; i++) {
                    const dataItem = event.items[i];
                    if (dataItem["subController"]) {
                        continue;
                    }
                    dataItem["subController"] = this.subController;
                }
            }
            const pageSize = event.sender.dataSource.pageSize();
            if (pageSize && (this.gridOptions.pageable as kendo.ui.GridPageable).pageSize) {
                if (pageSize !== (this.gridOptions.pageable as kendo.ui.GridPageable).pageSize) {
                    // Don't continue the data-bind. Do a new search instead
                    // Page Size Change detected, setting the new values into scope and running new search
                    this.currentPage = 1;
                    (this.gridOptions.pageable as kendo.ui.GridPageable).pageSize = pageSize;
                    this.search(null, "onDataBinding");
                    event.preventDefault();
                    return false;
                }
            }
            /*
            NOTE: This breaks the sort updates when the user changes it with the column header clicks
            if (event.sender.dataSource.options
                && event.sender.dataSource.options.data
                && event.sender.dataSource.options.data["Sorts"]) {
                this.sortBy = event.sender.dataSource.options.data["Sorts"];
                return true;
            }
            */
            const sorts = event.sender.dataSource.sort();
            if (sorts) {
                this.sortBy = sorts as ISortBy[]; // Inherits that class, so easy to cast
            }
            const groups = event.sender.dataSource.group();
            if (groups) {
                this.groupBy = groups as IGroupBy[]; // Inherits that class, so easy to cast
            }
            return true;
        }
        onDataBound = (event: kendo.ui.GridDataBoundEvent): boolean => {
            if (!this.autoColumnWidths || this.gettingData) {
                return true;
            }
            // Wait for angular digest cycle to autofit columns (after ng-binding)
            this.$timeout(() => {
                for (let i = 0; i < this.gridColumns.length; i++) {
                    event.sender.autoFitColumn(i);
                }
            }, 250);
            return true;
        }
        // NOTE: This must remain an arrow function
        setCommandCell = (id: number): void => {
            this.commandCell[id] = angular.element(document.querySelector(`#commandCell${id}`));
        }
        // NOTE: This must remain an arrow function
        onSelect = (event: kendo.ui.ContextMenuEvent): void => {
            ////this.consoleDebug(event);
        }
        // NOTE: This must remain an arrow function
        openMenu = (id: number, dataItem: any): void => {
            this.currentMenuDataItem = dataItem;
            this.dataItems[id] = dataItem;
            const menu = $(`#menuActions${id}`).data("kendoContextMenu");
            if (menu && angular.isFunction(menu.open)) {
                const rect = $(`#commandCell${id}`)[0].getBoundingClientRect();
                const x = rect.left;
                const y = rect.top + rect.height;
                menu.open(x, y);
            }
        }
        // NOTE: This must remain an arrow function
        onMenuOpen = (id: number, dataItem: any): void => {
            this.currentMenuDataItem = dataItem;
            this.dataItems[id] = dataItem;
            // Close other menus
            if (!this.dataItems) {
                return;
            }
            Object.keys(this.dataItems).forEach(diID => {
                if (String(diID) === String(id)) {
                    return;
                }
                const menu = $(`#menuActions${diID}`).data("kendoContextMenu");
                if (menu && angular.isFunction(menu.close)) {
                    menu.close(null);
                }
                delete this.dataItems[diID];
            });
        }
        // NOTE: This must remain an arrow function
        closeMenu = (id: number): void => {
            if (this.currentMenuDataItem && this.currentMenuDataItem.ID === id) {
                this.currentMenuDataItem = null;
            }
        }
        // Constructors
        constructor(
                // Some of these are public for the link function
                readonly $scope: IGridScope,
                readonly $attrs: ng.IAttributes,
                public readonly $translate: ng.translate.ITranslateService,
                public readonly $q: ng.IQService,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $location: ng.ILocationService,
                private readonly $timeout: ng.ITimeoutService,
                protected readonly cefConfig: core.CefConfig,
                public readonly cvStatesService: services.IStatesService,
                public readonly cvStatusesService: services.IStatusesService,
                public readonly cvTypesService: services.ITypesService,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvConfirmModalFactory: modals.IConfirmModalFactory) {
            super(cefConfig);
            if ($attrs["id"] != null) {
                $rootScope[$attrs["id"]] = { Search: this.search };
            }
        }
    }

    export const cvGridLinkFn: ng.IDirectiveLinkFn = (
        scope: IGridScope,
        instanceElement: ng.IAugmentedJQuery,
        instanceAttributes: ng.IAttributes,
        controller: CVGridController,
        transclude: ng.ITranscludeFunction) => {
        if (!controller) {
            throw new Error("Directive cv-grid: no controller.");
        }
        if (!transclude) {
            throw new Error("Directive cv-grid: no transclude function.");
        }
        if (!instanceAttributes["onSearch"]) {
            throw new Error("Directive cv-grid: The attribute 'on-search' is required.");
        }
        controller.buildingGrid = true;
        if (!controller.$translate.isReady()) {
            controller.$translate.onReady().then(
                () => cvGridLinkFnInner(scope, instanceElement, instanceAttributes, controller, transclude));
            return;
        }
        cvGridLinkFnInner(scope, instanceElement, instanceAttributes, controller, transclude);
    }

    const cvGridLinkFnInner: ng.IDirectiveLinkFn = (
            scope: IGridScope,
            instanceElement: ng.IAugmentedJQuery,
            instanceAttributes: ng.IAttributes,
            controller: CVGridController,
            transclude: ng.ITranscludeFunction) => {
        // CV Custom Properties
        controller.onSearchLiteral = instanceAttributes["onSearch"] || null;
        controller.onDeactivateLiteral = instanceAttributes["onDeactivate"] || null;
        controller.onReactivateLiteral = instanceAttributes["onReactivate"] || null;
        controller.onDeleteLiteral = instanceAttributes["onDelete"] || null;
        controller.onFilterFieldValuesLiteral = instanceAttributes["onFilterFieldValues"] || null;
        controller.editUrl = instanceAttributes["editUrl"] || null;
        controller.editState = instanceAttributes["editState"] || null;
        controller.detailUrl = instanceAttributes["detailUrl"] || null;
        controller.detailState = instanceAttributes["detailState"] || null;
        controller.idField = instanceAttributes["idField"] || null;
        controller.activeField = instanceAttributes["activeField"] || null;
        controller.columnTemplate = instanceAttributes["columnTemplate"] || null;
        controller.noInitialRun = instanceAttributes["noInitialRun"] === "true";
        controller.noRecordsKey = instanceAttributes["noRecordsKey"] || null;
        controller.noRecordsKeyWithNoInitialRun = instanceAttributes["noRecordsKeyWithNoInitialRun"] || null;
        controller.filterSide = instanceAttributes["filterSide"] === "true";
        controller.dontUseFullHeight = instanceAttributes["dontUseFullHeight"] === "true";
        controller.forceShowLabels = instanceAttributes["forceShowLabels"] === "true";
        controller.labelsOnTop = instanceAttributes["labelsOnTop"] === "true";
        controller.groupable = instanceAttributes["groupable"] === "true";
        controller.sortable = instanceAttributes["sortable"] === "true";
        controller.onRowNgClassLiteral = instanceAttributes["rowNgClass"];
        controller.rowClass = instanceAttributes["rowClass"];
        controller.onRowGroupNgClassLiteral = instanceAttributes["rowGroupNgClass"];
        controller.rowGroupClass = instanceAttributes["rowGroupClass"];
        controller.hasPermissionsBase = Boolean(instanceAttributes["permissionsBase"]);
        controller.permissionsBase = instanceAttributes["permissionsBase"];
        controller.refreshEvent = instanceAttributes["refreshEvent"];
        controller.partialReset = instanceAttributes["partialReset"] === "true";
        // Kendo Inherited
        controller.currentPage = 1;
        controller.serverPaging = instanceAttributes["serverPaging"] === "true";
        controller.serverFiltering = instanceAttributes["serverFiltering"] === "true";
        controller.serverGrouping = instanceAttributes["serverGrouping"] === "true";
        controller.serverSorting = instanceAttributes["serverSorting"] === "true";
        controller.serverAggregates = instanceAttributes["serverAggregates"] === "true";
        controller.buttonIndex = instanceAttributes["buttonIndex"];
        // Start the initialization process
        controller.hardcodedParams = {};
        controller.searchParams = {};
        controller.dateColumns = [];
        controller.filterColumns = [];
        controller.filterListeners = new Array<IFilterListener>();
        controller.gridColumns = new Array<ICVGridColumnDefinition>();
        const transcludedContent = instanceElement.find("ng-transclude");
        const columns = transcludedContent.find("cv-column");
        /*if (controller.columnTemplate != null) {
            const templateHtml = $templateCache.get<string>(controller.columnTemplate);
            columns = columns.add($($.parseHTML(templateHtml)).find("cv-column") as any) as any as ng.IAugmentedJQuery;
        }*/
        controller.fullRowTemplate = `<tr role="row" data-uid="#: uid #" data-id="#:ID#" class="${controller.rowClass || ""
            } dataID dataID#:ID#" ng-class="${controller.onRowNgClassLiteral || ""}">\r\n`;
        // Automatically insert as many cells as there are active groupings
        /* controller.fullRowTemplate += `#= new Array(this.group().length + 1).join('<td class="k-group-cell"></td>') #`; */
        controller.fullRowTemplate += `<td ng-repeat="groupCell in $parent.gridCtrl.gridDataSource.group()" class="k-group-cell"></td>\r\n`;
        controller.groupBy = [];
        controller.sortBy = [];
        const readRawColumnData = (el: Element): ICVGridColumnDefinition => {
            return <ICVGridColumnDefinition>{
                // GridColumn Properties
                field: el.getAttribute("field"),
                hidden: el.hasAttribute("hidden"),
                format: el.getAttribute("format"),
                width: el.getAttribute("width"),
                filterable: Boolean(el.hasAttribute("filter")),
                title: el.getAttribute("title"),
                titleKey: el.getAttribute("title-key") ? el.getAttribute("title-key") : null,
                attributes: { "class": "" },
                headerAttributes: { "class": "" },
                // Our Custom Properties
                if: el.getAttribute("if"),
                repeat: el.getAttribute("repeat"),
                type: el.getAttribute("type"),
                headerClass: el.hasAttribute("header-class") ? el.getAttribute("header-class") : "",
                headerNgClass: el.hasAttribute("header-ng-class") ? el.getAttribute("header-ng-class") : "",
                cellClass: el.hasAttribute("cell-class") ? el.getAttribute("cell-class") : "",
                cellNgClass: el.hasAttribute("cell-ng-class") ? el.getAttribute("cell-ng-class") : "",
                optional: el.hasAttribute("optional"),
                // Filter Properties
                filterField: el.hasAttribute("filter-field") ? el.getAttribute("filter-field") : el.getAttribute("field"),
                filterOrder: el.hasAttribute("filter-order") ? Number(el.getAttribute("filter-order")) : null,
                filterType: el.getAttribute("filter-type"),
                filterColspan: el.hasAttribute("filter-colspan") ? parseInt(el.getAttribute("filter-colspan")) : 2,
                filterClass: el.hasAttribute("filter-class") ? el.getAttribute("filter-class") : "",
                filterValue: el.hasAttribute("filter-value") ? el.getAttribute("filter-value") : null,
                filterMin: el.hasAttribute("filter-min")
                    ? el.getAttribute("filter-type") === "minmaxnumber"
                        ? parseInt(el.getAttribute("filter-min"))
                        : new Date(el.getAttribute("filter-min"))
                    : null,
                filterMax: el.hasAttribute("filter-max")
                    ? el.getAttribute("filter-type") === "minmaxnumber"
                        ? parseInt(el.getAttribute("filter-max"))
                        : new Date(el.getAttribute("filter-max"))
                    : null,
                filterStep: el.hasAttribute("filter-step") ? el.getAttribute("filter-step") : null,
                groupable: el.hasAttribute("groupable") ? el.getAttribute("groupable") !== "false" : null,
                groupBy: el.hasAttribute("group-by") ? Number(el.getAttribute("group-by")) : null,
                groupByField: el.hasAttribute("group-by-field") ? el.getAttribute("group-by-field") : el.getAttribute("field"),
                serverGroupByField: el.hasAttribute("server-group-by-field")
                    ? el.getAttribute("server-group-by-field")
                    : el.hasAttribute("group-by-field")
                        ? el.getAttribute("group-by-field")
                        : el.getAttribute("field"),
                sortable: el.hasAttribute("sortable") ? el.getAttribute("sortable") !== "false" : null,
                sortBy: el.hasAttribute("sort-by") ? Number(el.getAttribute("sort-by")) : null,
                sortByDirection: el.hasAttribute("sort-by-direction") ? el.getAttribute("sort-by-direction") : "asc",
                sortByField: el.hasAttribute("sort-by-field") ? el.getAttribute("sort-by-field") : el.getAttribute("field"),
                preloadStatusesKind: el.hasAttribute("preload-statuses-kind") ? el.getAttribute("preload-statuses-kind") : null
            };
        };
        const handleColumnClasses = (col: ICVGridColumnDefinition): void => {
            if (col.headerClass !== "") { col.headerAttributes.class = col.headerClass; }
            if (col.headerNgClass !== "") { col.headerAttributes.ngClass = col.headerNgClass; }
            if (col.cellClass !== "") { col.attributes.class = col.cellClass; }
            if (col.cellNgClass !== "") { col.attributes.ngClass = col.cellNgClass; }
        };
        const handleColumnTitle = (col: ICVGridColumnDefinition): ng.IPromise<string> => {
            return controller.$q(resolve => {
                // Strip off extra colons in the title so that they always are formatted to be title: in the template
                if (col.title != null) {
                    while (col.title.endsWith(":")) {
                        col.title = col.title.substring(0, col.title.length - 1);
                    }
                }
                // Create a default title if one is not specified (including a ui translation key also being not set)
                if (col.titleKey == null && col.title == null && col.field != null && !col.hidden) {
                    col.title = col.field
                        .replace("ID", "\\#")
                        .replace(/([A-Z])/g, " $1").replace(/^./, str => str.toUpperCase());
                    if (col.title.startsWith(" ")) {
                        col.title = col.title.substring(1);
                    }
                }
                if (col.hidden) {
                    resolve(""); // Nothing to append to full template
                    return;
                }
                const readColumnTitleInner = (translated: string, titleKey: string = null): void => {
                    let title = (translated ? translated : col.title)
                        || col.title
                        || "";
                    col.title = title; // Assign as it may have been from translation
                    title = title.replace(/#/, "\\#").replace(/\\\\#/, "\\#"); // Modify for kendo binding
                    let fullTemplateAppend = `<td class="${col.cellClass}" ng-class="${col.cellNgClass}"`
                        + (titleKey ? ` translate-attr="{ title: '${titleKey}' }"` : ` title="${title}"`);
                    if (titleKey) {
                        col.headerTemplate = `<span data-translate="${titleKey}"></span>`;
                    }
                    // NOTE: We don't append the '>' at the end here because we might be binding directly
                    resolve(fullTemplateAppend);
                }
                if (col.titleKey) {
                    controller.$translate(col.titleKey)
                        .then(translated => readColumnTitleInner(translated, col.titleKey))
                        .catch(() => readColumnTitleInner(null, col.titleKey));
                    return;
                }
                readColumnTitleInner(null, col.titleKey);
            });
        };
        const handleColumnFilters = (col: ICVGridColumnDefinition, el: Element): void => {
            if (!col.filterable) { return; }
            if (col.filterType == null || col.filterType === "") {
                col.filterType = "input";
            }
            if (col.filterType.toLowerCase() === "bool" || col.filterType.toLowerCase() === "boolean") {
                col.filterType = "boolean";
                col.filterOptions = [];
                col.filterOptions.push(<IOption>{ value: undefined, text: "Any", selected: true, sortOrder: -1 });
                col.filterOptions.push(<IOption>{ value: "true", text: "Yes", sortOrder: 0 });
                col.filterOptions.push(<IOption>{ value: "false", text: "No", sortOrder: 1 });
            } else if (col.filterType.toLowerCase() === "id") {
                col.filterType = "id"; // Positive numbers only
            } else if (col.filterType.toLowerCase() === "value") {
                controller.searchParams[col.filterField] = col.filterValue;
            } else if (col.filterType.toLowerCase() === "select") {
                col.filterOptions = [];
                col.emitName = col.filterField + "_FilterChanged";
                scope.$on(col.emitName, event => {
                    const cols = (event.targetScope as IGridScope).gridColumns;
                    angular.forEach(controller.filterListeners, fl => {
                        const col = _.find(cols, c => c.filterField === fl.listener);
                        if (col && col.onOptionRepeat) {
                            col.onOptionRepeat(fl);
                        }
                    });
                });
                $(el).children("option").each((i, elem: any) => {
                    col.filterOptions.push({
                        text: elem.textContent,
                        selected: elem.hasAttribute("selected"),
                        value: elem.getAttribute("value"),
                        sortOrder: elem.hasAttribute("sort-order") ? elem.getAttribute("sort-order") : null
                    });
                });
                if (el.hasAttribute("on-option-repeat")) {
                    col.optionRepeatParams = [];
                    col.optionScopeListeners = [];
                    $(el).children("option-repeat-parameter").each((i, elem: any) => {
                        const repeatParam = <IRepeatParam>{
                            param: elem.textContent,
                            value: elem.getAttribute("value"),
                            conditionString: elem.getAttribute("condition"),
                            hasOppositeValue: elem.hasAttribute("opposite-value"),
                            oppositeValue: elem.getAttribute("opposite-value")
                        };
                        col.optionRepeatParams.push(repeatParam);
                    });
                    $(el).children("option-repeat-parameter-listen").each((i, elem: any) => {
                        col.optionScopeListeners.push(elem.textContent);
                        controller.filterListeners.push(<IFilterListener>{
                            listener: col.filterField,
                            field: elem.textContent,
                            passAs: elem.getAttribute("pass-as")
                        });
                    });
                    const onOptionRepeat = (filterListener?: IFilterListener) => {
                        const fnGetOptions = scope.$eval(el.getAttribute("on-option-repeat"));
                        if (!angular.isFunction(fnGetOptions)) { return; }
                        const queryParams = {};
                        angular.forEach(col.optionRepeatParams, orp => {
                            if (orp.conditionString && orp.conditionString !== "") {
                                // Has condition, apply only if passing
                                if (scope.$eval(orp.conditionString)) {
                                    // Passing
                                    queryParams[orp.param] = orp.value;
                                } else if (orp.hasOppositeValue) {
                                    queryParams[orp.param] = orp.oppositeValue;
                                }
                                return;
                            }
                            // No condition, always apply
                            queryParams[orp.param] = orp.value;
                        });
                        angular.forEach(col.optionScopeListeners, param => {
                            let passParam = param;
                            if (filterListener
                                && filterListener.passAs
                                && filterListener.passAs !== "") {
                                // Has pass as, use that variable name instead
                                passParam = filterListener.passAs;
                            }
                            if (controller.searchParams[param]) {
                                queryParams[passParam] = controller.searchParams[param];
                            } else {
                                delete queryParams[passParam];
                            }
                        });
                        fnGetOptions(queryParams).then(r => {
                            col.filterOptions = [];
                            col.filterOptions.push(<IOption>{ value: undefined, text: "Any", selected: true, sortOrder: -1 });
                            if (!r || !r.data) {
                                return;
                            }
                            angular.forEach(r.data.Results ? r.data.Results : r.data, (value: any) => {
                                col.filterOptions.push(<IOption>{
                                    value: value.ID,
                                    text: value.DisplayName || value.Name || value.CustomKey,
                                    selected: false,
                                    sortOrder: value.SortOrder
                                });
                            });
                        });
                    }
                    col.onOptionRepeat = onOptionRepeat;
                    onOptionRepeat();
                }
                if (col.filterOptions.length > 0) {
                    let selected = _.find(col.filterOptions, "selected");
                    if (selected == null) {
                        selected = _.first(col.filterOptions);
                        selected.selected = true;
                    }
                    if (selected.value) {
                        controller.searchParams[col.filterField] = selected.value;
                    } else {
                        delete controller.searchParams[col.filterField];
                    }
                }
            } else if (col.filterType.toLowerCase() === "autocomplete") {
                col.filterOptions = [];
                if (el.hasAttribute("on-autocomplete")) {
                    const fnGetAutoComplete = scope.$eval(el.getAttribute("on-autocomplete"));
                    if (!angular.isFunction(fnGetAutoComplete)) { return; }
                    const queryParamsAutoComplete = { Paging: { StartIndex: 1, Size: 10 } };
                    if (!col.optionScopeListeners) { col.optionScopeListeners = []; }
                    $(el).children("autocomplete-parameter").each((i, elem: any) => {
                        col.autoCompleteParameterPropertyName = elem.textContent;
                        if (!elem.hasAttribute("listen")) {
                            queryParamsAutoComplete[col.autoCompleteParameterPropertyName] = elem.getAttribute("value");
                            return;
                        }
                        const currentValue = scope.$eval(`searchParams.${col.autoCompleteParameterPropertyName}`);
                        if (currentValue) {
                            queryParamsAutoComplete[col.autoCompleteParameterPropertyName] = currentValue;
                        } else {
                            delete queryParamsAutoComplete[col.autoCompleteParameterPropertyName];
                        }
                        col.optionScopeListeners.push(col.autoCompleteParameterPropertyName);
                        controller.filterListeners.push(<IFilterListener>{
                            listener: col.filterField,
                            field: col.autoCompleteParameterPropertyName,
                            passAs: elem.getAttribute("pass-as")
                        });
                    });
                    col.$q = angular.element(el).scope().search.$q;
                    col.updateAutoCompleteList = ($viewValue: string, $q: ng.IQService): ng.IPromise<IOption[]> => {
                        return $q((resolve, reject) => {
                            if (col.filterValue || $viewValue) {
                                queryParamsAutoComplete[col.autoCompleteParameterPropertyName] = col.filterValue || $viewValue;
                            } else {
                                delete queryParamsAutoComplete[col.autoCompleteParameterPropertyName];
                            }
                            fnGetAutoComplete(queryParamsAutoComplete).then(r => {
                                let counter = 0;
                                col.filterOptions = [];
                                if (!r || !r.data) {
                                    resolve(col.filterOptions);
                                    return;
                                }
                                (r.data.Results ? r.data.Results : r.data).forEach(value => {
                                    counter++;
                                    if (counter > 10) { return false; }
                                    col.filterOptions.push({
                                        value: value.CustomKey || value.Name || value.DisplayName || value.ID,
                                        text: ((value.DisplayName || value.Name) + (value.CustomKey ? " [" + value.CustomKey + "]" : "")),
                                        selected: col.filterValue === value.ID
                                               || col.filterValue === value.CustomKey
                                               || col.filterValue === value.Name
                                               || col.filterValue === value.DisplayName,
                                        sortOrder: value.SortOrder
                                    });
                                    return true;
                                });
                                resolve(col.filterOptions);
                            });
                        });
                    }
                    col.onAutoCompleteSelect = ($item, $model: api.BaseModel, $label, $event: ng.IAngularEvent): void => {
                        col.filterValue = $model.CustomKey;
                        /*if (selected.value) {
                            controller.searchParams[col.filterField] = selected.value;
                        } else {
                            delete controller.searchParams[col.filterField];
                        }*/
                    }
                }
            } else if (col.filterType.toLowerCase() === "minmaxdate") {
                /* These are handled in UI
                 * controller.searchParams["MinDate"] = col.filterValue;
                 * controller.searchParams["MaxDate"] = col.filterValue;
                 */
            } else if (col.filterType.toLowerCase() === "minmaxnumber") {
                /* These are handled in UI
                 * controller.searchParams[`Min${col.filterField}`] = col.filterValue;
                 * controller.searchParams[`Max${col.filterField}`] = col.filterValue;
                 */
            }
            controller.filterColumns.push(col);
        };
        const handlePreloadStates = (col: ICVGridColumnDefinition): void => {
            if (!col.preloadStatesKind) { return; }
            controller.cvStatesService.search(col.preloadStatesKind);
        };
        const handlePreloadStatuses = (col: ICVGridColumnDefinition): void => {
            if (!col.preloadStatusesKind) { return; }
            controller.cvStatusesService.search(col.preloadStatusesKind);
        };
        const handlePreloadTypes = (col: ICVGridColumnDefinition): void => {
            if (!col.preloadTypesKind) { return; }
            controller.cvTypesService.search(col.preloadTypesKind);
        };
        const handleColumnGroupings = (col: ICVGridColumnDefinition): void => {
            if (!col.groupBy) { return; }
            if (!_.some(controller.groupBy, x => x.field === col.groupByField)) {
                controller.groupBy.push(<IGroupBy>{
                    field: col.groupByField,
                    serverField: col.serverGroupByField,
                    order: col.groupBy,
                    sortOrder: col.sortBy,
                    dir: col.sortByDirection || "asc"
                });
            }
            if (!_.some(controller.sortBy, x => x.field === (col.sortByField || col.groupByField))) {
                controller.sortBy.push(<ISortBy>{
                    field: col.sortByField || col.groupByField,
                    serverField: col.sortByField || col.serverGroupByField,
                    order: col.sortBy,
                    dir: col.sortByDirection || "asc"
                });
            }
        };
        const handleColumnSorts = (col: ICVGridColumnDefinition): void => {
            if (!col.sortBy) { return; }
            if (_.some(controller.sortBy, x => x.field === col.sortByField)) {
                return;
            }
            controller.sortBy.push(<ISortBy>{
                field: col.sortByField,
                order: col.sortBy,
                dir: col.sortByDirection || "asc"
            });
        };
        const handleColumnCustomTemplates = (
                col: ICVGridColumnDefinition,
                el: Element
            ): { hasTemplate: boolean, hasHeaderTemplate: boolean, fullTemplateAppend: string } => {
            const result = { hasTemplate: false, fullTemplateAppend: "", hasHeaderTemplate: false };
            const templateHeaderCell = $(el).children("cv-column-header").first();
            if (templateHeaderCell.text()) {
                col.headerTemplate = templateHeaderCell.text();
                result.hasHeaderTemplate = true;
            }
            const templateElemCell = $(el).children("cv-column-template").first();
            if (templateElemCell.text()) {
                col.template = templateElemCell.text();
                if (!col.hidden) {
                    result.fullTemplateAppend += ">\r\n" + templateElemCell.text();
                    result.hasTemplate = true;
                }
            }
            const templateElemGroupHeader = $(el).children("cv-group-header-template").first();
            if (templateElemGroupHeader.text()) {
                col.groupHeaderTemplate = templateElemGroupHeader.text();
            }
            const templateElemFooter = $(el).children("cv-group-footer-template").first();
            if (templateElemFooter.text()) {
                col.groupFooterTemplate = templateElemFooter.text();
            }
            return result;
        };
        const handleDateColumn = (col: ICVGridColumnDefinition): { hasTemplate: boolean, fullTemplateAppend: string } => {
            const result = { hasTemplate: false, fullTemplateAppend: "" };
            if (col.type !== "date") { return result; }
            controller.dateColumns.push(col.field);
            if (!col.hidden) {
                result.fullTemplateAppend += ` ng-bind="dataItem.${col.field} | date: '${col.format || "yyyy-MM-dd"}'">`;
                result.hasTemplate = true;
            }
            return result;
        }
        const handleAttributeColumn = (col: ICVGridColumnDefinition): { hasTemplate: boolean, fullTemplateAppend: string } => {
            const result = { hasTemplate: false, fullTemplateAppend: "" };
            if (col.type !== "attribute") { return result; }
            if (!col.hidden) {
                result.fullTemplateAppend += ` ng-bind="dataItem.SerializableAttributes['${col.field}'].Value || 'N/A'`
                    + ` + (dataItem.SerializableAttributes['${col.field}'].UofM ? (' '`
                    + ` + dataItem.SerializableAttributes['${col.field}'].UofM)  : '')">`;
                result.hasTemplate = true;
            }
            if (!col.filterable) { return result; }
            col["filterButtonDisabled"] = false;
            col["addValue"] = (): void => {
                if (!controller.searchParams["JsonAttributes"]) {
                    controller.searchParams["JsonAttributes"] = {} as any;
                }
                if (!controller.searchParams["JsonAttributes"][col.filterField]) {
                    controller.searchParams["JsonAttributes"][col.filterField] = [];
                }
                col["filterButtonDisabled"] = true;
                controller.searchParams["JsonAttributes"][col.filterField].push(null);
            };
            col["removeValue"] = (index: number): void => {
                col["filterButtonDisabled"] = false;
                controller.searchParams["JsonAttributes"][col.filterField].splice(index, 1);
            }
            return result;
        };
        const handleBooleanColumn = (col: ICVGridColumnDefinition): { hasTemplate: boolean, fullTemplateAppend: string } => {
            const result = { hasTemplate: false, fullTemplateAppend: "" };
            if (col.type !== "boolean" || col.template) { return result; }
            col.template = `<script type="text/kendo-template"><input type="checkbox" disabled ng-model="dataItem.${col.field}" title="${col.title}" /></script>\r\n`;
            if (!col.hidden) {
                result.fullTemplateAppend += `>\r\n<input type="checkbox" ng-disabled="true"`;
                if (col.titleKey) {
                    result.fullTemplateAppend += `\r\ntranslate-attr="{ title: '${col.titleKey}' }"\r\n`;
                } else {
                    result.fullTemplateAppend += `\r\ntitle="${col.title}"\r\n`;
                }
                result.fullTemplateAppend += `\r\nng-model="dataItem.${col.field}" />\r\n`;
                result.hasTemplate = true;
            }
            return result;
        };
        const handleStateIDColumn = (col: ICVGridColumnDefinition): { hasTemplate: boolean, fullTemplateAppend: string } => {
            const result = { hasTemplate: false, fullTemplateAppend: "" };
            if (col.type !== "stateID" || col.template) { return result; }
            col.template = `<script type="text/kendo-template"><span ng-bind="dataItem.${col.field} | stateIDToText : '${col.preloadStatesKind}' : ['Translated','DisplayName','Name','CustomKey']"></span></script>\r\n`;
            if (!col.hidden) {
                result.fullTemplateAppend += `<span ng-bind="dataItem.${col.field} | stateIDToText : '${col.preloadStatesKind}' : ['Translated','DisplayName','Name','CustomKey']"></span>\r\n`;
                result.hasTemplate = true;
            }
            return result;
        };
        const handleStatusIDColumn = (col: ICVGridColumnDefinition): { hasTemplate: boolean, fullTemplateAppend: string } => {
            const result = { hasTemplate: false, fullTemplateAppend: "" };
            if (col.type !== "statusID" || col.template) { return result; }
            col.template = `<script type="text/kendo-template"><span ng-bind="dataItem.${col.field} | statusIDToText : '${col.preloadStatusesKind}' : ['Translated','DisplayName','Name','CustomKey']"></span></script>\r\n`;
            if (!col.hidden) {
                result.fullTemplateAppend += `<span ng-bind="dataItem.${col.field} | statusIDToText : '${col.preloadStatusesKind}' : ['Translated','DisplayName','Name','CustomKey']"></span>\r\n`;
                result.hasTemplate = true;
            }
            return result;
        };
        const handleTypeIDColumn = (col: ICVGridColumnDefinition): { hasTemplate: boolean, fullTemplateAppend: string } => {
            const result = { hasTemplate: false, fullTemplateAppend: "" };
            if (col.type !== "typeID" || col.template) { return result; }
            col.template = `<script type="text/kendo-template"><span ng-bind="dataItem.${col.field} | typeIDToText : '${col.preloadTypesKind}' : ['Translated','DisplayName','Name','CustomKey']"></span></script>\r\n`;
            if (!col.hidden) {
                result.fullTemplateAppend += `<span ng-bind="dataItem.${col.field} | typeIDToText : '${col.preloadTypesKind}' : ['Translated','DisplayName','Name','CustomKey']"></span>\r\n`;
                result.hasTemplate = true;
            }
            return result;
        };
        let columnElements = [];
        columns.each((_, columnElem: Element) => { columnElements.push(columnElem); });
        let preWorkedColumns = _.filter(columnElements.reduce((accu, columnElem) => {
            const column = readRawColumnData(columnElem);
            if (column.if) {
                const conditionResult = scope.$eval(column.if);
                if (conditionResult === false) {
                    return accu;
                }
            }
            handleColumnGroupings(column);
            handleColumnSorts(column);
            if (column.repeat) {
                // eval repeat to get the collection
                let collection = scope.$eval(column.repeat);
                if (collection && collection.length) {
                    for (let i = 0; i < collection.length; i++) {
                        const item = collection[i];
                        accu.push({
                            columnElem: $(columnElem).clone()[0],
                            column: {
                                ...column,
                                title: item.title || column.title,
                                field: column.type === "attribute" ? item.title : column.field,
                                filterField: item.title || column.filterField
                            }
                        });
                    }
                    return accu;
                }
            }
            accu.push({ columnElem, column });
            return accu;
        }, []), x => x !== null);
        let promises = preWorkedColumns.map(preWorked => {
            return controller.$q(resolve => {
                const column = preWorked["column"];
                handleColumnTitle(column).then(fullTemplateAppend => {
                    handleColumnClasses(column);
                    handleColumnFilters(column, preWorked["columnElem"]);
                    handlePreloadStates(column);
                    handlePreloadStatuses(column);
                    handlePreloadTypes(column);
                    let hasTemplate = false;
                    const customColumnResult = handleColumnCustomTemplates(column, preWorked["columnElem"]);
                    hasTemplate = customColumnResult.hasTemplate || hasTemplate;
                    fullTemplateAppend += customColumnResult.fullTemplateAppend;
                    const dateColumnResult = handleDateColumn(column);
                    hasTemplate = dateColumnResult.hasTemplate || hasTemplate;
                    fullTemplateAppend += dateColumnResult.fullTemplateAppend;
                    const attrColumnResult = handleAttributeColumn(column);
                    hasTemplate = attrColumnResult.hasTemplate || hasTemplate;
                    fullTemplateAppend += attrColumnResult.fullTemplateAppend;
                    const boolColumnResult = handleBooleanColumn(column);
                    hasTemplate = boolColumnResult.hasTemplate || hasTemplate;
                    fullTemplateAppend += boolColumnResult.fullTemplateAppend;
                    const stateIDColumnResult = handleStateIDColumn(column);
                    hasTemplate = stateIDColumnResult.hasTemplate || hasTemplate;
                    fullTemplateAppend += stateIDColumnResult.fullTemplateAppend;
                    const statusIDColumnResult = handleStatusIDColumn(column);
                    hasTemplate = statusIDColumnResult.hasTemplate || hasTemplate;
                    fullTemplateAppend += statusIDColumnResult.fullTemplateAppend;
                    const typeIDColumnResult = handleTypeIDColumn(column);
                    hasTemplate = typeIDColumnResult.hasTemplate || hasTemplate;
                    fullTemplateAppend += typeIDColumnResult.fullTemplateAppend;
                    if (column.width == null) {
                        // We'll count how many of these there are and make them
                        // into percentage width columns
                        column.width = "[%]";
                    }
                    if (!hasTemplate && !column.hidden) {
                        fullTemplateAppend += ` title="{{dataItem['${column.field}']}} aria-label="{{dataItem['${column.field}']}}"`;
                        if (column.format) {
                            fullTemplateAppend += ` ng-bind="dataItem.${column.field} | ${column.format}">`;
                        } else {
                            fullTemplateAppend += ` ng-bind="dataItem.${column.field}">`;
                        }
                        hasTemplate = true;
                    }
                    if (!column.hidden) {
                        fullTemplateAppend += `<//td>\r\n`;
                        controller.fullRowTemplate += fullTemplateAppend;
                    }
                    controller.gridColumns.push(column);
                    resolve();
                });
            });
        });
        controller.$q.all(promises).finally(() => {
            if (controller.gridColumns.length === 0) {
                controller.buildingGrid = false;
                throw new Error("Directive cv-grid: You must specify at least one cv-column.");
            }
            if (!controller.autoColumnWidths) {
                const noWidthSpecifiedColumnsCount = _.filter(controller.gridColumns, c => c.width === "[%]").length;
                if (noWidthSpecifiedColumnsCount > 0) {
                    const w = 100 / noWidthSpecifiedColumnsCount;
                    const ws = `${w}%`;
                    controller.gridColumns.forEach(c => {
                        if (c.width === "[%]") {
                            c.width = ws;
                        }
                    });
                }
            }
            if (controller.onDelete != null
                || controller.onDeactivate != null
                || controller.onReactivate != null
                || controller.editUrl != null
                || controller.editState != null
                || controller.detailUrl != null
                || controller.detailState != null) {
                let idField = controller.idField;
                if (!idField) { idField = "ID"; }
                if (controller.activeField == null) { controller.activeField = "Active"; }
                const commandColumn: ICVGridColumnDefinition = { };
                const rawTemplateHtml = "";
                const templateButtons = [];
                if (controller.detailUrl != null) { // Use in Storefront (no angular router)
                    templateButtons.push({
                        color: "btn-primary",
                        icon: "fa-list",
                        buttonIndex: controller.buttonIndex,
                        textKey: "ui.admin.product.catalog.results.listFull.viewDetails",
                        permission: controller.hasPermissionsBase
                            ? `ng-show="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.View')"`
                            : "",
                        action: `href="${controller.detailUrl}"`,
                        show: ""
                    });
                }
                commandColumn.template = dataItem => {
                    let tempHtml = rawTemplateHtml.replace(`[${idField}]`, dataItem[idField]);
                    const templateButtons1 = [];
                    const id = encodeURI(dataItem[idField]);
                    if (controller.editState != null) { // Use in CEF Admin (uses Angular UI Router)
                        const state = controller.editState.split(",")[0];
                        templateButtons1.push({
                            color: "btn-success",
                            icon: "fa-pencil",
                            textKey: "ui.admin.common.Edit",
                            permission: controller.hasPermissionsBase ? ` ng-if="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.Update')"` : "",
                            action: `ui-sref="${state}({ ${idField}: ${id} })"`,
                            show: ""
                        });
                        templateButtons1.push({
                            color: "btn-info",
                            icon: "fa-list",
                            textKey: "ui.admin.common.ViewDetails",
                            permission: controller.hasPermissionsBase
                                ? ` ng-if="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.View')`
                                    + ` && !$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.Update')"`
                                : "",
                            action: `ui-sref="${state}({ ${idField}: ${id} })"`,
                            show: ""
                        });
                        if (controller.onDeactivate != null) {
                            templateButtons1.push({
                                color: "btn-warning",
                                icon: "fa-recycle",
                                textKey: "ui.admin.common.Deactivate",
                                permission: controller.hasPermissionsBase ? `ng-if="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.Deactivate')"` : "",
                                action: `ng-click="gridCtrl.deactivate(${dataItem[idField]})"`,
                                show: `ng-show="dataItem.Active"`
                            });
                        }
                        if (controller.onReactivate != null) {
                            templateButtons1.push({
                                color: "btn-info",
                                icon: "fa-recycle",
                                textKey: "ui.admin.common.Reactivate",
                                permission: controller.hasPermissionsBase ? `ng-if="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.Reactivate')"` : "",
                                action: `ng-click="gridCtrl.reactivate(${id})"`,
                                show: `ng-hide="dataItem.Active"`
                            });
                        }
                        if (controller.onDelete != null) {
                            templateButtons1.push({
                                color: "btn-danger",
                                icon: "fa-times",
                                textKey: "ui.admin.common.Delete",
                                permission: controller.hasPermissionsBase ? `ng-if="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.Delete')"` : "",
                                action: `ng-click="gridCtrl.delete(${id})"`,
                                show: ""
                            });
                        }
                    }
                    angular.forEach(templateButtons, button => {
                        tempHtml += `<button type="button" class="btn btn-sm ${button.color}" ng-cloak\r\n`;
                        tempHtml += `        translate-attr="{ title: '${button.textKey}', 'aria-label': '${button.textKey}' }"\r\n`;
                        tempHtml += `        ${button.action}\r\n`;
                        tempHtml += `        ${button.permission}\r\n`;
                        tempHtml += `        ${button.show}>\r\n`;
                        tempHtml += `    <i class="far ${button.icon}"></i>\r\n`;
                        tempHtml += `    <span class="sr-only" data-translate="${button.textKey}"></span>\r\n`;
                        tempHtml += `</button>\r\n`;
                    });
                    return `<span ng-cloak class="grid-command-btns">\r\n${tempHtml}</span>\r\n`;
                };
                let rawTemplateHtml2 = rawTemplateHtml.replace(`[${idField}]`, `#= ${idField} #`);
                if (controller.editState != null) { // Use in CEF Admin (uses Angular UI Router)
                    const id1 = controller.editState.replace(`[${idField}]`, `'#= encodeAdmin(${idField}) #'`).split(",")[1];
                    const state = controller.editState.split(",")[0];
                    templateButtons.push({
                        color: "btn-success",
                        icon: "fa-pencil",
                        textKey: "ui.admin.common.Edit",
                        permission: controller.hasPermissionsBase
                            ? `ng-if="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.Update')"`
                            : "",
                        action: `ui-sref="${state}({ ${idField}: ${id1} })"`,
                        show: ""
                    });
                    templateButtons.push({
                        color: "btn-info",
                        icon: "fa-list",
                        buttonIndex: controller.buttonIndex,
                        textKey: "ui.admin.common.ViewDetails",
                        permission: controller.hasPermissionsBase
                            ? `ng-if="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.View') `
                                + `&& !$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.Update')"`
                            : "",
                        action: `ui-sref="${state}({ ${idField}: ${id1} })"`,
                        show: ""
                    });
                    if (controller.onDeactivate != null) {
                        templateButtons.push({
                            color: "btn-warning",
                            icon: "fa-recycle",
                            textKey: "ui.admin.common.Deactivate",
                            state: controller.editState.split(",")[0],
                            permission: controller.hasPermissionsBase
                                ? `ng-if="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.Deactivate')"`
                                : "",
                            action: `ng-click="gridCtrl.deactivate(dataItem.${idField.replace(/'/g, "")})"`,
                            show: `ng-show="dataItem.Active"`
                        });
                    }
                    if (controller.onReactivate != null) {
                        templateButtons.push({
                            color: "btn-info",
                            icon: "fa-recycle",
                            textKey: "ui.admin.common.Reactivate",
                            state: controller.editState.split(",")[0],
                            permission: controller.hasPermissionsBase
                                ? `ng-if="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.Reactivate')"`
                                : "",
                            action: `ng-click="gridCtrl.reactivate(dataItem.${idField.replace(/'/g, "")})"`,
                            show: `ng-hide="dataItem.Active"`
                        });
                    }
                    if (controller.onDelete != null) {
                        templateButtons.push({
                            color: "btn-danger",
                            icon: "fa-times",
                            textKey: "ui.admin.common.Delete",
                            state: controller.editState.split(",")[0],
                            permission: controller.hasPermissionsBase
                                ? `ng-if="$parent.$root.cvSecurityService.hasPermission('${controller.permissionsBase}.Delete')"`
                                : "",
                            action: `ng-click="gridCtrl.delete(dataItem.${idField.replace(/'/g, "")})"`,
                            show: ""
                        });
                    }
                }
                if (templateButtons.length <= 1) {
                    templateButtons.forEach((button, index) => {
                        rawTemplateHtml2 += `<button type="button" class="btn btn-sm ${button.color}" ng-cloak\r\n`;
                        rawTemplateHtml2 += `        id="btnCVGridListDetails_${button.buttonIndex}_${index}_{{dataItem.${idField}}}"\r\n`;
                        rawTemplateHtml2 += `        name="btnCVGridListDetails_${button.buttonIndex}_${index}_{{dataItem.${idField}}}"\r\n`;
                        rawTemplateHtml2 += `        translate-attr="{ title: '${button.textKey}', 'aria-label': '${button.textKey}' }"\r\n`;
                        rawTemplateHtml2 += `        ${button.action}\r\n`;
                        rawTemplateHtml2 += `        ${button.permission}\r\n`;
                        rawTemplateHtml2 += `        ${button.show}>\r\n`;
                        rawTemplateHtml2 += `    <i class="far ${button.icon}"></i>\r\n`;
                        rawTemplateHtml2 += `    <span class="sr-only" data-translate="${button.textKey}"></span>\r\n`;
                        rawTemplateHtml2 += `</button>\r\n`;
                    });
                } else {
                    rawTemplateHtml2 += `<a id="btnActions{{dataItem.ID}}" name="btnActions{{dataItem.ID}}"\r\n`;
                    rawTemplateHtml2 += `   translate-attr="{ title: 'ui.admin.common.Action.Plural',\r\n`;
                    rawTemplateHtml2 += `                     'aria-label': 'ui.admin.common.Action.Plural' }"\r\n`;
                    rawTemplateHtml2 += `   ng-click="gridCtrl.openMenu(dataItem.ID, dataItem)">\r\n`;
                    rawTemplateHtml2 += `    <i class="far fa-ellipsis-h"></i>\r\n`;
                    rawTemplateHtml2 += `    <span class="sr-only" data-translate="ui.admin.common.Action.Plural"></span>\r\n`;
                    rawTemplateHtml2 += `</a>\r\n`;
                    rawTemplateHtml2 += `<ul id="menuActions{{dataItem.ID}}" name="menuActions{{dataItem.ID}}"\r\n`;
                    rawTemplateHtml2 += `    kendo-context-menu\r\n`;
                    rawTemplateHtml2 += `    k-filter="'tr.dataID{{dataItem.ID}} > td'"\r\n`;
                    rawTemplateHtml2 += `    k-anchor="'\\#commandCell{{dataItem.ID}}'"\r\n`;
                    rawTemplateHtml2 += `    k-open-on-click="true"\r\n`;
                    rawTemplateHtml2 += `    k-close-on-click="true"\r\n`;
                    rawTemplateHtml2 += `    k-on-open="gridCtrl.onMenuOpen(dataItem.ID, dataItem)"\r\n`;
                    rawTemplateHtml2 += `    k-on-close="gridCtrl.closeMenu(dataItem.ID)">\r\n`;
                    rawTemplateHtml2 += `    <li role="presentation" class="p-0" disabled>\\#{{dataItem.ID}}</li>\r\n`;
                    templateButtons.forEach(button => {
                        rawTemplateHtml2 += `<li role="menuitem" class="p-0" ${button.permission} ${button.show}>\r\n`;
                        rawTemplateHtml2 += `    <a class="p-1 text-regular block nowrap"\r\n`;
                        rawTemplateHtml2 += `       translate-attr="{ title: '${button.textKey}',\r\n`;
                        rawTemplateHtml2 += `                         'aria-label': '${button.textKey}' }"\r\n`;
                        rawTemplateHtml2 += `       ${button.action}>\r\n`;
                        rawTemplateHtml2 += `       <i class="far ${button.icon} fa-fw"></i>&nbsp;<!--\r\n`;
                        rawTemplateHtml2 += `       --><span data-translate="${button.textKey}"></span>\r\n`;
                        rawTemplateHtml2 += `    </a>\r\n`;
                        rawTemplateHtml2 += `</li>\r\n`;
                    });
                    rawTemplateHtml2 += `</ul>\r\n`;
                }
                if (!controller.autoColumnWidths) {
                    if (_.some(controller.gridColumns,
                            x => !x.width
                                || angular.isString(x.width)
                                    && (x.width as string).indexOf("%") !== -1)) {
                        commandColumn.width = "2%";
                    } else {
                        commandColumn.width = "38px";
                    }
                }
                const injectRaw = rawTemplateHtml2;
                rawTemplateHtml2  = `<td class="p-1" id="commandCell{{dataItem.ID}}" title="Commands"\r\n`
                rawTemplateHtml2 += `    ng-init="gridCtrl.setCommandCell(dataItem.ID)">\r\n`
                rawTemplateHtml2 += `    <div ng-show="dataItem">\r\n`;
                rawTemplateHtml2 += `        ${injectRaw}\r\n`;
                rawTemplateHtml2 += `    </div>\r\n`;
                rawTemplateHtml2 += `</td>\r\n`;
                commandColumn.templateButtons = templateButtons;
                /*
                commandColumn.title = "Actions";
                // Put command column at beginning instead of end
                let startColumnIndex = controller.fullRowTemplate.indexOf(
                    `<td ng-repeat="groupCell in $parent.gridCtrl.gridDataSource.group()" class="k-group-cell"></td>`);
                controller.fullRowTemplate = controller.fullRowTemplate.substring(0, startColumnIndex)
                    + rawTemplateHtml2
                    + controller.fullRowTemplate.substring(startColumnIndex, controller.fullRowTemplate.length);
                controller.gridColumns.unshift(commandColumn);
                */
                controller.fullRowTemplate += rawTemplateHtml2;
                controller.gridColumns.push(commandColumn);
            }
            controller.fullRowTemplate += `</tr>\r\n`;
            controller.fullRowAltTemplate = controller.fullRowTemplate.replace(` class="`, ` class="k-alt `);
            if (instanceAttributes["rowTemplate"]) {
                // These will override whatever was calculated
                controller.fullRowTemplate = instanceAttributes["rowTemplate"];
                controller.fullRowAltTemplate = instanceAttributes["rowAltTemplate"]
                    ? instanceAttributes["rowAltTemplate"]
                    : instanceAttributes["rowTemplate"];
            }
            transcludedContent.find("cv-query-controller-param").each((index, queryParam) => {
                const field = queryParam.getAttribute("field");
                const valueRaw = queryParam.getAttribute("value");
                if (valueRaw && valueRaw.indexOf("{{") !== -1) {
                    throw new Error("Directive cv-grid: cv-query-controller-param should not include double braces");
                }
                const value = scope.$eval(valueRaw);
                const optional = queryParam.hasAttribute("optional");
                const type = scope.$eval(queryParam.getAttribute("type"));
                // If query param is not optional and field is null, throw;
                if (field == null || (value == null && !optional)) {
                    controller.buildingGrid = false;
                    throw new Error("Directive cv-grid: You must specify a field and the scope variable name as the value when using cv-query-controller-param.");
                }
                // If query param is not optional and field is null, continue jQuery loop;
                if (field == null || (value == null && optional)) {
                    return; // equivalent to 'continue' in a jQuery loop;
                }
                if (type === "attribute") {
                    if (!controller.hardcodedParams.JsonAttributes) {
                        controller.hardcodedParams.JsonAttributes = { };
                    }
                    if (controller.hardcodedParams.JsonAttributes[field]) {
                        controller.hardcodedParams.JsonAttributes[field].push(value);
                    } else {
                        controller.hardcodedParams.JsonAttributes[field] = [value];
                    }
                } else {
                    controller.hardcodedParams[field] = value;
                }
            });
            transcludedContent.find("cv-query-param").each((index, queryParam) => {
                const field = queryParam.getAttribute("field");
                const value = queryParam.getAttribute("value");
                const optional = queryParam.hasAttribute("optional");
                if (field == null || (value == null && !optional)) {
                    controller.buildingGrid = false;
                    throw new Error("Directive cv-grid: You must specify a field and value when using cv-query-param.");
                }
                controller.hardcodedParams[field] = value;
            });
            transcludedContent.remove();
            if (!instanceAttributes["gridOptions"]) {
                /*const pagingSettings: kendo.ui.GridPageable;
                const pageSizes = null;
                const firstPageSize = null;
                const defaultPageSize = null;
                if (attributes["pageSizes"]) {
                    pageSizes = attributes["pageSizes"];
                    defaultPageSize = attributes["defaultPageSize"];
                    if (defaultPageSize != null && defaultPageSize !== "") {
                        firstPageSize = defaultPageSize;
                    } else {
                        try { firstPageSize = (pageSizes as string).split(",")[1]; } catch (ex) { }
                        if (firstPageSize == null) {
                            try { firstPageSize = (pageSizes as string).split(",")[0]; } catch (ex) { }
                        }
                    }
                    /*if (firstPageSize == null) {
                        firstPageSize = "16";
                    }*//*
                }
                if (attributes["showPager"] === "true") {
                }
                pagingSettings = <kendo.ui.GridPageable>{
                    pageSize: firstPageSize ? parseInt(firstPageSize) : null,
                    // don't show sizes if only one size is available
                    pageSizes: !pageSizes || pageSizes.split(",").length === 1 ? null : pageSizes.split(","),
                    refresh: false,
                    buttonCount: 5,
                    change: (e) => {
                        const page = e.sender.page();
                        controller.currentPage = page;
                        controller.pageSize = e.sender.pageSize;
                        controller.search();
                    }
                };*/
                let pagingSettings: boolean|kendo.ui.GridPageable;
                if (instanceAttributes["showPager"] as boolean || false) {
                    let pageSizes = null;
                    let firstPageSize = null;
                    let defaultPageSize = null;
                    if (instanceAttributes["pageSizes"]) {
                        pageSizes = instanceAttributes["pageSizes"];
                        defaultPageSize = instanceAttributes["defaultPageSize"];
                        if (defaultPageSize != null && defaultPageSize !== "") {
                            firstPageSize = defaultPageSize;
                        } else {
                            try { firstPageSize = (pageSizes as string).split(",")[1]; } catch (ex) { }
                            if (firstPageSize == null) {
                                try { firstPageSize = (pageSizes as string).split(",")[0]; } catch (ex) { }
                            }
                        }
                        if (firstPageSize == null) {
                            firstPageSize = "16";
                        }
                        pagingSettings = <kendo.ui.GridPageable>{
                            pageSize: parseInt(firstPageSize),
                            // Don't show sizes if only one size is available
                            pageSizes: pageSizes.split(",").length === 1 ? null : pageSizes.split(","),
                            refresh: false,
                            buttonCount: 5,
                            change: (e) => {
                                const page = e.sender.page();
                                controller.currentPage = page;
                                controller.pageSize = e.sender.pageSize;
                                controller.search(null, "pagingSettings.change");
                            }
                        };
                    } else {
                        pagingSettings = false;
                    }
                } else {
                    pagingSettings = false;
                }
                controller.gridOptions = <kendo.ui.GridOptions>{
                    change: controller.onChange,
                    dataBinding: controller.onDataBinding,
                    dataBound: controller.onDataBound,
                    pageable: pagingSettings,
                    noRecords: {
                        template: () => `<div class="p-3">`
                            + (controller.noInitialRun
                                ? controller.$translate.instant(
                                    controller.noRecordsKeyWithNoInitialRun
                                    || controller.noRecordsKey
                                    || "No records, please try the search again or adjust your filters. This instance is set to require a manual pressing of the filter button before loading data.")
                                : controller.$translate.instant(
                                    controller.noRecordsKey
                                    || "No records, please try the search again or adjust your filters."))
                            + `</div>`
                    },
                    groupable: controller.groupable
                        ? <kendo.ui.GridGroupable>{
                            enabled: true,
                            showFooter: true
                        }
                        : false,
                    serverFiltering: true/*controller.serverFiltering*/,
                    serverGrouping: true/*controller.serverGrouping*/,
                    serverSorting: controller.serverSorting,
                    sortable: controller.sortable
                        // Saw mode "single" in docs, assuming "multiple" is also possible
                        ? <kendo.ui.GridSortable>{
                            allowUnsort: true,
                            mode: "multiple"
                        }
                        : false,
                    resizable: false,
                    scrollable: true, // !controller.autoColumnWidths,
                    height: "100%",
                    reorderable: false,
                    rowTemplate: () => { return $.proxy(controller.fullRowTemplate, controller.dataSource); },
                    altRowTemplate: () => { return $.proxy(controller.fullRowAltTemplate, controller.dataSource); },
                    /*filterable: true,
                    serverFiltering: true,
                    serverPaging: true,
                    serverAggregates: true*/
                };
                if (controller.fullRowTemplate) {
                    controller.gridOptions.rowTemplate = controller.fullRowTemplate;
                }
                if (controller.fullRowAltTemplate) {
                    controller.gridOptions.altRowTemplate = controller.fullRowAltTemplate;
                }
            } else {
                controller.gridOptions = angular.fromJson(instanceAttributes["gridOptions"] as string) as kendo.ui.GridOptions;
            }
            if (controller.refreshEvent) {
                scope.$on(controller.refreshEvent, () => controller.search(null, "refreshEvent"));
            }
            controller.buildingGrid = false;
            scope.$watchCollection("gridCtrl.sortBy", controller.watchSorts);
            // Wait until we are linked to search, don't do it in the controller
            if (!controller.noInitialRun) {
                controller.search(null, "initialRun");
            }
        });
    }

    export const cvGridDirectiveFn = ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        transclude: true, // Required
        templateUrl: $filter("corsLink")("/framework/admin/_core/grid.html", "ui"),
        scope: {
            editUrl: "@?",
            editState: "@?",
            detailUrl: "@?",
            detailState: "@?",
            idField: "@?",
            activeField: "@?",
            columnTemplate: "@?",
            noInitialRun: "@?",
            noRecordsKey: "@?",
            noRecordsKeyWithNoInitialRun: "@?",
            filterSide: "=?",
            dontUseFullHeight: "=?",
            forceShowLabels: "=?",
            labelsOnTop: "=?",
            groupable: "=?",
            sortable: "=?",
            rowNgClass: "@?",
            rowClass: "@?",
            rowGroupNgClass: "@?",
            rowGroupClass: "@?",
            permissionsBase: "@?",
            refreshEvent: "@?",
            partialReset: "=?",
            subController: "&?",
            autoColumnWidths: "=?"/*,
            currentPage: "=?",
            serverPaging: "@?",
            serverFiltering: "@?",
            serverGrouping: "@?",
            serverSorting: "@?",
            serverAggregates: "@?"*/
        },
        bindToController: {
            onSearch: "&",
            onDeactivate: "&?",
            onReactivate: "&?",
            onDelete: "&?",
            onFilterFieldValues: "&?",
            autoColumnWidths: "=?"
        },
        controller: CVGridController,
        controllerAs: "gridCtrl",
        link: cvGridLinkFn
    });
}
