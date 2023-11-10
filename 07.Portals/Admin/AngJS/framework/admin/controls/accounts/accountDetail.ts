/**
 * @file framework/admin/controls/accounts/accountDetail.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Account Editor detail class for CEF Administrators.
 */
module cef.admin.controls.accounts {
    class AssociatedAccountSearchParams { AccountKey: string; AccountName: string; }
    interface AssociatedAccountListViewModel extends api.AccountModel { Selected?: boolean; Quantity?: number; }
    class AccountDetailDisplaySetting { ShowAssociatedAccountPanel: boolean; }

    class AccountDetailController extends DetailBaseController<api.AccountModel> {
        // Forced overrides
        detailName = "Account";
        // Collections
        types: api.TypeModel[] = [];
        imageTypes: api.TypeModel[] = [];
        associationTypes: api.TypeModel[] = [];
        statuses: api.StatusModel[] = [];
        pricePoints: api.PricePointModel[] = [];
        stores: api.StoreModel[] = [];
        vendors: api.VendorModel[] = [];
        brands: api.BrandModel[] = [];
        currencies: api.CurrencyModel[] = [];
        users: api.UserModel[] = [];
        usersToRemove: api.UserModel[] = [];
        associatedAccounts: AssociatedAccountListViewModel[] = [];
        rolesForAccount: api.RoleForAccountModel[] = [];
        // UI Data
        accountPricePointID: number = null;
        associationSearchParams: AssociatedAccountSearchParams = { AccountKey: null, AccountName: null };
        display: AccountDetailDisplaySetting = { ShowAssociatedAccountPanel: false };
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 100 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.cvApi.currencies.GetCurrencies(standardDto).then(r => this.currencies = r.data.Results);
            this.cvApi.accounts.GetAccountTypes(standardDto).then(r => this.types = r.data.Results);
            this.cvApi.accounts.GetAccountAssociationTypes(standardDto).then(r => this.associationTypes = r.data.Results);
            this.cvApi.accounts.GetAccountStatuses(standardDto).then(r => this.statuses = r.data.Results);
            this.cvApi.accounts.GetAccountImageTypes(standardDto).then(r => this.imageTypes = r.data.Results);
            this.cvApi.pricing.GetPricePoints(standardDto).then(r => {
                this.pricePoints = r.data.Results;
                this.accountPricePointID = this.record.AccountPricePoints != null
                        && this.record.AccountPricePoints.length > 0
                    ? this.record.AccountPricePoints[0].SlaveID
                    : null;
            });
            this.cvApi.stores.GetStores(standardDto).then(r => this.stores = r.data.Results);
            this.cvApi.vendors.GetVendors(standardDto).then(r => this.vendors = r.data.Results);
            this.cvApi.brands.GetBrands(standardDto).then(r => {
                this.brands = r.data.Results;
                if (!this.brands.length) {
                    return;
                }
                //* TODO: Wrap this in a setting
                if (!this.record.Brands) {
                    this.record.Brands = [];
                }
                this.brands.forEach(x => {
                    const found = _.find(this.record.Brands, y => y.MasterID === x.ID);
                    if (found) {
                        return;
                    }
                    this.record.Brands.push(<api.BrandAccountModel>{
                        // Base Properties
                        ID: 0,
                        CustomKey: null,
                        Active: true,
                        CreatedDate: new Date(),
                        UpdatedDate: null,
                        //
                        MasterID: x.ID,
                        SlaveID: this.record.ID || null,
                        //
                        IsVisibleIn: true
                    });
                });
                // */
            });
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.AccountModel> {
            this.accountPricePointID = null;
            this.record = <api.AccountModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // Account Properties
                IsTaxable: true,
                TaxEntityUseCode: null,
                TaxExemptionNo: null,
                IsOnHold: false,
                // Related Objects
                TypeID: 0,
                Type: null,
                StatusID: 0,
                Status: null,
                // Associated Objects
                Images: [],
                Stores: [],
                Vendors: [],
                Brands: [],
                Users: [],
                Notes: [],
                AccountContacts: null, // Keep this null, address book service will handle the records
                AccountPricePoints: [],
                AccountAssociations: [],
                AccountsAssociatedWith: [],
                AccountCurrencies: []
            };
            this.rolesForAccount = [];
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> {
            this.detailName = "Account";
            return this.$q.resolve();
        }
        createRecordPreAction(): ng.IPromise<api.AccountModel> {
            this.applyPricePoint();
            this.updateTaxability();
            this.record.AccountContacts = null; // Keep this null, address book service will handle the records
            return this.$q.resolve(this.record);
        }
        updateRecordPreAction(): ng.IPromise<api.AccountModel> {
            this.applyPricePoint();
            this.updateTaxability();
            this.record.AccountContacts = null; // Keep this null, address book service will handle the records
            return this.$q.resolve(this.record);
        }
        loadRecordCall(id: number): ng.IHttpPromise<api.AccountModel> { return this.cvApi.accounts.GetAccountByID(id); }
        createRecordCall(routeParams: api.AccountModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.accounts.CreateAccount(routeParams); }
        updateRecordCall(routeParams: api.AccountModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.accounts.UpdateAccount(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.accounts.DeactivateAccountByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.accounts.ReactivateAccountByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.accounts.DeleteAccountByID(id); }
        loadRecordActionAfterSuccess(result: api.AccountModel): ng.IPromise<api.AccountModel> {
            this.cvApi.authentication.GetRolesForAccount(result.ID).success(results => this.rolesForAccount = results);
            this.cvApi.contacts.GetUsers({ Active: true, AsListing: true, AccountID: result.ID, Paging: { Size: 50, StartIndex: 1 } })
                .then(r => this.users = r.data.Results);
            this.record.AccountContacts = null; // Keep this null, address book service will handle the records
            return this.$q((resolve, reject) => {
                this.cvAddressBookService.getBook(result.ID, true)
                    .then(() => resolve(result)).catch(reject);
            });
        }
        createRecordActionAfterSuccess(result: api.CEFActionResponseT<number>): ng.IPromise<number> {
            this.deleteUsers();
            return this.$q((resolve, reject) => {
                this.addUserToAccount(result.Result).then(_usersUpdated => {
                    this.forms["Users"].$setDirty();
                    this.cvAddressBookService.getBook(result.Result, true)
                        .then(() => resolve(result.Result)).catch(reject);
                }).catch(reject);
            });
        }
        updateRecordActionAfterSuccess(result: api.CEFActionResponseT<number>): ng.IPromise<number> {
            this.deleteUsers();
            return this.$q((resolve, reject) => {
                this.addUserToAccount(result.Result).then(_usersUpdated => {
                    this.forms["Users"].$setDirty();
                    this.cvAddressBookService.getBook(result.Result, true)
                        .then(() => resolve(result.Result)).catch(reject);
                }).catch(reject);
            });
        }
        // Supportive Functions

        // User Management Events
        addUserToAccount(accountID: number): ng.IPromise<any> {
            let changed = false;
            return this.$q.all(this.users.map(user => {
                let thisUserChanged = false;
                if (this.brands.length) {
                    if (!user.Brands) {
                        user.Brands = [];
                    }
                    //* TODO: Wrap in a setting
                    this.brands.forEach(x => {
                        const found = _.find(user.Brands, y => y.MasterID === x.ID);
                        if (found) {
                            return; // don't add twice
                        }
                        thisUserChanged = true;
                        user.Brands.push(<api.BrandUserModel>{
                            // Base Properties
                            ID: 0,
                            CustomKey: null,
                            Active: true,
                            CreatedDate: new Date(),
                            UpdatedDate: null,
                            //
                            MasterID: x.ID,
                            SlaveID: 0,
                            //
                            HasAccessToBrand: true
                        });
                    });
                    // */
                }
                if (user.AccountID !== accountID) {
                    user.AccountID = accountID;
                    thisUserChanged = true;
                }
                if (thisUserChanged) {
                    changed = true;
                    if (user.ID > 0) {
                        return this.cvApi.contacts.UpdateUser(user);
                    } else {
                        return this.cvApi.contacts.CreateUser(user);
                    }
                } else {
                    return this.$q.resolve();
                }
            }));
        }
        removeUser(index: number): void {
            const user = this.users[index];
            this.cvConfirmModalFactory(
                this.$translate("ui.admin.controls.accounts.usersTab.removeUser.Message"))
            .then(r => {
                if (r) {
                    if (user.ID > 0) {
                        this.usersToRemove.push(user);
                    }
                    this.users.splice(index, 1);
                    this.forms["Users"].$setDirty();
                }
            });
        }
        deleteUsers(): void {
            if (this.usersToRemove.length <= 0) { return; }
            this.$q.all(this.usersToRemove.map(user =>
                this.cvApi.contacts.DeactivateUserByID(user.ID)))
                    .finally(() => {});
        }
        openAddNewUserModal(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/accounts/modals/accountAddNewUserModal.html", "ui"),
                controller: modals.AccountAddNewUserModalController,
                controllerAs: "aanumCtrl",
                size: this.cvServiceStrings.modalSizes.md,
                resolve: {
                    users: () => this.users
                }
            }).result.then((newUser: api.UserModel) => {
                if (newUser) {
                    this.users.push(newUser);
                    this.forms["Users"].$setDirty();
                }
            });
        }
        openAddExistingUserModal(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/accounts/modals/accountAddExistingUserModal.html", "ui"),
                controller: modals.AccountAddExistingUserModalController,
                controllerAs: "aaeumCtrl",
                size: this.cvServiceStrings.modalSizes.md,
                resolve: {
                    users: () => this.users
                }
            }).result.then(() => this.forms["Users"].$setDirty());
        }

        // Store Management Events
        addStore(): void {
            if (!this.record.Stores) { this.record.Stores = []; }
            this.record.Stores.push(<api.StoreAccountModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                MasterID: 0,
                SlaveID: this.record.ID,
                //
                HasAccessToStore: true
            });
            this.forms["Stores"].$setDirty();
        }
        removeStore(index: number): void {
            this.record.Stores.splice(index, 1);
            this.forms["Stores"].$setDirty();
        }

        // Store Management Events
        addVendor(): void {
            if (!this.record.Vendors) { this.record.Vendors = []; }
            this.record.Vendors.push(<api.VendorAccountModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                MasterID: 0,
                SlaveID: this.record.ID,
            });
            this.forms["Vendors"].$setDirty();
        }
        removeVendor(index: number): void {
            this.record.Vendors.splice(index, 1);
            this.forms["Vendors"].$setDirty();
        }

        // Brand Management Events
        addBrand(): void {
            if (!this.record.Brands) {
                this.record.Brands = [];
            }
            this.record.Brands.push(<api.BrandAccountModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                //
                MasterID: 0,
                SlaveID: this.record.ID,
                //
                IsVisibleIn: true
            });
            this.forms["Brands"].$setDirty();
        }
        removeBrand(index: number): void {
            this.record.Brands.splice(index, 1);
            this.forms["Brands"].$setDirty();
        }

        // Currency Management Events
        addCurrency(): void {
            if (!this.record.AccountCurrencies) {
                this.record.AccountCurrencies = [];
            }
            this.record.AccountCurrencies.push(<api.AccountCurrencyModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // IAmARelationshipTable Properties
                MasterID: this.record.ID,
                MasterKey: null,
                Master: null,
                SlaveID: 0,
                SlaveKey: null,
                Slave: null,
                //
                CustomName: null,
                IsPrimary: this.record.AccountCurrencies.length === 0,
                OverrideUnicodeSymbolValue: null
            });
            this.forms["Currencies"].$setDirty();
        }
        removeCurrency(index: number): void {
            this.record.AccountCurrencies.splice(index, 1);
            this.forms["Currencies"].$setDirty();
        }

        // Price Point Management Events
        addPricePoint(): void {
            this.record.AccountPricePoints.push(<api.AccountPricePointModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // IAmARelationshipTable Properties
                MasterID: this.record.ID,
                MasterKey: null,
                Master: null,
                SlaveID: this.accountPricePointID,
                SlaveKey: null,
                Slave: null,
            });
            this.forms["PricePoints"].$setDirty();
        }
        applyPricePoint(): void {
            var accountHasPricePoint = this.record.AccountPricePoints != null
                && this.record.AccountPricePoints.length > 0;
            var uiHasPricePointSelected = this.accountPricePointID != null;
            if (!accountHasPricePoint && !uiHasPricePointSelected) {
                // Do Nothing, they match as both null
                return;
            }
            if (accountHasPricePoint && !uiHasPricePointSelected) {
                // Deactivate the existing price point
                this.record.AccountPricePoints[0].Active = false;
            }
            if (!accountHasPricePoint && uiHasPricePointSelected) {
                // We need to add a new price point to the account
                if (this.record.AccountPricePoints == null) {
                    // Initialize array if null
                    this.record.AccountPricePoints = [];
                }
                this.addPricePoint();
                return;
            }
            if (accountHasPricePoint && uiHasPricePointSelected) {
                if (this.record.AccountPricePoints[0].SlaveID === this.accountPricePointID) {
                    // They match, no need to do anything
                    return;
                }
                // They don't match, remove the current one and add a new one
                this.record.AccountPricePoints.splice(0, 1);
                this.addPricePoint();
            }
        }
        updateTaxability(): void {
            if (this.record.IsTaxable) {
                this.record.TaxExemptionNo = null;
                this.record.TaxEntityUseCode = null;
            }
        }

        // Associated Account Management Events
        showAddAssociatedAccountsPanel(): void {
            this.associatedAccounts = [];
            this.display.ShowAssociatedAccountPanel = true;
        }
        hideAddAssociatedAccountsPanel(): void {
            this.display.ShowAssociatedAccountPanel = false;
        }
        searchAssociatedAccounts(): void {
            var notIDs: number[] = [];
            notIDs.push(this.record.ID);
            if (this.record.AccountAssociations && this.record.AccountAssociations.length)
            {
                this.record.AccountAssociations
                    .map(x => x.SlaveID)
                    .forEach(x => notIDs.push(x));
            }
            this.cvApi.accounts.GetAccounts({
                Active: true,
                AsListing: true,
                Name: this.associationSearchParams.AccountName || null,
                CustomKey: this.associationSearchParams.AccountKey || null,
                Paging: <api.Paging>{ Size: 20, StartIndex: 1 },
                NotIDs: notIDs
            }).then(r => this.associatedAccounts = r.data.Results);
        }
        addAssociatedAccounts(): void {
            if (!this.record.AccountAssociations) {
                this.record.AccountAssociations = [];
            }
            _.filter(this.associatedAccounts, x => x.Selected)
                .forEach(x => this.addAssociatedAccount(x));
            this.hideAddAssociatedAccountsPanel();
            this.forms["Associations"].$setDirty();
        }
        addAssociatedAccount(toAdd: api.AccountModel): void {
            if (toAdd.ID === this.record.ID) {
                // Can't add to self
                return;
            }
            if (_.some(this.record.AccountAssociations, x => x.SlaveID === toAdd.ID)) {
                // Already in list
                return;
            }
            const found = _.find(this.associatedAccounts, x => x.ID === toAdd.ID);
            if (!found) {
                // No data, shouldn't have been able to check it
                return;
            }
            this.record.AccountAssociations.push(<api.AccountAssociationModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                TypeID: this.associationTypes.length ? this.associationTypes[0].ID : null,
                PrimaryAccountID: this.record.ID,
                AssociatedAccountID: toAdd.ID,
                AssociatedAccountKey: toAdd.CustomKey,
                AssociatedAccountName: toAdd.Name,
                SlaveID: toAdd.ID,
                SlaveKey: toAdd.CustomKey,
                SlaveName: toAdd.Name
            });
            this.forms["Associations"].$setDirty();
        }
        removeAssociatedAccount(index: number): void {
            this.record.AccountAssociations.splice(index, 1);
            this.forms["Associations"].$setDirty();
        }
        // Image & Document Management Events
        removeImageNew(index: number): void {
            this.record.Images.splice(index, 1);
            this.forms["Images"].$setDirty();
        }
        removeFileNew(index: number): void {
            this.record.StoredFiles.splice(index, 1);
            this.forms["StoredFiles"].$setDirty();
        }
        // Constructor
        constructor(
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvServiceStrings: services.IServiceStrings,
                public readonly $scope: ng.IScope,
                public readonly $translate: ng.translate.ITranslateService,
                public readonly $stateParams: ng.ui.IStateParamsService,
                public readonly $state: ng.ui.IStateService,
                public readonly $window: ng.IWindowService,
                public readonly $filter: ng.IFilterService,
                public readonly $uibModal: ng.ui.bootstrap.IModalService,
                public readonly $q: ng.IQService,
                public readonly cefConfig: core.CefConfig,
                public readonly cvApi: api.ICEFAPI,
                public readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
            // This has to be here instead of the load function because $rootScope is undefined at the point there
            const unbind1 = $scope.$on(this.cvServiceStrings.events.account.refreshRoles, () => {
                this.cvApi.authentication.GetRolesForAccount(this.record.ID).then(r => this.rolesForAccount = r.data);
            });
            $scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    adminApp.directive("accountDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/accountDetail.html", "ui"),
        controller: AccountDetailController,
        controllerAs: "accountDetailCtrl"
    }));
}
