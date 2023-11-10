// https://developers.google.com/tag-manager
// Not implemented: impressions, promoView, promoClick

module cef.store.services {
    export interface IGoogleTagManagerService {
        click(product: api.ProductModel, callback?: Function): void;
        detail(product: api.ProductModel): void;
        add(product: api.ProductModel, quantity?: number): void;
        remove(product: api.ProductModel, quantity?: number, position?: number): void;
        checkout(
            cartItems: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>,
            step?: number,
            option?: string,
            callback?: Function): void;
        checkoutOption(step?: string, option?: string): void;
        purchase(cart: api.CartModel, orderID: number): void;
        refund(
            refund: api.SalesReturnModel,
            refundItems?: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>): void;
    }

    export class GoogleTagManagerService implements IGoogleTagManagerService {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        click(product: api.ProductModel, callback?: Function): void {
            if (this.$window.dataLayer != null) {
                this.$window.dataLayer.push({
                    "event": "eec.productClick",
                    "ecommerce": {
                        "click": {
                            "products": [this.mapToProductFieldObjectInner(product)]
                        }
                    },
                    "eventCallback": callback
                });
            }
        }
        detail(product: api.ProductModel): void {
            if (this.$window.dataLayer != null) {
                this.$window.dataLayer.push({
                    // "event": "eec.productDetail",
                    "event": "eec.detailView",
                    "ecommerce": {
                        "detail": {
                            "products": [this.mapToProductFieldObjectInner(product)]
                        }
                    }
                });
            }
        }
        add(product: api.ProductModel, quantity?: number): void {
            if (this.$window.dataLayer != null) {
                this.$window.dataLayer.push({
                    "event": "eec.addToCart",
                    "ecommerce": {
                        "add": {
                            "products": [this.mapToProductFieldObjectInner(product, quantity)]
                        }
                    }
                });
            }
        }
        remove(product: api.ProductModel, quantity?: number): void {
            if (this.$window.dataLayer != null) {
                this.$window.dataLayer.push({
                    "event": "eec.removeFromCart",
                    "ecommerce": {
                        "remove": {
                            "products": [this.mapToProductFieldObjectInner(product, quantity)]
                        }
                    }
                });
            }
        }
        checkoutOption(step?: string, option?: string): void {
            if (this.$window.dataLayer != null) {
                this.$window.dataLayer.push({
                    "event": "eec.checkoutOption",
                    "ecommerce": {
                        "checkout_option": {
                            "actionField": {
                                "step": step,
                                "option": option
                            }
                        }
                    }
                });
            }
        }
        checkout(
            cartItems: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>,
            step?: number,
            option?: string,
            callback?: Function)
            : void {
            if (this.$window.dataLayer == null) {
                return;
            }
            this.$q.when(cartItems.map(cartItem => this.mapToProductFieldObject(
                cartItem.ProductID,
                cartItem.Quantity + (cartItem.QuantityBackOrdered || 0) + (cartItem.QuantityPreSold || 0),
                cartItem.UnitSoldPrice || cartItem.UnitCorePrice)))
                .then(mapped => this.checkoutInner(mapped, step, option, callback));
        }
        private checkoutInner(
            mapped: Array<object>,
            step?: number,
            option?: string,
            callback?: Function): void {
            this.$window.dataLayer.push({
                "event": "eec.checkout",
                "ecommerce": {
                    "checkout": {
                        "actionField": {
                            "step": step,
                            "option": option
                        },
                        "products": mapped
                    }
                },
                "eventCallback": callback
            });
        }
        purchase(cart: api.CartModel, orderID: number): void {
            if (this.$window.dataLayer == null) {
                return;
            }
            this.$q.when(
                cart.SalesItems.map(salesItem => this.mapToProductFieldObject(
                    salesItem.ProductID,
                    salesItem.Quantity + (salesItem.QuantityBackOrdered || 0) + (salesItem.QuantityPreSold || 0),
                    salesItem.UnitSoldPrice || salesItem.UnitCorePrice))
            ).then(mapped => this.purchaseInner(cart, orderID, mapped));
        }
        private purchaseInner(cart: api.CartModel, orderID: number, mapped: Array<object>): void {
            this.consoleLog(mapped);
            for (var p in mapped) {
                this.consoleLog(p);
            }
            this.$window.dataLayer.push({
                "event": "eec.purchase",
                "ecommerce": {
                    "purchase": {
                        "actionField": {
                            "id": orderID,
                            "affiliation": "",
                            "revenue": cart.Totals.Total,
                            "tax": cart.Totals.Tax,
                            "shipping": cart.Totals.Shipping,
                            "coupon": cart.Discounts != null && cart.Discounts.length > 0
                                ? "" // cart.Discounts[0].Discount.Codes[0].Code // This is not mapped
                                : ""
                        },
                        "products": mapped
                    }
                }
            });
        }
        refund(
            refund: api.SalesReturnModel,
            refundItems?: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>)
            : void {
            if (this.$window.dataLayer == null) {
                return;
            }
            if (!refundItems || !refundItems.length) {
                this.refundInner(refund, []);
                return;
            }
            this.$q.when(refundItems.map(refundItem => this.mapToProductFieldObject(
                refundItem.ProductID,
                refundItem.Quantity + (refundItem.QuantityBackOrdered || 0) + (refundItem.QuantityPreSold || 0),
                refundItem.UnitSoldPrice || refundItem.UnitCorePrice)))
                .then(mapped => this.refundInner(refund, mapped));
        }
        private refundInner(
            refund: api.SalesReturnModel,
            mapped?: Array<object>)
            : void {
            this.$window.dataLayer.push({
                "event": "eec.refund",
                "ecommerce": {
                    "refund": {
                        "actionField": {
                            "id": refund.ID
                        },
                        "products": mapped
                    }
                }
            });
        }
        private mapToProductFieldObject(
            productID: number,
            quantity?: number,
            price?: number)
            : ng.IPromise<object> {
            return this.$q((resolve, reject) => {
                this.cvApi.products.GetProductByID(productID).then(r => {
                    if (!r || !r.data) {
                        reject();
                        return;
                    }
                    resolve(this.mapToProductFieldObjectInner(r.data, quantity, price));
                }).catch(reject);
            });
        }
        //// private mapToProductFieldObjectInner(
        ////         product: api.ProductModel,
        ////         quantity?: number,
        ////         price?: number)
        ////         : object {
        ////     return {
        ////         "name": product.Name,
        ////         "id": product.ID,
        ////         "price": price || product.PriceSale || product.PriceBase,
        ////         "brand": product.CustomKey,
        ////         "category": product.ProductCategories && product.ProductCategories.length > 0
        ////             ? product.ProductCategories[0].Category.Name
        ////             : "",
        ////         "quantity": quantity || 1,
        ////         "coupon": ""
        ////     };
        //// }
        private mapToProductFieldObjectInner(
            product: api.ProductModel,
            quantity?: number,
            price?: number)
            : object {
            if (!product.Name) {
                return null;
            }
            let prices: api.CalculatedPrices = { base: null, loading: true };
            if (angular.isFunction(this.cvPricingService.factoryAssign(product))) {
                prices = product.readPrices();
            }
            return {
                "name": product.Name || product.CustomKey,
                "id": product.ID || product.CustomKey,
                "price": price || (prices.isSale ? prices.sale : prices.base),
                "brand": product.BrandName || product.CustomKey,
                "category": product.ProductCategories && product.ProductCategories.length > 0 && product.ProductCategories[0].Slave != null
                    ? (product.ProductCategories[0].Slave.Name || product.ProductCategories[0].Slave.CustomKey || "Category Detail Missing")
                    : "",
                "quantity": quantity || 1,
                "coupon": /*product.Discount.DiscountProducts && product.DiscountProducts.length > 0
                    ? (product.DiscountProducts[0].Codes[0].Code || product.DiscountProducts[0].CustomKey || "Coupon Code Detail Missing")
                    :*/ ""
            };
        }
        // Constructor
        constructor(
            private readonly $window: ng.IWindowService,
            private readonly $q: ng.IQService,
            private readonly cefConfig: core.CefConfig,
            private readonly cvApi: api.ICEFAPI,
            private readonly cvPricingService: services.IPricingService) { }
    }
}
