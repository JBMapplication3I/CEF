/**
 * @file framework/store/_services/cvCurrencyService.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Currency Service, provides calls for managing the user's selected currency
 * as well as converting from one currency to another using server-side calls
 */
module cef.store.services {
    export interface ICurrencyService {
        getCurrentCurrencyCached: () => api.CurrencyModel;
        getCurrentCurrencyPromise: (refresh?: boolean) => ng.IPromise<api.CurrencyModel>;
        getAvailableCurrencies: () => ng.IHttpPromise<api.CurrencyPagedResults>;
        getCurrencyByKey: (key: string) => ng.IHttpPromise<api.CurrencyModel>;
        convertCurrencyAToB(value: number | string, alreadyCurrency?: string): ng.IPromise<number>;
        changeToCurrencyPromise: (key: string) => ng.IPromise<boolean>;
        setCurrentCurrencyByModel(currency: api.CurrencyModel): void;
        setCurrentCurrency(currencyID: string | number, currency?: api.CurrencyModel): void;
        clearCurrentCurrency(): void;
    }

    export class CurrencyService implements ICurrencyService {
        // Properties
        private readonly cookieName = "CURRENCY_KEY";
        private currentCurrency: api.CurrencyModel;
        private convertedCurrency: ng.IHttpPromiseCallbackArg<void>;
        private convertCurrencyValueAtoBDto: api.ConvertCurrencyValueAtoBDto = {} as any;
        private getPromiseInstance: { [key: string]: ng.IHttpPromise<api.CurrencyModel> } = {};
        private currency: ng.IPromise<number>;
        // Functions
        private getCookiesOptions(): ng.cookies.ICookiesOptions {
            return <ng.cookies.ICookiesOptions>{
                path: "/",
                domain: this.cefConfig.useSubDomainForCookies || !this.subdomain
                    ? this.$location.host()
                    : this.$location.host().replace(this.subdomain, "")
            };
        }
        getCurrentCurrencyCached(): api.CurrencyModel {
            if (!this.currentCurrency) {
                // Trigger it if not already triggered
                this.getCurrentCurrencyPromise();
                return null;
            }
            return this.currentCurrency;
        }
        getCurrentCurrencyPromise(refresh?: boolean): ng.IPromise<api.CurrencyModel> {
            return this.$q((resolve, reject) => {
                let key = !this.currentCurrency || !this.currentCurrency.CreatedDate
                    ? this.$cookies.get(this.cookieName)
                    : this.currentCurrency.CustomKey;
                if (!key) { key = "USD"; }
                if (this.getPromiseInstance[key]) {
                    // Already getting from server, don't ask again
                    this.getPromiseInstance[key]
                        .then(r => resolve(this.currentCurrency))
                        .catch(reason => reject(reason));
                    return;
                }
                if (!refresh && this.currentCurrency.CreatedDate) {
                    // Already got it from the server, send back what we got
                    resolve(this.currentCurrency);
                    return;
                }
                // Get it fresh from the server
                (this.getPromiseInstance[key] = this.cvApi.currencies.GetCurrencyByKey(key)).then(r => {
                    this.setCurrentCurrencyByModel(r.data);
                    resolve(this.currentCurrency);
                    delete this.getPromiseInstance[key];
                }).catch(reason => {
                    reject(reason);
                    delete this.getPromiseInstance[key];
                });
            });
        }
        clearCurrentCurrency(): void {
            this.currentCurrency = <api.CurrencyModel>{};
            this.$cookies.remove(this.cookieName, this.getCookiesOptions());
        }
        setCurrentCurrency(currencyID: string | number, currency?: api.CurrencyModel): void {
            if (currency) {
                this.currentCurrency = currency;
            }
            this.$cookies.put(this.cookieName, String(currencyID), this.getCookiesOptions());
            this.$rootScope.$broadcast(this.cvServiceStrings.events.currency.changeFinished, this.currentCurrency.CustomKey);
        }
        setCurrentCurrencyByModel(currency: api.CurrencyModel): void {
            if (currency) {
                this.currentCurrency = currency;
                this.$cookies.put(this.cookieName, currency.CustomKey, this.getCookiesOptions());
                this.$rootScope.$broadcast(this.cvServiceStrings.events.currency.changeFinished, this.currentCurrency.CustomKey);
            }
        }
        changeToCurrencyPromise(key: string): ng.IPromise<boolean> {
            return this.$q<boolean>((resolve, reject) => {
                if (!key) {
                    reject(false); // Not allowed to clear currency
                    return;
                }
                if (this.currentCurrency.CustomKey === key) {
                    resolve(true); // Already on this currency
                    return;
                }
                this.getCurrencyByKey(key).then(r => {
                    if (!r || !r.data) {
                        reject(false); // Currency not found
                        return;
                    }
                    this.setCurrentCurrencyByModel(r.data); // Applying found currency
                    resolve(true);
                });
            });
        }
        convertCurrencyAToB(value: string | number, alreadyCurrency: string = null): ng.IPromise<number> {
            return this.$q((resolve, reject) => {
                if (!this.currentCurrency.CustomKey) {
                    reject("No current currency key");
                    return;
                }
                if ((alreadyCurrency || /*this.cefConfig.defaultCurrency ||*/ "USD") === this.currentCurrency.CustomKey) {
                    // Just return the original since they match
                    resolve(value);
                    return;
                }
                const dto = <api.ConvertCurrencyValueAtoBDto>{
                    Value: Number(value),
                    KeyA: alreadyCurrency || /*this.cefConfig.defaultCurrency ||*/ "USD",
                    KeyB: this.currentCurrency.CustomKey
                };
                this.cvApi.currencies.ConvertCurrencyValueAtoB(dto).then(r => {
                    if (!r || angular.isUndefined(r.data)) {
                        reject("Couldn't convert currency");
                        throw Error("Couldn't convert currency");
                    }
                    resolve(r.data);
                });
            });
        }
        getAvailableCurrencies(): ng.IHttpPromise<api.CurrencyPagedResults> {
            return this.cvApi.currencies.GetCurrencies({ Active: true, AsListing: true });
        }
        getCurrencyByKey(key: string): ng.IHttpPromise<api.CurrencyModel> {
            if (!key) { return null; }
            return this.cvApi.currencies.GetCurrencyByKey(key);
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly $cookies: ng.cookies.ICookiesService,
                private readonly $location: ng.ILocationService,
                private readonly subdomain: string,
                private readonly cvApi: api.ICEFAPI,
                private readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings) {
            //// if (!$cookies.get(this.cookieName)) {
            ////     $cookies.put(this.cookieName, "USD", this.getCookiesOptions());
            ////     this.$rootScope.$broadcast(this.cvServiceStrings.events.currency.changeFinished, "USD");
            //// }
            //// this.currentCurrency = <api.CurrencyModel>{
            ////     Active: true,
            ////     CreatedDate: null,
            ////     CustomKey: $cookies.get(this.cookieName) || "USD",
            ////     UnicodeSymbolValue: null,
            ////     Name: null,
            //// };
            ////let go: string = null;
            this.getCurrentCurrencyPromise(true).then(cur => {
                if (!this.cefConfig.featureSet.brands.enabled) {
                    return;
                }
                this.$q.all([
                    ////this.cvApi.brands.GetBrands({ Active: true, AsListing: true }),
                    this.cvApi.brands.GetBrandCurrencies({ Active: true, AsListing: true }),
                    this.cvApi.brands.GetBrandSiteDomains({ Active: true, AsListing: true }),
                    this.cvApi.stores.GetSiteDomains({ Active: true, AsListing: true })
                ]).then((rarr: ng.IHttpPromiseCallbackArg<any>[]) => {
                    let index = -1;
                    ////const brands            = rarr[++index].data.Results as api.BrandModel[];
                    const brandCurrencies   = rarr[++index].data.Results as api.BrandCurrencyModel[];
                    const brandSiteDomains  = rarr[++index].data.Results as api.BrandSiteDomainModel[];
                    const siteDomains       = rarr[++index].data.Results as api.SiteDomainModel[];
                    const host = window.location.host;
                    for (let i = 0; i < siteDomains.length; i++) {
                        const element = siteDomains[i];
                        const sdUrl = element.Url.replace(/https?\:\/\//, "");
                        if (host !== sdUrl) {
                            continue;
                        }
                        const sdID = element.ID;
                        const found = _.find(brandSiteDomains, x => x.SlaveID === sdID);
                        if (!found) {
                            continue;
                        }
                        const bID = found.MasterID;
                        if (brandCurrencies && brandCurrencies.length) {
                            const filtered = brandCurrencies.filter(x => x.MasterID === bID);
                            if (!filtered.length) {
                                continue;
                            }
                            const primary = _.find(filtered, x => x.IsPrimary) || filtered[0];
                            const primaryKey = primary.SlaveKey.toUpperCase();
                            if (cur.CustomKey !== primaryKey) {
                                this.changeToCurrencyPromise(primaryKey).then(() => void 0);
                                return;
                            }
                        }
                    }
                }).catch(console.error);
            });
        }
    }
}
