module cef.store.category {
    class CategoryDetail {
        seoUrl: string;
        catType: string;
        categoryTypeID: number;
        disreguardParents: boolean;
        categories: Array<api.CategoryModel>;
        category: api.CategoryModel;

        getImageUrl(category: api.CategoryModel): string {
            if (!category || !category.Images || category.Images.length === 0) { return ""; }
            let image = _.find(category.Images, "IsDefault");
            if (!image) {
                image = category.Images[0];
            }
            const imagePath = category.TypeID === 2 ? "manufacturers/" : "products/catalog/";
            return imagePath + image.OriginalFileName;
        };

        load(): void {
            const url = window.location.pathname;
            this.seoUrl = "";
            this.disreguardParents = false;
            this.categories = [];
            this.seoUrl = url.substring(url.lastIndexOf("/") + 1);
            this.cvApi.categories.GetCategories({
                Active: true,
                AsListing: true,
                SeoUrl: this.seoUrl,
                IncludeChildrenInResults: false
            }).then(r => {
                this.category = r.data.Results[0];
                this.category.Description = this.$sce.trustAsHtml(this.category.Description);
            });
        };

        constructor(
                private readonly cvApi: api.ICEFAPI,
                private readonly $sce: ng.ISCEService) {
            this.load();
            /* this.SubBulletIndustry = {};
            this.getIndustryBullets;*/
        }
    }

    cefApp.directive("cefCategoryDetail", (): ng.IDirective => ({
        restrict: "EA",
        replace: true,
        transclude: true, // Required
        // Note: This is a control-specific template that must remain inline
        template: "<ng-transclude></ng-transclude>",
        controller: CategoryDetail,
        controllerAs: "categoryDetailCtrl"
    }));
}
