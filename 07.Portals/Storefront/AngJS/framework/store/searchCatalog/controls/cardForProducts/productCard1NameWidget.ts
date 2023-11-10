/**
 * @file framework/store/searchCatalog/controls/cardForProducts/productCard1NameWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #1: Name widget
 */
module cef.store.searchCatalog.controls.results {
    class ProductCardNameWidgetController extends ProductCardWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        rows: number;
        index: number;
        minPrice: number;
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
        minMax(): void {
            this.cvApi.pricing.CalculateMinMaxVariantPrices({
                ProductID: this.productId
            }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    return;
                }
                this.minPrice = r.data.Result.MinPrice.BasePrice;
            }).catch((error) => console.error(error));
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
        }
    }

    cefApp.directive("cefProductCardNameWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            productId: "=?",
            rows: "=?",
            index: "=",
            minPrice: "=?"
        },
        // Required to place properly in the BS4 Cards for alignments
        replace: true,
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/cardForProducts/productCard1NameWidget.html", "ui"),
        controller: ProductCardNameWidgetController,
        controllerAs: "pcnwCtrl",
        bindToController: true
    }));
}
