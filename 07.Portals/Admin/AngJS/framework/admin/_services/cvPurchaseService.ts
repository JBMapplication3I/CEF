/**
 * @file framework/admin/_services/cvPurchaseService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The service which can drive the process
 * by managing the data
 */
module cef.admin.services {
    export interface IPurchaseService {
        // Properties
        steps: { [lookupKey: string]: purchasing.steps.IPurchaseStep[] };
        building: { [lookupKey: string]: boolean };
        activeStep: { [lookupKey: string]: number };
        customAttributes: { [lookupKey: string]: api.SerializableAttributesDictionary };
        categoryIDs: { [lookupKey: string]: number[] };
        fileNames: { [lookupKey: string]: string[] };
        showEmptyCartMessage: boolean;
        showMoreThanNineItemsMessage: boolean;
        checkoutResult: api.CheckoutResult;
        confirmationNumber: string;
        isOrderComplete: boolean;
        showShippingSubtotal: boolean;
        // Functions
        activateStep(lookupKey: api.CartByIDLookupKey, stepIndex: number): ng.IPromise<boolean>;
        anyStepIsBuilding(lookupKey: api.CartByIDLookupKey): boolean;
        anyStepIsRunning(lookupKey: api.CartByIDLookupKey): boolean;
        anyStepIsInvalid(lookupKey: api.CartByIDLookupKey): boolean;
        initialize(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean>;
        finalize(lookupKey: api.CartByIDLookupKey, paymentData: api.CheckoutModel): ng.IPromise<boolean>;
        ////clearAnalysis(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean>;
    }

    export class PurchaseService implements IPurchaseService {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        // Properties
        steps: { [lookupKey: string]: purchasing.steps.IPurchaseStep[] } = { };
        building: { [lookupKey: string]: boolean } = { };
        activeStep: { [lookupKey: string]: number } = { };
        customAttributes: { [lookupKey: string]: api.SerializableAttributesDictionary } = { };
        categoryIDs: { [lookupKey: string]: number[] } = { };
        fileNames: { [lookupKey: string]: string[] } = { };
        checkoutResult: api.CheckoutResult = null;
        confirmationNumber: string = null;
        isOrderComplete: boolean = false;
        showShippingSubtotal: boolean = false;
        // Functions
        activateStep(lookupKey: api.CartByIDLookupKey, stepIndex: number): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseService.activateStep(cartType: "${lookupKey.toString()}", stepIndex: ${stepIndex})`);
            this.activeStep[lookupKey.toString()] = Number(stepIndex);
            return this.$q.resolve(this.steps[lookupKey.toString()][stepIndex].activate(lookupKey));
        }
        anyStepIsBuilding(lookupKey: api.CartByIDLookupKey): boolean {
            ////this.consoleDebug(`PurchaseService.anyStepIsBuilding(cartType: ${cartType})`);
            return this.building[lookupKey.toString()]
                || this.steps
                    && this.steps[lookupKey.toString()]
                    && _.some(this.steps[lookupKey.toString()], x => x.building[lookupKey.toString()]);
        }
        anyStepIsRunning(lookupKey: api.CartByIDLookupKey): boolean {
            ////this.consoleDebug(`PurchaseService.anyStepIsRunning(cartType: ${cartType})`);
            return this.steps
                && this.steps[lookupKey.toString()]
                && _.some(this.steps[lookupKey.toString()], x => x.viewState.running);
        }
        anyStepIsInvalid(lookupKey: api.CartByIDLookupKey): boolean {
            ////this.consoleDebug(`PurchaseService.anyStepIsInvalid(cartType: ${cartType})`);
            return this.steps
                && this.steps[lookupKey.toString()]
                && _.some(this.steps[lookupKey.toString()], x => x.viewState.hasError);
        }
        private isValidCart(cart: api.CartModel): boolean {
            ////this.consoleDebug(`PurchaseService.isValidCart(cart)`);
            if (!cart) {
                return false;
            }
            let asString = angular.toJson(cart);
            const copy = angular.fromJson(asString);
            delete copy["__caller"];
            delete copy["hasASelectedRateQuote"];
            delete copy["WasValidatedInService"];
            asString = angular.toJson(copy);
            return !(asString === "{}");
        }
        private fullReject(lookupKey: api.CartByIDLookupKey, reject, toReturn: any): void {
            this.consoleDebug(`PurchaseService.fullReject(cartType: "${lookupKey.toString()}", reject, toReturn)`);
            if (toReturn) {
                this.consoleDebug(toReturn);
            }
            reject(toReturn);
            this.building[lookupKey.toString()] = false;
        }
        showEmptyCartMessage: boolean = false;
        showMoreThanNineItemsMessage: boolean = false;
        initialize(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean> {
            const debugMsg = `PurchaseService.initialize(cartType: "${lookupKey.toString()}")`;
            this.consoleDebug(debugMsg);
            this.building[lookupKey.toString()] = true;
            return this.$q((resolve, reject) => {
                this.cvCartService.loadCart(lookupKey, false).then(cartResult => {
                    if (!cartResult) {
                        this.fullReject(lookupKey, reject, { issue: "No response for cart request" });
                        return;
                    }
                    if (!cartResult.ActionSucceeded) {
                        this.fullReject(lookupKey, reject, { issue: "Invalid cart", messages: cartResult.Messages });
                        return;
                    }
                    if (!this.isValidCart(cartResult.Result)) {
                        this.showEmptyCartMessage = true;
                        this.fullReject(lookupKey, reject, { issue: "Invalid cart" });
                        return;
                    }
                    /* TODO: Put this behind a setting
                    if (cartType === "Cart") {
                        const cartQuantity = cartResult.Result.SalesItems.reduce((prevValue, curValue) => {
                            return prevValue + curValue.Quantity + (curValue.QuantityBackOrdered || 0) + (curValue.QuantityPreSold || 0);
                        }, 0);
                        if (cartQuantity > 9) {
                            this.showMoreThanNineItemsMessage = true;
                            this.fullReject(cartType, reject, "Invalid cart");
                            return;
                        }
                    }
                    */
                    const stepSetups = [
                        new purchasing.steps.method.PurchaseStepMethod(
                            this.$q, this.cefConfig, this.cvServiceStrings),
                        new purchasing.steps.billing.PurchaseStepBilling(
                            this.$q, this.cefConfig, this.cvServiceStrings, this.cvAuthenticationService,
                            this.cvAddressBookService, this.cvCartService),
                        new purchasing.steps.splitShipping.PurchaseStepSplitShipping(
                            this.$q, this.cefConfig, this.cvServiceStrings, this.cvApi,
                            this.cvAuthenticationService, this.cvAddressBookService, this.cvCartService,
                            this),
                        new purchasing.steps.payment.PurchaseStepPayment(
                            this.$rootScope, this.$q, this.cefConfig, this.cvServiceStrings,
                            this.cvAuthenticationService, this.cvCartService, this.cvWalletService,
                            this),
                        new purchasing.steps.confirmation.PurchaseStepConfirmation(
                            this.$q, this.cefConfig, this.cvServiceStrings, this.cvAuthenticationService,
                            this.cvAddressBookService, this.cvCartService, this),
                        new purchasing.steps.completed.PurchaseStepCompleted(
                            this.$q, this.cefConfig, this.cvServiceStrings),
                    ];
                    const tempSteps: purchasing.steps.IPurchaseStep[] = [];
                    this.$q.all([
                        stepSetups[0].canEnable(cartResult.Result),
                        stepSetups[1].canEnable(cartResult.Result),
                        stepSetups[2].canEnable(cartResult.Result),
                        stepSetups[3].canEnable(cartResult.Result),
                        stepSetups[4].canEnable(cartResult.Result),
                        stepSetups[5].canEnable(cartResult.Result)
                    ]).then((rarr: boolean[]) => {
                        for (let i = 0; i < rarr.length; i++) {
                            if (rarr[i]) {
                                // Step is valid
                                this.consoleDebug(`${debugMsg} ${stepSetups[i].name} is valid, adding to UI`);
                                tempSteps.push(stepSetups[i]);
                                stepSetups[i].index = tempSteps.length - 1;
                            } else {
                                this.consoleDebug(`${debugMsg} ${stepSetups[i].name} is invalid, not adding to UI`);
                            }
                        }
                        if (!tempSteps.length) {
                            this.consoleDebug(`${debugMsg} ERROR! No steps area set up for purchase UI!`);
                            this.fullReject(lookupKey, reject, { issue: "ERROR! No steps area set up for purchase UI!" });
                            return;
                        }
                        this.cvAuthenticationService.preAuth().finally(() => {
                            this.$q.all(tempSteps.map(s => s.initialize(cartResult.Result))).then(rarr => {
                                for (let i = 0; i < rarr.length; i++) {
                                    if (!rarr[i]) {
                                        this.consoleDebug(`${debugMsg} step ${i} could not initialize`);
                                    }
                                }
                                const methodStep = _.find(tempSteps,
                                    x => x.name === this.cvServiceStrings.checkout.steps.method);
                                if (methodStep && this.cvAuthenticationService.isAuthenticated()) {
                                    methodStep.complete = true;
                                    this.activeStep[lookupKey.toString()] = 1;
                                } else {
                                    this.activeStep[lookupKey.toString()] = 0;
                                }
                                this.consoleDebug(`${debugMsg} Complete!`);
                                this.steps[lookupKey.toString()] = tempSteps;
                                const cart = this.cvCartService.accessCart(lookupKey);
                                if (!cart || !cart.SalesItems || !cart.SalesItems.length) {
                                    this.consoleDebug(`${debugMsg} Need to reload the cart again`);
                                    this.cvCartService.loadCart(lookupKey, true)
                                        .finally(() => {
                                            this.consoleDebug(`${debugMsg} cart reload finished`);
                                            this.building[lookupKey.toString()] = false;
                                            resolve(true);
                                        });
                                    return;
                                }
                                this.consoleDebug(`${debugMsg} didn't need to reload`);
                                this.building[lookupKey.toString()] = false;
                                resolve(true);
                            });
                        });
                    }).catch(reason2 => this.fullReject(lookupKey, reject, reason2));
                }).catch(reason1 => this.fullReject(lookupKey, reject, reason1));
            });
        }
        finalize(lookupKey: api.CartByIDLookupKey, paymentData: api.CheckoutModel): ng.IPromise<boolean> {
            const debugMsg = `PurchaseService.finalize(cartType: "${lookupKey.toString()}", paymentData)`;
            this.consoleDebug(debugMsg);
            return this.$q((resolve, reject) => {
                if (paymentData.PaymentStyle === this.cvServiceStrings.checkout.paymentMethods.quoteMe) {
                    // This should have been handled in the quoteMe service file (see the payment method step)
                    // resolve(this.cvSubmitQuoteService.finalizeFromPurchase(cartType, this.steps[lookupKey.toString()]));
                    reject("Handled elsewhere");
                    return;
                }
                const cart = this.cvCartService.accessCart(lookupKey);
                if (!cart) {
                    this.consoleDebug(`${debugMsg} no cart detected, failing`);
                    reject("No cart detected, failing");
                    return;
                }
                const billingStep = <purchasing.steps.billing.PurchaseStepBilling>_.find(
                    this.steps[lookupKey.toString()],
                    x => x.name === this.cvServiceStrings.checkout.steps.billing);
                if (!billingStep) {
                    this.consoleDebug(`${debugMsg} no billing step detected`);
                }
                const splitShippingStep = <purchasing.steps.splitShipping.PurchaseStepSplitShipping>_.find(
                    this.steps[lookupKey.toString()],
                    x => x.name === this.cvServiceStrings.checkout.steps.splitShipping);
                if (!splitShippingStep) {
                    this.consoleDebug(`${debugMsg} no split shipping step detected`);
                }
                const paymentStep = <purchasing.steps.payment.PurchaseStepPayment>_.find(
                    this.steps[lookupKey.toString()],
                    x => x.name === this.cvServiceStrings.checkout.steps.payment);
                if (!paymentStep) {
                    this.consoleDebug(`${debugMsg} no payment step detected`);
                }
                const checkoutDTO: api.CheckoutModel = {
                    WithCartInfo: <api.CheckoutWithCartInfo>{
                        CartID: cart.ID,
                    },
                    WithUserInfo: <api.CheckoutWithUserInfo>{
                        // UserID: number,
                        IsNewAccount: !this.cvAuthenticationService.isAuthenticated(),
                        // Username: string,
                        // Password: string,
                        // ExternalUserID: string
                    },
                    SpecialInstructions: splitShippingStep && splitShippingStep.specialInstructions || null,
                    Billing: billingStep && billingStep.currentAccountContact.Slave,
                    PaymentStyle: paymentStep && paymentStep.paymentMethod[lookupKey.toString()],
                    IsPartialPayment: false,
                    CategoryIDs: this.categoryIDs[lookupKey.toString()],
                    FileNames: this.fileNames[lookupKey.toString()],
                    SerializableAttributes: this.customAttributes[lookupKey.toString()],
                    /* All of these are set by the paymentData merge below "as needed"
                    WithTaxes: <api.CheckoutWithTaxes>{
                        VatID: string,
                        TaxExemptionNumber: string
                    },
                    PayByWalletEntry: <api.CheckoutPayByWalletEntry>{
                        WalletID: number,
                        WalletToken: string,
                        WalletCVV: string
                    },
                    PayByCreditCard: <api.CheckoutPayByCreditCard>{
                        CardType: string,
                        CardReferenceName: string,
                        CardHolderName: string,
                        CardNumber: string,
                        CVV: string,
                        ExpirationMonth: number,
                        ExpirationYear: number
                    },
                    PayByECheck: <api.CheckoutPayByECheck>{
                        AccountNumber: string,
                        RoutingNumber: string,
                        BankName: string
                    },
                    PayByBillMeLater: <api.CheckoutPayByBillMeLater>{
                        CustomerPurchaseOrderNumber: string
                    },
                    PayByPayPal: <api.CheckoutPayByPayPal>{
                        CancelUrl: string,
                        ReturnUrl: string,
                        PayerID: string,
                        PayPalToken: string
                    },
                    PayByPayoneer: <api.CheckoutPayByPayoneer>{
                        PayoneerAccountID: number,
                        PayoneerCustomerID: number
                    },
                    */
                    // ReferringStoreID: number, // Set by the server
                    // AffiliateAccountKey: string, // Set by the server
                    // SalesOrderID: number, // Set by the server
                    // CurrencyKey: string, // Set by the server
                    // Amount: number, // Use in partial payment scenario, unsupported at this time
                };
                // DNN Integration
                if ($("input[id$=hdnCurrentExternalUserID]") && $("input[id$=hdnCurrentExternalUserID]").val()) {
                    if (!checkoutDTO.WithUserInfo) {
                        checkoutDTO.WithUserInfo = <api.CheckoutWithUserInfo>{ };
                    }
                    checkoutDTO.WithUserInfo.ExternalUserID = $("input[id$=hdnCurrentExternalUserID]").val();
                }
                this.consoleDebug(`${debugMsg} checkoutDTO:`);
                this.consoleDebug(checkoutDTO);
                if (paymentData) {
                    angular.merge(checkoutDTO, paymentData);
                    this.consoleDebug(`${debugMsg} checkoutDTO with payment data merged:`);
                    this.consoleDebug(checkoutDTO);
                }
                this.cvAuthenticationService.preAuth().finally(() => {
                    if (this.cvAuthenticationService.isAuthenticated()) {
                        this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                            if (!checkoutDTO.WithUserInfo) {
                                checkoutDTO.WithUserInfo = <api.CheckoutWithUserInfo>{ };
                            }
                            checkoutDTO.WithUserInfo.IsNewAccount = false;
                            checkoutDTO.WithUserInfo.UserID = user.userID;
                            checkoutDTO.WithUserInfo.UserName = user.username;
                            this.consoleDebug(`${debugMsg} checkoutDTO with user data merged:`);
                            this.consoleDebug(checkoutDTO);
                            resolve(this.finalizeInner(lookupKey, checkoutDTO));
                        });
                        return;
                    }
                    this.consoleDebug(`${debugMsg} checkoutDTO without user data merged:`);
                    this.consoleDebug(checkoutDTO);
                    resolve(this.finalizeInner(lookupKey, checkoutDTO));
                });
            });
        }
        private finalizeInner(lookupKey: api.CartByIDLookupKey, checkoutDTO: api.CheckoutModel): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseService.finalizeInner(lookupKey: "${lookupKey.toString()}", paymentData, checkoutDTO)`);
            return this.$q((resolve, reject) => {
                this.consoleDebug(`PurchaseService.finalizeInner(lookupKey: "${lookupKey.toString()}", paymentData, checkoutDTO) Not Yet Implemented`);
                // TODO: PayPal specific processing that will redirect to their express checkout page
                const cart = this.cvCartService.accessCart(lookupKey);
                let promise: (routeParams: api.ProcessSpecificCartToTargetOrdersDto) => ng.IHttpPromise<api.CheckoutResult> = this.cvApi.providers.ProcessSpecificCartToTargetOrders;
                promise(checkoutDTO).then(r => {
                    if (!r
                        || !r.data
                        || !r.data.Succeeded
                        || r.data.ErrorMessage && r.data.ErrorMessage.toLowerCase().indexOf("email") === -1
                        || r.data.PaymentTransactionID && r.data.PaymentTransactionID.indexOf("ERROR") !== -1)
                    {
                        const message = r.data.ErrorMessage
                            || r.data.PaymentTransactionID
                            || "An unknown error occured.";
                        const error = r.data;
                        this.$uibModal.open({
                            templateUrl: this.$filter("corsLink")("/framework/store/checkout/errorMessage.html", "ui"),
                            controller: ($scope: ng.IScope) => {
                                $scope.error = error;
                                $scope.message = message;
                            },
                            resolve: {
                                message: () => message,
                                result: () => error
                            }
                        });
                        if (!r.data.Succeeded) {
                            reject(message);
                            return;
                        }
                    }
                    this.checkoutResult = r.data;
                    this.confirmationNumber = r.data.PaymentTransactionID
                        || (r.data.PaymentTransactionIDs ? r.data.PaymentTransactionIDs.join(", ") : "");
                    this.isOrderComplete = true;
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updated,
                        lookupKey);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.orders.complete,
                        r.data.OrderID ? r.data.OrderID : r.data.OrderIDs[0],
                        lookupKey,
                        this.steps[lookupKey.toString()]);
                    resolve(true);
                }).catch(reject);
            });
        }
        /*
        clearAnalysis(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                const cart = this.cvCartService.accessCart(lookupKey);
                if (!cart) {
                    this.consoleDebug("No cart detected, quitting with success");
                    resolve(true);
                    return;
                }
                const checkoutDTO: api.CheckoutModel = {
                    WithCartInfo: <api.CheckoutWithCartInfo>{
                        // CartID: cart.ID, // Don't assign this here
                        CartTypeName: cart.TypeName || cart.Type.Name,
                        CartSessionID: cart["SessionID"]
                    }
                };
                this.cvApi.providers.ClearSpecificCartToTargetCartsAnalysis(checkoutDTO).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        reject(r);
                        return;
                    }
                    resolve(true);
                }).catch(reject);
            });
        };
        */
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly $filter: ng.IFilterService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                private readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvCartService: services.ICartService,
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvWalletService: services.IWalletService) {
            // NOTE: The service isn't going to call load, the directive is when it needs it
            this.consoleDebug(`PurchaseService.ctor()`);
        }
    }
}
