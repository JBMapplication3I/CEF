/**
 * @file framework/admin/controls/sales/modals/payInvoiceModalBase.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Pay Invoice Modal Base for CSRs in CEF Admin (inherited by the separate modals)
 */
module cef.admin.controls.sales.modals {
    export abstract class PayInvoiceModalBase extends core.TemplatedControllerBase {
        // Properties
        forms: { edit: ng.IFormController };
        allowWallet = this.cefConfig.featureSet.payments.wallet.enabled;
        fee: number;
        wallet: Array<api.WalletModel> = [];
        paymentMethod: string = this.cvServiceStrings.checkout.paymentMethods.creditCard;
        selectedCard: api.WalletModel;
        selectedEcheck: api.WalletModel;
        paymentData = {
            PONumber: null,
            ReferenceName: null,
            WalletCardID: 0,
            CardType: null,
            CardNumber: null,
            CVV: null,
            ExpirationMonth: null,
            ExpirationYear: null,
            CardholderName: null,
            amount: null,
            Zip: null,
            // ExpirationMonth: new Date().getMonth() + 1,
            // ExpirationYear: new Date().getFullYear(),
            cardHolderName: null,
            paymentMethod: null,
            BankReferenceNo: null,
            BankDepositDate: null,
            PeriodToPost: null,
            CheckNumber: null,
            lockBoxNo: null
        };
        book: api.AccountContactModel[] = [];
        currentAccountContact: api.AccountContactModel = null;
        eCheckPaymentData = {
            RoutingNumber: null,
            AccountNumber: null,
            AccountHolder: null,
            CardType: null,
            Amount: null,
            BankName: null
        };
        sumTotal: number;
        firstLoad = true;
        get expirationMonths() { return this.cvWalletService.expirationMonths };
        get expirationYears() { return this.cvWalletService.expirationYears };
        paymentMethods: string[];
        processCreditCard = "Process Credit Card";
        applyWireTransfer = "Apply Wire Transfer";
        applyACH = "Apply ACH";
        applyCheckLockBox = "Apply Check (LockBox)";
        applyCheckCheckPoint = "Apply Check (CheckPoint)";
        checkByMailKey = "Check by Mail";
        wireTransferKey = "Wire Transfer";
        onlinePaymentRecordKey = "Online Payment Record";
        attributes = new api.SerializableAttributesDictionary();
        // Functions
        protected abstract onOpen(): void;
        protected abstract doPayment(): void;
        protected abstract updateAmount(): void;
        protected calculateUpliftFee(amount: number): { fee: number, total: number } {
            let amt = amount;
            let fee = 0;
            // TODO: Set up other payment types in this form
            if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.echeck) {
                let feeP = fee;
                let amtP = amt;
                let feeA = fee;
                let amtA = amt;
                if (this.cefConfig.featureSet.payments.methods.eCheck.uplifts.percent) {
                    const thisFee = amt * (0 + this.cefConfig.featureSet.payments.methods.eCheck.uplifts.percent);
                    feeP = feeP + thisFee;
                    amtP = amtP + thisFee;
                }
                if (this.cefConfig.featureSet.payments.methods.eCheck.uplifts.amount) {
                    const thisFee = this.cefConfig.featureSet.payments.methods.eCheck.uplifts.amount;
                    feeA = feeA + thisFee;
                    amtA = amtA + thisFee;
                }
                if (this.cefConfig.featureSet.payments.methods.eCheck.uplifts.useGreater) {
                    if (feeP > feeA) {
                        fee = feeP;
                        amt = amtP;
                    } else {
                        fee = feeA;
                        amt = amtA;
                    }
                } else {
                    fee = feeP + feeA;
                    amt = amt + fee;
                }
            } else if (this.paymentMethod == this.cvServiceStrings.checkout.paymentMethods.creditCard) {
                let feeP = fee;
                let amtP = amt;
                let feeA = fee;
                let amtA = amt;
                if (this.cefConfig.featureSet.payments.methods.creditCard.uplifts.percent) {
                    const thisFee = amt * (0 + this.cefConfig.featureSet.payments.methods.creditCard.uplifts.percent);
                    feeP = feeP + thisFee;
                    amtP = amtP + thisFee;
                }
                if (this.cefConfig.featureSet.payments.methods.creditCard.uplifts.amount) {
                    const thisFee = this.cefConfig.featureSet.payments.methods.creditCard.uplifts.amount;
                    feeA = feeA + thisFee;
                    amtA = amtA + thisFee;
                }
                if (this.cefConfig.featureSet.payments.methods.eCheck.uplifts.useGreater) {
                    if (feeP > feeA) {
                        fee = feeP;
                        amt = amt + amtP;
                    } else {
                        fee = feeA;
                        amt = amtA;
                    }
                } else {
                    fee = feeP + feeA;
                    amt = amt + fee;
                }
            }
            return { fee: fee, total: amt };
        }
        protected updatePaymentMethod(value: string): void {
            this.paymentMethod = value;
            this.updateAmount();
        }
        protected selectedCardIsFromWallet(): boolean {
            if (this.selectedCard == null) {
                return false;
            }
            return _.some(this.wallet, value => value.ID === this.selectedCard.ID);
        }
        protected isProcessCreditCard(): boolean {
            if (this.paymentData.paymentMethod == this.processCreditCard) {
                return true;
            }
            return false;
        }
        protected isApplyWireTransferOrAch(): boolean {
            if (this.paymentData.paymentMethod == this.applyWireTransfer
                || this.paymentData.paymentMethod == this.applyACH) {
                return true;
            }
            return false;
        }
        protected isApplyCheck(): boolean {
            if (this.paymentData.paymentMethod == this.applyCheckLockBox
                || this.paymentData.paymentMethod == this.applyCheckCheckPoint) {
                return true;
            }
            return false;
        }
        protected isLockBox(): boolean {
            if (this.paymentData.paymentMethod == this.applyCheckLockBox) {
                return true;
            }
            return false;
        }
        protected genPaymentMethods(): string[] {
            const out: string[] = [];
            out.push(this.processCreditCard);
            // out.push(this.applyWireTransfer);
            // out.push(this.applyACH);
            // out.push(this.applyCheckLockBox);
            // out.push(this.applyCheckCheckPoint);
            this.paymentData.paymentMethod = this.processCreditCard;
            return out;
        }
        // Wallet Handling
        protected loadWallet(): ng.IPromise<api.WalletModel[]> {
            return this.$q((resolve, reject) => {
                this.cvWalletService.getWallet(this.userID).then(wallet => {
                    this.wallet = wallet;
                    if (this.firstLoad && this.wallet && this.wallet.length > 0) {
                        this.firstLoad = false;
                        // Select their default card if they have one
                        let found = _.find(this.wallet, x => x.IsDefault);
                        if (!found) {
                            // Select first entry automatically
                            found = this.wallet[0];
                        }
                        if (found) {
                            if (this.isCard(found)) {
                                this.selectedCard = found;
                                this.selectedEcheck = null;
                            } else {
                                this.selectedEcheck = found;
                                this.selectedCard = null;
                            }
                        }
                    }
                    resolve(this.wallet);
                }).catch(reject);
            });
        }
        addWalletEntry(): void {
            this.cvWalletModalFactory(
                this.$translate("ui.admin.common.wallet.AddANewEntry"),
                this.$translate("ui.admin.common.wallet.AddEntry"),
                this.userID,
                null,
                "NewEntry",
                null)
            .then(newE => {
                this.setRunning();
                (newE.ID > 0
                    ? this.cvWalletService.updateEntry(this.userID, newE)
                    : this.cvWalletService.addEntry(this.userID, newE, true)
                ).then(result => {
                    if (!result) {
                        this.finishRunning(true);
                        return;
                    }
                    this.consoleDebug(`PayInvoiceModalBase.${this.cvServiceStrings.events.wallet.editSave} broadcast`);
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.editSave);
                    let defaultEntryID = this.cvWalletService.getDefaultEntryID(this.userID);
                    this.cvWalletService.getEntry(this.userID, { id: defaultEntryID }).then((entry) => {
                        this.selectedCard = entry;
                        this.updatePaymentDataWithCard(this.selectedCard);
                    }).then(() => {
                        this.loadWallet();
                        this.finishRunning()
                    })
                    .catch(reason => this.finishRunning(true, reason));
                }).catch(reason => this.finishRunning(true, reason));
            });
        }
        protected updatePaymentDataWithCard(card: api.WalletModel): void {
            this.paymentData.WalletCardID = card ? card.ID : null;
        }
        // Address Book Handling
        protected readAddressBook(newID: number = null): ng.IPromise<void> {
            const debugMsg = `PayInvoiceModalController.readAddressBook()`;
            this.consoleDebug(debugMsg);
            return this.$q((resolve, reject) => {
                let tempID: number = newID;
                if (!newID && this.currentAccountContact) {
                    // Copy the data in memory so we don't lose it
                    tempID = this.currentAccountContact.ID;
                    this.currentAccountContact = angular.fromJson(angular.toJson(this.currentAccountContact));
                }
                this.cvAuthenticationService.preAuth().finally(() => {
                    if (!this.cvAuthenticationService.isAuthenticated()) {
                        this.book = [];
                        resolve();
                        return;
                    }
                    this.cvAddressBookService.getBook(this.accountID).then(book => {
                        this.book = book;
                        if (tempID) {
                            const found = _.find(this.book, e => e.ID === tempID);
                            if (found) {
                                this.currentAccountContact = found;
                            } else {
                                const billingFound = _.find(this.book, e => e.IsBilling);
                                if (billingFound) {
                                    this.currentAccountContact = billingFound;
                                }
                            }
                            resolve();
                            return;
                        }
                        this.cvAddressBookService.refreshContactChecks(this.accountID, false).finally(() => {
                            this.consoleDebug(`${debugMsg} refreshContactChecks finished`);
                            if (!this.cvAddressBookService.defaultBillingID) {
                                this.consoleDebug(`${debugMsg} no default billing available to assign`);
                                resolve();
                                return;
                            }
                            this.consoleDebug(`${debugMsg} assigning default billing`);
                            this.currentAccountContact = this.cvAddressBookService.defaultBilling[this.accountID];
                            resolve();
                        });
                    }).catch(reject);
                });
            });
        }
        protected addAddress(): void {
            this.cvAddressModalFactory(
                this.$translate("ui.admin.addressBook.addressModal.AddANewAddress"), // title
                this.$translate("ui.admin.addressBook.addressModal.AddAddress"), // button
                "PaySingleInvoiceModal", // id suffix
                true, // is billing
                null, // existing
                this.accountID,
                false // restricted shipping
            ).then(() => this.makePrimaryLatest());
        }
        protected contactIsFromBook(): boolean {
            return this.currentAccountContact
                && this.currentAccountContact.ID > 0;
        }
        protected get usePhonePrefixLookups(): boolean {
            return this.cefConfig.usePhonePrefixLookups.enabled;
        }
        protected makePrimaryLatest(): void {
            this.readAddressBook().then(() => {
                this.readAddressBook(this.book[this.book.length - 1].ID)
            });
        }
        private isCard(entry: api.WalletModel): boolean {
            if (!entry) {
                return true;
            }
            return entry.CardType !== "Checking"
                && entry.CardType !== "Savings";
        }
        // Button Actions
        ok(): void {
            this.doPayment();
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
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
                protected readonly cvAuthenticationService: admin.services.IAuthenticationService,
                protected readonly cvAddressBookService: admin.services.IAddressBookService,
                protected readonly cvAddressModalFactory: admin.modals.IAddressModalFactory,
                protected readonly cvWalletService: admin.services.IWalletService,
                protected readonly cvWalletModalFactory: admin.modals.IWalletModalFactory,
                protected $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly accountID: number,
                protected readonly userID: number) {
            super(cefConfig);
            this.onOpen();
        }
    }
}
