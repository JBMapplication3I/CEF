/**
 * @file framework/store/cart/controls/addToCartBase.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Add to Cart base class
 */
module cef.store.cart.controls {
    export abstract class AddToCartControllerBase extends core.TemplatedControllerBase {
        // ===== Bound Scope Properties ========================
        // Item lookup
        get item(): api.ProductModel {
            if (this.itemInner) {
                return this.itemInner;
            }
            if (this._item) {
                // We already have the item or we don't have the ID yet
                this.itemInner = this._item;
                return this.itemInner;
            }
            if (!this.itemId && !this.itemName && !this.itemSeoUrl) {
                // We don't have an item or an id to use yet
                return null;
            }
            if (this.cannotLoad) {
                // Already verified we can't do this by the provided id, don't infinite loop
                return null;
            }
            if (this.isGetting) {
                // We're actively getting the item
                return null;
            }
            this.isGetting = true;
            this.setRunning();
            this.cvProductService.get({
                id: Number(this.itemId),
                name: this.itemName,
                seoUrl: this.itemSeoUrl
            }).then(p => {
                if (!p) {
                    this.finishRunning(
                        true,
                        "ERROR! Cannot load product with ID: " + this.itemId);
                    this.isGetting = false;
                    return;
                }
                this.itemInner = p;
                this.finishRunning();
                this.isGetting = false;
            }).catch(reason => {
                this.finishRunning(true, reason);
                this.isGetting = false;
            });
            return null;
        }
        set item(value: api.ProductModel) {
            this.itemInner = this._item = value;
        }
        itemId: number;
        itemName: string;
        itemSeoUrl: string;
        salesItem: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>;
        // Shared with base
        cartType: string;
        currentValue: number;
        defaultValue: number;
        alwaysAllowInput: boolean;
        externalDisabled: boolean;
        debug: boolean;
        index: string;
        notifyMaximum: boolean;
        // ===== Properties ====================================
        get menuEnabled(): boolean {
            return this.cefConfig.featureSet.salesQuotes.useQuoteCart;
        }
        get currentValueEditable(): number {
            if (angular.isUndefined(this.currentValue)) {
                // this.consoleDebug(`addToCartBase.currentValue.get-${this.$scope.$id
                //     } is undefined, returning undefined for now`);
                return undefined;
            }
            if (this.currentValue === null) {
                // console.trace(`addToCartBase.currentValue.get-${this.$scope.$id
                //     } is having to use default value because it's null`);
                this.currentValue = this.defaultValue || this.minimum;
            }
            // this.consoleDebug(`addToCartBase.currentValue.get-${this.$scope.$id
            //     } is defined and returning a value of '${this.currentValue}'`);
            return this.currentValue;
        }
        set currentValueEditable(newValue: number) {
            if (newValue === undefined) {
                // Ignore it
                return;
            }
            this.currentValue = newValue;
            this.dirty = true;
        }
        protected initializedAt: number;
        protected dirty = false;
        protected cannotLoad = false;
        protected isGetting: boolean;
        protected _item: api.ProductModel;
        protected itemInner: api.ProductModel;
        abstract get minimum(): number;
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvProductService: services.IProductService) {
            super(cefConfig);
            // const debugMsg = `addToCartBase.ctor-${$scope.$id}`;
            // this.consoleDebug(`${debugMsg}: entered`);
            this.initializedAt = Math.round(new Date().getTime());
            // this.consoleDebug(`${debugMsg}: exited`);
        }
    }
}
