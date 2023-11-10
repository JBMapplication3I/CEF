/**
 * @file framework/store/cart/controls/rateQuotes/cartRateQuotesManagerWidget.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Cart rate quotes manager widget class.
 */
module cef.store.cart.controls.rateQuotes {
    export class CartRateQuotesManagerWidgetController extends RateQuotesManagerWidgetControllerBase {
        // CartRateQuotesManagerWidgetController Properties
        countries: Array<api.CountryModel>; // Populated by load
        regions: Array<api.RegionModel>; // Populated by load and on country selection change
        get cart(): api.CartModel { return this.cvCartService.accessCart(this.type); }
        get havePostalCode() {
            return this.cart
                && this.cart.ShippingContact
                && this.cart.ShippingContact.Address
                && this.cart.ShippingContact.Address.PostalCode;
        }
        // Functions
        // NOTE: This must remain an arrow function
        protected loadCallback = (): void => {
            this.setRunning();
            this.cvCountryService.get({
                code: this.cvCartService.accessCart(this.type).ShippingContact
                    ? this.cvCartService.accessCart(this.type).ShippingContact.Address.CountryCode
                    : this.cefConfig.countryCode
            }).then(c => {
                this.cvRegionService.search({ Active: true, AsListing: true, Sorts: [{ field: "Name", order: 0, dir: "asc" }], CountryID: c.ID })
                    .then(r => {
                        this.regions = r;
                        this.finishRunning();
                    }).catch(reason => this.finishRunning(true, reason));
            });
        }
        // Events
        private onCountryIDChange(): void {
            this.setRunning();
            this.cvCountryService.get({
                id: this.cvCartService.accessCart(this.type).ShippingContact.Address.CountryID
            }).then(c => {
                this.cvRegionService.search({ Active: true, AsListing: true, Sorts: [{ field: "Name", order: 0, dir: "asc" }], CountryID: c.ID })
                    .then(r => {
                        this.regions = r;
                        this.finishRunning();
                    }).catch(reason => this.finishRunning(true, reason));
            });
        }
        // Constructors
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                protected readonly $timeout: ng.ITimeoutService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvViewStateService: services.IViewStateService,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCountryService: services.ICountryService,
                protected readonly cvRegionService: services.IRegionService) {
            super($rootScope, $scope, $q, $timeout, cefConfig, cvCartService, cvApi, cvServiceStrings, cvViewStateService);
        }
    }

    cefApp.directive("cefCartRateQuotesManagerWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { type: "=" }, // The type of the cart e.g.- "Cart"
        templateUrl: $filter("corsLink")("/framework/store/cart/controls/rateQuotes/cartRateQuotesManagerWidget.html", "ui"),
        controller: CartRateQuotesManagerWidgetController,
        controllerAs: "cefCartRateQuotesManagerWidgetCtrl",
        bindToController: true
    }));
}
