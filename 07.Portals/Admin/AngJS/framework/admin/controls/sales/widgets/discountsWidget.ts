module cef.admin.controls.sales.widgets {
    class DiscountsWidgetController extends core.TemplatedControllerBase {
        // Properties
        record: api.SalesCollectionBaseModel;
        discountItems: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>[];
        isEditable: boolean;
        hideHeader: boolean;
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    adminApp.directive("cefDiscountsWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            record: "=", // The SalesCollection such as a SalesOrder or SalesQuote
            discountItems: "=", // The array of discount items
            isEditable: "=", // Will show edit buttons if true
            hideHeader: "=?" // [Optional] Will hide the header if true
        },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/widgets/discountsWidget.html", "ui"),
        controller: DiscountsWidgetController,
        controllerAs: "discountsWidgetCtrl",
        bindToController: true
    }));
}
