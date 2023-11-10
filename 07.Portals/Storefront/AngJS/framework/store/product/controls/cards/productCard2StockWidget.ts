/**
 * @file framework/store/product/controls/cards/productCard2StockWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #2: Stock widget
 */
module cef.store.product.controls.cards {
    class ProductCardStockGenWidgetController extends ProductCardGenWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        hideStock: boolean;
        hideLabels: boolean;
        left: boolean;
        countStoreStockOnly: boolean;
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
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
            this.load();
        }
    }

    cefApp.directive("cefProductCardStockGenWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            product: "=?",
            productId: "=?",
            productSeoUrl: "=?",
            hideStock: "=?",
            hideLabels: "=?",
            countStoreStockOnly: "=?",
            left: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCard2StockWidget.html", "ui"),
        controller: ProductCardStockGenWidgetController,
        controllerAs: "pcswCtrl",
        bindToController: true
    }));
}
