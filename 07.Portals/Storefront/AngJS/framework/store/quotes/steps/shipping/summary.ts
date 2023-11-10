/**
 * @file framework/store/quotes/steps/shipping/summary.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote items in the quote cart: The steps of the wizard
 */
module cef.store.quotes.steps.shipping {
    // This is the controller for the directive
    class SubmitQuoteStepShippingSummaryController extends core.TemplatedControllerBase {
        // Properties
        protected cartType: string; // Bound by Scope
        private get step(): SubmitQuoteStepShipping {
            if (!this.cartType
                || !this.cvSubmitQuoteService
                || !this.cvSubmitQuoteService.steps
                || !this.cvSubmitQuoteService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return <SubmitQuoteStepShipping>
                _.find(this.cvSubmitQuoteService.steps[this.cartType],
                    x => x.name === this.cvServiceStrings.submitQuote.steps.shipping);
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
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSubmitQuoteService: services.ISubmitQuoteService,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefSubmitQuoteStepShippingSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/quotes/steps/shipping/summary.html", "ui"),
        controller: SubmitQuoteStepShippingSummaryController,
        controllerAs: "sqsssCtrl",
        bindToController: true
    }));
}
