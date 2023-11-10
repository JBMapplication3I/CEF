module cef.admin.controls.sales {
    class SalesOrderDetailController
        extends SalesDetailControllerBase<
            api.SalesOrderModel,
            api.TypeModel,
            api.AppliedSalesOrderDiscountModel,
            api.AppliedSalesOrderItemDiscountModel> {
        // Convenience Redirects
        get hasSalesGroup(): boolean {
            return this.cefConfig.featureSet.shipping.splitShipping.enabled
                && this.cefConfig.featureSet.salesGroups.enabled
                && (angular.isDefined(this.record.SalesGroupAsMasterID)
                 || angular.isDefined(this.record.SalesGroupAsSubID));
        }
        // Properties
        itemType = "Order";
        salesGroupRecord: api.SalesGroupModel;
        createRecordCall = this.cvApi.ordering.CreateSalesOrder;
        updateRecordCall = this.cvApi.ordering.UpdateSalesOrder;
        // Functions
        loadRecord(id: number): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.cvApi.ordering.GetSalesOrderByID(id).then(r => {
                    if (!r || !r.data) {
                        reject(`There was an error loading the ${this.itemType}`);
                        return;
                    }
                    this.record = r.data;
                    this.$q.all([
                        this.$q((resolve2, reject2) => {
                            if (!this.hasSalesGroup) {
                                resolve2(true);
                            } else {
                                this.cvApi.sales.GetSalesGroupByID(this.record.SalesGroupAsMasterID || this.record.SalesGroupAsSubID)
                                    .then(salesGroupResult => {
                                        resolve2(this.salesGroupRecord = salesGroupResult.data);
                                    }).catch(err => {
                                        resolve2();
                                        return this.consoleDebug(err);
                                    });
                            }
                        }),
                        this.doSameAsBillingCheckAndAssign(),
                        this.readOutDiscounts()
                    ]).then(() => resolve(true));
                }).catch(reject);
            });
        }
        newRecord(): api.SalesOrderModel {
            return <api.SalesOrderModel>{
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
            let newTotal = this.record.Totals.Total =
                  (this.record.Totals.SubTotal || 0)
                + (this.record.Totals.Shipping || 0)
                + (this.record.Totals.Handling || 0)
                + (this.record.Totals.Fees || 0)
                + (this.record.Totals.Tax || 0)
                - Math.abs(this.record.Totals.Discounts || 0);
            if (!this.record.BalanceDue) {
                this.record.BalanceDue = 0;
            }
            this.record.BalanceDue += (newTotal - oldTotal);
        }
        // Sales Order Actions (state changes, etc)
        confirmStockOrder(): void {
            console.warn("Confirm Stock On Order clicked but is not implemented yet");
            /*
            // TODO: I feel like this isn't the correct way to do this.
            this.selectedNotification = 5;
            this.sendNotification();   // Sends the email
            // TODO: Need to capture payment that was previously authorized
            // Find the correct status, and then set it
            var completedid = -1;
            this.statuses.forEach((item) => {
                if (item.Name === "Ready to Pickup at Store") {
                    completedid = item.ID;
                }
            });
            if (completedid === -1) {
                aler t("\"Ready to Pickup at Store\" not found in statuses");
                return;
            }
            this.salesOrder.StatusID = completedid;
            // TODO: The save currently isn't saving the status.  Need to figure out why.
            this.save(); // Saves the changes in the order, including the new status, and then loads the sales open orders search
            */
        }
        insufficientStockOrder(): void {
            console.warn("Mark Insufficient Stock On Order clicked but is not implemented yet");
            /*
            this.selectedNotification = 6;
            this.sendNotification();   // Sends the email
            // Find the correct status, and then set it
            var completedid = -1;
            this.statuses.forEach((item) => {
                if (item.Name === "Insufficient Stock for Store Pickup") {
                    completedid = item.ID;
                }
            });
            if (completedid === -1) {
                aler t("\"Insufficient Stock for Store Pickup\" not found in statuses");
                return;
            }
            this.salesOrder.StatusID = completedid;
            // TODO: The save currently isn't saving the status. Need to figure out why.
            this.saveRecord();
            */
        }
        confirmOrder(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.MarkOrderConfirmed.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.ordering.ConfirmSalesOrder));
        }
        backorderOrder(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.MarkOrderBackordered.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.ordering.BackorderSalesOrder));
        }
        /*
        splitOrder(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.MarkOrderSplit.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.ordering.SplitSalesOrder));
        }
        */
        createInvoiceForOrder(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.CreateInvoiceForOrder.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.providers.CreateInvoiceForSalesOrder));
        }
        capturePayment(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.CapturePaymentForOrder.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.ordering.CapturePaymentForSalesOrder));
        }
        pendingOrder(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.MarkOrderPending.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.ordering.PendingSalesOrder));
        }
        addPaymentToOrder(): void {
            console.warn("Add Payment to Order clicked but is not implemented yet");
            /*this.doStateChangeAction(
                "Are you sure you want to Capture Payment this Order? This action cannot be undone.",
                this.cvApi.ordering.CapturePaymentForSalesOrder);*/
        }
        createPickTicketForOrder(): void {
            this.doStateChangeAction(this.cvApi.ordering.CreatePickTicketForSalesOrder);
            // TODO: Generate some kind of window that they can print
        }
        dropShipOrder(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.MarkOrderDropShipped.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.ordering.DropShipSalesOrder));
        }
        shipOrder(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.MarkOrderShipped.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.ordering.ShipSalesOrder));
            // TODO: Open a form modal to capture the tracking number
        }
        readyForPickupOrder(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.MarkOrderReadyForPickup.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.ordering.ReadyForPickupSalesOrder));
        }
        completeOrder(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.MarkOrderCompleted.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.ordering.CompleteSalesOrder));
        }
        voidOrder(): void {
            this.$translate("ui.admin.controls.sales.salesOrderDetail.MarkOrderVoided.Message")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.ordering.VoidSalesOrder));
        }
        addPaymentToInvoice(): void {
            console.warn("Add Payment to Invoice clicked but is not implemented yet");
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

    adminApp.directive("salesOrderDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/salesOrderDetail.html", "ui"),
        controller: SalesOrderDetailController,
        controllerAs: "ssoeCtrl"
    }));
}
