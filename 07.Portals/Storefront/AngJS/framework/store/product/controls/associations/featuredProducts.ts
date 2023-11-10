/**
 * @file framework/store/product/controls/associations/featuredProducts.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc featured products directive
 */
 module cef.store.product.controls.associations {
    class FeaturedProductsController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        categoryId?: number;
        categoryKey?: string;
        // Properties
        quantity = 1;
        categories: api.MenuCategoryModel[] = [];
        products: api.ProductModel[] = [];
        // Functions
        load(): void {
            this.setRunning();
            this.cvCategoryService.get({
                id: this.categoryId,
                key: this.categoryKey,
            }).then(r => {
                if (!r) {
                    this.finishRunning(true, "No data returned");
                    return;
                }
                this.categories.push(r);
                // Load the first category's products
                this.grabProducts(this.categories[0].ID); // Will eventually call finishRunning
            }).catch(reason => this.finishRunning(true, reason));
        }
        setupWatchers(): void {
            if (angular.isDefined(this.$rootScope["globalBrandID"])) {
                return this.load();
            }
            let unbind1 = this.$rootScope.$on(this.cvServiceStrings.events.brands.globalBrandSiteDomainPopulated, () => this.load());
            this.$rootScope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
        grabProducts(categoryID: number): void {
            this.setRunning();
            this.categoryId = categoryID;
            var getProducts = {
                Active: true,
                AsListing: false,
                BrandID: this.$rootScope["globalBrandID"],
                IsVisible: true,
                HasAnyAncestorCategoryID: categoryID,
                Paging: { StartIndex: 1, Size: 4 },
                noCache: 1
            };
            this.cvApi.products.GetProducts(getProducts).then(r => {
                this.products = r.data.Results;
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        addCurrentItemToCart(quantity: number, item: any): void {
            this.cvCartService.addCartItem(
                item.ID,
                this.cvServiceStrings.carts.types.cart,
                this.quantity,
                <services.IAddCartItemParams>null,
                item);
        }
        addRelatedProductToCart(
            id: number,
            type: string = this.cvServiceStrings.carts.types.cart,
            quantity: number = 1,
            item: any)
            : void {
            this.cvCartService.addCartItem(id, type, quantity, <services.IAddCartItemParams>null, item);
        }
        addCurrentItemToWishList(item: any): void {
            this.setRunning();
            this.cvCartService.requireLoginForWishList(item.ID, true)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        removeCurrentItemFromWishList(item: any): void {
            this.setRunning();
            this.cvCartService.requireLoginForWishList(item.ID, false)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly cvCategoryService: services.CategoryService,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvCartService: services.ICartService) {
            super(cefConfig);
            if (!this.cefConfig.featureSet.brands.enabled) {
                this.load();
            }
            this.setupWatchers();
        }
    }

    cefApp.directive("cefFeaturedProducts", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            categoryId: "=?",
            categoryKey: "@?",
            template: "@?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/associations/featuredProducts.html", "ui"),
        controller: FeaturedProductsController,
        controllerAs: "featuredProductsCtrl",
        bindToController: true
    }));
}