module cef.store.searchCatalog.controls {
    class CategoryFilterNameController extends core.TemplatedControllerBase {
        // Bound Scope properties
        category: api.AggregateTree;
        index: string;
        level: number;
        mode: string;
        // Convenience Points (reduces HTML size)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        get selectedCategory(): string {
            return this
                && this.cvSearchCatalogService.activeSearchViewModel.Form.Category;
        }
        get active(): boolean {
            return this
                && this.category
                && this.category.Key === this.selectedCategory;
        }
        get hasDN(): boolean {
            return this
                && this.fullCat
                && angular.isDefined(this.fullCat.DisplayName)
                && this.fullCat.DisplayName !== null
                && this.fullCat.DisplayName !== "";
        }
        get name(): string {
            if (!this || !this.category || !this.fullCat) {
                return null;
            }
            return angular.isDefined(this.fullCat.DisplayName)
                && this.fullCat.DisplayName !== null
                && this.fullCat.DisplayName !== ""
                ? this.fullCat.DisplayName
                : this.fullCat.Name.toLowerCase();
        }
        private _fullCat: api.CategoryModel;
        get fullCat(): api.CategoryModel {
            if (!this || !this.cvSearchCatalogService.allCategories) {
                return null;
            }
            if (this._fullCat) {
                return this._fullCat;
            }
            this._fullCat = _.find(this.cvSearchCatalogService.allCategories,
                x => (x.Name + "|" + x.CustomKey) == this.category.Key);
            return this._fullCat;
        }
        // Functions
        push(): void {
            this.cvSearchCatalogService.pushCategory(
                this.mode,
                this.category["Key"]);
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefCatFilterName", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            category: "=",
            index: "=",
            level: "=",
            mode: "@"
        },
        // replace: true,
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/categoryFilterName.html", "ui"),
        controller: CategoryFilterNameController,
        controllerAs: "cfnCtrl",
        bindToController: true,
        require: ["^cefSearchCatalogFilterByCategoriesTree"]
    }));
}
