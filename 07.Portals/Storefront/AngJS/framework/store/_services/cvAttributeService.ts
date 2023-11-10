/**
 * @file framework/store/_services/cvAttributesService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Attributes Service class, stores the general attributes definitions
 * data that has been loaded to reduce calls to the server, and provide a
 * single source of information for all areas of the application.
 */
module cef.store.services {
    export interface IAttributeLookup {
        id?: number;
        key?: string;
        name?: string;
        displayName?: string;
    }

    export interface IAttributeService {
        search(model?: api.GeneralAttributeSearchModel): ng.IPromise<api.GeneralAttributeModel[]>;
        get(lookup: IAttributeLookup): ng.IPromise<api.GeneralAttributeModel>;
        read(lookup: IAttributeLookup, object: api.HaveJsonAttributesBaseModel)
            : ng.IPromise<{ attr: api.GeneralAttributeModel; value: api.SerializableAttributeObject }>;
        delete(lookup: IAttributeLookup, object: api.HaveJsonAttributesBaseModel): ng.IPromise<boolean>
    }

    export class AttributeService implements IAttributeService {
        private attributeCache: { [id: number]: api.GeneralAttributeModel } = { };
        private keyToIDLookup: { [key: string]: number } = { };
        private nameToIDLookup: { [name: string]: number } = { };
        private displayNameToIDLookup: { [displayName: string]: number } = { };
        private byIDPromises: { [id: number]: ng.IPromise<void> } = { };
        private byKeyPromises: { [key: string]: ng.IPromise<number> } = { };
        private byNamePromises: { [name: string]: ng.IPromise<number> } = { };
        private byDisplayNamePromises: { [displayName: string]: ng.IPromise<number> } = { };

        search(model: api.GeneralAttributeSearchModel = { Active: true, AsListing: true }): ng.IPromise<api.GeneralAttributeModel[]> {
            return this.$q((resolve, reject) => {
                if (!model || model.Active && model.AsListing) {
                    let retVal = [];
                    Object.keys(this.attributeCache).forEach(
                        key => retVal.push(this.attributeCache[key]));
                    resolve(retVal);
                    return;
                }
                this.cvApi.attributes.GetGeneralAttributes(model).then(r => {
                    if (!r || !r.data || !r.data.Results) {
                        reject("No data returned");
                        return;
                    }
                    resolve(r.data.Results);
                }).catch(reject);
            });
        }
        get(lookup: IAttributeLookup): ng.IPromise<api.GeneralAttributeModel> {
            if (!lookup || !lookup.id && !lookup.key && !lookup.name && !lookup.displayName) {
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
                            this.byKeyPromises[lookup.key] = this.cvApi.attributes
                                .CheckGeneralAttributeExistsByKey(lookup.key).then(r => {
                                    if (!r || !r.data) {
                                        reject("ERROR! Unable to locate product by Key");
                                        return;
                                    }
                                    // Save it so we don't have to look again
                                    this.keyToIDLookup[lookup.key] = r.data;
                                    // Loop back in with the id
                                    resolve(this.get({ id: r.data }));
                                }).catch(reason => reject(reason));
                            return;
                        }
                    } else if (lookup.name) {
                        if (this.nameToIDLookup[lookup.name]) {
                            lookup.id = this.nameToIDLookup[lookup.name];
                        } else if (this.byNamePromises[lookup.name]) {
                            resolve(this.byNamePromises[lookup.name]);
                            return;
                        } else {
                            this.byNamePromises[lookup.name] = this.cvApi.attributes
                                .CheckGeneralAttributeExistsByName({ Name: lookup.name }).then(r => {
                                    if (!r || !r.data) {
                                        reject("ERROR! Unable to locate product by Name");
                                        return;
                                    }
                                    // Save it so we don't have to look again
                                    this.nameToIDLookup[lookup.name] = r.data;
                                    // Loop back in with the id
                                    resolve(this.get({ id: r.data }));
                                }).catch(reason => reject(reason));
                            return;
                        }
                    } else if (lookup.displayName) {
                        if (this.displayNameToIDLookup[lookup.displayName]) {
                            lookup.id = this.displayNameToIDLookup[lookup.displayName];
                        } else if (this.byDisplayNamePromises[lookup.displayName]) {
                            resolve(this.byDisplayNamePromises[lookup.displayName]);
                            return;
                        } else {
                            this.byDisplayNamePromises[lookup.name] = this.cvApi.attributes
                                .CheckGeneralAttributeExistsByDisplayName({ DisplayName: lookup.displayName }).then(r => {
                                    if (!r || !r.data) {
                                        reject("ERROR! Unable to locate product by SeoUrl");
                                        return;
                                    }
                                    // Save it so we don't have to look again
                                    this.displayNameToIDLookup[lookup.displayName] = r.data;
                                    // Loop back in with the id
                                    resolve(this.get({ id: r.data }));
                                }).catch(reason => reject(reason));
                            return;
                        }
                    }
                }
                if (!lookup.id) {
                    reject("ERROR! Nothing to look up by");
                    return;
                }
                if (this.attributeCache[lookup.id]) {
                    resolve(this.attributeCache[lookup.id]);
                    return;
                } else if (this.byIDPromises[lookup.id]) {
                    resolve(this.byIDPromises[lookup.id]);
                    return;
                } else {
                    this.byIDPromises[lookup.id] = this.cvApi.attributes
                        .GetGeneralAttributeByID(lookup.id).then(r => {
                            if (!r || !r.data) {
                                reject("ERROR! Unable to locate attribute by ID");
                                return;
                            }
                            // Save it so we don't have to look again and return the good item
                            resolve(this.attributeCache[lookup.id] = r.data);
                        });
                }
            });
        }
        read(lookup: IAttributeLookup, object: api.HaveJsonAttributesBaseModel)
                : ng.IPromise<{ attr: api.GeneralAttributeModel; value: api.SerializableAttributeObject }> {
            return this.get(lookup).then(attr => {
                return {
                    attr: attr,
                    value: object.SerializableAttributes
                        && object.SerializableAttributes[attr.CustomKey]
                };
            });
        }
        delete(lookup: IAttributeLookup, object: api.HaveJsonAttributesBaseModel): ng.IPromise<boolean> {
            return this.get(lookup).then(attr => delete object.SerializableAttributes[attr.CustomKey]);
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly cvApi: api.ICEFAPI) {
            // Initial Data load
            cvApi.attributes.GetGeneralAttributes({ Active: true, AsListing: true, HideFromStorefront: false }).then(r => {
                if (!r || !r.data) {
                    console.warn("Attribute service was unable to load the data");
                    return;
                }
                r.data.Results.forEach(attr => {
                    this.attributeCache[attr.ID] = attr;
                    if (attr.CustomKey) { this.keyToIDLookup[attr.CustomKey] = attr.ID; }
                    if (attr.Name) { this.nameToIDLookup[attr.Name] = attr.ID; }
                    if (attr.DisplayName) { this.displayNameToIDLookup[attr.DisplayName] = attr.ID; }
                });
            });
        }
    }
}
