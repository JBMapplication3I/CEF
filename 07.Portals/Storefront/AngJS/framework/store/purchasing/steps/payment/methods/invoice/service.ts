/**
 * @file framework/store/purchasing/steps/payment/methods/invoice/service.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.invoice {
    export class InvoicePaymentMethod extends PaymentMethodBase {
        // Properties
        get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.invoice;
        }
        get stepName(): string {
            return this.cvServiceStrings.checkout.steps.payment;
        }
        paymentData: api.CheckoutModel = { } as api.CheckoutModel; // Bound to UI via the controller
        // Functions
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `InvoicePaymentMethod.validate(cart: "${cart && cart.TypeName}")`;
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
            const debugMsg = `InvoicePaymentMethod.submit(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            if (!this.name) {
                this.consoleDebug(`${debugMsg} no name yet`);
                return this.$q.reject(`${debugMsg} no name yet`);
            }
            const dataToUse = this.paymentData;
            if (this.paymentData.PayByBillMeLater
                && this.paymentData.PayByBillMeLater["StoredFiles"] // Manually assigned by the Upload Widget
                && this.paymentData.PayByBillMeLater["StoredFiles"].length) {
                if (!this.cvPurchaseService.fileNames[cart.TypeName]) {
                    this.cvPurchaseService.fileNames[cart.TypeName] = [];
                }
                const files = this.paymentData.PayByBillMeLater["StoredFiles"] as api.StoredFileModel[];
                const names = files.map(x => x.FileName);
                this.cvPurchaseService.fileNames[cart.TypeName].push(...names);
            }
            this.consoleDebug(`${debugMsg} resulting payment data:`);
            this.consoleDebug(dataToUse);
            return this.$q.resolve(this.cvPurchaseService.finalize(cart.TypeName, dataToUse));
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig);
            this.consoleDebug(`InvoicePaymentMethod.ctor()`);
        }
    }
}
