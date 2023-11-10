module cef.store.services {
    export interface ILocationUtilityService {
        coordsObj;
        extractCoords;
        getLatLngFromGeocode;
        simplifyGeolocation;
        requestDeviceLocation;
        reverseLookupLocation;
    }

    export const cvLocationUtilityServiceFn = (
            cefConfig: core.CefConfig,
            $q: ng.IQService,
            $http: ng.IHttpService,
            $location: ng.ILocationService,
            subdomain: string,
            uiGmapGoogleMapApi,
            $cookies: ng.cookies.ICookiesService) => {
        function coordsObj(lat: number, lng: number, rad: number = null, units = api.LocatorUnits.Miles) {
            return {
                lat: lat,
                lng: lng,
                latitude: lat,
                longitude: lng,
                radius: rad,
                units: units
            };
        }

        function extractCoords(obj) {
            if (!angular.isObject(obj)) {
                return null;
            }
            if (obj.lat && obj.lng) {
                return coordsObj(obj.lat, obj.lng, obj.radius);
            }
            if (obj.latitude && obj.longitude) {
                return coordsObj(obj.latitude, obj.longitude, obj.radius);
            }
            return null;
        }

        function getLatLngFromGeocode(geocode) {
            return geocode.length && coordsObj(geocode[0].geometry.location.lat(), geocode[0].geometry.location.lng());
        }

        function getAddressComponent(geocoderResponse, component, short) { // Borrowed a bit of code for fishing usable data from reverse geolocation lookups
            let element = null;
            angular.forEach(geocoderResponse.address_components, address_component => {
                if (address_component.types[0] === component) {
                    element = (short) ? address_component.short_name : address_component.long_name;
                }
            });
            return element;
        }

        function simplifyGeolocation(locationObj) {
            const baseObj = { zip: null, city: null, state: "" };
            if (locationObj && locationObj.length && locationObj[0].address_components) {
                angular.extend(baseObj, {
                    zip: getAddressComponent(locationObj[0], "postal_code", false),
                    city: getAddressComponent(locationObj[0], "locality", false),
                    state: getAddressComponent(locationObj[0], "administrative_area_level_1", true),
                    lat: locationObj[0].geometry.location.lat(),
                    lng: locationObj[0].geometry.location.lng()
                });
            }
            return baseObj;
        }

        function requestDeviceLocation() {
            return $q((resolve, reject) => {
                const beenPrompted = $cookies.get("cefLocationRequested") === "true";
                if (!beenPrompted) {
                    $cookies.put("cefLocationRequested", "true",
                    <ng.cookies.ICookiesOptions>{
                        path: "/",
                        expires: "Session",
                        domain: cefConfig.useSubDomainForCookies || !subdomain
                            ? $location.host()
                            : $location.host().replace(subdomain, "")
                    });
                }
                if (!navigator.geolocation) {
                    reject("Browser does not support geolocation.");
                    return;
                }
                function geolocationSuccess(position) {
                    resolve(coordsObj(position.coords.latitude, position.coords.longitude));
                }
                function googlelocationSuccess(position) {
                    resolve(coordsObj(position.data.location.lat, position.data.location.lng));
                }
                function geolocationFail() {
                    // Device location has failed, try coarse location detection.
                    // A workaround I found on StackExchange. Might need to find a better way to use the API.
                    $http({
                        method: "POST",
                        url: `https://www.googleapis.com/geolocation/v1/geolocate?key=${cefConfig.google.maps.apiKey}`
                    }).then(googlelocationSuccess, googlelocationFail);
                }
                function googlelocationFail() {
                    reject("Geolocation refused or failed.");
                }
                if (beenPrompted) {
                    geolocationFail(/*"Device location has already been requested."*/);
                } else {
                    navigator.geolocation.getCurrentPosition(geolocationSuccess, geolocationFail); // Now requires HTTPS to work!
                }
            });
        }

        function reverseLookupLocation(geocoderRequest) {
            return $q((resolve, reject) => {
                uiGmapGoogleMapApi.then(maps => {
                    geocoderRequest && new maps.Geocoder().geocode(geocoderRequest, (results, status) => {
                        if (status === "OK" && results.length) {
                            resolve(results);
                        }
                        reject("Geocoder request failed.");
                    });
                });
            });
        }

        return <ILocationUtilityService>{
            coordsObj: coordsObj,
            extractCoords: extractCoords,
            getLatLngFromGeocode: getLatLngFromGeocode,
            simplifyGeolocation: simplifyGeolocation,
            requestDeviceLocation: requestDeviceLocation,
            reverseLookupLocation: reverseLookupLocation
        };
    };
}
