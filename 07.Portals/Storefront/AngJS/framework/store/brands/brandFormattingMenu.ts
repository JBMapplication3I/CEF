module cef.store.brands {
    export class BrandFormattingMenuController extends BrandFormattingBaseController {
        // Properties
        menu: string;
        cachedMenuItems: any = null;
        // Functions
        parseMenu(): any {
            if (this.cachedMenuItems) {
                return this.cachedMenuItems;
            }
            if (!this.brand
                || !this.brand.BrandSiteDomains
                || this.brand.BrandSiteDomains.length <= 0
                || !this.brand.BrandSiteDomains[0].Slave
                || !this.brand.BrandSiteDomains[0].Slave.SerializableAttributes["Menu"]
                || !this.brand.BrandSiteDomains[0].Slave.SerializableAttributes["Menu"].Value) {
                return null;
            }
            const object = JSON.parse(this.brand.BrandSiteDomains[0].Slave.SerializableAttributes["Menu"].Value);
            if (!object) {
                return null;
            }
            this.cachedMenuItems = object;
            return this.cachedMenuItems;
        }
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvCurrentBrandService: services.ICurrentBrandService) {
            super($rootScope, cefConfig, cvCurrentBrandService);
        }
    }

    cefApp.directive("cefBrandFormattingMenu", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        replace: true,
        templateUrl: $filter("corsLink")("/framework/store/brands/brandFormattingMenu.html", "ui"),
        controller: BrandFormattingMenuController,
        controllerAs: "bfmc",
    }));
}
