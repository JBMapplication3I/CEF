/**
 * @file framework/admin/controls/sales/modals/payInvoiceModal.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Pay Invoice Modal for CSRs in CEF Admin
 */
module cef.admin.controls.sales.modals {
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
                        WalletID: this.selectedCard.ID || this.paymentData.WalletCardID,
                        TypeID: 1,
                        StatusID: 1,
                        Zip: this.paymentData.Zip,
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
                        WalletID: this.paymentData.WalletCardID,
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
            /*
            if (this.paymentData.paymentMethod == this.processCreditCard) {
                dto.Payment.CreatedDate = new Date();
                dto.Payment.CardNumber = this.paymentData.CardNumber;
                dto.Payment.CVV = this.paymentData.CVV;
                dto.Payment.PurchaseOrderNumber = this.paymentData.PONumber;
                dto.Payment.ExpirationMonth = this.paymentData.ExpirationMonth;
                dto.Payment.ExpirationYear = this.paymentData.ExpirationYear;
                dto.Payment.TypeID = 1;
            } else if (this.paymentData.paymentMethod == this.applyWireTransfer
                || this.paymentData.paymentMethod == this.applyACH) {
                dto.Payment.CreatedDate = this.formatDepositDateTime();
                dto.Payment.Amount = this.balanceDue;
                dto.Payment.TypeID = 5;
                dto.Payment.StatusID = 2;
                dto.Payment.BankName = this.paymentData.BankReferenceNo;
                dto.Payment.SerializableAttributes["Period To Post"] = <api.SerializableAttributeObject>{
                    ID: 0,
                    Key: "Period To Post",
                    Value: this.paymentData.PeriodToPost
                };
                if (this.paymentData.paymentMethod == this.applyWireTransfer) {
                    dto.Payment.PaymentMethodID = 4;
                    dto.Payment.PaymentMethodKey = this.wireTransferKey;
                } else {
                    dto.Payment.PaymentMethodID = 5;
                    dto.Payment.PaymentMethodKey = this.onlinePaymentRecordKey;
                }
            } else if (this.paymentData.paymentMethod == this.applyCheckLockBox
                    || this.paymentData.paymentMethod == this.applyCheckCheckPoint) {
                dto.Payment.CreatedDate = this.formatDepositDateTime();
                dto.Payment.Amount = this.balanceDue;
                dto.Payment.TypeID = 5;
                dto.Payment.StatusID = 2;
                dto.Payment.PaymentMethodID = 2;
                dto.Payment.PaymentMethodKey = this.checkByMailKey;
                dto.Payment.SerializableAttributes["Check Number"] = <api.SerializableAttributeObject>{
                    ID: 0,
                    Key: "Check Number",
                    Value: this.paymentData.CheckNumber
                };
                dto.Payment.SerializableAttributes["Period To Post"] = <api.SerializableAttributeObject>{
                    ID: 0,
                    Key: "Period To Post",
                    Value: this.paymentData.PeriodToPost
                };
                dto.Payment.SerializableAttributes["Total Payment Amount"] = <api.SerializableAttributeObject>{
                    ID: 0,
                    Key: "Total Payment Amount",
                    Value: this.paymentData.amount
                };
                if (this.paymentData.paymentMethod == this.applyCheckLockBox) {
                    dto.Payment.SerializableAttributes["LockBox Number"] = <api.SerializableAttributeObject>{
                        ID: 0,
                        Key: "LockBox Number",
                        Value: this.paymentData.lockBoxNo
                    };
                }
            }
            */
            this.cvApi.providers.PaySingleInvoiceByID(dto).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    const message = r.data.Messages[0] || "An unknown error occured.";
                    const error = r.data;
                    this.$uibModal.open({
                        templateUrl: this.$filter("corsLink")("/framework/admin/controls/sales/modals/errorMessage.html", "ui"),
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
            }).catch(reason => this.finishRunning(true, reason));
        }
        protected updateAmount(): void {
            if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.creditCard) {
                const newAmounts = this.calculateUpliftFee(
                    this.manualAmount || this.paymentData.amount || this.balanceDue);
                this.fee = newAmounts.fee;
                this.sumTotal = newAmounts.total;
                // this.balanceDue = this.sumTotal;
                return;
            }
            if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.echeck) {
                const newAmounts = this.calculateUpliftFee(
                    this.manualAmount || this.eCheckPaymentData.Amount || this.balanceDue);
                this.fee = newAmounts.fee;
                this.sumTotal = newAmounts.total;
                // this.balanceDue = this.sumTotal;
                return;
            }
            this.sumTotal = this.sumTotal - this.fee;
            // this.balanceDue = this.sumTotal;
            this.fee = 0;
        }
        // Events
        protected onOpen(): void {
            this.setRunning();
            this.$q.all([
                this.loadWallet(),
                this.readAddressBook()
            ]).then(() => {
                this.manualAmount = this.cefConfig.featureSet.salesInvoices.canPayViaCSR.single.partial
                    ? this.paymentData.amount || this.balanceDue
                    : null;
                const newAmounts = this.calculateUpliftFee(
                    this.manualAmount || this.paymentData.amount || this.balanceDue);
                this.fee = newAmounts.fee;
                this.sumTotal = newAmounts.total;
                this.paymentMethods = this.genPaymentMethods();
                this.finishRunning();
            }).catch(reason => {
                console.error(reason);
                this.finishRunning(true, reason);
            });
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $q: ng.IQService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAuthenticationService: admin.services.IAuthenticationService,
                protected readonly cvAddressBookService: admin.services.IAddressBookService,
                protected readonly cvAddressModalFactory: admin.modals.IAddressModalFactory,
                protected readonly cvWalletService: admin.services.IWalletService,
                protected readonly cvWalletModalFactory: admin.modals.IWalletModalFactory,
                protected readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly accountID: number,
                protected readonly userID: number,
                protected id: number,
                protected balanceDue: number) {
            super($scope, $rootScope, $q, $filter, $translate, $uibModal, cefConfig,
                cvServiceStrings, cvAuthenticationService, cvAddressBookService,
                cvAddressModalFactory, cvWalletService, cvWalletModalFactory, $uibModalInstance,
                accountID, userID);
            this.consoleDebug(`PayInvoiceModalController.ctor()`);
            const unbind1 = $scope.$on(cvServiceStrings.events.addressBook.loaded,
                () => this.readAddressBook());
            const unbind2 = $scope.$on(cvServiceStrings.events.wallet.editSave,
                () => this.loadWallet());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
    }
}
