module cef.store.locations.checkInventory {
    // This controller needs to stay in the injectable format and maintain use of the word 'function'
    cefApp.controller("CheckInventoryController",
        function ($scope: ng.IScope,
                  $filter: ng.IFilterService,
                  $uibModal: ng.ui.bootstrap.IModalService,
                  cvCartService: services.ICartService,
                  cvStoreLocationService: services.IStoreLocationService) {
            this.storeList = [] as api.StoreModel[];
            this.getStore = function () {
                cvStoreLocationService.getUserSelectedStore().then(result => {
                    this.store = result;
                    this.getStoreList();
                });
            }
            this.getStoreList = function () {
                // TODO: Rework for modern branding/stores 2020.4
                // cvInventoryService.getStoreListWithInventory(this.product as api.HasInventoryObject).then(storeList => {
                //     this.storeList = storeList;
                //     // Get selected store's distance
                //     const storeWithDistance: api.StoreModel = _.find(this.storeList, x => x.CustomKey === this.store.CustomKey);
                //     if (storeWithDistance) {
                //         this.store.Distance = storeWithDistance.Distance;
                //     }
                //     // Hide selected store from list
                //     this.updateSelectedStore();
                // });
            }
            this.updateSelectedStore = function () {
                // Update entire set of stores to show active
                this.storeList.forEach((_, i) => this.storeList[i].Active = true);
                // Set current store to inactive so it doesn't get duplicated in list
                this.storeList[this.storeList.findIndex(store => store.CustomKey === this.store.CustomKey)].Active = false;
                this.storeList = this.storeList.sort((store1, store2) => store1.Distance < store2.Distance);
            }
            /*this.centerMapOnUser = function() {
                cvUserLocationService.getUserLocation().then((location) => {
                    // Not sure why the mapControl doesn't like my location object, so simplifying
                    const coords = cvLocationUtilityService.extractCoords(location.coords);
                    this.mapControl && this.mapControl.refresh(coords);
                });
            }*/
            this.open = function () {
                const modalInstance = $uibModal.open({
                    templateUrl: $filter("corsLink")("/framework/store/location/checkInventory/views/modal-checkInventory.html", "ui"),
                    size: "lg",
                    scope: $scope
                });
                this.buy = (id: number, cartType: string, quantity: number, product: (api.ProductModel | api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>)) => {
                    cvCartService.addCartItem(id, cartType, quantity, <services.IAddCartItemParams>null, product);
                    modalInstance.close();
                };
                this.getStore();
                const unbind1 = $scope.$on(this.cvServiceStrings.events.stores.selectionUpdate, (e, store) => {
                    this.store = store;
                    this.getStoreList();
                });
                $scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                    if (angular.isFunction(unbind1)) { unbind1(); }
                });
            }
        });

    cefApp.directive("cefCheckInventoryButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            text: "@",
            product: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/location/checkInventory/checkInventoryButton.html", "ui"),
        controller: "CheckInventoryController",
        controllerAs: "checkInventoryCtrl",
        bindToController: true
    }));
}
