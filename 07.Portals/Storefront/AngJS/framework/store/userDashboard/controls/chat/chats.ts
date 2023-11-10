module cef.store.userDashboard.controls.chats {
    export class ChatsController extends core.TemplatedControllerBase {
        // Properties
        currentUser: api.UserModel;
        enableWeChat = false;
        weChatUserID: string = null;
        // Functions
        refreshUser(): void {
            this.setRunning();
            this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                this.currentUser = user;
                this.readWeChatData();
                this.finishRunning();
            }, reason => this.finishRunning(true, reason))
            .catch(result => this.finishRunning(true, result));
        }

        readWeChatData(): void {
            if (angular.isUndefined(this.currentUser.SerializableAttributes)) {
                return;
            }
            const chattingSetting = this.currentUser.SerializableAttributes["Offline-Messaging-Setting"];
            if (angular.isUndefined(chattingSetting)) {
                return;
            }
            this.enableWeChat = !angular.isUndefined(this.weChatUserID = chattingSetting.Value) && this.weChatUserID != null && this.weChatUserID !== "";
        }

        save(): void {
            this.setRunning();
            // TODO: More chat providers
            if (this.enableWeChat && this.weChatUserID.length > 0) {
                this.currentUser.SerializableAttributes["Offline-Messaging-Setting"] = <api.SerializableAttributeObject>{
                    ID: 0,
                    Key: "Offline-Messaging-Setting",
                    Group: "Chatting",
                    Value: this.weChatUserID,
                    UofM: "WeChatChattingProvider",
                    ValueType: "System.String",
                };
            } else if (this.currentUser.SerializableAttributes["Offline-Messaging-Setting"]) {
                delete this.currentUser.SerializableAttributes["Offline-Messaging-Setting"];
                this.enableWeChat = false;
                this.weChatUserID = null;
            }
            this.cvAuthenticationService.updateCurrentUser(this.currentUser).then(r3 => {
                if (!r3 || !r3.ActionSucceeded) {
                    this.finishRunning(true, null, r3 && r3.Messages);
                    return;
                }
                this.cvAuthenticationService.getCurrentUserPromise().then(r4 => {
                    this.currentUser = r4;
                    this.readWeChatData();
                    this.finishRunning();
                });
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.cvAuthenticationService.preAuth().finally(() => this.refreshUser());
        }
    }

    cefApp.directive("cefChats", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/chat/chats.html", "ui"),
        controller: ChatsController,
        controllerAs: "chatsCtrl"
    }));
}
