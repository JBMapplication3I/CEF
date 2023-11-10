module cef.store.product.controls {
    class ProductStockController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        product: api.ProductModel;
        // Properties
        get visible(): boolean {
            return this.cefConfig
                && this.cefConfig.featureSet.inventory.enabled
                && (!this.cefConfig.loginForInventory.enabled
                    || this.cvAuthenticationService.isAuthenticated());
        }
        get loading(): boolean {
            if (!this.cvInventoryService || !this.product) {
                return true;
            }
            return this.cvInventoryService.factoryAssignWithUOMs(this.product).readInventory().loading;
        }
        // Functions
        sumInventoryQuantities(p: api.ProductModel = this.product): number {
            if (p == null || this.loading) {
                return 0;
            }
            return this.cvInventoryService.factoryAssignWithUOMs(p).readInventory().QuantityOnHand;
        }
        showUnlimitedStock(): boolean {
            if (this.loading) {
                return false;
            }
            return this.product.readInventory().IsUnlimitedStock;
        }
        showStockAmount(): boolean {
            if (this.loading) {
                return false;
            }
            return !this.product.readInventory().IsUnlimitedStock
                && angular.isDefined(this.sumInventoryQuantities(this.product))
                && this.sumInventoryQuantities(this.product) > 0;
        }
        showStockAmountForUOMs(): boolean {
            if (this.loading) {
                return false;
            }
            return !this.product.readInventory().IsUnlimitedStock
                && angular.isDefined(this.sumInventoryQuantities(this.product))
                && this.sumInventoryQuantities(this.product) > 0
                && Math.floor(this.product["$_rawInventoryUOMs"].find(x => x.ProductUOM === this.product.UnitOfMeasure).QuantityOnHand) > 0;
        }
        getUOMStockQuantity(p: api.ProductModel = this.product): number {
            if (!Math.floor(p["$_rawInventoryUOMs"].find(x => x.ProductUOM === p.UnitOfMeasure).QuantityOnHand)) {
                return 0;
            }
            return Math.floor(p["$_rawInventoryUOMs"].find(x => x.ProductUOM === p.UnitOfMeasure).QuantityOnHand)
        }
        showOnBackOrder(): boolean {
            if (this.loading) {
                return false;
            }
            return !this.product.readInventory().IsUnlimitedStock
                && this.product.readInventory().AllowBackOrder
                && (angular.isUndefined(this.sumInventoryQuantities(this.product))
                    || this.sumInventoryQuantities(this.product) <= 0);
        }
        showOutOfStock(): boolean {
            if (this.loading) {
                return false;
            }
            return !this.product.readInventory().IsUnlimitedStock
                && !this.product.readInventory().AllowBackOrder
                && angular.isDefined(this.sumInventoryQuantities(this.product))
                && this.sumInventoryQuantities(this.product) <= 0;
        }
        showNotifyMe(): boolean {
            if (this.loading) {
                return false;
            }
            return !this.product.readInventory().IsUnlimitedStock
                && (angular.isUndefined(this.sumInventoryQuantities(this.product))
                    || this.sumInventoryQuantities(this.product) <= 0)
                && !this.cvCartService.cartContainsItem(
                        this.product.ID,
                        this.cvServiceStrings.carts.types.notifyMe);
        }
        showDontNotifyMe(): boolean {
            if (this.loading) {
                return false;
            }
            return !this.product.readInventory().IsUnlimitedStock
                && (angular.isUndefined(this.sumInventoryQuantities(this.product))
                    || this.sumInventoryQuantities(this.product) <= 0)
                && this.cvCartService.cartContainsItem(
                        this.product.ID,
                        this.cvServiceStrings.carts.types.notifyMe);
        }
        click(add: boolean): void {
            if (this.loading) {
                return;
            }
            this.cvCartService.requireLoginForNotifyMe(this.product.ID, add)
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvInventoryService: services.IInventoryService,
                private readonly cvCartService: services.ICartService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefProductStock", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/stock.html", "ui"),
        controller: ProductStockController,
        controllerAs: "psCtrl",
        bindToController: true
    }));
}
