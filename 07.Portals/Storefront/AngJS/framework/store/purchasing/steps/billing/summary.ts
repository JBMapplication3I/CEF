/**
 * @file framework/store/purchasing/steps/billing/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.billing {
    // This is the controller for the directive
    class PurchaseStepBillingSummaryController extends core.TemplatedControllerBase {
        // Properties
        protected cartType: string; // Bound by Scope
        private get step(): PurchaseStepBilling {
            if (!this.cartType ||
                !this.cvPurchaseService ||
                !this.cvPurchaseService.steps ||
                !this.cvPurchaseService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepBilling>
                _.find(this.cvPurchaseService.steps[this.cartType],
                    x => x.name === this.cvServiceStrings.checkout.steps.billing);
        }
        protected get currentAccountContact(): api.AccountContactModel {
            return this.step && this.step.currentAccountContact;
        }
        protected get addressTitle(): string {
            return this.step && this.step.addressTitle;
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
            this.consoleDebug(`PurchaseStepBillingSummaryController.ctor()`);
        }
    }

    cefApp.directive("cefPurchaseStepBillingSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/billing/summary.html", "ui"),
        controller: PurchaseStepBillingSummaryController,
        controllerAs: "psbsCtrl",
        bindToController: true
    }));
}
