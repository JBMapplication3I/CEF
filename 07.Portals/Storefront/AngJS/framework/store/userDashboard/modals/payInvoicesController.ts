/**
 * @file framework/store/userDashboard/modals/payInvoicesController.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Pay Invoices controller class
 */
module cef.store.userDashboard.modals {
    class PayInvoicesController {
        // Properties
        get disabled(): boolean {
            return false; // TODO: Read server looking for unpaid invoices
        }
        // Functions
        open(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/modals/payInvoicesModal.html", "ui"),
                size: this.cvServiceStrings.modalSizes.md,
                controller: userDashboard.modals.PayMultipleInvoicesModalController,
                controllerAs: "pmimCtrl",
                resolve: { }
            }).result.then(result => {
                this.$rootScope.$broadcast(this.cvServiceStrings.events.invoices.paymentMade);
                this.cvMessageModalFactory(
                        this.$translate("ui.storefront.userDashboard2.modals.payInvoice.multi.Success.Message"),
                        this.cvServiceStrings.modalSizes.sm)
                    .then(() => {
                        // TODO: Don't require page refresh, make the cv-grid listen
                        // for the above event and rerun search automatically
                        window.location.reload();
                    });
            });
        }
        // Constructor
        constructor(
            private readonly $rootScope: ng.IRootScopeService,
            private readonly $filter: ng.IFilterService,
            private readonly $uibModal: ng.ui.bootstrap.IModalService,
            private readonly $translate: ng.translate.ITranslateService,
            private readonly cefConfig: core.CefConfig, // Used by UI
            private readonly cvServiceStrings: services.IServiceStrings,
            private readonly cvMessageModalFactory: store.modals.IMessageModalFactory) {
        }
    }

    cefApp.controller("PayInvoicesController", PayInvoicesController);
}
