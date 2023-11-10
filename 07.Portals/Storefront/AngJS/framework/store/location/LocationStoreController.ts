module cef.store.locations {
    cefApp.controller("LocationStoreController", function (
            $rootScope: ng.IRootScopeService,
            $scope: ng.IScope,
            $filter: ng.IFilterService,
            $translate: ng.translate.ITranslateService,
            $uibModal: ng.ui.bootstrap.IModalService,
            cefConfig: core.CefConfig,
            cvServiceStrings: services.IServiceStrings,
            cvStoreLocationService: services.IStoreLocationService) {
        this.viewstate = {
            hasSelectedStore: false,
            loading: true
        };
        this.storeList = [];
        if (!this.title) {
            $translate("ui.storefront.common.Location.Plural").then(t => this.title = t);
        }
        if (!this.locatorUrl) {
            this.locatorUrl = cefConfig.routes.storeLocator.root; // Set a default
        }
        this.getUserStore = function () {
            cvStoreLocationService.getUserSelectedStore().then(store => {
                if (store) {
                    this.selectedStore = store;
                    this.viewstate.hasSelectedStore = true;
                    this.getNearbyStores();
                }
            }, this.getNearbyStores.bind(this));
        }

        this.filterSelectedStore = function (storeList) {
            if (storeList) {
                return storeList.filter(store => store.ID !== (this.selectedStore && this.selectedStore.ID));
            }
            return storeList;
        }

        this.getNearbyStores = function () {
            cvStoreLocationService.getUserNearbyStores().then(stores => {
                this.storeList = stores.filter(store => store.ID !== (this.selectedStore && this.selectedStore.ID));
                this.viewstate.loading = false;
            });
        }

        const unbind3 = $scope.$on(cvServiceStrings.events.stores.selectionUpdate, (__, store) => {
            this.selectedStore = store;
            this.getNearbyStores();
        });

        const unbind2 = $scope.$on(cvServiceStrings.events.stores.nearbyUpdate, (__, stores) => this.storeList = stores);

        this.setDefaultStore = function () {
            cvStoreLocationService.setDefaultUserStore().then(store => {
                if (store) {
                    this.selectedStore = store;
                    this.viewstate.hasSelectedStore = true;
                    this.getNearbyStores();
                }
            });
        };
        this.getUserStore();
        if (this.useDefaultStore()) {
            this.setDefaultStore();
        }

        this.isLocatorPage = function () {
            return window.location.pathname.toLowerCase() === this.locatorUrl.toLowerCase();
        };

        this.goToLocationPage = function () {
            $filter("goToCORSLink")("", "storeLocator");
        };

        this.goToCatalogPage = function () {
            if (window.location.pathname.toLowerCase().indexOf(cefConfig.routes.catalog.root.toLowerCase()) >= 0) {
                window.location.reload();
                return;
            }
            $filter("goToCORSLink")("", "catalog");
        };

        this.onLocatorNavigate = function () {
            if (!this.isLocatorPage()) {
                $filter("goToCORSLink")(this.locatorUrl);
            }
        };

        this.launchModal = function() {
            $uibModal.open({
                // Intentionally using an inline template to reduce excess code for modal
                template: "<div location-store-modal-content></div>",
                size: "lg",
                scope: $scope
            }).result.then(result => {
                if (result) {
                    this.onLocatorNavigate();
                }
            });
        };

        // Used to update storelist from map control
        const unbind1 = $scope.$on(cvServiceStrings.events.location.updated, (__, data) => {
            if (!data) {
                return;
            }
            this.userCoords = data.lat ? data : (data.coords || null);
            if (!this.userCoords) {
                return;
            }
            this.control && this.control.refresh && this.control.refresh(this.userCoords);
            cvStoreLocationService.lookupStoreLocationsLatLng(this.userCoords)
                .then(r => this.storeList = r);
        });
        $scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
            if (angular.isFunction(unbind1)) { unbind1(); }
            if (angular.isFunction(unbind2)) { unbind2(); }
            if (angular.isFunction(unbind3)) { unbind3(); }
        });
    });
}
