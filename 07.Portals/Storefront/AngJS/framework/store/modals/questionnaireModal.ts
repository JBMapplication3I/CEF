/**
 * @file framework/store/modals/questionnaireModal.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Confirm modal class
 */
 module cef.store.modals {
    export class QuestionnaireModalController extends core.TemplatedControllerBase {
        // Properties
        displayMessage: string;
        get headerKey(): string {
            return "ui.storefront.medicalQuestionnaire.PleaseAnswer";
        }
        get cancelKey(): string {
            return "ui.storefront.common.Cancel";
        }
        // Functions
        onQuestionnaireComplete = (answers: api.AnswerModel[]): void => {
            this.cvApi.questionnaire.SecureCreateAnswers({ Answers: answers }).then(r => {
                if (r.data.ActionSucceeded) {
                    this.$uibModalInstance.close(true);
                    return;
                }
                // TODO: Properly handle the error
                this.consoleLog(r.data.Messages);
            }).catch(err => {
                this.consoleLog(err);
            })
        }
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
                protected readonly cvApi: api.ICEFAPI,
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

    export interface IQuestionnaireModalFactory {
        (message: string | ng.IPromise<string>, callback?: (result: boolean|string) => void): ng.IPromise<boolean>;
    }

    export const cvQuestionnaireModalFactoryFn = ($uibModal: ng.ui.bootstrap.IModalService, $q: ng.IQService, $filter: ng.IFilterService): IQuestionnaireModalFactory =>
        (message: string | ng.IPromise<string>, callback?: (result: boolean | string) => void): ng.IPromise<boolean> => {
            const modalFunc = (msg: string) => {
                return $uibModal.open({
                    templateUrl: $filter("corsLink")("/framework/store/modals/questionnaireModal.html", "ui"),
                    size: "lg",
                    controller: QuestionnaireModalController,
                    controllerAs: "questionnaireModalCtrl",
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

    cefApp.factory("cvQuestionnaireModalFactory", modals.cvQuestionnaireModalFactoryFn);
}
