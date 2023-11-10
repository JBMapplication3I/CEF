/**
 * @file framework/store/quotes/steps/completed/body.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote items in the quote cart: The steps of the wizard
 */
module cef.store.quotes.steps.completed {
    // This is the controller for the directive
    class SubmitQuoteStepCompletedBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected cartType: string;
        // Properties
        get name(): string { return this.cvServiceStrings.submitQuote.steps.completed; }
        private get step(): ISubmitQuoteStep {
            if (!this.cartType
                || !this.cvSubmitQuoteService
                || !this.cvSubmitQuoteService.steps
                || !this.cvSubmitQuoteService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return _.find(this.cvSubmitQuoteService.steps[this.cartType],
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
                private readonly cvSubmitQuoteService: services.ISubmitQuoteService,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.consoleDebug(`SubmitQuoteStepCompletedBodyController.ctor()`);
        }
    }

    cefApp.directive("cefSubmitQuoteStepCompletedBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/quotes/steps/completed/body.html", "ui"),
        controller: SubmitQuoteStepCompletedBodyController,
        controllerAs: "sqscbCtrl",
        bindToController: true
    }));
}
