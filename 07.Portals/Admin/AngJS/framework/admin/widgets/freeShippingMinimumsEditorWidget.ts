module cef.admin.api {
    export interface IHaveFreeShippingMinimumsBaseModel {
        /** Gets or sets the minimum for free shipping dollar amount. */
        MinimumForFreeShippingDollarAmount?: number;

        /** Gets or sets the minimum for free shipping dollar amount after. */
        MinimumForFreeShippingDollarAmountAfter?: number;

        /** Gets or sets a message describing the minimum for free shipping dollar amount warning. */
        MinimumForFreeShippingDollarAmountWarningMessage?: string;

        /** Gets or sets a message describing the minimum for free shipping dollar amount ignored accepted. */
        MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage?: string;

        /** Gets or sets the minimum for free shipping quantity amount. */
        MinimumForFreeShippingQuantityAmount?: number;

        /** Gets or sets the minimum for free shipping quantity amount after. */
        MinimumForFreeShippingQuantityAmountAfter?: number;

        /** Gets or sets a message describing the minimum for free shipping quantity amount warning. */
        MinimumForFreeShippingQuantityAmountWarningMessage?: string;

        /** Gets or sets a message describing the minimum for free shipping quantity amount ignored accepted. */
        MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage?: string;
    }
}

module cef.admin {
    class FreeShippingMinimumsEditorController extends core.TemplatedControllerBase {
        // Properties
        master: api.IHaveFreeShippingMinimumsBaseModel; // Bound by Scope
        debounce500 = { debounce: 500, blur: 0 };
        categories: api.CategoryModel[] = [];
        products: api.ProductModel[] = [];
        // Constructors
        constructor(
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.cvApi.categories.GetCategories({ Active: true, AsListing: true, Paging: { StartIndex: 1, Size: 50 }, IncludeChildrenInResults: false })
                .then(r => this.categories = r.data.Results);
            this.cvApi.products.GetProducts({ Active: true, AsListing: true, Paging: { StartIndex: 1, Size: 50 } }).then(r => this.products = r.data.Results);
        }
    }

    adminApp.directive("cefFreeShippingMinimumsEditor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { master: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/freeShippingMinimumsEditorWidget.html", "ui"),
        controller: FreeShippingMinimumsEditorController,
        controllerAs: "freeShippingMinimumsEditorWidgetCtrl",
        bindToController: true
    }));
}
