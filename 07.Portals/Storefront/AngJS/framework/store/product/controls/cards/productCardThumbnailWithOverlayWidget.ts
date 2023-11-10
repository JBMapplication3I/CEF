module cef.store.product.controls.cards {
    class ProductCardThumbnailWithOverlayWidgetController extends ProductCardThumbnailWidgetController {
        // Bound Scope Properties
        // <See inherited>
        hideFavoritesList: boolean;
        hideWishList: boolean;
        hideNotifyMe: boolean;
        hideCompare: boolean;
        hideIcons: boolean;
        modalIsOpen: boolean;
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
            this.setRunning();
            if (!this.cvSearchCatalogProductCompareService.isInCompareCart(this.product.ID)) {
                this.cvSearchCatalogProductCompareService.addItem(this.product.ID)
                    .then(() => { this.finishRunning() })
                    .catch(err => this.finishRunning(true, err))
            }
        }
        removeFromCompare(): void {
            this.setRunning();
            if (this.cvSearchCatalogProductCompareService.isInCompareCart(this.product.ID)) {
                this.cvSearchCatalogProductCompareService.removeItem(this.product.ID)
                    .then(() => { this.finishRunning() })
                    .catch(err => this.finishRunning(true, err))
            }
        }
        get isInCompareCart(): boolean {
            if (!this.product) { return false; }
            return this.cvSearchCatalogProductCompareService.isInCompareCart(this.product.ID);
        }
        hideOverlayIfVariantMaster(): void {
            if (!this.product) { return; }
            if (this.product.TypeName
                && this.product.TypeName.toLowerCase() == "variant master") {
                this.hideFavoritesList = true;
                this.hideNotifyMe = true;
                this.hideCompare = true;
                this.hideWishList = true;
            }
        }
        // Constructor
        constructor(
                protected readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvSearchCatalogProductCompareService: services.ISearchCatalogProductCompareService,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvCategoryService: services.ICategoryService) {
            super($filter, cefConfig, cvProductService, cvCategoryService);
            this.hideOverlayIfVariantMaster();
        }
    }

    cefApp.directive("cefProductCardThumbnailWithOverlayWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=?",
            productId: "=?",
            productSeoUrl: "=?",
            productName: "=?",
            productImage: "=?",
            width: "=",
            height: "=",
            overrideImageRoot: "@?",
            withMargin: "=?",
            isCategory: "=?",
            noLink: "=?",
            index: "=",
            hideWishList: "=?",
            hideFavoritesList: "=?",
            hideCompare: "=?",
            hideNotifyMe: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCardThumbnailWithOverlayWidget.html", "ui"),
        controller: ProductCardThumbnailWithOverlayWidgetController,
        controllerAs: "pctwwoCtrl",
        bindToController: true
    }));
}
