/**
 * @file framework/admin/purchasing/steps/completed/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.completed {
    // This is the controller for the directive
    class PurchaseStepCompletedBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected lookupKey: api.CartByIDLookupKey;
        // Properties
        get name(): string { return this.cvServiceStrings.checkout.steps.completed; }
        private get step(): IPurchaseStep {
            if (!this.lookupKey
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.lookupKey.toString()]) {
                return null; // Not available (yet)
            }
            return _.find(this.cvPurchaseService.steps[this.lookupKey.toString()],
                x => x.name === this.name);
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
            this.consoleDebug(`PurchaseStepCompletedBodyController.ctor()`);
        }
    }

    adminApp.directive("cefPurchaseStepCompletedBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/completed/body.html", "ui"),
        controller: PurchaseStepCompletedBodyController,
        controllerAs: "pscbCtrl",
        bindToController: true
    }));
}
