module cef.store.navigation {
    class CatalogCategoryLinkController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        categoryId: number;
        category: api.MenuCategoryModel;
        hideName: boolean;
        showSeeAll: boolean;
        showAngles: boolean;
        showArrow: boolean;
        bold: boolean;
        level: number;
        index: string;
        // Properties
        // get category(): api.CategoryModel {
        //     return this
        //         && this.categoryId
        //         && this.cvCategoryService
        //         && this.cvCategoryService.getCached({ id: this.categoryId });
        // }
        get isReady(): boolean {
            return Boolean(this.category);
        }
        get stateName(): string | null {
            if (!this.isReady) {
                return null;
            }
            return this.cefConfig.catalog.showCategoriesForLevelsUpTo === 3
                && this.cefConfig.catalog.showCategoriesForLevelsUpTo > (this.level || 0)
                ? "searchCatalog.products.categories.level3"
                : this.cefConfig.catalog.showCategoriesForLevelsUpTo === 2
                    && this.cefConfig.catalog.showCategoriesForLevelsUpTo > (this.level || 0)
                    ? "searchCatalog.products.categories.level2"
                    : /*this.cefConfig.catalog.showCategoriesForLevelsUpTo <= (this.level || 0)
                        ?*/ "searchCatalog.products.results.both"
                        /*: null*/;
        }
        get paramsObjAsStr(): any {
            if (!this.isReady) {
                return null;
            }
            return `{ 'category': '${this.category.Name}|${this.category.CustomKey}', 'term': null, 'categoriesAny': null, 'categoriesAll': null, 'attributesAny': null, 'attributesAll': null, 'pricingRanges': null }`;
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvCategoryService: services.ICategoryService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefCatalogCategoryLink", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            category: "=?",
            categoryId: "=?",
            hideName: "@?",
            showSeeAll: "=?",
            showAngles: "=?",
            showArrow: "@?",
            bold: "@?",
            classes: "@?",
            level: "=?",
            index: "@"
        },
        replace: true, // Required so the content is placed in the correct location
        templateUrl: $filter("corsLink")("/framework/store/navigation/catalogCategoryLink.html", "ui"),
        controller: CatalogCategoryLinkController,
        controllerAs: "cclCtrl",
        bindToController: true,
        require: "CategoriesMenuInnerController"
    }));
}
