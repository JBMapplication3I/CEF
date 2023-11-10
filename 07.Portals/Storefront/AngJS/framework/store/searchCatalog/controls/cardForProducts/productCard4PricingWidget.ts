/**
 * @file framework/store/searchCatalog/controls/cardForProducts/productCard4PricingWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #4: Pricing widget
 */
module cef.store.searchCatalog.controls.results {
    class ProductCardPricingWidgetController extends ProductCardWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        displayStyle: string;
        minPrice: number;
        // Properties
        // <See inherited>
        // Convenience Redirects (Reduce binding text/conditions)
        // <See inherited>
        protected get pds(): string {
            return this.displayStyle
                || "sideBySide"; // Default
        }
        get showLoginRequired(): boolean {
            return this.cefConfig.loginForPricing.enabled
                && !this.cvAuthenticationService.isAuthenticated();
        }
        get loading(): boolean {
            if (!this.product) {
                return true;
            }
            if (!angular.isFunction(this.product.readPrices)) {
                // /*this.product =*/ this.cvPricingService.factoryAssign(this.product);
                return true;
            }
            return this.product.readPrices().loading;
        }
        get prices(): api.CalculatedPrices {
            if (this.loading) {
                return null;
            }
            return this.product.readPrices();
        }
        // Functions
        // <See inherited>
        minMax(): void {
            if (!this.product?.ID) return;
            this.cvApi.pricing.CalculateMinMaxVariantPrices({
                ProductID: this.product.ID
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
                protected readonly cvPricingService: services.IPricingService,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
        }
    }

    cefApp.directive("cefProductCardPricingWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            productId: "=?",
            displayStyle: "=?",
            minPrice: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/cardForProducts/productCard4PricingWidget.html", "ui"),
        controller: ProductCardPricingWidgetController,
        controllerAs: "scpcpCtrl",
        bindToController: true
    }));
}
