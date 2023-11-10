module cef.store.product.controls {
    class ProductCommonDataController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        product: api.ProductModel;
        // Properties
        // <None>
        // Functions
        getShippingLeadTimeText(): string {
            let isCalendarDays = this.product.ShippingLeadTimeIsCalendarDays;
            let isDayPlural = this.product.ShippingLeadTimeDays !== 1;
            return this.$translate.instant(
                isCalendarDays && isDayPlural ? "ui.storefront.product.detail.productDetail.LeadTimeDays.Template" :
                isCalendarDays ? "ui.storefront.product.detail.productDetail.LeadTimeDay.Template" :
                isDayPlural ? "ui.storefront.product.detail.productDetail.LeadTimeBusinessDays.Template" :
                "ui.storefront.product.detail.productDetail.LeadTimeBusinessDay.Template",
                { leadTimeDays: this.product.ShippingLeadTimeDays }) as string;
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefProductCommonData", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            product: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/commonData.html", "ui"),
        controller: ProductCommonDataController,
        controllerAs: "pdcdCtrl",
        bindToController: true
    }));
}
