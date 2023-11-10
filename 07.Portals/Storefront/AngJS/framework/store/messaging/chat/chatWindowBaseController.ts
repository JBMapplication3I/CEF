/**
 * @file framework/store/messaging/chat/_chatWindowBaseController.ts
 * @copyright 2017-2020 Clarity Ventures, Inc. All rights reserved.
 * @desc A base controller for chat windows that contains shared functionality.
 */
module cef.store.messaging.chat {
    export class ChatWindowBaseController extends core.TemplatedControllerBase {
        // Properties
        currentConversationID: number = null;
        searchTerm: string = null;
        filteredData: Array<api.MessageModel> = [];
        newMessage: cefalt.store.Dictionary<string> = {};
        // Functions
        switchToConversation(newID: number): void {
            if (!newID) { return; }
            this.currentConversationID = newID;
            if (!this.cvChatService.conversationMessages
                || angular.isUndefined(this.cvChatService.conversationMessages[newID])) {
                this.filteredData = [];
                this.cvChatService.loadEndedConversations(true, newID).then(() => {
                    this.filteredData = _.sortBy(this.cvChatService.conversationMessages[this.currentConversationID], x => x.CreatedDate);
                    (angular.element(".chat-block") as ng.IAugmentedJQuery).duScrollToElement(angular.element("#chatBlockBottom") as ng.IAugmentedJQuery, 0, 250);
                });
            } else {
                this.filteredData = _.sortBy(this.cvChatService.conversationMessages[this.currentConversationID], x => x.CreatedDate);
                (angular.element(".chat-block") as ng.IAugmentedJQuery).duScrollToElement(angular.element("#chatBlockBottom") as ng.IAugmentedJQuery, 0, 250);
            }
        }
        protected createConversationUser(userID: number): api.ConversationUserModel {
            if (!userID) { return null; }
            return <api.ConversationUserModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                IsTyping: false,
                UserID: userID, //
                LastHeartbeat: new Date(),
                ConversationID: 0 // Will be assigned a real value in the server
                // Don't pass the following because they are always null to start
                // UpdatedDate: null,
                // Conversation: null,
                // ConversationKey: null,
                // CustomKey: null,
                // Hash: null,
                // ID: null,
                // User: null,
                // UserKey: null
            };
        }
        protected selectObjectAsContextObjectBase(storeID: number, message: string, callback: () => void): void {
            this.cvApi.stores.GetStoreUsers({ Active: true, AsListing: true, StoreID: storeID }).then(r1 => {
                if (!r1 || !r1.data || !r1.data.Results) {
                    this.consoleLog("Unable to load store admin user");
                    return;
                }
                const newConversation = <api.ConversationModel>{
                    Active: true,
                    CreatedDate: new Date(),
                    HasEnded: false,
                    StoreID: storeID,
                    ConversationUsers: [],
                    CopyUserWhenEnded: false,
                    // Don't pass the following because they are always null to start
                    // UpdatedDate: null,
                    // Hash: null,
                    // ID: null,
                    // CustomKey: null,
                    // Store: null,
                    // StoreKey: null,
                    // StoreName: null,
                    // StoreSeoUrl: null,
                    // Messages: null,
                    // ConversationUsersCount: null,
                    // MessagesCount: null,
                };
                this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                    newConversation.ConversationUsers.push(this.createConversationUser(user.userID)); // The current user
                    newConversation.ConversationUsers.push(this.createConversationUser(r1.data.Results[0].SlaveID)); // The store owner user
                    this.cvApi.messaging.CreateConversation(newConversation).then(response => {
                        this.currentConversationID = response.data.Result; // The new conversation ID (data may get sync'd in separately in a moment)
                        const temp = this.newMessage[this.currentConversationID];
                        this.newMessage[this.currentConversationID] = message;
                        this.postMessage().finally(() => {
                            this.newMessage[this.currentConversationID] = temp;
                            callback();
                        });
                    });
                });
            });
        }
        // Events
        onKeyPress(event: JQueryKeyEventObject): void { // Angular returns this type of object per their docs
            this.cvChatService.setTypingStatus(true);
        }
        sendMessageByKeyStroke($event: JQueryKeyEventObject): void { // Angular returns this type of object per their docs
            const eventCode = $event.charCode ? $event.charCode : $event.which; // Enable this functionality to work in Firefox
            if (eventCode !== 13
                || $event.altKey
                || $event.ctrlKey
                || $event.shiftKey
                || $event.metaKey) { return; } // Only do anything if it was the enter key
            if (!this.newMessage[this.currentConversationID]) { return; } // And only if the user actually entered something
            event.preventDefault();
            event.stopPropagation();
            this.postMessage();
        }
        postMessage(): ng.IPromise<void> {
            const defer = this.$q.defer<void>();
            if (!this.newMessage[this.currentConversationID]) { return null; } // Only if the user actually entered something
            this.cvChatService.postMessageToConversation(this.currentConversationID, this.newMessage[this.currentConversationID])
                .then(() => {
                    this.newMessage[this.currentConversationID] = null;
                    defer.resolve();
                }, () => defer.reject())
                .catch(() => defer.reject());
            return defer.promise;
        }
        endConversation(): void {
            this.cvConfirmModalFactory(this.$translate("ui.storefront.messaging.chat.chatWindowBaseController.EndConvo.Message")).then(result => {
                if (!result) { return; }
                this.cvChatService.endConversation(this.currentConversationID);
            });
        }
        // Constructors
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvChatService: services.ChatService,
                protected readonly cvConfirmModalFactory: modals.IConfirmModalFactory,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.chat.newMessage,
                ($event: ng.IAngularEvent, conversationID: string/*, message: api.MessageModel*/) => {
                    if (Number(conversationID) !== this.currentConversationID) {
                        return;
                    }
                    $scope.$applyAsync(() => this.switchToConversation(this.currentConversationID));
                });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            })
        }
    }
}
