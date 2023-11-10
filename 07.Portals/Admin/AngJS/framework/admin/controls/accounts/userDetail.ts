/**
 * @file framework/admin/controls/accounts/userDetail.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc User Editor detail class for CEF Administrators.
 */
module cef.admin.controls.accounts {
    export class UserDetailController extends DetailBaseController<api.UserModel> {
        // Forced overrides
        detailName = "User";
        // Collections
        regions: api.RegionModel[] = [];
        countries: api.CountryModel[] = [];
        types: api.TypeModel[] = [];
        imageTypes: api.TypeModel[] = [];
        statuses: api.StatusModel[] = [];
        rolesForUser: api.RoleForUserModel[] = [];
        languages: api.LanguageModel[] = [];
        currencies: api.CurrencyModel[] = [];
        storePaging: core.ServerSidePaging<api.StoreModel, api.StorePagedResults>;
        brands: api.BrandModel[] = [];
        // UI Data
        password: string;
        // Required Functions
        loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 100 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.cvApi.geography.GetRegions(standardDto).then(r => this.regions = r.data.Results);
            this.cvApi.geography.GetCountries(standardDto).then(r => this.countries = r.data.Results);
            this.cvApi.contacts.GetUserTypes(standardDto).then(r => this.types = r.data.Results);
            this.cvApi.contacts.GetUserStatuses(standardDto).then(r => this.statuses = r.data.Results);
            this.cvApi.contacts.GetUserImageTypes(standardDto).then(r => this.imageTypes = r.data.Results);
            this.cvApi.currencies.GetCurrencies(standardDto).then(r => this.currencies = r.data.Results);
            this.cvApi.globalization.GetLanguages(standardDto).then(r => this.languages = r.data.Results);
            this.storePaging = new core.ServerSidePaging<api.StoreModel, api.StorePagedResults>(
                this.$rootScope, this.$scope, this.$filter, this.$q, this.cvServiceStrings, this.cvApi.stores.GetStores, 8, "stores");
            this.cvApi.brands.GetBrands(standardDto).then(r => {
                this.brands = r.data.Results;
                if (!this.brands.length) {
                    return;
                }
                if (!this.record.Brands) {
                    this.record.Brands = [];
                }
                /* TODO: Wrap this in a setting
                this.brands.forEach(x => {
                    const found = _.find(this.record.Brands, y => y.MasterID === x.ID);
                    if (found) {
                        return;
                    }
                    this.record.Brands.push(<api.BrandUserModel>{
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
                        HasAccessToBrand: true
                    });
                });
                // */
            });
            return this.$q.resolve();
        }
        loadNewRecord(): ng.IPromise<api.UserModel> {
            this.record = <api.UserModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                SerializableAttributes: {},
                Hash: null,
                // IHaveImagesBase
                Images: [],
                // IHaveNotesBase
                Notes: [],
                // IHaveStoredFilesBase
                StoredFiles: [],
                // AmFilterableByBrandsBase
                Brands: [],
                // AmFilterableByStoresBase
                Stores: [],
                // HaveAStatusBase
                StatusID: 1,
                // HaveATypeBase
                TypeID: 1,
                // HaveAContactBase
                ContactID: 0,
                Contact: this.createContactModel(),
                // User Properties
                UserName: null,
                Email: null,
                EmailConfirmed: false,
                ////PasswordHash: null,
                ////OverridePassword: null,
                ////SecurityStamp: null,
                PhoneNumber: null,
                PhoneNumberConfirmed: false,
                TwoFactorEnabled: false,
                LockoutEndDateUtc: null,
                LockoutEnabled: false,
                AccessFailedCount: 0,
                IsApproved: false,
                IsSMSAllowed: true,
                RequirePasswordChangeOnNextLogin: false,
                DisplayName: null,
                PercentDiscount: null,
                IsDeleted: false,
                IsSuperAdmin: false,
                IsEmailSubscriber: false,
                IsCatalogSubscriber: false,
                AccountID: 0,
                AccountKey: null,
                AccountName: null,
                Account: null,
                PreferredStoreID: 0,
                PreferredStoreKey: null,
                PreferredStoreName: null,
                PreferredStore: null,
                BillingAddress: null,
                SalesRepContactsUserID: 0,
                User: null,
                CurrencyID: 0,
                CurrencyKey: null,
                CurrencyName: null,
                Currency: null,
                LanguageID: 0,
                LanguageKey: null,
                Language: null,
                UserOnlineStatusID: 0,
                UserOnlineStatusKey: null,
                UserOnlineStatusName: null,
                UserOnlineStatus: null,
                Reviews: [],
            };
            this.rolesForUser = [];
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> {
            this.detailName = "User";
            return this.$q.resolve();
        }
        createRecordPreAction(): ng.IPromise<api.UserModel> {
            this.record.OverridePassword = this.password;
            return this.$q.resolve(this.record);
        }
        updateRecordPreAction(): ng.IPromise<api.UserModel> {
            if (this.password && this.password.length > 0) {
                this.record.OverridePassword = this.password;
            }
            return this.$q.resolve(this.record);
        }
        loadRecordCall(id: number): ng.IHttpPromise<api.UserModel> {
            return this.cvApi.contacts.GetUserByID(id);
        }
        createRecordCall(routeParams: api.UserModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.contacts.CreateUser(routeParams);
        }
        updateRecordCall(routeParams: api.UserModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.contacts.UpdateUser(routeParams);
        }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.contacts.DeactivateUserByID(id);
        }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.contacts.ReactivateUserByID(id);
        }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.contacts.DeleteUserByID(id);
        }
        loadRecordActionAfterSuccess(result: api.UserModel): ng.IPromise<api.UserModel> {
            return this.$q((resolve, reject) => {
                this.$q.all([
                    result.ID > 0
                        ? this.cvApi.authentication.GetRolesForUser(result.ID)
                        : this.$q.resolve({ data: null }),
                    result.ID > 0
                        ? this.cvApi.structure.GetNotes({ Active: true, AsListing: false, UserID: result.ID })
                        : this.$q.resolve({ data: null })
                ]).then((rarr: ng.IHttpPromiseCallbackArg<any>[]) => {
                    let index = -1;
                    this.rolesForUser = rarr[++index].data as any[] || [];
                    result.Notes = ((rarr[++index].data || { }) as api.NotePagedResults).Results || [];
                    resolve(result);
                }).catch(reject);
            });
        }
        createRecordActionAfterSuccess(result: api.CEFActionResponseT<number>): ng.IPromise<number> {
            if (!result.ActionSucceeded) {
                this.consoleLog(result);
                return this.$q.reject(result);
            }
            this.password = null;
            return this.$q.resolve(result.Result);
        }
        updateRecordActionAfterSuccess(result: api.CEFActionResponseT<number>): ng.IPromise<number> {
            if (!result.ActionSucceeded) {
                this.consoleLog(result);
                return this.$q.reject(result);
            }
            this.password = null;
            return this.$q.resolve(result.Result);
        }
        // Supportive Functions

        /**
         * The text entered into the Account typeahead
         * @private
         * @type {string}
         * @memberof SalesOrderNewWizardController
         */
        private accountToGrab: string = null;
        /**
         * The Accounts pulled in for the typeahead flyout based on the
         * {@see accountToGrab} as a search parameter
         * @private
         * @type {api.UserModel[]}
         * @memberof SalesOrderNewWizardController
         */
        private accounts: api.AccountModel[] = [];
        /**
         * The selected Account model as a result of picking the {@see accountID}
         * @private
         * @type {number}
         * @memberof SalesOrderNewWizardController
         */
        private account: api.AccountModel = null;
        /**
         * The selected Account identifier
         * @private
         * @type {number}
         * @memberof SalesOrderNewWizardController
         */
        private accountID: number = null;
        unbindAccountTAWatch: Function;
        setupAccountTA(): void {
            // Add watches to accountID and userID in case they are updated from any direction
            this.unbindAccountTAWatch = this.$scope.$watch(() => this.accountID, (newVal: number, oldVal: number) => {
                if (newVal === oldVal) { return; }
                if (!newVal) {
                    this.accountID = null;
                    this.account = null;
                    return;
                }
                this.cvApi.accounts.GetAccountByID(newVal).then(r => this.account = r.data);
            });
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(this.unbindAccountTAWatch)) { this.unbindAccountTAWatch(); }
            });
        }
        /**
         * Runs the search using {@see accountToGrab} and populates {@see accounts} for
         * the Account typeahead. Used by the UI.
         * @protected
         * @param {string} search
         * @returns {ng.IPromise<Array<api.AccountModel>>}
         * @memberof SalesOrderNewWizardController
         */
        protected grabAccounts(search: string): ng.IPromise<Array<api.AccountModel>> {
            const lookup = search.toLowerCase()
            return this.cvApi.accounts.GetAccounts({
                Active: true,
                AsListing: true,
                IDOrCustomKeyOrName: search,
                Paging: <api.Paging>{ Size: 50, StartIndex: 1 }
            }).then(r => this.accounts = r.data.Results.filter(
                item => (item.CustomKey || "").toLowerCase().indexOf(lookup) > -1
                     || (item.Name || "").toLowerCase().indexOf(lookup) > -1
                     || item.ID.toString().indexOf(lookup) > -1));
        }
        /**
         * The event handler for the Account typeahead which populates the {@see accountID}.
         * Used by the UI.
         * @protected
         * @param {*} $item - Unused, but required for positional parameter
         * @param {*} $model - The identifier to apply
         * @memberof SalesOrderNewWizardController
         */
        protected selectAccountFromTypeAhead($item, $model) {
            this.accountID = Number($model);
            this.record.AccountID = this.accountID;
            const account = _.find(this.accounts, x => x.ID === this.accountID);
            if (!account) {
                return;
            }
            this.record.AccountKey = account.CustomKey;
            this.record.AccountName = account.Name;
            this.record.Account = account;
        }

        // Store Management Events
        addStore(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.storePaging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            if (!this.record.Stores) {
                this.record.Stores = [];
            }
            // Ensure it's not already in the collection
            if (_.find(this.record.Stores, x => x.MasterID === model.ID)) {
                return;
            }
            // Add it
            this.record.Stores.push(<api.StoreUserModel>{
                // Base Properties
                Active: true,
                CreatedDate: new Date(),
                // Related Objects
                MasterID: model.ID,
                MasterKey: model.CustomKey,
                MasterName: model.Name,
                SlaveID: this.record.ID
            });
            this.forms["Stores"].$setDirty();
        }
        removeStore(index: number): void {
            this.record.Stores.splice(index, 1);
            this.forms["Stores"].$setDirty();
        }

        // Brand Management Events
        addBrand(): void {
            if (!this.record.Brands) {
                this.record.Brands = [];
            }
            this.record.Brands.push(<api.BrandUserModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                // Related Objects
                MasterID: 0,
                SlaveID: this.record.ID,
                //
                HasAccessToBrand: true
            });
            this.forms["Brands"].$setDirty();
        }
        removeBrand(index: number): void {
            this.record.Brands.splice(index, 1);
            this.forms["Brands"].$setDirty();
        }

        // User Management Events
        lockUser() {
            this.callAndHandle(this.cvApi.authentication.LockUser);
        }
        unlockUser() {
            this.callAndHandle(this.cvApi.authentication.UnlockUser);
        }
        approveUser() {
            this.callAndHandle(this.cvApi.authentication.ApproveUser);
        }
        unApproveUser() {
            this.callAndHandle(this.cvApi.authentication.UnApproveUser);
        }
        doRequirePasswordChangeOnNextLoginDorUser() {
            this.callAndHandle(this.cvApi.authentication.DoRequirePasswordResetOnNextLoginForUser);
        }
        dontRequirePasswordChangeOnNextLoginForUser() {
            this.callAndHandle(this.cvApi.authentication.DontRequirePasswordResetOnNextLoginForUser);
        }
        private callAndHandle(promise: (id: number) => ng.IHttpPromise<api.CEFActionResponse>): void {
            promise(this.record.ID).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, null, r && r.data && r.data.Messages);
                    return;
                }
                this.loadRecord(this.record.ID); // Will call finishRunning
            }).catch(reason => this.finishRunning(true, reason));
        }

        // Image & Document Management Events V2
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
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $state: ng.ui.IStateService,
                protected readonly $window: ng.IWindowService,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory,
                protected readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
            const unbind1 = $scope.$on(this.cvServiceStrings.events.users.updated, () => {
                if (this.record.ID > 0) {
                    this.loadRecord(this.record.ID);
                }
            });
            const unbind2 = $scope.$on(cvServiceStrings.events.users.refreshRoles, () => {
                this.cvApi.authentication.GetRolesForUser(this.record.ID)
                    .then(r => this.rolesForUser = r.data);
            });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
    }

    adminApp.directive("userDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/userDetail.html", "ui"),
        controller: UserDetailController,
        controllerAs: "userDetailCtrl"
    }));
}
