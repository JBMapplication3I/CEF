module cef.store.product.controls {
    /*
    isSale(): boolean {
        if (!this.product || !angular.isFunction(this.product.readPrices)) {
            return false;
        }
        return this.product.readPrices().isSale;
    }
    */
    cefApp.directive("cefFloatingAddProduct", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "E",
        templateUrl: $filter("corsLink")("/framework/store/product/controls/floatingAddProduct.html", "ui"),
    }));
}
