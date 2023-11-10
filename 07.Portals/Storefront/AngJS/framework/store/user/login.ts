/**
 * @file framework/store/user/login.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc login class
 */
module cef.store.user {
    export enum AfterLoginActionMode {
        DoNothing = 0,
        ReloadPage = 1,
        GoToReturnUrl = 2,
        GoToReturnState = 3
    }
    export type BoolFunc = () => boolean;
    export class LoginController extends core.TemplatedControllerBase {
        // Scope Properties
        loginCallback: (object: any) => void;
        returnUrl: string;
        reloadPage: boolean | BoolFunc;
        noReturn: BoolFunc;
        afterLoginActionMode: AfterLoginActionMode;
        modal: boolean;
        // Properties
        loginData: api.AuthProviderLoginDto = { Username: null, Password: null, RememberMe: false };
        showMFA = false;
        hideLogin: boolean;
        usePhone: boolean;
        mfaResult: api.MFARequirementsModel;
        isLoggedIn: boolean;
        thisUrl: string; // Used for the return url on the registration control when going there
        loggedInUser: any;
        emailFirstAndLastFour: string;
        // Functions
        urlDecode(str) { return decodeURIComponent((str + "").replace(/\+/g, "%20")); }
        getParameter(paramName: string): string {
            const params = window.location.search && window.location.search.substring(1)
                ? window.location.search.substring(1).split("&")
                : window.location.hash.substring(3).split("&");
            for (let i = 0; i < params.length; i++) {
                const val = params[i].split("=");
                if (val[0].toLowerCase() === paramName.toLowerCase()) {
                    return val[1];
                }
            }
            return null;
        }
        doAfterActionLoginMode(): void {
                switch (this.afterLoginActionMode) {
                    case AfterLoginActionMode.DoNothing: { /*this.consoleDebug("loginCtrl.signIn.do nothing");*/ this.finishRunning(); break; }
                    case AfterLoginActionMode.ReloadPage: { /*this.consoleDebug("loginCtrl.signIn.do reload 2");*/ this.doReload(); break; }
                    case AfterLoginActionMode.GoToReturnUrl: { /*this.consoleDebug("loginCtrl.signIn.do return");*/ this.return(); break; }
                case AfterLoginActionMode.GoToReturnState: { /*this.consoleDebug("loginCtrl.signIn.do return");*/ this.goToState(); break; }
            }
                }
        doReload(): void { this.$window.location.reload(); }
        return(): void {
            if (this.noReturn && this.noReturn()) { return; }
            if (this.returnUrl && this.returnUrl !== "" && this.returnUrl !== "null") {
                this.$filter("goToCORSLink")(this.returnUrl);
                return;
            }
            this.$filter("goToCORSLink")("/"); // Home
        }
        goToState(): void {
            if (this.returnUrl && this.returnUrl !== "" && this.returnUrl !== "null" && this.$state.href(this.returnUrl)) {
                this.$state.go(this.returnUrl);
                return;
            }
            this.$state.go("home"); // Home
        }
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
        cancel(): void { this.return(); }
        submit(usePhone?: boolean, state?: string): void {
            this.setRunning(); // Finish Running will be called for either an error state or when the page reloads, etc.
            // this.consoleDebug("loginCtrl.login.started");
            if (this.cefConfig.authProviderMFAEnabled) {
                this.cvAuthenticationService.loginRequiresMFA(this.loginData).then(requires => {
                    this.mfaResult = requires.Result;
                    console.log(this.mfaResult);
                    if (this.mfaResult.Email && this.mfaResult.EmailFirstAndLastFour) {
                        this.emailFirstAndLastFour = this.mfaResult.EmailFirstAndLastFour
                            .replace(/\*/gi, "&bull;");
                    }
                    if (requires && (this.mfaResult.Phone || this.mfaResult.Email) && !this.showMFA) {
                        this.hideLogin = true;
                        this.showMFA = true;
                        this.finishRunning();
                        return;
                    }
                    this.cvAuthenticationService.login(this.loginData).then(() => {
                        /* Do Nothing, sign in event is listed to */
                        // this.consoleDebug("loginCtrl.login.finished");
                    }).catch(reason => {
                        let fallBackMessage = "Invalid UserName or Password";
                        this.$translate("ui.storefront.login.InvalidUserNameOrPassword").then(r => {
                            fallBackMessage = r;
                        }).finally(() => {
                            let message = reason.data?.ResponseStatus?.Message || reason.data?.Message || fallBackMessage;
                            if (angular.isString(message) && message.indexOf("Log ID:") !== -1) {
                                // Strip the log guid
                                message = (<string>message).replace(/Log ID: (CEF: )?.{8}-.{4}-.{4}-.{4}-.{12} \| /g, "");
                            }
                            this.finishRunning(true, message);
                            if (angular.isFunction(this.loginCallback)) {
                                this.loginCallback({ loginSuccessful: false });
                            }
                        });
                    });
                });
            } else {
                this.cvAuthenticationService.login(this.loginData).then(() => {
                    /* Do Nothing, sign in event is listed to */
                    // this.consoleDebug("loginCtrl.login.finished");
                }).catch(reason => {
                    let fallBackMessage = "Invalid UserName or Password";
                    this.$translate("ui.storefront.login.InvalidUserNameOrPassword").then(r => {
                        fallBackMessage = r;
                    }).finally(() => {
                        let message = reason.data?.ResponseStatus?.Message || reason.data?.Message || fallBackMessage;
                        if (angular.isString(message) && message.indexOf("Log ID:") !== -1) {
                            // Strip the log guid
                            message = (<string>message).replace(/Log ID: (CEF: )?.{8}-.{4}-.{4}-.{4}-.{12} \| /g, "");
                        }
                        this.finishRunning(true, message);
                        if (angular.isFunction(this.loginCallback)) {
                            this.loginCallback({ loginSuccessful: false });
                        }
                    });
                });
            }
        }
        // Events
        userLoggedIn(): void {
            if (this.cvAuthenticationService.isAuthenticated()) {
                this.isLoggedIn = true;
                this.cvAuthenticationService.getCurrentUserPromise()
                    .then(user => {
                        this.loggedInUser = user;
                        this.doAfterActionLoginMode();
                    });
            }
        }
        keySubmit(evt): void {
            if (!evt || !evt.keyCode || evt.keyCode !== 13) { return; }
            if (!this.forms.login) { return; }
            this.forms.login.$setDirty();
            if (this.forms.login.$invalid) { return; }
            this.submit(null, "home");
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                readonly $state: ng.ui.IStateService,
                readonly $scope: ng.IScope,
                private readonly $filter: ng.IFilterService,
                private readonly $window: ng.IWindowService,
                private readonly $location: ng.ILocationService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly $translate: ng.translate.ITranslateService) {
            super(cefConfig);
            let locationReturnUrl = this.$location.absUrl().split("returnUrl=");
            if (locationReturnUrl.length > 1) {
                this.returnUrl = decodeURIComponent(this.$location.absUrl().split("returnUrl=")[1]);
            } else {
                this.returnUrl = this.returnUrl || this.urlDecode(this.getParameter("returnUrl"));
            }
            if (!this.returnUrl || this.returnUrl === "null") { this.returnUrl = "/"; }
            this.thisUrl = encodeURIComponent($window.location.pathname + $window.location.search);
            this.cvAuthenticationService.preAuth().finally(() => this.userLoggedIn());
            const unbind1 = $scope.$on(cvServiceStrings.events.auth.signIn, () => {
                // this.consoleDebug("loginCtrl.signIn.detected");
                this.setRunning();
                this.userLoggedIn();
                if (angular.isFunction(this.loginCallback)) {
                    // this.consoleDebug("loginCtrl.signIn.calling login callback");
                    this.loginCallback({ loginSuccessful: true });
                }
                if ((this.reloadPage === true
                     || angular.isFunction(this.reloadPage) && (<BoolFunc>this.reloadPage)())
                    && this.afterLoginActionMode !== AfterLoginActionMode.GoToReturnUrl) {
                    // this.consoleDebug("loginCtrl.signIn.do reload");
                    this.doReload();
                    return;
                }
                this.doAfterActionLoginMode();
            });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefLogin", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { loginCallback: "&", returnUrl: "@?", reloadPage: "=", noReturn:"&", afterLoginActionMode: "=", modal: "=?" },
        templateUrl: $filter("corsLink")("/framework/store/user/login.html", "ui"),
        controller: LoginController,
        controllerAs: "loginCtrl",
        bindToController: true
    }));

    cefApp.directive("cefPurchaseLogin", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { loginCallback: "&", returnUrl: "@?", reloadPage: "=", noReturn: "&", afterLoginActionMode: "=" },
        templateUrl: $filter("corsLink")("/framework/store/user/login.purchase.html", "ui"),
        controller: LoginController,
        controllerAs: "loginCtrl",
        bindToController: true
    }));
}
