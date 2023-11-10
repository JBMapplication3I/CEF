/**
 * @file framework/admin/purchasing/steps/payment/methods/creditCard/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.creditCard {
    class CreditCardPaymentMethodBodyController extends core.TemplatedControllerBase {
        // Properties
        private get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.creditCard;
        }
        protected lookupKey: api.CartByIDLookupKey; // Bound by Scope
        private get step(): PurchaseStepPayment {
            if (!this.lookupKey
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.lookupKey.toString()]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepPayment>
                _.find(this.cvPurchaseService.steps[this.lookupKey.toString()],
                    x => x.name === this.cvServiceStrings.checkout.steps.payment);
        }
        private get method(): CreditCardPaymentMethod {
            if (!this.step
                || !this.step.paymentMethods
                || !this.step.paymentMethods[this.lookupKey.toString()]
                || !this.step.paymentMethods[this.lookupKey.toString()][this.name]) {
                return null; // Not available (yet)
            }
            return <CreditCardPaymentMethod>
                this.step.paymentMethods[this.lookupKey.toString()][this.name];
        }
        private lastNewEntryNickname: string;
        tempPaymentData: api.CheckoutModel = { } as api.CheckoutModel;
        protected get paymentData(): api.CheckoutModel {
            if (!this.method) {
                return undefined;
            }
            return this.method.paymentData;
        }
        protected set paymentData(newValue: api.CheckoutModel) {
            this.consoleDebug(`CreditCardPaymentMethodBodyController.paymentData.set`);
            if (this.method) {
                this.consoleDebug(`CreditCardPaymentMethodBodyController.paymentData.set 2`);
                this.method.paymentData = newValue;
            }
        }
        protected get selectedCard(): api.WalletModel {
            if (!this.method) {
                return undefined;
            }
            return this.method.selectedCard;
        }
        protected set selectedCard(newValue: api.WalletModel) {
            this.consoleDebug(`CreditCardPaymentMethodBodyController.selectedCard.set`);
            if (this.method) {
                this.consoleDebug(`CreditCardPaymentMethodBodyController.selectedCard.set 2`);
                this.method.selectedCard = newValue;
            }
        }
        protected get selectedCardIsFromWallet(): boolean {
            if (!this.method) {
                return undefined;
            }
            return this.method.selectedCardIsFromWallet();
        }
        protected get wallet(): api.WalletModel[] {
            if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                return undefined;
            }
            if (!this.method) {
                return undefined;
            }
            return this.method.wallet;
        }
        protected set wallet(newValue: api.WalletModel[]) {
            if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                return;
            }
            if (!this.method) {
                return;
            }
            this.method.wallet = newValue;
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`CreditCardPaymentMethodBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`CreditCardPaymentMethodBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        // Functions
        private updateCartWalletSelection(): void {
            this.consoleDebug(`CreditCardPaymentMethodBodyController.updateCartWalletSelection()`);
            this.cvCartService.applyBillingContact(this.lookupKey);
        }
        private add(): void {
            this.cvWalletModalFactory(
                this.$translate("ui.admin.wallet.AddWalletEntry"), // Title
                this.$translate("ui.admin.common.Add"), // Button
                this.lookupKey.UID,
                null, // Callback
                "PaymentPane", // id indexer
                null // existing
            ).then(newEntryToBeAdded => {
                this.lastNewEntryNickname = newEntryToBeAdded.Name;
                this.setRunning();
                this.cvWalletService.addEntry(this.lookupKey.AID, newEntryToBeAdded).then(added => {
                    this.selectedCard = newEntryToBeAdded;
                    this.finishRunning();
                    this.readWallet();
                }).catch(error => {
                    this.finishRunning(true, error);
                });
            });
        }
        // Events
        // NOTE: This must remain an arrow function for angular events
        private readWallet = (): void => {
            this.consoleDebug(`CreditCardPaymentMethodBodyController.readWallet()`);
            if (!this.cefConfig.featureSet.payments.wallet.enabled) {
                return;
            }
            this.cvWalletService.getWallet(this.lookupKey.UID).then(wallet => {
                this.wallet = wallet;
                // If we already have something selected, ensure it stays selected if we don't need to override it
                let selectedEntry = this.selectedCard;
                // If we just added a card, select it automatically
                if (this.lastNewEntryNickname) {
                    const found = _.find(this.wallet, x => x.Name === this.lastNewEntryNickname);
                    this.lastNewEntryNickname = null;
                    if (found) {
                        selectedEntry = found;
                    }
                }
                // If we have a default entry and nothing is currently selected, select it automatically
                if (!selectedEntry) {
                    const found = _.find(this.wallet, x => x.IsDefault);
                    if (found) {
                        selectedEntry = found;
                    } else if (this.wallet.length > 0) {
                        // Select the first entry if no default available but there are entries
                        selectedEntry = this.wallet[0];
                    }
                }
                if (selectedEntry !== this.selectedCard) {
                    this.selectedCard = selectedEntry;
                }
                this.onChange();
            });
        }
        onWalletItemSelectionChange(): void {
            this.consoleDebug(`CreditCardPaymentMethodBodyController.onWalletItemSelectionChange()`);
            this.updateCartWalletSelection();
        }
        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            this.consoleDebug(`CreditCardPaymentMethodBodyController.onChange()`);
            this.$timeout(() => {
                if (!this.step) {
                    // Try again in a moment
                    this.onChange();
                    return;
                }
                this.invalid = true;
                if (this.forms["card"].$invalid) {
                    this.consoleDebug(`CreditCardPaymentMethodBodyController.onChange() failed!`);
                    this.invalid = true;
                    return;
                }
                this.paymentData = this.tempPaymentData;
                this.setRunning();
                this.step.validate(this.cvCartService.accessCart(this.lookupKey)).then(success => {
                    if (!success) {
                        this.consoleDebug(`CreditCardPaymentMethodBodyController.onChange() failed!`);
                        this.invalid = true;
                        this.finishRunning(true, "An error has occurred");
                        return;
                    }
                    this.consoleDebug(`CreditCardPaymentMethodBodyController.onChange() success!`);
                    this.invalid = false;
                    this.finishRunning();
                }).catch(reason => {
                    this.consoleDebug(`CreditCardPaymentMethodBodyController.onChange() failed!`);
                    this.invalid = true;
                    this.finishRunning(true, reason || "An error has occurred");
                });
            }, 250);
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $rootScope: ng.IRootScopeService,
                private readonly $translate: ng.translate.ITranslateService,
                readonly $timeout: ng.ITimeoutService,
                readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvCartService: services.ICartService,
                private readonly cvPurchaseService: services.IPurchaseService,
                private readonly cvWalletService: services.IWalletService,
                private readonly cvWalletModalFactory: modals.IWalletModalFactory) {
            super(cefConfig);
            this.consoleDebug(`CreditCardPaymentMethodBodyController.ctor()`);
            if (cefConfig.featureSet.payments.wallet.enabled) {
                const unbind1 = $scope.$on(cvServiceStrings.events.wallet.loaded, this.readWallet);
                $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                    if (angular.isFunction(unbind1)) { unbind1(); }
                });
                // Do an initial load of data
                this.readWallet();
            }
        }
    }

    adminApp.directive("cefPaymentMethodCreditCardBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/payment/methods/creditCard/body.html", "ui"),
        controller: CreditCardPaymentMethodBodyController,
        controllerAs: "pmccbCtrl",
        bindToController: true
    }));
}
