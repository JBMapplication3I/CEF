module cef.admin.purchasing.steps.splitShipping.controls {
    type SalesItem = api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>;

    export class CartController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        lookupKey: api.CartByIDLookupKey;
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
        get cartItems(): SalesItem[] {
            return this.cvCartService.accessCart(this.lookupKey)
                && this.cvCartService.accessCart(this.lookupKey).SalesItems;
        }
        get isMoreThanNineItems(): boolean {
            if (!this.cartItems || !this.cartItems.length) {
                return false;
            }
            return this.cartItems.reduce((prev, cur) => {
                return cur.Quantity + prev;
            }, 0) > 9;
        }
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
            return this.cartPromise = this.cvCartService.loadCart(this.lookupKey, force);
        }
        // NOTE: This must remain an arrow function to resolve 'this' properly
        onCartLoaded = (__: ng.IAngularEvent, lookupKey: api.CartByIDLookupKey): void => {
            // this.consoleDebug(`CartController.onCartLoaded(__, "${lookupKey.toString()}")`);
            if (lookupKey.toString() !== this.lookupKey.toString()) {
                return;
            }
            const currentCart = this.cvCartService.accessCart(this.lookupKey);
            if (!currentCart || angular.toJson(currentCart) === "{}"
                || angular.toJson(currentCart) === "{\"hasASelectedRateQuote\":false}") {
                this.setDiscounts(currentCart);
                this.setupCartItemsPaging();
                if (angular.isFunction(this.inheritedOnCartLoadedHook)) {
                    this.inheritedOnCartLoadedHook();
                }
                return;
            }
            this.setDiscounts(currentCart);
            if (!currentCart.SalesItems || currentCart.SalesItems.length <= 0) {
                this.setupCartItemsPaging();
                this.loadCurrentCartInner(currentCart, this.inheritedOnCartLoadedHook, null); // Will call finishRunning
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
                    this.cvCartService.loadCart(this.lookupKey, true);
                    return;
                }
                currentCart.SalesItems.forEach(
                    x => x["Product"] = _.find(result, y => y["SalesItemID"] === x.ID) as any);
                this.setupCartItemsPaging();
                this.loadCurrentCartInner(currentCart, this.inheritedOnCartLoadedHook, null); // Will call finishRunning
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
            this.cartItemsPaging.data = this.cvCartService.accessCart(this.lookupKey)
                && this.cvCartService.accessCart(this.lookupKey).SalesItems
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
                            resolve3();
                            /*
                            this.cvStoreLocationService.getStoreByID(
                                    Number(salesItem.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID].Value))
                                .then(store => {
                                    angular.extend(salesItem, { usersSelectedStore: store });
                                    resolve3();
                                }).catch(reject3);
                            */
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
        removeCartItem(id: number): ng.IPromise<void> {
            return this.cvCartService.removeCartItem(this.lookupKey, id);
        }
        removeCartDiscount(id: number): ng.IPromise<any> {
            return this.cvCartService.removeCartDiscount(this.lookupKey, id);
        }
        removeCartItemDiscount(id: number): ng.IPromise<any> {
            return this.cvCartService.removeCartItemDiscount(this.lookupKey, id);
        }
        kitComponents(item: api.ProductModel): api.ProductAssociationModel[] {
            if (!item || !item.ProductAssociations || !item.ProductAssociations.length) {
                return null;
            }
            return item.ProductAssociations.filter(x => x.TypeKey === 'KIT-COMPONENT');
        }
        // NOTE: This must remain an arrow function
        updateCartItemQuantity = (__: ng.IAngularEvent, productID: number, quantity: number): ng.IPromise<SalesItem> => {
            // this.consoleDebug("updateCartItemQuantity entered");
            if (!productID) {
                // this.consoleDebug("updateCartItemQuantity exiting, no product ID");
                return this.$q.reject("No product ID");
            }
            const cartItem = _.find(this.cvCartService.accessCart(this.lookupKey).SalesItems,
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
                const dto = <api.AdminUpdateCartItemQuantityForUserDto>{
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
                this.cvApi.shopping.AdminUpdateCartItemQuantityForUser(dto).then(r => {
                    // this.consoleDebug(`updateCartItemQuantity: api call is back from setting quantity of ${quantity}`);
                    if (r && r.data
                        && r.data.Messages
                        && r.data.Messages.length) {
                        // only capturing first error message to the UI
                        this.cvMessageModalFactory(cartItem.ProductName + " " + r.data.Messages[0], 'md');
                    }
                    this.updatingCart = false;
                    // this.consoleDebug("updateCartItemQuantity: broadcast cart updated");
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, this.lookupKey); // Will call finishRunning
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
                    this.cvApi.shopping.AdminUpdateCartItemsForUser(<api.AdminUpdateCartItemsForUserDto>{
                        ID: this.lookupKey.ID,
                        UserID: this.lookupKey.UID,
                        AccountID: this.lookupKey.AID,
                        StoreID: this.lookupKey.SID,
                        FranchiseID: this.lookupKey.FID,
                        BrandID: this.lookupKey.BID,
                        Items: this.cvCartService.accessCart(this.lookupKey).SalesItems
                    }).then(r => {
                        this.updatingCart = false;
                        resolve(r.data);
                        this.loadCurrentCart(true);
                    }).catch(reason2 => { this.finishRunning(true, reason2); reject(reason2); });
                }).catch(reason => { this.finishRunning(true, reason); reject(reason); });
            });
        }
        clearCart(): ng.IPromise<any> {
            return this.cvCartService.clearCart(this.lookupKey);
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
            return _.sumBy(this.cvCartService.accessCart(this.lookupKey).SalesItems,
                x => x.Quantity + (x.QuantityBackOrdered || 0) + (x.QuantityPreSold || 0));
        }
        totalPrice(): number {
            return _.sumBy(this.cvCartService.accessCart(this.lookupKey).SalesItems,
                x => x.ExtendedPrice);
        }
        cartItemCustomKeys(): Array<string> {
            return this.cvCartService.accessCart(this.lookupKey).SalesItems
                .map(x => x.ProductKey);
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
                protected readonly cvMessageModalFactory: modals.IMessageModalFactory,
                protected readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                ////protected readonly cvStoreLocationService: services.IStoreLocationService,
                protected readonly cvInventoryService: services.IInventoryService) {
            super(cefConfig);
            this.loadCollections();
        }
    }

    class SplitShippingController extends CartController {
        // Properties
        contacts: api.AccountContactModel[];
        addressOptions: api.ContactModel[];
        /* TODO: Restore In Store Pickup and Ship to Store options
        showShipToStoreOption: boolean;
        shipToStoreOption: api.ContactModel;
        showInStorePickupOption: boolean;
        inStorePickupOption: api.ContactModel;
        */
        hideAddAddressOption: boolean;
        usePhonePrefixLookups: boolean;
        readyToLoadShippingRateQuotes: boolean;
        keyToFormName(key: string): string {
            return key.replace(/( |-|,|\{|\}|\:|\")/g, '_');
        }
        keyToGroupFormName(key: string): string {
            return "groupForm" + key.replace(/( |-|,|\{|\}|\:|\")/g, '_');
        }
        keyAndIndexToTargetFormDropDown(key: string, itemID: number, index: number): any {
            return this.forms.shipping
                [this.keyToGroupFormName(key)]
                [`itemForm${itemID}`]
                [`ddlDestinationForTarget_${itemID}_${index}`];
        }
        grouped: {
            key: string;
            value: (() => api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>)[];
        }[];
        get targetedCarts(): api.CartModel[] {
            return this.cvCartService.accessTargetedCarts();
        }
        set targetedCarts(newValue: api.CartModel[]) {
            this.cvCartService.overrideTargetedCarts(newValue);
        }
        initialized: boolean = false;
        preselectedID: number = null;
        /** NOTE: Returning a -1 means the value is invalid */
        get totalShippingRaw(): number {
            return this.cvCartService.totalTargetedCartsShippingRaw();
        }
        get totalShipping(): string {
            return this.cvCartService.totalTargetedCartsShipping();
        }
        get totalTaxes(): string {
            return this.cvCartService.totalTaxes(this.lookupKey);
        }
        get grandTotal(): string {
            return this.cvCartService.grandTotal(this.lookupKey);
        }
        // Functions
        setItemAttribute(param, cart): void {
            this.cartItems.forEach(item => {
                if (item.ProductID === param.ProductID) {
                    item.SerializableAttributes = param.SerializableAttributes || {};
                }
                return item;
            })
            this.cvCartService.updateCartTargets(this.lookupKey, this.cartItems);
            this.cvCartService.overrideTargetedCarts(cart);
            this.cvCartService.updateCartTargets(cart.TypeKey, cart.SalesItems);
        }
        preselectValues(): void {
            if (this.preselectedID === -1) {
                // User wants to add a new address for preselect, deal with that before assigning
                this.checkAddressOption(-1, null);
                // NOTE: No action taken on fail (ignored)
                // NOTE: That function will change preselectValues value which will trigger another
                // call in to this function
                return;
            }
            // TODO: Warning modal if any targets already set that they will be overidden?
            this.cartItems.forEach(item => {
                if (!item.Targets) {
                    // NOTE: This should never happen, before this point, the UI has at least
                    // created a default target to use in initializeSalesItems
                    throw Error("There was not a targets list on this item");
                }
                if (!item.Targets.length) {
                    // NOTE: This should never happen, before this point, the UI has at least
                    // created a default target to use in initializeSalesItems
                    throw Error("There were no targets on this item in it's list");
                }
                item.Targets.forEach(target => {
                    target.DestinationContactID = item.ProductNothingToShip
                        ? this.cvCartService.accessCart(this.lookupKey).BillingContact
                            ? this.cvCartService.accessCart(this.lookupKey).BillingContact.ID
                            : this.preselectedID
                        : this.preselectedID;
                    this.checkAddressOption(target.DestinationContactID, target);
                });
            });
        }
        addShippingTarget(item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>): number {
            this.setRunning(this.$translate("ui.admin.common.Analyzing.Elipses"));
            if (!item.Targets) {
                // NOTE: This should never happen, before this point, the UI has at least
                // created a default target to use in initializeSalesItems
                throw Error("There was not a targets list on this item");
            }
            if (!item.Targets.length) {
                // NOTE: This should never happen, before this point, the UI has at least
                // created a default target to use in initializeSalesItems
                throw Error("There were no targets on this item in it's list");
            }
            // Generate a target with no quantity, the allocate method will assign a value if it can
            const newTarget = this.targetFactory(0);
            if (this.allocateQuantity(item, newTarget, 1)) {
                // We successfully allocated, so we can put it in the list of targets
                const resultA = item.Targets.push(newTarget);
                this.targetedCarts = []; // Wipe the list because it's going to need to be reanalyzed
                this.finishRunning();
                return resultA;
            }
            // We couldn't allocate, so just return the list as it was
            const resultB = item.Targets.length;
            this.finishRunning();
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
            return resultB;
        }
        removeShippingTarget(item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>, target: api.SalesItemTargetBaseModel): void {
            if (!item.Targets || !target) {
                return;
            }
            _.remove(item.Targets, item => item === target)
                .forEach((removed: any) => item.Targets[0].Quantity = item.Targets[0].Quantity + removed.Quantity);
            this.targetedCarts = []; // Wipe the list because it's going to need to be reanalyzed
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
        }
        targetFactory(quantity: number, contactID: number = null): api.SalesItemTargetBaseModel {
            return <api.SalesItemTargetBaseModel>{
                Active: true,
                CreatedDate: new Date(),
                DestinationContactID: contactID || null,
                DestinationContact: null,
                OriginProductInventoryLocationSectionID: 0,
                MasterID: 0,
                OriginStoreProductID: 0,
                OriginVendorProductID: 0,
                SelectedRateQuoteID: 0,
                TypeID: 0,
                TypeKey: this.cvServiceStrings.attributes.shipToHome,
                Quantity: quantity
            };
        }
        allocateQuantity(
            item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>,
            target: api.SalesItemTargetBaseModel,
            byQuantity: number = 1
            ): boolean {
            const totalQuantity = item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0);
            if (!(byQuantity <= (totalQuantity - 1))) {
                this.consoleDebug("invalid byQuantity for allocateQuantity, would attempt to cause over-allocation, blocking modification");
                return false;
            }
            if (!item.Targets) {
                this.consoleDebug("This item has no targets list, cannot modify quantity");
                return false;
            }
            return item.Targets.reduce((allocated: boolean, thisTarget) => {
                this.consoleDebug("Val-0: Checking to allocate");
                this.consoleDebug(thisTarget);
                if (allocated) {
                    this.consoleDebug("Val-1: Reduce already allocated");
                    return allocated;
                }
                if (thisTarget["$$hashKey"] === target["$$hashKey"]) {
                    this.consoleDebug("Val-2: Same target, skipping allocate");
                    return allocated;
                }
                if (!((thisTarget.Quantity - 1) >= byQuantity)) {
                    this.consoleDebug("Val-3: Would not be able to adjust this item up to allocate the new target down");
                    this.consoleDebug(`before: ${thisTarget.Quantity}`);
                    this.consoleDebug(`before: ${target.Quantity}`);
                    return allocated;
                }
                if (!((target.Quantity + byQuantity) < totalQuantity)) {
                    this.consoleDebug("Val-4: Would not be able to adjust this target to allocate to another item");
                    this.consoleDebug(`before: ${thisTarget.Quantity}`);
                    this.consoleDebug(`before: ${target.Quantity}`);
                    return allocated;
                }
                if (!((thisTarget.Quantity - byQuantity) > 0)) {
                    this.consoleDebug("Val-5: Would not be able to adjust this item down to allocate the other target up");
                    this.consoleDebug(`before: ${thisTarget.Quantity}`);
                    this.consoleDebug(`before: ${target.Quantity}`);
                    return allocated;
                }
                if (!((target.Quantity + byQuantity) > 0)) {
                    this.consoleDebug("Val-6: Would not be able to adjust this target down to allocate the other item up");
                    this.consoleDebug(`before: ${thisTarget.Quantity}`);
                    this.consoleDebug(`before: ${target.Quantity}`);
                    return allocated;
                }
                // All validations pass, do the allocation
                this.consoleDebug("Pass: Will do the adjustment now");
                this.consoleDebug("Val-7.3: Adjusting plain");
                thisTarget.Quantity -= byQuantity;
                target.Quantity += byQuantity;
                // Update the totals
                this.consoleDebug("Val-8: Updating the totals");
                allocated = true;
                return allocated;
            }, false);
        }
        modifyQuantity(item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>, target: api.SalesItemTargetBaseModel, byQuantity = 1): void {
            if (!item.Targets || !target) {
                this.consoleDebug("This item had no targets or there wasn't a target specified to modify, cannot modify quantity");
                return;
            }
            this.allocateQuantity(item, target, byQuantity);
            this.targetedCarts = []; // Wipe the list because it's going to need to be reanalyzed
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
        }
        initializeSalesItems(items: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]): ng.IPromise<void> {
            const debugMsg = `SplitShippingController.initializeSalesItems(items: ${items && items.length}): `;
            this.consoleDebug(`${debugMsg}entered`);
            if (!angular.isArray(items) || !items.length) {
                const cart = this.cvCartService.accessCart(this.lookupKey);
                if (!cart || !cart.SalesItems || !cart.SalesItems.length) {
                    this.consoleDebug(`${debugMsg}no sales items, rejecting`);
                    return this.$q.reject();
                }
                items = cart.SalesItems;
            }
            // Billing step updates the cart and pulls a fresh copy, that copy may not have
            // targets in it yet, so have to build them (again)
            ////if (this.initialized) {
            ////    return this.$q.resolve();
            ////}
            return this.$q((resolve, reject) => {
                this.consoleDebug(`${debugMsg}starting resolve promise`);
                ////this.initialized = true;
                let defaultShippingContactID: number = null;
                if (!this.viewState["analyzing"]) {
                    this.consoleDebug(`${debugMsg}not analyzing so calling set running with 'Initializing`);
                    this.setRunning(this.$translate("ui.admin.common.Analyzing.Elipses"));
                } else {
                    this.consoleDebug(`${debugMsg}analyzing so not calling set running`);
                }
                this.consoleDebug(`${debugMsg}load cart`);
                this.loadCurrentCart(false).then(() => {
                    this.consoleDebug(`${debugMsg}pre-auth`);
                    this.cvAuthenticationService.preAuth().finally(() => {
                        this.consoleDebug(`${debugMsg}pre-auth done`);
                        const doneFn = () => {
                            this.consoleDebug(`${debugMsg}doneFn`);
                            items.map(item => {
                                // Check if the quantity changed since the last time the targets were generated
                                const totalQuantity = item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0);
                                if (item.Targets
                                    && item.Targets.length
                                    && _.sumBy(item.Targets, x => x.Quantity) !== totalQuantity) {
                                    // Reset targets because it changed
                                    item.Targets = null;
                                }
                                if (!item.Targets || !item.Targets.length) {
                                    // When there is no list, create one with a default Target that has the full
                                    // quantity. If we have a default shipping address to use, assign that.
                                    // WARNING! This is the only location where a targets list should be
                                    // initialized in the entire platform!
                                    item.Targets = [
                                        this.targetFactory(totalQuantity, defaultShippingContactID)
                                    ];
                                    return item;
                                }
                                // Check for and collapse duplicates in the full list, this is a processing issue
                                // that happens sometimes, but easily corrected by re-grouping
                                const grouped = _.groupBy(item.Targets, x => {
                                    return angular.toJson({
                                        typeKey: x.Type && x.Type.CustomKey || x.TypeKey,
                                        storeID: x.OriginStoreProductID,
                                        vendorID: x.OriginVendorProductID,
                                        ilID: x.OriginProductInventoryLocationSectionID,
                                        destID: x.DestinationContactID,
                                        nothingToShip: x.NothingToShip
                                    });
                                });
                                const replacementList = [];
                                Object.keys(grouped).forEach(key => replacementList.push(grouped[key][0]));
                                item.Targets = replacementList;
                                return item;
                            });
                            this.grouped = this.$filter("flatGroupBy")(
                                this.cartItems,
                                ["ProductNothingToShip","Product.Stores[0].MasterName","Product.Vendors[0].MasterName"],
                                "splitShipping");
                            if (!this.viewState["analyzing"]) {
                                this.finishRunning();
                            } else {
                                this.consoleDebug(`${debugMsg}analyzing so not calling finishRunning`);
                            }
                            this.consoleDebug(`${debugMsg}resolve`);
                            resolve();
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
                        };
                        if (!this.cvAuthenticationService.isAuthenticated()) {
                            this.consoleDebug(`${debugMsg}not authed, moving to donFn`);
                            doneFn();
                            return;
                        }
                        this.consoleDebug(`${debugMsg}is authed, moving to contact checks`);
                        this.cvAddressBookService.refreshContactChecks(this.lookupKey.AID, false).finally(() => {
                            this.consoleDebug(`${debugMsg}contact checks finished, checking for default shipping`);
                            if (!this.cvAddressBookService.defaultShipping[this.lookupKey.AID]
                                || !this.cvAddressBookService.defaultShipping[this.lookupKey.AID].SlaveID) {
                                this.consoleDebug(`${debugMsg}no default shipping, moving to doneFn`);
                                doneFn();
                                return;
                            }
                            this.consoleDebug(`${debugMsg}have default shipping, setting and then moving to doneFn`);
                            defaultShippingContactID = this.cvAddressBookService.defaultShipping[this.lookupKey.AID].SlaveID;
                            doneFn();
                        });
                    });
                }).catch(reason => {
                    console.error("Initialize sales items failed");
                    console.error(reason);
                    this.finishRunning(true, reason);
                    reject(reason);
                });
            });
        }
        generateAddressOptions(opts: api.AccountContactModel[]): ng.IPromise<void> {
            console.log("generateAddressOptions entered");
            if (!angular.isArray(opts)) { return this.$q.resolve(); }
            console.log("generateAddressOptions is setting running with label Initializing");
            this.setRunning("Initializing (Address Options)..."/*this.$translate("ui.admin.common.Analyzing.Elipses")*/);
            return this.$q((resolve, reject) => {
                /* TODO: Restore In Store Pickup and Ship to Store options
                var inStorePickup = this.cvContactFactory.new();
                inStorePickup.CustomKey = this.cvServiceStrings.attributes.inStorePickup;
                inStorePickup.Address = null;
                var shipToStore = this.cvContactFactory.new();
                shipToStore.CustomKey = this.cvServiceStrings.attributes.shipToStore;
                shipToStore.Address = null;
                this.$q.all([
                    this.cvContactFactory.upsert(inStorePickup),
                    this.cvContactFactory.upsert(shipToStore)
                ]).then((responseArr: ng.IHttpPromiseCallbackArg<api.ContactModel>[]) => {*/
                    this.addressOptions = [...opts.map(item => item.Slave)];
                    /* TODO: Restore In Store Pickup and Ship to Store options
                    if (Boolean(this.showInStorePickupOption) === true) {
                        this.inStorePickupOption = responseArr[0].data;
                        this.addressOptions.unshift(this.inStorePickupOption);
                    }
                    if (Boolean(this.showShipToStoreOption) === true) {
                        this.shipToStoreOption = responseArr[1].data;
                        this.addressOptions.unshift(this.shipToStoreOption);
                    }*/
                    if (this.cefConfig.featureSet.addressBook.dashboardCanAddAddresses) {
                        this.$translate("ui.admin.controls.sales.salesOrderNewWizard.AddANewAddress").then(translated => {
                            if (_.find(this.addressOptions, x => x.CustomKey === translated)) {
                                // Already added
                                this.finishRunning();
                                resolve();
                                return;
                            }
                            this.addressOptions.push(<api.ContactModel>{
                                ID: 0,
                                Active: true,
                                CreatedDate: new Date(),
                                CustomKey: translated,
                                SameAsBilling: false
                            });
                            this.finishRunning();
                            resolve();
                        }).catch(reason => { this.finishRunning(true, reason); reject(reason); });
                        return;
                    }
                    this.finishRunning();
                    resolve();
                /* TODO: Restore In Store Pickup and Ship to Store options
                }).catch(reason => this.finishRunning(true, reason));*/
            });
        }
        checkAddressOption(optionID: number, target: api.SalesItemTargetBaseModel): ng.IPromise<void> {
            this.targetedCarts = []; // Wipe the list because it's going to need to be reanalyzed
            if (!optionID) {
                return this.$q.reject("No option ID supplied");
            }
            return this.$q((resolve, reject) => {
                switch (optionID) {
                    case -1: {
                        this.cvAddressModalFactory(
                            this.$translate("ui.admin.checkout.splitShipping.addressModal.EnterShippingDestination"),
                            this.$translate("ui.admin.checkout.splitShipping.addressModal.AddAddress"),
                            "SplitShipping",
                            false,
                            null,
                            this.lookupKey.AID,
                            false
                        ).then((newAC: api.AccountContactModel) => {
                            this.setRunning(this.$translate("ui.admin.checkout.splitShipping.SavingANewAddress.Ellipses"));
                            this.cvAuthenticationService.preAuth().finally(() => {
                                if (!this.cvAuthenticationService.isAuthenticated()) {
                                    // Can't store in address book, store in local memory only instead
                                    this.addressOptions.splice(this.addressOptions.length - 1, 0, newAC.Slave);
                                    newAC.SlaveID = newAC.Slave.ID = Math.min(_.minBy(this.addressOptions, x => x.ID).ID, -3) - 1
                                    if (this.preselectedID === -1) {
                                        this.preselectedID = newAC.SlaveID;
                                    }
                                    if (target) {
                                        // This will stack negative numbers so we have individual values to select by with the dropdowns
                                        target.DestinationContactID = newAC.SlaveID;
                                        target.DestinationContact = newAC.Slave;
                                        target.DestinationContactKey = newAC.Slave.CustomKey;
                                        target.TypeID = null;
                                        target.TypeKey = this.cvServiceStrings.attributes.shipToHome;
                                        target.TypeName = null;
                                        target.Type = null;
                                    }
                                    this.finishRunning();
                                    resolve();
                                    this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
                                    return;
                                }
                                // Store in Account's Address Book
                                this.cvAuthenticationService.getCurrentAccountPromise().then(account => {
                                    var dto = newAC as api.CreateAddressInBookDto;
                                    dto.MasterID = account.ID;
                                    this.cvApi.geography.CreateAddressInBook(dto).then(r => {
                                        this.addressOptions.splice(this.addressOptions.length - 1, 0, r.data.Slave);
                                        if (this.preselectedID === -1) {
                                            this.preselectedID = r.data.SlaveID;
                                        }
                                        if (target) {
                                            target.DestinationContactID = r.data.SlaveID;
                                            target.DestinationContact = r.data.Slave;
                                            target.DestinationContactKey = r.data.Slave.CustomKey;
                                            target.TypeID = null;
                                            target.TypeKey = this.cvServiceStrings.attributes.shipToHome;
                                            target.TypeName = null;
                                            target.Type = null;
                                        }
                                        this.finishRunning();
                                        resolve();
                                        this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
                                    }).catch(reason3 => {
                                        this.blankDestinationContactInfo(target);
                                        this.finishRunning(true, reason3);
                                        reject();
                                    });
                                }).catch(reason2 => {
                                    this.blankDestinationContactInfo(target);
                                    this.finishRunning(true, reason2);
                                    reject();
                                });
                            });
                        }).catch(() => this.blankDestinationContactInfo(target));
                        break;
                    }
                    /* TODO: Restore In Store Pickup and Ship to Store options
                    case this.shipToStoreOption.ID: {
                        // TODO: Use the address of the store
                        this.selectDestinationAndTypeKey(target, this.shipToStoreOption.ID, this.cvServiceStrings.attributes.shipToStore);
                        break;
                    }
                    case this.inStorePickupOption.ID: {
                        // TODO: Use the address of the store
                        this.selectDestinationAndTypeKey(target, this.inStorePickupOption.ID, this.cvServiceStrings.attributes.inStorePickup);
                        break;
                    }
                    */
                    default: {
                        this.selectDestinationAndTypeKey(target, optionID, this.cvServiceStrings.attributes.shipToHome);
                        break;
                    }
                }
            });
        }
        private blankDestinationContactInfo(target: api.SalesItemTargetBaseModel): void {
            if (!target) { return; }
            target.DestinationContactID = null;
            target.DestinationContact = null;
            target.DestinationContactKey = null;
        }
        private selectDestinationAndTypeKey(target: api.SalesItemTargetBaseModel, optionID: number, typeKey: string): void {
            target.DestinationContactID = optionID;
            target.DestinationContact = angular.fromJson(angular.toJson(_.find(this.addressOptions, x => x.ID == optionID)));
            target.DestinationContactKey = target.DestinationContact && target.DestinationContact.CustomKey;
            target.TypeID = null;
            target.TypeKey = typeKey;
            target.TypeName = null;
            target.Type = null;
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
        }
        submit(): void {
            this.viewState["analyzing"] = true;
            this.setRunning(this.$translate("ui.admin.common.Analyzing.Elipses"));
            // Update the items
            const updatedItems = this.cartItems;
            for (let i = 0; i < updatedItems.length; i++) {
                for (let j = 0; j < this.grouped.length; j++) {
                    for (let k = 0; k < this.grouped[j].value.length; k++) {
                        const item = this.grouped[j].value[k]();
                        if (updatedItems[i].ID !== item.ID) { continue; }
                        updatedItems[i] = item;
                    }
                }
            }
            this.cvCartService.updateCartTargets(this.lookupKey, updatedItems).then(() => {
                const dto: api.AnalyzeSpecificCartToTargetCartsDto = {
                    WithCartInfo: { CartID: this.lookupKey.ID },
                    WithUserInfo: { UserID: this.lookupKey.UID },
                    IsSameAsBilling: false,
                    IsPartialPayment: false,
                    ResetAnalysis: true, // Clear previous target setups
                };
                // In case the called finishRunning, set us back into running state
                this.setRunning(this.$translate("ui.admin.common.Analyzing.Elipses"));
                // Analyze and make all the separate carts
                this.targetedCarts = []; // Clear so UI doesn't add carts on re-submit
                this.cvApi.providers.AnalyzeSpecificCartToTargetCarts(dto).then(r => {
                    if (!r.data.ActionSucceeded) {
                        this.finishRunning(true, null, r.data.Messages);
                        return;
                    }
                    this.targetedCarts = r.data.Result;
                    this.$q.all(this.targetedCarts
                        .filter(x => x.ID != null)
                        .map(x => this.cvApi.shopping.GetCartItems({ Active: true, AsListing: true, MasterID: x.ID }))
                    ).then((rarr: ng.IHttpPromiseCallbackArg<api.CartItemPagedResults>[]) => {
                        rarr.forEach(pagedResult => {
                            if (!pagedResult || !pagedResult.data || !pagedResult.data.Results || !pagedResult.data.TotalCount) {
                                console.warn("No results on one of the paged results that should have had children!");
                                return;
                            }
                            const i = _.findIndex(this.targetedCarts, y => y.ID == pagedResult.data.Results[0].MasterID);
                            this.targetedCarts[i].SalesItems = pagedResult.data.Results;
                        });
                        this.$q.all(this.targetedCarts
                            .map(x => this.cvApi.shopping.AdminGetCartShippingContactForUser({ CartID: x.ID }))
                        ).then((rArr: ng.IHttpPromiseCallbackArg<api.CEFActionResponseT<api.ContactModel>>[]) => {
                            for (let i = 0; i < rArr.length; i++) {
                                if (!rArr[i].data.ActionSucceeded) {
                                    this.targetedCarts[i].ShippingContactID = null;
                                    this.targetedCarts[i].ShippingContact = null;
                                    continue;
                                }
                                this.targetedCarts[i].ShippingContact = rArr[i].data.Result;
                                this.targetedCarts[i].ShippingContactID = rArr[i].data.Result.ID;
                            }
                            this.viewState["analyzing"] = false;
                            this.finishRunning();
                            this.readyToLoadShippingRateQuotes = true;
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.ready,
                                this.cvCartService.accessCart(this.lookupKey),
                                false); // Don't reapply the shipping contact info to the cart
                        }).catch(reason4 => this.finishRunning(true, reason4));
                    }).catch(reason3 => this.finishRunning(true, reason3));
                }).catch(reason2 => {
                    // Retry if specific error
                    if (reason2 && (angular.toJson(reason2).indexOf("[4]") !== -1 || angular.toJson(reason2).indexOf("[2]") !== -1)) {
                        this.submit();
                    } else {
                        this.finishRunning(true, reason2);
                    }
                });
            }).catch(reason => angular.isArray(reason) ? this.finishRunning(true, null, reason) : this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $window: ng.IWindowService,
                protected readonly $location: ng.ILocationService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvMessageModalFactory: modals.IMessageModalFactory,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                ////protected readonly cvStoreLocationService: services.IStoreLocationService,
                protected readonly cvInventoryService: services.IInventoryService,
                protected readonly cvContactFactory: factories.IContactFactory,
                protected readonly cvAddressModalFactory: modals.IAddressModalFactory,
                protected readonly cvAddressBookService: services.IAddressBookService) {
            super($rootScope, $scope, $q, $filter, $window, $translate, cvApi,
                cefConfig, cvServiceStrings, cvCartService, cvMessageModalFactory, cvAuthenticationService,
                cvInventoryService);
            // this.showShipToStoreOption = this.cefConfig.featureSet.shipping.shipToStore.enabled;
            // this.showInStorePickupOption = this.cefConfig.featureSet.shipping.inStorePickup.enabled;
            this.usePhonePrefixLookups = this.cefConfig.featureSet.contacts.phonePrefixLookups.enabled;
            this.hideAddAddressOption = !this.cefConfig.featureSet.addressBook.dashboardCanAddAddresses;
            const unbind1 = $scope.$watchCollection(() => this.cartItems, newValue => this.initializeSalesItems(newValue));
            const unbind2 = $scope.$watchCollection(() => this.contacts, newValue => this.generateAddressOptions(newValue));
            if (this.cartItems && this.cartItems.length) {
                this.initializeSalesItems(this.cartItems);
            }
            if (this.contacts && this.contacts.length) {
                this.generateAddressOptions(this.contacts);
            }
            // Event for rate quote selection already handled to revalidate step
            this.$scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
    }

    adminApp.directive("cefPurchaseSplitShipping", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            // Inherited
            lookupKey: "=",
            apply: "=",
            // This directive specific
            contacts: "="
        },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/splitShipping/controls/splitShipping.html", "ui"),
        controller: SplitShippingController,
        controllerAs: "ssCtrl",
        bindToController: true
    }));
}
