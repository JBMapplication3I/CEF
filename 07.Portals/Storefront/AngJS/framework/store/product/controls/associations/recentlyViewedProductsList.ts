module cef.store.product.controls.associations {
    class RecentlyViewedProductsListController extends core.TemplatedControllerBase {
        // Properties
        size: number; // Bound by Scope
        products: Array<api.ProductModel> = []; // Populated by Load function
        get usersSelectedStore(): api.StoreModel {
            // Read the service memory directly instead of making a copy
            return this.cvStoreLocationService.getUsersSelectedStore();
        }
        // Functions
        load(): void {
            // TODO: Implement Paging controls
            this.cvApi.tracking.GetRecentlyViewedProducts({ Paging: { Size: this.size || 4, StartIndex: 1 } }).then(r => {
                if (!r || !r.data || !r.data.length) { return; }
                this.products = r.data;
            });
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvStoreLocationService: services.IStoreLocationService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("recentlyViewedProductsList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { size: "=", template: "@?" },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/associations/recentlyViewedProductsList.html", "ui"),
        controller: RecentlyViewedProductsListController,
        controllerAs: "recentlyViewedProductsListCtrl",
        bindToController: true
    }));
}
