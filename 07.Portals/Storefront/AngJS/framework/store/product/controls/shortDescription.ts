module cef.store.product.controls {
    class ProductShortDescriptionController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        product: api.ProductModel;
        /**
         * @type {number}
         * @default 255
         * @memberof ProductShortDescriptionController
         */
        limit: number;
        // Properties
        // <None>
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefProductShortDescription", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=",
            limit: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/shortDescription.html", "ui"),
        controller: ProductShortDescriptionController,
        controllerAs: "psdCtrl",
        bindToController: true
    }));
}
