module cef.store.globalization {
    class CurrencyButtonController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        selectedCurrencyObject: api.CurrencyModel;
        selectedCurrency: string;
        // Functions
        open(callback: (...args: any[]) => void): void {
            this.$uibModal.open({
                size: "sm",
                templateUrl: this.$filter("corsLink")("/framework/store/globalization/currencySelectorModal.html", "ui"),
                controller: CurrencyModalController,
                controllerAs: "currencySelectorModalCtrl",
            }).result.then((result: boolean) => {
                if (angular.isFunction(callback)) { callback(result); }
            });
        }
        readCurrency(): void {
            this.cvCurrencyService.getCurrentCurrencyPromise().then(currency => {
                this.selectedCurrencyObject = currency;
                this.selectedCurrency = currency.CustomKey;
            });
        }
        // Events
        // <None>
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $filter: ng.IFilterService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvCurrencyService: services.ICurrencyService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.currency.changeFinished, () => this.readCurrency());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
            this.readCurrency();
        }
    }

    cefApp.directive("cefCurrencySelectorButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/globalization/currencySelectorButton.html", "ui"),
        controller: CurrencyButtonController,
        controllerAs: "currencySelectorButtonCtrl",
        bindToController: true
    }));
}
