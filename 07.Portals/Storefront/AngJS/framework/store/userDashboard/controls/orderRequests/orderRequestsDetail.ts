module cef.store.userDashboard.controls.orderRequests {
    class OrderRequestDetailController extends core.TemplatedControllerBase {
        // Properties
        get currentCart(): api.CartModel {
            return this.cvCartService.accessCart(this.cartName);
        }
        get cartItems(): api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[] {
            return this.cvCartService.accessCart(this.cartName) &&
                this.cvCartService.accessCart(this.cartName).SalesItems
                || [];
        }
        cartPromise: ng.IPromise<api.CEFActionResponseT<api.CartModel>>;
        productList: api.ProductModel[];
        relatedProducts = [];
        cartName: string;
        newCartName: string;
        usersSelectedStore: api.StoreModel;
        cartType: api.CartTypeModel;
        // Functions
        load(): ng.IPromise<api.CEFActionResponseT<api.CartModel>> {
            this.setRunning();
            return this.$q((resolve, reject) => {
                this.cvAuthenticationService.preAuth().finally(() => {
                    if (!this.cvAuthenticationService.isAuthenticated()) {
                        reject();
                        return;
                    }
                    this.cvApi.shopping.GetCartTypeForCurrentUser({
                        TypeName: this.cartName
                    }).then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded) {
                            reject();
                            this.finishRunning(true, r as any);
                            return;
                        }
                        this.cartType = r.data.Result;
                        resolve(this.cartPromise = this.cvCartService.loadCart(this.cartName, true, "orderRequestDetail.load"));
                    }).catch(reason => this.finishRunning(true, reason));
                });
            });
        }
        updateName(): void {
            if (!this.newCartName) {
                return;
            }
            this.cartType.Name = this.newCartName;
            this.cvApi.shopping.UpdateCartType(this.cartType).finally(() => {
                this.$state.go("userDashboard.orderRequests.detail", { Name: this.cartType.Name });
            });
        }
        cancelEdit(): void {
            this.newCartName = this.cartType.Name;
            this.toggleEdit();
        }
        toggleEdit(): void {
            this.viewState["edit"] = !this.viewState["edit"];
        }
        getUserStore(): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.cvStoreLocationService.getUserSelectedStore().then(r => {
                    this.usersSelectedStore = r;
                    resolve();
                }).catch(reject);
            });
        }
        addListItem(id: number, quantity: number = 1): ng.IPromise<any> {
            return this.$q(resolve => {
                this.cvCartService.addCartItem(id, this.cartName, quantity).then(() => {
                    this.cvCartService.viewstate.checkoutIsProcessing = true;
                    this.load();
                    resolve();
                }).catch(resolve);
            });
        }
        removeCartItem(id: number, discount: string): ng.IPromise<any> {
            const promise = this.cvCartService.removeCartItem(
                id,
                _.find(this.cartItems, x => x.ID === id).ProductID,
                this.cartType.Name);
            promise.then(() => this.load());
            return promise;
        }
        clearList(): ng.IPromise<any> {
            return this.cvCartService.clearCart(this.currentCart.TypeName);
        }
        deleteList(): void {
            (this.currentCart.ID
                ? this.cvApi.shopping.DeleteCartByID(this.currentCart.ID)
                : this.$q.resolve())
            .finally(() => (this.cartType.ID
                            ? this.cvApi.shopping.DeleteCartTypeByID(this.cartType.ID)
                            : this.$q.resolve())
                .finally(() => this.$state.go("userDashboard.orderRequests.list")));
        }
        addAllToCart(): ng.IPromise<any> {
            return this.$q((resolve, reject) => {
                if (!this.currentCart || !this.currentCart.SalesItems.length) {
                    reject("Shopping list is null or empty");
                }
                const cloneSalesItems = _.cloneDeep(this.currentCart.SalesItems);
                cloneSalesItems.forEach(x => {
                    x.ID = null;
                    x.MasterID = null;
                });
                const addCartItemsDto = <api.AddCartItemsDto>{
                    TypeName: "Cart",
                    Items: cloneSalesItems,
                };
                this.cvCartService.viewstate.checkoutIsProcessing = true;
                this.cvApi.shopping.ClearCurrentCart().then(() =>
                    this.cvApi.shopping.AddCartItems(addCartItemsDto)
                        .then(() => {
                            if (this.currentCart.ShippingContactID) {
                                let obj: api.SerializableAttributesDictionary = {
                                    "orderRequestShippingcontactID": {
                                        ID: 1,
                                        Key: "orderRequestShippingcontactID",
                                        Value: this.currentCart.ShippingContactID.toString()
                                    },
                                }
                                const cart = this.cvCartService.accessCart("Cart");
                                cart.SerializableAttributes = Object.assign(obj);
                                this.cvCartService.updateCartAttributes("Cart");
                            }
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemAdded, "Cart");
                            resolve();
                        // Catch for Add Cart Items
                        }).catch(reason => {
                            this.cvMessageModalFactory(reason.data.ResponseStatus.Message);
                            reject(reason.data.ResponseStatus.Message);
                        })
                    // Catch for Clear Current Cart
                    ).catch(reason => {
                        this.cvMessageModalFactory(reason.data.ResponseStatus.Message);
                        reject(reason.data.ResponseStatus.Message);
                    })
                    // Ultimately...
                    .finally(() => {
                        this.cvCartService.viewstate.checkoutIsProcessing = false;
                        let translationMessage = "All items have been added to your cart.";
                        this.$translate("ui.storefront.cart.allItemsHaveBeenAddedToCart").then(r => {
                            translationMessage = r;
                        }).finally(() => this.cvMessageModalFactory(translationMessage));
                    });
            });
        }
        // Events
        // NOTE: This must remain an arrow function to resolve 'this' properly
        onCartLoaded_GetRelatedProducts = ($event: ng.IAngularEvent, cartType: string): void => {
            this.consoleDebug("orderRequestDetail.onCartLoaded");
            this.consoleDebug($event);
            this.consoleDebug(cartType);
            if (cartType !== this.cartName) { return; }
            this.getUserStore().finally(() => {
                const cart = this.cvCartService.accessCart(this.cartName);
                if (!cart) { return; }
                this.relatedProducts = _.flatMap(this.cartItems,
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
                    });
                // Doing this inside Cart Service now
                ////// Load inventory data for items
                ////if (this.cartItems != null) {
                ////    this.$q.all(this.cartItems.map(item => {
                ////        // Assign a new dynamic property with per-location inventory quantities.
                ////        return this.$q.resolve(
                ////            this.cvInventoryService.getInventoryObject([item], this.usersSelectedStore)
                ////                .then(result => item = result[0] as any)
                ////        );
                ////    })).then(() => this.finishRunning());
                ////}
            });
        }
        updateNameEnter(e): void {
            if (e.key === "Enter") {
                e.preventDefault();
                e.stopPropagation();
                this.updateName();
            }
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,

                readonly $scope: ng.IScope,
                private readonly $q: ng.IQService,
                private readonly $state: ng.ui.IStateService,
                readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvCartService: services.ICartService,
                private readonly cvStoreLocationService: services.IStoreLocationService,
                private readonly cvMessageModalFactory: cef.store.modals.IMessageModalFactory,
                protected readonly $translate: ng.translate.ITranslateService) {
            super(cefConfig);
            this.cartName = $stateParams["Name"] as string;
            if (!this.cartName || this.cartName === "") {
                throw "No Identifier";
            }
            const unbind1 = $scope.$on(cvServiceStrings.events.stores.selectionUpdate,
                () => {
                    this.usersSelectedStore = null;
                    this.getUserStore();
                });
            const unbind2 = $scope.$on(cvServiceStrings.events.stores.cleared,
                () => this.usersSelectedStore = null);
            const unbind3 = $scope.$on(cvServiceStrings.events.carts.cleared,
                () => this.load());
            const unbind4 = $scope.$on(cvServiceStrings.events.carts.loaded,
                this.onCartLoaded_GetRelatedProducts);
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
                if (angular.isFunction(unbind3)) { unbind3(); }
                if (angular.isFunction(unbind4)) { unbind4(); }
            });
            this.load();
        }
    }

    cefApp.directive("cefOrderRequestDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/orderRequests/orderRequestsDetail.html", "ui"),
        controller: OrderRequestDetailController,
        controllerAs: "ordCtrl"
    }));
}
