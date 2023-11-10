/**
 * @file framework/store/searchCatalog/controls/cardForProducts/productCard2StockWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #2: Stock widget
 */
module cef.store.searchCatalog.controls.results {
    class ProductCardStockWidgetController extends ProductCardWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        hideStock: boolean;
        hideLabels: boolean;
        left: boolean;
        countStoreStockOnly: boolean;
        uomStockQty: number;
        // Properties
        // <See inherited>
        // Convenience Redirects (Reduce binding text/conditions)
        // <See inherited>
        protected get hideS(): boolean {
            return !this.cefConfig.featureSet.inventory.enabled
                || Boolean(this.hideStock)
                || this.cefConfig.loginForInventory.enabled
                && !this.cvAuthenticationService.isAuthenticated()
                || false; // Default
        }
        // Functions
        // <See inherited>
        readIfUOMInventoryIsOutOfStock(): boolean {
            let currentUOMSelectionObj = this.cvProductService.productUOMSelectionObject[this.product.ID];
            let currentUOMSelection = currentUOMSelectionObj != null && currentUOMSelectionObj.length ? currentUOMSelectionObj[1] : null;
            let rawUOMInventories = this.product["$_rawInventoryUOMs"];
            if (currentUOMSelection && rawUOMInventories != null) {
                let findResult: api.CalculatedInventory = rawUOMInventories.find(x => x.ProductUOM === currentUOMSelection);
                if (Math.floor(findResult.QuantityOnHand) <= 0){
                    return true;
                } else {
                    this.uomStockQty = findResult.QuantityOnHand;
                }
            }
            return false;
        }
        productIsRX(): boolean {
            if (!this.product
                || !this.product.SerializableAttributes) {
                return false;
            }
            if (this.product.SerializableAttributes["PrescriptionDevice"]?.Value 
                || this.product.SerializableAttributes["PrescriptionDrug"]?.Value) {
                return true;
            }
            return false;
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
        }
    }

    cefApp.directive("cefProductCardStockWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            productId: "=?",
            hideStock: "=?",
            hideLabels: "=?",
            countStoreStockOnly: "=?",
            left: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/cardForProducts/productCard2StockWidget.html", "ui"),
        controller: ProductCardStockWidgetController,
        controllerAs: "pcswCtrl",
        bindToController: true
    }));
}
