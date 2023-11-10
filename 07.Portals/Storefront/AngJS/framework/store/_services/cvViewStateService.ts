/**
 * @file framework/store/_services/cvViewStateService.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc View State Service, provides a global storage for view states of
 * various directives to allow for consistent messaging locations and locking
 * UI during server-side calls.
 */
module cef.store.services {
    export interface IViewStateService {
        // Properties
        views: { [name: string]: core.ITemplatedControllerBase };
        // Functions
        isRunning(name?: string): boolean;
        setRunning(name: string, waitMessage?: core.IWaitMessageArg): void;
        hasWaitMessage(name?: string): boolean;
        waitMessage(name: string): string;
        finishRunning(name: string, hasError?: boolean, errorMessage?: core.IErrorMessageArg, errorMessages?: string[]): void;
        hasError(name?: string): boolean;
        errorMessages(name?: string): string[];
        processCEFActionResponseMessages(r: api.CEFActionResponse): void;
    }

    export class ViewStateService implements IViewStateService {
        consoleDebug(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.debug(...args);
        }
        consoleLog(...args: any[]) {
            if (!this.cefConfig.debug) { return; }
            console.log(...args);
        }
        // Properties
        views: { [name: string]: core.ITemplatedControllerBase } = { };
        // Functions
        isRunning(name?: string): boolean {
            this.generateIfMissing(name);
            if (name) {
                return this.views[name].viewState.running;
            }
            return Object.keys(this.views).some(n => this.views[n].viewState.running);
        }
        setRunning(name: string, waitMessage?: core.IWaitMessageArg): void {
            if (!name) { return; }
            this.generateIfMissing(name);
            this.views[name].setRunning(waitMessage);
        }
        hasWaitMessage(name?: string): boolean {
            this.generateIfMissing(name);
            if (name) {
                return !angular.isUndefined(this.views[name].viewState.waitMessage)
                    && this.views[name].viewState.waitMessage != null
                    && this.views[name].viewState.waitMessage !== "";
            }
            return Object.keys(this.views).some(n => !angular.isUndefined(this.views[n].viewState.waitMessage)
                && this.views[n].viewState.waitMessage != null
                && this.views[n].viewState.waitMessage !== "");
        }
        waitMessage(name: string): string {
            if (!name) { return null; }
            this.generateIfMissing(name);
            return this.views[name].viewState.waitMessage;
        }
        finishRunning(name: string, hasError?: boolean, errorMessage?: core.IErrorMessageArg, errorMessages?: string[]): void {
            if (!name) { return; }
            this.generateIfMissing(name);
            this.views[name].finishRunning(hasError, errorMessage, errorMessages);
        }
        hasError(name?: string): boolean {
            this.generateIfMissing(name);
            if (name) {
                return this.views[name].viewState.hasError;
            }
            return Object.keys(this.views).some(n => this.views[n].viewState.hasError);
        }
        errorMessages(name?: string): string[] {
            this.generateIfMissing(name);
            if (name) {
                return this.views[name].viewState.errorMessages || [];
            }
            const messages = new Array<string>();
            Object.keys(this.views).forEach(n => {
                if (this.views[n].viewState.errorMessage) {
                    messages.push(this.views[n].viewState.errorMessage);
                }
                if (this.views[n].viewState.errorMessages && this.views[n].viewState.errorMessages.length) {
                    messages.concat(this.views[n].viewState.errorMessage);
                }
            });
            return messages;
        }
        private generateIfMissing(name: string): void {
            if (!name) { return; }
            if (!this.views[name]) {
                this.views[name] = new core.TemplatedControllerBase(this.cefConfig);
            }
        }
        processCEFActionResponseMessages(r: api.CEFActionResponse): void {
            if (!r || !r.Messages || r.Messages.length <= 0) { return; }
            r.Messages.forEach(x => {
                if (x.indexOf("ERROR") !== -1) {
                    console.error(x);
                } else if (x.indexOf("WARNING") !== -1) {
                    console.warn(x);
                } else {
                    this.consoleLog(x);
                }
            });
        }
        // Constructor
        constructor(private readonly cefConfig: core.CefConfig) { }
    }
}
