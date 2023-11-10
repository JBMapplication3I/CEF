module cef.store.services {
    export interface IStoreLocationService {
        lookupStoreLocationsLatLng: (coords, distance?: number) => ng.IPromise<Array<api.StoreModel>>;
        getStore; // Is angular.noop
        getNearestStore: (postal: string) => ng.IHttpPromise<api.StorePagedResults>;
        getStateList: (countryID: number) => ng.IPromise<api.RegionModel[]>;
        getMapImageUrl: (paramsObj?) => string;
        getStoreByID: (storeId: number) => ng.IPromise<api.StoreModel>;
        storeLocationFactory: (store: api.StoreModel) => api.StoreModel;
        getUserSelectedStore: () => ng.IPromise<api.StoreModel>;
        getUsersSelectedStore: () => api.StoreModel;
        setUserSelectedStore: (store: api.StoreModel, callback?: Function) => void;
        clearUserSelectedStore: () => void;
        setDefaultUserStore: () => ng.IPromise<api.StoreModel>;
        getUserNearbyStores: () => ng.IPromise<api.StoreModel[]>;
        getUserSelectedStoreByName: (location: string) => api.StoreModel;
    }

    export class StoreLocationService implements IStoreLocationService {
        // User Store selections are here to avoid a circular dependency for all the utility functions here
        userSelectedStoreCacheFactory: core.ICacheFactoryService<api.StoreModel>;
        userNearbyStoresCacheFactory: core.ICacheFactoryService<Array<api.StoreModel>>;
        usersSelectedStore = null;
        userLocation = null;
        searchIsRunning = false;
        getStore = angular.noop();
        getUserSelectedStore: () => ng.IPromise<api.StoreModel>;
        getUserNearbyStores: () => ng.IPromise<api.StoreModel[]>;

        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $q: ng.IQService,
                protected readonly $cookies: ng.cookies.ICookiesService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvRegionService: services.IRegionService,
                protected readonly $location: ng.ILocationService,
                protected readonly $anchorScroll,
                protected readonly subdomain: string,
                protected readonly CacheFactoryService: core.ICacheFactoryService<any>,
                protected readonly cvUserLocationService: IUserLocationService,
                protected readonly cvLocationFactory: factories.ILocationFactory,
                protected readonly cvLocationUtilityService: ILocationUtilityService) {
            $rootScope.cvStoreLocationService = this;
            this.userSelectedStoreCacheFactory = this.CacheFactoryService(this.loadStoreFn, this.saveStoreFn, this);
            this.userNearbyStoresCacheFactory = this.CacheFactoryService(this.loadNearbyStores, this.saveNearbyStores, this);
            this.getUserSelectedStore = this.userSelectedStoreCacheFactory.get;
            this.getUserNearbyStores = this.userNearbyStoresCacheFactory.get;
            $rootScope.$on(cvServiceStrings.events.stores.selectionUpdate,
                (__, location) => this.userNearbyStoresCacheFactory.set(location));
        }

        private loadStoreFn(storeLocService: StoreLocationService | { [keys: number]: any } | any[]): ng.IPromise<api.StoreModel> {
            let storeLocSvc: StoreLocationService = null;
            if (angular.isArray(storeLocService)) {
                storeLocSvc = storeLocService[0];
            } else {
                storeLocSvc = storeLocService as StoreLocationService;
            }
            storeLocSvc.searchIsRunning = true;
            return storeLocSvc.$q((resolve, reject) => {
                const storeId = storeLocSvc.$cookies.get("cefSelectedStoreId");
                if (!storeId || Number(storeId) <= 0) {
                    reject("No selected store ID found");
                    storeLocSvc.searchIsRunning = false;
                    return;
                }
                storeLocSvc.cvApi.stores.GetStoreByID(Number(storeId)).then(response => {
                    storeLocSvc.usersSelectedStore = storeLocSvc.storeLocationFactory(response.data);
                    storeLocSvc.searchIsRunning = false;
                    resolve(storeLocSvc.usersSelectedStore);
                }).catch((err) => {
                    storeLocSvc.searchIsRunning = false;
                    return reject(err);
                });
            });
        }

        private saveStoreFn(store: api.StoreModel, storeLocService: StoreLocationService) {
            let storeLocSvc: StoreLocationService = null;
            if (angular.isArray(storeLocService)) {
                storeLocSvc = storeLocService[0];
            } else {
                storeLocSvc = storeLocService as StoreLocationService;
            }
            return storeLocSvc.$q((resolve, reject) => {
                storeLocSvc.$cookies.put(
                    "cefSelectedStoreId",
                    `${store.ID}`,
                    <ng.cookies.ICookiesOptions>{
                        path: "/",
                        /*expires: never*/
                        domain: storeLocSvc.cefConfig.useSubDomainForCookies || !storeLocSvc.subdomain
                            ? storeLocSvc.$location.host()
                            : storeLocSvc.$location.host().replace(storeLocSvc.subdomain, "")
                    });
                // TODO@ME: Add a global-level event broadcast here?
                storeLocSvc.$rootScope.$broadcast(storeLocSvc.cvServiceStrings.events.stores.selectionUpdate, store);
                storeLocSvc.usersSelectedStore = storeLocSvc.storeLocationFactory(store);
                resolve(storeLocSvc.usersSelectedStore);
            });
        }

        setDefaultUserStore(): ng.IPromise<api.StoreModel> {
            // If there's no selected store, select one for them based on available location
            return this.$q((resolve, reject) => {
                this.userSelectedStoreCacheFactory.get().then(
                    response => resolve(response),
                    r => {
                        this.cvUserLocationService.requestUserLocation().then(loc => {
                            const location = angular.copy(loc);
                            (location as any).coords.radius = 200; // Default only if within 200mi of a store.
                            this.lookupStoreLocationsLatLng((location as any).coords).then(r2 => {
                                if (!r2.length) {
                                    reject();
                                    return;
                                }
                                this.userSelectedStoreCacheFactory.set(r2[0]);
                                resolve(r2[0]);
                            });
                        }, reject);
                    }
                );
            });
        }

        clearUserSelectedStore(): void {
            this.userSelectedStoreCacheFactory = null;
            this.usersSelectedStore = null;
            this.$cookies.remove("cefSelectedStoreId", <ng.cookies.ICookiesOptions>{ path: "/" });
        }

        loadNearbyStores(storeLocService: StoreLocationService): ng.IHttpPromise<Array<api.StoreModel>> {
            if (storeLocService[0]) {
                storeLocService = storeLocService[0];
            }
            return storeLocService.cvUserLocationService.requestUserLocation().then(location => {
                if (location === storeLocService.userLocation) { return undefined; }
                return storeLocService.lookupStoreLocationsLatLng(location.coords).then(r => {
                    const storeList = storeLocService.enhanceStoreList(r);
                    storeLocService.$rootScope.$broadcast(storeLocService.cvServiceStrings.events.stores.nearbyUpdate, storeList);
                    return storeList;
                });
            });
        }

        saveNearbyStores(location, storeLocService: StoreLocationService): ng.IPromise<Array<api.StoreModel>> {
            if (storeLocService[0]) {
                storeLocService = storeLocService[0];
            }
            return storeLocService.lookupStoreLocationsLatLng(location.coords).then(stores => {
                const storeList = storeLocService.enhanceStoreList(stores);
                storeLocService.$rootScope.$broadcast(storeLocService.cvServiceStrings.events.stores.nearbyUpdate, storeList);
                return storeList;
            });
        }

        enhanceStoreList(storeList: Array<api.StoreModel>): Array<api.StoreModel> {
            return storeList.map(store => this.storeLocationFactory(store));
        }

        setUserSelectedStore(store: api.StoreModel, callback?: Function): void {
            this.userSelectedStoreCacheFactory.set(store).then(() => {
                this.$rootScope.$broadcast(this.cvServiceStrings.events.stores.selectionUpdate, store);
                if (angular.isFunction(callback)) {
                    callback(store);
                }
            });
        }

        storeLocationFactory(store: api.StoreModel): api.StoreModel {
            const service = this;
            const tmpCoordsObj = store && store.Contact && store.Contact.Address
                ? { coords: { lat: store.Contact.Address.Latitude, lng: store.Contact.Address.Longitude } }
                : { coords: { lat: null, lng: null } };
            const loc = this.cvLocationFactory(tmpCoordsObj);
            return (Object as any).assign(store, {
                coords: loc.coords,
                getMapImageUrl: function (paramsObj): string {
                    if (!this.Contact || !this.Contact.Address) { return ""; }
                    return this.getMapImageUrl(
                        angular.extend(
                            paramsObj || {},
                            { lat: this.Contact.Address.Latitude,
                              lng: this.Contact.Address.Longitude }));
                },
                mapImageUrl: this.getMapImageUrl(tmpCoordsObj.coords),
                mapUrl: (() => {
                    if (!store || !store.Contact || !store.Contact.Address) { return ""; }
                    const addr = store.Contact.Address;
                    const joined = [
                        addr.Street1,
                        addr.City,
                        addr.RegionKey || (addr.Region ? addr.Region.CustomKey : ""),
                        addr.PostalCode
                    ].join(",");
                    return encodeURI(`http://maps.google.com?q=${joined}`);
                })(),
                anchor: () => this.$anchorScroll("selectedStoreAnchor"),
                selectAsMine: function (callback) {
                    service.userSelectedStoreCacheFactory.get().then(selectedStore => {
                        if (selectedStore && selectedStore.ID === this.ID) {
                            if (angular.isFunction(callback)) {
                                callback(this);
                            }
                            return;
                        }
                        if (!this.Distance) {
                            // Put the Distance value on it before setting it
                            this.lookupStoreLocationsLatLng(tmpCoordsObj).then((results: Array<api.StoreModel>) => {
                                this.Distance = _.find(results, x => x.ID === selectedStore.ID).Distance;
                                service.setUserSelectedStore(this, callback);
                            });
                            return;
                        } 
                        service.setUserSelectedStore(this, callback);
                    }, () => {
                        // Get store rejected. Assuming there is no store and setting one
                        service.setUserSelectedStore(this, callback);
                    });
                    this.anchor();
                }
            });
        }

        lookupStoreLocationsLatLng(coords, distance?: number): ng.IPromise<Array<api.StoreModel>> {
            if (!coords) {
                return this.$q.reject("Latitude and longitude are required.");
            }
            return this.cvApi.stores.GetStores(coords as api.GetStoresDto)
                .then(r => this.enhanceStoreList(r.data.Results));
        }

        getNearestStore(postal: string): ng.IHttpPromise<api.StorePagedResults> {
            if (!postal) {
                return this.$q.reject("A postal code is required.") as any;
            }
            return this.cvApi.stores.GetStores({
                ZipCode: postal,
                Radius: null,
                Active: true,
                AsListing: true
            });
        }

        getStateList(countryID: number): ng.IPromise<api.RegionModel[]> {
            return this.cvRegionService.search({
                Active: true,
                AsListing: true,
                Sorts: [{ field: "Name", order: 0, dir: "asc" }],
                CountryID: countryID
            });
        }

        getMapImageUrl(paramsObj): string { // Uses gmaps to get an image of a map.
            if (!paramsObj.lat || !paramsObj.lng) {
                return "";
            }
            const defaults = {
                lat: 0,
                lng: 0,
                width: 200,
                height: 200,
                zoom: 10,
                scale: 1,
                marker: true,
                icon: ""
            };
            const localParams = angular.extend({}, defaults, (angular.isObject(paramsObj) ? paramsObj : {}));
            function latlng() { return `center=${localParams.lat},${localParams.lng}&`; }
            function size() { return `size=${localParams.width}x${localParams.height}&`; }
            function zoomscale() { return `zoom=${localParams.zoom}&scale=${localParams.scale}&`; }
            function markers() { // For now it's either an icon image or the default mark, and only one in the center.
                if (localParams.marker && localParams.icon) { return icon(); }
                if (localParams.marker) { return mark(); }
                return "";
            }
            function mark() { return `markers=|${localParams.lat},${localParams.lng}&`; }
            function icon() { return `markers=icon:${encodeURI(localParams.icon)}|${localParams.lat},${localParams.lng}&`; }
            return "http://maps.googleapis.com/maps/api/staticmap?" +
                latlng() + zoomscale() + size() + markers() +
                `key=${this.cefConfig.google.maps.apiKey}`;
        }

        getStoreByID(storeID: number): ng.IPromise<api.StoreModel> {
            return this.$q((resolve, reject) => {
                if (!storeID || storeID <= 0) {
                    reject("No storeId passed");
                    return;
                }
                this.cvApi.stores.GetStoreByID(storeID)
                    .then(r => resolve(this.storeLocationFactory(r.data)))
                    .catch(reject);
            });
        }

        getUsersSelectedStore() {
            if (!this.usersSelectedStore && !this.searchIsRunning) { this.loadStoreFn(this); }
            return this.usersSelectedStore;
        }
        getUserSelectedStoreByName(location: string): api.StoreModel {
            if (!location) {this.loadStoreFn(this);}
            this.usersSelectedStore = location;
            return this.usersSelectedStore;
        }
    }
}
