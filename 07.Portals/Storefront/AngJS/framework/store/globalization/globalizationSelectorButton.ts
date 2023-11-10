module cef.store.globalization {
    class GlobalizationButtonController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        selectedCurrencyObject: api.CurrencyModel;
        selectedCurrency: string;
        selectedLanguageObject: api.LanguageModel;
        selectedLanguage: string;
        // Functions
        open(callback: (...args: any[]) => void) {
            this.$uibModal.open({
                size: "sm",
                templateUrl: this.$filter("corsLink")("/framework/store/globalization/globalizationSelectorModal.html", "ui"),
                controller: GlobalizationModalController,
                controllerAs: "globalizationSelectorModalCtrl"
            }).result.then(result => {
                if (angular.isFunction(callback)) { callback(result); }
            });
        }
        readCurrency(): void {
            this.cvCurrencyService.getCurrentCurrencyPromise().then(currency => {
                this.selectedCurrencyObject = currency;
                this.selectedCurrency = currency.CustomKey;
            });
        }
        readLanguage(): void {
            this.cvLanguageService.getCurrentLanguagePromise().then(language => {
                this.selectedLanguageObject = language;
                this.selectedLanguage = language.CustomKey;
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
                private readonly cvCurrencyService: services.ICurrencyService,
                private readonly cvLanguageService: services.ILanguageService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.currency.changeFinished, () => this.readCurrency());
            const unbind2 = $scope.$on(cvServiceStrings.events.lang.changeFinished, () => this.readLanguage());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
            this.readCurrency();
            this.readLanguage();
        }
    }

    cefApp.directive("cefGlobalizationSelectorButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/globalization/globalizationSelectorButton.html", "ui"),
        controller: GlobalizationButtonController,
        controllerAs: "globalizationSelectorButtonCtrl"
    }));
}
