/**
 * @file /framework/store/user/forgotUsername.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Forgot username class
 */
module cef.store.user {
    class ForgotUsernameController extends core.TemplatedControllerBase {
        // Properties
        data = <api.ForgotUsernameDto>{ Email: "" };
        isLoggedIn: boolean;
        wasSent = false;
        // Functions
        submit(): void {
            this.wasSent = false;
            this.setRunning();
            this.cvAuthenticationService.forgotUsername(this.data).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(
                        true,
                        `Unable to send Username notification, please contact support for assistance: ${r.data.Messages[0]}`);
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

    cefApp.directive("cefForgotUsername", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/user/forgotUsername.html", "ui"),
        controller: ForgotUsernameController,
        controllerAs: "funCtrl"
    }));
}
