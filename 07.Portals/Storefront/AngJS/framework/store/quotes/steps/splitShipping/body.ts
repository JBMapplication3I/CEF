/**
 * @file framework/store/quotes/steps/splitShipping/body.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote items in the quote cart: The steps of the wizard
 */
module cef.store.quotes.steps.splitShipping {
    // This is the controller for the directive
    class SubmitQuoteStepSplitShippingBodyController extends core.TemplatedControllerBase {
        // Properties
        cartType: string; // Bound by Scope
        idSuffix: string; // Bound by Scope
        private get step(): SubmitQuoteStepSplitShipping {
            if (!this.cartType
                || !this.cvSubmitQuoteService
                || !this.cvSubmitQuoteService.steps
                || !this.cvSubmitQuoteService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return <SubmitQuoteStepSplitShipping>
                _.find(this.cvSubmitQuoteService.steps[this.cartType],
                    x => x.name === this.cvServiceStrings.submitQuote.steps.splitShipping);
        }
        protected book: api.AccountContactModel[] = []; // Bound on address book load event
        get specialInstructions(): string {
            if (this.cefConfig.submitQuote.showSpecialInstructions) {
                return this.step.specialInstructions;
            }
            return undefined;
        }
        set specialInstructions(newValue: string) {
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
            this.consoleDebug(`SubmitQuoteStepSplitShippingBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`SubmitQuoteStepSplitShippingBodyController.invalid.set 2`);
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
            this.consoleDebug(`SubmitQuoteStepSplitShippingBodyController.readAddressBook()`);
            this.cvAddressBookService.getBook().then(book => {
                this.book = book;
                this.onChange();
            });
        }
        // Events
        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            this.consoleDebug(`SubmitQuoteStepSplitShippingBodyController.onChange()`);
            this.invalid = true;
            if (this.forms.splitShipping.$invalid) {
                this.consoleDebug(`SubmitQuoteStepSplitShippingBodyController.onChange() failed!`);
                this.invalid = true;
                return;
            }
            this.setRunning();
            this.step.validate(this.cvCartService.accessCart(this.cartType)).then(success => {
                if (!success) {
                    this.consoleDebug(`SubmitQuoteStepSplitShippingBodyController.onChange() failed!`);
                    this.invalid = true;
                    this.finishRunning(true, "An error has occurred");
                    return;
                }
                this.consoleDebug(`SubmitQuoteStepSplitShippingBodyController.onChange() success!`);
                this.invalid = false;
                this.finishRunning();
            }).catch(reason => {
                this.consoleDebug(`SubmitQuoteStepSplitShippingBodyController.onChange() failed!`);
                this.invalid = true;
                this.finishRunning(true, reason || "An error has occurred");
            });
        }
        onRefreshShippingRateQuotesComplete(rateQuotes: api.RateQuoteModel[], selectedRateQuoteID: number): any {
            // TODO: Verify we don't need to do anything else here
            this.onChange();
        }
        onShippingRateQuoteSelected(cartType: string, selectedRateQuoteID: number): void {
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
                private readonly cvSubmitQuoteService: services.ISubmitQuoteService) {
            super(cefConfig);
            this.consoleDebug(`SubmitQuoteStepSplitShippingBodyController.ctor()`);
            const unbind1 = $scope.$on(cvServiceStrings.events.addressBook.loaded,
                this.readAddressBook);
            const unbind2 = this.$scope.$on(this.cvServiceStrings.events.shipping.rateQuoteSelected,
                (__: ng.IAngularEvent, cartType: string, selectedRateQuoteID: number): void =>
                    this.onShippingRateQuoteSelected(cartType, selectedRateQuoteID));
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

    cefApp.directive("cefSubmitQuoteStepSplitShippingBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/quotes/steps/splitShipping/body.html", "ui"),
        controller: SubmitQuoteStepSplitShippingBodyController,
        controllerAs: "sqsssbCtrl",
        bindToController: true
    }));
}
