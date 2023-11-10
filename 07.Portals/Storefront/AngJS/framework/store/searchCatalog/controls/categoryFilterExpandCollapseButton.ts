module cef.store.searchCatalog.controls {
    class CategoryFilterExpandCollapsedButtonController extends core.TemplatedControllerBase {
        // Convenience Points (reduces HTML size)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Bound Scope properties
        category: api.AggregateTree;
        // Properties
        showChildren = true;
        get show(): boolean {
            if (!this || !this.category) {
                return null;
            }
            return this.category.Key != 'N/A'
                && this.category.HasChildren;
        }
        get icon(): string {
            if (!this.category) {
                return 'far fa-plus';
            }
            return this.category["showChildren"]
                ? 'far fa-minus'
                : 'far fa-plus';
        }
        get titleKey(): string {
            return "ui.storefront.searchCatalog.filters.ToggleShowHideCategoryChildren.Message";
        }
        // Functions
        swapShow(): void {
            this.category["showChildren"] = !this.category["showChildren"];
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefCatFilterExpColBtn", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            category: "=",
            index: "=",
            mode: "@"
        },
        // replace: true,
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/categoryFilterExpandCollapseButton.html", "ui"),
        controller: CategoryFilterExpandCollapsedButtonController,
        controllerAs: "cfecbCtrl",
        bindToController: true,
        require: ["^cefSearchCatalogFilterByCategoriesTree"]
    }));
}
