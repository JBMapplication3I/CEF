/**
 * @file framework/store/user/registration/steps/basicInfo/service.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Register for the site: The steps of the wizard
 */
module cef.store.user.registration.steps.addressBook {
    // This is part of the Service
    export class RegistrationStepAddressBook extends RegistrationStep {
        // Properties
        get name(): string { return this.cvServiceStrings.registration.steps.addressBook; }
        book: api.AccountContactModel[] = [];
        hasService: boolean = true;
        // Functions
        canEnable(): ng.IPromise<boolean> {
            const debugMsg = `RegistrationStepAddressBook.canEnable()`;
            this.consoleDebug(debugMsg);
            if (!this.name) {
                this.consoleDebug(`${debugMsg} No name yet`);
                return this.$q.reject(`${debugMsg} does not have a 'name' yet`);
            }
            if (!this.cefConfig.featureSet.addressBook.enabled) {
                this.consoleDebug(`${debugMsg} Address Book is disabled`);
                return this.$q.resolve(false);
            }
            if (!this.cefConfig.featureSet.payments.enabled
                && !this.cefConfig.featureSet.shipping.enabled) {
                this.consoleDebug(`${debugMsg} Both Payments and Shipping are disabled`);
                return this.$q.resolve(false);
            }
            // Do Nothing
            return this.$q.resolve(
                this.cefConfig.register.sections[this.name] &&
                this.cefConfig.register.sections[this.name].show);
        }
        // initialize override not required
        validate(): ng.IPromise<boolean> {
            const debugMsg = `RegistrationStepAddressBook.validate()`;
            this.consoleDebug(debugMsg);
            if (!this.book || !this.book.length) {
                this.invalid = true;
                this.consoleLog(`${debugMsg}: invalid by having an empty book`);
                return this.$q.reject(`${debugMsg}: invalid by having an empty book`);
            }
            if (this.cefConfig.featureSet.payments.enabled
                && !this.hasBilling()) {
                this.invalid = true;
                this.consoleLog(`${debugMsg}: invalid by having a book without a billing address for payments`);
                return this.$q.reject(`${debugMsg}: invalid by having a book without a billing address for payments`);
            }
            if (this.cefConfig.featureSet.shipping.enabled
                && !this.hasShipping()) {
                this.invalid = true;
                this.consoleLog(`${debugMsg}: invalid by having a book without a shipping address for shipments`);
                return this.$q.reject(`${debugMsg}: invalid by having a book without a shipping address for shipments`);
            }
            this.invalid = false;
            return this.$q.resolve(true);
        }
        // submit override not required
        hasBilling(): boolean {
            if (!this.book) { return false; }
            return _.some(this.book, x => x.IsBilling);
        }
        hasShipping(): boolean {
            if (!this.book) { return false; }
            return _.some(this.book, x => x.IsPrimary);
        }
        billing(): api.AccountContactModel {
            if (!this.book) { return null; }
            return _.find(this.book, x => x.IsBilling);
        }
        shipping(): api.AccountContactModel {
            if (!this.book) { return null; }
            return _.find(this.book, x => x.IsPrimary);
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`RegistrationStepAddressBook.ctor()`);
        }
    }
}
