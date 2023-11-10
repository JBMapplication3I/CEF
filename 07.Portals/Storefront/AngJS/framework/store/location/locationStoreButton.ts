module cef.store.locations {
    cefApp.directive("locationStoreButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            useDefaultStore: "&",
            locatorUrl: "@"
        },
        templateUrl: $filter("corsLink")("/framework/store/location/locationStoreButton.html", "ui"),
        controller: "LocationStoreController",
        controllerAs: "locationButtonCtrl",
        bindToController: true
    }));
}
