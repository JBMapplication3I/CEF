module cef.admin.views.system {
    class SystemUITranslationsSearchController extends core.TemplatedControllerBase {
        doClearUITranslationsCache: () => void;
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.doClearUITranslationsCache = $rootScope["doClearUITranslationsCache"] = () => {
                cvApi.globalization.ClearUiTranslationCache()
                    .then(() => { /* Do Nothing */ })
                    .catch(e => console.log(e));
            }
        }
    }

    adminApp.controller("SystemUITranslationsSearchController", SystemUITranslationsSearchController);
}
