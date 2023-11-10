/**
 * @file framework/admin/purchasing/steps/payment/methods/wireTransfer/api.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.wireTransfer {
    export class WireTransferPaymentMethod extends PaymentMethodBase {
        // Properties
        paymentMethod: { [lookupKey: string]: string } = {}; // Bound to UI via the controller
        paymentMethods: { [lookupKey: string]: { [method: string]: methods.IPaymentMethod } } = {};
        paymentData: api.CheckoutModel = { } as api.CheckoutModel; // Bound to UI via the controller
        get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.wireTransfer;
        }
        submit(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `WireTransferPaymentMethod.submit(cart: "${cart && cart.TypeName}")`;
            if (!this.name) {
                this.consoleDebug(`${debugMsg} no name yet`);
                return this.$q.reject(`${debugMsg} no name yet`);
            }
            const dataToUse = this.paymentData;
            return this.$q.resolve(this.cvPurchaseService.finalize(api.CartByIDLookupKey.newFromCart(cart), dataToUse));
        }
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig);
            this.consoleDebug(`WireTransferPaymentMethod.ctor()`);
        }
    }
}
