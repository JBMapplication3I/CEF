/**
 * @file framework/store/user/registration/steps/completed/body.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Register for the site: The steps of the wizard
 */
module cef.store.user.registration.steps.completed {
    class RegistrationStepCompletedBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected idSuffix: string;
        // Properties
        private get step(): RegistrationStepCompleted {
            if (!this.cvRegistrationService ||
                !this.cvRegistrationService.steps) {
                return null; // Not available (yet)
            }
            return <RegistrationStepCompleted>
                _.find(this.cvRegistrationService.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.completed);
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`RegistrationStepCompletedBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepCompletedBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvRegistrationService: services.IRegistrationService) {
            super(cefConfig);
            this.consoleDebug(`RegistrationStepCompletedBodyController.ctor()`);
        }
    }

    cefApp.directive("cefRegStepCompletedBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { idSuffix: "@" },
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/completed/body.html", "ui"),
        controller: RegistrationStepCompletedBodyController,
        controllerAs: "rscbCtrl",
        bindToController: true
    }));
}
