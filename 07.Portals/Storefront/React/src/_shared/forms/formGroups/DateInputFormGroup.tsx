import { IDateInputFormGroupProps } from "./_formGroupTypes";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { Form, InputGroup } from "react-bootstrap";

export const DateInputFormGroup = (props: IDateInputFormGroupProps) => {
  const {
    register,
    errors,
    formIdentifier,
    disabled,
    required,
    requiredMessage,
    labelKey,
    labelText,
    tooltipKey,
    tooltipText,
    inputClass,
    formClasses,
    // onBlur seems to be ignored on input
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
    min,
    minMessage,
    max,
    maxMessage,
    includeTime
  } = props;

  const [tooltipIsOpen, setTooltipIsOpen] = useState<boolean>(false);

  const { t } = useTranslation();

  const dateInput = register(formIdentifier, {
    required: {
      value: typeof required === "boolean" ? required : null,
      message: requiredMessage ?? "This field is required"
    },
    max: {
      value: max ?? null,
      message: maxMessage ?? `Maximum date: ${max}`
    },
    min: {
      value: min ?? null,
      message: minMessage ?? `Minimum date ${min}`
    }
  });

  const timeInput = register(`time${formIdentifier}`, {
    required: {
      value: typeof required === "boolean" ? required : null,
      message: requiredMessage ?? "This field is required"
    }
  });

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
        <input
          className={`w-100 p-1 form-control ${inputClass ?? ""}`}
          type="date"
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
          {...dateInput}
          onChange={(e) => {
            dateInput.onChange(e);
            if (extraOnChange) {
              extraOnChange(e);
            }
          }}
          id={formIdentifier}
        />
        {includeTime ? (
          <InputGroup.Text className="with-control">
            <Form.Control
              type="time"
              id={`time${formIdentifier}}`}
              name={`time${formIdentifier}}`}
              {...timeInput}
              disabled={disabled}
              required={required}
            />
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
