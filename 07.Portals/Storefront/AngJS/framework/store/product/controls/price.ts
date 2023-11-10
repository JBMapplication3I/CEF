module cef.store.product.controls {
    class ProductPriceController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        private _product: api.ProductModel;
        private _productId: number;
        get productId(): number {
            return this._productId;
        }
        set productId(newValue: number) {
            if (!newValue) {
                return;
            }
            this._productId = newValue;
            this._product = null;
        }
        get product(): api.ProductModel {
            if (this._product) {
                return this._product;
            }
            if (this.productId) {
                this.setRunning();
                this.cvProductService.get({ id: this.productId }).then(p => {
                    this.product = p;
                    this.finishRunning();
                });
            }
            return null;
        }
        set product(value: api.ProductModel) {
            this._product = value;
        }
        withStyles: boolean = true;
        divisor: number = 1;
        multiplier: number = 1;
        showUnitOfMeasure: boolean;
        // Properties
        get prices(): api.CalculatedPrices {
            return this._product && this.product.readPrices && this.product.readPrices();
        }
        get lowestUOMPrice(): number {
            return this._product && this.product["$_rawPricesUOMs"] && this.product["$_rawPricesUOMs"][this.product.UnitOfMeasure].BasePrice;
        }
        get showLoginForPricing(): boolean {
            return this.cefConfig.loginForPricing.enabled
                && !this.cvAuthenticationService.isAuthenticated();
        }
        get loading(): boolean {
            return !this.product || !this.prices || this.prices.loading;
        }
        get visible(): boolean {
            return !this.loading
                ////&& this.product.TypeName !== "Kit"
                ////&& this.product.TypeName !== "Variant-Kit"
                && !this.showLoginForPricing;
        }
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvProductService: services.IProductService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefProductPrice", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=?",
            productId: "=?",
            withStyles: "@?",
            divisor: "=?",
            multiplier: "=?",
            showUnitOfMeasure: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/price.html", "ui"),
        controller: ProductPriceController,
        controllerAs: "pdpCtrl",
        bindToController: true
    }));
}
