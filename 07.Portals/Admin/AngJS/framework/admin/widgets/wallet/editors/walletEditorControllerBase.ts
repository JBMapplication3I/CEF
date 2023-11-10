/**
 * @file framework/admin/widgets/wallet/editors/walletEditorControllerBase.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Wallet Editor controller base class
 */
module cef.admin.userDashboard.controls.wallet {
    export class WalletEditorControllerBase extends core.TemplatedControllerBase {
        // Properties
        readOnly: boolean;
        createMode: boolean;
        editMode: boolean;
        viewstate: {
            readOnly: boolean;
            createMode: boolean;
            editMode: boolean;
        };
        entry: api.WalletModel;
        originalObject: string;
        hideHeader: boolean;
        hideFooter: boolean;
        userID: number;
        // Overridable strings
        formName = "Card";
        confirmDeactivateKey = "ui.admin.common.wallet.AreYouSureYouWantToDeactivateThisCard.Question";
        // Functions
        save(): void {
            if (this.viewstate.createMode) {
                this.add();
                return;
            }
            if (this.viewstate.editMode) {
                this.update();
            }
        }
        add(): void {
            this.forms[this.formName].$setDirty();
            if (this.forms[this.formName].$invalid) {
                return;
            }
            this.setRunning();
            this.cvWalletService.addEntry(this.userID, this.entry)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        update(): void {
            this.forms[this.formName].$setDirty();
            if (this.forms[this.formName].$invalid) {
                return;
            }
            this.setRunning();
            this.cvWalletService.updateEntry(this.userID, this.entry)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        deactivate(): void {
            const id = this.entry.ID;
            this.cvConfirmModalFactory(this.$translate(this.confirmDeactivateKey)).then(confirmed => {
                if (!confirmed) {
                    return;
                }
                this.setRunning();
                this.cvWalletService.deleteEntry(this.userID, { id: this.entry.ID }).then(success => {
                    if (!success) {
                        this.finishRunning(
                            true,
                            this.$translate("ui.admin.common.DeactivationFailed.Exclamation"));
                        return;
                    }
                    this.finishRunning();
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.updated, "deactivated", id);
                }).catch(reason => this.finishRunning(true, reason));
            });
        }
        edit(): void {
            this.viewstate.editMode = true;
        }
        cancel(): void {
            this.setRunning();
            if (this.viewstate.editMode) {
                this.viewstate.editMode = false;
                this.entry = angular.fromJson(this.originalObject);
            }
            if (this.viewstate.createMode) {
                this.viewstate.createMode = false;
            }
            this.finishRunning();
        }
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $q: ng.IQService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvWalletService: services.IWalletService,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super(cefConfig);
            this.originalObject = angular.toJson(this.entry);
            this.viewstate = {
                readOnly: this.readOnly || false,
                createMode: this.createMode || (this.entry && (this.entry as any).isNew) || false,
                editMode: this.editMode || false,
            };
        }
    }
}
