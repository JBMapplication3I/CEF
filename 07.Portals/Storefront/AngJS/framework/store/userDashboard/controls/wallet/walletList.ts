/**
 * @file framework/store/userDashboard/controls/wallet/walletList.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Wallet List controller class
 */
module cef.store.userDashboard.controls.wallet {
    class WalletListController extends core.TemplatedControllerBase {
        // Properties
        get wallet(): Array<api.WalletModel> {
            return this.cvWalletService.wallet;
        }
        get defaultEntryID(): number {
            return this.cvWalletService.defaultEntryID;
        }
        set defaultEntryID(value: number) {
            if (!value) {
                return;
            }
            this.consoleDebug(`WalletListController.set defaultEntryID oldValue: '${
                this.cvWalletService.defaultEntryID}' newValue: '${value}'`);
            this.cvWalletService.defaultEntryID = value;
        }
        get defaultEntry(): api.WalletModel {
            return this.cvWalletService.defaultEntry;
        }
        setEntryAsDefault(): void {
            if (!this.defaultEntryID
                || !this.wallet
                || this.cvWalletService.runningChecks) {
                return;
            }
            this.setRunning();
            this.cvWalletService.setEntryAsDefault()
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        refreshChecks(force: boolean = false): ng.IPromise<void> {
            this.setRunning();
            return this.cvWalletService.refreshChecks(force, "WalletController.refreshChecks")
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        // Functions
        newEntry(): void {
            this.cvWalletModalFactory(
                this.$translate("ui.storefront.userDashboard.wallet.AddANewEntry"),
                this.$translate("ui.storefront.userDashboard.wallet.AddEntry"),
                null,
                "NewEntry",
                null)
            .then(newE => {
                this.setRunning();
                this.saveInner(newE); // Will eventually call finishRunning
            });
        }
        editEntry(id: number): void {
            this.cvWalletService.getEntry({ id: id }).then(e => {
                this.cvWalletModalFactory(
                    this.$translate("ui.storefront.userDashboard.wallet.EditAnEntry"),
                    this.$translate("ui.storefront.common.Save"),
                    null,
                    "EditEntry",
                    e)
                .then(editedE => {
                    this.setRunning();
                    this.saveInner(editedE); // Will eventually call finishRunning
                });
            });
        }
        private saveInner(entry: api.WalletModel): void {
            (entry.ID > 0
                ? this.cvWalletService.updateEntry(entry)
                : this.cvWalletService.addEntry(entry)
            ).then(result => {
                if (!result) {
                    this.finishRunning(true);
                    return;
                }
                this.consoleDebug(`WalletController.${this.cvServiceStrings.events.wallet.updated} broadcast`);
                this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.updated);
                this.finishRunning();
            }).catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvWalletService: services.IWalletService,
                private readonly cvWalletModalFactory: store.modals.IWalletModalFactory) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.wallet.updated,
                (__: ng.IAngularEvent, action: string = null, key: number | string = null) => {
                    this.consoleDebug(`walletListCtrl: ${cvServiceStrings.events.wallet.updated} ${action} ${key}`);
                    this.cvWalletService.getWallet(true).then(() => { this.consoleDebug("walletListCtrl: getWallet finished"); });
                });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefWalletList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/wallet/walletList.html", "ui"),
        controller: WalletListController,
        controllerAs: "walletListCtrl"
    }));
}
