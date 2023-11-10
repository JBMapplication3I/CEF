module cef.store.product.controls.stores {
    class ProductDetailsStoreController extends core.TemplatedControllerBase {
        // Properties
        format: string; // Populated by Scope
        storeId: number; // Populated by Scope
        store: api.StoreModel; // Populated by API
        // Functions
        load(): void {
            if (!this.storeId) {
                // Do Nothing
                return;
            }
            this.setRunning();
            this.cvApi.stores.GetStoreByID(this.storeId).then(r => {
                if (!r || !r.data) {
                    this.finishRunning(true, "Unable to locate store by ID");
                    return;
                }
                this.store = r.data;
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefProductDetailsStore", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { storeId: "=", format: "@" },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/stores/store.html", "ui"),
        controller: ProductDetailsStoreController,
        controllerAs: "productDetailsStoreCtrl",
        bindToController: true
    }));
}
