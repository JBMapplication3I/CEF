/**
 * @file framework/store/purchasing/steps/payment/methods/ach/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.ach {
    class ACHPaymentMethodController extends core.TemplatedControllerBase {
    }

    cefApp.directive("cefPaymentMethodAchBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/ach/body.html", "ui"),
        controller: ACHPaymentMethodController,
        controllerAs: "pmachbCtrl",
        bindToController: true
    }));
}
