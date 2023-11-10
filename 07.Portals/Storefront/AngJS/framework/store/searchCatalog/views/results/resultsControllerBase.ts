module cef.store.searchCatalog.views.results {
    export abstract class SearchCatalogResultsControllerBase extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <See inherited>
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
                protected readonly cefConfig: core.CefConfig, // Used by UI
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig);
        }
    }
}
