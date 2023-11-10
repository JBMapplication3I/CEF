module cef.store.user.registration.steps.wallet {
    class RegistrationStepWalletBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected idSuffix: string;
        // Properties
        private get step(): RegistrationStepWallet {
            if (!this.cvRegistrationService ||
                !this.cvRegistrationService.steps) {
                return null; // Not available (yet)
            }
            return <RegistrationStepWallet>
                _.find(this.cvRegistrationService.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.wallet);
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`RegistrationStepWalletBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepWalletBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvRegistrationService: services.IRegistrationService) {
            super(cefConfig);
            this.consoleDebug(`RegistrationStepWalletBodyController.ctor()`);
        }
    }

    cefApp.directive("cefRegStepWalletBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { idSuffix: "@" },
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/wallet/body.html", "ui"),
        controller: RegistrationStepWalletBodyController,
        controllerAs: "regStepWalletBodyCtrl",
        bindToController: true
    }));
}
