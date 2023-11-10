/**
 * @file framework/admin/controls/inventory/manufacturerDetail.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Manufacturer detail class
 */
module cef.admin.controls.inventory {
    class ManufacturerDetailController extends shared.AdminDetailHasProductAssociatorBase<api.ManufacturerModel> {
        // Forced overrides
        detailName = "Manufacturer";
        // Collections
        productCollectionPropertyName = "Products";
        stores: api.StoreModel[] = [];
        vendors: api.VendorModel[] = [];
        imageTypes: api.TypeModel[] = [];
        // UI Data
        // <None>
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 50 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.cvApi.stores.GetStores(standardDto).then(r => this.stores = r.data.Results);
            this.cvApi.vendors.GetVendors(standardDto).then(r => this.vendors = r.data.Results);
            this.cvApi.manufacturers.GetManufacturerImageTypes(standardDto).then(r => this.imageTypes = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.ManufacturerModel> {
            this.record = <api.ManufacturerModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                Hash: null,
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
                // Related Objects
                ContactID: 0,
                Contact: this.createContactModel(),
                TypeID: 0,
                // Associated Objects
                Products: [],
                Vendors: [],
                Stores: [],
                Notes: [],
                Reviews: [],
                Images: []
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Manufacturer"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.ManufacturerModel> { return this.cvApi.manufacturers.GetManufacturerByID(id); }
        createRecordCall(routeParams: api.ManufacturerModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.manufacturers.CreateManufacturer(routeParams); }
        updateRecordCall(routeParams: api.ManufacturerModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.manufacturers.UpdateManufacturer(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.manufacturers.DeactivateManufacturerByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.manufacturers.ReactivateManufacturerByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.manufacturers.DeleteManufacturerByID(id); }
        loadRecordActionAfterSuccess(result: api.ManufacturerModel): ng.IPromise<api.ManufacturerModel> {
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

        // Vendor Management Events
        addVendor(): void {
            if (!this.record.Vendors) { this.record.Vendors = []; }
            this.record.Vendors.push(<api.VendorManufacturerModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                ManufacturerID: this.record.ID,
                VendorID: 0
            });
            this.forms["VendorManufacturers"].$setDirty();
        }
        removeVendor(index: number): void {
            this.record.Vendors.splice(index, 1);
            this.forms["VendorManufacturers"].$setDirty();
        }

        // Store Management Events
        addStore(): void {
            if (!this.record.Stores) { this.record.Stores = []; }
            this.record.Stores.push(<api.StoreManufacturerModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                ManufacturerID: this.record.ID,
                StoreID: 0
            });
            this.forms["Stores"].$setDirty();
        }
        removeStore(index: number): void {
            this.record.Stores.splice(index, 1);
            this.forms["Stores"].$setDirty();
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
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory,
                protected readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("manufacturerDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/inventory/manufacturerDetail.html", "ui"),
        controller: ManufacturerDetailController,
        controllerAs: "manufacturerDetailCtrl"
    }));
}
