module cef.store.searchCatalog.controls {
    class SearchCatalogFilterByCategoriesTreeController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        mode: string;
        // Properties
        expanded = false;
        // Convenience Points (reduces HTML size)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        get selectedCategory(): string {
            return this
                && this.service.activeSearchViewModel.Form.Category;
        }
        get categoryLimit(): number {
            return this
                && this.service.activeSearchViewModel.Form.Query
                && this.service.activeSearchViewModel.Form.Query.length > 0
                    ? 50
                    : this.cefConfig.catalog.maxTopLevelCategoriesInFilter;
        }
        get inactiveCategories(): any[] {
            if (this.service.allCategories
                && this.service.activeSearchViewModel.CategoriesTree.Children) {
                return this.service.allCategories.filter(cat => {
                    return !_.includes(
                        this.service.activeSearchViewModel.CategoriesTree.Children
                            .map(cat => cat.DisplayName),
                        cat.DisplayName);
                });
            }
            return [];
        }
        // Functions
        ancestorIsNACount(...cats: api.AggregateTree[]): number {
            if (!cats) { return 0; }
            return _.sumBy(cats, cat => cat.Key === "N/A" ? 0 : 1);
        }
        expandableStyle(...cats: api.AggregateTree[]): object {
            const count = 10 * this.ancestorIsNACount(...cats);
            const marginLeft = `${count}px`;
            let width = `calc(100% - ${count}px`;
            if (cats[cats.length - 1].HasChildren) {
                width += " - 17px";
            }
            return {
                "margin-left": marginLeft,
                "width": width + ")"
            };
        }
        autoExpandIfAtLevel(cat: api.AggregateTree): void {
            if (!_.isObject(cat)) {
                return;
            }
            const activeCategoryKey = this.service.activeSearchViewModel?.Form?.Category;
            if (!_.isString(activeCategoryKey)) {
                return;
            }
            if (!_.isObject(this.service.activeSearchViewModel.CategoriesTree)) {
                return;
            }
            const activeCat = this.findCatByKey(activeCategoryKey);
            if (!_.isObject(activeCat)) {
                return;
            }
            const activeLevel = this.findCatLevel(activeCat);
            const thisLevel = this.findCatLevel(cat);
            if (activeLevel >= thisLevel) {
                cat["showChildren"] = true;
            }
        }
        findCatLevel(targetCat: api.AggregateTree, currentCat = this.service.activeSearchViewModel?.CategoriesTree, level = 0): number | null {
            if (!currentCat) return null;

            if (currentCat.Key === targetCat.Key) {
                return level;
            }

            if (_.isArray(currentCat.Children)) {
                for (let i = 0; i < currentCat.Children.length; i++) {
                    const childLevel = this.findCatLevel(targetCat, currentCat.Children[i], level + 1);

                    if (childLevel !== null) {
                        return childLevel;
                    }
                }
            }
        
            return null;
        }
        findCatByKey(key: string, currentCat = this.service.activeSearchViewModel?.CategoriesTree): api.AggregateTree | null {
            if (!currentCat) return null;
            if (currentCat.Key === key) {
                return currentCat;
            }
            if (_.isArray(currentCat.Children)) {
                for (let i = 0; i < currentCat.Children.length; i++) {
                    const foundCat = this.findCatByKey(key, currentCat.Children[i]);
                    if (foundCat !== null) {
                        return foundCat;
                    }
                }
            }
            return null;
        }
        private setExpand(cat: api.AggregateTree): void {
            cat["showChildren"] = true;
            this.expanded = true;
            if (!cat.Children) { return; }
            cat.Children.forEach(child => this.setExpand(child));
        }
        private setCollapse(cat: api.AggregateTree): void {
            if (cat.Key !== "N/A") { cat["showChildren"] = false; }
            this.expanded = false;
            if (!cat.Children) { return; }
            cat.Children.forEach(child => this.setCollapse(child));
        }
        expandAll(): void {
            if (!this.service.activeSearchViewModel.CategoriesTree) {
                return;
            }
            this.setExpand(this.service.activeSearchViewModel.CategoriesTree);
        }
        collapseAll(): void {
            if (!this.service.activeSearchViewModel.CategoriesTree) {
                return;
            }
            this.setCollapse(this.service.activeSearchViewModel.CategoriesTree);
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogFilterByCategoriesTree", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { mode: "@" },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/filterByCategoriesTree.html", "ui"),
        controller: SearchCatalogFilterByCategoriesTreeController,
        controllerAs: "scfbctCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));

    cefApp.filter("cleanCategoryKey", () => (key: string, lower: boolean = true) => {
        if (!key) { return key; }
        var retVal = (lower ? key.toLowerCase() : key).split("|")[0];
        retVal = retVal.replace(/\//, " / ").trim();
        return retVal;
    });
}
