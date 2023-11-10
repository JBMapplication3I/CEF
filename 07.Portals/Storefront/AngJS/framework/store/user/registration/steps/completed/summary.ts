module cef.store.user.registration.steps.completed {
    class RegistrationStepCompletedSummaryController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
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

    cefApp.directive("cefRegStepCompletedSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/completed/summary.html", "ui"),
        controller: RegistrationStepCompletedSummaryController,
        controllerAs: "regStepCompletedSummaryCtrl",
        bindToController: true
    }));
}
