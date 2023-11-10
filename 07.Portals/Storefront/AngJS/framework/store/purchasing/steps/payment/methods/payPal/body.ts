/**
 * @file framework/store/purchasing/steps/payment/methods/payPal/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.payment.methods.payPal {
    class PayPalPaymentMethodController extends core.TemplatedControllerBase {
        // Properties
        private get name(): string {
            return this.cvServiceStrings.checkout.paymentMethods.payPal;
        }
        protected cartType: string; // Bound by Scope
        protected client_token: string;
        protected nonce: string;
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
        private get method(): PayPalPaymentMethod {
            if (!this.step ||
                !this.step.paymentMethods ||
                !this.step.paymentMethods[this.cartType] ||
                !this.step.paymentMethods[this.cartType][this.name]) {
                return null; // Not available (yet)
            }
            return <PayPalPaymentMethod>
                this.step.paymentMethods[this.cartType][this.name];
        }
        tempPaymentData: api.CheckoutModel = { } as api.CheckoutModel;
        protected get paymentData(): api.CheckoutModel {
            if (!this.method) {
                return undefined;
            }
            return this.method.paymentData;
        }
        protected set paymentData(newValue: api.CheckoutModel) {
            this.consoleDebug(`PayPalPaymentMethodController.paymentData.set`);
            if (this.method) {
                this.consoleDebug(`PayPalPaymentMethodController.paymentData.set 2`);
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
            this.consoleDebug(`PayPalPaymentMethodBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`PayPalPaymentMethodBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }

        loadBraintree(): void {
            this.$window.braintree.client.create({
                authorization: this.client_token
            }, function (clientErr, clientInstance) {
                // Stop if there was a problem creating the client.
                // This could happen if there is a network error or if the authorization
                // is invalid.
                if (clientErr) {
                    console.error('Error creating client:', clientErr);
                    return;
                }
                // Create a PayPal Checkout component.
                globalThis.braintree.paypalCheckout.create({
                    client: clientInstance
                }, function (paypalCheckoutErr, paypalCheckoutInstance) {
                    paypalCheckoutInstance.loadPayPalSDK({
                        vault: true
                    }, function () {
                        globalThis.paypal.Buttons({
                            fundingSource: globalThis.paypal.FUNDING.PAYPAL,

                            createBillingAgreement: function () {
                                return paypalCheckoutInstance.createPayment({
                                    flow: 'vault', // Required

                                    // The following are optional params
                                    /*billingAgreementDescription: 'Your agreement description',
                                    enableShippingAddress: true,
                                    shippingAddressEditable: false,
                                    shippingAddressOverride: {
                                        recipientName: 'Scruff McGruff',
                                        line1: '1234 Main St.',
                                        line2: 'Unit 1',
                                        city: 'Chicago',
                                        countryCode: 'US',
                                        postalCode: '60652',
                                        state: 'IL',
                                        phone: '123.456.7890'
                                    }*/
                                });
                            },

                            onApprove: function (data, actions) {
                                return paypalCheckoutInstance.tokenizePayment(data, function (err, payload) {
                                    // Add the nonce to the form and submit
                                    $("#nonce").val(payload.nonce);
                                });
                            },

                            onCancel: function (data) {
                                console.log('PayPal payment canceled', JSON.stringify(data, 0 as any, 2));
                            },

                            onError: function (err) {
                                console.error('PayPal error', err);
                            }
                        }).render('#paypal-button').then(function () {
                            // The PayPal button will be rendered in an html element with the ID
                            // `paypal-button`. This function will be called when the PayPal button
                            // is set up and ready to be used
                        });

                    });

                });

            });
        }

        waitForPayment(selector, callback, checkFrequencyInMs): void {
            (function loopSearch() {
                if ($(selector).val()) {
                    callback();
                    return;
                }
                else {
                    setTimeout(function () {
                        loopSearch();
                    }, checkFrequencyInMs);
                }
            })();
        }

        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            this.consoleDebug(`PayPalPaymentMethodBodyController.onChange()`);
            this.invalid = true;
            if (!$("#nonce").val() && !this.nonce) {
                this.consoleDebug(`PayPalPaymentMethodBodyController.onChange() failed!`);
                this.invalid = true;
                return;
            }
            // Save the nonce so if the user switches back to credit card it isn't erased
            // TODO: Let the customer know we've heard back from PayPal?
            this.nonce = $("#nonce").val();
            this.paymentData = this.tempPaymentData;
            this.setRunning();
            this.step.validate(this.cvCartService.accessCart(this.cartType))
                .then(success => {
                    if (!success) {
                        this.consoleDebug(`PayPalPaymentMethodBodyController.onChange() failed!`);
                        this.invalid = true;
                        this.finishRunning(true, "An error has occurred");
                        return;
                    }
                    this.consoleDebug(`PayPalPaymentMethodBodyController.onChange() success!`);
                    this.invalid = false;
                    this.finishRunning();
                }, result => {
                    this.consoleDebug(`PayPalPaymentMethodBodyController.onChange() failed!`);
                    this.invalid = true;
                    this.finishRunning(true, result || "An error has occurred");
                }).catch(reason => {
                    this.consoleDebug(`PayPalPaymentMethodBodyController.onChange() failed!`);
                    this.invalid = true;
                    this.finishRunning(true, reason || "An error has occurred");
                });
        }

        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $rootScope: ng.IRootScopeService,
                readonly cvServiceStrings: services.IServiceStrings,
                protected readonly $window: ng.IWindowService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvCartService: services.ICartService,
                private readonly cvPurchaseService: services.IPurchaseService,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);

            this.onChange();
            // When the nonce has a value, fire the onChange method so the checkout button is enabled
            this.waitForPayment("#nonce", this.onChange, 500);
            let params = {PaymentsProviderName: "BraintreePaymentsProvider"} as api.GetPaymentsProviderAuthenticationTokenDto;
            this.cvApi.payments.GetPaymentsProviderAuthenticationToken(params).then(r => {
                this.client_token = r.data.Result;
                $("#client_token").val(this.client_token);
                this.loadBraintree();
            });
        }
    }

    cefApp.directive("cefPaymentMethodPaypalBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/payment/methods/payPal/body.html", "ui"),
        controller: PayPalPaymentMethodController,
        controllerAs: "pmppbCtrl",
        bindToController: true
    }));
}
