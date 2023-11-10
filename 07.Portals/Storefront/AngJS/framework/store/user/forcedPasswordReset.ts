module cef.store.user {
    class ForcedPasswordResetController extends core.TemplatedControllerBase {
        // Properties
        data = <api.ForcedPasswordResetDto>{
            Email: this.$location.search().email || null,
            OldPassword: null,
            NewPassword: null
        };
        isLoggedIn: boolean;
        wasSent = false;
        // Functions
        doForcedPasswordReset(): void {
            this.wasSent = false;
            this.setRunning();
            this.cvAuthenticationService.forcedPasswordReset(this.data).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, 
                        this.$translate(
                            "ui.storefront.user.forcedPasswordReset.ErrorResettingPassword",
                            null,
                            null,
                            "Your current password was incorrect, please contact support for assistance."
                        ));
                    this.viewState.hasError = true;
                    return;
                }
                this.wasSent = true;
                this.$filter("goToCORSLink")("/");
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $location: ng.ILocationService,
                private readonly $filter: ng.IFilterService,
                private readonly $translate: ng.translate.ITranslateService,
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

    cefApp.directive("cefForcedPasswordReset", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/user/forcedPasswordReset.html", "ui"),
        controller: ForcedPasswordResetController,
        controllerAs: "fprCtrl"
    }));
}
