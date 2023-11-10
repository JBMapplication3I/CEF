module cef.store.cart.controls {
    class CartHeaderBarController extends core.TemplatedControllerBase {
        // Properties
        type: string; // Bound from scope
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvCartService: services.ICartService) { // Used by UI
            super(cefConfig);
        }
    }

    cefApp.directive("cefCartHeaderBar", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { type: "=" },
        templateUrl: $filter("corsLink")("/framework/store/cart/controls/cartHeaderBar.html", "ui"),
        controller: CartHeaderBarController,
        controllerAs: "cartHeaderBarCtrl",
        bindToController: true
    }));
}
