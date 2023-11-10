/**
 * @file framework/store/purchasing/steps/payment/methods/custom/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.custom {
    class CustomPaymentMethodController extends core.TemplatedControllerBase {
    }

    cefApp.directive("cefPaymentMethodCustomBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/custom/body.html", "ui"),
        controller: CustomPaymentMethodController,
        controllerAs: "pmcbCtrl",
        bindToController: true
    }));
}
