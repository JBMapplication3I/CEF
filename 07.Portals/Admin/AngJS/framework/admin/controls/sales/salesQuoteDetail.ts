module cef.admin.controls.sales {
    class SalesQuoteDetailController
        extends SalesDetailControllerBase<
            api.SalesQuoteModel,
            api.TypeModel,
            api.AppliedSalesQuoteDiscountModel,
            api.AppliedSalesQuoteItemDiscountModel> {
        // Properties
        itemType = "Quote";
        createRecordCall = this.cvApi.quoting.CreateSalesQuote;
        updateRecordCall = this.cvApi.quoting.UpdateSalesQuote;
        holdLineItemEditor = true;
        get salesGroupAsRequestMasters(): api.SalesGroupModel[] {
            if (!this.record) { return null; }
            return this.cvLineItemAnalyzerService.salesGroupAsRequestMasters[this.record.ID];
        }
        set salesGroupAsRequestMasters(newValue: api.SalesGroupModel[]) {
            this.cvLineItemAnalyzerService.salesGroupAsRequestMasters[this.record.ID] = newValue;
        }
        get salesGroupAsRequestSubs(): api.SalesGroupModel[] {
            if (!this.record) { return null; }
            return this.cvLineItemAnalyzerService.salesGroupAsRequestSubs[this.record.ID];
        }
        set salesGroupAsRequestSubs(newValue: api.SalesGroupModel[]) {
            this.cvLineItemAnalyzerService.salesGroupAsRequestSubs[this.record.ID] = newValue;
        }
        get salesGroupAsResponseMasters(): api.SalesGroupModel[] {
            if (!this.record) { return null; }
            return this.cvLineItemAnalyzerService.salesGroupAsResponseMasters[this.record.ID];
        }
        set salesGroupAsResponseMasters(newValue: api.SalesGroupModel[]) {
            this.cvLineItemAnalyzerService.salesGroupAsResponseMasters[this.record.ID] = newValue;
        }
        get salesGroupAsResponseSubs(): api.SalesGroupModel[] {
            if (!this.record) { return null; }
            return this.cvLineItemAnalyzerService.salesGroupAsResponseSubs[this.record.ID];
        }
        set salesGroupAsResponseSubs(newValue: api.SalesGroupModel[]) {
            this.cvLineItemAnalyzerService.salesGroupAsResponseSubs[this.record.ID] = newValue;
        }
        get salesQuoteAsRequestMasters(): api.SalesQuoteModel[] {
            if (!this.record) { return null; }
            return this.cvLineItemAnalyzerService.salesQuotesAsRequestMasters[this.record.ID];
        }
        set salesQuoteAsRequestMasters(newValue: api.SalesQuoteModel[]) {
            this.cvLineItemAnalyzerService.salesQuotesAsRequestMasters[this.record.ID] = newValue;
        }
        get salesQuoteAsRequestSubs(): api.SalesQuoteModel[] {
            if (!this.record) { return null; }
            return this.cvLineItemAnalyzerService.salesQuotesAsRequestSubs[this.record.ID];
        }
        set salesQuoteAsRequestSubs(newValue: api.SalesQuoteModel[]) {
            this.cvLineItemAnalyzerService.salesQuotesAsRequestSubs[this.record.ID] = newValue;
        }
        get salesQuoteAsResponseMasters(): api.SalesQuoteModel[] {
            if (!this.record) { return null; }
            return this.cvLineItemAnalyzerService.salesQuotesAsResponseMasters[this.record.ID];
        }
        set salesQuoteAsResponseMasters(newValue: api.SalesQuoteModel[]) {
            this.cvLineItemAnalyzerService.salesQuotesAsResponseMasters[this.record.ID] = newValue;
        }
        get salesQuoteAsResponseSubs(): api.SalesQuoteModel[] {
            if (!this.record) { return null; }
            return this.cvLineItemAnalyzerService.salesQuotesAsResponseSubs[this.record.ID];
        }
        set salesQuoteAsResponseSubs(newValue: api.SalesQuoteModel[]) {
            this.cvLineItemAnalyzerService.salesQuotesAsResponseSubs[this.record.ID] = newValue;
        }
        // Functions
        loadRecord(id: number): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.cvApi.quoting.GetSalesQuoteByID(id).then(r => {
                    if (!r || !r.data) {
                        reject(`There was an error loading the ${this.itemType}`);
                        return;
                    }
                    this.record = r.data;
                    this.$q.all([
                        this.doSameAsBillingCheckAndAssign(),
                        this.readOutDiscounts()/*,
                        this.loadLineItemProducts()*/
                    ]).then(() => resolve(true));
                }).catch(reject);
            });
        }
        newRecord(): api.SalesQuoteModel {
            return <api.SalesQuoteModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                // SalesItemCollection Properties
                ItemQuantity: 0,
                BillingContact: null,
                ShippingContact: null,
                SubtotalItems: 0,
                Totals: this.generateNewTotals(),
                SalesItems: [],
                Discounts: [],
                ShippingSameAsBilling: false,
                TypeID: 1,
                StatusID: 1,
                StateID: 1,
                // SalesQuote Properties
                Balance: 0,
                HasChildren: false
            };
        }
        protected loadRecordAfterAction(result: boolean): void {
            this.cvLineItemAnalyzerService.loadSalesGroupInfo(this.record)
                .then(() => this.holdLineItemEditor = false);
        }
        /*
        protected loadLineItemProducts(): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                const promises = _.uniq(_.filter(this.record.SalesItems, x => x.ProductID > 0).map(x => x.ProductID))
                    .map(x => this.cvApi.products.GetProductFull(x));
                this.$q.all(promises).then((rarr: ng.IHttpPromiseCallbackArg<api.ProductModel>[]) => {
                    for (let i = 0; i < this.record.SalesItems.length; i++) {
                        if (this.record.SalesItems[i]["Product"]) {
                            continue;
                        }
                        const found = _.find(rarr, r => r.data.ID == this.record.SalesItems[i].ProductID);
                        if (!found) {
                            continue;
                        }
                        const p = found.data;
                        this.record.SalesItems[i]["Product"] = p;
                        this.record.SalesItems[i].ProductKey = p.CustomKey;
                        this.record.SalesItems[i].ProductName = p.Name;
                        this.record.SalesItems[i].ProductDescription = p.ShortDescription || p.Description;
                        this.record.SalesItems[i].ProductSeoUrl = p.SeoUrl;
                        this.record.SalesItems[i].ProductIsUnlimitedStock = p.IsUnlimitedStock;
                        this.record.SalesItems[i].ProductAllowBackOrder = p.AllowBackOrder;
                        this.record.SalesItems[i].ProductIsEligibleForReturn = p.IsEligibleForReturn;
                        this.record.SalesItems[i].ProductRestockingFeePercent = p.RestockingFeePercent;
                        this.record.SalesItems[i].ProductRestockingFeeAmount = p.RestockingFeeAmount;
                        this.record.SalesItems[i].ProductNothingToShip = p.NothingToShip;
                        this.record.SalesItems[i].ProductIsTaxable = p.IsTaxable;
                        this.record.SalesItems[i].ProductTaxCode = p.TaxCode;
                        this.record.SalesItems[i].ProductShortDescription = p.ShortDescription;
                        this.record.SalesItems[i].ProductUnitOfMeasure = p.UnitOfMeasure;
                        this.record.SalesItems[i].ProductSerializableAttributes = p.SerializableAttributes;
                        this.record.SalesItems[i].ProductTypeID = p.TypeID;
                        this.record.SalesItems[i].ProductTypeKey = p.TypeKey;
                        this.record.SalesItems[i].Sku = this.record.SalesItems[i].Sku || p.CustomKey;
                    }
                    resolve();
                });
            });
        }
        */
        // Forwards into the line item analyzer service
        awardItem(originalSalesQuoteItemID: number, responseSalesQuoteItemID: number): void {
            this.cvLineItemAnalyzerService.awardItem(originalSalesQuoteItemID, responseSalesQuoteItemID)
                .then(() => this.loadRecord(this.record.ID)
                    .then(result => this.loadRecordAfterAction(result)));
        }
        // NOTE: This must remain an arrow function to resolve 'this' properly
        updateTotals = (): void => {
            if (!this.record.Totals) {
                this.record.Totals = this.generateNewTotals();
            }
            if (!this.record.SalesItems) {
                this.record.SalesItems = [];
            }
            let subTotal = 0;
            this.record.SalesItems.forEach(x => {
                x.ExtendedPrice = this.$filter("modifiedValue")(
                    x.UnitSoldPrice ? x.UnitSoldPrice : x.UnitCorePrice,
                    x.UnitSoldPriceModifier,
                    x.UnitSoldPriceModifierMode);
                x.ExtendedPrice *= x.Quantity + (x.QuantityBackOrdered || 0) + (x.QuantityPreSold || 0);
                subTotal += x.ExtendedPrice;
            });
            this.record.Totals.SubTotal = subTotal;
            this.record.Totals.Total =
                  (this.record.Totals.SubTotal || 0)
                + (this.record.Totals.Shipping || 0)
                + (this.record.Totals.Handling || 0)
                + (this.record.Totals.Fees || 0)
                + (this.record.Totals.Tax || 0)
                - Math.abs(this.record.Totals.Discounts || 0);
        }
        // Sales Quote Actions (state changes, etc)
        inProcessTheSalesQuote(): void {
            this.$translate("ui.admin.controls.sales.salesQuoteDetail.MarkQuoteInProcess.Message")
                .then(translated => this.doStateChangeActionWithConfirm(
                    translated,
                    this.cvApi.providers.SetSalesQuoteAsInProcess));
        }
        processedTheSalesQuote(): void {
            this.$translate("ui.admin.controls.sales.salesQuoteDetail.MarkQuoteProcessed.Message")
                .then(translated => this.doStateChangeActionWithConfirm(
                    translated,
                    this.cvApi.providers.SetSalesQuoteAsProcessed));
        }
        approveTheSalesQuote(): void {
            this.$translate("ui.admin.controls.sales.salesQuoteDetail.MarkQuoteApproved.Message")
                .then(translated => this.doStateChangeActionWithConfirm(
                    translated,
                    this.cvApi.providers.SetSalesQuoteAsApproved));
        }
        rejectTheSalesQuote(): void {
            this.$translate("ui.admin.controls.sales.salesQuoteDetail.MarkQuoteRejected.Message")
                .then(translated => this.doStateChangeActionWithConfirm(
                    translated,
                    this.cvApi.providers.SetSalesQuoteAsRejected));
        }
        voidTheSalesQuote(): void {
            this.$translate("ui.admin.controls.sales.salesQuoteDetail.MarkQuoteVoided.Message")
                .then(translated => this.doStateChangeActionWithConfirm(
                    translated,
                    this.cvApi.providers.SetSalesQuoteAsVoided));
        }
        exportTheSalesQuote(): void {
            this.setRunning();
            this.$translate("ui.admin.controls.sales.salesQuoteDetail.ExportQuote.Message")
                .then(translated => this.cvConfirmModalFactory(translated as string).then(() => {
                // TODO: Select a mapping config to export with
                /* Download a file stream and initiate a file save dialog per:
                 * http://jaliyaudagedara.blogspot.com/2016/05/angularjs-download-files-by-sending.html
                 */
                this.$http({
                    method: "POST",
                    url: this.$filter("corsLink")(["Providers", "SalesQuoteImporters", "Excel", "ExportSalesQuoteAsFile", this.record.ID].join("/"), "api"),
                    responseType: "arraybuffer"
                }).then(response/*(data, status, headers)*/ => {
                        if (response.status === 204) {
                            this.$translate("ui.admin.errors.ThereWasNoContentReturnedForTheFile")
                                .then(translated2 =>this.finishRunning(true, translated2 as string));
                            return;
                        }
                        var responseHeaders = response.headers();
                        var filename = responseHeaders["x-filename"];
                        var contentType = responseHeaders["content-type"];
                        var linkElement = document.createElement("a");
                        try {
                            const blob = new Blob([response.data as any], { type: contentType });
                            const url = window.URL.createObjectURL(blob);
                            linkElement.setAttribute("href", url);
                            linkElement.setAttribute("download", filename);
                            const clickEvent = new MouseEvent("click", {
                                "view": window,
                                "bubbles": true,
                                "cancelable": false
                            });
                            linkElement.dispatchEvent(clickEvent);
                            this.finishRunning();
                        } catch (ex) {
                            this.finishRunning(true, ex);
                        }
                    },
                    result => this.finishRunning(true, `ERROR: ${result.status} - ${result.statusText}`)
                ).catch(reason => this.finishRunning(true, reason));
            }));
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $http: ng.IHttpService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $q: ng.IQService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                private readonly cvLineItemAnalyzerService: services.ILineItemAnalyzerService,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super($scope, $stateParams, $uibModal, $q, cefConfig, cvServiceStrings, cvApi, cvConfirmModalFactory);
            const unbind1 = this.$scope.$on(this.cvServiceStrings.events.lineItems.updated, this.updateTotals);
            const unbind2 = this.$scope.$on(this.cvServiceStrings.events.sales.totalsUpdated, this.updateTotals);
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
    }

    adminApp.directive("salesQuoteDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/salesQuoteDetail.html", "ui"),
        controller: SalesQuoteDetailController,
        controllerAs: "ssqeCtrl"
    }));
}
