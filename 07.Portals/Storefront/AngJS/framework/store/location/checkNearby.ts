module cef.store.locations {
    class CheckNearbyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        product: api.ProductModel;
        // Properties
        selectedStore: api.StoreModel;
        radioModel: string;
        quantity = 1;
        mapControl: any;
        // Functions
        changeStore(store: api.StoreModel): void {
            this.selectedStore = store;
            this.radioModel = this.selectedStore && this.selectedStore.Name;
            this.product = this.cvInventoryService.factoryAssign(this.product);
        }
        viewStore(store: api.StoreModel): void {
            this.changeStore(store);
            if (this.mapControl) {
                this.mapControl.refresh(store["coords"]);
            }
        }
        centerMapOnUser(): void {
            this.cvUserLocationService.getUserLocation().then(l => {
                if (this.mapControl) {
                    this.mapControl.refresh(this.cvLocationUtilityService.extractCoords(l.coords));
                }
            });
        }
        buy(id: number, cartType: string, quantity: number, product): void {
            this.cvCartService.addCartItem(
                    id,
                    cartType,
                    quantity,
                    <services.IAddCartItemParams>null,
                    product)
                .then(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.modals.close));
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                readonly $rootScope: ng.IRootScopeService,
                readonly $scope: ng.IScope,
                readonly cvCartService: services.ICartService,
                readonly cvStoreLocationService: services.IStoreLocationService,
                private readonly cvInventoryService: services.IInventoryService,
                private readonly cvUserLocationService: services.IUserLocationService,
                private readonly cvLocationUtilityService: services.ILocationUtilityService,
                readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
            cvStoreLocationService.getUserSelectedStore().then(store => this.changeStore(store));
            const unbind1 = $scope.$on(cvServiceStrings.events.stores.selectionUpdate,
                (__, store) => this.selectedStore = store);
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
            // TODO: Rework with 2020.4 inventory and brands/stores
            // cvInventoryService.getStoreListWithInventory(this.product as api.HasInventoryObject)
            //     .then(storeList => this.storeList = storeList);
        }
    }

    cefApp.directive("cefCheckNearby", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            product: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/location/checkNearby-Modal.html", "ui"),
        controller: CheckNearbyController,
        controllerAs: "checkNearbyCtrl",
        bindToController: true
    }));
}
