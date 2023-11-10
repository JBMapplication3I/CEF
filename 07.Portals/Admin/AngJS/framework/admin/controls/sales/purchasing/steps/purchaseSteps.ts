/**
 * @file framework/admin/purchasing/steps/purchaseSteps.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */
module cef.admin.purchasing.steps {
    export interface IPurchaseStep extends core.ITemplatedControllerBase {
        // Properties
        readonly name: string;
        /**
         * The text that would be on the button of the preceding step which would lead
         * to this step
         * @virtual
         * @memberof IPurchaseStep
         */
        readonly continueTextKey: string;
        readonly titleKey: string;
        invalid: boolean;
        complete: boolean;
        readonly order: number;
        readonly index: number;
        readonly templateURL: string;
        building: { [lookupKey: string]: boolean };
        // Functions
        /**
         * By default, no requirements should be checked, and just return a promise with
         * value of true
         * @virtual
         * @param {api.CartModel} cart - The cart which may have data that affects
         *                                   how the step is validated
         * @returns {ng.IPromise<boolean>}
         * @memberof IPurchaseStep
         */
        canEnable(cart: api.CartModel): ng.IPromise<boolean>;
        /**
         * By default, no action is taken, and just return a promise with value of true
         * @virtual
         * @param {api.CartModel} cart - The cart which may have data that affects
         *                                   how the step is validated
         * @returns {ng.IPromise<boolean>}
         * @memberof IPurchaseStep
         */
        initialize(cart: api.CartModel): ng.IPromise<boolean>;
        /**
         * By default, no actions should be required, but if the step needs to do
         * something as the user enters the step, the logic would be in here
         * @virtual
         * @param {api.CartByIDLookupKey} lookupKey The lookup key to identify the appropriate cart
         * @returns {ng.IPromise<boolean>}
         * @memberof IPurchaseStep
         */
        activate(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean>;
        /**
         * By default, no requirements should be checked, and just return a promise with
         * value of true
         * @param {api.CartModel} cart - The cart which may have data that affects
         *                                   how the step is validated
         * @returns {ng.IPromise<boolean>}
         * @memberof IPurchaseStep
         */
        validate(cart: api.CartModel): ng.IPromise<boolean>;
        submit(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean>;
        // Events
        // <None>
    }

    export abstract class PurchaseStep extends core.TemplatedControllerBase implements IPurchaseStep {
        // Properties
        building: { [lookupKey: string]: boolean } = { };
        index: number;
        abstract get name(): string;
        private _invalid = true;
        get invalid(): boolean { return this._invalid; }
        set invalid(newValue: boolean) { this._invalid = newValue; }
        complete: boolean = false;
        get continueTextKey(): string {
            if (!this.name) { return undefined; }
            return this.cefConfig.purchase.sections[this.name].continueTextKey;
        }
        get titleKey(): string {
            if (!this.name) { return undefined; }
            return this.cefConfig.purchase.sections[this.name].titleKey;
        }
        get order(): number {
            if (!this.name) { return undefined; }
            return this.cefConfig.purchase.sections[this.name].order;
        }
        get templateURL(): string {
            if (!this.name) { return undefined; }
            return this.cefConfig.purchase.sections[this.name].templateURL;
        }
        // Functions
        canEnable(cart: api.CartModel): ng.IPromise<boolean> {
            const debug = `PurchaseStep.canEnable(cart: "${cart && cart.TypeName}")`;
            this.consoleDebug(debug);
            if (!this.name) {
                this.consoleDebug(`${debug} No name yet`);
                return this.$q.reject(`${debug} does not have a 'name' yet`);
            }
            // Do Nothing
            return this.$q.resolve(
                this.cefConfig.purchase.sections[this.name]
                && this.cefConfig.purchase.sections[this.name].show);
        }
        initialize(cart: api.CartModel): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStep.initialize(cart: "${cart && cart.TypeName}")`);
            this.building[cart.TypeName] = true;
            return this.$q((resolve, __) => {
                this.building[cart.TypeName] = false;
                resolve(true);
            });
        }
        activate(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStep.activate(lookupKey: "${lookupKey.toString()}")`);
            // Do Nothing
            return this.$q.resolve(true);
        }
        validate(cart: api.CartModel): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStep.validate(cart: "${cart && cart.TypeName}")`);
            // Do Nothing
            return this.$q.resolve(true);
        }
        submit(lookupKey: api.CartByIDLookupKey): ng.IPromise<boolean> {
            this.consoleDebug(`PurchaseStep.submit(lookupKey: "${lookupKey.toString()}")`);
            // Do Nothing
            return this.$q.resolve(true);
        }
        protected fullReject(
            lookupKey: api.CartByIDLookupKey,
            reject: (...args: any[]) => void | any,
            toReturn: any
        ): void {
            this.consoleDebug(`PurchaseStep.fullReject(lookupKey: "${lookupKey.toString()}", reject, toReturn)`);
            reject(toReturn);
            this.building[lookupKey.toString()] = false;
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
