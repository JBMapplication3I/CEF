module cef.store.product.controls.variants {
    class ProductVariantSwatchesController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        productIdForTheCurrentVariant: number;
        productVariants: [];
        // Properties
        // <None>
        // Functions
        // <None>
        // Events
        changeVariant(newID: number): void {
            this.productIdForTheCurrentVariant = newID;
            this.$rootScope.$broadcast(this.cvServiceStrings.events.products.selectedVariantChanged, this.productIdForTheCurrentVariant);
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                private readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefProductVariantSwatches", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            productIdForTheCurrentVariant: "=",
            productVariants: "=",
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/variants/variantSwatches.html", "ui"),
        controller: ProductVariantSwatchesController,
        controllerAs: "productVariantSwatchesCtrl",
        bindToController: true
    }));
}
