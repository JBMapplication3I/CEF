/**
 * @file framework/admin/_services/cvLanguageService.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Language Service, provides calls for managing the user's selected language
 * as well as switching languages
 */
module cef.admin.services {
    export interface ILanguageService {
        getCurrentLanguagePromise: (refresh?: boolean) => ng.IPromise<api.LanguageModel>;
        changeToLanguagePromise: (key: string) => ng.IPromise<boolean>;
        getAvailableLanguages: () => ng.IHttpPromise<api.LanguagePagedResults>;
        getLanguageByKey: (key: string) => ng.IHttpPromise<api.LanguageModel>;
    }

    export class LanguageService implements ILanguageService {
        // Properties
        private currentLanguage: api.LanguageModel;
        private getPromiseInstance: { [key: string]: ng.IPromise<api.LanguageModel> } = {};
        // Functions
        getCurrentLanguagePromise(force?: boolean): ng.IPromise<api.LanguageModel> {
            if (!this.$translate.isReady()) {
                // Translation service hasn't fully loaded, wait for it and then pull
                return this.$translate.onReady().then(() => this.getCurrentLanguagePromise(force));
            }
            const missing = !this.currentLanguage || !this.currentLanguage.CreatedDate;
            const key = missing
                ? this.$translate.use() || this.cefConfig.featureSet.languages.default
                : this.currentLanguage.CustomKey;
            /* If we are not forcing a refresh of the information and we already have the
             * information, then just return what we already have instead of making another call */
            if (!force && !missing) {
                return this.$q.resolve(this.currentLanguage);
            }
            /* If we don't have the data yet, but already have a promise out, then just return that
             * promise instead of making another call */
            if (!force && missing && this.getPromiseInstance[key]) {
                return this.getPromiseInstance[key];
            }
            /* Otherwise we don't have the data or a promise out so we need to make one */
            return this.getPromiseInstance[key] = this.$q((resolve, reject) => {
                this.getLanguageByKey(key).then(r => {
                    this.setCurrentLanguageByModel(r.data);
                    resolve(this.currentLanguage);
                }, reject)
                .catch(reject)
                .finally(() => delete this.getPromiseInstance[key]);
            });
        }

        private clearCurrentLanguage(): void {
            this.currentLanguage = <api.LanguageModel>{};
            this.$translate.use(this.cefConfig.featureSet.languages.default)
                .finally(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.lang.changeFinished,
                    this.currentLanguage.CustomKey));
        }
        private setCurrentLanguage(languageID: string | number, language?: api.LanguageModel): void {
            if (language) {
                this.currentLanguage = language;
            }
            this.$translate.use(languageID as string)
                .finally(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.lang.changeFinished,
                    this.currentLanguage.CustomKey));
        }
        private setCurrentLanguageByModel(language: api.LanguageModel): void {
            if (language && language["data"]) {
                throw Error("bad language");
            }
            if (!language ||
                !language.CreatedDate && this.currentLanguage && this.currentLanguage.CreatedDate ||
                this.currentLanguage && this.currentLanguage.CreatedDate && language.CustomKey === this.currentLanguage.CustomKey) {
                return;
            }
            this.currentLanguage = language;
            this.$translate.use(language.CustomKey)
                .finally(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.lang.changeFinished,
                    this.currentLanguage.CustomKey));
        }
        changeToLanguagePromise(key: string): ng.IPromise<boolean> {
            return this.$q<boolean>((resolve, reject) => {
                if (!key) {
                    reject(false); // Not allowd to clear language
                    return;
                }
                if (this.currentLanguage.CustomKey === key) {
                    resolve(true); // Already on this language
                    return;
                }
                this.getLanguageByKey(key).then(r => {
                    if (!r || !r.data) {
                        reject(false); // Language not found
                        return;
                    }
                    // Applying found language to this service
                    this.setCurrentLanguageByModel(r.data);
                    // Telling translations to load the alternate language
                    this.$translateAsyncPartialLoaderAdmin.addPart(this.currentLanguage.Locale, 1, "lang={lang}&part={part}");
                    //this.$translate.refresh(this.currentLanguage.Locale);
                    resolve(true);
                });
            });
        }
        getAvailableLanguages(): ng.IHttpPromise<api.LanguagePagedResults> {
            return this.cvApi.globalization.GetLanguages({ Active: true, AsListing: true });
        }
        getLanguageByKey(key: string): ng.IHttpPromise<api.LanguageModel> {
            if (!key) { return null; }
            return this.cvApi.globalization.GetLanguageByKey(key.replace(/%22/, ""));
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $translateAsyncPartialLoaderAdmin: ng.translate.ITranslatePartialLoaderService,
                private readonly cvApi: api.ICEFAPI,
                private readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: IServiceStrings) {
            const construct = () => {
                this.currentLanguage = <api.LanguageModel>{
                    ID: 0,
                    Active: true,
                    CreatedDate: null,
                    CustomKey: $translate.use(),
                    Name: null,
                };
                this.getCurrentLanguagePromise(true);
            };
            if (this.$translate.isReady()) {
                construct();
                return;
            }
            this.$translate.onReady().then(() => construct());
        }
    }
}
