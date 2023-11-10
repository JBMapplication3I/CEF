/**
 * @file framework/admin/_services/cvWalletService.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Wallet Service, provides a global storage for wallet access CSRs
 * for placing payments on the customer's behalf.
 */
module cef.admin.services {
    export interface IWalletLookup {
        id?: number;
        name?: string;
        key?: string;
    }

    export interface IWalletService {
        wallet: { [userID: number]: Array<api.WalletModel> };
        eCheckAccountTypes: Array<{ Key: string, Value: string }>;
        expirationMonths: Array<{ Key: number, Value: string }>;
        expirationYears: Array<{ Key: number, Value: number }>;
        getWallet: (userID: number, force?: boolean) => ng.IPromise<Array<api.WalletModel>>;
        getBlankCard: () => api.WalletModel;
        getBlankEcheck: () => api.WalletModel;
        getDefaultEntryID: (userID: number) => number;
        setDefaultEntryID: (userID: number, id: number) => void;
        defaultEntry: { [userID: number]: api.WalletModel; }
        noDefaultEntryFound: { [userID: number]: boolean; }
        runningChecks: { [userID: number]: boolean };
        refreshChecks: (userID: number, force?: boolean, caller?: string) => ng.IPromise<void>;
        getEntry: (userID: number, lookup: IWalletLookup, force?: boolean) => ng.IPromise<api.WalletModel>;
        addEntry: (userID: number, entry: api.WalletModel, isDefault?: boolean) => ng.IPromise<number>;
        updateEntry: (userID: number, entry: api.WalletModel, reload?: boolean) => ng.IPromise<number>;
        deleteEntry: (userID: number, lookup: IWalletLookup) => ng.IPromise<boolean>;
    }

    export class WalletService implements IWalletService {
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
        private walletCache: { [userID: number]: { [id: number]: api.WalletModel } } = { };
        private byIDPromises: { [userID: number]: { [id: number]: ng.IPromise<void> } } = { };
        wallet: { [userID: number]: Array<api.WalletModel> } = { };
        refreshChecksPromise: { [userID: number]: ng.IPromise<void> } = { };
        eCheckAccountTypes: Array<{ Key: string, Value: string }>;
        expirationMonths: Array<{ Key: number, Value: string }>;
        expirationYears = (() => {
            var out = [];
            var currentYear = new Date().getFullYear();
            for (let y = 0; y < 10; y++) {
                out.push({ Key: (currentYear - 2000) + y, Value: currentYear + y });
            }
            return out;
        })();
        walletIsEmpty: { [userID: number]: boolean } = { };
        // Default Entry
        private _defaultEntryID: { [userID: number]: number } = { };
        getDefaultEntryID(userID: number): number {
            return this._defaultEntryID[userID];
        }
        setDefaultEntryID(userID: number, value: number) {
            this.consoleDebug(`WalletService.set_defaultEntryID oldValue: '${
                this._defaultEntryID[userID]}' newValue: '${value}'`);
            this._defaultEntryID[userID] = value;
        }
        defaultEntry: { [userID: number]: api.WalletModel } = { };
        noDefaultEntryFound: { [userID: number]: boolean; } = { };
        // Checks
        runningChecks: { [userID: number]: boolean } = { };
        refreshChecks(userID: number, force?: boolean, caller?: string): ng.IPromise<void> {
            const debug = `WalletService.refreshContactChecks(force: ${force}, caller: '${caller}')`;
            this.consoleDebug(debug);
            this.runningChecks[userID] = true;
            if (!force && this.refreshChecksPromise) {
                this.consoleDebug(`${debug} not forced, promise already set`);
                return this.$q.resolve(this.refreshChecksPromise[userID]);
            }
            this.consoleDebug(`${debug} resetting flags`);
            this.noDefaultEntryFound[userID] = true;
            this.refreshChecksPromise[userID] = this.$q((resolve, reject) => {
                this.getWallet(userID, force).then(wallet => {
                    if (!wallet) {
                        this.consoleDebug(`${debug} Wallet didn't load on this run`);
                        reject("Wallet didn't load on this run");
                        return;
                    }
                    this.consoleDebug(`${debug} wallet loaded`);
                    wallet.forEach(entry => {
                        if (entry.IsDefault) {
                            this.consoleDebug(`${debug} have default`);
                            this.noDefaultEntryFound[userID] = false;
                            this.setDefaultEntryID(userID, entry.ID);
                            this.consoleDebug(`${debug} defaultEntryID is now '${this.getDefaultEntryID(userID)}'`);
                            this.defaultEntry[userID] = entry;
                        }
                    });
                    this.consoleDebug(`${debug} finished`);
                    resolve();
                }).catch(reject)
                .finally(() => {
                    delete this.refreshChecksPromise;
                    this.runningChecks[userID] = false;
                });
            });
            return this.refreshChecksPromise[userID];
        }
        getEntry(userID: number, lookup: IWalletLookup, force: boolean = false): ng.IPromise<api.WalletModel> {
            if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                return this.$q.resolve(null);
            }
            if (!userID || !lookup || !lookup.id && !lookup.name && !lookup.key) {
                return this.$q.reject("ERROR! Nothing to look up by");
            }
            return this.$q((resolve, reject) => {
                if (!lookup.id) {
                    reject("Lookups without a wallet entry id are not implemented yet");
                    return;
                }
                // Resume
                if (!force && this.walletCache[userID] && this.walletCache[userID][lookup.id]) {
                    resolve(this.walletCache[userID][lookup.id]);
                    return;
                } else if (!force && this.byIDPromises[userID] && this.byIDPromises[userID][lookup.id]) {
                    resolve(this.byIDPromises[userID][lookup.id]);
                    return;
                }
                if (!this.byIDPromises[userID]) {
                    this.byIDPromises[userID] = { };
                }
                this.byIDPromises[userID][lookup.id] = this.cvApi.payments
                    .GetUserWalletEntryByIDAsCSR(userID, lookup.id).then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded) {
                            reject("ERROR! Unable to locate wallet entry by ID");
                            return;
                        }
                        // Save it so we don't have to look again and return the good item
                        if (!this.walletCache[userID]) {
                            this.walletCache[userID] = { };
                        }
                        resolve(this.walletCache[userID][lookup.id] = r.data.Result);
                    }).catch(reject);
            });
        }
        removeDefaultWalletEntries(userID: number): ng.IPromise<api.WalletModel> {
            return this.$q((resolve, reject) => {
                this.getWallet(userID).then(wallet => {
                    wallet.forEach((entry) => {
                        if (entry.IsDefault) {
                            entry.IsDefault = false;
                            this.cvApi.payments.UpdateWallet(entry).then(r => {
                                if (!r || !r.data) {
                                    reject("Unable to remove existing default wallet entries");
                                    return;
                                }
                            }).catch(reject);
                        }
                    });
                    resolve(this.wallet[userID]);
                }).catch(reject);
            });
        }
        addEntry(userID: number, entry: api.WalletModel, isDefault: boolean = false): ng.IPromise<number> {
            return this.$q((resolve, reject) => {
                if (isDefault) {
                    this.removeDefaultWalletEntries(userID);
                    entry.IsDefault = true;
                }
                entry.UserID = userID;
                this.cvApi.payments.CreateUserWalletEntryAsCSR(userID, entry).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        reject("Unable to add wallet entry");
                        return;
                    }
                    if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                        // We cannot store in an actual wallet, add it to the local cache manually
                        this.cvApi.payments.GetUserWalletEntryByIDAsCSR(userID, r.data.Result.ID).then(r2 => {
                            if (!r2 || !r2.data) {
                                reject("Could not read the entry that was just created");
                                return;
                            }
                            if (!this.walletCache[userID]) {
                                this.walletCache[userID] = { };
                            }
                            this.walletCache[userID][r.data.Result.ID] = r2.data.Result;
                            this.consoleDebug(`WalletService.${this.cvServiceStrings.events.wallet.added} broadcast`);
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.added, r.data.Result.ID);
                            resolve(r.data.Result.ID);
                        });
                        return;
                    }
                    // Pull it into the cache
                    this.getWallet(userID, true)
                        .then(() => {
                            if (isDefault) {
                                this.setDefaultEntryID(userID, r.data.Result.ID);
                            }
                            resolve(r.data.Result.ID);
                        })
                        .catch(reject);
                }).catch(reject);
            });
        }
        updateEntry(userID: number, entry: api.WalletModel, reload: boolean = true): ng.IPromise<number> {
            return this.$q((resolve, reject) => {
                entry.UserID = userID;
                this.cvApi.payments.UpdateUserWalletEntryAsCSR(userID, entry).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        reject("Unable to update wallet entry");
                        return;
                    }
                    // Remove it from the cache
                    this.removeEntryFromLocalCache(userID, { id: r.data.Result.ID }).finally(() => {
                        if (!reload) {
                            resolve(r.data.Result.ID);
                            return;
                        }
                        // Get it again, fresh from the server
                        this.getWallet(userID, true)
                            .then(() => resolve(r.data.Result.ID))
                            .catch(reject);
                    });
                }).catch(reject);
            });
        }
        deleteEntry(userID: number, lookup: IWalletLookup): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.getEntry(userID, lookup)
                    .then(entry => this.cvApi.payments.DeactivateUserWalletEntryAsCSR(userID, entry.ID)
                        .finally(() => resolve(this.removeEntryFromLocalCache(userID, lookup))))
                    .catch(reject);
            });
        }
        private removeEntryFromLocalCache(userID: number, lookup: IWalletLookup): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.getEntry(userID, lookup).then(entry => {
                    delete this.walletCache[userID][entry.ID];
                    if (this.getDefaultEntryID(userID) === entry.ID) {
                        this.setDefaultEntryID(userID, null);
                        this.defaultEntry[userID] = null;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.updated, userID, "deleted", entry.ID);
                    resolve(true);
                }).catch(reject);
            });
        }
        // Functions
        private updateWallet(userID: number, newWallet: Array<api.WalletModel>): Array<api.WalletModel> {
            if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                return [];
            }
            this.wallet[userID] = newWallet;
            this.refreshChecks(userID, false, "updateWallet").finally(() => { });
            return this.wallet[userID];
        }
        getWallet(userID: number, force: boolean = false): ng.IPromise<Array<api.WalletModel>> {
            return this.$q((resolve, reject) => {
                if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                    resolve([]);
                    return;
                }
                if (!force && this.wallet && this.wallet[userID] && this.wallet[userID].length) {
                    resolve(this.wallet[userID])
                    return;
                }
                if (!force && this.walletIsEmpty[userID]) {
                    resolve([]);
                    return;
                }
                this.cvAuthenticationService.preAuth().finally(() => {
                    if (!this.cvAuthenticationService.isAuthenticated()) {
                        reject();
                        return;
                    }
                    this.cvApi.payments.GetUserWalletAsCSR(userID).then(r => {
                        if (!r.data.ActionSucceeded) {
                            this.wallet[userID] = [];
                            if (r.data.Messages && r.data.Messages[0] === "The result was an empty collection") {
                                this.walletIsEmpty[userID] = true;
                                resolve([]);
                                this.refreshChecks(userID, false, "getWallet.Empty").finally(() => { });
                                return;
                            }
                            reject(r.data.Messages);
                            this.refreshChecks(userID, false, "getWallet.Error").finally(() => { });
                            return;
                        }
                        resolve(this.updateWallet(userID, r.data.Result));
                    }).catch(reason => {
                        this.wallet[userID] = [];
                        reject(reason);
                        this.refreshChecks(userID, false, "getWallet.ErrorCatch").finally(() => { });
                    }).finally(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.loaded, userID));
                });
            });
        }
        getBlankCard(): api.WalletModel {
            const blank = <api.WalletModel>{
                Active: true,
                CreatedDate: new Date(),
                UserID: 0,
                Name: null,
                CreditCardNumber: null,
                CardHolderName: null,
                ExpirationMonth: null,
                ExpirationYear: null,
                CardType: null,
            };
            (blank as any).isNew = true;
            return blank;
        }
        getBlankEcheck(): api.WalletModel {
            const blank = <api.WalletModel>{
                Active: true,
                CreatedDate: new Date(),
                UserID: 0,
                Name: null,
                RoutingNumber: null,
                AccountNumber: null,
                BankName: null,
                CardType: "Checking",
                CardHolderName: null,
            };
            (blank as any).isNew = true;
            return blank;
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                readonly $translate: ng.translate.ITranslateService,
                private readonly cvApi: api.ICEFAPI,
                private readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            // Translate month names and eCheck account types
            (monthNames => {
                $q.all(monthNames.map(month => $translate(`ui.admin.checkout.views.paymentInformation.Months.${month}`))).then(monthResponses => {
                    this.expirationMonths = monthResponses.map((monthResponse: string, idx) => {
                        return { Key: (idx+1), Value: monthResponse };
                    });
                }, () => {
                    this.expirationMonths = monthNames.map((month, idx) => {
                        return { Key: (idx + 1), Value: month };
                    });
                });
            })(["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]);
            (types => {
                $q.all(types.map(type => $translate(`ui.admin.common.eCheckAccountTypes.${type}`))).then(typeResponses => {
                    this.eCheckAccountTypes = typeResponses.map((t: string, idx) => {
                        return { Key: types[idx], Value: t };
                    });
                }, () => {
                    this.eCheckAccountTypes = types.map((t: string, idx) => {
                        return { Key: types[idx], Value: t };
                    });
                });
            })(["Checking", "Savings"]);
        }
    }
}
