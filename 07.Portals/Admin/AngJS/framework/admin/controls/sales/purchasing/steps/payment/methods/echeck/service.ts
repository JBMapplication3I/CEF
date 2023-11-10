/**
 * @file framework/admin/purchasing/steps/payment/methods/echeck/service.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.echeck {
    export class EcheckPaymentMethod extends PaymentMethodBase {
        // Properties
        get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.echeck;
        }
        get stepName(): string {
            return this.cvServiceStrings.checkout.steps.payment;
        }
        selectedEcheck: api.WalletModel = null; // Bound to UI via the controller
        walletTitle: string = null; // Bound to UI via the controller
        selectedEcheckIsFromWallet(): boolean {
            return this.selectedEcheck &&
                this.selectedEcheck.ID > 0;
        }
        wallet: api.WalletModel[] = []; // Bound on wallet load event
        paymentData: api.CheckoutModel = { } as api.CheckoutModel; // Bound to UI via the controller
        // Functions
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `EcheckPaymentMethod.validate(cart: "${cart && cart.TypeName}")`;
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
            const debugMsg = `EcheckPaymentMethod.submit(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            // TODO: Validate the payment data entered
            if (!this.name) {
                this.consoleDebug(`${debugMsg} no name yet`);
                return this.$q.reject(`${debugMsg} no name yet`);
            }
            const dataToUse = { } as api.CheckoutModel;
            if (this.cefConfig.featureSet.payments.wallet.enabled &&
                this.cvAuthenticationService.isAuthenticated() &&
                this.selectedEcheckIsFromWallet) {
                if (!dataToUse.PayByWalletEntry) {
                    dataToUse.PayByWalletEntry = <api.CheckoutPayByWalletEntry>{ };
                }
                dataToUse.PayByWalletEntry.WalletID = this.selectedEcheck.ID;
            } else if (this.paymentData.PayByECheck && this.paymentData.PayByECheck.AccountNumber) {
                if (!dataToUse.PayByECheck) {
                    dataToUse.PayByECheck = <api.CheckoutPayByECheck>{ };
                }
                dataToUse.PayByECheck.AccountHolderName = this.paymentData.PayByECheck.AccountHolderName;
                dataToUse.PayByECheck.AccountNumber = this.paymentData.PayByECheck.AccountNumber;
                dataToUse.PayByECheck.RoutingNumber = this.paymentData.PayByECheck.RoutingNumber;
                dataToUse.PayByECheck.BankName = this.paymentData.PayByECheck.BankName;
                dataToUse.PayByECheck.AccountType = this.paymentData.PayByECheck.AccountType;
            } else {
                throw new Error("Couldn't determine payment kind");
            }
            this.consoleDebug(`${debugMsg} resulting payment data:`);
            this.consoleDebug(dataToUse);
            return this.$q.resolve(this.cvPurchaseService.finalize(api.CartByIDLookupKey.newFromCart(cart), dataToUse));
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvWalletService: services.IWalletService,
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig);
            this.consoleDebug(`EcheckPaymentMethod.ctor()`);
        }
    }
}
