module cef.store.searchCatalog.controls {
    class SearchRelatedProductsController extends core.TemplatedControllerBase {
        // Properties
        products: Array<api.ProductModel> = [];
        // Functions
        load(): void {
            const searchProducts = this.cvSearchCatalogService.searchViewModelPr.ResultIDs;
            // Filter the attributes
            if (!searchProducts || !searchProducts.length) {
                return;
            }
            this.cvProductService.bulkGet(searchProducts).then(products => {
                products.forEach(x => {
                    if (!x || !x.ID) {
                        return;
                    }
                    if (!x.ProductAssociations
                        || !x.ProductAssociations.filter(y => y.TypeName === "Related Product").length) {
                        return;
                    }
                    x.ProductAssociations
                        .filter(y => y.TypeName === "Related Product")
                        .forEach(y => this.products.push(y.Slave));
                });
            });
        }
        // Constructors
        constructor(
                readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvProductService: services.IProductService,
                private readonly cvSearchCatalogService: services.SearchCatalogService,
                private readonly cvCartService: services.ICartService) { // Used by UI
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.catalog.searchComplete,
                () => this.load());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefSearchRelatedProducts", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/searchRelatedProducts.html", "ui"),
        controller: SearchRelatedProductsController,
        controllerAs: "searchRelatedProductsCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
