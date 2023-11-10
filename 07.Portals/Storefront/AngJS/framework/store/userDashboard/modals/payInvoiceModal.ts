/**
 * @file framework/store/userDashboard/modals/payInvoiceModal.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Pay Invoice Modal for Users in User Dashboard
 */
module cef.store.userDashboard.modals {
    export class PayInvoiceModalController extends PayInvoiceModalBase {
        // Properties
        manualAmount: number;
        // Functions
        protected doPayment(): void {
            this.setRunning();
            let dto = <api.PaySingleInvoiceByIDDto>{ };
            if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.creditCard) {
                dto = <api.PaySingleInvoiceByIDDto>{
                    InvoiceID: this.id,
                    Payment: <api.PaymentModel>{
                        ID: 0,
                        Active: true,
                        CreatedDate: new Date(),
                        CustomKey: null,
                        CardNumber: this.paymentData.CardNumber,
                        CardholderName: this.paymentData.CardholderName,
                        CVV: this.paymentData.CVV,
                        PurchaseOrder: this.paymentData.PONumber,
                        ExpirationMonth: this.paymentData.ExpirationMonth,
                        ExpirationYear: this.paymentData.ExpirationYear,
                        Amount: this.manualAmount || this.paymentData.amount || this.balanceDue,
                        CardType: this.paymentData.CardType,
                        TypeID: 1,
                        StatusID: 1,
                        Zip: this.paymentData.Zip,
                        WalletID: this.selectedCardIsFromWallet()
                            ? this.paymentData.WalletCardID
                            : null
                    },
                    Billing: this.currentAccountContact.Slave
                };
            } else if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.echeck) {
                dto = <api.PaySingleInvoiceByIDDto>{
                    InvoiceID: this.id,
                    Payment: <api.PaymentModel>{
                        ID: 0,
                        Active: true,
                        CreatedDate: new Date(),
                        CustomKey: null,
                        RoutingNumber: this.eCheckPaymentData.RoutingNumber,
                        AccountNumber: this.eCheckPaymentData.AccountNumber,
                        AccountHolder: this.eCheckPaymentData.AccountHolder,
                        CardType: this.eCheckPaymentData.CardType,
                        CardHolderName: this.eCheckPaymentData.AccountHolder,
                        Amount: this.manualAmount || this.eCheckPaymentData.Amount || this.balanceDue,
                        BankName: this.eCheckPaymentData.BankName,
                        TypeID: 1,
                        StatusID: 1,
                        Zip: this.paymentData.Zip,
                    },
                    Billing: this.currentAccountContact.Slave
                };
            }

            if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.creditCard
                || this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.echeck) {
                // The back end will apply the fees, just send the raw payment amount to apply to the invoice
                // const newAmounts = this.calculateUpliftFee(dto.Payment.Amount);
                // if (newAmounts.total !== dto.Payment.Amount) {
                //     this.fee = newAmounts.fee;
                //     this.sumTotal = dto.Payment.Amount = newAmounts.total;
                // } else {
                    this.sumTotal = dto.Payment.Amount;
                    this.fee = 0;
                // }
            }
            this.cvApi.providers.PaySingleInvoiceByID(dto).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    const message = r.data.Messages[0] || "An unknown error occured.";
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
                    if (!r.data.ActionSucceeded) {
                        this.finishRunning(true, message);
                        return;
                    }
                }
                this.finishRunning();
                this.$uibModalInstance.close();
            }).catch( reason => {
                this.isSubmitting = false;
                this.finishRunning(true, reason);
            });
        }
        protected updateAmount(): void {
            if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.creditCard) {
                const newAmounts = this.calculateUpliftFee(
                    this.manualAmount || this.paymentData.amount || this.balanceDue);
                this.fee = newAmounts.fee;
                this.sumTotal = newAmounts.total;
                // this.balanceDue = this.sumTotal;
                this.calcSurcharge(this.id);
                return;
            }
            if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.echeck) {
                const newAmounts = this.calculateUpliftFee(
                    this.manualAmount || this.eCheckPaymentData.Amount || this.balanceDue);
                this.fee = newAmounts.fee;
                this.sumTotal = newAmounts.total;
                // this.balanceDue = this.sumTotal;
                this.calcSurcharge(this.id);
                return;
            }
            this.sumTotal = this.sumTotal - this.fee;
            // this.balanceDue = this.sumTotal;
            this.fee = 0;
            this.calcSurcharge(this.id);
        }
        // Events
        propagateAccountContactChange(): void {
            this.$timeout(() => {
                this.calcSurcharge(this.id);
            }, 250);
        }
        protected onOpen(): void {
            this.setRunning();
            this.$q.all([
                this.loadWallet(),
                this.genExpirationMonths(),
                this.genExpirationYears(),
                this.readAddressBook()
            ]).then(() => {
                this.manualAmount = this.cefConfig.featureSet.salesInvoices.canPayViaUserDashboard.single.partial
                    ? this.paymentData.amount || this.balanceDue
                    : null;
                const newAmounts = this.calculateUpliftFee(
                    this.manualAmount || this.paymentData.amount || this.balanceDue);
                this.fee = newAmounts.fee;
                this.sumTotal = newAmounts.total;
                this.finishRunning();
            })
            .then(() => this.calcSurcharge(this.id))
            .catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $timeout: ng.ITimeoutService,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $q: ng.IQService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvAddressBookService: services.IAddressBookService,
                protected readonly cvAddressModalFactory: store.modals.IAddressModalFactory,
                protected readonly cvWalletService: services.IWalletService,
                protected readonly cvWalletModalFactory: store.modals.IWalletModalFactory,
                protected readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected id: number,
                protected balanceDue: number) {
            super($scope, $rootScope, $q, $filter, $translate, $uibModal, cefConfig,
                cvServiceStrings, cvApi, cvAuthenticationService, cvAddressBookService,
                cvAddressModalFactory, cvWalletService, cvWalletModalFactory, $uibModalInstance);
            this.consoleDebug(`PayInvoiceModalController.ctor()`);
            const unbind1 = $scope.$on(cvServiceStrings.events.addressBook.loaded,
                () => this.readAddressBook());
            const unbind2 = $scope.$on(cvServiceStrings.events.wallet.loaded,
                () => this.loadWallet());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
    }
}
