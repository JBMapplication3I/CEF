/**
 * @file framework/admin/_services/cvAddressBookService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Address Book Service class, stores account contact data that has been
 * loaded to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.admin.services {
    export interface IAddressLookup {
        id?: number;
        name?: string;
        mainKey?: string;
        contactKey?: string;
        addressKey?: string;
    }

    export interface IAddressBookService {
        // Properties
        addressBookCache: { [accountID: number]: { [id: number]: api.AccountContactModel } };
        defaultBillingID: { [accountID: number]: number };
        defaultBilling: { [accountID: number]: api.AccountContactModel };
        defaultShippingID: { [accountID: number]: number };
        defaultShipping: { [accountID: number]: api.AccountContactModel };
        noBillingContactFound: { [accountID: number]: boolean };
        noPrimaryContactFound: { [accountID: number]: boolean };
        primaryAndBillingAreSameContact: { [accountID: number]: boolean };
        // Functions
        getBook(accountID: number, force?: boolean): ng.IPromise<api.AccountContactModel[]>;
        getEntry(accountID: number, lookup: IAddressLookup, force?: boolean): ng.IPromise<api.AccountContactModel>;
        /**
         * @param {api.AccountContactModel} entry
         * @returns {ng.IPromise<number>} The ID of the edited record
         * @memberof IAddressBookService
         */
        updateEntry(accountID: number, entry: api.AccountContactModel): ng.IPromise<number>;
        /**
         * @param {api.AccountContactModel} entry
         * @returns {ng.IPromise<number>} The ID of the new record
         * @memberof IAddressBookService
         */
        addEntry(accountID: number, entry: api.AccountContactModel): ng.IPromise<number>;
        deleteEntry(accountID: number, lookup: IAddressLookup): ng.IPromise<boolean>;
        blankAccountContactModel(): api.AccountContactModel;
        reset(): ng.IPromise<void>;
        setEntryAsIsBilling(accountID: number): ng.IPromise<void>;
        setEntryAsIsPrimary(accountID: number): ng.IPromise<void>;
        refreshContactChecks(accountID: number, force?: boolean): ng.IPromise<void>;
        validate(accountID: number, contact: api.ContactModel, showModal?: boolean): ng.IPromise<api.CEFActionResponseT<api.ContactModel>>;
    }

    export class AddressBookService implements IAddressBookService {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        // Properties
        addressBookCache: { [accountID: number]: { [id: number]: api.AccountContactModel } } = { };
        private addressBookPromise: { [accountID: number]: ng.IPromise<void> } = { };
        private keyToIDLookup: { [key: string]: number } = { };
        private nameToIDLookup: { [name: string]: number } = { };
        private byIDPromises: { [id: number]: ng.IPromise<void> } = { };
        private byKeyPromises: { [key: string]: ng.IPromise<number> } = { };
        private byNamePromises: { [name: string]: ng.IPromise<number> } = { };
        defaultBillingID: { [accountID: number]: number } = { };
        defaultBilling: { [accountID: number]: api.AccountContactModel } = { };
        defaultShippingID: { [accountID: number]: number } = { };
        defaultShipping: { [accountID: number]: api.AccountContactModel } = { };
        noBillingContactFound: { [accountID: number]: boolean } = { };
        noPrimaryContactFound: { [accountID: number]: boolean } = { };
        primaryAndBillingAreSameContact: { [accountID: number]: boolean } = { };
        refreshContactChecksPromise: ng.IPromise<void>;
        addressBookIsEmpty = false;
        // Functions
        reset(): ng.IPromise<void> {
            this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.reset);
            return this.$q(resolve => {
                this.addressBookCache = {};
                this.addressBookPromise = null;
                this.keyToIDLookup = {};
                this.nameToIDLookup = {};
                this.byIDPromises = {};
                this.byKeyPromises = {};
                this.byNamePromises = {};
                this.defaultShipping = {};
                this.defaultShippingID = {};
                this.defaultBilling = {};
                this.defaultBillingID = {};
                this.noBillingContactFound = {};
                this.noPrimaryContactFound = {};
                this.primaryAndBillingAreSameContact = {};
                resolve();
            });
        }
        getBook(accountID: number, force: boolean = false): ng.IPromise<api.AccountContactModel[]> {
            return this.$q((resolve, reject) => {
                if (!force
                    && this.addressBookCache[accountID]
                    && Object.keys(this.addressBookCache[accountID]).length) {
                    // We're not forcing a refresh and already have the data
                    let retVal = [];
                    Object.keys(this.addressBookCache[accountID]).forEach(
                        key => retVal.push(this.addressBookCache[accountID][key]));
                    resolve(retVal);
                    return;
                }
                if (!this.addressBookPromise) {
                    // Must have the main object to store promises
                    this.addressBookPromise = { };
                }
                if (!force
                    && this.addressBookPromise
                    && this.addressBookPromise[accountID]) {
                    resolve(this.addressBookPromise[accountID]);
                    return;
                }
                this.addressBookPromise[accountID] = this.cvApi.geography.GetAddressBookAsAdmin(accountID).then(r => {
                    if (!r || !r.data) {
                        reject("Unable to load the address book for the specified account");
                        return;
                    }
                    this.addressBookCache[accountID] = { };
                    if (!r.data.length) {
                        // They don't have anything in their address book
                        resolve([]);
                        return;
                    }
                    r.data.forEach(entry => {
                        this.addressBookCache[accountID][entry.ID] = entry;
                        if (entry.CustomKey) {
                            this.keyToIDLookup[entry.CustomKey] = entry.ID;
                        }
                        if (entry.Slave.CustomKey) {
                            this.keyToIDLookup[entry.Slave.CustomKey] = entry.ID;
                        }
                        if (entry.Slave.Address.CustomKey) {
                            this.keyToIDLookup[entry.Slave.Address.CustomKey] = entry.ID;
                        }
                        if (entry.Name) {
                            this.nameToIDLookup[entry.Name] = entry.ID;
                        }
                    });
                    resolve(this.getBook(accountID, false));
                });
            });
        }
        getEntry(accountID: number, lookup: IAddressLookup, force: boolean = false): ng.IPromise<api.AccountContactModel> {
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
                if (!force && this.addressBookCache[accountID][lookup.id]) {
                    resolve(this.addressBookCache[accountID][lookup.id]);
                    return;
                } else if (!force && this.byIDPromises[lookup.id]) {
                    resolve(this.byIDPromises[lookup.id]);
                    return;
                }
                this.byIDPromises[lookup.id] = this.cvApi.accounts.GetAccountContactByID(lookup.id).then(r => {
                    if (!r || !r.data) {
                        reject("ERROR! Unable to locate product by ID");
                        return;
                    }
                    // Save it so we don't have to look again and return the good item
                    resolve(this.addressBookCache[accountID][lookup.id] = r.data);
                });
            });
        }
        updateEntry(accountID: number, entry: api.AccountContactModel): ng.IPromise<number> {
            return this.$q((resolve, reject) => {
                this.cvApi.accounts.UpdateAccountContact(entry).then(r => {
                    if (!r || !r.data) {
                        ////console.warn("Unable to update address book entry");
                        reject();
                        return;
                    }
                    // Remove it from the cache
                    this.removeEntry(accountID, { id: r.data.Result }).finally(() => {
                        // Get it again, fresh from the server
                        this.getEntry(accountID, { id: r.data.Result }).then(() => {
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.editSave, {
                                restrictedShipping: false
                            });
                            resolve(r.data.Result);
                        }).catch(reject);
                    });
                }).catch(reject);
            });
        }
        addEntry(accountID: number, entry: api.AccountContactModel): ng.IPromise<number> {
            return this.$q((resolve, reject) => {
                entry.MasterID = accountID;
                this.cvApi.accounts.CreateAccountContact(entry).then(r => {
                    if (!r || !r.data) {
                        console.warn("Unable to add address book entry");
                        return;
                    }
                    // Pull it into the cache
                    this.getEntry(accountID, { id: r.data.Result })
                        .then(() => resolve(r.data.Result))
                        .catch(reject);
                }).catch(reject);
            });
        }
        deleteEntry(accountID: number, lookup: IAddressLookup): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.getEntry(accountID, lookup)
                    .then(entry => this.cvApi.accounts.DeleteAccountContactByID(entry.ID)
                        .finally(() => resolve(this.removeEntry(accountID, lookup))))
                    .catch(reject);
            });
        }
        private removeEntry(accountID: number, lookup: IAddressLookup): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.getEntry(accountID, lookup).then(entry => {
                    delete this.addressBookCache[accountID][entry.ID];
                    resolve(true);
                }).catch(reject);
            });
        }
        blankAccountContactModel(): api.AccountContactModel {
            return <api.AccountContactModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                IsBilling: false,
                IsPrimary: false,
                TransmittedToERP: false,
                Name: null,
                Description: null,
                MasterID: 0,
                Master: null,
                SlaveID: 0,
                Slave: this.cvContactFactory.new()
            };
        }
        setEntryAsIsBilling(accountId: number): ng.IPromise<void> {
            if (!this.defaultBillingID) {
                return this.$q.resolve();
            }
            return this.$q((resolve, reject) => {
                this.getBook(accountId).then(book => {
                    this.defaultBilling[accountId] = _.find(book, x => x.ID === this.defaultBillingID[accountId]);
                    let changed = false;
                    if (!this.defaultBilling[accountId].IsBilling) {
                        this.defaultBilling[accountId].IsBilling = true;
                        changed = true;
                    }
                    if (this.defaultBilling[accountId].IsPrimary) {
                        this.defaultBilling[accountId].IsPrimary = false;
                        changed = true;
                    }
                    (changed
                        ? this.updateEntry(accountId, this.defaultBilling[accountId])
                        : this.$q.resolve(this.defaultBillingID[accountId])
                    ).then(success => {
                        if (!success) {
                            reject();
                            return;
                        }
                        this.$q.all(book.filter(x => x.ID !== this.defaultBillingID[accountId]).map(e => {
                            if (!e.IsBilling) {
                                return this.$q.resolve(true);
                            }
                            e.IsBilling = false;
                            return this.updateEntry(accountId, e);
                        })).finally(() => this.refreshContactChecks(accountId, true).then(resolve));
                    }).catch(reject);
                }).catch(reject);
            });
        }
        setEntryAsIsPrimary(accountId: number): ng.IPromise<void> {
            if (!this.defaultShippingID) {
                return this.$q.resolve();
            }
            return this.$q((resolve, reject) => {
                this.getBook(accountId).then(book => {
                    this.defaultShipping[accountId] = _.find(book, x => x.ID === this.defaultShippingID[accountId]);
                    let changed = false;
                    if (this.defaultShipping[accountId].IsBilling) {
                        this.defaultShipping[accountId].IsBilling = false;
                        changed = true;
                    }
                    if (!this.defaultShipping[accountId].IsPrimary) {
                        this.defaultShipping[accountId].IsPrimary = true;
                        changed = true;
                    }
                    (changed
                        ? this.updateEntry(accountId, this.defaultShipping[accountId])
                        : this.$q.resolve(this.defaultShippingID[accountId])
                    ).then(success => {
                        if (!success) {
                            reject();
                            return;
                        }
                        this.$q.all(book.filter(x => x.ID !== this.defaultShippingID[accountId]).map(e => {
                            if (!e.IsPrimary) {
                                return this.$q.resolve(true);
                            }
                            e.IsPrimary = false;
                            return this.updateEntry(accountId, e);
                        })).finally(() => this.refreshContactChecks(accountId, true).then(resolve));
                    }).catch(reject);
                }).catch(reject);
            });
        }
        refreshContactChecks(accountId: number, force: boolean = false): ng.IPromise<void> {
            const debug = `AddressBookService.refreshContactChecks(force: ${force})`;
            this.consoleDebug(debug);
            if (!force && this.refreshContactChecksPromise) {
                return this.$q.resolve(this.refreshContactChecksPromise);
            }
            this.noBillingContactFound[accountId] = true;
            this.noPrimaryContactFound[accountId] = true;
            this.primaryAndBillingAreSameContact[accountId] = false;
            this.refreshContactChecksPromise = this.$q((resolve, reject) => {
                this.getBook(accountId, force).then(book => {
                    if (!book) {
                        this.consoleDebug(`${debug} Book didn't load on this run`);
                        reject("Book didn't load on this run");
                        return;
                    }
                    this.consoleDebug(`${debug} book loaded`);
                    book.forEach(entry => {
                        this.primaryAndBillingAreSameContact[accountId] = this.primaryAndBillingAreSameContact[accountId]
                            || entry.IsBilling && entry.IsPrimary;
                        if (entry.IsBilling) {
                            this.consoleDebug(`${debug} have billing`);
                            this.noBillingContactFound[accountId] = false;
                            this.defaultBillingID[accountId] = entry.ID;
                            this.defaultBilling[accountId] = entry;
                            if (this.defaultShippingID[accountId] == entry.ID) {
                                this.defaultShippingID[accountId] = null;
                                this.defaultShipping[accountId] = null;
                            }
                        }
                        if (entry.IsPrimary) {
                            this.consoleDebug(`${debug} have shipping`);
                            this.noPrimaryContactFound[accountId] = false;
                            this.defaultShippingID[accountId] = entry.ID;
                            this.defaultShipping[accountId] = entry;
                            if (this.defaultBillingID[accountId] == entry.ID) {
                                this.defaultBillingID[accountId] = null;
                                this.defaultBilling[accountId] = null;
                            }
                        }
                    });
                    this.consoleDebug(`${debug} finished`);
                    resolve();
                }).catch(reject);
            });
            return this.refreshContactChecksPromise;
        }
        validate(accountId: number, contact: api.ContactModel, showModal: boolean = false): ng.IPromise<api.CEFActionResponseT<api.ContactModel>> {
            return this.$q((resolve, reject) => {
                this.cvApi.providers.ValidateAddress({
                    AccountContactID: 0,
                    ContactID: contact.ID,
                    AddressID: contact.AddressID,
                    Address: contact.Address
                }).then(r => {
                    if (!r.data.IsValid) {
                        if (r.data.Message == "No Region selected when selected Country has Regions") {
                            // The UI might be stuck without the regions select box, force a reload of the UI to show it
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.resetRegionDropdownNeeded)
                            reject(r.data.Message);
                            return;
                        }
                        if (!showModal) {
                            reject(r.data.Message);
                            return;
                        }
                        let translated = "Address Validation Failed"; // Fallback value
                        this.$translate("ui.admin.checkout.validateAddress.Failed")
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
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvContactFactory: factories.IContactFactory,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory) {
            // No Constructor Actions to take
        }
    }
}
