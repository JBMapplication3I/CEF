/**
 * @file /framework/store/user/forgotPassword.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Forgot password class
 */
module cef.store.user {
    class VerifyEmailController extends core.TemplatedControllerBase {
        // Properties
        // Functions
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

    cefApp.directive("cefVerifyEmail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/user/verifyEmail.html", "ui"),
        controller: VerifyEmailController,
        controllerAs: "veCtrl"
    }));
}
