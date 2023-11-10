/**
 * @file framework/store/_services/cvCategoryService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Category Service class, stores category data that has been loaded
 * to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.store.services {
    export interface ICategoryLookupInterface extends ILookupInterfaceBase {
        seoUrl?: string;
    }

    export interface ICategoryService extends IDataServiceBase<api.CategoryModel, ICategoryLookupInterface> {
    }

    export class CategoryService extends DataServiceBase<api.CategoryModel, ICategoryLookupInterface> implements ICategoryService {
        // Properties
        private seoUrlToIDLookup: { [seoUrl: string]: number } = { };
        private bySeoUrlPromises: { [seoUrl: string]: ng.IPromise<number> } = { };
        // Abstracts
        getByIDPromise = (id: number) => this.cvApi.categories.GetCategoryByID({
            ID: id,
            ExcludeProductCategories: true
        });
        getByIDsPromise = (ids: { IDs: Array<number> }) => this.cvApi.categories.GetCategories({
            IDs: ids.IDs,
            ExcludeProductCategories: true,
            IncludeChildrenInResults: false
        });
        checkExistsByIDPromise      = this.cvApi.categories.CheckCategoryExistsByID;
        checkExistsByKeyPromise     = this.cvApi.categories.CheckCategoryExistsByKey;
        checkExistsByNamePromise    = this.cvApi.categories.CheckCategoryExistsByName;
        // Functions
        getCached(lookup: ICategoryLookupInterface): api.CategoryModel {
            if (!lookup || !lookup.id && !lookup.name && !lookup.seoUrl) {
                return null;
            }
            if (!lookup.id) {
                if (lookup.name) {
                    if (this.nameToIDLookup[lookup.name]) {
                        lookup.id = this.nameToIDLookup[lookup.name];
                    } else {
                        this.get(lookup);
                        return null; // Waiting on promise to cache
                    }
                } else if (lookup.seoUrl) {
                    if (this.seoUrlToIDLookup[lookup.seoUrl]) {
                        lookup.id = this.seoUrlToIDLookup[lookup.seoUrl];
                    } else {
                        this.get(lookup);
                        return null; // Waiting on promise to cache
                    }
                }
            }
            if (!lookup.id) {
                return null;
            }
            if (this.recordCache[lookup.id]) {
                return this.recordCache[lookup.id];
            }
            this.get(lookup);
            return null; // Waiting on promise to cache
        }
        get(lookup: ICategoryLookupInterface): ng.IPromise<api.CategoryModel> {
            if (!lookup || !lookup.id && !lookup.name && !lookup.seoUrl && !lookup.key) {
                return this.$q.reject("ERROR! Nothing to look up by");
            }
            return this.$q((resolve, reject) => {
                if (!lookup.id) {
                    if (lookup.name) {
                        if (this.nameToIDLookup[lookup.name]) {
                            lookup.id = this.nameToIDLookup[lookup.name];
                        } else if (this.byNamePromises[lookup.name]) {
                            resolve(this.byNamePromises[lookup.name]);
                            return;
                        } else {
                            this.byNamePromises[lookup.name] = this.cvApi.categories
                                .CheckCategoryExistsByName({ Name: lookup.name }).then(r => {
                                    if (!r || !r.data) {
                                        reject("ERROR! Unable to locate category by Name");
                                        return;
                                    }
                                    // Save it so we don't have to look again
                                    this.nameToIDLookup[lookup.name] = r.data;
                                    // Loop back in with the id
                                    resolve(this.get({ id: r.data }));
                                }, result => reject(result))
                                .catch(reason => reject(reason));
                            return;
                        }
                    } else if (lookup.seoUrl) {
                        if (this.seoUrlToIDLookup[lookup.seoUrl]) {
                            lookup.id = this.seoUrlToIDLookup[lookup.seoUrl];
                        } else if (this.bySeoUrlPromises[lookup.seoUrl]) {
                            resolve(this.bySeoUrlPromises[lookup.seoUrl]);
                            return;
                        } else {
                            this.bySeoUrlPromises[lookup.name] = this.cvApi.categories
                                .CheckCategoryExistsBySeoUrl({ SeoUrl: lookup.seoUrl }).then(r => {
                                    if (!r || !r.data) {
                                        reject("ERROR! Unable to locate category by SeoUrl");
                                        return;
                                    }
                                    // Save it so we don't have to look again
                                    this.seoUrlToIDLookup[lookup.seoUrl] = r.data;
                                    // Loop back in with the id
                                    resolve(this.get({ id: r.data }));
                                }, result => reject(result))
                                .catch(reason => reject(reason));
                            return;
                        }
                    } else if (lookup.key) {
                        if (this.keyToIDLookup[lookup.key]) {
                            lookup.id = this.keyToIDLookup[lookup.key];
                        } else if (this.byKeyPromises[lookup.key]) {
                            resolve(this.byKeyPromises[lookup.key]);
                            return;
                        } else {
                            this.byKeyPromises[lookup.name] = this.cvApi.categories
                                .CheckCategoryExistsByKey(lookup.key).then(r => {
                                    if (!r || !r.data) {
                                        reject("ERROR! Unable to locate category by Key");
                                        return;
                                    }
                                    // Save it so we don't have to look again
                                    this.keyToIDLookup[lookup.key] = r.data;
                                    // Loop back in with the id
                                    resolve(this.get({ id: r.data }));
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
                if (this.recordCache[lookup.id]) {
                    resolve(this.recordCache[lookup.id]);
                    return;
                }
                if (this.byIDPromises[lookup.id]) {
                    resolve(this.byIDPromises[lookup.id]);
                    return;
                }
                this.byIDPromises[lookup.id] = this.cvApi.categories.GetCategoryByID({
                    ID: lookup.id,
                    ExcludeProductCategories: true
                }).then(r => {
                    if (!r || !r.data) {
                        reject("ERROR! Unable to locate category by ID");
                        return;
                    }
                    const pre = r.data;
                    // If the images came with it, we need to ensure the Primary is in the data at
                    // first position of the array
                    if (pre.Images && pre.Images.length > 1) {
                        pre.Images = pre.Images.sort((a, b) => {
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
                            .forEach(key => pre.SerializableAttributes[key].Value =
                                this.$sce.trustAsHtml(pre.SerializableAttributes[key].Value));
                    }
                    // Save it so we don't have to look again
                    this.recordCache[lookup.id] = pre;
                    resolve(this.recordCache[lookup.id]);
                });
            });
        }
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly $sce: ng.ISCEService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvInventoryService: IInventoryService,
                protected readonly cvPricingService: IPricingService,
                protected readonly cvPromiseFactory: factories.IPromiseFactory,
                protected readonly cvStoreLocationService: IStoreLocationService) {
            super($q, $sce, cvPromiseFactory);
        }
    }
}
