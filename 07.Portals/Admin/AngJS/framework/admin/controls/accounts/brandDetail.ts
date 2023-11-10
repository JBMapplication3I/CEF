/**
 * @file framework/admin/controls/accounts/brandDetail.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc brand detail class
 */
module cef.admin.controls.accounts {
    class BrandDetailController extends shared.AdminDetailHasProductAssociatorBase<api.BrandModel> {
        // Forced overrides
        detailName = "Brand";
        // Collections
        userPaging: core.ServerSidePaging<api.UserModel, api.UserPagedResults>;
        siteDomainPaging: core.ServerSidePaging<api.SiteDomainModel, api.SiteDomainPagedResults>;
        storePaging: core.ServerSidePaging<api.StoreModel, api.StorePagedResults>;
        inventoryLocationPaging: core.ServerSidePaging<api.InventoryLocationModel, api.InventoryLocationPagedResults>;
        languagePaging: core.ServerSidePaging<api.LanguageModel, api.LanguagePagedResults>;
        currencyPaging: core.ServerSidePaging<api.CurrencyModel, api.CurrencyPagedResults>;
        imageTypes: api.TypeModel[] = [];
        brandInventoryLocationTypes: api.TypeModel[] = [];
        users: api.UserModel[] = [];
        productCollectionPropertyName = "Products";
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 500 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.userPaging = new core.ServerSidePaging<api.UserModel, api.UserPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.contacts.GetUsers, 8, "users", "IDOrUserNameOrCustomKeyOrEmailOrContactName");
            this.siteDomainPaging = new core.ServerSidePaging<api.SiteDomainModel, api.SiteDomainPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.stores.GetSiteDomains, 8, "siteDomains");
            this.inventoryLocationPaging = new core.ServerSidePaging<api.InventoryLocationModel, api.InventoryLocationPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.inventory.GetInventoryLocations, 8, "inventoryLocations");
            this.languagePaging = new core.ServerSidePaging<api.LanguageModel, api.LanguagePagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.globalization.GetLanguages, 8, "languages");
            this.currencyPaging = new core.ServerSidePaging<api.CurrencyModel, api.CurrencyPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.currencies.GetCurrencies, 8, "currencies");
            this.storePaging = new core.ServerSidePaging<api.StoreModel, api.StorePagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.stores.GetStores, 8, "stores");
            this.cvApi.brands.GetBrandImageTypes(standardDto).then(r => this.imageTypes = r.data.Results);
            this.cvApi.brands.GetBrandInventoryLocationTypes(standardDto).then(r => this.brandInventoryLocationTypes = r.data.Results);
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
            const markDirty = (name: string): void => {
                switch (name) {
                    case "accounts": { this.forms["Accounts"].$setDirty(); break; }
                    case "manufacturers": { this.forms["Manufacturers"].$setDirty(); break; }
                    case "vendors": { this.forms["Vendors"].$setDirty(); break; }
                    case "brands": { this.forms["Brands"].$setDirty(); break; }
                    case "badges": { this.forms["Badges"].$setDirty(); break; }
                    case "currencies": { this.forms["Currencies"].$setDirty(); break; }
                    case "languages": { this.forms["Languages"].$setDirty(); break; }
                    case "stores": { this.forms["Stores"].$setDirty(); break; }
                    default: { this.forms["Details"].$setDirty(); break; }
                }
            }
            const unbind2 = this.$scope.$on(this.cvServiceStrings.events.associator.added, (
                    $event: ng.IAngularEvent,
                    master: api.StoreModel,
                    collection: string,
                    name: string,
                    added: api.AmARelationshipTableModel<api.BaseModel>
                ) => markDirty(name));
            const unbind3 = this.$scope.$on(this.cvServiceStrings.events.associator.removed, (
                    $event: ng.IAngularEvent,
                    master: api.StoreModel,
                    collection: string,
                    name: string,
                    removed: api.AmARelationshipTableModel<api.BaseModel>
                ) => markDirty(name));
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
                if (angular.isFunction(unbind3)) { unbind3(); }
            });
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.BrandModel> {
            this.record = <api.BrandModel>{
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
                // Associated Objects
                Accounts: [],
                BrandInventoryLocations: [],
                BrandSiteDomains: [],
                BrandCurrencies: [],
                BrandLanguages: [],
                Categories: [],
                Images: [],
                Notes: [],
                Products: [],
                Stores: [],
                Users: []
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> {
            this.detailName = "Brand";
            return this.$q.resolve();
        }
        createRecordPreAction(toSend: api.BrandModel): ng.IPromise<api.BrandModel> {
            this.brandProductsToUpsert = toSend.Products;
            return this.$q.resolve(toSend);
        }
        updateRecordPreAction(toSend: api.BrandModel): ng.IPromise<api.BrandModel> {
            this.brandProductsToUpsert = toSend.Products;
            return this.$q.resolve(toSend);
        }
        loadRecordCall(id: number): ng.IHttpPromise<api.BrandModel> {
            return this.cvApi.brands.GetBrandByID(id);
        }
        createRecordCall(routeParams: api.BrandModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.brands.CreateBrand(routeParams);
        }
        updateRecordCall(routeParams: api.BrandModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.brands.UpdateBrand(routeParams);
        }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.brands.DeactivateBrandByID(id);
        }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.brands.ReactivateBrandByID(id);
        }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.brands.DeleteBrandByID(id);
        }
        loadRecordActionAfterSuccess(result: api.BrandModel): ng.IPromise<api.BrandModel> {
            this.loadCategoryTree();
            this.cvApi.contacts.GetUsers({
                Active: true,
                AsListing: true,
                BrandID: result.ID,
                Paging: { Size: 50, StartIndex: 1 }
            }).then(r => this.users = r.data.Results);
            this.loadBrandProducts(result.ID);
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
            this.saveHook(result.Result).then(() => this.loadBrandProducts(result.Result));
            return this.$q.resolve(result.Result);
        }
        createBrandProduct(brandProduct: api.BrandProductModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.brands.CreateBrandProduct(brandProduct);
        }
        createBrandCategory(brandCategory: api.BrandCategoryModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.brands.CreateBrandCategory(brandCategory);
        }
        loadBrandProducts(id: number): void {
            // Brand Products are specifically excluded from the brand call due to performance
            // issues when thousands of products are assigned. Manually calling them here and
            // assigning onto the model
            this.cvApi.brands.GetBrandProducts({
                Active: true,
                AsListing: true,
                MasterID: id,
                Paging: <api.Paging>{ StartIndex: 1, Size: 50 }
            }).then(r => this.record.Products = r.data.Results);
        }
        // NOTE: This is a variable so it can override the base implementation more easily
        saveHook = (id: number): ng.IPromise<number> => {
            return this.$q((resolve, reject) => {
                this.deleteBrandProducts()
                    .then(() => this.upsertProductsToBrand(id)
                        .then(() => resolve(id))
                        .catch(reject))
                    .catch(reject);
            });
        }
        // Supportive Functions
        // <None>

        // Inventory Location Management Events
        addInventoryLocation(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.inventoryLocationPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            if (!this.record.BrandInventoryLocations) {
                this.record.BrandInventoryLocations = [];
            }
            // Ensure it's not already in the collection
            if (_.find(this.record.BrandInventoryLocations,
                    x => x.SlaveID === model.ID || x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.BrandInventoryLocations.push(<api.BrandInventoryLocationModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // IAmARelationshipTable Properties
                InventoryLocationID: model.ID,
                InventoryLocationKey: model.CustomKey,
                InventoryLocationName: model.Name,
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name,
                TypeID: 0
            });
            this.forms["Warehouses"].$setDirty();
        }
        removeInventoryLocation(toRemove: api.BrandInventoryLocationModel): void {
            for (let i = 0; i < this.record.BrandInventoryLocations.length; i++) {
                if (toRemove === this.record.BrandInventoryLocations[i]) {
                    this.record.BrandInventoryLocations.splice(i, 1);
                    this.forms["Warehouses"].$setDirty();
                    return;
                }
            }
        }
        // Site Domain Management Events
        addSiteDomain(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.siteDomainPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            if (!this.record.BrandSiteDomains) {
                this.record.BrandSiteDomains = [];
            }
            // Ensure it's not already in the collection
            if (_.find(this.record.BrandSiteDomains, x => x.SlaveID === model.ID || x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.BrandSiteDomains.push(<api.BrandSiteDomainModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // IAmARelationshipTable Properties
                SiteDomainID: model.ID,
                SiteDomainKey: model.CustomKey,
                SiteDomainName: model.Name,
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name
            });
            this.forms["SiteDomains"].$setDirty();
        }
        removeSiteDomain(toRemove: api.BrandSiteDomainModel): void {
            for (let i = 0; i < this.record.BrandSiteDomains.length; i++) {
                if (toRemove === this.record.BrandSiteDomains[i]) {
                    this.record.BrandSiteDomains.splice(i, 1);
                    this.forms["SiteDomains"].$setDirty();
                    return;
                }
            }
        }
        // Currency Management Events
        addCurrency(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.currencyPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            if (!this.record.BrandCurrencies) {
                this.record.BrandCurrencies = [];
            }
            // Ensure it's not already in the collection
            if (_.find(this.record.BrandCurrencies, x => x.SlaveID === model.ID || x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.BrandCurrencies.push(<api.BrandCurrencyModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // IAmARelationshipTable Properties
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name
            });
            this.forms["Currencies"].$setDirty();
        }
        removeCurrency(toRemove: api.BrandCurrencyModel): void {
            for (let i = 0; i < this.record.BrandCurrencies.length; i++) {
                if (toRemove === this.record.BrandCurrencies[i]) {
                    this.record.BrandCurrencies.splice(i, 1);
                    this.forms["Currencies"].$setDirty();
                    return;
                }
            }
        }
        // Language Management Events
        addLanguage(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.languagePaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            if (!this.record.BrandLanguages) {
                this.record.BrandLanguages = [];
            }
            // Ensure it's not already in the collection
            if (_.find(this.record.BrandLanguages,
                    x => x.SlaveID === model.ID || x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.BrandLanguages.push(<api.BrandLanguageModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // IAmARelationshipTable Properties
                LanguageID: model.ID,
                LanguageKey: model.CustomKey,
                SlaveID: model.ID,
                SlaveKey: model.CustomKey
            });
            this.forms["Languages"].$setDirty();
        }
        removeLanguage(toRemove: api.BrandLanguageModel): void {
            for (let i = 0; i < this.record.BrandLanguages.length; i++) {
                if (toRemove === this.record.BrandLanguages[i]) {
                    this.record.BrandLanguages.splice(i, 1);
                    this.forms["Languages"].$setDirty();
                    return;
                }
            }
        }
        // Store Mangement Events
        addStore(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.storePaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            if (!this.record.Stores) {
                this.record.Stores = [];
            }
            // Ensure it's not already in the collection
            if (_.find(this.record.Stores, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.Stores.push(<api.BrandStoreModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // Store
                StoreID: model.ID,
                StoreKey: model.CustomKey,
                StoreName: model.Name,
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name,
                IsVisibleIn: true,
            });
            this.forms["Stores"].$setDirty();
        }
        removeStore(toRemove: api.BrandStoreModel): void {
            for (let i = 0; i < this.record.Stores.length; i++) {
                if (toRemove === this.record.Stores[i]) {
                    this.record.Stores.splice(i, 1);
                    this.forms["Stores"].$setDirty();
                    return;
                }
            }
        }
        // Brand Product Management Events
        upsertBrandProduct(brandProduct: api.BrandProductModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.brands.UpsertBrandProduct(brandProduct);
        }
        brandProductsToUpsert: api.BrandProductModel[] = []
        upsertProductsToBrand(brandID: number): ng.IPromise<any> {
            if (!brandID) {
                return this.$q.reject("Cannot perform this action without the brand ID");
            }
            return this.$q.resolve(
                this.$q.all(
                    this.brandProductsToUpsert.map(sp => {
                        sp.MasterID = brandID;
                        return this.upsertBrandProduct(sp).then(r => {
                            this.cvApi.brands.GetBrandProductByID(r.data.Result).then(r2 => {
                                sp = r2.data;
                            });
                        });
                    })));
        }
        deleteBrandProducts(): ng.IPromise<any> {
            if (this.productsToRemove.length <= 0) {
                return this.$q.resolve();
            }
            return this.$q.resolve(this.$q.all(this.productsToRemove
                .filter(x => x.ID)
                .map(x => this.cvApi.brands.DeactivateBrandProductByID(x.ID))
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
            this.record.Users.push(<api.BrandUserModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // IAmARelationshipTable Properties
                SlaveID: model.ID,
                SlaveKey: model.CustomKey
            });
            this.forms["Users"].$setDirty();
        }
        removeUser(toRemove: api.BrandUserModel): void {
            for (let i = 0; i < this.record.Users.length; i++) {
                if (toRemove === this.record.Users[i]) {
                    this.record.Users.splice(i, 1);
                    this.forms["Users"].$setDirty();
                    return;
                }
            }
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
                    this.record.Users.push(<api.BrandUserModel>{
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
        // Category Management Events
        categoryTree: kendo.ui.TreeView;
        categoryTreeOptions = <kendo.ui.TreeViewOptions>{
            checkboxes: <kendo.ui.TreeViewCheckboxes>{
                checkChildren: true,
                // NOTE: This is a required inline template to work with Kendo
                template: `<input type="checkbox" disabled ng-model="dataItem.IsSelfSelected" />`
            },
            // NOTE: This is a required inline template to work with Kendo
            template: `<button type="button" ng-click="brandDetailCtrl.addCategory(dataItem)"`
                       + ` ng-disabled="dataItem.IsSelfSelected"`
                       + ` ng-class="{'disabled': dataItem.IsSelfSelected}"`
                       + ` class="btn btn-xs btn-success"><i class="far fa-fw fa-plus"></i><span class="sr-only">Add</span></button>`
                    + ` {{dataItem.ID | zeroPadNumber: 6}}: {{dataItem.Name}}<span ng-if="dataItem.CustomKey"> [{{dataItem.CustomKey}}]</span>`,
            loadOnDemand: true
        };
        selectedCategory: api.ProductCategorySelectorModel;
        editCategory: api.BrandCategoryModel;
        categorySelected(di/*: kendo.ui.TreeViewSelectEvent*/): void {
            this.selectedCategory = di;
        }
        categoryTreeData: kendo.data.HierarchicalDataSource;
        loadCategoryTree(): void {
            // Get all Categories with no parents, we'll lazy-load children afterward
            this.categoryTreeData = new kendo.data.HierarchicalDataSource({
                transport: <kendo.data.DataSourceTransport>{
                    read: (options => {
                        this.cvApi.categories.GetCategoryTree(<api.GetCategoryTreeDto>{
                            Active: true,
                            AsListing: true,
                            ParentID: (options.data as api.ProductCategorySelectorModel).ID || null,
                            IncludeChildrenInResults: false,
                            DisregardParents: false,
                            SelectedStoreID: this.record.ID || 0,
                            Sorts: [<api.Sort>{ dir: "asc", field: "Name", order: 0 }]
                        }).success(results => options.success(results));
                    }) as kendo.data.DataSourceTransportRead
                },
                schema: <kendo.data.HierarchicalDataSourceSchema>{
                    model: {
                        id: "ID",
                        hasChildren: "HasChildren"
                    }
                }
            });
        }
        addCategory(pcs: api.ProductCategorySelectorModel): void {
            if (!this.record.Categories) {
                this.record.Categories = [];
            }
            var existing = _.some(this.record.Categories, x => x.SlaveID === pcs.ID);
            if (existing) { return; } // Prevent Duplicates
            this.record.Categories.push(<api.BrandCategoryModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // IAmARelationshipTable Properties
                SlaveID: pcs.ID,
                SlaveKey: pcs.CustomKey,
                SlaveName: pcs.Name
            });
            this.forms["Categories"].$setDirty();
        }
        removeCategory(toRemove: api.BrandCategoryModel): void {
            for (let i = 0; i < this.record.Categories.length; i++) {
                if (toRemove === this.record.Categories[i]) {
                    this.record.Categories.splice(i, 1);
                    this.forms["Categories"].$setDirty();
                    return;
                }
            }
        }
        // Clone Methods (Store and Product)
        cloneProducts(): void {
            // Show Modal Pop-up
            // Allow selection of a store
            // Show products in that store
            // Allow addition of any selected items from that store to a list
            // When add products selected add those products to the current store
        }
        cloneBrand(): void {
            // Deep clone store object
            var tempBrand = this.record;
            // Remove keys, ids, etc. to prevent clashes
            tempBrand.ID = null;
            tempBrand.CustomKey = null;
            tempBrand.Accounts = [];
            //tempStore.Categories = [];
            //tempStore.Products = [];
            tempBrand.Users = [];
            tempBrand.BrandSiteDomains = [];
            //tempStore.Images = [];
            tempBrand.Stores = [];
            // Assign cleaned store to current store and remove URL parameter for ID value
            //$location.search("id", null)
            // Put user on initial field of initial form necessary for clone updates before they save
            this.record = tempBrand;
            this.cloneRecord(tempBrand);
        }
        cloneRecord(toClone: api.BrandModel, state: string = null): void {
            this.setRunning(`Saving ${this.detailName}...`);
            this.createRecordPreAction(this.record).then(r1 => {
                this.createRecordCall(r1).then(r2 => {
                    this.cvApi.brands.GetBrandByID(r2.data.Result).then(r3 => {
                        this.record = r3.data;
                        this.createRecordActionAfterSuccess(r2.data).then(newID => {
                            if (state) {
                                this.$state.go(state);
                                return;
                            }
                            this.$stateParams.ID = newID;
                            toClone.ID = newID;
                            this.record = toClone;
                            // Replace Brand Product IDs to match new brand
                            if (!this.record.Products) {
                                this.record.Products = [];
                            }
                            this.record.Products.forEach(sp => {
                                sp.MasterID = toClone.ID;
                                sp.ID = null;
                                sp.CustomKey = null;
                                this.createBrandProduct(sp).then(r4 => {
                                    this.cvApi.brands.GetBrandProductByID(r4.data.Result).then(r5 => {
                                        sp = r5.data;
                                    });
                                });
                            });
                            // Replace Brand Category IDs to match new brand
                            if (!this.record.Categories) {
                                this.record.Categories = [];
                            }
                            this.record.Categories.forEach(sc => {
                                sc.MasterID = toClone.ID;
                                sc.ID = null;
                                sc.CustomKey = null;
                                this.createBrandCategory(sc).then(r4 => {
                                    this.cvApi.brands.GetBrandCategoryByID(r4.data.Result).then(r5 => {
                                        sc = r5.data
                                    });
                                });
                            });
                            this.updateRecordPreAction(this.record).then(r5 => {
                                this.updateRecordCall(r5).then(r6 => {
                                    this.cvApi.brands.GetBrandByID(r6.data.Result).then(r7 => {
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

    adminApp.directive("brandDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/brandDetail.html", "ui"),
        controller: BrandDetailController,
        controllerAs: "brandDetailCtrl"
    }));
}
