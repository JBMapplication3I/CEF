/**
 * @file framework/store/cart/views/cart.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Cart class
 */
module cef.store.cart.views {
    type SalesItem = api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>;
    export class CartController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        type: string;
        noInitialLoad: boolean;
        includeQuickOrder: boolean;
        // Properties
        updatingCart = false;
        hasShipping = false;
        gettingRates = false;
        shippingStrikethrough = false;
        unbindCartLoadedWatch: Function;
        cartItemsPaging: core.Paging<SalesItem>;
        cartPromise: ng.IPromise<api.CEFActionResponseT<api.CartModel>>;
        orderDiscounts: api.AppliedCartDiscountModel[] = [];
        shippingDiscounts: api.AppliedCartDiscountModel[] = [];
        relatedProducts: api.ProductAssociationModel[] = [];
        allSelectedShipping: core.ShippingOptions;
        supervisorList: api.UserModel[] = [];
        supervisorSelectList: any[] = [];
        selectedSupervisorID: number;
        currentUserName: string;
        currentCartID: number;
        get cartItems(): SalesItem[] {
            return this.cvCartService.accessCart(this.type)
                && this.cvCartService.accessCart(this.type).SalesItems;
        }
        get isMoreThanNineItems(): boolean {
            if (!this.cartItems || !this.cartItems.length) {
                return false;
            }
            return this.cartItems.reduce((prev, cur) => {
                return cur.Quantity + prev;
            }, 0) > 9;
        }
        get isSupervisor(): boolean { return this.cvSecurityService.hasRole("Supervisor") };
        // Functions
        loadCollections(): void {
            this.consoleDebug("CartController.loadCollections()");
            this.setRunning();
            this.cvAuthenticationService.preAuth().finally(() => {
                /* TODO: In client implementations, if login is required to use the site, this will kick out
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    this.redirectUnauthorizedUser();
                    this.finishRunning();
                    return;
                }
                */
                // this.consoleDebug("CartController.loadCollections()");
                this.unbindCartLoadedWatch = this.$scope.$on(this.cvServiceStrings.events.carts.loaded,
                    this.onCartLoaded);
                if (!(Boolean(this.noInitialLoad) === true)) {
                    this.loadCurrentCart(false);
                }
                this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                    if (angular.isFunction(this.unbindCartLoadedWatch)) { this.unbindCartLoadedWatch(); }
                });
                this.finishRunning();
            });
        }
        loadCurrentCart(force: boolean): ng.IPromise<api.CEFActionResponseT<api.CartModel>> {
            // this.consoleDebug(`CartController.loadCurrentCart(force: ${force})`);
            this.setRunning();
            return this.cartPromise = this.cvCartService.loadCart(this.type, force, "CartController.loadCurrentCart");
        }
        loadSupervisors(): void {
            this.setRunning();
            this.cvAuthenticationService.preAuth().finally(() => {
                if (this.cvAuthenticationService.isAuthenticated() && !this.isSupervisor) {
                    this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                        this.currentUserName = user.username;
                        this.cvApi.contacts.GetSupervisorsForCurrentUser().then(r => {
                            if (!r || !r.data) {
                                this.finishRunning();
                                return;
                            }
                            this.supervisorList = r.data;
                            this.supervisorList.forEach((x, index) => {
                                this.supervisorSelectList.push({
                                    "ID": x.ID,
                                    "Name": x.ContactFirstName + " " + x.ContactLastName,
                                    "SortOrder": index
                                });
                            });
                            this.finishRunning();
                        }).catch(error => this.finishRunning(true, error))
                    });
                }
            });
        }
        submitCartToSupervisor(): void {
            this.setRunning();
            this.cvApi.shopping.TransferCartItemsToSupervisorShoppingCart({
                CartID: this.cvCartService.accessCart(this.type).ID,
                SupervisorUserID: this.selectedSupervisorID,
                CurrentUserUsername: this.currentUserName,
                ShippingAddressID: this.cvAddressBookService.defaultShippingID ? this.cvAddressBookService.defaultShippingID : null
            }).then(r => {
                if (!r || !r.data.ActionSucceeded) {
                    this.finishRunning();
                    return;
                }
                this.cvMessageModalFactory(this.$translate("ui.storefront.cart.cartTransferredSuccessfully")).then(confirmed => {
                    this.$filter("goToCORSLink")("/");
                });
            }).catch(error => this.finishRunning(true, error));;
        }
        // NOTE: This must remain an arrow function to resolve 'this' properly
        onCartLoaded = (__: ng.IAngularEvent, cartType: string): void => {
            // this.consoleDebug(`CartController.onCartLoaded(__, "${cartType}")`);
            if (cartType !== this.type) {
                return;
            }
            const currentCart = this.cvCartService.accessCart(this.type);
            if (!currentCart || angular.toJson(currentCart) === "{}"
                || angular.toJson(currentCart) === "{\"hasASelectedRateQuote\":false}") {
                this.setDiscounts(currentCart);
                this.setupCartItemsPaging();
                if (angular.isFunction(this.inheritedOnCartLoadedHook)) {
                    this.inheritedOnCartLoadedHook();
                }
                return;
            }
            this.cvStoreLocationService.getUserSelectedStore().then(store => store, () => null).then(store => {
                this.setDiscounts(currentCart);
                if (!currentCart.SalesItems || currentCart.SalesItems.length <= 0) {
                    this.setupCartItemsPaging();
                    this.loadCurrentCartInner(currentCart, this.inheritedOnCartLoadedHook, store); // Will call finishRunning
                    return;
                }
                currentCart.SalesItems.forEach(x => {
                    if (!x.Discounts || !x.Discounts.length) {
                        return;
                    }
                    x["DiscountTotal"] = _.sum(x.Discounts.map(y => y.DiscountTotal));
                });
                const productModels = currentCart.SalesItems.map(x => {
                    if (x["Product"]) {
                        x["Product"]["SalesItemID"] = x.ID;
                        return x["Product"];
                    }
                    return <api.HasInventoryObject>{
                        ID: x.ProductID,
                        CustomKey: x.ProductKey,
                        Name: x.ProductName,
                        SeoUrl: x.ProductSeoUrl,
                        SalesItemID: x.ID
                    };
                });
                this.cvInventoryService.bulkFactoryAssign(productModels).then(result => {
                    if (!currentCart || !currentCart.SalesItems) {
                        this.cvCartService.loadCart(this.type, true, "cart.onCartLoadedafterInventory");
                        return;
                    }
                    currentCart.SalesItems.forEach(x => {
                        x["Product"] = _.find(result, y => y["SalesItemID"] === x.ID) as any;
                        this.getStorePriceRules(x["Product"]);
                    });
                    this.setupCartItemsPaging();
                    this.loadCurrentCartInner(currentCart, this.inheritedOnCartLoadedHook, store); // Will call finishRunning
                });
            });
        }
        protected inheritedOnCartLoadedHook = (): void => {
            // this.consoleDebug("CartController.inheritedOnCartLoadedHook()");
        }
        private setupCartItemsPaging(): void {
            // this.consoleDebug("CartController.setupCartItemsPaging()");
            this.cartItemsPaging = new core.Paging<SalesItem>(this.$filter);
            this.cartItemsPaging.pageSize = 8;
            this.cartItemsPaging.pageSetSize = 3;
            this.cartItemsPaging.data = this.cvCartService.accessCart(this.type)
                && this.cvCartService.accessCart(this.type).SalesItems
                || [];
        }
        private setDiscounts(cart: api.CartModel): void {
            // this.consoleDebug("CartController.setDiscounts(cart)");
            this.orderDiscounts = [];
            this.shippingDiscounts = [];
            if (!cart || angular.toJson(cart) == "{}") {
                return;
            }
            if (cart.Discounts) {
                cart.Discounts.forEach(x => {
                    switch (x.DiscountTypeID) {
                        case 0: {
                            this.orderDiscounts.push(x);
                            break;
                        }
                        case 2: {
                            this.shippingDiscounts.push(x);
                            break;
                        }
                    }
                });
            }
            this.shippingStrikethrough = _.sumBy(this.shippingDiscounts, d => d.DiscountTotal)
                + (cart.Totals && cart.Totals.Shipping || 0) === 0;
        }
        loadCurrentCartInner(
                cart: api.CartModel,
                callback: (...args) => void|any,
                store: api.StoreModel)
                : ng.IPromise<void> {
            // this.consoleDebug(`CartController.loadCurrentCartInner(cart, callback: ${callback ? "fn" : "null"}, store: ${store})`);
            return this.$q((resolve, reject) => {
                if (!cart.SalesItems || !cart.SalesItems.length) {
                    if (!this.viewState["analyzing"]) {
                        this.finishRunning();
                    }
                    resolve();
                    return;
                }
                this.$q.all(cart.SalesItems.map(salesItem => {
                    // Check for associated products data, which will need their own copy of this same action
                    if (!salesItem
                        || !salesItem["Product"]
                        || !(salesItem["Product"] as api.ProductModel).ProductAssociations
                        || !(salesItem["Product"] as api.ProductModel).ProductAssociations.filter(x => x.Slave).length) {
                        return this.$q.resolve();
                    }
                    return this.$q((resolve2, __) => {
                        this.cvInventoryService.bulkFactoryAssign(
                            salesItem["Product"].ProductAssociations
                                .filter(x => x.Slave)
                                .map(x => x.Slave))
                        .then((resultInner: api.ProductModel[]) => {
                            salesItem["Product"].ProductAssociations
                                .filter(x => x.Slave)
                                .forEach(x => x.Slave = (resultInner as any)
                                    .find(y => y.ID === x.SlaveID));
                                resolve2();
                        }).catch(resolve2); // Resolve even if this extra data fails
                    });
                })).then(() => {
                    this.relatedProducts = _.flatMap(cart.SalesItems,
                        item => {
                            if (!item
                                || !item["Product"]
                                || !(item["Product"] as api.ProductModel).ProductAssociations
                                || !(item["Product"] as api.ProductModel).ProductAssociations.filter(x => x.Slave).length) {
                                return [];
                            }
                            return (item["Product"] as api.ProductModel).ProductAssociations
                                .filter(x => x.Slave)
                                .filter(x => x.TypeName === "Related Product"
                                    || x.Type && x.Type.Name === "Related Product");
                        });
                    this.$q.all(cart.SalesItems.map(salesItem => {
                        return this.$q((resolve3, reject3) => {
                            if (!salesItem.SerializableAttributes
                                || !salesItem.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID]
                                || !salesItem.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID].Value
                                || Number(salesItem.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID]) <= 0) {
                                return resolve3();
                            }
                            this.cvStoreLocationService.getStoreByID(
                                    Number(salesItem.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID].Value))
                                .then(store => {
                                    angular.extend(salesItem, { usersSelectedStore: store });
                                    resolve3();
                                }).catch(reject3);
                        });
                    })).then(resolve)
                    .catch(reason2 => { this.finishRunning(true, reason2); reject(reason2); });
                }).catch(reason => { this.finishRunning(true, reason); reject(reason); });
            }).then(() => {
                if (angular.isFunction(callback)) {
                    callback();
                }
                // This causes singles checkout to fire stuff before we want it to
                // this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.ready,
                //     cart,
                //     false); // Don't reapply shipping address, we only just loaded it
                if (!this.viewState["analyzing"]) { this.finishRunning(); }
            });
        }
        proceedToCheckout(): void {
            switch (this.type) {
                case this.cvServiceStrings.carts.types.quote: {
                    this.$filter("goToCORSLink")("", "submitQuote");
                    break;
                }
                case this.cvServiceStrings.carts.types.cart:
                default: {
                    this.$filter("goToCORSLink")("", "checkout");
                    break;
                }
            }
        }
        removeCartItem(id: number): ng.IPromise<any> {
            return this.cvCartService.removeCartItem(
                id,
                _.find(this.cvCartService.accessCart(this.type).SalesItems, x => x.ID === id).ProductID,
                this.type).then(() => {
                    if (this.cartItems.length === 0) {
                        const cart = this.cvCartService.accessCart(this.type);
                        cart.SerializableAttributes = { };
                        this.cvCartService.updateCartAttributes("Cart");
                    }
                });
        }
        removeCartDiscount(id: number): ng.IPromise<any> {
            return this.cvCartService.removeCartDiscount(id, this.type);
        }
        removeCartItemDiscount(id: number): ng.IPromise<any> {
            return this.cvCartService.removeCartItemDiscount(id, this.type);
        }
        kitComponents(item: api.ProductModel): api.ProductAssociationModel[] {
            if (!item || !item.ProductAssociations || !item.ProductAssociations.length) {
                return null;
            }
            return item.ProductAssociations.filter(x => x.TypeKey === 'KIT-COMPONENT');
        }
        moveCartItem(
            cartItem: SalesItem,
            fromCartType: string,
            toCartType: string)
            : ng.IPromise<any> {
            return this.$q((resolve, reject) => {
                if (!(cartItem && fromCartType && toCartType)) {
                    reject("Incomplete arguments");
                    return;
                }
                const promise: ng.IPromise<any[]> = this.$q.all([
                    this.cvCartService.removeCartItem(cartItem.ID, cartItem.ProductID, fromCartType),
                    this.cvCartService.addCartItem(
                        cartItem.ProductID,
                        toCartType,
                        cartItem.Quantity + (cartItem.QuantityBackOrdered || 0) + (cartItem.QuantityPreSold || 0))
                ]);
                promise.finally(() => {
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, fromCartType);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, toCartType);
                });
                resolve(promise);
            });
        }
        // NOTE: This must remain an arrow function
        updateCartItemQuantity = (__: ng.IAngularEvent, productID: number, quantity: number): ng.IPromise<SalesItem> => {
            // this.consoleDebug("updateCartItemQuantity entered");
            if (!productID) {
                // this.consoleDebug("updateCartItemQuantity exiting, no product ID");
                return this.$q.reject("No product ID");
            }
            const cartItem = _.find(this.cvCartService.accessCart(this.type).SalesItems,
                x => x.ProductID === productID);
            if (!cartItem) {
                // this.consoleDebug("updateCartItemQuantity exiting, no cart item to adjust");
                return this.$q.reject("Could not find cart item to adjust");
            }
            const originalTotalQuantity = (cartItem.Quantity || 0)
                + (cartItem.QuantityBackOrdered || 0)
                + (cartItem.QuantityPreSold || 0);
            if (originalTotalQuantity === quantity) {
                // Don't make an API call as we match original
                // this.consoleDebug("updateCartItemQuantity exiting, we match orig");
                return this.$q.resolve(cartItem);
            }
            this.updatingCart = true;
            // this.consoleDebug(`updateCartItemQuantity: we're gonna run a change to api to set new quantity of ${quantity} from old quantity of ${originalTotalQuantity}`);
            this.setRunning();
            return this.$q((resolve, reject) => {
                const dto = <api.UpdateCartItemQuantityDto>{
                    CartItemID: cartItem.ID,
                    Quantity: 0,
                    QuantityBackOrdered: 0,
                    QuantityPreSold: 0
                };
                if ((cartItem.QuantityPreSold || 0) > 0) {
                    dto.QuantityPreSold = quantity;
                } else {
                    dto.Quantity = quantity;
                }
                // this.consoleDebug(`updateCartItemQuantity: run api call to set quantity of ${quantity}`);
                this.cvApi.shopping.UpdateCartItemQuantity(dto).then(r => {
                    // this.consoleDebug(`updateCartItemQuantity: api call is back from setting quantity of ${quantity}`);
                    if (r && r.data
                        && r.data.Messages
                        && r.data.Messages.length) {
                        // only capturing first error message to the UI
                        this.cvMessageModalFactory(cartItem.ProductName + " " + r.data.Messages[0], 'md');
                    }
                    this.updatingCart = false;
                    // this.consoleDebug("updateCartItemQuantity: broadcast cart updated");
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, this.type); // Will call finishRunning
                    // this.consoleDebug("updateCartItemQuantity: resolving promise");
                    resolve(r.data);
                }).catch(reason2 => { this.finishRunning(true, reason2); reject(reason2); });
            });
        }
        updateCartItems(): ng.IPromise<SalesItem[]> {
            this.updatingCart = true;
            this.setRunning();
            return this.$q((resolve, reject) => {
                this.cartPromise.then(() => {
                    this.cvApi.shopping.UpdateCartItems(<api.UpdateCartItemsDto>{
                        Items: this.cvCartService.accessCart(this.type).SalesItems,
                        TypeName: this.type
                    }).then(r => {
                        this.updatingCart = false;
                        resolve(r.data);
                        this.loadCurrentCart(true);
                    }).catch(reason2 => { this.finishRunning(true, reason2); reject(reason2); });
                }).catch(reason => { this.finishRunning(true, reason); reject(reason); });
            });
        }
        clearCart(): ng.IPromise<any> {
            return this.cvCartService.clearCart(this.type);
        }
        setAllShippingSelections(): void {
            switch (this.allSelectedShipping) {
                case core.ShippingOptions.ShipToHome:
                case core.ShippingOptions.InStorePickup:
                case core.ShippingOptions.ShipToStore:
                default: {
                    // Do Nothing
                    break;
                }
            }
        }
        totalItems(): number {
            return _.sumBy(this.cvCartService.accessCart(this.type).SalesItems,
                x => x.Quantity + (x.QuantityBackOrdered || 0) + (x.QuantityPreSold || 0));
        }
        totalPrice(): number {
            return _.sumBy(this.cvCartService.accessCart(this.type).SalesItems,
                x => x.ExtendedPrice);
        }
        cartItemCustomKeys(): Array<string> {
            return this.cvCartService.accessCart(this.type).SalesItems
                .map(x => x.ProductKey);
        }
        getStorePriceRules(product: api.ProductModel): void {
            if (!product || !product.Stores || !product.Stores.length) { return; }
            product.Stores.forEach(store => {
                this.cvApi.pricing.GetPriceRules({ StoreID: store.SlaveID }).then(response => {
                    if (!response || !response.data || !response.data.Results || !response.data.Results.length) {
                        return;
                    }
                    product["StorePriceRules"] = response.data.Results;
                    this.setFundraisingAdjustment(product);
                }).catch(reason => console.error(reason))
            })
        }
        setFundraisingAdjustment(product: api.ProductModel) {
            if (!product["StorePriceRules"] || !product["StorePriceRules"].length) { return; }
            for (let i = 0; i < product["StorePriceRules"].length; i++) {
                if (product["StorePriceRules"][i].Name === "Fundraising") {
                    product["FundraisingAdjustment"] = product["StorePriceRules"][i].PriceAdjustment;
                    break;
                }
            }
        }
        refreshContactChecks(force: boolean = false): ng.IPromise<void> {
            this.setRunning();
            return this.cvAddressBookService.refreshContactChecks(force, "AddressBookController.refreshContactChecks")
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $window: ng.IWindowService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvMessageModalFactory: store.modals.IMessageModalFactory,
                protected readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                protected readonly cvStoreLocationService: services.IStoreLocationService,
                protected readonly cvInventoryService: services.IInventoryService,
                protected readonly cvFacebookPixelService: services.IFacebookPixelService,
                protected readonly cvGoogleTagManagerService: services.IGoogleTagManagerService,
                protected readonly cvSecurityService: services.ISecurityService,
                protected readonly cvAddressBookService: services.IAddressBookService,) {
            super(cefConfig);
            this.loadCollections();
            this.loadSupervisors();
            this.refreshContactChecks(true);
        }
    }

    cefApp.directive("cefCart", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            includeQuickOrder: "=?",
            noInitialLoad: "=?",
            type: "=",
            supervisorList: "=?",
            supervisorSelectList: "=?",
            selectedSupervisorID: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/cart/views/cart.html", "ui"),
        controller: CartController,
        controllerAs: "cartCtrl",
        bindToController: true
    }));
}
