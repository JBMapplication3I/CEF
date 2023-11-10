/**
 * @file framework/admin/controls/sales/salesOrderCreate.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc The New Sales Order wizard for Administrators
 */
module cef.admin.controls.sales {
    export class SalesOrderCreateController extends core.TemplatedControllerBase {
        // Properties
        commonDtoBody: api.BaseSearchModel;
        lockLookupAndShowCarts = false;
        showPurchase = false;
        lookupKey: api.CartByIDLookupKey = new api.CartByIDLookupKey(null);
        brands: api.BrandModel[] = [];
        franchises: api.FranchiseModel[] = [];
        stores: api.StoreModel[] = [];
        /**
         * The Users pulled in for the typeahead flyout based on the
         * {@see userToGrab} as a search parameter
         * @private
         * @type {api.UserModel[]}
         * @memberof SalesOrderCreateController
         */
        users: api.UserModel[] = [];
        /**
         * The Accounts pulled in for the typeahead flyout based on the
         * {@see accountToGrab} as a search parameter
         * @private
         * @type {api.AccountModel[]}
         * @memberof SalesOrderCreateController
         */
        accounts: api.AccountModel[] = [];
        /**
         * The text entered into the Account typeahead
         * @private
         * @type {string}
         * @memberof SalesOrderCreateController
         */
        private accountToGrab: string = null;
        /**
         * The text entered into the User typeahead
         * @private
         * @type {string}
         * @memberof SalesOrderCreateController
         */
        private userToGrab: string = null;
        /**
         * The selected Account model as a result of picking the {@see lookupKey.AID}
         * @private
         * @type {number}
         * @memberof SalesOrderCreateController
         */
        private account: api.AccountModel = null;
        /**
         * The selected User model as a result of picking the {@see lookupKey.UID}
         * @private
         * @type {api.UserModel}
         * @memberof SalesOrderCreateController
         */
        private user: api.UserModel = null;
        currentCart: api.CartModel;
        private cartPicked = false;
        private userPicked = false;
        userCarts: api.CartModel[] = [];
        productPaging: core.ServerSidePaging<api.ProductModel, api.ProductPagedResults>;
        subtotalModifierModes: Array<{ value: number, label: string }> = [];
        unbindSearchComplete: Function;
        // Functions
        loadCollections(): void {
            if (!this.commonDtoBody) {
                const paging = <api.Paging>{ Size: 50, StartIndex: 1 };
                const sorts: api.Sort[] = [{
                    field: "ID",
                    dir: "asc",
                    order: 0,
                }];
                this.commonDtoBody = <api.BaseSearchModel>{
                    Paging: paging,
                    Sorts: sorts,
                    Active: true,
                    AsListing: true,
                };
            }
            this.setRunning();
            this.$q.all([
                this.loadBrandsCollection(),
                this.loadFranchisesCollection(),
                this.loadStoresCollection()
            ]).then(() => {
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        loadBrandsCollection(): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.cvApi.brands.GetBrands(this.commonDtoBody).then(r => {
                    if (!r || !r.data) {
                        reject("Failed to load Brands data");
                        return;
                    }
                    // TODO: Handle >50 result count when detected
                    this.brands = r.data.Results;
                    resolve();
                }).catch(reject);
            });
        }
        loadFranchisesCollection(): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                const dto = angular.copy(this.commonDtoBody) as api.GetFranchisesDto;
                if (this.lookupKey.BID) {
                    dto.BrandID = this.lookupKey.BID;
                }
                this.cvApi.franchises.GetFranchises(dto).then(r => {
                    if (!r || !r.data) {
                        reject("Failed to load Franchises data");
                        return;
                    }
                    // TODO: Handle >50 result count
                    this.franchises = r.data.Results;
                    resolve();
                });
            });
        }
        loadStoresCollection(): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                const dto = angular.copy(this.commonDtoBody) as api.GetStoresDto;
                if (this.lookupKey.FID) {
                    dto.FranchiseID = this.lookupKey.FID;
                } else if (this.lookupKey.BID) {
                    dto.BrandID = this.lookupKey.BID;
                }
                this.cvApi.stores.GetStores(dto).then(r => {
                    if (!r || !r.data) {
                        reject("Failed to load Stores data");
                        return;
                    }
                    // TODO: Handle >50 result count
                    this.stores = r.data.Results;
                    resolve();
                });
            });
        }
        setup_UserAndAccount(): ng.IPromise<boolean> {
            // Add watches to accountID and userID in case they are updated from any direction
            this.$scope.$watch(() => this.lookupKey.AID, (newVal: number, oldVal: number) => {
                if (newVal === oldVal) { return; }
                if (!newVal) {
                    this.lookupKey.AID = null;
                    this.account = null;
                    return;
                }
                this.cvApi.accounts.GetAccountByID(newVal).then(r => {
                    this.lookupKey.AID = newVal;
                    this.account = r.data;
                });
            });
            this.$scope.$watch(() => this.lookupKey.UID, (newVal: number, oldVal: number) => {
                if (newVal === oldVal) { return; }
                if (!newVal) {
                    this.lookupKey.UID = null;
                    this.user = null;
                    return;
                }
                this.cvApi.contacts.GetUserByID(newVal).then(r => {
                    this.lookupKey.UID = newVal;
                    this.user = r.data;
                    this.lookupKey.AID = this.user.AccountID;
                    if (!this.lookupKey.AID) { return; }
                    this.accountToGrab = this.lookupKey.AID.toString();
                });
            });
            return this.$q.resolve(true);
        }
        reset_UserAndAccount(): ng.IPromise<boolean> {
            // Clear lookup values
            this.accounts = [];
            this.users = [];
            this.accountToGrab = null;
            this.userToGrab = null;
            // Clear selections
            this.lookupKey.UID = null;
            this.user = null;
            this.lookupKey.AID = null;
            this.account = null;
            ////this.accountContacts = [];
            // Return
            return this.$q.resolve(true);
        }
        /**
         * Runs the search using {@see accountToGrab} and populates {@see accounts} for
         * the Account typeahead. Used by the UI.
         * @protected
         * @param {string} search
         * @returns {ng.IPromise<Array<api.AccountModel>>}
         * @memberof SalesOrderNewWizardController
         */
        protected grabAccounts(search: string): ng.IPromise<Array<api.AccountModel>> {
            const lookup = search.toLowerCase()
            return this.cvApi.accounts.GetAccounts({
                Active: true,
                AsListing: true,
                IDOrCustomKeyOrName: search,
                Paging: <api.Paging>{ Size: 25, StartIndex: 1 }
            }).then(r => this.accounts = r.data.Results.filter(
                item => (item.CustomKey || "").toLowerCase().indexOf(lookup) > -1
                     || (item.Name || "").toLowerCase().indexOf(lookup) > -1
                     || item.ID.toString().indexOf(lookup) > -1));
        }
        /**
         * Runs the search using {@see userToGrab} and populates {@see users} for
         * the User typeahead. Used by the UI.
         * @protected
         * @param {string} search
         * @returns {ng.IPromise<Array<api.AccountModel>>}
         * @memberof SalesOrderNewWizardController
         */
        protected grabUsers(search: string): ng.IPromise<Array<api.UserModel>> {
            return this.cvApi.contacts.GetUsers({
                Active: true,
                AsListing: true,
                AccountID: this.lookupKey.AID,
                IDOrUserNameOrCustomKeyOrEmailOrContactName: search,
                Paging: <api.Paging>{ Size: 25, StartIndex: 1 }
            }).then(r => this.users = r.data.Results);
        }
        /**
         * The event handler for the Account typeahead which populates the {@see lookupKey.AID}.
         * Used by the UI.
         * @protected
         * @param {*} $item - Unused, but required for positional parameter
         * @param {*} $model - The identifier to apply
         * @memberof SalesOrderNewWizardController
         */
        protected selectAccountFromTypeAhead($item: api.AccountModel, $model: number) {
            this.lookupKey.AID = Number($model);
        }
        /**
         * The event handler for the User typeahead which populates the {@see lookupKey.UID}.
         * Used by the UI.
         * @protected
         * @param {*} $item - Unused, but required for positional parameter
         * @param {*} $model - The identifier to apply
         * @memberof SalesOrderNewWizardController
         */
        protected selectUserFromTypeAhead($item: api.UserModel, $model: number) {
            this.lookupKey.UID = Number($model);
        }
        continueWithUser(): void {
            if (!this.user) {
                return;
            }
            this.lockLookupAndShowCarts = true;
            this.userPicked = true;
            this.setup_CartLineItems();
        }
        setup_CartLineItems(): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                // Translate the modifier modes values
                this.$translate([
                    "ui.admin.controls.sales.salesOrderNewWizard.Override$",
                    "ui.admin.common.PlusOrMinusDollar",
                    "ui.admin.common.PlusOrMinusPercentage"
                ]).then(translated => {
                    this.subtotalModifierModes = [
                        { value: 1, label: translated["ui.admin.controls.sales.salesOrderNewWizard.Override$"] },
                        { value: 2, label: translated["ui.admin.common.PlusOrMinusDollar"] },
                        { value: 3, label: translated["ui.admin.common.PlusOrMinusPercentage"] },
                    ];
                }).finally(() => {
                    // Set up the product pager
                    this.productPaging = new core.ServerSidePaging<api.ProductModel, api.ProductPagedResults>(
                        this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.products.GetProducts, 8, "products");
                    // Hook into the product pager so we can apply the specified user's pricing to the UI
                    // instead of seeing the current user's
                    this.unbindSearchComplete = this.$scope.$on(this.cvServiceStrings.events.paging.searchComplete,
                        (__: ng.IAngularEvent, name: string, newResults: api.ProductModel[]) => {
                            if (name !== "products") { return; }
                            if (!this.lookupKey.UID && (!this.user || !this.user.ID)) {
                                // TODO: Error message that couldn't load products without the target user
                                return;
                            }
                            const productIDs = newResults.map(x => x.ID);
                            this.cvApi.pricing.GetPricesForProductsAsUser({
                                UserID: this.lookupKey.UID,
                                ProductIDs: productIDs
                            }).then(r => {
                                if (!r || !r.data || !r.data.ActionSucceeded) {
                                    // TODO: Display appropriate error messaging to the UI
                                    console.log(r.data);
                                    return;
                                }
                                Object.keys(r.data.Result).forEach(id => {
                                    const product = _.find(this.productPaging.dataUnpaged, x => x.ID === Number(id));
                                    if (!product) { return; }
                                    if (!r.data.Result[id].IsValid) {
                                        // TODO: Display appropriate error messaging to the UI
                                        console.warn(`The price for ${id} was not valid`);
                                        return;
                                    }
                                    product["readPrices"] = (): { base: number, sale?: number } => {
                                        return {
                                            base: r.data.Result[id].BasePrice,
                                            sale: r.data.Result[id].SalePrice,
                                        };
                                    };
                                });
                            });
                        });
                    // If we have a user, pull the shopping carts they have so we can take over
                    if (!this.lookupKey.UID) {
                        resolve(true);
                        return;
                    }
                    this.getUsersCarts().then(() => resolve(true)).catch(reject);
                });
            });
        }
        getUsersCarts(): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminGetCartsForUser(this.lookupKey.UID).then(r => {
                    this.userCarts = r.data.filter(x => x.TypeName.indexOf("Target") === -1);
                    resolve();
                }).catch(reject);
            });
        }
        removeCartItem(itemID: number): void {
            this.cvApi.shopping.AdminRemoveCartItemByIDForUser(itemID).then(() => {
                this.loadUserCart().then(() => { /* Do Nothing */ });
            });
        }
        reset_CartLineItems(): ng.IPromise<boolean> {
            // Clear the cart selection
            this.lookupKey.ID = null;
            this.currentCart = null;
            // Clear the list of carts so they can be pulled again
            this.userCarts = [];
            // Clear the filter in the product pager
            this.productPaging.quickFilter = null;
            return this.$q.resolve(true);
        }
        submit_CartLineItems(): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.updateCart()
                    .then(() => resolve(true))
                    .catch(reject);
            });
        }
        continueWithCart(): void {
            if (!this.currentCart) {
                return;
            }
            this.submit_CartLineItems().then(() => {
                this.cartPicked = true;
                this.showPurchase = true;
            });
        }
        /**
         * Used by UI to shorthand
         * @param {string} name
         * @returns {number}
         * @memberof SalesOrderNewWizardController
         */
        protected getModifiedValueByName(name: string): number {
            var altName = name === "Tax" ? "Taxes" : name;
            return this.getModifiedValue(
                this.currentCart.Totals[name],
                this.currentCart["Subtotal" + altName + "Modifier"],
                this.currentCart["Subtotal" + altName + "ModifierMode"]
            );
        }
        /**
         * Used by UI to shorthand
         * @param {string} name
         * @returns {number}
         * @memberof SalesOrderNewWizardController
         */
        protected getModifierMinByName(name: string): number {
            var altName = name === "Tax" ? "Taxes" : name;
            return this.currentCart["Subtotal" + altName + "ModifierMode"] == 1
                ? 0
                : this.currentCart["Subtotal" + altName + "ModifierMode"] == 2
                    ? -99999999
                    : this.currentCart["Subtotal" + altName + "ModifierMode"] == 3
                        ? -100
                        : 0;
        }
        /**
         * Used by UI to shorthand
         * @param {string} name
         * @returns {number}
         * @memberof SalesOrderNewWizardController
         */
        protected getModifierMaxByName(name: string): number {
            var altName = name === "Tax" ? "Taxes" : name;
            return this.currentCart["Subtotal" + altName + "ModifierMode"] == 1
                ? 99999999
                : this.currentCart["Subtotal" + altName + "ModifierMode"] == 2
                    ? 99999999
                    : this.currentCart["Subtotal" + altName + "ModifierMode"] == 3
                        ? 100000
                        : 99999999;
        }
        /**
         * Used by UI to shorthand
         * @param {string} name
         * @returns {number}
         * @memberof SalesOrderNewWizardController
         */
        protected itemSoldPriceModifierMin(item: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>): number {
            return item.UnitSoldPriceModifierMode == 1
                ? 0
                : item.UnitSoldPriceModifierMode == 2
                    ? -99999999
                    : item.UnitSoldPriceModifierMode == 3
                        ? -100
                        : 0;
        }
        /**
         * Used by UI to shorthand
         * @param {string} name
         * @returns {number}
         * @memberof SalesOrderNewWizardController
         */
        protected itemSoldPriceModifierMax(item: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>): number {
            return item.UnitSoldPriceModifierMode == 1
                ? 99999999
                : item.UnitSoldPriceModifierMode == 2
                    ? 99999999
                    : item.UnitSoldPriceModifierMode == 3
                        ? 100000
                        : 99999999;
        }
        /**
         * Used by the Product Pager to add items to the cart. Note that this is in-memory
         * only, it does not push the data to the server like the add to cart in storefront
         * does. This allows modifications to the line item before running an Update Cart
         * call.
         * @protected
         * @param {number} id
         * @returns {void}
         * @memberof SalesOrderNewWizardController
         */
        protected addProduct(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.productPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            // If already in the collection, increase the quantity
            // TODO: Add a modal for input a specific quantity to add and the optional ForceUniqueLineItemKey
            const currentItem = _.find(this.currentCart.SalesItems, v => v.ProductID === id);
            if (currentItem) {
                currentItem.Quantity++;
                this.updateCurrentCartTotals();
                return;
            }
            // Add it
            const newSalesItem: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel> = {
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                // Sales Item Properties
                MasterID: 0,
                MasterKey: null,
                ItemType: null,
                // Product
                ProductID: model.ID,
                Sku: model.CustomKey,
                Name: model.Name,
                Description: model.ShortDescription || model.Description,
                ProductName: model.Name,
                ProductDescription: model.ShortDescription || model.Description,
                ProductKey: model.CustomKey,
                ProductSeoUrl: model.SeoUrl,
                ForceUniqueLineItemKey: null, // TODO
                //
                Quantity: 1,
                QuantityBackOrdered: 0,
                QuantityPreSold: 0,
                UnitSoldPriceModifierMode: 2,
                UnitSoldPriceModifier: 0,
                UnitCorePrice: 0,
                ExtendedPrice: 0,
            };
            if (angular.isFunction(model["readPrices"])) {
                const prices = model["readPrices"]();
                newSalesItem.UnitCorePrice = prices.base;
                newSalesItem.UnitSoldPrice
                    = newSalesItem.ExtendedPrice
                    = prices.sale || prices.base;
            }
            this.currentCart.SalesItems.push(newSalesItem);
            this.updateCurrentCartTotals();
            this.forms["CartLineItems"].$setDirty();
        }
        private createNewCart(): void {
            this.currentCart = this.fixCart(<api.CartModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                CustomKey: `New Cart for User ${this.lookupKey.UID || 'Unassigned'} Account ${this.lookupKey.AID || 'Unassigned'}`,
                SessionID: api.Guid.create(),

                NothingToShip: false,
                RequestedShipDate: null,
                SubtotalShippingModifier: null,
                SubtotalShippingModifierMode: null,
                SubtotalTaxesModifier: null,
                SubtotalTaxesModifierMode: null,
                SubtotalFeesModifier: null,
                SubtotalFeesModifierMode: null,
                SubtotalHandlingModifier: null,
                SubtotalHandlingModifierMode: null,
                SubtotalDiscountsModifier: null,
                SubtotalDiscountsModifierMode: null,
                RateQuotes: [],
                ItemDiscounts: [],

                Contacts: [],
                SalesItems: [],
                Discounts: [],

                StoredFiles: [],

                TypeID: 0,
                Type: null,
                TypeKey: "Cart",
                TypeName: null,
                TypeDisplayName: null,
                TypeSortOrder: null,

                StatusID: 0,
                Status: null,
                StatusKey: "New",
                StatusName: null,
                StatusDisplayName: null,
                StatusSortOrder: null,

                StateID: 0,
                State: null,
                StateKey: "WORK",
                StateName: null,
                StateDisplayName: null,
                StateSortOrder: null,

                ItemQuantity: 0,

                StoreID: 0,
                Store: null,
                StoreKey: null,
                StoreName: null,
                StoreSeoUrl: null,

                AccountID: 0,
                Account: null,
                AccountKey: null,
                AccountName: null,

                UserID: 0,
                User: null,
                UserKey: null,
                UserName: null,
                UserUserName: null,
                UserContactFirstName: null,
                UserContactLastName: null,
                UserContactEmail: null,

                BalanceDue: null,
                ShippingSameAsBilling: null,
                BillingContact: null,
                ShippingContact: null,

                Totals: <api.CartTotals>{
                    Discounts: 0,
                    Shipping: 0,
                    Tax: 0,
                    Fees: 0,
                    Handling: 0,
                    SubTotal: 0,
                    Total: 0
                },

                ShippingDetail: null,
                ShipmentName: null
            });
        }
        private loadUserCart(): ng.IPromise<void> {
            if (!this.lookupKey.ID || (!this.lookupKey.UID && (!this.user || !this.user.ID))) {
                return this.$q.reject();
            }
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminGetUserCartByID({
                    ID: this.lookupKey.ID,
                    UserID: this.lookupKey.UID,
                }).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        console.log(r && r.data);
                        return;
                    }
                    this.currentCart = this.fixCart(r.data.Result);
                    resolve(this.updateCurrentCartTotals());
                }).catch(reject);
            });
        }
        protected updateCart(): ng.IPromise<void> {
            // Validate the Cart
            // TODO@JTG: Error message that you can't update a null cart
            if (!this.currentCart) {
                return this.$q.reject("can't update a null cart");
            }
            // TODO@JTG: Error message that you can't set a cart to no items
            if (!this.currentCart.SalesItems || this.currentCart.SalesItems.length <= 0) {
                return this.$q.reject("can't set a cart to no items");
            }
            // Apply the selections from the Wizard
            this.currentCart.AccountID = this.lookupKey.AID;
            this.currentCart.UserID = this.lookupKey.UID || this.user && this.user.ID;
            // TODO@JTG: Error message that you can't set a cart without a user
            if (!this.currentCart.UserID) {
                return this.$q.reject("can't set a cart without a user");
            }
            // Push the Updated data
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminUpsertCartForUser({
                    ID: this.currentCart.UserID,
                    Cart: this.currentCart
                }).then(r => {
                    if (!r.data || !r.data.ActionSucceeded) {
                        // TODO@JTG: Error message that it didn't update correctly
                        reject("Didn't update correctly");
                        return;
                    }
                    this.lookupKey.ID = r.data.Result;
                    this.getUsersCarts().then(() => { /* Do nothing */ });
                    this.loadUserCart()
                        .then(resolve)
                        .catch(reject);
                }).catch(reject);
            });
        }
        private updateCurrentCartTotals(): ng.IPromise<void> {
            const totals = <api.CartTotals>{
                SubTotal: 0,
                Shipping: this.currentCart.Totals.Shipping,
                Discounts: this.currentCart.Totals.Discounts,
                Fees: this.currentCart.Totals.Fees,
                Handling: this.currentCart.Totals.Handling,
                Tax: this.currentCart.Totals.Tax,
                Total: 0,
            };
            this.currentCart.SalesItems.forEach(item => {
                item.UnitSoldPrice = Number(this.getModifiedValue(
                    item.UnitCorePrice, item.UnitSoldPriceModifier, item.UnitSoldPriceModifierMode).toFixed(2));
                item.ExtendedPrice = Number((item.UnitSoldPrice * (item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0))).toFixed(2));
                totals.SubTotal += item.ExtendedPrice;
            });
            totals.SubTotal = Number(totals.SubTotal.toFixed(2));
            const pretotal = totals.SubTotal
                + this.getModifiedValue(totals.Fees,      this.currentCart.SubtotalFeesModifier,      this.currentCart.SubtotalFeesModifierMode)
                + this.getModifiedValue(totals.Shipping,  this.currentCart.SubtotalShippingModifier,  this.currentCart.SubtotalShippingModifierMode)
                + this.getModifiedValue(totals.Handling,  this.currentCart.SubtotalHandlingModifier,  this.currentCart.SubtotalHandlingModifierMode)
                + this.getModifiedValue(totals.Tax,       this.currentCart.SubtotalTaxesModifier,     this.currentCart.SubtotalTaxesModifierMode)
                - this.getModifiedValue(totals.Discounts, this.currentCart.SubtotalDiscountsModifier, this.currentCart.SubtotalDiscountsModifierMode);
            totals.Total = Number(pretotal.toFixed(2));
            this.currentCart.Totals = totals;
            return this.$q.resolve();
        }
        private getModifiedValue(startingValue: number, modifier: number, mode: number): number {
            if (!mode) { mode = 1; }
            if (!modifier) { modifier = 0; }
            // 1 = Override Price
            // 2 = +/- Amount
            // 3 = +/- Percent from -100% to positive 100,000%
            switch (mode) {
                case 1: { return modifier; }
                case 2: { return startingValue + modifier; }
                case 3: { return startingValue * ((modifier + 100) / 100); }
            }
            return startingValue;
        }
        /**
         * Enforces modifier modes on all line items and for the totals where undefined
         * @private
         * @param {api.CartModel} cart
         * @returns {api.CartModel}
         * @memberof SalesOrderNewWizardController
         */
        private fixCart(cart: api.CartModel): api.CartModel {
            cart.SalesItems.forEach(item => {
                if (!item.UnitSoldPriceModifierMode) { item.UnitSoldPriceModifierMode = 2; } if (!item.UnitSoldPriceModifier) { item.UnitSoldPriceModifier = 0; }
            });
            if (!cart.SubtotalFeesModifierMode     ) { cart.SubtotalFeesModifierMode      = 2; } if (!cart.SubtotalFeesModifier     ) { cart.SubtotalFeesModifier      = 0; }
            if (!cart.SubtotalShippingModifierMode ) { cart.SubtotalShippingModifierMode  = 2; } if (!cart.SubtotalShippingModifier ) { cart.SubtotalShippingModifier  = 0; }
            if (!cart.SubtotalHandlingModifierMode ) { cart.SubtotalHandlingModifierMode  = 2; } if (!cart.SubtotalHandlingModifier ) { cart.SubtotalHandlingModifier  = 0; }
            if (!cart.SubtotalTaxesModifierMode    ) { cart.SubtotalTaxesModifierMode     = 2; } if (!cart.SubtotalTaxesModifier    ) { cart.SubtotalTaxesModifier     = 0; }
            if (!cart.SubtotalDiscountsModifierMode) { cart.SubtotalDiscountsModifierMode = 2; } if (!cart.SubtotalDiscountsModifier) { cart.SubtotalDiscountsModifier = 0; }
            return cart;
        }
        // Events
        onBrandChanged() {
            this.$timeout(() => {
                this.setRunning();
                this.$q.all([
                    this.loadFranchisesCollection(),
                    this.loadStoresCollection()
                ]).then(() => {
                    this.finishRunning();
                }).catch(reason => this.finishRunning(true, reason));
            }, 10);
        }
        onFranchiseChanged() {
            this.$timeout(() => {
                this.setRunning();
                this.$q.all([
                    this.loadStoresCollection()
                ]).then(() => {
                    this.finishRunning();
                }).catch(reason => this.finishRunning(true, reason));
            }, 10);
        }
        onStoreChanged() {
            // Do Nothing
        }
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $timeout: ng.ITimeoutService,
                private readonly $q: ng.IQService,
                private readonly $filter: ng.IFilterService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: admin.services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.loadCollections();
            this.setup_UserAndAccount();
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(this.unbindSearchComplete)) { this.unbindSearchComplete(); }
            });
        }
    }

    adminApp.directive("salesOrderCreate", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/salesOrderCreate.html", "ui"),
        controller: SalesOrderCreateController,
        controllerAs: "socCtrl"
    }));
}
