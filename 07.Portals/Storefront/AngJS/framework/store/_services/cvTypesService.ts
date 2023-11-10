/**
 * @file framework/store/_services/cvTypesService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Types Service class, stores the type definitions
 * data that has been loaded to reduce calls to the server, and provide a
 * single source of information for all areas of the application.
 */
module cef.store.services {
    export interface ITypeLookupInterface {
        kind: string;
        id?: number;
        key?: string;
        name?: string;
        displayName?: string;
    }

    export interface ITypesService {
        search(kind: string, model?: api.TypeSearchModel): ng.IPromise<api.TypeModel[]>;
        get(lookup: ITypeLookupInterface): ng.IPromise<api.TypeModel>;
        getCached(lookup: ITypeLookupInterface): api.TypeModel;
    }

    export class TypesService implements ITypesService {
        private typesCache: { [kind: string]: { [id: number]: api.TypeModel; } } = { };
        private keyToIDLookup: { [kind: string]: { [key: string]: number; } } = { };
        private nameToIDLookup: { [kind: string]: { [name: string]: number; } } = { };
        private displayNameToIDLookup: { [kind: string]: { [displayName: string]: number; } } = { };
        private byIDPromises: { [kind: string]: { [id: number]: ng.IPromise<void>; } } = { };
        private byKeyPromises: { [kind: string]: { [key: string]: ng.IPromise<number>; } } = { };
        private byNamePromises: { [kind: string]: { [name: string]: ng.IPromise<number>; } } = { };
        private byDisplayNamePromises: { [kind: string]: { [displayName: string]: ng.IPromise<number>; } } = { };

        search(kind: string, model: api.TypeSearchModel = { Active: true, AsListing: true }): ng.IPromise<api.TypeModel[]> {
            if (!kind) {
                return this.$q.reject("Must provided a type kind");
            }
            return this.$q((resolve, reject) => {
                if (this.typesCache[kind]) {
                    resolve(this.searchInner(kind, model));
                    return;
                }
                this.kindToSearchPromise(kind)({ Active: true, AsListing: true }).then(r => {
                    if (!r || !r.data) {
                        reject("No data returned");
                        return;
                    }
                    this.typesCache[kind] = r.data.Results;
                    resolve(this.searchInner(kind, model));
                });
            });
        }
        private searchInner(kind: string, model: api.TypeSearchModel) {
            // TODO: Filter the data
            // if (!model || model === { }) {
                let retVal = [];
                Object.keys(this.typesCache[kind]).forEach(
                    key => retVal.push(this.typesCache[kind][key]));
                return retVal;
                // return;
            // }
        }
        getCached(lookup: ITypeLookupInterface): api.TypeModel {
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
            if (!this.typesCache[lookup.kind]) {
                this.typesCache[lookup.kind] = { };
            }
            return _.find(this.typesCache[lookup.kind], x => x.ID === lookup.id);
        }
        get(lookup: ITypeLookupInterface): ng.IPromise<api.TypeModel> {
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
                                }, result => reject(result))
                                .catch(reason => reject(reason));
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
                                }, result => reject(result))
                                .catch(reason => reject(reason));
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
                                }, result => reject(result))
                                .catch(reason => reject(reason));
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
                if (!this.typesCache[lookup.kind]) {
                    this.typesCache[lookup.kind] = { };
                }
                if (this.typesCache[lookup.kind][lookup.id]) {
                    resolve(this.typesCache[lookup.kind][lookup.id]);
                    return;
                } else if (this.byIDPromises[lookup.kind][lookup.id]) {
                    resolve(this.byIDPromises[lookup.kind][lookup.id]);
                    return;
                } else {
                    this.byIDPromises[lookup.kind][lookup.id] = this.kindToIDPromise(lookup.kind)(lookup.id).then(r => {
                        if (!r || !r.data) {
                            reject("ERROR! Unable to locate type by ID");
                            return;
                        }
                        // Save it so we don't have to look again and return the good item
                        resolve(this.typesCache[lookup.kind][lookup.id] = r.data);
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.types.loaded, lookup, r.data);
                    });
                }
            });
        }
        private kindToIDPromise(kind: string): (id: number) => ng.IHttpPromise<api.TypeModel> {
            switch (kind) {
                case "SalesOrder": { return this.cvApi.ordering.GetSalesOrderTypeByID; }
                case "SalesInvoice": { return this.cvApi.invoicing.GetSalesInvoiceTypeByID; }
                case "SalesQuote": { return this.cvApi.quoting.GetSalesQuoteTypeByID; }
                case "SalesReturn": { return this.cvApi.returning.GetSalesReturnTypeByID; }
                default: { throw new Error(`Unknown kind: '${kind}'`); }
            }
        }
        private kindToCustomKeyPromise(kind: string): (key: string) => ng.IHttpPromise<number> {
            switch (kind) {
                case "SalesOrder": { return this.cvApi.ordering.CheckSalesOrderTypeExistsByKey; }
                case "SalesInvoice": { return this.cvApi.invoicing.CheckSalesInvoiceTypeExistsByKey; }
                case "SalesQuote": { return this.cvApi.quoting.CheckSalesQuoteTypeExistsByKey; }
                case "SalesReturn": { return this.cvApi.returning.CheckSalesReturnTypeExistsByKey; }
                default: { throw new Error(`Unknown kind: '${kind}'`); }
            }
        }
        private kindToNamePromise(kind: string): (dto: { Name: string }) => ng.IHttpPromise<number> {
            switch (kind) {
                case "SalesOrder": { return this.cvApi.ordering.CheckSalesOrderTypeExistsByName; }
                case "SalesInvoice": { return this.cvApi.invoicing.CheckSalesInvoiceTypeExistsByName; }
                case "SalesQuote": { return this.cvApi.quoting.CheckSalesQuoteTypeExistsByName; }
                case "SalesReturn": { return this.cvApi.returning.CheckSalesReturnTypeExistsByName; }
                default: { throw new Error(`Unknown kind: '${kind}'`); }
            }
        }
        private kindToDisplayNamePromise(kind: string): (dto: { DisplayName: string }) => ng.IHttpPromise<number> {
            switch (kind) {
                case "SalesOrder": { return this.cvApi.ordering.CheckSalesOrderTypeExistsByDisplayName; }
                case "SalesInvoice": { return this.cvApi.invoicing.CheckSalesInvoiceTypeExistsByDisplayName; }
                case "SalesQuote": { return this.cvApi.quoting.CheckSalesQuoteTypeExistsByDisplayName; }
                case "SalesReturn": { return this.cvApi.returning.CheckSalesReturnTypeExistsByDisplayName; }
                default: { throw new Error(`Unknown kind: '${kind}'`); }
            }
        }
        private kindToSearchPromise(kind: string): (dto: api.TypeSearchModel) => ng.IHttpPromise<api.PagedResultsBase<api.TypeModel>> {
            switch (kind) {
                case "SalesOrder": { return this.cvApi.ordering.GetSalesOrderTypes; }
                case "SalesInvoice": { return this.cvApi.invoicing.GetSalesInvoiceTypes; }
                case "SalesQuote": { return this.cvApi.quoting.GetSalesQuoteTypes; }
                case "SalesReturn": { return this.cvApi.returning.GetSalesReturnTypes; }
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
