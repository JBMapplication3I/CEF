/**]
 * @file framework/store/_factories/cvMessagingService.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Messaging Service, provides calls for getting an email-like internal
 * messaging system's messages and reply, forward, etc. type actions.
 */
module cef.store.factories {
    export interface IMessage {
        model: api.MessageModel;
        deactivate: () => ng.IHttpPromise<api.CEFActionResponse>;
        update: () => ng.IHttpPromise<api.CEFActionResponseT<number>>;
        send: () => ng.IHttpPromise<api.CEFActionResponseT<number>>;
        reply: () => ng.IPromise<IMessage>;
        forward: () => ng.IPromise<IMessage>;
        setFrom: (user) => void;
        getFull: () => ng.IPromise<IMessage>;
    }
    export interface IMessageBox {
        model: IMessage;
        currentMessage: IMessage;
        createMessage: () => IMessage;
        getMessages: (params: api.BaseSearchModel) => ng.IHttpPromise<api.MessagePagedResults>;
        getMessage: IMessage;
    }
    export interface IMessagingFactory {
        messageBoxFactory: () => IMessageBox;
        messageFactory: (messageModel?: api.MessageModel) => IMessage;
        messageRecipientFactory: (userModel: api.UserModel) => api.MessageRecipientModel;
        getMessageById: (id: number) => ng.IPromise<IMessage>;
        getContactList: (contacts?: Array<api.UserModel>) => ng.IPromise<Array<api.MessageRecipientModel>>;
        getContacts: (userIDs: number | Array<number>) => ng.IPromise<Array<api.MessageRecipientModel>>;
        setCurrentUser: (id?: number) => ng.IPromise<{}>;
    }
    export const cvMessagingFactoryFn =
        (
            cvApi: api.ICEFAPI,
            $q: ng.IQService,
            $filter: ng.IFilterService,
            cvAuthenticationService: services.IAuthenticationService
        ): IMessagingFactory => {
            let currentUser; // Will store the current user for the api.
            const messageBoxBase = <IMessageBox>{
                model: {},
                currentMessage: null,
                createMessage(): IMessage {
                    const newMessage = messageFactory();
                    newMessage.setFrom(currentUser);
                    return this.currentMessage = newMessage;
                },
                getMessages(params): ng.IHttpPromise<api.MessagePagedResults> {
                    return cvApi.messaging.GetMessagesForCurrentUser({
                        Paging: angular.isObject(params) && params.Paging ? params.Paging : { },
                        Active: true,
                        AsListing: true
                    }).then(r => {
                        this.model = r.data;
                        this.model.Results = this.model.Results.map(item => {
                            const mf = messageFactory(item);
                            // mf.model.CreatedDate = new Date(item.CreatedDate); // Get back-end to fix the date format returned
                            // mf.model.UpdatedDate = new Date(item.UpdatedDate);
                            return mf;
                        }).reverse();
                        return this.model;
                    });
                },
                getMessage: {}
            };
            const messageBase = <IMessage>{
                model: {},
                deactivate(): ng.IHttpPromise<api.CEFActionResponse> {
                    return cvApi.messaging.DeactivateMessageByID(this.model.ID);
                },
                update(): ng.IHttpPromise<api.CEFActionResponseT<number>> {
                    return cvApi.messaging.UpdateMessage(this.model);
                },
                send(): ng.IHttpPromise<api.CEFActionResponseT<number>> {
                    return cvApi.messaging.CreateMessage(this.model);
                },
                reply(): ng.IPromise<IMessage> {
                    // Move FROM to the TO field. Plus recipients if reply all is allowed.
                    const newRecipients = [messageRecipientFactory(this.model.SentByUser)];
                    if (this.model.IsReplyAllAllowed && false) { // Will need a check for a reply-all flag.
                        //_.uniq(newRecipients.concat(this.model.MessageRecipients)); //Might be how it's done?
                    }
                    const replyObj = {
                        ID: null,
                        Subject: `Re: ${this.model.Subject}`,
                        Body: `<br /><br /><hr />${this.model.Body}`,
                        CreatedDate: null,
                        UpdatedDate: null,
                        // SentByUserID: 2, //Needs to be the current user.
                        MessageRecipients: newRecipients
                    };
                    return $q((resolve/*, reject*/) => {
                        (Object as any).assign(this.model, replyObj);
                        resolve(this);
                    });
                },
                forward(): ng.IPromise<IMessage> {
                    const forwardObj = <api.MessageModel>{
                        // Base Properties
                        Active: true,
                        CreatedDate: new Date(),
                        // Message Properties
                        Subject: `Fw: ${this.model.Subject}`,
                        Body: `<br /><br /><hr />${this.model.Body}`,
                        //SentByUserID: 2, // Needs to be the current user
                        MessageRecipients: [],
                        IsReplyAllAllowed: false
                    };
                    return $q((resolve/*, reject*/) => {
                        (Object as any).assign(this.model, forwardObj);
                        resolve(this);
                    });
                },
                setFrom(user): void {
                    this.model.SentByUserID = user.ID;
                    this.model.SentByUser = user;
                },
                getFull(): ng.IPromise<IMessage>{
                    return $q((resolve, reject) => {
                        getMessageById(this.model.ID).then((response) => {
                            this.model = response.model;
                            resolve(true);
                        }, reject);
                    });
                }
            };
            function messageBoxFactory(): IMessageBox {
                return Object.create(messageBoxBase);
            }
            const messageModelEmpty = <api.MessageModel>{
                ID: null,
                Active: true,
                CreatedDate: null,
                Subject: null,
                Body: null,
                IsReplyAllAllowed: true,
                SentByUserID: 1,
                MessageRecipients: [],
                MessageAttachments: []
            };
            function messageFactory(messageModel?: api.MessageModel): IMessage {
                const newMsg = (Object as any).assign(Object.create(messageBase),
                    { model: messageModel || angular.copy(messageModelEmpty) });
                if (newMsg.model.CreatedDate) {
                    newMsg.model.CreatedDate = $filter("convertJSONDate")(newMsg.model.CreatedDate);
                }
                if (newMsg.model.UpdatedDate) {
                    newMsg.model.UpdatedDate = $filter("convertJSONDate")(newMsg.model.UpdatedDate);
                }
                return newMsg;
            }
            function messageRecipientFactory(userModel: api.UserModel): api.MessageRecipientModel {
                return (Object as any).assign(
                    { },
                    <api.MessageRecipientModel>{
                        ID: 0,
                        Active: true,
                        CreatedDate: new Date(),
                        ToUserID: 1,
                        MessageID: 1,
                        IsRead: false,
                        IsArchived: false,
                        HasSentAnEmail: false
                    }, {
                        ToUserID: userModel.ID,
                        ToUser: userModel
                    });
            }
            // TODO@EA: All this userList stuff should get cleaned up
            let userList: Array<api.UserModel> = [];
            function getAllUsers(): void {
                cvApi.contacts.GetUsers({
                    Active: true,
                    AsListing: true,
                    Paging: <api.Paging>{ StartIndex: 1, Size: 50 }
                }).then(r => userList = r.data.Results);
            }
            getAllUsers();
            function getContactList(contacts?: Array<api.UserModel>): ng.IPromise<Array<api.MessageRecipientModel>> {
                return $q((resolve, _) => {
                    resolve((contacts && angular.isArray(contacts) ? contacts : userList)
                        .map(item => messageRecipientFactory(item)));
                });
            }
            function getUser(id: number): ng.IHttpPromise<api.UserModel> {
                return cvApi.contacts.GetUserByID(id);
            }
            function setCurrentUser(id?: number): ng.IPromise<{}> {
                return $q((resolve, reject) => {
                    const setUser = (uid: number): void => {
                        if (!uid) {
                            reject("No ID supplied, and no user is currently logged in.");
                            return;
                        }
                        getUser(uid).then(response => {
                            currentUser = response.data;
                            resolve(currentUser);
                        }, () => reject(`User ID ${uid} requested, but no user was returned.`));
                    }
                    if (id) {
                        setUser(id);
                        return;
                    }
                    cvAuthenticationService.preAuth().finally(() => {
                        if (cvAuthenticationService.isAuthenticated()) {
                            cvAuthenticationService.getCurrentUserPromise().then(user => setUser(user.userID));
                        }
                    });
                });
            }
            function getContacts(userIDs: number | Array<number>): ng.IPromise<Array<api.MessageRecipientModel>> {
                return $q((resolve, reject) => {
                    if (!userIDs) {
                        reject("Needs an id or array of ids.");
                        return;
                    }
                    if (!angular.isArray(userIDs)) {
                        resolve(getUser(userIDs as number)
                            .then(r => messageRecipientFactory(r.data)),
                                  reject);
                        return;
                    }
                    $q.all((userIDs as Array<number>).map(item => getUser(item)))
                        .then(r => resolve(r.map(user => messageRecipientFactory((user as any).data))),
                              reject);
                });
            }
            function getMessageById(id: number): ng.IPromise<IMessage> {
                return $q((resolve, reject) => {
                    if (!id) {
                        reject("ID required");
                        return;
                    }
                    cvApi.messaging.GetMessageByID(id)
                        .then(r => resolve(messageFactory(r.data)), reject);
                });
            }
            setCurrentUser();
            return <IMessagingFactory>{
                messageBoxFactory: messageBoxFactory,
                messageFactory: messageFactory,
                messageRecipientFactory: messageRecipientFactory,
                getMessageById: getMessageById,
                getContactList: getContactList,
                getContacts: getContacts,
                setCurrentUser: setCurrentUser
            };
        };
}
