module cef.store.widgets.paging {
    class ServerSidePagingHeaderController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        paging: core.ServerSidePaging<any, any>;
        disabled: boolean;
        narrow: boolean;
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

    cefApp.directive("cefServerSidePagingHeader", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { paging: "=", disabled: "=?", narrow: "=?" },
        templateUrl: $filter("corsLink")("/framework/store/widgets/paging/serverSidePagingHeader.html", "ui"),
        controller: ServerSidePagingHeaderController,
        controllerAs: "wssphCtrl",
        bindToController: true
    }));
}
