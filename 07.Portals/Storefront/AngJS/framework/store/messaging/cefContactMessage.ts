module cef.store.messaging {
    cefApp.directive("cefContactMessage", (
            cefConfig: core.CefConfig,
            cvApi: api.ICEFAPI,
            $stateParams: ng.ui.IStateParamsService,
            cvMessagingFactory: factories.IMessagingFactory
        ): ng.IDirective => ({
        restrict: "E",
        // NOTE: This is a control specific template that must remain inline
        template: `<div login-modal><cef-message></cef-message></div>`,
        scope: {
            message: "=msg",
            mode: "@",
            contacts: "=",
            subject: "=",
            sendCallback: "&",
            cancelCallback: "&"
        },
        controller: function ($scope) {
            this.viewstate = {
                readmode: true,
                replymode: false,
                composemode: false,
                showmessage: false,
                messagetext: null,
            };
            this.contactList = [];
            if (!this.message) {
                if ($stateParams["msg"] && ($stateParams["msg"] as any as factories.IMessage).model) {
                    this.message = $stateParams["msg"] as any as factories.IMessage;
                    this.message.getFull();
                } else if ($stateParams["id"]) {
                    cvMessagingFactory.getMessageById($stateParams["id"] as any as number).then((msgObj) => {
                        this.message = msgObj;
                    });
                } else {
                    this.message = cvMessagingFactory.messageFactory();
                }
            }
            $scope.$watch(() => this.contacts, (newVal) => {
                if (angular.isArray(newVal)) {
                    cvMessagingFactory.getContacts(newVal).then(response => {
                        this.message.model.MessageRecipients = response;
                        this.contactList = response;
                    });
                }
            });
            $scope.$watch(() => this.subject, (newVal) => {
                if (newVal) {
                    this.message.model.Subject = newVal;
                }
            });
            $scope.$watch(() => this.mode, (modeStr) => {
                switch (modeStr) {
                    case "compose": {
                        (Object as any).assign(this.viewstate, {
                            readmode: false,
                            replymode: false,
                            composemode: true
                        });
                        break;
                    }
                    default: { // read
                        (Object as any).assign(this.viewstate, {
                            readmode: true,
                            replymode: false,
                            composemode: false
                        });
                        break;
                    }
                }
            });
            this.selectOptions = {
                placeholder: "Select Recipients...",
                dataTextField: "ToUser.UserName",
                dataValueField: "ToUserID",
                //valuePrimitive: false,
                autoBind: false,
                dataSource: {
                    transport: {
                        read: function (options) {
                            options.success(this.contactList);
                        }.bind(this)
                    }
                }
            };
            this.sendMessage = function () {
                this.message.send().then(() => {
                    (Object as any).assign(this.viewstate, {
                        readmode: false,
                        replymode: false,
                        composemode: false,
                        showmessage: true,
                        messagetext: "Message sent."
                    });
                    if (angular.isFunction(this.sendCallback)) { this.sendCallback(); }
                });
            };
            this.cancel = function () {
                if (angular.isFunction(this.cancelCallback)) { this.cancelCallback(); }
            }
        },
        controllerAs: "messageCtrl",
        bindToController: true
    }));
}
