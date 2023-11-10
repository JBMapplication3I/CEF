module cef.store.globalization {
    export class GlobalizationModalController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        currencies: api.CurrencyModel[];
        languages: api.LanguageModel[];
        selectedCurrencyObject: api.CurrencyModel;
        selectedLanguageObject: api.LanguageModel;
        selectedCurrency: string;
        selectedLanguage: string;
        // Functions
        load(): void {
            this.cvCurrencyService.getAvailableCurrencies().then(r => {
                this.currencies = r.data.Results;
                this.cvCurrencyService.getCurrentCurrencyPromise().then(currency => {
                    this.selectedCurrencyObject = currency;
                    this.selectedCurrency = currency.CustomKey;
                });
            });
            this.cvLanguageService.getAvailableLanguages().then(r => {
                this.languages = r.data.Results;
                this.cvLanguageService.getCurrentLanguagePromise().then(language => {
                    this.selectedLanguageObject = language;
                    this.selectedLanguage = language.CustomKey;
                });
            });
        }
        ok(): void {
            this.$q.all([
                this.cvCurrencyService.changeToCurrencyPromise(this.selectedCurrency),
                this.cvLanguageService.changeToLanguagePromise(this.selectedLanguage)
            ]).then(() => this.$uibModalInstance.close(true));
        }
        cancel(): void {
            this.$uibModalInstance.dismiss(false);
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvCurrencyService: services.ICurrencyService,
                private readonly cvLanguageService: services.ILanguageService) {
            super(cefConfig);
            this.load();
        }
    }
}
