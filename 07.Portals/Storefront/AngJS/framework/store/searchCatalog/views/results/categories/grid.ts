module cef.store.searchCatalog.views.results {
    class SearchCatalogCategoryResultsGridController extends SearchCatalogResultsControllerBase {
        // Bound Scope Properties
        // <See inherited>
        // Convenience Redirects
        // <See inherited>
        // Properties
        // <None>
        // Functions
        filterByOnlyShowCurrentCategoriesLevel(): (recordID: number) => boolean {
            return (recordID: number): boolean => {
                let selectedCategory = this.service.selectedCategory;
                if (!_.isObject(selectedCategory)) {
                    switch (this.$rootScope["globalBrand"].CustomKey) {
                        case "ems": {
                            selectedCategory = this.service.allCategories.find(cat => cat.CustomKey === "EMS");
                            break;
                        }
                        case "medsurg": {
                            selectedCategory = this.service.allCategories.find(cat => cat.CustomKey === "Medsurg");
                            break;
                        }
                    }
                }
                const activeCat = _.find(this.service.allCategories, cat => cat.CustomKey === selectedCategory?.CustomKey);
                const recordCat = _.find(this.service.allCategories, cat => cat.ID === recordID);
        
                if (!_.isArray(this.service.allCategories)) {
                    return false;
                }
        
                if (!recordCat) {
                    return false;
                }
        
                const activeResult = activeCat ? this.findCat(activeCat) : { cat: null, level: 1 };
                const recordResult = this.findCat(recordCat);
                if (recordResult.level === null) {
                    return _.some(activeCat?.Children ?? [], ch => ch.ID === recordID);
                }
                return activeResult.level + 1 === recordResult.level;
            };
        }
        
        findCat(targetCat: api.CategoryModel, level = 0): { cat: api.AggregateTree | null, level: number | null } {
            const findLevel = (treeCat: api.AggregateTree, currentLevel: number): { cat: api.AggregateTree | null, level: number | null } => {
                if (treeCat.Key.split("|")[0] === targetCat.Name) {
                    return { cat: treeCat, level: currentLevel };
                }
                if (_.isArray(treeCat.Children) && treeCat.Children.length > 0) {
                    for (let i = 0; i < treeCat.Children.length; i++) {
                        const childResult = findLevel(treeCat.Children[i], currentLevel + 1);
                        if (childResult.cat !== null) {
                            return childResult;
                        }
                    }
                }
                return { cat: null, level: null };
            };
            return findLevel(this.service.activeSearchViewModel.CategoriesTree, level);
        }
        
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig, cvAuthenticationService, cvSearchCatalogService);
        }
    }

    cefApp.directive("cefSearchCatalogCategoryResultsGrid", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            thumbWidth: "=?",
            thumbHeight: "=?",
            nameRows: "=?",
            hideStock: "=?",
            hideSku: "=?",
            hideShortDesc: "=?",
            shortDescRows: "=?",
            pricingDisplayStyle: "=?",
            quickAdd: "=?",
            actionButtonView: "=?",
            hideFavoritesList: "=?",
            hideWishList: "=?",
            hideNotifyMe: "=?",
            hideCompare: "=?",
            hideIcons: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/views/results/categories/grid.html", "ui"),
        controller: SearchCatalogCategoryResultsGridController,
        controllerAs: "sccagCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
