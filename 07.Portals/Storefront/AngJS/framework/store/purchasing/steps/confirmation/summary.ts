/**
 * @file framework/store/purchasing/steps/confirmation/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.confirmation {
    // This is the controller for the directive
    class PurchaseStepConfirmationSummaryController extends core.TemplatedControllerBase {
        // Properties
        protected cartType: string; // Bound by Scope
        private get step(): IPurchaseStep {
            if (!this.cartType
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return _.find(this.cvPurchaseService.steps[this.cartType],
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

    cefApp.directive("cefPurchaseStepConfirmationSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/confirmation/summary.html", "ui"),
        controller: PurchaseStepConfirmationSummaryController,
        controllerAs: "pscsCtrl",
        bindToController: true
    }));
}
