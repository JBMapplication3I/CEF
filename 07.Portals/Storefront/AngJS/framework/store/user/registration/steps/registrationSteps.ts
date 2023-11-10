/**
 * @file framework/store/user/registration/steps/registrationSteps.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Register for the site: The steps of the wizard
 */
module cef.store.user.registration.steps {
    export interface IRegistrationStep extends core.ITemplatedControllerBase {
        // Properties
        readonly name: string;
        /**
         * The text that would be on the button of the preceding step which would lead
         * to this step
         * @virtual
         * @memberof IRegistrationStep
         */
        readonly continueTextKey: string;
        readonly titleKey: string;
        invalid: boolean;
        complete: boolean;
        readonly order: number;
        readonly index: number;
        readonly templateURL: string;
        building: boolean;
        // Functions
        /**
         * By default, no requirements should be checked, and just return a promise with
         * value of true
         * @virtual
         * @returns {ng.IPromise<boolean>}
         * @memberof IRegistrationStep
         */
        canEnable(): ng.IPromise<boolean>;
        /**
         * By default, no action is taken, and just return a promise with value of true
         * @virtual
         * @returns {ng.IPromise<boolean>}
         * @memberof IRegistrationStep
         */
        initialize(): ng.IPromise<boolean>;
        /**
         * By default, no requirements should be checked, and just return a promise with
         * value of true
         * @returns {ng.IPromise<boolean>}
         * @memberof IRegistrationStep
         */
        validate(): ng.IPromise<boolean>;
        submit(): ng.IPromise<boolean>;
        // Events
        // <None>
    }

    export abstract class RegistrationStep extends core.TemplatedControllerBase implements IRegistrationStep {
        // Properties
        building: boolean = true;
        index: number;
        abstract get name(): string;
        invalid: boolean = true;
        complete: boolean = false;
        get continueTextKey(): string {
            if (!this.name) { return undefined; }
            return this.cefConfig.register.sections[this.name].continueTextKey;
        }
        get titleKey(): string {
            if (!this.name) { return undefined; }
            return this.cefConfig.register.sections[this.name].titleKey;
        }
        get order(): number {
            if (!this.name) { return undefined; }
            return this.cefConfig.register.sections[this.name].order;
        }
        get templateURL(): string {
            if (!this.name) { return undefined; }
            return this.cefConfig.register.sections[this.name].templateURL;
        }
        // Functions
        canEnable(): ng.IPromise<boolean> {
            this.consoleDebug(`RegistrationStep.canEnable()`);
            if (!this.name) {
                this.consoleDebug(`RegistrationStep.canEnable() No name yet`);
                return this.$q.reject(`RegistrationStep.canEnable() does not have a 'name' yet`);
            }
            // Do Nothing
            return this.$q.resolve(
                this.cefConfig.register.sections[this.name] &&
                this.cefConfig.register.sections[this.name].show);
        }
        initialize(): ng.IPromise<boolean> {
            this.consoleDebug(`RegistrationStep.initialize()`);
            this.building = true;
            return this.$q((resolve, __) => {
                this.building = false;
                resolve(true);
            });
        }
        validate(): ng.IPromise<boolean> {
            this.consoleDebug(`RegistrationStep.validate()`);
            // Do Nothing
            return this.$q.resolve(true);
        }
        submit(): ng.IPromise<boolean> {
            this.consoleDebug(`RegistrationStep.submit()`);
            // Do Nothing
            return this.$q.resolve(true);
        }
        protected fullReject(reject: (...args: any[]) => void | any, toReturn: any): void {
            this.consoleDebug(`RegistrationStep.fullReject(reject, toReturn)`);
            reject(toReturn);
            this.building = false;
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