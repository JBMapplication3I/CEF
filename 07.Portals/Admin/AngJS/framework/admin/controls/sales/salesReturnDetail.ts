module cef.admin.controls.sales {
    export class SalesReturnDetailController
        extends SalesDetailControllerBase<
            api.SalesReturnModel,
            api.TypeModel,
            api.AppliedSalesReturnDiscountModel,
            api.AppliedSalesReturnItemDiscountModel> {
        // Properties
        itemType = "Return";
        createRecordCall = this.cvApi.returning.CreateSalesReturn;
        updateRecordCall = this.cvApi.returning.UpdateSalesReturn;
        // Functions
        loadRecord(id: number): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                this.cvApi.returning.GetSalesReturnByID(id).then(r => {
                    if (!r || !r.data) {
                        reject(`There was an error loading the ${this.itemType}`);
                        return;
                    }
                    this.record = r.data;
                    this.doSameAsBillingCheckAndAssign();
                    this.readOutDiscounts();
                    resolve(true);
                }, reject).catch(reject);
            });
        }
        newRecord(): api.SalesReturnModel {
            return <api.SalesReturnModel>{
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
        // Sales Return Actions (state changes, etc)
        validateReturn(): void {
            this.$translate("ui.admin.controls.sales.salesReturnDetail.ValidateReturn")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.providers.SetSalesReturnAsConfirmed));
        }
        rejectReturn(): void {
            this.$translate("ui.admin.controls.sales.salesReturnDetail.RejectReturn")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.providers.SetSalesReturnAsRejected));
        }
        refundReturn(): void {
            this.$translate("ui.admin.controls.sales.salesReturnDetail.RefundReturn")
                .then(translated =>
                    this.doStateChangeActionWithConfirm(
                        translated,
                        this.cvApi.providers.SetSalesReturnAsRefunded));
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
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory,
                protected readonly cvMessageModalFactory: admin.modals.IMessageModalFactory) {
            super($scope, $stateParams, $uibModal, $q, cefConfig, cvServiceStrings, cvApi, cvConfirmModalFactory);
            const unbind1 = this.$scope.$on(this.cvServiceStrings.events.lineItems.updated, this.updateTotals);
            const unbind2 = this.$scope.$on(this.cvServiceStrings.events.sales.totalsUpdated, this.updateTotals);
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
        // Deprecated, to remove
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
                { field: "ExtendedPrice", format: "${0:#,##0.00}", title: "Total $", width: "100px", attributes: { style: "text-align: right;" }, headerAttributes: { style: "text-align: center;" } },
                { field: "RestockingFeeAmount", format: "${0:#,##0.00}", title: "Restocking Fee $", width: "100px", attributes: { style: "text-align: right;" }, headerAttributes: { style: "text-align: center;" } }
            ],
            scrollable: false,
            pageable: false
        };
    }

    adminApp.directive("salesReturnDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/salesReturnDetail.html", "ui"),
        controller: SalesReturnDetailController,
        controllerAs: "salesReturnDetailCtrl"
    }));
}
