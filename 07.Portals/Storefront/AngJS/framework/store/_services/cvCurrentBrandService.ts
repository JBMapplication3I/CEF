/**
 * <copyright file="cvCurrentBrandService.ts" company="clarity-ventures.com">
 * Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * </copyright>
 * <summary>Clarity Ventures Current Brand Angular Service. Determines the
 * current brand based the url, it's sub-domain or a sub-directory for
 * showing content appropriate to the brand</summary>
 */
module cef.store.services {
    export interface ICurrentBrandService {
        getCurrentBrandPromise: (refresh?: boolean) => ng.IPromise<api.BrandModel>;
        haveFullBrandDetails: () => boolean;
    }

    export class CurrentBrandService implements ICurrentBrandService {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        // Properties
        private currentBrandPromise: ng.IPromise<api.BrandModel>;
        private currentBrand: api.BrandModel = null;
        private askingForFullDetails = false;
        private gotFullDetails = false;
        // Functions
        getCurrentBrandPromise(refresh?: boolean): ng.IPromise<api.BrandModel> {
            return this.$q.resolve({} as api.BrandModel);
            // if (!refresh && this.currentBrand) {
            //     return this.$q.resolve(this.currentBrand);
            // }
            // if (!refresh && !this.currentBrand && this.currentBrandPromise) {
            //     return this.currentBrandPromise;
            // }
            // this.currentBrandPromise = this.cvApi.brands.GetCurrentBrand().then(r => {
            //     if (!r || !r.data || !r.data.ActionSucceeded || !r.data.Result) {
            //         return undefined;
            //     }
            //     return this.currentBrand = r.data.Result;
            // });
            // return this.currentBrandPromise;
            /*
            var defer = this.$q.defer<api.BrandModel>();
            if (refresh) {
                this.askingForFullDetails = true;
                this.cvApi.brands.GetCurrentBrand().then(response => {
                    if (!response || !response.data || !response.data.ActionSucceeded || !response.data.Result) {
                        this.askingForFullDetails = false;
                        defer.reject(response.data.Messages);
                        return;
                    }
                    this.setCurrentBrandByModel(response.data.Result);
                    defer.resolve(this.currentBrand);
                    this.askingForFullDetails = false;
                    this.gotFullDetails = true;
                });
            } else {
                if (this.gotFullDetails) {
                    defer.resolve(this.currentBrand);
                } else {
                    defer.reject("brand not located");
                }
            }
            return defer.promise;
            */
        }
        haveFullBrandDetails(): boolean { return !!this.currentBrand; }
        private clearCurrentBrand(): void { this.currentBrand = null; }
        private setCurrentUser(brandID: string | number, brand?: api.BrandModel): void {
            if (brand) { this.currentBrand = brand; }
            this.gotFullDetails = this.currentBrand && this.currentBrand.ID > 0 && this.currentBrand.Name != null && this.currentBrand.Name !== "";
        }
        private setCurrentBrandByModel(brand: api.BrandModel): void {
            if (brand) { this.currentBrand = brand; }
            this.gotFullDetails = this.currentBrand && this.currentBrand.ID > 0 && this.currentBrand.Name != null && this.currentBrand.Name !== "";
        }
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            if (!$rootScope.cvCurrentBrandService) { $rootScope.cvCurrentBrandService = this; }
            // this.getCurrentBrandPromise(true).then(() => this.consoleLog("brand check done"));
        }
    }
}
