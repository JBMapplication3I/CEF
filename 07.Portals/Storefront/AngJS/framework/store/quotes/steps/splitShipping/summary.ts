/**
 * @file framework/store/quotes/steps/splitShipping/summary.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote items in the quote cart: The steps of the wizard
 */
module cef.store.quotes.steps.shipping {
    // This is the controller for the directive
    class SubmitQuoteStepSplitShippingSummaryController extends core.TemplatedControllerBase {
        // Properties
        protected cartType: string; // Bound by Scope
        private get step(): ISubmitQuoteStep {
            if (!this.cartType
                || !this.cvSubmitQuoteService
                || !this.cvSubmitQuoteService.steps
                || !this.cvSubmitQuoteService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return _.find(this.cvSubmitQuoteService.steps[this.cartType],
                x => x.name === this.cvServiceStrings.submitQuote.steps.splitShipping);
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

    cefApp.directive("cefSubmitQuoteStepSplitShippingSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/quotes/steps/splitShipping/summary.html", "ui"),
        controller: SubmitQuoteStepSplitShippingSummaryController,
        controllerAs: "sqssssCtrl",
        bindToController: true
    }));
}
