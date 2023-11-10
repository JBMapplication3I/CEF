/**
 * @file framework/admin/purchasing/steps/confirmation/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.confirmation {
    // This is the controller for the directive
    class PurchaseStepConfirmationSummaryController extends core.TemplatedControllerBase {
        // Bound by Scope Properties
        protected lookupKey: api.CartByIDLookupKey;
        // Properties
        private get step(): IPurchaseStep {
            if (!this.lookupKey
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.lookupKey.toString()]) {
                return null; // Not available (yet)
            }
            return _.find(this.cvPurchaseService.steps[this.lookupKey.toString()],
                x => x.name === this.cvServiceStrings.checkout.steps.confirmation);
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
        }
    }

    adminApp.directive("cefPurchaseStepConfirmationSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/confirmation/summary.html", "ui"),
        controller: PurchaseStepConfirmationSummaryController,
        controllerAs: "pscsCtrl",
        bindToController: true
    }));
}
