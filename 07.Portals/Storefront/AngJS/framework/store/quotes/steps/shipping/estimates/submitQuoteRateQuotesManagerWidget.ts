/**
 * @file framework/store/quotes/steps/shipping/estimates/submitQuoteRateQuotesManagerWidget.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote rate quotes manager widget class.
 */
module cef.store.quotes.steps.shipping.estimates {
    class SubmitQuoteRateQuotesManagerWidgetController extends cart.controls.rateQuotes.RateQuotesManagerWidgetControllerBase {
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
                protected readonly cvSubmitQuoteService: services.ISubmitQuoteService,
                protected readonly cvViewStateService: services.IViewStateService) {
            super($rootScope, $scope, $q, $timeout, cefConfig, cvCartService, cvApi, cvServiceStrings, cvViewStateService);
        }
    }

    cefApp.directive("cefSubmitQuoteRateQuotesManagerWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            type: "=", // The type of the cart e.g.- "Cart"
            contact: "=", // The selected address in Checkout
            apply: "=",
            hideAddress: "=?",
            hideTitle: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/quotes/steps/shipping/estimates/submitQuoteRateQuotesManagerWidget.html", "ui"),
        controller: SubmitQuoteRateQuotesManagerWidgetController,
        controllerAs: "cRQMWCtrl",
        bindToController: true
    }));
}
