module cef.store.locations {
    cefApp.controller("customerLocationEntryController", function (
        $scope: ng.IScope,
        $rootScope: ng.IRootScopeService,
        cvUserLocationService: services.IUserLocationService,
        cvStoreLocationService: services.IStoreLocationService,
        cvLocationUtilityService: services.ILocationUtilityService,
        cvApi: api.ICEFAPI,
        cefConfig: core.CefConfig,
        cvCountryService: services.ICountryService) {
        this.userEntry = {
            zip: null,
            city: null,
            state: null,
            radius: null,
            units: "mi",
            toString: function () {
                return [this.city, this.state, this.zip].join(",");
            }
        };
        /* this.deviceLocationSupported = cvUserLocationService.deviceLocationSupported; // This function doesn't exist */
        this.stateList = [];
        cvCountryService.get({ code: cefConfig.countryCode })
            .then(c => cvStoreLocationService.getStateList(c.ID)
                .then(r => this.stateList = r));
        this.userFocusedEntry = false;
        this.updateUserLocation = location => {
            location = cvLocationUtilityService.simplifyGeolocation(location);
            const coords = cvLocationUtilityService.extractCoords(location);
            if (!coords) {
                return;
            }
            coords.radius = this.userEntry.radius;
            $rootScope.$broadcast(this.cvServiceStrings.events.location.updated, coords);
            if (this.locationChangeEvent) {
                $rootScope.$broadcast(this.locationChangeEvent, coords);
            }
        };
        this.lookupLocation = function () {
            // Use the userEntry data to get coordinates
            cvLocationUtilityService.reverseLookupLocation({ address: this.userEntry.toString() })
                .then(this.updateUserLocation);
        };
        this.zipFocused = function () {
            this.userEntry.city = "";
            this.userEntry.state = "";
        };
        this.cityStateFocused = function () {
            this.userEntry.zip = "";
        };
        this.getLocationFromDevice = function () {
            cvUserLocationService.requestUserLocation().then(this.updateUserLocation);
        };
        if (!$scope.customerLocationEntryCtrl) $scope.customerLocationEntryCtrl = this; // For when the controller is used outside a directive.
        $scope.$watch(() => this.userEntry.zip, (newVal) => {
            if (newVal && !this.userFocusedEntry) {
                this.lookupLocation();
            }
        });
        this.getLocationFromDevice(); // Init
    });

    cefApp.directive("customerLocationEntry", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            publishLocation: "=",
            locationChangeEvent: "@"
        },
        templateUrl: $filter("corsLink")("/framework/store/location/customerLocationEntry.html", "ui"),
        controller: "customerLocationEntryController",
        controllerAs: "customerLocationEntryCtrl",
        bindToController: true
    }));
    cefApp.directive("customerLocationEntrySimple", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            publishLocation: "=",
            locationChangeEvent: "@"
        },
        templateUrl: $filter("corsLink")("/framework/store/location/customerLocationEntrySimple.html", "ui"),
        controller: "customerLocationEntryController",
        controllerAs: "customerLocationEntrySimpleCtrl",
        bindToController: true
    }));
}
