/**
 * <copyright file="cvCurrentStoreService.ts" company="clarity-ventures.com">
 * Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * </copyright>
 * @desc Clarity Ventures Current Store Angular Service. Determines the
 * current store based the url, it's sub-domain or a sub-directory for
 * showing content appropriate to the store
 */
module cef.store.services {
    export interface ICurrentStoreService {
        getCurrentStorePromise: (refresh?: boolean) => ng.IPromise<api.StoreModel>;
        haveFullStoreDetails: () => boolean;
        getStoreInventoryLocationsMatrixImmediate: () => Array<api.StoreInventoryLocationsMatrixModel>;
        getStoreInventoryLocationsMatrixPromise: () => ng.IPromise<Array<api.StoreInventoryLocationsMatrixModel>>;
    }

    export class CurrentStoreService implements ICurrentStoreService {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        // Properties
        private currentStore: api.StoreModel = null;
        private askingForFullDetails = false;
        private gotFullDetails = false;
        // Functions
        getCurrentStorePromise(refresh?: boolean): ng.IPromise<api.StoreModel> {
            var defer = this.$q.defer<api.StoreModel>();
            if (refresh) {
                this.askingForFullDetails = true;
                this.cvApi.stores.GetCurrentStore().then(response => {
                    if (!response || !response.data || !response.data.ActionSucceeded || !response.data.Result) {
                        this.askingForFullDetails = false;
                        defer.reject(response.data.Messages);
                        return;
                    }
                    this.setCurrentStoreByModel(response.data.Result);
                    defer.resolve(this.currentStore);
                    this.askingForFullDetails = false;
                    this.gotFullDetails = true;
                });
            } else {
                if (this.gotFullDetails) {
                    defer.resolve(this.currentStore);
                } else {
                    defer.reject("store not located");
                }
            }
            return defer.promise;
        }

        haveFullStoreDetails() { return this.gotFullDetails; }

        private clearCurrentStore(): void { this.currentStore = null; }
        private setCurrentUser(storeID: string | number, store?: api.StoreModel): void {
            if (store) { this.currentStore = store; }
            this.gotFullDetails = this.currentStore && this.currentStore.ID > 0 && this.currentStore.Name != null && this.currentStore.Name !== "";
        }
        private setCurrentStoreByModel(store: api.StoreModel): void {
            if (store) { this.currentStore = store; }
            this.gotFullDetails = this.currentStore && this.currentStore.ID > 0 && this.currentStore.Name != null && this.currentStore.Name !== "";
        }

        private cachedStoreInventoryLocationsMatrix: Array<api.StoreInventoryLocationsMatrixModel> = null;
        private deferIsRunning: boolean = false;
        private deferPromise: ng.IPromise<Array<api.StoreInventoryLocationsMatrixModel>> = null;
        getStoreInventoryLocationsMatrixImmediate(): Array<api.StoreInventoryLocationsMatrixModel> {
            if (!this.cefConfig.featureSet.stores.enabled) { return []; }
            return this.cachedStoreInventoryLocationsMatrix || [];
        }
        getStoreInventoryLocationsMatrixPromise(): ng.IPromise<Array<api.StoreInventoryLocationsMatrixModel>> {
            if (!this.cefConfig.featureSet.stores.enabled) { return this.$q.resolve([]); }
            var defer = this.$q.defer<Array<api.StoreInventoryLocationsMatrixModel>>();
            if (this.cachedStoreInventoryLocationsMatrix) {
                defer.resolve(this.cachedStoreInventoryLocationsMatrix);
            } else if (this.deferIsRunning) {
                return this.deferPromise;
            } else {
                // TODO@ME: Use Cached endpoint once proven works
                this.deferIsRunning = true;
                this.cvApi.stores.GetStoreInventoryLocationsMatrix()
                    .then(response => {
                        if (!response || !response.data || !response.data.ActionSucceeded) {
                            defer.reject("Failed to execute");
                            return;
                        }
                        this.cachedStoreInventoryLocationsMatrix = response.data.Result;
                        defer.resolve(this.cachedStoreInventoryLocationsMatrix);
                    }, () => defer.reject("Failed to execute"))
                    .catch(() => defer.reject("Failed to execute"))
                    .finally(() => { this.deferIsRunning = false; });
            }
            this.deferPromise = defer.promise;
            return defer.promise;
        }
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly cvApi: api.ICEFAPI,
                private readonly cefConfig: core.CefConfig) {
            if (!$rootScope.cvCurrentStoreService) { $rootScope.cvCurrentStoreService = this; }
            if (!this.cefConfig.featureSet.stores.enabled) { return; }
            this.getCurrentStorePromise().then(() => this.consoleLog("store check done"));
            this.getStoreInventoryLocationsMatrixPromise().then(() => { /*this.consoleLog("store inventory locations matrix done")*/ });
        }
    }
}
