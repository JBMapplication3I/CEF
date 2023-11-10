/**
 * @file framework/admin/purchasing/steps/billing/service.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.billing {
    // This is part of the Service
    export class PurchaseStepBilling extends PurchaseStep {
        // Properties
        get name(): string { return this.cvServiceStrings.checkout.steps.billing; }
        book: api.AccountContactModel[] = []; // Bound on address book load event via the controller
        protected contactIsFromBook(): boolean {
            return this.currentAccountContact
                && this.currentAccountContact.ID > 0;
        }
        currentAccountContact: api.AccountContactModel = null; // Bound to UI via the controller
        addressTitle: string = null; // Bound to UI via the controller
        makeThisNewDefaultBilling: boolean = false; // Bound to UI via the controller
        // Functions
        // canEnable override not required
        initialize(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `PurchaseStepBilling.initialize(cart: "${cart && cart.TypeName}")`;
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
                    this.cvAddressBookService.refreshContactChecks(cart.AccountID, false).finally(() => {
                        this.consoleDebug(`${debugMsg} refreshContactChecks finished`);
                        if (!this.cvAddressBookService.defaultBillingID[cart.AccountID]) {
                            this.consoleDebug(`${debugMsg} no default billing available to assign`);
                            this.currentAccountContact = this.cvAddressBookService.blankAccountContactModel();
                            doneFn();
                            return;
                        }
                        this.consoleDebug(`${debugMsg} assigning default billing`);
                        this.currentAccountContact = this.cvAddressBookService.defaultBilling[cart.AccountID];
                        this.validate(cart).finally(() => doneFn());
                    });
                });
            });
        }
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStepBilling.validate(cart: "${cart && cart.TypeName}")`);
            if (!this.name) {
                const msg = `PurchaseStepBilling.validate(cart: "${cart && cart.TypeName}") doesn't have a name yet`;
                this.consoleLog(msg);
                this.invalid = true;
                return this.$q.reject(msg);
            }
            if (!this.cefConfig.purchase.sections[this.name].show) {
                const msg = "Billing: Invalid: Not Enabled";
                this.consoleLog(msg);
                this.invalid = true;
                return this.$q.reject(msg);
            }
            if (!this.currentAccountContact
                || !this.currentAccountContact.Slave
                || !this.currentAccountContact.Slave.Address
                || !this.currentAccountContact.Slave.Address.CountryID) {
                const msg = "Billing: Invalid: No country selected";
                this.consoleLog(msg);
                this.invalid = true;
                return this.$q.reject(msg);
            }
            if (this.contactIsFromBook
                && this.currentAccountContact.Slave.Address.SerializableAttributes
                && _.some(Object.keys(this.currentAccountContact.Slave.Address.SerializableAttributes),
                    x => x.startsWith("Validated-By-"))) {
                this.consoleLog("Billing: Valid!");
                this.invalid = false;
                return this.$q.resolve(true);
            }
            return this.$q((resolve, reject) => {
                this.cvAddressBookService.validate(cart.AccountID, this.currentAccountContact.Slave, false)
                    .then(r => {
                        if (!r.ActionSucceeded) {
                            const msg = `Billing: Invalid: ${r.Messages}`;
                            this.consoleLog(msg);
                            if (r.Messages && r.Messages.length) {
                                r.Messages.forEach(x => this.consoleLog(x));
                            }
                            this.invalid = true;
                            reject(r);
                            return;
                        }
                        this.currentAccountContact.Slave = r.Result;
                        this.consoleLog("Billing: Valid!");
                        this.invalid = false;
                        resolve(true);
                    }).catch(reason => {
                        const msg = `Billing: Invalid: Server Error: ${reason}`;
                        this.consoleLog(msg);
                        this.invalid = true;
                        reject(reason);
                    });
            });
        }
        submit(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStepBilling.submit(lookupKey: "${lookupKey.toString()}")`);
            return this.$q((resolve, reject) => {
                this.cvCartService.accessCart(lookupKey).BillingContact = this.currentAccountContact.Slave;
                this.cvCartService.applyBillingContact(lookupKey)
                    .then(() => resolve(true))
                    .catch(reject);
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
                protected readonly cvCartService: services.ICartService) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`PurchaseStepBilling.ctor()`);
        }
    }
}
