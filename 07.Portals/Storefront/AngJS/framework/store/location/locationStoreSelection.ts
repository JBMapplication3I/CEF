module cef.store.locations {
    cefApp.directive("locationStoreSelection", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            storeData: "=locationStoreSelection",
            showSelect: "&",
            selectCallback: "&",
            selectedStore: "=currentStore",
            storeAction: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/location/locationStoreSelection.html", "ui"),
        controller: "LocationStoreController",
        controllerAs: "locationStoreCtrl",
        bindToController: true
    }));
}
