module cef.store.locations {
    class CheckInventoryModalController extends core.TemplatedControllerBase {
        // Properties
        store: api.StoreModel;
        storeList: api.StoreModel[]
        viewstate: { checkStore: boolean; } = { checkStore: false };
        quantity: number = 1;
        // Functions
        buy(id: number, cartType: string, quantity: number, product: api.ProductModel): void {
            this.cvCartService.addCartItem(
                    id,
                    cartType,
                    quantity,
                    <services.IAddCartItemParams>null,
                    product)
                .then(() => this.$uibModalInstance.close());
        }
        checkStore(): void {
            this.viewstate.checkStore = !this.viewstate.checkStore;
        }
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $q: ng.IQService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvCartService: services.ICartService,
                readonly cvStoreLocationService: services.IStoreLocationService,
                readonly cvInventoryService: services.IInventoryService,
                private product: api.ProductModel) {
            super(cefConfig);
            this.$q.all([
                cvStoreLocationService.getUserSelectedStore(),
                cvStoreLocationService.getUserNearbyStores()
            ]).then(rarr => {
                this.store = rarr[0] as api.StoreModel;
                this.storeList = rarr[1] as api.StoreModel[];
                if (this.store && !this.store.Distance && this.storeList) {
                    this.store.Distance = _.find(this.storeList, x => x.ID === this.store.ID).Distance;
                }
            });
            this.product = cvInventoryService.factoryAssign(this.product);
            const unbind1 = this.$scope.$on(cvServiceStrings.events.modals.close, () => {
                this.viewstate.checkStore = false;
                this.$uibModalInstance.close();
            });
            this.$scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    class CheckInventoryController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        text: string;
        product: api.ProductModel;
        // Properties
        storeCheck: api.StoreModel;
        // Functions
        open(): void {
            if (!angular.isObject(this.storeCheck)) {
                this.$uibModal.open({
                    templateUrl: this.$filter("corsLink")("/framework/store/location/select-store-modal.html", "ui"),
                    size: this.cvServiceStrings.modalSizes.lg
                });
                return;
            }
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/store/location/checkInventory-modal.html", "ui"),
                size: this.cvServiceStrings.modalSizes.lg,
                controller: CheckInventoryModalController,
                controllerAs: "checkInventoryModalCtrl",
                resolve: { product: () => this.product }
            });
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                readonly cvStoreLocationService: services.IStoreLocationService) {
            super(cefConfig);
            cvStoreLocationService.getUserSelectedStore().then(store => this.storeCheck = store);
        }
    }

    cefApp.directive("cefCheckInventory", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            text: "@",
            product: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/location/checkInventory-button.html", "ui"),
        controller: CheckInventoryController,
        controllerAs: "checkInventoryCtrl",
        bindToController: true
    }));
}
