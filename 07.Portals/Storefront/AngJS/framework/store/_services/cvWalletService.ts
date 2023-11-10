/**
 * @file framework/store/_services/cvWalletService.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Wallet Service class
 */
module cef.store.services {
    export interface IWalletLookup {
        id?: number;
        name?: string;
        key?: string;
    }

    export interface IWalletService {
        wallet: Array<api.WalletModel>;
        eCheckAccountTypes: Array<{ Key: string, Value: string }>;
        expirationMonths: Array<{ Key: number, Value: string }>;
        expirationYears: Array<{ Key: number, Value: number }>;
        onAccountUpdate: () => ng.IPromise<Array<api.WalletModel>>;
        getWallet: (force?: boolean) => ng.IPromise<Array<api.WalletModel>>;
        getBlankCard: () => api.WalletModel;
        getBlankEcheck: () => api.WalletModel;
        defaultEntryID: number;
        defaultEntry: api.WalletModel;
        noDefaultEntryFound: boolean;
        runningChecks: boolean;
        setEntryAsDefault: () => ng.IPromise<void>;
        refreshChecks: (force?: boolean, caller?: string) => ng.IPromise<void>;
        getEntry: (lookup: IWalletLookup, force?: boolean) => ng.IPromise<api.WalletModel>;
        addEntry: (entry: api.WalletModel) => ng.IPromise<number>;
        updateEntry: (entry: api.WalletModel, reload?: boolean) => ng.IPromise<number>;
        deleteEntry: (lookup: IWalletLookup) => ng.IPromise<boolean>;
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
        private walletCache: { [id: number]: api.WalletModel } = { };
        private byIDPromises: { [id: number]: ng.IPromise<void> } = { };
        wallet: Array<api.WalletModel> = [];
        refreshChecksPromise: ng.IPromise<void>;
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
        walletIsEmpty = false;
        // Default Entry
        private _defaultEntryID: number = null;
        get defaultEntryID(): number {
            return this._defaultEntryID;
        }
        set defaultEntryID(value: number) {
            this.consoleDebug(`WalletService.set_defaultEntryID oldValue: '${
                this._defaultEntryID}' newValue: '${value}'`);
            this._defaultEntryID = value;
        }
        defaultEntry: api.WalletModel;
        noDefaultEntryFound: boolean;
        setEntryAsDefault(): ng.IPromise<void> {
            if (!this.defaultEntryID) {
                return this.$q.reject("Default Entry not selected");
            }
            return this.$q((resolve, reject) => {
                this.cvApi.payments.CurrentUserMarkWalletEntryAsDefault(this.defaultEntryID).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        console.error(r && r.data);
                        reject(r && r.data);
                        return;
                    }
                    resolve(this.refreshChecks(true, "setEntryAsDefault"));
                }).catch(reject);
            });
        }
        // Checks
        runningChecks: boolean;
        refreshChecks(force?: boolean, caller?: string): ng.IPromise<void> {
            const debug = `WalletService.refreshContactChecks(force: ${force}, caller: '${caller}')`;
            this.consoleDebug(debug);
            this.runningChecks = true;
            if (!force && this.refreshChecksPromise) {
                this.consoleDebug(`${debug} not forced, promise already set`);
                return this.$q.resolve(this.refreshChecksPromise);
            }
            this.consoleDebug(`${debug} resetting flags`);
            this.noDefaultEntryFound = true;
            this.refreshChecksPromise = this.$q((resolve, reject) => {
                this.getWallet(force).then(wallet => {
                    if (!wallet) {
                        this.consoleDebug(`${debug} Wallet didn't load on this run`);
                        reject("Wallet didn't load on this run");
                        return;
                    }
                    this.consoleDebug(`${debug} wallet loaded`);
                    wallet.forEach(entry => {
                        if (entry.IsDefault) {
                            this.consoleDebug(`${debug} have default`);
                            this.noDefaultEntryFound = false;
                            this.defaultEntryID = entry.ID;
                            this.consoleDebug(`${debug} defaultEntryID is now '${this.defaultEntryID}'`);
                            this.defaultEntry = entry;
                        }
                    });
                    this.consoleDebug(`${debug} finished`);
                    resolve();
                }).catch(reject)
                .finally(() => {
                    delete this.refreshChecksPromise;
                    this.runningChecks = false;
                });
            });
            return this.refreshChecksPromise;
        }
        getEntry(lookup: IWalletLookup, force: boolean = false): ng.IPromise<api.WalletModel> {
            if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                return this.$q.resolve(null);
            }
            if (!lookup || !lookup.id && !lookup.name && !lookup.key) {
                return this.$q.reject("ERROR! Nothing to look up by");
            }
            return this.$q((resolve, reject) => {
                if (!lookup.id) {
                    reject("Lookups without a wallet entry id are not implemented yet");
                    return;
                }
                // Resume
                if (!force && this.walletCache[lookup.id]) {
                    resolve(this.walletCache[lookup.id]);
                    return;
                } else if (!force && this.byIDPromises[lookup.id]) {
                    resolve(this.byIDPromises[lookup.id]);
                    return;
                }
                this.byIDPromises[lookup.id] = this.cvApi.payments
                    .GetCurrentUserWalletEntryByID(lookup.id).then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded) {
                            reject("ERROR! Unable to locate wallet entry by ID");
                            return;
                        }
                        // Save it so we don't have to look again and return the good item
                        resolve(this.walletCache[lookup.id] = r.data.Result);
                    }).catch(reject);
            });
        }
        addEntry(entry: api.WalletModel): ng.IPromise<number> {
            return this.$q((resolve, reject) => {
                this.cvApi.payments.CreateCurrentUserWalletEntry(entry).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        reject("Unable to add wallet entry");
                        return;
                    }
                    if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                        // We cannot store in an actual wallet, add it to the local cache manually
                        this.cvApi.payments.GetCurrentUserWalletEntryByID(r.data.Result.ID).then(r2 => {
                            if (!r2 || !r2.data) {
                                reject("Could not read the entry that was just created");
                                return;
                            }
                            this.walletCache[r.data.Result.ID] = r2.data.Result;
                            this.consoleDebug(`WalletService.${this.cvServiceStrings.events.wallet.updated} broadcast`);
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.updated, "added", r.data.Result.ID);
                            resolve(r.data.Result.ID);
                        });
                        return;
                    }
                    // Pull it into the cache
                    this.getWallet(true)
                        .then(() => resolve(r.data.Result.ID))
                        .catch(reject);
                }).catch(reject);
            });
        }
        updateEntry(entry: api.WalletModel, reload: boolean = true): ng.IPromise<number> {
            return this.$q((resolve, reject) => {
                this.cvApi.payments.UpdateCurrentUserWalletEntry(entry).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        reject("Unable to update wallet entry");
                        return;
                    }
                    // Remove it from the cache
                    this.removeEntryFromLocalCache({ id: r.data.Result.ID }).finally(() => {
                        if (!reload) {
                            resolve(r.data.Result.ID);
                            return;
                        }
                        // Get it again, fresh from the server
                        this.getWallet(true)
                            .then(() => resolve(r.data.Result.ID))
                            .catch(reject);
                    });
                }).catch(reject);
            });
        }
        deleteEntry(lookup: IWalletLookup): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.getEntry(lookup)
                    .then(entry => this.cvApi.payments.DeactivateCurrentUserWalletEntry(entry.ID)
                        .finally(() => resolve(this.removeEntryFromLocalCache(lookup))))
                    .catch(reject);
            });
        }
        private removeEntryFromLocalCache(lookup: IWalletLookup): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.getEntry(lookup).then(entry => {
                    delete this.walletCache[entry.ID];
                    if (this.defaultEntryID === entry.ID) {
                        this.defaultEntryID = null;
                        this.defaultEntry = null;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.updated, "deleted", entry.ID);
                    resolve(true);
                }).catch(reject);
            });
        }
        // Functions
        onAccountUpdate(): ng.IPromise<Array<api.WalletModel>> {
            if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                return this.$q.resolve([]);
            }
            this.wallet = [];
            return this.getWallet(true);
        }
        private updateWallet(newWallet: Array<api.WalletModel>): Array<api.WalletModel> {
            if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                return [];
            }
            this.wallet = newWallet;
            this.refreshChecks(false, "updateWallet").finally(() => { });
            return this.wallet;
        }
        getWallet(force: boolean = false): ng.IPromise<Array<api.WalletModel>> {
            return this.$q((resolve, reject) => {
                if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                    resolve([]);
                    return;
                }
                if (!force && this.wallet && this.wallet.length) {
                    resolve(this.wallet)
                    return;
                }
                if (!force && this.walletIsEmpty) {
                    resolve([]);
                    return;
                }
                this.cvAuthenticationService.preAuth().finally(() => {
                    if (!this.cvAuthenticationService.isAuthenticated()) {
                        reject();
                        return;
                    }
                    this.cvApi.payments.GetCurrentUserWallet().then(r => {
                        if (!r.data.ActionSucceeded) {
                            this.wallet = [];
                            if (r.data.Messages && r.data.Messages[0] === "The result was an empty collection") {
                                this.walletIsEmpty = true;
                                resolve([]);
                                this.refreshChecks(false, "getWallet.Empty");
                                this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.loaded)
                                return;
                            }
                            reject(r.data.Messages);
                            this.refreshChecks(false, "getWallet.Error");
                            return;
                        }
                        resolve(this.updateWallet(r.data.Result));
                    }).catch(reason => {
                        this.wallet = [];
                        reject(reason);
                    });
                });
            });
        }
        getBlankCard(): api.WalletModel {
            const blank = <api.WalletModel>{
                Active: true,
                CreatedDate: new Date(),
                UserID: null,
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
                UserID: null,
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
                $q.all(monthNames.map(month => $translate(`ui.storefront.common.months.${month}`))).then(monthResponses => {
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
                $q.all(types.map(type => $translate(`ui.storefront.common.eCheckAccountTypes.${type}`))).then(typeResponses => {
                    this.eCheckAccountTypes = typeResponses.map((t: string, idx) => {
                        return { Key: types[idx], Value: t };
                    });
                }, () => {
                    this.eCheckAccountTypes = types.map((t: string, idx) => {
                        return { Key: types[idx], Value: t };
                    });
                });
            })(["Checking", "Savings"]);
            // Initial wallet request
            this.onAccountUpdate();
        }
    }
}
