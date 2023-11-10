/**
 * @file framework/store/user/registration/steps/completed/body.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Register for the site: The steps of the wizard
 * NOTE: This file is intended to be overriden in client projects to
 * enable custom registration requirements
 */
module cef.store.user.registration.steps.custom {
    class RegistrationStepCustomBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected idSuffix: string;
        // Properties
        private get step(): RegistrationStepCustom {
            if (!this.cvRegistrationService ||
                !this.cvRegistrationService.steps) {
                return null; // Not available (yet)
            }
            return <RegistrationStepCustom>
                _.find(this.cvRegistrationService.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.custom);
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`RegistrationStepCustomBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepCustomBodyController.invalid.set 2`);
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
                private readonly cvRegistrationService: services.IRegistrationService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefRegStepCustomBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { idSuffix: "@" },
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/custom/body.html", "ui"),
        controller: RegistrationStepCustomBodyController,
        controllerAs: "regStepCustomBodyCtrl",
        bindToController: true
    }));
}
