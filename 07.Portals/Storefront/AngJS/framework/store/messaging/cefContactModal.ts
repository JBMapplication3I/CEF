/**
 * @file framework/store/messaging/cefContactModal.ts
 * @desc Contact modal class
 */
module cef.store.messaging {
    cefApp.controller("cefContactModal", function (
            cefConfig: core.CefConfig,
            cvAuthenticationService: services.IAuthenticationService,
            $scope: ng.IScope,
            $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
            $stateParams: IModalComposeStateParams) {
        $scope.stateParams = $stateParams;
        this.contacts = $stateParams["contacts"];
        this.subject = $stateParams["subject"];
        this.messageSent = () => this.showCloseButton = true;
        $scope.confirm = () => {
            $uibModalInstance.close(true);
            window.location.hash = ""; // Remove the router path to avoid page load weirdness
        };
        $scope.closeThisDialog = () => {
            $uibModalInstance.dismiss(false);
            window.location.hash = ""; // Remove the router path to avoid page load weirdness
        }
    });
}
