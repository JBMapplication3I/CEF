module cef.store.product.controls.actions {
    class ProductActionWishListController extends ActionButtonControllerBase {
        // Bound Scope Properties
        // <None at this level>
        // Properties
        protected cartType = this.cvServiceStrings.carts.types.wishList;
        protected addFunc = () => this.cvCartService.requireLoginForWishList(this.product.ID, true);
        protected removeFunc = () => this.cvCartService.requireLoginForWishList(this.product.ID, false);
        protected addKey = "ui.storefront.common.WishList.AddTo";
        protected removeKey = "ui.storefront.common.WishList.RemoveFrom";
        protected addIndex = "AddToWishList";
        protected removeIndex = "RemoveFromWishList";
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

    cefApp.directive("cefProductWishList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=" // The product to Check
        },
        replace: true, // Required for placement
        templateUrl: $filter("corsLink")("/framework/store/product/controls/actions/wishList.html", "ui"),
        controller: ProductActionWishListController,
        controllerAs: "pwlCtrl",
        bindToController: true
    }));
}
