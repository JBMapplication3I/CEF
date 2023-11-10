/**
 * @file framework/admin/views/system/emailTemplates.list.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Email templates list class
 */
module cef.admin.views.system {
    class SystemEmailTemplatesSearchController extends core.TemplatedControllerBase {
        doProcessEmailBatch: () => void;
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvApi: api.ICEFAPI,
                readonly cvMessageModalFactory: modals.IMessageModalFactory) {
            super(cefConfig);
            this.doProcessEmailBatch = $rootScope["doProcessEmailBatch"] = () => {
                this.setRunning();
                cvApi.tasks.RunEmailBatchManually()
                    .then(r => r.data
                            ? cvMessageModalFactory($translate("ui.admin.controls.system.siteMaintenance.EmailBatchProcessed.Message")).then(() => this.finishRunning())
                            : cvMessageModalFactory($translate("ui.admin.controls.system.siteMaintenance.EmailBatchProcessed.Error")).then(() => this.finishRunning(true)),
                          result => this.finishRunning(true, result))
                    .catch(reason => this.finishRunning(true, reason));
            }
        }
    }

    adminApp.controller("SystemEmailTemplatesSearchController", SystemEmailTemplatesSearchController);
}
