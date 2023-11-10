/**
 * @file framework/store/product/controls/cards/productCard2SkuWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #2: SKU widget
 */
module cef.store.product.controls.cards {
    class ProductCardSkuGenWidgetController extends ProductCardGenWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        hideSku: boolean;
        hideLabels: boolean;
        left: boolean;
        useMnfNumber: boolean;
        // Properties
        // <See inherited>
        // Convenience Redirects (Reduce binding text/conditions)
        // <See inherited>
        protected get hideK(): boolean {
            return Boolean(this.hideSku)
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

    cefApp.directive("cefProductCardSkuGenWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=?",
            productId: "=?",
            productSeoUrl: "=?",
            hideSku: "=?",
            hideLabels: "=?",
            left: "=?",
            useMnfNumber: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCard2SkuWidget.html", "ui"),
        controller: ProductCardSkuGenWidgetController,
        controllerAs: "pckwCtrl",
        bindToController: true
    }));
}
