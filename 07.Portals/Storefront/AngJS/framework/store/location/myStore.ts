module cef.store.locations {
    class MyStoreController extends core.TemplatedControllerBase {
        // Properties
        currentStore: api.StoreModel;
        // Functions
        getStore(): void {
            this.cvStoreLocationService.getUserSelectedStore()
                .then(store => this.currentStore = store);
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvStoreLocationService: services.IStoreLocationService) {
            super(cefConfig);
            this.getStore();
            const unbind1 = $scope.$on(cvServiceStrings.events.stores.selectionUpdate, () => this.getStore());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("myStore", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/location/myStore.html", "ui"),
        controller: MyStoreController,
        controllerAs: "myStoreCtrl"
    }));
}
