import { IconDefinition } from "@fortawesome/fontawesome-common-types";
import {
  MouseEventHandler,
  FocusEventHandler,
  KeyboardEventHandler,
  ChangeEventHandler
} from "react";
import {
  UseFormRegister,
  FieldValues,
  FieldError,
  DeepMap
} from "react-hook-form";

interface IFormGroupProps<T> {
  register: UseFormRegister<any>;
  errors: DeepMap<FieldValues, FieldError>;
  formIdentifier: string;
  formClasses?: string;
  labelKey?: string;
  labelText?: string;
  tooltipKey?: string;
  tooltipText?: string;
  disabled?: boolean;
  showValidTooltip?: boolean;
  hideInvalidTooltip?: boolean;
  failsOn?: boolean;
  forceTooltipWithFocus?: boolean;
  required?: boolean;
  requiredMessage?: string;
  onBlur?: FocusEventHandler<T>;
  onFocus?: FocusEventHandler<T>;
  onKeyDown?: KeyboardEventHandler<T>;
  onKeyUp?: KeyboardEventHandler<T>;
  onMouseDown?: MouseEventHandler<T>;
  onMouseEnter?: MouseEventHandler<T>;
  onMouseLeave?: MouseEventHandler<T>;
  onMouseMove?: MouseEventHandler<T>;
  onMouseOver?: MouseEventHandler<T>;
  onMouseUp?: MouseEventHandler<T>;
  extraOnChange?: ChangeEventHandler<T>;
}

type TCheckboxOption = {
  identifier: string;
  labelKey: string;
  labelText: string;
  required?: boolean;
  requiredMessage?: string;
};
export interface ICheckboxFormGroupProps
  extends IFormGroupProps<HTMLInputElement> {
  leftIcon?: IconDefinition;
  rightIcon?: IconDefinition;
  inputClass?: string;
  checkboxOptions: Array<TCheckboxOption>;
}

export interface IDateInputFormGroupProps
  extends IFormGroupProps<HTMLInputElement> {
  min?: string;
  minMessage?: string;
  max?: string;
  maxMessage?: string;
  placeholderKey?: string;
  placeholderText?: string;
  inputClass?: string;
  includeTime?: boolean;
}

export interface INumberInputFormGroupProps
  extends IFormGroupProps<HTMLInputElement> {
  inputClass?: string;
  startTouched?: boolean;
  min?: number;
  minMessage?: string;
  max?: number;
  maxMessage?: string;
  step?: number;
  precision?: number;
  isCurrency?: boolean;
  isPercent?: boolean;
  placeholderText?: string;
  placeholderKey?: string;
}

export interface IPasswordInputFormGroupProps
  extends IFormGroupProps<HTMLInputElement> {
  autoComplete?: string;
  inputClass?: string;
  startTouched?: boolean;
  placeholderKey?: string;
  placeholderText?: string;
  minLength?: number;
  minLengthMessage?: string;
  maxLength?: number;
  maxLengthMessage?: string;
  pattern?: RegExp;
  patternMessage?: string;
}

export interface IRadioFormGroupProps
  extends IFormGroupProps<HTMLInputElement> {
  radioOptions: Array<{ text: string }>;
  inputClass?: string;
}

export interface ISelectFormGroupProps
  extends IFormGroupProps<HTMLSelectElement> {
  options: Array<{ option: string; value?: string }>;
  initialOption?: { option: string; value?: string };
  autocomplete?: string;
  leftIcon?: IconDefinition;
  rightIcon?: IconDefinition;
  selectClass?: string;
  isAutofocus?: boolean;
  optionsAsValue?: boolean;
  includeNull?: boolean;
  nullKey?: string;
  nullText?: string;
  hideOptionKey?: boolean;
}

export interface ITextAreaFormGroupProps
  extends IFormGroupProps<HTMLTextAreaElement> {
  validateObject?: { [key: string]: (value: string) => boolean | string };
  autoComplete?: string;
  leftIcon?: IconDefinition;
  inputClass?: string;
  startTouched?: boolean;
  placeholderKey?: string;
  placeholderText?: string;
  minLength?: number;
  minLengthMessage?: string;
  maxLength?: number;
  maxLengthMessage?: string;
  showCharacterCounter?: boolean;
  rows?: number;
}

export interface ITextInputFormGroupProps
  extends IFormGroupProps<HTMLInputElement> {
  validateObject?: { [key: string]: (value: string) => boolean | string };
  autoComplete?: string;
  leftIcon?: IconDefinition;
  rightIcon?: IconDefinition;
  inputClass?: string;
  startTouched?: boolean;
  placeholderKey?: string;
  placeholderText?: string;
  minLength?: number;
  minLengthMessage?: string;
  maxLength?: number;
  maxLengthMessage?: string;
  pattern?: RegExp;
  patternMessage?: string;
  isEmail?: boolean;
  isPhone?: boolean;
  isFax?: boolean;
  isUsername?: boolean;
}

export interface IToggleFormGroupFormGroupProps
  extends IFormGroupProps<HTMLInputElement> {
  inputClass?: string;
  trueStyle?: string;
  falseStyle?: string;
  trueText?: string;
  falseText?: string;
  labelKey: string;
  labelText: string;
  required?: boolean;
  requiredMessage?: string;
}
