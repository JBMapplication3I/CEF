/**
 * @file framework/store/_services/_DataServiceBase.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Data Service Service class, stores data that has been loaded
 * to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.store.services {
    export interface ILookupInterfaceBase {
        id?: number;
        key?: string;
        name?: string;
        ref?: string;
        force?: boolean;
    }

    export interface IDataServiceBase<TModel extends api.NameableBaseModel, TLookup extends ILookupInterfaceBase> {
        bulkGet(recordIDs: number[]): ng.IPromise<TModel[]>;
        get(lookup: TLookup): ng.IPromise<TModel>;
        getCached(lookup: TLookup): TModel;
        replace(id: number, value: TModel): void;
        remove(lookup: TLookup): ng.IPromise<boolean>;
    }

    export abstract class DataServiceBase<TModel extends api.NameableBaseModel, TLookup extends ILookupInterfaceBase>
            implements IDataServiceBase<TModel, TLookup> {
        // Properties
        protected recordCache: { [id: number]: TModel; } = { };
        protected nameToIDLookup: { [name: string]: number; } = { };
        protected keyToIDLookup: { [key: string]: number; } = { };
        protected byIDPromises: { [id: number]: ng.IPromise<void>; } = { };
        protected byKeyPromises: { [key: string]: ng.IPromise<number>; } = { };
        protected byNamePromises: { [name: string]: ng.IPromise<number>; } = { };
        // Abstracts
        abstract getByIDPromise: (id: number) => ng.IHttpPromise<TModel>;
        abstract getByIDsPromise: (ids: { IDs: number[]; }) => ng.IHttpPromise<api.PagedResultsBase<TModel>>;
        abstract checkExistsByIDPromise: (id: number) => ng.IHttpPromise<number | null>;
        abstract checkExistsByKeyPromise: (key: string) => ng.IHttpPromise<number | null>;
        abstract checkExistsByNamePromise: (name: { Name: string }) => ng.IHttpPromise<number | null>;
        // Functions
        getCached(lookup: TLookup): TModel {
            if (!lookup || !lookup.id && !lookup.name) {
                return null;
            }
            if (!lookup.id) {
                if (lookup.key) {
                    if (!this.keyToIDLookup[lookup.key]) {
                        this.get(lookup);
                        return null; // Waiting on promise to cache
                    }
                    lookup.id = this.keyToIDLookup[lookup.key];
                } else if (lookup.name) {
                    if (!this.nameToIDLookup[lookup.name]) {
                        this.get(lookup);
                        return null; // Waiting on promise to cache
                    }
                    lookup.id = this.nameToIDLookup[lookup.name];
                }
            }
            if (!lookup.id) {
                return null;
            }
            if (this.recordCache[lookup.id] && !lookup.force) {
                return this.recordCache[lookup.id];
            }
            this.get(lookup);
            return null; // Waiting on promise to cache
        }
        get(lookup: TLookup): ng.IPromise<TModel> {
            if (!lookup || !lookup.id && !lookup.name) {
                return this.$q.reject("ERROR! Nothing to look up by");
            }
            return this.$q((resolve, reject) => {
                if (!lookup.id) {
                    if (lookup.key) {
                        if (this.keyToIDLookup[lookup.key]) {
                            lookup.id = this.keyToIDLookup[lookup.key];
                        } else if (this.byKeyPromises[lookup.key]) {
                            resolve(this.byKeyPromises[lookup.key]);
                            return;
                        } else {
                            this.byKeyPromises[lookup.key] = this.checkExistsByKeyPromise(lookup.key).then(r => {
                                if (!r || !r.data) {
                                    reject("ERROR! Unable to locate record by Key");
                                    return;
                                }
                                // Save it so we don't have to look again
                                this.keyToIDLookup[lookup.key] = r.data;
                                // Loop back in with the id
                                const newLookup = <TLookup>{
                                    id: r.data,
                                    ref: lookup.ref
                                };
                                resolve(this.get(newLookup));
                            }).catch(reject);
                            return;
                        }
                    } else if (lookup.name) {
                        if (this.nameToIDLookup[lookup.name]) {
                            lookup.id = this.nameToIDLookup[lookup.name];
                        } else if (this.byNamePromises[lookup.name]) {
                            resolve(this.byNamePromises[lookup.name]);
                            return;
                        } else {
                            this.byNamePromises[lookup.name] = this.checkExistsByNamePromise({ Name: lookup.name }).then(r => {
                                if (!r || !r.data) {
                                    reject("ERROR! Unable to locate record by Name");
                                    return;
                                }
                                // Save it so we don't have to look again
                                this.nameToIDLookup[lookup.name] = r.data;
                                // Loop back in with the id
                                const newLookup = <TLookup>{
                                    id: r.data,
                                    ref: lookup.ref
                                };
                                resolve(this.get(newLookup));
                            }).catch(reject);
                            return;
                        }
                    }
                }
                if (!lookup.id) {
                    reject("ERROR! Nothing to look up by");
                    return;
                }
                if (this.recordCache[lookup.id] && !lookup.force) {
                    if (lookup.ref) {
                        const clone = angular.fromJson(angular.toJson(this.recordCache[lookup.id]));
                        clone["ref"] = lookup.ref;
                        resolve(clone);
                        return;
                    }
                    resolve(this.recordCache[lookup.id]);
                    return;
                } else if (this.byIDPromises[lookup.id]) {
                    // if (lookup.force) {
                    //     // Cancel the existing HHR request
                    //     try {
                    //         this.byIDPromises[lookup.id].abort();
                    //     } catch (Error) {
                    //         // Do Nothing
                    //     }
                    // } else {
                        resolve(this.byIDPromises[lookup.id]);
                        return;
                    // }
                }
                this.byIDPromises[lookup.id] = this.getByIDPromise(lookup.id).then(r => {
                    if (!r || !r.data) {
                        reject("ERROR! Unable to locate record by ID");
                        return;
                    }
                    const pre = r.data;
                    // Fix date formats for Available Start/End
                    /*
                    if (pre.AvailableStartDate) {
                        pre.AvailableStartDate = new Date(pre.AvailableStartDate.toString());
                    }
                    if (pre.AvailableEndDate) {
                        pre.AvailableEndDate = new Date(pre.AvailableEndDate.toString());
                    }
                    */
                    // If the images came with it, we need to ensure the Primary is in the data at
                    // first position of the array
                    if (pre["Images"] && pre["Images"].length > 1) {
                        pre["Images"] = pre["Images"].sort((a, b) => {
                            if (a.IsPrimary && !b.IsPrimary) {
                                return -1;
                            }
                            if (!a.IsPrimary && b.IsPrimary) {
                                return 1;
                            }
                            // Both are either primary or not primary
                            return 0;
                        });
                    }
                    // We need to trust several values that could contain HTML from the server
                    if (pre.Description) {
                        pre.Description = this.$sce.trustAsHtml(pre.Description);
                    }
                    if (pre.SerializableAttributes) {
                        Object.keys(pre.SerializableAttributes)
                            .forEach(key => pre.SerializableAttributes[key].Value
                                = this.$sce.trustAsHtml(pre.SerializableAttributes[key].Value));
                    }
                    // Save it so we don't have to look again
                    this.recordCache[lookup.id] = pre;
                    if (lookup.ref) {
                        const clone = angular.fromJson(angular.toJson(this.recordCache[lookup.id])) as TModel;
                        clone["ref"] = lookup.ref;
                        resolve(clone);
                        return;
                    }
                    resolve(this.recordCache[lookup.id]);
                }).catch(reject);
            });
        }
        bulkGet(recordIDs: number[]): ng.IPromise<TModel[]> {
            if (!recordIDs || !recordIDs.length) {
                return this.$q.reject("No IDs to search with");
            }
            const have: number[] = [];
            const have2: TModel[] = [];
            const need: number[] = [];
            recordIDs.forEach(x => {
                if (!this.recordCache[x]) {
                    need.push(x);
                    return;
                }
                have.push(x);
                have2.push(this.recordCache[x]);
            });
            if (!need.length) {
                // We already have all of these records
                return this.$q.resolve(have2);
            }
            return this.$q((resolve, reject) => {
                /* NOTE: This causes issue with too much data pulled at once from the server and redis caching
                // Using alternative version where it asks for each record individually
                const dto = { IDs: need, };
                this.getByIDsPromise(dto).then(r => {
                    if (!r || !r.data || !r.data.length) {
                        reject("No data returned");
                        return;
                    }
                    this.cvPricingService.bulkFactoryAssign(r.data)
                        .then(r2 => this.cvInventoryService.bulkFactoryAssign(r2)
                            .then(r3 => {
                                const final: TModel[] = [];
                                final.push(...have2);
                                r3.forEach(x => {
                                    this.recordCache[x.ID] = x;
                                    if (x.Name) {
                                        this.nameToIDLookup[x.Name] = x.ID;
                                    }
                                    if (x.CustomKey) {
                                        this.keyToIDLookup[x.CustomKey] = x.ID;
                                    }
                                    final.push(x);
                                });
                                resolve(final);
                            }).catch(reject)
                        ).catch(reject);
                }).catch(reject);
                */
                /*
                // NOTE: This method has issues where-in if any one promise in the chain fails,
                // then the entire catalog doesn't load
                // Alternative method: one at a time, but individually cached to memory
                this.$q.all(need.map(x => this.get({ id: x }))).then((rarr: TModel[]) => {
                    if (!rarr || !rarr || !rarr.length) {
                        reject("No data returned");
                        return;
                    }
                    this.cvPricingService.bulkFactoryAssign(rarr)
                        .then(r2 => this.cvInventoryService.bulkFactoryAssign(r2)
                            .then(r3 => {
                                const final: TModel[] = [];
                                final.push(...have2);
                                r3.forEach(x => {
                                    this.recordCache[x.ID] = x;
                                    if (x.Name) {
                                        this.nameToIDLookup[x.Name] = x.ID;
                                    }
                                    if (x.CustomKey) {
                                        this.keyToIDLookup[x.CustomKey] = x.ID;
                                    }
                                    final.push(x);
                                });
                                resolve(final);
                            }).catch(reject)
                        ).catch(reject);
                }).catch(reject);
                */
                // Alternative method #2: Create a timeout for a set of indexed promises so that if any promises aren't
                // fully resolved by the timeout, they will be clipped and, if allowed, retried, otherwise ignored and
                // just return the results which we did get
                this.cvPromiseFactory.addTimeoutToPromises(
                    2000, // timeout after x ms
                    3, // Retries allowed for failed/timed out promises
                    ...need.map(x => {
                        return {
                            key: String(x), // index it so results are coallated
                            promiseFn: () => this.get({ id: x } as any)
                        };
                    })
                ).then(indexedResults => {
                    const records: TModel[] = Object.keys(indexedResults)
                        .map(key => indexedResults[key]);
                    const final: TModel[] = [];
                    final.push(...have2);
                    records.forEach(x => {
                        this.recordCache[x.ID] = x;
                        if (x.Name) {
                            this.nameToIDLookup[x.Name] = x.ID;
                        }
                        if (x.CustomKey) {
                            this.keyToIDLookup[x.CustomKey] = x.ID;
                        }
                        final.push(x);
                    });
                    resolve(final);
                }).catch(reject);
            });
        }
        replace(id: number, value: TModel): void {
            this.recordCache[id] = value;
        }
        remove(lookup: TLookup): ng.IPromise<boolean> {
            return this.$q.reject("Not yet implemented");
        }
        // Constructor
        constructor(
            protected readonly $q: ng.IQService,
            protected readonly $sce: ng.ISCEService,
            protected readonly cvPromiseFactory: factories.IPromiseFactory) {
            // No additional actions at this level
        }
    }
}
