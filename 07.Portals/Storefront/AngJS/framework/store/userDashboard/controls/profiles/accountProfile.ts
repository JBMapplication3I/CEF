/**
 * @file framework/store/userDashboard/controls/profiles/accountProfile.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Account profile controller class
 */
module cef.store.userDashboard.controls.profiles {
    class AccountProfileController extends core.TemplatedControllerBase {
        // Properties
        get currentAccount(): api.AccountModel {
            return this.cvAuthenticationService["currentAccount"];
        }
        status: string;
        accountDetails: boolean;
        accountDetailsEdit: boolean;
        // Functions
        load(): void {
            // Initial load to ensure data is present
            this.cvAuthenticationService.getCurrentAccountPromise().finally(() => { /* Do Nothing */ });
        }
        removeImage(index: number): void {
            this.currentAccount.Images.splice(index, 1);
            this.forms["Account"].$setDirty();
        }
        removeStoredFile(index: number): void {
            this.currentAccount.StoredFiles.splice(index, 1);
            this.forms["Account"].$setDirty();
        }
        save(): void {
            this.status = "saving";
            const copy = this.currentAccount;
            delete copy.AccountContacts; // Don't override the address book on the back end
            this.cvApi.accounts.UpdateCurrentAccount(copy).then(r => {
                // Reset the data now that it's updated on the server
                this.cvAuthenticationService.getCurrentAccountPromise(true).then(() => {
                    this.status = "done";
                    this.accountDetailsEdit = false;
                    this.finishRunning();
                }).catch(reason => {
                    this.status = "error";
                    this.finishRunning(true, reason);
                });
            }).catch(reason => {
                this.status = "error";
                this.finishRunning(true, reason);
            });
        }
        cancel(): void {
            this.status = "cancelling";
            // Reset the data to just what the server has
            this.cvAuthenticationService.getCurrentAccountPromise(true).then(() => { });
            this.accountDetailsEdit = false;
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.load();
            this.accountDetails = true;
            this.accountDetailsEdit = false;
        }
    }

    cefApp.directive("cefAccountProfile", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/profiles/accountProfile.html", "ui"),
        controller: AccountProfileController,
        controllerAs: "accountProfileCtrl"
    }));
}
