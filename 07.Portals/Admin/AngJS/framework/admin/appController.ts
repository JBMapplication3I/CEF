module cef.admin {
    // This class controller is used in the skin file, do not remove
    export class AppController {
        constructor(
                readonly $scope: ng.IRootScopeService,
                readonly $rootScope: ng.IRootScopeService,
                readonly $timeout: ng.ITimeoutService,
                readonly $state: ng.ui.IStateService,
                readonly $window: ng.IWindowService,
                readonly cvServiceStrings: services.IServiceStrings,
                readonly cvAuthenticationService: services.IAuthenticationService) {
            $rootScope.loading = { loaded: false, outstandingRequests: 0 };
            $rootScope.$state = $state;
            $rootScope.$window = $window;
            cvAuthenticationService.preAuth().finally(() => {
                if (!cvAuthenticationService.isAuthenticated()) {
                    $state.go("login");
                }
            });
            const unbind1 = $scope.$on("cfpLoadingBar:completed", () => {
                // Wait an instant before setting loaded to allow JavaScript to render
                $timeout(() => {
                    $rootScope.loading.loaded = true;
                    $rootScope.$apply();
                    $(window).resize();
                }, 400);
            });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    adminApp.controller("AppController", AppController);
}
