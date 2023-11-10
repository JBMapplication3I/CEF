module cef.admin.api {
    export interface IHaveOrderMinimumsBaseModel {
        /** Gets or sets the minimum order amount. */
        MinimumOrderDollarAmount?: number;

        /** Gets or sets the minimum order amount after. */
        MinimumOrderDollarAmountAfter?: number;

        /** Gets or sets a message describing the minimum order dollar amount warning. */
        MinimumOrderDollarAmountWarningMessage?: string;

        /** Gets or sets the minimum order dollar amount override fee. */
        MinimumOrderDollarAmountOverrideFee?: number;

        /** Gets or sets a value indicating whether the minimum order dollar amount override fee is percent. */
        MinimumOrderDollarAmountOverrideFeeIsPercent: boolean;

        /** Gets or sets a message describing the minimum order dollar amount override fee warning. */
        MinimumOrderDollarAmountOverrideFeeWarningMessage?: string;

        /** Gets or sets a message describing the minimum order dollar amount override fee accepted. */
        MinimumOrderDollarAmountOverrideFeeAcceptedMessage?: string;

        /** Gets or sets the minimum order quantity amount. */
        MinimumOrderQuantityAmount?: number;

        /** Gets or sets the minimum order quantity amount after. */
        MinimumOrderQuantityAmountAfter?: number;

        /** Gets or sets a message describing the minimum order quantity amount warning. */
        MinimumOrderQuantityAmountWarningMessage?: string;

        /** Gets or sets the minimum order quantity amount override fee. */
        MinimumOrderQuantityAmountOverrideFee?: number;

        /** Gets or sets a value indicating whether the minimum order quantity amount override fee is percent. */
        MinimumOrderQuantityAmountOverrideFeeIsPercent: boolean;

        /** Gets or sets a message describing the minimum order quantity amount override fee warning. */
        MinimumOrderQuantityAmountOverrideFeeWarningMessage?: string;

        /** Gets or sets a message describing the minimum order quantity amount override fee has been accepted. */
        MinimumOrderQuantityAmountOverrideFeeAcceptedMessage?: string;
    }
}

module cef.admin {
    class OrderMinimumsEditorController extends core.TemplatedControllerBase {
        // Properties
        master: api.IHaveOrderMinimumsBaseModel; // Bound by Scope
        categories: api.CategoryModel[] = [];
        products: api.ProductModel[] = [];
        // Constructors
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.cvApi.categories.GetCategories({ Active: true, AsListing: true, Paging: { StartIndex: 1, Size: 50 }, IncludeChildrenInResults: false })
                .then(r => this.categories = r.data.Results);
            this.cvApi.products.GetProducts({  Active: true, AsListing: true, Paging: { StartIndex: 1, Size: 50 } }).then(r => this.products = r.data.Results);
        }
    }

    adminApp.directive("cefOrderMinimumsEditor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { master: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/orderMinimumsEditorWidget.html", "ui"),
        controller: OrderMinimumsEditorController,
        controllerAs: "orderMinimumsEditorWidgetCtrl",
        bindToController: true
    }));
}
