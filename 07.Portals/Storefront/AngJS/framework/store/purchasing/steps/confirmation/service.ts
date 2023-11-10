/**
 * @file framework/store/purchasing/steps/confirmation/api.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.confirmation {
    // This is part of the Service
    export class PurchaseStepConfirmation extends PurchaseStep {
        // Properties
        get name(): string {
            return this.cvServiceStrings.checkout.steps.confirmation;
        }
        book: api.AccountContactModel[] = []; // Bound on address book load event via the controller
        protected contactIsFromBook_Bill(): boolean {
            return this.currentAccountContact_Bill
                && this.currentAccountContact_Bill.ID > 0;
        }
        protected contactIsFromBook_Ship(): boolean {
            return this.currentAccountContact_Ship
                && this.currentAccountContact_Ship.ID > 0;
        }
        currentAccountContact_Bill: api.AccountContactModel = null; // Bound to UI via the controller
        addressTitle_Bill: string = null; // Bound to UI via the controller
        currentAccountContact_Ship: api.AccountContactModel = null; // Bound to UI via the controller
        addressTitle_Ship: string = null; // Bound to UI via the controller
        // Functions
        canEnable(cart: api.CartModel): ng.IPromise<boolean> {
            const debug = `PurchaseStepConfirmation.canEnable(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debug);
            if (!this.name) {
                this.consoleDebug(`${debug} No name yet`);
                return this.$q.reject(`${debug} does not have a 'name' yet`);
            }
            if (cart.Totals.Total <= 0
                && (cart.NothingToShip
                    || !_.some(cart.SalesItems, x => !x.ProductNothingToShip))
                && this.cefConfig.purchase.sections[this.name]
                && this.cefConfig.purchase.sections[this.name].showConditionally) {
                // Since there's nothing to ship and the items are all free, we'd be skipping
                // all other panes, so we have to force enable the confirmation pane
                return this.$q.resolve(true);
            }
            return this.$q.resolve(
                this.cefConfig.purchase.sections[this.name]
                && this.cefConfig.purchase.sections[this.name].show);
        }
        initialize(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `PurchaseStepConfirmation.initialize(cart: "${cart && cart.TypeName}")`;
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
                    /*
                    if (!this.cvAuthenticationService.isAuthenticated()) {
                        this.currentAccountContact_Bill = this.cvAddressBookService.blankAccountContactModel();
                        this.currentAccountContact_Ship = this.cvAddressBookService.blankAccountContactModel();
                        doneFn();
                        return;
                    }
                    this.cvAddressBookService.refreshContactChecks(false, "purchasing.steps.confirmation.service").finally(() => {
                        this.consoleDebug(`${debugMsg} refreshContactChecks finished`);
                        const doBill = (): boolean => {
                            if (!this.cvAddressBookService.defaultBillingID) {
                                this.consoleDebug(`${debugMsg} no default billing available to assign`);
                                this.currentAccountContact_Bill = this.cvAddressBookService.blankAccountContactModel();
                                return false;
                            }
                            this.consoleDebug(`${debugMsg} assigning default billing`);
                            this.currentAccountContact_Bill = this.cvAddressBookService.defaultBilling;
                            return true;
                        };
                        const doShip = (): boolean => {
                            if (!this.cvAddressBookService.defaultShippingID) {
                                this.consoleDebug(`${debugMsg} no default shipping available to assign`);
                                this.currentAccountContact_Ship = this.cvAddressBookService.blankAccountContactModel();
                                return false;
                            }
                            this.consoleDebug(`${debugMsg} assigning default shipping`);
                            this.currentAccountContact_Ship = this.cvAddressBookService.defaultShipping;
                            return true;
                        };
                        const goodBill = doBill();
                        const goodShip = doShip();
                        const preValid = goodBill && goodShip;
                        if (!preValid) {
                            doneFn();
                            return;
                        }*/
                        this.validate(cart).finally(() => doneFn());
                        /*
                    });
                    */
                });
            });
        }
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStepConfirmation.validate(cart: "${cart && cart.TypeName}")`);
            if (!this.name) {
                const msg = `PurchaseStepConfirmation.validate(cart: "${cart && cart.TypeName}") doesn't have a name yet`;
                this.consoleLog(msg);
                this.invalid = true;
                return this.$q.reject(msg);
            }
            /*
            if (!this.cefConfig.purchase.sections[this.name].show) {
                const msg = "Conf Billing: Invalid: Not Enabled";
                this.consoleLog(msg);
                this.invalid = true;
                return this.$q.reject(msg);
            }
            */
            /*
            if (!this.currentAccountContact_Bill
                || !this.currentAccountContact_Bill.Slave
                || !this.currentAccountContact_Bill.Slave.Address
                || !this.currentAccountContact_Bill.Slave.Address.CountryID) {
                const msg = "Conf Billing: Invalid: No country selected";
                this.consoleLog(msg);
                this.invalid = true;
                return this.$q.reject(msg);
            }
            */
            /*
            if (!this.currentAccountContact_Ship
                || !this.currentAccountContact_Ship.Slave
                || !this.currentAccountContact_Ship.Slave.Address
                || !this.currentAccountContact_Ship.Slave.Address.CountryID) {
                const msg = "Conf Shipping: Invalid: No country selected";
                this.consoleLog(msg);
                this.invalid = true;
                return this.$q.reject(msg);
            }
            */
            /*
            if (this.contactIsFromBook_Bill
                && this.currentAccountContact_Bill.Slave.Address.SerializableAttributes
                && _.some(Object.keys(this.currentAccountContact_Bill.Slave.Address.SerializableAttributes),
                    x => x.startsWith("Validated-By-"))) {
                this.consoleLog("Conf Billing: Valid!");
                this.invalid = false;
                return this.$q.resolve(true);
            }
            */
            /*
            if (this.contactIsFromBook_Ship
                && this.currentAccountContact_Ship.Slave.Address.SerializableAttributes
                && _.some(Object.keys(this.currentAccountContact_Ship.Slave.Address.SerializableAttributes),
                    x => x.startsWith("Validated-By-"))) {
                this.consoleLog("Conf Shipping: Valid!");
                this.invalid = false;
                return this.$q.resolve(true);
            }
            */
            return this.$q((resolve, reject) => {
                /*
                this.cvAddressBookService.validate(this.currentAccountContact_Bill.Slave, false).then(r => {
                    if (!r.ActionSucceeded) {
                        const msg = `Conf Billing: Invalid: ${r.Messages}`;
                        this.consoleLog(msg);
                        if (r.Messages && r.Messages.length) {
                            r.Messages.forEach(x => this.consoleLog(x));
                        }
                        this.invalid = true;
                        reject(r);
                        return;
                    }
                    this.currentAccountContact_Bill.Slave = r.Result;
                    this.consoleLog("Conf Billing: Valid!");
                    this.cvAddressBookService.validate(this.currentAccountContact_Ship.Slave, false).then(r => {
                        if (!r.ActionSucceeded) {
                            const msg = `Conf Shipping: Invalid: ${r.Messages}`;
                            this.consoleLog(msg);
                            if (r.Messages && r.Messages.length) {
                                r.Messages.forEach(x => this.consoleLog(x));
                            }
                            this.invalid = true;
                            reject(r);
                            return;
                        }
                        this.currentAccountContact_Ship.Slave = r.Result;
                        this.consoleLog("Conf Shipping: Valid!");
                        */
                        this.invalid = false;
                        resolve(true);
                        /*
                    }).catch(reason => {
                        const msg = `Conf Shipping: Invalid: Server Error: ${reason}`;
                        this.consoleLog(msg);
                        this.invalid = true;
                        reject(reason);
                    });
                }).catch(reason => {
                    const msg = `Conf Billing: Invalid: Server Error: ${reason}`;
                    this.consoleLog(msg);
                    this.invalid = true;
                    reject(reason);
                });
                */
            });
        }
        submit(cartType: string): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStepConfirmation.submit(cartType: "${cartType}")`);
            return this.$q((resolve, reject) => {
                /*this.cvCartService.applyBillingContact(
                    cartType,
                    this.currentAccountContact_Bill.Slave
                ).then(() => {
                    this.cvCartService.applyShippingContact(
                            cartType,
                            this.currentAccountContact_Ship.Slave)
                        .then(() => resolve(*/
                            this.cvPurchaseService.finalize(
                                cartType,
                                {
                                    PaymentStyle: this.cvServiceStrings.checkout.paymentMethods.invoice,
                                    IsSameAsBilling: false,
                                    Billing: this.currentAccountContact_Bill.Slave,
                                    Shipping: this.currentAccountContact_Ship.Slave,
                                })/*))
                        .catch(reject)
                }).catch(reject);
                */
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
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`PurchaseStepConfirmation.ctor()`);
        }
    }
}
