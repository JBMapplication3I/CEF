/**
 * @file framework/store/purchasing/steps/shipping/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.shipping {
    // This is the controller for the directive
    class PurchaseStepShippingSummaryController extends core.TemplatedControllerBase {
        // Properties
        protected cartType: string; // Bound by Scope
        private get step(): PurchaseStepShipping {
            if (!this.cartType
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepShipping>
                _.find(this.cvPurchaseService.steps[this.cartType],
                    x => x.name === this.cvServiceStrings.checkout.steps.shipping);
        }
        protected get currentAccountContact(): api.AccountContactModel {
            if (!this.step) {
                return undefined;
            }
            return this.step.currentAccountContact;
        }
        protected get addressTitle(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.addressTitle;
        }
        protected get sameAsBilling(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.sameAsBilling;
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

    cefApp.directive("cefPurchaseStepShippingSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/shipping/summary.html", "ui"),
        controller: PurchaseStepShippingSummaryController,
        controllerAs: "psssCtrl",
        bindToController: true
    }));
}
