module cef.store.product.controls.actions {
    class ActionsGroupController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        product: api.ProductModel;
        storeProduct: api.StoreProductModel;
        // Properties
        store: api.StoreModel;
        quoteSuccess: boolean;
        get isSupervisor(): boolean { return this.cvSecurityService.hasRole("Supervisor") };
        // Functions
        load(): void {
            if (!this.storeProduct) { return; }
            this.cvApi.stores.GetStoreByID(this.storeProduct.MasterID).then(r => {
                if (!r || !r.data) {
                    console.error("Unable to locate store by ID");
                    return;
                }
                this.store = r.data;
            }).catch(reason => console.error(reason));
        }
        addWatcher(): void {
            const unbind1 = this.$scope.$on(this.cvServiceStrings.events.carts.itemAdded, (item, type, dto) => {
                if (type != this.cvServiceStrings.carts.types.quote) {
                    return;
                }
                this.quoteSuccess = true;
            });
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvSecurityService: services.ISecurityService) {
            super(cefConfig);
            this.load();
            this.addWatcher();
        }
    }

    cefApp.directive("cefProductActionsGroup", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=", // The Product to pass through
            storeProduct: "=?" // The Store Product to pass through
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/actions/actionsGroup.html", "ui"),
        controller: ActionsGroupController,
        controllerAs: "pagCtrl",
        bindToController: true
    }));
}
