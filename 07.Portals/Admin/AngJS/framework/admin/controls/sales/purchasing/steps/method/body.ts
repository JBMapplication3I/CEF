/**
 * @file framework/admin/purchasing/steps/method/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.method {
    // This is the controller for the directive
    class PurchaseStepMethodBodyController extends core.TemplatedControllerBase {
        // Bound by Scope Properties
        protected lookupKey: api.CartByIDLookupKey;
        // Properties
        private get step(): IPurchaseStep {
            if (!this.lookupKey
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.lookupKey.toString()]) {
                return null; // Not available (yet)
            }
            return _.find(this.cvPurchaseService.steps[this.lookupKey.toString()],
                x => x.name === this.cvServiceStrings.checkout.steps.method);
        }
        protected get allowLogin(): boolean {
            return !this.cvAuthenticationService.isAuthenticated() &&
                this.cefConfig.featureSet.login.enabled === true;
        }
        protected get allowGuest(): boolean {
            return !this.cvAuthenticationService.isAuthenticated() &&
                (this.cefConfig.featureSet.login.enabled === false ||
                 this.cefConfig.purchase.allowGuest === true);
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
            this.cvPurchaseService.activateStep(this.lookupKey, this.step.index + 1);
        };
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvPurchaseService: services.IPurchaseService,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.checkout.nextStep,
                this.onNextStep);
            const unbind2 = $scope.$on(cvServiceStrings.events.auth.signIn,
                ($event: ng.IAngularEvent) => {
                    if (this.cvPurchaseService.activeStep[this.lookupKey.toString()] === this.step.index) {
                        this.onNextStep($event, this.step.name);
                    }
                });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
    }

    adminApp.directive("cefPurchaseStepMethodBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/method/body.html", "ui"),
        controller: PurchaseStepMethodBodyController,
        controllerAs: "psmbCtrl",
        bindToController: true
    }));
}
