/**
 * @file framework/store/product/widgets/cards/productCard9StoresInventoryWidget.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #9: StoresIventory widget
 */
module cef.store.searchCatalog.controls.results {
    class StoresInventoryWidgetController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        productId: number;
        stores: api.StorePagedResults;
        // Properties
        get product(): api.ProductModel {
            return this.cvProductService.getCached({ id: this.productId });
        }
        catalogAttrList: api.GeneralAttributeModel[];
        // Functions
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvProductService: services.IProductService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefProductCardStoresInventoryWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            productId: "=?",
            stores: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/cardForProducts/productCard8StoresInventoryWidget.html", "ui"),
        controller: StoresInventoryWidgetController,
        controllerAs: "pcsiCtrl",
        bindToController: true
    }));
}
