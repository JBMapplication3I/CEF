/**
 * @file framework/store/searchCatalog/widgets/footerCatalogWidget.ts
 * @desc Creates a searchCatalog view for sub-categories
 */
module cef.store.searchCatalog {
    class FooterCatalogWidgetController extends core.TemplatedControllerBase {
        // Properties
        selections: any[] = [
            {
                "ID": 1,
                "Name": "Industrial",
                "SortOrder": 1
            },
            {
                "ID": 2,
                "Name": "Food Service",
                "SortOrder": 2
            },
            {
                "ID": 3,
                "Name": "Hospitality",
                "SortOrder": 3
            },
            {
                "ID": 4,
                "Name": "Healthcare",
                "SortOrder": 4
            },
            {
                "ID": 5,
                "Name": "Microfiber",
                "SortOrder": 5
            },
            {
                "ID": 6,
                "Name": "Disposable",
                "SortOrder": 6
            },
            {
                "ID": 7,
                "Name": "Salon",
                "SortOrder": 7
            },
            {
                "ID": 8,
                "Name": "Retail",
                "SortOrder": 8
            },
            
        ];
        // Functions
        search($event: ng.IAngularEvent): void {
            $event.preventDefault();
            // How to send email
            window.location.href = "/PDF/sample.pdf";
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly $filter: ng.IFilterService,
                private readonly $location: ng.ILocationService,
                private readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefFooterCatalogWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/widgets/footerCatalogWidget.html", "ui"),
        controller: FooterCatalogWidgetController,
        controllerAs: "fcwCtrl"
    }));
}
