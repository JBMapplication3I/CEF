module cef.store.product.controls {
    class PricePointTableController extends core.TemplatedControllerBase {
        // Constructor
        constructor(protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefPricePointTable", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/product/controls/pricePointTable.html", "ui"),
        controller: PricePointTableController,
        controllerAs: "pricePointTableCtrl"
    }));
}
