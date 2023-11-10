/**
 * @file framework/store/cart/views/miniCartConfirmation.ts
 * @author Cipyright (c) 2021 clarity-ventures.com. All rights reserved.
 * @desc Mini-Cart class, normally shown on the checkout page
 */
 module cef.store.cart.views {
    class MiniCartConfirmationController extends CartController {
        // Bound by Scope
        hideShipping: boolean;
        hideHandling: boolean;
        hideFees: boolean;
        hideDiscounts: boolean;
        hideTaxes: boolean;
        quantityItemsIterate: number;
        // Properties
        quantityItemsShown: number;
        discountCode: string = null;
        get showShowMoreButton() {
            return this.cvCartService
                && this.cvCartService.accessCart(this.type)
                && this.cvCartService.accessCart(this.type).SalesItems
                && this.cvCartService.accessCart(this.type).SalesItems.length > this.quantityItemsIterate
                && this.quantityItemsShown < this.cvCartService.accessCart(this.type).SalesItems.length;
        }
        get showShowLessButton() {
            return this.cvCartService
                && this.cvCartService.accessCart(this.type)
                && this.cvCartService.accessCart(this.type).SalesItems
                && this.cvCartService.accessCart(this.type).SalesItems.length > this.quantityItemsIterate
                && this.quantityItemsShown > this.quantityItemsIterate;
        }
        ready = false;
        getDiscountedProductPriceDisplay(
                item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>,
                discount: api.AppliedCartItemDiscountModel)
                : string {
            switch (discount.DiscountValueType) {
                case 0: { // Percent
                    return `${discount.DiscountValue}%`;
                }
                case 1: { // Amount
                    const itemPrice = this.modifiedSalePrice(item);
                    const itemQuantity = item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0);
                    const itemExtendedPrice = itemPrice * itemQuantity;
                    const discountedProductPrice = itemExtendedPrice - discount.DiscountValue;
                    return discountedProductPrice <= 0
                        ? this.$translate.instant("ui.storefront.common.Free")
                        : this.$filter("globalizedCurrency")(discountedProductPrice);
                }
                default: {
                    return undefined;
                }
            }
        };
        // Functions
        private load(): void {
            this.setRunning();
            if (!this.quantityItemsIterate) {
                this.quantityItemsIterate = 20;
            }
            this.quantityItemsShown = this.quantityItemsIterate;
            if (_.has(this.$location.search(), "type")) {
                this.type = this.$location.search()["type"] as string;
            }
            if (!(Boolean(this.noInitialLoad) === true)) {
                this.loadCart(this.type);
            }
        }
        private loadCart(cartType: string): void {
            if (cartType !== this.type) {
                return;
            }
            this.cvCartService.loadCart(this.type, false, "miniCartShipping.loadCart")
                .then(() => this.finishRunning(),
                      result => this.finishRunning(true, result))
                .catch(reason => this.finishRunning(true, reason));
        }
        showMore(): void {
            this.quantityItemsShown = this.quantityItemsShown + this.quantityItemsIterate;
        }
        showLess(): void {
            this.quantityItemsShown = Math.max(
                this.quantityItemsIterate,
                this.quantityItemsShown - this.quantityItemsIterate);
        }
        modifiedSalePrice(item: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>): number {
            if (!item) { return 0; }
            return this.$filter("modifiedValue")(
                angular.isUndefined(item.UnitSoldPrice) || item.UnitSoldPrice === null
                    ? item.UnitCorePrice
                    : item.UnitSoldPrice,
                item.UnitSoldPriceModifier,
                item.UnitSoldPriceModifierMode);
        }
        addDiscountEnter($event: ng.IAngularEvent): void {
            if ($event["key"] === "Enter") {
                $event.preventDefault();
                $event.stopPropagation();
                this.addDiscount();
            }
        }
        addDiscount(): ng.IPromise<api.CEFActionResponse> {
            if (!this.discountCode) {
                this.cvMessageModalFactory(this.$translate("ui.store.cart.cart.AddDiscount.NoDiscountCode"));
                return this.$q.reject();
            }
            return this.$q((resolve, reject) => {
                this.cvApi.shopping.CurrentCartAddDiscount({
                    Code: this.discountCode
                }).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        if (r.data.Messages.length > 1) {
                            var dateVal = new Date(r.data.Messages[1]);
                            this.cvMessageModalFactory(r.data.Messages[0] + ' ' + dateVal.toString());
                        } else {
                            this.cvMessageModalFactory(r.data.Messages[0]);
                        }
                        reject(r.data);
                        return;
                    }
                    this.discountCode = null;
                    this.loadCurrentCart(true);
                    resolve(r.data);
                }).catch(reject);
            });
        }
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $window: ng.IWindowService,
                protected readonly $location: ng.ILocationService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvPurchaseService: services.IPurchaseService,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvMessageModalFactory: store.modals.IMessageModalFactory,
                protected readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                protected readonly cvStoreLocationService: services.IStoreLocationService,
                protected readonly cvInventoryService: services.IInventoryService,
                protected readonly cvFacebookPixelService: services.IFacebookPixelService,
                protected readonly cvGoogleTagManagerService: services.IGoogleTagManagerService,
                protected readonly cvSecurityService: services.ISecurityService,
                protected readonly cvAddressBookService: services.IAddressBookService,) {
            super($rootScope, $scope, $q, $filter, $window, $translate, cvApi,
                cefConfig, cvServiceStrings, cvCartService, cvMessageModalFactory, cvAuthenticationService, cvStoreLocationService,
                cvInventoryService, cvFacebookPixelService, cvGoogleTagManagerService, cvSecurityService, cvAddressBookService);
            this.load();
            const unbind1 = $scope.$on(this.cvServiceStrings.events.carts.loaded,
                (_: ng.IAngularEvent, cartType: string) => this.loadCart(cartType));
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefMiniCartConfirmation", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            /**
             * The type of cart
             * @type {string}
             */
            type: "=?",
            /**
             * The number of items to show before initiating paging
             * @type {number}
             * @default 20
             */
            quantityItemsIterate: "=?",
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
            /**
             * @type {boolean}
             * @default false
             */
            noInitialLoad: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/cart/views/miniCartConfirmation.html", "ui"),
        controller: MiniCartConfirmationController,
        controllerAs: "cvmcCtrl",
        bindToController: true
    }));
}
