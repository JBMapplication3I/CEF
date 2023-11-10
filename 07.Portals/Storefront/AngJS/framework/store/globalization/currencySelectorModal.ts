module cef.store.globalization {
    export class CurrencyModalController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        currencies: api.CurrencyModel[];
        selectedCurrencyObject: api.CurrencyModel;
        selectedCurrency: string;
        // Functions
        load(): void {
            this.cvCurrencyService.getAvailableCurrencies().then(r => {
                this.currencies = r.data.Results;
                this.cvCurrencyService.getCurrentCurrencyPromise().then(currency => {
                    this.selectedCurrencyObject = currency;
                    this.selectedCurrency = currency.CustomKey;
                });
            });
        }
        ok(): void {
            /*
            this.cvCurrencyService.changeToCurrencyPromise(this.selectedCurrency)
                .then(() => this.$uibModalInstance.close(true));
            */
            this.setRunning();
            this.cvCurrencyService.changeToCurrencyPromise(this.selectedCurrency).then(() => {
                /*// TODO@JTG: Make a core-first version of this
                let go: boolean = false;
                switch (this.selectedCurrency) {
                    case "USD": {
                        go = window.location.host !== "amazon-en-us-usd.clarityclient.com";
                        break;
                    }
                    case "EUR": {
                        go = window.location.host !== "amazon-fr-fr-eur.clarityclient.com";
                        break;
                    }
                    case "INR": {
                        go = window.location.host !== "amazon-hi-in-inr.clarityclient.com";
                        break;
                    }
                }
                if (go) {
                    // Do Nothing as the event is handling this for now,
                    // have to wait for the lang part to load
                    return;
                }
                */
                this.$uibModalInstance.close(true);
            });
        }
        cancel(): void {
            this.$uibModalInstance.dismiss(false);
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvCurrencyService: services.ICurrencyService) {
            super(cefConfig);
            this.load();
        }
    }
}
