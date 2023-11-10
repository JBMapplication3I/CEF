/**
 * @file framework/store/purchasing/steps/payment/methods/payPal/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.payPal {
    class PayPalPaymentMethodSummaryController extends core.TemplatedControllerBase {
    }

    cefApp.directive("cefPaymentMethodPaypalSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/payPal/summary.html", "ui"),
        controller: PayPalPaymentMethodSummaryController,
        controllerAs: "pmppsCtrl",
        bindToController: true
    }));
}
