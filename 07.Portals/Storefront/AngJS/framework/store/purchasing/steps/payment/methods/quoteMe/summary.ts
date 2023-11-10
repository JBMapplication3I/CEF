/**
 * @file framework/store/purchasing/steps/payment/methods/quoteMe/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.quoteMe {
    class QuoteMePaymentMethodSummaryController extends core.TemplatedControllerBase {
    }

    cefApp.directive("cefPaymentMethodQuoteMeSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/quoteMe/summary.html", "ui"),
        controller: QuoteMePaymentMethodSummaryController,
        controllerAs: "pmqmsCtrl",
        bindToController: true
    }));
}
