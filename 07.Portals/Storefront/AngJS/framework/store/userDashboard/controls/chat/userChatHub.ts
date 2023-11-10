/**
 * @file framework/store/userDashboard/controls/chat/userChatHub.ts
 * @author James Gray
 * @copyright 2017-2020 Clarity Ventures, Inc.
 * @since 4.7 2017-09-28
 * @desc A controller class & directive for the Store Administrator's Message
 *       Hub (Inbox with Chat)
 */
module cef.store.userDashboard.controls.chats {
    export class UserChatHubController extends messaging.chat.ChatWindowBaseController {
        // Properties
        showOnlyUnread = false;
        // Functions
        doSearch() {
            // TODO@JTG: Filter the Active and Ended conversations lists by a search term and Only Unread flag
        }
        // Events
        // <None>
        // Constructors
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvChatService: services.ChatService,
                protected readonly cvConfirmModalFactory: store.modals.IConfirmModalFactory,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvServiceStrings: services.IServiceStrings) {
            super($rootScope, $scope, $q, $translate, cefConfig, cvApi, cvChatService, cvConfirmModalFactory, cvAuthenticationService, cvServiceStrings);
        }
    }

    cefApp.directive("cefUserChatHub", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/chat/userChatHub.html", "ui"),
        controller: UserChatHubController,
        controllerAs: "userChatHubCtrl",
        bindToController: true
    }));
}
