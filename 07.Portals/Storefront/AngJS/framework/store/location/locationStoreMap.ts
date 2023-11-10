module cef.store.locations {
    cefApp.directive("locationStoreMap", (
            $filter: ng.IFilterService,
            cvApi: api.ICEFAPI,
            uiGmapGoogleMapApi,
            cvUserLocationService: services.IUserLocationService,
            cvStoreLocationService: services.IStoreLocationService,
            cefConfig: core.CefConfig,
            cvServiceStrings: services.IServiceStrings,
            cvLocationUtilityService: services.ILocationUtilityService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            mapControl: "=",
            showToolbar: "&"
        },
        templateUrl: $filter("corsLink")("/framework/store/location/locationStoreMap.html", "ui"),
        controller: function ($scope) {
            this.control = {};
            this.mapControl = this.control;
            this.userLocation = null;
            this.userCoords = null;
            this.userStore = null;
            this.stores = [];
            this.mapOptions = {
                zoom: 4,
                mapTypeControl: false,
                streetViewControl: false
            };
            this.radiusToZoom = radius => {
                return Math.round(14 - Math.log(radius) / Math.LN2);
            };
            this.markerEvents = {
                click: x => {
                    this.infoWindow.marker = x.model;
                    this.infoWindow.model = x.model;
                    this.infoWindow.show = true;
                }
            };
            this.infoWindow = {
                marker: {},
                show: false,
                closeClick: function () {
                    this.show = false;
                },
                options: {},
                catalogUrl: cefConfig.routes.catalog.root,
                isUserStore: function (storeId) {
                    return this.userStore && (this.userStore.ID === storeId);
                }.bind(this),
                onButtonClick: function () {
                    if (this.isUserStore(this.model.ID)) {
                        this.$filter("goToCORSLink")(this.catalogUrl);
                        return;
                    }
                    this.model.selectAsMine();
                }
            };
            this.getUser = function () {
                cvUserLocationService.requestUserLocation().then(r => {
                    this.userLocation = r;
                    this.userCoords = r.coords;
                    if (this.control && this.control.refresh) {
                        this.control.refresh(r.coords);
                    }
                    this.getStores();
                });
                cvStoreLocationService.getUserSelectedStore().then(userStore => {
                    if (userStore) {
                        this.userStore = userStore;
                    }
                });
            };
            this.getStores = function (coords) {
                const newCoords = (coords || this.userCoords);
                if (!newCoords) {
                    return;
                }
                cvStoreLocationService.lookupStoreLocationsLatLng(newCoords).then((response) => {
                    this.stores = response;
                });
            };
            const unbind1 = $scope.$on(cvServiceStrings.events.stores.selectionUpdate, (__, data) => {
                if (!data) { return; }
                this.userStore = data;
                this.infoWindow.closeClick();
            });
            const unbind2 = $scope.$on(cvServiceStrings.events.location.updated, (__, data) => {
                if (!data) { return; }
                if (data.coords && data.geo) this.userLocation = data;
                this.userCoords = data.lat ? data : (data.coords || null);
                if (data.radius) {
                    this.mapOptions.zoom = this.radiusToZoom(Number(data.radius));
                }
                if (this.userCoords) {
                    this.control && this.control.refresh && this.control.refresh(this.userCoords);
                    this.getStores();
                }
            });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
            this.getUser();
        },
        controllerAs: "locationStoreMapCtrl",
        bindToController: true
    }));
}
