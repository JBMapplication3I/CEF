module cef.admin {
    class StandardDetailHeaderWidgetController extends core.TemplatedControllerBase {
        // Properties
        record: api.BaseModel;
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

    adminApp.directive("cefStandardDetailHeaderWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            record: "=", // The main object, like a Product
            newKindKey: "@", // Translation key that would result to something like "New Product"
            existingKindKey: "@", // Translation key that would result to something like "Product"
        },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/standardDetailHeaderWidget.html", "ui"),
        controller: StandardDetailHeaderWidgetController,
        controllerAs: "standardDetailHeaderWidgetCtrl",
        bindToController: true
    }));
}
