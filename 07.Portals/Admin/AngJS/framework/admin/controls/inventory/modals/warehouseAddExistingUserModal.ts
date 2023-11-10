module cef.admin.controls.inventory.modals {
    export class WarehouseAddExistingUserModalController {
        // Properties
        userText: string;
        user: api.UserModel;
        selectableUsers: api.UserModel[];
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
                LockoutEnabled: false
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
            this.reloadSelectables();
        }
        reloadSelectables(): void {
            this.cvApi.contacts.GetUsers({
                Active: true,
                AsListing: true,
                Paging: <api.Paging>{ Size: 10, StartIndex: 1 },
                IDOrUserNameOrCustomKeyOrEmailOrContactName: this.userText
            }).then(r => this.selectableUsers = r.data.Results);
        }
        save(): void {
            this.$uibModalInstance.close(this.user);
        }
        close(): void {
            this.$uibModalInstance.dismiss("cancel");
        }

        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                private readonly cvApi: api.ICEFAPI) {
            this.load();
        }
    }
}
