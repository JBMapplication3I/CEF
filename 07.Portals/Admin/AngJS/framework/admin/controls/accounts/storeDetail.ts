/**
 * @file framework/admin/controls/accounts/storeDetail.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Store editor class for CEF Administrators.
 */
module cef.admin.controls.accounts {
    class StoreDetailController extends shared.AdminDetailHasProductAssociatorBase<api.StoreModel> {
        // Forced overrides
        detailName = "Store";
        // Collections
        userPaging: core.ServerSidePaging<api.UserModel, api.UserPagedResults>;
        inventoryLocationPaging: core.ServerSidePaging<api.InventoryLocationModel, api.InventoryLocationPagedResults>;
        types: api.TypeModel[] = [];
        imageTypes: api.TypeModel[] = [];
        languages: api.LanguageModel[] = [];
        currencies: api.CurrencyModel[] = [];
        storeInventoryLocationTypes: api.TypeModel[] = [];
        timeZones: api.TimeZoneInfo[] = [];
        users: api.UserModel[] = [];
        productCollectionPropertyName = "Products";
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 500 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.userPaging = new core.ServerSidePaging<api.UserModel, api.UserPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.contacts.GetUsers, 8, "users", "IDOrUserNameOrCustomKeyOrEmailOrContactName");
            this.inventoryLocationPaging = new core.ServerSidePaging<api.InventoryLocationModel, api.InventoryLocationPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.inventory.GetInventoryLocations, 8, "inventoryLocations");
            this.cvApi.stores.GetStoreTypes(standardDto).then(r => this.types = r.data.Results);
            this.cvApi.stores.GetStoreImageTypes(standardDto).then(r => this.imageTypes = r.data.Results);
            this.cvApi.stores.GetStoreInventoryLocationTypes(standardDto).then(r => this.storeInventoryLocationTypes = r.data.Results);
            this.cvApi.geography.GetTimeZonesList().then(r => this.timeZones = r.data);
            this.cvApi.globalization.GetLanguages(standardDto).then(r => this.languages = r.data.Results);
            this.cvApi.currencies.GetCurrencies(standardDto).then(r => this.currencies = r.data.Results);
            const unbind1 = this.$scope.$on(this.cvServiceStrings.events.attributes.changed,
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
            const unbind2 = this.$scope.$on(this.cvServiceStrings.events.associator.added,
                (
                    $event: ng.IAngularEvent,
                    master: api.StoreModel,
                    collection: string,
                    name: string,
                    added: api.AmARelationshipTableModel<api.BaseModel>
                ) => {
                    switch (name) {
                        case "accounts": { this.forms["Accounts"].$setDirty(); break; }
                        case "manufacturers": { this.forms["Manufacturers"].$setDirty(); break; }
                        case "vendors": { this.forms["Vendors"].$setDirty(); break; }
                        case "brands": { this.forms["Brands"].$setDirty(); break; }
                        case "badges": { this.forms["Badges"].$setDirty(); break; }
                        default: { this.forms["Details"].$setDirty(); break; }
                    }
                });
            const unbind3 = this.$scope.$on(this.cvServiceStrings.events.associator.removed,
                (
                    $event: ng.IAngularEvent,
                    master: api.StoreModel,
                    collection: string,
                    name: string,
                    removed: api.AmARelationshipTableModel<api.BaseModel>
                ) => {
                    switch (name) {
                        case "accounts": { this.forms["Accounts"].$setDirty(); break; }
                        case "manufacturers": { this.forms["Manufacturers"].$setDirty(); break; }
                        case "vendors": { this.forms["Vendors"].$setDirty(); break; }
                        case "brands": { this.forms["Brands"].$setDirty(); break; }
                        case "badges": { this.forms["Badges"].$setDirty(); break; }
                        default: { this.forms["Details"].$setDirty(); break; }
                    }
                });
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
                if (angular.isFunction(unbind3)) { unbind3(); }
            });
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.StoreModel> {
            this.record = <api.StoreModel>{
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
                // Related Objects
                ContactID: 0,
                Contact: this.createContactModel(),
                TypeID: 0,
                // Associated Objects
                Accounts: [],
                Brands: [],
                Images: [],
                Products: [],
                Manufacturers: [],
                StoreBadges: [],
                Users: [],
                Vendors: []
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> {
            this.detailName = "Store";
            return this.$q.resolve();
        }
        createRecordPreAction(toSend: api.StoreModel): ng.IPromise<api.StoreModel> {
            this.storeProductsToUpsert = toSend.Products;
            return this.$q.resolve(toSend);
        }
        updateRecordPreAction(toSend: api.StoreModel): ng.IPromise<api.StoreModel> {
            this.storeProductsToUpsert = toSend.Products;
            return this.$q.resolve(toSend);
        }
        loadRecordCall(id: number): ng.IHttpPromise<api.StoreModel> { return this.cvApi.stores.AdminGetStoreFull(id); }
        createRecordCall(routeParams: api.StoreModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.stores.CreateStore(routeParams); }
        updateRecordCall(routeParams: api.StoreModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.stores.UpdateStore(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.stores.DeactivateStoreByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.stores.ReactivateStoreByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.stores.DeleteStoreByID(id); }
        loadRecordActionAfterSuccess(result: api.StoreModel): ng.IPromise<api.StoreModel> {
            this.cvApi.contacts.GetUsers({ Active: true, AsListing: true, StoreID: result.ID, Paging: { Size: 50, StartIndex: 1 } })
                .then(r => this.users = r.data.Results);
            this.loadStoreProducts(result.ID);
            return this.$q.resolve(result);
        }
        createRecordActionAfterSuccess(result: api.CEFActionResponseT<number>): ng.IPromise<number> {
            if (!result.ActionSucceeded) {
                this.consoleLog(result);
                return this.$q.reject(result);
            }
            this.saveHook(result.Result);
            return this.$q.resolve(result.Result);
        }
        updateRecordActionAfterSuccess(result: api.CEFActionResponseT<number>): ng.IPromise<number> {
            if (!result.ActionSucceeded) {
                this.consoleLog(result);
                return this.$q.reject(result);
            }
            this.saveHook(result.Result).then(() => this.loadStoreProducts(result.Result));
            return this.$q.resolve(result.Result);
        }
        createStoreProduct(storeProduct: api.StoreProductModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.stores.CreateStoreProduct(storeProduct);
        }
        loadStoreProducts(id: number): void {
            // Store Products are specifically excluded from the store call due to performance
            // issues when thousands of products are assigned. Manually calling them here and
            // assigning onto the model
            this.cvApi.stores.GetStoreProducts({
                Active: true,
                AsListing: true,
                MasterID: id,
                Paging: <api.Paging>{ StartIndex: 1, Size: 50 }
            }).then(r => this.record.Products = r.data.Results);
        }
        // NOTE: This is a variable so it can override the base implementation more easily
        saveHook = (id: number): ng.IPromise<number> => {
            return this.$q((resolve, reject) => {
                this.deleteStoreProducts()
                    .then(() => this.upsertProductsToStore(id).then(() => resolve(id)).catch(reject))
                    .catch(reject);
            });
        }
        // Store Hours
        private hours: any[];
        get hoursList(): any[] {
            if (this.hours) { return this.hours; }
            const retVal: Array<any> = [];
            for (let i = 0; i < 24; i++) {
                for (let j = 0; j < 4; j++) {
                    const hour = this.$filter("zeroPadNumber")(i > 12 ? i - 12 : i, 2);
                    const minute = this.$filter("zeroPadNumber")(j * 15, 2);
                    const ampm = (i > 11 ? "PM" : "AM");
                    retVal.push({
                        display: `${hour}:${minute} ${ampm}`,
                        value: i + (j * 25 / 100)
                    });
                }
            }
            this.hours = retVal;
            return retVal;
        }
        // Inventory Location Management Events
        addInventoryLocation(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.inventoryLocationPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            if (!this.record.StoreInventoryLocations) {
                this.record.StoreInventoryLocations = [];
            }
            // Ensure it's not already in the collection
            if (_.find(this.record.StoreInventoryLocations, x => x.SlaveID === model.ID || x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.StoreInventoryLocations.push(<api.StoreInventoryLocationModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                SerializableAttributes: { },
                // IAmARelationshipTable Properties
                MasterID: this.record.ID,
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name,
                TypeID: 0
            });
            this.forms["Warehouses"].$setDirty();
        }
        removeInventoryLocation(index: number): void {
            this.record.StoreInventoryLocations.splice(index, 1);
            this.forms["Warehouses"].$setDirty();
        }
        // Store Product Management Events
        upsertStoreProduct(storeProduct: api.StoreProductModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.stores.UpsertStoreProduct(storeProduct);
        }
        storeProductsToUpsert: api.StoreProductModel[] = []
        upsertProductsToStore(storeID: number): ng.IPromise<any> {
            return this.$q.resolve(
                this.$q.all(
                    this.storeProductsToUpsert.map(sp => {
                        sp.MasterID = storeID;
                        return this.upsertStoreProduct(sp).then(r => {
                            return this.cvApi.stores.GetStoreProductByID(r.data.Result).then(r2 => {
                                return sp = r2.data;
                            });
                        });
                    })));
        }
        deleteStoreProducts(): ng.IPromise<any> {
            if (this.productsToRemove.length <= 0) {
                return this.$q.resolve();
            }
            return this.$q.resolve(this.$q.all(this.productsToRemove
                .filter(x => x.ID)
                .map(x => this.cvApi.stores.DeactivateStoreProductByID(x.ID))
            ).finally(() => { this.productsToRemove = []; }));
        }
        // User Management Events
        addUser(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.userPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            if (!this.record.Users) {
                this.record.Users = [];
            }
            // Ensure it's not already in the collection
            if (_.find(this.record.Users, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.Users.push(<api.StoreUserModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                SerializableAttributes: { },
                // IAmARelationshipTable Properties
                MasterID: this.record.ID,
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                UserName: model.UserName
            });
            this.forms["Users"].$setDirty();
        }
        removeUser(index: number): void {
            var removed = this.record.Users.splice(index, 1);
            if (this.users.length && this.users.indexOf(removed[0].Slave) !== -1) {
                this.users.splice(this.users.indexOf(removed[0].Slave), 1);
            }
            this.forms["Users"].$setDirty();
        }
        openAddNewUserModal(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/accounts/modals/accountAddNewUserModal.html", "ui"),
                controller: modals.AccountAddNewUserModalController,
                controllerAs: "aanumCtrl",
                size: this.cvServiceStrings.modalSizes.md
            }).result.then((newUser: api.UserModel) => {
                if (newUser) {
                    this.users.push(newUser);
                    this.record.Users.push(<api.StoreUserModel>{
                        ID: 0,
                        CustomKey: null,
                        CreatedDate: new Date(),
                        SerializableAttributes: { },
                        Slave: newUser
                    });
                    this.forms["Users"].$setDirty();
                }
            });
        }
        // Clone Methods (Store and Product)
        cloneProducts(): void {
            // Show Modal Pop-up
            // Allow selection of a store
            // Show products in that store
            // Allow addition of any selected items from that store to a list
            // When add products selected add those products to the current store
        }
        cloneStore(): void {
            // Deep clone store object
            var tempStore = this.record;
            // Remove keys, ids, etc. to prevent clashes
            tempStore.ID = null;
            tempStore.CustomKey = null;
            tempStore.Accounts = [];
            //tempStore.Products = [];
            tempStore.Manufacturers = [];
            tempStore.Vendors = [];
            tempStore.Users = [];
            //tempStore.Images = [];
            tempStore.Brands = [];
            tempStore.StoreBadges = [];
            // Assign cleaned store to current store and remove URL parameter for ID value
            //$location.search("id", null)
            // Put user on initial field of initial form necessary for clone updates before they save
            this.record = tempStore;
            this.cloneRecord(tempStore);
        }
        cloneRecord(toClone: api.StoreModel, state: string = null): void {
            this.setRunning(`Saving ${this.detailName}...`);
            this.createRecordPreAction(this.record).then(r1 => {
                this.createRecordCall(r1).then(r2 => {
                    this.cvApi.stores.GetStoreByID(r2.data.Result).then(r3 => {
                        this.record = r3.data;
                        this.createRecordActionAfterSuccess(r2.data).then(newID => {
                            if (state) {
                                this.$state.go(state);
                                return;
                            }
                            this.$stateParams.ID = newID;
                            toClone.ID = newID;
                            this.record = toClone;
                            // Replace Store Product IDs to match new store
                            if (!this.record.Products) {
                                this.record.Products = [];
                            }
                            this.record.Products.forEach(sp => {
                                sp.MasterID = toClone.ID;
                                sp.ID = null;
                                sp.CustomKey = null;
                                this.createStoreProduct(sp).then(r4 => {
                                    this.cvApi.stores.GetStoreProductByID(r4.data.Result).then(r5 => {
                                        sp = r5.data;
                                    });
                                });
                            });
                            this.updateRecordPreAction(this.record).then(r5 => {
                                this.updateRecordCall(r5).then(r6 => {
                                    this.cvApi.stores.GetStoreByID(r6.data.Result).then(r7 => {
                                        this.record = r7.data;
                                        this.updateRecordActionAfterSuccess(r6.data).then(r8 => {
                                            if (state) {
                                                this.$state.go(state);
                                                return;
                                            }
                                            Object.keys(this.forms).forEach(key => this.forms[key].$setPristine());
                                            this.finishRunning();
                                            const newState = this.$state.current;
                                            newState.params = this.$stateParams;
                                            this.$state.go(newState);
                                        }).catch(reason8 => this.finishRunning(true, reason8));
                                    }).catch(reason7 => this.finishRunning(true, reason7));
                                }).catch(reason6 => this.finishRunning(true, reason6));
                            }).catch(reason5 => this.finishRunning(true, reason5));
                        }).catch(reason4 => this.finishRunning(true, reason4));
                    }).catch(reason3 => this.finishRunning(true, reason3));
                }).catch(reason2 => this.finishRunning(true, reason2));
            }).catch(reason1 => this.finishRunning(true, reason1));
        }
        // Image Management Events V2
        removeImageNew(index: number): void {
            this.record.Images.splice(index, 1);
            this.forms["Images"].$setDirty();
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $state: ng.ui.IStateService,
                protected readonly $window: ng.IWindowService,
                protected readonly $q: ng.IQService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("storeDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/storeDetail.html", "ui"),
        controller: StoreDetailController,
        controllerAs: "storeEditorCtrl"
    }));
}
