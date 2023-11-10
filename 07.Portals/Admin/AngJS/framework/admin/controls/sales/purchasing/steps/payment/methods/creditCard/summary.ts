/**
 * @file framework/admin/purchasing/steps/payment/methods/creditCard/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.creditCard {
    class CreditCardPaymentMethodSummaryController extends core.TemplatedControllerBase {
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
        protected get paymentData(): api.CheckoutModel {
            if (!this.method) {
                return undefined;
            }
            return this.method.paymentData;
        }
        protected get selectedCard(): api.WalletModel {
            if (!this.method) {
                return undefined;
            }
            return this.method.selectedCard;
        }
        protected get selectedCardIsFromWallet(): boolean {
            if (!this.method) {
                return undefined;
            }
            return this.method.selectedCardIsFromWallet();
        }
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $rootScope: ng.IRootScopeService,
                readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvPurchaseService: services.IPurchaseService) {
            super(cefConfig);
            this.consoleDebug(`CreditCardPaymentMethodSummaryController.ctor()`);
        }
    }

    adminApp.directive("cefPaymentMethodCreditCardSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/payment/methods/creditCard/summary.html", "ui"),
        controller: CreditCardPaymentMethodSummaryController,
        controllerAs: "pmccsCtrl",
        bindToController: true
    }));
}
