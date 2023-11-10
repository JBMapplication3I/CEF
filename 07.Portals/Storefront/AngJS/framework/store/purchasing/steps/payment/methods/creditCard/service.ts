/**
 * @file framework/store/purchasing/steps/payment/methods/creditCard/service.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.creditCard {
    export class CreditCardPaymentMethod extends PaymentMethodBase {
        // Properties
        get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.creditCard;
        }
        get stepName(): string {
            return this.cvServiceStrings.checkout.steps.payment;
        }
        selectedCard: api.WalletModel = null; // Bound to UI via the controller
        walletTitle: string = null; // Bound to UI via the controller
        selectedCardIsFromWallet(): boolean {
            return this.selectedCard
                && this.selectedCard.ID > 0;
        }
        wallet: api.WalletModel[] = []; // Bound on wallet load event
        paymentData: api.CheckoutModel = { } as api.CheckoutModel; // Bound to UI via the controller
        // Functions
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            const debugMsg = `CreditCardPaymentMethod.validate(cart: "${cart && cart.TypeName}")`;
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
            const debugMsg = `CreditCardPaymentMethod.submit(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debugMsg);
            if (!this.name) {
                this.consoleDebug(`${debugMsg} no name yet`);
                return this.$q.reject(`${debugMsg} no name yet`);
            }
            const dataToUse = { } as api.CheckoutModel;
            if (this.cefConfig.featureSet.payments.wallet.enabled
                && this.cvAuthenticationService.isAuthenticated()
                && this.selectedCardIsFromWallet()) {
                if (!dataToUse.PayByWalletEntry) {
                    dataToUse.PayByWalletEntry = <api.CheckoutPayByWalletEntry>{ };
                }
                dataToUse.PayByWalletEntry.WalletID = this.selectedCard.ID;
                dataToUse.PayByWalletEntry.WalletCVV = this.selectedCard["CVV"]; // Manually assigned by the UI
            } else if (this.paymentData.PayByCreditCard && this.paymentData.PayByCreditCard.CVV) {
                if (!dataToUse.PayByCreditCard) {
                    dataToUse.PayByCreditCard = <api.CheckoutPayByCreditCard>{ };
                }
                dataToUse.PayByCreditCard.CardType = this.paymentData.PayByCreditCard.CardType;
                dataToUse.PayByCreditCard.CardReferenceName = this.paymentData.PayByCreditCard.CardReferenceName;
                dataToUse.PayByCreditCard.CardHolderName = this.paymentData.PayByCreditCard.CardHolderName;
                dataToUse.PayByCreditCard.CardNumber = this.paymentData.PayByCreditCard.CardNumber;
                dataToUse.PayByCreditCard.ExpirationMonth = this.paymentData.PayByCreditCard.ExpirationMonth;
                dataToUse.PayByCreditCard.ExpirationYear = this.paymentData.PayByCreditCard.ExpirationYear;
                dataToUse.PayByCreditCard.CVV = this.paymentData.PayByCreditCard.CVV;
            } else {
                throw new Error("Couldn't determine payment kind");
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
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvWalletService: services.IWalletService,
                protected readonly cvPurchaseService: services.IPurchaseService) {
            super($q, cefConfig);
            this.consoleDebug(`CreditCardPaymentMethod.ctor()`);
        }
    }
}
