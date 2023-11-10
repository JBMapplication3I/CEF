module cef.store.user.registrationInvites {
    class RegistrationInvites extends core.TemplatedControllerBase {
        // Properties
        returnUrl: string;
        // Functions
        userLogin(user): void {
            this.setRunning();
            this.cvAuthenticationService.login({
                Username: user.UserName,
                Password: user.OverridePassword
            }).then(() => {
                this.$filter("goToCORSLink")(this.returnUrl && this.returnUrl !== "null" ? this.returnUrl : "/");
                this.finishRunning();
            }, result => this.finishRunning(true, result.data.ResponseStatus.Message));
        }
        registerUser(user): void {
            user.UserName = user.Email;
            this.cvApi.contacts.CreateUserWithCode(user)
                .then(() => this.userLogin(user),
                      result => this.finishRunning(true, result.data.ResponseStatus.Message));
        }

        constructor(
                private readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefRegistrationInvites", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { returnUrl: "=" },
        templateUrl: $filter("corsLink")("/framework/store/user/registrationInvites/registrationInvites.html", "ui"),
        controller: RegistrationInvites,
        controllerAs: "cefRegistrationInvitesCtrl",
        bindToController: true
    }));
}
