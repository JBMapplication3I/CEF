/**
 * @file framework/store/cart/controls/quickAdd/quickAddToCart.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Quick Add to Cart widget
 */
module cef.store.cart.controls.quickAdd {
    class QuickAddToCartController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        type: string;
        flyoutCount: number;
        // Properties
        protected product: api.SuggestResultBase;
        protected quantity: number;
        protected suggestions: Array<api.SuggestResultBase> = [];
        get isSupervisor(): boolean { return this.cvSecurityService.hasRole("Supervisor") };
        // Functions
        protected add(): void {
            if (this.viewState.running || !this.product || !this.product.ID) {
                return;
            }
            if (!this.isSupervisor && this.type === "Cart") {
                return;
            }
            this.setRunning();
            if (!this.suggestions || !this.suggestions.length) {
                this.finishRunning();
                return;
            }
            let fullProduct = null;
            this.cvProductService.get({ id: this.product.ID })
                .then(p => fullProduct = p)
                .finally(() => {
                    this.cvCartService.addCartItem(
                        this.product.ID,
                        this.type,
                        this.quantity || 1,
                        { forceNoModal: true },
                        fullProduct
                    ).then(() => {
                        this.cvCartService.viewstate.checkoutIsProcessing = true;
                        this.product = null;
                        this.quantity = null;
                        this.finishRunning();
                    }).catch(reason => this.finishRunning(true, reason));
                });
        }
        getSuggestionsPromise(viewValue: string): ng.IPromise<api.SuggestResultBase[]> {
            this.setRunning();
            return this.$q<api.SuggestResultBase[]>((resolve, reject) => {
                if (!viewValue || !viewValue.trim()) {
                    this.suggestions = [];
                    this.finishRunning();
                    resolve([]);
                    return;
                }
                const dto = <api.SuggestProductCatalogWithProviderDto>{
                    Query: viewValue,
                    Page: 1,
                    PageSize: this.flyoutCount || 8,
                    PageSetSize: 1,
                    Sort: api.SearchSort.Relevance,
                };
                if (this.cefConfig.featureSet.stores.enabled) {
                    dto.StoreID = this.cvSearchCatalogService.activeSearchViewModel.Form.StoreID
                        || this.cefConfig.catalog.onlyApplyStoreToFilterByUI
                        ? this.cvSearchCatalogService.filterByStoreID
                        : this.cvStoreLocationService.getUsersSelectedStore()
                            && this.cvStoreLocationService.getUsersSelectedStore().ID;
                }
                this.setRunning();
                this.cvApi.providers.SuggestProductCatalogWithProvider(dto).then(r => {
                    this.suggestions = r.data;
                    this.finishRunning();
                    resolve(this.suggestions);
                }).catch(reason => { this.finishRunning(); reject(reason); } );
            });
        }
        protected isValid(): boolean {
            return this.suggestions
                && this.suggestions.length
                && this.product
                && !this.viewState.running;
        }
        protected inputKeyPress(event: JQueryKeyEventObject): void {
            if (event.key !== "Enter") {
                // Only do anything if it was the enter key
                return;
            }
            event.preventDefault();
            event.stopPropagation();
            this.add();
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvCartService: services.ICartService,
                private readonly cvStoreLocationService: services.IStoreLocationService,
                private readonly cvSearchCatalogService: services.SearchCatalogService,
                private readonly cvProductService: services.IProductService,
                private readonly cvSecurityService: services.ISecurityService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefQuickAddToCart", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            type: "=", // The type of cart
            flyoutCount: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/cart/controls/quickAdd/quickAddToCart.html", "ui"),
        controller: QuickAddToCartController,
        controllerAs: "qatcCtrl",
        bindToController: true
    }));
}
