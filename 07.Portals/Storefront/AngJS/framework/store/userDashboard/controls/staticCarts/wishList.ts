/**
 * @file framework/store/userDashboard/controls/orders/staticCarts/wishList.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Wishlist directive for viewing static cart wishlist
 */
module cef.store.userDashboard.controls {
    class WishListController extends StaticCartViewerBaseController {
        // Properties
        // Functions
        convertWishListToQuote($event?: ng.IAngularEvent): void {
            $event.preventDefault();
            this.cvConfirmModalFactory(
                this.$translate("ui.storefront.userDashboard2.controls.wishList.ConvertToQuote.ConfirmHeader",
                    null,
                    null,
                    "Are you sure you want to convert your wishlist to a quote?"))
            .then(this.convertWishListToQuoteRequest)
            .catch(error => this.finishRunning(true, error));
        }
        // NOTE: This must remain an arrow function
        private convertWishListToQuoteInner = (): void => {
            this.setRunning();
            let wishListItems = this.cvCartService.accessCart(this.cvServiceStrings.carts.types.wishList).SalesItems;
            let addToQuoteCartPromises: ng.IPromise<void>[] = [];
            for (let i = 0; i < wishListItems.length; i++) {
                const item = wishListItems[i];
                addToQuoteCartPromises.push(
                    this.cvCartService.addCartItem(
                        item.ID,
                        this.cvServiceStrings.carts.types.quote,
                        item.Quantity,
                        <services.IAddCartItemParams>null,
                        item));
            }
            this.$q.all(addToQuoteCartPromises)
                .then(this.submitQuoteCart)
                .catch(error => this.finishRunning(true, error));
        }
        private convertWishListToQuoteRequest = async (): Promise<void> => {
            this.setRunning();
            let currentUser: api.UserModel;
            try {
                currentUser = await this.cvAuthenticationService.getCurrentUserPromise();
            } catch (err) {
                this.finishRunning(true, err);
                return;
            }
            if (!currentUser || !currentUser.Contact) {
                this.finishRunning(true, "CurrentUser Contact not found");
            }
            this.cvApi.providers.SubmitRequestForQuoteForSingleProduct(<api.SubmitRequestForQuoteForSingleProductDto>{
                ShippingSameAsBilling: false,
                ShippingContact: currentUser.Contact,
                StatusID: 1, // "Submitted"
                StateID: 1, // "WORK"
                Active: true,
                CreatedDate: new Date(),
                TypeID: 1, // "General"
                // Required
                Totals: {
                    SubTotal: 0,
                    Shipping: 0,
                    Handling: 0,
                    Fees: 0,
                    Discounts: 0,
                    Tax: 0,
                    Total: 0
                },
                SalesItems: this.cvCartService.accessCart(this.cvServiceStrings.carts.types.wishList).SalesItems.map(salesItem => {
                    salesItem.ExtendedPrice = salesItem.ExtendedPrice ? salesItem.ExtendedPrice : salesItem.UnitCorePrice
                    return salesItem;
                }),
            }).then(success => {
                if (!success) {
                    return;
                }
                this.cvMessageModalFactory(
                    this.$translate("ui.storefront.userDashboard2.controls.wishList.ConvertToQuote.Success",
                    null,
                    null,
                    "Successfully converted wish list to quote."))
                // clear wishList if successfully converted to a quote
                this.clearList()
                    .then(() => this.finishRunning())
                    .catch(error => this.finishRunning(true, error));
            }).catch(error => this.finishRunning(true, error));
        }
        private submitQuoteCart = (): void => {
            this.setRunning(
                this.$translate("ui.storefront.userDashboard2.controls.wishList.ConvertToQuote.WaitMessage.Ellipsis",
                    null,
                    null,
                    "Submitting quote..."));
            this.cvCartService.loadCart(this.cvServiceStrings.carts.types.quote, true, "cefWishList.submitQuoteCart").then(async cart => {
                const requiredProps = {
                    Billing: cart.Result.BillingContact,
                    Shipping: cart.Result.ShippingContact,
                    IsNewAccount: false,
                    IsPartialPayment: false
                };
                if (!requiredProps.Billing) {
                    requiredProps.Billing = (await this.cvAuthenticationService.getCurrentUserPromise()).Contact;
                }
                this.cvApi.providers.SubmitRequestForQuoteForSingleProduct(angular.extend({}, requiredProps, { StoredFiles: [] }, null)).then(success => {
                    if (!success) {
                        return;
                    }
                    this.cvMessageModalFactory(
                        this.$translate("ui.storefront.userDashboard2.controls.wishList.ConvertToQuote.Success",
                        null,
                        null,
                        "Successfully converted wish list to quote."));
                    // clear wishList if successfully converted to a quote
                    this.clearList()
                        .then(() => this.finishRunning())
                        .catch(error => this.finishRunning(true, error))
                }).catch(error => this.finishRunning(true, error));
            }).catch(error => this.finishRunning(true, error));
        }
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $window: ng.IWindowService,
                protected readonly $q: ng.IQService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvCartService: services.ICartService,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvInventoryService: services.IInventoryService,
                protected readonly cvPurchaseService: services.IPurchaseService,
                private readonly cvMessageModalFactory: store.modals.IMessageModalFactory,
                private readonly cvConfirmModalFactory: store.modals.IConfirmModalFactory) {
            super($q, $window, $rootScope, $scope, cefConfig, cvCartService, cvAuthenticationService, cvServiceStrings, cvInventoryService);
            this.cvAuthenticationService.preAuth().finally(() => this.cvCartService.loadCart(this.type(), true, "WishListController.ctor"));
        }

        type(): string { return this.cvServiceStrings.carts.types.wishList; }
    }

    cefApp.directive("cefWishList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/staticCarts/wishList.html", "ui"),
        controller: WishListController,
        controllerAs: "udwlCtrl"
    }));
}
