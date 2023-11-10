/**
 * @file framework/store/userDashboard/controls/users/userDetail.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc User Detail class for local admins
 */
module cef.store.userDashbord.controls.users {
    class UserDetailController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        // <None>
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefLocalAdminUserDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/users/userDetail.html", "ui"),
        controller: UserDetailController,
        controllerAs: "userDetailCtrl",
        bindToController: true
    }));
}
