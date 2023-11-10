/**
 * @file framework/store/purchasing/steps/payment/methods/echeck/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.echeck {
    class EcheckPaymentMethodController extends core.TemplatedControllerBase {
        // Properties
        private get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.creditCard;
        }
        protected cartType: string; // Bound by Scope
        private get step(): PurchaseStepPayment {
            if (!this.cartType ||
                !this.cvPurchaseService ||
                !this.cvPurchaseService.steps ||
                !this.cvPurchaseService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepPayment>
                _.find(this.cvPurchaseService.steps[this.cartType],
                    x => x.name === this.cvServiceStrings.checkout.steps.payment);
        }
        private get method(): EcheckPaymentMethod {
            if (!this.step ||
                !this.step.paymentMethods ||
                !this.step.paymentMethods[this.cartType] ||
                !this.step.paymentMethods[this.cartType][this.name]) {
                return null; // Not available (yet)
            }
            return <EcheckPaymentMethod>
                this.step.paymentMethods[this.cartType][this.name];
        }
        tempPaymentData: api.CheckoutModel = { } as api.CheckoutModel;
        protected get paymentData(): api.CheckoutModel {
            if (!this.method) {
                return undefined;
            }
            return this.method.paymentData;
        }
        protected set paymentData(newValue: api.CheckoutModel) {
            this.consoleDebug(`EcheckPaymentMethodController.paymentData.set`);
            if (this.method) {
                this.consoleDebug(`EcheckPaymentMethodController.paymentData.set 2`);
                this.method.paymentData = newValue;
            }
        }
        protected get selectedEcheck(): api.WalletModel {
            if (!this.method) {
                return undefined;
            }
            return this.method.selectedEcheck;
        }
        protected set selectedEcheck(newValue: api.WalletModel) {
            this.consoleDebug(`EcheckPaymentMethodController.selectedEcheck.set`);
            if (this.method) {
                this.consoleDebug(`EcheckPaymentMethodController.selectedEcheck.set 2`);
                this.method.selectedEcheck = newValue;
            }
        }
        protected get selectedEcheckIsFromWallet(): boolean {
            if (!this.method) {
                return undefined;
            }
            return this.method.selectedEcheckIsFromWallet();
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
            this.consoleDebug(`EcheckPaymentMethodController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`EcheckPaymentMethodController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        // Function
        private updateCartWalletSelection(): void {
            this.consoleDebug(`EcheckPaymentMethodController.updateCartWalletSelection()`);
            this.cvCartService.applyBillingContact(this.cartType);
        }
        // Events
        // NOTE: This must remain an arrow function for angular events
        private readWallet = (): void => {
            this.consoleDebug(`EcheckPaymentMethodController.readWallet()`);
            if (!this.cefConfig.featureSet.payments.wallet.enabled) { return; }
            this.cvWalletService.getWallet().then(wallet => {
                this.wallet = wallet;
                this.onChange();
            });
        }
        onWalletItemSelectionChange(): void {
            this.consoleDebug(`EcheckPaymentMethodController.onWalletItemSelectionChange()`);
            this.updateCartWalletSelection();
        }
        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            this.consoleDebug(`EcheckPaymentMethodController.onChange()`);
            this.invalid = true;
            if (this.forms["Card"].$invalid) {
                this.consoleDebug(`EcheckPaymentMethodController.onChange() failed!`);
                this.invalid = true;
                return;
            }
            this.paymentData = this.tempPaymentData;
            this.setRunning();
            this.step.validate(this.cvCartService.accessCart(this.cartType))
                .then(success => {
                    if (!success) {
                        this.consoleDebug(`EcheckPaymentMethodController.onChange() failed!`);
                        this.invalid = true;
                        this.finishRunning(true, "An error has occurred");
                        return;
                    }
                    this.consoleDebug(`EcheckPaymentMethodController.onChange() success!`);
                    this.invalid = false;
                    this.finishRunning();
                }, result => {
                    this.consoleDebug(`EcheckPaymentMethodController.onChange() failed!`);
                    this.invalid = true;
                    this.finishRunning(true, result || "An error has occurred");
                }).catch(reason => {
                    this.consoleDebug(`EcheckPaymentMethodController.onChange() failed!`);
                    this.invalid = true;
                    this.finishRunning(true, reason || "An error has occurred");
                });
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $rootScope: ng.IRootScopeService,
                readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvCartService: services.ICartService,
                private readonly cvPurchaseService: services.IPurchaseService,
                private readonly cvWalletService: services.IWalletService) {
            super(cefConfig);
            this.consoleDebug(`EcheckPaymentMethodController.ctor()`);
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

    cefApp.directive("cefPaymentMethodEcheckBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/echeck/body.html", "ui"),
        controller: EcheckPaymentMethodController,
        controllerAs: "pmebCtrl",
        bindToController: true
    }));
}
