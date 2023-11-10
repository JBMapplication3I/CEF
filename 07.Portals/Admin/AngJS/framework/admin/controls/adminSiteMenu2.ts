/**
 * @file framework/admin/controls/adminSiteMenu2.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc The admin site menu
 */
module cef.admin.controls {
    class AdminSiteMenuController extends core.TemplatedControllerBase {
        // Properties
        status = {
            isopen: false
        };
        // Functions
        toggleDropdown(): void {
            this.status.isopen = !this.status.isopen;
        }
        logout(): void {
            this.cvAuthenticationService.logout().then(() => this.$state.go("login"));
        }
        // Constructor
        constructor(
                private readonly $state: ng.ui.IStateService,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSecurityService: services.ISecurityService, // Used by UI
                private readonly cvViewStateService: services.IViewStateService) { // Used by UI
            super(cefConfig);
        }
    }

    adminApp.directive("cvAdminSiteMenu2", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/admin/controls/adminSiteMenu2.html", "ui"),
        controller: AdminSiteMenuController,
        controllerAs: "siteMenu2Ctrl"
    }));
}
