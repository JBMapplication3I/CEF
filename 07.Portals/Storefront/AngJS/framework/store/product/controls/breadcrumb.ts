module cef.store.product.controls {
    class ProductDetailsBreadcrumbController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        divider: string;
        product: api.ProductModel;
        prefix: string;
        suffix: string;
        firstItem: string;
        lastItem: string;
        // Properties
        categoryList: api.CategoryModel[] = [];
        // Functions
        buildCategoryList(): void {
            /* Interogate Product to find valid category
             * in this case, one with a parentID (simple logic that could be extended for
             * more robust scenarios)
             * TODO: this logic assumes two levels of category based on a specific use case
             * but could be extended to handle more levels as needed
             */
            let found = false;
            _.sortBy(this.product.ProductCategories
                    .filter(x => x.Slave && x.Slave.ParentID && x.Slave.Name !== "Featured Product"),
                            x => x.Slave && !x.Slave.ParentID)
                .forEach(x => {
                    if (found) { return; }
                    /* For now, checking to see if a parentCategory exists and taking that
                     * category as the lowest level child category */
                    this.categoryList.push(x.Slave);
                    this.getCategoryParentIfExists(x.Slave);
                    found = true;
                });
            this.categoryList = _.uniqBy(this.categoryList, x => x.ID);
        }
        getCategoryParentIfExists(category: api.CategoryModel): void {
            // Follow category tree up the chain until we get to a category without a parentID
            if (!category || !category.ParentID || category.Name === "Featured Product") {
                return;
            }
            this.cvCategoryService.get({ id: category.ParentID }).then(cat => {
                if (!cat) {
                    console.error("Unable to get parent category by ID");
                    return;
                }
                // Push the parent categories to the beginning of the array
                this.categoryList.unshift(cat);
                this.categoryList = _.uniqBy(this.categoryList, x => x.ID);
                // Recursive call to follow chain up to last parent category
                this.getCategoryParentIfExists(cat);
            }).catch(reason => console.error(reason));
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig, // Used by UI
                private readonly cvCategoryService: services.ICategoryService) {
            super(cefConfig);
            this.buildCategoryList();
        }
    }

    cefApp.directive("cefProductDetailsBreadcrumb", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=",
            divider: "@",
            prefix: "@",
            suffix: "@",
            firstItem: "@",
            lastItem: "@"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/breadcrumb.html", "ui"),
        controller: ProductDetailsBreadcrumbController,
        controllerAs: "pdbCtrl",
        bindToController: true
    }));
}
