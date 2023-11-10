/**
 * @file framework/admin/purchasing/steps/payment/methods/wireTransfer/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.wireTransfer {
    class WireTransferPaymentMethodSummaryController extends core.TemplatedControllerBase {
    }

    adminApp.directive("cefPaymentMethodWireTransferSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/payment/methods/wireTransfer/summary.html", "ui"),
        controller: WireTransferPaymentMethodSummaryController,
        controllerAs: "pmwtsCtrl",
        bindToController: true
    }));
}
