/**
 * @file framework/store/cart/views/microCart.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Micro Cart directive class
 */
module cef.store.cart.views {
    class MicroCartController extends core.TemplatedControllerBase {
        // Properties
        private _type: string;
        get type(): string { return this._type; } // Bound from scope
        set type(value: string) {
            this._type = value;
            if (!this.cvCartService) { return; }
            this.cvCartService.loadCart(value, false, "microCart.type.set");
        }
        get microCartTitle(): string {
            if (this.cvCartService.accessCart(this.type) == null
                || this.cvCartService.accessCart(this.type).SalesItems.length === 0) {
                return this.type;
            }
            if (this.cvCartService.accessCart(this.type).SalesItems.length === 1) {
                return this.$translate.instant("ui.storefront.cart.No1Item");
            }
            return this.cvCartService.accessCart(this.type).SalesItems.length
                + " " + (this.$translate.instant("ui.storefront.cart.Item.Plural"));
        }
        get microCartTotal(): number {
            if (!this.cvCartService.accessCart(this.type)) {
                this.cvCartService.loadCart(this.type, false, "microCart.microCartTotal.get");
                return 0;
            }
            return this.cvCartService.accessCart(this.type)
                   && this.cvCartService.accessCart(this.type).SalesItems.length
                ? this.cvCartService.accessCart(this.type).Totals.Total
                : 0;
        }
        get microCartTotalItems(): number {
            return this.cvCartService.accessCart(this.type).SalesItems.length
                ? _.sumBy(this.cvCartService.accessCart(this.type).SalesItems,
                    x => (x.Quantity || 0) + (x.QuantityBackOrdered || 0) + (x.QuantityPreSold || 0))
                : 0;
        }
        get isSupervisor(): boolean { return this.cvSecurityService.hasRole("Supervisor") };
        // Functions
        // NOTE: This must remain an arrow function
        showPopover = (type: string): void => {
            // TODO: Swap between modal and toast message versions based on a setting
            if (type !== this.type) {
                return;
            }
            if ($(`[id="btnMicroCart_${this.type}"]`)) {
                $(`[id="btnMicroCart_${this.type}"]`)["popover"]("show");
                setTimeout(() => $(`[id="btnMicroCart_${this.type}"]`)["popover"]("hide"), 4000);
            }
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSecurityService: services.ISecurityService,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvCartService: services.ICartService) {
            super(cefConfig);
            cvCartService.loadCart(this.type, false, "microCart.ctor");
            const unbind1 = $scope.$on(cvServiceStrings.events.carts.itemAdded,
                (_e, type, product, addCartItemDto) => this.showPopover(type));
            const unbind2 = $scope.$on(cvServiceStrings.events.carts.itemsAdded,
                (_e, type, product, addCartItemDto) => this.showPopover(type));
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
    }

    cefApp.directive("cefMicroCart", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { type: "=" },
        templateUrl: $filter("corsLink")("/framework/store/cart/views/microCart.html", "ui"),
        controller: MicroCartController,
        controllerAs: "mcc",
        bindToController: true
    }));
}
