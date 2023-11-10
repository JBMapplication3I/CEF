/**
 * @file /framework/store/user/forgotPassword.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Forgot password class
 */
module cef.store.user {
    class ForgotPasswordController extends core.TemplatedControllerBase {
        // Properties
        data = <api.ForgotPasswordDto>{ Email: "" };
        isLoggedIn: boolean;
        wasSent = false;
        // Functions
        submit(): void {
            this.wasSent = false;
            this.setRunning();
            this.cvAuthenticationService.forgotPassword(this.data).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(
                        true,
                        "Unable to send Password Reset Token, please contact support for assistance");
                    return;
                }
                this.wasSent = true;
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        cancel(): void {
            this.$filter("goToCORSLink")("/");
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.auth.signIn, () => {
                this.$filter("goToCORSLink")("/");
            });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefForgotPassword", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/user/forgotPassword.html", "ui"),
        controller: ForgotPasswordController,
        controllerAs: "fpCtrl"
    }));
}
