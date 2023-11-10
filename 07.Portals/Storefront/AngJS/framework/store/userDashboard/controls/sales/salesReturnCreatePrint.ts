module cef.store.userDashboard.controls.sales {
    class SalesReturnCreatePrintController extends core.TemplatedControllerBase {
        // Properties
        id: number;
        salesReturn: api.SalesReturnModel;
        returnContact: api.ContactModel;
        get billingContact() { return this.salesReturn && this.salesReturn.BillingContact; }
        get shippingContact() { return this.salesReturn && this.salesReturn.ShippingContact; }
        // Functions
        load(): void {
            this.setRunning();
            this.cvApi.returning.GetSalesReturnByID(this.id).then(r => {
                if (!r || !r.data) {
                    this.finishRunning(true);
                    return;
                }
                this.salesReturn = r.data;
                this.salesReturn.BillingContact;
                this.salesReturn.ShippingContact;
                var returnContactModel = (<any>(this.salesReturn.Contacts))
                    .find(x => x.Contact.TypeKey === "Returning");
                if (returnContactModel) {
                    this.returnContact = returnContactModel.Contact;
                }
                this.cvProductService.appendToSalesItems(this.salesReturn.SalesItems).then(items => {
                    this.salesReturn.SalesItems = items;
                    this.finishRunning();
                });
            });
        }
        // Constructor
        constructor(
                private readonly $window: ng.IWindowService,
                private readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvProductService: services.ProductService) {
            super(cefConfig);
            this.id = this.$stateParams.ID;
            if (!this.id) {
                const parts = this.$window.location.pathname.split("/");
                this.id = Number(parts[parts.length - 1]);
            }
            this.load();
        }
    }

    cefApp.directive("cefSalesReturnCreatePrint", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { type: "@" },
        transclude: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/sales/salesReturnCreatePrint.html", "ui"),
        controller: SalesReturnCreatePrintController,
        controllerAs: "salesReturnCreatePrintCtrl"
    }));
}
