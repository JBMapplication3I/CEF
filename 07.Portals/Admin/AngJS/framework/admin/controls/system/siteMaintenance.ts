module cef.admin {
    class SiteMaintenanceController extends core.TemplatedControllerBase {
        // Properties
        // <None>
        // Functions
        doGetProductSiteMapContent(): void {
            this.setRunning();
            this.cvApi.products.GetProductSiteMapContent()
                .then(r => this.$window.location.href = r.data.DownloadUrl)
                .catch(reason => this.finishRunning(true, reason));
        }
        doRegenerateProductSiteMap(): void {
            this.setRunning();
            this.cvApi.products.RegenerateProductSiteMap()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.productsGrid.UpdatedProductSiteMap.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.productsGrid.UpdatedProductSiteMap.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doIndexAuctions(): void {
            this.setRunning();
            this.cvApi.providers.IndexAuctions()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.auctionsGrid.UpdatedAuctionIndex.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.auctionsGrid.UpdatedAuctionIndex.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doIndexCategories(): void {
            this.setRunning();
            this.cvApi.providers.IndexCategories()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.categoriesGrid.UpdatedCategoryIndex.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.categoriesGrid.UpdatedCategoryIndex.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doIndexFranchises(): void {
            this.setRunning();
            this.cvApi.providers.IndexFranchises()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.auctionsGrid.UpdatedFranchiseIndex.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.auctionsGrid.UpdatedFranchiseIndex.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doIndexLots(): void {
            this.setRunning();
            this.cvApi.providers.IndexLots()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.lotsGrid.UpdatedLotIndex.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.lotsGrid.UpdatedLotIndex.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doIndexManufacturers(): void {
            this.setRunning();
            this.cvApi.providers.IndexManufacturers()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.manufacturersGrid.UpdatedManufacturerIndex.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.manufacturersGrid.UpdatedManufacturerIndex.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doIndexProducts(): void {
            this.setRunning();
            this.cvApi.providers.IndexProducts()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.productsGrid.UpdatedProductIndex.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.productsGrid.UpdatedProductIndex.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doIndexStores(): void {
            this.setRunning();
            this.cvApi.providers.IndexStores()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.storesGrid.UpdatedStoreIndex.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.storesGrid.UpdatedStoreIndex.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doIndexVendors(): void {
            this.setRunning();
            this.cvApi.providers.IndexVendors()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.vendorGrid.UpdatedVendorIndex.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.vendorGrid.UpdatedVendorIndex.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doClearPriceCache(): void {
            this.setRunning();
            this.cvApi.pricing.ClearPriceCache()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.system.siteMaintenance.ClearedPriceCache.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.system.siteMaintenance.ClearedPriceCache.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doGetCategorySiteMapContent(): void {
            this.setRunning();
            this.cvApi.categories.GetCategorySiteMapContent()
                .then(r => this.$window.location.href = r.data.DownloadUrl)
                .catch(reason => this.finishRunning(true, reason));
        }
        doRegenerateCategorySiteMap(): void {
            this.setRunning();
            this.cvApi.categories.RegenerateCategorySiteMap()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.categoryGrid.UpdatedCategorySiteMap.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.categoryGrid.UpdatedCategorySiteMap.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doProcessEmailBatch(): void {
            this.setRunning();
            this.cvApi.tasks.RunEmailBatchManually()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.system.siteMaintenance.EmailBatchProcessed.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.system.siteMaintenance.EmailBatchProcessed.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doClearHardSoftStopsCache(): void {
            this.setRunning();
            this.cvApi.shopping.ClearHardSoftStopsCaches()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.system.siteMaintenance.ClearedHardSoftStopsCache.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.system.siteMaintenance.ClearedHardSoftStopsCache.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        doClearJSConfigsCache(): void {
            this.setRunning();
            this.cvApi.jsConfigs.ClearJSConfigsCaches()
                .then(r => r.data
                    ? this.cvMessageModalFactory(this.$translate("ui.admin.controls.system.siteMaintenance.ClearedJSConfigsCache.Message")).then(() => this.finishRunning())
                    : this.cvMessageModalFactory(this.$translate("ui.admin.controls.system.siteMaintenance.ClearedJSConfigsCache.Error")).then(() => this.finishRunning(true)))
                .catch(reason => this.finishRunning(true, reason));
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $window: ng.IWindowService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory) {
            super(cefConfig);
        }
    }

    adminApp.directive("siteMaintenanceDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/siteMaintenance.html", "ui"),
        controller: SiteMaintenanceController,
        controllerAs: "siteMaintenanceCtrl"
    }));
}
