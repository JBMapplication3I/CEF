module cef.store.category {
    class CategoryCatalog extends core.TemplatedControllerBase {
        // Properties
        seoUrl: string;
        categoryTypeID: number;
        imageUrlPart: string;
        disreguardParents: boolean;
        categories: Array<api.CategoryModel>;
        currentCategory: api.CategoryModel;
        // Functions
        getImageUrl(category: api.CategoryModel): string {
            if (!category || !category.Images || category.Images.length === 0) { return ""; }
            let image = _.find(category.Images, "IsDefault");
            if (!image) { image = category.Images[0]; }
            const imagePath = category.TypeID === 2 ? "manufacturers/" : "products/catalog/";
            return imagePath + image.OriginalFileName;
        }
        getCategoryUrl(category: api.CategoryModel): string {
            if (window.location.pathname.indexOf("artist") >= 0) {
                return core.Utility.joinPaths(window.location.pathname, "/artist/", category.SeoUrl);
            }
            if (window.location.pathname.indexOf("turquoise") >= 0) {
                return core.Utility.joinPaths(window.location.pathname, "/turquoise/", category.SeoUrl);
            }
            return core.Utility.joinPaths(window.location.pathname, category.SeoUrl);
        }
        load(): void {
            const url = window.location.pathname;
            this.consoleLog(url);
            this.seoUrl = url.substr(window.location.pathname.lastIndexOf("/") + 1);
            this.disreguardParents = false;
            this.imageUrlPart = "products/catalog";
            this.categoryTypeID = 3;
            this.categories = [];
            if (this.seoUrl === "artist-profiles") {
                this.categoryTypeID = 2;
                this.imageUrlPart = "manufacturers";
            }
            this.cvApi.categories.GetCategories({
                Active: true,
                AsListing: true,
                TypeID: this.categoryTypeID,
                IncludeChildrenInResults: false
            }).then(r => {
                this.categories = this.categoryTypeID === 2
                    ? _.sortBy(r.data.Results, item => _.last(_.words(item.Name)))
                    : r.data.Results;
            });
        }
        // Construcotr
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefCategoryCatalog", () => ({
        restrict: "EA",
        replace: true,
        transclude: true, // Required
        // Note: This is a control-specific tempalte that must remain inline
        template: "<ng-transclude></ng-transclude>",
        controller: CategoryCatalog,
        controllerAs: "catalog"
    }));
}