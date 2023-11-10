module cef.store.user.registration.steps.confirmation {
    class RegistrationStepConfirmationSummaryController extends core.TemplatedControllerBase {
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

    cefApp.directive("cefRegStepConfirmationSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/confirmation/summary.html", "ui"),
        controller: RegistrationStepConfirmationSummaryController,
        controllerAs: "regStepConfirmationSummaryCtrl",
        bindToController: true
    }));
}
