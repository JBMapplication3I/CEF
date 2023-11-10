module cef.store.category {
    class CategoriesOnHomeController extends core.TemplatedControllerBase {
        // Properties
        categoryName: string; // Bound by Scope
        products: Array<api.ProductModel>;
        categories: Array<api.CategoryModel>;
        // Note: Currently the TypeID filter doesn't seem to be working on GetCategories(). Tried both number and string. TODO: Fix this
        constructor(
                protected readonly cefConfig: core.CefConfig, // Used by UI
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.load();
        }

        load(): void {
            this.cvApi.categories.GetCategories({ Active: true, AsListing: true, IncludeChildrenInResults: false })
                .then(r => this.categories = r.data.Results);
            if (!this.categoryName) {
                return;
            }
            this.cvApi.products.GetProducts({ Active: true, AsListing: true, CategoryName: this.categoryName })
                .then(r => this.products = r.data.Results);
        };
    }

    cefApp.directive("cefCategoriesOnHome", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/category/categoriesOnHome.html", "ui"),
        controller: CategoriesOnHomeController,
        controllerAs: "categoriesOnHomeCtrl"
    }));

    cefApp.directive("cefCategoryProducts", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/category/categoryProducts.html", "ui"),
        controller: CategoriesOnHomeController,
        scope: {
            categoryName: "@?"
        },
        controllerAs: "categoryProductsCtrl",
        bindToController: true
    }));
}
