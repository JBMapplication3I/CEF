module cef.admin.controls.sales {
    export class ShippingDetailController extends core.TemplatedControllerBase {
        // Properties
        id: number;

        constructor(
                private readonly $stateParams: ng.ui.IStateParamsService,
                private readonly $location: ng.ILocationService,
                readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.id = this.$stateParams.ID;
        }
    }

    adminApp.directive("shippingDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/shippingDetail.html", "ui"),
        controller: ShippingDetailController,
        controllerAs: "shippingDetailCtrl"
    }));
}
