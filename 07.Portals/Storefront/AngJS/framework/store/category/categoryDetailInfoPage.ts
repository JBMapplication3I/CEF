// Create an informational page for a product category
module cef.store.category {
    class CategoryDetailInfoPageController extends core.TemplatedControllerBase {
        seoUrl: string;
        catType: string;
        categoryTypeID: number;
        disreguardParents: boolean;
        categories: Array<api.CategoryModel>;
        category: api.CategoryModel;
        products: Array<api.ProductModel>;
        bullets: any;
        bullets2: any;
        bullets3: any;
        bullets4: any;
        bullets5: any;
        bullets6: any;
        bullets7: any;
        bullets8: any;
        bullets9: any;
        bullets10: any;

        getImageUrl(category: api.CategoryModel): string {
            if (!category || !category.Images || category.Images.length === 0) { return ""; }
            let image = _.find(category.Images, "IsDefault");
            if (!image) {
                image = category.Images[0];
            }
            return image.OriginalFileName;
        }

        getCategoryUrl(category: api.CategoryModel): string {
            return core.Utility.joinPaths(window.location.pathname, category.SeoUrl);
        }

        getBullets(obj: {}, params: string) {
            var paramsArray = [];
            if (!obj) {
                return paramsArray;
            }
            Object.keys(obj).forEach(key => {
                if (key.indexOf(params) === -1 || !obj[key].Value) { return; }
                const content = obj[key].Value.split(";");
                content.forEach(value => {
                    var splitValue = value.split(",");
                    paramsArray.push({ Text: splitValue[0], URL: splitValue[1] });
                });
            });
            return paramsArray;
        }

        load(): void {
            const url = window.location.pathname;
            this.disreguardParents = false;
            this.categories = [];
            this.seoUrl = url.substring(url.lastIndexOf("/") + 1);
            this.cvApi.categories.GetCategories({
                Active: true,
                AsListing: true,
                SeoUrl: this.seoUrl,
                IncludeChildrenInResults: false
            }).then(r => {
                const id = r.data.Results[0].ID;
                this.cvApi.categories.GetCategoryByID({ ID: id, ExcludeProductCategories: true }).then(response2 => {
                    this.category = response2.data;
                    this.bullets = this.getBullets(this.category.SerializableAttributes, "IND_Sub_Bullet_A_1");
                    this.bullets2 = this.getBullets(this.category.SerializableAttributes, "IND_Sub_Bullet_A_2");
                    this.bullets3 = this.getBullets(this.category.SerializableAttributes, "IND_Sub_Bullet_A_3");
                    this.bullets4 = this.getBullets(this.category.SerializableAttributes, "IND_Sub_Bullet_A_4");
                    this.bullets5 = this.getBullets(this.category.SerializableAttributes, "IND_Sub_Bullet_A_5");
                    this.bullets6 = this.getBullets(this.category.SerializableAttributes, "IND_Sub_Bullet_A_6");
                    this.bullets7 = this.getBullets(this.category.SerializableAttributes, "IND_Sub_Bullet_A_7");
                    this.bullets8 = this.getBullets(this.category.SerializableAttributes, "IND_Sub_Bullet_A_8");
                    this.bullets9 = this.getBullets(this.category.SerializableAttributes, "IND_Sub_Bullet_A_9");
                    this.bullets10 = this.getBullets(this.category.SerializableAttributes, "IND_Sub_Bullet_A_10");
                }).then(() => this.cvApi.products.GetProducts({ Active: true, AsListing: true, CategoryID: this.category.ID })
                    .then(r3 => this.products = r3.data.Results));
            });
        };

        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.load();
            this.bullets = [];
            this.bullets2 = [];
            this.bullets3 = [];
            this.bullets4 = [];
            this.bullets5 = [];
            this.bullets6 = [];
            this.bullets7 = [];
            this.bullets8 = [];
            this.bullets9 = [];
            this.bullets10 = [];
        }
    }

    cefApp.directive("cefCategoryDetailInfoPage", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/category/categoryDetailInfoPage.html", "ui"),
        controller: CategoryDetailInfoPageController,
        controllerAs: "categoryDetailInfoPage",
    }));

    cefApp.directive("cefCategoryDetailInfoPage2", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/category/categoryDetailInfoPage2.html", "ui"),
        controller: CategoryDetailInfoPageController,
        controllerAs: "categoryDetailInfoPage2",
    }));
}
