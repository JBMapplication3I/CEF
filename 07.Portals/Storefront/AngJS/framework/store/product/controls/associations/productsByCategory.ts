module cef.store.product.controls.associations {
    class ProductsByCategoryController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        product: api.ProductModel;
        products: api.ProductModel[];
        size: number;
        type: string;
        includeInvisible: boolean;
        template: string;
        // Properties
        skip = 0;
        paging: core.Paging<api.ProductModel>;
        thumbWidth = 200;
        thumbHeight = 200;
        get actionButtonView(): string { return "addToCart"; }
        get pricingDisplayStyle(): string { return "sideBySide"; }
        // functions
        load(): void {
            // Client will have a small number of products (<20).
            // If the above changes, this may need to be revised.
            var categoryID = this.product.ProductCategories[0].SlaveID;
            this.cvApi.products.GetProductsByCategory({ProductTypeIDs: [categoryID]}).then(r => {
                if (!r || !r.data || !r.data.ProductsByCategory.length) {
                    return;
                }
                //this.categoryMembers = r.data.ProductsByCategory[0].Products;
                this.products = r.data.ProductsByCategory[0].Products;
                this.paging.data = this.products
                        .filter(x => x.TypeName === this.type);
                // r.data.ProductsByCategory[0].Products.forEach(x => {
                //     this.products.push(x);
                // });
                return;
            })
            // this.categoryMembers.forEach(x => {
            //     if (!x) {
            //         return;
            //     }
            //     if (x.CategoryID == categoryID) {
            //         this.products = x.Products;
            //         return;
            //     }
            // });
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
            this.paging = new core.Paging<api.ProductModel>($filter);
            this.paging.pageSize = this.size;
            this.paging.pageSetSize = 100;
            const unbind1 = $scope.$watchCollection(
                () => this.products,
                () => this.products
                    && (this.paging.data = this.products
                        .filter(x => x.TypeName === this.type)));
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
            this.load();
        }
    }

    cefApp.directive("productsByCategory", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=",
            size: "=?",
            type: "@?",
            template: "@?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/associations/productsByCategory.html", "ui"),
        controller: ProductsByCategoryController,
        controllerAs: "pbcCtrl",
        bindToController: true
    }));
}
