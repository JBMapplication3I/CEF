/**
 * @file framework/store/_services/cvAuthenticationService.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Authentication Service, provides calls to manage login status and
 * things like forgot password/username, registration, etc.
 */
module cef.store.services {
    export class ExtendedUserModel implements api.UserModel {
        ID: number;
        // UserModel Properties
        EmailConfirmed: boolean;
        PhoneNumberConfirmed: boolean;
        TwoFactorEnabled: boolean;
        LockoutEnabled: boolean;
        AccessFailedCount: number;
        IsDeleted: boolean;
        IsSuperAdmin: boolean;
        IsEmailSubscriber: boolean;
        IsCatalogSubscriber: boolean;
        Active: boolean;
        CreatedDate: Date;
        StatusID: number;
        TypeID: number;
        ContactID: number;
        Contact: api.ContactModel;
        IsApproved: boolean;
        RequirePasswordChangeOnNextLogin: boolean;
        IsSMSAllowed: boolean;
        DateOfBirth?: Date;
        Gender?: string;
        UseAutoPay: boolean;
        // Extended Properties
        userID: number;
        username: string;
        displayName: string;
        isAuthenticated: boolean;
        SerializableAttributes: api.SerializableAttributesDictionary;
        // Constructor
        constructor(user?: api.UserModel) {
            if (user) {
                _.extend(this, user);
                this.userID = user.ID;
                this.username = user.UserName;
                this.displayName = user.DisplayName;
                this.isAuthenticated = true;
                return;
            }
            this.userID = null;
            this.username = "";
            this.displayName = "";
            this.isAuthenticated = false;
        }
    }

    export interface IServiceStackAuthResponse { status: number; }

    export interface IAuthenticationService {
        isAuthenticated: () => boolean;
        login: (loginData: api.AuthProviderLoginDto) => ng.IPromise<void>;
        loginRequiresMFA: (loginData: api.AuthProviderLoginDto) => ng.IPromise<api.CEFActionResponseT<api.MFARequirementsModel>>;
        requestMFA: (loginData: api.AuthProviderLoginDto, usePhone: boolean) => ng.IPromise<boolean>;
        preAuth: () => ng.IPromise<void>;
        logout: (redirectToHome?: boolean /* default: false */) => ng.IPromise<void>;
        getCurrentUserPromise: (force?: boolean) => ng.IPromise<ExtendedUserModel>;
        getCurrentAccountPromise: (force?: boolean) => ng.IPromise<api.AccountModel>;
        forgotUsername: (data: api.ForgotUsernameDto) => ng.IHttpPromise<api.CEFActionResponse>;
        forgotPassword: (data: api.ForgotPasswordDto) => ng.IHttpPromise<api.CEFActionResponse>;
        forgotPasswordReturn: (data: api.ForgotPasswordReturnDto) => ng.IHttpPromise<api.CEFActionResponseT<string>>;
        forcedPasswordReset: (data: api.ForcedPasswordResetDto) => ng.IHttpPromise<api.CEFActionResponse>;
        sendInvitation: (data: api.SendInvitationDto) => ng.IHttpPromise<api.CEFActionResponse>;
        approveUserRegistration: (data: api.ApproveUserRegistrationDto) => ng.IHttpPromise<api.CEFActionResponse>;
        validateUserNameIsGood: (data: api.ValidateUserNameIsGoodDto) => ng.IHttpPromise<api.CEFActionResponse>;
        validateEmailIsUnique: (data: api.ValidateEmailIsUniqueDto) => ng.IHttpPromise<api.CEFActionResponse>;
        validatePasswordIsGood: (data: api.ValidatePasswordIsGoodDto) => ng.IHttpPromise<api.CEFActionResponse>;
        validatePassword: (data: api.ValidatePasswordDto) => ng.IHttpPromise<api.CEFActionResponse>;
        confirmEmail: (data: api.ValidateEmailDto) => ng.IHttpPromise<api.CEFActionResponse>;
        changePassword: (data: api.ChangePasswordDto) => ng.IHttpPromise<api.CEFActionResponse>;
        createUser: (data: api.CreateUserDto) => ng.IHttpPromise<api.CEFActionResponseT<number>>;
        register: (data: api.RegisterNewUserDto) => ng.IHttpPromise<api.CEFActionResponse>;
        updateCurrentUser: (data: api.UpdateCurrentUserDto) => ng.IPromise<api.CEFActionResponse>;
        getUserByUsername: (username: string) => ng.IHttpPromise<api.UserModel>;
        checkUserExistsByUsername: (username: string) => ng.IHttpPromise<number>;
        setCurrentUser: (userID: string | number, username: string, displayName: string, user?: api.UserModel) => void;
    }

    export class AuthenticationService implements IAuthenticationService {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        // Properties
        private currentUser: ExtendedUserModel;
        private currentAccount: api.AccountModel;
        private mfaResult: api.CEFActionResponseT<api.MFARequirementsModel>;
        private readonly viewStateName = "AuthenticationService";
        private getPromiseInstance: {
            user?: ng.IPromise<ExtendedUserModel>;
            account?: ng.IPromise<api.AccountModel>;
        } = { };
        // Functions
        isAuthenticated(): boolean { return this.currentUser && this.currentUser.isAuthenticated; }
        getCurrentUserPromise(force?: boolean): ng.IPromise<ExtendedUserModel> {
            ////const debugMsg = `getCurrentUserPromise(force: ${force || false})`;
            ////this.consoleDebug(debugMsg);
            const missing = !this.currentUser;
            /* If we are not forcing a refresh of the information and we already have the
             * information, then just return what we already have instead of making another call */
            if (!force && !missing) {
                ////this.consoleDebug(debugMsg + ": !force && !missing : exit with flat resolve");
                return this.$q.resolve(this.currentUser);
            }
            /* If we don't have the data yet, but already have a promise out, then just return that
             * promise instead of making another call */
            if (!force && missing && this.getPromiseInstance.user) {
                ////this.consoleDebug(debugMsg + ": !force && missing && existing promise : exit with existing promise");
                return this.getPromiseInstance.user;
            }
            /* Otherwise we don't have the data or a promise out so we need to make one */
            ////this.consoleDebug(debugMsg + ": force || missing || !existing promise : exit with making new promise");
            return this.getPromiseInstance.user = this.$q((resolve, reject) => {
                ////this.consoleDebug(debugMsg + ": promise : start");
                this.preAuth().finally(() => {
                    ////this.consoleDebug(debugMsg + ": promise : preAuth : authed, calling API for user");
                    /*
                    if (!this.isAuthenticated()) {
                        this.currentUser = null;
                        reject();
                        delete this.getPromiseInstance.user;
                        return;
                    }
                    */
                    this.cvApi.contacts.GetCurrentUser().then(r => {
                        ////this.consoleDebug(debugMsg + ": promise : preAuth : current user : replied");
                        if (!r || !r.data) {
                            ////this.consoleDebug(debugMsg + ": promise : preAuth : current user : replied : exit with error");
                            this.errorCallback(reject, JSON.stringify(r.data), true);
                            return;
                        }
                        ////this.consoleDebug(debugMsg + ": promise : preAuth : current user : replied : exit after setting model");
                        this.setCurrentUserByModel(r.data);
                        resolve(this.currentUser);
                    }).catch(reject)
                    .finally(() => delete this.getPromiseInstance.user);
                });
            });
        }
        getCurrentAccountPromise(force?: boolean): ng.IPromise<api.AccountModel> {
            const missing = !this.currentAccount;
            /* If we are not forcing a refresh of the information and we already have the
             * information, then just return what we already have instead of making another call */
            if (!force && !missing) {
                return this.$q.resolve(this.currentAccount);
            }
            /* If we don't have the data yet, but already have a promise out, then just return that
             * promise instead of making another call */
            if (!force && missing && this.getPromiseInstance.account) {
                return this.getPromiseInstance.account;
            }
            /* Otherwise we don't have the data or a promise out so we need to make one */
            return this.getPromiseInstance.account = this.$q((resolve, reject) => {
                this.preAuth().finally(() => {
                    if (!this.isAuthenticated()) {
                        this.currentAccount = null;
                        reject(this.$translate("ui.storefront.common.AccountNotFound"));
                        delete this.getPromiseInstance.account;
                        return;
                    }
                    this.cvApi.accounts.GetCurrentAccount().then(r => {
                        if (!r) {
                            this.errorCallback(reject, JSON.stringify(r.data), true);
                            return;
                        }
                        // NOTE: Account returns 204 when not set to the user (or not logged in)
                        // and that's considered ok. So we're not checking "!r.data"
                        this.setCurrentAccountByModel(r.data);
                        resolve(this.currentAccount);
                    }).catch(reject)
                    .finally(() => delete this.getPromiseInstance.account);
                });
            });
        }

        private clearCurrentUser(): void {
            this.currentUser = new ExtendedUserModel();
        }
        private clearCurrentUserCarts(): void {
            const domain = this.cefConfig.routes.api.host.replace(/^(https?:\/\/)?api/, "");
            const cookieNames = ["cef_cart_quote", "cef_cart_shopping", "cef_cart_compare"];
            const cookieOptions = { domain, path: '/' };
            for (const cookieName of cookieNames) {
                this.$cookies.remove(cookieName, cookieOptions);
            }
        }
        setCurrentUser(userID: string | number, username: string, displayName: string, user?: api.UserModel): void {
            if (user) {
                this.currentUser = new ExtendedUserModel(user);
            }
            if (!this.currentUser) {
                return;
            }
            this.currentUser.userID = Number(userID);
            this.currentUser.username = username;
            this.currentUser.displayName = displayName;
            this.currentUser.isAuthenticated = this.currentUser.userID > 0
                && this.currentUser.username != null
                && this.currentUser.username !== "";
        }
        private setCurrentUserByModel(user: api.UserModel): void {
            if (user) {
                this.currentUser = new ExtendedUserModel(user);
            }
            this.currentUser.isAuthenticated = this.currentUser.userID > 0
                && this.currentUser.username != null
                && this.currentUser.username !== "";
        }
        private setCurrentAccountByModel(account: api.AccountModel): void {
            this.currentAccount = account;
            this.$rootScope.$broadcast(this.cvServiceStrings.events.account.loaded);
        }

        private errorCallback(reject: ng.IQResolveReject<any>, result: any, clearUser: boolean, doThrow = false) {
            if (clearUser) {
                this.clearCurrentUser();
            }
            reject(result);
            this.loginPromise = null;
            this.preAuthPromise = null;
            this.cvViewStateService.finishRunning(this.viewStateName, true, result);
            if (doThrow) {
                throw Error(result);
            }
        }

        private preAuthPromise: ng.IHttpPromise<void | any> = null;
        preAuth(): ng.IPromise<void | any> {
            if (this.isAuthenticated()) {
                // Already authenticated, no need to preAuth again
                return this.$q.resolve();
            }
            if (this.preAuthPromise) {
                // It's already running, don't fire a second time
                return this.preAuthPromise;
                ////return this.$q((resolve, reject) => {
                ////    this.preAuthPromise
                ////        .then(() => resolve(this.preAuth()))
                ////        .catch(reason => this.errorCallback(reject, reason, false));
                ////});
            }
            this.cvViewStateService.setRunning(
                this.viewStateName,
                this.$translate("ui.storefront.common.CheckingLoginStatus.Ellipses"));
            return this.preAuthPromise = this.$q<void | any>((resolve, reject) => {
                if (this.isAuthenticated()) {
                    // Already authenticated, no need to preAuth again
                    resolve();
                    return;
                }
                const provider = this.cefConfig.authProvider.indexOf(",") !== -1
                    ? this.cefConfig.authProvider.split(",")[0]
                    : this.cefConfig.authProvider;
                switch (provider) {
                    case this.cvServiceStrings.auth.providers.dnnSSO: {
                        let el1 = this.$document.find("#cef_dnn_hdnCurrentUsername");
                        // Added for backwards compatibility. This ID is deprecated
                        if (!el1.length) {
                            el1 = this.$document.find("#dnn_hdnCurrentUsername");
                        }
                        if (!el1.length) {
                            this.errorCallback(reject, this.cvServiceStrings.auth.errors.unableToDetectDNNLogin, true, false);
                            this.logout();
                            return;
                        }
                        let el2 = this.$document.find("#cef_dnn_hdnCurrentToken");
                        // Added for backwards compatibility. This ID is deprecated
                        if (!el2.length) {
                            el2 = this.$document.find("#dnn_hdnCurrentToken");
                        }
                        if (!el2.length) {
                            this.errorCallback(reject, this.cvServiceStrings.auth.errors.unableToDetectDNNLogin, true, false);
                            this.logout();
                            return;
                        }
                        // TODO: Initiate a 30 minute timer to alert the user they need to
                        // refresh the page to update the token (maybe a 5 minute warning too)
                        if (this.loginPromise) {
                            // It's already running, don't fire a second time
                            this.loginPromise.then(() => {
                                resolve();
                                this.cvViewStateService.finishRunning(this.viewStateName);
                            }).catch(reason => this.errorCallback(reject, reason, true));
                            return;
                        }
                        const dnnUsername = el1.val();
                        if (!dnnUsername) {
                            this.errorCallback(reject, this.cvServiceStrings.auth.errors.unableToDetectDNNLogin, true, false);
                            this.logout();
                            return;
                        }
                        const dnnToken = el2.val();
                        if (!dnnToken) {
                            this.errorCallback(reject, this.cvServiceStrings.auth.errors.unableToDetectDNNLogin, true, false);
                            this.logout();
                            return;
                        }
                        this.login({ Username: dnnUsername, Password: dnnToken }).then(() => {
                            resolve();
                            this.cvViewStateService.finishRunning(this.viewStateName);
                        }).catch(reason => this.errorCallback(reject, reason, true));
                        break;
                    }
                    case this.cvServiceStrings.auth.providers.cobalt: {
                        if (this.loginPromise) {
                            // It's already running, don't fire a second time
                            this.loginPromise
                                .then(() => resolve(this.preAuth()))
                                .catch(reason => this.errorCallback(reject, reason, false));
                            return;
                        }
                        let gan = this.$rootScope["globalAccountNumber"];
                        if (!gan) {
                            const el3 = angular.element("cef-checkout");
                            if (el3 && el3[0]) {
                                gan = el3[0].attributes["global-account-number"].value
                            }
                        }
                        if (!gan) {
                            const el4 = angular.element("div[ui-view='main']");
                            if (el4 && el4[0]) {
                                gan = el4[0].attributes["global-account-number"].value
                            }
                        }
                        if (!gan) {
                            throw new Error("Unable to locate GAN");
                        }
                        const ganLogin = (): ng.IPromise<void> => {
                            return this.login({ Username: gan, Password: null }).then(() => {
                                resolve();
                                this.cvViewStateService.finishRunning(this.viewStateName);
                            }).catch(reason => this.errorCallback(reject, reason, true));
                        };
                        this.cvApi.contacts.GetCurrentUserName().then(r => {
                            if (!r || !r.data || !r.data.ActionSucceeded) {
                                ganLogin();
                                return;
                            }
                            if (r.data.Result["UserAuthName"] !== gan) {
                                ganLogin();
                                return;
                            }
                            this.setCurrentUser(
                                r.data.Result["UserId"],
                                r.data.Result["UserName"],
                                r.data.Result["DisplayName"]);
                            resolve();
                        });
                        break;
                    }
                    default: { // "identity"
                        if (this.loginPromise) {
                            // It's already running, don't fire a second time
                            this.loginPromise
                                .then(() => resolve(this.preAuth()))
                                .catch(reason => this.errorCallback(reject, reason, false));
                            return;
                        }
                        this.cvApi.contacts.GetCurrentUserName().then(r => {
                            if (!r || !r.data) {
                                this.errorCallback(reject, JSON.stringify(r.data), true);
                                return;
                            }
                            if (!r.data.ActionSucceeded) {
                                if (r.data.Messages
                                    && r.data.Messages.length > 0
                                    && r.data.Messages[0] === "No user currently logged in") {
                                    this.errorCallback(reject, null, true);
                                    return;
                                }
                                this.errorCallback(reject, r.data.Messages, true);
                                return;
                            }
                            this.setCurrentUser(
                                r.data.Result["UserId"],
                                r.data.Result["UserName"],
                                r.data.Result["DisplayName"]);
                            resolve();
                            this.preAuthPromise = null;
                            this.cvViewStateService.finishRunning(this.viewStateName);
                        }).catch(reason => this.errorCallback(reject, reason, true));
                        break;
                    }
                }
            });
        }
        private loginPromise: ng.IHttpPromise<api.AuthenticateResponse> = null;
        loginRequiresMFA(loginData: api.AuthProviderLoginDto): ng.IPromise<api.CEFActionResponseT<api.MFARequirementsModel>> {
            // if (!this.cefConfig.authProviderMFAEnabled) {
            //     return this.$q.resolve(false);
            // }
            return this.$q((resolve, reject) => {
                this.cvApi.authentication.CheckForMFAForUsername(loginData.Username).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        reject("ERROR! Something with the login process went wrong");
                        return;
                    }
                    this.mfaResult = r.data;
                    console.log(this.mfaResult);
                    resolve(this.mfaResult);
                });
            });
        }
        requestMFA(loginData: api.AuthProviderLoginDto, usePhone: boolean): ng.IPromise<boolean> {
            if (!this.cefConfig.authProviderMFAEnabled) {
                return this.$q.resolve(false);
            }
            return this.$q((resolve, reject) => {
                this.cvApi.authentication.RequestMFAForUsername(loginData.Username, { UsePhone: usePhone }).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        resolve(false);
                        return;
                    }
                    resolve(true);
                });
            });
        }
        login(loginData: api.AuthProviderLoginDto, alt: boolean = false, checked: boolean = false): ng.IPromise<void> {
            this.consoleDebug("cvAuthenticationService.login.entered");
            const loginClone = _.clone(loginData);
            this.cvViewStateService.setRunning(
                this.viewStateName,
                this.$translate("ui.storefront.common.LoggingIn.Ellipses"));
            return this.$q((resolve, reject) => {
                this.consoleDebug("cvAuthenticationService.login.promise.start");
                const provider = this.cefConfig.authProvider.indexOf(",") !== -1
                    ? alt
                        ? this.cefConfig.authProvider.split(",")[1]
                        : this.cefConfig.authProvider.split(",")[0]
                    : this.cefConfig.authProvider;
                if (this.cefConfig.authProviderMFAEnabled && !checked) {
                    this.cvApi.authentication.CheckForMFAForUsername(loginClone.Username).then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded) {
                            resolve(this.login(loginClone, alt, true));
                            return;
                        }
                        if (!loginClone["MFAToken"]) {
                            reject("ERROR! Missing MFA Token");
                            return;
                        }
                        loginClone.Password = loginClone.Password + loginClone["MFAToken"];
                        delete loginClone["MFAToken"];
                        resolve(this.login(loginClone, alt, true));
                    });
                    return;
                }
                // check for requires password reset
                if (!checked) {
                    this.cvApi.authentication.CheckForcedPasswordReset({ UserName: loginData.Username, Password: loginData.Password })
                        .then(r => {
                            if (r.data?.ActionSucceeded) {
                                // does need password reset
                                this.$filter("goToCORSLink")("/", "forcedPasswordReset");
                                return;
                            }
                            resolve(this.login(loginClone, alt, true));
                        }).catch(_err => {
                            // does not need password reset
                            resolve(this.login(loginClone, alt, true));
                        });
                    return;
                }
                (this.loginPromise = this.cvApi.authentication2.ProviderLogin(provider, loginClone)).then(r => {
                    this.consoleDebug("cvAuthenticationService.login.promise.reply");
                    if (!r || !r.data) {
                        this.consoleDebug("cvAuthenticationService.login.promise.reply.noData");
                        if (this.cefConfig.authProvider.indexOf(",") !== -1 && !alt) {
                            resolve(this.login(loginClone, true));
                            this.consoleDebug("cvAuthenticationService.login.promise.reply.noData");
                            this.consoleDebug("cvAuthenticationService.login.promise.reply.noData.reattemptAlt");
                            return;
                        }
                        this.consoleDebug("cvAuthenticationService.login.promise.reply.noData.doError");
                        this.errorCallback(reject, JSON.stringify(r.data), true);
                        this.loginPromise = null;
                        return;
                    }
                    this.consoleDebug("cvAuthenticationService.login.promise.reply 2");
                    if (!r.data.UserName && r.data.ResponseStatus.Message) {
                        this.consoleDebug("cvAuthenticationService.login.promise.reply.noUsername");
                        if (this.cefConfig.authProvider.indexOf(",") !== -1 && !alt) {
                            this.consoleDebug("cvAuthenticationService.login.promise.reply.noUsername.reattemptAlt");
                            resolve(this.login(loginClone, true));
                            return;
                        }
                        this.consoleDebug("cvAuthenticationService.login.promise.reply.noUsername.doError");
                        this.errorCallback(reject, r.data.ResponseStatus.Message, true);
                        this.loginPromise = null;
                        return;
                    }
                    this.consoleDebug("cvAuthenticationService.login.promise.reply 3");
                    ////this.setCurrentUser(r.data.UserId, r.data.UserName, r.data.DisplayName);
                    this.getCurrentUserPromise(true).then(u => {
                        ////this.consoleDebug("cvAuthenticationService.login.promise.reply.getCurrentUserPromise.reply");
                        if (!u.IsApproved) {
                            this.logout();
                            return;
                        }
                        ////this.consoleDebug("cvAuthenticationService.login.promise.reply 4");
                        this.preAuth().finally(() => {
                            ////this.consoleDebug("cvAuthenticationService.login.promise.reply 4.4");
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.auth.signIn);
                            ////this.consoleDebug("cvAuthenticationService.login.promise.reply 4.5");
                        });
                        this.cvViewStateService.finishRunning(this.viewStateName);
                        resolve();
                        this.loginPromise = null;
                        ////this.consoleDebug("cvAuthenticationService.login.promise.reply 5");
                    });
                }).catch(reason => {
                    this.consoleDebug("cvAuthenticationService.login.promise.reply.reason:");
                    this.consoleDebug(reason);
                    if (this.cefConfig.authProvider.indexOf(",") !== -1 && !alt) {
                        resolve(this.login(loginClone, true));
                        return;
                    }
                    this.errorCallback(reject, reason, true);
                    this.loginPromise = null;
                });
            });
        }
        logout(redirectToHome = false): ng.IPromise<void> {
            if (this.cefConfig.authProvider.toLowerCase().split(",").indexOf(this.cvServiceStrings.auth.providers.openIdConnect.toLowerCase()) >= 0) {
                // MVC Wrapper Logout
                return this.$http.get("/Logout").then(() => {
                    window.location.href = this.cefConfig.authProviderLogoutUrl;
                });
            }
            // Standard Logout Process
            return this.$q((resolve, reject) => {
                this.cvApi.authentication2.ProviderLogin(this.cvServiceStrings.auth.providers.logout).then(r => {
                    if (!r || !r.data) {
                        this.errorCallback(reject, JSON.stringify(r.data), false);
                        return;
                    }
                    if (!r.data.UserName && r.data.ResponseStatus.Message) {
                        this.errorCallback(reject, r.data.ResponseStatus.Message, false);
                        return;
                    }
                    this.clearCurrentUser();
                    this.clearCurrentUserCarts();
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.auth.signOut);
                    if (this.cefConfig.authProvider.split(',')[0].toLowerCase() === this.cvServiceStrings.auth.providers.openIdConnect.toLowerCase()) {
                        // TODO: Implement passing id_token_hint
                        window.location.href = this.cefConfig.authProviderLogoutUrl;
                    } else if (redirectToHome) {
                        this.$filter("goToCORSLink")("/");
                    }
                    resolve();
                }).catch(reason => this.errorCallback(reject, reason, false));
            });
        }
        updateCurrentUser(data: ExtendedUserModel): ng.IPromise<api.CEFActionResponse> {
            this.cvViewStateService.setRunning(
                this.viewStateName,
                this.$translate("ui.storefront.common.UpdatingCurrentUser.Ellipses"));
            // These properties conflict with real ones
            delete data.displayName;
            delete data.username;
            // Send the object
            return this.$q((resolve, reject) => {
                this.cvApi.contacts.UpdateCurrentUser(data).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.errorCallback(reject, JSON.stringify(r.data), false);
                        return;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.users.updated);
                    resolve(r.data);
                    this.cvViewStateService.finishRunning(this.viewStateName);
                }).catch(reason => this.errorCallback(reject, reason, false));
            });
        }
        forgotUsername(data: api.ForgotUsernameDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.ForgotUsername(data);
        }
        forgotPassword(data: api.ForgotPasswordDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.ForgotPassword(data);
        }
        forgotPasswordReturn(data: api.ForgotPasswordReturnDto): ng.IHttpPromise<api.CEFActionResponseT<string>> {
            return this.cvApi.authentication.ForgotPasswordReturn(data);
        }
        forcedPasswordReset(data: api.ForcedPasswordResetDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.ForcedPasswordReset(data);
        }
        sendInvitation(data: api.SendInvitationDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.providers.SendInvitation(data);
        }
        approveUserRegistration(data: api.ApproveUserRegistrationDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.ApproveUserRegistration(data);
        }
        validateUserNameIsGood(data: api.ValidateUserNameIsGoodDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.ValidateUserNameIsGood(data);
        }
        validateEmailIsUnique(data: api.ValidateEmailIsUniqueDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.ValidateEmailIsUnique(data);
        }
        validatePasswordIsGood(data: api.ValidatePasswordIsGoodDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.ValidatePasswordIsGood(data);
        }
        validatePassword(data: api.ValidatePasswordDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.ValidatePassword(data);
        }
        confirmEmail(data: api.ValidateEmailDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.ValidateEmail(data);
        }
        changePassword(data: api.ChangePasswordDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.ChangePassword(data);
        }
        createUser(data: api.CreateUserDto): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.contacts.CreateUser(data);
        }
        register(data: api.RegisterNewUserDto): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.authentication.RegisterNewUser(data);
        }
        getUserByUsername(username: string): ng.IHttpPromise<api.UserModel> {
            return this.cvApi.contacts.GetUserByKey(username);
        }
        checkUserExistsByUsername(username: string): ng.IHttpPromise<number> {
            return this.cvApi.contacts.CheckUserExistsByKey(username);
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $document: ng.IDocumentService,
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly cvApi: api.ICEFAPI,
                private readonly cefConfig: core.CefConfig,
                private readonly cvViewStateService: services.IViewStateService,
                private readonly cvServiceStrings: IServiceStrings,
                private readonly $http: ng.IHttpService,
                private readonly $cookies: ng.cookies.ICookiesService) {
            this.currentUser = new ExtendedUserModel();
            this.preAuth()
                .then(() => this.getCurrentUserPromise(true) // Force to get expanded data
                    .then(() => this.getCurrentAccountPromise()));
            $rootScope.$on(cvServiceStrings.events.users.updated,
                () => this.getCurrentUserPromise(true).then(() => { }));
        }
    }
}
