/**
 * @file framework/admin/purchasing/steps/billing/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.billing {
    // This is the controller for the directive
    class PurchaseStepBillingSummaryController extends core.TemplatedControllerBase {
        // Bound by Scope Properties
        protected lookupKey: api.CartByIDLookupKey;
        // Properties
        private get step(): PurchaseStepBilling {
            if (!this.lookupKey
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.lookupKey.toString()]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepBilling>
                _.find(this.cvPurchaseService.steps[this.lookupKey.toString()],
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
                private readonly cvAuthenticationService: services.IAuthenticationService) { // Used by UI
            super(cefConfig);
            this.consoleDebug(`PurchaseStepBillingSummaryController.ctor()`);
        }
    }

    adminApp.directive("cefPurchaseStepBillingSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/billing/summary.html", "ui"),
        controller: PurchaseStepBillingSummaryController,
        controllerAs: "psbsCtrl",
        bindToController: true
    }));
}
