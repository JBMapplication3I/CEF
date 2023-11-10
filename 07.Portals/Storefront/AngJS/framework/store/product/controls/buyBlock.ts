module cef.store.product.controls {
    class ProductBuyBlockController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        productWhichIsTheMaster: api.ProductModel;
        productWhichIsTheCurrentVariant: api.ProductModel;
        productToDisplay: api.ProductModel;
        productIdForTheCurrentVariant: number;
        productVariants: [];
        quantityForAddToCart: number;
        usersSelectedStore: api.StoreModel;
        getSerializableAttributes: Function;
        masterController: ProductDetailsController;
        // Properties
        private _showMinAllowedToPurchaseMessage: boolean = undefined;
        private _showMinAllowedToPurchaseAfterMessage: boolean = undefined;
        private _showMaxAllowedToPurchaseMessage: boolean = undefined;
        private _showMaxAllowedToPurchaseAfterMessage: boolean = undefined;
        private _minAllowedToPurchase: number = undefined;
        private _minAllowedToPurchaseAfter: number = undefined;
        private _maxAllowedToPurchase: number = undefined;
        private _maxAllowedToPurchaseAfter: number = undefined;
        private accessibleAccounts: Array<api.AccountModel> = [];
        private selectedAccount: api.AccountModel;
        private selectedAccountKey: string = null;
        private readonly cookieName = "cefSelectedAffiliateAccountKey";
        get currentAccount(): api.AccountModel { return this.cvAuthenticationService["currentAccount"] };
        get isSupervisor(): boolean { return this.cvSecurityService.hasRole("Supervisor") };
        // Functions
        private clearMinMaxAllowedToPurchase(): void {
            this._showMinAllowedToPurchaseMessage = undefined;
            this._showMinAllowedToPurchaseAfterMessage = undefined;
            this._showMaxAllowedToPurchaseMessage = undefined;
            this._showMaxAllowedToPurchaseAfterMessage = undefined;
            this._minAllowedToPurchase = undefined;
            this._minAllowedToPurchaseAfter = undefined;
            this._maxAllowedToPurchase = undefined;
            this._maxAllowedToPurchaseAfter = undefined;
        }
        disableAddButtons(): boolean {
            return this.viewState.running
                || this.forms.buy.$invalid
                || this.productToDisplay.IsDiscontinued
        }
        showOutOfStockButton(): boolean {
            return this.productToDisplay.readInventory().IsOutOfStock
                && !this.productToDisplay.readInventory().AllowBackOrder;
        }
        showQuoteCartButton(): boolean {
            return this.cefConfig.featureSet.salesQuotes.useQuoteCart;
        }
        showRestrictions(): boolean {
            return this.showMinAllowedToPurchaseMessage()
                || this.showMinAllowedToPurchaseAfterMessage()
                || this.showMaxAllowedToPurchaseMessage()
                || this.showMaxAllowedToPurchaseAfterMessage()
                || this.showMissingRolesAltMessage();
        }
        showMinAllowedToPurchaseMessage(): boolean {
            if (!angular.isUndefined(this._showMinAllowedToPurchaseMessage)) {
                return this._showMinAllowedToPurchaseMessage;
            }
            const min = this.calculateMinAllowedPurchase(this.productToDisplay);
            const minAfter = this.calculateMinAllowedPurchaseAfter(this.productToDisplay);
            return this._showMinAllowedToPurchaseMessage = min > 1 && (minAfter === 0 || min === minAfter)
        }
        showMinAllowedToPurchaseAfterMessage(): boolean {
            if (!angular.isUndefined(this._showMinAllowedToPurchaseAfterMessage)) {
                return this._showMinAllowedToPurchaseAfterMessage;
            }
            const min = this.calculateMinAllowedPurchase(this.productToDisplay);
            const minAfter = this.calculateMinAllowedPurchaseAfter(this.productToDisplay);
            return this._showMinAllowedToPurchaseAfterMessage = min > 1 && (minAfter != 0 && min != minAfter);
        }
        showMaxAllowedToPurchaseMessage(): boolean {
            if (!angular.isUndefined(this._showMaxAllowedToPurchaseMessage)) {
                return this._showMaxAllowedToPurchaseMessage;
            }
            const max = this.calculateMaxAllowedPurchase(this.productToDisplay);
            const maxAfter = this.calculateMaxAllowedPurchaseAfter(this.productToDisplay);
            return this._showMaxAllowedToPurchaseMessage = max < 9999999 && (maxAfter == 0 || max == maxAfter)
        }
        showMaxAllowedToPurchaseAfterMessage(): boolean {
            if (!angular.isUndefined(this._showMaxAllowedToPurchaseAfterMessage)) {
                return this._showMaxAllowedToPurchaseAfterMessage;
            }
            const max = this.calculateMaxAllowedPurchase(this.productToDisplay);
            const maxAfter = this.calculateMaxAllowedPurchaseAfter(this.productToDisplay);
            return this._showMaxAllowedToPurchaseAfterMessage = max < 9999999 && (maxAfter != 0 && max != maxAfter)
        }
        calculateMinAllowedPurchase(p: api.ProductModel = this.productToDisplay): number {
            if (p == null) {
                return 1;
            }
            if (!angular.isUndefined(this._minAllowedToPurchase)) {
                return this._minAllowedToPurchase;
            }
            return this._minAllowedToPurchase = p.IsDiscontinued
                ? 0
                : p.MinimumPurchaseQuantity == null
                    ? 1
                    : p.MinimumPurchaseQuantity;
        }
        calculateMinAllowedPurchaseAfter(p: api.ProductModel = this.productToDisplay): number {
            if (p == null) {
                return 1;
            }
            if (!angular.isUndefined(this._minAllowedToPurchaseAfter)) {
                return this._minAllowedToPurchaseAfter;
            }
            return this._minAllowedToPurchaseAfter = p.IsDiscontinued
                ? 0
                : p.MinimumPurchaseQuantityIfPastPurchased == null
                    ? 1
                    : p.MinimumPurchaseQuantityIfPastPurchased;
        }
        calculateMaxAllowedPurchase(p: api.ProductModel = this.productToDisplay): number {
            if (p == null) {
                return 9999999;
            }
            if (!angular.isUndefined(this._maxAllowedToPurchase)) {
                return this._maxAllowedToPurchase;
            }
            return this._maxAllowedToPurchase = p.IsDiscontinued
                ? 0
                : p.MaximumPurchaseQuantity == null
                    ? 9999999
                    : p.MaximumPurchaseQuantity;
        }
        calculateMaxAllowedPurchaseAfter(p: api.ProductModel = this.productToDisplay): number {
            if (p == null) {
                return 9999999;
            }
            if (!angular.isUndefined(this._maxAllowedToPurchaseAfter)) {
                return this._maxAllowedToPurchaseAfter;
            }
            return this._maxAllowedToPurchaseAfter = p.IsDiscontinued
                ? 0
                : p.MaximumPurchaseQuantityIfPastPurchased == null
                    ? 9999999
                    : p.MaximumPurchaseQuantityIfPastPurchased;
        }
        showMissingRolesAltMessage(): boolean {
            return this.productToDisplay
                && this.productToDisplay.RequiresRolesListAlt
                && this.productToDisplay.RequiresRolesListAlt.length
                && _.some(this.productToDisplay.RequiresRolesListAlt,
                        x => !this.cvSecurityService.hasRole(x));
        }
        addCurrentItemToQuoteCart(quantity: number, shipOption?: any, item?: any): ng.IPromise<void> {
            return this.addCurrentItemToCartInner(
                this.cvServiceStrings.carts.types.quote,
                quantity,
                shipOption,
                item);
        }
        addCurrentItemToCart(quantity: number, shipOption?: any, item?: any): ng.IPromise<void> {
            return this.addCurrentItemToCartInner(
                this.cvServiceStrings.carts.types.cart,
                quantity,
                shipOption,
                item);
        }
        private addCurrentItemToCartInner(type: string, quantity: number, shipOption?: any, item?: any): ng.IPromise<void> {
            // Check for Discontinued first
            if (this.productToDisplay.IsDiscontinued) {
                return this.$q.reject();
            }
            this.loadSelectedAccountKey();
            let licenseRequired = this.productToDisplay.DocumentRequiredForPurchase === "1"
                || this.productToDisplay.DocumentRequiredForPurchase?.toLocaleLowerCase() === "true";
            if (this.selectedAccountKey) {
                this.selectedAccount = this.accessibleAccounts.find(x => x.CustomKey === this.selectedAccountKey);
            }
            if (licenseRequired && !this.selectedAccount?.MedicalLicenseNumber) {
                const translation = this.$translate.instant("ui.storefront.medicalLicenseRequired");
                this.cvMessageModalFactory(`${this.selectedAccount ? this.selectedAccount.Name : 'Current'} ${translation}`);
                return this.$q.reject();
            }
            if (licenseRequired && !this.currentAccount?.MedicalLicenseNumber && !this.selectedAccount) {
                const translation = this.$translate.instant("ui.storefront.medicalLicenseRequired");
                this.cvMessageModalFactory(`${this.currentAccount.Name} ${translation}`);
                return this.$q.reject();
            }
            this.setRunning(this.$translate("ui.storefront.common.AddingToCart.Ellipses"));
            let cartProduct = item;
            // TODO: This was a client-specific workflow that may longer supported in this manner, suggest review and remove
            if (shipOption && this.productWhichIsTheCurrentVariant) {
                cartProduct = shipOption === this.cvServiceStrings.attributes.inStorePickup
                    ? this.productWhichIsTheMaster.ProductAssociations[1].Slave
                    : this.productWhichIsTheCurrentVariant;
            }
            return this.$q((resolve, reject) => {
                let params = <services.IAddCartItemParams>{
                    storeID: this.usersSelectedStore ? this.usersSelectedStore.ID : null,
                    SerializableAttributes: this.getSerializableAttributes(),
                    selectedShipOption: shipOption,
                    ForceUniqueLineItemKey: this.productToDisplay.CustomKey,
                }
                let obj: api.SerializableAttributesDictionary = {
                    "SelectedUOM": {
                        ID: 1,
                        Key: "SelectedUOM",
                        Value: item.UnitOfMeasure
                    },
                    "SoldPrice": {
                        ID: 1,
                        Key: "SoldPrice",
                        Value: item["$_rawPricesUOMs"][item.UnitOfMeasure]["SalePrice"]
                    },
                }
                params.SerializableAttributes = Object.assign(obj);
                params.ForceUniqueLineItemKey = item.CustomKey + item.UnitOfMeasure;
                this.cvCartService.requireLoginForSessionCart(
                    this.productWhichIsTheCurrentVariant
                        ? this.productWhichIsTheCurrentVariant.ID
                        : cartProduct.ID,
                    type,
                    true,
                    quantity,
                    params,
                    cartProduct
                ).then(() => {
                    this.finishRunning();
                    resolve();
                }).catch(reason => { reject(reason); this.finishRunning(true, reason); });
            });
        }
        // Events
        variantChanged(): void {
            this.$rootScope.$broadcast(this.cvServiceStrings.events.products.selectedVariantChanged,
                this.productIdForTheCurrentVariant);
        }
        private loadSelectedAccountKey(): void {
            const value = this.$cookies.get(this.cookieName);
            this.selectedAccountKey = value ? value : null;
        }
        private loadAccounts(): void {
            this.cvSecurityService.hasRolePromise("CEF Affiliate Administrator").then(hasRole => {
                if (!hasRole) {
                    return;
                }
                this.cvApi.accounts.GetAccountsForCurrentAccount({
                    Active: true,
                    AsListing: true
                }).then(r => {
                    if (!r || !r.data) {
                        return;
                    }
                    this.accessibleAccounts = r.data.Results;
                });
            });
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSecurityService: services.ISecurityService,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvCartService: services.ICartService,
                private readonly cvMessageModalFactory: modals.IMessageModalFactory,
                private readonly $cookies: ng.cookies.ICookiesService,
                private readonly cvApi: api.ICEFAPI,) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.products.detailProductLoaded,
                () => this.clearMinMaxAllowedToPurchase());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
            this.loadAccounts();
        }
    }

    cefApp.directive("cefProductBuyBlock", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            productToDisplay: "=",
            productWhichIsTheMaster: "=",
            productWhichIsTheCurrentVariant: "=",
            productIdForTheCurrentVariant: "=",
            productVariants: "=",
            quantityForAddToCart: "=",
            getSerializableAttributes: "&",
            showQuantitySelector: "=?",
            masterController: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/buyBlock.html", "ui"),
        controller: ProductBuyBlockController,
        controllerAs: "pbbCtrl",
        bindToController: true
    }));
}
