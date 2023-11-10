/**
 * @file framework/store/userDashboard/controls/sales/salesDetail.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Sales detail class
 */
module cef.store.userDashboard.controls {
    interface ISalesGroupUIContent {
        type: string;
        subType: string;
        title?: string;
        loading: boolean;
    }
    export class TrackingMatcher {
        carrier: string;
        method: string;
        regex: RegExp;
        link: string;
    }

    class SalesDetailController extends core.TemplatedControllerBase {
        // Bound Scope Properies
        type: string;
        allowPayoneer: boolean;
        allowContactSeller: boolean;
        // Properties
        status = "searching";
        salesOrder: api.SalesOrderModel;
        salesInvoice: api.SalesInvoiceModel;
        shipment: api.ShipmentModel;
        salesQuote: api.SalesQuoteModel;
        sampleRequest: api.SampleRequestModel;
        salesReturn: api.SalesReturnModel;
        subscription: api.SubscriptionModel;
        subscriptionHistory: api.SubscriptionHistoryModel[];
        salesObject: api.SalesCollectionBaseModelT<api.TypeModel,
            api.AmARelationshipTableModel<api.ContactModel>,
            api.SalesEventBaseModel,
            api.AppliedDiscountBaseModel,
            api.AppliedDiscountBaseModel,
            api.AmAStoredFileRelationshipTableModel>;
        salesGroupBillingContact: api.ContactModel;
        sellerUserID: number;
        itemsPaging: core.Paging<api.SalesItemBaseModel<api.AppliedDiscountBaseModel>>;
        id: number;
        salesObjectType: string;
        readyForReturn = false;
        newNoteText = "";
        sendNewNoteButtonDisabled = false;
        sendNewNoteButtonText = "Add Note";
        content: ISalesGroupUIContent = { type: null, subType: null, loading: false };
        events = {
            aggregate: [],
            loading: true
        };
        salesGroupRecord: api.SalesGroupModel;
        reorderAllEnabled: boolean = !this.cefConfig.paymentHubEnabled;
        balanceDueDisabled: boolean = true;
        get today(): Date {
            return new Date();
        }
        get hasBalanceDue(): boolean {
            if (!this.salesObject
                || !this.salesObject.BalanceDue
                || this.salesObject.BalanceDue < 0) {
                return false;
            }
            return true;
        }
        get hasDueDate(): boolean {
            if (!this.hasBalanceDue) {
                return false;
            }
            return angular.isDefined(this.salesInvoice["DueDate"]);
        }
        private parseISOString(s: string): Date {
            const b = s.split(/\D+/);
            return new Date(Date.UTC(Number(b[0]), Number(b[1]) - 1, Number(b[2]), Number(b[3]), Number(b[4]), Number(b[5]), Number(b[6])));
        }
        get balanceIsPassedDue(): boolean {
            if (!this.hasBalanceDue || !this.hasDueDate) {
                return false;
            }
            if (angular.isString(this.salesInvoice["DueDate"])) {
                const temp = this.$filter("convertJSONDate")(this.salesInvoice["DueDate"]);
                if (angular.isString(temp)) {
                    return this.parseISOString(temp as any as string) < this.today;
                }
                return temp < this.today;
            }
            return this.salesInvoice["DueDate"] < this.today;
        }
        get reorderAllQuoteMenuEnabled(): boolean {
            return this.cefConfig.featureSet.salesQuotes.useQuoteCart;
        }
        // Functions
        checkForCustomSalesItems(salesItems: api.SalesItemBaseModel<api.AppliedSalesOrderItemDiscountModel>[]): void {
            if (!salesItems) { return; }
            salesItems.forEach(item => {
                if (!item.ProductID
                    && !item.ProductKey
                    && !item.ProductName
                    && !item.ProductSeoUrl) {
                    this.reorderAllEnabled = false;
                }
            });
        }
        load(): void {
            this.cvApi.sales.GetSecureSalesGroup(Number(this.$stateParams["GroupID"])).then(r => {
                if (!r || !r.data) {
                    this.finishRunning(true, this.$translate.instant(
                        "ui.storefront.userDashboard2.controls.salesGroupDetail.UnableToFindRecordWithID")
                        + this.$stateParams["GroupID"]);
                    return;
                }
                this.salesGroupRecord = r.data;
                this.loadAggregateEvents();
                if (!this.$stateParams["ID"]) {
                   this.setContent(null, null);
                   return;
                }
                if (this.$state.includes("userDashboard.salesGroup.detail.quote")) {
                    this.setContent(
                        "SalesQuote",
                        _.some(
                            this.salesGroupRecord.SalesQuoteRequestMasters,
                            x => x.ID === Number(this.$stateParams["ID"]))
                                ? "request-master"
                                : _.some(
                                    this.salesGroupRecord.SalesQuoteRequestSubs,
                                    x => x.ID === Number(this.$stateParams["ID"]))
                                        ? "request-sub"
                                        : _.some(
                                            this.salesGroupRecord.SalesQuoteResponseMasters,
                                            x => x.ID === Number(this.$stateParams["ID"]))
                                                ? "response-master"
                                                : _.some(
                                                    this.salesGroupRecord.SalesQuoteResponseSubs,
                                                    x => x.ID === Number(this.$stateParams["ID"]))
                                                        ? "response-sub"
                                                        : "invalid",
                        Number(this.$stateParams["ID"]));
                    return;
                }
                if (this.$state.includes("userDashboard.salesGroup.detail.order")) {
                    this.setContent(
                        "SalesOrder",
                        _.some(
                            this.salesGroupRecord.SalesOrderMasters[0],
                            x => x.ID === Number(this.$stateParams["ID"]))
                                ? "master"
                                : "sub",
                        Number(this.$stateParams["ID"]));
                    return;
                }
                if (this.$state.includes("userDashboard.salesGroup.detail.invoice")) {
                    this.setContent(
                        "SalesInvoice",
                        null,
                        Number(this.$stateParams["ID"]));
                    return;
                }
                if (this.$state.includes("userDashboard.salesGroup.detail.return")) {
                    this.setContent(
                        "SalesReturn",
                        null,
                        Number(this.$stateParams["ID"]));
                    return;
                }
                this.setContent(null, null);
            }).catch(reason => this.finishRunning(true, reason));
            switch (this.type.toLowerCase()) {
                case "quote": case "sales quote": case "sales-quote": {
                    this.salesObjectType = "Sales Quote";
                    this.setRunning();
                    this.cvApi.providers.GetSecureSalesQuote(this.id).then(r => {
                        if (r.data.SalesGroupAsRequestMasterID > 0
                            && !this.$state.includes("userDashboard.salesGroups.detail.quote")) {
                            this.$filter("goToCORSLink")(
                                "DashboardState:userDashboard.salesGroups.detail.quote",
                                "dashboard",
                                "primary",
                                false,
                                { "GroupID": r.data.SalesGroupAsRequestMasterID, "ID": r.data.ID });
                            return;
                        }
                        if (r.data.SalesGroupAsRequestSubID > 0
                            && !this.$state.includes("userDashboard.salesGroups.detail.quote")) {
                            this.$filter("goToCORSLink")(
                                "DashboardState:userDashboard.salesGroups.detail.quote",
                                "dashboard",
                                "primary",
                                false,
                                { "GroupID": r.data.SalesGroupAsRequestSubID, "ID": r.data.ID });
                            return;
                        }
                        if (r.data.SalesGroupAsResponseMasterID > 0
                            && !this.$state.includes("userDashboard.salesGroups.detail.quote")) {
                            this.$filter("goToCORSLink")(
                                "DashboardState:userDashboard.salesGroups.detail.quote",
                                "dashboard",
                                "primary",
                                false,
                                { "GroupID": r.data.SalesGroupAsResponseMasterID, "ID": r.data.ID });
                            return;
                        }
                        if (r.data.SalesGroupAsResponseSubID > 0
                            && !this.$state.includes("userDashboard.salesGroups.detail.quote")) {
                            this.$filter("goToCORSLink")(
                                "DashboardState:userDashboard.salesGroups.detail.quote",
                                "dashboard",
                                "primary",
                                false,
                                { "GroupID": r.data.SalesGroupAsResponseSubID, "ID": r.data.ID });
                            return;
                        }
                        this.salesQuote = this.salesObject = <any>r.data;
                        this.loadDiscounts().then(() => {
                            this.setupItemsPaging();
                            this.loadSellerUserID(); // Will call finishRunning
                        });
                    });
                    break;
                }
                case "order": case "sales order": case "sales-order": {
                    this.salesObjectType = "Sales Order";
                    this.setRunning();
                    this.cvApi.ordering.GetSecureSalesOrder(this.id).then(r => {
                        if (r.data.SalesGroupAsMasterID > 0
                            && !this.$state.includes("userDashboard.salesGroups.detail.order")) {
                            this.$filter("goToCORSLink")(
                                "DashboardState:userDashboard.salesGroups.detail.order",
                                "dashboard",
                                "primary",
                                false,
                                { "GroupID": r.data.SalesGroupAsMasterID, "ID": r.data.ID });
                            return;
                        }
                        if (r.data.SalesGroupAsSubID > 0
                            && !this.$state.includes("userDashboard.salesGroups.detail.order")) {
                            this.$filter("goToCORSLink")(
                                "DashboardState:userDashboard.salesGroups.detail.order",
                                "dashboard",
                                "primary",
                                false,
                                { "GroupID": r.data.SalesGroupAsSubID, "ID": r.data.ID });
                            return;
                        }
                        this.salesOrder = this.salesObject = <any>r.data;
                        this.setupItemsPaging();
                        if (this.salesOrder.SalesGroupAsMaster
                            && this.salesOrder.SalesGroupAsMaster.BillingContactID) {
                            this.cvApi.contacts.GetContactByID(
                                this.salesOrder.SalesGroupAsMaster.BillingContactID
                            ).then(r2 => this.salesGroupBillingContact = r2.data);
                        }
                        this.loadDiscounts().then(() => {
                            this.cvApi.providers.IsSalesOrderReadyForReturn(this.id)
                                .then(r2 => this.readyForReturn = r2.data.ActionSucceeded);
                            this.setupItemsPaging();
                            this.loadSellerUserID(); // Will call finishRunning
                        });
                        let salesGroupID = this.salesOrder.SalesGroupAsMasterID
                            ? this.salesOrder.SalesGroupAsMasterID
                            : this.salesOrder.SalesGroupAsSubID;
                        if (!salesGroupID) {
                            return;
                        }
                        this.cvApi.sales.GetSecureSalesGroup(salesGroupID).then(r2 => {
                            this.salesInvoice = r2.data.SalesInvoices ?? []
                                .find(x => x.CustomKey === "InvoiceForOrder-" + this.salesObject.ID);
                            if (this.salesInvoice
                                && this.salesInvoice.BalanceDue
                                && this.salesInvoice.BalanceDue > 0) {
                                this.salesObject.BalanceDue = this.salesInvoice.BalanceDue;
                            }
                            this.balanceDueDisabled = false;
                        });
                    });
                    break;
                }
                case "invoice": case "sales invoice": case "sales-invoice": {
                    this.salesObjectType = "Sales Invoice";
                    this.setRunning();
                    this.cvApi.providers.GetSecureSalesInvoice(this.id).then(r => {
                        if (r.data.SalesGroupID > 0
                            && !this.$state.includes("userDashboard.salesGroups.detail.invoice")) {
                            this.$filter("goToCORSLink")(
                                "DashboardState:userDashboard.salesGroups.detail.invoice",
                                "dashboard",
                                "primary",
                                false,
                                { "GroupID": r.data.SalesGroupID, "ID": r.data.ID });
                            return;
                        }
                        this.salesInvoice = this.salesObject = <any>r.data;
                        this.checkForCustomSalesItems(this.salesInvoice.SalesItems);
                        this.loadDiscounts().then(() => {
                            this.setupItemsPaging();
                            this.loadSellerUserID(); // Will call finishRunning
                        });
                    });
                    break;
                }
                case "shipment": case "sales shipment": case "sales-shipment": {
                    this.salesObjectType = "Sales Shipment";
                    this.setRunning();
                    this.cvApi.shipping.GetShipmentByID({ShipmentID: this.id}).then(r => {
                        if (r.data.SalesGroupID > 0
                            && !this.$state.includes("userDashboard.salesGroups.detail.shipping")) {
                            this.$filter("goToCORSLink")(
                                "DashboardState:userDashboard.salesGroups.detail.shipping",
                                "dashboard",
                                "primary",
                                false,
                                { "GroupID": r.data.SalesGroupID, "ID": r.data.ID });
                            return;
                        }
                        this.shipment = <api.ShipmentModel>r.data;
                        // this.checkForCustomSalesItems(this.salesInvoice.SalesItems);
                        // this.loadDiscounts().then(() => {
                        //     this.setupItemsPaging();
                        //     this.loadSellerUserID(); // Will call finishRunning
                        // });
                    });
                    break;
                }
                case "sample": case "sample request": case "sample-request": {
                    this.salesObjectType = "Sample Request";
                    this.setRunning();
                    this.cvApi.providers.GetSecureSampleRequest(this.id).then(r => {
                        /*
                        if (r.data.SalesGroupID > 0
                            && !this.$state.includes("userDashboard.salesGroups.detail.sample")) {
                            this.$filter("goToCORSLink")(
                                "DashboardState:userDashboard.salesGroups.detail.sample",
                                "dashboard",
                                "primary",
                                false,
                                { "GroupID": r.data.SalesGroupID, "ID": r.data.ID });
                            return;
                        }
                        */
                        this.sampleRequest = this.salesObject = <any>r.data;
                        this.loadDiscounts().then(() => {
                            this.setupItemsPaging();
                            this.loadSellerUserID(); // Will call finishRunning
                        });
                    });
                    break;
                }
                case "return": case "sales return": case "sales-return": {
                    this.salesObjectType = "Sales Return";
                    this.setRunning();
                    this.cvApi.providers.GetSecureSalesReturn(this.id).then(r => {
                        if (r.data.SalesGroupID > 0
                            && !this.$state.includes("userDashboard.salesGroups.detail.return")) {
                            this.$filter("goToCORSLink")(
                                "DashboardState:userDashboard.salesGroups.detail.return",
                                "dashboard",
                                "primary",
                                false,
                                { "GroupID": r.data.SalesGroupID, "ID": r.data.ID });
                            return;
                        }
                        this.salesReturn = this.salesObject = <any>r.data;
                        this.loadDiscounts().then(() => {
                            this.setupItemsPaging();
                            this.loadSellerUserID(); // Will call finishRunning
                        });
                    });
                    break;
                }
                case "subscription": {
                    this.salesObjectType = "Subscription";
                    this.setRunning();
                    // possible cors error?
                    this.cvApi.payments.GetSubscriptionByID(this.id).then(r => {
                        this.subscription = this.salesObject = <any>r.data;
                        this.setupItemsPaging();
                        this.loadSellerUserID();
                    });
                    break;
                }
            }
        }
        private setContent(type: string, subType: string, id: number = null): void {
            switch (type) {
                case "SalesQuote": {
                    if (subType !== "request-master" && !id) {
                        return;
                    }
                    this.setRunning();
                    this.content = {
                        type: type,
                        subType: subType,
                        loading: true
                    };
                    switch (subType) {
                        case "request-master": {
                            this.content.title = this.$translate.instant(
                                "ui.storefront.userDashboard2.controls.salesGroupDetail.MasterQuoteRequest");
                            this.content.loading = false;
                            this.finishRunning();
                            return;
                        }
                        case "request-sub": {
                            this.content.title = this.$translate.instant(
                                "ui.storefront.userDashboard2.controls.salesGroupDetail.SubQuoteRequest");
                            this.content.loading = false;
                            this.finishRunning();
                            return;
                        }
                        case "response-master": {
                            this.content.title = this.$translate.instant(
                                "ui.storefront.userDashboard2.controls.salesGroupDetail.SellerResponseQuoteMaster");
                            this.content.loading = false;
                            this.finishRunning();
                            return;
                        }
                        case "response-sub": {
                            this.content.title = this.$translate.instant(
                                "ui.storefront.userDashboard2.controls.salesGroupDetail.SellerResponseQuoteSub");
                            this.content.loading = false;
                            this.finishRunning();
                            return;
                        }
                        default: {
                            this.content.loading = false;
                            // ERROR
                            return;
                        }
                    }
                }
                case "SalesOrder": {
                    if (subType !== "master" && !id) { return; }
                    this.setRunning();
                    this.content = {
                        type: type,
                        subType: subType,
                        loading: true
                    };
                    switch (subType) {
                        case "master": {
                            this.content.title = this.$translate.instant(
                                "ui.storefront.userDashboard2.controls.salesGroupDetail.MasterSalesOrder");
                            this.setContent(type, subType, this.salesGroupRecord.SalesOrderMasters[0].ID);
                            return;
                        }
                        case "sub": {
                            this.content.title = this.$translate.instant(
                                "ui.storefront.userDashboard2.controls.salesGroupDetail.SubOrder");
                            this.content.loading = false;
                            this.finishRunning();
                            return;
                        }
                        default: {
                            this.content.loading = false;
                            // ERROR
                            this.finishRunning(true);
                            return;
                        }
                    }
                }
                case "SalesInvoice": {
                    if (!id) {
                        return;
                    }
                    this.setRunning();
                    this.content = {
                        type: type,
                        subType: null,
                        loading: true
                    };
                    this.content.title = this.$translate.instant("ui.storefront.common.SalesInvoice");
                    this.content.loading = false;
                    this.finishRunning();
                    break;
                }
                case "SalesReturn": {
                    if (!id) {
                        return;
                    }
                    this.setRunning();
                    this.content = {
                        type: type,
                        subType: null,
                        loading: true
                    };
                    this.content.title = this.$translate.instant("ui.storefront.common.SalesReturn");
                    this.content.loading = false;
                    this.finishRunning();
                    break;
                }
                case "SampleRequest": {
                    if (!id) {
                        return;
                    }
                    this.setRunning();
                    this.content = {
                        type: type,
                        subType: null,
                        loading: true
                    };
                    this.content.title = this.$translate.instant("ui.storefront.common.SampleRequest");
                    this.content.loading = false;
                    this.finishRunning();
                    break;
                }
                default: {
                    this.setRunning();
                    this.$translate(
                        "ui.storefront.userDashboard2.controls.salesGroupDetail.SelectSomethingOnTheLeftToSeeMoreDetails"
                    ).then(translated => {
                        this.content = {
                            type: null,
                            subType: null,
                            title: translated,
                            loading: false
                        };
                        this.finishRunning();
                    });
                    return;
                }
            }
        }
        private loadAggregateEvents(): void {
            const dtoFactory = () => {
                return {
                    MasterID: 0,
                    Active: true,
                    AsListing: true,
                    Paging: <api.Paging>{ StartIndex: 1, Size: 20 }
                };
            };
            const promises = [];
            if (this.salesGroupRecord.SalesOrderMasters) {
                this.salesGroupRecord.SalesOrderMasters.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.ordering.GetSalesOrderEvents(dto));
                });
            }
            if (this.salesGroupRecord.SubSalesOrders) {
                this.salesGroupRecord.SubSalesOrders.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.ordering.GetSalesOrderEvents(dto));
                });
            }
            if (this.salesGroupRecord.SalesQuoteRequestMasters) {
                this.salesGroupRecord.SalesQuoteRequestMasters.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.quoting.GetSalesQuoteEvents(dto));
                });
            }
            if (this.salesGroupRecord.SalesQuoteRequestSubs) {
                this.salesGroupRecord.SalesQuoteRequestSubs.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.quoting.GetSalesQuoteEvents(dto));
                });
            }
            if (this.salesGroupRecord.SalesQuoteResponseMasters) {
                this.salesGroupRecord.SalesQuoteResponseMasters.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.quoting.GetSalesQuoteEvents(dto));
                });
            }
            if (this.salesGroupRecord.SalesQuoteResponseSubs) {
                this.salesGroupRecord.SalesQuoteResponseSubs.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.quoting.GetSalesQuoteEvents(dto));
                });
            }
            if (this.salesGroupRecord.SalesInvoices) {
                this.salesGroupRecord.SalesInvoices.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.invoicing.GetSalesInvoiceEvents(dto));
                });
            }
            if (this.salesGroupRecord.SalesReturns) {
                this.salesGroupRecord.SalesReturns.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.returning.GetSalesReturnEvents(dto));
                });
            }
            /* TODO
            if (this.salesGroupRecord.SampleRequests) {
                this.salesGroupRecord.SampleRequests.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.sampling.GetSampleRequestsEvents(dto));
                });
            }
            */
            this.$q.all(promises)
                .then((rarr: ng.IHttpPromiseCallbackArg<{ Results: api.SalesEventBaseModel[] }>[]) => {
                    let temp: Array<api.SalesEventBaseModel> = [];
                    rarr.forEach(r => temp.push(...r.data.Results));
                    temp = _.sortBy(temp, x => x.CreatedDate);
                    temp = temp.reverse();
                    this.events.aggregate = temp;
                });
            this.events.loading = false;
        }
        private loadDiscounts(): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                switch (this.type.toLowerCase()) {
                    case "quote": case "sales quote": case "sales-quote": {
                        this.cvApi.providers.GetDiscountsForQuote(this.salesQuote.ID).then(r => {
                            if (!r || !r.data) {
                                reject();
                                return;
                            }
                            this.salesQuote.Discounts = r.data.Discounts;
                            this.salesQuote.SalesItems.forEach(x => {
                                x.Discounts = _.filter(
                                    r.data.ItemDiscounts,
                                    y => y.MasterID === x.ID);
                                x["DiscountTotal"] = _.sumBy(
                                    x.Discounts,
                                    y => y.DiscountTotal);
                            });
                            resolve();
                        });
                        break;
                    }
                    case "order": case "sales order": case "sales-order": {
                        this.cvApi.providers.GetDiscountsForOrder(this.salesOrder.ID).then(r => {
                            if (!r || !r.data) {
                                reject();
                                return;
                            }
                            this.salesOrder.Discounts = r.data.Discounts;
                            this.salesOrder.SalesItems.forEach(x => {
                                x.Discounts = _.filter(
                                    r.data.ItemDiscounts,
                                    y => y.MasterID === x.ID);
                                x["DiscountTotal"] = _.sumBy(
                                    x.Discounts,
                                    y => y.DiscountTotal);
                            });
                            resolve();
                        });
                        break;
                    }
                    case "invoice": case "sales invoice": case "sales-invoice": {
                        this.cvApi.providers.GetDiscountsForInvoice(this.salesInvoice.ID).then(r => {
                            if (!r || !r.data) {
                                reject();
                                return;
                            }
                            this.salesInvoice.Discounts = r.data.Discounts;
                            this.salesInvoice.SalesItems.forEach(x => {
                                x.Discounts = _.filter(
                                    r.data.ItemDiscounts,
                                    y => y.MasterID === x.ID);
                                x["DiscountTotal"] = _.sumBy(
                                    x.Discounts,
                                    y => y.DiscountTotal);
                            });
                            resolve();
                        });
                        break;
                    }
                    case "sample": case "sample request": case "sample-request": {
                        resolve();
                        break;
                    }
                    case "return": case "sales return": case "sales-return": {
                        resolve();
                        break;
                    }
                    default: {
                        resolve();
                        return;
                    }
                }
            });
        }
        /* Direct URL for UPS shipment tracking:
         *      http://wwwapps.ups.com/WebTracking/track?track=yes&trackNums=1ZXXXXXXXXXXXXXXXX
         * If any value is provided for the "track" parameter then the confirmation page is skipped
         * and the user goes directly to the tracking page.
         * If you'd rather send the user to the confirmation page then just omit that parameter.
         * Direct URL For UPS Mail Innovations tracking:
         *      https://www.ups-mi.net/packageID/packageid.aspx?pid=XXXXXXXXXXXXXXXXX
         * Direct URL for FedEx shipment tracking:
         *      http://www.fedex.com/Tracking?action=track&tracknumbers=XXXXXXXXXXXXXXXXX
         * Direct URL for USPS shipment tracking
         *      https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1=XXXXXXXXXXXXXXXXX
         * Direct URL for DHL US shipment tracking
         *      http://track.dhl-usa.com/TrackByNbr.asp?ShipmentNumber=XXXXXXXXXXXXXXXXX
         *  Direct URL for DHL Express shipment tracking
         *      http://www.dhl.com/en/express/tracking.html?AWB=XXXXXXXXXXXXXXXXX
         * Direct URL for DHL Global shipment tracking
         *      http://webtrack.dhlglobalmail.com/?mobile=&trackingnumber=XXXXXXXXXXXXXXXXX
         * Direct URL for OnTrac shipment tracking
         *      http://www.ontrac.com/trackingdetail.asp?tracking=XXXXXXXXXXXXXXXXX
         * Direct URL for ICC World shipment tracking
         *      http://iccworld.com/track.asp?txtawbno=XXXXXXXXXXXXXXXXX
         * Direct URL for LaserShip shipment tracking
         *      http://www.lasership.com/track.php?track_number_input=XXXXXXXXXXXXXXXXX
         * Direct URL for Canada Post shipment tracking
         *      http://www.canadapost.ca/cpotools/apps/track/personal/findByTrackNumber?trackingNumber=XXXXXXXXXXXXXXXXX&amp;LOCALE=en
         * (change LOCALE= en to LOCALE= fr for results in French)
         * Direct URL for Averitt Express shipment tracking
         *      https://www.averittexpress.com/trackLTLById.avrt?serviceType=LTL&resultsPageTitle=LTL+Tracking+by+PRO+and+BOL&trackPro=XXXXXXXXXXXXXXXXX
         * Direct URL for Conway Frieght shipment tracking
         *      https://www.con-way.com/webapp/manifestrpts_p_app/shipmentTracking.do?PRO=XXXXXXXXXXXXXXXXX
         * Direct URL for Old Dominion shipment tracking
         *      https://www.odfl.com/Trace/standardResult.faces?pro=XXXXXXXXXXXXXXXXX
         * Direct URL for YRC shipment tracking
         *      http://www.usfc.com/shipmentStatus/track.do?proNumber=XXXXXXXXXXXXXXXXX
         * R + L Carriers
         *      http://www2.rlcarriers.com/freight/shipping/shipment-tracing?pro=XXXXXXXXXXXXXXXXX&docType=PRO */
        matchers = [
            <TrackingMatcher>{
                carrier: "UPS",
                method: "Standard",
                regex: /\b(1Z ?[0-9A-Z]{3} ?[0-9A-Z]{3} ?[0-9A-Z]{2} ?[0-9A-Z]{4} ?[0-9A-Z]{3} ?[0-9A-Z]|[\dT]\d\d\d ?\d\d\d\d ?\d\d\d)\b/,
                link: "http://wwwapps.ups.com/WebTracking/track?track=yes&trackNums="
            },
            <TrackingMatcher>{
                carrier: "UPS",
                method: "Mail Innovations",
                regex: /\b(1Z ?[0-9A-Z]{3} ?[0-9A-Z]{3} ?[0-9A-Z]{2} ?[0-9A-Z]{4} ?[0-9A-Z]{3} ?[0-9A-Z]|[\dT]\d\d\d ?\d\d\d\d ?\d\d\d|\d{22})\b/i,
                link: "https://www.ups-mi.net/packageID/packageid.aspx?pid="
            },
            <TrackingMatcher>{
                carrier: "FedEx",
                method: "Standard 1",
                regex: /(\b96\d{20}\b)|(\b\d{15}\b)|(\b\d{12}\b)/,
                link: "http://www.fedex.com/Tracking?action=track&tracknumbers="
            },
            <TrackingMatcher>{
                carrier: "FedEx",
                method: "Standard 2",
                regex: /\b((98\d\d\d\d\d?\d\d\d\d|98\d\d) ?\d\d\d\d ?\d\d\d\d( ?\d\d\d)?)\b/,
                link: "http://www.fedex.com/Tracking?action=track&tracknumbers="
            },
            <TrackingMatcher>{
                carrier: "FedEx",
                method: "Standard 3",
                regex: /^[0-9]{15}$/,
                link: "http://www.fedex.com/Tracking?action=track&tracknumbers="
            },
            <TrackingMatcher>{
                carrier: "USPS",
                method: "Standard 1",
                regex: /(\b\d{30}\b)|(\b91\d+\b)|(\b\d{20}\b)/,
                link: "https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1="
            },
            <TrackingMatcher>{
                carrier: "USPS",
                method: "Standard 2",
                regex: /^E\D{1}\d{9}\D{2}$|^9\d{15,21}$/,
                link: "https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1="
            },
            <TrackingMatcher>{
                carrier: "USPS",
                method: "Standard 3",
                regex: /^91[0-9]+$/,
                link: "https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1="
            },
            <TrackingMatcher>{
                carrier: "USPS",
                method: "Standard 4",
                regex: /^[A-Za-z]{2}[0-9]+US$/,
                link: "https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1="
            }
        ];
        getPaymentTerms(): string {
            if (this.salesObject.Account.SerializableAttributes
                && this.salesObject.Account.SerializableAttributes["Payment Terms"]) {
                return this.salesObject.Account.SerializableAttributes["Payment Terms"].Value;
            }
            return "DUE UPON RECEIPT";
        }
        getInvoiceDueDate(): string {
            const paymentTerms = this.getPaymentTerms();
            if (paymentTerms[0] != "NET") {
                return paymentTerms;
            }
            const daysToAdd = parseInt(paymentTerms[1]);
            const date = new Date(
                this.salesObject.CreatedDate.getTime() + (1000 * 60 * 60 * 24 * daysToAdd));
            const month = date.toLocaleString("en-us", { month: "short" });
            const day = date.getDate();
            const year = date.getFullYear();
            const newDateString = month + ' ' + day + ', ' + year;
            return newDateString;
        }
        getDiscounts(): number {
            let discounts = 0;
            for (let i = 0; i < this.salesObject.Discounts.length; i++) {
                discounts += this.salesObject.Discounts[i].DiscountValue;
            }
            return discounts;
        }
        // <div ng-bind-html=\"salesOrderHistory.genTrackingLinks(dataItem.TrackingNumber)\"></div>
        genTrackingLinks(trackingNumber: string): string {
            if (trackingNumber && trackingNumber.indexOf("\r\n") > -1) {
                let retVal = "";
                const numbers = trackingNumber.split("\r\n");
                _.each(numbers, (value) => {
                    retVal += this.genSingleTrackingLink(value) + "<br/>\r\n";
                });
                return this.$sce.trustAsHtml(retVal);
            }
            if (!trackingNumber || trackingNumber.length === 0) {
                return "";
            }
            return this.$sce.trustAsHtml(this.genSingleTrackingLink(trackingNumber));
        }
        genSingleTrackingLink(trackingNumber: string): string {
            let returnLink = "";
            this.matchers.forEach(value => {
                if (trackingNumber && trackingNumber.match(value.regex) == null) {
                    return true;
                }
                returnLink = value.link + trackingNumber;
                return false;
            });
            return `<a target="_blank" href="${returnLink}">${trackingNumber}</a>`;
        }
        private setupItemsPaging(): void {
            this.itemsPaging = new core.Paging<
                api.SalesItemBaseModel<api.AppliedSalesOrderItemDiscountModel>>(
                    this.$filter);
            this.itemsPaging.pageSize = 8;
            this.itemsPaging.pageSetSize = 3;
            this.cvProductService.appendToSalesItems(this.salesObject.SalesItems as any)
                .then(salesItems => this.itemsPaging.data = salesItems as any);
        }
        loadSellerUserID(): void {
            if (!this.salesObject.StoreID) {
                if (this.viewState.running) {
                    this.finishRunning();
                }
                return;
            }
            this.setRunning();
            this.cvApi.stores.GetStoreAdministratorUser(this.salesObject.StoreID).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded || !r.data.Result) {
                    console.warn("Failed to get store administrator user to contact");
                    this.finishRunning(true);
                    return;
                }
                this.sellerUserID = r.data.Result.ID;
                this.finishRunning();
            });
        }
        print(): void {
            // TODO: Where is this state?
            ////if (this.salesObjectType === "Sales Return") {
            ////    this.$state.go("salesReturns.print", { ID: this.salesObject.ID });
            ////} else {
                window["print"]();
            ////}
        }
        addCartItem(id: number, quantity: number): void {
            this.cvCartService.addCartItem(id, this.cvServiceStrings.carts.types.cart, quantity);
        }
        requestReturn(): void {
            this.$state.go("userDashboard.salesReturns.new", { ID: this.salesObject.ID });
        }
        cancelOrder(): void {
            console.warn("TODO: implement customer cancel order");
        }
        cancelReturn(): void {
            console.warn("TODO: implement customer cancel return");
        }
        reorderAll(cartType: string = this.cvServiceStrings.carts.types.cart): void {
            const addCartItems = <api.AddCartItemsDto>{
                Items: this.salesObject.SalesItems,
                TypeName: cartType
            };
            this.setRunning();
            this.cvApi.shopping.AddCartItems(addCartItems).then(r => {
                if (!r || !r.data
                    || (!r.data.ActionSucceeded
                        && (!r.data.Result || !_.some(r.data.Result, x => x > 0)))) {
                    this.cvMessageModalFactory(
                        this.$translate("ui.storefront.errors.NoItemsFromThisOrderCouldBeReordered"))
                    .finally(() => this.finishRunning(true, "ERROR! Unable to reorder all items."));
                    return;
                }
                if (!r.data.Result || !r.data.Result.length) {
                    this.cvMessageModalFactory(
                        this.$translate("ui.storefront.errors.NoItemsFromThisOrderCouldBeReordered"))
                    .finally(() => this.finishRunning(true, "ERROR! Items could not be reordered."));
                    return;
                }
                if (!r.data.Messages) {
                    r.data.Messages = [];
                }
                const uniqItemsAdded = _.countBy(r.data.Result, x => x > 0);
                let translated = `${uniqItemsAdded[true as any]} unique items added to the cart.`;
                this.$translate(
                        "ui.storefront.carts.MultipleItemsWereAddedToTheCart.Template",
                        { count: uniqItemsAdded[true as any] })
                    .then(t => { if (t) { translated = t; } })
                    .finally(() => {
                        r.data.Messages.push(translated);
                        this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.itemsAdded,
                            /*type*/ addCartItems.TypeName,
                            /*dto*/ addCartItems,
                            /*forceNoModal*/ false,
                            /*messages*/ r.data.Messages);
                    });
            }).catch(reason => {
                this.cvMessageModalFactory(
                    this.$translate("ui.storefront.errors.NoItemsFromThisOrderCouldBeReordered"))
                .finally(() => this.finishRunning(true, reason));
            });
        }
        doContactSeller(): void {
            if (!this.salesOrder.Store) { return; }
            this.cvApi.stores.GetStoreAdministratorUser(this.salesOrder.StoreID).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded || !r.data.Result) {
                    console.warn("Failed to get store administrator user to contact");
                    return;
                }
                const contacts = [r.data.Result.ID];
                this.$state.go("userDashboard.inbox.modalCompose", { contacts: contacts });
            });
        }
        showPayoneerPaymentInstructions(): void {
            this.cvApi.payments.GetPaymentInstructionsUrlForEscrowOrder(
                this.salesObject.ID,
                -1,
                -1
            ).then(r => {
                if (!r.data.ActionSucceeded) {
                    // TODO: Show an error
                    return;
                }
                // Use the URL returned from the request
                const url = r.data.Result;
                armor.openModal(url, false, null, null);
            });
        }
        showPayoneerReleaseFunds(): void {
            this.cvApi.payments.GetAuthedURLToReleaseFundsForEscrowOrder(
                this.salesObject.ID
            ).then(r => {
                if (!r.data.ActionSucceeded) {
                    // TODO: Show an error
                    return;
                }
                // Use the URL returned from the request
                const url = r.data.Result;
                armor.openModal(url, false, null, null);
            });
        }
        sendNewNote(): void {
            this.sendNewNoteButtonDisabled = true;
            this.sendNewNoteButtonText = "Sending...";
            if (!this.salesQuote.Notes) {
                this.salesQuote.Notes = [];
            }
            this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                const noteToSend = <api.NoteModel>{
                    Active: true,
                    CreatedDate: new Date(),
                    SalesQuoteID: this.salesQuote.ID,
                    CreatedByUserID: user.userID,
                    Note1: this.newNoteText,
                    TypeID: 0,
                    TypeName: "Private"
                };
                this.cvApi.structure.CreateNote(noteToSend).then(r => {
                    this.sendNewNoteButtonDisabled = false;
                    if (!r || !r.data) {
                        this.sendNewNoteButtonText = "Error Sending";
                        return;
                    }
                    this.newNoteText = "";
                    this.sendNewNoteButtonDisabled = false;
                    this.sendNewNoteButtonText = "Add Note";
                    this.cvApi.structure.GetNoteByID(r.data.Result).then(r2 => {
                        this.salesQuote.Notes.push(r2.data);
                    });
                }).catch(reason => {
                    this.sendNewNoteButtonDisabled = false;
                    this.sendNewNoteButtonText = "Error Sending";
                    if (reason.statusText) {
                        this.sendNewNoteButtonText += `: ${reason.statusText}`;
                    }
                });
            });
        }
        // Invoice Actions
        startPayInvoice(overrideID: number, overrideBalance: number, alertOnFinish: boolean): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.$uibModal.open({
                    templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/modals/payInvoiceModal.html", "ui"),
                    size: this.cvServiceStrings.modalSizes.md,
                    controller: userDashboard.modals.PayInvoiceModalController,
                    controllerAs: "udpimcCtrl",
                    resolve: {
                        id: () => overrideID || this.salesObject.ID,
                        balanceDue: () => overrideBalance || this.salesObject.BalanceDue
                    }
                }).result.then(result => {
                    if (overrideID || !alertOnFinish) { return; } // Don't reload here
                    this.setRunning();
                    this.$state.reload(); // Will call finishRunning
                }).finally(() => resolve());
            });
        }
        // Quote Actions
        private startQuoteApproveMaster(masterId: number): ng.IPromise<api.CEFActionResponseT<{ item1: number, item2: number }>> {
            return this.$q((resolve, reject) => {
                this.cvApi.providers.ConvertQuoteToOrderForCurrentUser(masterId).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.finishRunning(true, null, r && r.data && r.data.Messages);
                        reject(r);
                        return;
                    }
                    // Comes over the wire as a string, transform to a "tuple" object
                    const regex = new RegExp("^\\((?<orderId>\\d+),\\s*(?<invoiceId>\\d+)\\)$");
                    const match = regex.exec(r.data.Result as any);
                    r.data.Result = {
                        item1: Number(match["groups"]["orderId"]),
                        item2: Number(match["groups"]["invoiceId"])
                    };
                    this.$state.reload(); // Will call finishRunning
                    this.cvMessageModalFactory(
                        this.$translate(
                            "ui.storefront.SalesQuoteStatuses.Approved.Finished.Template",
                            { orderId: r.data.Result.item1, invoiceId: r.data.Result.item2 })
                    ).finally(() => { this.finishRunning(); resolve(r.data); });
                }).catch(reason => { this.finishRunning(true, reason); reject(reason); });
            });
        }
        startSubQuoteApprove(): ng.IPromise<api.CEFActionResponseT<{ item1: number, item2: number }>> {
            this.setRunning();
            return this.$q((resolve, reject) => {
                this.cvConfirmModalFactory(
                    this.$translate("ui.storefront.SalesQuoteStatuses.Approved.Sub.Confirm.Template")
                ).then(accept => {
                    if (!accept) {
                        reject("User cancelled");
                        return;
                    }
                    // This sub-quote should become marked as approved on this server first (if not already)
                    this.cvApi.providers.SetSalesQuoteAsApproved(this.salesObject.ID).then(r => {
                        if (!r?.data?.ActionSucceeded) {
                            this.finishRunning(true, null, r?.data?.Messages);
                            reject(r?.data?.Messages ?? "Failed to set approved for unknown reason");
                            return;
                        }
                        if (!this.salesGroupRecord) {
                            // There is no sales group, this is just the one quote (no splitting)
                            // NOTE: This workflow is technically deprecated, not going to perform any additional testing
                            resolve(this.startQuoteApproveMaster(this.salesObject.ID));
                            return;
                        }
                        // After that, check to see if all of the existing sub-quotes are approved and if they are,
                        // then go to the approval from the master section
                        let allSubsAccepted = true;
                        for (const sub of this.salesGroupRecord.SalesQuoteRequestSubs) {
                            if (sub.StatusKey !== "Accepted"
                                && sub != this.salesObject
                                && this.salesGroupRecord.SalesQuoteRequestSubs.length > 1) {
                                allSubsAccepted = false;
                            }
                        }
                        if (!allSubsAccepted) {
                            // NOTE: This is not an error state, we just don't move forward
                            ////this.finishRunning(true, "All sub-requests must be accepted.");
                            ////reject("All sub-requests must be accepted.");
                            reject();
                            this.cvMessageModalFactory(
                                this.$translate("ui.storefront.SalesQuoteStatuses.Approved.Sub.ButOthersNotYet.Template")
                            ).finally(() => {
                                // We still may have changed the status of this quote, reload the $state to refresh the data
                                this.$state.reload();
                            });
                            return;
                        }
                        resolve(this.startQuoteApproveMaster(this.salesGroupRecord.SalesQuoteRequestMasters[0].ID));
                    }).catch(reason2 => { this.finishRunning(true, reason2); reject(reason2); });
                }).catch(reason => { this.finishRunning(true, reason); reject(reason); });
            });
        }
        startSubQuoteApproveAndPay(): void {
            this.startSubQuoteApprove().then(cefar => {
                if (!cefar.ActionSucceeded) {
                    // Log to the console for t/s
                    this.consoleLog(cefar);
                    // We still may have changed the status of this quote, reload the $state to refresh the data
                    this.$state.reload();
                    return;
                }
                const orderID = cefar.Result.item1;
                const invoiceID = cefar.Result.item2;
                this.startPayInvoice(invoiceID, this.salesObject.Totals.Total, false)
                    .finally(() => this.$state.reload());
            });
        }
        startQuoteReject(): void {
            this.cvConfirmModalFactory(
                    this.$translate(this.cvServiceStrings.salesQuotes.statuses.rejected.confirmTemplate))
                .then(success => {
                    if (!success) {
                        return;
                    }
                    this.setRunning();
                    this.cvApi.providers.SetSalesQuoteAsRejected(this.salesObject.ID).then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded) {
                            this.finishRunning(true, null, r && r.data && r.data.Messages);
                            return;
                        }
                        this.$state.reload(); // Will eventually call finish running
                    }).catch(reason => this.finishRunning(true, reason));
                });
        }
        startQuoteVoid(): void {
            this.cvConfirmModalFactory(
                this.$translate(this.cvServiceStrings.salesQuotes.statuses.void.confirmTemplate))
            .then(success => {
                if (!success) {
                    return;
                }
                this.setRunning();
                this.cvApi.providers.SetSalesQuoteAsVoided(this.salesObject.ID).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.finishRunning(true, null, r && r.data && r.data.Messages);
                        return;
                    }
                    this.$state.reload(); // Will eventually call finish running
                }).catch(reason => this.finishRunning(true, reason));
            });
        }

        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $window: ng.IWindowService,
                private readonly $q: ng.IQService,
                private readonly $filter: ng.IFilterService,
                private readonly $sce: ng.ISCEService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                private readonly $state: ng.ui.IStateService,
                private readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvCartService: services.ICartService,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvProductService: services.IProductService,
                private readonly cvMessageModalFactory: store.modals.IMessageModalFactory,
                private readonly cvConfirmModalFactory: store.modals.IConfirmModalFactory) {
            super(cefConfig);
            this.id = this.$stateParams.ID;
            if (!this.id) {
                const parts = this.$window.location.pathname.split("/");
                this.id = Number(parts[parts.length - 1]);
            }
            this.load();
        }
    }

    cefApp.directive("cefSalesDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { type: "@", allowPayoneer: "@?", allowContactSeller: "@?" },
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/sales/salesDetail.html", "ui"),
        controller: SalesDetailController,
        controllerAs: "udsdCtrl",
        bindToController: true
    }));

    cefApp.directive("cefShipmentSalesDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { type: "@", allowPayoneer: "@?", allowContactSeller: "@?" },
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/sales/shipmentSalesDetail.html", "ui"),
        controller: SalesDetailController,
        controllerAs: "udsdCtrl",
        bindToController: true
    }));

    cefApp.filter('titleCaseFilter', function () {
        return function (input) {
            if (!input) return '';
            // Split string on dash and capitalize each word
            var out = input.split('-').map(function(word) {
                return word.charAt(0).toUpperCase() + word.slice(1);
            }).join(' ');
            return out;
        };
    });
}
