/**
 * @file framework/store/userDashboard/modals/payInvoiceModalBase.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Pay Invoice Modal Base for Users in User Dashboard (inherited by the separate modals)
 */
module cef.store.userDashboard.modals {
    export abstract class PayInvoiceModalBase extends core.TemplatedControllerBase {
        // Properties
        forms: { edit: ng.IFormController };
        allowWallet = this.cefConfig.featureSet.payments.wallet.enabled;
        fee: number;
        wallet: Array<api.WalletModel> = [];
        paymentMethod: string = this.cvServiceStrings.checkout.paymentMethods.creditCard;
        selectedCard: api.WalletModel;
        selectedEcheck: api.WalletModel;
        expirationMonths: Array<{ Value: number, Label: string }>;
        expirationYears: Array<{ Value: number, Label: string }>;
        paymentData = {
            PONumber: null,
            ReferenceName: null,
            WalletCardID: null,
            CardType: null,
            CardNumber: null,
            CVV: null,
            ExpirationMonth: null,
            ExpirationYear: null,
            CardholderName: null,
            amount: null,
            // contact info
            Zip: null
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
        isSubmitting: boolean;
        surchargeAmount: number;
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
        // Wallet Handling
        protected loadWallet(): ng.IPromise<api.WalletModel[]> {
            return this.cvWalletService.getWallet().then(wallet => {
                this.wallet = wallet;
                if (!this.wallet.length) {
                    return this.wallet;
                }
                // Select their default card if they have one
                const found = _.find(this.wallet, x => x.IsDefault);
                if (found) {
                    this.updatePaymentDataWithCard(found);
                    if (this.isCard(found)) {
                        this.selectedCard = found;
                        this.selectedEcheck = null;
                    } else {
                        this.selectedEcheck = found;
                        this.selectedCard = null;
                    }
                    return this.wallet;
                }
                // Select added card if no default
                const newCard = this.wallet[this.wallet.length - 1];
                if (newCard) {
                    this.updatePaymentDataWithCard(newCard);
                    if (this.isCard(newCard)) {
                        this.selectedCard = newCard;
                        this.selectedEcheck = null;
                    } else {
                        this.selectedEcheck = this.selectedCard;
                        this.selectedCard = null;
                    }
                    return this.wallet;
                }
                return this.wallet;
            });
        }
        protected addWalletEntry(): void {
            this.cvWalletModalFactory(
                this.$translate("ui.storefront.userDashboard.wallet.AddANewEntry"),
                this.$translate("ui.storefront.userDashboard.wallet.AddEntry"),
                null,
                "NewEntry",
                null)
            .then(newE => {
                this.setRunning();
                this.wallet.push(newE);
                this.selectedCard = newE;
                (newE.ID > 0
                    ? this.cvWalletService.updateEntry(newE)
                    : this.cvWalletService.addEntry(newE)
                ).then(result => {
                    if (!result) {
                        this.finishRunning(true);
                        return;
                    }
                    this.finishRunning();
                }).catch(reason => this.finishRunning(true, reason));
            });
        }
        protected updatePaymentDataWithCard(card: api.WalletModel): void {
            this.paymentData.WalletCardID = card ? card.ID : null;
        }
        protected selectedCardIsFromWallet(): boolean {
            if (this.selectedCard == null) { return false; }
            return _.some(this.wallet, value => value.ID === this.selectedCard.ID);
        }
        protected genExpirationMonths(): ng.IPromise<void> {
            return this.$q(resolve => {
                (monthNames => {
                    this.$q.all(monthNames.map(month => this.$translate(`ui.storefront.common.months.${month}`))).then(months => {
                        this.expirationMonths = months.map((monthResponse: string, idx) => {
                            return { Value: (idx + 1), Label: `${idx + 1} - ${monthResponse}` };
                        });
                    }, () => {
                        console.warn("Failed to get month name translation values. Check the database.");
                        this.expirationMonths = monthNames.map((month, idx) => {
                            return { Value: (idx + 1), Label: `${idx + 1} - ${month}` };
                        });
                    }).finally(() => { this.paymentData.ExpirationMonth = new Date().getMonth() + 1; resolve(); });
                })(["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]);
            });
        }
        protected genExpirationYears(): ng.IPromise<void> {
            return this.$q(resolve => {
                const out: Array<{ Value: number, Label: string }> = [];
                const currentYear = new Date().getFullYear();
                for (let y = 0; y < 10; y++) {
                    const year = currentYear + y;
                    out.push({ Value: year, Label: year.toString().substr(2,2) });
                }
                this.expirationYears = out;
                this.paymentData.ExpirationYear = new Date().getFullYear();
                resolve();
            });
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
                    this.cvAddressBookService.getBook().then(book => {
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
                        this.cvAddressBookService.refreshContactChecks(false, "purchasing.steps.billing.body").finally(() => {
                            this.consoleDebug(`${debugMsg} refreshContactChecks finished`);
                            if (!this.cvAddressBookService.defaultBillingID) {
                                this.consoleDebug(`${debugMsg} no default billing available to assign`);
                                resolve();
                                return;
                            }
                            this.consoleDebug(`${debugMsg} assigning default billing`);
                            this.currentAccountContact = this.cvAddressBookService.defaultBilling;
                            resolve();
                        });
                    }).catch(reject);
                });
            });
        }
        protected addAddress(): void {
            this.cvAddressModalFactory(
                this.$translate("ui.storefront.userDashboard2.controls.addressEditor2.AddANewAddress"),
                this.$translate("ui.storefront.checkout.splitShipping.addressModal.AddAddress"),
                null,
                "PaySingleInvoiceModal",
                true,
                null
            ).then(newEntry => this.checkForPrimaryBilling(newEntry));
        }
        protected contactIsFromBook(): boolean {
            return this.currentAccountContact
                && this.currentAccountContact.ID > 0;
        }
        protected get usePhonePrefixLookups(): boolean {
            return this.cefConfig.featureSet.contacts.phonePrefixLookups.enabled;
        }
        protected checkForPrimaryBilling(newEntry: api.AccountContactModel): void {
            if (this.book == null) {
                newEntry.IsBilling = true;
            } else if (!this.book.filter(x => x.IsBilling && x.Active).length) {
                newEntry.IsBilling = true;
            }
            this.cvAddressBookService.addEntry(newEntry)
                .then(newID => this.readAddressBook(newID));
        }
        private isCard(entry: api.WalletModel): boolean {
            if (!entry) {
                return true;
            }
            return entry.CardType !== "Checking"
                && entry.CardType !== "Savings";
        }
        // Surcharge Handling
        protected calcSurcharge(ids: number | number[]): ng.IPromise<void> {
            this.setRunning();
            if (!this.selectedCard.SerializableAttributes
                || !this.selectedCard.SerializableAttributes["BIN"]?.Value) {
                this.finishRunning(true, "Cannot calculate surcharge, selected card has no BIN number assigned.");
                return this.$q.reject("Cannot calculate surcharge, selected card has no BIN number assigned.");
            }
            if (!this.currentAccountContact.SlaveID) {
                this.finishRunning(true, "Cannot calculate surcharge, current account contact not set.");
                return this.$q.reject("Cannot calculate surcharge, current account contact not set.");
            }
            let dto = <api.GetSurchargeForInvoicePaymentDto>{
                BIN: this.selectedCard.SerializableAttributes["BIN"].Value,
                BillingContactID: this.currentAccountContact.SlaveID,
                InvoiceIDs: Array.isArray(ids) ? new Set(ids) : new Set([ids]),
                TotalAmount: this.sumTotal
            };
            this.consoleDebug(`Sending ${dto}`);
            return this.cvApi.payments.GetSurchargeForInvoicePayment(dto).then(r => {
                if (!r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, "Cannot calculate surcharge, network error.");
                    return this.$q.reject("Cannot calculate surcharge, network error.");
                }
                this.finishRunning();
                return this.surchargeAmount = r.data.Result;
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Button Actions
        ok(): void {
            this.isSubmitting = true;
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
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvAddressBookService: services.IAddressBookService,
                protected readonly cvAddressModalFactory: store.modals.IAddressModalFactory,
                protected readonly cvWalletService: services.IWalletService,
                protected readonly cvWalletModalFactory: store.modals.IWalletModalFactory,
                protected $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance) {
            super(cefConfig);
            this.onOpen();
        }
    }
}
