/**
 * @file framework/store/purchasing/steps/payment/methods/wireTransfer/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.wireTransfer {
    class WireTransferPaymentMethodController extends core.TemplatedControllerBase {
        // Properties
        private get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.wireTransfer;
        }
        protected cartType: string; // Bound by Scope
        private get step(): PurchaseStepPayment {
            if (!this.cartType ||
                !this.cvPurchaseService ||
                !this.cvPurchaseService.steps ||
                !this.cvPurchaseService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepPayment>
                _.find(this.cvPurchaseService.steps[this.cartType],
                    x => x.name === this.cvServiceStrings.checkout.steps.payment);
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            if (this.step) {
                this.step.invalid = newValue;
            }
        }
        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            this.invalid = false;
        }
        // Constructor
        constructor(
            readonly $scope: ng.IScope,
            readonly $rootScope: ng.IRootScopeService,
            readonly cvServiceStrings: services.IServiceStrings,
            protected readonly cefConfig: core.CefConfig, // Used by UI
            private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
            private readonly cvCartService: services.ICartService,
            private readonly cvPurchaseService: services.IPurchaseService) {
            super(cefConfig);
            this.onChange();
        }
    }

    cefApp.directive("cefPaymentMethodWireTransferBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/wireTransfer/body.html", "ui"),
        controller: WireTransferPaymentMethodController,
        controllerAs: "pmwtbCtrl",
        bindToController: true
    }));
}
