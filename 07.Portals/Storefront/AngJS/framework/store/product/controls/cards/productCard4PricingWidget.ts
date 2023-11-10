/**
 * @file framework/store/product/controls/cards/productCard4PricingWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #4: Pricing widget
 */
module cef.store.product.controls.cards {
    class ProductCardPricingGenWidgetController extends ProductCardGenWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        displayStyle: string;
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
            if (this.product.ProductAssociations && this.product.ProductAssociations.length)
            {
                return this.product.ProductAssociations
                    .sort(x => x.Slave?.readPrices().sale
                        ? x.Slave?.readPrices().sale
                        : x.Slave?.readPrices().base
                    )[0].Slave?.readPrices();
            }
            return this.product.readPrices();
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
                protected readonly cvSearchCatalogService: services.SearchCatalogService,
                protected readonly cvPricingService: services.IPricingService) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
            this.load();
        }
    }

    cefApp.directive("cefProductCardPricingGenWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            productId: "=?",
            productSeoUrl: "=?",
            productName: "=?",
            displayStyle: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCard4PricingWidget.html", "ui"),
        controller: ProductCardPricingGenWidgetController,
        controllerAs: "scpcpCtrl",
        bindToController: true
    }));
}
