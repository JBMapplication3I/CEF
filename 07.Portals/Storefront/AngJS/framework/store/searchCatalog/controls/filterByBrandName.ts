module cef.store.searchCatalog.controls {
    class SearchCatalogFilterByBrandNameController extends core.TemplatedControllerBase {
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        get form(): api.ProductCatalogSearchForm {
            return this && this.service.activeSearchViewModel.Form;
        }
        // Properties
        mode: string; // From scope
        showOptions = new cefalt.store.Dictionary<boolean>();
        get searchIsRunning() { return this.cvSearchCatalogService.searchIsRunning; }
        private _brandNames: string[];
        get brandNames(): string[] {
            if (this._brandNames) {
                return this._brandNames;
            }
            if (!this.cvSearchCatalogService.activeSearchViewModel.ResultIDs) {
                return null;
            }
            this.cvProductService.bulkGet(this.cvSearchCatalogService.activeSearchViewModel.ResultIDs)
                .then(r => this.brandNames = r.map(x => x.BrandName).filter(Boolean));
            return this.brandNames;
        }
        set brandNames(value: string[]) {
            this._brandNames = value;
        }
        get formBrandNames(): string[] {
            return this.cvSearchCatalogService
                && this.cvSearchCatalogService.activeSearchViewModel
                && this.cvSearchCatalogService.activeSearchViewModel.Form
                && this.cvSearchCatalogService.activeSearchViewModel.Form["BrandNames"];
        }
        isActive(brandName: string): boolean {
            return this.formBrandNames
                && this.formBrandNames.length
                && this.formBrandNames.indexOf(brandName) !== -1;
        }
        // Functions
        pushBrandName(brandName: string): void {
            if (this.searchIsRunning) { return; }
            this.cvSearchCatalogService.pushBrandName(brandName);
        }
        expandAll(): void {
            Object.keys(this.brandNames).forEach(x => this.showOptions[x] = true);
        }
        collapseAll(): void {
            Object.keys(this.brandNames).forEach(x => this.showOptions[x] = false);
        }
        toggleShowOptions(key: string): void {
            this.showOptions[key] = !this.showOptions[key];
        }
        optionIsActive(option: string): boolean {
            return false;
        }
        plusOrMinus(key: string): string {
            return this.showOptions[key] ? "fa-minus" : "fa-plus";
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvProductService: services.IProductService,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogFilterByBrandName", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            mode: "@" // "single" (default: "single")
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/filterByBrandName.html", "ui"),
        controller: SearchCatalogFilterByBrandNameController,
        controllerAs: "scfbbnCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
