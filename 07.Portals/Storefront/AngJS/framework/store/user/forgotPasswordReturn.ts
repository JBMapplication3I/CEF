/**
 * @file /framework/store/user/forgotPasswordReturn.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Forgot password return class
 */
module cef.store.user {
    class ForgotPasswordReturnController extends core.TemplatedControllerBase {
        // Properties
        data = <api.ForgotPasswordReturnDto>{
            Email: null,
            Token: null,
            Password: ""
        };
        isLoggedIn: boolean;
        passwordValidityState = "Empty";
        // Functions
        submit(): void {
            this.setRunning();
            this.cvAuthenticationService.forgotPasswordReturn(
                this.data
            ).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(
                        true,
                        `Unable to apply new Password, please contact support for assistance: ${r.data.Messages[0]}`);
                    return;
                }
                this.finishRunning();
                if (this.cvAuthenticationService.isAuthenticated()) {
                    this.cvAuthenticationService.logout()
                        .then(() => this.$filter("goToCORSLink")("", "login"));
                    return;
                }
                this.$filter("goToCORSLink")("", "login");
            }).catch(reason => this.finishRunning(true, reason));
        }
        cancel(): void {
            this.$filter("goToCORSLink")("/");
        }
        // Events
        passwordChanged(): void {
            if (!this.data
                || !this.data.Password
                || this.data.Password === "") {
                this.passwordValidityState = "Empty";
                return;
            }
            if (this.data.Password.length < 7) {
                this.passwordValidityState = "Invalid";
                return;
            }
            const oneUpperCheck = /.*[A-Z].*/;
            if (!oneUpperCheck.test(this.data.Password)) {
                this.passwordValidityState = "Invalid";
                return;
            }
            const oneLowerCheck = /.*[a-z].*/;
            if (!oneLowerCheck.test(this.data.Password)) {
                this.passwordValidityState = "Invalid";
                return;
            }
            const oneNumberCheck = /.*\d.*/;
            if (!oneNumberCheck.test(this.data.Password)) {
                this.passwordValidityState = "Invalid";
                return;
            }
            this.cvAuthenticationService.validatePasswordIsGood({
                Password: this.data.Password
            }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.passwordValidityState = "Invalid";
                    return;
                }
                this.passwordValidityState = "Valid";
            }).catch(() => this.passwordValidityState = "Invalid");
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $location: ng.ILocationService,
                private readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            const email = $location.search().email;
            const token = $location.search().token;
            if (email) { this.data.Email = email; }
            if (token) { this.data.Token = token; }
            const unbind1 = $scope.$on(cvServiceStrings.events.auth.signIn, () => {
                this.$filter("goToCORSLink")("/");
            });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefForgotPasswordReturn", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/user/forgotPasswordReturn.html", "ui"),
        controller: ForgotPasswordReturnController,
        controllerAs: "fprCtrl"
    }));
}
