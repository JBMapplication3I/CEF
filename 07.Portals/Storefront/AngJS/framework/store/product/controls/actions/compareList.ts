module cef.store.product.controls.actions {
    class ProductActionCompareListController extends ActionButtonControllerBase {
        // Bound Scope Properties
        // <None at this level>
        // Properties
        protected cartType = this.cvServiceStrings.carts.types.compare;
        protected addFunc = () => this.cvCartService.addCartItem(this.product.ID, this.cartType);
        protected removeFunc = () => this.cvCartService.removeCartItemByType(this.product.ID, this.cartType);
        protected addKey = "ui.storefront.common.Compare.AddTo";
        protected removeKey = "ui.storefront.common.Compare.RemoveFrom";
        protected addIndex = "AddToCompareList";
        protected removeIndex = "RemoveFromCompareList";
        // Functions
        // <None at this level>
        // Events
        // <None at this level>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig, // Used by UI
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCartService: services.ICartService) {
            super(cefConfig, cvServiceStrings, cvCartService);
        }
    }

    cefApp.directive("cefProductCompareList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=" // The product to Check
        },
        replace: true, // Required for placement
        templateUrl: $filter("corsLink")("/framework/store/product/controls/actions/compareList.html", "ui"),
        controller: ProductActionCompareListController,
        controllerAs: "pclCtrl",
        bindToController: true
    }));
}
