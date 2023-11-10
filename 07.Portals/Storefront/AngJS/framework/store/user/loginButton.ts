module cef.store.user {
    class LoginButtonController extends core.TemplatedControllerBase {
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService) { // Used by UI
            super(cefConfig);
        }
    }

    cefApp.directive("cefLoginButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/user/loginButton.html", "ui"),
        controller: LoginButtonController,
        controllerAs: "loginButtonCtrl"
    }));
}
