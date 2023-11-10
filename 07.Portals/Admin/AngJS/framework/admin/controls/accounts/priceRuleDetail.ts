/**
 * @file framework/admin/controls/accounts/priceRuleDetail.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Price rule detail class
 */
module cef.admin.controls.accounts {
    class PriceRuleDetailController extends DetailBaseController<api.PriceRuleModel> {
        // Forced overrides
        detailName = "Price Rule";
        // Collections
        // <None>
        // UI Data
        accountPaging: core.ServerSidePaging<api.AccountModel, api.AccountPagedResults>;
        accountTypePaging: core.ServerSidePaging<api.TypeModel, api.AccountTypePagedResults>;
        categoryPaging: core.ServerSidePaging<api.CategoryModel, api.CategoryPagedResults>;
        countryPaging: core.ServerSidePaging<api.CountryModel, api.CountryPagedResults>;
        manufacturerPaging: core.ServerSidePaging<api.ManufacturerModel, api.ManufacturerPagedResults>;
        productPaging: core.ServerSidePaging<api.ProductModel, api.ProductPagedResults>;
        productTypePaging: core.ServerSidePaging<api.TypeModel, api.ProductTypePagedResults>;
        storePaging: core.ServerSidePaging<api.StoreModel, api.StorePagedResults>;
        vendorPaging: core.ServerSidePaging<api.VendorModel, api.VendorPagedResults>;
        roles: cefalt.admin.Dictionary<number>;
        currencies: api.CurrencyModel[];
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            this.accountPaging = new core.ServerSidePaging<api.AccountModel, api.AccountPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.accounts.GetAccounts, 8, "accounts");
            this.accountTypePaging = new core.ServerSidePaging<api.TypeModel, api.AccountTypePagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.accounts.GetAccountTypes, 8, "accountTypes");
            this.categoryPaging = new core.ServerSidePaging<api.CategoryModel, api.CategoryPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.categories.GetCategories, 8, "categories");
            this.countryPaging = new core.ServerSidePaging<api.CountryModel, api.CountryPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.geography.GetCountries, 8, "countries");
            this.manufacturerPaging = new core.ServerSidePaging<api.ManufacturerModel, api.ManufacturerPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.manufacturers.GetManufacturers, 8, "manufacturers");
            this.productPaging = new core.ServerSidePaging<api.ProductModel, api.ProductPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.products.GetProducts, 8, "products");
            this.productTypePaging = new core.ServerSidePaging<api.TypeModel, api.ProductTypePagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.products.GetProductTypes, 8, "productTypes");
            this.storePaging = new core.ServerSidePaging<api.StoreModel, api.StorePagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.stores.GetStores, 8, "stores");
            this.vendorPaging = new core.ServerSidePaging<api.VendorModel, api.VendorPagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.vendors.GetVendors, 8, "vendors");
            this.cvApi.authentication.GetRoles().then(r => this.roles = r.data);
            this.cvApi.currencies.GetCurrencies({ Active: true, AsListing: true }).then(r => this.currencies = r.data.Results);
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.PriceRuleModel> {
            this.record = <api.PriceRuleModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // Price Rule Properties
                CustomCurrency: null,
                StartDate: null,
                EndDate: null,
                IsExclusive: false,
                IsMarkup: false,
                IsOnlyForAnonymousUsers: false,
                IsPercentage: true,
                PriceAdjustment: null,
                UsePriceBase: true,
                Priority: null,
                UnitOfMeasure: null,
                // Related Objects
                CurrencyID: 0,
                CurrencyKey: null,
                CurrencyName: null,
                Currency: null,
                // Associated Objects
                Accounts: [],
                Manufacturers: [],
                Products: [],
                Stores: [],
                Vendors: [],
                PriceRuleAccountTypes: [],
                PriceRuleCategories: [],
                PriceRuleCountries: [],
                PriceRuleProductTypes: [],
                PriceRuleUserRoles: [],
            }
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "Price Rules"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.PriceRuleModel> { return this.cvApi.pricing.GetPriceRuleByID(id); }
        createRecordCall(routeParams: api.PriceRuleModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.pricing.CreatePriceRule(routeParams); }
        updateRecordCall(routeParams: api.PriceRuleModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.pricing.UpdatePriceRule(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.pricing.DeactivatePriceRuleByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.pricing.ReactivatePriceRuleByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.pricing.DeletePriceRuleByID(id); }
        loadRecordActionBeforeAssign(result: api.PriceRuleModel): ng.IPromise<api.PriceRuleModel> {
            this.fixDates(result);
            return this.$q.resolve(result);
        }
        fixDates = (result: api.PriceRuleModel): void => {
            if (result.StartDate as any as string === "0001-01-01T00:00:00.0000000") {
                result.StartDate = null;
            } else if (result.StartDate) {
                const tmpFrom = new Date(result.StartDate.toString());
                tmpFrom.setTime(tmpFrom.getTime() + tmpFrom.getTimezoneOffset()*60*1000);
                result.StartDate = tmpFrom;
            }
            if (result.EndDate as any as string === "0001-01-01T00:00:00.0000000") {
                result.EndDate = null;
            } else if (result.EndDate) {
                const tmpFrom = new Date(result.EndDate.toString());
                tmpFrom.setTime(tmpFrom.getTime() + tmpFrom.getTimezoneOffset()*60*1000);
                result.EndDate = tmpFrom;
            }
        }

        // Serializable Attributes Rounding Value

        get RoundingValue(): number {
            let retvalue: string;
            if (this.record && this.record.SerializableAttributes && this.record.SerializableAttributes['RoundingValue'].Value) retvalue = this.record.SerializableAttributes['RoundingValue'].Value;
            return parseInt(retvalue);
        }
        set RoundingValue(value: number) {
            if (!this.record.SerializableAttributes) {
                this.record.SerializableAttributes = new api.SerializableAttributesDictionary;
            }
            if (!this.record.SerializableAttributes["RoundingValue"]) {
                this.record.SerializableAttributes["RoundingValue"] = <api.SerializableAttributeObject>{
                    ID: this.record.ID
                }
            }
            this.record.SerializableAttributes['RoundingValue'].Value = value.toString();
        }

        maxPriceCeiling(): void {
            if (this.forms && this.forms.Details && this.forms.Details["numberInputFormGroupCtrl.forms.MaxQuantity"]) {
                (<HTMLInputElement>this.$window.document.getElementById("nudMaxQuantity")).value = "2147483647"; // Maxint
            }
        }

        // Supportive Functions
        addAccount(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.accountPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            // Ensure it's not already in the collection
            if (_.find(this.record.Accounts, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.Accounts.push(<api.PriceRuleAccountModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: this.record.ID,
                // Account
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name
            });
            this.forms["Accounts"].$setDirty();
        }
        removeAccount(index: number): void {
            this.record.Accounts.splice(index, 1);
            this.forms["Accounts"].$setDirty();
        }
        addAccountType(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.accountTypePaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            // Ensure it's not already in the collection
            if (_.find(this.record.PriceRuleAccountTypes, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.PriceRuleAccountTypes.push(<api.PriceRuleAccountTypeModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // Account Type
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name
            });
            this.forms["AccountTypes"].$setDirty();
        }
        removeAccountType(index: number): void {
            this.record.PriceRuleAccountTypes.splice(index, 1);
            this.forms["AccountTypes"].$setDirty();
        }
        addCategory(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.categoryPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            // Ensure it's not already in the collection
            if (_.find(this.record.PriceRuleCategories, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.PriceRuleCategories.push(<api.PriceRuleCategoryModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // Category
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name
            });
            this.forms["Categories"].$setDirty();
        }
        removeCategory(index: number): void {
            this.record.PriceRuleCategories.splice(index, 1);
            this.forms["Categories"].$setDirty();
        }
        addCountry(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.countryPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            // Ensure it's not already in the collection
            if (_.find(this.record.PriceRuleCountries, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.PriceRuleCountries.push(<api.PriceRuleCountryModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // Country
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name
            });
            this.forms["Countries"].$setDirty();
        }
        removeCountry(index: number): void {
            this.record.PriceRuleCountries.splice(index, 1);
            this.forms["Countries"].$setDirty();
        }
        addManufacturer(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.manufacturerPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            // Ensure it's not already in the collection
            if (_.find(this.record.Manufacturers, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.Manufacturers.push(<api.PriceRuleManufacturerModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // Manufacturer
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name
            });
            this.forms["Manufacturers"].$setDirty();
        }
        removeManufacturer(index: number): void {
            this.record.Manufacturers.splice(index, 1);
            this.forms["Manufacturers"].$setDirty();
        }
        addProduct(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.productPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            // Ensure it's not already in the collection
            if (_.find(this.record.Products, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.Products.push(<api.PriceRuleProductModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // Product
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name,
            });
            this.forms["Products"].$setDirty();
        }
        removeProduct(index: number): void {
            this.record.Products.splice(index, 1);
            this.forms["Products"].$setDirty();
        }
        addProductType(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.productTypePaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            // Ensure it's not already in the collection
            if (_.find(this.record.PriceRuleProductTypes, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.PriceRuleProductTypes.push(<api.PriceRuleProductTypeModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // Product Type
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name
            });
            this.forms["ProductTypes"].$setDirty();
        }
        removeProductType(index: number): void {
            this.record.PriceRuleProductTypes.splice(index, 1);
            this.forms["ProductTypes"].$setDirty();
        }
        addStore(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.storePaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            // Ensure it's not already in the collection
            if (_.find(this.record.Stores, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.Stores.push(<api.PriceRuleStoreModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // Store
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name,
            });
            this.forms["Stores"].$setDirty();
        }
        removeStore(index: number): void {
            this.record.Stores.splice(index, 1);
            this.forms["Stores"].$setDirty();
        }
        addUserRole(name: string): void {
            if (!name) { return; }
            // Ensure the data is loaded
            if (!this.roles[name]) { return; }
            // Ensure it's not already in the collection
            if (_.find(this.record.PriceRuleUserRoles, x => x.RoleName === name)) {
               return;
            }
            // Add it
            this.record.PriceRuleUserRoles.push(<api.PriceRuleUserRoleModel>{
               // Base Properties
               Active: true,
               CreatedDate: new Date(),
               PriceRuleID: 0,
               // User Role
               RoleName: name,
            });
            this.forms["UserRoles"].$setDirty();
        }
        removeUserRole(index: number): void {
            this.record.PriceRuleUserRoles.splice(index, 1);
            this.forms["UserRoles"].$setDirty();
        }
        addVendor(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.vendorPaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            // Ensure it's not already in the collection
            if (_.find(this.record.Vendors, x => x.SlaveID === model.ID)) {
                return;
            }
            // Add it
            this.record.Vendors.push(<api.PriceRuleVendorModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                MasterID: 0,
                // Vendor
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model.Name
            });
            this.forms["Vendors"].$setDirty();
        }
        removeVendor(index: number): void {
            this.record.Vendors.splice(index, 1);
            this.forms["Vendors"].$setDirty();
        }
        // Constructor
        constructor(
                public readonly $scope: ng.IScope,
                public readonly $rootScope: ng.IRootScopeService,
                public readonly $translate: ng.translate.ITranslateService,
                public readonly $stateParams: ng.ui.IStateParamsService,
                public readonly $state: ng.ui.IStateService,
                public readonly $window: ng.IWindowService,
                public readonly $filter: ng.IFilterService,
                public readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                public readonly cvServiceStrings: services.IServiceStrings,
                public readonly cvApi: api.ICEFAPI,
                public readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("priceRuleDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/priceRuleDetail.html", "ui"),
        controller: PriceRuleDetailController,
        controllerAs: "priceRuleDetailCtrl"
    }));
}
