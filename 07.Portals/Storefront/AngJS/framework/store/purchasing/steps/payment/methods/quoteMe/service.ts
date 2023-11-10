/**
 * @file framework/store/purchasing/steps/payment/methods/quoteMe/api.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.quoteMe {
    export class QuoteMePaymentMethod extends PaymentMethodBase {
        // Properties
        get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.quoteMe;
        }
        get stepName(): string {
            return this.cvServiceStrings.checkout.steps.payment;
        }
        paymentData: api.CheckoutModel = { } as api.CheckoutModel; // Bound to UI via the controller
        // Functions
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `QuoteMePaymentMethod.validate(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            // TODO: Validate the payment data entered
            if (!this.name || !this.stepName) {
                this.consoleDebug(`${debugMsg} no name or step name`);
                return this.$q.reject(`${debugMsg} no name or step name`);
            }
            if (!this.cefConfig.purchase.sections[this.stepName].show) {
                this.consoleDebug(`${debugMsg} step shouldn't be shown`);
                return this.$q.reject(`${debugMsg} step shouldn't be shown`);
            }
            this.consoleDebug(`${debugMsg} step validated`);
            return this.$q.resolve(true);
        }
        submit(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `QuoteMePaymentMethod.submit(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            if (!this.name) {
                this.consoleDebug(`${debugMsg} no name yet`);
                return this.$q.reject(`${debugMsg} no name yet`);
            }
            const dataToUse = this.paymentData;
            dataToUse.PaymentStyle = this.cvServiceStrings.checkout.paymentMethods.quoteMe;
            return this.$q.resolve(
                this.cvPurchaseService.finalizeQuoteFromPurchase(
                    cart.TypeName,
                    this.cvPurchaseService.steps[cart.TypeName]));
            /*
            return this.$q((resolve, reject) => {
                this.cvCartService.submitCartAsQuote().then(r => {
                    if (!r || !r.data || !r.data.Succeeded) {
                        reject(r);
                        return;
                    }
                    this.cvPurchaseService.checkoutResult = r.data;
                    this.cvCartService.clearCart("Cart");
                    // TODO: Copy the content from the last part of the
                    // finalizeInner function that sets the completd pane
                    // as active
                    ////this.checkoutSteps[this.activeStep].active = false;
                    ////this.checkoutSteps.forEach(x => {
                    ////    x.complete = true;
                    ////    if (x.name.toLowerCase() == "confirmation") {
                    ////        x.active = true;
                    ////    }
                    ////});
                    this.cvPurchaseService.quoteSubmitted = true;
                    this.cvPurchaseService.checkoutResult = r.data;
                    this.cvPurchaseService.confirmationNumber = r.data.PaymentTransactionID
                        || (r.data.PaymentTransactionIDs ? r.data.PaymentTransactionIDs.join(", ") : "");
                    this.cvPurchaseService.isOrderComplete = true;
                    //if (this.cefConfig.googleTagManager.enabled) {
                    //    this.cvGoogleTagManagerService.purchase(cart, r.data.OrderID);
                    //}
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.orders.complete,
                        r.data.OrderID ? r.data.OrderID : r.data.OrderIDs[0],
                        cart.TypeName,
                        this.cvPurchaseService.steps[cart.TypeName]);
                    resolve(true);
                });
            });
            */
        }
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cvCartService: services.ICartService,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig);
            this.consoleDebug(`QuoteMePaymentMethod.ctor()`);
        }
    }
}
