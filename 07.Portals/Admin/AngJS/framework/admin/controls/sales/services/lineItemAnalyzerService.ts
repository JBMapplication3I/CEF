module cef.admin.controls.sales.services {
    export interface ILineItemAnalyzerService {
        // Properties
        relevantResponses: { [originalID: number]: api.SalesQuoteModel[] };
        relevantResponseItems: { [originalItemID: number]: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>[] };
        salesGroupAsRequestMasters  : { [originalID: number]: api.SalesGroupModel[] };
        salesGroupAsRequestSubs     : { [originalID: number]: api.SalesGroupModel[] };
        salesGroupAsResponseMasters : { [originalID: number]: api.SalesGroupModel[] };
        salesGroupAsResponseSubs    : { [originalID: number]: api.SalesGroupModel[] };
        salesQuotesAsRequestMasters : { [originalID: number]: api.SalesQuoteModel[] };
        salesQuotesAsRequestSubs    : { [originalID: number]: api.SalesQuoteModel[] };
        salesQuotesAsResponseMasters: { [originalID: number]: api.SalesQuoteModel[] };
        salesQuotesAsResponseSubs   : { [originalID: number]: api.SalesQuoteModel[] };
        // Functions
        loadSalesGroupInfo: (record: api.SalesQuoteModel) => ng.IPromise<void>;
        analyzeResponses: (original: api.SalesQuoteModel,
            responses: Array<api.SalesQuoteModel>,
            originalID: number,
            newValue: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>,
            isMaster: boolean) => ng.IPromise<void>;
        awardItem: (originalSalesQuoteItemID: number, responseSalesQuoteItemID: number) => ng.IPromise<void>;
        loadResponse: (originalSalesQuoteID: number, responseSalesQuoteID: number) => ng.IPromise<void>;
    }

    class LineItemAnalyzerService implements ILineItemAnalyzerService {
        // Properties
        records: { [originalID: number]: api.SalesQuoteModel } = {};
        responses: { [originalID: number]: api.SalesQuoteModel[] } = {};
        relevantResponses: { [originalID: number]: api.SalesQuoteModel[] } = {};
        relevantResponseItems: { [originalItemID: number]: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>[] } = {};
        salesGroupAsRequestMasters  : { [originalID: number]: api.SalesGroupModel[] } = {};
        salesGroupAsRequestSubs     : { [originalID: number]: api.SalesGroupModel[] } = {};
        salesGroupAsResponseMasters : { [originalID: number]: api.SalesGroupModel[] } = {};
        salesGroupAsResponseSubs    : { [originalID: number]: api.SalesGroupModel[] } = {};
        salesQuotesAsResponseMasters: { [originalID: number]: api.SalesQuoteModel[] } = {};
        salesQuotesAsResponseSubs   : { [originalID: number]: api.SalesQuoteModel[] } = {};
        salesQuotesAsRequestMasters : { [originalID: number]: api.SalesQuoteModel[] } = {};
        salesQuotesAsRequestSubs    : { [originalID: number]: api.SalesQuoteModel[] } = {};
        // Functions
        loadSalesGroupInfo(record: api.SalesQuoteModel): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                if (!this.salesGroupAsRequestMasters[record.ID]  ) { this.salesGroupAsRequestMasters[record.ID]   = []; }
                if (!this.salesGroupAsRequestSubs[record.ID]     ) { this.salesGroupAsRequestSubs[record.ID]      = []; }
                if (!this.salesGroupAsResponseMasters[record.ID] ) { this.salesGroupAsResponseMasters[record.ID]  = []; }
                if (!this.salesGroupAsResponseSubs[record.ID]    ) { this.salesGroupAsResponseSubs[record.ID]     = []; }
                if (!this.salesQuotesAsRequestMasters[record.ID] ) { this.salesQuotesAsRequestMasters[record.ID]  = []; }
                if (!this.salesQuotesAsRequestSubs[record.ID]    ) { this.salesQuotesAsRequestSubs[record.ID]     = []; }
                if (!this.salesQuotesAsResponseMasters[record.ID]) { this.salesQuotesAsResponseMasters[record.ID] = []; }
                if (!this.salesQuotesAsResponseSubs[record.ID]   ) { this.salesQuotesAsResponseSubs[record.ID]    = []; }
                // Look for SalesGroup data that might have responses, or is this a response?
                if (record.SalesGroupAsRequestMasterID) {
                    this.cvApi.sales.GetSalesGroupByID(record.SalesGroupAsRequestMasterID).then(r => {
                        this.salesGroupAsRequestMasters[record.ID].push(r.data);
                        this.salesQuotesAsRequestMasters[record.ID].push(...r.data.SalesQuoteRequestMasters);
                        this.salesQuotesAsRequestSubs[record.ID].push(...r.data.SalesQuoteRequestSubs);
                        this.salesQuotesAsResponseMasters[record.ID].push(...r.data.SalesQuoteResponseMasters);
                        this.salesQuotesAsResponseSubs[record.ID].push(...r.data.SalesQuoteResponseSubs);
                        resolve(this.$q.all(record.SalesItems
                            .map(x => this.analyzeResponses(record, this.salesQuotesAsRequestMasters[record.ID], record.ID, x, true))));
                    });
                    return;
                }
                if (record.SalesGroupAsRequestSubID) {
                    this.cvApi.sales.GetSalesGroupByID(record.SalesGroupAsRequestSubID).then(r => {
                        this.salesGroupAsRequestSubs[record.ID].push(r.data);
                        this.salesQuotesAsRequestMasters[record.ID].push(...r.data.SalesQuoteRequestMasters);
                        this.salesQuotesAsRequestSubs[record.ID].push(...r.data.SalesQuoteRequestSubs);
                        this.salesQuotesAsResponseMasters[record.ID].push(...r.data.SalesQuoteResponseMasters);
                        this.salesQuotesAsResponseSubs[record.ID].push(...r.data.SalesQuoteResponseSubs);
                        resolve(this.$q.all(record.SalesItems
                            .map(x => this.analyzeResponses(record, this.salesQuotesAsRequestSubs[record.ID], record.ID, x, true))));
                    });
                    return;
                }
                if (record.SalesGroupAsResponseMasterID) {
                    this.cvApi.sales.GetSalesGroupByID(record.SalesGroupAsResponseMasterID).then(r => {
                        this.salesGroupAsResponseMasters[record.ID].push(r.data);
                        this.salesQuotesAsRequestMasters[record.ID].push(...r.data.SalesQuoteRequestMasters);
                        this.salesQuotesAsRequestSubs[record.ID].push(...r.data.SalesQuoteRequestSubs);
                        this.salesQuotesAsResponseMasters[record.ID].push(...r.data.SalesQuoteResponseMasters);
                        this.salesQuotesAsResponseSubs[record.ID].push(...r.data.SalesQuoteResponseSubs);
                        resolve(this.$q.all(record.SalesItems
                            .map(x => this.analyzeResponses(record, this.salesQuotesAsResponseMasters[record.ID], record.ID, x, false))));
                    });
                    return;
                }
                if (record.SalesGroupAsResponseSubID) {
                    this.cvApi.sales.GetSalesGroupByID(record.SalesGroupAsResponseSubID).then(r => {
                        this.salesGroupAsResponseSubs[record.ID].push(r.data);
                        this.salesQuotesAsRequestMasters[record.ID].push(...r.data.SalesQuoteRequestMasters);
                        this.salesQuotesAsRequestSubs[record.ID].push(...r.data.SalesQuoteRequestSubs);
                        this.salesQuotesAsResponseMasters[record.ID].push(...r.data.SalesQuoteResponseMasters);
                        this.salesQuotesAsResponseSubs[record.ID].push(...r.data.SalesQuoteResponseSubs);
                        resolve(this.$q.all(record.SalesItems
                            .map(x => this.analyzeResponses(record, this.salesQuotesAsResponseSubs[record.ID], record.ID, x, false))));
                    });
                    return;
                }
                resolve();
            });
        }
        analyzeResponses(
            original: api.SalesQuoteModel,
            responses: Array<api.SalesQuoteModel>,
            originalID: number,
            item: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>,
            isMaster: boolean
        ): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                if (!this.responses) {
                    this.responses = {};
                }
                if (!this.responses[originalID] && responses && responses.length) {
                    this.responses[originalID] = responses;
                }
                if (!this.responses[originalID] || !this.responses[originalID].length) {
                    resolve();
                    return;
                }
                const idsToLoad = [];
                const idsAlreadyLoaded = [];
                this.responses[originalID].forEach(x => {
                    if (!x) {
                        // Bad item in array
                        return;
                    }
                    if (x.ID && (!x.SalesItems || !x.SalesItems.length)) {
                        // We have an ID but haven't loaded it's data yet
                        idsToLoad.push(x.ID);
                        return;
                    }
                    // We have an ID and already have it's line items
                    idsAlreadyLoaded.push(x.ID);
                });
                if (idsToLoad.length > 0) {
                    this.$q.all(idsToLoad.map(x => this.cvApi.quoting.GetSalesQuoteByID(x)))
                        .then(responseArr => this.idsLoaded(originalID, original, item, responseArr, isMaster, resolve));
                    return;
                }
                this.idsLoaded(originalID, original, item, [], isMaster, resolve);
            });
        }
        idsLoaded(
            originalID: number,
            original: api.SalesQuoteModel,
            item: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>,
            responseArr: Array<ng.IHttpPromiseCallbackArg<api.SalesQuoteModel>>,
            isMaster: boolean,
            resolve: Function)
            : void {
            responseArr.forEach(r => {
                const index = _.findIndex(this.responses[originalID], x => x.ID === r.data.ID);
                this.responses[originalID][index] = r.data;
            });
            // Now that we have all the data, look for quotes that have responses to this line item
            this.relevantResponses = [];
            this.responses[originalID].forEach(x =>  {
                const itemID = _.findIndex(x.SalesItems, y => {
                    const key = y.CustomKey && y.CustomKey.startsWith("Supplier-vendor")
                        ? "Supplier-vendor-Response-Original-Item"
                        : y.CustomKey && y.CustomKey.startsWith("Supplier-store")
                            ? "Supplier-store-Response-Original-Item"
                            : null;
                    if (key && item.ID === Number(y.SerializableAttributes[key].Value)) {
                        y["releventItemID"] = item.ID;
                        return true;
                    }
                    return false;
                });
                if (itemID < 0) {
                    return;
                }
                if (!this.relevantResponses[originalID]) {
                    this.relevantResponses[originalID] = [];
                }
                this.relevantResponses[originalID].push(x);
            });
            if (isMaster) {
                this.extendResponseItemWithOriginalItemAccessor(original, item).then(() => resolve());
                return;
            }
            this.extendOriginalItemWithResponseItemsAccessor(originalID, item).then(() => resolve());
        }
        extendResponseItemWithOriginalItemAccessor(
            record: api.SalesQuoteModel,
            responseItem: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>)
            : ng.IPromise<void> {
            this.records[record.ID] = record;
            return this.$q((resolve, reject) => {
                const id = record.ID;
                const key = responseItem.CustomKey && responseItem.CustomKey.startsWith("Supplier-vendor")
                    ? "Supplier-vendor-Response-Original-Item"
                    : "Supplier-store-Response-Original-Item";
                responseItem["originalItem"] = () => _.find(
                    this.records[id].SalesItems,
                    x => x.ID === Number(responseItem.SerializableAttributes[key].Value));
                resolve();
            });
        }
        extendOriginalItemWithResponseItemsAccessor(
            originalSalesQuoteID: number,
            originalItem: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>)
            : ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                if (!this.relevantResponseItems) {
                    this.relevantResponseItems = {};
                }
                if (!this.relevantResponseItems[originalSalesQuoteID]) {
                    if (!this.salesQuotesAsResponseMasters) {
                        this.salesQuotesAsResponseMasters = {};
                    }
                    if (!this.salesQuotesAsResponseMasters[originalSalesQuoteID]) {
                        reject();
                        return;
                    }
                }
                resolve();
            }).then(() => {
                if (!this.relevantResponseItems[originalSalesQuoteID]) {
                    this.relevantResponseItems[originalSalesQuoteID] = this.salesQuotesAsResponseMasters[originalSalesQuoteID]
                        .map(x => x.SalesItems.filter(y => {
                            const key = y.CustomKey && y.CustomKey.startsWith("Supplier-vendor")
                                ? "Supplier-vendor-Response-Original-Item"
                                : "Supplier-store-Response-Original-Item";
                            return originalSalesQuoteID === Number(y.SerializableAttributes[key].Value);
                        })).reduce(x => x);
                }
                originalItem["responseItems"] = () => this.relevantResponseItems[originalSalesQuoteID];
            });
        }
        loadResponse(originalSalesQuoteID: number, responseSalesQuoteID: number): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                if (!responseSalesQuoteID) {
                    reject();
                    return;
                }
                this.cvApi.quoting.GetSalesQuoteByID(responseSalesQuoteID).then(r => {
                    const index = _.findIndex(
                        this.salesQuotesAsResponseMasters[originalSalesQuoteID],
                        x => x.ID === responseSalesQuoteID);
                    this.salesQuotesAsResponseMasters[originalSalesQuoteID][index] = r.data;
                    resolve();
                });
            });
        }
        awardItem(originalSalesQuoteItemID: number, responseSalesQuoteItemID: number): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                if (!originalSalesQuoteItemID || !responseSalesQuoteItemID) {
                    return;
                }
                this.cvConfirmModalFactory(
                    "Are you sure you want to mark this item as Awarded?",
                    () => this.cvApi.providers.AwardSalesQuoteLineItem(
                            originalSalesQuoteItemID,
                            responseSalesQuoteItemID)
                        .then(r => resolve()));
            });
        }
        // Constructors
        constructor(
            private readonly $q: ng.IQService,
            private readonly cvApi: api.ICEFAPI,
            private readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            // Do Nothing
        }
    }

    adminApp.service("cvLineItemAnalyzerService", LineItemAnalyzerService);
}
