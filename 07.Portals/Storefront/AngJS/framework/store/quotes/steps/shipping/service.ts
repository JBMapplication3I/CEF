/**
 * @file framework/store/quotes/steps/shipping/step.shipping.api.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote items in the quote cart: The steps of the wizard
 */
module cef.store.quotes.steps.shipping {
    // This is part of the Service
    export class SubmitQuoteStepShipping extends SubmitQuoteStep {
        // Properties
        get name(): string { return this.cvServiceStrings.submitQuote.steps.shipping; }
        book: api.AccountContactModel[] = []; // Bound on address book load event via the controller
        protected contactIsFromBook(): boolean {
            return this.currentAccountContact &&
                this.currentAccountContact.ID > 0;
        }
        get invalid(): boolean {
            return this.invalidByAddress || this.invalidByRates;
        }
        set invalid(newValue: boolean) {
            // Do Nothing
            // console.error("tried to set shipping step invalid to a value");
        }
        invalidByAddress: boolean = true;
        invalidByRates: boolean = true;
        currentAccountContact: api.AccountContactModel = null; // Bound to UI via the controller
        addressTitle: string = null; // Bound to UI via the controller
        get showShippingSubtotal(): boolean {
            return this.cvSubmitQuoteService.showShippingSubtotal;
        }
        set showShippingSubtotal(newValue: boolean) {
            this.cvSubmitQuoteService.showShippingSubtotal = newValue;
        }
        private specialInstructionsValue: string = null;
        get specialInstructions(): string {
            if (this.cefConfig.submitQuote.showSpecialInstructions) {
                return this.specialInstructionsValue;
            }
            return undefined;
        }
        set specialInstructions(newValue: string) {
            if (this.cefConfig.submitQuote.showSpecialInstructions) {
                this.specialInstructionsValue = newValue;
            }
        }
        // Populated by service call on button press from rate quotes manager
        rateQuotes: { [cartType: string]: Array<api.RateQuoteModel> } = { };
        haveRateQuotes: { [cartType: string]: boolean } = { };
        haveShippingOrEquivalentForRates: { [cartType: string]: boolean } = { };
        selectedRateQuoteID: { [cartType: string]: number } = { };
        showDeliveryFail: { [cartType: string]: boolean } = { };
        isGettingRates: { [cartType: string]: boolean } = { };
        // Functions
        canEnable(cart: api.CartModel): ng.IPromise<boolean> {
            this.consoleDebug(`SubmitQuoteStepShipping.canEnable(cart: "${cart && cart.TypeName}")`);
            if (!this.name) {
                this.consoleDebug(`SubmitQuoteStepShipping.canEnable(cart: "${cart && cart.TypeName}") No name yet`);
                return this.$q.reject(`SubmitQuoteStepShipping.canEnable(cart: "${cart && cart.TypeName}") No name yet`);
            }
            if (!this.cefConfig.submitQuote.sections[this.name] ||
                !this.cefConfig.submitQuote.sections[this.name].show) {
                this.consoleDebug(`SubmitQuoteStepShipping.canEnable(cart: "${cart && cart.TypeName}") Section set to not show`);
                return this.$q.resolve(false);
            }
            if (cart.NothingToShip) {
                this.consoleDebug(`SubmitQuoteStepShipping.canEnable(cart: "${cart && cart.TypeName}") Cart does not contain any shippable items`);
                return this.$q.resolve(false);
            }
            return this.$q.resolve(true);
        }
        initialize(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `SubmitQuoteStepShipping.initialize(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            this.building[cart.TypeName] = true;
            return this.$q((resolve, __) => {
                // Load the default billing address if possible so that it can be pre-selected
                const doneFn = () => {
                    this.consoleDebug(`${debugMsg} done`);
                    this.building[cart.TypeName] = false;
                    resolve(true);
                };
                this.cvAuthenticationService.preAuth().finally(() => {
                    this.consoleDebug(`${debugMsg} preAuth finished`);
                    if (!this.cvAuthenticationService.isAuthenticated()) {
                        this.currentAccountContact = this.cvAddressBookService.blankAccountContactModel();
                        doneFn();
                        return;
                    }
                    this.cvAddressBookService.refreshContactChecks(false, "submitQuote.steps.shipping.service").finally(() => {
                        this.consoleDebug(`${debugMsg} refreshContactChecks finished`);
                        if (!this.cvAddressBookService.defaultShippingID) {
                            this.consoleDebug(`${debugMsg} no default shipping available to assign`);
                            this.currentAccountContact = this.cvAddressBookService.blankAccountContactModel();
                            doneFn();
                            return;
                        }
                        this.consoleDebug(`${debugMsg} assigning default shipping`);
                        this.currentAccountContact = this.cvAddressBookService.defaultShipping;
                        this.validate(cart).finally(() => doneFn());
                    });
                });
            });
        }
        private validateRates(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `SubmitQuoteStepShipping.validateRates(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            if (!this.cefConfig.featureSet.shipping.rates.estimator.enabled) {
                this.consoleDebug(`${debugMsg} Disabled, so ignore it`);
                this.invalidByRates = false;
                return this.$q.resolve(true);
            }
            if (!this.rateQuotes
                || !this.rateQuotes[cart.TypeName]
                || !this.rateQuotes[cart.TypeName].length) {
                this.consoleDebug(`${debugMsg} No Rates`);
                this.haveRateQuotes[cart.TypeName] = false;
                this.haveShippingOrEquivalentForRates[cart.TypeName] =
                    (cart.ShippingSameAsBilling || false) && !cart.BillingContact && !cart.BillingContactID
                        ? false
                        : !this.cefConfig.featureSet.shipping.splitShipping.enabled
                          && !(cart.ShippingSameAsBilling || false) && !cart.ShippingContact && !cart.ShippingContactID
                            ? false
                            : true;
                this.invalidByRates = true;
                return this.$q.reject("Error: No rates");
            }
            this.haveRateQuotes[cart.TypeName] = true;
            this.haveShippingOrEquivalentForRates[cart.TypeName] =
                (cart.ShippingSameAsBilling || false) && !cart.BillingContact && !cart.BillingContactID
                    ? false
                    : !this.cefConfig.featureSet.shipping.splitShipping.enabled
                      && !(cart.ShippingSameAsBilling || false) && !cart.ShippingContact && !cart.ShippingContactID
                        ? false
                        : true;
            if (!_.some(this.rateQuotes[cart.TypeName], x => x.Selected) &&
                !this.selectedRateQuoteID[cart.TypeName]) {
                this.consoleDebug(`${debugMsg} Error: Rates but none selected`);
                this.invalidByRates = true;
                return this.$q.reject("Error: Rates but none selected");
            }
            this.consoleDebug(`${debugMsg} Valid (have rates and selected)`);
            this.invalidByRates = false;
            return this.$q.resolve(this.invalid);
        }
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `SubmitQuoteStepShipping.validate(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            if (!this.name) {
                const msg = `${debugMsg} Shipping: Error: no name yet`;
                this.consoleDebug(msg);
                this.invalidByAddress = true;
                return this.$q.reject(msg);
            }
            if (!this.cefConfig.submitQuote.sections[this.name].show) {
                const msg = `${debugMsg} Shipping: Not Enabled`;
                this.consoleDebug(msg);
                this.invalidByAddress = false;
                return this.$q.reject(msg);
            }
            if (!this.currentAccountContact
                || !this.currentAccountContact.Slave
                || !this.currentAccountContact.Slave.Address
                || !this.currentAccountContact.Slave.Address.CountryID) {
                const msg = `${debugMsg} Shipping: Error: no country selected`;
                this.consoleDebug(msg);
                this.invalidByAddress = true;
                return this.$q.reject(msg);
            }
            if (this.contactIsFromBook
                && this.currentAccountContact.Slave.Address.SerializableAttributes
                && _.some(Object.keys(this.currentAccountContact.Slave.Address.SerializableAttributes),
                    x => x.startsWith("Validated-By-"))) {
                this.consoleLog(`${debugMsg} Shipping: Valid!`);
                this.invalidByAddress = false;
                return this.validateRates(cart);
            }
            return this.$q((resolve, reject) => {
                this.cvAddressBookService.validate(this.currentAccountContact.Slave, false)
                    .then(r => {
                        if (!r.ActionSucceeded) {
                            const msg = `Shipping: Invalid: ${r.Messages}`;
                            this.consoleLog(msg);
                            if (r.Messages && r.Messages.length) {
                                r.Messages.forEach(x => this.consoleLog(x));
                            }
                            this.invalidByAddress = true;
                            reject(r);
                            return;
                        }
                        this.currentAccountContact.Slave = r.Result;
                        this.consoleLog("Shipping: Valid Address!");
                        this.invalidByAddress = false;
                        resolve(this.validateRates(cart));
                    }).catch(reason => {
                        const msg = `Shipping: Invalid: Server Error: ${reason}`;
                        this.consoleLog(msg);
                        this.invalidByAddress = true;
                        reject(reason);
                    });
            });
        }
        submit(cartType: string): ng.IPromise<boolean> {
            this.consoleDebug(`SubmitQuoteStepShipping.submit(cartType: "${cartType}")`);
            return this.$q((resolve, reject) => {
                this.cvCartService.applyShippingContact(
                    cartType,
                    this.currentAccountContact.Slave)
                .then(() => {
                    this.showShippingSubtotal = true;
                    resolve(this.cvSubmitQuoteService.finalize(cartType));
                }).catch(reject);
            });
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvAddressBookService: services.IAddressBookService,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvSubmitQuoteService: services.ISubmitQuoteService) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`SubmitQuoteStepShipping.ctor()`);
        }
    }
}
