module cef.store.searchCatalog.controls {
    class SearchCatalogFiltersAppliedController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        hideQuery: boolean;
        pricingRangesMode: string;
        hidePricingRanges: boolean;
        categoriesMode: string;
        hideCategories: boolean;
        attributesMode: string;
        hideAttributes: boolean;
        hideTypes: boolean;
        // Properties
        get showNone(): boolean {
            return this
                && this.form
                && !this.form.Query
                && !this.form.Category
                && !this.form.TypeID
                && !this.form["BrandName"]
                && !this.categoriesAllHasValues()
                && !this.categoriesAnyHasValues()
                && !this.pricingRangesHasValues()
                && !this.attributesAnyHasValues()
                && !this.attributesAllHasValues()
                && !this.typeIdsAnyHasValues();
        }
        get disableClear(): boolean {
            return this
                && (this.service.searchIsRunning
                    || (this.form && !this.form.Query
                    && !this.form.Category
                    && !this.form.TypeID
                    && !this.form["BrandName"]
                    && !this.categoriesAllHasValues()
                    && !this.categoriesAnyHasValues()
                    && !this.pricingRangesHasValues()
                    && !this.attributesAnyHasValues()
                    && !this.attributesAllHasValues()
                    && !this.typeIdsAnyHasValues()));
        }
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        get form(): api.ProductCatalogSearchForm {
            return this
                && this.service
                && this.service.activeSearchViewModel
                && this.service.activeSearchViewModel.Form;
        }
        // Functions
        categoriesAnyHasValues = (): boolean => {
            if (!this.service.activeSearchViewModel) {
                return false;
            }
            return this.form.CategoriesAny
                && this.form.CategoriesAny.length > 0;
        }
        categoriesAllHasValues = (): boolean => {
            if (!this.service.activeSearchViewModel) {
                return false;
            }
            return this.form.CategoriesAll
                && this.form.CategoriesAll.length > 0;
        }
        typeIdsAnyHasValues = (): boolean => {
            if (!this.cvSearchCatalogService.activeSearchViewModel) {
                return false;
            }
            return this.form.TypeIDsAny
                && this.form.TypeIDsAny.length > 0;
        }
        pricingRangesHasValues = (): boolean => {
            if (!this.service.activeSearchViewModel) {
                return false;
            }
            return this.form.PricingRanges
                && this.form.PricingRanges.length > 0;
        }
        attributesAnyHasValues = (): boolean => {
            if (!this.service.activeSearchViewModel) {
                return false;
            }
            return this.form.AttributesAny
                && Object.keys(this.form.AttributesAny).length > 0;
        }
        attributesAllHasValues = (): boolean => {
            if (!this.service.activeSearchViewModel) {
                return false;
            }
            return this.form.AttributesAll
                && Object.keys(this.form.AttributesAll).length > 0;
        }
        attributeDisplayName(key: string): string {
            let attribute = _.find(this.service.allAttributes, attrs => attrs.CustomKey === key);
            if (!attribute) {
                return key;
            }
            return attribute.DisplayName || attribute.Name || attribute.CustomKey;
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogFiltersApplied", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            hideQuery: "@", // A boolean
            pricingRangesMode: "@", // "single", "multi-any", "multi-all" (default: "multi-any")
            hidePricingRanges: "@", // A boolean
            categoriesMode: "@",  // "single", "multi-any", "multi-all" (default: "multi-any")
            hideCategories: "@",  // A boolean
            hideTypes: "@",  // A boolean
            attributesMode: "@",  // "single", "multi-any", "multi-all" (default: "single" [until multi-any/all work, see JTG for info])
            hideAttributes: "@"   // A boolean
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/filtersApplied.html", "ui"),
        controller: SearchCatalogFiltersAppliedController,
        controllerAs: "scfaCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
