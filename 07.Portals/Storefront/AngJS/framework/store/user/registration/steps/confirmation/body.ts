module cef.store.user.registration.steps.confirmation {
    class RegistrationStepConfirmationBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected idSuffix: string;
        // Properties
        private get step(): RegistrationStepConfirmation {
            if (!this.cvRegistrationService ||
                !this.cvRegistrationService.steps) {
                return null; // Not available (yet)
            }
            return <RegistrationStepConfirmation>
                _.find(this.cvRegistrationService.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.confirmation);
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`RegistrationStepConfirmationBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepConfirmationBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        protected get termsAgreementAgreed(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.termsAgreementAgreed;
        }
        protected set termsAgreementAgreed(newValue: boolean) {
            this.consoleDebug(`RegistrationStepConfirmationBodyController.termsAgreementAgreed.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepConfirmationBodyController.termsAgreementAgreed.set 2`);
                this.step.termsAgreementAgreed = newValue;
            }
        }
        protected get isContractor(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.isContractor;
        }
        protected set isContractor(newValue: boolean) {
            if (this.step) {
                this.step.isContractor = newValue;
            }
        }
        // Functions
        // <None>
        // Events
        onChange = (): void => {
            const debugMsg = `RegistrationStepConfirmationBodyController.onChange()`;
            this.consoleDebug(debugMsg);
            this.invalid = true;
            if (this.forms.confirmation.$invalid) {
                this.consoleDebug(`${debugMsg} failed! Form is invalid`);
                this.consoleDebug(this.forms.confirmation.$error);
                this.invalid = true;
                return;
            }
            this.setRunning();
            this.step.validate().then(success => {
                if (!success) {
                    this.consoleDebug(`${debugMsg} failed! Not a successful step.validate`);
                    this.invalid = true;
                    this.finishRunning(true, "An error has occurred");
                    return;
                }
                this.consoleDebug(`${debugMsg} success!`);
                this.invalid = false;
                this.finishRunning();
            }).catch(reason => {
                this.consoleDebug(`${debugMsg} failed! Catch`);
                this.consoleDebug(reason);
                this.invalid = true;
                this.finishRunning(true, reason || "An error has occurred");
            });
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvRegistrationService: services.IRegistrationService,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.consoleDebug(`RegistrationStepConfirmationBodyController.ctor()`);
        }
    }

    cefApp.directive("cefRegStepConfirmationBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { idSuffix: "@" },
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/confirmation/body.html", "ui"),
        controller: RegistrationStepConfirmationBodyController,
        controllerAs: "rscbCtrl",
        bindToController: true
    }));
}
