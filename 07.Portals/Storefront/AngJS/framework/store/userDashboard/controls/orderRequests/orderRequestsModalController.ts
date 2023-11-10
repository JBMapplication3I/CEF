module cef.store.userDashboard.controls.orderRequests {
    export class OrderRequestModalController extends core.TemplatedControllerBase {
        // Properties
        orderRequest: string = null;
        listItem = null;
        cartTypes: api.CartTypeModel[] = [];
        createdMessage: string = null;
        createListDisplay: boolean;
        addListDisplay: boolean;
        // Functions
        getCartTypes(): ng.IPromise<any> {
            return this.cvApi.shopping.GetCurrentUserCartTypes({ IncludeNotCreated: false, FilterCartsByOrderRequest: true })
                .then(r => this.cartTypes = r.data.Results);
        }
        createCart(name: string): ng.IPromise<any> {
            return this.cvApi.shopping.CreateCartTypeForCurrentUser({
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                Name: name,
                CustomKey: name,
                BrandID: this.$rootScope["globalBrandID"]
            });
        }
        createOrderRequest(newListName: string, closeAfter: boolean): void {
            this.setRunning();
            if (this.cartTypes.some(item => item.Name === newListName)) {
                this.cvMessageModalFactory(
                    this.$translate("ui.storefront.userDashboard2.controls.orderRequests.OrderRequestAlreadyExists"));
                this.finishRunning(true);
                return;
            }
            this.createdMessage = this.orderRequest;
            this.createCart(newListName).then(() => {
                this.orderRequest = null;
                this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updateOrderRequests);
                this.getCartTypes().then(() => {
                    this.listItem = _.find(this.cartTypes, x => x.Name === newListName);
                    if (this.productID) {
                        this.addToList({ Name: newListName }).then(() => {
                            if (closeAfter) {
                                this.$uibModalInstance.close();
                            }
                        });
                        return;
                    }
                    if (closeAfter) {
                        this.$uibModalInstance.close();
                    }
                    this.finishRunning();
                });
            });
        }
        isInList(params): boolean {
            if (!params) { return true; }
            return this.cvCartService.cartContainsItem(this.productID, params.Name);
        }
        addToList(params): ng.IPromise<any> {
            this.createdMessage = null;
            this.listItem = null;
            this.setRunning();
            return this.cvCartService.addCartItem(this.productID, params.Name, 1).then(() => {
                this.createdMessage = params.Name;
                this.finishRunning();
            });
        }
        createAddViewToggle(): void {
            this.createdMessage = null;
            this.createListDisplay = !this.createListDisplay;
            this.addListDisplay = !this.addListDisplay;
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
            if (!this.orderRequest) { return; } // And only if the user actually entered something
            event.preventDefault();
            event.stopPropagation();
            this.createOrderRequest(this.orderRequest, false);
        }
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvMessageModalFactory: store.modals.IMessageModalFactory,
                protected type: string,
                protected productID: number,
                protected productName: string) { // Used by UI
            super(cefConfig);
            if (this.type === "addToOrderRequest") {
                this.createListDisplay = false;
                this.addListDisplay = true;
            }
            this.getCartTypes();
        }
    }
}
