module cef.store.locations {
    cefApp.directive("locationStoreModalButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            useDefaultStore: "&",
            locatorUrl: "@",
            title: "=?"
        },
        replace: true, // Required so it sits in the correct place for formatting and insertion to actual menu
        templateUrl: $filter("corsLink")("/framework/store/location/locationStoreModalButton.html", "ui"),
        controller: "LocationStoreController",
        controllerAs: "locationButtonCtrl",
        bindToController: true
    }));
}
