module cef.store.user.registration.steps.basicInfo {
    export class RegistrationStepBasicInfo extends RegistrationStep {
        // Properties
        get name(): string { return this.cvServiceStrings.registration.steps.basicInfo; }
        basicInfoContact: api.ContactModel = null;
        basicInfoCompany: string;
        username: string = null;
        usernameValidityState = "Empty";
        usernameError = "Empty";
        password: string = null;
        passwordValidityState = "Empty";
        dateOfBirth: Date;
        age: number;
        generalAttributes: api.GeneralAttributeModel[];
        userRegistrationTypeSelectionID: number;
        businessType: string;
        dunsNumber: string;
        taxExempt: boolean = false;
        taxExemptNumber: string;
        einNumber: string;
        deaNumber: string;
        medicalLicenseNumber: string;
        medicalLicenseState: string;
        medicalLicenseHolderName: string;
        businessMedicalType: string;
        estimatedMonthlyPurchases: string;
        accountPayableContactName: string;
        accountPayableContactPhone: string;
        accountPayableContactEmail: string;
        accountPayableContactEmailForInvoices: string;
        accountPayableInvoiceMethod: string;
        paymentBankingName: string;
        paymentBankingTitle: string;
        paymentBankingOptionalName: string;
        paymentBankingOptionalTitle: string;
        paymentBankingResponsiblePerson: string;
        paymentBankingBankName: string;
        paymentBankingAccountType: string;
        paymentBankingBankContactFirstName: string;
        paymentBankingBankContactLastName: string;
        paymentBankingPhone: string;
        paymentBankingFax: string;
        businessReferencesName1: string;
        businessReferencesContact1: string;
        businessReferencesEmail1: string;
        businessReferencesPhone1: string;
        businessReferencesFax1: string;
        businessReferencesName2: string;
        businessReferencesContact2: string;
        businessReferencesEmail2: string;
        businessReferencesPhone2: string;
        businessReferencesFax2: string;
        businessReferencesName3: string;
        businessReferencesContact3: string;
        businessReferencesEmail3: string;
        businessReferencesPhone3: string;
        businessReferencesFax3: string;
        termsCheck: boolean;
        eSignature: string;
        signatureCheck: boolean;
        signedDate: Date;
        // Functions
        // canEnable override not required
        initialize(): ng.IPromise<boolean> {
            this.consoleDebug(`RegistrationStepBasicInfo.initialize()`);
            this.building = true;
            return this.$q((resolve, __) => {
                this.basicInfoContact = this.cvContactFactory.new();
                this.loadUserAttributes().then(() => {
                    this.building = false;
                    resolve(true);
                });
            });
        }
        validate(): ng.IPromise<boolean> {
            this.consoleDebug(`RegistrationStepBasicInfo.validate()`);
            if (this.usernameValidityState != "Valid") {
                this.invalid = true;
                return this.$q.reject(this.$translate("ui.storefront.user.login.ThisIsNotAValidUsername"));
            }
            /* This should only be used if a typeahead is active on the country part of the address form
            if (this.contact && this.contact.Address && !this.contact.Address.CountryID) {
                this.finishRunning(true, "Country is Required");
                this.return;
            }
            */
            /* TODO: Put this behind a setting
            if (this.age < 18) {
                this.invalid = true;
                return this.$q.reject(this.$translate("ui.storefront.user.registration.MinimumAge.Message"));
            }
            */
            this.invalid = false;
            return this.$q.resolve(true);
        }
        // submit override not required
        // For clients that capture additional data, placing them in attributes
        // via the template override
        attrsDelimitedValues: { [key: string]: string; } = { };
        private loadUserAttributes(): ng.IPromise<void> {
            return this.$q(resolve => {
                // TODO: Migrate to cvAttributesService with the specific filter
                this.cvApi.attributes.GetGeneralAttributes({
                    Active: true,
                    AsListing: true,
                    TypeName: "User"
                }).then(r => {
                    this.generalAttributes = r.data.Results;
                    this.setSerializableAttributes();
                    resolve();
                });
            });
        }
        private setSerializableAttributes(): void {
            this.generalAttributes.forEach(x => {
                this.basicInfoContact[x.CustomKey] = <api.SerializableAttributeObject>{
                    ID: x.ID,
                    Group: x.Group,
                    Key: x.CustomKey,
                    SortOrder: x.SortOrder,
                    Value: undefined,
                    AllowedValues: new Array<string>(),
                };
                if (!x.IsPredefined) {
                    return;
                }
                if (!x.GeneralAttributePredefinedOptions) {
                    x.GeneralAttributePredefinedOptions = [];
                }
                this.cvApi.attributes.GetGeneralAttributePredefinedOptions({
                    Active: true,
                    AsListing: true,
                    AttributeID: x.ID
                }).then(r => {
                    r.data.Results.forEach(option => {
                        this.basicInfoContact[x.CustomKey].AllowedValues.push(option.Value);
                        if (x.GeneralAttributePredefinedOptions.filter(po => po.Value === option.Value).length === 0) {
                            x.GeneralAttributePredefinedOptions.push(option);
                        }
                    });
                });
            });
        }
        private setAttributesDelimitedValues(model: api.BaseModel): void {
            if (!model.SerializableAttributes) {
                model.SerializableAttributes = {};
            }
            Object.keys(this.attrsDelimitedValues).forEach(key => {
                model.SerializableAttributes[key] = { ID: 0, Key: key, Value: "" };
                let value = "";
                Object.keys(this.attrsDelimitedValues[key] as any).forEach(v => {
                    if (!v) {
                        return;
                    }
                    if (value.length > 0) {
                        value = value + ",";
                    }
                    value = value + v;
                });
                model.SerializableAttributes[key].Value = value;
            });
        }
        getSerializedAttributesDictionary(): api.SerializableAttributesDictionary {
            const attributesDictionary = new api.SerializableAttributesDictionary();
            this.generalAttributes.forEach(x => attributesDictionary[x.CustomKey] = this.basicInfoContact[x.CustomKey]);
            if (this.businessMedicalType != null) {
                attributesDictionary["businessMedicalType"] = {
                    ID: 1,
                    Key: "businessMedicalType",
                    Value: this.businessMedicalType
                }
            }
            if (this.accountPayableContactName != null) {
                attributesDictionary["accountPayableContactName"] = {
                    ID: 2,
                    Key: "accountPayableContactName",
                    Value: this.accountPayableContactName
                }
            }
            if (this.accountPayableContactPhone != null) {
                attributesDictionary["accountPayableContactPhone"] = {
                    ID: 3,
                    Key: "accountPayableContactPhone",
                    Value: this.accountPayableContactPhone
                }
            }
            if (this.accountPayableContactEmail != null) {
                attributesDictionary["accountPayableContactEmail"] = {
                    ID: 4,
                    Key: "accountPayableContactEmail",
                    Value: this.accountPayableContactEmail
                }
            }
            if (this.accountPayableContactEmailForInvoices != null) {
                attributesDictionary["accountPayableContactEmailForInvoices"] = {
                    ID: 5,
                    Key: "accountPayableContactEmailForInvoices",
                    Value: this.accountPayableContactEmailForInvoices
                }
            }
            if (this.estimatedMonthlyPurchases != null) {
                attributesDictionary["estimatedMonthlyPurchases"] = {
                    ID: 6,
                    Key: "estimatedMonthlyPurchases",
                    Value: this.estimatedMonthlyPurchases
                }
            }
            if (this.paymentBankingName != null) {
                attributesDictionary["paymentBankingName"] = {
                    ID: 7,
                    Key: "paymentBankingName",
                    Value: this.paymentBankingName
                }
            }
            if (this.paymentBankingTitle != null) {
                attributesDictionary["paymentBankingTitle"] = {
                    ID: 8,
                    Key: "paymentBankingTitle",
                    Value: this.paymentBankingTitle
                }
            }
            if (this.paymentBankingOptionalName != null) {
                attributesDictionary["paymentBankingOptionalName"] = {
                    ID: 9,
                    Key: "paymentBankingOptionalName",
                    Value: this.paymentBankingOptionalName
                }
            }
            if (this.paymentBankingOptionalTitle != null) {
                attributesDictionary["paymentBankingOptionalTitle"] = {
                    ID: 10,
                    Key: "paymentBankingOptionalTitle",
                    Value: this.paymentBankingOptionalTitle
                }
            }
            if (this.paymentBankingResponsiblePerson != null) {
                attributesDictionary["paymentBankingResponsiblePerson"] = {
                    ID: 11,
                    Key: "paymentBankingResponsiblePerson",
                    Value: this.paymentBankingResponsiblePerson
                }
            }
            if (this.paymentBankingBankName != null) {
                attributesDictionary["paymentBankingBankName"] = {
                    ID: 12,
                    Key: "paymentBankingBankName",
                    Value: this.paymentBankingBankName
                }
            }
            if (this.paymentBankingAccountType != null) {
                attributesDictionary["paymentBankingAccountType"] = {
                    ID: 13,
                    Key: "paymentBankingAccountType",
                    Value: this.paymentBankingAccountType
                }
            }
            if (this.paymentBankingBankContactFirstName != null) {
                attributesDictionary["paymentBankingBankContactFirstName"] = {
                    ID: 14,
                    Key: "paymentBankingBankContactFirstName",
                    Value: this.paymentBankingBankContactFirstName
                }
            }
            if (this.paymentBankingBankContactLastName != null) {
                attributesDictionary["paymentBankingBankContactLastName"] = {
                    ID: 15,
                    Key: "paymentBankingBankContactLastName",
                    Value: this.paymentBankingBankContactLastName
                }
            }
            if (this.paymentBankingPhone != null) {
                attributesDictionary["paymentBankingPhone"] = {
                    ID: 16,
                    Key: "paymentBankingPhone",
                    Value: this.paymentBankingPhone
                }
            }
            if (this.paymentBankingFax != null) {
                attributesDictionary["paymentBankingFax"] = {
                    ID: 17,
                    Key: "paymentBankingFax",
                    Value: this.paymentBankingFax
                }
            }
            if (this.businessReferencesName1 != null) {
                attributesDictionary["businessReferencesName1"] = {
                    ID: 18,
                    Key: "businessReferencesName1",
                    Value: this.businessReferencesName1
                }
            }
            if (this.businessReferencesContact1 != null) {
                attributesDictionary["businessReferencesContact1"] = {
                    ID: 19,
                    Key: "businessReferencesContact1",
                    Value: this.businessReferencesContact1
                }
            }
            if (this.businessReferencesEmail1 != null) {
                attributesDictionary["businessReferencesEmail1"] = {
                    ID: 20,
                    Key: "businessReferencesEmail1",
                    Value: this.businessReferencesEmail1
                }
            }
            if (this.businessReferencesPhone1 != null) {
                attributesDictionary["businessReferencesPhone1"] = {
                    ID: 21,
                    Key: "businessReferencesPhone1",
                    Value: this.businessReferencesPhone1
                }
            }
            if (this.businessReferencesFax1 != null) {
                attributesDictionary["businessReferencesFax1"] = {
                    ID: 22,
                    Key: "businessReferencesFax1",
                    Value: this.businessReferencesFax1
                }
            }
            if (this.businessReferencesName2 != null) {
                attributesDictionary["businessReferencesName2"] = {
                    ID: 23,
                    Key: "businessReferencesName2",
                    Value: this.businessReferencesName2
                }
            }
            if (this.businessReferencesContact2 != null) {
                attributesDictionary["businessReferencesContact2"] = {
                    ID: 24,
                    Key: "businessReferencesContact2",
                    Value: this.businessReferencesContact2
                }
            }
            if (this.businessReferencesEmail2 != null) {
                attributesDictionary["businessReferencesEmail2"] = {
                    ID: 25,
                    Key: "businessReferencesEmail2",
                    Value: this.businessReferencesEmail2
                }
            }
            if (this.businessReferencesPhone2 != null) {
                attributesDictionary["businessReferencesPhone2"] = {
                    ID: 26,
                    Key: "businessReferencesPhone2",
                    Value: this.businessReferencesPhone2
                }
            }
            if (this.businessReferencesFax2 != null) {
                attributesDictionary["businessReferencesFax2"] = {
                    ID: 27,
                    Key: "businessReferencesFax2",
                    Value: this.businessReferencesFax2
                }
            }
            if (this.businessReferencesName3 != null) {
                attributesDictionary["businessReferencesName3"] = {
                    ID: 28,
                    Key: "businessReferencesName3",
                    Value: this.businessReferencesName3
                }
            }
            if (this.businessReferencesContact3 != null) {
                attributesDictionary["businessReferencesContact3"] = {
                    ID: 29,
                    Key: "businessReferencesContact3",
                    Value: this.businessReferencesContact3
                }
            }
            if (this.businessReferencesEmail3 != null) {
                attributesDictionary["businessReferencesEmail3"] = {
                    ID: 30,
                    Key: "businessReferencesEmail3",
                    Value: this.businessReferencesEmail3
                }
            }
            if (this.businessReferencesPhone3 != null) {
                attributesDictionary["businessReferencesPhone3"] = {
                    ID: 31,
                    Key: "businessReferencesPhone3",
                    Value: this.businessReferencesPhone3
                }
            }
            if (this.businessReferencesFax3 != null) {
                attributesDictionary["businessReferencesFax3"] = {
                    ID: 32,
                    Key: "businessReferencesFax3",
                    Value: this.businessReferencesFax3
                }
            }
            if (this.eSignature != null) {
                attributesDictionary["eSignature"] = {
                    ID: 33,
                    Key: "eSignature",
                    Value: this.eSignature
                }
            }
            if (this.signedDate != null) {
                attributesDictionary["signedDate"] = {
                    ID: 34,
                    Key: "signedDate",
                    Value: this.signedDate.toString()
                }
            }
            if (this.signatureCheck != null) {
                attributesDictionary["signatureCheck"] = {
                    ID: 35,
                    Key: "signedDate",
                    Value: this.signatureCheck.toString()
                };
            }
            if (this.userRegistrationTypeSelectionID != null) {
                attributesDictionary["userRegistrationTypeSelectionID"] = {
                    ID: 36,
                    Key: "userRegistrationTypeSelectionID",
                    Value: this.userRegistrationTypeSelectionID.toString()
                };
            }
            if (this.basicInfoCompany != null) {
                attributesDictionary["basicInfoCompany"] = {
                    ID: 37,
                    Key: "basicInfoCompany",
                    Value: this.basicInfoCompany
                }
            }
            if (this.termsCheck != null) {
                attributesDictionary["termsCheck"] = {
                    ID: 38,
                    Key: "termsCheck",
                    Value: this.termsCheck.toString()
                };
            }
            return attributesDictionary;
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvContactFactory: factories.IContactFactory) {
            super($q, cefConfig, cvServiceStrings);
            this.consoleDebug(`RegistrationStepBasicInfo.ctor()`);
        }
    }
}
