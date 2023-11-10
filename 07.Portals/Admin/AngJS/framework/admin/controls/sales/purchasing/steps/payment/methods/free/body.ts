/**
 * @file framework/admin/purchasing/steps/payment/methods/free/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.free {
    class FreePaymentMethodController extends core.TemplatedControllerBase {
    }

    adminApp.directive("cefPaymentMethodFreeBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/payment/methods/free/body.html", "ui"),
        controller: FreePaymentMethodController,
        controllerAs: "pmfbCtrl",
        bindToController: true
    }));
}
