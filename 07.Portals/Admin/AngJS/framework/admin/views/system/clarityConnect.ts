module cef.admin.views.system {
    class SystemClarityConnectController extends core.TemplatedControllerBase {
        constructor(protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    adminApp.controller("SystemClarityConnectController", SystemClarityConnectController);
}
