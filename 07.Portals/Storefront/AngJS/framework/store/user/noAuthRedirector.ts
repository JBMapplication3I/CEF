/**
 * @file noAuthRedirector.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc no authentication redirector class
 */
module cef.store.user {
    class NoAuthRedirectorController {
        // Properties
        isAuthenticated = false;
        // Functions
        showUnauthorized(): void {
            this.$filter("goToCORSLink")("#!?returnUrl=[ReturnUrl]", "login");
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $window: ng.IWindowService,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            cvAuthenticationService.preAuth().finally(() => {
                if (!cvAuthenticationService.isAuthenticated()) {
                    // Kick out
                    this.showUnauthorized();
                    return;
                }
                // Stay
                this.isAuthenticated = true;
            });
        }
    }

    cefApp.directive("cefNoAuthRedirector", (): ng.IDirective => ({
        restrict: "EA",
        template: "", // NOTE: THis intentionally does not have a template as it acts as a redirect instead
        controller: NoAuthRedirectorController,
        controllerAs: "noAuthRedirectorCtrl",
        bindToController: true
    }));
}
