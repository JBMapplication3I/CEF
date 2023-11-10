module cef.store.userDashboard.controls.sales {
    class SalesReturnCreateController extends core.TemplatedControllerBase {
        // Properties
        status = "searching";
        salesOrder: api.SalesOrderModel;
        salesReturn: api.SalesReturnModel;
        existingReturns: api.SalesReturnModel[] = [];
        reasons: api.SalesReturnReasonModel[] = [];
        salesObject: api.SalesCollectionBaseModelT<api.TypeModel, api.AmARelationshipTableModel<api.ContactModel>, api.SalesEventBaseModel, api.AppliedDiscountBaseModel, api.AppliedDiscountBaseModel, api.AmAStoredFileRelationshipTableModel>;
        id: number;
        salesObjectType: string;
        itemsProducts: { [salesItemID: number]: boolean } = {};
        itemsModel: { [id: number]: any } = {};
        itemCheckedCount: number;
        // Functions
        load(): void {
            this.itemCheckedCount = 0;
            this.salesObjectType = "Sales Return";
            this.cvApi.ordering.GetSalesOrderByID(this.id).then(r => {
                this.salesOrder = this.salesObject = <any>r.data;
                this.cvProductService.appendToSalesItems(this.salesOrder.SalesItems).then(salesItems => {
                    this.salesOrder.SalesItems = salesItems;
                    var returnQty = {};
                    this.cvApi.returning.GetSalesReturns({
                        Active: true,
                        AsListing: false,
                        SalesOrderID: this.id
                    }).then(r2 => {
                        this.existingReturns = r2.data.Results;
                        this.existingReturns.forEach(sr => {
                            sr.SalesItems.forEach(item => {
                                if (!returnQty[item.ProductID]) {
                                    returnQty[item.ProductID] = 0;
                                }
                                returnQty[item.ProductID] += item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0);
                                ////this.consoleDebug(`4 PID: ${item.Product.ID}, Qty: ${returnQty[item.Product.ID]}`);
                            });
                        });
                        this.salesOrder.SalesItems.forEach(item => {
                            this.itemsProducts[item.ID] = (item["Product"] as api.ProductModel).IsEligibleForReturn;
                            ////this.consoleDebug(`5 I: ${item.Product.ID} IQ: ${item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0)} rtQ: ${returnQty[item.Product.ID]}`);
                            let retQty = returnQty[item.ProductID];
                            if (!retQty) {
                                retQty = 0;
                            }
                            var qty = (item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0)) - retQty;
                            this.itemsModel[item.ID] = {
                                ID: item.ID,
                                Quantity: item.Quantity + (item.QuantityBackOrdered || 0) + (item.QuantityPreSold || 0),
                                ProductID: item.ProductID,
                                QuantityReturnable: qty < 0 ? 0 : qty,
                                selectedQuantity: null
                            };
                        });
                    });
                });
            });
            this.cvApi.returning.GetSalesReturnReasons({ Active: true, AsListing: true })
                .then(r => this.reasons = r.data.Results);
        }
        checkDisableControl(itemID: number): boolean {
            if (this.itemsModel[itemID] && this.itemsModel[itemID].itemChecked) {
                return false;
            }
            if (this.itemsModel[itemID] && !this.itemsModel[itemID].itemChecked) {
                this.itemsModel[itemID].selectedQuantity = null;
            }
            return true;
        }
        hasRestockFee(itemID: number): boolean {
            const item = this.itemsModel[itemID];
            if (!item) { return false; }
            var reasonID = this.itemsModel[itemID].selectedReason;
            const reason = (<any>(this.reasons)).find(x => x.ID === reasonID);
            if (!reason) { return false; }
            if (reason.IsRestockingFeeApplicable) {
                return true;
            }
            return false;
        }
        isEligibleForReturn(itemID: number): boolean {
            if (!this.itemsModel[itemID] || this.itemsModel[itemID].QuantityReturnable <= 0) {
                return false;
            }
            return this.itemsProducts[itemID];
        }
        updateSelectedItems(itemID: number): boolean {
            if (!this.itemsModel[itemID].itemChecked) {
                this.itemsModel[itemID].selectedReason = null;
                this.itemsModel[itemID].selectedQuantity = null;
                this.itemsModel[itemID].Description = null;
                this.itemCheckedCount--;
                return true;
            }
            if (!this.itemsModel[itemID].selectedQuantity) {
                this.itemsModel[itemID].selectedQuantity = 1;
            }
            this.itemCheckedCount++;
            return false;
        }
        save(): void {
            const salesReturn = <api.SalesReturnModel>{
                ID: 0,
                HasChildren: false,
                SalesItems: null,
                ShippingSameAsBilling: false,
                Discounts: null,
                ItemQuantity: null,
                Active: true,
                CreatedDate: new Date(),
                TypeID: 1,
                StatusID: 1,
                StateID: 1,
            };
            salesReturn.SalesOrderIDs = [];
            salesReturn.SalesItems = [];
            salesReturn.SalesOrderIDs.push(this.salesOrder.ID);
            for (let i in this.itemsModel) {
                if (this.itemsModel[i] && this.itemsModel[i].selectedQuantity > 0) {
                    salesReturn.SalesItems.push({
                        ID: this.itemsModel[i].ID,
                        ExtendedPrice: 0.00,
                        ItemType: null,
                        RestockingFeeAmount: 0.00,
                        Quantity: this.itemsModel[i].selectedQuantity,
                        UnitCorePrice: 0.00,
                        Active: true,
                        CreatedDate: new Date(),
                        ProductID: this.itemsModel[i].ProductID,
                        Description: this.itemsModel[i].itemNote,
                        SalesReturnReasonID: this.itemsModel[i].selectedReason
                    });
                }
            }
            this.cvApi.providers.CreateSalesReturnFromStorefront(salesReturn).then(r => {
                if (!r.data.ActionSucceeded) {
                    this.cvMessageModalFactory(r.data.Messages[0]);
                    return;
                }
                this.$state.go("userDashboard.salesReturns.print", { ID: r.data.Result });
            });
        }
        // Constructor
        constructor(
                private readonly $window: ng.IWindowService,
                private readonly $state: ng.ui.IStateService,
                private readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvProductService: services.IProductService,
                private readonly cvCartService: services.ICartService,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvMessageModalFactory: store.modals.IMessageModalFactory) {
            super(cefConfig);
            this.id = this.$stateParams.ID;
            if (!this.id) {
                const parts = this.$window.location.pathname.split("/");
                this.id = Number(parts[parts.length - 1]);
            }
            this.load();
        }
    }

    cefApp.directive("cefSalesReturnCreate", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { type: "@" },
        transclude: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/sales/salesReturnCreate.html", "ui"),
        controller: SalesReturnCreateController,
        controllerAs: "salesReturnCreateCtrl"
    }));
}
