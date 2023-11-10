module cef.store.user.registration.steps.custom {
    class RegistrationStepCustomSummaryController extends core.TemplatedControllerBase {
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

    cefApp.directive("cefRegStepCustomSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/custom/summary.html", "ui"),
        controller: RegistrationStepCustomSummaryController,
        controllerAs: "regStepCustomSummaryCtrl",
        bindToController: true
    }));
}
