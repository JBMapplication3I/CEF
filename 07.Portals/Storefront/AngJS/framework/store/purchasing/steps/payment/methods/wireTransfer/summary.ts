/**
 * @file framework/store/purchasing/steps/payment/methods/wireTransfer/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.wireTransfer {
    class WireTransferPaymentMethodSummaryController extends core.TemplatedControllerBase {
    }

    cefApp.directive("cefPaymentMethodWireTransferSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/wireTransfer/summary.html", "ui"),
        controller: WireTransferPaymentMethodSummaryController,
        controllerAs: "pmwtsCtrl",
        bindToController: true
    }));
}
