module cef.store.widgets.paging {
    class PagingHeaderController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        paging: core.Paging<any>;
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

    cefApp.directive("cefPagingHeader", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { paging: "=" },
        templateUrl: $filter("corsLink")("/framework/store/widgets/paging/pagingHeader.html", "ui"),
        controller: PagingHeaderController,
        controllerAs: "wphCtrl",
        bindToController: true
    }));
}
