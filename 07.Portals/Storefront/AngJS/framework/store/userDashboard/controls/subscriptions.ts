/**
 * @file framework/store/userDashboard/controls/subscriptions.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved
 * @desc Subscription viewer for users to see their current subscription data
 */
module cef.store.userDashboard.controls {
    abstract class SetDefaultAddressModalControllerBase extends core.TemplatedControllerBase {
        // Properties
        addressBook: api.AccountContactModel[] = [];
        selectedID: number = null;
        // Functions
        ok(): void {
            this.$uibModalInstance.close(this.selectedID);
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        protected load(): void {
            this.cvAddressBookService.getBook().then(book => {
                if (!book || !book.length) {
                    return;
                }
                this.loadInner(book);
            });
        }
        protected abstract loadInner(data: api.AccountContactModel[]): void;
        // Constructor
        constructor(
                protected readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAddressBookService: services.IAddressBookService) {
            super(cefConfig);
        }
    }

    class SetDefaultBillingModalController extends SetDefaultAddressModalControllerBase {
        // Functions
        protected loadInner(data: api.AccountContactModel[]): void {
            this.addressBook = data.filter(x => !x.IsPrimary);
            const selected = _.find(this.addressBook, x => x.IsBilling);
            if (selected) {
                this.selectedID = selected.SlaveID;
            }
        }
        // Constructor
        constructor(
                protected readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAddressBookService: services.IAddressBookService) {
            super($uibModalInstance, cefConfig, cvApi, cvAddressBookService);
            this.load();
        }
    }

    class SetDefaultShippingModalController extends SetDefaultAddressModalControllerBase {
        // Functions
        protected loadInner(data: api.AccountContactModel[]): void {
            this.addressBook = data.filter(x => !x.IsBilling);
            const selected = _.find(this.addressBook, x => x.IsPrimary);
            if (selected) {
                this.selectedID = selected.SlaveID;
            }
        }
        // Constructor
        constructor(
                protected readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAddressBookService: services.IAddressBookService) {
            super($uibModalInstance, cefConfig, cvApi, cvAddressBookService);
            this.load();
        }
    }

    class ChangeSubscriptionWalletEntryModalController extends core.TemplatedControllerBase {
        // Properties
        wallet: api.WalletModel[] = [];
        selectedID: number = null;
        // Functions
        ok(): void {
            this.$uibModalInstance.close(this.selectedID);
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        protected load(): void {
            this.setRunning();
            this.cvApi.payments.GetCurrentUserWallet().then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true, null, r && r.data && r.data.Messages);
                    return;
                }
                this.wallet = r.data.Result;
                this.selectedID = this.currentWalletID;
                this.finishRunning();
            }, result => this.finishRunning(true, result))
            .catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                protected readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                private readonly currentWalletID: number) {
            super(cefConfig);
        }
    }


    class SubscriptionsController extends UserDashboardControllerBase {
        // Properties
        record: api.SubscriptionModel = null;
        records: Array<api.SubscriptionModel> = [];
        showAlert = false;
        alertMessage: string = null;
        imagePath: string = null;
        pastPayments: Array<any> = [];
        notEligibleToCancel = false;
        notEligibleToEditBilling = false;
        notEligibleToEditShipping = false;
        notEligibleToEditWallet = false;
        billingContactID: number = null;
        shippingContactID: number = null;
        currentWalletID: number = null;
        // Functions
        cancel(): void {
            this.setRunning();
            this.cvConfirmModalFactory(
                    this.$translate("ui.storefront.userDashboard2.controls.subscriptions.ConfirmCancel.Message"))
                .then(confirmed => {
                    if (!confirmed) {
                        this.finishRunning();
                        return;
                    }
                    this.cvApi.providers.CancelSubscription({ ID: this.record.ID }).then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded) {
                            this.finishRunning(true);
                            return;
                        }
                        this.load();
                    }, result => this.finishRunning(true, result))
                    .catch(reason => this.finishRunning(true, reason));
                });
        }
        editBilling(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/modals/changeDefaultBillingModal.html", "ui"),
                controller: SetDefaultBillingModalController,
                controllerAs: "setDefaultBillingModalCtrl",
                size: "sm"
            }).result.then((result: number) => {
                this.billingContactID = result;
                this.updateSubscription();
            });
        }
        editShipping(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/modals/changeDefaultShippingModal.html", "ui"),
                controller: SetDefaultShippingModalController,
                controllerAs: "setDefaultShippingModalCtrl",
                size: "sm"
            }).result.then((result: number) => {
                this.shippingContactID = result;
                this.updateSubscription();
            });
        }
        editWallet(): void {
            this.$uibModal.open({
                templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/modals/changeSubscriptionWalletEntryModal.html", "ui"),
                controller: ChangeSubscriptionWalletEntryModalController,
                controllerAs: "changeSubscriptionWalletEntryModalCtrl",
                size: "sm"
            }).result.then((result: number) => {
                this.currentWalletID = result;
                this.updateSubscription();
            });
        }
        private updateSubscription(): void {
            this.setRunning();
            const dto = <api.ModifySubscriptionForCurrentUserDto>{
                SubscriptionID: this.record.ID,
                BillingContactID: this.billingContactID,
                ShippingContactID: this.shippingContactID,
                WalletID: this.currentWalletID
            };
            this.cvApi.providers.ModifySubscriptionForCurrentUser(dto).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.finishRunning(true);
                    return;
                }
                this.load();
            }, result => this.finishRunning(true, result))
            .catch(reason => this.finishRunning(true, reason));
        }
        private load(): void {
            this.setRunning();
            this.record.ProductMembershipLevel.Slave.Name
            this.cvApi.payments.GetCurrentUserSubscriptions().then(r => {
                if (!r || !r.data) {
                    this.finishRunning(true);
                    return;
                }
                this.records = r.data.Results;
                this.record = this.records.length > 0 ? this.records[0] : null;
                if (this.record) {
                    this.notEligibleToCancel = false;
                    this.notEligibleToEditBilling = false;
                    this.notEligibleToEditShipping = false;
                    this.notEligibleToEditWallet = false;
                } else {
                    this.notEligibleToCancel = true;
                    this.notEligibleToEditBilling = true;
                    this.notEligibleToEditShipping = true;
                    this.notEligibleToEditWallet = true;
                }
                this.finishRunning()
            }, result => this.finishRunning(true, result))
            .catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvConfirmModalFactory: store.modals.IConfirmModalFactory) {
            super(cefConfig, cvAuthenticationService);
        }
    }

    cefApp.directive("cefSubscriptions", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/subscriptions.html", "ui"),
        controller: SubscriptionsController,
        controllerAs: "subscriptionsCtrl"
    }));
}
