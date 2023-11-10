/**
 * @file framework/admin/_services/cvCartService.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Cart service class
 */
module cef.admin.services {
    export interface ICartService {
        currentBrandIDToEnforce?: number;
        currentFranchiseIDToEnforce?: number;
        currentStoreIDToEnforce?: number;
        validationResponse: api.CEFActionResponseT<api.CartModel>;
        validForCheckout: boolean;
        expectValidateFrom: string;
        /**
         * Accesses the data in the local memory cart cache. Will load data in the
         * background if not found (does not wait for promise return).
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @returns {api.CartModel}
         * @memberof ICartService
         */
        accessCart: (lookupKey: api.CartByIDLookupKey) => api.CartModel;
        /**
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @param {api.CartModel} newValue The new value to set
         * @memberof ICartService
         */
        overrideCachedCartWithModifications: (lookupKey: api.CartByIDLookupKey, newValue: api.CartModel) => void;
        /**
         * Removes a locally memory cached cart.
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @memberof ICartService
         */
        uncacheCart: (lookupKey: api.CartByIDLookupKey) => void;
        /**
         * Access the Targeted Carts as stored from Target Checkout's Analyzer
         * @returns {api.CartModel[]}
         * @memberof ICartService
         */
        accessTargetedCarts: () => api.CartModel[];
        /**
         * Replace the local memory cached values for target carts with these values.
         * @param {api.CartModel[]} newValue The new values to replace the existing carts
         * @memberof ICartService
         */
        overrideTargetedCarts: (newValue: api.CartModel[]) => void;
        /**
         * Loads the full cart details for the specified cart type. Carts are locally cached in the service
         * for faster recall
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @param {boolean} [force=true] Force a reload of the cached cart (defaults to true as that was the legacy behavior)
         * @returns {ng.IPromise<api.CartModel>} An Asyncronous Promise to return the full cart model for the specified cart type
         * @memberof ICartService
         */
        loadCart: (
            lookupKey: api.CartByIDLookupKey,
            force?: boolean
        ) => ng.IPromise<api.CEFActionResponseT<api.CartModel>>;
        /**
         * Loads the SalesItems for the specified cart to be looked up
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @returns {ng.IPromise<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]>} An Asyncronous Promise to return an array of Sales Items for the specified cart type
         * @memberof ICartService
         */
        loadCartItems: (lookupKey: api.CartByIDLookupKey) => ng.IPromise<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]>;
        /**
         * Adds the specified item to the specified cart type, optionally with a quantity and specialized
         * parameters
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @param {number} productID The identifier of the product
         * @param {number} [quantity=1] The quantity of the product to add
         * @param {api.IAddCartItemParams} [params=null] The specialized parameters such as Serializable Attributes
         * @param {any} [item=null] The product object itself, to provide additional data such as purchase constraints
         *                          (limited stock, or max purchase quanties). Usually a {@see api.ProductModel}.
         * @returns {ng.IPromise<void>} An Asyncronous Promise to return when the operation is complete.
         * @memberof ICartService
         */
        addCartItem: (
            lookupKey: api.CartByIDLookupKey,
            productID: number,
            quantity?: number,
            params?: api.IAddCartItemParams,
            item?: any
        ) => ng.IPromise<void>;
        /**
         * Remove an item from any cart by the cart item id (not specific to any cart type)
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @param {number} salesItemID The identifier of the sales item
         * @returns {ng.IPromise<void>} An Asyncronous Promise to return the result of removing the cart item completes
         * @memberof ICartService
         */
        removeCartItem: (lookupKey: api.CartByIDLookupKey, salesItemID: number) => ng.IPromise<void>;
        /**
         * Clears/Empties the specified cart
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @returns {ng.IPromise<void>} An Asyncronous Promise to return the result of clearing the cart completes.
         * @memberof ICartService
         */
        clearCart: (lookupKey: api.CartByIDLookupKey) => ng.IPromise<void>;
        /**
         * Removes a Discount from the target user's session cart (Shopping, Quote) (Not available for Static carts or Samples carts)
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @param {number} discountID The identifier of the discount.
         * @returns {ng.IPromise<api.CEFActionResponse>} An Asyncronous Promise to return the result of removing the discount completes.
         * @memberof ICartService
         */
        removeCartDiscount: (lookupKey: api.CartByIDLookupKey, discountID: number) => ng.IPromise<void>;
        /**
         * Removes a Discount from an item in the target user's session cart (Shopping, Quote) (Not available for Static carts or Samples carts)
         * @param {number} discountID The identifier of the discount.
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @returns {ng.IPromise<api.CEFActionResponse>} An Asyncronous Promise to return the result of removing the discount completes.
         * @memberof ICartService
         */
        removeCartItemDiscount: (lookupKey: api.CartByIDLookupKey, discountID: number) => ng.IPromise<void>;
        /**
         * Recalcuates the totals in a cart of the specified type
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @memberof ICartService
         */
        recalculateTotals: (lookupKey: api.CartByIDLookupKey) => void;
        /**
         * Sends the current cart record in memory to the server with the billing contact on it to specifically update it's record in
         * the server. Note that the data must be set before calling this function.
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @returns {ng.IPromise<void>} An Asyncronous Promise to return when the action of applying the billing contact completes.
         * @memberof ICartService
         */
        applyBillingContact: (lookupKey: api.CartByIDLookupKey) => ng.IPromise<void>;
        /**
         * Sends the current cart record in memory to the server with the shipping contact on it to specifically update it's record
         * in the server. Note that the data must be set before calling this function.
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @returns {ng.IPromise<void>} An Asyncronous Promise to return when the action of applying the shipping contact completes.
         * @memberof ICartService
         */
        applyShippingContact: (lookupKey: api.CartByIDLookupKey) => ng.IPromise<void>;
        /**
         * Sends the current cart record in memory to the server with the attributes on it to specifically update it's record
         * in the server. Note that the data must be set before calling this function.
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @returns {ng.IPromise<void>} An Asyncronous Promise to return when the action of applying the attributes completes.
         * @memberof ICartService
         */
        applyCartAttributes: (lookupKey: api.CartByIDLookupKey) => ng.IPromise<void>;
        /**
         * Updates the sales items on the cart including the Targets assignments
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to locate the cart
         * @param {api.SalesItemBaseModel[]} salesItems The sales items array with the targets on each
         * @returns {ng.IPromise<void>} An Asyncronous Promise to return when the action of updateing the targets completes.
         * @memberof ICartService
         */
        updateCartTargets: (
            lookupKey: api.CartByIDLookupKey,
            salesItems: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]
        ) => ng.IPromise<void>;
        /**
         * Checks if the cart of the specified tpe has an instance of this product id in it
         * @param {number} productID Identifier for the product.
         * @param {string} [type='Cart'] The type.
         * @return {cartContainsItem} A cartContainsItem.
         */
        cartContainsItem: (
            lookupKey: api.CartByIDLookupKey,
            productID: number
        ) => boolean;
        /**
         * NOTE: Returning a -1 means the value is invalid
         */
        totalTargetedCartsShippingRaw: () => number;
        totalTargetedCartsShipping: () => string;
        totalTaxes: (lookupKey: api.CartByIDLookupKey) => string;
        grandTotal: (lookupKey: api.CartByIDLookupKey) => string;
        allShippableTargetCartsHaveRatesSelected: () => boolean;
    }

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
        private carts: { [cartID: number]: api.CartModel } = {};
        private cartPromises: { [lookupKey: string]: ng.IPromise<api.CEFActionResponseT<api.CartModel>> } = {};
        private cachedTargetedCarts: api.CartModel[] = null;

        validationResponse: api.CEFActionResponseT<api.CartModel> = null;
        expectValidateFrom: string;
        validForCheckout = true;
        currentBrandIDToEnforce?: number;
        currentFranchiseIDToEnforce?: number;
        currentStoreIDToEnforce?: number;

        private forceAppendDataToLookupKey(lookupKey: api.CartByIDLookupKey): void {
            if (this.currentBrandIDToEnforce && lookupKey.BID !== this.currentBrandIDToEnforce) {
                lookupKey.BID = this.currentBrandIDToEnforce;
            }
            if (this.currentFranchiseIDToEnforce && lookupKey.FID !== this.currentFranchiseIDToEnforce) {
                lookupKey.FID = this.currentFranchiseIDToEnforce;
            }
            if (this.currentStoreIDToEnforce && lookupKey.SID !== this.currentStoreIDToEnforce) {
                lookupKey.SID = this.currentStoreIDToEnforce;
            }
        }
        accessCart(lookupKey: api.CartByIDLookupKey): api.CartModel {
            if (!lookupKey) {
                return undefined;
            }
            if (!lookupKey.ID) {
                this.loadCart(lookupKey).then(() => { });
                return null;
            }
            this.forceAppendDataToLookupKey(lookupKey);
            if (!this.carts[lookupKey.ID]) {
                this.loadCart(lookupKey).then(() => { });
                return null;
            }
            return this.carts[lookupKey.ID];
        }
        overrideCachedCartWithModifications(lookupKey: api.CartByIDLookupKey, newValue: api.CartModel): void {
            if (!lookupKey) {
                return undefined;
            }
            if (!lookupKey.ID) {
                if (!newValue.ID) {
                    throw new Error("Cannot override cached cart without the cart id");
                }
                lookupKey.ID = newValue.ID;
            }
            this.forceAppendDataToLookupKey(lookupKey);
            this.carts[lookupKey.ID] = newValue;
        }
        uncacheCart(lookupKey: api.CartByIDLookupKey): void {
            this.forceAppendDataToLookupKey(lookupKey);
            delete this.carts[lookupKey.ID];
        }
        accessTargetedCarts(): api.CartModel[] {
            if (this.cachedTargetedCarts != null) {
                return this.cachedTargetedCarts;
            }
            this.cachedTargetedCarts = _.filter(
                this.carts,
                x => x && x.TypeKey && x.TypeKey.startsWith(this.cvServiceStrings.carts.targetGroupingPrefix));
            return this.cachedTargetedCarts;
        }
        overrideTargetedCarts(newValue: api.CartModel[]): void {
            if (!newValue || !newValue.length) {
                return;
            }
            newValue.forEach(x => this.carts[x.ID] = x);
            this.cachedTargetedCarts = null;
        }
        loadCart(
            lookupKey: api.CartByIDLookupKey,
            force: boolean = false
        ): ng.IPromise<api.CEFActionResponseT<api.CartModel>> {
            if (typeof(lookupKey) === typeof(api.SessionCartBySessionAndTypeLookupKey)) {
                throw new Error("Cannot load cart by session+type");
            }
            this.forceAppendDataToLookupKey(lookupKey);
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid cart lookup key");
            }
            if (typeof(lookupKey) === typeof(api.CartByIDLookupKey)) {
                // Same as default
            }
            return this.loadSessionCartInner(lookupKey as api.CartByIDLookupKey, force);
        }
        private loadSessionCartInner(lookupKey: api.CartByIDLookupKey, force: boolean): ng.IPromise<api.CEFActionResponseT<api.CartModel>> {
            this.forceAppendDataToLookupKey(lookupKey);
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid cart lookup key");
            }
            /* If we are not forcing a refresh of the information and we already have the
             * information, then just return what we already have instead of making another call
             */
            if (!force && lookupKey.ID && this.carts[lookupKey.ID]) {
                return this.$q.resolve({
                    Result: this.carts[lookupKey.ID],
                    ActionSucceeded: true
                } as api.CEFActionResponseT<api.CartModel>);
            }
            /* If we don't have the data yet, but already have a promise out, then just return that
             * promise instead of making another call */
            if (!force && lookupKey.ID && !this.carts[lookupKey.ID] && this.cartPromises[lookupKey.toString()]) {
                return this.cartPromises[lookupKey.toString()];
            }
            // Otherwise we don't have the data or a promise out so we need to make one
            return this.cartPromises[lookupKey.toString()] = this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminGetUserCartByID({
                    ID: lookupKey.ID,
                    UserID: lookupKey.UID,
                    AccountID: lookupKey.AID,
                    StoreID: lookupKey.SID,
                    FranchiseID: lookupKey.FID,
                    BrandID: lookupKey.BID
                }).then(r => {
                    const cart = angular.isUndefined(r.data.Result) || r.data.Result as any == ""
                        ? <api.CartModel>{ }
                        : r.data.Result;
                    /*
                    if (!lookupKey.ID && cart.ID) {
                        lookupKey.ID = cart.ID;
                    }
                    */
                    if (cart.ID) {
                        this.carts[lookupKey.ID] = cart;
                    }
                    this.cachedTargetedCarts = null;
                    ////if (lookupKey == this.cvServiceStrings.carts.types.cart || lookupKey == this.expectValidateFrom) {
                    ////    this.validationResponse = r && r.data && r.data.Messages && r.data.Messages.length ? r.data : null;
                    ////}
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.loaded, lookupKey);
                    resolve(r.data);
                }).catch(reject)
                .finally(() => delete this.cartPromises[lookupKey.toString()]);
            });
        }
        loadCartItems(lookupKey: api.CartByIDLookupKey): ng.IPromise<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]> {
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookupKey");
            }
            return this.loadSessionCartItemsInner(lookupKey);
        }
        private loadSessionCartItemsInner(lookupKey: api.CartByIDLookupKey): ng.IPromise<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]> {
            if (!lookupKey || !lookupKey.isValid()) {
                // TODO: Error message
                return this.$q.reject();
            }
            return this.cvApi.shopping.AdminGetCartItemsForUser(
                lookupKey.ID,
                lookupKey.UID,
                lookupKey.AID,
                { StoreID: lookupKey.SID, FranchiseID: lookupKey.FID, BrandID: lookupKey.BID }
            ).then(r => r.data);
        }
        addCartItem(
            lookupKey: api.CartByIDLookupKey,
            productID: number,
            quantity: number = 1,
            params: api.IAddCartItemParams = null,
            product: any | null = null
        ): ng.IPromise<void> {
            this.forceAppendDataToLookupKey(lookupKey);
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookup key");
            }
            return this.$q(resolve => {
                if (!params) {
                    params = <api.IAddCartItemParams>{};
                }
                if (params.currentInventoryLimit && quantity > params.currentInventoryLimit) {
                    quantity = params.currentInventoryLimit;
                }
                if (params.storeID) {
                    this.addCartItemInner(resolve, lookupKey, productID, quantity, params, product);
                    return;
                }
                this.addCartItemInner(resolve, lookupKey, productID, quantity, params, product);
            });
        }
        private addCartItemInner(
            resolve: ng.IQResolveReject<any>,
            lookupKey: api.CartByIDLookupKey,
            productID: number,
            quantity: number = 1,
            params: api.IAddCartItemParams = null,
            product: any | null = null
        ): void {
            this.forceAppendDataToLookupKey(lookupKey);
            if (!lookupKey || !lookupKey.isValid()) {
                // TODO: Error message
                return;
            }
            this.addSessionCartItemInner(resolve, lookupKey, productID, quantity, params, product);
        }
        private addSessionCartItemInner(
            resolve,
            lookupKey: api.CartByIDLookupKey,
            productID: number,
            quantity: number,
            params: api.IAddCartItemParams,
            product: any | null = null)
            : ng.IPromise<void> {
            this.forceAppendDataToLookupKey(lookupKey);
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookup key");
            }
            params = this.processParams(params);
            const addCartItemDto = <api.AdminAddCartItemForUserDto>{
                ProductID: productID,
                Quantity: quantity,
                SerializableAttributes: params ? params.SerializableAttributes || null : null,
                ProductInventoryLocationSectionID: params ? params.ProductInventoryLocationSectionID || null : null,
                ForceUniqueLineItemKey: params && params.ForceUniqueLineItemKey ? params.ForceUniqueLineItemKey : null,
                CartID: lookupKey.ID,
                UserID: params ? params.userID || lookupKey.UID : null,
                AccountID: params ? params.accountID || lookupKey.AID || null : null,
                StoreID: params ? params.storeID || lookupKey.SID || null : null,
                FranchiseID: params ? params.franchiseID || lookupKey.FID || null : null,
                BrandID: params ? params.brandID || lookupKey.BID || null : null,
            };
            return resolve(this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminAddCartItemForUser(addCartItemDto).then(() => {
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemAdded,
                        lookupKey,
                        product,
                        addCartItemDto);
                    resolve();
                    // NOTE: Modal now handled in onCartLoaded
                }, result => this.cvMessageModalFactory(result.data.ResponseStatus.Message)
                    .finally(() => reject(result.data.ResponseStatus.Message))
                ).catch(reject);
            }));
        }
        private processParams(params: api.IAddCartItemParams): api.IAddCartItemParams {
            if (!params.SerializableAttributes) {
                params.SerializableAttributes = new api.SerializableAttributesDictionary();
            }
            if (params.selectedShipOption) {
                if (params.SerializableAttributes[this.cvServiceStrings.attributes.shipOption]) {
                    params.SerializableAttributes[this.cvServiceStrings.attributes.shipOption].Value = params.selectedShipOption;
                } else {
                    params.SerializableAttributes[this.cvServiceStrings.attributes.shipOption] = <api.SerializableAttributeObject>{
                        ID: 0, Key: this.cvServiceStrings.attributes.shipOption, Value: params.selectedShipOption
                    };
                }
            }
            if (params.storeID) {
                if (params.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID]) {
                    params.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID].Value = String(params.storeID);
                } else {
                    params.SerializableAttributes[this.cvServiceStrings.attributes.selectedStoreID] = <api.SerializableAttributeObject>{
                        ID: 0, Key: this.cvServiceStrings.attributes.selectedStoreID, Value: String(params.storeID)
                    };
                }
            }
            return params;
        }
        removeCartItem(lookupKey: api.CartByIDLookupKey, salesItemID: number): ng.IPromise<void> {
            if (!salesItemID) {
                return this.$q.reject("ERROR! Invalid sales item ID");
            }
            this.forceAppendDataToLookupKey(lookupKey);
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookup key");
            }
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminRemoveCartItemByIDForUser(salesItemID).then(() => {
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemRemoved,
                        lookupKey,
                        salesItemID);
                    resolve();
                }).catch(reject);
            });
        }
        clearCart(lookupKey: api.CartByIDLookupKey): ng.IPromise<void> {
            this.forceAppendDataToLookupKey(lookupKey);
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookup key");
            }
            return this.$q(resolve => {
                this.cvApi.shopping.AdminClearCartForUser({
                    ID: lookupKey.ID,
                    UserID: lookupKey.UID,
                    AccountID: lookupKey.AID,
                    StoreID: lookupKey.SID,
                    FranchiseID: lookupKey.FID,
                    BrandID: lookupKey.BID
                }).finally(() => {
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.cleared, lookupKey)
                    resolve();
                });
            });
        }
        removeCartDiscount(lookupKey: api.CartByIDLookupKey, discountID: number): ng.IPromise<void> {
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookup key");
            }
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminRemoveCartDiscountForUser({ AppliedCartDiscountID: discountID }).then(() => {
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, lookupKey);
                    resolve();
                }).catch(reject);
            });
        }
        removeCartItemDiscount(lookupKey: api.CartByIDLookupKey, discountID: number): ng.IPromise<void> {
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookup key");
            }
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminRemoveCartItemDiscountForUser({
                    AppliedCartItemDiscountID: discountID,
                    ID: lookupKey.ID,
                    UserID: lookupKey.UID,
                    AccountID: lookupKey.AID,
                    StoreID: lookupKey.SID,
                    FranchiseID: lookupKey.FID,
                    BrandID: lookupKey.BID
                }).then(() => {
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, lookupKey);
                    resolve();
                }).catch(reject);
            });
        }
        recalculateTotals(lookupKey: api.CartByIDLookupKey) {
            if (!lookupKey.ID) {
                return;
            }
            if (!this.carts || !this.carts[lookupKey.ID] || !this.carts[lookupKey.ID].Totals) {
                return;
            }
            this.carts[lookupKey.ID].Totals.Total =
                  (this.carts[lookupKey.ID].Totals.SubTotal || 0)
                + (this.carts[lookupKey.ID].Totals.Shipping || 0)
                + (this.carts[lookupKey.ID].Totals.Handling || 0)
                + (this.carts[lookupKey.ID].Totals.Fees || 0)
                + (this.carts[lookupKey.ID].Totals.Tax || 0)
                + (Math.abs(this.carts[lookupKey.ID].Totals.Discounts || 0) * -1); // Always add a negative amount
        }
        applyBillingContact(lookupKey: api.CartByIDLookupKey): ng.IPromise<void> {
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookup key");
            }
            return this.$q<void>((resolve, reject) => {
                this.cvApi.shopping.AdminSetCartBillingContactForUser({
                    CartID: lookupKey.ID,
                    BillingContact: this.accessCart(lookupKey).BillingContact
                }).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.processCEFActionResponseMessages(r.data);
                        reject();
                        return;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, lookupKey);
                    resolve();
                }).catch(reject);
            });
        }
        applyShippingContact(lookupKey: api.CartByIDLookupKey): ng.IPromise<void> {
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookup key");
            }
            return this.$q<void>((resolve, reject) => {
                this.cvApi.shopping.AdminSetCartShippingContactForUser({
                    CartID: lookupKey.ID,
                    ShippingContact: this.accessCart(lookupKey).ShippingContact
                }).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.processCEFActionResponseMessages(r.data);
                        reject();
                        return;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, lookupKey);
                    resolve();
                }).catch(reject);
            });
        }
        applyCartAttributes(lookupKey: api.CartByIDLookupKey): ng.IPromise<void> {
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookup key");
            }
            return this.$q<void>((resolve, reject) => {
                this.cvApi.shopping.AdminUpdateCartAttributesForUser({
                    ID: lookupKey.ID,
                    UserID: lookupKey.UID,
                    AccountID: lookupKey.AID,
                    StoreID: lookupKey.SID,
                    FranchiseID: lookupKey.FID,
                    BrandID: lookupKey.BID,
                    Attributes: this.accessCart(lookupKey).SerializableAttributes
                }).then(() => {
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, lookupKey);
                    resolve();
                }).catch(reject);
            });
        }
        updateCartTargets(
            lookupKey: api.CartByIDLookupKey,
            salesItems: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]
        ): ng.IPromise<void> {
            if (!lookupKey || !lookupKey.isValid()) {
                return this.$q.reject("ERROR! Invalid lookup key");
            }
            return this.$q<void>((resolve, reject) => {
                this.cvApi.shopping.AdminUpdateCartItemsForUser({
                    ID: lookupKey.ID,
                    UserID: lookupKey.UID,
                    AccountID: lookupKey.AID,
                    StoreID: lookupKey.SID,
                    FranchiseID: lookupKey.FID,
                    BrandID: lookupKey.BID,
                    Items: salesItems,
                }).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        reject(r && r.data && r.data.Messages);
                        return;
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, lookupKey);
                    resolve();
                }).catch(reject);
            });
        }
        cartContainsItem(lookupKey: api.CartByIDLookupKey, productID: number): boolean {
            if (!lookupKey || !lookupKey.isValid()) {
                // TODO: Error message
                return false;
            }
            return this.cartContainsItemSessionInner(lookupKey, productID);
        }
        private cartContainsItemSessionInner(lookupKey: api.CartByIDLookupKey, productID: number): boolean {
            if (!lookupKey || !lookupKey.isValid()) {
                return false; // TODO: Error message
            }
            if (!this.accessCart(lookupKey)) {
                this.loadCart(lookupKey, true);
                return false;
            }
            if (!this.accessCart(lookupKey).SalesItems
                || !this.accessCart(lookupKey).SalesItems.length) {
                return false;
            }
            return this.accessCart(lookupKey).SalesItems.some(x => x.ProductID == productID);
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
            return valid ? total : -1;
        }
        totalTargetedCartsShipping(): string {
            const value = this.totalTargetedCartsShippingRaw();
            if (value < 0) {
                return this.$translate.instant(
                    "ui.admin.controls.sales.salesOrderNewWizard.InvalidValueSelectRateQuote.Message") as string;
            }
            return this.$filter("globalizedCurrency")(this.totalTargetedCartsShippingRaw());
        }
        totalTaxes(lookupKey: api.CartByIDLookupKey): string {
            const cart = this.accessCart(lookupKey);
            const totals = cart && cart.Totals;
            return this.$filter("globalizedCurrency")(totals ? totals.Tax : 0);
        }
        grandTotal(lookupKey: api.CartByIDLookupKey): string {
            const shipping = this.totalTargetedCartsShippingRaw();
            if (shipping < 0) {
                return this.$translate.instant(
                    "ui.admin.controls.sales.salesOrderNewWizard.InvalidValueSelectRateQuote.Message");
            }
            const cart = this.accessCart(lookupKey);
            const totals = cart && cart.Totals;
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
        allShippableTargetCartsHaveRatesSelected(): boolean {
            if (!this.cefConfig.featureSet.shipping.splitShipping.enabled) {
                return false;
            }
            const carts = this.accessTargetedCarts();
            let isGood = true;
            for (let i = 0; i < carts.length; i++) {
                carts[i][this.cvServiceStrings.carts.props.hasASelectedRateQuote]
                    = carts[i].NothingToShip // NothingToShip carts don't need to select a rate quote
                        || Boolean(carts[i].RateQuotes
                                    && carts[i].RateQuotes.length
                                    && _.some(carts[i].RateQuotes, x => x.Selected));
                isGood = isGood
                    && carts[i][this.cvServiceStrings.carts.props.hasASelectedRateQuote];
            }
            return isGood;
        }
        // NOTE: This must remain an arrow function to resolve 'this' correctly
        private onCartUpdated = (__: ng.IAngularEvent, lookupKey: api.CartByIDLookupKey, change: string): void => {
            if (!lookupKey || !lookupKey.isValid()) {
                // TODO: Error message
                return;
            }
            // Something changed the cart in the server, so we need to load the data from the server again
            this.loadCart(lookupKey, true);
        }
        // NOTE: This must remain an arrow function to resolve 'this' correctly
        private onCartItemAdded = (__: ng.IAngularEvent, lookupKey: api.CartByIDLookupKey): void => {
            if (!lookupKey || !lookupKey.isValid()) {
                return;
            }
            this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, lookupKey, "itemAdded");
        }
        // NOTE: This must remain an arrow function to resolve 'this' correctly
        private onCartItemRemoved = (__: ng.IAngularEvent, lookupKey: api.CartByIDLookupKey): void => {
            if (!lookupKey || !lookupKey.isValid()) {
                return;
            }
            // Do Nothing at this time, but fire the updated event since we are "done" with our "action"
            this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated, lookupKey, "itemRemoved");
        }
        // NOTE: This must remain an arrow function to resolve 'this' correctly
        private onCartCleared = (__: ng.IAngularEvent, lookupKey: api.CartByIDLookupKey): void => {
            this.uncacheCart(lookupKey);
        }
        // NOTE: This must remain an arrow function to resolve 'this' correctly
        private onRateQuoteSelected = (__: ng.IAngularEvent, lookupKey: api.CartByIDLookupKey, selectedRateQuoteID: number) => {
            const debugMsg = `cartService.onRateQuoteSelected(__, cartType: ${lookupKey}, selectedRateQuoteID: ${selectedRateQuoteID})`;
            this.debug(debugMsg);
            const cart = this.accessCart(lookupKey);
            if (!cart
                || !cart.RateQuotes
                || !cart.RateQuotes.length) {
                this.debug(`${debugMsg} No Rates on cart`);
                return;
            }
            this.debug(`${debugMsg} Rates on cart, updating the selected one in the collection`);
            // Assign the corrected one with the Selected bool, all others to false
            cart.RateQuotes.forEach(x => x.Selected = x.ID === selectedRateQuoteID);
            this.debug(`${debugMsg} Marking the custom in-memory flag for the cart`);
            cart[this.cvServiceStrings.carts.props.hasASelectedRateQuote] = _.some(cart.RateQuotes, x => x.Selected);
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
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: admin.services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory) {
            $rootScope.$on(cvServiceStrings.events.shipping.rateQuoteSelected,
                this.onRateQuoteSelected);
            // Always reload the cart of the type that was updated when it happens
            $rootScope.$on(cvServiceStrings.events.carts.updated, this.onCartUpdated);
            // Once a cart item has been added, there are sometimes extra actions to be taken
            $rootScope.$on(cvServiceStrings.events.carts.itemAdded, this.onCartItemAdded);
            $rootScope.$on(cvServiceStrings.events.carts.itemRemoved, this.onCartItemRemoved);
            $rootScope.$on(cvServiceStrings.events.carts.cleared, this.onCartCleared);
        }
    }
}
