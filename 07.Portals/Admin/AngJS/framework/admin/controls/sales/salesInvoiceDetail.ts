module cef.admin.controls.sales {
    class SalesInvoiceDetailController
        extends SalesDetailControllerBase<
            api.SalesInvoiceModel,
            api.TypeModel,
            api.AppliedSalesInvoiceDiscountModel,
            api.AppliedSalesInvoiceItemDiscountModel> {
        // Convenience Redirects
        get hasSalesGroup(): boolean {
            return this.cefConfig.featureSet.shipping.splitShipping.enabled
                && this.cefConfig.featureSet.salesGroups.enabled
                && angular.isDefined(this.record.SalesGroupID);
        }
        salesGroupRecord: api.SalesGroupModel;
        // Properties
        itemType = "Invoice";
        createRecordCall = this.cvApi.invoicing.CreateSalesInvoice;
        updateRecordCall = (inv: api.UpdateSalesInvoiceDto): angular.IHttpPromise<api.CEFActionResponseT<number>> => {
            return this.cvApi.invoicing.UpdateSalesInvoice(this.record);
        }
        payments: api.SalesInvoicePaymentModel[];
        // Functions
        loadRecord(id: number): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.cvApi.invoicing.GetSalesInvoiceByID(id).then(r => {
                    if (!r || !r.data) {
                        reject(`There was an error loading the ${this.itemType}`);
                        return;
                    }
                    this.record = r.data;
                    this.$q.all([
                        this.$q(resolve2 => {
                            if (!this.hasSalesGroup) {
                                resolve2(true);
                                return;
                            }
                            this.cvApi.sales.GetSalesGroupByID(this.record.SalesGroupID)
                                .then(r2 => resolve2(this.salesGroupRecord = r2.data))
                                .catch(reason2 => {
                                    this.consoleDebug(reason2);
                                    resolve2();
                                });
                        }),
                        this.doSameAsBillingCheckAndAssign(),
                        this.readOutDiscounts()
                    ]).then(() => {
                        this.finishRunning();
                        resolve(true); 
                    });
                }).catch(reject);
            });
        }
        newRecord(): api.SalesInvoiceModel {
            return <api.SalesInvoiceModel>{
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
            this.loadPayments();
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
            let oldTotal = this.record.Totals.Total;
            this.record.SalesItems.filter(x => x.Active).forEach(x => {
                x.ExtendedPrice = this.$filter("modifiedValue")(
                    x.UnitSoldPrice ? x.UnitSoldPrice : x.UnitCorePrice,
                    x.UnitSoldPriceModifier,
                    x.UnitSoldPriceModifierMode);
                x.ExtendedPrice *= x.Quantity + (x.QuantityBackOrdered || 0) + (x.QuantityPreSold || 0);
                subTotal += x.ExtendedPrice;
            });
            this.record.Totals.SubTotal = subTotal;
            let newTotal = this.record.Totals.Total
                = (this.record.Totals.SubTotal || 0)
                + (this.record.Totals.Shipping || 0)
                + (this.record.Totals.Handling || 0)
                + (this.record.Totals.Fees || 0)
                + (this.record.Totals.Tax || 0)
                - Math.abs(this.record.Totals.Discounts || 0);
            if (!this.record.BalanceDue){
                this.record.BalanceDue = 0;
            }
            this.record.BalanceDue += (newTotal - oldTotal);
        }
        // Sales Invoice Actions (state changes, etc)
        payInvoice(): void {
            this.$uibModal.open({
                size: this.cvServiceStrings.modalSizes.md,
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/sales/modals/payInvoiceModal.html", "ui"),
                controller: modals.PayInvoiceModalController,
                controllerAs: "udpimcCtrl",
                resolve: {
                    id: () => this.record.ID,
                    balanceDue: () => this.record.BalanceDue,
                    accountID: () => this.record.AccountID,
                    userID: () => this.record.UserID,
                }
            }).result.then(result => {
                this.setRunning();
                this.loadRecord(this.record.ID); // Will call finishRunning
            });
        }
        voidInvoice(): void {
            this.$translate("ui.admin.controls.sales.salesInvoiceDetail.MarkInvoiceVoided.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.providers.SetSalesInvoiceAsVoided));
        }
        loadPayments(): void {
            if (!this.record || !(this.record.ID > 0)) {
                return;
            }
            // Shouldn't need to worry about paging for a single invoice's payments
            this.cvApi.payments.GetSalesInvoicePayments({
                Active: true,
                MasterID: this.record.ID,
            }).then(r => {
                if (!r || !r.data) {
                    console.warn("No payments data could be loaded");
                    return;
                }
                this.payments = r.data.Results;
            });
        }
        protected editValue(property: string): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/sales/modals/editATotalModal.html", "ui"),
                size: this.cvServiceStrings.modalSizes.sm,
                controller: modals.EditATotalModalController,
                controllerAs: "editATotalModalCtrl",
                resolve: {
                    originalValue: () => this.record.BalanceDue,
                    property: () => property
                }
            }).result.then((result: number) => this.record.BalanceDue = result);
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $stateParams: ng.ui.IStateParamsService,
                protected readonly $q: ng.IQService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
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

    adminApp.directive("salesInvoiceDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/salesInvoiceDetail.html", "ui"),
        controller: SalesInvoiceDetailController,
        controllerAs: "ssieCtrl"
    }));
}
