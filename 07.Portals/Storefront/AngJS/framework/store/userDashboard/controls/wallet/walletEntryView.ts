module cef.store.userDashboard.controls.wallet {
    class WalletEntryViewController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        entry: api.WalletModel;
        readonly: boolean;
        hideHeader: boolean;
        hideFooter: boolean;
        nonCard: boolean;
        // Static Properties
        private static readonly confirmDeactivateKey = {
            "card": "ui.storefront.userDashboard.wallet.AreYouSureYouWantToDeactivateThisCard.Question",
            "echeck": "ui.storefront.userDashboard.wallet.AreYouSureYouWantToDeactivateThisEcheck.Question"
        };
        // Functions
        protected remove(): void {
            const id = this.entry.ID;
            const question = WalletEntryViewController.confirmDeactivateKey[this.isCard() ? "card" : "echeck"];
            this.cvConfirmModalFactory(this.$translate(question)).then(confirmed => {
                if (!confirmed) {
                    return;
                }
                this.setRunning();
                this.cvWalletService.deleteEntry({ id: id }).then(success => {
                    if (!success) {
                        this.finishRunning(
                            true,
                            this.$translate("ui.storefront.common.DeactivationFailed.Exclamation"));
                        return;
                    }
                    this.finishRunning();
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.updated, "deleted", id);
                }).catch(reason => this.finishRunning(true, reason));
            });
        }
        private isCard(): boolean {
            if (!this.entry) {
                return true;
            }
            return this.entry.CardType !== "Checking"
                && this.entry.CardType !== "Savings";
        }
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvConfirmModalFactory: store.modals.IConfirmModalFactory,
                protected readonly cvWalletService: services.IWalletService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefWalletEntryView", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            entry: "=",
            readonly: "=?",
            hideHeader: "=?",
            hideFooter: "=?",
            nonCard: "=?"
        },
        replace: true, // Required for Layout
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/wallet/walletEntryView.html", "ui"),
        controller: WalletEntryViewController,
        controllerAs: "wevCtrl",
        bindToController: true
    }));
}
