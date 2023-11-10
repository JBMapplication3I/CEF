/**
 * @file framework/store/purchasing/steps/payment/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment {
    // This is the controller for the directive
    class PurchaseStepPaymentSummaryController extends core.TemplatedControllerBase {
        // Properties
        protected cartType: string; // Bound by Scope
        private get step(): PurchaseStepPayment {
            if (!this.cartType
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepPayment>
                _.find(this.cvPurchaseService.steps[this.cartType],
                    x => x.name === this.cvServiceStrings.checkout.steps.payment);
        }
        protected get paymentMethod(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.paymentMethod[this.cartType];
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
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.consoleDebug(`PurchaseStepPaymentSummaryController.ctor()`);
        }
    }

    cefApp.directive("cefPurchaseStepPaymentSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/summary.html", "ui"),
        controller: PurchaseStepPaymentSummaryController,
        controllerAs: "pspsCtrl",
        bindToController: true
    }));
}
