module cef.store.product.controls.cards {
    export abstract class ProductCardGenWidgetControllerBase extends core.TemplatedControllerBase {
        // Bound Scope Properties
        productId: number;
        productSeoUrl: string;
        productName: string;
        // Properties
        private _product: api.ProductModel;
        get product(): api.ProductModel {
            return this
                && (this._product
                    || this.cvProductService.getCached({
                        id: this.productId,
                        name: this.productName,
                        seoUrl: this.productSeoUrl
                    }));
        }
        set product(value: api.ProductModel) {
            if (value) {
                this._product = value;
            }
        }
        // Convenience Redirects (Reduce binding text/conditions)
        get auth(): services.IAuthenticationService {
            return this && this.cvAuthenticationService;
        }
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Functions
        /** NOTE: Call this in the inherited class */
        protected load(): void {
            if (this.product) {
                // All good, already have the data
                return;
            }
            if (!this.productId && !this.productName && !this.productSeoUrl) {
                console.warn("Cannot load product for card widget");
                return;
            }
            this.setRunning();
            this.cvProductService.get({
                id: this.productId,
                name: this.productName,
                seoUrl: this.productSeoUrl
            }).then(p => {
                this.product = p;
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Events
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }
}
