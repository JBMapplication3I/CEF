/**
 * @file framework/admin/purchasing/steps/payment/methods/storeCredit/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.storeCredit {
    class StoreCreditPaymentMethodController extends core.TemplatedControllerBase {
        // Properties
        private get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.storeCredit;
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
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            if (this.step) {
                this.step.invalid = newValue;
            }
        }
        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            this.invalid = false;
        }
        // Constructor
        constructor(
            readonly $scope: ng.IScope,
            readonly $rootScope: ng.IRootScopeService,
            readonly cvServiceStrings: services.IServiceStrings,
            protected readonly cefConfig: core.CefConfig, // Used by UI
            private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
            private readonly cvCartService: services.ICartService,
            private readonly cvPurchaseService: services.IPurchaseService) {
            super(cefConfig);
            this.onChange();
        }
    }

    adminApp.directive("cefPaymentMethodStoreCreditBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/payment/methods/storeCredit/body.html", "ui"),
        controller: StoreCreditPaymentMethodController,
        controllerAs: "pmscbCtrl",
        bindToController: true
    }));
}
