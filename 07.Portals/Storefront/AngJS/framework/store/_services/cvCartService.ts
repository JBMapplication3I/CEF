/**
 * @file framework/store/_services/cvCartService.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Cart service class
 */
module cef.store.services {
    export interface IAddCartItemParams {
        SerializableAttributes?: api.SerializableAttributesDictionary;
        selectedShipOption?: string;
        ProductInventoryLocationSectionID?: number;
        currentInventoryLimit?: number;
        ForceUniqueLineItemKey?: string;
        userID?: number;
        accountID?: number;
        storeID?: number;
        forceNoModal?: boolean;
    }

    export interface ICartService {
        /**
         * @memberof ICartService
         */
        viewstate: { checkoutIsProcessing: boolean };
        /**
         * @param {string} [type='Cart'] The type of cart
         * @memberof ICartService
         */
        accessCart: (type: string) => api.CartModel;
        /**
         * Access the Targeted Carts as stored from Targeted Checkout
         * @memberof ICartService
         */
        accessTargetedCarts: () => api.CartModel[];
        overrideTargetedCarts: (newValue: api.CartModel[]) => void;
        /**
         * @param {string} [type='Cart'] - The type of cart
         * @memberof ICartService
         */
        overrideCachedCartWithModifications: (type: string, newValue: api.CartModel) => void;
        /**
         * @memberof ICartService
         */
        uncacheCart: (type: string) => void;
        /**
         * Loads the full cart details for the specified cart type. Carts are locally cached in the service
         * for faster recall
         * @param {string} [type='Cart'] - The type of cart
         * @param {boolean} [force=true] - Force a reload of the cached cart (defaults to true as that was the legacy behavior)
         * @param {string} [caller=null] - Adds a debug message to the outgoing call to trace where it came from
         * @returns {ng.IPromise<api.CartModel>} - An Asyncronous Promise to return the full cart model
         *                                         for the specified cart type
         * @memberof ICartService
         */
        loadCart: (
            type: string,
            force: boolean,
            caller: string
            ) => ng.IPromise<api.CEFActionResponseT<api.CartModel>>;
        /**
         * Loads the SalesItems for the specified cart type
         * @param {string} [type='Cart'] - The type of cart
         * @returns {ng.IPromise<api.SalesItemBaseModel[]>} An Asyncronous Promise to return an array
         *                                                  of Sales Items for the specified cart type
         * @memberof ICartService
         */
        loadCartItems: (
            type?: string
            ) => ng.IPromise<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]>;
        /**
         * Adds the specified item to the specified cart type, optionally with a quantity and specialized
         * parameters
         * @param {number} id - The Product ID
         * @param {string} [type='Cart'] - The type of cart
         * @param {number} [quantity] - The quantity of the product to add
         * @param {@link IAddCartItemParams} [params] - The specialized parameters such as Serializable
         *                                              Attributes
         * @param {any} [item] - The product object itself, to provide additional data such as purchase
         *                       constraints (limited stock, or max purchase quanties). Usually a
         *                       {@link api.ProductCatalogItem} or {@link api.ProductModel}
         * @returns {ng.IPromise<void>} An Asyncronous Promise to return when the operation is complete.
         * @memberof ICartService
         */
        addCartItem: (id: number, type?: string, quantity?: number, params?: IAddCartItemParams, item?: any) => ng.IPromise<void>;
        /**
         * Adds the specified item to the specified cart type, optionally with a quantity and specialized
         * parameters
         * @param {number} id - The Product ID
         * @param {string} [type='Cart'] - The type of cart
         * @param {number} [quantity] - The quantity of the product to add
         * @param {@link IAddCartItemParams[]} [params] - The specialized parameters such as Serializable
         *                                                Attributes for each cart item to add
         * @returns {ng.IPromise<void>} An Asyncronous Promise to return when the operation is complete.
         * @memberof ICartService
         */
        addCartItems: (id: number, type?: string, quantity?: number, params?: IAddCartItemParams[]) => ng.IPromise<void>;
        /**
         * Remove an item from any cart by the cart item id (not specific to any cart type)
         * @param {number} id - The cart item identifier
         * @param {number} productID - The product identifier
         * @param {string} [type='Cart'] - The type of cart
         * @returns {ng.IPromise<any>} An Asyncronous Promise to return the result of removing the cart
         *                             item.
         * @memberof ICartService
         */
        removeCartItem: (id: number, productID: number, type?: string) => ng.IPromise<any>;
        /**
         * Remove an item from a specified cart type by the product identifier
         * @param {number} productID - The product identifier
         * @param {string} [type='Cart'] - The type of cart
         * @param {string} [forceUniqueLineItemKey=null] - The special unique key used in some scenarios
         *                                                 to narrow to a specific cart item
         * @returns {ng.IPromise<any>} An Asyncronous Promise to return the result of removing the cart
         *                             item.
         * @memberof ICartService
         */
        removeCartItemByType: (productID: number, type?: string, forceUniqueLineItemKey?: string) => ng.IPromise<any>;
        /**
         * Clears/Empties the specified cart
         * @param {string} [type='Cart'] - The type of cart
         * @returns {ng.IPromise<any>} An Asyncronous Promise to return the result of clearing the cart.
         * @memberof ICartService
         */
        clearCart: (type: string) => ng.IPromise<any>;
        /**
         * Removes a Discount from the current user's session cart (Shopping, Quote) (Not available for Static carts or Samples carts)
         * @param {@link api.RemoveCartDiscountDto} routeParams - The route parameters as a Body Object
         * @generatedByCSharpType Clarity.Ecommerce.Service.RemoveCartDiscount
         * @path <API Root>/Shopping/CurrentCart/Discount/Remove
         * @verb DELETE
         * @returns {ng.IHttpPromise<CEFActionResponse>}
         * @public
         */
        removeCartDiscount: (id: number, type: string) => ng.IPromise<api.CEFActionResponse>;
        /**
         * Removes a Discount from an item in the current user's session cart (Shopping, Quote) (Not available for Static carts or Samples carts)
         * @param {@link api.removeCartItemDiscountDto} routeParams - The route parameters as a Body Object
         * @generatedByCSharpType Clarity.Ecommerce.Service.removeCartItemDiscount
         * @path <API Root>/Shopping/CurrentItem/Discount/Remove
         * @verb DELETE
         * @returns {ng.IHttpPromise<CEFActionResponse>}
         * @public
         */
        removeCartItemDiscount: (id: number, type: string) => ng.IPromise<api.CEFActionResponse>;
        /**
         * Submits the current user's session cart as a quote
         * @param {string} [type='Cart'] - The type of cart
         */
        submitCartAsQuote(type?: string): ng.IPromise<any>;
        /**
         * Redirects to the login page if not logged in before adding the specified
         * item to the Static "Notify Me When In Stock" Cart
         * @param {number} productID - The product identifier
         * @param {boolean} addOrRemove - Whether to Add or Remove the product
         * @param {string} [uniqueKey=null] - The special unique key used in some scenarios
         *                                    to narrow to a specific cart item
         * @param {number} quantity - The quantity
         * @memberof ICartService
         */
        requireLoginForNotifyMe: (productID: number, addOrRemove: boolean, uniqueKey?: string, quantity?: number) => ng.IPromise<void>;
        /**
         * Redirects to the login page if not logged in before adding the specified
         * item to the Static "Wish List" Cart
         * @param {number} productID - The product identifier
         * @param {boolean} addOrRemove - Whether to Add or Remove the product
         * @param {string} [uniqueKey=null] - The special unique key used in some scenarios
         *                                    to narrow to a specific cart item
         * @memberof ICartService
         */
        requireLoginForWishList: (productID: number, addOrRemove: boolean, uniqueKey?: string) => ng.IPromise<void>;
        /**
         * Redirects to the login page if not logged in before adding the specified
         * item to the Static "Favorites" Cart
         * @param {number} productID - The product identifier
         * @param {boolean} addOrRemove - Whether to Add or Remove the product
         * @param {string} [uniqueKey=null] - The special unique key used in some scenarios to narrow to a specific cart item
         * @memberof ICartService
         */
        requireLoginForFavorites: (productID: number, addOrRemove: boolean, uniqueKey?: string) => ng.IPromise<void>;
        requireLoginForSessionCart: (productID: number, typeName: string, addOrRemove: boolean, quantity?: number, params?: any, product?: any) => ng.IPromise<void>;
        /**
         * Recalcuates the totals in a cart of the specified type
         * @param {string} [type='Cart'] - The type of cart
         * @memberof ICartService
         */
        recalculateTotals: (type?: string) => void;
        applyShippingSameAsBilling: (type: string, isSame: boolean) => ng.IPromise<void>;
        /**
         * @param {string} [type='Cart'] - The type of cart
         * @param {api.ContactModel} [contact='null'] - Optionally, a direct contact value to apply
         * @memberof ICartService
         */
        applyShippingContact: (type?: string, contact?: api.ContactModel) => ng.IPromise<void>;
        /**
         * @param {string} [type='Cart'] - The type of cart
         * @param {api.ContactMOdel} [contact='null'] - Optionally, a direct contact value to apply
         * @memberof ICartService
         */
        applyBillingContact: (type?: string, contact?: api.ContactModel) => ng.IPromise<void>;
        /**
         * Updates the sales items on the cart including the Targets assignments
         * @param {string} [type] - The type of cart
         * @param {api.SalesItemBaseModel[]} [salesItems] - The sales items array with the targets on each
         * @memberof ICartService
         */
        updateCartTargets: (type: string, salesItems: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]) => ng.IPromise<void>;
        /**
         * .Checks if the cart of the specified tpe has an instance of this product id in it
         * @param {number} productID Identifier for the product.
         * @param {string} [type='Cart'] The type.
         * @return {cartContainsItem} A cartContainsItem.
         */
        cartContainsItem: (productID: number, type?: string) => boolean;
        /**
         * NOTE: Returning a -1 means the value is invalid
         */
        totalTargetedCartsShippingRaw: () => number;
        totalTargetedCartsShipping: () => string;
        totalTaxes: (type?: string) => string;
        grandTotal: (type?: string) => string;
        validationResponse: api.CEFActionResponseT<api.CartModel>;
        validForCheckout: boolean;
        validForSubmitQuote: boolean;
        expectValidateFrom: string;
        allShippableTargetCartsHaveRatesSelected: () => boolean;
        updateCartAttributes: (type?: string) => void;
    }

    var requestTrace: number = 0;

    export class CartService implements ICartService {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        private debug(msg: any): void {
            if (!this.cefConfig.featureSet.carts.serviceDebug.enabled) { return; }
            this.consoleDebug(msg);
        }
        // TODO: Phase this out and use the running status instead
        viewstate = {
            checkoutIsProcessing: false,
            cartIsLoading: {
                session: false,
                compare: false,
                static: false,
            },
        };

        private carts: { [type: string]: api.CartModel } = {};
        private cartPromises: { [type: string]: ng.IPromise<api.CEFActionResponseT<api.CartModel>> } = {};
        validationResponse: api.CEFActionResponseT<api.CartModel> = null;
        expectValidateFrom: string;
        validForCheckout = true;
        validForSubmitQuote = true;

        accessCart(type: string): api.CartModel { return this.carts[type]; }

        overrideCachedCartWithModifications(type: string, newValue: api.CartModel): void {
            this.carts[type] = newValue;
        }
        accessTargetedCarts(): api.CartModel[] {
            return _.filter(this.carts, x => x && x.TypeKey && x.TypeKey.startsWith(this.cvServiceStrings.carts.targetGroupingPrefix));
        }
        overrideTargetedCarts(newValue: api.CartModel[]): void {
            if (!newValue) {
                return;
            }
            if (!newValue.length) {
                Object.keys(this.carts)
                    .filter(x => x.startsWith("Target"))
                    .forEach(x => delete this.carts[x]);
                return;
            }
            newValue.forEach(x => this.carts[x.TypeKey] = x);
        }
        recalculateTotals(type: string = this.cvServiceStrings.carts.types.cart) {
            if (!this.carts || !this.carts[type] || !this.carts[type].Totals) {
                return;
            }
            this.carts[type].Totals.Total =
                  (this.carts[type].Totals.SubTotal || 0)
                + (this.carts[type].Totals.Shipping || 0)
                + (this.carts[type].Totals.Handling || 0)
                + (this.carts[type].Totals.Fees || 0)
                + (this.carts[type].Totals.Tax || 0)
                + Math.abs(this.carts[type].Totals.Discounts || 0) * -1 // Always Add a negative amount
                ;
        }
        /**
         * NOTE: Returning a -1 means the value is invalid
         */
        totalTargetedCartsShippingRaw(): number {
            const targetedCarts = this.accessTargetedCarts();
            if (!targetedCarts || !targetedCarts.length) {
                return -1;
            }
            let valid = true;
            let total = 0;
            targetedCarts.forEach(x => {
                if (x.NothingToShip) { return true; } // Skip
                if (!x.RateQuotes
                    || !x.RateQuotes.length
                    || !x.RateQuotes.some(y => y.Active && y.Selected)) {
                    valid = false;
                    return false;
                }
                const selectedRateQuote = _.find(x.RateQuotes, y => y.Active && y.Selected);
                total += selectedRateQuote.Rate;
                return true;
            });
            return valid ? total : 0;
        }
        totalTargetedCartsShipping(): string {
            const value = this.totalTargetedCartsShippingRaw();
            if (value < 0) {
                return this.$translate.instant(
                    "ui.storefront.cartService.targetCarts.InvalidValueSelectRateQuote.Message");
            }
            return this.$filter("globalizedCurrency")(this.totalTargetedCartsShippingRaw());
        }
        totalTaxes(type: string = this.cvServiceStrings.carts.types.cart): string {
            const totals = this.accessCart(type) && this.accessCart(type).Totals;
            return this.$filter("globalizedCurrency")(totals ? totals.Tax : 0);
        }
        grandTotal(type: string = this.cvServiceStrings.carts.types.cart): string {
            const shipping = this.totalTargetedCartsShippingRaw();
            if (shipping < 0) {
                return this.$translate.instant(
                    "ui.storefront.cartService.targetCarts.InvalidValueSelectRateQuote.Message");
            }
            const totals = this.accessCart(type) && this.accessCart(type).Totals;
            if (!totals) {
                return this.$filter("globalizedCurrency")(0);
            }
            const newTotal = (totals.SubTotal || 0)
                + (shipping || 0)
                + (totals.Handling || 0)
                + (totals.Fees || 0)
                + (totals.Tax || 0)
                + Math.abs(totals.Discounts || 0) * -1;
            return this.$filter("globalizedCurrency")(newTotal);
        }
        updateCartAttributes(type?: string): void {
            switch (this.determineCartKind(type)) {
                case this.cvServiceStrings.carts.kinds.session: {
                    if (!this.carts[type]) {
                        // TODO: Error message
                        return;
                    }
                    this.cvApi.shopping.CurrentCartUpdateAttributes(this.carts[type])
                        .then(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, type, "attrsSaved"));
                    return;
                }
                case this.cvServiceStrings.carts.kinds.compare: { return; }
                case this.cvServiceStrings.carts.kinds.static:
                default: { return; }
            }
        }
        allShippableTargetCartsHaveRatesSelected(): boolean {
            ////this.debug("cartService.allShippableTargetCartsHaveRatesSelected");
            if (!this.cefConfig.featureSet.shipping.splitShipping.enabled) {
                ////this.debug("!this.cefConfig.featureSet.shipping.splitShipping.enabled");
                return false;
            }
            const carts = this.accessTargetedCarts();
            let isGood = true;
            ////this.debug(carts);
            ////this.debug("cartService.allShippableTargetCartsHaveRatesSelected: target count: " + String(carts.length));
            if (!carts.length) {
                return false;
            }
            for (let i = 0; i < carts.length; i++) {
                ////this.debug(i);
                ////this.debug(carts[i].TypeName || carts[i].Type && carts[i].Type.Name);
                ////this.debug(carts[i][this.cvServiceStrings.carts.props.hasASelectedRateQuote] || false);
                carts[i][this.cvServiceStrings.carts.props.hasASelectedRateQuote]
                    = carts[i].NothingToShip // NothingToShip carts don't need to select a rate quote
                    || Boolean(carts[i].RateQuotes
                        && carts[i].RateQuotes.length
                        && _.some(carts[i].RateQuotes, x => x.Selected));
                isGood = isGood && carts[i][this.cvServiceStrings.carts.props.hasASelectedRateQuote];
            }
            ////this.debug("cartService.allShippableTargetCartsHaveRatesSelected: all have a selection?: " + String(isGood));
            return isGood;
            ////const retVal = !_.some(this.accessTargetedCarts(), x => !x[this.cvServiceStrings.carts.props.hasASelectedRateQuote]);
            ////this.debug("cartService.allShippableTargetCartsHaveRatesSelected: all have: " + String(retVal));
            ////return retVal;
        }
        uncacheCart(type: string): void {
            delete this.carts[type];
        }
        clearCart(type: string): ng.IPromise<any> {
            switch (this.determineCartKind(type)) {
                case this.cvServiceStrings.carts.kinds.session: { return this.clearSessionCartInner(type); }
                case this.cvServiceStrings.carts.kinds.compare: { return this.clearCompareCartInner(); }
                case this.cvServiceStrings.carts.kinds.static:
                default: { return this.clearStaticCartInner(type); }
            }
        }
        loadCart(
            type: string,
            force: boolean,
            caller: string,
            event: string = null)
            : ng.IPromise<api.CEFActionResponseT<api.CartModel>> {
            switch (this.determineCartKind(type)) {
                case this.cvServiceStrings.carts.kinds.session: {
                    this.viewstate.cartIsLoading.session = true;
                    return this.loadSessionCartInner(type, force, caller);
                }
                case this.cvServiceStrings.carts.kinds.compare: {
                    this.viewstate.cartIsLoading.compare = true;
                    return this.loadCompareCartInner(force, event);
                }
                case this.cvServiceStrings.carts.kinds.static:
                default: {
                    this.viewstate.cartIsLoading.static = true;
                    return this.loadStaticCartInner(type, force);
                }
            }
        }
        loadCartItems(
            type: string = this.cvServiceStrings.carts.types.cart)
            : ng.IPromise<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]> {
            switch (this.determineCartKind(type)) {
                case this.cvServiceStrings.carts.kinds.session: { return this.loadSessionCartItemsInner(type); }
                case this.cvServiceStrings.carts.kinds.compare: { return this.loadCompareCartItemsInner(); }
                case this.cvServiceStrings.carts.kinds.static:
                default: { return this.loadStaticCartItemsInner(type); }
            }
        }
        addCartItem(
            id: number,
            type: string = this.cvServiceStrings.carts.types.cart,
            quantity: number = 1,
            params: IAddCartItemParams = null,
            product?: any)
            : ng.IPromise<void> {
            this.viewstate.checkoutIsProcessing = true;
            return this.$q(resolve => {
                if (!params) {
                    params = <IAddCartItemParams>{};
                }
                if (params.currentInventoryLimit && quantity > params.currentInventoryLimit) {
                    quantity = params.currentInventoryLimit;
                }
                if (params.storeID) {
                    this.addCartItemInner(resolve, id, type, quantity, params, product);
                    return;
                }
                this.cvStoreLocationService.getUserSelectedStore()
                    .then(r => params.storeID = r.ID)
                    .finally(() => this.addCartItemInner(
                        resolve, id, type, quantity, params, product));
            });
        }
        private addCartItemInner(
            resolve: ng.IQResolveReject<any>,
            id: number,
            type: string = this.cvServiceStrings.carts.types.cart,
            quantity: number = 1,
            params: IAddCartItemParams = null,
            product?: any)
            : void {
            const kind = this.determineCartKind(type);
            if (kind === this.cvServiceStrings.carts.kinds.session) {
                resolve(this.addSessionCartItemInner(id, type, quantity, params, product));
                return;
            }
            if (kind === this.cvServiceStrings.carts.kinds.compare) {
                resolve(this.addCompareCartItemInner(id, params, product));
                return;
            }
            resolve(this.addStaticCartItemInner(id, type, params, product, quantity));
        }
        addCartItems(
            id: number,
            type: string = this.cvServiceStrings.carts.types.cart,
            quantity: number = 1,
            params: IAddCartItemParams[] = null)
            : ng.IPromise<void> {
            const kind = this.determineCartKind(type);
            if (kind !== this.cvServiceStrings.carts.kinds.session) {
                return this.$q.reject();
            }
            this.viewstate.checkoutIsProcessing = true;
            return this.$q((resolve, __) => {
                if (!params) {
                    params = [];
                }
                let storeID: number = null;
                this.cvStoreLocationService.getUserSelectedStore()
                    .then(r => storeID = r.ID)
                    .finally(() => {
                        params.forEach(p => {
                            if (!p) {
                                p = <IAddCartItemParams>{};
                            }
                            if (p.currentInventoryLimit && quantity > p.currentInventoryLimit) {
                                quantity = p.currentInventoryLimit;
                            }
                            if (!p.storeID && storeID) {
                                p.storeID = storeID;
                            }
                        });
                        resolve(this.addSessionCartItemsInner(id, type, quantity, params));
                    });
            });
        }
        private removeCartItemConfirmModalCheckGetProduct(productID: number, type: string): ng.IPromise<void> {
            return this.cvProductService.get({ id: productID })
                .then(product => this.removeCartItemConfirmModalCheck(product.Name, type));
        }
        private removeCartItemConfirmModalCheck(productName: string, type: string): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.$uibModal.open({
                    templateUrl: this.$filter("corsLink")("/framework/store/cart/modals/removeFromCartModal.html", "ui"),
                    controller: cart.modals.RemoveFromCartModal,
                    controllerAs: "removeFromCartModalCtrl",
                    size: this.cvServiceStrings.modalSizes.md,
                    resolve: {
                        itemName: () => productName,
                        type: () => type
                    }
                }).result.then(r => {
                    if (r === true) {
                        resolve();
                        return;
                    }
                    reject();
                }).catch(reject);
            });
        }
        removeCartItemByType(
            productID: number,
            type: string = this.cvServiceStrings.carts.types.cart,
            forceUniqueLineItemKey: string = null)
            : ng.IPromise<any> {
            return this.removeCartItemConfirmModalCheckGetProduct(productID, type).then(() => {
                switch (this.determineCartKind(type)) {
                    case this.cvServiceStrings.carts.kinds.session: {
                        return this.removeSessionCartItemByTypeInner(productID, type, forceUniqueLineItemKey);
                    }
                    case this.cvServiceStrings.carts.kinds.compare: {
                        return this.removeCompareCartItemByTypeInner(productID);
                    }
                    case this.cvServiceStrings.carts.kinds.static:
                    default: {
                        return this.removeStaticCartItemByTypeInner(productID, type, forceUniqueLineItemKey);
                    }
                }
            });
        }
        removeCartItem(
            id: number,
            productID: number,
            type: string = this.cvServiceStrings.carts.types.cart)
            : ng.IPromise<any> {
            return this.removeCartItemConfirmModalCheckGetProduct(productID, type).then(() => {
                return this.cvApi.shopping.RemoveCartItemByID(id)
                    .then(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemRemoved, type));
            });
        }
        requireLoginForNotifyMe(productID: number, addOrRemove: boolean, uniqueKey: string = null, quantity?: number): ng.IPromise<void> {
            return this.requireLoginForStaticCartInner(
                this.cvServiceStrings.carts.types.notifyMe, productID, addOrRemove, uniqueKey, quantity);
        }
        requireLoginForWishList(productID: number, addOrRemove: boolean, uniqueKey: string = null): ng.IPromise<void> {
            return this.requireLoginForStaticCartInner(
                this.cvServiceStrings.carts.types.wishList, productID, addOrRemove, uniqueKey);
        }
        requireLoginForFavorites(productID: number, addOrRemove: boolean, uniqueKey: string = null): ng.IPromise<void> {
            return this.requireLoginForStaticCartInner(
                this.cvServiceStrings.carts.types.favorites, productID, addOrRemove, uniqueKey);
        }
        private requireLoginForStaticCartInner(
            typeName: string,
            productID: number,
            addOrRemove: boolean,
            uniqueKey: string = null,
            qtyToAdd: number = 1)
            : ng.IPromise<void> {
            if (typeName === this.cvServiceStrings.carts.types.wishList && !this.cefConfig.featureSet.carts.wishList.enabled
                || typeName === this.cvServiceStrings.carts.types.favorites && !this.cefConfig.featureSet.carts.favoritesList.enabled
                || typeName === this.cvServiceStrings.carts.types.notifyMe && !this.cefConfig.featureSet.carts.notifyMeWhenInStock.enabled) {
                return this.$q.reject("Disabled");
            }
            return this.$q((resolve, reject) => {
                this.cvLoginModalFactory(null, null, false, true).finally(() => {
                    if (typeName === this.cvServiceStrings.carts.types.notifyMe && addOrRemove === true && qtyToAdd === 1) {
                        this.$uibModal.open({
                            size: this.cvServiceStrings.modalSizes.sm,
                            templateUrl: this.$filter("corsLink")("/framework/store/cart/modals/quantityInputModal.html", "ui"),
                            controller($uibModalInstance: ng.ui.bootstrap.IModalServiceInstance) {
                                this.qtyInput = 1;
                                this.submit = () => $uibModalInstance.close(this.qtyInput);
                                this.cancel = () => $uibModalInstance.dismiss();
                            },
                            controllerAs: "quantityInputModalCtrl"
                        }).result.then(qty => this.addCartItem(
                            productID,
                            typeName,
                            qty,
                            { ForceUniqueLineItemKey: uniqueKey },
                            null));
                        return;
                    }
                    (addOrRemove
                        ? this.addCartItem(productID, typeName, qtyToAdd, { ForceUniqueLineItemKey: uniqueKey }, null)
                        : this.removeCartItemByType(productID, typeName, uniqueKey)
                    ).then(resolve).catch(reject);
                });
            });
        }

        requireLoginForSessionCart(productID: number, typeName: string, addOrRemove: boolean, quantity: number = 1, params: any = null, product: any = null): ng.IPromise<void> {
            return this.requireLoginForSessionCartInner(
                typeName, productID, addOrRemove, quantity, params, product);
        }

        private requireLoginForSessionCartInner(
            typeName: string,
            productID: number,
            addOrRemove: boolean,
            quantity: number = 1,
            params: any = null,
            product: any = null)
            : ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.cvLoginModalFactory(null, null, false, true).finally(() => {
                    (addOrRemove
                        ? this.addCartItem(productID, typeName, quantity, params, product)
                        : this.removeCartItemByType(productID, typeName, null)
                    ).then(resolve).catch(reject);
                });
            });
        }

        removeCartDiscount(id: number, type: string): ng.IPromise<any> {
            return this.$q((resolve, reject) => {
                this.cvConfirmModalFactory(
                    this.$translate("ui.storefront.discounts.AreYouSureYouWantToRemoveThisDiscount")
                ).then(result => {
                    if (!result) {
                        reject();
                        return;
                    }
                    this.cvApi.shopping.CurrentCartRemoveDiscount({ ID: id }).then(() => {
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated,
                            type,
                            "cartDiscountRemoved");
                        resolve();
                    }).catch(reject);
                });
            });
        }
        removeCartItemDiscount(id: number, type: string): ng.IPromise<api.CEFActionResponse> {
            return this.$q((resolve, reject) => {
                this.cvConfirmModalFactory(
                    this.$translate("ui.storefront.discounts.AreYouSureYouWantToRemoveThisDiscount")
                ).then(result => {
                    if (!result) {
                        reject();
                        return;
                    }
                    this.cvApi.shopping.RemoveCartItemDiscount({ ID: id }).then(() => {
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated,
                            type,
                            "cartItemDiscountRemoved");
                        resolve();
                    }).catch(reject);
                });
            });
        }
        addToQuote(type: string, id: number, quantity: number = 1, product?: any): ng.IPromise<any> {
            return this.$q((resolve, reject) => {
                if (!id) {
                    reject("No ID was supplied.");
                    return;
                }
                const quoteItemBase = <api.AddCartItemDto>{
                    ProductID: id,
                    Quantity: quantity,
                    TypeName: type,
                    SerializableAttributes: {},
                    StoreID: null,
                    ProductInventoryLocationSectionID: null
                };
                const params = angular.extend({}, quoteItemBase);
                if (angular.isObject(product)) {
                    angular.extend(params, product);
                }
                resolve(this.cvApi.shopping.AddCartItem(params));
            });
        }
        submitCartAsQuote(type?: string): ng.IPromise<any> {
            if (angular.isUndefined(type)) {
                type = this.cvServiceStrings.carts.types.cart;
            }
            return this.loadCart(type, true, "cartService.submitCartAsQuote").then(c => {
                const cart = c.Result;
                if (cart == null) { return null; }
                var items = cart.SalesItems;
                // Try to push just the first item so we don't have a collision of multiple items creating carts
                return this.addToQuote(this.cvServiceStrings.carts.types.quote,
                    items[0].ProductID,
                    items[0].Quantity + (items[0].QuantityBackOrdered || 0) + (items[0].QuantityPreSold || 0)
                ).then(() => {
                    items.shift();
                    return this.$q.all(items.map(item => this.addToQuote(
                        this.cvServiceStrings.carts.types.quote,
                        item.ProductID,
                        item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0)))
                    ).then(() => {
                        const requiredProps = {
                            Billing: c.Result.BillingContact,
                            Shipping: c.Result.ShippingContact,
                            IsNewAccount: false,
                            IsPartialPayment: false,
                            IsSameAsBilling: true
                        };
                        // return this.cvApi.providers.SalesQuoteCheckout(angular.extend({}, requiredProps, { StoredFiles: [] }, null));
                        return null;
                    });
                });
            });
        }
        cartContainsItem(
            productID: number,
            type: string = this.cvServiceStrings.carts.types.cart)
            : boolean {
            switch (this.determineCartKind(type)) {
                case this.cvServiceStrings.carts.kinds.session: {
                    return this.cartContainsItemSessionInner(productID, type);
                }
                case this.cvServiceStrings.carts.kinds.compare: {
                    return this.cartContainsItemCompareInner(productID);
                }
                case this.cvServiceStrings.carts.kinds.static:
                default: {
                    return this.cartContainsItemStaticInner(productID, type);
                }
            }
        }
        changeStoreOnCartItems(type: string = this.cvServiceStrings.carts.types.cart): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.cvStoreLocationService.getUserSelectedStore().then(r => {
                    if (!r) { reject(); return; }
                    const storeID = r.ID;
                    if (!storeID) { reject(); return; }
                    const tempCartItems = this.carts[type].SalesItems;
                    if (!tempCartItems || !tempCartItems.length) {
                        resolve();
                        return;
                    }
                    tempCartItems.forEach(cartItem => {
                        if (!cartItem.SerializableAttributes) {
                            return;
                        }
                        if (cartItem.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID]) {
                            cartItem.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID].Value = String(storeID);
                            return;
                        }
                        cartItem.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID] = <api.SerializableAttributeObject>{
                            ID: null,
                            Key: this.cvServiceStrings.attributes.selectedStoreID,
                            Value: String(storeID)
                        };
                    });
                    const dto = <api.UpdateCartItemsDto>{
                        Items: tempCartItems as any,
                        StoreID: storeID as any,
                        TypeName: type as any
                    };
                    const modifiedCart = this.accessCart(type);
                    modifiedCart.StoreID = storeID as any;
                    this.overrideCachedCartWithModifications(type, modifiedCart);
                    this.cvApi.shopping.UpdateCartItems(dto).then(r2 => {
                        if (!r2 || !r2.data) {
                            reject();
                            return;
                        }
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, type, "itemsSaved");
                        resolve();
                    }).catch(reject);
                });
            });
        }
        applyShippingSameAsBilling(type: string = this.cvServiceStrings.carts.types.cart, isSame: boolean): ng.IPromise<void> {
            return this.$q<void>((resolve, reject) => {
                this.cvApi.shopping.CurrentCartSetSetSameAsBilling({ TypeName: type, IsSameAsBilling: isSame }).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.processCEFActionResponseMessages(r.data);
                        reject();
                        return;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, type, "shippingSameAsBillingSet");
                    resolve();
                }).catch(reject);
            });
        }
        applyShippingContact(
            type: string = this.cvServiceStrings.carts.types.cart,
            contact?: api.ContactModel)
            : ng.IPromise<void> {
            if (!(contact || this.accessCart(type).ShippingContact)) {
                return this.$q.reject("Nothing to apply");
            }
            return this.$q<void>((resolve, reject) => {
                (contact && angular.toJson(contact) === "{}"
                    ? this.cvApi.shopping.CurrentCartClearShippingContact({ TypeName: type })
                    : this.cvApi.shopping.CurrentCartSetShippingContact({
                        TypeName: type,
                        ShippingContact: contact || this.accessCart(type).ShippingContact
                    })
                ).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.processCEFActionResponseMessages(r.data);
                        reject();
                        return;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, type, "shippingContactSet");
                    resolve();
                }).catch(reject);
            });
        }
        applyBillingContact(
            type: string = this.cvServiceStrings.carts.types.cart,
            contact?: api.ContactModel)
            : ng.IPromise<void> {
            if (!(contact || this.accessCart(type).BillingContact)) {
                return this.$q.reject("Nothing to apply");
            }
            return this.$q<void>((resolve, reject) => {
                (contact && angular.toJson(contact) === "{}"
                    ? this.cvApi.shopping.CurrentCartClearBillingContact({ TypeName: type })
                    : this.cvApi.shopping.CurrentCartSetBillingContact({
                        TypeName: type,
                        BillingContact: contact || this.accessCart(type).BillingContact
                    })
                ).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.processCEFActionResponseMessages(r.data);
                        reject();
                        return;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, type, "billingContactSet");
                    resolve();
                }).catch(reject);
            });
        }
        updateCartTargets(
            type: string,
            salesItems: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[])
            : ng.IPromise<void> {
            const dto = [...salesItems.map(x => ({...x, Product: null}))];
            return this.$q<void>((resolve, reject) => {
                this.cvApi.shopping.UpdateCartItems({
                    TypeName: type,
                    Items: dto
                }).then(r => {
                    /*if (!r.data.ActionSucceeded) {
                        reject(r.data);
                        return;
                    }*/
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, type, "cartTargetsSaved");
                    resolve();
                }).catch(reject);
            });
        }

        private determineCartKind(type: string = this.cvServiceStrings.carts.types.cart): string {
            switch (type.toLowerCase()) {
                case this.cvServiceStrings.carts.types.cart.toLowerCase():
                case this.cvServiceStrings.carts.types.quote.toLowerCase():
                case this.cvServiceStrings.carts.types.samples.toLowerCase(): {
                    return this.cvServiceStrings.carts.kinds.session;
                }
                case this.cvServiceStrings.carts.types.compare.toLowerCase(): {
                    return this.cvServiceStrings.carts.kinds.compare;
                }
                case this.cvServiceStrings.carts.types.wishList.toLowerCase():
                case this.cvServiceStrings.carts.types.notifyMe.toLowerCase():
                case this.cvServiceStrings.carts.types.favorites.toLowerCase():
                default: {
                    if (type.toLowerCase().startsWith(this.cvServiceStrings.carts.targetGroupingPrefix.toLowerCase())) {
                        return this.cvServiceStrings.carts.kinds.session;
                    }
                    return this.cvServiceStrings.carts.kinds.static;
                }
            }
        }

        /** These carts are tied to session cookies and do not require login */
        private loadSessionCartInner(
            type: string,
            force: boolean,
            caller: string)
            : ng.IPromise<api.CEFActionResponseT<api.CartModel>> {
            const requestTraceValue = ++requestTrace;
            const debugMsg = `CART-READER-${requestTraceValue}`;
            this.debug(`${debugMsg}: Starting session Load cart (type: ${type}, force: ${force}, caller: '${caller}')`);
            this.debug(this.carts[type]);
            const promiseKey = type;
            /* If we are not forcing a refresh of the information and we already have the
             * information, then just return what we already have instead of making another call */
            if (!force && this.carts[type]) {
                this.debug(`${debugMsg}: Not forcing and already cart loaded, returning memory data`);
                this.viewstate.cartIsLoading.session = false;
                return this.$q.resolve({
                    Result: this.carts[type],
                    ActionSucceeded: true,
                    __caller: caller
                } as api.CEFActionResponseT<api.CartModel>);
            }
            /* If we don't have the data yet, but already have a promise out, then just return that
             * promise instead of making another call */
            if (!force && !this.carts[type] && this.cartPromises[promiseKey]) {
                this.debug(`${debugMsg}: Not forcing and already have a promise, returning existing promise`);
                this.viewstate.cartIsLoading.session = false;
                return this.$q.resolve(this.cartPromises[promiseKey]);
            }
            /* Otherwise we don't have the data or a promise out so we need to make one */
            this.debug(`${debugMsg}: Needing to start a call from the server, set promise`);
            const fullResolve = (resolve, cart: api.CartModel) => {
                this.debug(`${debugMsg}: Resolving in success with the following value`);
                this.carts[type] = cart;
                if (cart.RateQuotes && cart.RateQuotes.length && _.some(cart.RateQuotes, x => x.Selected)) {
                    this.onRateQuoteSelected(null, type, _.filter(cart.RateQuotes, x => x.Selected)[0].ID);
                }
                this.debug(this.carts[type]);
                this.viewstate.cartIsLoading.session = false;
                resolve({ ActionSucceeded: true, Result: this.carts[type] });
                this.debug(`${debugMsg}: Deleting promise in success`);
                delete this.cartPromises[promiseKey];
                this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.loaded, type);
            }
            return this.cartPromises[promiseKey] = this.$q((resolve, reject) => {
                this.debug(`${debugMsg}: Get Current Cart Started`);
                this.cvApi.shopping.GetCurrentCart({
                    TypeName: type,
                    Validate: type === this.cvServiceStrings.carts.types.cart,
                    __caller: caller
                } as any).then(r => {
                    this.debug(`${debugMsg}: Get Current Cart Returned`);
                    this.debug(r.data);
                    let cart = angular.isUndefined(r.data.Result) || r.data.Result as any == ""
                        ? <api.CartModel>{ "__caller": caller }
                        : r.data.Result;
                    cart["WasValidatedInService"] = true;
                    if (type == this.cvServiceStrings.carts.types.cart || type == this.expectValidateFrom) {
                        this.validationResponse = r && r.data && r.data.Messages && r.data.Messages.length ? r.data : null;
                    }
                    if (!cart.SalesItems || !cart.SalesItems.length) {
                        // this.consoleDebug(`${debugMsg}: No Line Items, resolving as empty`);
                        fullResolve(resolve, cart); /* Will call finish running */
                        return;
                    }
                    this.debug(`${debugMsg}: Get Store`);
                    this.cvStoreLocationService.getUserSelectedStore().then(store => store, () => null).then(store => {
                        this.debug(`${debugMsg}: Got Store`);
                        // Create a Summary value for Discounts that may be on the items
                        cart.SalesItems.forEach(x => {
                            x["$_totalQuantity"] = x.Quantity + (x.QuantityBackOrdered || 0) + (x.QuantityPreSold || 0);
                            Object.defineProperty(x, "TotalQuantity", {
                                // Note: using function in stead of lambda so that "this" is the object itself, not cart service
                                get: function (): number { return this["$_totalQuantity"] },
                                set: function (newValue: number) {
                                    if (newValue === undefined) {
                                        // console.trace("new value would be undefined, ignoring");
                                        return;
                                    }
                                    if (this["$_totalQuantity"] === newValue) {
                                        // console.trace("new value would be the same, ignoring");
                                        return;
                                    }
                                    // console.trace("new value is different, setting");
                                    this["$_totalQuantity"] = newValue;
                                }
                            });
                            if (!x.Discounts || !x.Discounts.length) {
                                return;
                            }
                            x["DiscountTotal"] = _.sum(x.Discounts.map(y => y.DiscountTotal));
                        });
                        // Load the data for the products' details and inventory values
                        this.debug(`${debugMsg}: Starting load of detailed cart data (products will get full data and inventory load)`);
                        this.$q.all(cart.SalesItems.map(x => {
                            return this.$q((resolve2, reject2) => {
                                this.debug(`${debugMsg}: Loading details for product id ${x.ProductID}`);
                                this.cvProductService.get({
                                    id: x.ProductID,
                                    name: x.ProductName,
                                    seoUrl: x.ProductSeoUrl,
                                    storeID: store && store.ID,
                                    // quantity: x.Quantity + (x.QuantityBackOrdered || 0) + (x.QuantityPreSold || 0)
                                }).then(p => {
                                    this.debug(`${debugMsg}: Details loaded for product id ${x.ProductID}, resolving inner promise`);
                                    // Sometimes the promise returns undefined, read the value more directly when that happens
                                    resolve2(p || this.cvProductService.getCached({ id: x.ProductID }));
                                }).catch(reject2);
                            });
                        })).then((items: api.ProductModel[]) => {
                            this.debug(`${debugMsg}: Details loaded for all products in cart, doing next step`);
                            if (!cart) {
                                // console.warn(`${debugMsg}: Cart was loading detailed data and during that time was changed to no value at all`);
                                fullResolve(resolve, cart); /* Will call finish running */
                                return;
                            }
                            if (!cart.SalesItems || !cart.SalesItems.length) {
                                // console.warn(`${debugMsg}: Cart was loading detailed data and during that time was changed to no line items`);
                                fullResolve(resolve, cart); /* Will call finish running */
                                return;
                            }
                            this.debug(`${debugMsg}: Associating products back to the line items`);
                            // Associate the products to their line items
                            cart.SalesItems.forEach(
                                x => x["Product"] = _.find(items, y => y.ID === x.ProductID) as any);
                            this.debug(`${debugMsg}: Finished, resolving and broadcasting`);
                            fullResolve(resolve, cart); /* Will call finish running */
                            this.debug(`${debugMsg}: exiting`);
                        }).catch(reason2 => {
                            // console.warn(`${debugMsg}: Deleting promise in catch 2`);
                            // console.warn(reason2);
                            reject(reason2);
                            delete this.cartPromises[promiseKey];
                            this.viewstate.cartIsLoading.session = false;
                        });
                    });
                }).catch(reason => {
                    // console.warn(`${debugMsg}: Deleting promise in catch`);
                    // console.warn(reason);
                    reject(reason);
                    delete this.cartPromises[promiseKey];
                    this.viewstate.cartIsLoading.session = false;
                });
            });
        }
        private compareCartFails = false;
        /** These carts are tied to session cookies and do not require login */
        private loadCompareCartInner(force: boolean, event: string = null): ng.IPromise<api.CEFActionResponseT<api.CartModel>> {
            if (!this.cefConfig.featureSet.carts.compare.enabled) {
                return this.$q.reject("Disabled");
            }
            const type = this.cvServiceStrings.carts.types.compare;
            /* If we are not forcing a refresh of the information and we already have the
             * information, then just return what we already have instead of making another call */
            if (!force && this.carts[type]) {
                this.viewstate.cartIsLoading.compare = false;
                return this.$q.resolve({
                    Result: this.carts[type],
                    ActionSucceeded: true
                } as api.CEFActionResponseT<api.CartModel>);
            }
            /* If we don't have the data yet, but already have a promise out, then just return that
             * promise instead of making another call */
            if (!force && !this.carts[type] && this.cartPromises[type]) {
                return this.cartPromises[type];
            }
            /* If the compare cart is stuck in 500 errors, stop trying */
            if (this.compareCartFails) {
                this.viewstate.cartIsLoading.compare = false;
                return this.$q.reject();
            }
            /* Otherwise we don't have the data or a promise out so we need to make one */
            return this.cartPromises[type] = this.$q((resolve, reject) => {
                this.cvApi.shopping.GetCurrentCompareCart().then(r => {
                    this.carts[type] = r.data as any == ""
                        ? <api.CartModel>{ }
                        : r.data;
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.loaded,
                        this.cvServiceStrings.carts.types.compare,
                        event);
                    resolve({
                        Result: this.carts[type],
                        ActionSucceeded: true
                    } as api.CEFActionResponseT<api.CartModel>);
                }, result => { reject(result); this.compareCartFails = true; })
                .catch(reason => { reject(reason); this.compareCartFails = true; })
                .finally(() => {
                    delete this.cartPromises[type];
                    this.viewstate.cartIsLoading.compare = false;
                });
            });
        }
        /** These carts are tied to the logged in user and require login */
        private loadStaticCartInner(type: string, force: boolean): ng.IPromise<api.CEFActionResponseT<api.CartModel>> {
            if (type === this.cvServiceStrings.carts.types.wishList && !this.cefConfig.featureSet.carts.wishList.enabled
                || type === this.cvServiceStrings.carts.types.favorites && !this.cefConfig.featureSet.carts.favoritesList.enabled
                || type === this.cvServiceStrings.carts.types.notifyMe && !this.cefConfig.featureSet.carts.notifyMeWhenInStock.enabled) {
                return this.$q.reject("Disabled");
            }
            /* If we are not forcing a refresh of the information and we already have the
             * information, then just return what we already have instead of making another call */
            if (!force && this.carts[type]) {
                this.viewstate.cartIsLoading.static = false;
                return this.$q.resolve({
                    Result: this.carts[type],
                    ActionSucceeded: true
                } as api.CEFActionResponseT<api.CartModel>);
            }
            /* If we don't have the data yet, but already have a promise out, then just return that
             * promise instead of making another call */
            if (!force && !this.carts[type] && this.cartPromises[type]) {
                return this.cartPromises[type];
            }
            /* Otherwise we don't have the data or a promise out so we need to make one */
            return this.cartPromises[type] = this.$q((resolve, reject) => {
                // This preAuth is required to verify the user is logged in, but if they aren't
                // logged in, we get an infinite loop of checks against the server.
                // The workaround is to run the preAuth outside of this so we don't get that
                // loop
                // this.cvAuthenticationService.preAuth().finally(() => {
                    if (!this.cvAuthenticationService.isAuthenticated()) {
                        reject();
                        delete this.cartPromises[type];
                        this.viewstate.cartIsLoading.static = false;
                        return;
                    }
                    this.cvApi.shopping.GetCurrentStaticCart({ TypeName: type }).then(r => {
                        this.carts[type] = r.data as any == ""
                            ? <api.CartModel>{ }
                            : r.data;
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.loaded, type);
                        resolve({
                            Result: this.carts[type],
                            ActionSucceeded: true
                        } as api.CEFActionResponseT<api.CartModel>);
                    }).catch(reason => reject(reason))
                    .finally(() => {
                        delete this.cartPromises[type];
                        this.viewstate.cartIsLoading.static = false;
                    });
                // });
            });
        }

        private loadSessionCartItemsInner(
            type: string)
            : ng.IPromise<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]> {
            return this.cvApi.shopping.GetCurrentCartItems({ TypeName: type })
                .then(r => r.data);
        }
        private loadCompareCartItemsInner(): ng.IPromise<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]> {
            if (!this.cefConfig.featureSet.carts.compare.enabled) {
                return this.$q.reject("Disabled");
            }
            return this.cvApi.shopping.GetCurrentCompareCartItems().then(r => r.data);
        }
        private loadStaticCartItemsInner(type: string): ng.IPromise<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]> {
            if (type === this.cvServiceStrings.carts.types.wishList && !this.cefConfig.featureSet.carts.wishList.enabled
                || type === this.cvServiceStrings.carts.types.favorites && !this.cefConfig.featureSet.carts.favoritesList.enabled
                || type === this.cvServiceStrings.carts.types.notifyMe && !this.cefConfig.featureSet.carts.notifyMeWhenInStock.enabled) {
                return this.$q.reject("Disabled");
            }
            return this.$q((resolve, reject) => {
                this.cvLoginModalFactory(null, null, false, true).finally(() => {
                    this.cvApi.shopping.GetCurrentStaticCartItems({ TypeName: type })
                        .then(r => resolve(r.data));
                });
            });
        }

        private addSessionCartItemsInner(
            id: number,
            type: string,
            quantity: number,
            params: IAddCartItemParams[])
            : ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                const arr = params.map(p => this.processParams(p)).map(p => {
                    return <api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>{
                        ID: 0,
                        Active: true,
                        CreatedDate: new Date(),
                        UnitCorePrice: null,
                        ExtendedPrice: null,
                        ItemType: api.ItemType.Item,
                        StatusID: 1,
                        ProductID: id,
                        Quantity: quantity,
                        SerializableAttributes: p ? p.SerializableAttributes || null : null,
                        StoreID: p ? p.storeID || null : null,
                        ProductInventoryLocationSectionID: p ? p.ProductInventoryLocationSectionID || null : null,
                        ForceUniqueLineItemKey: p && p.ForceUniqueLineItemKey ? p.ForceUniqueLineItemKey : null,
                        UserID: p ? p.userID || null : null
                    };
                });
                const addCartItemsDto = <api.AddCartItemsDto>{
                    TypeName: type,
                    Items: arr,
                };
                this.cvApi.shopping.AddCartItems(addCartItemsDto).then(() => {
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemAdded, type, arr[0]);
                    resolve();
                }).catch(reason => this.cvMessageModalFactory(reason.data.ResponseStatus.Message)
                    .finally(() => reject(reason.data.ResponseStatus.Message)))
                .finally(() => this.viewstate.checkoutIsProcessing = false);
            });
        }
        private addSessionCartItemInner(
            id: number,
            type: string,
            quantity: number,
            params: IAddCartItemParams,
            product?: any)
            : ng.IPromise<void> {
            this.processParams(params);
            const addCartItemDto = <api.AddCartItemDto>{
                ProductID: id,
                Quantity: quantity,
                TypeName: type,
                SerializableAttributes: params ? params.SerializableAttributes || null : null,
                ProductInventoryLocationSectionID: params ? params.ProductInventoryLocationSectionID || null : null,
                ForceUniqueLineItemKey: params && params.ForceUniqueLineItemKey ? params.ForceUniqueLineItemKey : null,
                UserID: params ? params.userID || null : null,
                AccountID: params ? params.accountID || null : null,
                StoreID: params ? params.storeID || null : null,
            };
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.AddCartItem(addCartItemDto).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        if (r.data.Messages[1] === "MULTIPLE_STORES_ERROR!") {
                            this.cvConfirmModalFactory(
                                r && r.data && r.data.Messages && r.data.Messages[0]
                                || "Unable to add item to cart")
                                .then(result => {
                                    if (!result) {
                                        reject();
                                        return;
                                    }
                                    resolve(this.clearCart("Cart").then(() => {
                                        this.cvApi.shopping.AddCartItem(addCartItemDto).then(() => {
                                            this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemAdded,
                                                type,
                                                product,
                                                addCartItemDto,
                                                params.forceNoModal || false,
                                                r.data.Messages);
                                            // Load Google tracking for add cart item if turned on
                                            if (this.cefConfig.googleTagManager.enabled
                                                && product
                                                && type === this.cvServiceStrings.carts.types.cart) {
                                                this.cvGoogleTagManagerService.add(product, quantity);
                                            }
                                            resolve();
                                        })
                                    }));
                                })
                                .finally(() => reject(r && r.data && r.data.Messages));
                        return;
                        }
                        this.cvMessageModalFactory(
                                r && r.data && r.data.Messages && r.data.Messages[0]
                                || "Unable to add item to cart")
                            .finally(() => reject(r && r.data && r.data.Messages))
                        return;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemAdded,
                        type,
                        product,
                        addCartItemDto,
                        params.forceNoModal || false,
                        r.data.Messages);
                    // Load Google tracking for add cart item if turned on
                    if (this.cefConfig.googleTagManager.enabled
                        && product
                        && type === this.cvServiceStrings.carts.types.cart) {
                        this.cvGoogleTagManagerService.add(product, quantity);
                    }
                    resolve();
                    // NOTE: Modal now handled in onCartLoaded
                })
                .catch(reason => this.cvMessageModalFactory(reason.data.ResponseStatus.Message)
                    .finally(() => reject(reason.data.ResponseStatus.Message)))
                .finally(() => this.viewstate.checkoutIsProcessing = false);
            });
        }
        private addCompareCartItemInner(id: number, params: IAddCartItemParams, item: any): ng.IPromise<void> {
            if (!this.cefConfig.featureSet.carts.compare.enabled) {
                return this.$q.reject("Disabled");
            }
            this.processParams(params);
            const dto = <api.AddCompareCartItemDto>{
                ProductID: id,
                SerializableAttributes: params ? params.SerializableAttributes || null : null,
                StoreID: params ? params.storeID || null : null
            };
            return this.$q((resolve, __) => {
                this.cvApi.shopping.AddCompareCartItem(dto).then(() => {
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemAdded,
                        this.cvServiceStrings.carts.types.compare,
                        item,
                        dto);
                    resolve();
                    // NOTE: Modal now handled in onCartLoaded
                }).finally(() => this.viewstate.checkoutIsProcessing = false);
            });
        }
        private addStaticCartItemInner(id: number, type: string, params: IAddCartItemParams, item: any, quantity?: number): ng.IPromise<void> {
            if (type === this.cvServiceStrings.carts.types.wishList && !this.cefConfig.featureSet.carts.wishList.enabled
                || type === this.cvServiceStrings.carts.types.favorites && !this.cefConfig.featureSet.carts.favoritesList.enabled
                || type === this.cvServiceStrings.carts.types.notifyMe && !this.cefConfig.featureSet.carts.notifyMeWhenInStock.enabled) {
                return this.$q.reject("Disabled");
            }
            this.processParams(params);
            const dto = <api.AddStaticCartItemDto>{
                ProductID: id,
                TypeName: type,
                SerializableAttributes: params ? params.SerializableAttributes || null : null,
                StoreID: params ? params.storeID || null : null,
                Quantity: quantity
            };
            return this.$q((resolve, reject) => {
                this.cvLoginModalFactory(null, null, false, true).finally(() => {
                    this.cvAuthenticationService.preAuth().finally(() => { // Pre-auth is required here
                        if (!this.cvAuthenticationService.isAuthenticated()) {
                            reject();
                            return;
                        }
                        this.cvApi.shopping.AddStaticCartItem(dto).then(() => {
                            this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemAdded, type, item, dto);
                            resolve();
                            // NOTE: Modal now handled in onCartLoaded
                        }).finally(() => this.viewstate.checkoutIsProcessing = false);
                    });
                });
            });
        }
        private processParams(params: IAddCartItemParams): IAddCartItemParams {
            if (!params.SerializableAttributes) {
                params.SerializableAttributes = new api.SerializableAttributesDictionary();
            }
            if (params.selectedShipOption) {
                if (params.SerializableAttributes[this.cvServiceStrings.attributes.shipOption]) {
                    params.SerializableAttributes[this.cvServiceStrings.attributes.shipOption].Value = params.selectedShipOption;
                } else {
                    params.SerializableAttributes[this.cvServiceStrings.attributes.shipOption] = <api.SerializableAttributeObject>{
                        ID: null, Key: this.cvServiceStrings.attributes.shipOption, Value: params.selectedShipOption
                    };
                }
            }
            if (params.storeID) {
                if (params.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID]) {
                    params.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID].Value = String(params.storeID);
                } else {
                    params.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID] = <api.SerializableAttributeObject>{
                        ID: null, Key: this.cvServiceStrings.attributes.selectedStoreID, Value: String(params.storeID)
                    };
                }
            }
            return params;
        }

        private removeSessionCartItemByTypeInner(productID: number, type: string, forceUniqueLineItemKey: string): ng.IPromise<any> {
            const dto = <api.RemoveCartItemByProductIDAndTypeDto>{
                ProductID: productID,
                TypeName: type,
                ForceUniqueLineItemKey: forceUniqueLineItemKey
            };
            return this.cvApi.shopping.RemoveCartItemByProductIDAndType(dto)
                .finally(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemRemoved, type));
        }
        private removeCompareCartItemByTypeInner(productID: number): ng.IPromise<any> {
            if (!this.cefConfig.featureSet.carts.compare.enabled) {
                return this.$q.reject("Disabled");
            }
            return this.cvApi.shopping.RemoveCompareCartItemByProductID(productID)
                .finally(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemRemoved,
                    this.cvServiceStrings.carts.types.compare));
        }
        private removeStaticCartItemByTypeInner(productID: number, type: string, uniqueKey: string = null): ng.IPromise<any> {
            if (type === this.cvServiceStrings.carts.types.wishList && !this.cefConfig.featureSet.carts.wishList.enabled
                || type === this.cvServiceStrings.carts.types.favorites && !this.cefConfig.featureSet.carts.favoritesList.enabled
                || type === this.cvServiceStrings.carts.types.notifyMe && !this.cefConfig.featureSet.carts.notifyMeWhenInStock.enabled) {
                return this.$q.reject("Disabled");
            }
            return this.$q((resolve, reject) => {
                this.cvLoginModalFactory(null, null, false, true).then(() => {
                    const dto = <api.RemoveStaticCartItemByProductIDAndTypeDto>{
                        ProductID: productID,
                        TypeName: type,
                        ForceUniqueLineItemKey: uniqueKey
                    };
                    this.cvApi.shopping.RemoveStaticCartItemByProductIDAndType(dto).finally(() => {
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemRemoved, type);
                        resolve();
                    });
                }).catch(reject);
            });
        }

        private onCartUpdated = (__: ng.IAngularEvent, cartType: string, event: string): void => {
            // Something changed the cart in the server, so we need to load the data from the server again
            this.loadCart(cartType, true, "cartService.onCartUpdated", event);
        }

        // NOTE: This must remain an arrow function to resolve 'this' correctly
        private onCartItemAdded = (
                __: ng.IAngularEvent, // Required positional argument
                type: string,
                item = null,
                dto: api.AddStaticCartItemDto | api.AddCartItemDto = null,
                forceNoModal: boolean = false,
                messages: string[] = null
            ): void => {
            this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, type, "itemAdded", item);
            if (forceNoModal
                || (this.cefConfig.disableAddToCartModals
                    && this.cefConfig.addToQuoteCartModalDisabled)) {
                // Disabled
                return;
            }
            if (!item) {
                // Not from the right event
                return;
            }
            if (!this.cefConfig.disableAddToCartModals 
                && type === this.cvServiceStrings.carts.types.cart) {
                this.doAddToCartModal(item, type, this.carts[type], dto, messages);
                return;
            }
            if (this.cefConfig.addToQuoteCartModalDisabled
                || type === this.cvServiceStrings.carts.types.quote) {
                return;
            }
            this.doAddToCartModal(item, type, this.carts[type], dto, messages);
        }

        doAddToCartModal(
                item2,
                cartType: string,
                cart2: api.CartModel,
                dto: api.AddStaticCartItemDto | api.AddCartItemDto,
                messages: string[])
            : void {
            const quoteTemplate = this.$filter("corsLink")("/framework/store/cart/modals/addToQuoteCartModal.html", "ui")
            const cartTemplate = this.$filter("corsLink")("/framework/store/cart/modals/addToCartModal.html", "ui")
            this.$uibModal.open({
                size: this.cvServiceStrings.modalSizes.lg,
                templateUrl: cartType === "Quote Cart" ? quoteTemplate : cartTemplate,
                controller: cart.modals.AddToCartModalController,
                controllerAs: "addToCartModalCtrl",
                resolve: {
                    url: () => window.location.pathname,
                    dto: () => dto,
                    item: () => item2,
                    cart: () => cart2,
                    type: () => cartType,
                    messages: () => messages
                }
            });
        }

        // NOTE: This must remain an arrow function to resolve 'this' correctly
        private onCartItemsAdded = (
                __: ng.IAngularEvent, // Required positional argument
                type: string,
                dto: api.AddCartItemsDto = null,
                forceNoModal: boolean = false,
                messages: string[] = null
            ): void => {
            ////if (item) { this.debug(item); }
            ////if (dto) { this.debug(dto); }
            this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, type, "itemsAdded", dto.Items);
            if (this.cefConfig.disableAddToCartModals || this.cefConfig.addToQuoteCartModalDisabled || forceNoModal) {
                // Disabled
                return;
            }
            switch (type) {
                case this.cvServiceStrings.carts.types.compare: {
                    // Do Nothing, SearchCatalogProductCompareService will generate a modal
                    break;
                }
                case this.cvServiceStrings.carts.types.cart: {
                    this.doMultiAddToCartModal(type, this.carts[type], dto, messages);
                    break;
                }
            }
        }

        doMultiAddToCartModal(
            type: string,
            cart2: api.CartModel,
            dto: api.AddCartItemsDto,
            messages: string[])
            : void {
            this.$uibModal.open({
                size: this.cvServiceStrings.modalSizes.md,
                templateUrl: this.$filter("corsLink")("/framework/store/cart/modals/addToCartModal.html", "ui"),
                controller: cart.modals.AddToCartModalController,
                controllerAs: "addToCartModalCtrl",
                resolve: {
                    url: () => window.location.pathname,
                    dto: () => dto,
                    item: () => null,
                    cart: () => cart2,
                    type: () => type,
                    messages: () => messages
                }
            });
        }

        // NOTE: This must remain an arrow function to resolve 'this' correctly
        private onCartItemRemoved = (__: ng.IAngularEvent, type: string) => {
            // Do Nothing at this time, but fire the updated event since we are "done" with our "action"
            this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, type, "itemRemoved");
        }

        // NOTE: This must remain an arrow function to resolve 'this' correctly
        private onCartCleared = ($event: ng.IAngularEvent, cartType: string) => {
            delete this.carts[cartType];
            delete this.cartPromises[cartType];
        }

        private clearSessionCartInner(type: string): ng.IPromise<any> {
            const dto = { TypeName: type };
            return this.cvApi.shopping.ClearCurrentCart(dto)
                .finally(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.cleared, type));
        }
        private clearCompareCartInner(): ng.IPromise<any> {
            if (!this.cefConfig.featureSet.carts.compare.enabled) {
                return this.$q.reject("Disabled");
            }
            return this.cvApi.shopping.ClearCurrentCompareCart()
                .finally(() => this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.cleared,
                    this.cvServiceStrings.carts.types.compare));
        }
        private clearStaticCartInner(type: string): ng.IPromise<any> {
            if (type === this.cvServiceStrings.carts.types.wishList && !this.cefConfig.featureSet.carts.wishList.enabled
                || type === this.cvServiceStrings.carts.types.favorites && !this.cefConfig.featureSet.carts.favoritesList.enabled
                || type === this.cvServiceStrings.carts.types.notifyMe && !this.cefConfig.featureSet.carts.notifyMeWhenInStock.enabled) {
                return this.$q.reject("Disabled");
            }
            return this.$q((resolve, __) => {
                this.cvLoginModalFactory(null, null, false, true).then(() => {
                    this.cvApi.shopping.ClearCurrentStaticCart({ TypeName: type }).finally(() => {
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.cleared, type);
                        resolve();
                    });
                });
            });
        }

        private cartContainsItemSessionInner(productID: number, type: string): boolean {
            if (!this.accessCart(type)) {
                this.loadCart(type, false, "cartService.cartContainsItemSessionInner");
                return false;
            }
            if (!this.accessCart(type).SalesItems
                || !this.accessCart(type).SalesItems.length) {
                return false;
            }
            return this.accessCart(type).SalesItems.some(x => x.ProductID == productID);
        }
        private cartContainsItemCompareInner(productID: number): boolean {
            if (!this.accessCart(this.cvServiceStrings.carts.types.compare)) {
                this.loadCart(this.cvServiceStrings.carts.types.compare, false, "cartService.cartContainsItemCompareInner");
                return false;
            }
            if (!this.accessCart(this.cvServiceStrings.carts.types.compare).SalesItems
                || !this.accessCart(this.cvServiceStrings.carts.types.compare).SalesItems.length) {
                return false;
            }
            return this.accessCart(this.cvServiceStrings.carts.types.compare)
                .SalesItems
                .some(x => x.ProductID == productID);
        }
        private cartContainsItemStaticInner(productID: number, type: string): boolean {
            if (!this.accessCart(type)) {
                this.loadCart(type, false, "cartService.cartContainsItemStaticInner");
                return false;
            }
            if (!this.accessCart(type).SalesItems
                || !this.accessCart(type).SalesItems.length) {
                return false;
            }
            return this.accessCart(type).SalesItems.some(x => x.ProductID == productID);
        }

        private processCEFActionResponseMessages(r: api.CEFActionResponse): boolean {
            if (!r || !r.Messages || r.Messages.length <= 0) {
                return r && r.ActionSucceeded;
            }
            r.Messages.forEach(x => {
                if (x.indexOf("ERROR") !== -1) {
                    console.error(x);
                } else if (x.indexOf("WARNING") !== -1) {
                    console.warn(x);
                } else {
                    this.consoleLog(x);
                }
            });
            return r.ActionSucceeded;
        }
        private onRateQuoteSelected = (__: ng.IAngularEvent, cartType: string, selectedRateQuoteID: number) => {
            const debugMsg = `cartService.onRateQuoteSelected(__, cartType: ${cartType}, selectedRateQuoteID: ${selectedRateQuoteID})`;
            this.debug(debugMsg);
            if (!this.carts[cartType]
                || !this.carts[cartType].RateQuotes
                || !this.carts[cartType].RateQuotes.length) {
                this.debug(`${debugMsg} No Rates on cart`);
                return;
            }
            this.debug(`${debugMsg} Rates on cart, updating the selected one in the collection`);
            // Assign the corrected one with the Selected bool, all others to false
            this.carts[cartType].RateQuotes.forEach(x => x.Selected = x.ID === selectedRateQuoteID);
            this.debug(`${debugMsg} Marking the custom in-memory flag for the cart`);
            this.carts[cartType][this.cvServiceStrings.carts.props.hasASelectedRateQuote] =
                _.some(this.carts[cartType].RateQuotes, x => x.Selected);
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvProductService: services.IProductService,
                private readonly cvStoreLocationService: IStoreLocationService,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvGoogleTagManagerService: IGoogleTagManagerService,
                private readonly cvConfirmModalFactory: modals.IConfirmModalFactory,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory,
                private readonly cvLoginModalFactory: user.ILoginModalFactory) {
            $rootScope.$on(cvServiceStrings.events.stores.selectionUpdate, () => {
                if (this.carts[cvServiceStrings.carts.types.cart]) {
                    this.changeStoreOnCartItems();
                }
            });
            $rootScope.$on(cvServiceStrings.events.stores.nearbyUpdate, () => {
                if (this.carts[cvServiceStrings.carts.types.cart]) {
                    this.changeStoreOnCartItems();
                }
            });
            $rootScope.$on(cvServiceStrings.events.shipping.rateQuoteSelected,
                this.onRateQuoteSelected);
            $rootScope.$on(cvServiceStrings.events.auth.signIn, () => {
                // We can't have the pre-auth on the inner part, so run it now to ensure we've waited
                this.cvAuthenticationService.preAuth().finally(() => {
                    this.loadCart(cvServiceStrings.carts.types.cart, true, "cartService.signInEvent");
                    this.loadCart(cvServiceStrings.carts.types.wishList, true, "cartService.signInEvent");
                    this.loadCart(cvServiceStrings.carts.types.favorites, true, "cartService.signInEvent");
                    this.loadCart(cvServiceStrings.carts.types.notifyMe, true, "cartService.signInEvent");
                    this.loadCart(cvServiceStrings.carts.types.compare, true, "cartService.signInEvent");
                });
            });
            $rootScope.$on(cvServiceStrings.events.auth.signOut, () => {
                delete this.carts[cvServiceStrings.carts.types.wishList];
                delete this.carts[cvServiceStrings.carts.types.favorites];
                delete this.carts[cvServiceStrings.carts.types.notifyMe];
                delete this.cartPromises[cvServiceStrings.carts.types.wishList];
                delete this.cartPromises[cvServiceStrings.carts.types.favorites];
                delete this.cartPromises[cvServiceStrings.carts.types.notifyMe];
            });
            // Always reload the cart of the type that was updated when it happens
            $rootScope.$on(cvServiceStrings.events.carts.updated, this.onCartUpdated);
            // Once a cart item has been added, there are sometimes extra actions to be taken
            $rootScope.$on(cvServiceStrings.events.carts.itemAdded, this.onCartItemAdded);
            $rootScope.$on(cvServiceStrings.events.carts.itemsAdded, this.onCartItemsAdded);
            $rootScope.$on(cvServiceStrings.events.carts.itemRemoved, this.onCartItemRemoved);
            $rootScope.$on(cvServiceStrings.events.carts.cleared, this.onCartCleared);
        }
    }
}
