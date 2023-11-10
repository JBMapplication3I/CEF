module cef.store.widgets.loginForPricingWidget {
    cefApp.directive("cefLoginForPricing", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { key: "@?" },
        replace: true, // Required for overlay mode
        templateUrl: $filter("corsLink")("/framework/store/widgets/loginForPricingWidget.html", "ui"),
        controller() { },
        controllerAs: "loginForPricingCtrl",
        bindToController: true
    }));
}
