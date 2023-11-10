module cef.admin.controls.accounts.modals {
    export class AccountAddNewUserModalController {
        // Properties
        user: api.UserModel;
        types: api.TypeModel[];
        statuses: api.StatusModel[];
        // Functions
        private createBlankUser(): api.UserModel {
            return <api.UserModel>{
                Active: true,
                CreatedDate: new Date(),
                Contact: <api.ContactModel>{
                    Active: true,
                    CreatedDate: new Date(),
                    SameAsBilling: false,
                    TypeKey: "User",
                    Address: this.createNewBlankAddress()
                },
                BillingAddress: this.createNewBlankAddress(),
                IsDeleted: false,
                IsSuperAdmin: false,
                IsEmailSubscriber: false,
                IsCatalogSubscriber: false,
                AccessFailedCount: 0,
                EmailConfirmed: false,
                PhoneNumberConfirmed: false,
                TwoFactorEnabled: false,
                LockoutEnabled: false,
                RequirePasswordChangeOnNextLogin: true,
            };
        }
        private createNewBlankAddress(): api.AddressModel {
            return <api.AddressModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                IsBilling: false,
                IsPrimary: false,
            };
        }
        load(): void {
            this.user = this.createBlankUser();
            this.cvApi.contacts.GetUserTypes({ Active: true, AsListing: true }).then(r => this.types = r.data.Results);
            this.cvApi.contacts.GetUserStatuses({ Active: true, AsListing: true }).then(r => this.statuses = r.data.Results);
        }
        save(): void {
            this.$uibModalInstance.close(this.user);
        }
        close(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Events
        // NOTE: This must remain an arrow function for angular events
        emailChanged = ($event: ng.IAngularEvent, control: ng.INgModelController) => {
            if (control.$valid) {
                this.user.Contact.Email1 = control.$modelValue;
            }
            if (this.cefConfig.featureSet.registration.usernameIsEmail) {
                this.usernameChanged($event, control, true);
            }
        }

        customkeyChanged = ($event: ng.IAngularEvent, control: ng.INgModelController) => {
            control.$setValidity("customkey",  true );
            control.$setValidity("ckisdupe",   true );
            control.$setValidity("ckbyserver", true );
            if (!control.$modelValue || control.$modelValue === "") {
                control.$setValidity("customkey",  false );
                control.$setValidity("ckisdupe",   false );
                control.$setValidity("ckbyserver", false );
            }
            if (control.$invalid) {
                control.$setValidity("ckbyserver", true );
                control.$setValidity("customkey",  false );
            }
            this.cvAuthenticationService.validateCustomKeyIsUnique({
                CustomKey: control.$modelValue
            }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    if (r && r.data && r.data.Messages 
                        && r.data.Messages[0] === "An entry with that key already exists.") {
                        control.$setValidity("ckisdupe", false);
                    } else {
                        control.$setValidity("ckbyserver", false);
                    }
                    return;
                }
                control.$setValidity("ckbyserver", true);
                this.user.CustomKey = control.$modelValue;
            }).catch(() => control.$setValidity("ckbyserver", false))
            .finally(() => control.$setValidity("customkey", control.$valid));
        }

        // NOTE: This must remain an arrow function for angular events
        usernameChanged = ($event: ng.IAngularEvent, control: ng.INgModelController, fromEmailChanged: boolean = false) => {
            if (!fromEmailChanged && control.$valid) {
                this.user.CustomKey = control.$modelValue;
            }
            // if (this.cefConfig.featureSet.registration.usernameIsEmail) {
            //     this.usernameValidityState = "Valid";
            //     return;
            // }
            control.$setValidity("username",   true);
            control.$setValidity("unisdupe",   true);
            control.$setValidity("unbyserver", true);
            if (!control.$modelValue || control.$modelValue === "") {
                control.$setValidity("required",    false);
                control.$setValidity("username",    false);
                control.$setValidity("unminlength", false);
                control.$setValidity("unformat",    false);
                control.$setValidity("unbyserver",  false);
                return;
            }
            control.$setValidity("unminlength", control.$modelValue.length >= 5);
            control.$setValidity("unformat", (/^[A-Za-z0-9_\.@]+$/.test(control.$modelValue)));
            if (control.$invalid) {
                control.$setValidity("unbyserver",  true);
                control.$setValidity("username",    false);
                return;
            }
            this.cvAuthenticationService.validateUserNameIsGood({
                UserName: control.$modelValue
            }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    if (r && r.data && r.data.Messages && r.data.Messages[0] === "This username is already taken") {
                        control.$setValidity("unisdupe", false);
                    } else {
                        control.$setValidity("unbyserver", false);
                    }
                    return;
                }
                control.$setValidity("unbyserver", true);
                if (this.cefConfig.featureSet.registration.usernameIsEmail && fromEmailChanged) {
                    this.user.CustomKey = this.user.UserName = control.$modelValue;
                }
                // Check local users
                if (this.users) {
                    control.$setValidity("unisdupe", !_.some(this.users, user => user.UserName === this.user.UserName));
                }
            }).catch(() => control.$setValidity("unbyserver", false))
            .finally(() => control.$setValidity("username", control.$valid));
        }
        // Constructor
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                private readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly users: api.UserModel[]) {
            this.load();
        }
    }
}
