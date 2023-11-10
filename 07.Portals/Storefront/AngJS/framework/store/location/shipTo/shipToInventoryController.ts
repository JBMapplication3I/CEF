module cef.store.locations.shipTo {
    export class ShipToInventoryController extends core.TemplatedControllerBase {
        // Properties
        product: api.ProductModel; // Bound by Scope
        cartItems: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>; // Bound by Scope
        shipToValue: string; // Bound by Scope
        showShipToHomeWhenUnavailable: boolean; // Bound by Scope
        showShipToHomeWhenRestricted: boolean; // Bound by Scope
        showShipToHomeStock: boolean; // Bound by Scope
        showInStorePickupWhenUnavailable: boolean; // Bound by Scope
        showInStorePickupAsChooseStoreLinkWhenNoStore: boolean; // Bound by Scope
        showInStorePickupStock: boolean; // Bound by Scope
        showShipToStoreWhenUnavailable: boolean; // Bound by Scope
        showShipToStoreWhenInStorePickupIsAvailable: boolean; // Bound by Scope
        showShipToStoreStock: boolean; // Bound by Scope
        phonesAreNotLinks: boolean; // Bound by Scope
        condenseRadios: boolean; // Bound by Scope
        separateSkuFromRadios: boolean; // Bound by Scope
        includeNameLinkBlock: boolean; // Bound by Scope
        includeModelSkuBlock: boolean; // Bound by Scope
        includeAddressBlock: boolean; // Bound by Scope
        includeBuyBlock: boolean; // Bound by Scope
        unique: string; // Bound by Scope
        nameLimit: number; // Bound by Scope
        editable: boolean; // Bound by Scope
        debug: boolean; // Bound by Scope
        newProduct: api.ProductModel;
        viewstate = { checkStore: false, checkoutIsProcessing: false, productIsProcessing: false };
        hasStore: boolean;
        store: api.StoreModel;
        unitProduct: api.ProductModel;
        packProduct: api.ProductModel;
        productPrice: api.CalculatedPrices;
        unitPrice: api.CalculatedPrices;
        packPrice: api.CalculatedPrices;
        noUnitInventory: boolean;
        noPackInventory: boolean;
        authenticated: boolean;
        address: api.AccountContactModel;
        quantity = 1;
        // ================================================================
        // Note: All of the below functions MUST remain an arrow functions
        // ================================================================
        private processLoginChange = (): void => {
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    this.authenticated = false;
                    this.address = null;
                    return;
                }
                this.authenticated = true;
                if (!this.includeAddressBlock) { return; }
                // Load the user's primary shipping address to the UI
                this.cvApi.geography.GetCurrentAccountPrimaryShippingAddress().then(r => {
                    this.address = r && r.data || null;
                });
            });
        }
        checkStore = (): void => {
            this.viewstate.checkStore = !this.viewstate.checkStore;
        }
        private initialShipToValue = (): string => {
            // Sort By Cheapest Available Option to First:
            // * InStorePickup = no shipping for customer, no shipping for company, also single items instead of packs
            // * ShipToStore = no shipping for customer, shipping for company, packs
            // * ShipToHome = shipping for customer, no shipping for company, packs
            if (this.hasStore && this.haveInStorePickupStock) {
                return this.cvServiceStrings.attributes.inStorePickup;
            }
            if (this.hasStore && this.haveShipToStoreStock) {
                return this.cvServiceStrings.attributes.shipToStore;
            }
            if (/*!this.isShippingRestricted &&*/ this.haveShipToHomeStock) {
                return this.cvServiceStrings.attributes.shipToHome;
            }
            // TODO: Determine the true fall-back value
            if (this.hasStore /*&& this.isShippingRestricted*/) {
                return this.cvServiceStrings.attributes.shipToStore;
            }
            return this.cvServiceStrings.attributes.shipToHome;
        }
        private setInitialShipToValue = (): void => {
            if (!this.editable) { return; }
            this.shipToValue = this.initialShipToValue();
        }
        // TODO: Rework with 2020.4 inventory and brands/stores
        // private applyBreakablePackFlag = (product: api.ProductModel): void => {
        //     if (product.SerializableAttributes
        //         && product.SerializableAttributes["R"]
        //         && product.SerializableAttributes["R"].Value) {
        //         // Sometimes this comes in wrapped in SCE trust, unwrap it if it is
        //         if (product.SerializableAttributes["R"].Value["$$unwrapTrustedValue"]) {
        //             this.inventory.breakablePack = this.$sce.getTrustedHtml(product.SerializableAttributes["R"].Value);
        //         } else if (product.SerializableAttributes["R"].Value) {
        //             this.inventory.breakablePack = product.SerializableAttributes["R"].Value;
        //         }
        //     }
        // }
        // TODO: Rework with Restrictions Schema
        // private applyRestrictShipFlag = (product: api.ProductModel): void => {
        //     if (product.SerializableAttributes
        //         && product.SerializableAttributes["SKU-Restrictions"]
        //         && product.SerializableAttributes["SKU-Restrictions"].Value) {
        //         // Sometimes this comes in wrapped in SCE trust, unwrap it if it is
        //         if (product.SerializableAttributes["SKU-Restrictions"].Value["$$unwrapTrustedValue"]) {
        //             if (this.$sce.getTrustedHtml(product.SerializableAttributes["SKU-Restrictions"].Value)
        //                     .indexOf('"RestrictShipFlag":"Y"') !== -1) {
        //                 this.inventory.restrictShipFlag = true;
        //             }
        //         } else if (product.SerializableAttributes["SKU-Restrictions"].Value.indexOf('"RestrictShipFlag":"Y"') !== -1) {
        //             this.inventory.restrictShipFlag = true;
        //         }
        //     }
        // }
        private processProduct = (): void => {
            if (!this.product) {
                return;
            }
            let prices: api.CalculatedPrices = { base: null, loading: true };
            if (angular.isFunction(this.product.readPrices)) {
                prices = this.product.readPrices();
            }
            this.productPrice = prices;
            // this.applyRestrictShipFlag(this.product);
            // this.applyBreakablePackFlag(this.product);
            this.cvStoreLocationService.getUserSelectedStore().then(store => {
                this.store = store;
                this.hasStore = true;
                this.buildInventory(this.product, this.store);
            }, (/*reject*/) => {
                this.buildInventory(this.product, null);
                ////this.product.ProductAssociations.forEach(assoc => {
                ////    if (assoc.Slave.TypeKey !== "KIT") { return; }
                ////    this.processPack(assoc.Slave, null);
                ////});
                ////this.viewstate.productIsProcessing = false;
            });
        }
        private processSingle(product: api.ProductModel): void {
            this.noUnitInventory = !product.readInventory && product.readInventory().IsOutOfStock;
            let prices: api.CalculatedPrices = { base: null, loading: true };
            if (angular.isFunction(product.readPrices)) {
                prices = product.readPrices();
            }
            this.unitPrice = prices;
            this.unitProduct = product;
            // this.applyRestrictShipFlag(this.unitProduct);
            // this.applyBreakablePackFlag(this.unitProduct);
        }
        private processPack(kitProduct: api.ProductModel): void {
            this.noPackInventory = !kitProduct.readInventory && kitProduct.readInventory().IsOutOfStock;
            // this.inventory.anyDCIQty = Math.floor(kitProduct.inventoryObject.totalQty);
            // this.inventory.storeDCIQty = Math.floor(kitProduct.inventoryObject.totalQty);
            let prices: api.CalculatedPrices = { base: null, loading: true };
            if (angular.isFunction(kitProduct.readPrices)) {
                prices = kitProduct.readPrices();
            }
            this.packPrice = prices;
            this.packProduct = kitProduct;
            // this.applyRestrictShipFlag(this.packProduct);
            // this.applyBreakablePackFlag(this.packProduct);
        }
        private buildInventory(product: api.ProductModel, store: api.StoreModel): void {
            product = this.cvInventoryService.factoryAssign(product);
            if (!product.ProductAssociations
                || !product.ProductAssociations.filter(x => x.Slave).length) {
                this.viewstate.productIsProcessing = false;
                this.setInitialShipToValue();
                return;
            }
            product.ProductAssociations.filter(x => x.Slave).forEach(x => {
                x.Slave = this.cvInventoryService.factoryAssign(x.Slave);
                if (x.TypeKey === "VARIANT-OF-MASTER" && x.Slave.TypeKey === "GENERAL") {
                    this.processSingle(x.Slave);
                } else if (x.TypeKey === "VARIANT-OF-MASTER" && x.Slave.TypeKey === "KIT") {
                    this.processPack(x.Slave);
                }
                this.viewstate.productIsProcessing = false;
                this.setInitialShipToValue();
            });
        }
        // Determining what data to show
        get productID(): number { return this.product["ID"] || this.product["id"]; }
        get displayNameLimit(): number {
            return Number(this.nameLimit) || 50;
        }
        get displayModel(): string {
            switch (this.shipToValue || this.initialShipToValue()) {
                case this.cvServiceStrings.attributes.inStorePickup: {
                    if (this.unitProduct) {
                        return this.unitProduct.ManufacturerPartNumber;
                    }
                    return this.product.ManufacturerPartNumber;
                }
                case this.cvServiceStrings.attributes.shipToStore:
                case this.cvServiceStrings.attributes.shipToHome: {
                    if (this.unitProduct) { // The pack product will not have it, check unitProduct instead
                        return this.unitProduct.ManufacturerPartNumber;
                    }
                    return this.product.ManufacturerPartNumber;
                }
            }
            return "";
        }
        get displaySKU(): string {
            switch (this.shipToValue || this.initialShipToValue()) {
                case this.cvServiceStrings.attributes.inStorePickup: {
                    if (this.unitProduct) {
                        return this.unitProduct.CustomKey;
                    }
                    return this.product.CustomKey;
                }
                case this.cvServiceStrings.attributes.shipToStore:
                case this.cvServiceStrings.attributes.shipToHome: {
                    if (this.packProduct) {
                        return this.packProduct.CustomKey;
                    }
                    return this.product.CustomKey;
                }
            }
            return "";
        }
        get displayPrice(): number {
            switch (this.shipToValue || this.initialShipToValue()) {
                case this.cvServiceStrings.attributes.inStorePickup: {
                    if (this.unitPrice) {
                        return this.unitPrice.isSale ? this.unitPrice.sale : this.unitPrice.base;
                    }
                    return this.productPrice.isSale ? this.productPrice.sale : this.productPrice.base;
                }
                case this.cvServiceStrings.attributes.shipToStore:
                case this.cvServiceStrings.attributes.shipToHome: {
                    if (this.packPrice) {
                        return this.packPrice.isSale ? this.packPrice.sale : this.packPrice.base;
                    }
                    return this.productPrice.isSale ? this.productPrice.sale : this.productPrice.base;
                }
            }
            return null;
        }
        get displayUofM(): string {
            switch (this.shipToValue || this.initialShipToValue()) {
                case this.cvServiceStrings.attributes.inStorePickup: {
                    return "Each";
                }
                case this.cvServiceStrings.attributes.shipToStore:
                case this.cvServiceStrings.attributes.shipToHome: {
                    if (this.packProduct) {
                        return `per pack of ${this.packProduct.CustomKey.substring(9).trim()}`;
                    }
                    return "Each";
                }
            }
            return "";
        }
        get displayUofMShort(): string {
            switch (this.shipToValue || this.initialShipToValue()) {
                case this.cvServiceStrings.attributes.inStorePickup: {
                    return "Each";
                }
                case this.cvServiceStrings.attributes.shipToStore:
                case this.cvServiceStrings.attributes.shipToHome: {
                    if (this.packProduct) {
                        return "per pack";
                    }
                    return "Each";
                }
            }
            return "";
        }
        get displayInventoryLimit(): number {
            switch (this.shipToValue || this.initialShipToValue()) {
                case this.cvServiceStrings.attributes.inStorePickup: {
                    return this.inStorePickupStock();
                }
                case this.cvServiceStrings.attributes.shipToStore: {
                    return this.shipToStoreStock();
                }
                case this.cvServiceStrings.attributes.shipToHome: {
                    return this.shipToHomeStock();
                }
            }
            return 0;
        }
        // Determining which ship to selectors to show
        // get breakablePack(): string { return this.inventory.breakablePack; }
        // get isShippingRestricted(): boolean { return this.inventory.restrictShipFlag; }
        showShipToHome = (): boolean => {
            // if (this.isShippingRestricted) { return this.showShipToHomeWhenRestricted; }
            return this.showShipToHomeWhenUnavailable || this.haveShipToHomeStock;
        }
        shipToHomeStock = (): number => {
            if (this.viewstate.productIsProcessing) { return 0; }
            if (this.packProduct && this.packProduct.readInventory) {
                return Math.floor(this.packProduct.readInventory().QuantityOnHand * 0.8); // dc_007 only/20% buffer
            }
            if (this.unitProduct && this.unitProduct.readInventory) {
                return Math.floor(this.unitProduct.readInventory().QuantityOnHand * 0.8); // dc_007 only/20% buffer
            }
            if (!this.product.readInventory) {
                return 0;
            }
            return Math.floor(this.product.readInventory().QuantityOnHand * 0.8); // dc_007 only/20% buffer
        }
        get haveShipToHomeStock(): boolean { return this.shipToHomeStock() >= 5; }
        get isShipToHome(): boolean { return this.shipToValue === this.cvServiceStrings.attributes.shipToHome; }
        showInStorePickup = (): boolean => {
            if (!this.hasStore) { return this.showInStorePickupAsChooseStoreLinkWhenNoStore; }
            return this.showInStorePickupWhenUnavailable || this.haveInStorePickupStock;
        }
        inStorePickupStock = (): number => {
            if (this.viewstate.productIsProcessing) { return 0; }
            if (!this.hasStore) { return 0; }
            if (this.unitProduct) {
                if (!this.unitProduct.readInventory) {
                    console.error("The unit product does not have an inventory object to read storeQty from");
                    return 0;
                }
                return this.unitProduct.readInventory().QuantityOnHand;
            }
            if (!this.product.readInventory()) {
                return 0;
            }
            return this.product.readInventory().QuantityOnHand;
        }
        get haveInStorePickupStock(): boolean { return  this.inStorePickupStock() > 0; }
        get isInStorePickup(): boolean { return this.shipToValue === this.cvServiceStrings.attributes.inStorePickup; }
        showShipToStore(): boolean {
            if (!this.hasStore) {
                return false;
            }
            if (this.haveInStorePickupStock && !this.showShipToStoreWhenInStorePickupIsAvailable) {
                return false;
            }
            return this.showShipToStoreWhenUnavailable || this.haveShipToStoreStock;
        }
        shipToStoreStock(): number {
            if (this.viewstate.productIsProcessing) { return 0; }
            if (!this.hasStore) { return 0; }
            if (this.unitProduct) { // NOTE: Intentionally the unitProduct
                if (!angular.isFunction(this.unitProduct.readInventory)) {
                    console.error("The unit product does not have an inventory object to read storeDCIQty from");
                    return 0;
                }
                if (!this.packProduct || !this.packProduct.CustomKey) {
                    return 0;
                }
                return Math.max(0,
                    // TODO: Change to read store stock only
                    Math.floor((this.unitProduct.readInventory().QuantityOnHand || 0) /
                        parseInt(this.packProduct.CustomKey.split("x")[1])));
            }
            if (!angular.isFunction(this.product.readInventory)) {
                return 0;
            }
            // TODO: Change to read store stock only
            return this.product.readInventory().QuantityOnHand;
        }
        get haveShipToStoreStock(): boolean { return this.shipToStoreStock() > 0; }
        get isShipToStore(): boolean { return this.shipToValue === this.cvServiceStrings.attributes.shipToStore; }
        private get showRadioCount(): number {
            let toShow = 0;
            if (this.showShipToHome()) { toShow++; }
            if (this.showInStorePickup()) { toShow++; }
            if (this.showShipToStore()) { toShow++; }
            return toShow;
        }
        get showOneFillerBlock(): boolean { return this.showRadioCount < 2; }
        get showTwoFillerBlocks(): boolean { return this.showRadioCount < 1; }
        // Cart Actions
        decreaseQuantity(): void {
            this.quantity = Math.max(this.calculateMinAllowedPurchase(), this.quantity - 1);
        }
        increaseQuantity(): void {
            this.quantity = Math.min(this.calculateMaxAllowedPurchase(), this.quantity + 1);
        }
        calculateMinAllowedPurchase(): number {
            if (this.productToAdd == null) { return 1; }
            switch (this.shipToValue) {
                case this.cvServiceStrings.attributes.inStorePickup: {
                    if (!this.haveInStorePickupStock) { return 0; }
                    break;
                }
                case this.cvServiceStrings.attributes.shipToStore: {
                    if (!this.haveShipToStoreStock) { return 0; }
                    break;
                }
                case this.cvServiceStrings.attributes.shipToHome: {
                    if (!this.haveShipToHomeStock) { return 0; }
                    break;
                }
            }
            if (this.productToAdd.MinimumPurchaseQuantity == null) { return 1; }
            return this.productToAdd.MinimumPurchaseQuantity;
        }
        calculateMaxAllowedPurchase(): number {
            const productToAdd = this.productToAdd; // This is a getter with a switch in it
            if (productToAdd == null) { return 9999999; }
            switch (this.shipToValue) {
                case this.cvServiceStrings.attributes.inStorePickup: {
                    if (!this.haveInStorePickupStock) { return 0; }
                    const inStorePickupStock = this.inStorePickupStock();
                    if (productToAdd.MaximumPurchaseQuantity
                        && productToAdd.MaximumPurchaseQuantity < inStorePickupStock) {
                        return productToAdd.MaximumPurchaseQuantity;
                    }
                    return inStorePickupStock;
                }
                case this.cvServiceStrings.attributes.shipToStore: {
                    if (!this.haveShipToStoreStock) { return 0; }
                    const shipToStoreStock = this.shipToStoreStock();
                    if (productToAdd.MaximumPurchaseQuantity
                        && productToAdd.MaximumPurchaseQuantity < shipToStoreStock) {
                        return productToAdd.MaximumPurchaseQuantity;
                    }
                    return shipToStoreStock;
                }
                case this.cvServiceStrings.attributes.shipToHome: {
                    if (!this.haveShipToHomeStock) { return 0; }
                    const shipToHomeStock = this.shipToHomeStock();
                    if (productToAdd.MaximumPurchaseQuantity
                        && productToAdd.MaximumPurchaseQuantity < shipToHomeStock) {
                        return productToAdd.MaximumPurchaseQuantity;
                    }
                    return shipToHomeStock;
                }
            }
            return 9999999;
        }
        get disableAddToCartButton(): boolean {
            const max = this.calculateMaxAllowedPurchase();
            return !this.showAddToCartButton
                || this.viewstate.checkoutIsProcessing
                || !this.quantity
                || this.quantity <= 0
                || (this.isShipToHome && (/*this.isShippingRestricted ||*/ this.quantity > max))
                || (this.isInStorePickup && this.quantity > max)
                || (this.isShipToStore && this.quantity > max);
        }
        get showAddToCartButton(): boolean {
            return !this.showOutOfStockButton;
        }
        get showOutOfStockButton(): boolean {
            return this.isShipToHome && (!this.haveShipToHomeStock /*|| this.isShippingRestricted*/)
                || this.isInStorePickup && !this.haveInStorePickupStock
                || this.isShipToStore && !this.haveShipToStoreStock;
        }
        get productToAdd(): api.ProductModel {
            switch (this.shipToValue) {
                case this.cvServiceStrings.attributes.inStorePickup: {
                    return this.unitProduct ? this.unitProduct : this.product;
                }
                case this.cvServiceStrings.attributes.shipToStore:
                case this.cvServiceStrings.attributes.shipToHome: {
                    return this.packProduct ? this.packProduct : this.product;
                }
                default: {
                    return null;
                }
            }
        }
        addCartItem(): void {
            this.viewstate.checkoutIsProcessing = true;
            const productToAdd = this.productToAdd; // this is a getter with a switch in it
            if (!productToAdd) {
                throw Error("Could not determine the appropriate product to add");
            }
            this.cvCartService.addCartItem(
                productToAdd.ID,
                this.cvServiceStrings.carts.types.cart,
                this.quantity,
                {
                    selectedShipOption: this.shipToValue,
                    currentInventoryLimit: this.displayInventoryLimit
                },
                productToAdd
            ).finally(() => this.viewstate.checkoutIsProcessing = false);
        }
        updateProductSelection(): void {
            if (angular.isFunction(this.product.readInventory)) {
                this.newProduct = this.product;
            } else if (this.product.ProductAssociations) {
                this.product.ProductAssociations
                    .filter(x => x.Slave)
                    .forEach(assoc => {
                        if (angular.isFunction(assoc.Slave.readInventory)) {
                            this.newProduct = assoc.Slave;
                            return false; // stops the forEach loop
                        }
                        return true;
                    });
            }
            this.consoleLog(`change: ${this.product.ID}`);
        }
        // Constructors
        constructor(
                readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvInventoryService: services.IInventoryService,
                private readonly cvStoreLocationService: services.IStoreLocationService,
                private readonly cvSearchCatalogProductCompareService: services.ISearchCatalogProductCompareService, // Used by UI
                private readonly cvCartService: services.ICartService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.stores.selectionUpdate,
                (__: ng.IAngularEvent, store: api.StoreModel): void => {
                    this.store = store;
                    this.buildInventory(this.product, this.store);
                });
            const unbind2 = $scope.$on(cvServiceStrings.events.auth.signIn, () => this.processLoginChange());
            const unbind3 = $scope.$on(cvServiceStrings.events.auth.signOut, () => this.processLoginChange());
            $scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
                if (angular.isFunction(unbind3)) { unbind3(); }
            });
            this.processLoginChange();
            this.viewstate.productIsProcessing = true;
            this.processProduct();
        }
    }
}
