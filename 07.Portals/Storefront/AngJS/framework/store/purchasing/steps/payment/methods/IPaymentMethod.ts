/**
 * @file framework/store/purchasing/steps/payment/methods/IPaymentMethod.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods {
    export interface IPaymentMethod {
        readonly name: string;
        readonly titleKey: string;
    }
}
