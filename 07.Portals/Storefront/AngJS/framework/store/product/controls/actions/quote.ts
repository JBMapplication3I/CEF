/**
 * @file framework/store/product/controls/actions/quote.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Product request for quote button class (only works with a product).
 */
module cef.store.product.controls.actions {
    class ProductRequestForQuoteButtonController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        product: api.ProductModel;
        storeProduct: api.StoreProductModel;
        // Properties
        protected index = "GetAQuickQuote";
        protected key = "ui.storefront.common.GetAQuickQuote";
        // Functions
        click(): void {
            this.cvLoginModalFactory(() => null, null, true, false).then(() => {
                this.$uibModal.open({
                    templateUrl: this.$filter("corsLink")("/framework/store/quotes/modals/productRequestForQuoteModal.html", "ui"),
                    scope: this.$scope,
                    size: this.cvServiceStrings.modalSizes.lg,
                    controller: quotes.modals.ProductRequestForQuoteModalController,
                    controllerAs: "prfqmCtrl",
                    resolve: {
                        product: () => this.product,
                        storeProduct: () => this.storeProduct
                    }
                });
            });
        }
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $filter: ng.IFilterService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvLoginModalFactory: user.ILoginModalFactory) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefProductRequestForQuoteButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=", // The product to use for the modal
            storeProduct: "=" // The store product to use for the modal
        },
        replace: true, // Required for placement
        templateUrl: $filter("corsLink")("/framework/store/product/controls/actions/quote.html", "ui"),
        controller: ProductRequestForQuoteButtonController,
        controllerAs: "pqqCtrl",
        bindToController: true
    }));
}
