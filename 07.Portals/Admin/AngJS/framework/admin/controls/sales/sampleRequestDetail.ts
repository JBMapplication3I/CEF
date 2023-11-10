module cef.admin.controls.sales {
    export class SampleRequestDetailController extends core.TemplatedControllerBase {
        // Properties
        states: api.RegionModel[] = [];
        countries: api.CountryModel[] = [];
        sampleRequest: api.SampleRequestModel;
        sampleRequestItems: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>[] = [];
        paymentMethods: api.PaymentMethodModel[] = [];
        types: api.TypeModel[] = [];
        statuses: api.StatusModel[] = [];
        users: api.UserModel[] = [];
        attributes: api.GeneralAttributeModel[] = [];
        accounts: api.AccountModel[] = [];
        products: api.ProductModel[] = [];
        warehouses: api.InventoryLocationModel[] = [];
        items = [];
        selected;
        discountItems = [];
        showAddressEditors = false;
        selectedNotification: number = null;
        orderKeyTxt: string;
        purchaseOrderNoTxt: string;

        gridConfiguration: kendo.ui.GridOptions = {
            columns: [
                { field: "ItemType", title: "Type", width: "60px;", headerAttributes: { style: "text-align: center;" } },
                { field: "ProductName", title: "Product Name" },
                { field: "Sku", title: "SKU" },
                { field: "ShippingCarrierMethodName", title: "Ship Method" },
                { field: "StatusName", title: "Status", width: "200px" },
                { field: "UnitCorePrice", format: "${0:#,##0.00}", title: "Unit $", width: "100px", attributes: { style: "text-align: right;" }, headerAttributes: { style: "text-align: center;" } },
                { field: "UnitSoldPrice", format: "${0:#,##0.00}", title: "Sold at $", width: "100px", attributes: { style: "text-align: right;" }, headerAttributes: { style: "text-align: center;" } },
                { field: "Quantity", format: "{0:#,##0}", width: "75px", attributes: { style: "text-align: right;" }, headerAttributes: { style: "text-align: center;" } },
                { field: "ExtendedPrice", format: "${0:#,##0.00}", title: "Total $", width: "100px", attributes: { style: "text-align: right;" }, headerAttributes: { style: "text-align: center;" } }
            ],
            scrollable: false,
            pageable: false
        };

        discountsGridConfiguration: kendo.ui.GridOptions = {
            columns: [
                { field: "ItemType", title: "Type", width: "60px;", headerAttributes: { style: "text-align: center;" } },
                { field: "Name", title: "Name" },
                { field: "DiscountPrice", format: "${0:#,##0.00}", title: "Discount Value", width: "100px", attributes: { style: "text-align: right;" }, headerAttributes: { style: "text-align: center;" } }
            ],
            scrollable: false,
            pageable: false
        }

        updateShippingAddress = () => {
            // TODO: Assign the fields from the BillingContact to the ShippingContact for the view
        }

        createNewContact(): api.ContactModel {
            return <api.ContactModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                FirstName: null,
                MiddleName: null,
                LastName: null,
                FullName: null,
                Phone1: null,
                Phone2: null,
                Phone3: null,
                Fax1: null,
                Fax2: null,
                Fax3: null,
                Email1: null,
                Email2: null,
                Email3: null,
                Website1: null,
                Website2: null,
                Website3: null,
                NotificationPhone: null,
                NotificationEmail: null,
                NotificationViaEmail: false,
                NotificationViaSMSPhone: false,
                DateOfBirth: null,
                Gender: false,
                SameAsBilling: false,
                TypeID: 0,
                Address: <api.AddressModel>{
                    ID: 0,
                    Active: true,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    CustomKey: null,
                    IsBilling: true,
                    IsPrimary: true,
                    Name: null,
                    Description: null,
                    FirstName: null,
                    LastName: null,
                    Company: null,
                    Phone: null,
                    Fax: null,
                    Email: null,
                    Street1: null,
                    Street2: null,
                    Street3: null,
                    City: null,
                    RegionID: 0,
                    RegionCustom: null,
                    RegionName: null,
                    CountryID: 0,
                    CountryCode: null,
                    CountryName: null,
                    PostalCode: null,
                    Region: null,
                    Country: null,
                    Latitude: null,
                    Longitude: null
                }
            };
        }

        newOrder = () => {
            this.sampleRequest = <api.SampleRequestModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // SalesItemCollection Properties
                SourceType: null,
                SourceID: 0,
                SampleRequestTypeID: 1,
                ContactsUserID: 0,
                ItemQuantity: 0,
                BillingContact: this.createNewContact(),
                ShippingContact: this.createNewContact(),
                ShippingDetail: null,
                RequestedShipDate: null,
                SubtotalItems: 0,
                Totals: <api.CartTotals>{
                    SubTotal: 0,
                    Discounts: 0,
                    Shipping: 0,
                    Handling: 0,
                    Fees: 0,
                    Tax: 0,
                    Total: 0
                },
                SalesItems: [],
                SalesItemCollectionAttributes: null,
                FlattenedSalesItems: [],
                // SampleRequest Properties
                HasChildren: false,
                OrderDate: null,
                Balance: 0,
                ShippingSameAsBilling: false,
                TypeID: 1,
                StatusID: 1,
                StateID: 1,
                AccountKey: null,
                SampleRequestStateName: null,
                TrackingNumber: null,
                UserID: 0,
                User: null,
                SampleRequestStatusID: 1,
                SampleRequestStatus: null,
                InvoiceIDs: [],
                PurchaseOrderIDs: [],
                PaymentDetails: [],
                SampleRequestItems: [],
                SampleRequestAttributes: [],
                SampleRequestOrderDiscounts: [],
                Notes: [],

                TypeName: null,
                Type: null,
                StatusName: null,
                Status: null,
                ShipOption: null,
                ShippingAddress: null,
                Discounts: [],
                Attributes: []
            };
        }

        load = () => {
            const paging = <api.Paging>{ StartIndex: 1, Size: 100 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.cvApi.payments.GetPaymentMethods(standardDto).then(r => this.paymentMethods = r.data.Results);
            this.cvApi.geography.GetCountries(standardDto).then(r => this.countries = r.data.Results);
            this.cvApi.geography.GetRegions(standardDto).then(r => this.states = r.data.Results);
            this.cvApi.inventory.GetInventoryLocations(standardDto).then(r => this.warehouses = r.data.Results);
            this.cvApi.sampling.GetSampleRequestTypes(standardDto).then(r => this.types = r.data.Results);
            this.cvApi.sampling.GetSampleRequestStatuses(standardDto).then(r => this.statuses = r.data.Results);
            this.cvApi.contacts.GetUsers(standardDto).then(r => this.users = r.data.Results);
            this.cvApi.accounts.GetAccounts(standardDto).then(r => this.accounts = r.data.Results);
            this.cvApi.attributes.GetGeneralAttributes(standardDto).then(r => this.attributes = r.data.Results);
            // Load Sales Order
            if (this.sampleRequest.ID > 0) {
                this.cvApi.sampling.GetSampleRequestByID(this.sampleRequest.ID).then(r => {
                    this.sampleRequest = r.data;
                    if (this.sampleRequest.BillingContact && this.sampleRequest.ShippingContact
                        && this.sampleRequest.BillingContact.FirstName === this.sampleRequest.ShippingContact.FirstName
                        && this.sampleRequest.BillingContact.LastName === this.sampleRequest.ShippingContact.LastName
                        && this.sampleRequest.BillingContact.Address.Street1 === this.sampleRequest.ShippingContact.Address.Street1
                        && this.sampleRequest.BillingContact.Address.Street2 === this.sampleRequest.ShippingContact.Address.Street2
                        && this.sampleRequest.BillingContact.Address.Street3 === this.sampleRequest.ShippingContact.Address.Street3
                        && this.sampleRequest.BillingContact.Address.City === this.sampleRequest.ShippingContact.Address.City
                        && this.sampleRequest.BillingContact.Address.PostalCode === this.sampleRequest.ShippingContact.Address.PostalCode
                        && this.sampleRequest.BillingContact.Address.RegionID === this.sampleRequest.ShippingContact.Address.RegionID
                        && this.sampleRequest.BillingContact.Address.CountryID === this.sampleRequest.ShippingContact.Address.CountryID
                        && this.sampleRequest.BillingContact.Phone1 === this.sampleRequest.ShippingContact.Phone1
                        && this.sampleRequest.BillingContact.Phone2 === this.sampleRequest.ShippingContact.Phone2
                        && this.sampleRequest.BillingContact.Phone3 === this.sampleRequest.ShippingContact.Phone3
                        && this.sampleRequest.BillingContact.Fax1 === this.sampleRequest.ShippingContact.Fax1
                        && this.sampleRequest.BillingContact.Email1 === this.sampleRequest.ShippingContact.Email1) {
                        this.sampleRequest.ShippingSameAsBilling = true;
                    }
                    this.loadDiscountLineItems();
                });
            }
        }

        loadDiscountLineItems = () => {
            this.sampleRequest.Discounts.forEach(item => {
                var itemType: string;
                switch (item.DiscountTypeID) {
                    case 1: {
                        itemType = "Shipping";
                        break;
                    }
                    default: {
                        itemType = "Order";
                        break;
                    }
                }
                this.discountItems.push({
                    Type: itemType,
                    Name: item.SlaveName,
                    DiscountPrice: item.DiscountValue
                });
            });
        }

        showAddProducts = () => {
            this.$uibModal.open({
                templateUrl: "selectSampleRequestItemModalContent.html",
                controller: "SelectSampleRequestItemModal",
                resolve: {
                    items: () => this.items
                }
            }).result.then(selectedItem => { this.selected = selectedItem; }, () => { });
        }

        formatDate = (jsonDate) => { return new Date(parseInt(jsonDate)); }

        /*//confirmStockOrder = () => {
        ////    // TODO: I feel like this isn't the correct way to do this.
        ////    this.selectedNotification = 5;
        ////    this.sendNotification();   // Sends the email
        ////    // TODO: Need to capture payment that was previously authorized
        ////    // Find the correct status, and then set it
        ////    var completedid = -1;
        ////    this.statuses.forEach((item) => {
        ////        if (item.Name === "Ready to Pickup at Store") {
        ////            completedid = item.ID;
        ////        }
        ////    });
        ////    if (completedid === -1) {
        ////        aler t("\"Ready to Pickup at Store\" not found in statuses");
        ////        return;
        ////    }
        ////    this.sampleRequest.StatusID = completedid;
        ////    // TODO: The save currently isn't saving the status.  Need to figure out why.
        ////    this.save(); // Saves the changes in the order, including the new status, and then loads the sales open orders search
        ////}

        ////insufficientStockOrder = () => {
        ////    this.selectedNotification = 6;
        ////    this.sendNotification();   // Sends the email
        ////    // Find the correct status, and then set it
        ////    var completedid = -1;
        ////    this.statuses.forEach((item) => {
        ////        if (item.Name === "Insufficient Stock for Store Pickup") {
        ////            completedid = item.ID;
        ////        }
        ////    });
        ////    if (completedid === -1) {
        ////        aler t("\"Insufficient Stock for Store Pickup\" not found in statuses");
        ////        return;
        ////    }
        ////    this.sampleRequest.StatusID = completedid;
        ////    // TODO: The save currently isn't saving the status.  Need to figure out why.
        ////    this.save(this.$ router.g enerate('sales:sampleRequestsSearch')); // Saves the changes in the order, including the new status, and then loads the sales open orders search
        ////}*/

        // Order Actions
        confirmOrder = () => {
            if (confirm("Are you sure you want to Confirm this Order? This action cannot be undone and the customer will receive an email notification.")) {
                this.cvApi.providers.SetSampleRequestAsConfirmed(this.sampleRequest.ID).success(result => {
                    if (!result || !result.ActionSucceeded) {
                        console.error(result);
                        return;
                    }
                    // Refresh the data
                    this.load();
                }).error(response => { console.log(response); });
            }
        }
        backorderOrder = () => {
            if (confirm("Are you sure you want to Backorder this Order? This action cannot be undone and the customer will receive an email notification.")) {
                this.cvApi.providers.SetSampleRequestAsBackordered(this.sampleRequest.ID).success(result => {
                    if (!result || !result.ActionSucceeded) {
                        console.error(result);
                        return;
                    }
                    // Refresh the data
                    this.load();
                }).error(response => { console.log(response); });
            }
        }
        splitOrder = () => {
            /*
            if (confirm("Are you sure you want to Split this Order based on Item Statuses? This action cannot be undone and the customer will receive an email notification.")) {
                this.cvApi.sampling.SplitSampleRequest(this.sampleRequest.ID).success(result => {
                    if (!result || !result.ActionSucceeded) {
                        console.error(result);
                        return;
                    }
                    // Refresh the data
                    this.load();
                }).error(response => { console.log(response); });
            }
            */
        }
        createInvoiceForOrder = () => {
            if (confirm("Are you sure you want to Create an Invoice for this Order? This action cannot be undone and the customer will receive an email notification.")) {
                this.cvApi.providers.CreateInvoiceForSampleRequest(this.sampleRequest.ID).success(result => {
                    if (!result || !result.ActionSucceeded) {
                        console.error(result);
                        return;
                    }
                    // Refresh the data
                    this.load();
                }).error(response => { console.log(response); });
            }
        }
        addPaymentToOrder = () => {
            /*this.cvApi.ordering.AddPaymentToSampleRequest(this.sampleRequest.ID, { ID: this.sampleRequest.ID, Payment: null }).success(result => {
            //    if (!result || !result.ActionSucceeded) {
            //        console.error(result);
            //        return;
            //    }
            //    // Refresh the data
            //    this.load();
            //}).error(response => { console.log(response); });*/
        }
        createPickTicketForOrder = () => {
            this.cvApi.providers.CreatePickTicketForSampleRequest(this.sampleRequest.ID).success(result => {
                if (!result || !result.ActionSucceeded) {
                    console.error(result);
                    return;
                }
                // Refresh the data
                this.load();
            }).error(response => { console.log(response); });
        }
        dropShipOrder = () => {
            if (confirm("Are you sure you want to Drop Ship this Order? This action cannot be undone and the customer will receive an email notification.")) {
                this.cvApi.providers.SetSampleRequestAsDropShipped(this.sampleRequest.ID).success(result => {
                    if (!result || !result.ActionSucceeded) {
                        console.error(result);
                        return;
                    }
                    // Refresh the data
                    this.load();
                }).error(response => { console.log(response); });
            }
        }
        shipOrder = () => {
            if (confirm("Are you sure you want to Ship this Order? This action cannot be undone and the customer will receive an email notification.")) {
                this.cvApi.providers.SetSampleRequestAsShipped(this.sampleRequest.ID).success(result => {
                    if (!result || !result.ActionSucceeded) {
                        console.error(result);
                        return;
                    }
                    // Refresh the data
                    this.load();
                }).error(response => { console.log(response); });
            }
        }
        completeOrder = () => {
            if (confirm("Are you sure you want to Complete this Order? This action cannot be undone and the customer will receive an email notification.")) {
                this.cvApi.providers.SetSampleRequestAsCompleted(this.sampleRequest.ID).success(result => {
                    if (!result || !result.ActionSucceeded) {
                        console.error(result);
                        return;
                    }
                    // Refresh the data
                    this.load();
                }).error(response => { console.log(response); });
            }
        }
        voidOrder = () => {
            if (confirm("Are you sure you want to Void this Order? This action cannot be undone and the customer will receive an email notification.")) {
                this.cvApi.providers.SetSampleRequestAsVoided(this.sampleRequest.ID).success(result => {
                    if (!result || !result.ActionSucceeded) {
                        console.error(result);
                        return;
                    }
                    // Refresh the data
                    this.load();
                }).error(response => { console.log(response); });
            }
        }
        addPaymentToInvoice = () => { console.warn("Add Payment to Invoice clicked but is not implemented yet"); }
        capturePayment = () => { console.warn("Capture Payment clicked but is not implemented yet"); }

        // Form Actions
        save = () => {
            if (this.sampleRequest.ID > 0) {
                this.cvApi.sampling.UpdateSampleRequest(this.sampleRequest).success(() => {
                    // Refresh the data
                    this.load();
                });
            } else {
                this.cvApi.sampling.CreateSampleRequest(this.sampleRequest).success(() => {
                    // Refresh the data
                    this.load();
                });
            }
        }

        constructor(
                private readonly $stateParams: ng.ui.IStateParamsService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            $translate("ui.admin.controls.sales.sampleRequestDetail.OrderKeyEllipse").then(x => this.orderKeyTxt = x);
            $translate("ui.admin.controls.sales.sampleRequestDetail.PONumberPlaceholder").then(x => this.purchaseOrderNoTxt = x);
            const id = this.$stateParams.ID;
            this.newOrder();
            if (id > 0) { this.sampleRequest.ID = id; } else { this.showAddressEditors = true; }
            this.items = ["item1", "item2", "item3"];
            this.load();
        }
    }

    adminApp.directive("sampleRequestDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/sampleRequestDetail.html", "ui"),
        controller: SampleRequestDetailController,
        controllerAs: "sampleRequestDetailCtrl"
    }));
}
