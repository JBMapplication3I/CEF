module cef.admin.widgets.paging {
    class ServerSidePagingWithSetsHeaderController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        paging: core.ServerSidePagingWithSets<any, any>;
        // Properties
        // <None>
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    adminApp.directive("cefServerSidePagingWithSetsHeader", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { paging: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/paging/serverSidePagingWithSetsHeader.html", "ui"),
        controller: ServerSidePagingWithSetsHeaderController,
        controllerAs: "wssphwsCtrl",
        bindToController: true
    }));
}
