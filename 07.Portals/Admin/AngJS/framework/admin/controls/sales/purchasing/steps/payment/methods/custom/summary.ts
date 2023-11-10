/**
 * @file framework/admin/purchasing/steps/payment/methods/custom/summary.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.custom {
    class CustomPaymentMethodSummaryController extends core.TemplatedControllerBase {
    }

    adminApp.directive("cefPaymentMethodCustomSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/payment/methods/custom/summary.html", "ui"),
        controller: CustomPaymentMethodSummaryController,
        controllerAs: "pmccCtrl",
        bindToController: true
    }));
}
