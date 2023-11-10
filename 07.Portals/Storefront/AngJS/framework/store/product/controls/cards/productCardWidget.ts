/**
 * @file framework/store/product/controls/cards/productCard1NameWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Module, collects the separate sub-modules of the product
 * card together under one main control which can be repeated more easily.
 */
module cef.store.product.controls.cards {
    class ProductCardGenWidgetController extends ProductCardGenWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        index: number;
        thumbWidth: number;          // default: 250
        thumbHeight: number;         // default: 250
        nameRows: number;            // default: 2
        shortDescRows: number;       // default: 2
        pricingDisplayStyle: string; // default: "sideBySide"
        actionButtonView: string;    // default: "addToCart"
        hideStock: boolean;          // default: false
        hideSku: boolean;            // default: false
        hideShortDesc: boolean;      // default: false
        hideFavoritesList: boolean;  // default: false
        hideWishList: boolean;       // default: false
        hideNotifyMe: boolean;       // default: false
        hideCompare: boolean;        // default: false
        hideIcons: boolean;          // default: false
        hideCta: boolean;            // default: false
        quickAdd: boolean;           // default: true
        // Properties
        // <See inherited>
        // Convenience Redirects (Reduce binding text/conditions)
        get auth(): services.IAuthenticationService {
            return this && this.cvAuthenticationService;
        }
        get service(): services.SearchCatalogService {
            return this && this.cvSearchCatalogService;
        }
        protected get thumbW(): number {
            return Number(this.thumbWidth)
                || 250; // Default
        }
        protected get thumbH(): number {
            return Number(this.thumbHeight)
                || 250; // Default
        }
        protected get nameR(): number {
            return Number(this.nameRows)
                || 2; // Default
        }
        protected get shortDescR(): number {
            return Number(this.shortDescRows)
                || 2; // Default
        }
        protected get pds(): string {
            return this.pricingDisplayStyle
                || "sideBySide"; // Default
        }
        protected get abv(): string {
            return this.actionButtonView
                || "addToCart"; // Default
        }
        protected get hideS(): boolean {
            return !this.cefConfig.featureSet.inventory.enabled
                || Boolean(this.hideStock)
                || this.cefConfig.loginForInventory.enabled
                && !this.cvAuthenticationService.isAuthenticated()
                || false; // Default
        }
        protected get hideK(): boolean {
            return Boolean(this.hideSku)
                || false; // Default
        }
        protected get hideD(): boolean {
            return Boolean(this.hideShortDesc)
                || false; // Default
        }
        protected get quickA(): boolean {
            return Boolean(this.quickAdd)
                || !(this.pds == 'false'); // the template had it like this
                //|| true; // Default
        }
        protected get hideFL(): boolean {
            return !this.cefConfig.featureSet.carts.favoritesList.enabled
                || Boolean(this.hideFavoritesList)
                || false; // Default
        }
        protected get hideWL(): boolean {
            return !this.cefConfig.featureSet.carts.wishList.enabled
                || Boolean(this.hideWishList)
                || false; // Default
        }
        protected get hideNM(): boolean {
            return !this.cefConfig.featureSet.carts.notifyMeWhenInStock.enabled
                || Boolean(this.hideNotifyMe)
                || false; // Default
        }
        protected get hideC(): boolean {
            return !this.cefConfig.featureSet.carts.compare.enabled
                || Boolean(this.hideCompare)
                || false; // Default
        }
        protected get hideI(): boolean {
            return Boolean(this.hideIcons)
                || (this.hideFL && this.hideWL && this.hideNM && this.hideC)
                || false; // Default
        }
        // Functions
        // <See inherited>
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService,
                protected readonly cvProductService: services.IProductService) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
            this.load();
        }
    }

    cefApp.directive("cefProductCardGenWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=?",
            productId: "=?",
            productSeoUrl: "=?",
            productName: "=?",
            index: "=",
            thumbWidth: "=?", // Default 250
            thumbHeight: "=?", // Default 250
            nameRows: "=?", // Default 2
            hideStock: "=?", // Default undefined == false
            hideSku: "=?", // Default undefined == false
            hideShortDesc: "=?", // Default undefined == false
            shortDescRows: "=?", // Default 2
            pricingDisplayStyle: "=?", // Default 'sideBySide'
            quickAdd: "=?", // Default true
            actionButtonView: "=?", // Default 'addToCart'
            hideFavoritesList: "=?", // Default undefined == false
            hideWishList: "=?", // Default undefined == false
            hideNotifyMe: "=?", // Default undefined == false
            hideCompare: "=?", // Default undefined == false
            hideIcons: "=?", // Default undefined == false
            hideCta: "=?" // Default undefined == false
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCardWidget.html", "ui"),
        controller: ProductCardGenWidgetController,
        controllerAs: "scprgCtrl",
        bindToController: true
    }));
}
