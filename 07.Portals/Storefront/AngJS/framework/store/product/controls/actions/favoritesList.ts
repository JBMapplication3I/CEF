module cef.store.product.controls.actions {
    class ProductActionFavoritesListController extends ActionButtonControllerBase {
        // Bound Scope Properties
        // <None at this level>
        // Properties
        protected cartType = this.cvServiceStrings.carts.types.favorites;
        protected addFunc = () => this.cvCartService.requireLoginForFavorites(this.product.ID, true);
        protected removeFunc = () => this.cvCartService.requireLoginForFavorites(this.product.ID, false);
        protected addKey = "ui.storefront.common.Favorites.AddTo";
        protected removeKey = "ui.storefront.common.Favorites.RemoveFrom";
        protected addIndex = "AddToFavoritesList";
        protected removeIndex = "RemoveFromFavoritesList";
        // Functions
        // <None at this level>
        // Events
        // <None at this level>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig, // Used by UI
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCartService: services.ICartService) {
            super(cefConfig, cvServiceStrings, cvCartService);
        }
    }

    cefApp.directive("cefProductFavoritesList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "=" // The product to Check
        },
        replace: true, // Required for placement
        templateUrl: $filter("corsLink")("/framework/store/product/controls/actions/favoritesList.html", "ui"),
        controller: ProductActionFavoritesListController,
        controllerAs: "pflCtrl",
        bindToController: true
    }));
}
