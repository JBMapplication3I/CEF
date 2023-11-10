/**
 * @file framework/store/quotes/submitQuote.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submits items in the quote cart
 */
module cef.store.quotes {
    class SubmitQuoteController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        cartType: string;
        // Properties
        // <None>
        // Convenience Properties (reduces HTML size)
        get service(): services.ISubmitQuoteService {
            return this && this.cvSubmitQuoteService;
        }
        // Functions
        protected goToStep(stepIndex: number): void {
            this.consoleDebug(`SubmitQuoteController.goToStep(stepIndex: ${stepIndex})`);
            this.setRunning();
            this.cvSubmitQuoteService.activateStep(this.cartType, stepIndex).then(success => {
                if (!success) {
                    this.finishRunning(true,
                        `Activating step ${stepIndex} for ${this.cartType} failed`);
                    return;
                }
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        protected submit(cartType: string, step: steps.ISubmitQuoteStep): void {
            const debug = `SubmitQuoteController.submit(cartType: "${cartType
                }", step: "${step && step.name}")`;
            this.consoleDebug(debug);
            this.setRunning();
            step.submit(cartType).then(success => {
                if (!success) {
                    this.consoleDebug(`${debug} An error occurred in step submit, it was not successful`);
                    this.finishRunning(
                        true,
                        "An error occurred in step submit, it was not successful");
                    return;
                }
                this.consoleDebug(`${debug} Submit current step succeeded, Going to next step`);
                step.complete = true;
                this.goToStep(this.cvSubmitQuoteService.activeStep[this.cartType] + 1);
            }).catch(reason => this.finishRunning(
                true,
                reason || "An error occurred in step submit, it was not successful"));
        }
        // Events
        // <None>
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSubmitQuoteService: services.ISubmitQuoteService) {
            super(cefConfig);
            this.consoleDebug(`SubmitQuoteController.ctor()`);
            if (!this.cartType) {
                this.cartType = cvServiceStrings.carts.types.cart;
            }
            this.setRunning();
            this.cvSubmitQuoteService.building[this.cartType] = true;
            this.cvSubmitQuoteService.initialize(this.cartType)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
            $rootScope.getCartType = () => this.cartType;
            $(window).bind("beforeunload", function() {
                // Clear the target carts so they can't cause issues with subsequent runs
                cvSubmitQuoteService.clearAnalysis($rootScope.getCartType()).then(() => { /* Do Nothing */ });
                return undefined;
            });
        }
    }

    cefApp.directive("cefSubmitQuote", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/quotes/submitQuote.html", "ui"),
        controller: SubmitQuoteController,
        controllerAs: "sqCtrl",
        bindToController: true
    }));
}
