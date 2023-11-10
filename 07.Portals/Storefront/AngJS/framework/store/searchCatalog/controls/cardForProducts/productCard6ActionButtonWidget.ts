/**
 * @file framework/store/searchCatalog/controls/cardForProducts/productCard6ActionButtonWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #6: Action Buttons widget
 */
module cef.store.searchCatalog.controls.results {
    class ProductCardActionButtonWidgetController extends ProductCardWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        buttonView: string;
        quickAdd: boolean;
        small: number;
        asIcon: number;
        // Properties
        // <See inherited>
        quantity: number = 1;
        get isSupervisor(): boolean { return this.cvSecurityService.hasRole("Supervisor") };
        // Convenience Redirects (Reduce binding text/conditions)
        protected get abv(): string {
            return this.buttonView
                || "addToCart"; // Default
        }
        protected get quickA(): boolean {
            return Boolean(this.quickAdd);
                //|| true; // Default
        }
        protected get menuEnabled(): boolean {
            return this.cefConfig.featureSet.salesQuotes.useQuoteCart;
        }
        // Functions
        // <See inherited>
        addCartItem(quantity = this.quantity): void {
            this.setRunning();
            let params: services.IAddCartItemParams = { };
            if (this.cvProductService.productUOMSelectionObject[this.productId] && this.cvProductService.productUOMSelectionObject[this.productId][1]) {
                let obj: api.SerializableAttributesDictionary = {
                    "SelectedUOM": {
                        ID: 1,
                        Key: "SelectedUOM",
                        Value: this.cvProductService.productUOMSelectionObject[this.productId][1]
                    },
                    "SoldPrice": {
                        ID: 1,
                        Key: "SoldPrice",
                        Value: this.cvProductService.productUOMSelectionObject[this.productId][0]
                    },
                }
                params.SerializableAttributes = Object.assign(obj);
                params.ForceUniqueLineItemKey = this.product.CustomKey + this.cvProductService.productUOMSelectionObject[this.productId][1];
            } else {
                params.ForceUniqueLineItemKey = this.product.CustomKey;
            };
            this.cvCartService.addCartItem(
                    this.product.ID,
                    this.cvServiceStrings.carts.types.cart,
                    quantity || 1,
                    params,
                    this.product)
                .finally(() => this.finishRunning());
        }
        addQuoteCartItem(quantity = this.quantity): void {
            this.setRunning();
            let params: services.IAddCartItemParams = { };
            if (this.cvProductService.productUOMSelectionObject[this.productId] && this.cvProductService.productUOMSelectionObject[this.productId][1]) {
                let obj: api.SerializableAttributesDictionary = {
                    "SelectedUOM": {
                        ID: 1,
                        Key: "SelectedUOM",
                        Value: this.cvProductService.productUOMSelectionObject[this.productId][1]
                    },
                    "SoldPrice": {
                        ID: 1,
                        Key: "SoldPrice",
                        Value: this.cvProductService.productUOMSelectionObject[this.productId][0]
                    },
                }
                params.SerializableAttributes = Object.assign(obj);
                params.ForceUniqueLineItemKey = this.product.CustomKey + this.cvProductService.productUOMSelectionObject[this.productId][1];
            } else {
                params.ForceUniqueLineItemKey = this.product.CustomKey;
            };
            this.cvCartService.addCartItem(
                    this.product.ID,
                    this.cvServiceStrings.carts.types.quote,
                    quantity || 1,
                    params,
                    this.product)
                .finally(() => this.finishRunning());
        }
        /** This is used in a special variants setup */
        addCartItems(quantity = this.quantity): void {
            this.setRunning();
            if (this.readIfUOMInventoryIsOutOfStock()) {
                console.log("Working")
                return;
            }
            this.cvCartService.addCartItem(
                    this.product.ID,
                    this.cvServiceStrings.carts.types.cart,
                    quantity || 1,
                    { },
                    this.product)
                .finally(() => this.finishRunning());
        }
        addQuoteCartItems(quantity = this.quantity): void {
            this.setRunning();
            this.cvCartService.addCartItem(
                    this.product.ID,
                    this.cvServiceStrings.carts.types.quote,
                    quantity || 1,
                    { },
                    this.product)
                .finally(() => this.finishRunning());
        }
        readIfUOMInventoryIsOutOfStock(): boolean {
            let currentUOMSelectionObj = this.cvProductService.productUOMSelectionObject[this.product.ID];
            let currentUOMSelection = currentUOMSelectionObj != null && currentUOMSelectionObj.length ? currentUOMSelectionObj[1] : null;
            let rawUOMInventories = this.product["$_rawInventoryUOMs"];
            if (currentUOMSelection && rawUOMInventories != null) {
                let findResult: api.CalculatedInventory = rawUOMInventories.find(x => x.ProductUOM === currentUOMSelection)
                if (Math.floor(findResult.QuantityOnHand) <  this.quantity) return true;
            }
            return false;
        }
        isDisabled(): boolean {
            return !this.product
                || this.cefConfig.featureSet.inventory.enabled
                && !angular.isFunction(this.product.readInventory)
                || this.cefConfig.featureSet.inventory.enabled
                && (this.product.readInventory().IsOutOfStock)
                && !this.product.readInventory().AllowBackOrder
                && !((this.product.ProductAssociations || [])
                        .filter(x => x.TypeName == "Variant of Master")).length;
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService,
                private readonly cvCartService: services.ICartService,
                private readonly cvSecurityService: services.ISecurityService) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
        }
    }

    cefApp.directive("cefProductCardActionButtonWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            productId: "=?",
            buttonView: "=",
            quickAdd: "=?",
            small: "=?",
            asIcon: "=?",
            index: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/cardForProducts/productCard6ActionButtonWidget.html", "ui"),
        controller: ProductCardActionButtonWidgetController,
        controllerAs: "pcabwCtrl",
        bindToController: true
    }));
}
