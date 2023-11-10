/**
 * @file framework/store/userDashboard/controls/addressBook/addressBook.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Address book class
 */
module cef.store.userDashboard.controls.addressBook {
    class AddressBookController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        usePhonePrefixLookups: boolean;
        typeId: number;
        restrictedShipping: boolean;
        readOnlyAddress: boolean;
        // Properties
        selectedFullNameAndStreet1: string;
        get useTypeId(): number { return this.typeId || null; };
        idSuffix: number;
        get noBillingContactFound(): boolean {
            return this.cvAddressBookService.noBillingContactFound;
        }
        get noPrimaryContactFound(): boolean {
            return this.cvAddressBookService.noPrimaryContactFound;
        }
        get primaryAndBillingAreSameContact(): boolean {
            return this.cvAddressBookService.primaryAndBillingAreSameContact;
        }
        get defaultBillingID(): number {
            return this.cvAddressBookService.defaultBillingID;
        }
        set defaultBillingID(value: number) {
            if (!value) {
                return;
            }
            this.consoleDebug(`AddressBookController.set defaultBillingID oldValue: '${
                this.cvAddressBookService.defaultBillingID}' newValue: '${value}'`);
            this.cvAddressBookService.defaultBillingID = value;
        }
        get defaultBilling(): api.AccountContactModel { return this.cvAddressBookService.defaultBilling; }
        get defaultShippingID(): number { return this.cvAddressBookService.defaultShippingID; }
        set defaultShippingID(value: number) {
            if (!value) {
                return;
            }
            this.consoleDebug(`AddressBookController.set defaultShippingID oldValue: '${
                this.cvAddressBookService.defaultShippingID}' newValue: '${value}'`);
            this.cvAddressBookService.defaultShippingID = value;
        }
        get defaultShipping(): api.AccountContactModel { return this.cvAddressBookService.defaultShipping; }
        get isSupervisor(): boolean { return this.cvSecurityService.hasRole("Supervisor") };
        addressBook: api.AccountContactModel[] = null;
        bllingAddressBook: api.AccountContactModel[] = null
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
        preselectValues(selected: api.AccountContactModel): void {
            this.defaultShippingID = selected.ID;
            this.setContactAsIsPrimary();
        }
        // Address Management Events (Grid View)
        setContactAsIsBilling(): void {
            if (!this.defaultBillingID
                || !this.addressBook
                || this.cvAddressBookService.runningContactChecks) {
                return;
            }
            this.setRunning();
            this.cvAddressBookService.setEntryAsIsBilling(true)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        setContactAsIsPrimary(): void {
            if (!this.defaultShippingID
                || !this.addressBook
                || this.cvAddressBookService.runningContactChecks) {
                return;
            }
            this.setRunning();
            this.cvAddressBookService.setEntryAsIsPrimary(true)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        refreshContactChecks(force: boolean = false): ng.IPromise<void> {
            this.setRunning();
            return this.cvAddressBookService.refreshContactChecks(force, "AddressBookController.refreshContactChecks")
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        newContact(): void {
            this.cvAddressModalFactory(
                this.$translate("ui.storefront.userDashboard2.controls.addressEditor2.AddANewAddress"),
                this.$translate("ui.storefront.checkout.splitShipping.addressModal.AddAddress"),
                null,
                "NewContact",
                false,
                null)
            .then(newAC => {
                this.setRunning();
                if (this.restrictedShipping && newAC) {
                    this.cvRestrictedRegionCheckService.validate(newAC.Slave).then(restrict => {
                        if (restrict) {
                            this.cvRestrictedRegionCheckService.triggerModal(newAC.Slave);
                        }
                    }).finally(() => this.saveInner(newAC)); // Will eventually call finishRunning
                    return;
                }
                this.saveInner(newAC); // Will eventually call finishRunning
            });
        }
        editContact(id: number): void {
            this.cvAddressBookService.getEntry({ id: id }).then(ac => {
                this.cvAddressModalFactory(
                    this.$translate("ui.storefront.userDashboard2.controls.addressEditor2.EditAnAddress"),
                    this.$translate("ui.storefront.common.Save"),
                    null,
                    "EditContact",
                    ac.IsBilling,
                    ac)
                .then(editedAC => {
                    this.setRunning();
                    if (this.restrictedShipping && editedAC) {
                        this.cvRestrictedRegionCheckService.validate(editedAC.Slave).then(restrict => {
                            if (restrict) {
                                this.cvRestrictedRegionCheckService.triggerModal(editedAC.Slave);
                            }
                        }).finally(() => this.saveInner(editedAC)); // Will eventually call finishRunning
                        return;
                    }
                    this.saveInner(editedAC); // Will eventually call finishRunning
                });
            });
        }
        private saveInner(accountContact: api.AccountContactModel): void {
            (accountContact.ID > 0
                ? this.cvAddressBookService.updateEntry(accountContact)
                : this.cvAddressBookService.addEntry(accountContact)
            ).then(result => {
                if (!result) {
                    this.finishRunning(true);
                    return;
                }
                this.consoleDebug(`AddressBookController.${this.cvServiceStrings.events.addressBook.editSave} broadcast`);
                this.$rootScope.$broadcast(this.cvServiceStrings.events.addressBook.editSave, {
                    restrictedShipping: this.restrictedShipping
                });
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        removeContactByID(id: number): void {
            this.setRunning(this.$translate("ui.storefront.userDashboard.addressBook.RemovingAddressBookEntry.Ellipses"));
            this.cvAddressBookService.deleteEntry({ id: id })
                .then(() => this.refreshContactChecks(false)); // Will eventually call finishRunning
        }
        copyDict(attrs: api.GeneralAttributeModel[], contact: api.ContactModel)
                : api.SerializableAttributesDictionary {
            const dict = new api.SerializableAttributesDictionary();
            attrs.forEach(a => dict[a.CustomKey] = contact[a.CustomKey]);
            return dict;
        }
        // Pagination
        currentPage: number = 0;
        pageSize: number = 9;
        pageSetSize: number = 5;
        currentPageSet = 0;
        pagedResults: api.AccountContactPagedResults;
        pagedResultsTotal: number;
        cachedPagesInSets: Array<Array<number>> = null;

        maxPage = (): number => {
            return this.pageSize <= 0
                ? 0
                : Math.ceil((this.pagedResultsTotal || 1) / this.pageSize);
        }

        maxPageSet = (): number => {
            return Math.max(0, this.pagesInSets.length - 1);
        }

        get pagesInSets(): Array<Array<number>> {
            if (this.cachedPagesInSets && this.cachedPagesInSets.length > 0) {
                return this.cachedPagesInSets;
            }
            const sets = new Array<Array<number>>();
            let currentSet = 0;
            for (let i = 0; i < this.maxPage(); i++) {
                if (!sets[currentSet]) { sets[currentSet] = []; }
                sets[currentSet].push(i);
                if (sets[currentSet].length >= this.pageSetSize) {
                    currentSet++;
                }
            }
            this.cachedPagesInSets = sets;
            return this.cachedPagesInSets;
        }

        getFirstPageInPageSet = (): number => {
            return this.pagesInSets[this.currentPageSet][0];
        }

        getLastPageInPageSet = (): number => {
            return this.pagesInSets[this.currentPageSet][this.pagesInSets[this.currentPageSet].length-1];
        }

        resetCachedPagesInSets = (): void => {
            this.cachedPagesInSets = null;
        }

        goToFirstPage = () => {
            this.currentPage = 0;
            this.currentPageSet = 0;
        }

        goToPreviousPage = () => {
            if (this.currentPage <= 0) {
                return;
            }
            this.currentPage--;
            if (this.currentPage < this.getFirstPageInPageSet()) {
                this.goToPreviousPageSet();
            }
        }

        goToPreviousPageSet = () => {
            if (this.currentPageSet <= 0) {
                return;
            }
            this.currentPageSet--;
            this.goToLastPageInPageSet();
        }

        goToNextPage = () => {
            if (this.currentPage >= this.maxPage()) {
                return;
            }
            this.currentPage++;
            if (this.currentPage > this.getLastPageInPageSet()) {
                this.goToNextPageSet();
            }
        }
        goToNextPageSet = () => {
            if (this.currentPageSet >= this.maxPageSet()) {
                return;
            }
            this.currentPageSet++;
            this.goToFirstPageInPageSet();
        }

        goToFirstPageInPageSet = () => {
            this.currentPage = this.getFirstPageInPageSet();
        }

        goToLastPageInPageSet = () => {
            this.currentPage = this.getLastPageInPageSet();
        }

        goToLastPage = () => {
            this.currentPageSet = this.maxPageSet();
            this.goToLastPageInPageSet();
        }

        getPagedResults = () => {
            let dto: api.GetAddressBookPagedDto = {
                Active: true,
            }
            dto.Paging = {
                StartIndex: this.currentPage + 1,
                Size: this.pageSize
            }
            this.cvApi.geography.GetAddressBookPaged(dto).then(r => {
                this.addressBook = r.data.Results;
                this.pagedResults = r.data;
                this.pagedResultsTotal = r.data.TotalPages
            });
        }

        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                protected readonly $q: ng.IQService,
                private readonly $rootScope: ng.IRootScopeService,
                readonly $timeout: ng.ITimeoutService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvAddressModalFactory: store.modals.IAddressModalFactory,
                private readonly cvRestrictedRegionCheckService: services.IRestrictedRegionCheckService,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvApi: api.ICEFAPI,
                private readonly $cookies: ng.cookies.ICookiesService,
                private readonly cvSecurityService: services.ISecurityService,) { // Used by UI
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.addressBook.deleteConfirmed,
                (__, id: number) => {
                    this.consoleDebug(`AddressBookController.${cvServiceStrings.events.addressBook.deleteConfirmed} detected`);
                    this.removeContactByID(id);
                });
            const unbind2 = $scope.$on(cvServiceStrings.events.addressBook.deleteCancelled,
                () => { /* Do Nothing */ });
            const unbind3 = $scope.$on(cvServiceStrings.events.addressBook.editSave,
                (__, { restrictedShipping }) => {
                    this.consoleDebug(`AddressBookController.${cvServiceStrings.events.addressBook.editSave} detected`);
                    this.restrictedShipping = restrictedShipping;
                    this.refreshContactChecks(false);
                });
            const unbind4 = $scope.$on(cvServiceStrings.events.addressBook.editCancelled,
                () => {
                    this.consoleDebug(`AddressBookController.${cvServiceStrings.events.addressBook.editCancelled} detected`);
                    this.refreshContactChecks(false);
                });
            const unbind5 = $scope.$on(cvServiceStrings.events.addressBook.reset,
                () => {
                    this.consoleDebug(`AddressBookController.${cvServiceStrings.events.addressBook.reset} detected`);
                    this.addressBook = null;
                });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
                if (angular.isFunction(unbind3)) { unbind3(); }
                if (angular.isFunction(unbind4)) { unbind4(); }
                if (angular.isFunction(unbind5)) { unbind5(); }
            });
            $scope.$watch(() => this.currentPage, () => {
                this.getPagedResults();
            });
            function getBillingBook() {
                const _this = this;
                _this.setRunning();
                _this.cvApi.geography.GetAddressBookPaged({
                    IsBilling: true
                }).then(results => {
                    _this.bllingAddressBook = results.data?.Results;
                    _this.finishRunning();
                }).catch((err) => _this.finishRunning(true, err));
            }
            function getBook() {
                const _this = this;
                _this.setRunning();
                _this.cvAddressBookService.getBook(false, true).then(book => {
                    _this.addressBook = book;
                    _this.finishRunning();
                }).finally(() => {
                    this.cvAddressBookService.getPrimaryBilling().then(r => {
                        if (r) {
                            this.cvAddressBookService.noBillingContactFound = false;
                        }
                    });
                    this.cvAddressBookService.getPrimaryShipping().then(r => {
                        if (r) {
                            this.cvAddressBookService.noPrimaryContactFound = false;
                        }
                    });
                });
            }
            this.cvAddressBookService.getPrimaryBilling();
            this.cvAddressBookService.getPrimaryShipping();
            getBillingBook.call(this);
            getBook.call(this);
        }
    }

    cefApp.directive("cefAddressBook", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            usePhonePrefixLookups: "@?",
            typeId: "=?",
            restrictedShipping: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/addressBook/addressBook.html", "ui"),
        controller: AddressBookController,
        controllerAs: "addressBookCtrl",
        bindToController: true
    }));
}
