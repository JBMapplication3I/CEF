/**
 * @file framework/store/purchasing/steps/shipping/estimates/purchaseRateQuotesManagerWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Checkout rate quotes manager widget class.
 */
module cef.store.purchasing.steps.shipping.estimates {
    class PurchaseRateQuotesManagerWidgetController extends cart.controls.rateQuotes.RateQuotesManagerWidgetControllerBase {
        // Properties
        get cart(): api.CartModel {
            return this.cvCartService.accessCart(this.type);
        }
        get havePostalCode() {
            return this.contact && this.contact.Address && this.contact.Address.PostalCode;
        }
        // Constructors
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $timeout: ng.ITimeoutService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvPurchaseService: services.IPurchaseService,
                protected readonly cvViewStateService: services.IViewStateService) {
            super($rootScope, $scope, $q, $timeout, cefConfig, cvCartService, cvApi, cvServiceStrings, cvViewStateService);
        }
    }

    cefApp.directive("cefPurchaseRateQuotesManagerWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            type: "=", // The type of the cart e.g.- "Cart"
            contact: "=", // The selected address in Checkout
            apply: "=",
            hideAddress: "=?",
            hideTitle: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/shipping/estimates/purchaseRateQuotesManagerWidget.html", "ui"),
        controller: PurchaseRateQuotesManagerWidgetController,
        controllerAs: "cRQMWCtrl",
        bindToController: true
    }));
}
