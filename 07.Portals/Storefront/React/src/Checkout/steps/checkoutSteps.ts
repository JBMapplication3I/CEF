/**
 * @file framework/store/purchasing/steps/purchaseSteps.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Purchase items in the cart: The steps of the wizard
 */

import { CartModel } from "../../_api/cvApi._DtoClasses";
import { CEFConfig, IPurchaseStepConfig } from "../../_redux/_reduxTypes";
import { ServiceStrings } from "../../_shared/ServiceStrings";

export interface IPurchaseStep extends IPurchaseStepConfig {
  // Properties
  readonly name: string;
  /**
   * The text that would be on the button of the preceding step which would lead
   * to this step
   * @virtual
   * @memberof IPurchaseStep
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
  building: { [cartType: string]: boolean };
  // Functions
  /**
   * By default, no requirements should be checked, and just return a promise with
   * value of true
   * @virtual
   * @param {CartModel} cart - The cart which may have data that affects
   *                                   how the step is validated
   * @returns {Promise<boolean>}
   * @memberof IPurchaseStep
   */
  canEnable(cart: CartModel): Promise<boolean>;
  /**
   * By default, no action is taken, and just return a promise with value of true
   * @virtual
   * @param {CartModel} cart - The cart which may have data that affects
   *                                   how the step is validated
   * @returns {Promise<boolean>}
   * @memberof IPurchaseStep
   */
  initialize(cart: CartModel): Promise<boolean>;
  /**
   * By default, no actions should be required, but if the step needs to do
   * something as the user enters the step, the logic would be in here
   * @virtual
   * @param {string} cartType - The type name of the cart this triggers for
   * @returns {Promise<boolean>}
   * @memberof IPurchaseStep
   */
  activate(cartType: string): Promise<boolean>;
  /**
   * By default, no requirements should be checked, and just return a promise with
   * value of true
   * @param {CartModel} cart - The cart which may have data that affects
   *                                   how the step is validated
   * @returns {Promise<boolean>}
   * @memberof IPurchaseStep
   */
  validate(cart: CartModel): Promise<boolean>;
  submit(cartType: string): Promise<boolean>;
  // Events
  // <None>
}

export abstract class PurchaseStep implements IPurchaseStep {
  // Properties
  building: { [cartType: string]: boolean } = {};
  index: number;
  show: boolean;
  showButton: boolean;
  abstract get name(): string;
  private _invalid = true;
  get invalid(): boolean {
    return this._invalid;
  }
  set invalid(newValue: boolean) {
    this._invalid = newValue;
  }
  complete: boolean = false;
  get continueTextKey(): string {
    if (!this.name) {
      return undefined;
    }
    return this.cefConfig.purchase.sections[this.name].continueTextKey;
  }
  get titleKey(): string {
    if (!this.name) {
      return undefined;
    }
    return this.cefConfig.purchase.sections[this.name].titleKey;
  }
  get order(): number {
    if (!this.name) {
      return undefined;
    }
    return this.cefConfig.purchase.sections[this.name].order;
  }
  get templateURL(): string {
    if (!this.name) {
      return undefined;
    }
    return this.cefConfig.purchase.sections[this.name].templateURL;
  }
  // Functions
  canEnable(cart: CartModel): Promise<boolean> {
    const debug = `PurchaseStep.canEnable(cart: "${cart && cart.TypeName}")`;
    console.debug(debug);
    if (!this.name) {
      console.debug(`${debug} No name yet`);
      return new Promise((resolve, reject) => {
        reject(`${debug} does not have a 'name' yet`);
      });
    }
    // Do Nothing
    return new Promise((resolve, reject) => {
      resolve(
        this.cefConfig.purchase.sections[this.name] &&
          this.cefConfig.purchase.sections[this.name].show
      );
    });
  }
  initialize(cart: CartModel): Promise<boolean> {
    console.debug(`PurchaseStep.initialize(cart: "${cart && cart.TypeName}")`);
    this.building[cart.TypeName] = true;
    return new Promise((resolve, __) => {
      this.building[cart.TypeName] = false;
      resolve(true);
    });
  }
  activate(cartType: string): Promise<boolean> {
    console.debug(`PurchaseStep.activate(cartType: "${cartType}")`);
    // Do Nothing
    return new Promise((resolve, reject) => {
      resolve(true);
    });
  }
  validate(cart: CartModel): Promise<boolean> {
    console.debug(`PurchaseStep.validate(cart: "${cart && cart.TypeName}")`);
    // Do Nothing
    return new Promise((resolve, reject) => {
      resolve(true);
    });
  }
  submit(cartType: string): Promise<boolean> {
    console.debug(`PurchaseStep.submit(cartType: "${cartType}")`);
    // Do Nothing
    return new Promise((resolve, reject) => {
      resolve(true);
    });
  }
  protected fullReject(
    cartType: string,
    reject: (...args: any[]) => void | any,
    toReturn: any
  ): void {
    console.debug(`PurchaseStep.fullReject(cartType: "${cartType}", reject, toReturn)`);
    reject(toReturn);
    this.building[cartType] = false;
  }
  // Events
  // <None>
  // Constructor
  constructor(
    protected readonly cefConfig: CEFConfig,
    public readonly component: Element | JSX.Element
  ) {}
}

export const purchaseStepFactory = (cefConfig: CEFConfig): IPurchaseStep[] => {
  if (!cefConfig) {
    console.warn("Missing cefConfig in registrationStepFactory");
    return [];
  }
  return Object.keys(cefConfig.purchase.sections)
    .reduce((steps, current) => {
      let purchaseStepConfig = cefConfig.purchase.sections[current];
      if (cefConfig.debug) {
        console.log(
          "cefConfig.purchase.sections\n",
          cefConfig.purchase.sections,
          "\n",
          "current\n",
          current,
          "\n",
          "ServiceStrings.checkout.steps[current]\n",
          ServiceStrings.checkout.steps[current],
          "\n",
          "purchaseStepConfig\n",
          purchaseStepConfig
        );
      }
      if (purchaseStepConfig && purchaseStepConfig.show) {
        steps.push(purchaseStepConfig);
      }
      return steps;
    }, [])
    .sort((a: IPurchaseStep, b: IPurchaseStep): number => {
      return a.order - b.order;
    });
};
