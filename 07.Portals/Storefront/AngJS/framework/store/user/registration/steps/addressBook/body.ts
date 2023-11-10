/**
 * @file framework/store/user/registration/steps/addressBook/body.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Register for the site: The steps of the wizard
 */
module cef.store.user.registration.steps.addressBook {
    class RegistrationStepAddressBookBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected idSuffix: string;
        // Properties
        private get step(): RegistrationStepAddressBook {
            if (!this.cvRegistrationService ||
                !this.cvRegistrationService.steps) {
                return null; // Not available (yet)
            }
            return <RegistrationStepAddressBook>
                _.find(this.cvRegistrationService.steps,
                    x => x.name === this.cvServiceStrings.registration.steps.addressBook);
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`RegistrationStepAddressBookBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepAddressBookBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        protected get book(): api.AccountContactModel[] {
            if (!this.step) {
                return undefined;
            }
            return this.step.book;
        }
        protected set book(newValue: api.AccountContactModel[]) {
            this.consoleDebug(`RegistrationStepAddressBookBodyController.addressTitle.set`);
            if (this.step) {
                this.consoleDebug(`RegistrationStepAddressBookBodyController.addressTitle.set 2`);
                this.step.book = newValue;
            }
        }
        protected get hasBilling(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.hasBilling();
        }
        protected get hasShipping(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.hasShipping();
        }
        protected get billing(): api.AccountContactModel {
            if (!this.step) {
                return undefined;
            }
            return this.step.billing();
        }
        protected get shipping(): api.AccountContactModel {
            if (!this.step) {
                return undefined;
            }
            return this.step.shipping();
        }
        // Functions
        protected add(isBilling: boolean): void {
            const basicInfoStep = <user.registration.steps.basicInfo.RegistrationStepBasicInfo>_.find(
                this.cvRegistrationService.steps,
                x => x.name === this.cvServiceStrings.registration.steps.basicInfo);
            const basicInfoContact = basicInfoStep && basicInfoStep.basicInfoContact || null;
            const basicInfoContactCopy: api.ContactModel = basicInfoContact && angular.fromJson(angular.toJson(basicInfoContact)) || null;
            if (basicInfoContactCopy) {
                if (!basicInfoContactCopy.Address) {
                    basicInfoContactCopy.Address = { ID: 0, Active: true, CreatedDate: new Date() };
                }
                basicInfoContactCopy.Address.CustomKey = isBilling ? "BILL TO" : "SHIP TO";
            }
            const prepopulateWithBasicInfoData = basicInfoContactCopy && <api.AccountContactModel>{
                Active: true,
                CreatedDate: new Date(),
                TransmittedToERP: false,
                IsBilling: isBilling,
                IsPrimary: !isBilling,
                MasterID: null,
                SlaveID: null,
                Slave: basicInfoContactCopy
            } || null;
            this.cvAddressModalFactory(
                this.$translate("ui.storefront.userDashboard2.controls.addressEditor2.AddANewAddress"),
                this.$translate("ui.storefront.checkout.splitShipping.addressModal.AddAddress"),
                null,
                "RegistrationAddressBook" + (isBilling ? "Billing" : "Shipping"),
                isBilling,
                prepopulateWithBasicInfoData
            ).then(newEntry => this.checkForPrimaryAndBilling(newEntry));
        }
        protected checkForPrimaryAndBilling(newEntry: api.AccountContactModel): void {
            if (!this.book) {
                this.book = [];
            }
            // if (!this.book.length) {
            //     newEntry.IsBilling = true;
            // } else if (!this.book.filter(x => x.IsBilling && x.Active).length) {
            //     newEntry.IsBilling = true;
            // }
            // if (!newEntry.IsBilling && !this.book.filter(x => x.IsPrimary && x.Active).length) {
            //     newEntry.IsPrimary = true;
            // }
            this.book.push(newEntry);
            // if (!newEntry.IsBilling) {
                // Check if address is in service
                /* NOTE: This endpoint is in a client specific library
                this.setRunning("Checking for service...");
                this.cvApi.contractors.CheckForServiceAtAddress(newEntry.Slave.Address).then(r => {
                    if (r) {
                        this.cvRegistrationService.hasService = this.step.hasService = r.data;
                    }
                    this.finishRunning();
                */
                    // this.onChange();
                /*
                }).catch(err => {
                    this.consoleError('Unable to determine service at address: ', err);
                    this.finishRunning(true, "Unable to determine service at address");
                });
                return;
                */
            // }
            // run onChange for billing address
            this.onChange();
        }
        protected copyBilling(): void {
            this.setRunning();
            if (!this.hasBilling) {
                this.consoleDebug("Cannot copy billing if it doesn't exist yet");
                this.finishRunning(true, "Cannot copy billing if it doesn't exist yet");
                return;
            }
            const billingCopy = angular.fromJson(angular.toJson(this.billing)) as api.AccountContactModel;
            billingCopy.IsBilling = false;
            billingCopy.IsPrimary = true;
            billingCopy.Slave.SameAsBilling = true;
            billingCopy.Slave.Address.CustomKey = "SHIP TO";
            this.checkForPrimaryAndBilling(billingCopy);
            this.finishRunning();
        }
        // Events
        onChange = (): void => {
            const debugMsg = `RegistrationStepAddressBookBodyController.onChange()`;
            this.consoleDebug(debugMsg);
            this.invalid = true;
            if (this.forms.addressBook.$invalid) {
                this.consoleDebug(`${debugMsg} failed! Form is invalid`);
                this.consoleDebug(this.forms.basicInfo.$error);
                this.invalid = true;
                return;
            }
            this.setRunning();
            this.step.validate().then(success => {
                if (!success) {
                    this.consoleDebug(`${debugMsg} failed! Not a successful step.validate`);
                    this.invalid = true;
                    this.finishRunning(true, "An error has occurred");
                    return;
                }
                this.consoleDebug(`${debugMsg} success!`);
                this.invalid = false;
                this.finishRunning();
            }).catch(reason => {
                this.consoleDebug(`${debugMsg} failed! Catch`);
                this.consoleDebug(reason);
                this.invalid = true;
                this.finishRunning(true, reason || "An error has occurred");
            });
        }
        // Constructor
        constructor(
                protected readonly cvApi: api.ICEFAPI,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAddressModalFactory: modals.IAddressModalFactory,
                private readonly cvRegistrationService: services.IRegistrationService) {
            super(cefConfig);
            this.consoleDebug(`RegistrationStepAddressBookBodyController.ctor()`);
        }
    }

    cefApp.directive("cefRegStepAddressBookBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { idSuffix: "@" },
        templateUrl: $filter("corsLink")("/framework/store/user/registration/steps/addressBook/body.html", "ui"),
        controller: RegistrationStepAddressBookBodyController,
        controllerAs: "regStepAddressBookBodyCtrl",
        bindToController: true
    }));
}
