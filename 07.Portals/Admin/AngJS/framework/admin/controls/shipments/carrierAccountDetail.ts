/**
 * @file framework/admin/controls/shipping/carrierAccountDetail.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Carrier Account detail class
 */
module cef.admin.controls.shipments {
    class CarrierAccountDetailController extends DetailBaseController<api.ShipCarrierModel> {
        // Forced overrides
        detailName = "Carrier Account";
        // Collections
        // <None>
        // UI Data
        // <None>
        // Required Functions
        loadNewRecord(): ng.IPromise<api.ShipCarrierModel> {
            this.record = <api.ShipCarrierModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // ContactableBase Properties
                Phone: null,
                Fax: null,
                Email: null,
                // ShipCarrier Properties
                IsInbound: true,
                IsOutbound: true,
                // Related Objects
                ContactID: 0,
                Contact: this.createContactModel()
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Ship Carrier"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.ShipCarrierModel> { return this.cvApi.shipping.GetShipCarrierByID(id); }
        createRecordCall(routeParams: api.ShipCarrierModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.shipping.CreateShipCarrier(routeParams); }
        updateRecordCall(routeParams: api.ShipCarrierModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.shipping.UpdateShipCarrier(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.shipping.DeactivateShipCarrierByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.shipping.ReactivateShipCarrierByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.shipping.DeleteShipCarrierByID(id); }
        loadRecordActionAfterSuccess(result: api.ShipCarrierModel): ng.IPromise<api.ShipCarrierModel> {
            if (result && !result.Contact) {
                result.Contact = this.createContactModel();
                return this.$q.resolve(result);
            }
            if (result && result.Contact && !result.Contact.Address && !result.Contact.AddressID) {
                result.Contact.Address = this.createAddressModel();
            }
            return this.$q.resolve(result);
        }
        // Supportive Functions
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

    adminApp.directive("carrierAccountDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/shipments/carrierAccountDetail.html", "ui"),
        controller: CarrierAccountDetailController,
        controllerAs: "carrierAccountDetailCtrl"
    }));
}
