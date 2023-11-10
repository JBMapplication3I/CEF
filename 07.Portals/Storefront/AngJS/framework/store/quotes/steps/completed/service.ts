/**
 * @file framework/store/quotes/steps/completed/api.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote items in the quote cart: The steps of the wizard
 */
module cef.store.quotes.steps.completed {
    // This is part of the Service
    export class SubmitQuoteStepCompleted extends SubmitQuoteStep {
        // Properties
        get name(): string { return this.cvServiceStrings.submitQuote.steps.completed; }
        // Functions
        // canEnable override not required
        // initialize override not required
        // validate override not required
        // submit override not required
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`SubmitQuoteStepCompleted.ctor()`);
        }
    }
}
