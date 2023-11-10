module cef.store.userDashboard.controls {
    class NotifyMeListController extends StaticCartViewerBaseController {
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $q: ng.IQService,
                protected readonly $window: ng.IWindowService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvInventoryService: services.IInventoryService) {
            super($q, $window, $rootScope, $scope, cefConfig, cvCartService, cvAuthenticationService, cvServiceStrings, cvInventoryService);
            this.cvAuthenticationService.preAuth().finally(() => this.cvCartService.loadCart(this.type(), true, "NotifyMeListController.ctor"));
        }

        type(): string { return this.cvServiceStrings.carts.types.notifyMe; }
    }

    cefApp.directive("cefNotifyMeList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/staticCarts/notifyMeList.html", "ui"),
        controller: NotifyMeListController,
        controllerAs: "udnmlCtrl"
    }));
}
