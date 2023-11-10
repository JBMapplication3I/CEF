/**
 * @file framework/admin/modals/walletModal.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc New wallet modal class
 */
module cef.admin.modals {
    export class WalletModalController extends core.TemplatedControllerBase {
        // Properties
        currentEntry: api.WalletModel;
        duplicateKey: boolean;
        type: string = "card";
        get entry(): api.WalletModel {
            return this.currentEntry;
        }
        set entry(newValue: api.WalletModel) {
            this.currentEntry = newValue;
        }
        get showTypeSelectors(): boolean {
            // TODO: As other wallet types are added, or the type of 'echeck'
            // replaces 'card' as default for a client, this logic will need
            // to change.
            return this.cefConfig.featureSet.payments.wallet.eCheck.enabled;
        }
        // Functions
        protected load(): void {
            if (this.existing) {
                // Clone by converting to json string and back, this way we can
                // cancel without editing original. Be sure to apply back on save
                // at the time of this modal's result.then(...) call
                this.type = "card"; // TODO: determine if echeck and use that type
                this.currentEntry = angular.fromJson(angular.toJson(this.existing));
            } else {
                this.newEntry();
            }
        }
        newEntry(): void {
            this.type = "card";
            this.typeChanged();
        }
        updateKeys(): void {
            // See if this account has the same name already
            this.cvWalletService.getWallet(this.userID).then(book => {
                this.duplicateKey = _.some(book,
                    e => e.Name === this.entry.Name);
                const msg = "This is a duplicate Entry Key.";
                if (!this.viewState.errorMessages) {
                    this.viewState.errorMessages = [];
                }
                if (this.duplicateKey) {
                    this.viewState.hasError = true;
                    if (!_.some(this.viewState.errorMessages, x => x === msg)) {
                        this.viewState.errorMessages.push(msg);
                    }
                    return;
                }
                while (_.some(this.viewState.errorMessages, x => x === msg)) {
                    const index = _.findIndex(this.viewState.errorMessages, msg);
                    this.viewState.errorMessages.splice(index, 1);
                }
                this.viewState.hasError = this.viewState.errorMessages.length > 0;
            });
        }
        // Events
        typeChanged(): void {
            this.currentEntry = this.type === "card"
                ? this.cvWalletService.getBlankCard()
                : this.cvWalletService.getBlankEcheck();
        }
        ok(): void {
            this.$uibModalInstance.close(this.currentEntry);
        }
        cancel(): void {
            this.$uibModalInstance.dismiss();
        }
        // Constructor
        constructor(
                protected readonly $timeout: ng.ITimeoutService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvWalletService: services.IWalletService,
                private readonly modalTitle: string, // Used by UI
                private readonly buttonTitle: string, // Used by UI
                private readonly idSuffix: string, // Used by UI
                protected readonly existing: api.WalletModel,
                protected readonly userID: number) {
            super(cefConfig);
            this.load();
        }
    }

    export interface IWalletModalFactory {
        (
            modalTitle: string | ng.IPromise<string>,
            buttonTitle: string | ng.IPromise<string>,
            userID: number,
            callback?: (result: api.WalletModel) => void,
            idSuffix?: string,
            existing?: api.WalletModel,
        ): ng.IPromise<api.WalletModel>;
    }

    export const cvWalletModalFactoryFn = (
            $uibModal: ng.ui.bootstrap.IModalService,
            $q: ng.IQService,
            $filter: ng.IFilterService,
            cvServiceStrings: services.IServiceStrings)
            : IWalletModalFactory =>
        (modalTitle: string | ng.IPromise<string>,
         buttonTitle: string | ng.IPromise<string>,
         userID: number,
         callback?: (result: api.WalletModel) => void,
         idSuffix?: string,
         existing?: api.WalletModel)
         : ng.IPromise<api.WalletModel> => {
            const modalFunc = (modalTtl: string, buttonTtl: string) => {
                return $uibModal.open({
                    templateUrl: $filter("corsLink")("/framework/admin/modals/walletModal.html", "ui"),
                    size: cvServiceStrings.modalSizes.md,
                    backdrop: "static",
                    controller: WalletModalController,
                    controllerAs: "wmCtrl",
                    resolve: {
                        modalTitle: () => modalTtl,
                        buttonTitle: () => buttonTtl,
                        userID: () => userID,
                        idSuffix: () => idSuffix,
                        existing: () => existing
                    }
                }).result.then(result => {
                    if (angular.isFunction(callback)) {
                        callback(result);
                    }
                    return result;
                });
            };
            if (angular.isFunction((modalTitle as ng.IPromise<string>).then)
                && angular.isFunction((buttonTitle as ng.IPromise<string>).then)) {
                let test1 = "Translation 1 Failed",
                    test2 = "Translation 2 Failed";
                return $q((resolve, __) => {
                    $q.when(modalTitle as ng.IPromise<string>).then(translated1 => {
                        test1 = translated1;
                    }).finally(() => {
                        $q.when(buttonTitle as ng.IPromise<string>).then(translated2 => {
                            test2 = translated2;
                        }).finally(() => {
                            resolve(modalFunc(test1, test2));
                        });
                    });
                });
            }
            if (angular.isFunction((modalTitle as ng.IPromise<string>).then)) {
                let test1 = "Translation 1 Failed";
                return $q((resolve, __) => {
                    $q.when(modalTitle as ng.IPromise<string>).then(translated => {
                        test1 = translated;
                    }).finally(() => resolve(modalFunc(test1, buttonTitle as string)));
                });
            }
            if (angular.isFunction((buttonTitle as ng.IPromise<string>).then)) {
                let test2 = "Translation 2 Failed";
                return $q((resolve, __) => {
                    $q.when(buttonTitle as ng.IPromise<string>).then(translated => {
                        test2 = translated;
                    }).finally(() => resolve(modalFunc(modalTitle as string, test2)));
                });
            }
            return modalFunc(modalTitle as string, buttonTitle as string);
        };

    adminApp.factory("cvWalletModalFactory", modals.cvWalletModalFactoryFn);
}
