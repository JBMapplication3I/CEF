/**
 * @file framework/admin/purchasing/steps/payment/methods/echeck/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.echeck {
    class EcheckPaymentMethodSummaryController extends core.TemplatedControllerBase {
    }

    adminApp.directive("cefPaymentMethodEcheckSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/payment/methods/echeck/summary.html", "ui"),
        controller: EcheckPaymentMethodSummaryController,
        controllerAs: "pmesCtrl",
        bindToController: true
    }));
}
