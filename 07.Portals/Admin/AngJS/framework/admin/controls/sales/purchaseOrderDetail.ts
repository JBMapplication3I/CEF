module cef.admin.controls.sales {
    export class PurchaseOrderDetailController extends core.TemplatedControllerBase {
        // Properties
        id: number;
        purchaseOrder: api.PurchaseOrderModel;
        purchaseOrderStatuses: Array<api.StatusModel> = [];
        vendors: Array<api.VendorModel> = [];
        warehouses: Array<api.InventoryLocationModel> = [];
        openSalesOrders: Array<api.SalesOrderModel> = [];
        attributes: api.GeneralAttributeModel[] = [];
        selectOrdersTxt: string;

        gridConfiguration: kendo.ui.GridOptions = {
            columns: [
                { field: "TypeName", title: "Type", width: "60px;", headerAttributes: { style: "text-align: center;" } },
                { field: "ProductName", title: "Product Name" },
                { field: "Sku", title: "SKU" },
                { field: "ShippingCarrierMethodName", title: "Ship Method" },
                { field: "UnitCorePrice", format: "${0:#,##0.00}", title: "Unit $", width: "100px", attributes: { style: "text-align: right;" }, headerAttributes: { style: "text-align: center;" } },
                {
                    field: "Quantity", format: "{0:#,##0}", width: "75px", attributes: { class: "text-align-right" }, headerAttributes: { class: "text-align-center" },
                    template: "<input type='number' ng-model='dataItem.Quantity' min='1' max='999999' step='1' class='text-align-center' ng-change='inventoryPurchaseOrderDetail.updateTotals(dataItem)' />"
                },
                { field: "ExtendedPrice", format: "${0:#,##0.00}", title: "Total $", width: "100px", attributes: { style: "text-align: right;" }, headerAttributes: { style: "text-align: center;" } },
                { field: "", title: "", width: "50px", template: "<a class='btn btn-sm btn-danger text-align-center' ng-click='inventoryPurchaseOrderDetail.removeSalesItem(dataItem.Sku)'><i class='far fa-times' /></a>" }
            ],
            scrollable: false,
            pageable: false
        };

        makePurchaseOrderModel = (): api.PurchaseOrderModel => {
            return <api.PurchaseOrderModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // SalesCollectionBase Properties
                Totals: <api.CartTotals>{
                    SubTotal: 0,
                    Shipping: 0,
                    Handling: 0,
                    Fees: 0,
                    Tax: 0,
                    Discounts: 0,
                    Total: 0
                },
                ItemQuantity: 0,
                OriginalDate: null,
                DueDate: null,
                ShippingSameAsBilling: false,
                AccountKey: null,
                BalanceDue: 0,
                TypeID: 1,
                TypeName: null,
                Type: null,
                StatusID: 1,
                Status: null,
                StatusName: null,
                StateID: 1,
                ShipOptionID: 0,
                ShipOption: null,
                ShippingDetail: null,
                UserID: 0,
                User: null,
                BillingContactID: 0,
                BillingContact: null,
                ShippingContactID: 0,
                ShippingContact: null,
                ShippingAddressID: 0,
                ShippingAddress: null,
                SalesItems: [],
                Discounts: [],
                Attributes: [],
                // PurchaseOrder Properties
                EstimatedArrivalDate: null,
                SalesOrderIDs: null,
                DateReleased: null,
                DateReceived: null,
                FOB: false,
                VendorID: 0,
                Vendor: this.makeVendorModel(),
                InventoryLocationID: 0,
                InventoryLocation: this.makeInventoryLocationModel(),
                Notes: [],
                FlattenedSalesItems: [],
                HasChildren: false
            };
        }

        makeVendorModel = (): api.VendorModel => {
            return <api.VendorModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CustomKey: null,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // ContactableBase Properties
                Phone: null,
                Fax: null,
                Email: null,
                // Vendor Properties
                Notes1: null,
                AccountNumber: null,
                Terms: null,
                TermNotes: null,
                SendMethod: null,
                EmailSubject: null,
                ShipTo: null,
                ShipViaNotes: null,
                SignBy: null,
                DefaultDiscount: null,
                AllowDropShip: false,
                CompanyID: 0,
                Company: null,
                ContactID: 0,
                Contact: null,
                AddressID: 0,
                Address: null,
                VendorsShipViaID: 0,
                ShipVia: null,
                TypeID: 0,
                TermID: 0,
                Term: null,
                MustResetPassword: false,
                MinimumOrderDollarAmountOverrideFeeIsPercent: false,
                MinimumOrderQuantityAmountOverrideFeeIsPercent: false,
                // Associated Objects
                PurchaseOrders: [],
                Shipments: [],
                Products: [],
                Notes: []
            };
        }

        makeInventoryLocationModel = (): api.InventoryLocationModel => {
            return <api.InventoryLocationModel>{
                // Base Properties
                ID: 0,
                Active: true,
                CustomKey: null,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // ContactableBase Properties
                Phone: null,
                Fax: null,
                Email: null,
                // InventoryLocation Properties
                AddressID: 0,
                Address: null,
                Sections: []
            };
        }

        loadCollections = () => {
            this.cvApi.purchasing.GetPurchaseOrderStatuses({ Active: true, AsListing: true }).success(results => {
                this.purchaseOrderStatuses = results.Results;
                if (!isNaN(this.id)) { return; }
                this.purchaseOrderStatuses.forEach(value => {
                    if (value.Name === "In Progress") {
                        this.purchaseOrder.StatusID = value.ID;
                    }
                });
            });
            this.cvApi.attributes.GetGeneralAttributes({ Active: true, AsListing: true, TypeName: "Purchase Order", IncludeGeneralWithTypeName: true }).then(r => { this.attributes = r.data.Results; });
            this.cvApi.vendors.GetVendors({ Active: true, AsListing: true }).then(r => this.vendors = r.data.Results);
            this.cvApi.inventory.GetInventoryLocations().then(r => { this.warehouses = r.data.Results; });
            this.cvApi.ordering.GetSalesOrders({ Active: true, AsListing: true, ExcludedStateKey: "Completed" }).then(r => this.openSalesOrders = r.data.Results);
            if (this.id > 0) {
                // Load purchase order details
                this.cvApi.purchasing.GetPurchaseOrderByID(this.id).success(data => { this.purchaseOrder = data; });
            } else {
                // Set current date for all new purchase orders
                const fullDate = new Date();
                const twoDigitMonth = ((fullDate.getMonth().toString().length + 1) === 1) ? (fullDate.getMonth() + 1).toString() : `0${fullDate.getMonth() + 1}`;
                const currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
                this.purchaseOrder.CreatedDate = new Date(currentDate);
            }
        }

        updateTotals = (dataItem?: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>) => {
            this.purchaseOrder.Totals.SubTotal = 0;
            this.purchaseOrder.SalesItems.forEach(value => {
                if (dataItem && value.Sku === dataItem.Sku) {
                    value.Quantity = dataItem.Quantity;
                }
                value.ExtendedPrice = value.Quantity * value.UnitCorePrice;
                this.purchaseOrder.Totals.SubTotal += value.ExtendedPrice;
            });
            if (this.purchaseOrder.Totals.SubTotal === 0) {
                this.purchaseOrder.Totals.Shipping = 0;
            }
            this.purchaseOrder.Totals.Total = this.purchaseOrder.Totals.Shipping + this.purchaseOrder.Totals.SubTotal;
        }

        loadVendor = () => {
            if (this.purchaseOrder.VendorID) {
                this.cvApi.vendors.GetVendorByID(this.purchaseOrder.VendorID).success(result => { this.purchaseOrder.Vendor = result; });
            } else {
                this.purchaseOrder.Vendor = this.makeVendorModel();
            }
        }

        loadWarehouse = () => {
            if (this.purchaseOrder.InventoryLocationID) {
                this.cvApi.inventory.GetInventoryLocationByID(this.purchaseOrder.InventoryLocationID).success(result => { this.purchaseOrder.InventoryLocation = result; });
            } else {
                this.purchaseOrder.InventoryLocation = this.makeInventoryLocationModel();
            }
        }

        removeSalesItem = (sku: string) => {
            var toRemove = _.findIndex(this.purchaseOrder.SalesItems, value => value.Sku === sku);
            this.purchaseOrder.SalesItems.splice(toRemove, 1);
            this.updateTotals();
        }

        save = (path) => {
            if (this.purchaseOrder.ID > 0) {
                this.cvApi.purchasing.UpdatePurchaseOrder(this.purchaseOrder).then(() => this.$location.path(path));
            } else {
                this.cvApi.purchasing.CreatePurchaseOrder(this.purchaseOrder).then(() => this.$location.path(path));
            }
        }

        constructor(
                private readonly $stateParams: ng.ui.IStateParamsService,
                private readonly $location: ng.ILocationService,
                readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.id = this.$stateParams.ID;
            this.purchaseOrder = this.makePurchaseOrderModel();
            this.loadCollections();
            $translate("ui.admin.controls.sales.purchaseOrderDetail.SelectTheOrders").then(x => this.selectOrdersTxt = x);
        }
    }

    adminApp.directive("purchaseOrderDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchaseOrderDetail.html", "ui"),
        controller: PurchaseOrderDetailController,
        controllerAs: "purchaseOrderDetailCtrl"
    }));
}
