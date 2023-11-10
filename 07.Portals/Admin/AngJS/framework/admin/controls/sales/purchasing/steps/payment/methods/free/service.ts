/**
 * @file framework/admin/purchasing/steps/payment/methods/free/api.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.free {
    export class FreePaymentMethod extends PaymentMethodBase {
        get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.free;
        }
        paymentData: api.CheckoutModel = { } as api.CheckoutModel; // Bound to UI via the controller
        submit(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `FreePaymentMethod.submit(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            // TODO: Validate the payment data entered
            if (!this.name) {
                this.consoleDebug(`${debugMsg} no name yet`);
                return this.$q.reject(`${debugMsg} no name yet`);
            }
            return this.$q.resolve(this.cvPurchaseService.finalize(api.CartByIDLookupKey.newFromCart(cart), this.paymentData));
        }
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig);
            this.consoleDebug(`FreePaymentMethod.ctor()`);
        }
    }
}
