module cef.store.widgets {
    class LoginRedirectNoAccountController extends core.TemplatedControllerBase {
        doError() {
            this.cvMessageModalFactory(this.$translate("ui.storefront.widgets.loginRedirectNoAccount.FailToSync.Message"))
                .then(() => this.$filter("goToCORSLink")("/Help"));
            
        }
        getQueryVariable(variable) {
            const query = window.location.search.substring(1);
            const vars = query.split("&");
            for (let i = 0; i < vars.length; i++) {
                const pair = vars[i].split("=");
                if (decodeURIComponent(pair[0]) === variable) {
                    return decodeURIComponent(pair[1]);
                }
            }
            this.consoleDebug("Query variable %s not found", variable);
            return null;
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvApi: api.ICEFAPI,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory) {
            super(cefConfig);
            const accountKey = this.getQueryVariable("accountKey");
            let token = this.getQueryVariable("token");
            var redirectUrl = this.getQueryVariable("redirectUrl");
            let goodToGo = true;
            if (accountKey) { } else { goodToGo = false; }
            if (token) { token = decodeURI(token); } else { goodToGo = false; }
            if (redirectUrl) { redirectUrl = decodeURI(redirectUrl); } else { goodToGo = false; }
            if (!goodToGo) { this.consoleDebug("something's missing"); return; }
            // TODO: Show a status to the user that we are logging them in
            // maybe dim the screen with a spinner on a small dialog?
            (cvApi.authentication as any).AuthUserByThirdPartyNoAccount({
                UserKey: accountKey,
                Token: token
            }).then(r => {
                if (r.data.IsLoggedIn) {
                    if (r.data.AccountIsOnHold) {
                        this.consoleDebug("Exiting Login Redirect: account is on hold so sending user to /FAQs/QuestionID/29/AFMID/524");
                        $filter("goToCORSLink")("/FAQs/QuestionID/29/AFMID/524");
                        return;
                    }
                    this.consoleDebug(`Exiting Login Redirect: sending user to ${redirectUrl}`);
                    this.$filter("goToCORSLink")(redirectUrl);
                    return;
                }
                this.consoleDebug("Exiting Login Redirect: login failed");
                this.doError();
            });
        }
    }

    cefApp.directive("cefLoginRedirectNoAccount", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/user/authentication/loginRedirectNoAccount.html", "ui"),
        controller: LoginRedirectNoAccountController,
        controllerAs: "lrcna"
    }));
}
