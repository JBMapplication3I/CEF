module cef.store.userDashboard.controls.shoppingLists {
    export class ShoppingListModalController extends core.TemplatedControllerBase {
        // Properties
        shoppingList: string = null;
        listItem = null;
        cartTypes: api.CartTypeModel[] = [];
        createdMessage: string = null;
        createListDisplay: boolean;
        addListDisplay: boolean;
        // Functions
        getCartTypes(): ng.IPromise<any> {
            return this.cvApi.shopping.GetCurrentUserCartTypes({ IncludeNotCreated: false })
                .then(r => this.cartTypes = r.data.Results);
        }
        createCart(displayName: string): angular.IHttpPromise<api.CartTypeModel> {
            return this.cvApi.shopping.CreateCartTypeForCurrentUser({
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                DisplayName: displayName,
                BrandID: this.$rootScope["globalBrandID"]
            });
        }
        createShoppingList(newListDisplayName: string, closeAfter: boolean): void {
            this.setRunning();
            this.createdMessage = this.shoppingList;
            this.createCart(newListDisplayName).then(r => {
                this.shoppingList = null;
                this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updateShoppingLists);
                this.getCartTypes().then(() => {
                    this.listItem = _.find(this.cartTypes, x => x.Name === r.data.Name);
                    if (this.productID) {
                        this.addToList({ Name: r.data.DisplayName, DisplayName: newListDisplayName }).then(() => {
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
            return this.cvCartService.addCartItem(this.productID, params.Name, 1, this.uomParams).then(() => {
                this.createdMessage = params.DisplayName;
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
            if (!this.shoppingList) { return; } // And only if the user actually entered something
            event.preventDefault();
            event.stopPropagation();
            this.createShoppingList(this.shoppingList, false);
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
                protected productName: string,
                protected uomParams: any) { // Used by UI
            super(cefConfig);
            if (this.type === "addToShoppingList") {
                this.createListDisplay = false;
                this.addListDisplay = true;
            }
            this.getCartTypes();
        }
    }
}
