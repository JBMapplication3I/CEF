module cef.admin.controls.sales {
    export abstract class SalesDetailControllerBase<
            TRecord extends api.SalesCollectionBaseModelT<
                                TTypeModel,
                                api.AmARelationshipTableModel<api.ContactModel>,
                                api.SalesEventBaseModel,
                                TDiscountModel,
                                TItemDiscountModel,
                                api.AmAStoredFileRelationshipTableModel>,
            TTypeModel extends api.TypeModel,
            TDiscountModel extends api.AppliedDiscountBaseModel,
            TItemDiscountModel extends api.AppliedDiscountBaseModel>
        extends core.TemplatedControllerBase
    {
        // SalesDetailControllerBase Properties
        record: TRecord;
        items: api.SalesItemBaseModel<TItemDiscountModel>[] = [];
        productItems = [];
        discountItems = [];
        paymentMethods: api.PaymentMethodModel[] = [];
        types: api.TypeModel[] = [];
        statuses: api.StatusModel[] = [];
        attributes: api.GeneralAttributeModel[] = [];
        products: api.ProductModel[] = [];
        warehouses: api.InventoryLocationModel[] = [];
        states: api.RegionModel[] = [];
        countries: api.CountryModel[] = [];
        showAddressEditors = false;
        // Abstract Properties to be implemented by inheritors
        abstract itemType: string;
        // Boolean to notify sucessful save
        saveSuccess: boolean;
        saveFailed: boolean;
        // Abstract Functions to be implemented by inheritors
        abstract newRecord(): TRecord;
        abstract loadRecord(id: number): ng.IPromise<boolean>;
        protected loadRecordAfterAction(result: boolean): void {
            // Do Nothing by default
        }
        abstract createRecordCall: (dto: TRecord) => ng.IHttpPromise<api.CEFActionResponseT<number>>;
        abstract updateRecordCall: (dto: TRecord) => ng.IHttpPromise<api.CEFActionResponseT<number>>;
        // Functions
        protected doSameAsBillingCheckAndAssign(): ng.IPromise<void> {
            if (!this.record) { return this.$q.reject("No record to use"); }
            if (this.record.BillingContact && this.record.ShippingContact
                && this.record.BillingContact.FirstName === this.record.ShippingContact.FirstName
                && this.record.BillingContact.LastName === this.record.ShippingContact.LastName
                && this.record.BillingContact.Address.Street1 === this.record.ShippingContact.Address.Street1
                && this.record.BillingContact.Address.Street2 === this.record.ShippingContact.Address.Street2
                && this.record.BillingContact.Address.Street3 === this.record.ShippingContact.Address.Street3
                && this.record.BillingContact.Address.City === this.record.ShippingContact.Address.City
                && this.record.BillingContact.Address.PostalCode === this.record.ShippingContact.Address.PostalCode
                && this.record.BillingContact.Address.RegionID === this.record.ShippingContact.Address.RegionID
                && this.record.BillingContact.Address.CountryID === this.record.ShippingContact.Address.CountryID
                && this.record.BillingContact.Phone1 === this.record.ShippingContact.Phone1
                && this.record.BillingContact.Phone2 === this.record.ShippingContact.Phone2
                && this.record.BillingContact.Phone3 === this.record.ShippingContact.Phone3
                && this.record.BillingContact.Fax1 === this.record.ShippingContact.Fax1
                && this.record.BillingContact.Email1 === this.record.ShippingContact.Email1) {
                this.record.ShippingSameAsBilling = true;
            }
            return this.$q.resolve();
        }
        protected loadCollections(): ng.IPromise<void> {
            const paging = <api.Paging>{ StartIndex: 1, Size: 50 };
            const dto = { Active: true, AsListing: true, Paging: paging };
            this.cvApi.payments.GetPaymentMethods(dto).then(r => this.paymentMethods = r.data.Results);
            this.cvApi.geography.GetCountries(dto).then(r => this.countries = r.data.Results);
            this.cvApi.geography.GetRegions(dto).then(r => this.states = r.data.Results);
            this.cvApi.inventory.GetInventoryLocations(dto).then(r => this.warehouses = r.data.Results);
            this.cvApi.ordering.GetSalesOrderTypes(dto).then(r => this.types = r.data.Results);
            this.cvApi.ordering.GetSalesOrderStatuses(dto).then(r => this.statuses = r.data.Results);
            this.cvApi.contacts.GetUsers(dto).then(r => this.users = r.data.Results);
            this.cvApi.accounts.GetAccounts(dto).then(r => this.accounts = r. data.Results);
            this.cvApi.attributes.GetGeneralAttributes(dto).then(r => this.attributes = r.data.Results);
            return this.$q.resolve();
        }
        protected readOutDiscounts(): ng.IPromise<void> {
            if (this.record.Discounts) {
                this.record.Discounts.forEach(item => {
                    var itemType: string;
                    switch (item.DiscountTypeID) {
                        case 1: { itemType = "Shipping"; break; }
                        default: { itemType = this.itemType; break; }
                    }
                    this.discountItems.push({
                        Type: itemType,
                        Name: item.SlaveName,
                        DiscountPrice: item.DiscountValue
                    });
                });
            }
            return this.$q.resolve();
        }
        protected recordIsNew(): boolean { return this.record && !this.record.ID; }
        protected isOneOfTheseStatuses(statuses: string[]): boolean {
            if (!statuses || statuses.length == 0 || !this.record) { return false; }
            return _.some(
                statuses,
                status => status == this.record.StatusName
                       || status == this.record.StatusKey
                       || this.record.Status && status == this.record.Status.CustomKey
                       || this.record.Status && status == this.record.Status.Name);
        }
        protected updateShippingAddressFromBilling() {
            // TODO: Assign the fields from the BillingContact to the ShippingContact for the view
        }
        protected saveRecord(): void {
            this.saveSuccess = false;
            this.saveFailed = false;
            if (this.record.ID > 0) {
                this.updateRecordCall(this.record)
                    .then(() => this.loadRecord(this.record.ID)
                        .then(r2 => this.saveSuccess = r2)
                        .catch(reason2 => this.saveFailed = reason2))
                    .catch(reason1 => this.saveFailed = reason1);
                return;
            }
            this.createRecordCall(this.record)
                .then(r1 => this.loadRecord(r1.data.Result)
                    .then(r2 => this.saveSuccess = r2)
                    .catch(reason2 => this.saveFailed = reason2))
                .catch(reason1 => this.saveFailed = reason1);
        }
        protected generateNewTotals(): api.CartTotals {
            return <api.CartTotals>{
                SubTotal: 0,
                Shipping: 0,
                Handling: 0,
                Fees: 0,
                Tax: 0,
                Discounts: 0,
                Total: 0
            };
        }
        // Collection Actions (state changes, etc)
        protected doStateChangeAction(call: (id: number) => ng.IHttpPromise<api.CEFActionResponse>)
            : ng.IPromise<api.CEFActionResponse> {
            return this.doStateChangeCall(call);
        }
        protected doStateChangeActionWithConfirm(
            message: string,
            call: (id: number) => ng.IHttpPromise<api.CEFActionResponse>)
            : ng.IPromise<api.CEFActionResponse> {
            return this.$q((resolve, reject) => {
                this.cvConfirmModalFactory(message)
                    .then(result => result ? resolve(this.doStateChangeCall(call)) : reject(result))
                    .catch(reject);
            });
        }
        private doStateChangeCall(call: (id: number) => ng.IHttpPromise<api.CEFActionResponse>): ng.IPromise<api.CEFActionResponse> {
            return this.$q((resolve, reject) => {
                call(this.record.ID).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        reject(r);
                        return;
                    }
                    // Refresh the data
                    this.loadRecord(this.record.ID).then(r2 => this.loadRecordAfterAction(r2));
                    resolve(r.data);
                }).catch(reject);
            });
        }
        removeFileNew(index: number): void {
            this.record.StoredFiles.splice(index, 1);
            this.forms["StoredFiles"].$setDirty();
        }
        /**
         * The text entered into the Account typeahead
         * @private
         * @type {string}
         * @memberof SalesOrderNewWizardController
         */
        protected accountToGrab: string = null;
        /**
         * The Accounts pulled in for the typeahead flyout based on the
         * {@see accountToGrab} as a search parameter
         * @private
         * @type {api.UserModel[]}
         * @memberof SalesOrderNewWizardController
         */
        protected accounts: api.AccountModel[] = [];
        /**
         * The selected Account model as a result of picking the {@see accountID}
         * @private
         * @type {number}
         * @memberof SalesOrderNewWizardController
         */
        protected account: api.AccountModel = null;
        /**
         * The selected Account identifier
         * @private
         * @type {number}
         * @memberof SalesOrderNewWizardController
         */
        protected accountID: number = null;
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
        /**
         * The text entered into the User typeahead
         * @private
         * @type {string}
         * @memberof SalesOrderNewWizardController
         */
        protected userToGrab: string = null;
        /**
         * The Users pulled in for the typeahead flyout based on the
         * {@see userToGrab} as a search parameter
         * @private
         * @type {api.UserModel[]}
         * @memberof SalesOrderNewWizardController
         */
        protected users: api.UserModel[] = [];
        /**
         * The selected User model as a result of picking the {@see userID}
         * @private
         * @type {number}
         * @memberof SalesOrderNewWizardController
         */
        protected user: api.UserModel = null;
        /**
         * The selected User identifier
         * @private
         * @type {number}
         * @memberof SalesOrderNewWizardController
         */
        protected userID: number = null;
        unbindUserTAWatch: Function;
        setupUserTA(): void {
            // Add watches to userID and userID in case they are updated from any direction
            this.unbindUserTAWatch = this.$scope.$watch(() => this.userID, (newVal: number, oldVal: number) => {
                if (newVal === oldVal) { return; }
                if (!newVal) {
                    this.userID = null;
                    this.user = null;
                    return;
                }
                this.cvApi.contacts.GetUserByID(newVal).then(r => this.user = r.data);
            });
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(this.unbindUserTAWatch)) { this.unbindUserTAWatch(); }
            });
        }
        /**
         * Runs the search using {@see userToGrab} and populates {@see users} for
         * the User typeahead. Used by the UI.
         * @protected
         * @param {string} search
         * @returns {ng.IPromise<Array<api.UserModel>>}
         * @memberof SalesOrderNewWizardController
         */
        protected grabUsers(search: string): ng.IPromise<Array<api.UserModel>> {
            const lookup = search.toLowerCase()
            return this.cvApi.contacts.GetUsers({
                Active: true,
                AsListing: true,
                AccountID: this.accountID || null,
                IDOrUserNameOrCustomKeyOrEmailOrContactName: search,
                Paging: <api.Paging>{ Size: 50, StartIndex: 1 }
            }).then(r => this.users = r.data.Results.filter(
                item => (item.CustomKey || "").toLowerCase().indexOf(lookup) > -1
                    || (item.UserName || "").toLowerCase().indexOf(lookup) > -1
                    || item.ID.toString().indexOf(lookup) > -1));
        }
        /**
         * The event handler for the User typeahead which populates the {@see userID}.
         * Used by the UI.
         * @protected
         * @param {*} $item - Unused, but required for positional parameter
         * @param {*} $model - The identifier to apply
         * @memberof SalesOrderNewWizardController
         */
        protected selectUserFromTypeAhead($item, $model) {
            this.userID = Number($model);
            this.record.UserID = this.userID;
            const user = _.find(this.users, x => x.ID === this.userID);
            if (!user) {
                return;
            }
            this.record.UserKey = user.CustomKey;
            this.record.UserName = user.UserName;
            this.record.User = user;
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super(cefConfig);
            this.loadCollections();
            if (this.$stateParams.ID > 0) {
                this.loadRecord(this.$stateParams.ID).then(r => this.loadRecordAfterAction(r));
                return;
            }
            this.record = this.newRecord();
            this.showAddressEditors = true;
        }
    }
}
