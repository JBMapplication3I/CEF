module cef.store.userDashboard.controls {
    class DashboardController extends core.TemplatedControllerBase {
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefDashboard", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/dashboard.html", "ui"),
        controller: DashboardController,
        controllerAs: "dashboardCtrl",
        bindToController: true
    }));
}
