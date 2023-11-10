module cef.store.globalization {
    export class LanguageModalController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        languages: api.LanguageModel[];
        selectedLanguageObject: api.LanguageModel;
        selectedLanguage: string;
        // Functions
        private load(): void {
            this.cvLanguageService.getAvailableLanguages().then(r => {
                this.languages = r.data.Results;
                this.cvLanguageService.getCurrentLanguagePromise().then(language => {
                    this.selectedLanguageObject = language;
                    this.selectedLanguage = language.CustomKey;
                });
            });
        }
        ok(): void {
            this.cvLanguageService.changeToLanguagePromise(this.selectedLanguage)
                .then(() => this.$uibModalInstance.close(true));
            /*
            this.setRunning();
            this.cvLanguageService.changeToLanguagePromise(this.selectedLanguage).then(() => {
                // TODO@JTG: Make a core-first version of this
                let go: boolean = false;
                switch (this.selectedLanguage) {
                    case "en_US": {
                        go = window.location.host !== "demo-en.clarityclient.com";
                        break;
                    }
                    case "de_DE": {
                        go = window.location.host !== "demo-de.clarityclient.com";
                        break;
                    }
                    case "ja_JP": {
                        go = window.location.host !== "demo-jp.clarityclient.com";
                        break;
                    }
                }
                if (go) {
                    // Do Nothing as the event is handling this for now,
                    // have to wait for the lang part to load
                    return;
                }
                this.$uibModalInstance.close(true);
            });
            */
        }
        cancel(): void {
            this.$uibModalInstance.dismiss(false);
        }
        // Events
        // <None>
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $timeout: ng.ITimeoutService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvLanguageService: services.ILanguageService) {
            super(cefConfig);
            this.load();
            /*
            const unbind1 = $scope.$on(cvServiceStrings.events.lang.changeFinished, ($event: ng.IAngularEvent, langKey: string) => {
                // TODO@JTG: Make a core-first version of this
                let go: string = null;
                switch (langKey) {
                    case "en_US": {
                        if (window.location.host !== "demo-en.clarityclient.com") {
                            go = window.location.href.replace(
                                /demo-(?:de|jp)\.clarityclient\.com/,
                                "demo-en.clarityclient.com");
                        }
                        break;
                    }
                    case "de_DE": {
                        if (window.location.host !== "demo-de.clarityclient.com") {
                            go = window.location.href.replace(
                                /demo-(?:en|jp)\.clarityclient\.com/,
                                "demo-de.clarityclient.com");
                        }
                        break;
                    }
                    case "ja_JP": {
                        if (window.location.host !== "demo-jp.clarityclient.com") {
                            go = window.location.href.replace(
                                /demo-(?:en|de)\.clarityclient\.com/,
                                "demo-jp.clarityclient.com");
                        }
                        break;
                    }
                }
                if (go) {
                    if (!this.$translate.isReady()) {
                        // Translation service hasn't fully loaded, wait for it and then pull
                        this.$translate.onReady()
                            .then(() => this.$timeout(() => window.location.href = go, 250));
                    }
                    this.$timeout(() => window.location.href = go, 250);
                    return;
                }
                this.$uibModalInstance.close(true);
            });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
            */
        }
    }
}
