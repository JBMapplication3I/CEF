/**
 * @file framework/store/purchasing/steps/payment/methods/payPal/service.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.payPal {
    export class PayPalPaymentMethod extends PaymentMethodBase {
        get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.payPal;
        }
        get stepName(): string {
            return this.cvServiceStrings.checkout.steps.payment;
        }
        paymentData: api.CheckoutModel = { } as api.CheckoutModel; // Bound to UI via the controller

        /*selectedPayPal: services.ExtendedWalletModel = null; // Bound to UI via the controller
        walletTitle: string = null; // Bound to UI via the controller
        selectedPayPalIsFromWallet(): boolean {
            return this.selectedPayPal && this.selectedPayPal.ID > 0;
        }
        wallet: api.WalletModel[] = []; // Bound on wallet load event*/

        // Functions
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `PayPalPaymentMethod.validate(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            if (!$("#nonce").val()) {
                this.consoleDebug(`${debugMsg} payment nonce not set yet`);
                return this.$q.reject(`Submit payment through PayPal button`);
            }
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
            const debugMsg = `PayPalPaymentMethod.submit(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            if (!$("#nonce").val()) {
                this.consoleDebug(`${debugMsg} payment nonce not set yet`);
                return this.$q.reject(`Submit payment through PayPal button`);
            }
            /*if (!this.name) {
                this.consoleDebug(`${debugMsg} no name yet`);
                return this.$q.reject(`${debugMsg} no name yet`);
            }
            if (this.cefConfig.featureSet.payments.wallet.enabled &&
                this.cvAuthenticationService.isAuthenticated() &&
                this.selectedPayPalIsFromWallet()) {
                if (!dataToUse.PayByWalletEntry) {
                    dataToUse.PayByWalletEntry = <api.CheckoutPayByWalletEntry>{ };
                }
                dataToUse.PayByWalletEntry.WalletID = this.selectedPayPal.ID;
            } else if (this.paymentData.PayByPayPal) {
                if (!dataToUse.PayByPayPal) {
                    dataToUse.PayByPayPal = <api.CheckoutPayByPayPal>{ };
                }
            } else {
                throw new Error("Couldn't determine payment kind");
            }*/
            const dataToUse = { PayByPayPal: { PayPalToken: $("#nonce").val() } } as api.CheckoutModel;
            this.consoleDebug(`${debugMsg} resulting payment data:`);
            this.consoleDebug(dataToUse);
            return this.$q.resolve(this.cvPurchaseService.finalize(cart.TypeName, dataToUse));
        }
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig);
            this.consoleDebug(`PayPalPaymentMethod.ctor()`);
        }
    }
}
