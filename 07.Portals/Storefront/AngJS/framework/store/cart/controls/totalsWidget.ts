/**
 * @file framework/store/cart/widgets/totalsWidget.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Totals Widget directive class
 */
module cef.store.cart.controls {
    class TotalsWidgetController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        static: boolean;
        totals: api.CartTotals;
        showHeader: boolean;
        noMarginBottom: boolean;
        padAlign: boolean;
        hideShipping: boolean;
        hideHandling: boolean;
        hideFees: boolean;
        hideDiscounts: boolean;
        hideTaxes: boolean;
        originalCurrency: string;
        sellingCurrency: string;
        private _type: string;
        get type(): string { return this._type; }
        set type(value: string) {
            this._type = value;
            if (!this.cvCartService) { return; }
            this.load();
        }
        // Properties
        get title(): string {
            if (Boolean(this.static)) {
                return "";
            }
            if (this.cvCartService.accessCart(this.type) == null
                || this.cvCartService.accessCart(this.type) == {} as any
                || this.cvCartService.accessCart(this.type).SalesItems.length === 0) {
                return this.type;
            }
            if (this.cvCartService.accessCart(this.type).SalesItems.length === 1) {
                return this.$translate.instant("ui.storefront.cart.No1Item") as any as string;
            }
            return this.cvCartService.accessCart(this.type).SalesItems.length
                + " " + (this.$translate.instant("ui.storefront.cart.Item.Plural") as any as string);
        }
        get total(): number {
            if (Boolean(this.static)) {
                return this.totals.Total;
            }
            if (!this.cvCartService.accessCart(this.type)
                || this.cvCartService.accessCart(this.type) == {} as any
                || this.cvCartService.accessCart(this.type).SalesItems.length === 0) {
                this.load();
                return 0;
            }
            return this.cvCartService.accessCart(this.type).SalesItems.length
                ? this.cvCartService.accessCart(this.type).Totals.Total
                : 0;
        }
        get totalItems(): number {
            if (Boolean(this.static)) {
                return 0;
            }
            return this.cvCartService.accessCart(this.type).SalesItems.length
                ? _.sumBy(
                    this.cvCartService.accessCart(this.type).SalesItems,
                    x => (x.Quantity || 0) + (x.QuantityBackOrdered || 0) + (x.QuantityPreSold || 0))
                : 0;
        }
        get totalsToUse(): api.CartTotals {
            return Boolean(this.static)
                ? this.totals
                : this.cvCartService.accessCart(this.type).Totals;
        }
        // Functions
        load(): ng.IPromise<void> {
            if (Boolean(this.static)) {
                return this.$q.resolve();
            }
            this.setRunning();
            return this.$q((resolve, reject) => {
                this.cvCartService.loadCart(this.type, false, "totalsWidget.load")
                    .then(() => {
                        // this.updateCurrentCartTotals(); Handled by Event
                        this.finishRunning();
                        resolve();
                    }, result => { this.finishRunning(true, result); reject(result); })
                    .catch(reason => { this.finishRunning(true, reason); reject(reason); });
            });
        }
        updateCurrentCartTotals(): void {
            const cart = this.cvCartService.accessCart(this.type);
            if (!cart || !cart.Totals) { return; }
            const totals = <api.CartTotals>{
                SubTotal: 0,
                Shipping: cart.Totals.Shipping,
                Discounts: cart.Totals.Discounts,
                Fees: cart.Totals.Fees,
                Handling: cart.Totals.Handling,
                Tax: cart.Totals.Tax,
                Total: 0,
            };
            cart.SalesItems.forEach(item => {
                var soldAt = this.$filter("modifiedValue")(
                    angular.isUndefined(item.UnitSoldPrice) || item.UnitSoldPrice == null
                        ? item.UnitCorePrice
                        : item.UnitSoldPrice,
                    item.UnitSoldPriceModifier,
                    item.UnitSoldPriceModifierMode);
                item.ExtendedPrice = soldAt * (item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0));
                totals.SubTotal += item.ExtendedPrice;
            });
            totals.SubTotal = totals.SubTotal;
            if (this.cefConfig.featureSet.shipping.splitShipping.enabled) {
                const targetCarts = this.cvCartService.accessTargetedCarts();
                totals.Shipping = 0;
                if (targetCarts && targetCarts.length) {
                    targetCarts.forEach(x => {
                        if (!x || !x.Totals || !x.Totals.Shipping) { return; }
                        totals.Shipping = totals.Shipping + x.Totals.Shipping;
                    });
                }
            } else {
                totals.Shipping = (cart.ShippingSameAsBilling || false) && !cart.BillingContact && !cart.BillingContactID
                    ? 0
                    : !(cart.ShippingSameAsBilling || false) && !cart.ShippingContact && !cart.ShippingContactID
                        ? 0
                        : totals.Shipping;
            }
            totals.Total = totals.SubTotal
                + this.$filter("modifiedValue")(totals.Fees,      cart.SubtotalFeesModifier,      cart.SubtotalFeesModifierMode)
                + this.$filter("modifiedValue")(totals.Shipping,  cart.SubtotalShippingModifier,  cart.SubtotalShippingModifierMode)
                + this.$filter("modifiedValue")(totals.Handling,  cart.SubtotalHandlingModifier,  cart.SubtotalHandlingModifierMode)
                + this.$filter("modifiedValue")(totals.Tax,       cart.SubtotalTaxesModifier,     cart.SubtotalTaxesModifierMode)
                + this.$filter("modifiedValue")(totals.Discounts, cart.SubtotalDiscountsModifier, cart.SubtotalDiscountsModifierMode);
            cart.Totals = totals;
            this.cvCartService.overrideCachedCartWithModifications(this.type, cart);
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $rootScope: ng.IRootScopeService,
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                protected readonly cefConfig: core.CefConfig,
                private readonly cvCartService: services.ICartService,
                readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.carts.loaded,
                (__: ng.IAngularEvent) => this.updateCurrentCartTotals());
            const unbind2 = $scope.$on(cvServiceStrings.events.shipping.ready,
                (__: ng.IAngularEvent) => this.updateCurrentCartTotals());
            const unbind3 = $scope.$on(cvServiceStrings.events.shipping.loaded,
                (__: ng.IAngularEvent) => this.updateCurrentCartTotals());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
                if (angular.isFunction(unbind3)) { unbind3(); }
            });
            this.load();
        }
    }

    cefApp.directive("cefTotalsWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            /**
             * The type of cart
             * @type {string}
             */
            type: "=",
            /**
             * Ignore the cart/type, just use the values provided. Use with {see totals}
             * @type {boolean}
             */
            static: "=?",
            /**
             * Ignore the cart/type, just use the values provided. Use with {see static}
             * @type {api.CartTotals}
             */
            totals: "=?",
            /**
             * @type {boolean}
             * @default false
             */
            showHeader: "=?",
            /**
             * @type {boolean}
             * @default false
             */
            noMarginBottom: "=?",
            /**
             * @type {boolean}
             * @default false
             */
            padAlign: "=?",
            /**
             * @type {boolean}
             * @default false
             */
            hideShipping: "=?",
            /**
             * @type {boolean}
             * @default false
             */
            hideHandling: "=?",
            /**
             * @type {boolean}
             * @default false
             */
            hideFees: "=?",
            /**
             * @type {boolean}
             * @default false
             */
            hideDiscounts: "=?",
            /**
             * @type {boolean}
             * @default false
             */
            hideTaxes: "=?",
            originalCurrency: "=?",
            sellingCurrency: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/cart/controls/totalsWidget.html", "ui"),
        controller: TotalsWidgetController,
        controllerAs: "twCtrl",
        bindToController: true
    }));
}
