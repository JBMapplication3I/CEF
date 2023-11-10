/**
 * @file framework/store/userDashboard/controls/sales/salesGroupDetail.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Sales group detail class, shows the entire sales proces for end users
 */
module cef.store.userDashboard.controls.sales {
    interface ISalesGroupUIContent {
        type: string;
        subType: string;
        title?: string;
        loading: boolean;
    }

    class SalesGroupDetailController extends core.TemplatedControllerBase {
        // Bound by Scope Properties
        allowPayoneer: boolean;
        allowContactSeller: boolean;
        // Properties
        record: api.SalesGroupModel = null;
        content: ISalesGroupUIContent = { type: null, subType: null, loading: false };
        events = {
            aggregate: [],
            loading: true
        };
        currentQuoteOrderID: number = null;
        shipments: api.ShipmentModel[];
        // Functions
        private load(): void {
            this.setRunning();
            if (!this.$stateParams["GroupID"]) {
                const msg = this.$translate.instant(
                    "ui.storefront.userDashboard2.controls.salesGroupDetail.NoIDProvidedCannotLoadPage");
                this.finishRunning(true, msg);
                throw Error(msg);
            }
            this.cvApi.sales.GetSecureSalesGroup(Number(this.$stateParams["GroupID"])).then(r => {
                if (!r || !r.data) {
                    this.finishRunning(true, this.$translate.instant(
                        "ui.storefront.userDashboard2.controls.salesGroupDetail.UnableToFindRecordWithID")
                        + this.$stateParams["GroupID"]);
                    return;
                }
                this.record = r.data;
                this.getShipments(r.data.ID).then(shipments => {
                    if (_.isArray(shipments)) {
                        this.shipments = shipments;
                    }
                    this.loadAggregateEvents();
                    if (!this.$stateParams["ID"]) {
                       this.setContent(null, null);
                       return;
                    }
                    if (this.$state.includes("userDashboard.salesGroup.detail.quote")) {
                        this.setContent(
                            "SalesQuote",
                            _.some(
                                this.record.SalesQuoteRequestMasters,
                                x => x.ID === Number(this.$stateParams["ID"]))
                                    ? "request-master"
                                    : _.some(
                                        this.record.SalesQuoteRequestSubs,
                                        x => x.ID === Number(this.$stateParams["ID"]))
                                            ? "request-sub"
                                            : _.some(
                                                this.record.SalesQuoteResponseMasters,
                                                x => x.ID === Number(this.$stateParams["ID"]))
                                                    ? "response-master"
                                                    : _.some(
                                                        this.record.SalesQuoteResponseSubs,
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
                                this.record.SalesOrderMasters[0],
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
                    if (this.$state.includes("userDashboard.salesGroup.detail.shipping")) {
                        this.setContent(
                            "Shipments",
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
                })
            }).catch(reason => this.finishRunning(true, reason));
        }
        private getShipments(salesGroupID: number): ng.IPromise<api.ShipmentModel[]> {
            return this.$q((resolve, reject) => {
                this.cvApi.shipping.GetShipmentsBySalesGroupID({SalesGroupID: salesGroupID}).then(r2 => {
                    resolve(r2.data);
                }).catch(error => {
                    resolve();
                })
            });
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
                            this.setContent(type, subType, this.record.SalesOrderMasters[0].ID);
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
            if (this.record.SalesOrderMasters) {
                this.record.SalesOrderMasters.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.ordering.GetSalesOrderEvents(dto));
                });
            }
            if (this.record.SubSalesOrders) {
                this.record.SubSalesOrders.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.ordering.GetSalesOrderEvents(dto));
                });
            }
            if (this.record.SalesQuoteRequestMasters) {
                this.record.SalesQuoteRequestMasters.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.quoting.GetSalesQuoteEvents(dto));
                });
            }
            if (this.record.SalesQuoteRequestSubs) {
                this.record.SalesQuoteRequestSubs.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.quoting.GetSalesQuoteEvents(dto));
                });
            }
            if (this.record.SalesQuoteResponseMasters) {
                this.record.SalesQuoteResponseMasters.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.quoting.GetSalesQuoteEvents(dto));
                });
            }
            if (this.record.SalesQuoteResponseSubs) {
                this.record.SalesQuoteResponseSubs.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.quoting.GetSalesQuoteEvents(dto));
                });
            }
            if (this.record.SalesInvoices) {
                this.record.SalesInvoices.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.invoicing.GetSalesInvoiceEvents(dto));
                });
            }
            if (this.record.SalesReturns) {
                this.record.SalesReturns.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.returning.GetSalesReturnEvents(dto));
                });
            }
            if (this.shipments) {
                this.shipments.forEach(x => {
                    const dto = dtoFactory();
                    dto.MasterID = x.ID;
                    promises.push(this.cvApi.shipping.GetShipmentsBySalesGroupID({ SalesGroupID: x.ID }));
                });
            }
            /* TODO
            if (this.record.SampleRequests) {
                this.record.SampleRequests.forEach(x => {
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
        setCurrentQuoteOrderID(index: number): void {
            if (index) {
                this.currentQuoteOrderID = this.record.SubSalesOrders[index].ID;
            }
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                private readonly $state: ng.ui.IStateService,
                private readonly $stateParams: ng.ui.IStateParamsService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefSalesGroupDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            /**
             * Allows the Payoneer process to display for orders
             * @type {boolean}
             * @default false
             */
            allowPayoneer: "=",
            /**
             * Allows the contact seller process to display (chat window functionality)
             * @type {boolean}
             * @default false
             */
            allowContactSeller: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/sales/salesGroupDetail.html", "ui"),
        controller: SalesGroupDetailController,
        controllerAs: "sgdCtrl",
        bindToController: true
    }));
}
