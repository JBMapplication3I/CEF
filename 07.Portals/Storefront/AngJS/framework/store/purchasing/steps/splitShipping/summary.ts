/**
 * @file framework/store/purchasing/steps/splitShipping/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.shipping {
    // This is the controller for the directive
    class PurchaseStepSplitShippingSummaryController extends core.TemplatedControllerBase {
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
                x => x.name === this.cvServiceStrings.checkout.steps.splitShipping);
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

    cefApp.directive("cefPurchaseStepSplitShippingSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/splitShipping/summary.html", "ui"),
        controller: PurchaseStepSplitShippingSummaryController,
        controllerAs: "pssssCtrl",
        bindToController: true
    }));
}
