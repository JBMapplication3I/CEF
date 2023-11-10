/**
 * @file framework/store/quotes/steps/shipping/body.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote items in the quote cart: The steps of the wizard
 */
module cef.store.quotes.steps.shipping {
    // This is the controller for the directive
    class SubmitQuoteStepShippingBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected cartType: string;
        protected idSuffix: string;
        // Properties
        private get step(): SubmitQuoteStepShipping {
            if (!this.cartType
                || !this.cvSubmitQuoteService
                || !this.cvSubmitQuoteService.steps
                || !this.cvSubmitQuoteService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return <SubmitQuoteStepShipping>
                _.find(this.cvSubmitQuoteService.steps[this.cartType],
                    x => x.name === this.cvServiceStrings.submitQuote.steps.shipping);
        }
        protected get book(): api.AccountContactModel[] {
            if (!this.step) {
                return undefined;
            }
            return this.step.book;
        }
        protected set book(newValue: api.AccountContactModel[]) {
            this.consoleDebug(`SubmitQuoteStepShippingBodyController.addressTitle.set`);
            if (this.step) {
                this.consoleDebug(`SubmitQuoteStepShippingBodyController.addressTitle.set 2`);
                this.step.book = newValue;
            }
        }
        protected get showMakeThisMyDefault(): boolean {
            return this.cefConfig.submitQuote.sections.submitQuoteStepShipping["showMakeThisMyDefault"] || false;
        }
        protected get internalShowContactWidget(): boolean {
            return !this.cefConfig.featureSet.addressBook.enabled
                || !this.cvAuthenticationService.isAuthenticated()
                || !this.book.length;
        }
        protected get internalShowEstimator(): boolean {
            return this.cefConfig.featureSet.shipping.rates.estimator.enabled
                && this.forms["shipping"].$valid
                && this.currentAccountContact.Slave.Address.CountryID > 0
                && this.currentAccountContact.Slave.Address.Street1
                && this.currentAccountContact.Slave.Address.Street1.length > 0;
        }
        protected get internalShowAddressBookSelector(): boolean {
            return this.cvAuthenticationService.isAuthenticated()
                && this.book.length > 0;
        }
        protected get specialInstructions(): string {
            if (this.cefConfig.submitQuote.showSpecialInstructions) {
                return this.step.specialInstructions;
            }
            return undefined;
        }
        protected set specialInstructions(newValue: string) {
            if (this.cefConfig.submitQuote.showSpecialInstructions) {
                this.step.specialInstructions = newValue;
            }
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`SubmitQuoteStepShippingBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`SubmitQuoteStepShippingBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        protected get usePhonePrefixLookups(): boolean {
            return this.cefConfig.featureSet.contacts.phonePrefixLookups.enabled;
        }
        protected get restrictedShipping(): boolean {
            return this.cefConfig.featureSet.shipping.restrictions.enabled;
        }
        protected get currentAccountContact(): api.AccountContactModel {
            if (!this.step) {
                return undefined;
            }
            return this.step.currentAccountContact;
        }
        protected set currentAccountContact(newValue: api.AccountContactModel) {
            this.consoleDebug(`SubmitQuoteStepShippingBodyController.currentAccountContact.set`);
            if (this.step) {
                this.consoleDebug(`SubmitQuoteStepShippingBodyController.currentAccountContact.set 2`);
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
            this.consoleDebug(`SubmitQuoteStepShippingBodyController.addressTitle.set`);
            if (this.step) {
                this.consoleDebug(`SubmitQuoteStepShippingBodyController.addressTitle.set 2`);
                this.step.addressTitle = newValue;
            }
        }
        get invalidAddress(): boolean {
            if (!this.step) {
                return false;
            }
            return this.step.invalid;
        }
        protected get selectedRateQuoteID(): number {
            if (!this.step) {
                return undefined;
            }
            return this.step.selectedRateQuoteID[this.cartType];
        }
        protected set selectedRateQuoteID(newValue: number) {
            this.consoleDebug(`ShippingEstimatesController.selectedRateQuoteID.set`);
            if (this.step) {
                this.consoleDebug(`ShippingEstimatesController.selectedRateQuoteID.set 2`);
                this.step.selectedRateQuoteID[this.cartType] = newValue;
            }
        }
        protected get haveShippingOrEquivalentForRates(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.haveShippingOrEquivalentForRates[this.cartType];
        }
        protected get haveRateQuotes(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.haveRateQuotes[this.cartType];
        }
        protected set haveRateQuotes(newValue: boolean) {
            this.consoleDebug(`ShippingEstimatesController.haveRateQuotes.set`);
            if (this.step) {
                this.consoleDebug(`ShippingEstimatesController.haveRateQuotes.set 2`);
                this.step.haveRateQuotes[this.cartType] = newValue;
                const cart = this.cvCartService.accessCart(this.cartType);
                // if (!cart.ShippingContact) {
                //     cart.ShippingContact = this.currentAccountContact.Slave;
                //     this.cvCartService.applyShippingContact(
                //         this.cartType,
                //         this.sameAsBilling
                //             ? <api.ContactModel>{ /* null */ }
                //             : this.currentAccountContact.Slave)
                // }
                this.step.haveShippingOrEquivalentForRates[this.cartType] =
                    (cart.ShippingSameAsBilling || false) && !cart.BillingContact && !cart.BillingContactID
                        ? false
                        : !this.cefConfig.featureSet.shipping.splitShipping.enabled
                          && !(cart.ShippingSameAsBilling || false) && !cart.ShippingContact && !cart.ShippingContactID
                            ? false
                            : true;
            }
        }
        protected get showDeliveryFail(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.showDeliveryFail[this.cartType];
        }
        protected set showDeliveryFail(newValue: boolean) {
            this.consoleDebug(`ShippingEstimatesController.showDeliveryFail.set`);
            if (this.step) {
                this.consoleDebug(`ShippingEstimatesController.showDeliveryFail.set 2`);
                this.step.showDeliveryFail[this.cartType] = newValue;
            }
        }
        protected get isGettingRates(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.isGettingRates[this.cartType];
        }
        protected set isGettingRates(newValue: boolean) {
            this.consoleDebug(`ShippingEstimatesController.isGettingRates.set`);
            if (this.step) {
                this.consoleDebug(`ShippingEstimatesController.isGettingRates.set 2`);
                this.step.isGettingRates[this.cartType] = newValue;
            }
        }
        protected get internalEstimateTranslationKey(): string {
            return "ui.storefront.checkout.views.shippingInformation."
                + (this.selectedRateQuoteID ? "reestimateShippingCost" : "estimateShippingCost");
        }
        // Functions
        // NOTE: This must remain an arrow function for angular events
        private readAddressBook = () => {
            const debugMsg = `SubmitQuoteStepShippingBodyController.readAddressBook()`;
            this.consoleDebug(debugMsg);
            let tempID: number = null;
            if (this.currentAccountContact) {
                // Copy the data in memory so we don't lose it
                tempID = this.currentAccountContact.ID;
                this.currentAccountContact = angular.fromJson(angular.toJson(this.currentAccountContact));
            }
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    this.book = [];
                    return;
                }
                this.cvAddressBookService.getBook().then(book => {
                    this.book = book;
                    if (tempID) {
                        const found = _.find(this.book, e => e.ID === tempID);
                        if (found) {
                            this.currentAccountContact = found;
                        }
                        this.onChangeWithRatesReset();
                        return;
                    }
                    this.cvAddressBookService.refreshContactChecks(false, "submitQuote.steps.shipping.body").finally(() => {
                        this.consoleDebug(`${debugMsg} refreshContactChecks finished`);
                        if (!this.cvAddressBookService.defaultBillingID) {
                            this.consoleDebug(`${debugMsg} no default billing available to assign`);
                            this.onChangeWithRatesReset();
                            return;
                        }
                        this.consoleDebug(`${debugMsg} assigning default billing`);
                        this.currentAccountContact = this.cvAddressBookService.defaultBilling;
                        this.step.validate(this.cvCartService.accessCart(this.cartType))
                            .finally(() => this.onChange(false));
                    });
                });
            });
        }
        protected add(): void {
            this.cvAddressModalFactory(
                this.$translate("ui.storefront.userDashboard2.controls.addressEditor2.AddANewAddress"),
                this.$translate("ui.storefront.checkout.splitShipping.addressModal.AddAddress"),
                null,
                "SubmitQuoteShipping",
                false,
                null
            ).then(newEntry => this.checkForPrimaryShipping(newEntry));
        }
        protected checkForPrimaryShipping(newEntry: api.AccountContactModel): void {
            var shippingContact = this.book.filter(x => x.IsPrimary);
            if (!shippingContact.length) {
                newEntry.IsPrimary = true;
            }
            this.cvAddressBookService.addEntry(newEntry);
        }
        protected submitAddress(): void {
            this.consoleDebug(`ShippingEstimatesController.submitAddress()`);
            this.showDeliveryFail = false;
            if (this.step.invalidByAddress && this.forms["shipping"].$valid) {
                // We probably need to onChange, call that
                this.onChange(true);
                return;
            }
            this.cvCartService.applyShippingSameAsBilling(this.cartType, false).then(() => {
                this.cvCartService.applyShippingContact(
                    this.cartType,
                    this.currentAccountContact.Slave)
                .then(() => this.refreshShippingRateQuotes());
            });
        }
        refreshShippingRateQuotes(): void {
            const debugMsg = `ShippingEstimatesController.refreshShippingRateQuotes()`;
            this.consoleDebug(debugMsg);
            if (this.isGettingRates) {
                this.consoleDebug(`${debugMsg} isGettingRates was was true, don't fire event twice`);
                return;
            }
            this.consoleDebug(`${debugMsg} isGettingRates was was false, fire event`);
            /* TODO: Not sure how reliable starting the set running here will be, the
             * finishRunning would be fired by one or more closers */
            this.setRunning();
            this.isGettingRates = true;
            this.haveRateQuotes = false; // Reset this while we are getting data
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.ready,
                this.cvCartService.accessCart(this.cartType));
            /* The rate quote manager widget will react by applying the updated shipping contact to the
             * server, then it will get rate quotes. When that finishes, it will broadcast
             * "loadShippingRateQuotesCompleted" which this controller will react to with a call to the
             * "onRefreshShippingRatesComplete" function
             */
        }
        // Events
        // NOTE: This must remain an arrow function for angular events
        onChangeWithRatesReset = (): void => {
            const debugMsg = `SubmitQuoteStepShippingBodyController.onChangeWithRatesReset()`;
            this.consoleDebug(debugMsg);
            const cart = this.cvCartService.accessCart(this.cartType);
            if (!cart
                || !cart.RateQuotes
                || !cart.RateQuotes.length) {
                // Only fire this if we actually need to clear UI
                this.consoleDebug(`${debugMsg} Exiting after taking no action`);
                this.$timeout(() => this.onChange(false), 750);
                return;
            }
            if (!_.some(cart.RateQuotes, x => x.Selected)) {
                // We just need to clear UI, not make a server call
                this.consoleDebug(`${debugMsg} firing shipping unready`);
                this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.unready,
                    this.cvCartService.accessCart(this.cartType),
                    false);
                this.$timeout(() => this.onChange(false), 750);
                return;
            }
            this.setRunning();
            this.cvApi.shopping.ClearCurrentCartShippingRateQuote({ TypeName: this.cartType }).then(() => {
                this.finishRunning();
                this.consoleDebug(`${debugMsg} firing shipping unready`);
                this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.unready,
                    this.cvCartService.accessCart(this.cartType),
                    true); // The on change will eventually be fired because of this request to reload the cart
            }).catch(reason => this.finishRunning(true, reason));
        };
        // NOTE: This must remain an arrow function for angular events
        onChange = (submitIfGood: boolean): void => {
            const debugMsg = `SubmitQuoteStepShippingBodyController.onChange()`;
            this.consoleDebug(debugMsg);
            if (this.forms["shipping"].$invalid) {
                this.consoleDebug(`${debugMsg} failed! via address selection form`);
                return;
            }
            if (this.cefConfig.featureSet.shipping.rates.estimator.enabled
                && this.forms["estimating"].$invalid) {
                this.consoleDebug(`${debugMsg} failed! via estimating form`);
                return;
            }
            this.setRunning();
            this.step.validate(this.cvCartService.accessCart(this.cartType))
                .then(success => {
                    if (!success) {
                        this.consoleDebug(`${debugMsg} failed! via false`);
                        this.finishRunning(true, "An error has occurred");
                        return;
                    }
                    this.consoleDebug(`${debugMsg} success!`);
                    this.finishRunning();
                    if (submitIfGood === true) {
                        this.submitAddress();
                    }
                }).catch(reason => {
                    this.consoleDebug(`${debugMsg} failed! via catch`);
                    this.finishRunning(true, reason || "An error has occurred");
                });
        }
        // TODO: onRefreshShippingRatesFailed
        onRefreshShippingRateQuotesComplete(rateQuotes: api.RateQuoteModel[], selectedRateQuoteID: number): void {
            this.selectedRateQuoteID = selectedRateQuoteID;
            this.haveRateQuotes = true;
            this.isGettingRates = false;
            this.step.rateQuotes[this.cartType] = rateQuotes;
            this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.setSelectedRateQuoteID);
            this.finishRunning();
        }
        onShippingRateQuoteSelected(type: string, selectedRateQuoteID: number): void {
            if (type === this.cartType) {
                this.selectedRateQuoteID = selectedRateQuoteID;
            }
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $timeout: ng.ITimeoutService,
                private readonly $translate: ng.translate.ITranslateService,
                readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAddressModalFactory: store.modals.IAddressModalFactory,
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvCartService: services.ICartService,
                private readonly cvSubmitQuoteService: services.ISubmitQuoteService) {
            super(cefConfig);
            this.consoleDebug(`SubmitQuoteStepShippingBodyController.ctor()`);
            const unbind1 = $scope.$on(cvServiceStrings.events.addressBook.loaded, this.readAddressBook);
            ////const unbind2 = $scope.$on(cvServiceStrings.events.shipping.loaded, this.onChange);
            const unbind2 = $scope.$on(cvServiceStrings.events.shipping.loaded,
                (__: ng.IAngularEvent, rateQuotes: api.RateQuoteModel[], selectedRateQuoteID: number) =>
                    this.onRefreshShippingRateQuotesComplete(rateQuotes, selectedRateQuoteID));
            ////const unbind3 = $scope.$on(cvServiceStrings.events.shipping.rateQuoteSelected, this.onChange);
            const unbind3 = $scope.$on(cvServiceStrings.events.shipping.rateQuoteSelected,
                (__: ng.IAngularEvent, cartType: string, selectedRateQuoteID: number): void =>
                    this.onShippingRateQuoteSelected(cartType, selectedRateQuoteID));
            const unbind4 = $scope.$on(cvServiceStrings.events.carts.loaded, () => this.onChange(false));
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
                if (angular.isFunction(unbind3)) { unbind3(); }
                if (angular.isFunction(unbind4)) { unbind4(); }
            });
            // Do an initial load of data
            this.readAddressBook();
        }
    }

    cefApp.directive("cefSubmitQuoteStepShippingBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@", idSuffix: "=?" },
        templateUrl: $filter("corsLink")("/framework/store/quotes/steps/shipping/body.html", "ui"),
        controller: SubmitQuoteStepShippingBodyController,
        controllerAs: "sqssbCtrl",
        bindToController: true
    }));
}
