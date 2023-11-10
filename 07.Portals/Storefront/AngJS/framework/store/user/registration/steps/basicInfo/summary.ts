module cef.store.user.registration.steps.basicInfo {
    class RegistrationStepBasicInfoSummaryController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        // <None>
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvRegistrationService: services.IRegistrationService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefRegStepBasicInfoSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/basicInfo/summary.html", "ui"),
        controller: RegistrationStepBasicInfoSummaryController,
        controllerAs: "regStepBasicInfoSummaryCtrl",
        bindToController: true
    }));
}
