/**
 * @file framework/admin/controls/inventory/vendorDetail.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Vendor detail class
 */
module cef.admin.controls.inventory {
    class VendorDetailController extends shared.AdminDetailHasProductAssociatorBase<api.VendorModel> {
        // Forced overrides
        detailName = "Vendor";
        // Collections
        manufacturers: api.ManufacturerModel[] = [];
        productCollectionPropertyName = "Products";
        types: api.TypeModel[] = [];
        imageTypes: api.TypeModel[] = [];
        unbindAttributesChanged: Function;
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 50 };
            const standardDto = { Paging: paging, Active: true, AsListing: true };
            this.cvApi.manufacturers.GetManufacturers(standardDto).then(r => this.manufacturers = r.data.Results);
            this.cvApi.vendors.GetVendorTypes(standardDto).then(r => this.types = r.data.Results);
            this.cvApi.vendors.GetVendorImageTypes(standardDto).then(r => this.imageTypes = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.VendorModel> {
            this.record = <api.VendorModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // IHaveOrderMinimums Properties
                MinimumOrderDollarAmount: null,
                MinimumOrderDollarAmountAfter: null,
                MinimumOrderDollarAmountWarningMessage: null,
                MinimumOrderDollarAmountOverrideFee: null,
                MinimumOrderDollarAmountOverrideFeeIsPercent: false,
                MinimumOrderDollarAmountOverrideFeeWarningMessage: null,
                MinimumOrderQuantityAmount: null,
                MinimumOrderQuantityAmountAfter: null,
                MinimumOrderQuantityAmountWarningMessage: null,
                MinimumOrderQuantityAmountOverrideFee: null,
                MinimumOrderQuantityAmountOverrideFeeIsPercent: false,
                MinimumOrderQuantityAmountOverrideFeeWarningMessage: null,
                // Vendor Properties
                AllowDropShip: true,
                AccountNumber: null,
                Terms: null,
                TermNotes: null,
                SendMethod: null,
                EmailSubject: null,
                ShipTo: null,
                ShipViaNotes: null,
                SignBy: null,
                MustResetPassword: false,
                // Related Objects
                Contact: this.createContactModel(),
                TypeID: 0,
                // Associated Objects
                Manufacturers: [],
                Products: [],
                Accounts: [],
                Stores: [],
                Notes: []
            };
            this.listenToAttributes();
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Vendor"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.VendorModel> { return this.cvApi.vendors.GetVendorByID(id); }
        createRecordCall(routeParams: api.VendorModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.vendors.CreateVendor(routeParams); }
        updateRecordCall(routeParams: api.VendorModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.vendors.UpdateVendor(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.vendors.DeactivateVendorByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.vendors.ReactivateVendorByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.vendors.DeleteVendorByID(id); }
        loadRecordActionAfterSuccess(result: api.VendorModel): ng.IPromise<api.VendorModel> {
            this.listenToAttributes();
            if (result && !this.record.Contact) {
                result.Contact = this.createContactModel();
                return this.$q.resolve(result);
            }
            if (result && result.Contact && !result.Contact.Address && !result.Contact.AddressID) {
                result.Contact.Address = this.createAddressModel();
            }
            return this.$q.resolve(result);
        }
        // Supportive Functions
        private listenToAttributes() {
            this.unbindAttributesChanged = this.unbindAttributesChanged
                || this.$scope.$on(this.cvServiceStrings.events.attributes.changed,
                    ($event: ng.IAngularEvent, changedList: widgets.IAttributeChangedEventArg[]) => {
                        if (!this.forms || !this.forms["Attributes"]) {
                            return;
                        }
                        if (!_.some(changedList,
                                x => x.property == "Value"
                                && x.newValue !== null
                                && x.oldValue !== undefined)) {
                            return;
                        }
                        this.forms["Attributes"].$setDirty();
                    });
        }
        // Manufacturer Management Events
        addManufacturer(): void {
            if (!this.record.Manufacturers) { this.record.Manufacturers = []; }
            this.record.Manufacturers.push(<api.VendorManufacturerModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                VendorID: this.record.ID,
                ManufacturerID: 0
            });
            this.forms["VendorManufacturers"].$setDirty();
        }
        removeManufacturer(index: number): void {
            this.record.Manufacturers.splice(index, 1);
            this.forms["VendorManufacturers"].$setDirty();
        }

        // Image Management Events V2
        removeImageNew(index: number): void {
            this.record.Images.splice(index, 1);
            this.forms["Images"].$setDirty();
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $state: ng.ui.IStateService,
                protected readonly $window: ng.IWindowService,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory,
                protected readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(this.unbindAttributesChanged)) { this.unbindAttributesChanged(); }
            });
        }
    }

    adminApp.directive("vendorDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/inventory/vendorDetail.html", "ui"),
        controller: VendorDetailController,
        controllerAs: "vendorDetailCtrl"
    }));
}
