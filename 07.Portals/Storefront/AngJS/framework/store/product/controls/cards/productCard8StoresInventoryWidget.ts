/**
 * @file framework/store/product/controls/cards/productCard9StoresInventoryWidget.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #9: StoresIventory widget
 */
module cef.store.product.controls.cards {
    class StoresInventoryGenWidgetController extends core.TemplatedControllerBase {
        // Properties
        private stores: api.StorePagedResults;
        private _product: api.ProductModel;
        get product(): api.ProductModel {
            return this._product ||
                this.cvProductService.getCached({
                    id: this.productId,
                    name: this.productName,
                    seoUrl: this.productSeoUrl
                });
        }
        set product(value: api.ProductModel) { if (value) { this._product = value; } }
        productId: number; // Bound by Scope
        productSeoUrl: string; // Bound by Scope
        productName: string; // Bound by Scope
        catalogAttrList: api.GeneralAttributeModel[];
        // Functions
        private load(): void {
            if (this.product) { return; } // All good, already have the data
            if (!this.productId && !this.productName && !this.productSeoUrl) {
                console.warn("Cannot load product for attributes widget");
                return;
            }
            this.cvProductService.get({ id: this.productId, name: this.productName, seoUrl: this.productSeoUrl })
                .then(product => this.product = product);
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly $state: ng.ui.IStateService,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $timeout: ng.ITimeoutService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvSearchCatalogService: services.SearchCatalogService,
                private readonly cvProductService: services.IProductService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefProductCardStoresInventoryGenWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            product: "=?",
            productId: "=?",
            productSeoUrl: "=?",
            productName: "=?",
            stores: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCard8StoresInventoryWidget.html", "ui"),
        controller: StoresInventoryGenWidgetController,
        controllerAs: "pcsiCtrl",
        bindToController: true
    }));
}
