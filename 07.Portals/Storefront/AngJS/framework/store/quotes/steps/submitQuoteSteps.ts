/**
 * @file framework/store/quotes/steps/submitQuoteeSteps.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Submit Quote items in the quote cart: The steps of the wizard
 */
module cef.store.quotes.steps {
    export interface ISubmitQuoteStep extends core.ITemplatedControllerBase {
        // Properties
        readonly name: string;
        /**
         * The text that would be on the button of the preceding step which would lead
         * to this step
         * @virtual
         * @memberof ISubmitQuoteStep
         */
        readonly continueTextKey: string;
        readonly titleKey: string;
        invalid: boolean;
        complete: boolean;
        readonly order: number;
        readonly index: number;
        readonly templateURL: string;
        building: { [cartType: string]: boolean };
        // Functions
        /**
         * By default, no requirements should be checked, and just return a promise with
         * value of true
         * @virtual
         * @param {api.CartModel} cart - The cart which may have data that affects
         *                               how the step is validated
         * @returns {ng.IPromise<boolean>}
         * @memberof ISubmitQuoteStep
         */
        canEnable(cart: api.CartModel): ng.IPromise<boolean>;
        /**
         * By default, no action is taken, and just return a promise with value of true
         * @virtual
         * @param {api.CartModel} cart - The cart which may have data that affects
         *                               how the step is validated
         * @returns {ng.IPromise<boolean>}
         * @memberof ISubmitQuoteStep
         */
        initialize(cart: api.CartModel): ng.IPromise<boolean>;
        /**
         * By default, no actions should be required, but if the step needs to do
         * something as the user enters the step, the logic would be in here
         * @virtual
         * @param {string} cartType - The type name of the cart this triggers for
         * @returns {ng.IPromise<boolean>}
         * @memberof IPurchaseStep
         */
         activate(cartType: string): ng.IPromise<boolean>;
         /**
         * By default, no requirements should be checked, and just return a promise with
         * value of true
         * @param {api.CartModel} cart - The cart which may have data that affects
         *                               how the step is validated
         * @returns {ng.IPromise<boolean>}
         * @memberof ISubmitQuoteStep
         */
        validate(cart: api.CartModel): ng.IPromise<boolean>;
        submit(cartType: string): ng.IPromise<boolean>;
        // Events
        // <None>
    }

    export abstract class SubmitQuoteStep extends core.TemplatedControllerBase implements ISubmitQuoteStep {
        // Properties
        building: { [cartType: string]: boolean } = { };
        index: number;
        abstract get name(): string;
        private _invalid = true;
        get invalid(): boolean { return this._invalid; }
        set invalid(newValue: boolean) { this._invalid = newValue; }
        complete: boolean = false;
        get continueTextKey(): string {
            if (!this.name) { return undefined; }
            return this.cefConfig.submitQuote.sections[this.name].continueTextKey;
        }
        get titleKey(): string {
            if (!this.name) { return undefined; }
            return this.cefConfig.submitQuote.sections[this.name].titleKey;
        }
        get order(): number {
            if (!this.name) { return undefined; }
            return this.cefConfig.submitQuote.sections[this.name].order;
        }
        get templateURL(): string {
            if (!this.name) { return undefined; }
            return this.cefConfig.submitQuote.sections[this.name].templateURL;
        }
        // Functions
        canEnable(cart: api.CartModel): ng.IPromise<boolean> {
            const debug = `SubmitQuoteStep.canEnable(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debug);
            if (!this.name) {
                this.consoleDebug(`${debug} No name yet`);
                return this.$q.reject(`${debug} does not have a 'name' yet`);
            }
            // Do Nothing
            return this.$q.resolve(
                this.cefConfig.submitQuote.sections[this.name]
                && this.cefConfig.submitQuote.sections[this.name].show);
        }
        initialize(cart: api.CartModel): ng.IPromise<boolean> {
            this.consoleDebug(`SubmitQuoteStep.initialize(cart: "${cart && cart.TypeName}")`);
            this.building[cart.TypeName] = true;
            return this.$q((resolve, __) => {
                this.building[cart.TypeName] = false;
                resolve(true);
            });
        }
        activate(cartType: string): ng.IPromise<boolean> {
            this.consoleDebug(`SubmitQuoteStep.activate(cartType: "${cartType}")`);
            // Do Nothing
            return this.$q.resolve(true);
        }
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            this.consoleDebug(`SubmitQuoteStep.validate(cart: "${cart && cart.TypeName}")`);
            // Do Nothing
            return this.$q.resolve(true);
        }
        submit(cartType: string): ng.IPromise<boolean> {
            this.consoleDebug(`SubmitQuoteStep.submit(cartType: "${cartType}")`);
            // Do Nothing
            return this.$q.resolve(true);
        }
        protected fullReject(
            cartType: string,
            reject: (...args: any[]) => void | any,
            toReturn: any
        ): void {
            this.consoleDebug(`SubmitQuoteStep.fullReject(cartType: "${cartType}", reject, toReturn)`);
            reject(toReturn);
            this.building[cartType] = false;
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
        }
    }
}
