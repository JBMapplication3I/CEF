module cef.store.factories {
    export interface ILocationFactory {
        (initObj): any;
    }

    export const cvLocationFactoryFn = ($q: ng.IQService, cvLocationUtilityService: services.ILocationUtilityService) => {
        const baseObject = {
            syncGeoByCoords: function () {
                return $q((resolve, reject) => {
                    cvLocationUtilityService.reverseLookupLocation({ location: this.coords }).then((response) => {
                        this.geo = response;
                        resolve(this.geo);
                    }, reject);
                });
            },
            syncCoordsByGeo: function () {
                return $q((resolve, reject) => {
                    const coords = cvLocationUtilityService.extractCoords(this.geo);
                    if (coords) {
                        this.coords = coords;
                        resolve(this.coords);
                    } else {
                        reject("Could not extract coords from geo.");
                    }
                });
            }
        };
        Object.defineProperties(baseObject, {
            coords: {
                enumerable: true,
                set: function (newVal) {
                    this._coords = cvLocationUtilityService.extractCoords(newVal);
                },
                get: function () {
                    return this._coords;
                }
            },
            geo: {
                enumerable: true,
                set: function (newVal) {
                    if (newVal && newVal.length && newVal[0].address_components) { // It's from Google, so simplify it
                        this._geo = cvLocationUtilityService.simplifyGeolocation(newVal);
                    } else {
                        this._geo = newVal;
                    }
                },
                get: function () {
                    return this._geo;
                }
            }
        });
        function create(initObj) {
            return (Object as any).assign(Object.create(baseObject), initObj);
        }
        return create;
    };
}
