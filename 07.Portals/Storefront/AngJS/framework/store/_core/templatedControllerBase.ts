/**
 * @file framework/store/_core/templatedControllerBase.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Templated controller base class.
 */
module cef.store.core {
    export type IWaitMessageArg = string | ng.IPromise<string>;
    export type IErrorMessageArg = string | ng.IPromise<string> | ng.IHttpPromiseCallbackArg<ResponseError>;

    export interface ITemplatedControllerBase {
        transclude: boolean;
        templateUrl: string;
        classes?: string;
        viewState: {
            running?: boolean;
            success?: boolean;
            hasError?: boolean;
            errorMessage?: string;
            errorMessages?: string[];
            waitMessage?: string;
        };
        forms: { [form: string]: ng.IFormController; };
        setRunning(waitMessage?: IWaitMessageArg): void;
        finishRunning(hasError: boolean, errorMessage?: IErrorMessageArg, errorMessages?: string[]): void;
        //setError(message: string, messages: string[]): void;
        //clearError(): void;
        //clearSuccess(): void;
        //processCEFActionResponseMessages(response: api.CEFActionResponse): void;
    }

    export class ResponseStatus {
        ErrorCode: string;
        Errors: string[] = [];
        Message: string;
        StackTrace: string;
    }

    export class ResponseError {
        ResponseStatus: ResponseStatus;
    }

    export class TemplatedControllerBase implements ITemplatedControllerBase {
        // ITemplateController Properties
        transclude: boolean;
        templateUrl: string;
        classes: string;
        // TemplatedControllerBase properties
        viewState = {
            running: false,
            success: false,
            hasError: false,
            errorMessage: null as string,
            errorMessages: null as string[],
            waitMessage: null as string
        };
        forms: { [form: string]: ng.IFormController; };
        debounce500 = <ng.INgModelOptions>{ // ng-model-options
            updateOn: "default blur",
            debounce: { default: 500, blur: 0 },
            allowInvalid: false
        };
        // Functions
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        consoleError(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.error(...args);
        }
        setRunning(waitMessage: IWaitMessageArg = null): void {
            if (!this.viewState) {
                this.viewState = { } as any;
            }
            this.viewState.running = true;
            this.clearSuccess();
            this.clearError();
            if (!waitMessage) {
                return;
            }
            if (angular.isFunction(waitMessage["then"])) {
                // Only apply if it is still running
                (waitMessage as ng.IPromise<string>)
                    .then(value => this.viewState.waitMessage = this.viewState.running ? value : null);
                return;
            }
            this.viewState.waitMessage = waitMessage as string;
        }
        finishRunning(hasError: boolean = false, errorMessage: IErrorMessageArg = null, errorMessages: string[] = null): void {
            this.viewState.running = false;
            this.viewState.waitMessage = null;
            if (hasError) {
                this.setError(errorMessage, errorMessages);
                return;
            }
            this.clearError();
            this.viewState.success = true;
        }
        private setError(message: IErrorMessageArg, messages: string[]): void {
            this.clearSuccess();
            this.viewState.hasError = true;
            this.viewState.success = false;
            this.viewState.errorMessages = messages;
            if (!message) {
                if (messages && messages.length) {
                    this.viewState.errorMessage = messages[0];
                    this.viewState.errorMessages = messages;
                    this.consoleDebug(this.viewState.errorMessage);
                }
                return;
            }
            if (angular.isFunction(message["then"])) {
                (message as ng.IPromise<string>).then(value => this.viewState.errorMessage = value);
                return;
            }
            if ((message as ng.IHttpPromiseCallbackArg<ResponseError>).status) {
                const asCallbackArg = (message as ng.IHttpPromiseCallbackArg<ResponseError>);
                this.viewState.errorMessage = asCallbackArg.status // 401
                    + ": " + asCallbackArg.statusText // "Unauthorized"
                    + (!asCallbackArg.data || !asCallbackArg.data.ResponseStatus || !asCallbackArg.data.ResponseStatus.Message
                        ? angular.toJson(asCallbackArg)
                        :  ": " + asCallbackArg.data.ResponseStatus.Message); // "No active user in session."
                return;
            }
            this.viewState.errorMessage = message as string;
            this.consoleDebug(this.viewState.errorMessage);
        }
        private clearError(): void {
            this.viewState.hasError = false;
            this.viewState.errorMessage = null;
            this.viewState.errorMessages = null;
        }
        private clearSuccess(): void {
            this.viewState.success = false;
        }
        // Constructor
        constructor(protected readonly cefConfig: CefConfig) { }
    }
}
