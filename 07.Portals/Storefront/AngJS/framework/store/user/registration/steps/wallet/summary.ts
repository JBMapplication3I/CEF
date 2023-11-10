module cef.store.user.registration.steps.wallet {
    class RegistrationStepWalletSummaryController extends core.TemplatedControllerBase {
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

    cefApp.directive("cefRegStepWalletSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/wallet/summary.html", "ui"),
        controller: RegistrationStepWalletSummaryController,
        controllerAs: "regStepWalletSummaryCtrl",
        bindToController: true
    }));
}
