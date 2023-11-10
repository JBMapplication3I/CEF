// https://developers.facebook.com/docs/facebook-pixel

module cef.store.services {
    export interface IFacebookPixelService {
        viewContent(value?: number, currency?: string, contentName?: string, contentType?: string, contentIds?: Array<string>, contents?: Array<api.ProductModel>): void;
        search(value?: number, currency?: string, contentCategory?: string, contentIds?: Array<string>, contents?: Array<api.ProductModel>, searchString?: string): void;
        addToCart(value?: number, currency?: string, contentName?: string, contentType?: string, contentIds?: Array<string>, contents?: Array<api.ProductModel>): void;
        addToWishlist(value?: number, currency?: string, contentName?: string, contentCategory?: string, contentIds?: Array<string>, contents?: Array<api.ProductModel>): void;
        initiateCheckout(value?: number, currency?: string, contentName?: string, contentCategory?: string, contentIds?: Array<string>, contents?: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>, numItems?: number): void;
        addPaymentInfo(value?: number, currency?: string, contentCategory?: string, contentIds?: Array<string>, contents?: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>): void;
        purchase(value: number, currency: string, contentName?: string, contentType?: string, contentIds?: Array<string>, contents?: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>, numItems?: number): void;
        lead(value?: number, currency?: string, contentName?: string, contentCategory?: string): void;
        completeRegistration(value?: number, currency?: string, contentName?: string, status?: string): void;
    }

    export class FacebookPixelService implements IFacebookPixelService {
        viewContent(
                value?: number,
                currency?: string,
                contentName?: string,
                contentType?: string,
                contentIds?: Array<string>,
                contents?: Array<api.ProductModel>): void {
            if (this.$window.fbq != null) {
                this.$window.fbq("track", "ViewContent", {
                    value: value,
                    currency: currency,
                    content_name: contentName,
                    content_type: contentType,
                    content_ids: contentIds,
                    contents: contents && contents.map(product => this.mapToProductObjectInner(product))
                });
            }
        };
        search(
                value?: number,
                currency?: string,
                contentCategory?: string,
                contentIds?: Array<string>,
                contents?: Array<api.ProductModel>,
                searchString?: string): void {
            if (this.$window.fbq != null) {
                this.$window.fbq("track", "Search", {
                    value: value,
                    currency: currency,
                    content_category: contentCategory,
                    content_ids: contentIds,
                    contents: contents && contents.map(product => this.mapToProductObjectInner(product)),
                    search_string: searchString
                });
            }
        };
        addToCart(
                value?: number,
                currency?: string,
                contentName?: string,
                contentType?: string,
                contentIds?: Array<string>,
                contents?: Array<api.ProductModel>): void {
            if (this.$window.fbq != null) {
                this.$window.fbq("track", "AddToCart", {
                    value: value,
                    currency: currency,
                    content_name: contentName,
                    content_type: contentType,
                    content_ids: contentIds,
                    contents: contents && contents.map(product => this.mapToProductObjectInner(product))
                });
            }
        };
        addToWishlist(
                value?: number,
                currency?: string,
                contentName?: string,
                contentCategory?: string,
                contentIds?: Array<string>,
                contents?: Array<api.ProductModel>): void {
            if (this.$window.fbq != null) {
                this.$window.fbq("track", "AddToWishlist", {
                    value: value,
                    currency: currency,
                    content_name: contentName,
                    content_category: contentCategory,
                    content_ids: contentIds,
                    contents: contents && contents.map(product => this.mapToProductObjectInner(product))
                });
            }
        };
        initiateCheckout(
                value?: number,
                currency?: string,
                contentName?: string,
                contentCategory?: string,
                contentIds?: Array<string>,
                contents?: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>,
                numItems?: number)
                : void {
            if (!this.$window.fbq) {
                return;
            }
            if (!contents) {
                this.initiateCheckoutInner(
                    value, currency, contentName, contentCategory, contentIds, null, numItems);
            }
            this.$q.when(contents.map(cartItem => this.mapToProductObject(
                cartItem.ProductID,
                cartItem.Quantity + (cartItem.QuantityBackOrdered || 0) + (cartItem.QuantityPreSold || 0),
                cartItem.UnitSoldPrice || cartItem.UnitCorePrice))
            ).then(mapped => this.initiateCheckoutInner(
                value, currency, contentName, contentCategory, contentIds, mapped, numItems));
        }
        private initiateCheckoutInner(
                value?: number,
                currency?: string,
                contentName?: string,
                contentCategory?: string,
                contentIds?: Array<string>,
                mapped?: Array<object>,
                numItems?: number)
                : void {
            this.$window.fbq("track", "InitiateCheckout", {
                value: value,
                currency: currency,
                content_name: contentName,
                content_category: contentCategory,
                content_ids: contentIds,
                contents: mapped,
                num_items: numItems
            });
        }
        addPaymentInfo(
                value?: number,
                currency?: string,
                contentCategory?: string,
                contentIds?: Array<string>,
                contents?: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>)
                : void {
            if (!this.$window.fbq) {
                return;
            }
            if (!contents) {
                this.addPaymentInfoInner(
                    value, currency, contentCategory, contentIds, null);
            }
            this.$q.when(contents.map(cartItem => this.mapToProductObject(
                cartItem.ProductID,
                cartItem.Quantity + (cartItem.QuantityBackOrdered || 0) + (cartItem.QuantityPreSold || 0),
                cartItem.UnitSoldPrice || cartItem.UnitCorePrice))
            ).then(mapped => this.addPaymentInfoInner(
                value, currency, contentCategory, contentIds, mapped));
        }
        private addPaymentInfoInner(
                value?: number,
                currency?: string,
                contentCategory?: string,
                contentIds?: Array<string>,
                mapped?: Array<object>)
                : void {
            this.$window.fbq("track", "AddPaymentInfo", {
                value: value,
                currency: currency,
                content_category: contentCategory,
                content_ids: contentIds,
                contents: mapped
            });
        }
        purchase(
                value: number,
                currency: string,
                contentName?: string,
                contentType?: string,
                contentIds?: Array<string>,
                contents?: Array<api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>>,
                numItems?: number)
                : void {
            if (!this.$window.fbq) {
                return;
            }
            if (!contents) {
                this.purchaseInner(
                    value, currency, contentName, contentType, contentIds, null, numItems);
                return;
            }
            this.$q.when(contents.map(cartItem => this.mapToProductObject(
                cartItem.ProductID,
                cartItem.Quantity + (cartItem.QuantityBackOrdered || 0) + (cartItem.QuantityPreSold || 0),
                cartItem.UnitSoldPrice || cartItem.UnitCorePrice))
            ).then(mapped => this.purchaseInner(
                value, currency, contentName, contentType, contentIds, mapped, numItems));
        };
        private purchaseInner(
                value: number,
                currency: string,
                contentName?: string,
                contentType?: string,
                contentIds?: Array<string>,
                mapped?: Array<object>,
                numItems?: number)
                : void {
            this.$window.fbq("track", "Purchase", {
                value: value,
                currency: currency,
                content_name: contentName,
                content_type: contentType,
                content_ids: contentIds,
                contents: mapped,
                num_items: numItems
            });
        }
        lead(value?: number, currency?: string, contentName?: string, contentCategory?: string): void {
            if (this.$window.fbq != null) {
                this.$window.fbq("track", "Lead", {
                    value: value,
                    currency: currency,
                    content_name: contentName,
                    content_category: contentCategory
                });
            }
        };
        completeRegistration(
                value?: number,
                currency?: string,
                contentName?: string,
                status?: string)
                : void {
            if (this.$window.fbq != null) {
                this.$window.fbq("track", "CompleteRegistration", {
                    value: value,
                    currency: currency,
                    content_name: contentName,
                    status: status
                });
            }
        }
        private mapToProductObject(
                productID: number, quantity?: number, price?: number)
                : ng.IPromise<object> {
            return this.$q((resolve, reject) => {
                this.cvApi.products.GetProductByID(productID).then(r => {
                    if (!r || !r.data) {
                        reject();
                        return;
                    }
                    resolve(this.mapToProductObjectInner(r.data, quantity, price));
                }).catch(reason => reject(reason));
            });
        }
        private mapToProductObjectInner(
                product: api.ProductModel,
                quantity?: number,
                price?: number)
                : object {
            let prices: api.CalculatedPrices = { base: null, loading: true };
            if (angular.isFunction(this.cvPricingService.factoryAssign(product))) {
                prices = product.readPrices();
            }
            return {
                "id": product.CustomKey,
                "quantity": quantity,
                "item_price": price || (prices.isSale ? prices.sale : prices.base)
            };
        }
        // Constructor
        constructor(
            private readonly $window: ng.IWindowService,
            private readonly $q: ng.IQService,
            private readonly cvApi: api.ICEFAPI,
            private readonly cvPricingService: services.IPricingService) { }
    }
}
