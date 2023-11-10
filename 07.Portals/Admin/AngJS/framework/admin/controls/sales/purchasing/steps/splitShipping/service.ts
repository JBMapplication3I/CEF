/**
 * @file framework/admin/purchasing/steps/ssplitShipping/step.splitShipping.api.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.splitShipping {
    // This is part of the Service
    export class PurchaseStepSplitShipping extends PurchaseStep {
        // Properties
        get name(): string { return this.cvServiceStrings.checkout.steps.splitShipping; }
        book: api.AccountContactModel[] = []; // Bound on address book load event via the controller
        protected contactIsFromBook(accountContact: api.AccountContactModel): boolean {
            return accountContact && accountContact.ID > 0;
        }
        get invalid(): boolean {
            return this.invalidByAddress || this.invalidByRates;
        }
        set invalid(newValue: boolean) {
            // Do Nothing
            // console.error("tried to set split shipping step invalid to a value");
        }
        invalidByAddress: boolean = true;
        invalidByRates: boolean = true;
        sameAsBilling: boolean = false; // Bound to UI via the controller
        get showShippingSubtotal(): boolean {
            return this.cvPurchaseService.showShippingSubtotal;
        }
        set showShippingSubtotal(newValue: boolean) {
            this.cvPurchaseService.showShippingSubtotal = newValue;
        }
        private specialInstructionsValue: string = null;
        get specialInstructions(): string {
            if (this.cefConfig.purchase.showSpecialInstructions) {
                return this.specialInstructionsValue;
            }
            return undefined;
        }
        set specialInstructions(newValue: string) {
            if (this.cefConfig.purchase.showSpecialInstructions) {
                this.specialInstructionsValue = newValue;
            }
        }
        // Functions
        canEnable(cart: api.CartModel): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStepSplitShipping.canEnable(cart: "${cart && cart.TypeName}")`);
            if (!this.name) {
                this.consoleDebug(`PurchaseStepSplitShipping.canEnable(cart: "${cart && cart.TypeName}") No name yet`);
                return this.$q.reject(`PurchaseStepSplitShipping.canEnable(cart: "${cart && cart.TypeName}") does not have a name yet`);
            }
            if (!this.cefConfig.purchase.sections[this.name] ||
                !this.cefConfig.purchase.sections[this.name].show) {
                this.consoleDebug(`PurchaseStepSplitShipping.canEnable(cart: "${cart && cart.TypeName}") Section set to not show`);
                return this.$q.resolve(false);
            }
            if (cart.NothingToShip) {
                this.consoleDebug(`PurchaseStepSplitShipping.canEnable(cart: "${cart && cart.TypeName}") Cart does not contain any shippable items`);
                return this.$q.resolve(false);
            }
            return this.$q.resolve(true);
        }
        initialize(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `PurchaseStepSplitShipping.initialize(cart: "${cart && cart.TypeName}")`;
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
                        //this.currentAccountContact = this.cvAddressBookService.blankAccountContactModel();
                        doneFn();
                        return;
                    }
                    this.cvAddressBookService.refreshContactChecks(cart.AccountID).then(() => {
                        this.consoleDebug(`${debugMsg} refreshContactChecks finished`);
                        if (!this.cvAddressBookService.defaultShippingID[cart.AccountID]) {
                            this.consoleDebug(`${debugMsg} no default shipping available to assign`);
                            //this.currentAccountContact = this.cvAddressBookService.blankAccountContactModel();
                            doneFn();
                            return;
                        }
                        this.consoleDebug(`${debugMsg} assigning default shipping`);
                        //this.currentAccountContact = this.cvAddressBookService.defaultShipping[cart.AccountID];
                        this.validate(cart).finally(() => doneFn());
                    }).catch(err => {
                        console.error(`${debugMsg} waiting for refresh contacts checks failed`);
                        console.error(err);
                    });
                });
            });
        }
        activate(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStepSplitShipping.activate(lookupKey: "${lookupKey.toString()}")`);
            // Do Nothing
            return this.$q.resolve(true);
        }
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStepSplitShipping.validate(cart: "${cart && cart.TypeName}")`);
            if (!this.name) {
                this.invalidByAddress = true;
                return this.$q.reject(`PurchaseStepSplitShipping.validate(cart: "${cart && cart.TypeName}") doesn't have a name yet`);
            }
            if (!this.cefConfig.purchase.sections[this.name].show) {
                this.invalidByAddress = false;
                this.invalidByRates = false;
                return this.$q.reject("Not Enabled");
            }
            if (!cart || !cart.SalesItems || !cart.SalesItems.length) {
                this.invalidByAddress = true;
                this.invalidByRates = true;
                return this.$q.reject("Empty/Invalid cart");
            }
            return this.$q((resolve, reject) => {
                const addressesToCheck: api.ContactModel[] = [];
                const addressesAlreadyChecked: api.ContactModel[] = [];
                if (_.some(_.filter(_.flatten(cart.SalesItems.map(x => x.Targets || [])), x => !x.NothingToShip), x => !x.DestinationContact && !x.DestinationContactID)) {
                    this.consoleDebug("Split Shipping Pane Invalid (by Address): Some shippable targets are missing destinations");
                    reject("Some shippable targets are missing destinations");
                    return;
                }
                this.$q.all(_.flatten(cart.SalesItems.map(x => x.Targets || [])).map(y => {
                    return y.DestinationContact
                        ? this.$q.resolve({ data: y.DestinationContact })
                        : y.DestinationContactID
                            ? this.cvApi.contacts.GetContactByID(y.DestinationContactID)
                            // This shouldn't happen as the above reject should have covered it
                            : this.$q.reject("No target assigned");
                })).then((rarr: { data: api.ContactModel }[]) => {
                    rarr.map(x => x.data).forEach(x => {
                        // Map missing ones onto the targets so they don't have to ask the server again
                        cart.SalesItems.forEach(y => {
                            y.Targets.forEach(z => {
                                if (z.DestinationContact || z.DestinationContactID !== x.ID) {
                                    return;
                                }
                                z.DestinationContact = x;
                            });
                        });
                        if (x && x.ID && _.some(addressesToCheck, y => y.ID === x.ID)) {
                            // Matched by ID, don't dupe
                            return;
                        } else if (x && x.ID && _.some(addressesAlreadyChecked, y => y.ID === x.ID)) {
                            // Matched by ID, don't dupe
                            return;
                        } else if (!x.ID
                            && x.Address.Street1
                            && _.some(addressesToCheck,
                                    y => !y.ID && y.Address.Street1 === x.Address.Street1)) {
                            // Matched by a lack of ID and by street, don't dupe
                            return;
                        } else if (!x.ID
                            && x.Address.Street1
                            && _.some(addressesAlreadyChecked,
                                    y => !y.ID && y.Address.Street1 === x.Address.Street1)) {
                            // Matched by a lack of ID and by street, don't dupe
                            return;
                        }
                        if (x.Address.SerializableAttributes
                            && _.some(Object.keys(x.Address.SerializableAttributes), x => x.startsWith("Validated-By-"))) {
                            // Address is previously validated, don't ask the server again
                            addressesAlreadyChecked.push(x);
                            return;
                        }
                        addressesToCheck.push(x);
                    });
                    if (addressesToCheck.length === 0 && addressesAlreadyChecked.length === 0) {
                        this.invalidByAddress = true;
                        this.consoleDebug("Split Shipping Pane Invalid (by Address): No addresses assigned to any targets");
                        reject("No addresses assigned to any targets");
                        return;
                    }
                    const rateQuoteCheck = () => {
                        // We passed the validator for all addresses in all targets
                        this.invalidByAddress = false;
                        const targetCarts = this.cvCartService.accessTargetedCarts();
                        if (!targetCarts.length) {
                            this.invalidByRates = true;
                            this.consoleDebug("Split Shipping Pane Invalid (by Rate): No target carts loaded to check for rates");
                            reject("No target carts loaded to check for rates");
                            return;
                        }
                        for (let i = 0; i < targetCarts.length; i++) {
                            if (targetCarts[i].NothingToShip) { continue; } // Doesn't need to pass this check
                            if (!this.cefConfig.featureSet.shipping.rates.estimator.enabled) {
                                // Don't need to validate rates as the estimator isn't enabled
                                continue;
                            }
                            if (!_.some(targetCarts[i].RateQuotes, y => y.Selected)) {
                                this.invalidByRates = true;
                                this.consoleDebug("Split Shipping Pane Invalid (by Rate): Target without Rate Quote selected");
                                reject("Target without Rate Quote selected");
                                return;
                            }
                        }
                        this.invalidByRates = false;
                        resolve(true);
                    };
                    if (addressesToCheck.length === 0 && addressesAlreadyChecked.length > 0) {
                        // We passed the validator for all addresses in all targets
                        this.invalidByAddress = false;
                        rateQuoteCheck();
                        return;
                    }
                    this.$q.all(_.uniqBy(addressesToCheck, x => x.ID)
                            .map(x => this.cvAddressBookService.validate(cart.AccountID, x, false)))
                        .then((rarr: api.CEFActionResponseT<api.ContactModel>[]) => {
                            for (let i = 0; i < rarr.length; i++) {
                                if (!rarr[i].ActionSucceeded) {
                                    this.consoleDebug("Split Shipping Pane Invalid (by Address): Address failed validation");
                                    reject(rarr[i].Messages);
                                    return;
                                }
                            }
                            // We passed the validator for all addresses in all targets
                            this.invalidByAddress = false;
                            rateQuoteCheck();
                        }).catch(reason => {
                            this.invalidByAddress = true;
                            this.consoleDebug("Split Shipping Pane Invalid (catch)");
                            this.consoleDebug(reason);
                            reject(reason);
                        });
                }).catch(reason => {
                    this.invalidByAddress = true;
                    this.consoleDebug(reason);
                    reject(reason);
                });
            });
        }
        submit(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStepSplitShipping.submit(lookupKey: "${lookupKey.toString()}")`);
            return this.$q(resolve => {
                this.showShippingSubtotal = true;
                resolve(true);
            });
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvAddressBookService: services.IAddressBookService,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`PurchaseStepSplitShipping.ctor()`);
        }
    }
}
