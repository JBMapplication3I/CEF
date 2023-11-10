module cef.store.user.registration.steps.wallet {
    export class RegistrationStepWallet extends RegistrationStep {
        // Properties
        get name(): string { return this.cvServiceStrings.registration.steps.wallet; }
        // Functions
        canEnable(): ng.IPromise<boolean> {
            const debugMsg = `RegistrationStepWallet.canEnable()`;
            this.consoleDebug(debugMsg);
            if (!this.name) {
                this.consoleDebug(`${debugMsg} No name yet`);
                return this.$q.reject(`${debugMsg} does not have a 'name' yet`);
            }
            if (!this.cefConfig.featureSet.payments.enabled) {
                this.consoleDebug(`${debugMsg} Payments are disabled`);
                return this.$q.resolve(false);
            }
            if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                this.consoleDebug(`${debugMsg} Wallet is disabled`);
                return this.$q.resolve(false);
            }
            // Do Nothing
            return this.$q.resolve(
                this.cefConfig.register.sections[this.name] &&
                this.cefConfig.register.sections[this.name].show);
        }
        // initialize override not required
        // validate override not required
        // submit override not required
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`RegistrationStepWallet.ctor()`);
        }
    }
}
