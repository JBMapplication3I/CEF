import { ICheckboxFormGroupProps } from "./_formGroupTypes";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { Form, InputGroup } from "react-bootstrap";
import { useState } from "react";
import { useTranslation } from "react-i18next";

export const CheckboxFormGroup = (props: ICheckboxFormGroupProps) => {
  const {
    register,
    errors,
    disabled,
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
    checkboxOptions
  } = props;

  const [touched, setTouched] = useState<boolean>(false);
  const [tooltipIsOpen, setTooltipIsOpen] = useState<boolean>(false);

  const { t } = useTranslation();

  const relevantFields = checkboxOptions.map((op) => op.identifier);
  let errorExists = false;
  for (const field of relevantFields) {
    if (errors[field] != null) {
      errorExists = true;
      break;
    }
  }

  const applyHasErrorClass = (): string => {
    return errorExists ? "has-error" : "";
  };

  return (
    <Form.Group
      className={`position-relative mb-2 ${
        formClasses ?? ""
      } ${applyHasErrorClass()}`}>
      {tooltipIsOpen ? (
        <div
          className={`rounded border text-center p-2 w-25 small bg-dark text-light form-group-tooltip`}>
          {t(tooltipKey)}
        </div>
      ) : null}
      <InputGroup className="d-block">
        {checkboxOptions.map((option) => {
          const { identifier, required, requiredMessage, labelKey, labelText } =
            option;
          const checkboxInput = register(identifier, {
            required: {
              value: typeof required === "boolean" ? required : null,
              message: requiredMessage
            }
          });
          return (
            <Form.Label
              htmlFor={identifier}
              key={labelText}
              className="control-label d-flex"
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
              <Form.Check
                as="input"
                className={`${inputClass ?? ""}`}
                type="checkbox"
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
                {...checkboxInput}
                onChange={(e) => {
                  checkboxInput.onChange(e);
                  if (extraOnChange) {
                    extraOnChange(e);
                  }
                  if (!touched) {
                    setTouched(true);
                  }
                }}
                id={identifier}
                name={identifier}
              />
              <span className="ml-2">{t(labelKey)}</span>
              {required ? <span className="text-danger">&nbsp;*</span> : null}
              {tooltipKey || tooltipText ? (
                <span className="text-info">
                  <FontAwesomeIcon icon={faCheckCircle} />
                </span>
              ) : null}
            </Form.Label>
          );
        })}
      </InputGroup>
      {errorExists && !hideInvalidTooltip ? (
        <InputGroup.Text className="text-start text-wrap" role="alert">
          {Object.keys(errors)
            .filter((errorKey) => relevantFields.includes(errorKey))
            .map((errorKey) => {
              if (!errors[errorKey]) {
                return null;
              }
              return (
                <span key={errorKey} className="text-danger">
                  {errors[errorKey].message}
                </span>
              );
            })}
        </InputGroup.Text>
      ) : null}
    </Form.Group>
  );
};
