module cef.store.searchCatalog.controls {
    class SearchCatalogResultsPageFormatController extends core.TemplatedControllerBase {
        // Convenience Redirects (Reduce binding text/conditions)
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        // Bound Scope Properties
        pageFormatViewType: string;
        defaultValue: string;
        hideText: boolean;
        hideLabel: boolean;
        layoutMode: boolean;
        // Properties
        get searchViewModel(): any {
            return this.service.activeSearchViewModel;
        }
        get currentChoice(): string {
            return this.searchViewModel && this.searchViewModel.Form.PageFormat || this.defaultValue || "grid";
        }
        set currentChoice(newValue: string) {
            this.searchViewModel.Form.PageFormat = newValue;
        }
        get choices(): Array<string> {
            return this.service.resultsPageFormatChoices || ["grid","table","list"];
        }
        get icons(): cefalt.store.Dictionary<string> {
            return this.service.resultsPageFormatChoiceIcons
                || {
                    "grid": "fas fa-th-large",
                    "table": "far fa-table",
                    "list": "fas fa-th-list"
                };
        }
        // Functions
        // <None>
        // Constructor
        constructor(
                private readonly $state: ng.ui.IStateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSearchCatalogResultsPageFormat", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            pageFormatViewType: "@",
            defaultValue: "@",
            hideText: "@",
            hideLabel: "@",
            layoutMode: "@",
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/resultsPageFormatSelector.html", "ui"),
        controller: SearchCatalogResultsPageFormatController,
        controllerAs: "scrpfCtrl",
        bindToController: true,
        require: ["^cefSearchCatalog"]
    }));
}
