module cef.store.user.registration.steps.confirmation {
    export class RegistrationStepConfirmation extends RegistrationStep {
        // Properties
        get name(): string { return this.cvServiceStrings.registration.steps.confirmation; }
        termsAgreementAgreed: boolean = false;
        isContractor: boolean = false;
        // Functions
        // canEnable override not required
        initialize(): ng.IPromise<boolean> {
            this.consoleDebug(`RegistrationStepConfirmation.initialize()`);
            this.building = true;
            return this.$q((resolve, __) => {
                resolve(true);
            });
        }
        validate(): ng.IPromise<boolean> {
            this.consoleDebug(`RegistrationStepConfirmation.validate()`);
            this.invalid = false;
            return this.$q.resolve(true);
        }
        submit(): ng.IPromise<boolean> {
            const debugMsg = `RegistrationStepConfirmation.submit()`;
            this.consoleDebug(debugMsg);
            if (!this.name) {
                this.consoleDebug(`${debugMsg} no name yet`);
                return this.$q.reject(`${debugMsg} no name yet`);
            }
            return this.$q.resolve(this.cvRegistrationService.finalize());
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvRegistrationService: services.IRegistrationService) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`RegistrationStepConfirmation.ctor()`);
        }
    }
}
