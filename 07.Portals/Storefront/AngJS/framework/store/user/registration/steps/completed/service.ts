module cef.store.user.registration.steps.completed {
    export class RegistrationStepCompleted extends RegistrationStep {
        // Properties
        get name(): string { return this.cvServiceStrings.registration.steps.completed; }
        get continueTextKey(): string {
            if (!this.name) { return undefined; }
            if (this.cvRegistrationService.hasService) {
                return this.cefConfig.register.sections[this.name].continueTextKey;
            }
            return "ui.storefront.common.RequestService";
        }
        // Functions
        // canEnable override not required
        // initialize override not required
        // validate override not required
        // submit override not required
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvRegistrationService: services.IRegistrationService) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`RegistrationStepCompleted.ctor()`);
        }
    }
}
