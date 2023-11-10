/**
 * @file framework/admin/modals/confirmModal.ts
 * @desc Confirm modal class
 */
module cef.admin.modals {
    export class ConfirmModalController extends core.TemplatedControllerBase {
        // Properties
        displayMessage: string;
        get headerKey(): string {
            return "ui.admin.common.PleaseConfirm";
        }
        get yesKey(): string {
            return "ui.admin.common.Yes";
        }
        get noKey(): string {
            return "ui.admin.common.No";
        }
        // Functions
        ok(): void {
            this.$uibModalInstance.close(true);
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                readonly message: string | ng.IPromise<string>) {
            super(cefConfig);
            if (angular.isFunction(message["then"])) {
                (message as ng.IPromise<string>).then(result => this.displayMessage = result);
            } else {
                this.displayMessage = message as string;
            }
        }
    }

    export interface IConfirmModalFactory {
        (message: string | ng.IPromise<string>, callback?: (result: boolean|string) => void): ng.IPromise<boolean>;
    }

    export const cvConfirmModalFactoryFn = (
            $uibModal: ng.ui.bootstrap.IModalService,
            $q: ng.IQService,
            $filter: ng.IFilterService,
            cvServiceStrings: services.IServiceStrings
            ): IConfirmModalFactory =>
        (message: string | ng.IPromise<string>, callback?: (result: boolean | string) => void): ng.IPromise<boolean> => {
            const modalFunc = (msg: string) => {
                return $uibModal.open({
                    templateUrl: $filter("corsLink")("/framework/admin/modals/confirmModal.html", "ui"),
                    size: cvServiceStrings.modalSizes.md,
                    controller: ConfirmModalController,
                    controllerAs: "confirmModalCtrl",
                    resolve: {
                        message: () => msg
                    }
                }).result.then(result => {
                    if (angular.isFunction(callback)) {
                        callback(result);
                    }
                    return result;
                });
            };
            if (angular.isFunction((message as ng.IPromise<string>).then)) {
                return $q.when(message as ng.IPromise<string>)
                    .then(translated => modalFunc(translated));
            }
            return modalFunc(message as string);
        };

    adminApp.factory("cvConfirmModalFactory", modals.cvConfirmModalFactoryFn);
}
