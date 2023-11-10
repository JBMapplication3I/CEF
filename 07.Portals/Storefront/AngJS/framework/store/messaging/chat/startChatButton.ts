/**
 * @file framework/store/messaging/chat/startChatButton.ts
 * @author James Gray
 * @copyright 2017-2020 Clarity Ventures, Inc.
 * @since 4.7 2017-09-28
 * @desc A controller class & directive for the End User's Chat initiation
 *       button, this will open a modal with the Chat features available
 * @example
 *      <a class="btn btn-lg btn-block btn-primary"
 *         translate-attr="{ title: 'ui.storefront.messaging.chat.startChatButton.ChatNow' }"
 *         cef-start-chat-button
 *         target-user-id="productDetailsActionsSidebarCtrl.store.StoreUsers[0].ID"
 *         ><span data-translate="ui.storefront.messaging.chat.startChatButton.ChatNow"></span></a>
 */
module cef.store.messaging.chat {
    export class StartChatButtonController extends core.TemplatedControllerBase {
        // RequestForQuoteButtonController Properties
        targetUserId: number;     // From Scope
        targetProductId: number;  // From Scope
        targetOrderId: number;    // From Scope
        targetReturnId: number;   // From Scope
        targetQuoteId: number;    // From Scope
        targetInvoiceId: number;  // From Scope
        targetSampleId: number;   // From Scope
        // Constructors
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                private readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvChatService: services.ChatService, // Used by UI
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvLoginModalFactory: user.ILoginModalFactory) {
            super(cefConfig);
        }
        // Functions
        requireLoginForChat(): void {
            if (this.cvAuthenticationService.isAuthenticated()) {
                this.openChatModal();
                return;
            }
            this.cvAuthenticationService.preAuth().finally(() => {
                if (this.cvAuthenticationService.isAuthenticated()) {
                    this.openChatModal();
                    return;
                }
                this.cvLoginModalFactory(this.openChatModal);
            });
        }
        private openChatModal(): void {
            const modalInstance = this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/store/messaging/chat/chatWindow.html", "ui"),
                scope: this.$scope,
                size: "lg",
                backdrop: false,
                keyboard: false,
                controller: ChatWindowController,
                controllerAs: "chatWindowModalCtrl",
                resolve: {
                    targetUserId: () => this.targetUserId,
                    targetProductId: () => this.targetProductId,
                    targetOrderId: () => this.targetOrderId,
                    targetReturnId: () => this.targetReturnId,
                    targetQuoteId: () => this.targetQuoteId,
                    targetInvoiceId: () => this.targetInvoiceId,
                    targetSampleId: () => this.targetSampleId,
                    modalInstanceFunc: () => () => modalInstance
                }
            });
        }
        // Events
        // <None>
    }

    cefApp.directive("cefStartChatButton", (): ng.IDirective => ({
        restrict: "A",
        scope: {
            targetUserId: "=?",
            targetProductId: "=?",
            targetOrderId: "=?",
            targetReturnId: "=?",
            targetQuoteId: "=?",
            targetInvoiceId: "=?",
            targetSampleId: "=?"
        },
        /* Note: There is no template as this must be applied to a clickable element
         * which could follow multiple style patterns (<a>s, <button>s, etc.) */
        controller: StartChatButtonController,
        controllerAs: "startChatButtonCtrl",
        link(scope: ng.IScope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes, controller: StartChatButtonController) {
            element.on("click", (eventObject: JQueryEventObject): void => {
                if (attrs.$attr["disabled"] || element.attr("disabled") || element.hasClass("disabled")) {
                    return;
                }
                controller.requireLoginForChat();
            });
        },
        bindToController: true
    }));
}
