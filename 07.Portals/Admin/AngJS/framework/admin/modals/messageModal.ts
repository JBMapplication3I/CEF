/**
 * @file framework/admmin/modals/messageModal.ts
 * @desc Message modal class
 */
module cef.admin.modals {
    export class MessageModalController extends core.TemplatedControllerBase {
        // Properties
        displayMessage: string;
        get headerKey(): string {
            return "ui.admin.common.Message";
        }
        get okKey(): string {
            return "ui.admin.common.OK";
        }
        // Functions
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

    export interface IMessageModalFactory {
        (message: string | ng.IPromise<string>, size?: string, callback?: () => void): ng.IPromise<void>;
    }

    export const cvMessageModalFactoryFn = (
        $uibModal: ng.ui.bootstrap.IModalService,
        $q: ng.IQService,
        $filter: ng.IFilterService)
        : IMessageModalFactory =>
        (message: string | ng.IPromise<string>, size: string = "md", callback?: () => void): ng.IPromise<void> => {
            const modalFunc = (msg: string) => $q<void>(resolve => {
                $uibModal.open({
                    templateUrl: $filter("corsLink")("/framework/admin/modals/messageModal.html", "ui"),
                    size: size,
                    controller: MessageModalController,
                    controllerAs: "messageModalCtrl",
                    resolve: {
                        message: () => msg
                    }
                }).result.finally(() => {
                    if (angular.isFunction(callback)) {
                        callback();
                    }
                    resolve();
                });
            });
            if (angular.isFunction((message as ng.IPromise<string>).then)) {
                return $q.when<void>(
                    message as any,
                    translated => modalFunc(translated),
                    result => console.warn(result));
            }
            return modalFunc(message as string);
        };

    adminApp.factory("cvMessageModalFactory", modals.cvMessageModalFactoryFn);
}
