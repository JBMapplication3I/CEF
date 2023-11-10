// <copyright file="confirmation.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>confirmation class</summary>
module cef.store.user {
    export class UserConfirmationController extends core.TemplatedControllerBase {
        load(): void {
            const qs = window.location.search;
            let token: string = null;
            let id: string = null;
            if (qs) {
                const re = /token=([^&]*)&ID=([^&]*)/gi;
                const tokenThere = re.exec(qs);
                if (angular.isArray(tokenThere) && tokenThere[1] && tokenThere[2]) {
                    token = tokenThere[1];
                    id = tokenThere[2];
                }
            }
            if (!token || !id) {
                this.cvMessageModalFactory(this.$translate("ui.storefront.user.confirmation.ErrorConfirmingUser"));
                return;
            }
            this.cvAuthenticationService.approveUserRegistration({ Token: token, ID: Number(id) }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.cvMessageModalFactory(this.$translate("ui.storefront.user.confirmation.ErrorConfirmingUser"));
                    return;
                }
                this.cvMessageModalFactory(this.$translate("ui.storefront.user.confirmation.UserConfirmed"));
            });
        }
        // Constructor
        constructor(
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefUserConfirmation", () => ({
        restrict: "EA",
        controller: UserConfirmationController,
        controllerAs: "userConfirmationCtrl"
    }));
}
