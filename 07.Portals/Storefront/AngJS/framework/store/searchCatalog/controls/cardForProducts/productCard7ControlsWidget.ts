/**
 * @file framework/store/searchCatalog/controls/cardForProducts/productCard7ControlsWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #7: Controls widget
 */
module cef.store.searchCatalog.controls.results {
    export class ProductCardControlsWidgetController extends ProductCardWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        hideFavoritesList: boolean;
        hideWishList: boolean;
        hideNotifyMe: boolean;
        hideCompare: boolean;
        hideIcons: boolean;
        // Convenience Redirects (Reduce binding text/conditions)
        // <See inherited>
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
        // Properties
        // <See inherited>
        // Functions
        // <See inherited>
        addToWishList(): void {
            this.cvCartService.requireLoginForWishList(this.product.ID, true);
        }
        removeFromWishList(): void {
            this.cvCartService.requireLoginForWishList(this.product.ID, false);
        }
        get isInWishList(): boolean {
            if (!this.product) { return false; }
            return this.cvCartService.cartContainsItem(this.product.ID,
                this.cvServiceStrings.carts.types.wishList);
        }
        addToFavoritesList(): void {
            this.cvCartService.requireLoginForFavorites(this.product.ID, true);
        }
        removeFromFavoritesList(): void {
            this.cvCartService.requireLoginForFavorites(this.product.ID, false);
        }
        get isInFavoritesList(): boolean {
            if (!this.product) { return false; }
            return this.cvCartService.cartContainsItem(this.product.ID,
                this.cvServiceStrings.carts.types.favorites);
        }
        addToNotifyMeList(): void {
            this.cvCartService.requireLoginForNotifyMe(this.product.ID, true);
        }
        removeFromNotifyMeList(): void {
            this.cvCartService.requireLoginForNotifyMe(this.product.ID, false);
        }
        get isInNotifyMeList(): boolean {
            if (!this.product) { return false; }
            return this.cvCartService.cartContainsItem(this.product.ID,
                this.cvServiceStrings.carts.types.notifyMe);
        }
        addToCompare(): void {
            this.cvSearchCatalogProductCompareService.addItem(this.product.ID)
                .then(() => { });
        }
        removeFromCompare(): void {
            this.cvSearchCatalogProductCompareService.removeItem(this.product.ID)
                .then(() => { });
        }
        get isInCompareCart(): boolean {
            if (!this.product) { return false; }
            return this.cvSearchCatalogProductCompareService.isInCompareCart(this.product.ID);
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService,
                protected readonly cvSearchCatalogProductCompareService: services.ISearchCatalogProductCompareService,
                protected readonly cvProductService: services.IProductService) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
        }
    }

    cefApp.directive("cefProductCardControlsWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            product: "=?",
            productId: "=?",
            productSeoUrl: "=?",
            productName: "=?",
            hideFavoritesList: "=?",
            hideWishList: "=?",
            hideNotifyMe: "=?",
            hideCompare: "=?",
            hideIcons: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/cardForProducts/productCard7ControlsWidget.html", "ui"),
        controller: ProductCardControlsWidgetController,
        controllerAs: "pccwCtrl",
        bindToController: true
    }));
}
