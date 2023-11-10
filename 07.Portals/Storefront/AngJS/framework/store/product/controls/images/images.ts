module cef.store.product.controls.images {
    cefApp.directive("cefProductImages", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/product/controls/images/images.html", "ui")
    }));
}
