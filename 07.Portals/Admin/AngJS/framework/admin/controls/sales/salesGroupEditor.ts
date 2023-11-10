module cef.admin.controls.sales {
    class SalesGroupEditorController extends core.TemplatedControllerBase {
        // Properties
        record: api.SalesGroupModel;
        accounts: Array<api.AccountModel>;
        salesQuotes: Array<api.SalesQuoteModel>;
        salesOrders: Array<api.SalesOrderModel>;
        salesInvoices: Array<api.SalesInvoiceModel>;
        purchaseOrders: Array<api.PurchaseOrderModel>;
        salesReturns: Array<api.SalesReturnModel>;
        // ninetyDaysAgo = this.$filter("amSubtract")(new Date(), 90, "days").toDate();
        private _masterSalesQuote = null;
        get masterSalesQuote(): api.SalesQuoteModel {
            if (!this.record 
                || !this.record.SalesQuoteRequestMasters
                || !this.record.SalesQuoteRequestMasters.length
                || !this.record.SalesQuoteRequestMasters[0].ID) { 
                return null; 
            }
            if (this._masterSalesQuote && this._masterSalesQuote.ID === this.record.SalesQuoteRequestMasters[0].ID) {
                return this._masterSalesQuote;
            }
            return this._masterSalesQuote = _.find(this.salesQuotes, x => x.ID === this.record.SalesQuoteRequestMasters[0].ID);
        }
        private _masterSalesOrder = null;
        get masterSalesOrder(): api.SalesOrderModel {
            if (!this.record 
                || !this.record.SalesOrderMasters
                || !this.record.SalesOrderMasters.length
                || !this.record.SalesOrderMasters[0].ID) { 
                return null; 
            }
            if (this._masterSalesOrder && this._masterSalesOrder.ID === this.record.SalesOrderMasters[0].ID) {
                return this._masterSalesOrder;
            }
            return this._masterSalesOrder = _.find(this.salesOrders, x => x.ID === this.record.SalesOrderMasters[0].ID);
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                // private readonly $filter: ng.IFilterService,
                private readonly cvAddressBookService: cef.admin.services.IAddressBookService,
                readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            if (Number($stateParams.ID) > 0) {
                this.loadRecord(Number($stateParams.ID)).then(() => this.loadCollections());
                return;
            }
            this.record = this.newRecord();
            this.loadCollections();
        }
        // Functions
        private loadCollections(): ng.IPromise<void> {
            this.cvApi.accounts.GetAccounts({
                Active: true,
                AsListing: true,
                Paging: { StartIndex: 1, Size: 8 }
            }).then(r => this.accounts = r.data.Results);
            this.onAccountIDChange();
            return this.$q.resolve();
        }
        private loadSalesQuotes(): void {
            const dto = <api.GetSalesQuotesDto>{
                AccountID: this.record ? this.record.AccountID : null,
                SalesGroupID: this.record.ID,
                // ModifiedSince: this.ninetyDaysAgo,
                Paging: { StartIndex: 1, Size: 8 }
            };
            this.cvApi.quoting.GetSalesQuotes(dto).then(r => {
                this.salesQuotes = r.data.Results;
                if (!this.record.SalesQuoteRequestMasters
                    || !this.record.SalesQuoteRequestMasters.length
                    || !this.record.SalesQuoteRequestMasters[0].ID
                    || _.find(this.salesQuotes, x => x.ID == this.record.SalesQuoteRequestMasters[0].ID)) {
                    return;
                }
                this.cvApi.quoting.GetSalesQuoteByID(this.record.SalesQuoteRequestMasters[0].ID)
                    .then(r2 => r2.data && this.salesQuotes.push(r2.data));
            });
        }

        private loadSalesOrders(): void {
            const dto = <api.GetSalesOrdersDto>{
                AccountID: this.record ? this.record.AccountID : null,
                SalesGroupID: this.record.ID,
                // ModifiedSince: this.ninetyDaysAgo,
                Paging: { StartIndex: 1, Size: 8 }
            };
            this.cvApi.ordering.GetSalesOrders(dto).then(r => {
                this.salesOrders = r.data.Results;
                if (!this.record.SalesOrderMasters
                    || !this.record.SalesOrderMasters.length
                    || !this.record.SalesOrderMasters[0].ID
                    || _.find(this.salesOrders, x => x.ID == this.record.SalesOrderMasters[0].ID)) {
                    return;
                }
                this.cvApi.ordering.GetSalesOrderByID(this.record.SalesOrderMasters[0].ID)
                    .then(r2 => r2.data && this.salesOrders.push(r2.data));
            });
        }

        private loadSalesInvoices(): void {
            const dto = <api.GetSalesInvoicesDto>{
                AccountID: this.record ? this.record.AccountID : null,
                // ModifiedSince: this.ninetyDaysAgo,
                SalesGroupID: this.record.ID,
                Paging: { StartIndex: 1, Size: 8 }
            };
            this.cvApi.invoicing.GetSalesInvoices(dto)
                .then(r => this.salesInvoices = r.data.Results);
        }

        private loadPurchaseOrders(): void {
            const dto = <api.GetPurchaseOrdersDto>{
                Active: true,
                AsListing: true,
                SalesGroupID: this.record.ID,
                // ModifiedSince: this.ninetyDaysAgo,
                Paging: { StartIndex: 1, Size: 8 }
            };
            this.cvApi.purchasing.GetPurchaseOrders(dto)
                .then(r => this.purchaseOrders = r.data.Results);
        }

        private loadSalesReturns(): void {
            const dto = <api.GetSalesReturnsDto>{
                AccountID: this.record ? this.record.AccountID : null,
                SalesGroupID: this.record.ID,
                // ModifiedSince: this.ninetyDaysAgo,
                Paging: { StartIndex: 1, Size: 8 }
            };
            this.cvApi.returning.GetSalesReturns(dto)
                .then(r => this.salesReturns = r.data.Results);
        }

        private loadRecord(id: number): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.cvApi.sales.GetSalesGroupByID(id).then(r => {
                    if (!r || !r.data) {
                        reject(`There was an error loading the Group.`);
                        return;
                    }
                    this.record = r.data;
                    if (this.record.BillingContact.ID) {
                        resolve(true);
                        return;
                    }
                    if (this.cvAddressBookService.defaultBilling
                        && this.cvAddressBookService.defaultBilling[this.record.AccountID]
                        && this.cvAddressBookService.defaultBilling[this.record.AccountID].Slave) {
                        this.record.BillingContact = this.cvAddressBookService.defaultBilling[this.record.AccountID].Slave;
                        resolve(true);
                        return;
                    }
                    this.cvAddressBookService.getBook(this.record.AccountID).then(book => {
                        if (!book.length) {
                            reject(`There was an error loading the Billing Contact.`);
                            return;
                        }
                        this.record.BillingContact = book[0].Slave;
                        resolve(true);
                    });
                });
            });
        }
        private newRecord(): api.SalesGroupModel {
            return <api.SalesGroupModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
            };
        }
        // Boolean to notify sucessful save
        saveSuccess: boolean;
        saveFailed: boolean;
        private saveRecord(): void {
            this.saveSuccess = false;
            this.saveFailed = false;
            if (this.record.ID > 0) {
                this.cvApi.sales.UpdateSalesGroup(this.record)
                    .then(() => this.loadRecord(this.record.ID)
                        .then(success => this.saveSuccess = success)
                        .catch(e2 => this.saveFailed = e2))
                    .catch(e1 => this.saveFailed = e1);
                return;
            }
            this.cvApi.sales.CreateSalesGroup(this.record)
                .then(r => this.loadRecord(r.data.Result)
                    .then(success => this.saveSuccess = success)
                    .catch(e2 => this.saveFailed = e2))
                .catch(e1 => this.saveFailed = e1);
        }
        // Events
        onAccountIDChange(): void {
            this.loadSalesQuotes();
            this.loadSalesOrders();
            this.loadSalesInvoices();
            this.loadPurchaseOrders();
            this.loadSalesReturns();
        }
    }

    adminApp.directive("salesGroupEditor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/salesGroupEditor.html", "ui"),
        controller: SalesGroupEditorController,
        controllerAs: "sgeCtrl"
    }));
}
