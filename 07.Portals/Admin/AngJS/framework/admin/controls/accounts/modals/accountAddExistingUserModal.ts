module cef.admin.controls.accounts.modals {
    export class AccountAddExistingUserModalController {
        // Properties
        userText: string;
        user: api.UserModel;
        selectableUsers: api.UserModel[];
        // Functions
        load(): void {
            this.reloadSelectables();
        }
        reloadSelectables(): void {
            this.cvApi.contacts.GetUsers({
                Active: true,
                AsListing: true,
                Paging: <api.Paging>{ Size: 10, StartIndex: 1 },
                IDOrUserNameOrCustomKeyOrEmailOrContactName: this.userText
            }).then(r => this.selectableUsers = r.data.Results.filter(u => this.users.every(x => x.ID !== u.ID)));
        }
        save(): void {
            this.users.push(this.user);
            this.$uibModalInstance.close();
        }
        close(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                private readonly cvApi: api.ICEFAPI,
                private readonly users: api.UserModel[]) {
            this.load();
        }
    }
}
