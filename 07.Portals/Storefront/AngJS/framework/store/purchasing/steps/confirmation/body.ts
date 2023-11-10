/**
 * @file framework/store/purchasing/steps/confirmation/body.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.store.purchasing.steps.confirmation {
    // This is the controller for the directive
    class PurchaseStepConfirmationBodyController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        protected cartType: string;
        protected idSuffix: string;
        // Properties
        private get step(): PurchaseStepConfirmation {
            if (!this.cartType
                || !this.cvPurchaseService
                || !this.cvPurchaseService.steps
                || !this.cvPurchaseService.steps[this.cartType]) {
                return null; // Not available (yet)
            }
            return <PurchaseStepConfirmation>
                _.find(this.cvPurchaseService.steps[this.cartType],
                    x => x.name === this.cvServiceStrings.checkout.steps.confirmation);
        }
        protected get book(): api.AccountContactModel[] {
            if (!this.step) {
                return undefined;
            }
            return this.step.book;
        }
        protected set book(newValue: api.AccountContactModel[]) {
            this.consoleDebug(`PurchaseStepConfirmationBodyController.addressTitle.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepConfirmationBodyController.addressTitle.set 2`);
                this.step.book = newValue;
            }
        }
        protected get currentAccountContact_Bill(): api.AccountContactModel {
            if (!this.step) {
                return undefined;
            }
            return this.step.currentAccountContact_Bill;
        }
        protected set currentAccountContact_Bill(newValue: api.AccountContactModel) {
            this.consoleDebug(`PurchaseStepConfirmationBodyController.currentAccountContact_Bill.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepConfirmationBodyController.currentAccountContact_Bill.set 2`);
                this.step.currentAccountContact_Bill = newValue;
            }
        }
        protected get currentAccountContact_Ship(): api.AccountContactModel {
            if (!this.step) {
                return undefined;
            }
            return this.step.currentAccountContact_Ship;
        }
        protected set currentAccountContact_Ship(newValue: api.AccountContactModel) {
            this.consoleDebug(`PurchaseStepConfirmationBodyController.currentAccountContact_Ship.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepConfirmationBodyController.currentAccountContact_Ship.set 2`);
                this.step.currentAccountContact_Ship = newValue;
            }
        }
        protected get addressTitle_Bill(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.addressTitle_Bill;
        }
        protected set addressTitle_Bill(newValue: string) {
            this.consoleDebug(`PurchaseStepConfirmationBodyController.addressTitle_Bill.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepConfirmationBodyController.addressTitle_Bill.set 2`);
                this.step.addressTitle_Bill = newValue;
            }
        }
        protected get addressTitle_Ship(): string {
            if (!this.step) {
                return undefined;
            }
            return this.step.addressTitle_Ship;
        }
        protected set addressTitle(newValue: string) {
            this.consoleDebug(`PurchaseStepConfirmationBodyController.addressTitle_Ship.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepConfirmationBodyController.addressTitle_Ship.set 2`);
                this.step.addressTitle_Ship = newValue;
            }
        }
        protected get invalid(): boolean {
            if (!this.step) {
                return undefined;
            }
            return this.step.invalid;
        }
        protected set invalid(newValue: boolean) {
            this.consoleDebug(`PurchaseStepConfirmationBodyController.invalid.set`);
            if (this.step) {
                this.consoleDebug(`PurchaseStepConfirmationBodyController.invalid.set 2`);
                this.step.invalid = newValue;
            }
        }
        // Functions
        private readAddressBook(): void {
            const debugMsg = `PurchaseStepConfirmationBodyController.readAddressBook()`;
            this.consoleDebug(debugMsg);
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    this.book = [];
                    this.onChange();
                    return;
                }
                this.cvAddressBookService.getBook().then(book => {
                    this.book = book;
                    this.cvAddressBookService.refreshContactChecks(false, "purchasing.steps.confirmation.body").finally(() => {
                        this.consoleDebug(`${debugMsg} refreshContactChecks finished`);
                        const doBill = (): boolean =>  {
                            if (!this.cvAddressBookService.defaultBillingID) {
                                this.consoleDebug(`${debugMsg} no default billing available to assign`);
                                return false;
                            }
                            this.consoleDebug(`${debugMsg} assigning default billing`);
                            this.currentAccountContact_Bill = this.cvAddressBookService.defaultBilling;
                            return true;
                        };
                        const doShip = (): boolean =>  {
                            if (!this.cvAddressBookService.defaultShippingID) {
                                this.consoleDebug(`${debugMsg} no default shipping available to assign`);
                                return false;
                            }
                            this.consoleDebug(`${debugMsg} assigning default shipping`);
                            this.currentAccountContact_Ship = this.cvAddressBookService.defaultShipping;
                            return true;
                        };
                        const goodBill = doBill();
                        const goodShip = doShip();
                        const preValid = goodBill && goodShip;
                        if (!preValid) {
                            this.onChange();
                            return;
                        }
                        this.step.validate(this.cvCartService.accessCart(this.cartType))
                            .finally(() => this.onChange());
                    });
                });
            });
        }
        // Events
        // NOTE: This must remain an arrow function for angular events
        onChange = (): void => {
            const debugMsg = "PurchaseStepConfirmationBodyController.onChange()";
            this.consoleDebug(debugMsg);
            this.invalid = true;
            // No form to check in UI
            // if (this.forms.billing.$invalid) {
            //     this.consoleDebug(`${debugMsg} failed! Form invalid`);
            //     this.consoleDebug(this.forms.billing.$error);
            //     this.invalid = true;
            //     return;
            // }
            this.setRunning();
            this.step.validate(this.cvCartService.accessCart(this.cartType)).then(success => {
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
            this.consoleDebug(`PurchaseStepConfirmationBodyController.ctor()`);
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

    cefApp.directive("cefPurchaseStepConfirmationBody", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { cartType: "@" },
        templateUrl: $filter("corsLink")("/framework/store/purchasing/steps/confirmation/body.html", "ui"),
        controller: PurchaseStepConfirmationBodyController,
        controllerAs: "pscbCtrl",
        bindToController: true
    }));
}
