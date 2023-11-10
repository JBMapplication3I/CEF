module cef.store.product.controls.associations {
    // TODO: Give this directive it's own controller
    cefApp.directive("cefProductBestSellers", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/product/controls/associations/bestSellers.html", "ui"),
    }));
}
