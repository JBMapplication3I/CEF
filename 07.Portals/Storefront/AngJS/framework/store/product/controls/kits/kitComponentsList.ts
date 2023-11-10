module cef.store.product.controls.kits {
    class KitComponentsListController extends core.TemplatedControllerBase {
        // Properties
        products: api.ProductModel[]; // Bound by Scope
    }

    cefApp.directive("kitComponentsList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { products: "=" },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/kits/kitComponentsList.html", "ui"),
        controller: KitComponentsListController,
        controllerAs: "kitComponentsListCtrl",
        bindToController: true
    }));
}
