/**
 * @file framework/admin/_services/cvRegionService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Region Service class, stores the region definitions data
 * that has been loaded to reduce calls to the server, and provide
 * a single source of information for all areas of the application.
 */
module cef.admin.services {
    export interface IRegionLookupInterface {
        id?: number;
        code?: string;
        key?: string;
        name?: string;
    }

    export interface IRegionService {
        search(model?: api.RegionSearchModel, force?: boolean)
            : ng.IPromise<api.RegionModel[]>;
        get(lookup: IRegionLookupInterface): ng.IPromise<api.RegionModel>;
        read(lookup: IRegionLookupInterface, object: api.RegionModel)
            : ng.IPromise<api.RegionModel>;
    }

    export class RegionService implements IRegionService {
        private cache: { [id: number]: api.RegionModel } = { };
        private searchCache: { [model: string]: api.RegionModel[] } = { };
        private searchPromises: { [model: string]: ng.IPromise<api.RegionModel[]> } = { };
        private codeToIDLookup: { [code: string]: number } = { };
        private keyToIDLookup: { [key: string]: number } = { };
        private nameToIDLookup: { [name: string]: number } = { };
        private byIDPromises: { [id: number]: ng.IPromise<void> } = { };
        private byCodePromises: { [code: string]: ng.IPromise<number> } = { };
        private byKeyPromises: { [key: string]: ng.IPromise<number> } = { };
        private byNamePromises: { [name: string]: ng.IPromise<number> } = { };

        private searchInner(
                model: api.RegionSearchModel,
                results: api.RegionModel[],
                populate = false)
                : api.RegionModel[] {
            if (populate) {
                this.searchCache[angular.toJson(model)] = results;
                results.forEach(x => {
                    this.cache[x.ID] = x;
                    if (x.CustomKey) { this.keyToIDLookup[x.CustomKey] = x.ID; }
                    if (x.Name) { this.nameToIDLookup[x.Name] = x.ID; }
                    if (x.Code) { this.codeToIDLookup[x.Code] = x.ID; }
                });
            }
            return this.searchCache[angular.toJson(model)];
        }

        search(model: api.RegionSearchModel = { Active: true, AsListing: true }, force = false): ng.IPromise<api.RegionModel[]> {
            if (!this.searchCache) {
                this.searchCache = { };
            }
            const modelToUse = model || { Active: true, AsListing: true };
            const modelToUseStringified = angular.toJson(modelToUse);
            const missing = !this.searchCache[modelToUseStringified];
            /* If we are not forcing a refresh of the information and we already have the
             * information, then just return what we already have instead of making another call */
            if (!force && !missing) {
                return this.$q.resolve(this.searchCache[modelToUseStringified]);
            }
            /* If we don't have the data yet, but already have a promise out, then just return that
             * promise instead of making another call */
            if (!force && missing && this.searchPromises[modelToUseStringified]) {
                return this.searchPromises[modelToUseStringified];
            }
            /* Otherwise we don't have the data or a promise out so we need to make one */
            return this.searchPromises[modelToUseStringified] = this.$q((resolve, reject) => {
                this.cvApi.geography.GetRegions(modelToUse)
                    .then(r => resolve(this.searchInner(modelToUse, r.data.Results, true)))
                    .catch(reject)
                    .finally(() => delete this.searchPromises[modelToUseStringified]);
            });
        }
        read(lookup: IRegionLookupInterface): ng.IPromise<api.RegionModel> {
            return this.get(lookup);
        }
        get(lookup: IRegionLookupInterface): ng.IPromise<api.RegionModel> {
            if (!lookup || !lookup.id && !lookup.key && !lookup.name && !lookup.code) {
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
                            this.byKeyPromises[lookup.key] = this.cvApi.geography
                                .CheckRegionExistsByKey(lookup.key).then(r => {
                                    if (!r || !r.data) {
                                        reject("ERROR! Unable to locate Region by Key");
                                        return;
                                    }
                                    // Save it so we don't have to look again
                                    this.keyToIDLookup[lookup.key] = r.data;
                                    // Loop back in with the id
                                    resolve(this.get({ id: r.data }));
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
                            this.byNamePromises[lookup.name] = this.cvApi.geography
                                .CheckRegionExistsByName({ Name: lookup.name }).then(r => {
                                    if (!r || !r.data) {
                                        reject("ERROR! Unable to locate Region by Name");
                                        return;
                                    }
                                    // Save it so we don't have to look again
                                    this.nameToIDLookup[lookup.name] = r.data;
                                    // Loop back in with the id
                                    resolve(this.get({ id: r.data }));
                                }).catch(reject);
                            return;
                        }
                    } else if (lookup.code) {
                        if (this.codeToIDLookup[lookup.code]) {
                            lookup.id = this.codeToIDLookup[lookup.code];
                        } else if (this.byCodePromises[lookup.code]) {
                            resolve(this.byCodePromises[lookup.code]);
                            return;
                        } else {
                            this.byCodePromises[lookup.name] = this.cvApi.geography
                                .CheckRegionExistsByCode({ Code: lookup.code }).then(r => {
                                    if (!r || !r.data) {
                                        reject("ERROR! Unable to locate Region by Code");
                                        return;
                                    }
                                    // Save it so we don't have to look again
                                    this.codeToIDLookup[lookup.code] = r.data;
                                    // Loop back in with the id
                                    resolve(this.get({ id: r.data }));
                                }).catch(reject);
                            return;
                        }
                    }
                }
                if (!lookup.id) {
                    reject("ERROR! Nothing to look up by");
                    return;
                }
                if (this.cache[lookup.id]) {
                    resolve(this.cache[lookup.id]);
                    return;
                }
                if (this.byIDPromises[lookup.id]) {
                    resolve(this.byIDPromises[lookup.id]);
                    return;
                }
                this.byIDPromises[lookup.id] = this.cvApi.geography
                    .GetRegionByID(lookup.id).then(r => {
                        if (!r || !r.data) {
                            reject("ERROR! Unable to locate Region by ID");
                            return;
                        }
                        // Save it so we don't have to look again and return the good record
                        resolve(this.cache[lookup.id] = r.data);
                    });
            });
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly cvApi: api.ICEFAPI) {
            // No Initial Data load
        }
    }
}
