module cef.admin.controls.shipments {
    /*
    interface ShippingDiscountModel extends api.BaseModel, api.UpsertShippingDiscountDto { 
    }
    class ShippingDiscountDetailController extends DetailBaseController<ShippingDiscountModel> {
        // Collections
        types: api.TypeModel[] = [];
        shipCarrierMethods: api.ShipCarrierMethodModel[];
        // UI Data
        // <None>
        // Required Functions
        // Override of saveRecord
        saveRecord(): void {
            this.record.DiscountRate = this.record.DiscountRate / 100;
            super.saveRecord(this.$state.current.name, { OptionName: this.record.OptionName });
        }

        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 50 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.cvApi.shipping.GetShipCarrierMethods(standardDto).then(r => this.shipCarrierMethods = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<ShippingDiscountModel> {
            this.record = <ShippingDiscountModel>{
                /** The name of the option to apply the discount to. * /
                OptionName: null,
                /** The discount to apply to the given shipping option. * /
                DiscountRate: null,
            };
            return this.$q.resolve(this.record);
        }
        loadRecordActionAfterSuccess(result: ShippingDiscountModel) {
            let mapped = Object.keys(result).map(function(key){
                return { OptionName: key, Value: result[key] };
            })[0];
            this.record = { ...mapped, Active: true, UpdatedDate: null, CreatedDate: null, DiscountRate: mapped.Value * 100 }
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Discount"; this.$stateParams.Key = this.$stateParams.OptionName; return this.$q.resolve(); }
        loadRecordCall(id: any): ng.IHttpPromise<any> { return this.cvApi.shipping.GetShippingDiscountByKey(id); }
        createRecordCall(routeParams: ShippingDiscountModel): ng.IHttpPromise<any> { return this.cvApi.shipping.UpsertShippingDiscount(routeParams); }
        updateRecordCall(routeParams: ShippingDiscountModel): ng.IHttpPromise<any> { return this.cvApi.shipping.UpsertShippingDiscount(routeParams); }
        deactivateRecordCall(id: any): ng.IHttpPromise<any> { return this.cvApi.shipping.DeactivateShippingDiscountsByKey(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<any> { return null; }
        deleteRecordCall(id: number): ng.IHttpPromise<any> { return null; }
        // Supportive Functions
        logGetShippingDiscounts(): void {
            this.cvApi.shipping.GetShippingDiscounts().then(r => {
                console.log('this.cvApi.shipping.GetShippingDiscounts()\n', r);
            })
        }
        // <None>
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $state: ng.ui.IStateService,
                protected readonly $window: ng.IWindowService,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvConfirmModalFactory: modals.IConfirmModalFactory,
                protected readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("shippingDiscountDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/shipments/shippingDiscountDetail.html", "ui"),
        controller: ShippingDiscountDetailController,
        controllerAs: "shippingDiscountDetailCtrl"
    }));
    */
}
