/**
 * @file framework/admin/purchasing/steps/payment/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment {
    // This is the controller for the directive
    class PurchaseStepPaymentBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected lookupKey: api.CartByIDLookupKey;
        // Properties
        private get step(): PurchaseStepPayment {
            if (!this.lookupKey
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.lookupKey.toString()]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepPayment>
                _.find(this.cvPurchaseService.steps[this.lookupKey.toString()],
                    x => x.name === this.cvServiceStrings.checkout.steps.payment);
        }
        protected get paymentMethod(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.paymentMethod[this.lookupKey.toString()];
        }
        protected set paymentMethod(newValue: string) {
            this.consoleDebug(`PurchaseStepPaymentBodyController.paymentMethod.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepPaymentBodyController.paymentMethod.set 2`);
                this.step.paymentMethod[this.lookupKey.toString()] = newValue;
            }
        }
        protected get paymentMethods(): { [method: string]: methods.IPaymentMethod } {
            if (!this.step) {
                return undefined;
            }
            return this.step.paymentMethods[this.lookupKey.toString()];
        }
        protected hidePaymentMethod(name: string): boolean {
            return !this.cvCartService.accessCart(this.lookupKey)
                || !this.cvCartService.accessCart(this.lookupKey).Totals
                || name === this.cvServiceStrings.checkout.paymentMethods.free
                && this.cvCartService.accessCart(this.lookupKey).Totals.Total > 0
                || name !== this.cvServiceStrings.checkout.paymentMethods.free
                    && this.cvCartService.accessCart(this.lookupKey).Totals.Total <= 0;
        }
        // Functions
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`PurchaseStepPaymentBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepPaymentBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        // Events
        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            this.consoleDebug(`PurchaseStepPaymentBodyController.onChange()`);
            this.invalid = true;
            if (this.forms.payment.$invalid) {
                this.consoleDebug(`PurchaseStepPaymentBodyController.onChange() failed!`);
                this.invalid = true;
                return;
            }
            this.setRunning();
            this.step.validate(this.cvCartService.accessCart(this.lookupKey)).then(success => {
                if (!success) {
                    this.consoleDebug(`PurchaseStepPaymentBodyController.onChange() failed!`);
                    this.invalid = true;
                    this.finishRunning(true, "An error has occurred");
                    return;
                }
                this.consoleDebug(`PurchaseStepPaymentBodyController.onChange() success!`);
                this.invalid = false;
                this.finishRunning();
            }).catch(reason => {
                this.consoleDebug(`PurchaseStepPaymentBodyController.onChange() failed!`);
                this.invalid = true;
                this.finishRunning(true, reason || "An error has occurred");
            });
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $rootScope: ng.IRootScopeService,
                readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvCartService: services.ICartService,
                private readonly cvPurchaseService: services.IPurchaseService) {
            super(cefConfig);
            this.consoleDebug(`PurchaseStepPaymentBodyController.ctor()`);
        }
    }

    adminApp.directive("cefPurchaseStepPaymentBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/payment/body.html", "ui"),
        controller: PurchaseStepPaymentBodyController,
        controllerAs: "pspbCtrl",
        bindToController: true
    }));
}
