module cef.store.locations {
    cefApp.directive("locationStoreDetails", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            storeData: "=locationStoreDetails",
            showSelect: "&",
            selectCallback: "&",
            selectedStoreTrue: "=?",
            storeAction: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/location/locationStoreDetails.html", "ui"),
        controller: function () {
            this.onStoreSelected = function (storeData) {
                this.selectCallback({ store: storeData });
            }.bind(this);
        },
        controllerAs: "locationStoreCtrl",
        bindToController: true
    }));
}
