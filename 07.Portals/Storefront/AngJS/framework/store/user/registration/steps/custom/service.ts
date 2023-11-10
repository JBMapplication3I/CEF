module cef.store.user.registration.steps.custom {
    export class RegistrationStepCustom extends RegistrationStep {
        // Properties
        get name(): string { return this.cvServiceStrings.registration.steps.custom; }
        // Functions
        canEnable(): ng.IPromise<boolean> {
            // This function must be overriden in the client file to enable the step
            return this.$q.resolve(false)
        }
        // initialize override not required here, but should be in the client override file
        // validate override not required here, but should be in the client override file
        // submit override not required here, but should be in the client override file
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`RegistrationStepCustom.ctor()`);
        }
    }
}
