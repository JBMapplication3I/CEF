/**
 * @file framework/admin/wallet/walletList.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Wallet List controller class
 */
module cef.admin.userDashboard.controls.wallet {
    class WalletListController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        userId: number;
        // Properties
        get wallet(): Array<api.WalletModel> {
            return this.cvWalletService.wallet[this.userId];
        }
        get defaultEntryID(): number {
            return this.cvWalletService[this.userId].defaultEntryID;
        }
        set defaultEntryID(value: number) {
            if (!value) {
                return;
            }
            this.consoleDebug(`WalletListController.set defaultEntryID oldValue: '${
                this.cvWalletService[this.userId].defaultEntryID}' newValue: '${value}'`);
            this.cvWalletService[this.userId].defaultEntryID = value;
        }
        get defaultEntry(): api.WalletModel {
            return this.cvWalletService[this.userId].defaultEntry;
        }
        refreshChecks(force: boolean = false): ng.IPromise<void> {
            this.setRunning();
            return this.cvWalletService.refreshChecks(this.userId, force, "WalletController.refreshChecks")
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        // Functions
        newEntry(): void {
            this.cvWalletModalFactory(
                this.$translate("ui.admin.common.wallet.AddANewEntry"),
                this.$translate("ui.admin.common.wallet.AddEntry"),
                this.userId,
                null,
                "NewEntry",
                null)
            .then(newE => {
                this.setRunning();
                this.saveInner(newE); // Will eventually call finishRunning
            });
        }
        editEntry(id: number): void {
            this.cvWalletService.getEntry(this.userId, { id: id }).then(e => {
                this.cvWalletModalFactory(
                    this.$translate("ui.admin.common.wallet.EditAnEntry"),
                    this.$translate("ui.admin.common.Save"),
                    this.userId,
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
                ? this.cvWalletService.updateEntry(this.userId, entry)
                : this.cvWalletService.addEntry(this.userId, entry)
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
                private readonly cvWalletModalFactory: admin.modals.IWalletModalFactory) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.wallet.updated,
                (__: ng.IAngularEvent, action: string = null, key: number | string = null) => {
                    this.consoleDebug(`walletListCtrl: ${cvServiceStrings.events.wallet.updated} ${action} ${key}`);
                    this.cvWalletService.getWallet(this.userId, true).then(() => { this.consoleDebug("walletListCtrl: getWallet finished"); });
                });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    adminApp.directive("cefWalletList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { userId: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/wallet/walletList.html", "ui"),
        controller: WalletListController,
        controllerAs: "walletListCtrl",
        bindToController: true
    }));
}
