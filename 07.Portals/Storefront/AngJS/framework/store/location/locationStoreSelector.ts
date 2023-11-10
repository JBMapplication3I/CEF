module cef.store.locations {
    cefApp.directive("locationStoreSelector", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            storeList: "=?",
            selectCallback: "&",
            selectedStore: "=",
            storeAction: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/location/locationStoreSelector.html", "ui"),
        controller: () => {
            // Bypass if already on location page
            if (window.location.href.toLowerCase().match(/\/(store-locator|location)/)) {
                location.reload();
            }
        },
        controllerAs: "locationStoreSelectorCtrl",
        bindToController: true
    }));
}
