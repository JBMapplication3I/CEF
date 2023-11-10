module cef.admin.controls.sales.widgets {
    class TotalsWidgetController extends core.TemplatedControllerBase {
        // Bound Scope properties
        totals: api.CartTotals;
        isEditable: boolean;
        record: api.SalesCollectionBaseModel;
        /**
         * A boolean flag to update the record's balance due on value change
         * @type {boolean}
         * @default undefined
         */
        updateBalanceDue: boolean;
        discountItems: Array<any>;
        originalCurrency: string;
        sellingCurrency: string;
        // Properties
        // <None>
        // Functions
        get labelColumnWidthClass(): string { return this.isEditable ? "w-45" : "w-50"; }
        get valueColumnWidthClass(): string { return this.isEditable ? "w-45" : "w-50"; }
        get editColumnWidthClass(): string { return this.isEditable ? "w-10" : "hide"; }

        protected isOneOfTheseStatuses(statuses: string[]): boolean {
            if (!statuses || statuses.length == 0 || !this.record) { return false; }
            return _.some(
                statuses,
                status => status == this.record.StatusName
                       || status == this.record.StatusKey
                       || this.record.Status && status == this.record.Status.CustomKey
                       || this.record.Status && status == this.record.Status.Name);
        }

        protected editValue(property: string): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/sales/modals/editATotalModal.html", "ui"),
                size: this.cvServiceStrings.modalSizes.sm,
                controller: modals.EditATotalModalController,
                controllerAs: "editATotalModalCtrl",
                resolve: {
                    originalValue: () => this.totals[property],
                    property: () => property
                }
            }).result
                .then((result: number) => this.totals[property] = result)
                .then(() => {
                    let oldTotal, newTotal;
                    if (this.updateBalanceDue) {
                        oldTotal = this.totals.Total;
                    }
                    newTotal = this.totals.Total =
                        + (this.totals.SubTotal || 0)
                        + (this.totals.Shipping || 0)
                        + (this.totals.Handling || 0)
                        + (this.totals.Fees || 0)
                        + (this.totals.Tax || 0)
                        - Math.abs(this.totals.Discounts || 0);
                    if (this.updateBalanceDue) {
                        if (!this.record.BalanceDue) {
                            this.record.BalanceDue = 0;
                        }
                        this.record.BalanceDue += (newTotal - oldTotal);
                    }
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.sales.totalsUpdated);
                });
        }
        protected openDiscountsModal(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/sales/modals/editAppliedDiscountsModal.html", "ui"),
                size: this.cvServiceStrings.modalSizes.lg,
                controller: modals.EditAppliedDiscountsModalController,
                controllerAs: "editAppliedDiscountsModalCtrl",
                resolve: {
                    record: () => this.record,
                    discountItems: () => this.discountItems
                }
            }).result.then(result => this.discountItems = result);
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $filter: ng.IFilterService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: admin.services.IServiceStrings) {
            super(cefConfig);
        }
    }

    adminApp.directive("cefTotalsWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            totals: "=", // A CartTotals object, usually {salesCollection}.Totals
            isEditable: "=", // Will show edit buttons if true
            record: "=",
            updateBalanceDue: "=?",
            discountItems: "=",
            originalCurrency: "=?",
            sellingCurrency: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/widgets/totalsWidget.html", "ui"),
        controller: TotalsWidgetController,
        controllerAs: "twCtrl",
        bindToController: true
    }));
}
