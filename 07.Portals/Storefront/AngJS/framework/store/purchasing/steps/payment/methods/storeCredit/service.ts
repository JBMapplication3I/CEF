/**
 * @file framework/store/purchasing/steps/payment/methods/storeCredit/api.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.storeCredit {
    export class StoreCreditPaymentMethod extends PaymentMethodBase {
        // Properties
        paymentMethod: { [cartType: string]: string } = {}; // Bound to UI via the controller
        paymentMethods: { [cartType: string]: { [method: string]: methods.IPaymentMethod } } = {};
        paymentData: api.CheckoutModel = { } as api.CheckoutModel; // Bound to UI via the controller
        get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.storeCredit;
        }
        submit(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `StoreCreditPaymentMethod.submit(cart: "${cart && cart.TypeName}")`;
            if (!this.name) {
                this.consoleDebug(`${debugMsg} no name yet`);
                return this.$q.reject(`${debugMsg} no name yet`);
            }
            const dataToUse = this.paymentData;
            return this.$q.resolve(this.cvPurchaseService.finalize(cart.TypeName, dataToUse));
        }
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig);
            this.consoleDebug(`StoreCreditPaymentMethod.ctor()`);
        }
    }
}
