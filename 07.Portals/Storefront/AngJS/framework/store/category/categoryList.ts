/**
 * @desc Creates a grid/list of categories on a page
 */
module cef.store.category {
    class CategoryListController extends core.TemplatedControllerBase {
        // Properties
        seoUrl: string;
        categoryTypeID: number;
        imageUrlPart: string;
        disreguardParents: boolean;
        categories: Array<api.CategoryModel>;
        currentCategory: api.CategoryModel;
        // Functions
        getImageUrl(category: api.CategoryModel): string {
            if (!category || !category.Images || category.Images.length === 0) { return null; }
            let image = _.find(category.Images, "IsDefault");
            if (!image) { image = category.Images[0]; }
            const imagePath = category.TypeID === 2 ? "manufacturers/" : "products/catalog/";
            return imagePath + image.OriginalFileName;
        }
        getCategoryUrl(category: api.CategoryModel): string {
            return core.Utility.joinPaths(window.location.pathname, category.SeoUrl);
        }
        load(): void {
            const url = window.location.pathname;
            this.seoUrl = url.substr(window.location.pathname.lastIndexOf("/") + 1);
            this.disreguardParents = false;
            this.imageUrlPart = "products/catalog";
            this.categoryTypeID = 1;
            this.categories = [];
            this.cvApi.categories.GetCategories({
                Active: true,
                AsListing: true,
                TypeID: this.categoryTypeID,
                IncludeChildrenInResults: false
            }).then(r => {
                this.categories = this.categoryTypeID === 2
                    ? _.sortBy(r.data.Results, item => _.last(_.words(item.Name)))
                    : this.categories = r.data.Results;
            });
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefCategoryList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/category/categoryList.html", "ui"),
        controller: CategoryListController,
        controllerAs: "categoryListCtrl"
    }));
}
