module cef.store.product.controls.variants {
    class ProductVariantTablesController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        productVariants: [];
        
        // Properties
        // <None>
        // Functions
        // <None>
        // Events
        // changeVariant(newID: number): void {
        //     this.productIdForTheCurrentVariant = newID;
        //     this.$rootScope.$broadcast(this.cvServiceStrings.events.products.selectedVariantChanged, this.productIdForTheCurrentVariant);
        // }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                private readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefProductVariantTables", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            productVariants: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/variants/variantTables.html", "ui"),
        controller: ProductVariantTablesController,
        controllerAs: "productVariantTablesCtrl",
        bindToController: true
    }));
}
