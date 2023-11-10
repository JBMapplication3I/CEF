module cef.store.product.controls.actions {
    class ProductActionShoppingListController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        product: api.ProductModel;
        // Properties
        cartTypes: api.CartTypeModel[] = [];
        handleUoms: boolean;
        productUomKey: string;
        productUomValue: string;
        protected index = "AddToShoppingList";
        protected key = "ui.storefront.product.detail.productDetails.addToShoppingList"
        // Functions
        isAuthed() { return this.cvAuthenticationService.isAuthenticated(); }
        // Events
        open(): void {
            let uomParams: any = {};
            if (this.productUomKey && this.productUomValue) {
                let obj: api.SerializableAttributesDictionary = {
                    "SelectedUOM": {
                        ID: 1,
                        Key: "SelectedUOM",
                        Value: this.productUomKey
                    },
                    "SoldPrice": {
                        ID: 1,
                        Key: "SoldPrice",
                        Value: this.productUomValue
                    },
                };
                uomParams.SerializableAttributes = Object.assign(obj);
                uomParams.ForceUniqueLineItemKey = this.product.CustomKey + this.productUomKey;
            }
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/controls/shoppingLists/shoppingLists.addTo.modal.html", "ui"),
                controller: userDashboard.controls.shoppingLists.ShoppingListModalController,
                controllerAs: "shoppingListAddToModalCtrl",
                resolve: {
                    type: () => "addToShoppingList",
                    productID: () => this.product && this.product.ID,
                    productName: () => this.product && this.product.Name,
                    uomParams: () => uomParams
                }
            }).result.then(() => {
                this.cvApi.shopping.GetCurrentUserCartTypes({ IncludeNotCreated: false })
                    .then(r => this.cartTypes = r.data.Results);
            });
        }
        requireLoginForShoppingLists(go = false): void {
            if (this.isAuthed()) { return; }
            this.cvAuthenticationService.preAuth().finally(() => this.requireLoginForShoppingListsInner(go));
        }
        requireLoginForShoppingListsInner(go = false): void {
            if (this.isAuthed()) { return; }
            this.cvLoginModalFactory(null, null, false, true).then(() => this.open());
        }
        protected click(add: boolean): void {
            if (this.isAuthed()) {
                this.open();
            } else {
                this.requireLoginForShoppingLists(true);
            }
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvLoginModalFactory: user.ILoginModalFactory,
                private readonly cvCartService: services.ICartService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefProductShoppingList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=", // The product to Check
            // UOM
            handleUoms: "=?",
            productUomKey: "=?",
            productUomValue: "=?"
        },
        replace: true, // Required for placement
        templateUrl: $filter("corsLink")("/framework/store/product/controls/actions/shoppingList.html", "ui"),
        controller: ProductActionShoppingListController,
        controllerAs: "pslCtrl",
        bindToController: true
    }));
}
