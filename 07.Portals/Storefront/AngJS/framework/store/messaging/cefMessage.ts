module cef.store.messaging {
    cefApp.directive("cefMessage", ($filter: ng.IFilterService, $stateParams: ng.ui.IStateParamsService, cvMessagingFactory: factories.IMessagingFactory): ng.IDirective => ({
        restrict: "EA",
        scope: {
            message: "=msg",
            mode: "@",
            sendCallback: "&",
            cancelCallback: "&"
        },
        templateUrl: $filter("corsLink")("/framework/store/messaging/messaging-detail.html", "ui"),
        controller($scope, $state: ng.ui.IStateService) {
            this.viewstate = {
                readmode: true,
                replymode: false,
                forwardmode: false,
                composemode: false,
                showmessage: false,
                messagetext: ""
            };
            if (!this.message) {
                if ($stateParams["msg"] && ($stateParams["msg"] as any as factories.IMessage).model) {
                    this.message = ($stateParams["msg"] as any as factories.IMessage);
                    this.message.getFull();
                } else if ($stateParams["id"]) {
                    cvMessagingFactory.getMessageById($stateParams["id"] as any as number)
                        .then(msgObj => this.message = msgObj);
                }
            }
            $scope.$watch("messageCtrl.mode", (modeStr: string): void => {
                switch (modeStr) {
                    case "reply": {
                        (Object as any).assign(this.viewstate, {
                            readmode: false,
                            replymode: true,
                            forwardmode: false,
                            composemode: false
                        });
                        break;
                    }
                    case "forward": {
                        (Object as any).assign(this.viewstate, {
                            readmode: false,
                            replymode: false,
                            forwardmode: true,
                            composemode: false
                        });
                        break;
                    }
                    case "compose": {
                        (Object as any).assign(this.viewstate, {
                            readmode: false,
                            replymode: false,
                            forwardmode: false,
                            composemode: true
                        });
                        break;
                    }
                    case "deleted": {
                        (Object as any).assign(this.viewstate, {
                            readmode: false,
                            replymode: false,
                            forwardmode: false,
                            composemode: false,
                            showmessage: true,
                            messagetext: "Message deleted."
                        });
                        break;
                    }
                    default: { // read
                        (Object as any).assign(this.viewstate, {
                            readmode: true,
                            replymode: false,
                            forwardmode: false,
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
                        read(options) {
                            cvMessagingFactory.getContactList().then((response) => {
                                options.success(response);
                            });
                        }
                    }
                }
            };
            this.sendMessage = function () {
                this.message.send().then(() => {
                    (Object as any).assign(this.viewstate, {
                        readmode: false,
                        replymode: false,
                        forwardmode: false,
                        composemode: false,
                        showmessage: true,
                        messagetext: "Message sent."
                    });
                    if (angular.isFunction(this.sendCallback)) { this.sendCallback(); }
                });
            };

            this.cancel = function () {
                if (angular.isFunction(this.cancelCallback)) { this.cancelCallback(); }
                $state.go("userDashboard.inbox.folder", { folder: "inbox" });
            };
        },
        controllerAs: "messageCtrl",
        bindToController: true
    }));
}
