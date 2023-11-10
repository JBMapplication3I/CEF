/**
 * @file framework/admin/controls/inventory/warehouseDetail.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Warehouse detail class
 */
module cef.admin.controls.inventory {
    class WarehouseDetailController extends DetailBaseController<api.InventoryLocationModel> {
        // Forced overrides
        detailName = "Warehouse";
        // Collections
        users: api.UserModel[] = [];
        // UI Data
        sectionName: string;
        sectionKey: string;
        // Required Functions
        loadNewRecord(): ng.IPromise<api.InventoryLocationModel> {
            this.record = <api.InventoryLocationModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CustomKey: null,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // Related Objects
                ContactID: 0,
                Contact: this.createContactModel(),
                // Associated Objects
                Sections: []
            };
            return this.$q.resolve(this.record);
        }
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 50 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            if (this.record.Users && this.record.Users.length) {
                this.cvApi.contacts.GetUsers({...standardDto, IDs: this.record.Users.map(x => x.SlaveID)}).then(r => {
                    this.users = r.data.Results;
                });
            }
            return this.$q.resolve();
        }
        createRecordPreAction(): ng.IPromise<api.InventoryLocationModel> {
            this.pushUsersToRecord();
            return this.$q.resolve(this.record);
        }
        updateRecordPreAction(): ng.IPromise<api.InventoryLocationModel> {
            this.pushUsersToRecord();
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Warehouse"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.InventoryLocationModel> { return this.cvApi.inventory.GetInventoryLocationByID(id); }
        createRecordCall(routeParams: api.InventoryLocationModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.inventory.CreateInventoryLocation(routeParams); }
        updateRecordCall(routeParams: api.InventoryLocationModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.inventory.UpdateInventoryLocation(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.inventory.DeactivateInventoryLocationByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.inventory.ReactivateInventoryLocationByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.inventory.DeleteInventoryLocationByID(id); }
        loadRecordActionAfterSuccess(result: api.InventoryLocationModel): ng.IPromise<api.InventoryLocationModel> {
            if (result && result.Users && result.Users.length) {
                this.cvApi.contacts.GetUsers({
                    Active: true,
                    AsListing: true,
                    Paging: { Size: 50, StartIndex: 1 },
                    IDs: result.Users.map(x => x.SlaveID)
                }).then(r => this.users = r.data.Results);
            }
            if (result && !result.Contact) {
                this.record.Contact = this.createContactModel();
                return this.$q.resolve(result);
            }
            if (result && result.Contact && !this.record.Contact.Address && !this.record.Contact.AddressID) {
                result.Contact.Address = this.createAddressModel();
            }
            return this.$q.resolve(result);
        }
        // Supportive Functions
        // <None>
        // Section Management events
        addSection(): void {
            if (!this.record.Sections) { this.record.Sections = []; }
            if (!this.sectionName || this.sectionName.length <= 0) {
                this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.warehouseDetail.SectionNameIsRequired"));
                return;
            }
            if (_.some(this.record.Sections, { Name: this.sectionName })) {
                this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.warehouseDetail.ThereIsAlreadyASectionWithName.Colon"));
                return;
            }
            if (this.sectionKey && _.some(this.record.Sections, { CustomKey: this.sectionKey })) {
                this.cvMessageModalFactory(this.$translate("ui.admin.controls.inventory.warehouseDetail.ThereIsAlreadyASectionWithKey.Colon"));
                return;
            }
            this.record.Sections.push(<api.InventoryLocationSectionModel>{
                Active: true,
                CustomKey: this.sectionKey,
                CreatedDate: new Date(),
                UpdatedDate: null,
                Name: this.sectionName,
                Description: null,
                InventoryLocationID: this.record.ID
            });
            this.sectionName = null;
            this.forms["Sections"].$setDirty();
        }
        deleteSection(index: number): void {
            this.record.Sections.splice(index, 1);
            this.forms["Sections"].$setDirty();
        }
        pushUsersToRecord(): void {
            this.record.Users = this.users.map(user => <api.InventoryLocationUserModel>{
                MasterID: this.record.ID,
                SlaveID: user.ID,
                Slave: user
            });
        }
        removeUser(index: number): void {
            this.users.splice(index, 1);
            this.forms["Users"].$setDirty();
        }
        openAddExistingUserModal(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/inventory/modals/warehouseAddExistingUserModal.html", "ui"),
                controller: modals.WarehouseAddExistingUserModalController,
                controllerAs: "waeumCtrl",
                size: this.cvServiceStrings.modalSizes.md
            }).result.then((existingUser: api.UserModel) => {
                if (existingUser) {
                    this.users.push(existingUser);
                    this.forms["Users"].$setDirty();
                }
            });
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
                private readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory,
                protected readonly cvMessageModalFactory: admin.modals.IMessageModalFactory,
                protected readonly $filter: ng.IFilterService,
                public readonly $uibModal: ng.ui.bootstrap.IModalService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("warehouseDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/inventory/warehouseDetail.html", "ui"),
        controller: WarehouseDetailController,
        controllerAs: "warehouseDetailCtrl"
    }));
}
