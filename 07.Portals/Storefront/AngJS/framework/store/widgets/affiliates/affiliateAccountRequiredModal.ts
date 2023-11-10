module cef.store.widgets.affiliates {
    class AffiliateAccountRequiredModalController extends core.TemplatedControllerBase {
        selectedAccountKey: string = null;
        ok(): void {
            this.$uibModalInstance.close(this.selectedAccountKey);
        }
        cancel(): void {
            this.$uibModalInstance.dismiss();
        }
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig,
                private readonly accessibleAccounts: Array<{ value: string, label: string }>) { // Used by UI
            super(cefConfig);
        }
    }

    export interface IAffiliateAccountSelectorModalFactory {
        (
            accessibleAccounts: Array<{ value: string, label: string }>,
            callback?: (result: string) => void
        ): ng.IPromise<string>;
    }

    export const cvAffiliateAccountSelectorModalFactoryFn = (
        $filter: ng.IFilterService,
        $uibModal: ng.ui.bootstrap.IModalService,
        cvServiceStrings: services.IServiceStrings
    ): IAffiliateAccountSelectorModalFactory => {
        return (
            accessibleAccounts?: Array<{ value: string, label: string }>,
            callback?: (result: string) => void
        ): ng.IPromise<string> => {
            const modalFunc = () => {
                return $uibModal.open({
                    templateUrl: $filter("corsLink")("/framework/store/widgets/affiliates/affiliateAccountRequiredModal.html", "ui"),
                    size: cvServiceStrings.modalSizes.sm,
                    backdrop: "static",
                    controller: AffiliateAccountRequiredModalController,
                    controllerAs: "aarmCtrl",
                    resolve: {
                        accessibleAccounts: () => accessibleAccounts,
                    }
                }).result.then(result => {
                    if (angular.isFunction(callback)) {
                        callback(result);
                    }
                    return result;
                });
            };
            return modalFunc();
        };
    }

    cefApp.factory("cvAffiliateAccountSelectorModalFactory", cvAffiliateAccountSelectorModalFactoryFn);
}
