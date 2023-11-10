/**
 * @file framework/store/userDashboard/controls/profiles/userProfile.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc User profile controller class
 */
module cef.store.userDashboard.controls.profiles {
    class UserProfileController extends core.TemplatedControllerBase {
        // Properties
        get currentUser(): api.UserModel {
            return this.cvAuthenticationService["currentUser"];
        }
        password: string;
        newPassword: string;
        usePhonePrefixLookups = false;
        status: string;
        // Functions
        load(): void {
            this.setRunning();
            this.cvAuthenticationService.preAuth()
                .then(() => {
                    this.cvAuthenticationService.getCurrentUserPromise();
                    this.finishRunningWithStatus("loaded user");
                });
        }
        runPhonePrefixLookup(): void {
            if (!this.usePhonePrefixLookups // Don't use this
                || !this.currentUser.Contact.Phone1 // Must have content
                || new RegExp("0+").exec(this.currentUser.Contact.Phone1).length > 0 // Must not be all 0's
                || new RegExp("\d+").exec(this.currentUser.Contact.Phone1).length <= 0 // Must have a number in the value
            ) { return; }
            const cleanPhone = `+${this.currentUser.Contact.Phone1.trim().replace(new RegExp("[a-zA-Z\s)(+_-]+"), "")}`;
            this.cvApi.geography.ReversePhonePrefixToCityRegionCountry({ Prefix: cleanPhone }).then(r => {
                if (!removeEventListener || !r.data || !r.data.Results) {
                    return;
                }
                if (r.data.Results.length <= 0) {
                    return;
                }
                var result = r.data.Results[0];
                this.currentUser.Contact.Address.CountryID = result.CountryID;
                this.currentUser.Contact.Address.RegionID = result.RegionID;
                this.currentUser.Contact.Address.City = result.CityName;
            });
        }
        // NOTE: These two functions must remain arrow functions as they are used in callbacks
        onImageUploaded = (): void => {
            this.forms["User"].$setDirty();
        }
        onStoredFileUploaded = (): void => {
            this.forms["User"].$setDirty();
        }
        removeImage(index: number): void {
            this.currentUser.Images.splice(index, 1);
            this.forms["User"].$setDirty();
        }
        removeStoredFile(index: number): void {
            this.currentUser.StoredFiles.splice(index, 1);
            this.forms["User"].$setDirty();
        }
        private finishRunningWithStatus(
                status: string,
                hasError: boolean = false,
                errorMessage: core.IErrorMessageArg = null,
                errorMessages: string[] = null): void {
            this.finishRunning(hasError, errorMessage, errorMessages);
            this.status = status;
            if (status !== "done") { return; }
            this.password = null;
            this.newPassword = null;
            this.forms["User"].$setPristine();
            this.$state.reload();
        }
        save(): void {
            this.setRunning();
            this.status = "saving";
            this.cvAuthenticationService.validatePassword({
                UserName: this.currentUser.UserName,
                Password: this.password
            }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunningWithStatus("error", true, "Invalid Password");
                    throw "Invalid Password";
                }
                if (this.newPassword && this.newPassword !== "") {
                    const dto = {
                        UserName: this.currentUser.UserName,
                        Password: this.password,
                        NewPassword: this.newPassword
                    };
                    this.cvAuthenticationService.changePassword(dto).then(r2 => {
                        if (!r2 || !r2.data || !r2.data.ActionSucceeded) {
                            this.finishRunningWithStatus("error", true, "Invalid Password");
                            throw "Invalid Password";
                        }
                        this.cvAuthenticationService.updateCurrentUser(this.currentUser).then(r3 => {
                            if (!r3 || !r3.ActionSucceeded) {
                                this.finishRunningWithStatus("error", true, null, r3 && r3.Messages);
                                return;
                            }
                            this.finishRunningWithStatus("done");
                        }).catch(reason3 => this.finishRunningWithStatus("error", true, reason3));
                    }).catch(reason2 => this.finishRunningWithStatus("error", true, reason2));
                    return;
                }
                this.cvAuthenticationService.updateCurrentUser(this.currentUser).then(r3 => {
                    if (!r3 || !r3.ActionSucceeded) {
                        this.finishRunningWithStatus("error", true, null, r3 && r3.Messages);
                        return;
                    }
                    this.finishRunningWithStatus("done");
                }).catch(reason2 => this.finishRunningWithStatus("error", true, reason2));
            }).catch(reason1 => this.finishRunningWithStatus("error", true, reason1));
        }
        cancel(): void {
            this.status = "cancelling";
            this.cvAuthenticationService.getCurrentUserPromise(true);
        }
        clickNotificationSettiings(param: any): void {
            if (param == 'TEXT') {
                // Get TEXT notification on product change
            }
            if (param == 'MODAL') {
                // Get MODAL notifcation on product change
            }
        }
        // calculate age function
        private convertBirthdayToAge(birthday: Date) {
            const ageDifMs = Date.now() - birthday.getTime();
            const ageDate = new Date(ageDifMs); // milliseconds from epoch
            return Math.abs(ageDate.getUTCFullYear() - 1970);
        }
        // Constructor
        constructor(
                private readonly $state: ng.ui.IStateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefUserProfile", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { user: "=", usePhonePrefixLookups: "@?" },
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/profiles/userProfile.html", "ui"),
        controller: UserProfileController,
        controllerAs: "userProfileCtrl",
        bindToController: true
    }));
}
