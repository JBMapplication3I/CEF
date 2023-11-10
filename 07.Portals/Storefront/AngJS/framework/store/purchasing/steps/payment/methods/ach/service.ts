/**
 * @file framework/store/purchasing/steps/payment/methods/ach/api.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.ach {
    export class ACHPaymentMethod extends PaymentMethodBase {
        get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.ach;
        }
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings) {
            super($q, cefConfig);
            this.consoleDebug(`ACHPaymentMethod.ctor()`);
        }
    }
}
