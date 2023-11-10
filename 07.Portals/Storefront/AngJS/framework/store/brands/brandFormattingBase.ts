module cef.store.brands {
    export abstract class BrandFormattingBaseController extends core.TemplatedControllerBase {
        // Properties
        brand: api.BrandModel;
        get siteDomain(): api.SiteDomainModel {
            return this.$rootScope["globalBrandSiteDomain"];
        }
        // Functions
        private loadBrand(): void {
            this.cvCurrentBrandService.getCurrentBrandPromise(!this.cvCurrentBrandService.haveFullBrandDetails()).then(b => {
                if (!b) {
                    console.error("BrandFormattingBaseController: Unable to get brand");
                    return;
                }
                this.brand = b;
            });
        }
        protected load(): void {
            if (!this.cefConfig.featureSet.brands.enabled) {
                return;
            }
            this.loadBrand();
        }
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvCurrentBrandService: services.ICurrentBrandService) {
            super(cefConfig);
            this.load();
        }
    }
}
