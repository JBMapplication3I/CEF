/**
 * @file framework/store/purchasing/steps/payment/methods/free/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.free {
    class FreePaymentMethodSummaryController extends core.TemplatedControllerBase {
    }

    cefApp.directive("cefPaymentMethodFreeSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/free/summary.html", "ui"),
        controller: FreePaymentMethodSummaryController,
        controllerAs: "pmfsCtrl",
        bindToController: true
    }));
}
