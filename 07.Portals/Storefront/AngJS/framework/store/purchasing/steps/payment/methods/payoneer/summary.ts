/**
 * @file framework/store/purchasing/steps/payment/methods/payoneer/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.payoneer {
    class PayoneerPaymentMethodSummaryController extends core.TemplatedControllerBase {
    }

    cefApp.directive("cefPaymentMethodPayoneerSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/payoneer/summary.html", "ui"),
        controller: PayoneerPaymentMethodSummaryController,
        controllerAs: "pmpsCtrl",
        bindToController: true
    }));
}
