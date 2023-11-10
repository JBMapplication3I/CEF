module cef.store.product.controls.kits {
    cefApp.directive("cefProductKitComponents", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/product/controls/kits/kitComponents.html", "ui")
    }));
}
