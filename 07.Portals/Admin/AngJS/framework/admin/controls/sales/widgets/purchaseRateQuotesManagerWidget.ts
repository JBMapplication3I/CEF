/**
 * @file framework/admin/controls/sales/widgets/purchaseRateQuotesManagerWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase rate quotes manager widget class.
 */
module cef.admin.controls.sales.widgets {
    class PurchaseRateQuotesManagerWidgetController extends RateQuotesManagerWidgetControllerBase {
        // Bound Scope Properties
        contactKey: string;
        // Properties
        get cart(): api.CartModel {
            return this.cvCartService.accessCart(this.lookupKey);
        }
        get havePostalCode() {
            return this.contact && this.contact.Address && this.contact.Address.PostalCode;
        }
        // Constructors
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvCartService: admin.services.ICartService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                protected readonly cvViewStateService: admin.services.IViewStateService) {
            super($rootScope, $scope, $q, cvCartService, cefConfig, cvApi, cvServiceStrings, cvViewStateService);
        }
    }

    adminApp.directive("cefPurchaseRateQuotesManagerWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            currentCartId: "=",
            userId: "=",
            lookupKey: "=", // The type of the cart as a lookup key
            contact: "=", // The selected address in Checkout
            contactKey: "=", // The Key of the address that was selected
            apply: "=",
            hideAddress: "=?",
            hideTitle: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/widgets/purchaseRateQuotesManagerWidget.html", "ui"),
        controller: PurchaseRateQuotesManagerWidgetController,
        controllerAs: "cRQMWCtrl",
        bindToController: true
    }));
}
