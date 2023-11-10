/**
 * @file framework/store/product/controls/cards/productCard6ActionButtonWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #6: Action Buttons widget
 */
module cef.store.product.controls.cards {
    class ProductCardActionButtonGenWidgetController extends ProductCardGenWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        buttonView: string;
        quickAdd: boolean;
        small: number;
        asIcon: number;
        hideQuantitySelector: boolean;
        // Properties
        // <See inherited>
        quantity: number;
        // Convenience Redirects (Reduce binding text/conditions)
        protected get abv(): string {
            return this.buttonView
                || "addToCart"; // Default
        }
        protected get quickA(): boolean {
            return Boolean(this.quickAdd);
                //|| true; // Default
        }
        protected get menuEnabled(): boolean {
            return this.cefConfig.featureSet.salesQuotes.useQuoteCart;
        }

        protected get showViewDetailsButton(): boolean {
            return this.product.readInventory().IsOutOfStock 
                || (this.product.readInventory().AllowBackOrder 
                && this.product.TypeKey === 'VARIANT-MASTER')
        }
        // Functions
        // <See inherited>
        addCartItem(quantity = this.quantity): void {
            this.setRunning();
            this.cvCartService.addCartItem(
                    this.product.ID,
                    this.cvServiceStrings.carts.types.cart,
                    quantity || 1,
                    { },
                    this.product)
                .finally(() => this.finishRunning());
        }
        addQuoteCartItem(quantity = this.quantity): void {
            this.setRunning();
            this.cvCartService.addCartItem(
                    this.product.ID,
                    this.cvServiceStrings.carts.types.quote,
                    quantity || 1,
                    { },
                    this.product)
                .finally(() => this.finishRunning());
        }
        /** This is used in a special variants setup */
        addCartItems(quantity = this.quantity): void {
            this.setRunning();
            this.cvCartService.addCartItem(
                    this.product.ID,
                    this.cvServiceStrings.carts.types.cart,
                    quantity || 1,
                    { },
                    this.product)
                .finally(() => this.finishRunning());
        }
        addQuoteCartItems(quantity = this.quantity): void {
            this.setRunning();
            this.cvCartService.addCartItem(
                    this.product.ID,
                    this.cvServiceStrings.carts.types.quote,
                    quantity || 1,
                    { },
                    this.product)
                .finally(() => this.finishRunning());
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService,
                private readonly cvCartService: services.ICartService) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
            this.load();
        }
    }

    cefApp.directive("cefProductCardActionButtonGenWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=?",
            productId: "=?",
            productSeoUrl: "=?",
            productName: "=?",
            buttonView: "=",
            quickAdd: "=?",
            small: "=?",
            asIcon: "=?",
            index: "=?",
            hideQuantitySelector: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCard6ActionButtonWidget.html", "ui"),
        controller: ProductCardActionButtonGenWidgetController,
        controllerAs: "pcabwCtrl",
        bindToController: true
    }));
}
