/**
 * @file framework/admin/controls/sales/widgets/lineItemsWidget.ts
 * @desc Line Items Widget for Sales Collection Details Pages (displays
 * the line items and grants access to editing them)
 */
module cef.admin.controls.sales.widgets {
    class LineItemsWidgetController extends core.TemplatedControllerBase {
        // Properties
        /** The SalesCollection such as a SalesOrder or SalesQuote. Populated by Scope. */
        record: api.SalesCollectionBaseModelT<
            api.TypeModel,
            api.AmARelationshipTableModel<api.ContactModel>,
            api.SalesEventBaseModel,
            api.AppliedDiscountBaseModel,
            api.AppliedDiscountBaseModel,
            api.AmAStoredFileRelationshipTableModel>;
        /** Will show edit buttons if true. Populated by Scope. */
        isEditable: boolean;
        /** Quote Responses for the line items */
        responses: Array<api.SalesQuoteModel>;
        paging: core.Paging<api.SalesItemBaseModel<api.AppliedDiscountBaseModel>> = null;
        kind: string; // Bound by Scope
        // Functions
        loadAndAssignItems(): void {
            if (!this.record || !this.record.SalesItems) {
                this.$timeout(() => this.loadAndAssignItems(), 100, true);
                return;
            }
            this.paging.data = _.filter(
                this.record.SalesItems,
                value => value.Active === true);
            this.finishRunning();
        }
        protected addLineItem(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/sales/modals/addALineItemModal.html", "ui"),
                size: this.cvServiceStrings.modalSizes.lg,
                controller: modals.AddALineItemModalController,
                controllerAs: "addALineItemModalCtrl",
                resolve: {
                    responses: () => this.responses,
                    kind: () => this.kind,
                    targetUserID: () => this.record.UserID
                }
            }).result.then(result => {
                this.setRunning();
                let fakeID = this.record.SalesItems.length * -1;
                while (_.findIndex(this.record.SalesItems, x => x.ID === fakeID) !== -1) {
                    fakeID = fakeID - 1;
                }
                result.ID = fakeID;
                this.record.SalesItems.push(result);
                this.loadAndAssignItems(); // Will do finishRunning
                this.$rootScope.$broadcast(this.cvServiceStrings.events.lineItems.updated);
            });
        }
        protected editLineItem(item: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>): void {
            if (!item) { return; }
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/admin/controls/sales/modals/editALineItemModal.html", "ui"),
                size: this.cvServiceStrings.modalSizes.lg,
                controller: modals.EditALineItemModalController,
                controllerAs: "editALineItemModalCtrl",
                resolve: {
                    originalValue: () => item,
                    responses: () => this.responses,
                    kind: () => this.kind,
                    targetUserID: () => this.record.UserID
                }
            }).result.then(result => {
                this.setRunning();
                var index = _.findIndex(
                    this.record.SalesItems,
                    (x: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>) => (x.ID && result.ID && x.ID === result.ID)
                         || (x.CustomKey && result.CustomKey && x.CustomKey === result.CustomKey));
                this.record.SalesItems[index] = result;
                this.loadAndAssignItems(); // Will do finishRunning
                this.$rootScope.$broadcast(this.cvServiceStrings.events.lineItems.updated);
            });
        }
        protected removeLineItem(item: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>): void {
            if (!item) { return; }
            // Pop a confirm remove modal
            this.cvConfirmModalFactory(this.$translate("ui.admin.common.Remove", { name: item.Name })).then(ok => {
                if (!ok) { return; }
                var index = _.findIndex(
                    this.record.SalesItems,
                    (x: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>) => (x.ID && item.ID && x.ID === item.ID)
                        || (x.CustomKey && item.CustomKey && x.CustomKey === item.CustomKey));
                this.record.SalesItems[index].Active = false;
                this.loadAndAssignItems(); // Will do finishRunning
                this.$rootScope.$broadcast(this.cvServiceStrings.events.lineItems.updated);
            });
        }
        // Events
        // <None>
        // Context Menu Managment
        dataItems: { [id: number]: any } = { };
        currentMenuDataItem: any = null;
        commandCellLineItem: { [id: number]: ng.IAugmentedJQuery } = { };
        // NOTE: This must remain an arrow function
        setCommandCell = (id: number): void => {
            this.commandCellLineItem[id] = angular.element(document.querySelector(`#commandCellLineItem${id}`));
        };
        // NOTE: This must remain an arrow function
        openMenu = (id: number, item: any): void => {
            this.currentMenuDataItem = item;
            this.dataItems[id] = item;
            var menu = $(`#menuActionsLineItem${id}`).data("kendoContextMenu");
            if (menu && angular.isFunction(menu.open)) {
                const rect = $(`#commandCellLineItem${id}`)[0].getBoundingClientRect();
                const x = rect.left;
                const y = rect.top + rect.height;
                menu.open(x, y);
            }
        };
        // NOTE: This must remain an arrow function
        onMenuOpen = (id: number, item: any): void => {
            this.currentMenuDataItem = item;
            this.dataItems[id] = item;
            // Close other menus
            if (!this.dataItems) {
                return;
            }
            Object.keys(this.dataItems).forEach(diID => {
                if (String(diID) === String(id)) {
                    return;
                }
                var menu = $(`#menuActionsLineItem${diID}`).data("kendoContextMenu");
                if (menu && angular.isFunction(menu.close)) {
                    menu.close(null);
                }
                delete this.dataItems[diID];
            });
        };
        // NOTE: This must remain an arrow function
        closeMenu = (id: number): void => {
            if (this.currentMenuDataItem && this.currentMenuDataItem.ID === id) {
                this.currentMenuDataItem = null;
            }
        };
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $filter: ng.IFilterService,
                private readonly $timeout: ng.ITimeoutService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: admin.services.IServiceStrings,
                private readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory/*,
                private readonly cvLineItemAnalyzerService: ILineItemAnalyzerService*/) {
            super(cefConfig);
            this.setRunning();
            this.paging = new core.Paging<api.SalesItemBaseModel<api.AppliedDiscountBaseModel>>($filter);
            this.paging.pageSize = 8;
            this.paging.pageSetSize = 3;
            this.loadAndAssignItems();
        }
    }

    adminApp.directive("cefLineItemsWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            /** The SalesCollection such as a SalesOrder or SalesQuote */
            record: "=",
            /** Will show edit buttons if true */
            isEditable: "=",
            /** Quote Responses for the line items */
            responses: "=?",
            /** The kind of line item eg- "Sales Quote", "Purchase Order", etc. */
            kind: "@?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/widgets/lineItemsWidget.html", "ui"),
        controller: LineItemsWidgetController,
        controllerAs: "liwCtrl",
        bindToController: true
    }));
}
