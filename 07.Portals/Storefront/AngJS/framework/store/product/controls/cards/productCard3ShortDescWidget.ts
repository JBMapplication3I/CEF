/**
 * @file framework/store/product/controls/cards/productCard2ShortDescWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #3: Short Description widget
 */
module cef.store.product.controls.cards {
    class ProductCardShortDescGenWidgetController extends ProductCardGenWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        hideShortDesc: boolean;
        rows: number;
        index: string;
        // Properties
        // <See inherited>
        // Convenience Redirects (Reduce binding text/conditions)
        // <See inherited>
        protected get shortDescR(): number {
            return Number(this.rows)
                || 2; // Default
        }
        protected get hideD(): boolean {
            return Boolean(this.hideShortDesc)
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

    cefApp.directive("cefProductCardShortDescGenWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            product: "=?",
            productId: "=?",
            productSeoUrl: "=?",
            productName: "=?",
            hideShortDesc: "=?",
            rows: "=?",
            index: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCard3ShortDescWidget.html", "ui"),
        controller: ProductCardShortDescGenWidgetController,
        controllerAs: "pcsdwCtrl",
        bindToController: true
    }));
}
