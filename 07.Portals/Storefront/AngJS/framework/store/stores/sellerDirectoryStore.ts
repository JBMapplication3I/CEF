module cef.store.stores {
    cefApp.directive("cefSellerDirectoryStore", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { store: "=", view: "=" },
        templateUrl: $filter("corsLink")("/framework/store/stores/sellerDirectoryStore.html", "ui"),
        controller: () => { },
        controllerAs: "sellerDirectoryStoreCtrl",
        bindToController: true
    }));
}
