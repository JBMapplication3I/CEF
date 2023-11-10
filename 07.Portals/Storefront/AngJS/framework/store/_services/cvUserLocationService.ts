module cef.store.services {
    export interface IUserLocationService {
        // The requestUserLocation is for backwards-compatibility
        requestUserLocation;
        getUserLocation;
        setUserLocation;
    }

    export const cvUserLocationServiceFn = (
        $rootScope: ng.IRootScopeService,
        $q: ng.IQService,
        $cookies: ng.cookies.ICookiesService,
        $location: ng.ILocationService,
        subdomain: string,
        CacheFactoryService: core.ICacheFactoryService<any>,
        cefConfig: core.CefConfig,
        cvLocationFactory: factories.ILocationFactory,
        cvLocationUtilityService: ILocationUtilityService) =>
    {
        // This service handles data that globally applies to the current user.
        const userLocationStore = CacheFactoryService(loadUserLocation, saveUserLocation);

        function getCookieOptions(): ng.cookies.ICookiesOptions {
            return <ng.cookies.ICookiesOptions>{
                path: "/",
                expires: "Session",
                domain: cefConfig.useSubDomainForCookies || !subdomain
                    ? $location.host()
                    : $location.host().replace(subdomain, "")
            };
        }

        function loadUserLocation() {
            return $q((resolve, reject) => {
                const loc = cvLocationFactory({
                    coords: $cookies.getObject("cefCoords"),
                    geo: $cookies.getObject("cefGeo")
                });
                if (loc.coords && loc.geo) {
                    resolve(loc);
                    return;
                }
                cvLocationUtilityService.requestDeviceLocation().then(response => {
                    const newLocation = cvLocationFactory({ coords: response });
                    newLocation.syncGeoByCoords().then(() => {
                        $cookies.putObject("cefCoords", newLocation.coords, getCookieOptions());
                        $cookies.putObject("cefGeo", newLocation.geo, getCookieOptions());
                        resolve(newLocation);
                    });
                }, reject);
            });
        }

        function saveUserLocation(location) {
            return $q((resolve, reject) => {
                if (!angular.isObject(location) || !location.coords || !location.geo) {
                    reject();
                    return;
                }
                $cookies.putObject("cefCoords", location.coords, getCookieOptions());
                $cookies.putObject("cefGeo", location.geo, getCookieOptions());
                const newUserLocation = cvLocationFactory({
                    coords: location.coords,
                    geo: location.geo
                });
                // Add a global-level event broadcast here?
                $rootScope.$broadcast(this.cvServiceStrings.events.stores.selectionUpdate, newUserLocation);
                resolve(newUserLocation);
            });
        }

        function setUserLocation(location) {
            return userLocationStore.set(location);
        }

        function getUserLocation() {
            // Now a promise. Used to be a straight return
            return userLocationStore.get();
        }

        return {
            // The requestUserLocation is for backwards-compatibility
            requestUserLocation: userLocationStore.get,
            getUserLocation: getUserLocation,
            setUserLocation: setUserLocation
        };
    };
}
