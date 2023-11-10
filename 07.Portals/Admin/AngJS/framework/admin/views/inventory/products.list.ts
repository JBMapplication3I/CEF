/**
 * @file framework/admin/views/inventory/products.list.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Products.list class
 */
module cef.admin.views.inventory {
    class InventoryProductSearchController extends core.TemplatedControllerBase {
        doExportToExcel: () => void;
        doGetProductSiteMapContent: () => void;
        doRegenerateProductSiteMap: () => void;
        doIndexProducts: () => void;
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                readonly $window: ng.IWindowService,
                readonly $filter: ng.IFilterService,
                readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvApi: api.ICEFAPI,
                readonly cvMessageModalFactory: modals.IMessageModalFactory) {
            super(cefConfig);
            this.doExportToExcel = $rootScope["doExportToExcel"] = () => {
                this.setRunning();
                cvApi.products.GetProductsAsExcelDoc()
                    .then(r => $window.location.href = $filter("corsStoredFilesLink")(r.data.DownloadUrl, "products"))
                    .catch(reason => this.finishRunning(true, reason));
            }
            this.doGetProductSiteMapContent = $rootScope["doGetProductSiteMapContent"] = () => {
                this.setRunning();
                cvApi.products.GetProductSiteMapContent()
                    .then(r => $window.location.href = r.data.DownloadUrl)
                    .catch(reason => this.finishRunning(true, reason));
            }
            this.doRegenerateProductSiteMap = $rootScope["doRegenerateProductSiteMap"] = () => {
                this.setRunning();
                cvApi.products.RegenerateProductSiteMap()
                    .then(r => r.data
                        ? cvMessageModalFactory($translate("ui.admin.controls.inventory.productsGrid.UpdatedProductSiteMap.Message")).then(() => this.finishRunning())
                        : cvMessageModalFactory($translate("ui.admin.controls.inventory.productsGrid.UpdatedProductSiteMap.Error")).then(() => this.finishRunning(true)))
                    .catch(reason => this.finishRunning(true, reason));
            }
            this.doIndexProducts = $rootScope["doIndexProducts"] = () => {
                this.setRunning();
                cvApi.providers.IndexProducts()
                    .then(r => r.data
                        ? cvMessageModalFactory($translate("ui.admin.controls.inventory.productsGrid.UpdatedProductIndex.Message")).then(() => this.finishRunning())
                        : cvMessageModalFactory($translate("ui.admin.controls.inventory.productsGrid.UpdatedProductIndex.Error")).then(() => this.finishRunning(true)))
                    .catch(reason => this.finishRunning(true, reason));
            }
        }
    }

    adminApp.controller("InventoryProductSearchController", InventoryProductSearchController);
}
