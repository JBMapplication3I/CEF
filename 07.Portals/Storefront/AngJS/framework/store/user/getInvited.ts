module cef.store.user {
    class GetInvitedController extends core.TemplatedControllerBase {
        // Properties
        inviteData = <api.SendInvitationDto>{ Email: "" };
        // Functions
        sendInvitation(): void {
            this.setRunning();
            this.cvAuthenticationService.sendInvitation(this.inviteData).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, `There was an error sending your invitaton, please contact us for assistance: ${r.data.Messages[0]}`);
                    return;
                }
                this.finishRunning();
            });
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefGetInvited", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/user/getInvited.html", "ui"),
        controller: GetInvitedController,
        controllerAs: "getInvitedCtrl",
        link: ($scope: ng.IScope) => {
            // TODO: This should be in the controller constructor
            ($scope["getInvitedCtrl"] as any).successMessage = false;
        }
    }));
}
