/**
 * @file framework/store/purchasing/steps/payment/methods/paymentMethodBase.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods {
    export interface IPaymentMethod {
        readonly name: string;
        readonly titleKey: string;
        readonly order: number;
        readonly templateURL: string;
        readonly uplifts: { percent?: number; amount?: number; };
        validate(cart: api.CartModel): ng.IPromise<boolean>;
        submit(cart: api.CartModel): ng.IPromise<boolean>;
    }

    export abstract class PaymentMethodBase implements IPaymentMethod {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        abstract get name(): string;
        get titleKey(): string {
            return this.cefConfig.purchase.paymentMethods[this.name].titleKey;
        }
        get order(): number {
            return this.cefConfig.purchase.paymentMethods[this.name].order;
        }
        get templateURL(): string {
            return this.cefConfig.purchase.paymentMethods[this.name].templateURL;
        }
        get uplifts(): { percent?: number; amount?: number; } {
            return this.cefConfig.purchase.paymentMethods[this.name].uplifts;
        }
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            // Do Nothing
            return this.$q.resolve(true);
        }
        submit(cart: api.CartModel): ng.IPromise<boolean> {
            // Do Nothing
            return this.$q.resolve(true);
        }
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig) {
            this.consoleDebug(`PaymentMethodBase.ctor()`);
        }
    }
}
