/**
 * @author Eric.Adam 
 * @since 10/6/2016
 */
module cef.store.messaging {
    cefApp.directive("cefMessaging", ($filter: ng.IFilterService, cvAuthenticationService: services.IAuthenticationService, $state: ng.ui.IStateService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/messaging/messaging-layout.html", "ui"),
        controller(cvMessagingFactory: factories.IMessagingFactory) {
            this.isLoggedIn = false;
            this.folders = {};
            this.init = function () {
                cvAuthenticationService.preAuth().finally(() => {
                    this.isLoggedIn = cvAuthenticationService.isAuthenticated();
                    if (this.isLoggedIn) {
                        this.initInbox();
                    }
                });
            };
            this.initInbox = function () {
                this.folders.inbox = { name: "Inbox", data: cvMessagingFactory.messageBoxFactory() };
                $state.go("userDashboard.inbox.folder", { folder: "inbox", box: this.folders.inbox.data });
            };
            this.composeMessage = function () {
                this.folders.inbox.data.createMessage();
                $state.go("userDashboard.inbox.compose", { msg: this.folders.inbox.data.currentMessage });
            };
            this.fakeMessage = function () {
                this.testy = this.folders.inbox.createMessage();
                this.testy.model.Subject = `TEST ${Math.random()}`;
                this.testy.model.Body = `Test body. ${Math.random()}`;
                this.testy.model.MessageRecipients.push({
                    ToUserID: 1,
                    MessageID: 1
                });
                this.testy.send();
                this.consoleLog(this.testy);
            };
            this.refreshFolders = function () {
                Object.keys(this.folders).forEach(box => {
                    this.folders[box].data.getMessages();
                });
            };
            this.init();
        },
        controllerAs: "messagingCtrl"
    }));
}
