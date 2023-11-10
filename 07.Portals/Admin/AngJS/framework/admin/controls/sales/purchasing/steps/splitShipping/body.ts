/**
 * @file framework/admin/purchasing/steps/splitShipping/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps.splitShipping {
    // This is the controller for the directive
    class PurchaseStepSplitShippingBodyController extends core.TemplatedControllerBase {
        // Properties
        lookupKey: api.CartByIDLookupKey; // Bound by Scope
        idSuffix: string; // Bound by Scope
        private get step(): PurchaseStepSplitShipping {
            if (!this.lookupKey
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.lookupKey.toString()]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepSplitShipping>
                _.find(this.cvPurchaseService.steps[this.lookupKey.toString()],
                    x => x.name === this.cvServiceStrings.checkout.steps.splitShipping);
        }
        protected book: api.AccountContactModel[] = []; // Bound on address book load event
        protected get billingContact(): api.AccountContactModel {
            if (!this.step) {
                return undefined;
            }
            return (<steps.billing.PurchaseStepBilling>
                    _.find(this.cvPurchaseService.steps[this.lookupKey.toString()],
                        x => x.name === this.cvServiceStrings.checkout.steps.billing))
                .currentAccountContact;
        }
        protected get sameAsBilling(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.sameAsBilling;
        }
        protected set sameAsBilling(newValue: boolean) {
            this.consoleDebug(`PurchaseStepSplitShippingBodyController.sameAsBilling.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepSplitShippingBodyController.sameAsBilling.set 2`);
                this.step.sameAsBilling = newValue;
            }
        }
        get specialInstructions(): string {
            if (this.cefConfig.purchase.showSpecialInstructions) {
                return this.step.specialInstructions;
            }
            return undefined;
        }
        set specialInstructions(newValue: string) {
            if (this.cefConfig.purchase.showSpecialInstructions) {
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
            this.consoleDebug(`PurchaseStepSplitShippingBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepSplitShippingBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        protected get usePhonePrefixLookups(): boolean {
            return this.cefConfig.featureSet.contacts.phonePrefixLookups.enabled;
        }
        protected get restrictedShipping(): boolean {
            return this.cefConfig.featureSet.shipping.restrictions.enabled;
        }
        // Functions
        // NOTE: This must remain an arrow function for angular events
        private readAddressBook = () => {
            this.consoleDebug(`PurchaseStepSplitShippingBodyController.readAddressBook()`);
            this.cvAddressBookService.getBook(this.lookupKey.AID).then(book => {
                this.book = book;
                this.onChange();
            });
        }
        // Events
        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            this.consoleDebug(`PurchaseStepSplitShippingBodyController.onChange()`);
            this.invalid = true;
            if (this.forms.splitShipping.$invalid) {
                this.consoleDebug(`PurchaseStepSplitShippingBodyController.onChange() failed!`);
                this.invalid = true;
                return;
            }
            this.setRunning();
            this.step.validate(this.cvCartService.accessCart(this.lookupKey)).then(success => {
                if (!success) {
                    this.consoleDebug(`PurchaseStepSplitShippingBodyController.onChange() failed!`);
                    this.invalid = true;
                    this.finishRunning(true, "An error has occurred");
                    return;
                }
                this.consoleDebug(`PurchaseStepSplitShippingBodyController.onChange() success!`);
                this.invalid = false;
                this.finishRunning();
            }).catch(reason => {
                this.consoleDebug(`PurchaseStepSplitShippingBodyController.onChange() failed!`);
                this.invalid = true;
                this.finishRunning(true, reason || "An error has occurred");
            });
        }
        onRefreshShippingRateQuotesComplete(rateQuotes: api.RateQuoteModel[], selectedRateQuoteID: number): any {
            // TODO: Verify we don't need to do anything else here
            this.onChange();
        }
        onShippingRateQuoteSelected(lookupKey: api.CartByIDLookupKey, selectedRateQuoteID: number): void {
            // TODO: Verify we don't need to do anything else here
            this.onChange();
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $rootScope: ng.IRootScopeService,
                readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvCartService: services.ICartService,
                private readonly cvPurchaseService: services.IPurchaseService) {
            super(cefConfig);
            this.consoleDebug(`PurchaseStepSplitShippingBodyController.ctor()`);
            const unbind1 = $scope.$on(cvServiceStrings.events.addressBook.loaded,
                this.readAddressBook);
            const unbind2 = this.$scope.$on(this.cvServiceStrings.events.shipping.rateQuoteSelected,
                (__: ng.IAngularEvent, lookupKey: api.CartByIDLookupKey, selectedRateQuoteID: number): void =>
                    this.onShippingRateQuoteSelected(lookupKey, selectedRateQuoteID));
            const unbind3 = this.$scope.$on(this.cvServiceStrings.events.shipping.loaded,
                (__: ng.IAngularEvent, rateQuotes: api.RateQuoteModel[], selectedRateQuoteID: number) =>
                    this.onRefreshShippingRateQuotesComplete(rateQuotes, selectedRateQuoteID));
            const unbind4 = this.$scope.$on(this.cvServiceStrings.events.shipping.revalidateStep,
                () => {
                    this.consoleDebug("revalidateStep detected");
                    this.onChange();
                });
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

    adminApp.directive("cefPurchaseStepSplitShippingBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { lookupKey: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/purchasing/steps/splitShipping/body.html", "ui"),
        controller: PurchaseStepSplitShippingBodyController,
        controllerAs: "psssbCtrl",
        bindToController: true
    }));
}
