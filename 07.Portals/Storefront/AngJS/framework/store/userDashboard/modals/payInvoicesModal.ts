/**
 * @file framework/store/userDashboard/modals/payInvoicesModal.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Pay Multiple Invoices Modal for Users in User Dashboard
 */
module cef.store.userDashboard.modals {
    export class PayMultipleInvoicesModalController extends PayInvoiceModalBase {
        // Properties
        unpaidInvoices: Array<api.SalesInvoiceModel> = [];
        balanceDue: number = 0;
        manualAmount: number = 0;
        isSubmitting: boolean;
        // Functions
        private loadUnpaidInvoices(): ng.IPromise<api.SalesInvoiceModel[]> {
            return this.cvApi.providers.GetCurrentAccountSalesInvoices().then(r => {
                const invoices = r.data.Results.filter(x => x.BalanceDue > 0);
                invoices.forEach(i => i["amount"] = i.BalanceDue);
                this.unpaidInvoices = invoices;
                this.updateAmount();
                return this.unpaidInvoices;
            });
        }
        protected updateAmount(): void {
            this.unpaidInvoices.forEach(i => i["amount"] = i["amount"] ?? 0);
            this.paymentData.amount = _.sumBy(this.unpaidInvoices, i => i["amount"]);
            this.eCheckPaymentData.Amount = _.sumBy(this.unpaidInvoices, i => i["amount"]);
            this.balanceDue = _.sumBy(this.unpaidInvoices, i => i.BalanceDue);
            this.manualAmount = this.paymentData.amount;
            let invoiceIDs = this.unpaidInvoices.map(x => x.ID);
            if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.creditCard) {
                const newAmounts = this.calculateUpliftFee(
                    this.manualAmount || this.paymentData.amount || this.balanceDue);
                this.fee = newAmounts.fee;
                this.sumTotal = newAmounts.total;
                this.calcSurcharge(invoiceIDs);
                return;
            }
            if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.echeck) {
                const newAmounts = this.calculateUpliftFee(
                    this.manualAmount || this.eCheckPaymentData.Amount || this.balanceDue);
                this.fee = newAmounts.fee;
                this.sumTotal = newAmounts.total;
                this.calcSurcharge(invoiceIDs);
                return;
            }
            this.sumTotal = this.manualAmount + this.fee;
            this.fee = 0;
            this.calcSurcharge(invoiceIDs);
        }
        protected doPayment(): void {
            this.setRunning();
            this.isSubmitting = true;
            let dto = <api.PayMultipleInvoicesByAmountsDto>{};
            if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.creditCard) {
                dto = <api.PayMultipleInvoicesByAmountsDto>{
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
                        Amount: this.paymentData.amount,
                        CardType: this.paymentData.CardType,
                        TypeID: 1,
                        StatusID: 1,
                        Zip: this.paymentData.Zip,
                        WalletID: this.selectedCardIsFromWallet()
                            ? this.paymentData.WalletCardID
                            : null
                    },
                    Billing: this.currentAccountContact.Slave,
                    Amounts: { }
                };
            } else if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.echeck) {
                dto = <api.PayMultipleInvoicesByAmountsDto>{
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
                        BankName: this.eCheckPaymentData.BankName,
                        Amount: this.eCheckPaymentData.Amount,
                        TypeID: 1,
                        StatusID: 1,
                        Zip: this.paymentData.Zip,
                    },
                    Billing: this.currentAccountContact.Slave,
                    Amounts: { }
                };
            }
            this.unpaidInvoices.filter(x => x["amount"] > 0).forEach(x => dto.Amounts[x.ID] = x["amount"]);
            if (this.paymentMethod === this.cvServiceStrings.checkout.paymentMethods.creditCard
                || this.paymentMethod === this.cvServiceStrings.checkout.paymentMethods.echeck) {
                const newAmounts = this.calculateUpliftFee(
                    Object.keys(dto.Amounts).reduce((sum, key) => sum + dto.Amounts[key] || 0, 0));
                if (newAmounts.total !== dto.Payment.Amount) {
                    dto.Payment.Amount = newAmounts.total;
                }
            }
            this.cvApi.providers.PayMultipleInvoicesByAmounts(dto).then(r => {
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
            }).catch(reason => {
                this.isSubmitting = false;
                this.finishRunning(true, reason)
            });
        }
        // Events
        protected onOpen(): void {
            this.setRunning();
            this.$q.all([
                this.loadWallet(),
                this.genExpirationMonths(),
                this.genExpirationYears(),
                this.readAddressBook(),
                this.loadUnpaidInvoices()
            ]).then(() => this.finishRunning())
            .catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $q: ng.IQService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig, // Used by UI
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvAddressBookService: services.IAddressBookService,
                protected readonly cvAddressModalFactory: store.modals.IAddressModalFactory,
                protected readonly cvWalletService: services.IWalletService,
                protected readonly cvWalletModalFactory: store.modals.IWalletModalFactory,
                protected $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance) {
            super($scope, $rootScope, $q, $filter, $translate, $uibModal, cefConfig,
                cvServiceStrings, cvApi, cvAuthenticationService, cvAddressBookService,
                cvAddressModalFactory, cvWalletService, cvWalletModalFactory, $uibModalInstance);
            this.consoleDebug(`PayMultipleInvoicesModalController.ctor()`);
            const unbind1 = $scope.$on(cvServiceStrings.events.addressBook.loaded,
                () => this.readAddressBook());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }
}
