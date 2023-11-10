/**
 * Directive can be dropped on individual pages to check for authentication
 * Place tag in header for site-wide application
 * Will redirect visitor to specified page or home by default if returnUrl is not specified
 */
module cef.store.user.authentication {
    cefApp.directive("cefAuthenticationRedirect", (): ng.IDirective => ({
        restrict: "EA",
        scope: {
            modal: "@?",
            args: "=",
            returnUrl: "=?",
            reloadPage: "@?",
            noReturn: "@?",
            modalSize: "=?",
            staticModal: "@?"
        },
        controller: function ($window: ng.IWindowService, cvAuthenticationService: services.IAuthenticationService, cvLoginModalFactory: user.ILoginModalFactory) {
            cvAuthenticationService.preAuth().finally(() => {
                if (cvAuthenticationService.isAuthenticated()) {
                    return;
                }
                if (!this.modal) {
                    $window.location.href = this.$filter("corsLink")(`#!?returnUrl=${this.returnUrl || "[ReturnUrl]"}`, "login");
                    return;
                }
                cvLoginModalFactory(this.args, this.returnUrl, this.reloadPage, this.noReturn, this.modalSize, this.staticModal);
            });
        },
        controllerAs: "cefAuthenticationRedirectCtrl",
        bindToController: true
    }));
}
