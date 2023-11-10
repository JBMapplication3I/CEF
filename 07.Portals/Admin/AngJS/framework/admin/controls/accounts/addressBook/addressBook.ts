/**
 * @file framework/admin/controls/accounts/addressBook/accountAddressBook.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Admin Account Address Book With Editor Widget
 */
module cef.admin.controls.accounts.addressBook {
    class AddressBookController extends core.TemplatedControllerBase {
        // Properties
        accountId: number; // Bound by Scope
        usePhonePrefixLookups: boolean; // Bound by Scope
        typeId: number; // Bound by Scope
        restrictedShipping: boolean; // Bound by Scope
        get useTypeId(): number { return this.typeId || null; };
        editorOpen = false;
        idSuffix: number;
        get noBillingContactFound(): boolean {
            return this.cvAddressBookService.noBillingContactFound
                && this.cvAddressBookService.noBillingContactFound[this.accountId];
        }
        set noBillingContactFound(value: boolean) {
            this.cvAddressBookService.noBillingContactFound[this.accountId] = value;
        }
        get noPrimaryContactFound(): boolean {
            return this.cvAddressBookService.noPrimaryContactFound
                && this.cvAddressBookService.noPrimaryContactFound[this.accountId];
        }
        set noPrimaryContactFound(value: boolean) {
            this.cvAddressBookService.noPrimaryContactFound[this.accountId] = value;
        }
        get primaryAndBillingAreSameContact(): boolean {
            return this.cvAddressBookService.primaryAndBillingAreSameContact
                && this.cvAddressBookService.primaryAndBillingAreSameContact[this.accountId];
        }
        set primaryAndBillingAreSameContact(value: boolean) {
            this.cvAddressBookService.primaryAndBillingAreSameContact[this.accountId] = value;
        }
        get defaultBillingID(): number {
            return this.cvAddressBookService.defaultBillingID
                && this.cvAddressBookService.defaultBillingID[this.accountId];
        }
        set defaultBillingID(value: number) {
            this.cvAddressBookService.defaultBillingID[this.accountId] = value;
        }
        get defaultBilling(): api.AccountContactModel {
            return this.cvAddressBookService.defaultBilling
                && this.cvAddressBookService.defaultBilling[this.accountId];
        }
        set defaultBilling(value: api.AccountContactModel) {
            this.cvAddressBookService.defaultBilling[this.accountId] = value;
        }
        get defaultShippingID(): number {
            return this.cvAddressBookService.defaultShippingID
                && this.cvAddressBookService.defaultShippingID[this.accountId];
        }
        set defaultShippingID(value: number) {
            this.cvAddressBookService.defaultShippingID[this.accountId] = value;
        }
        get defaultShipping(): api.AccountContactModel {
            return this.cvAddressBookService.defaultShipping
                && this.cvAddressBookService.defaultShipping[this.accountId];
        }
        set defaultShipping(value: api.AccountContactModel) {
            this.cvAddressBookService.defaultShipping[this.accountId] = value;
        }
        get addressBook(): { [id: number]: api.AccountContactModel } {
            return this.cvAddressBookService.addressBookCache
                && this.cvAddressBookService.addressBookCache[this.accountId];
        }
        set addressBook(value: { [id: number]: api.AccountContactModel }) {
            this.cvAddressBookService.addressBookCache[this.accountId] = value;
        }
        ////addressBook: api.AccountContactModel[] = null;
        // Functions
        // NOTE: This function must remain as an arrow function
        typeIdFilter = (value: api.AccountContactModel): boolean => {
            if (!this.useTypeId) {
                return true;
            }
            return value.Slave
                && value.Slave.TypeID
                && Number(value.Slave.TypeID) === Number(this.useTypeId);
        }
        // Address Management Events (Grid View)
        setContactAsIsBilling(): void {
            if (!this.defaultBillingID) { return; }
            this.setRunning();
            this.cvAddressBookService.setEntryAsIsBilling(this.accountId)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
            /*
            this.cvAddressBookService.getBook(this.accountId).then(book => {
                this.defaultBilling = _.find(book, x => x.ID === this.defaultBillingID);
                let changed = false;
                if (!this.defaultBilling.IsBilling) {
                    this.defaultBilling.IsBilling = true;
                    changed = true;
                }
                if (this.defaultBilling.IsPrimary) {
                    this.defaultBilling.IsPrimary = false;
                    changed = true;
                }
                (changed
                    ? this.cvAddressBookService.updateEntry(this.accountId, this.defaultBilling)
                    : this.$q.resolve(true)
                ).then((success: boolean) => {
                    if (!success) {
                        this.finishRunning(true);
                        return;
                    }
                    this.$q.all(book.filter(x => x.ID !== this.defaultBillingID).map(e => {
                        if (!e.IsBilling) { return this.$q.resolve(true); }
                        e.IsBilling = false;
                        return this.cvAddressBookService.updateEntry(this.accountId, e);
                    })).finally(() => this.refreshContactChecks(true));
                }).catch(reason2 => this.finishRunning(true, reason2));
            }).catch(reason => this.finishRunning(true, reason));
            */
        }
        setContactAsIsPrimary(): void {
            if (!this.defaultShippingID) { return; }
            this.setRunning();
            this.cvAddressBookService.setEntryAsIsPrimary(this.accountId)
                .then(() => this.finishRunning(),
                      result => this.finishRunning(true, result))
                .catch(reason => this.finishRunning(true, reason));
            /*
            this.cvAddressBookService.getBook(this.accountId).then(book => {
                this.defaultShipping = _.find(book, x => x.ID === this.defaultShippingID);
                let changed = false;
                if (this.defaultShipping.IsBilling) {
                    this.defaultShipping.IsBilling = false;
                    changed = true;
                }
                if (!this.defaultShipping.IsPrimary) {
                    this.defaultShipping.IsPrimary = true;
                    changed = true;
                }
                (changed
                    ? this.cvAddressBookService.updateEntry(this.accountId, this.defaultShipping)
                    : this.$q.resolve(true)
                ).then(success => {
                    if (!success) {
                        this.finishRunning(true);
                        return;
                    }
                    this.$q.all(book.filter(x => x.ID !== this.defaultShippingID).map(e => {
                        if (!e.IsPrimary) { return this.$q.resolve(true); }
                        e.IsPrimary = false;
                        return this.cvAddressBookService.updateEntry(this.accountId, e);
                    })).finally(() => this.refreshContactChecks(true));
                }).catch(reason2 => this.finishRunning(true, reason2));
            }).catch(reason => this.finishRunning(true, reason));
            */
        }
        refreshContactChecks(force: boolean = false): ng.IPromise<void> {
            this.setRunning();
            return this.cvAddressBookService.refreshContactChecks(this.accountId, force)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
            /*
            this.noBillingContactFound = true;
            this.noPrimaryContactFound = true;
            this.primaryAndBillingAreSameContact = false;
            return this.$q((resolve, reject) => {
                this.cvAddressBookService.getBook(this.accountId, force).then(book => {
                    if (force) {
                        this.addressBook = null; // Clear the UI
                    }
                    this.addressBook = book;
                    book.forEach(entry => {
                        this.primaryAndBillingAreSameContact = this.primaryAndBillingAreSameContact
                            || entry.IsBilling && entry.IsPrimary;
                        if (entry.IsBilling) {
                            this.noBillingContactFound = false;
                            this.defaultBillingID = entry.ID;
                            this.defaultBilling = entry;
                        }
                        if (entry.IsPrimary) {
                            this.noPrimaryContactFound = false;
                            this.defaultShippingID = entry.ID;
                            this.defaultShipping = entry;
                        }
                    });
                    resolve();
                    this.finishRunning();
                }).catch(reason => {
                    this.finishRunning(true, reason);
                    reject(reason);
                });
            });
            */
        }
        showEditor(): void { this.editorOpen = true; }
        hideEditor(): void { this.editorOpen = false; }
        editContact(id: number, index: number): void {
            this.cvAddressBookService.getEntry(this.accountId, { id: id }).then(ac => {
                this.cvAddressModalFactory(
                    this.$translate("ui.admin.controls.addressEditor.EditAnAddress"), // title
                    this.$translate("ui.admin.common.Save"), // button
                    "EditContact", // id suffix
                    ac.IsBilling, // is billing
                    ac, // existing
                    this.accountId,
                    false // restricted shipping
                ).then(editedAC => {
                    // Original content for this handler was moved to the modal
                });
            });
            /*
            this.editorAccountContactID = id;
            this.idSuffix = index;
            this.showEditor();
            */
        }
        newContact(): void {
            this.cvAddressModalFactory(
                this.$translate("ui.admin.controls.sales.salesOrderNewWizard.AddANewAddress"), // title
                this.$translate("ui.admin.controls.accounts.accountDetail.AddAddress"), // button
                "NewContact", // id suffix
                false, // is billing
                null, // existing
                this.accountId,
                false // restricted shipping
            ).then(newAC => {
                // Original content for this handler was moved to the modal
            });
            /*
            this.editorAccountContactID = -1;
            this.idSuffix = 0;
            this.showEditor();
            */
        }
        removeContactByID(id: number): void {
            this.setRunning(this.$translate("ui.admin.userDashboard.addressBook.RemovingAddressBookEntry.Ellipses"));
            this.cvAddressBookService.deleteEntry(this.accountId, { id: id })
                .then(() => this.refreshContactChecks(true)); // Will eventually call finishRunning
        }
        copyDict(attrs: api.GeneralAttributeModel[], contact: api.ContactModel): api.SerializableAttributesDictionary {
            const dict = new api.SerializableAttributesDictionary();
            attrs.forEach(a => dict[a.CustomKey] = contact[a.CustomKey]);
            return dict;
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvAddressModalFactory: admin.modals.IAddressModalFactory,
                private readonly cvAuthenticationService: services.IAuthenticationService) { // Used by UI
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.addressBook.deleteConfirmed,
                (__, id: number) => this.removeContactByID(id));
            const unbind2 = $scope.$on(cvServiceStrings.events.addressBook.deleteCancelled,
                () => { /* Do Nothing */ });
            const unbind3 = $scope.$on(cvServiceStrings.events.addressBook.editSave,
                (__, { restrictedShipping }) => {
                    this.restrictedShipping = restrictedShipping;
                    this.hideEditor();
                    this.refreshContactChecks(true);
                });
            const unbind4 = $scope.$on(cvServiceStrings.events.addressBook.editCancelled,
                () => {
                    this.hideEditor();
                    this.refreshContactChecks(true);
                });
            const unbind5 = $scope.$on(cvServiceStrings.events.addressBook.reset,
                () => this.addressBook = null);
            const unbind6 = $scope.$on(cvServiceStrings.events.addressBook.loaded,
                () => this.cvAddressBookService.getBook(this.accountId, false).then(book => this.addressBook = book));
            this.refreshContactChecks(true);
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
                if (angular.isFunction(unbind3)) { unbind3(); }
                if (angular.isFunction(unbind4)) { unbind4(); }
                if (angular.isFunction(unbind5)) { unbind5(); }
                if (angular.isFunction(unbind6)) { unbind6(); }
            });
        }
    }

    adminApp.directive("cefAddressBook", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            accountId: "=?",
            usePhonePrefixLookups: "@?",
            typeId: "=?",
            restrictedShipping: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/controls/accounts/addressBook/addressBook.html", "ui"),
        controller: AddressBookController,
        controllerAs: "addressBookCtrl",
        bindToController: true
    }));
}
