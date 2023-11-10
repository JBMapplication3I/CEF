/**
 * @file framework/store/user/registration/steps/addressBook/summary.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Register for the site: The steps of the wizard
 */
module cef.store.user.registration.steps.addressBook {
    class RegistrationStepAddressBookSummaryController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        private get step(): RegistrationStepAddressBook {
            if (!this.cvRegistrationService
                || !this.cvRegistrationService.steps) {
                return null; // Not available (yet)
            }
            return <RegistrationStepAddressBook>
                _.find(this.cvRegistrationService.steps,
                    x => x.name === this.cvServiceStrings.checkout.steps.billing);
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
            this.consoleDebug(`RegistrationStepAddressBookSummaryController.ctor()`);
        }
    }

    cefApp.directive("cefRegStepAddressBookSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/addressBook/summary.html", "ui"),
        controller: RegistrationStepAddressBookSummaryController,
        controllerAs: "regStepAddressBookSummaryCtrl",
        bindToController: true
    }));
}
