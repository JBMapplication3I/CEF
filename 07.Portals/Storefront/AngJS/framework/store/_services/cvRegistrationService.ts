/**
 * @file framework/store/_services/cvRegistrationService.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Register for the website: The service which can drive the process
 * by managing the data
 */
module cef.store.services {
    export interface IRegistrationService {
        // Properties
        steps: user.registration.steps.IRegistrationStep[];
        building: boolean;
        activeStep: number;
        customAttributes: api.SerializableAttributesDictionary;
        isComplete: boolean;
        returnUrl: string;
        companyName: string;
        hasService: boolean;
        userRegistrationTypeSelectionID: number;
        newUserRegistrationType: IRegistrationType[];
        // Functions
        activateStep(stepIndex: number): ng.IPromise<boolean>;
        anyStepIsBuilding(): boolean;
        anyStepIsRunning(): boolean;
        anyStepIsInvalid(): boolean;
        initialize(): ng.IPromise<boolean>;
        finalize(): ng.IPromise<boolean>;
    }

    export interface IRegistrationType {
        ID: number,
        Name: string,
        SortOrder: number
    }

    export class RegistrationService implements IRegistrationService {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        // Properties
        steps: user.registration.steps.IRegistrationStep[] = [];
        building: boolean = false;
        activeStep: number = null;
        customAttributes: api.SerializableAttributesDictionary = null;
        isComplete: boolean = false;
        returnUrl: string = null;
        companyName: string = null;
        hasService: boolean = true;
        isAuthenticated: boolean = false;
        userRegistrationTypeSelectionID: number;
        newUserRegistrationType: IRegistrationType[] = [
            {
                ID: 1,
                Name: "New With Terms",
                SortOrder: 1
            },
            {
                ID: 2,
                Name: "New Without Terms",
                SortOrder: 2
            },
            {
                ID: 3,
                Name: "Existing",
                SortOrder: 3
            }
        ];
        // Functions
        activateStep(stepIndex: number): ng.IPromise<boolean> {
            this.consoleDebug(`RegistrationService.activateStep(stepIndex: ${stepIndex})`);
            this.activeStep = Number(stepIndex);
            return this.$q.resolve(true);
        }
        anyStepIsBuilding(): boolean {
            this.consoleDebug(`RegistrationService.anyStepIsBuilding()`);
            return this.building ||
                this.steps && _.some(this.steps, x => x.building);
        }
        anyStepIsRunning(): boolean {
            this.consoleDebug(`RegistrationService.anyStepIsRunning()`);
            return this.steps && _.some(this.steps, x => x.viewState.running);
        }
        anyStepIsInvalid(): boolean {
            this.consoleDebug(`RegistrationService.anyStepIsInvalid()`);
            return this.steps && _.some(this.steps, x => x.viewState.hasError);
        }
        private isValidData(data: user.registration.IRegistrationData): boolean {
            this.consoleDebug(`RegistrationService.isValidData(data)`);
            if (!data) {
                return false;
            }
            let asString = angular.toJson(cart);
            const copy = angular.fromJson(asString);
            asString = angular.toJson(copy);
            return !(asString === "{}");
        }
        private fullReject(reject, toReturn: any): void {
            this.consoleDebug(`RegistrationService.fullReject(reject, toReturn)`);
            if (toReturn) {
                this.consoleDebug(toReturn);
            }
            reject(toReturn);
            this.building = false;
        }
        initialize(): ng.IPromise<boolean> {
            const debugMsg = `RegistrationService.initialize()`;
            this.consoleDebug(debugMsg);
            this.building = true;
            return this.$q((resolve, reject) => {
                const stepSetups = [
                    new user.registration.steps.basicInfo.RegistrationStepBasicInfo(this.$q, this.$translate, this.cefConfig, this.cvServiceStrings, this.cvApi, this.cvContactFactory),
                    new user.registration.steps.addressBook.RegistrationStepAddressBook(this.$q, this.cefConfig, this.cvServiceStrings),
                    new user.registration.steps.wallet.RegistrationStepWallet(this.$q, this.cefConfig, this.cvServiceStrings),
                    new user.registration.steps.custom.RegistrationStepCustom(this.$q, this.cefConfig, this.cvServiceStrings),
                    new user.registration.steps.confirmation.RegistrationStepConfirmation(this.$q, this.cefConfig, this.cvServiceStrings, this),
                    new user.registration.steps.completed.RegistrationStepCompleted(this.$q, this.cefConfig, this.cvServiceStrings, this),
                ];
                const tempSteps: user.registration.steps.IRegistrationStep[] = [];
                this.$q.all([
                    stepSetups[0].canEnable(),
                    stepSetups[1].canEnable(),
                    stepSetups[2].canEnable(),
                    stepSetups[3].canEnable(),
                    stepSetups[4].canEnable(),
                    stepSetups[5].canEnable()
                ]).then((rarr: boolean[]) => {
                    for (let i = 0; i < rarr.length; i++) {
                        if (rarr[i]) {
                            // Step is valid
                            this.consoleDebug(`${debugMsg} ${stepSetups[i].name} is valid, adding to UI`);
                            stepSetups[i].index = tempSteps.length;
                            tempSteps.push(stepSetups[i]);
                        } else {
                            this.consoleDebug(`${debugMsg} ${stepSetups[i].name} is invalid, not adding to UI`);
                        }
                    }
                    if (!tempSteps.length) {
                        this.consoleDebug(`${debugMsg} ERROR! No steps area set up for registration UI!`);
                        this.fullReject(reject, { issue: "ERROR! No steps area set up for registration UI!" });
                        return;
                    }
                    this.cvAuthenticationService.preAuth().finally(() => {
                        this.$q.all(tempSteps.map(s => s.initialize())).then(rarr => {
                            for (let i = 0; i < rarr.length; i++) {
                                if (!rarr[i]) {
                                    this.consoleDebug(`${debugMsg} step ${i} could not initialize`);
                                }
                            }
                            this.activeStep = 0;
                            this.consoleDebug(`${debugMsg} Complete!`);
                            this.steps = tempSteps;
                            this.building = false;
                            resolve(true);
                        }).catch(reason3 => this.fullReject(reject, reason3));
                    });
                }).catch(reason2 => this.fullReject(reject, reason2));
            });
        }
        finalize(): ng.IPromise<boolean> {
            const debugMsg = `RegistrationService.finalize(data)`;
            this.consoleDebug(debugMsg);
            return this.$q((resolve, reject) => {
                const basicInfoStep = <user.registration.steps.basicInfo.RegistrationStepBasicInfo>_.find(
                    this.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.basicInfo);
                if (!basicInfoStep) {
                    this.consoleDebug(`${debugMsg} no basicInfo step detected, rejecting`);
                    reject(`${debugMsg} no basicInfo step detected, rejecting`);
                    return;
                }
                const addressBookStep = <user.registration.steps.addressBook.RegistrationStepAddressBook>_.find(
                    this.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.addressBook);
                if (!addressBookStep) {
                    this.consoleDebug(`${debugMsg} no addressBook step detected`);
                }
                const walletStep = <user.registration.steps.wallet.RegistrationStepWallet>_.find(
                    this.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.wallet);
                if (!walletStep) {
                    this.consoleDebug(`${debugMsg} no wallet step detected`);
                }
                const customStep = <user.registration.steps.custom.RegistrationStepCustom>_.find(
                    this.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.custom);
                if (!customStep) {
                    this.consoleDebug(`${debugMsg} no custom step detected`);
                }
                const confirmationStep = <user.registration.steps.confirmation.RegistrationStepConfirmation>_.find(
                    this.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.confirmation);
                if (!confirmationStep) {
                    this.consoleDebug(`${debugMsg} no confirmation step detected`);
                }
                const contact = basicInfoStep.basicInfoContact;
                contact.Address = addressBookStep.book.find((b) => b.IsPrimary == true).Slave.Address;
                contact.Active = true;
                contact.TypeID = 1;
                contact.CustomKey = basicInfoStep.username;
                contact.CreatedDate = new Date();
                if (contact.Address && contact.Address.CustomKey == null) {
                    contact.Address.CustomKey = contact.Address.Street1;
                }
                const dto = <api.RegisterNewUserDto>{
                    ID: 0,
                    // TODO: basic Info
                    CreatedDate: new Date(),
                    Active: true,
                    CustomKey: basicInfoStep.username,
                    UserName: basicInfoStep.username,
                    Email: contact.Email1,
                    Password: basicInfoStep.password,
                    OverridePassword: basicInfoStep.password,
                    IsDeleted: false,
                    IsSuperAdmin: false,
                    IsEmailSubscriber: false,
                    IsCatalogSubscriber: false,
                    EmailConfirmed: false,
                    StatusID: null,
                    Status: null,
                    StatusKey: "Registered",
                    StatusName: null,
                    TypeID: null,
                    Type: null,
                    TypeKey: null,
                    TypeName: "Customer",
                    ContactID: null,
                    Contact: contact,
                    DateOfBirth: basicInfoStep.dateOfBirth,
                    // AccountID: accountID,
                    AccessFailedCount: 0,
                    PhoneNumberConfirmed: false,
                    TwoFactorEnabled: false,
                    LockoutEnabled: false,
                    SerializableAttributes: basicInfoStep.getSerializedAttributesDictionary(),
                    IsApproved: false,
                    RequirePasswordChangeOnNextLogin: false,
                    IsSMSAllowed: false,
                    UseAutoPay: false,
                    // TODO: address book
                    // TODO: wallet
                    // TODO: custom
                    AddressBook: addressBookStep && addressBookStep.book,
                    InService: this.hasService,
                    BusinessType: basicInfoStep.businessType,
                    DunsNumber: basicInfoStep.dunsNumber,
                    TaxExempt: basicInfoStep.taxExempt,
                    TaxExemptNumber: basicInfoStep.taxExemptNumber,
                    EIN: basicInfoStep.einNumber,
                    DEANumber: basicInfoStep.deaNumber,
                    MedicalLicenseNumber: basicInfoStep.medicalLicenseNumber,
                    MedicalLicenseState: basicInfoStep.medicalLicenseState,
                    MedicalLicenseHolderName: basicInfoStep.medicalLicenseHolderName,
                };
                // Set registration type
                if (this.userRegistrationTypeSelectionID) {
                    const info = this.newUserRegistrationType.find(x => x.ID == this.userRegistrationTypeSelectionID);
                    dto.RegistrationType = info?.Name;
                }
                this.consoleDebug(`${debugMsg} dto:`);
                this.consoleDebug(dto);
                /* TODO: Restricted shipping to read the contacts from this address book
                if (this.restrictedShipping && this.contact) {
                    // TODO: Should this delay completing registration until modal is resolved?
                    this.cvRestrictedRegionCheckService.validate(contact).then(restrict => {
                        if (restrict) {
                            this.cvRestrictedRegionCheckService.triggerModal(contact);
                        }
                    }).catch(reason => reject(reason));
                }
                */
                const loginData = <api.AuthProviderLoginDto>{
                    Username: dto.UserName,
                    Password: basicInfoStep.password,
                    RememberMe: true
                };
                // Attempt to see if account already exists, if so, do not register
                this.cvAuthenticationService.login(loginData).finally(() => {
                    if (this.cvAuthenticationService.isAuthenticated()) {
                        this.isAuthenticated = true;
                        this.isComplete = true;
                        return;
                    } else {
                        this.isAuthenticated = false;
                    }
                    // Register new user
                    this.cvAuthenticationService.register(dto).then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded) {
                            // this.finishRunning(true, null, r.data.Messages);
                            reject("Failed to register user");
                            return;
                        }
                        if (!this.cefConfig.newUserRegister.newUsersAreDefaultApproved
                            && !this.cefConfig.featureSet.registration.verificationIsRequired) {
                            this.isComplete = true;
                            resolve(true);
                            return;
                        }
                        this.cvAuthenticationService.login(loginData).finally(() => {
                            // wait a moment to show the success messages.
                            this.isAuthenticated = true;
                            this.isComplete = true;
                            resolve(true);
                            this.$timeout(() => {
                                this.$filter("goToCORSLink")(this.cefConfig.routes.site.root);
                            }, 7500);
                        });
                    }).catch(reason => {
                        // wait a moment to show the error messages.
                        reject(reason, "Failed to register user");
                    });
                });
            });
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly $filter: ng.IFilterService,
                private readonly $timeout: ng.ITimeoutService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                private readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvContactFactory: factories.IContactFactory,
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvCurrentBrandService: services.ICurrentBrandService,
                private readonly cvWalletService: services.IWalletService) {
            // NOTE: The service isn't going to call load, the directive is when it needs it
            this.consoleDebug(`RegistrationService.ctor()`);
        }
    }
}
