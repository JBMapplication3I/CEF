/**
 * @file framework/admin/purchasing/steps/completed/api.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.completed {
    // This is part of the Service
    export class PurchaseStepCompleted extends PurchaseStep {
        // Properties
        get name(): string { return this.cvServiceStrings.checkout.steps.completed; }
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
            this.consoleDebug(`PurchaseStepCompleted.ctor()`);
        }
    }
}
