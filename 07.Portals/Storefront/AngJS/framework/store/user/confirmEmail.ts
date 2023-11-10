/**
 * @file registration.ts
 * Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * Registration class
 */
module cef.store.user {
    export class ConfirmEmailController extends core.TemplatedControllerBase {
        load(): void {
            this.setRunning();
            const email: string = this.$location.search()["email"];
            if (!email) {
                this.finishRunning(true, "No Email found in your link url");
                return;
            }
            const token: string = this.$location.search()["token"];
            if (!token) {
                this.finishRunning(true, "No Validation Token found in your link url");
                return;
            }
            this.cvAuthenticationService.confirmEmail({ Email: email, Token: token }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, null, r.data.Messages);
                    return;
                }
                this.finishRunning();
            }, result => {
                this.finishRunning(true, result);
            }).catch(reason => {
                this.finishRunning(true, reason);
            });
        }
        // Constructor
        constructor(
                private readonly $location: ng.ILocationService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefConfirmEmail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/user/confirmEmail.html", "ui"),
        controller: ConfirmEmailController,
        controllerAs: "confirmEmailCtrl"
    }));
}
