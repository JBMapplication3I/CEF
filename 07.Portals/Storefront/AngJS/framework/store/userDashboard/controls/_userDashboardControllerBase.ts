/**
 * @file framework/store/userDashboard/controls/_userDashboardControllerBase.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved
 * @desc Base class for User Dashboard controls, includes access to the current
 * user and account
 */
module cef.store.userDashboard.controls {
    export abstract class UserDashboardControllerBase extends core.TemplatedControllerBase {
        // Properties
        get currentUser(): api.UserModel {
            return this.cvAuthenticationService &&
                this.cvAuthenticationService["currentUser"];
        }
        get currentAccount(): api.AccountModel {
            return this.cvAuthenticationService &&
                this.cvAuthenticationService["currentAccount"];
        }
        // Functions
        loadUser(): void {
            this.setRunning();
            this.cvAuthenticationService.getCurrentUserPromise()
                .then(() => {
                    this.cvAuthenticationService.getCurrentAccountPromise()
                        .then(() => this.finishRunning(),
                            reason => this.finishRunning(true, reason))
                        .catch(result => this.finishRunning(true, result));
                }, reason => this.finishRunning(true, reason))
                .catch(result => this.finishRunning(true, result));
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.cvAuthenticationService.preAuth().finally(() => this.loadUser());
        }
    }
}
