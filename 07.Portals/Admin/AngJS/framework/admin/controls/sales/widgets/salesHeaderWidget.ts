module cef.admin.controls.sales.widgets {
    class SalesHeaderWidgetController extends core.TemplatedControllerBase {
        // Properties
        record: api.SalesCollectionBaseModel;
        newKindKey: string;
        existingKindKey: string;
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    adminApp.directive("cefSalesHeaderWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            record: "=", // The SalesCollection object, like a SalesOrder or SalesQuote
            newKindKey: "@", // Translation key that would result to something like "New Sales Quote"
            existingKindKey: "@", // Translation key that would result to something like "Sales Quote"
        },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/widgets/salesHeaderWidget.html", "ui"),
        controller: SalesHeaderWidgetController,
        controllerAs: "salesHeaderWidgetCtrl",
        bindToController: true
    }));
}
