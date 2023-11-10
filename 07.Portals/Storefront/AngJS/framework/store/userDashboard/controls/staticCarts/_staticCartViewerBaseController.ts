module cef.store.userDashboard.controls {
    export abstract class StaticCartViewerBaseController extends core.TemplatedControllerBase {
        // Properties
        relatedProducts: Array<api.ProductModel|api.ProductAssociationModel> = [];
        abstract type(): string;
        // Functions
        removeCartItem(id: number, discount: string): ng.IPromise<any> {
            this.setRunning();
            return this.cvCartService.removeCartItem(
                id,
                _.find(this.cvCartService.accessCart(this.type()).SalesItems, x => x.ID === id).ProductID,
                this.type()); // The cart loaded event will be fired
        }
        clearList(): ng.IPromise<any> {
            this.setRunning();
            return this.cvCartService.clearCart(this.type()); // The cart loaded event will be fired
        }
        // Events
        // NOTE: This must remain an arrow function to resolve 'this' properly
        onCartLoaded_GetRelatedProducts = ($event: ng.IAngularEvent, cartType: string): void => {
            this.consoleDebug("StaticCartViewerBaseController.onCartLoaded_GetRelatedProducts");
            this.consoleDebug($event);
            this.consoleDebug(cartType);
            if (this.type() !== cartType) { return; }
            this.setRunning();
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    return;
                }
                const cart = this.cvCartService.accessCart(this.type());
                if (cart == null) { return; }
                this.relatedProducts = _.flatMap(cart.SalesItems,
                    (item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>) => {
                        if (!item ||
                            !item["Product"] ||
                            !item["Product"].ProductAssociations ||
                            !item["Product"].ProductAssociations.filter(x => x.Slave).length) {
                            return [];
                        }
                        return item["Product"].ProductAssociations
                            .filter(x => x.Slave)
                            .filter(x => x.TypeName === "Related Product" ||
                                x.Type && x.Type.Name === "Related Product");
                    }
                );
                this.finishRunning();
            });
        }
        // Constructor
        protected constructor(
                protected readonly $q: ng.IQService,
                protected readonly $window: ng.IWindowService,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvInventoryService: services.IInventoryService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.carts.loaded,
                this.onCartLoaded_GetRelatedProducts);
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }
}
