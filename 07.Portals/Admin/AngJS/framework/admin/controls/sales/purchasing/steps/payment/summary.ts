/**
 * @file framework/admin/purchasing/steps/payment/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment {
    // This is the controller for the directive
    class PurchaseStepPaymentSummaryController extends core.TemplatedControllerBase {
        // Bound by Scope Properties
        protected lookupKey: api.CartByIDLookupKey;
        // Properties
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
        protected get paymentMethod(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.paymentMethod[this.lookupKey.toString()];
        }
        // Convenience Properties (reduces HTML size)
        get paymentMethods() {
            return this && this.cvServiceStrings.checkout.paymentMethods;
        }
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvPurchaseService: services.IPurchaseService,
                private readonly cvAuthenticationService: services.IAuthenticationService) { // Used by UI
            super(cefConfig);
            this.consoleDebug(`PurchaseStepPaymentSummaryController.ctor()`);
        }
    }

    adminApp.directive("cefPurchaseStepPaymentSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/payment/summary.html", "ui"),
        controller: PurchaseStepPaymentSummaryController,
        controllerAs: "pspsCtrl",
        bindToController: true
    }));
}
