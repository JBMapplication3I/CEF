﻿module cef.store.userDashboard.controls {
    class FavoritesListController extends StaticCartViewerBaseController {
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $window: ng.IWindowService,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvInventoryService: services.IInventoryService) {
            super($q, $window, $rootScope, $scope, cefConfig, cvCartService, cvAuthenticationService, cvServiceStrings, cvInventoryService);
            this.cvAuthenticationService.preAuth().finally(() => this.cvCartService.loadCart(this.type(), true, "FavoritesListController.ctor"));
        }

        type(): string { return this.cvServiceStrings.carts.types.favorites; }
    }

    cefApp.directive("cefFavoritesList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/staticCarts/favoritesList.html", "ui"),
        controller: FavoritesListController,
        controllerAs: "udflCtrl"
    }));
}
