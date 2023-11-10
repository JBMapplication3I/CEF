/**
 * @file framework/store/quotes/steps/method/body.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote items in the quote cart: The steps of the wizard
 */
module cef.store.quotes.steps.method {
    // This is the controller for the directive
    class SubmitQuoteStepMethodBodyController extends core.TemplatedControllerBase {
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
                x => x.name === this.cvServiceStrings.submitQuote.steps.method);
        }
        protected get allowLogin(): boolean {
            return !this.cvAuthenticationService.isAuthenticated() &&
                this.cefConfig.featureSet.login.enabled === true;
        }
        protected get allowGuest(): boolean {
            return !this.cvAuthenticationService.isAuthenticated() &&
                (this.cefConfig.featureSet.login.enabled === false ||
                 this.cefConfig.submitQuote.allowGuest === true);
        }
        // Functions
        protected loginCallback(success: boolean): void {
            if (success) {
                this.nextStep();
            }
        }
        protected nextStep(): void {
            this.onNextStep(null, this.step.name);
        }
        // Events
        // NOTE: Must remain an arrow function for angular events
        private onNextStep = (__: ng.IAngularEvent, currentStepName: string): void => {
            if (currentStepName !== this.step.name) {
                return;
            }
            this.step.complete = true;
            this.cvSubmitQuoteService.activateStep(this.cartType, this.step.index + 1);
        };
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSubmitQuoteService: services.ISubmitQuoteService,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.checkout.nextStep,
                this.onNextStep);
            const unbind2 = $scope.$on(cvServiceStrings.events.auth.signIn,
                ($event: ng.IAngularEvent) => {
                    if (this.cvSubmitQuoteService.activeStep[this.cartType] === this.step.index) {
                        this.onNextStep($event, this.step.name);
                    }
                });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
    }

    cefApp.directive("cefSubmitQuoteStepMethodBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/quotes/steps/method/body.html", "ui"),
        controller: SubmitQuoteStepMethodBodyController,
        controllerAs: "sqsmbCtrl",
        bindToController: true
    }));
}
