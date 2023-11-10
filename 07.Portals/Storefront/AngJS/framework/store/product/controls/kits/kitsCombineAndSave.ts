module cef.store.product.controls.kits {
    cefApp.directive("cefProductKitsCombineAndSave", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/product/controls/kits/kitsCombineAndSave.html", "ui")
    }));
}
