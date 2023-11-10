module cef.store.user.registration.steps.basicInfo {
    class RegistrationStepBasicInfoBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected idSuffix: string;
        // Properties
        regions: api.RegionModel[] = [];
        states: api.RegionModel[];
        businessTypeSelections: any[] = [
            {
                "ID": "Corporation",
                "Name": "Corporation",
                "SortOrder": 1
            },
            {
                "ID": "Partnership",
                "Name": "Partnership",
                "SortOrder": 2
            },
            {
                "ID": "Owner",
                "Name": "Owner",
                "SortOrder": 3
            }
        ];
        businessMedicalTypeSelections: any[] = [
            {
                "ID": "OB/GYN",
                "Name": "OB/GYN",
                "SortOrder": 1
            },
            {
                "ID": "Cardiology",
                "Name": "Cardiology",
                "SortOrder": 2
            },
            {
                "ID": "Dermatology",
                "Name": "Dermatology",
                "SortOrder": 3
            },
            {
                "ID": "EMS",
                "Name": "EMS",
                "SortOrder": 4
            },
            {
                "ID": "Municipality",
                "Name": "Municipality",
                "SortOrder": 5
            }
        ];
        accountPayableInvoiceMethod: any[] = [
            {
                "ID": "Mail",
                "Name": "Mail",
                "SortOrder": 1
            },
            {
                "ID": "Email",
                "Name": "Email",
                "SortOrder": 2
            }
        ];
        paymentBankingAccountTypes: any[] = [
            {
                "ID": "Checking",
                "Name": "Checking",
                "SortOrder": 1
            },
            {
                "ID": "Savings",
                "Name": "Savings",
                "SortOrder": 2
            }
        ];
        private get step(): RegistrationStepBasicInfo {
            if (!this.cvRegistrationService ||
                !this.cvRegistrationService.steps) {
                return null; // Not available (yet)
            }
            return <RegistrationStepBasicInfo>
                _.find(this.cvRegistrationService.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.basicInfo);
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`RegistrationStepBasicInfoBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepBasicInfoBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        protected get username(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.username;
        }
        protected set username(newValue: string) {
            this.consoleDebug(`RegistrationStepBasicInfoBodyController.username.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepBasicInfoBodyController.username.set 2`);
                this.step.username = newValue;
            }
        }
        protected get usernameValidityState(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.usernameValidityState;
        }
        protected set usernameValidityState(newValue: string) {
            this.consoleDebug(`RegistrationStepBasicInfoBodyController.usernameValidityState.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepBasicInfoBodyController.usernameValidityState.set 2`);
                this.step.usernameValidityState = newValue;
            }
        }
        protected get usernameError(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.usernameError;
        }
        protected set usernameError(newValue: string) {
            this.consoleDebug(`RegistrationStepBasicInfoBodyController.usernameError.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepBasicInfoBodyController.usernameError.set 2`);
                this.step.usernameError = newValue;
            }
        }
        protected get password(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.password;
        }
        protected set password(newValue: string) {
            this.consoleDebug(`RegistrationStepBasicInfoBodyController.password.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepBasicInfoBodyController.password.set 2`);
                this.step.password = newValue;
            }
        }
        protected get passwordValidityState(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.passwordValidityState;
        }
        protected set passwordValidityState(newValue: string) {
            this.consoleDebug(`RegistrationStepBasicInfoBodyController.passwordValidityState.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepBasicInfoBodyController.passwordValidityState.set 2`);
                this.step.passwordValidityState = newValue;
            }
        }
        protected get dateOfBirth(): Date {
            console.log("get date of birth 1");
            if (!this.step) {
                console.log("get date of birth 2");
                return undefined;
            }
            return this.step.dateOfBirth;
        }
        protected set dateOfBirth(newValue: Date) {
            console.log("set date of birth 1");
            if (this.step) {
                console.log("set date of birth 2");
                this.step.dateOfBirth = newValue;
            }
        }
        protected get age(): number {
            if (!this.step) {
                return undefined;
            }
            return this.step.age;
        }
        protected set age(newValue: number) {
            if (this.step) {
                this.step.age = newValue;
            }
        }
        protected get basicInfoContact(): api.ContactModel {
            if (!this.step) {
                return undefined;
            }
            return this.step.basicInfoContact;
        }
        protected set basicInfoContact(newValue: api.ContactModel) {
            this.consoleDebug(`RegistrationStepBasicInfoBodyController.basicInfoContact.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepBasicInfoBodyController.basicInfoContact.set 2`);
                this.step.basicInfoContact = newValue;
            }
        }
        protected get usernameIsEmail(): boolean {
            return this.cefConfig.featureSet.registration.usernameIsEmail;
        }
        protected get useSpecialCharInEmail(): boolean {
            return this.cefConfig.featureSet.registration.useSpecialCharInEmail;
        }
        protected get wholesalerNumber(): string {
            if (!this.step || !this.step.basicInfoContact || !this.step.basicInfoContact || !this.step.basicInfoContact["Wholesaler Number"]) {
                return undefined;
            }
            return this.step.basicInfoContact["Wholesaler Number"].Value;
        }
        protected set wholesalerNumber(newValue: string) {
            this.consoleDebug(`RegistrationStepBasicInfoBodyController.wholesalerNumber.set`);
            if (this.step && this.step.basicInfoContact && this.step.basicInfoContact && this.step.basicInfoContact["Wholesaler Number"]) {
                this.consoleDebug(`RegistrationStepBasicInfoBodyController.wholesalerNumber.set 2`);
                this.step.basicInfoContact["Wholesaler Number"].Value = newValue;
            }
        }
        // Functions
        // <None>
        // Events
        usernameChanged(): ng.IPromise<void> {
            if (this.cefConfig.featureSet.registration.usernameIsEmail) {
                this.usernameValidityState = "Valid";
                return this.$q.resolve();
            }
            if (!this.username || this.username === "") {
                this.usernameValidityState = "Empty";
                return this.$q.resolve();
            }
            if (this.username.length < 5) {
                this.usernameValidityState = "Invalid";
                return this.$q.resolve();
            }
            if (!(/^[A-Za-z0-9_\.@!#\$%\&'\*+/=?\^_`{|}~-]+$/.test(this.username)) && !this.useSpecialCharInEmail) {
                this.usernameValidityState = "Invalid";
                return this.$q.resolve();
            }
            return this.$q((resolve, reject) => {
                this.cvAuthenticationService.validateUserNameIsGood({
                    UserName: this.username
                }).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.usernameValidityState = "Invalid";
                        this.usernameError = r && r.data && r.data.Messages && r.data.Messages[0];
                        reject();
                        return;
                    }
                    this.usernameValidityState = "Valid";
                    this.usernameError = "";
                    if (this.cefConfig.featureSet.registration.usernameIsEmail) {
                        this.basicInfoContact.Email1 = this.username;
                    }
                    resolve();
                });
            });
        }
        getAge(birthDate: Date) {
            var today = new Date();
            var age = today.getFullYear() - birthDate.getFullYear();
            var m = today.getMonth() - birthDate.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                age--;
            }
            return age;
        }
        dateOfBirthChanged(): ng.IPromise<void> {
            console.log("hello?");
            return this.$q.resolve(
                this.$timeout(() => {
                    this.age = this.getAge(this.dateOfBirth);
                    this.onChange();
                }, 250));
        }
        // Events
        onChange = (): void => {
            const debugMsg = `RegistrationStepBasicInfoBodyController.onChange()`;
            this.consoleDebug(debugMsg);
            this.invalid = true;
            if (this.forms.basicInfo.$invalid) {
                this.consoleDebug(`${debugMsg} failed! Form is invalid`);
                this.consoleDebug(this.forms.basicInfo.$error);
                this.invalid = true;
                return;
            }
            this.setRunning();
            this.usernameChanged().then(() => {
                this.step.validate()
                    .then(success => {
                        if (!success) {
                            this.consoleDebug(`${debugMsg} failed! Not a successful step.validate`);
                            this.invalid = true;
                            this.finishRunning(true, "An error has occurred");
                            return;
                        }
                        this.consoleDebug(`${debugMsg} success!`);
                        this.invalid = false;
                        this.finishRunning();
                    }, result => {
                        this.consoleDebug(`${debugMsg} failed! Error`);
                        this.consoleDebug(result);
                        this.invalid = true;
                        this.finishRunning(true, result || "An error has occurred");
                    }).catch(reason => {
                        this.consoleDebug(`${debugMsg} failed! Catch`);
                        this.consoleDebug(reason);
                        this.invalid = true;
                        this.finishRunning(true, reason || "An error has occurred");
                    });
            });
        }
        getStates(): void {
            this.cvRegionService.search({
                Active: true,
                AsListing: true,
                Sorts: [{ field: "Name", order: 0, dir: "asc" }],
                CountryID: 1
            }).then(r => this.regions = r);
        }
        setUserRegistrationTypeID(data) {
            this.step.userRegistrationTypeSelectionID = data;
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                protected readonly $timeout: ng.ITimeoutService,
                readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvRegistrationService: services.IRegistrationService,
                private readonly cvRegionService: services.IRegionService,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.getStates();
            this.consoleDebug(`RegistrationStepBasicInfoBodyController.ctor()`);
            const unbind1 = $scope.$on("typeSelectionIDChanged",
                (__: ng.IAngularEvent, data: number) => this.setUserRegistrationTypeID(data));
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefRegStepBasicInfoBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { idSuffix: "@", regions: "=?" },
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/basicInfo/body.html", "ui"),
        controller: RegistrationStepBasicInfoBodyController,
        controllerAs: "regStepBasicInfoBodyCtrl",
        bindToController: true
    }));
}
