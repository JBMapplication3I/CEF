/**
 * @file framework/store/_services/cvAddressBookService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Address Book Service class, stores account contact data that has been
 * loaded to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.store.services {
    export interface IAddressLookup {
        id?: number;
        name?: string;
        mainKey?: string;
        contactKey?: string;
        addressKey?: string;
    }

    export interface IAddressBookService {
        // Properties
        defaultBillingID: number;
        defaultBilling: api.AccountContactModel;
        defaultShippingID: number;
        defaultShipping: api.AccountContactModel;
        noBillingContactFound: boolean;
        noPrimaryContactFound: boolean;
        primaryAndBillingAreSameContact: boolean;
        runningContactChecks: boolean;
        suggestions: Array<api.AccountContactPagedResults>;
        queryTerm: string;
        addressBookCache: { [id: number]: api.AccountContactModel }
        // Functions
        getBook(force?: boolean, paginated?: boolean): ng.IPromise<api.AccountContactModel[]>;
        getPrimaryShipping(force?: boolean): ng.IPromise<api.AccountContactModel>;
        getPrimaryBilling(force?: boolean): ng.IPromise<api.AccountContactModel>;
        getEntry(lookup: IAddressLookup, force?: boolean): ng.IPromise<api.AccountContactModel>;
        /**
         * @param {api.AccountContactModel} entry
         * @returns {ng.IPromise<number>} The ID of the edited record
         * @memberof IAddressBookService
         */
        updateEntry(entry: api.AccountContactModel): ng.IPromise<number>;
        /**
         * @param {api.AccountContactModel} entry
         * @returns {ng.IPromise<number>} The ID of the new record
         * @memberof IAddressBookService
         */
        addEntry(entry: api.AccountContactModel): ng.IPromise<number>;
        deleteEntry(lookup: IAddressLookup): ng.IPromise<boolean>;
        blankAccountContactModel(): api.AccountContactModel;
        reset(): ng.IPromise<void>;
        setEntryAsIsBilling(dontRefresh?: boolean): ng.IPromise<void>;
        setEntryAsIsPrimary(dontRefresh?: boolean): ng.IPromise<void>;
        refreshContactChecks(force: boolean, caller: string): ng.IPromise<void>;
        validate(contact: api.ContactModel, showModal?: boolean): ng.IPromise<api.CEFActionResponseT<api.ContactModel>>;
        checkReadOnlyAddress(addressFlag: boolean): ng.IPromise<boolean>;
    }

    export class AddressBookService implements IAddressBookService {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) {
                return;
            }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) {
                return;
            }
            console.log(...args);
        }
        // Properties
        addressBookCache: { [id: number]: api.AccountContactModel } = { };
        private addressBookPromise: ng.IPromise<void> = null;
        private keyToIDLookup: { [key: string]: number } = { };
        private nameToIDLookup: { [name: string]: number } = { };
        private byIDPromises: { [id: number]: ng.IPromise<void> } = { };
        private _defaultBillingID: number = null;
        get defaultBillingID(): number {
            return this._defaultBillingID;
        }
        set defaultBillingID(value: number) {
            if (this._defaultBillingID === value) { return; }
            this.consoleDebug(`AddressBookService.set_defaultBillingID oldValue: '${
                this._defaultBillingID}' newValue: '${value}'`);
            this._defaultBillingID = value;
        }
        defaultBilling: api.AccountContactModel = null;
        private _defaultShippingID: number = null;
        get defaultShippingID(): number {
            return this._defaultShippingID;
        }
        set defaultShippingID(value: number) {
            if (this._defaultShippingID === value) { return; }
            this.consoleDebug(`AddressBookService.set_defaultShippingID oldValue: '${
                this._defaultShippingID}' newValue: '${value}'`);
            this._defaultShippingID = value;
        }
        defaultShipping: api.AccountContactModel = null;
        noBillingContactFound: boolean;
        noPrimaryContactFound: boolean;
        primaryAndBillingAreSameContact: boolean;
        refreshContactChecksDefer: ng.IDeferred<void>;
        refreshContactChecksPromise: ng.IPromise<void>;
        addressBookIsEmpty = false;
        runningContactChecks: boolean = false;
        suggestions: Array<api.AccountContactPagedResults> = [];
        queryTerm: string = null;
        // Functions
        reset(): ng.IPromise<void> {
            const debug = `AddressBookService.reset()`;
            this.consoleDebug(`AddressBookService.${this.cvServiceStrings.events.addressBook.reset} broadcast`);
            this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.reset);
            return this.$q(resolve => {
                this.consoleDebug(`${debug} entered`);
                this.addressBookCache = {};
                this.addressBookPromise = null;
                this.keyToIDLookup = {};
                this.nameToIDLookup = {};
                this.byIDPromises = {};
                this.noBillingContactFound = true;
                this.noPrimaryContactFound = true;
                this.primaryAndBillingAreSameContact = false;
                this.addressBookIsEmpty = false;
                this.consoleDebug(`${debug} exited`);
                resolve();
            });
        }
        getBook(force: boolean = false, paginated: boolean = false): ng.IPromise<api.AccountContactModel[]> {
            return this.$q((resolve, reject) => {
                this.cvAuthenticationService.preAuth().finally(() => {
                    if (!this.cvAuthenticationService.isAuthenticated()) {
                        reject("Not logged in");
                        return;
                    }
                    const readLocalCache = (): void => {
                        let retVal = [];
                        Object.keys(this.addressBookCache).forEach(
                            key => retVal.push(this.addressBookCache[key]));
                        resolve(retVal);
                    };
                    if (!this.cefConfig.featureSet.addressBook.enabled) {
                        readLocalCache();
                        return;
                    }
                    if (!force && Object.keys(this.addressBookCache).length) {
                        // We're not forcing a refresh and already have the data
                        readLocalCache();
                        return;
                    }
                    if (!force && this.addressBookIsEmpty) {
                        resolve([]);
                        return;
                    }
                    if (!force && this.addressBookPromise) {
                        resolve(this.addressBookPromise);
                        return;
                    }
                    this.addressBookPromise = paginated
                        ? this.paginatedPromise(resolve, reject)
                        : this.notPaginatedPromise(resolve, reject);
                });
            });
        }
        getPrimaryShipping(force?: boolean): ng.IPromise<api.AccountContactModel> {
            return this.$q((resolve, reject) => {
                if (_.isObject(this.defaultShipping) && !force) {
                    resolve(this.defaultShipping);
                    return;
                }
                this.cvApi.geography.GetCurrentAccountPrimaryShippingAddress().then(result => {
                    this.defaultShipping = result.data;
                    this.defaultShippingID = result.data?.ID;
                    resolve(result.data);
                })
            })
        }
        getPrimaryBilling(force?: boolean): ng.IPromise<api.AccountContactModel> {
            return this.$q((resolve, reject) => {
                if (_.isObject(this.defaultBilling) && !force) {
                    resolve(this.defaultBilling);
                    return;
                }
                this.cvApi.geography.GetCurrentAccountPrimaryBillingAddress().then(result => {
                    this.defaultBilling = result.data;
                    this.defaultBillingID = result.data?.ID;
                    resolve(result.data);
                })
            })
        }
        notPaginatedPromise = (resolve, reject) => this.cvApi.geography.GetCurrentAccountAddressBook( { } ).then(r => {
            if (!r || !r.data) {
                reject("Unable to load the address book for the current user");
                return;
            }
            this.reset().then(() => {
                if (!r.data.length) {
                    // They don't have anything in their address book
                    this.consoleDebug(`AddressBookService.${this.cvServiceStrings.events.addressBook.loaded} broadcast`);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.loaded);
                    this.addressBookIsEmpty = true;
                    resolve([]);
                    return;
                }
                this.$q.all(r.data.map(ac => {
                    const dto = <api.ValidateAddressDto>{
                        AccountContactID: ac.ID,
                        ContactID: ac.SlaveID,
                        AddressID: ac.Slave && ac.Slave.AddressID,
                        Address: ac.Slave && ac.Slave.Address
                    };
                    if (ac.Slave
                        && ac.Slave.Address
                        && ac.Slave.Address.SerializableAttributes
                        && _.some(Object.keys(ac.Slave.Address.SerializableAttributes),
                            x => x.startsWith("Validated-By-"))) {
                        // Already validated
                        dto["IsValid"] = true;
                        return this.$q.resolve({ data: dto });
                    }
                    return this.cvApi.providers.ValidateAddress(dto);
                })).then((rarr: ng.IHttpPromiseCallbackArg<api.AddressValidationResultModel>[]) => {
                    rarr.forEach(entry => {
                        if (!entry.data.IsValid) {
                            this.consoleLog("Account contact was invalid, not applying to memory store");
                            this.consoleDebug(entry.data);
                            return;
                        }
                        const found = _.find(r.data, y => y.ID === entry.data.AccountContactID);
                        if (!found) {
                            return;
                        }
                        // TODO: Pop a modal to offer swapping to "fixed" address instead
                        ////this.consoleDebug("Account contact is 'valid', adding to view");
                        this.addressBookCache[found.ID] = found;
                        if (found.CustomKey) { this.keyToIDLookup[found.CustomKey] = found.ID; }
                        if (found.Slave.CustomKey) { this.keyToIDLookup[found.CustomKey] = found.ID; }
                        if (found.Slave.Address.CustomKey) { this.keyToIDLookup[found.Slave.Address.CustomKey] = found.ID; }
                        if (found.Name) { this.nameToIDLookup[found.Name] = found.ID; }
                    });
                    resolve(this.getBook(false));
                    this.consoleDebug(`AddressBookService.${this.cvServiceStrings.events.addressBook.loaded} broadcast`);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.loaded);
                });
            });
        });
        paginatedPromise = (resolve, reject) => this.cvApi.geography.GetAddressBookPaged( {
            Paging: {
                Size: 50,
                StartIndex: 1,
            }
        } ).then(r => {
            if (!r || !r.data) {
                reject("Unable to load the address book for the current user");
                return;
            }
            this.reset().then(() => {
                if (!r.data?.Results?.length) {
                    // They don't have anything in their address book
                    this.consoleDebug(`AddressBookService.${this.cvServiceStrings.events.addressBook.loaded} broadcast`);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.loaded);
                    this.addressBookIsEmpty = true;
                    resolve([]);
                    return;
                }
                this.$q.all(r.data.Results.map(ac => {
                    const dto = <api.ValidateAddressDto>{
                        AccountContactID: ac.ID,
                        ContactID: ac.SlaveID,
                        AddressID: ac.Slave && ac.Slave.AddressID,
                        Address: ac.Slave && ac.Slave.Address
                    };
                    if (ac.Slave
                        && ac.Slave.Address
                        && ac.Slave.Address.SerializableAttributes
                        && _.some(Object.keys(ac.Slave.Address.SerializableAttributes),
                            x => x.startsWith("Validated-By-"))) {
                        // Already validated
                        dto["IsValid"] = true;
                        return this.$q.resolve({ data: dto });
                    }
                    return this.cvApi.providers.ValidateAddress(dto);
                })).then((rarr: ng.IHttpPromiseCallbackArg<api.AddressValidationResultModel>[]) => {
                    rarr.forEach(entry => {
                        if (!entry.data.IsValid) {
                            this.consoleLog("Account contact was invalid, not applying to memory store");
                            this.consoleDebug(entry.data);
                            return;
                        }
                        const found = _.find(r.data.Results, y => y.ID === entry.data.AccountContactID);
                        if (!found) {
                            return;
                        }
                        // TODO: Pop a modal to offer swapping to "fixed" address instead
                        ////this.consoleDebug("Account contact is 'valid', adding to view");
                        this.addressBookCache[found.ID] = found;
                        if (found.CustomKey) { this.keyToIDLookup[found.CustomKey] = found.ID; }
                        if (found.Slave.CustomKey) { this.keyToIDLookup[found.CustomKey] = found.ID; }
                        if (found.Slave.Address.CustomKey) { this.keyToIDLookup[found.Slave.Address.CustomKey] = found.ID; }
                        if (found.Name) { this.nameToIDLookup[found.Name] = found.ID; }
                    });
                    resolve(this.getBook(false));
                    this.consoleDebug(`AddressBookService.${this.cvServiceStrings.events.addressBook.loaded} broadcast`);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.loaded);
                });
            });
        });
        getEntry(lookup: IAddressLookup, force: boolean = false): ng.IPromise<api.AccountContactModel> {
            if (!this.cefConfig.featureSet.addressBook.enabled) {
                return this.$q.resolve(null);
            }
            if (!lookup || !lookup.id && !lookup.name && !lookup.mainKey && !lookup.contactKey && !lookup.addressKey) {
                return this.$q.reject("ERROR! Nothing to look up by");
            }
            return this.$q((resolve, reject) => {
                if (!lookup.id) {
                    reject("Lookups without an account contact id are not implemented yet");
                    return;
                }
                // Resume
                if (!force && this.addressBookCache[lookup.id]) {
                    resolve(this.addressBookCache[lookup.id]);
                    return;
                } else if (!force && this.byIDPromises[lookup.id]) {
                    resolve(this.byIDPromises[lookup.id]);
                    return;
                }
                this.byIDPromises[lookup.id] = this.cvApi.accounts
                    .GetAccountContactByID(lookup.id).then(r => {
                        if (!r || !r.data) {
                            reject("ERROR! Unable to locate address book entry by ID");
                            return;
                        }
                        // Save it so we don't have to look again and return the good item
                        resolve(this.addressBookCache[lookup.id] = r.data);
                    }).catch(reject);
            });
        }
        updateEntry(entry: api.AccountContactModel, reload: boolean = true): ng.IPromise<number> {
            return this.$q((resolve, reject) => {
                this.cvApi.accounts.UpdateAccountContact(entry).then(r => {
                    if (!r || !r.data) {
                        ////console.warn("Unable to update address book entry");
                        reject();
                        return;
                    }
                    // Remove it from the cache
                    this.removeEntry({ id: r.data.Result }).finally(() => {
                        if (!reload) {
                            resolve(r.data.Result);
                            return;
                        }
                        // Get it again, fresh from the server
                        this.getBook(true)
                            .then(() => resolve(r.data.Result))
                            .catch(reject);
                    });
                }).catch(reject);
            });
        }
        addEntry(entry: api.AccountContactModel): ng.IPromise<number> {
            return this.$q((resolve, reject) => {
                this.cvAuthenticationService.getCurrentAccountPromise().then(a => {
                    entry.MasterID = a.ID;
                    this.cvApi.accounts.CreateAccountContact(entry).then(r => {
                        if (!r || !r.data) {
                            ////console.warn("Unable to add address book entry");
                            return;
                        }
                        if (!this.cefConfig.featureSet.addressBook.enabled) {
                            // We cannot store in an actual address book, add it to the local cache manually
                            this.cvApi.accounts.GetAccountContactByID(r.data.Result).then(r2 => {
                                if (!r2 || !r2.data) {
                                    reject("Could not read the entry that was just created");
                                    return;
                                }
                                this.addressBookCache[r.data.Result] = r2.data;
                                this.consoleDebug(`AddressBookService.${this.cvServiceStrings.events.addressBook.added} broadcast`);
                                this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.added, r.data.Result);
                                resolve(r.data.Result);
                            });
                            return;
                        }
                        // Pull it into the cache
                        this.getBook(true)
                            .then(() => resolve(r.data.Result))
                            .catch(reject);
                    }).catch(reject);
                });
            });
        }
        deleteEntry(lookup: IAddressLookup): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.getEntry(lookup)
                    .then(entry => this.cvApi.accounts.DeleteAccountContactByID(entry.ID)
                        .finally(() => resolve(this.removeEntry(lookup))))
                    .catch(reject);
            });
        }
        private removeEntry(lookup: IAddressLookup): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.getEntry(lookup).then(entry => {
                    delete this.addressBookCache[entry.ID];
                    if (this.defaultBillingID === entry.ID) {
                        this.defaultBillingID = null;
                        this.defaultBilling = null;
                    }
                    if (this.defaultShippingID === entry.ID) {
                        this.defaultShippingID = null;
                        this.defaultShipping = null;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.deleted, entry.ID);
                    resolve(true);
                }).catch(reject);
            });
        }
        blankAccountContactModel(): api.AccountContactModel {
            return <api.AccountContactModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                // NameableBase Properties
                Name: null,
                // AccountContact Properties
                IsBilling: false,
                IsPrimary: false,
                TransmittedToERP: false,
                // Related Objects
                MasterID: null,
                SlaveID: null,
                Slave: this.cvContactFactory.new()
            };
        }
        setEntryAsIsBilling(dontRefresh?: boolean): ng.IPromise<void> {
            if (!this.defaultBillingID) {
                return this.$q.reject("Default Billing contact not selected");
            }
            return this.$q((resolve, reject) => {
                this.cvApi.accounts.MarkAccountContactAsDefaultBilling(this.defaultBillingID).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        console.error(r && r.data);
                        reject(r && r.data);
                        return;
                    }
                    if (dontRefresh) {
                        this.getPrimaryBilling(true).then(result => {
                            resolve();
                            return;
                        });
                        return;
                    }

                    resolve(this.refreshContactChecks(true, "setEntryAsIsBilling"));
                }).catch(reject);
            });
        }
        setEntryAsIsPrimary(dontRefresh?: boolean): ng.IPromise<void> {
            if (!this.defaultShippingID) {
                return this.$q.reject("Primary Shipping contact not selected");
            }
            return this.$q((resolve, reject) => {
                this.cvApi.accounts.MarkAccountContactAsPrimaryShipping(this.defaultShippingID).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        console.error(r && r.data);
                        reject(r && r.data);
                        return;
                    }
                    if (dontRefresh) {
                        this.getPrimaryShipping(true).then(result => {
                            resolve();
                            return;
                        });
                        return;
                    }
                    resolve(this.refreshContactChecks(true, "setEntryAsIsPrimary"));
                }).catch(reject);
            });
        }
        refreshContactChecks(force: boolean, caller: string): ng.IPromise<void> {
            const debug = `AddressBookService.refreshContactChecks(force: ${force}, caller: '${caller}')`;
            this.consoleDebug(debug);
            this.runningContactChecks = true;
            if (!force && this.refreshContactChecksPromise) {
                this.consoleDebug(`${debug} not forced, promise already set`);
                return this.$q.resolve(this.refreshContactChecksPromise);
            }
            this.consoleDebug(`${debug} resetting flags`);
            this.noBillingContactFound = true;
            this.noPrimaryContactFound = true;
            this.primaryAndBillingAreSameContact = false;
            if (this.refreshContactChecksDefer) {
                // console.warn("setting this.refreshContactChecksPromise as rejected before replacing it");
                this.refreshContactChecksDefer.reject("Cancelled by new caller: " + caller);
            }
            this.refreshContactChecksDefer = this.$q.defer();
            // this.consoleDebug("setting this.refreshContactChecksPromise");
            this.refreshContactChecksPromise = this.refreshContactChecksDefer.promise;
            this.getBook(force).then(book => {
                if (!book) {
                    this.consoleDebug(`${debug} Book didn't load on this run`);
                    // resolve(this.refreshContactChecks(true, caller));
                    this.refreshContactChecksDefer.reject("Book didn't load on this run");
                    return;
                }
                this.consoleDebug(`${debug} book loaded`);
                book.forEach(entry => {
                    this.primaryAndBillingAreSameContact = this.primaryAndBillingAreSameContact
                        || entry.IsBilling && entry.IsPrimary;
                    if (entry.IsBilling) {
                        this.consoleDebug(`${debug} have billing`);
                        this.noBillingContactFound = false;
                        this.defaultBillingID = entry.ID;
                        this.consoleDebug(`${debug} defaultBillingID is now '${this.defaultBillingID}'`);
                        this.defaultBilling = entry;
                        if (this.defaultShippingID == entry.ID) {
                            // this.defaultShippingID = null;
                            this.consoleDebug(`${debug} defaultShippingID is now 'null'`);
                            // this.defaultShipping = null;
                        }
                    }
                    if (entry.IsPrimary) {
                        this.consoleDebug(`${debug} have shipping`);
                        this.noPrimaryContactFound = false;
                        this.defaultShippingID = entry.ID;
                        this.consoleDebug(`${debug} defaultShippingID is now '${this.defaultShippingID}'`);
                        this.defaultShipping = entry;
                        if (this.defaultBillingID == entry.ID) {
                            // this.defaultBillingID = null;
                            this.consoleDebug(`${debug} defaultBillingID is now 'null'`);
                            // this.defaultBilling = null;
                        }
                    }
                });
                this.consoleDebug(`${debug} finished`);
                this.refreshContactChecksDefer.resolve();
            }).catch(err => {
                console.error(err);
                this.refreshContactChecksDefer.reject(err);
            }).finally(() => {
                this.refreshContactChecksDefer = null;
                this.refreshContactChecksPromise = null;
                this.runningContactChecks = false;
            });
            return this.refreshContactChecksPromise;
        }
        validate(contact: api.ContactModel, showModal: boolean = false): ng.IPromise<api.CEFActionResponseT<api.ContactModel>> {
            return this.$q((resolve, reject) => {
                this.cvApi.providers.ValidateAddress({
                    AccountContactID: null,
                    ContactID: contact.ID,
                    AddressID: contact.AddressID,
                    Address: contact.Address
                }).then(r => {
                    if (!r.data.IsValid) {
                        if (r.data.Message == "No Region selected when selected Country has Regions") {
                            // The UI might be stuck without the regions select box, force a reload of the UI to show it
                            this.consoleDebug(`AddressBookService.${this.cvServiceStrings.events.addressBook.resetRegionDropdownNeeded} broadcast`);
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.resetRegionDropdownNeeded);
                            reject(r.data.Message);
                            return;
                        }
                        if (!showModal) {
                            reject(r.data.Message);
                            return;
                        }
                        let translated = "Address Validation Failed"; // Fallback value
                        this.$translate("ui.storefront.checkout.validateAddress.Failed")
                            .then(t => translated = t)
                            .finally(() => this.cvMessageModalFactory(translated)
                                .then(() => reject(r.data.Message)));
                        return;
                    }
                    contact.Address = r.data.MergedAddress || r.data.SourceAddress;
                    resolve(<api.CEFActionResponseT<api.ContactModel>>{
                        Result: contact,
                        ActionSucceeded: r.data.IsValid
                    });
                }).catch(reject);
            });
        }
        checkReadOnlyAddress(addressFlag: boolean): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.cvAuthenticationService.preAuth().finally(() => {
                    if (this.cvAuthenticationService.isAuthenticated()) {
                        reject("not logged in");
                        return;
                    }
                    this.cvAuthenticationService.getCurrentUserPromise(true).then(user => {
                        if (user.SerializableAttributes["ReadOnlyAddress"].Value) {
                            addressFlag = true;
                            resolve();
                            return;
                        }
                        addressFlag = false;
                        resolve();
                    }).catch(err => reject(err));
                });
            });
        }
        getSuggestionsPromise(viewValue: string): ng.IPromise<api.SuggestResultBase[]> {
            // this.setRunning();
            return this.$q<api.SuggestResultBase[]>((resolve, reject) => {
                if (!viewValue || !viewValue.trim()) {
                    this.suggestions = [];
                    // this.finishRunning();
                    resolve([]);
                    return;
                }
                const dto = <api.SuggestAddressBookCurrentAccountDto>{
                    IDOrCustomKeyOrNameOrDescription: viewValue,
                    Page: 1,
                    PageSize: 8,
                    PageSetSize: 1,
                    Sort: api.SearchSort.Relevance,
                };
                // if (this.cefConfig.featureSet.stores.enabled) {
                //     dto.StoreID = this.cvSearchCatalogService.activeSearchViewModel.Form.StoreID
                //         || this.cefConfig.catalog.onlyApplyStoreToFilterByUI
                //         ? this.cvSearchCatalogService.filterByStoreID
                //         : this.cvStoreLocationService.getUsersSelectedStore()
                //             && this.cvStoreLocationService.getUsersSelectedStore().ID;
                // }
                // this.setRunning();
                this.cvApi.geography.SuggestAddressBookCurrentAccount(dto).then(r => {
                    // @ts-ignore
                    this.suggestions = r.data.Results;
                    // this.finishRunning();
                    resolve(this.suggestions);
                }).catch(reason => {
                    // this.finishRunning();
                    reject(reason);
                } );
            });
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: IServiceStrings,
                private readonly cvContactFactory: factories.IContactFactory,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            $rootScope.$on(cvServiceStrings.events.auth.signIn, () => this.getBook(true));
            $rootScope.$on(cvServiceStrings.events.auth.signOut, () => this.reset());
        }
    }
}
