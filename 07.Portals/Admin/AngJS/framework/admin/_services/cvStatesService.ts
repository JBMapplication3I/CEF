/**
 * @file framework/admin/_services/cvStatesService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc States Service class, stores the state definitions
 * data that has been loaded to reduce calls to the server, and provide a
 * single source of information for all areas of the application.
 */
module cef.admin.services {
    export interface IStateLookupInterface {
        kind: string;
        id?: number;
        key?: string;
        name?: string;
        displayName?: string;
    }

    export interface IStatesService {
        search(kind: string, model?: api.StateSearchModel): ng.IPromise<api.StateModel[]>;
        get(lookup: IStateLookupInterface): ng.IPromise<api.StateModel>;
        getCached(lookup: IStateLookupInterface): api.StateModel;
    }

    export class StatesService implements IStatesService {
        private statesCache: { [kind: string]: { [id: number]: api.StateModel; } } = { };
        private keyToIDLookup: { [kind: string]: { [key: string]: number; } } = { };
        private nameToIDLookup: { [kind: string]: { [name: string]: number; } } = { };
        private displayNameToIDLookup: { [kind: string]: { [displayName: string]: number; } } = { };
        private byIDPromises: { [kind: string]: { [id: number]: ng.IPromise<void>; } } = { };
        private byKeyPromises: { [kind: string]: { [key: string]: ng.IPromise<number>; } } = { };
        private byNamePromises: { [kind: string]: { [name: string]: ng.IPromise<number>; } } = { };
        private byDisplayNamePromises: { [kind: string]: { [displayName: string]: ng.IPromise<number>; } } = { };

        search(kind: string, model: api.StateSearchModel = { Active: true, AsListing: true }): ng.IPromise<api.StateModel[]> {
            if (!kind) {
                return this.$q.reject("Must provided a state kind");
            }
            return this.$q((resolve, reject) => {
                if (this.statesCache[kind]) {
                    resolve(this.searchInner(kind, model));
                    return;
                }
                this.kindToSearchPromise(kind)({ Active: true, AsListing: true }).then(r => {
                    if (!r || !r.data) {
                        reject("No data returned");
                        return;
                    }
                    this.statesCache[kind] = r.data.Results;
                    resolve(this.searchInner(kind, model));
                });
            });
        }
        private searchInner(kind: string, model: api.StateSearchModel) {
            // TODO: Filter the data
            // if (!model || model === { }) {
                let retVal = [];
                Object.keys(this.statesCache[kind]).forEach(
                    key => retVal.push(this.statesCache[kind][key]));
                return retVal;
                // return;
            // }
        }
        getCached(lookup: IStateLookupInterface): api.StateModel {
            if (!lookup.id) {
                if (lookup.key) {
                    if (!this.keyToIDLookup[lookup.kind]) {
                        this.keyToIDLookup[lookup.kind] = { };
                    }
                    lookup.id = this.keyToIDLookup[lookup.kind][lookup.key];
                }
                if (!lookup.id && lookup.name) {
                    if (!this.nameToIDLookup[lookup.kind]) {
                        this.nameToIDLookup[lookup.kind] = { };
                    }
                    lookup.id = this.nameToIDLookup[lookup.kind][lookup.name];
                }
                if (!lookup.id && lookup.displayName) {
                    if (!this.displayNameToIDLookup[lookup.kind]) {
                        this.displayNameToIDLookup[lookup.kind] = { };
                    }
                    lookup.id = this.displayNameToIDLookup[lookup.kind][lookup.displayName];
                }
                if (!lookup.id) {
                    return null;
                }
            }
            if (!this.statesCache[lookup.kind]) {
                this.statesCache[lookup.kind] = { };
            }
            return _.find(this.statesCache[lookup.kind], x => x.ID === lookup.id);
        }
        get(lookup: IStateLookupInterface): ng.IPromise<api.StateModel> {
            if (!lookup || !lookup.kind || !lookup.id && !lookup.key && !lookup.name && !lookup.displayName) {
                return this.$q.reject("ERROR! Nothing to look up by");
            }
            return this.$q((resolve, reject) => {
                if (!lookup.id) {
                    if (lookup.key) {
                        if (!this.keyToIDLookup[lookup.kind]) {
                            this.keyToIDLookup[lookup.kind] = { };
                        }
                        if (!this.byKeyPromises[lookup.kind]) {
                            this.byKeyPromises[lookup.kind] = { };
                        }
                        if (this.keyToIDLookup[lookup.kind][lookup.key]) {
                            lookup.id = this.keyToIDLookup[lookup.kind][lookup.key];
                        } else if (this.byKeyPromises[lookup.kind][lookup.key]) {
                            resolve(this.byKeyPromises[lookup.kind][lookup.key]);
                            return;
                        } else {
                            this.byKeyPromises[lookup.kind][lookup.key] = this.kindToCustomKeyPromise(lookup.kind)(lookup.key).then(r => {
                                if (!r || !r.data) {
                                    reject("ERROR! Unable to locate product by Key");
                                    return;
                                }
                                // Save it so we don't have to look again
                                this.keyToIDLookup[lookup.kind][lookup.key] = r.data;
                                // Loop back in with the id
                                resolve(this.get({ kind: lookup.kind, id: r.data }));
                            }).catch(reject);
                            return;
                        }
                    } else if (lookup.name) {
                        if (!this.nameToIDLookup[lookup.kind]) {
                            this.nameToIDLookup[lookup.kind] = { };
                        }
                        if (!this.byNamePromises[lookup.kind]) {
                            this.byNamePromises[lookup.kind] = { };
                        }
                        if (this.nameToIDLookup[lookup.kind][lookup.name]) {
                            lookup.id = this.nameToIDLookup[lookup.kind][lookup.name];
                        } else if (this.byNamePromises[lookup.kind][lookup.name]) {
                            resolve(this.byNamePromises[lookup.kind][lookup.name]);
                            return;
                        } else {
                            this.byNamePromises[lookup.kind][lookup.name] = this.kindToNamePromise(lookup.kind)({ Name: lookup.name }).then(r => {
                                if (!r || !r.data) {
                                    reject("ERROR! Unable to locate product by Name");
                                    return;
                                }
                                // Save it so we don't have to look again
                                this.nameToIDLookup[lookup.kind][lookup.name] = r.data;
                                // Loop back in with the id
                                resolve(this.get({ kind: lookup.kind, id: r.data }));
                            }).catch(reject);
                            return;
                        }
                    } else if (lookup.displayName) {
                        if (!this.displayNameToIDLookup[lookup.kind]) {
                            this.displayNameToIDLookup[lookup.kind] = { };
                        }
                        if (!this.byDisplayNamePromises[lookup.kind]) {
                            this.byDisplayNamePromises[lookup.kind] = { };
                        }
                        if (this.displayNameToIDLookup[lookup.kind][lookup.displayName]) {
                            lookup.id = this.displayNameToIDLookup[lookup.kind][lookup.displayName];
                        } else if (this.byDisplayNamePromises[lookup.kind][lookup.displayName]) {
                            resolve(this.byDisplayNamePromises[lookup.kind][lookup.displayName]);
                            return;
                        } else {
                            this.byDisplayNamePromises[lookup.kind][lookup.displayName] = this.kindToDisplayNamePromise(lookup.kind)({ DisplayName: lookup.displayName }).then(r => {
                                if (!r || !r.data) {
                                    reject("ERROR! Unable to locate product by SeoUrl");
                                    return;
                                }
                                // Save it so we don't have to look again
                                this.displayNameToIDLookup[lookup.kind][lookup.displayName] = r.data;
                                // Loop back in with the id
                                resolve(this.get({ kind: lookup.kind, id: r.data }));
                            }).catch(reject);
                            return;
                        }
                    }
                }
                if (!lookup.id) {
                    reject("ERROR! Nothing to look up by");
                    return;
                }
                if (!this.byIDPromises[lookup.kind]) {
                    this.byIDPromises[lookup.kind] = { };
                }
                if (!this.statesCache[lookup.kind]) {
                    this.statesCache[lookup.kind] = { };
                }
                if (this.statesCache[lookup.kind][lookup.id]) {
                    resolve(this.statesCache[lookup.kind][lookup.id]);
                    return;
                } else if (this.byIDPromises[lookup.kind][lookup.id]) {
                    resolve(this.byIDPromises[lookup.kind][lookup.id]);
                    return;
                } else {
                    this.byIDPromises[lookup.kind][lookup.id] = this.kindToIDPromise(lookup.kind)(lookup.id).then(r => {
                        if (!r || !r.data) {
                            reject("ERROR! Unable to locate state by ID");
                            return;
                        }
                        // Save it so we don't have to look again and return the good item
                        resolve(this.statesCache[lookup.kind][lookup.id] = r.data);
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.states.loaded, lookup, r.data);
                    });
                }
            });
        }
        private kindToIDPromise(kind: string): (id: number) => ng.IHttpPromise<api.StateModel> {
            switch (kind) {
                case "SalesOrder": { return this.cvApi.ordering.GetSalesOrderStateByID; }
                case "SalesInvoice": { return this.cvApi.invoicing.GetSalesInvoiceStateByID; }
                case "SalesQuote": { return this.cvApi.quoting.GetSalesQuoteStateByID; }
                case "SalesReturn": { return this.cvApi.returning.GetSalesReturnStateByID; }
                default: { throw new Error(`Unknown kind: '${kind}'`); }
            }
        }
        private kindToCustomKeyPromise(kind: string): (key: string) => ng.IHttpPromise<number> {
            switch (kind) {
                case "SalesOrder": { return this.cvApi.ordering.CheckSalesOrderStateExistsByKey; }
                case "SalesInvoice": { return this.cvApi.invoicing.CheckSalesInvoiceStateExistsByKey; }
                case "SalesQuote": { return this.cvApi.quoting.CheckSalesQuoteStateExistsByKey; }
                case "SalesReturn": { return this.cvApi.returning.CheckSalesReturnStateExistsByKey; }
                default: { throw new Error(`Unknown kind: '${kind}'`); }
            }
        }
        private kindToNamePromise(kind: string): (dto: { Name: string }) => ng.IHttpPromise<number> {
            switch (kind) {
                case "SalesOrder": { return this.cvApi.ordering.CheckSalesOrderStateExistsByName; }
                case "SalesInvoice": { return this.cvApi.invoicing.CheckSalesInvoiceStateExistsByName; }
                case "SalesQuote": { return this.cvApi.quoting.CheckSalesQuoteStateExistsByName; }
                case "SalesReturn": { return this.cvApi.returning.CheckSalesReturnStateExistsByName; }
                default: { throw new Error(`Unknown kind: '${kind}'`); }
            }
        }
        private kindToDisplayNamePromise(kind: string): (dto: { DisplayName: string }) => ng.IHttpPromise<number> {
            switch (kind) {
                case "SalesOrder": { return this.cvApi.ordering.CheckSalesOrderStateExistsByDisplayName; }
                case "SalesInvoice": { return this.cvApi.invoicing.CheckSalesInvoiceStateExistsByDisplayName; }
                case "SalesQuote": { return this.cvApi.quoting.CheckSalesQuoteStateExistsByDisplayName; }
                case "SalesReturn": { return this.cvApi.returning.CheckSalesReturnStateExistsByDisplayName; }
                default: { throw new Error(`Unknown kind: '${kind}'`); }
            }
        }
        private kindToSearchPromise(kind: string): (dto: api.StateSearchModel) => ng.IHttpPromise<api.PagedResultsBase<api.StateModel>> {
            switch (kind) {
                case "SalesOrder": { return this.cvApi.ordering.GetSalesOrderStates; }
                case "SalesInvoice": { return this.cvApi.invoicing.GetSalesInvoiceStates; }
                case "SalesQuote": { return this.cvApi.quoting.GetSalesQuoteStates; }
                case "SalesReturn": { return this.cvApi.returning.GetSalesReturnStates; }
                default: { throw new Error(`Unknown kind: '${kind}'`); }
            }
        }
        // Constructor
        constructor(
            private readonly $rootScope: ng.IRootScopeService,
            private readonly $q: ng.IQService,
            private readonly cvApi: api.ICEFAPI,
            private readonly cvServiceStrings: IServiceStrings) { }
    }
}
