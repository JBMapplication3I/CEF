module cef.store.checkout {
    cefApp.directive("cefPurchaseCartEmpty", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/purchasing/widgets/emptyCart.html", "ui"),
        require: "^cefPurchase"
    }));
}
