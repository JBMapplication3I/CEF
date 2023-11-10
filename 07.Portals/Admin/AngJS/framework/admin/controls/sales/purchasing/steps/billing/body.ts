/**
 * @file framework/admin/purchasing/steps/billing/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.billing {
    // This is the controller for the directive
    class PurchaseStepBillingBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected lookupKey: api.CartByIDLookupKey;
        protected idSuffix: string;
        // Properties
        private get step(): PurchaseStepBilling {
            if (!this.lookupKey
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.lookupKey.toString()]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepBilling>
                _.find(this.cvPurchaseService.steps[this.lookupKey.toString()],
                    x => x.name === this.cvServiceStrings.checkout.steps.billing);
        }
        protected get book(): api.AccountContactModel[] {
            if (!this.step) {
                return undefined;
            }
            return this.step.book;
        }
        protected set book(newValue: api.AccountContactModel[]) {
            this.consoleDebug(`PurchaseStepBillingBodyController.addressTitle.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepBillingBodyController.addressTitle.set 2`);
                this.step.book = newValue;
            }
        }
        protected get currentAccountContact(): api.AccountContactModel {
            if (!this.step) {
                return undefined;
            }
            return this.step.currentAccountContact;
        }
        protected set currentAccountContact(newValue: api.AccountContactModel) {
            this.consoleDebug(`PurchaseStepBillingBodyController.currentAccountContact.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepBillingBodyController.currentAccountContact.set 2`);
                this.step.currentAccountContact = newValue;
            }
        }
        protected get addressTitle(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.addressTitle;
        }
        protected set addressTitle(newValue: string) {
            this.consoleDebug(`PurchaseStepBillingBodyController.addressTitle.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepBillingBodyController.addressTitle.set 2`);
                this.step.addressTitle = newValue;
            }
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`PurchaseStepBillingBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepBillingBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        protected get usePhonePrefixLookups(): boolean {
            return this.cefConfig.featureSet.contacts.phonePrefixLookups.enabled;
        }
        protected get restrictedShipping(): boolean {
            return this.cefConfig.featureSet.shipping.restrictions.enabled;
        }
        protected get showMakeThisMyDefault(): boolean {
            return this.cefConfig.purchase.sections.purchaseStepBilling["showMakeThisMyDefault"] || false;
        }
        protected get smtmd(): boolean {
            return this.showMakeThisMyDefault
                && this.cvAuthenticationService.isAuthenticated()
                && this.currentAccountContact.ID !== this.cvAddressBookService.defaultBillingID[this.lookupKey.AID][this.cvPurchaseService.activeStep[this.lookupKey.toString()]]
                && this.currentAccountContact.ID !== this.cvAddressBookService.defaultShippingID[this.lookupKey.AID][this.cvPurchaseService.activeStep[this.lookupKey.toString()]];
        }
        protected get makeThisNewDefaultBilling(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.makeThisNewDefaultBilling;
        }
        protected set makeThisNewDefaultBilling(newValue: boolean) {
            this.consoleDebug(`PurchaseStepBillingBodyController.makeThisNewDefaultBilling.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepBillingBodyController.makeThisNewDefaultBilling.set 2`);
                this.step.makeThisNewDefaultBilling = newValue;
            }
        }
        // Functions
        private readAddressBook(newID: number = null) {
            const debugMsg = `PurchaseStepBillingBodyController.readAddressBook()`;
            this.consoleDebug(debugMsg);
            let tempID: number = newID;
            if (!newID && this.currentAccountContact) {
                // Copy the data in memory so we don't lose it
                tempID = this.currentAccountContact.ID;
                this.currentAccountContact = angular.fromJson(angular.toJson(this.currentAccountContact));
            }
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    this.book = [];
                    this.onChange();
                    return;
                }
                this.cvAddressBookService.getBook(this.lookupKey.AID).then(book => {
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
                        // Delay so the UI can catch up first and then we can run a validate
                        this.$timeout(() => this.onChange(), 500);
                        return;
                    }
                    this.cvAddressBookService.refreshContactChecks(this.lookupKey.AID, false).finally(() => {
                        this.consoleDebug(`${debugMsg} refreshContactChecks finished`);
                        if (!this.cvAddressBookService.defaultBillingID[this.lookupKey.AID]) {
                            this.consoleDebug(`${debugMsg} no default billing available to assign`);
                            this.onChange();
                            return;
                        }
                        this.consoleDebug(`${debugMsg} assigning default billing`);
                        this.currentAccountContact = this.cvAddressBookService.defaultBilling[this.lookupKey.AID];
                        this.step.validate(this.cvCartService.accessCart(this.lookupKey))
                            .finally(() => this.onChange());
                    });
                });
            });
        }
        protected add(): void {
            this.cvAddressModalFactory(
                this.$translate("ui.admin.controls.sales.salesOrderNewWizard.AddANewAddress"),
                this.$translate("ui.admin.checkout.splitShipping.addressModal.AddAddress"),
                "PurchasingBilling",
                true,
                null,
                this.lookupKey.AID,
                false
            ).then(newEntry => this.checkForPrimaryBilling(newEntry));
        }
        protected checkForPrimaryBilling(newEntry: api.AccountContactModel): void {
            if (this.book == null) {
                newEntry.IsBilling = true;
            } else if (!this.book.filter(x => x.IsBilling && x.Active).length) {
                newEntry.IsBilling = true;
            }
            this.cvAddressBookService.addEntry(this.lookupKey.AID, newEntry)
                .then(newID => this.readAddressBook(newID));
        }
        // Events
        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            const debugMsg = "PurchaseStepBillingBodyController.onChange()";
            this.consoleDebug(debugMsg);
            this.invalid = true;
            if (this.forms.billing.$invalid) {
                this.consoleDebug(`${debugMsg} failed! Form invalid`);
                this.consoleDebug(this.forms.billing.$error);
                this.invalid = true;
                return;
            }
            this.setRunning();
            this.step.validate(this.cvCartService.accessCart(this.lookupKey))
                .then(success => {
                    if (!success) {
                        this.consoleDebug(`${debugMsg} failed! validate returned false`);
                        this.invalid = true;
                        this.finishRunning(true, "An error has occurred");
                        return;
                    }
                    this.consoleDebug(`${debugMsg} success!`);
                    this.invalid = false;
                    this.finishRunning();
                }).catch(reason => {
                    this.consoleDebug(`${debugMsg} failed! Caught issue ${reason || "An error has occurred"}`);
                    this.invalid = true;
                    this.finishRunning(true, reason || "An error has occurred");
                });
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $timeout: ng.ITimeoutService,
                private readonly $translate: ng.translate.ITranslateService,
                readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvAddressModalFactory: modals.IAddressModalFactory,
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvCartService: services.ICartService,
                private readonly cvPurchaseService: services.IPurchaseService) {
            super(cefConfig);
            this.consoleDebug(`PurchaseStepBillingBodyController.ctor()`);
            const unbind1 = $scope.$on(cvServiceStrings.events.addressBook.loaded, () => this.readAddressBook());
            const unbind2 = $scope.$on(cvServiceStrings.events.auth.signIn, () => $timeout(() => this.readAddressBook(), 1000));
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
            // Do an initial load of data
            this.readAddressBook();
        }
    }

    adminApp.directive("cefPurchaseStepBillingBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=", idSuffix: "@" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/billing/body.html", "ui"),
        controller: PurchaseStepBillingBodyController,
        controllerAs: "psbbCtrl",
        bindToController: true
    }));
}
