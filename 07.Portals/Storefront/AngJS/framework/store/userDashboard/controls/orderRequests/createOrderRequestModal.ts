module cef.store.userDashboard.controls.orderRequests {
    export class CreateOrderRequestModalController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        newName: string = null;
        createdMessage: string = null;
        cartTypes: api.CartTypeModel[] = [];
        // Functions
        private getCartTypes(): void {
            this.cvApi.shopping.GetCurrentUserCartTypes({ IncludeNotCreated: false, FilterCartsByOrderRequest: true })
                .then(r => this.cartTypes = r.data.Results);
        }
        private createCart(name: string): ng.IPromise<any> {
            return this.cvApi.shopping.CreateCartTypeForCurrentUser({
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                Name: name,
                CustomKey: name,
                BrandID: this.$rootScope["globalBrandID"]
            });
        }
        ok(): void {
            this.setRunning();
            if (this.cartTypes.some(item => item.Name === this.newName)) {
                this.cvMessageModalFactory(
                    this.$translate("ui.storefront.userDashboard2.controls.orderRequests.OrderRequestAlreadyExists"));
                this.finishRunning(true);
                return;
            }
            this.createdMessage = this.newName;
            this.createCart(this.newName).then(() => {
                this.newName = null;
                this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updateOrderRequests);
                this.close();
            });
        }
        close(): void {
            this.$uibModalInstance.close(true);
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Events
        inputKeyPress(event: JQueryKeyEventObject): void { // Angular returns this type of object per their docs
            if (event.key !== "Enter") { return; } // Only do anything if it was the enter key
            if (!this.newName) { return; } // And only if the user actually entered something
            event.preventDefault();
            event.stopPropagation();
            this.ok();
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvMessageModalFactory: store.modals.IMessageModalFactory) {
            super(cefConfig);
            this.getCartTypes();
        }
    }
}
