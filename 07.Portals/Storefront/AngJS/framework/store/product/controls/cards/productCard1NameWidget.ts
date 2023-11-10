/**
 * @file framework/store/product/controls/cards/productCard1NameWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #1: Name widget
 */
module cef.store.product.controls.cards {
    class ProductCardNameGenWidgetController extends ProductCardGenWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        rows: number;
        index: number;
        // Properties
        // <See inherited>
        // Convenience Redirects (Reduce binding text/conditions)
        // <See inherited>
        protected get nameR(): number {
            return Number(this.rows)
                || 2; // Default
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

    cefApp.directive("cefProductCardNameGenWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=?",
            productId: "=?",
            productSeoUrl: "=?",
            productName: "=?",
            rows: "=?",
            index: "="
        },
        // Required to place properly in the BS4 Cards for alignments
        replace: true,
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCard1NameWidget.html", "ui"),
        controller: ProductCardNameGenWidgetController,
        controllerAs: "pcnwCtrl",
        bindToController: true
    }));
}
