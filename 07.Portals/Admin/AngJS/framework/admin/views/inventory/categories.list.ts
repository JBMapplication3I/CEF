/**
 * @file framework/admin/views/inventory/categories.list.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Categories.list class
 */
module cef.admin.views.inventory {
    class InventoryCategorySearchController extends core.TemplatedControllerBase {
        doGetCategorySiteMapContent: () => void;
        doRegenerateCategorySiteMap: () => void;
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $window: ng.IWindowService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory) {
            super(cefConfig);
            this.doGetCategorySiteMapContent = $rootScope["doGetCategorySiteMapContent"] = () => {
                this.setRunning();
                cvApi.categories.GetCategorySiteMapContent()
                    .then(r => $window.location.href = r.data.DownloadUrl,
                          result => this.finishRunning(true, result))
                    .catch(reason => this.finishRunning(true, reason));
            }
            this.doRegenerateCategorySiteMap = $rootScope["doRegenerateCategorySiteMap"] = () => {
                this.setRunning();
                cvApi.categories.RegenerateCategorySiteMap()
                    .then(r => r.data
                            ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.categoryGrid.UpdatedCategorySiteMap.Message")).then(() => this.finishRunning())
                            : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.categoryGrid.UpdatedCategorySiteMap.Error")).then(() => this.finishRunning(true)),
                        result => this.finishRunning(true, result))
                    .catch(reason => this.finishRunning(true, reason));
            }
        }
    }

    adminApp.controller("InventoryCategorySearchController", InventoryCategorySearchController);
}
