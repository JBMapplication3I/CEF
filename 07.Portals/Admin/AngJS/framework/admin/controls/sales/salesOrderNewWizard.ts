/**
 * @file framework/admin/controls/sales/salesOrderNewWizard.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc The New Sales Order wizard for Administrators
 */
module cef.admin.controls.sales {
    interface IStepDefinition {
        key: string;
        icon: string;
        titleKey: string;
        title?: string;
        form: () => ng.IFormController;
        beforePreviousGo?: () => ng.IPromise<boolean>;
        start?: () => ng.IPromise<boolean>;
        reset?: () => ng.IPromise<boolean>;
        submit?: () => ng.IPromise<boolean>;
        setupComplete?: boolean;
        submitted?: boolean;
        started?: boolean;
    }

    export class SalesOrderNewWizardController extends core.TemplatedControllerBase {
        // All Steps
        currentStep: number = -1;
        private stepDefinitions: IStepDefinition[] = [];
        forms = {
            "UserAndAccount": <ng.IFormController>{},
            "CartLineItems": <ng.IFormController>{},
            "ShippingInfo": <ng.IFormController>{},
            "BillingInfo": <ng.IFormController>{},
            "Notes": <ng.IFormController>{},
            "Attributes": <ng.IFormController>{},
            "Payment": <ng.IFormController>{},
            "Confirmation": <ng.IFormController>{}
        };
        allowWallet: boolean = false;
        showSplitShipping: boolean = true;
        unbindPagingSearchComplete: Function;
        unbindShippingRateQuoteSelected: Function;
        unbindLoadShippingRateQuotesComplete: Function;

        setupSteps(): void {
            this.setRunning();
            this.allowWallet = this.cefConfig.featureSet.payments.wallet.enabled || false;
            this.showSplitShipping = this.cefConfig.featureSet.shipping.splitShipping.enabled || false;
            this.stepDefinitions = [];
            // Step 1: User/Account Selection
            this.stepDefinitions.push(<IStepDefinition>{
                key: "UserAndAccount",
                icon: "far fa-users",
                titleKey: "ui.admin.controls.sales.salesOrderNewWizard.UserAndAccount",
                title: "User/Account",
                form: () => this.forms.UserAndAccount,
                start: () => this.setup_UserAndAccount(),
                reset: () => this.reset_UserAndAccount(),
                submit: () => this.submit_UserAndAccount()
            });
            // Step 2: Cart and Items Selection
            this.stepDefinitions.push(<IStepDefinition>{
                key: "CartLineItems",
                icon: "far fa-shopping-cart",
                titleKey: "ui.admin.controls.sales.salesOrderNewWizard.CartLineItems",
                title: "Cart Line Items",
                form: () => this.forms.CartLineItems,
                start: () => this.setup_CartLineItems(),
                reset: () => this.reset_CartLineItems(),
                submit: () => this.submit_CartLineItems()
            });
            // Step 3: Billing Information
            this.stepDefinitions.push(<IStepDefinition>{
                key: "BillingInfo",
                icon: "far fa-usd",
                titleKey: "ui.admin.controls.sales.salesOrderNewWizard.BillingInfo",
                title: "Billing Info",
                form: () => this.forms.BillingInfo,
                start: () => this.setup_BillingInfo(),
                reset: () => this.reset_BillingInfo(),
                submit: () => this.submit_BillingInfo()
            });
            // Step 4: Shipping Information
            this.stepDefinitions.push(<IStepDefinition>{
                key: "ShippingInfo",
                icon: "far fa-truck",
                titleKey: "ui.admin.controls.sales.salesOrderNewWizard.ShippingInfo",
                title: "Shipping Info",
                form: () => this.forms.ShippingInfo,
                start: () => this.setup_ShippingInfo(),
                reset: () => this.reset_ShippingInfo(),
                submit: () => this.submit_ShippingInfo()
            });
            // Step 5: Notes
            this.stepDefinitions.push(<IStepDefinition>{
                key: "Notes",
                icon: "far fa-sticky-note",
                titleKey: "ui.admin.common.Note.Plural",
                title: "Notes",
                form: () => this.forms.Notes,
                start: () => this.setup_Notes(),
                reset: () => this.reset_Notes(),
                submit: () => this.submit_Notes()
            });
            // Step 6: Attributes
            this.stepDefinitions.push(<IStepDefinition>{
                key: "Attributes",
                icon: "far fa-th-list",
                titleKey: "ui.admin.common.Attribute.Plural",
                title: "Attributes",
                form: () => this.forms.Attributes,
                start: () => this.setup_Attributes(),
                reset: () => this.reset_Attributes(),
                submit: () => this.submit_Attributes()
            });
            // Step 7: Payment
            this.stepDefinitions.push(<IStepDefinition>{
                key: "Payment",
                icon: "far fa-credit-card",
                titleKey: "ui.admin.common.Payment",
                title: "Payment",
                form: () => this.forms.Payment,
                start: () => this.setup_Payment(),
                reset: () => this.reset_Payment(),
                submit: () => this.submit_Payment()
            });
            // Step 8: Confirmation
            this.stepDefinitions.push(<IStepDefinition>{
                key: "Confirmation",
                icon: "far fa-check-square-o",
                titleKey: "ui.admin.common.Confirmation",
                title: "Confirmation",
                form: () => this.forms.Payment,
                start: () => this.setup_Confirmation(),
                reset: () => this.reset_Confirmation(),
                submit: () => this.submit_Confirmation()
            });
            this.nextStepGoB(); // Initiate/Start the first step, will eventually call finishRunning
        }
        validateCurrentStep(): boolean {
            return this.stepDefinitions[this.currentStep].form().$valid
                && this.currentStep < this.stepDefinitions.length;
        }
        resetStep(): void {
            this.setRunning();
            this.stepDefinitions[this.currentStep].reset()
                .then(r => this.finishRunning(!r),
                      result => this.finishRunning(true, result))
                .catch(reason => this.finishRunning(true, reason));
        }
        prevStepGo(): void {
            this.setRunning();
            if (this.stepDefinitions[this.currentStep]
                && angular.isFunction(this.stepDefinitions[this.currentStep].beforePreviousGo)) {
                this.stepDefinitions[this.currentStep].beforePreviousGo().then(r => {
                    if (!r) {
                        this.finishRunning(true);
                        return;
                    }
                    this.currentStep--;
                    this.finishRunning();
                }, result => this.finishRunning(true, result))
                .catch(reason => this.finishRunning(true, reason));
                return;
            }
            this.currentStep--;
            this.finishRunning();
        }
        nextStepGo(): void {
            this.setRunning();
            if (this.stepDefinitions[this.currentStep]
                && angular.isFunction(this.stepDefinitions[this.currentStep].submit)) {
                this.stepDefinitions[this.currentStep].submit().then(r => {
                    if (r) {
                        this.nextStepGoB(); // Will eventually call finishRunning
                        return;
                    }
                    this.finishRunning(true);
                }, result => this.finishRunning(true, result))
                .catch(reason => this.finishRunning(true, reason));
                return;
            }
            this.nextStepGoB(); // Will eventually call finishRunning
        }
        private nextStepGoB(): void {
            this.currentStep++;
            if (this.stepDefinitions
                && !this.stepDefinitions[this.currentStep].started
                && angular.isFunction(this.stepDefinitions[this.currentStep].start)) {
                this.stepDefinitions[this.currentStep].start()
                    .then(r => this.finishRunning(!r),
                          result => this.finishRunning(true, result))
                    .catch(reason => this.finishRunning(true, reason));
            }
        }

        // Pre-Step
        createEmptyAddress = (): api.AddressModel => <api.AddressModel>{
            ID: 0,
            CustomKey: null,
            Active: null,
            CreatedDate: new Date(),
            UpdatedDate: null,
            Name: null,
            Description: null,
            Phone: null,
            Phone2: null,
            Phone3: null,
            Fax: null,
            Email: null,
            Street1: null,
            Street2: null,
            Street3: null,
            City: null,
            PostalCode: null,
            IsBilling: null,
            IsPrimary: null,
            RegionID: 0,
            RegionName: null,
            Region: null,
            CountryID: 1,
            CountryName: null,
            Country: null,
            Latitude: null,
            Longitude: null
        }
        createEmptyContactWithAddress = (): api.ContactModel => <api.ContactModel>{
            ID: 0,
            CustomKey: null,
            Active: true,
            CreatedDate: new Date(),
            UpdatedDate: null,
            SameAsBilling: false,
            Address: this.createEmptyAddress()
        }

        // Step 1: User/Account Step
        /**
         * The text entered into the Account typeahead
         * @private
         * @type {string}
         * @memberof SalesOrderNewWizardController
         */
        private accountToGrab: string = null;
        /**
         * The text entered into the User typeahead
         * @private
         * @type {string}
         * @memberof SalesOrderNewWizardController
         */
        private userToGrab: string = null;
        /**
         * The Accounts pulled in for the typeahead flyout based on the
         * {@see accountToGrab} as a search parameter
         * @private
         * @type {api.UserModel[]}
         * @memberof SalesOrderNewWizardController
         */
        private accounts: api.AccountModel[] = [];
        /**
         * The Users pulled in for the typeahead flyout based on the
         * {@see userToGrab} as a search parameter
         * @private
         * @type {api.UserModel[]}
         * @memberof SalesOrderNewWizardController
         */
        private users: api.UserModel[] = [];
        /**
         * The selected Account identifier
         * @private
         * @type {number}
         * @memberof SalesOrderNewWizardController
         */
        private accountID: number = null;
        /**
         * The selected User identifier
         * @private
         * @type {number}
         * @memberof SalesOrderNewWizardController
         */
        private userID: number = null;
        /**
         * The selected Account model as a result of picking the {@see accountID}
         * @private
         * @type {number}
         * @memberof SalesOrderNewWizardController
         */
        private account: api.AccountModel = null;
        /**
         * The selected User model as a result of picking the {@see userID}
         * @private
         * @type {api.UserModel}
         * @memberof SalesOrderNewWizardController
         */
        private user: api.UserModel = null;

        setup_UserAndAccount(): ng.IPromise<boolean> {
            // Add watches to accountID and userID in case they are updated from any direction
            this.$scope.$watch(() => this.accountID, (newVal: number, oldVal: number) => {
                if (newVal === oldVal) { return; }
                if (!newVal) {
                    this.accountID = null;
                    this.account = null;
                    return;
                }
                this.cvApi.accounts.GetAccountByID(newVal).then(r => {
                    this.account = r.data;
                });
            });
            this.$scope.$watch(() => this.userID, (newVal: number, oldVal: number) => {
                if (newVal === oldVal) { return; }
                if (!newVal) {
                    this.userID = null;
                    this.user = null;
                    return;
                }
                this.cvApi.contacts.GetUserByID(newVal).then(r => {
                    this.user = r.data;
                    this.accountID = this.user.AccountID;
                    if (!this.accountID) { return; }
                    this.accountToGrab = this.accountID.toString();
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
            this.userID = null;
            this.user = null;
            this.accountID = null;
            this.account = null;
            this.accountContacts = [];
            // Return
            return this.$q.resolve(true);
        }

        submit_UserAndAccount(): ng.IPromise<boolean> {
            // Do Nothing
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
                Paging: <api.Paging>{ Size: 50, StartIndex: 1 }
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
            const lookup = search.toLowerCase()
            return this.cvApi.contacts.GetUsers({
                Active: true,
                AsListing: true,
                AccountID: this.accountID,
                IDOrUserNameOrCustomKeyOrEmailOrContactName: search,
                Paging: <api.Paging>{ Size: 50, StartIndex: 1 }
            }).then(r => this.users = r.data.Results.filter(
                item => (item.CustomKey || "").indexOf(lookup) > -1
                     || (item.UserName || "").indexOf(lookup) > -1
                     || item.ID.toString().indexOf(lookup) > -1));
        }

        /**
         * The event handler for the Account typeahead which populates the {@see accountID}.
         * Used by the UI.
         * @protected
         * @param {*} $item - Unused, but required for positional parameter
         * @param {*} $model - The identifier to apply
         * @memberof SalesOrderNewWizardController
         */
        protected selectAccountFromTypeAhead($item, $model) {
            this.accountID = Number($model);
        }

        /**
         * The event handler for the User typeahead which populates the {@see userID}.
         * Used by the UI.
         * @protected
         * @param {*} $item - Unused, but required for positional parameter
         * @param {*} $model - The identifier to apply
         * @memberof SalesOrderNewWizardController
         */
        protected selectUserFromTypeAhead($item, $model) {
            this.userID = Number($model);
        }

        // Step 2: Cart Line Items Step
        currentCartID: number = null;
        currentCart: api.CartModel;
        userCarts: api.CartModel[] = [];
        productPaging: core.ServerSidePaging<api.ProductModel, api.ProductPagedResults>;
        subtotalModifierModes: Array<{ value: number, label: string }> = [];

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
                    this.unbindPagingSearchComplete = this.$scope.$on(this.cvServiceStrings.events.paging.searchComplete,
                        (event: ng.IAngularEvent, name: string, newResults: api.ProductModel[]) => {
                            if (name !== "products") { return; }
                            const productIDs = newResults.map(x => x.ID);
                            this.cvApi.pricing.GetPricesForProductsAsUser({
                                UserID: this.userID,
                                ProductIDs: productIDs
                            }).then(r => {
                                if (!r || !r.data || !r.data.ActionSucceeded) {
                                    // TODO: Display appropriate error messaging to the UI
                                    console.log(r.data);
                                    return;
                                }
                                Object.keys(r.data.Result).forEach(id => {
                                    var product = _.find(this.productPaging.dataUnpaged, x => x.ID === Number(id));
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
                                    }
                                });
                            });
                        });
                    // If we have a user, pull the shopping carts they have so we can take over
                    if (!this.userID) {
                        resolve(true);
                        return;
                    }
                    this.cvApi.shopping.AdminGetCartsForUser(this.userID).then(r => {
                        this.userCarts = r.data.filter(x => x.TypeName.indexOf("Target") === -1);
                        resolve(true);
                    }).catch(reject);
                });
            });
        }

        removeCartItem(itemID: number): void {
            this.cvApi.shopping.AdminRemoveCartItemByIDForUser(itemID).then(() => {
                this.loadUserCart().then(() => { /* Do Nothing */ });
            });
        }

        reset_CartLineItems(): ng.IPromise<boolean> {
            // Clear the cart selection
            this.currentCartID = null;
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
                ExtendedPrice: 0
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
                CustomKey: `New Cart for User ${this.userID || 'Unassigned'} Account ${this.accountID || 'Unassigned'}`,
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
            if (!this.currentCartID) {
                return this.$q.reject();
            }
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminGetUserCartByID({
                    ID: this.currentCartID,
                    UserID: this.userID,
                    AccountID: this.accountID,
                    StoreID: 0,
                    FranchiseID: 0,
                    BrandID: 0,
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
                return this.$q.reject();
            }
            // TODO@JTG: Error message that you can't set a cart to no items
            if (!this.currentCart.SalesItems || this.currentCart.SalesItems.length <= 0) {
                return this.$q.reject();
            }
            // Apply the selections from the Wizard
            this.currentCart.AccountID = this.accountID;
            this.currentCart.UserID = this.userID;
            // Push the Updated data
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminUpsertCartForUser({
                    ID: this.userID,
                    Cart: this.currentCart
                }).then(r => {
                    if (!r.data || !r.data.ActionSucceeded) {
                        // TODO@JTG: Error message that it didn't update correctly
                        reject();
                        return;
                    }
                    this.currentCartID = r.data.Result;
                    this.loadUserCart()
                        .then(() => resolve())
                        .catch(reject);
                });
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

        // Steps 3-4 (Shared parts)
        countries: api.CountryModel[] = [];
        accountContacts: api.AccountContactModel[] = [];

        protected propagateCountryChange(contact: api.ContactModel, billOrShip: boolean): void {
            contact.Address.CountryID = contact.Address.CountryID ? contact.Address.CountryID : null;
            if (contact.Address.CountryID == null) {
                contact.Address.Country = null;
                contact.Address.CountryKey = contact.Address.CountryCode = contact.Address.CountryName = null;
                return;
            }
            contact.Address.Country = _.find(this.countries, v => v.ID === contact.Address.CountryID);
            contact.Address.CountryKey = contact.Address.Country.CustomKey;
            contact.Address.CountryCode = contact.Address.Country.Code;
            contact.Address.CountryName = contact.Address.Country.Name;
            this.cvApi.geography.GetRegions({
                Active: true,
                AsListing: true,
                CountryID: contact.Address.CountryID
            }).then(r => {
                if (billOrShip) {
                    this.regionsForBilling = r.data.Results;
                } else {
                    this.regionsForShipping = r.data.Results;
                }
            });
        }

        protected propagateRegionChange(contact: api.ContactModel, billOrShip: boolean): void {
            contact.Address.RegionID = contact.Address.RegionID ? contact.Address.RegionID : null;
            if (contact.Address.RegionID == null) {
                contact.Address.Region = null;
                contact.Address.RegionKey = contact.Address.RegionCode = contact.Address.RegionName = null;
                return;
            }
            contact.Address.Region = _.find((billOrShip ? this.regionsForBilling : this.regionsForShipping), v => v.ID === contact.Address.RegionID);
            contact.Address.RegionKey = contact.Address.Region.CustomKey;
            contact.Address.RegionCode = contact.Address.Region.Code;
            contact.Address.RegionName = contact.Address.Region.Name;
        }

        private createEmptyAccountContactWithAddress = (): api.AccountContactModel => <api.AccountContactModel>{
            ID: 0,
            CustomKey: null,
            Active: true,
            CreatedDate: new Date(),
            UpdatedDate: null,
            IsPrimary: false,
            IsBilling: false,
            TransmittedToERP: false,
            AccountID: 0,
            Account: null,
            Name: null,
            Description: null,
            SlaveID: 0,
            Slave: this.createEmptyContactWithAddress()
        }

        // Step 3: Billing Info
        /**
         * When the billing Country ID is changed, regions will be pulled which
         * are tied to the specific country, if any
         * @protected
         * @type {api.RegionModel[]}
         * @memberof SalesOrderNewWizardController
         */
        protected regionsForBilling: api.RegionModel[] = [];
        /**
         * Whether or not the Edit Mode UI should be visible for billing address
         * @protected
         * @type {boolean}
         * @memberof SalesOrderNewWizardController
         */
        protected editBilling: boolean;
        /**
         * The Contact record which has either been selected from the address book
         * or entered as new via the Edit Mode UI
         * @protected
         * @type {api.ContactModel}
         * @memberof SalesOrderNewWizardController
         */
        protected selectedBilling: api.ContactModel;

        setup_BillingInfo(): ng.IPromise<boolean> {
            this.selectedBilling = this.currentCart.BillingContact;
            if (!this.selectedBilling) {
                this.selectedBilling = this.createEmptyContactWithAddress();
            }
            return this.$q((resolve, reject) => {
                this.cvApi.geography.GetRegions({ Active: true, AsListing: true }).then(r => {
                    this.regionsForBilling = r.data.Results;
                    if (!this.accountID) {
                        resolve(true);
                        return;
                    }
                    this.cvApi.geography.GetAddressBookAsAdmin(this.accountID).then(r => {
                        this.accountContacts = r.data;
                        resolve(true);
                    }).catch(reason2 => reject(reason2));
                }).catch(reject);
            });
        }

        reset_BillingInfo(): ng.IPromise<boolean> {
            return this.$q.resolve(true);
        }

        submit_BillingInfo(): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.AdminSetCartBillingContactForUser({
                    CartID: this.currentCartID,
                    BillingContact: this.selectedBilling
                }).then(() => resolve(true))
                .catch(reject);
            });
        }

        /**
         * Used by the UI
         * @protected
         * @param {api.ContactModel} value
         * @memberof SalesOrderNewWizardController
         */
        protected setBillingAddress(value: api.ContactModel): void {
            this.editBilling = value === null;
        }
        /*
        setAddress(value: api.ContactModel, type: string) {
            this[`selected${type}`] = value;
            this[`edit${type}`] = !value;
            if (!value || !value.Address || !value.Address.Country) {
                this[`selected${type}CountryName`] = undefined;
            } else {
                this[`selected${type}CountryName`] = value.Address.Country.Name;
                this.filterCountries(type);
                this.updateCartAddress(value, type);
            }
            if (type === "Shipping") {
                this.viewstate.haveRateQuotes = false;
            }
        }
        */

        // Step 4: Shipping Info
        /**
         * When the shipping Country ID is changed, regions will be pulled which
         * are tied to the specific country, if any.
         * @protected
         * @type {api.RegionModel[]}
         * @memberof SalesOrderNewWizardController
         */
        protected regionsForShipping: api.RegionModel[] = [];
        /**
         * Whether or not the Edit Mode UI should be visible for shipping address.
         * @protected
         * @type {boolean}
         * @memberof SalesOrderNewWizardController
         */
        protected editShipping: boolean;
        /**
         * The Contact record which has either been selected from the address book
         * or entered as new via the Edit Mode UI.
         * @protected
         * @type {api.ContactModel}
         * @memberof SalesOrderNewWizardController
         */
        protected selectedShipping: api.ContactModel;
        /**
         * Whether the Shipping Contact should be redirected to the Billing Contact's
         * data.
         * @protected
         * @type {boolean}
         * @memberof SalesOrderNewWizardController
         */
        protected isShippingSameAsBilling: boolean = false;
        /**
         * Whether we are currently do a rate request run.
         * @private
         * @type {boolean}
         * @memberof SalesOrderNewWizardController
         */
        private gettingRates: boolean = false;
        /**
         * Whether we have successfully received rate quotes from a request run.
         * @protected
         * @type {boolean}
         * @memberof SalesOrderNewWizardController
         */
        protected haveRateQuotes: boolean = false;
        /**
         * The rate quote from the list that has been selected.
         * @protected
         * @type {boolean}
         * @memberof SalesOrderNewWizardController
         */
        protected selectedRateQuoteID: number = null;
        /**
         * A "special instructions" message to the shipper.
         * @protected
         * @type {string}
         * @memberof SalesOrderNewWizardController
         */
        protected specialInstructions: string = null;

        setup_ShippingInfo(): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.$q.all([
                    this.cvApi.geography.GetCountries({ Active: true, AsListing: true }),
                    this.cvApi.geography.GetRegions({ Active: true, AsListing: true })
                ]).then((rarr: any[]) => {
                    this.countries = rarr[0].data.Results;
                    this.regionsForShipping = rarr[1].data.Results;
                    this.unbindLoadShippingRateQuotesComplete = this.$scope.$on(this.cvServiceStrings.events.shipping.loaded,
                        ($event, rateQuotes, selectedRateQuoteID) =>
                            this.onRefreshShippingRateQuotesComplete(rateQuotes, selectedRateQuoteID));
                    this.unbindShippingRateQuoteSelected = this.$scope.$on(this.cvServiceStrings.events.shipping.rateQuoteSelected,
                        ($event, type, selectedRateQuoteID) =>
                         this.onShippingRateQuoteSelected(type, selectedRateQuoteID));
                    if (!this.selectedShipping) {
                        this.selectedShipping = this.createEmptyContactWithAddress();
                    }
                    resolve(true);
                }).catch(reject);
            });
        }

        reset_ShippingInfo(): ng.IPromise<boolean> {
            this.selectedShipping = null;
            return this.$q((resolve, reject) => {
                this.updateShippingAddressSelection()
                    .then(() => resolve(true))
                    .catch(reject);
            });
        }

        submit_ShippingInfo(): ng.IPromise<boolean> {
            // Do Nothing, the estimator will have applied everything
            return this.$q.resolve(true);
        }

        protected updateShippingAddressSelection(): ng.IPromise<void> {
            if (this.isShippingSameAsBilling) {
                this.updateShippingFromBilling();
            }
            if (this.selectedShipping == null) {
                return this.$q.reject();
            }
            this.currentCart.ShippingSameAsBilling = this.isShippingSameAsBilling;
            this.currentCart.BillingContact = this.selectedBilling;
            if (this.currentCart.BillingContact.ID) {
                this.currentCart.BillingContactID = this.currentCart.BillingContact.ID;
            }
            this.currentCart.ShippingContact = this.selectedShipping;
            if (this.currentCart.ShippingContact.ID) {
                this.currentCart.ShippingContactID = this.currentCart.ShippingContact.ID;
            }
            return this.$q.resolve(
                this.cvApi.shopping.AdminUpdateUserCart(this.currentCart).then(() => {
                    if (this.gettingRates) { return; }
                    /* TODO: Not sure how reliable starting the set running here will be, the finishRunning would be fired by one or more closers
                    if (!this.viewState.running) {
                        this.setRunning();
                    }*/
                    this.gettingRates = true;
                    this.haveRateQuotes = false; // Reset this while we are getting data
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.ready, this.currentCart);
                    /* The rate quote manager widget will react by applying the updated shipping contact to the
                     * server, then it will get rate quotes. When that finishes, it will broadcast
                     * "loadShippingRateQuotesCompleted" which this controller will react to with a call to the
                     * "onRefreshShippingRatesComplete" function */
                })
            );
        }

        protected updateShippingFromBilling(): void {
            this.selectedShipping = this.isShippingSameAsBilling
                ? this.selectedBilling
                : this.selectedShipping;
        }

        // TODO: onRefreshShippingRatesFailed
        private onRefreshShippingRateQuotesComplete(rateQuotes: api.RateQuoteModel[], selectedRateQuoteID: number): void {
            this.selectedRateQuoteID = selectedRateQuoteID;
            this.haveRateQuotes = true;
            this.gettingRates = false;
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.setSelectedRateQuoteID);
            this.finishRunning();
        }

        private onShippingRateQuoteSelected(type: string, selectedRateQuoteID: number): void {
            if (type !== "Cart") { return; }
            this.selectedRateQuoteID = selectedRateQuoteID;
        }

        protected setShippingAddress(value: api.ContactModel): void {
            this.haveRateQuotes = false;
            this.selectedShipping = value;
            this.editShipping = value === null;
        }

        // Step 5: Notes
        setup_Notes(): ng.IPromise<boolean> {
            return this.$q.resolve(true);
        }

        reset_Notes(): ng.IPromise<boolean> {
            this.currentCart.Notes = [];
            return this.$q.resolve(true);
        }

        submit_Notes(): ng.IPromise<boolean> {
            return this.$q.resolve(true);
            ////return this.$q((resolve, reject) => {
            ////    this.updateCart()
            ////        .then(() => resolve(true),
            ////              result => reject(result))
            ////        .catch(reject);
            ////});
        }

        // Step 6: Attributes
        setup_Attributes(): ng.IPromise<boolean> {
            // Do Nothing
            return this.$q.resolve(true);
        }

        reset_Attributes(): ng.IPromise<boolean> {
            this.currentCart.SerializableAttributes = { };
            return this.$q.resolve(true);
        }

        submit_Attributes(): ng.IPromise<boolean> {
            return this.$q.resolve(true);
            ////return this.$q((resolve, reject) => {
            ////    this.updateCart()
            ////        .then(() => resolve(true),
            ////              result => reject(result))
            ////        .catch(reject);
            ////});
        }

        // Step 7: Payment Information Pane
        paymentMethod = null; // Only matters if you are using PayPal (value: 'easyPay') right now.
        paymentStyle = null; // For not capturing a payment, just how the customer intends to pay eventually
        paymentData = {
            WalletCardID: 0,
            CardType: null,
            ReferenceName: null,
            CardHolderName: null,
            CardNumber: null,
            CVV: null,
            ExpirationMonth: new Date().getMonth() + 1,
            ExpirationYear: new Date().getFullYear(),
            PONumber: null
        };
        expirationMonths: Array<{ Key: number, Value: string }>;
        expirationYears = [];
        paypal = { working: false, error: false, success: false, message: null }
        wallet: Array<api.WalletModel> = [];
        selectedCard: api.WalletModel;
        viewstate = { checkoutIsProcessing: false };
        checkoutResult: api.CheckoutResult;
        confirmationNumber = null;
        orderComplete = false;
        taxExemptionNumber: string = null;
        orderAttributes: api.SerializableAttributesDictionary = {};
        acceptedPayments: core.PaymentSection;

        setup_Payment(): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.acceptedPayments = this.cefConfig.checkout.paymentOptions;
                if (!_.some(this.acceptedPayments, true)) {
                    this.acceptedPayments.creditCard = true;
                }
                ((monthNames) => {
                    this.$q.all(monthNames.map(month => this.$translate(`ui.admin.common.months.${month}`))).then(monthResponses => {
                        this.expirationMonths = monthResponses.map((monthResponse: string, idx) => {
                            return { Key: (idx+1), Value: monthResponse };
                        });
                    }, () => {
                        console.warn("Failed to get month name translation values. Check the database."); // Here for a reason, not debugging
                        this.expirationMonths = monthNames.map((month, idx) => {
                            return { Key: (idx + 1), Value: month };
                        });
                    });
                })(["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]);
                this.expirationYears = [];
                const currentYear = new Date().getFullYear();
                for (let y = 0; y < 10; y++) {
                    this.expirationYears.push(currentYear + y);
                }
                this.cvApi.payments.GetUserWallet(this.userID)
                    .then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded) {
                            reject(r.data.Messages);
                            return;
                        }
                        this.wallet = r.data.Result;
                        resolve(true);
                    }, result => reject(result))
                    .catch(reject);
            });
        }

        reset_Payment(): ng.IPromise<boolean> {
            this.currentCart.SerializableAttributes = { };
            return this.$q.resolve(true);
        }

        submit_Payment(): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.updateCart()
                    .then(() => resolve(this.doPayment()),
                          result => reject(result))
                    .catch(reject);
            });
        }

        updatePaymentDataWithCard(card: api.WalletModel): void {
            if (card) {
                this.paymentData.WalletCardID = card.ID;
            } else {
                this.paymentData.WalletCardID = null;
            }
        }
        selectedCardIsFromWallet(): boolean {
            if (this.selectedCard == null) { return false; }
            return _.some(this.wallet, value => value.ID === this.selectedCard.ID);
        }
        doPayment(obj?): ng.IPromise<any> {
            this.viewstate.checkoutIsProcessing = true;
            this.setRunning(this.$translate("ui.admin.controls.sales.salesOrderNewWizard.SubmittingYourOrders.Ellipses"));
            return this.$q<any>((resolve, reject) => {
                if (this.selectedShipping == null && this.isShippingSameAsBilling) {
                    this.selectedShipping = this.selectedBilling;
                } else if (this.selectedShipping == null
                           && this.accountContacts != null
                           && this.accountContacts.length > 0) {
                    this.selectedShipping = this.accountContacts[0].Slave;
                }
                var fileNames = [];
                ////if (obj.checkoutObj != null && obj.checkoutObj.StoredFiles != null) {
                ////    for (var i = 0; i < obj.checkoutObj.StoredFiles.length; i++) {
                ////        fileNames.push(obj.checkoutObj.StoredFiles[i].Name);
                ////    }
                ////}
                var checkoutDto = <api.CheckoutModel>{
                    Active: true,
                    CreatedDate: new Date(),
                    CustomKey: null,
                    CartTypeName: "Cart",
                    CartID: this.currentCartID,
                    CardNumber: this.paymentData.CardNumber,
                    CVV: this.paymentData.CVV,
                    PurchaseOrder: this.paymentData.PONumber,
                    ExpirationMonth: this.paymentData.ExpirationMonth,
                    ExpirationYear: this.paymentData.ExpirationYear,
                    CardHolderName: this.paymentData.CardHolderName,
                    CardReferenceName: this.paymentData.ReferenceName,
                    ExternalUserID: this.user.CustomKey,
                    Billing: this.selectedBilling,
                    Shipping: this.selectedShipping,
                    IsNewAccount: false,
                    Username: null,
                    Password: null,
                    UserID: this.userID,
                    IsSameAsBilling: this.isShippingSameAsBilling
                        || (this.selectedShipping
                            && this.selectedShipping.ID
                            && this.selectedShipping.ID > 0
                            && this.selectedShipping.ID === this.selectedBilling.ID),
                    Country: null,
                    State: null,
                    ZipCode: null,
                    Date: null,
                    Delivery: null,
                    DeliveryType: null,
                    IsPartialPayment: null,
                    PaymentStyle: this.paymentStyle || this.paymentMethod,
                    PaymentGatewayProvider: null,
                    PaymentLogin: null,
                    TransactionKey: null,
                    TestMode: false,
                    Token: null,
                    CardType: null,
                    WalletID: this.paymentData.WalletCardID,
                    WalletToken: null,
                    PayoneerAccountID: 0,
                    PayoneerCustomerID: 0,
                    SpecialInstructions: this.specialInstructions,
                    TaxExemptionNumber: this.taxExemptionNumber,
                    FileNames: fileNames,
                    SerializableAttributes: this.orderAttributes
                };
                switch (this.paymentMethod) {
                    default: {
                        var promise: (routeParams: api.ProcessSpecificCartToSingleOrderDto |
                                                   api.ProcessSpecificCartToTargetOrdersDto)
                            => ng.IHttpPromise<api.CheckoutResult> = null;
                        if (!this.cefConfig.checkout) {
                            throw new Error("ERROR! No checkout config set up, please contact a Dev for support.");
                        }
                        if (this.cefConfig.featureSet.shipping.splitShipping.enabled) {
                            promise = this.cvApi.providers.ProcessSpecificCartToTargetOrders;
                        } else {
                            promise = this.cvApi.providers.ProcessSpecificCartToSingleOrder;
                        }
                        promise(checkoutDto).then(response => {
                            if (!response || !response.data || !response.data.Succeeded
                                || response.data.ErrorMessage && response.data.ErrorMessage.toLowerCase().indexOf("email") === -1
                                || response.data.PaymentTransactionID && response.data.PaymentTransactionID.indexOf("ERROR") !== -1
                            ) {
                                const message = response.data.ErrorMessage || response.data.PaymentTransactionID || "An unknown error occured.";
                                const error = response.data;
                                this.$uibModal.open({
                                    templateUrl: this.$filter("corsLink")("/framework/admin/controls/sales/modals/errorMessage.html", "ui"),
                                    controller: ($scope: ng.IScope) => {
                                        $scope.error = error;
                                        $scope.message = message;
                                    },
                                    resolve: {
                                        message: () => message,
                                        result: () => error
                                    }
                                });
                                if (!response.data.Succeeded) {
                                    this.viewstate.checkoutIsProcessing = false;
                                    this.finishRunning(true, message);
                                    reject(message);
                                    return;
                                }
                            }
                            this.checkoutResult = response.data;
                            this.confirmationNumber = response.data.PaymentTransactionID
                                || (response.data.PaymentTransactionIDs ? response.data.PaymentTransactionIDs.join(", ") : "");
                            this.orderComplete = true;
                            this.viewstate.checkoutIsProcessing = true;
                            resolve(true);
                            this.finishRunning();
                        }, result => { this.finishRunning(true, result); reject(result); })
                        .catch(reason => { this.finishRunning(true, reason); reject(reason); });
                    }
                }
            });
        }

        // Step 8: Confirmation
        setup_Confirmation(): ng.IPromise<boolean> {
            // Do Nothing
            return this.$q.resolve(true);
        }

        reset_Confirmation(): ng.IPromise<boolean> {
            // Do Nothing
            return this.$q.resolve(true);
        }

        submit_Confirmation(): ng.IPromise<boolean> {
            // Do Nothing
            return this.$q.resolve(true);
        }
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: admin.services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.setupSteps();
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(this.unbindPagingSearchComplete)) { this.unbindPagingSearchComplete(); }
                if (angular.isFunction(this.unbindLoadShippingRateQuotesComplete)) { this.unbindLoadShippingRateQuotesComplete(); }
                if (angular.isFunction(this.unbindShippingRateQuoteSelected)) { this.unbindShippingRateQuoteSelected(); }
            });
        }
    }

    adminApp.directive("salesOrderNewWizard", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/salesOrderNewWizard.html", "ui"),
        controller: SalesOrderNewWizardController,
        controllerAs: "salesOrderNewWizardCtrl"
    }));
}
