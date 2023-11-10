module cef.store.searchCatalog.controls.results {
    export abstract class ProductCardWidgetControllerBase extends core.TemplatedControllerBase {
        // Bound Scope Properties
        productId: number;
        // Properties
        get hideQuantitySelector(): boolean {
            return this.product
                && this.product.TypeName
                && this.product.TypeName.toLowerCase() == "variant master";
        }
        get product(): api.ProductModel {
            return this && this.cvProductService.getCached({ id: this.productId });
        }
        // Convenience Redirects (Reduce binding text/conditions)
        get auth(): services.IAuthenticationService {
            return this && this.cvAuthenticationService;
        }
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Functions
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
