/**
 * @file framework/admin/controls/login.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc CEF Admin Login controller class
 */
module cef.admin.controls {
    class LoginController extends core.TemplatedControllerBase {
        // Properties
        readonly viewStateName = "Login";
        initialLoad = true;
        loginData = <api.AuthProviderLoginDto>{ Username: null, Password: null };
        showMFA = false;
        hideLogin: boolean;
        usePhone: boolean;
        mfaResult: api.MFARequirementsModel;
        emailFirstAndLastFour: string;
        // Functions
        requestMFATokenViaEmail(): void {
            this.setRunning();
            this.cvAuthenticationService.requestMFA(this.loginData, false).then(success => {
                this.hideLogin = false;
                //this.showMFA = false;
                this.finishRunning();
            });
        }
        requestMFATokenViaSMS(): void {
            this.setRunning();
            this.cvAuthenticationService.requestMFA(this.loginData, true).then(success => {
                this.hideLogin = false;
                //this.showMFA = false;
                this.finishRunning();
            });
        }
        doLogin(usePhone?: boolean, state?: string): void {
            this.cvViewStateService.setRunning(
                this.viewStateName,
                this.$translate("ui.admin.common.LoggingIn.Ellipses"));
                if (this.cefConfig.authProviderMFAEnabled) {
            this.cvAuthenticationService.loginRequiresMFA(this.loginData).then(requires => {
                this.mfaResult = requires.Result;
                if (this.mfaResult.Email && this.mfaResult.EmailFirstAndLastFour) {
                    this.emailFirstAndLastFour = this.mfaResult.EmailFirstAndLastFour
                        .replace(/\*/gi, "&bull;");
                }
                if (requires && !this.showMFA && (this.mfaResult.Phone || this.mfaResult.Email)) {
                    this.hideLogin = true;
                    this.showMFA = true;
                    this.finishRunning();
                    return;
                }
                this.initialLoad = false;
                this.cvAuthenticationService.login(this.loginData)
                    .then(() => this.validate(state))
                    .catch(reason => this.cvViewStateService.finishRunning(
                        this.viewStateName,
                        true,
                        reason));
            }).catch(reason => this.cvViewStateService.finishRunning(
                this.viewStateName,
                true,
                reason));
            } else {
                this.initialLoad = false;
                this.cvAuthenticationService.login(this.loginData)
                    .then(() => this.validate(state))
                    .catch(reason => this.cvViewStateService.finishRunning(
                        this.viewStateName,
                        true,
                        reason));
            }
        }
        validate(state: string): void {
            this.cvViewStateService.setRunning(
                this.viewStateName,
                this.$translate("ui.admin.common.CheckingLoginStatus.Ellipses"));
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    // Do Nothing, we are on the login page and not logged in
                    this.cvViewStateService.finishRunning(this.viewStateName);
                    return;
                }
                // We are logged in already, check for Admin permissions
                this.cvAuthenticationService.getCurrentUserPromise(true).then(() => {
                    this.cvSecurityService.hasPermissionPromise("/^Admin\\..*/").then(has => {
                        if (!has) {
                            this.cvViewStateService.finishRunning(
                                this.viewStateName,
                                true,
                                this.$translate("ui.admin.controls.login.NotEnoughPermissions.Message"));
                            return;
                        }
                        // We are logged in and have permissions
                        this.cvViewStateService.finishRunning(this.viewStateName);
                        this.$state.go(state || "home");
                    });
                });
            });
        }
        showWait(): boolean {
            return this.cvViewStateService.isRunning() &&
                this.cvViewStateService.hasWaitMessage();
        }
        showError(): boolean {
            if (this.initialLoad) {
                return false;
            }
            // There are no errors
            if (!this.cvViewStateService.hasError()) {
                return false;
            }
            const messages = this.cvViewStateService.errorMessages();
            // There is an error but there are no messages (indicates the 401 only from preAuth)
            if (!messages || !messages.length) {
                return false;
            }
            // There is more than one message
            if (messages.length > 1) {
                return true;
            }
            // Ignore this specific error message when by itself
            return messages[0] !== "401: Unauthorized: No active user in session.";
        }
        // Events
        keySubmit($event: ng.IAngularEvent): void {
            if (!$event || !$event["keyCode"] || $event["keyCode"] !== 13) {
                return;
            }
            $event.preventDefault();
            $event.stopPropagation();
            if (!this.loginData ||
                !this.loginData.Username ||
                !this.loginData.Password) {
                return;
            }
            this.doLogin(null, "home");
        }
        // Constructor
        constructor(
                readonly $timeout: ng.ITimeoutService,
                private readonly $state: ng.ui.IStateService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvSecurityService: services.ISecurityService,
                private readonly cvViewStateService: services.IViewStateService) {
            super(cefConfig);
            // TODO: Read a return url/state value
            this.validate(null);
            // Focus the username input so the user can start typing immediately
            $timeout(() => angular.element("#txtLoginUsername").focus(), 250);
        }
    }

    adminApp.directive("cefAdminLogin", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/login.html", "ui"),
        controller: LoginController,
        controllerAs: "loginCtrl"
    }));
}
