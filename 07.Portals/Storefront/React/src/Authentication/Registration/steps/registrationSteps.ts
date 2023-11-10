/**
 * @file framework/store/user/registration/steps/registrationSteps.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Register for the site: The steps of the wizard
 */

import { CEFConfig, IRegistrationStepConfig } from "../../../_redux/_reduxTypes";
import { ServiceStrings } from "../../../_shared/ServiceStrings";

export interface IRegistrationStep extends IRegistrationStepConfig {
  // Properties
  readonly name: string;
  /**
   * The text that would be on the button of the preceding step which would lead
   * to this step
   * @virtual
   * @memberof IRegistrationStep
   */
  readonly continueTextKey: string;
  readonly continueText?: string;
  readonly titleKey: string;
  invalid: boolean;
  complete: boolean;
  readonly order: number;
  readonly index: number;
  readonly templateURL: string;
  component: Element | JSX.Element;
  building: boolean;
  // Functions
  /**
   * By default, no requirements should be checked, and just return a promise with
   * value of true
   * @virtual
   * @returns {Promise<boolean>}
   * @memberof IRegistrationStep
   */
  canEnable(): Promise<boolean>;
  /**
   * By default, no action is taken, and just return a promise with value of true
   * @virtual
   * @returns {Promise<boolean>}
   * @memberof IRegistrationStep
   */
  initialize(): Promise<boolean>;
  /**
   * By default, no requirements should be checked, and just return a promise with
   * value of true
   * @returns {Promise<boolean>}
   * @memberof IRegistrationStep
   */
  validate(): Promise<boolean>;
  submit(): Promise<boolean>;
  // Events
  // <None>
}

export abstract class RegistrationStep implements IRegistrationStep {
  // Properties
  building: boolean = true;
  index: number;
  show: boolean;
  showButton: boolean;
  abstract get name(): string;
  invalid: boolean = true;
  complete: boolean = false;
  get continueTextKey(): string {
    if (!this.name) {
      return undefined;
    }
    return this.cefConfig.register.sections[this.name].continueTextKey;
  }
  get titleKey(): string {
    if (!this.name) {
      return undefined;
    }
    return this.cefConfig.register.sections[this.name].titleKey;
  }
  get order(): number {
    if (!this.name) {
      return undefined;
    }
    return this.cefConfig.register.sections[this.name].order;
  }
  get templateURL(): string {
    if (!this.name) {
      return undefined;
    }
    return this.cefConfig.register.sections[this.name].templateURL;
  }
  // Functions
  canEnable(): Promise<boolean> {
    console.debug(`RegistrationStep.canEnable()`);
    if (!this.name) {
      console.debug(`RegistrationStep.canEnable() No name yet`);
      return new Promise((resolve, reject) => {
        reject(`RegistrationStep.canEnable() does not have a 'name' yet`);
      });
    }
    // Do Nothing
    return new Promise((resolve, reject) => {
      resolve(
        this.cefConfig.register.sections[this.name] &&
          this.cefConfig.register.sections[this.name].show
      );
    });
  }
  initialize(): Promise<boolean> {
    console.debug(`RegistrationStep.initialize()`);
    this.building = true;
    return new Promise((resolve, __) => {
      this.building = false;
      resolve(true);
    });
  }
  validate(): Promise<boolean> {
    console.debug(`RegistrationStep.validate()`);
    // Do Nothing
    return new Promise((resolve, reject) => {
      resolve(true);
    });
  }
  submit(): Promise<boolean> {
    console.debug(`RegistrationStep.submit()`);
    // Do Nothing
    return new Promise((resolve, reject) => {
      resolve(true);
    });
  }
  protected fullReject(reject: (...args: any[]) => void | any, toReturn: any): void {
    console.debug(`RegistrationStep.fullReject(reject, toReturn)`);
    reject(toReturn);
    this.building = false;
  }
  // Events
  // <None>
  // Constructor
  constructor(
    protected readonly cefConfig: CEFConfig,
    public readonly component: Element | JSX.Element
  ) {}
}

export const registrationStepFactory = (cefConfig: CEFConfig): IRegistrationStep[] => {
  if (!cefConfig) {
    console.warn("Missing cefConfig in registrationStepFactory");
    return [];
  }
  return Object.keys(cefConfig.register.sections)
    .reduce((steps, current) => {
      let registrationStepConfig = cefConfig.register.sections[current];
      if (cefConfig.debug) {
        console.log(
          "cefConfig.register.sections\n",
          cefConfig.register.sections,
          "\n",
          "current\n",
          current,
          "\n",
          "ServiceStrings.registration.steps[current]\n",
          ServiceStrings.registration.steps[current],
          "\n",
          "registrationStepConfig\n",
          registrationStepConfig
        );
      }
      if (registrationStepConfig && registrationStepConfig.show) {
        steps.push(registrationStepConfig);
      }
      return steps;
    }, [])
    .sort((a: IRegistrationStep, b: IRegistrationStep): number => {
      return a.order - b.order;
    });
};
