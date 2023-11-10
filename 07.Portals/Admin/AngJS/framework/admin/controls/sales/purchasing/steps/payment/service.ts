/**
 * @file framework/admin/purchasing/steps/payment/api.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment {
    // This is part of the Service
    export class PurchaseStepPayment extends PurchaseStep {
        // Properties
        get name(): string { return this.cvServiceStrings.checkout.steps.payment; }
        paymentMethod: { [lookupKey: string]: string } = { }; // Bound to UI via the controller
        paymentMethods: { [lookupKey: string]: { [method: string]: methods.IPaymentMethod } } = { };
        defaultPaymentMethod: { [lookupKey: string]: string } = { };
        // Functions
        // canEnable override not required
        initialize(cart: api.CartModel): ng.IPromise<boolean> {
            const debug = `PurchaseStepPayment.initialize(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debug);
            if (!this.paymentMethods) {
                this.paymentMethods = { };
            }
            const asString = api.CartByIDLookupKey.newFromCart(cart).toString();
            this.paymentMethods[asString] = { };
            ////if (cart.Totals.Total <= 0 && cart.NothingToShip)
            {
                this.consoleDebug(`${debug} cart is free`);
                // The Cart total is "Free" ($0) so payment is not actually required.
                // Allow the user to to read the messaging around this and submit the
                // order.
                // Remember, item could be free but shipping might not be
                const method = new methods.free.FreePaymentMethod(
                    this.$q, this.cefConfig, this.cvServiceStrings, this.cvPurchaseService);
                this.paymentMethods[asString][method.name] = method;
                if (cart.Totals.Total <= 0) {
                    this.paymentMethod[asString] = method.name; // Select it by default
                    this.invalid = false; // Make the form valid, and allow the user to confirm order.
                }
                ////return this.$q.resolve(true);
            }
            this.consoleDebug(`${debug} read payment methods available`);
            Object.keys(this.cefConfig.purchase.paymentMethods).forEach(paymentOption => {
                switch (paymentOption.toLowerCase()) {
                    case this.cvServiceStrings.checkout.paymentMethods.ach.toLowerCase(): {
                        const method = new methods.ach.ACHPaymentMethod(
                            this.$q, this.cefConfig, this.cvServiceStrings);
                        this.paymentMethods[asString][method.name] = method;
                        break;
                    }
                    case this.cvServiceStrings.checkout.paymentMethods.creditCard.toLowerCase(): {
                        const method = new methods.creditCard.CreditCardPaymentMethod(
                            this.$q, this.cefConfig, this.cvServiceStrings, this.cvAuthenticationService,
                            this.cvWalletService, this.cvPurchaseService);
                        this.paymentMethods[asString][method.name] = method;
                        break;
                    }
                    case this.cvServiceStrings.checkout.paymentMethods.echeck.toLowerCase(): {
                        const method = new methods.echeck.EcheckPaymentMethod(
                            this.$q, this.cefConfig, this.cvServiceStrings, this.cvAuthenticationService,
                            this.cvWalletService, this.cvPurchaseService);
                        this.paymentMethods[asString][method.name] = method;
                        break;
                    }
                    case this.cvServiceStrings.checkout.paymentMethods.invoice.toLowerCase(): {
                        const method = new methods.invoice.InvoicePaymentMethod(
                            this.$q, this.cefConfig, this.cvServiceStrings, this.cvPurchaseService);
                        this.paymentMethods[asString][method.name] = method;
                        break;
                    }
                    case this.cvServiceStrings.checkout.paymentMethods.storeCredit.toLowerCase(): {
                        const method = new methods.storeCredit.StoreCreditPaymentMethod(
                            this.$q, this.cefConfig, this.cvServiceStrings, this.cvPurchaseService);
                        this.paymentMethods[asString][method.name] = method;
                        break;
                    }
                    case this.cvServiceStrings.checkout.paymentMethods.wireTransfer.toLowerCase(): {
                        const method = new methods.wireTransfer.WireTransferPaymentMethod(
                            this.$q, this.cefConfig, this.cvServiceStrings, this.cvPurchaseService);
                        this.paymentMethods[asString][method.name] = method;
                        break;
                    }
                    case this.cvServiceStrings.checkout.paymentMethods.free.toLowerCase(): {
                        // Already made it, skip
                        break;
                    }
                    default: {
                        const method = new methods.custom.CustomPaymentMethod(
                            paymentOption, this.$q, this.cefConfig, this.cvServiceStrings);
                        this.paymentMethods[asString][method.name] = method;
                        break;
                    }
                }
            });
            // Set up the first one in the order as the default selection
            this.consoleDebug(`${debug} select first payment method available`);
            var m: steps.payment.methods.IPaymentMethod[] = [];
            Object.keys(this.paymentMethods[asString])
                .forEach(key => m.push(this.paymentMethods[asString][key]));
            this.paymentMethod[asString]
                = this.defaultPaymentMethod[asString]
                = _.minBy(m, x => x.order).name;
            this.switchIfFreeOrNot(cart);
            this.consoleDebug(`${debug} finished`);
            return this.$q.resolve(true);
        }
        switchIfFreeOrNot(cart: api.CartModel): void {
            const asString = api.CartByIDLookupKey.newFromCart(cart).toString();
            if (cart.Totals.Total <= 0
                && this.paymentMethod[asString] !== this.cvServiceStrings.checkout.paymentMethods.free) {
                // We need to enforce free
                this.paymentMethod[asString] = this.cvServiceStrings.checkout.paymentMethods.free;
            } else if (cart.Totals.Total > 0
                && this.paymentMethod[asString] === this.cvServiceStrings.checkout.paymentMethods.free) {
                // We need to enforce not free
                this.paymentMethod[asString] = this.defaultPaymentMethod[asString];
            }
        }
        activate(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean> {
            const debug = `PurchaseStepPayment.activate(lookupKey: "${lookupKey.toString()}")`;
            this.consoleDebug(debug);
            this.switchIfFreeOrNot(this.cvCartService.accessCart(lookupKey));
            return this.$q.resolve(true);
        }
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            const debug = `PurchaseStepPayment.validate(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debug);
            // TODO: Validate the payment data entered
            if (!this.name) {
                return this.$q.reject(`${debug} doesn't have a name yet`);
            }
            this.switchIfFreeOrNot(cart);
            const asString = api.CartByIDLookupKey.newFromCart(cart).toString();
            return this.$q.resolve(
                this.paymentMethods[asString][this.paymentMethod[asString]]
                    .validate(cart));
        }
        submit(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStepPayment.submit(cartTlookupKeyype: "${lookupKey.toString()}")`);
            return this.$q.resolve(
                this.paymentMethods[lookupKey.toString()][this.paymentMethod[lookupKey.toString()]]
                    .submit(this.cvCartService.accessCart(lookupKey)));
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvWalletService: services.IWalletService,
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`PurchaseStepPayment.ctor()`);
        }
    }
}
