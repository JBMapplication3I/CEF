/**
 * @file framework/store/user/registration/registration.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Registration Wizard, main wrapper
 */
 module cef.store.user.registration {
    class RegistrationController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        get userRegistrationTypeSelectionID(): number {
            return this.cvRegistrationService.userRegistrationTypeSelectionID;
        }
        set userRegistrationTypeSelectionID(value: number) {
            this.cvRegistrationService.userRegistrationTypeSelectionID = value;
        }
        get newUserRegistrationType(): services.IRegistrationType[] {
            return this.cvRegistrationService.newUserRegistrationType;
        }
        // Functions
        protected goToStep(stepIndex: number): void {
            this.consoleDebug(`RegistrationController.goToStep(stepIndex: ${stepIndex})`);
            this.setRunning();
            this.cvRegistrationService.activateStep(stepIndex)
                .then(success => {
                    if (!success) {
                        this.finishRunning(true, `Activating step ${stepIndex} failed`);
                        return;
                    }
                    this.finishRunning();
                }).catch(reason => this.finishRunning(true, reason));
        }
        protected submit(step: steps.IRegistrationStep): void {
            const debug = `RegistrationController.submit(step: "${step && step.name}")`;
            this.consoleDebug(debug);
            this.setRunning();
            step.submit()
                .then(success => {
                    if (!success) {
                        this.finishRunning(true, "An error occurred in step submit, it was not successful");
                    }
                    this.consoleDebug(`${debug}: Submit current step succeeded, Going to next step`);
                    step.complete = true;
                    this.goToStep(this.cvRegistrationService.activeStep + 1);
                    if (step.name === "registrationStepConfirmation") {
                        const basicInfoStep = <user.registration.steps.basicInfo.RegistrationStepBasicInfo>_.find(
                            this.cvRegistrationService.steps,
                            x => x.name === this.cvServiceStrings.registration.steps.basicInfo);
                        if(this.userRegistrationTypeSelectionID === 1 && basicInfoStep.taxExempt) {
                            this.cvMessageModalFactory(this.$translate("ui.storefront.registration.termsAndTaxExempt"));
                        } else if (this.userRegistrationTypeSelectionID === 2 && basicInfoStep.taxExempt) {
                            this.cvMessageModalFactory(this.$translate("ui.storefront.registration.taxExepmtOnly"));
                        } else if (this.userRegistrationTypeSelectionID === 1 && !basicInfoStep.taxExempt) {
                            this.cvMessageModalFactory(this.$translate("ui.storefront.registration.termsOnly"));
                        }
                    }
                }).catch(reason => {
                    this.finishRunning(true, reason || "An error occurred in step submit, it was not successful");
                });
        }
        private setReturnURL(): ng.IPromise<void> {
            return this.$q(resolve => {
                this.cvRegistrationService.returnUrl = this.cvRegistrationService.returnUrl
                    || decodeURIComponent((this.getParameter("returnUrl") + "").replace(/\+/g, "%20"));
                if (!this.cvRegistrationService.returnUrl
                    || this.cvRegistrationService.returnUrl === "null"
                    || this.cvRegistrationService.returnUrl === "/Authentication/Sign-In") {
                    this.cvRegistrationService.returnUrl = "/";
                }
                resolve();
            });
        }
        private getParameter(paramName: string): string {
            const searchString = this.$window.location.search.substring(1);
            let params: Array<string>;
            if (searchString === "") {
                const hashString = this.$window.location.hash.substring(3);
                params = hashString.split("&");
            } else {
                params = searchString.split("&");
            }
            for (let i = 0; i < params.length; i++) {
                const val = params[i].split("=");
                if (val[0].toLowerCase() === paramName.toLowerCase()) { return val[1]; }
            }
            return null;
        }
        private setCompanyName(): ng.IPromise<void> {
            return this.$q(resolve => {
                /* TODO: Setting to pull name from current store data
                this.cvCurrentStoreService.getCurrentStorePromise(!this.cvCurrentStoreService.haveFullStoreDetails()).then(r => {
                    if (!r || !r.Name) {
                        this.companyName = "ClarityClient.com";
                        return;
                    }
                    this.companyName = r.Name;
                });
                */
                this.cvRegistrationService.companyName = this.cefConfig.companyName;
                resolve();
            });
        }
        private setAuthState(): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.cvAuthenticationService.preAuth().finally(() => {
                    if (this.cvAuthenticationService.isAuthenticated()) {
                        reject();
                        this.cancel();
                        return;
                    }
                    resolve();
                });
            });
        }
        cancel(): void {
            this.$filter("goToCORSLink")("/", "site", "primary", false, { });
        }
        // Events
        userRegistrationTypeSelectionIDChanged($event: ng.IAngularEvent, control: ng.INgModelController): void {
            this.$rootScope.$broadcast("typeSelectionIDChanged", control.$modelValue);
        }
        // Constructor
        constructor(
                private readonly $window: ng.IWindowService,
                private readonly $filter: ng.IFilterService,
                private readonly $rootScope: ng.IRootScopeService,
                readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvRegistrationService: services.IRegistrationService,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory,
                protected readonly $translate: ng.translate.ITranslateService,) {
            super(cefConfig);
            this.consoleDebug(`RegistrationController.ctor()`);
            this.setRunning();
            this.cvRegistrationService.building = true;
            this.setAuthState().then(() => {
                this.$q.all([
                    this.setReturnURL(),
                    this.setCompanyName()
                ]).finally(() => this.cvRegistrationService.initialize()
                    .then(() => this.finishRunning())
                    .catch(reason => this.finishRunning(true, reason)));
            });
        }
    }

    cefApp.directive("cefRegistration", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/user/registration/registration.html", "ui"),
        controller: RegistrationController,
        controllerAs: "registrationCtrl",
        bindToController: true
    }));
}
