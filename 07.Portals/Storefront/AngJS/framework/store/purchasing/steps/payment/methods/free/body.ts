/**
 * @file framework/store/purchasing/steps/payment/methods/free/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.free {
    class FreePaymentMethodController extends core.TemplatedControllerBase {
    }

    cefApp.directive("cefPaymentMethodFreeBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/free/body.html", "ui"),
        controller: FreePaymentMethodController,
        controllerAs: "pmfbCtrl",
        bindToController: true
    }));
}
