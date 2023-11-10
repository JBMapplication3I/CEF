/**
 * @file framework/store/_services/cvChatService.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Chat Service, provides calls to manage the chat process including monitoring
 * for new messages from the server
 */
module cef.store.services {
    export interface IChatService {
        getConversation(id: number): api.ConversationModel;
        postMessageToConversation(conversationID: number, message: string): ng.IPromise<void>;
        endConversation(conversationID: number): void;
        getFlashWindowOnIncomingMessage(): boolean;
        setFlashWindowOnIncomingMessage(newValue: boolean): void;
        onlineStatus: string;
        getOtherConversationUsersTypingStatuses(): Array<api.UserTypingStatus>;
        getOtherConversationUsersTypingStatusesPromise(): ng.IPromise<Array<api.UserTypingStatus>>;
        getTypingStatus(): boolean;
        setTypingStatus(newStaus: boolean): void;
        startChatOnlineStatusSessionTimer(): void;
        startChatGoOfflineStatusSessionTimer(): void;
        startThisConversationUserTypingStatusTimer(): void;
        startConversationMessagesTimer(): void;
    }
    /**
     * Controls chat functionality such as user online status, the user's 'is typing'
     * status and reading other users in conversation's typing statuses. It also
     * heartbeats to maintain those statuses and look for new messages (incoming or
     * outgoing) and places them in to a dictionary where the key is the Conversation
     * ID.
     */
    export class ChatService implements IChatService {
        // Properties
        private _onlineStatus = "Offline";
        private otherConversationUsersTypingStatuses: Array<api.UserTypingStatus> = null;
        private typingStatus = true;
        private chatOnlineStatusSessionTimer: ng.IPromise<void> = null;
        private chatGoOfflineStatusSessionTimer: ng.IPromise<void> = null;
        private thisConversationUserTypingStatusTimer: ng.IPromise<void> = null;
        private otherConversationUsersTypingStatusesTimer: ng.IPromise<void> = null;
        private conversationMessagesTimer: ng.IPromise<void> = null;
        private flashWindowOnIncomingMessage = true;
        userOnlineStatuses: Array<api.StatusModel>;
        activeConversations: Array<api.ConversationModel> = null;
        endedConversations: Array<api.ConversationModel> = null;
        conversationMessages: cefalt.store.Dictionary<Array<api.MessageModel>> = null;
        private loadUserOnlineStatuses(): void {
            this.cvApi.contacts.GetUserOnlineStatuses({ Active: true, AsListing: true }).then(r1 => {
                if (!r1 || !r1.data || !r1.data.Results) {
                    this.$log.error("Could not load online active conversations list.");
                    return;
                }
                this.userOnlineStatuses = r1.data.Results;
                this.cvApi.contacts.GetCurrentUserOnlineStatus().then(r2 => {
                    if (!r2 || !r2.data) {
                        this.$log.error("Could not load current user's online status.");
                        return;
                    }
                    this.onlineStatus = r2.data.CustomKey;
                });
            });
        }
        private loadActiveConversations(doAll: boolean): void {
            const dto1 = <api.GetConversationHeadersForCurrentUserDto>{
                HasEnded: false,
                Sorts: [
                    <api.Sort>{ field: "UpdatedDate", dir: "desc", order: 0 },
                    <api.Sort>{ field: "CreatedDate", dir: "desc", order: 1 }
                ],
                Paging: { Size: 50, StartIndex: 1 }
            };
            this.cvApi.messaging.GetConversationHeadersForCurrentUser(dto1).then(r => {
                if (!r || !r.data || !r.data.Results) {
                    this.$log.error("Could not load active conversations headers.");
                    return;
                }
                if (angular.toJson(this.activeConversations) !== angular.toJson(r.data.Results)) {
                    this.activeConversations = r.data.Results;
                }
                const postedSince = new Date();
                postedSince.setSeconds(postedSince.getSeconds() - 15);
                const dto2 = doAll ? { } : { PostedSince: postedSince };
                this.cvApi.messaging.GetMessagesForActiveConversationsForCurrentUser(dto2).then(r2 => {
                    if (!r2 || !r2.data) {
                        this.$log.error("Could not load messages for active conversations dictionary.");
                        return;
                    }
                    angular.forEach(Object.keys(r2.data.Result), conversationID => {
                        if (!this.conversationMessages[conversationID]) {
                            this.conversationMessages[conversationID] = [];
                        }
                        angular.forEach(r2.data.Result[conversationID], message => {
                            if (_.find(this.conversationMessages[conversationID], z => z.ID === message.ID)) {
                                return;
                            }
                            this.conversationMessages[conversationID].push(message);
                            this.$rootScope.$broadcast("newConversationMessage", conversationID, message);
                        });
                    });
                    this.resetConversationMessagesTimer();
                });
            });
        }
        public loadEndedConversations(doAll: boolean, byID?: number): ng.IPromise<void> {
            const dto1 = <api.GetConversationHeadersForCurrentUserDto>{
                HasEnded: true,
                Sorts: [
                    <api.Sort>{ field: "UpdatedDate", dir: "desc", order: 0 },
                    <api.Sort>{ field: "CreatedDate", dir: "desc", order: 1 }
                ],
                Paging: { Size: 8, StartIndex: 1 }
            };
            if (byID) { dto1.ID = byID; }
            return this.$q((resolve, reject) => {
                this.cvApi.messaging.GetConversationHeadersForCurrentUser(dto1).then(r => {
                    if (!r || !r.data || !r.data.Results) {
                        this.$log.error("Could not load ended conversations headers.");
                        reject("Could not load ended conversations headers.");
                        return;
                    }
                    if (!byID) {
                        if (angular.toJson(this.endedConversations) !== angular.toJson(r.data.Results)) {
                            this.endedConversations = r.data.Results;
                        }
                    }
                    const postedSince = new Date();
                    postedSince.setSeconds(postedSince.getSeconds() - 15);
                    const dto2: api.GetMessagesForEndedConversationsForCurrentUserDto = doAll ? { } : { PostedSince: postedSince };
                    if (byID) { dto2.ID = byID; }
                    this.cvApi.messaging.GetMessagesForEndedConversationsForCurrentUser(dto2).then(r2 => {
                        if (!r2 || !r2.data) {
                            this.$log.error("Could not load messages for ended conversations dictionary.");
                            reject("Could not load messages for ended conversations dictionary.");
                            return;
                        }
                        angular.forEach(Object.keys(r2.data.Result), conversationID => {
                            if (!this.conversationMessages[conversationID]) {
                                this.conversationMessages[conversationID] = [];
                            }
                            angular.forEach(r2.data.Result[conversationID], message => {
                                if (_.find(this.conversationMessages[conversationID], z => z.ID === message.ID)) {
                                    return;
                                }
                                this.conversationMessages[conversationID].push(message);
                            });
                        });
                        this.resetConversationMessagesTimer();
                        resolve();
                    });
                });
            });
        }
        getConversation(id: number): api.ConversationModel {
            if (!id) { return null; }
            return (this.activeConversations as any).find(x => x.ID === id)
                || (this.endedConversations as any).find(x => x.ID === id);
        }
        postMessageToConversation(conversationID: number, message: string): ng.IPromise<void> {
            return this.$q<void>((resolve, reject) => {
                if (!conversationID || !message) {
                    reject("Invalid arguments");
                    return;
                }
                this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                    const model = <api.MessageModel>{
                        Active: true,
                        CreatedDate: new Date(),
                        SentByUserID: user.userID,
                        ConversationID: conversationID,
                        Body: message,
                        IsReplyAllAllowed: true,
                    };
                    this.cvApi.messaging.PostMessageToConversation(model).then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded || !r.data.Result) {
                            reject("Could not post a message to the conversation.");
                            return;
                        }
                        /* NOTE: Don't do this here, let the pull from server bring it back in
                         * this.conversationMessages[conversationID].push(response.data.Result); */
                        resolve();
                    });
                });
            });
        }
        endConversation(conversationID: number): void {
            if (!conversationID) { return; }
            this.cvApi.messaging.EndConversation(conversationID);
        }
        getFlashWindowOnIncomingMessage(): boolean {
            return this.flashWindowOnIncomingMessage;
        }
        setFlashWindowOnIncomingMessage(newValue: boolean): void {
            this.flashWindowOnIncomingMessage = newValue;
        }
        get onlineStatus(): string { return this._onlineStatus; }
        set onlineStatus(newStatus: string) {
            this._onlineStatus = newStatus;
            this.cvApi.contacts.SetCurrentUserOnlineStatus({ OnlineStatus: newStatus });
            if (newStatus !== "Away" && newStatus !== "Offline") {
                this.resetChatOnlineStatusSessionTimer();
            }
        }
        getOtherConversationUsersTypingStatuses(): Array<api.UserTypingStatus> {
            return this.otherConversationUsersTypingStatuses;
        }
        getOtherConversationUsersTypingStatusesPromise(): ng.IPromise<Array<api.UserTypingStatus>> {
            return this.$q<Array<api.UserTypingStatus>>((resolve, reject) => {
                this.cvApi.messaging.GetOtherConversationUsersTypingStatuses()
                    .then(response => {
                        if (angular.toJson(this.otherConversationUsersTypingStatuses) !== angular.toJson(response.data.Result)) {
                            this.otherConversationUsersTypingStatuses = response.data.Result;
                        }
                        resolve(response.data.Result);
                    }, reason => reject(reason))
                    .catch(result => reject(result));
            });
        }
        getTypingStatus(): boolean { return this.typingStatus; }
        setTypingStatus(newStatus: boolean): void {
            this.typingStatus = newStatus;
            this.cvApi.messaging.SetConversationUserTypingStateForCurrentUser(newStatus);
            if (newStatus) {
                this.resetChatOnlineStatusSessionTimer();
                this.stopChatGoOfflineStatusSessionTimer();
                this.resetThisConversationUserTypingStatusTimer();
            }
        }
        // Timers Management
        startChatOnlineStatusSessionTimer(): void {
            this.chatOnlineStatusSessionTimer = this.$timeout(this.chatOnlineStatusSessionTimerExpiring, 5*60*1000);
        }
        startChatGoOfflineStatusSessionTimer(): void {
            this.chatGoOfflineStatusSessionTimer = this.$timeout(this.chatGoOfflineStatusSessionTimerExpiring, 60*60*1000);
        }
        startThisConversationUserTypingStatusTimer(): void {
            this.thisConversationUserTypingStatusTimer = this.$timeout(this.thisConversationUserTypingStatusTimerExpiring, 5*1000);
        }
        startOtherConversationUsersTypingStatusesTimer(): void {
            this.otherConversationUsersTypingStatusesTimer = this.$timeout(this.otherConversationUsersTypingStatusesTimerExpiring, 5*1000);
        }
        startConversationMessagesTimer(): void {
            this.conversationMessagesTimer = this.$timeout(this.conversationMessagesTimerExpiring, 5*1000);
        }
        // Note: This must remain an arrow function to work
        stopChatOnlineStatusSessionTimer = (): void => {
            if (!this.chatOnlineStatusSessionTimer) { return; }
            this.$timeout.cancel(this.chatOnlineStatusSessionTimer);
        }
        // Note: This must remain an arrow function to work
        stopChatGoOfflineStatusSessionTimer = (): void => {
            if (!this.chatGoOfflineStatusSessionTimer) { return; }
            this.$timeout.cancel(this.chatGoOfflineStatusSessionTimer);
        }
        // Note: This must remain an arrow function to work
        stopThisConversationUserTypingStatusTimer = (): void => {
            if (!this.thisConversationUserTypingStatusTimer) { return; }
            this.$timeout.cancel(this.thisConversationUserTypingStatusTimer);
        }
        // Note: This must remain an arrow function to work
        stopOtherConversationUsersTypingStatusesTimer = (): void => {
            if (!this.otherConversationUsersTypingStatusesTimer) { return; }
            this.$timeout.cancel(this.otherConversationUsersTypingStatusesTimer);
        }
        // Note: This must remain an arrow function to work
        stopConversationMessagesTimer = (): void => {
            if (!this.conversationMessagesTimer) { return; }
            this.$timeout.cancel(this.conversationMessagesTimer);
        }
        resetChatOnlineStatusSessionTimer(): void {
            this.stopChatOnlineStatusSessionTimer();
            this.startChatOnlineStatusSessionTimer();
        }
        resetChatGoOfflineStatusSessionTimer(): void {
            this.stopChatGoOfflineStatusSessionTimer();
            this.startChatGoOfflineStatusSessionTimer();
        }
        resetThisConversationUserTypingStatusTimer(): void {
            this.stopThisConversationUserTypingStatusTimer();
            this.startThisConversationUserTypingStatusTimer();
        }
        resetOtherConversationUsersTypingStatusesTimer(): void {
            this.stopOtherConversationUsersTypingStatusesTimer();
            this.startOtherConversationUsersTypingStatusesTimer();
        }
        resetConversationMessagesTimer(): void {
            this.stopConversationMessagesTimer();
            this.startConversationMessagesTimer();
        }
        // Note: This must remain an arrow function to work
        private chatOnlineStatusSessionTimerExpiring = (): void => {
            this.stopChatOnlineStatusSessionTimer();
            this.onlineStatus = "Away";
            this.$rootScope.$broadcast("chatSessionExpiring", "Away");
            this.resetChatGoOfflineStatusSessionTimer();
        }
        // Note: This must remain an arrow function to work
        private chatGoOfflineStatusSessionTimerExpiring = (): void => {
            this.stopChatOnlineStatusSessionTimer();
            this.onlineStatus = "Offline";
            this.$rootScope.$broadcast("chatSessionExpiring", "Offline");
            this.stopChatGoOfflineStatusSessionTimer();
        }
        // Note: This must remain an arrow function to work
        private thisConversationUserTypingStatusTimerExpiring = (): void => {
            this.stopThisConversationUserTypingStatusTimer();
            this.setTypingStatus(false);
        }
        // Note: This must remain an arrow function to work
        private otherConversationUsersTypingStatusesTimerExpiring = (): void => {
            this.stopOtherConversationUsersTypingStatusesTimer();
            this.getOtherConversationUsersTypingStatusesPromise()
                .then(() => this.resetOtherConversationUsersTypingStatusesTimer());
        }
        // Note: This must remain an arrow function to work
        private conversationMessagesTimerExpiring = (): void => {
            this.stopConversationMessagesTimer();
            this.loadActiveConversations(false);
            this.loadEndedConversations(false);
        }
        // Constructors
        constructor(
            private readonly $rootScope: ng.IRootScopeService,
            private readonly $timeout: ng.ITimeoutService,
            private readonly $log: ng.ILogService,
            private readonly $q: ng.IQService,
            private readonly cvApi: api.ICEFAPI,
            private readonly cvAuthenticationService: IAuthenticationService,
            private readonly cvMessagingFactory: factories.IMessagingFactory
        ) {
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    // Don't set up any chat services unless the user is logged in
                    return;
                }
                if (!this.conversationMessages) {
                    this.conversationMessages = {};
                }
                this.loadUserOnlineStatuses();
                this.loadActiveConversations(true);
                this.startChatOnlineStatusSessionTimer();
                this.startThisConversationUserTypingStatusTimer();
                this.startOtherConversationUsersTypingStatusesTimer();
                this.startConversationMessagesTimer();
            });
        }
    }
}
