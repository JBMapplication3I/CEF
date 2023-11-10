module cef.store.locations {
    cefApp.directive("locationStoreModalContent", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/location/locationStoreModalContent.html", "ui"),
    }));
}
