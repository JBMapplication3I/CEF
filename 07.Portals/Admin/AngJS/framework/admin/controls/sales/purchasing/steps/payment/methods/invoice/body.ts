/**
 * @file framework/admin/purchasing/steps/payment/methods/invoice/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.payment.methods.invoice {
    class InvoicePaymentMethodBodyController extends core.TemplatedControllerBase {
        // Properties
        private get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.invoice;
        }
        protected lookupKey: api.CartByIDLookupKey; // Bound by Scope
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
        private get method(): InvoicePaymentMethod {
            if (!this.step
                || !this.step.paymentMethods
                || !this.step.paymentMethods[this.lookupKey.toString()]
                || !this.step.paymentMethods[this.lookupKey.toString()][this.name]) {
                return null; // Not available (yet)
            }
            return <InvoicePaymentMethod>
                this.step.paymentMethods[this.lookupKey.toString()][this.name];
        }
        protected get paymentData(): api.CheckoutModel {
            if (!this.method) {
                return undefined;
            }
            return this.method.paymentData;
        }
        protected set paymentData(newValue: api.CheckoutModel) {
            this.consoleDebug(`InvoicePaymentMethodBodyController.paymentData.set`);
            if (this.method) {
                this.consoleDebug(`InvoicePaymentMethodBodyController.paymentData.set 2`);
                this.method.paymentData = newValue;
            }
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`InvoicePaymentMethodBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`InvoicePaymentMethodBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            this.consoleDebug(`InvoicePaymentMethodBodyController.onChange()`);
            this.invalid = true;
            if (!this.forms || this.forms["invoice"].$invalid) {
                this.consoleDebug(`InvoicePaymentMethodBodyController.onChange() failed!`);
                this.invalid = true;
                return;
            }
            this.setRunning();
            this.step.validate(this.cvCartService.accessCart(this.lookupKey))
                .then(success => {
                    if (!success) {
                        this.consoleDebug(`InvoicePaymentMethodBodyController.onChange() failed!`);
                        this.invalid = true;
                        this.finishRunning(true, "An error has occurred");
                        return;
                    }
                    this.consoleDebug(`InvoicePaymentMethodBodyController.onChange() success!`);
                    this.invalid = false;
                    this.finishRunning();
                }).catch(reason => {
                    this.consoleDebug(`InvoicePaymentMethodBodyController.onChange() failed!`);
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
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvCartService: services.ICartService,
                private readonly cvPurchaseService: services.IPurchaseService) {
            super(cefConfig);
            this.consoleDebug(`InvoicePaymentMethodBodyController.ctor()`);
            this.onChange();
        }
    }

    adminApp.directive("cefPaymentMethodInvoiceBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/payment/methods/invoice/body.html", "ui"),
        controller: InvoicePaymentMethodBodyController,
        controllerAs: "pmibCtrl",
        bindToController: true
    }));
}
