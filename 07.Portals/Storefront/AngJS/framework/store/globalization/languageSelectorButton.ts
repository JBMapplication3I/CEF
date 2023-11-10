module cef.store.globalization {
    class LanguageButtonController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        selectedLanguageObject: api.LanguageModel;
        selectedLanguage: string;
        // Functions
        open(callback: (...args: any[]) => void): void {
            this.$uibModal.open({
                size: this.cvServiceStrings.modalSizes.sm,
                templateUrl: this.$filter("corsLink")("/framework/store/globalization/languageSelectorModal.html", "ui"),
                controller: LanguageModalController,
                controllerAs: "languageSelectorModalCtrl",
            }).result.then((result: boolean) => {
                if (angular.isFunction(callback)) { callback(result); }
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
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvLanguageService: services.ILanguageService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.lang.changeFinished, () => this.readLanguage());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
            this.readLanguage();
        }
    }

    cefApp.directive("cefLanguageSelectorButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/globalization/languageSelectorButton.html", "ui"),
        controller: LanguageButtonController,
        controllerAs: "languageSelectorButtonCtrl",
        bindToController: true
    }));
}
