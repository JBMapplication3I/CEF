/**
 * @file framework/store/cart/controls/addToCartButton.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Add to cart button class
 */
module cef.store.cart.controls {
    class AddToCartButtonController extends AddToCartControllerBase {
        // Bound Scope Properties
        showIcon: boolean;
        showText: boolean;
        shipOption: string;
        outOfStockLinksToDetails: boolean;
        handleUoms: boolean;
        productUomKey: string;
        productUomValue: string;
        forceDisableQuote: boolean;
        // Constants
        outOfStockKey = "ui.storefront.common.OutOfStock";
        addBackOrderKey = "ui.storefront.userDashboard2.controls.salesDetail.Reorder";
        addPreSalesKey = "ui.storefront.common.AddPreSales";
        addToCartKey = "ui.storefront.cart.addToCart";
        viewDetailsKey = "ui.storefront.common.ViewDetails";
        addQuoteKey = "ui.storefront.quotes.cart.add";
        loadingKey = "ui.storefront.common.Loading.Ellipses";
        // Properties
        get minimum(): number { return 1; }
        get isSupervisor(): boolean { return this.cvSecurityService.hasRole("Supervisor") };
        // Functions
        isDisabled(): boolean {
            return !this.item
                || this.cefConfig.featureSet.inventory.enabled
                && !angular.isFunction(this.item.readInventory)
                || this.cefConfig.featureSet.inventory.enabled
                && this.item.readInventory().IsOutOfStock
                && !this.item.readInventory().AllowBackOrder
                && !Boolean(this.outOfStockLinksToDetails)
                && !((this.item.ProductAssociations || [])
                        .filter(x => x.TypeName == "Variant of Master")).length;
        }
        isDisabledQuote(): boolean {
            return !this.item || this.currentTitleKey() === this.addQuoteKey || this.forceDisableQuote == true;
        }
        currentTitleKey(): string {
            if (!this.item) {
                return this.loadingKey;
            }
            if (!angular.isFunction(this.item.readPrices)) {
                this.cvPricingService.factoryAssign(this.item);
                return this.loadingKey;
            }
            if (this.cefConfig.featureSet.inventory.enabled
                && !angular.isFunction(this.item.readInventory)) {
                this.cvInventoryService.factoryAssign(this.item);
                return this.loadingKey;
            }
            if (((this.item.ProductAssociations || [])
                    .filter(x => x.TypeName == "Variant of Master")).length > 1) {
                return this.viewDetailsKey;
            }
            if (!this.item.readPrices().haveBase) {
                return this.addQuoteKey;
            }
            if (this.cefConfig.featureSet.inventory.enabled) {
                if (this.item.readInventory().AllowPreSale) {
                    return this.addPreSalesKey;
                }
                if (this.item.readInventory().IsOutOfStock
                    && this.item.readInventory().AllowBackOrder) {
                    return this.addBackOrderKey;
                }
                if (this.item.readInventory().IsOutOfStock
                    && !this.item.readInventory().AllowBackOrder) {
                    if (Boolean(this.outOfStockLinksToDetails)) {
                        return this.viewDetailsKey;
                    }
                    return this.outOfStockKey;
                }
            }
            if (Boolean(this.outOfStockLinksToDetails)) {
                return this.outOfStockKey;
            }
            return this.addToCartKey;
        }
        currentIcon(): string {
            if (!this.showIcon) {
                return null;
            }
            switch (this.currentTitleKey()) {
                case this.outOfStockKey: { return "far fa-do-not-enter"; }
                case this.addBackOrderKey: { return "far fa-cart-plus"; }
                case this.addPreSalesKey: { return "far fa-cart-plus"; }
                case this.addToCartKey: { return "far fa-cart-plus"; }
                case this.viewDetailsKey: { return "far fa-list"; }
                case this.addQuoteKey: { return "far fa-cart-plus"; }
                default: { return "far fa-do-not-enter"; }
            }
        }
        currentBtnClass(): string {
            switch (this.currentTitleKey()) {
                case this.outOfStockKey: { return "btn-warning"; }
                case this.addBackOrderKey: { return "btn-warning"; }
                case this.addPreSalesKey: { return "btn-warning"; }
                case this.addToCartKey: { return "btn-success"; }
                case this.viewDetailsKey: { return "btn-primary"; }
                case this.addQuoteKey: { return "btn-dark"; }
                default: { return "btn-success"; }
            }
        }
        // Events
        click(): void {
            if (this.isDisabled()) {
                return;
            }
            switch (this.currentTitleKey()) {
                case this.viewDetailsKey: {
                    // Go to the details page for this product
                    this.$filter("corsProductLink")(this.item.SeoUrl);
                    break;
                }
                case this.outOfStockKey: {
                    // Do Nothing
                    return;
                }
                case this.addQuoteKey: {
                    this.setRunning();
                    const attrs = { };
                    if (this.shipOption) {
                        attrs["SerializableAttributes"] = {
                            ShipOption: {
                                ID: null,
                                Key: this.cvServiceStrings.attributes.shipOption,
                                Value: this.shipOption
                            }
                        };
                    }
                    this.cvCartService.addCartItem(
                            this.itemId,
                            this.cvServiceStrings.carts.types.quote,
                            this.currentValue,
                            attrs,
                            this.item)
                        .finally(() => this.finishRunning());
                    break;
                }
                case this.addBackOrderKey:
                case this.addPreSalesKey:
                case this.addToCartKey:
                default: {
                    this.setRunning();
                    const attrs = { };
                    attrs["SerializableAttributes"] = {};
                    if (this.shipOption) {
                        attrs["SerializableAttributes"] = {
                            ShipOption: {
                                ID: null,
                                Key: this.cvServiceStrings.attributes.shipOption,
                                Value: this.shipOption
                            }
                        };
                    }
                    this.cvCartService.addCartItem(
                            this.itemId,
                            this.cartType || this.cvServiceStrings.carts.types.cart,
                            this.currentValue,
                            attrs,
                            this.item)
                        .finally(() => this.finishRunning());
                    break;
                }
            }
        }
        clickQuote(): void {
            if (this.isDisabledQuote()) {
                return;
            }
            this.setRunning();
            const attrs = { };
            if (this.shipOption) {
                attrs["SerializableAttributes"] = {
                    ShipOption: {
                        ID: null,
                        Key: this.cvServiceStrings.attributes.shipOption,
                        Value: this.shipOption
                    }
                };
            }
            this.cvCartService.addCartItem(
                    this.itemId,
                    this.cvServiceStrings.carts.types.quote,
                    this.currentValue,
                    attrs,
                    this.item)
                .finally(() => this.finishRunning());
        }
        addCartItemWithUOM(): void {
            this.setRunning();
            let params: services.IAddCartItemParams = { };
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
                params.SerializableAttributes = Object.assign(obj);
                params.ForceUniqueLineItemKey = this.item.CustomKey + this.productUomKey;
            } else {
                params.ForceUniqueLineItemKey = this.item.CustomKey;
            };
            this.cvCartService.addCartItem(
                    this.item.ID,
                    this.isSupervisor ? this.cvServiceStrings.carts.types.cart : this.cvServiceStrings.carts.types.quote,
                    1,
                    params,
                    this.item)
                .finally(() => this.finishRunning());
        }
        readIfUOMInventoryIsOutOfStock(): boolean {
            let rawUOMInventories = this.item["$_rawInventoryUOMs"];
            if (this.productUomKey && rawUOMInventories != null) {
                let findResult: api.CalculatedInventory = rawUOMInventories.find(x => x.ProductUOM === this.productUomKey)
                if (Math.floor(findResult.QuantityOnHand) === 0) {
                    return true;
                }
            }
            return false;
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvPricingService: services.IPricingService,
                protected readonly cvInventoryService: services.IInventoryService,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvSecurityService: services.ISecurityService) {
            super($scope, cefConfig, cvProductService);
        }
    }

    cefApp.directive("cefAddToCartButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            // Item lookup
            itemId: "=?",
            itemName: "=?",
            itemSeoUrl: "=?",
            salesItem: "=?",
            // Shared with base
            cartType: "=",
            currentValue: "=",
            defaultValue: "=?",
            alwaysAllowInput: "=?",
            externalDisabled: "=?",
            debug: "=?",
            index: "=?",
            forceDisableQuote: "=?",
            // Button specific
            showIcon: "=?",
            showText: "=?",
            shipOption: "=?",
            outOfStockLinksToDetails: "=?",
            // UOM
            handleUoms: "=?",
            productUomKey: "=?",
            productUomValue: "=?"
        },
        replace: true, // Required for layout
        templateUrl: $filter("corsLink")("/framework/store/cart/controls/addToCartButton.html", "ui"),
        controller: AddToCartButtonController,
        controllerAs: "atcbCtrl",
        bindToController: true
    }));
}
