module cef.store.product.controls.actions {
    class ProductActionNotifyMeController extends ActionButtonControllerBase {
        // Bound Scope Properties
        // <None at this level>
        // Properties
        protected cartType = this.cvServiceStrings.carts.types.notifyMe;
        protected addFunc = () => this.cvCartService.requireLoginForNotifyMe(this.product.ID, true, null, 1);
        protected removeFunc = () => this.cvCartService.requireLoginForNotifyMe(this.product.ID, false, null, 1);
        protected addKey = "ui.storefront.common.AddToNotifyMe";
        protected removeKey = "ui.storefront.common.RemoveFromNotifyMe";
        protected addIndex = "AddToNotifyMe";
        protected removeIndex = "RemoveFromNotifyMe";
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

    cefApp.directive("cefProductNotifyMe", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=" // The product to Check
        },
        replace: true, // Required for placement
        templateUrl: $filter("corsLink")("/framework/store/product/controls/actions/notifyMe.html", "ui"),
        controller: ProductActionNotifyMeController,
        controllerAs: "productNotifyMeCtrl",
        bindToController: true
    }));
}
