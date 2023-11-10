/**
 * @author Eric.Adam 
 * @since 10/4/2016
 */
module cef.store.messaging {
    cefApp.directive("cefMessageBox", ($filter: ng.IFilterService, $stateParams: ng.ui.IStateParamsService, cvMessagingFactory: factories.IMessagingFactory): ng.IDirective => ({
        restrict: "EA",
        scope: { messageBox: "=box" },
        templateUrl: $filter("corsLink")("/framework/store/messaging/messaging-folder.html", "ui"),
        controller() {
            if (!this.messageBox) {
                if (($stateParams["box"] as any as factories.IMessageBox).model) {
                    this.messageBox = ($stateParams["box"] as any as factories.IMessageBox);
                    this.messageBox.getMessages();
                } else {
                    this.messageBox = cvMessagingFactory.messageBoxFactory();
                    this.messageBox.getMessages();
                }
            }
        },
        controllerAs: "messageBoxCtrl",
        bindToController: true
    }));
}
