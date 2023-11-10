import { INumberInputFormGroupProps } from "./_formGroupTypes";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { useState } from "react";
import { faDollarSign, faPercent } from "@fortawesome/free-solid-svg-icons";
import { useTranslation } from "react-i18next";
import { Form, InputGroup } from "react-bootstrap";

export const NumberInputFormGroup = (props: INumberInputFormGroupProps) => {
  const {
    register,
    errors,
    formIdentifier,
    min,
    minMessage,
    max,
    maxMessage,
    placeholderText,
    placeholderKey,
    isCurrency,
    isPercent,
    step,
    disabled,
    required,
    requiredMessage,
    labelKey,
    labelText,
    tooltipKey,
    tooltipText,
    // onBlur seems to be ignored on input
    forceTooltipWithFocus,
    inputClass,
    formClasses,
    onBlur,
    onFocus,
    onKeyDown,
    onKeyUp,
    onMouseDown,
    onMouseEnter,
    onMouseLeave,
    onMouseMove,
    onMouseOver,
    onMouseUp,
    extraOnChange,
    showValidTooltip,
    hideInvalidTooltip,
    failsOn,
    startTouched
  } = props;

  const [touched, setTouched] = useState<boolean>(false);
  const [tooltipIsOpen, setTooltipIsOpen] = useState<boolean>(false);

  const { t } = useTranslation();

  const numberInput = register(formIdentifier, {
    required: {
      value: typeof required === "boolean" ? required : null,
      message: requiredMessage ?? "This field is required"
    },
    min: {
      value: typeof min === "number" ? min : null,
      message: minMessage ?? `Must reach minimum of ${min}`
    },
    max: {
      value: typeof max === "number" ? max : null,
      message: maxMessage ?? `Must not exceed maximum of ${max}`
    }
  });

  const applyValidClass = (): string => {
    if (!touched) {
      return "";
    }
    return errors[formIdentifier] ? "is-invalid" : "is-valid";
  };

  return (
    <Form.Group
      className={`position-relative ${formClasses ?? ""} ${
        errors[formIdentifier] ? "has-error" : ""
      }`}>
      <Form.Label
        htmlFor={formIdentifier}
        className="control-label"
        onMouseEnter={(e) => {
          if (tooltipKey || tooltipText) {
            setTooltipIsOpen(true);
          }
        }}
        onMouseLeave={(e) => {
          if (tooltipIsOpen) {
            setTooltipIsOpen(false);
          }
        }}>
        <span>{t(labelKey)}</span>
        {required ? <span className="text-danger">&nbsp;*</span> : null}
        {tooltipKey || tooltipText ? (
          <span className="text-info">
            <FontAwesomeIcon icon={faCheckCircle} />
          </span>
        ) : null}
      </Form.Label>
      {tooltipIsOpen ? (
        <div
          className={`rounded border text-center p-2 w-25 small bg-dark text-light form-group-tooltip`}>
          {t(tooltipKey)}
        </div>
      ) : null}
      <InputGroup>
        {isCurrency ? (
          <InputGroup.Text>
            <FontAwesomeIcon icon={faDollarSign} />
          </InputGroup.Text>
        ) : null}
        <input
          className={`form-control ${applyValidClass()} ${inputClass ?? ""}`}
          id={formIdentifier}
          type="number"
          placeholder={placeholderText}
          min={min}
          max={max}
          step={step ?? 1}
          onBlur={onBlur}
          onFocus={onFocus}
          onKeyDown={onKeyDown}
          onKeyUp={onKeyUp}
          onMouseDown={onMouseDown}
          onMouseEnter={onMouseEnter}
          onMouseLeave={onMouseLeave}
          onMouseMove={onMouseMove}
          onMouseOver={onMouseOver}
          onMouseUp={onMouseUp}
          disabled={disabled}
          {...numberInput}
          onChange={(e) => {
            numberInput.onChange(e);
            if (extraOnChange) {
              extraOnChange(e);
            }
            if (!touched) {
              setTouched(true);
            }
          }}
        />
        {isPercent ? (
          <InputGroup.Text>
            <FontAwesomeIcon icon={faPercent} />
          </InputGroup.Text>
        ) : null}
        {errors[formIdentifier] && !hideInvalidTooltip ? (
          <Form.Control.Feedback type="invalid">
            {errors[formIdentifier].message}
          </Form.Control.Feedback>
        ) : null}
      </InputGroup>
    </Form.Group>
  );
};
