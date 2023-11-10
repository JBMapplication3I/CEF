module cef.store.searchCatalog.controls {
    class SearchCatalogResultsControlBarController extends core.TemplatedControllerBase {
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        get searchViewModel(): any {
            return this.service.activeSearchViewModel;
        }
        get filterCount(): number {
            return this.cvSearchCatalogService.countAppliedFilters();
        }
        // Bound Scope Properties
        layoutMode: string;
        hrPosition: string;
        formatViewType: string;
        formatDefaultValue: string;
        formatHideText: boolean;
        formatHideLabel: boolean;
        perPageViewType: string;
        perPageDefaultValue: number;
        perPageHideLabel: boolean;
        perPageChoices: Array<number>;
        sortViewType: string;
        sortDefaultValue: string;
        sortHideLabel: boolean;
        pageViewType: string;
        pageDefaultValue: number;
        pageHideLabel: boolean;
        loadMoreButton: boolean;
        pricelistEnabled: boolean;
        onHandEnabled: boolean;
        // Functions
        checkPriceListOnLoad(): void {
            if (this.$stateParams["filterByCurrentAccountRoles"]
                && this.$stateParams["filterByCurrentAccountRoles"] == "true") {
                this.pricelistEnabled = true;
            }
        }
        checkOnHandOnLoad(): void {
            this.onHandEnabled = this.$stateParams["onHand"]
                && this.$stateParams["onHand"] == "true"
                || false;
        }
        pricelistEnabledOnChange(): void {
            this.searchViewModel.Form["FilterByCurrentAccountRoles"] = this.pricelistEnabled.toString();
        }
        onHandEnabledOnChange(): void {
            this.searchViewModel.Form.OnHand = this.onHandEnabled.toString();
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly $stateParams: IModalComposeStateParams) {
            super(cefConfig);
            this.checkPriceListOnLoad();
            this.checkOnHandOnLoad();
        }
    }

    cefApp.directive("cefSearchCatalogResultsControlBar", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            layoutMode: "@",            // 'standard', 'centered' or 'hug-right' (default: 'standard')
            hrPosition: "@?",           // 'top', 'bottom', 'both' or 'none' (default: 'none')
            formatViewType: "@",        // 'dropdown', 'buttons', 'split-button' or 'none' (default: 'buttons')
            formatDefaultValue: "@?",   // 'grid' or 'list' (default: 'grid')
            formatHideText: "@?",       // a boolean (default: false)
            formatHideLabel: "@?",      // a boolean (default: false)
            perPageViewType: "@",       // 'dropdown', 'buttons', 'split-button' or 'none' (default: 'dropdown')
            perPageDefaultValue: "=?",  // a positive integer (default: 9)
            perPageHideLabel: "@",      // a boolean (default: false)
            perPageChoices: "=?",       // an array of numbers to pass to the dropdown for select, if not set will default to [9,18,27]
            sortViewType: "@",          // 'dropdown', 'buttons', 'split-button' or 'none' (default: 'dropdown')
            sortDefaultValue: "@?",     // 'Relevance', 'Popular', 'Recent', 'NameAscending', 'NameDescending', 'PriceAscending', 'PriceDescending', 'Defined' (default: 'Relevance')
            sortHideLabel: "@?",        // a boolean (default: false)
            pageViewType: "@",          // 'dropdown', 'buttons' or 'none' (default: 'buttons')
            pageDefaultValue: "@?",     // a positive integer (default: 1)
            pageHideLabel: "@?",        // a boolean (default: false)
            loadMoreButton: "=?",        // a boolean (default: false)
            pricelistEnabled: "=?",
            onHandEnabled: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/resultsControlBar.html", "ui"),
        controller: SearchCatalogResultsControlBarController,
        controllerAs: "scrcbCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
