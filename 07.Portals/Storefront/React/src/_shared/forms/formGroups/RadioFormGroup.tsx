import { useState } from "react";
import { useTranslation } from "react-i18next";
import { IRadioFormGroupProps } from "./_formGroupTypes";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { InputGroup, Form } from "react-bootstrap";

export const RadioFormGroup = (props: IRadioFormGroupProps) => {
  const {
    radioOptions,
    inputClass,
    register,
    errors,
    formIdentifier,
    formClasses,
    labelKey,
    labelText,
    tooltipKey,
    tooltipText,
    disabled,
    showValidTooltip,
    hideInvalidTooltip,
    failsOn,
    forceTooltipWithFocus,
    required,
    requiredMessage,
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
    extraOnChange
  } = props;

  const [touched, setTouched] = useState<boolean>(false);
  const [tooltipIsOpen, setTooltipIsOpen] = useState<boolean>(false);
  const { t } = useTranslation();

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
      {tooltipIsOpen ? (
        <div
          className={`rounded border text-center p-2 w-25 small bg-dark text-light form-group-tooltip`}>
          {t(tooltipKey)}
        </div>
      ) : null}
      <InputGroup className={`d-block ${applyValidClass()}`}>
        {radioOptions && Array.isArray(radioOptions)
          ? radioOptions
              .sort((a, b) => {
                return a.text > b.text ? 1 : -1;
              })
              .map((op, index) => {
                const radioInput = register(formIdentifier, {
                  required: {
                    value: typeof required === "boolean" ? required : null,
                    message: requiredMessage ?? "This field is required"
                  }
                });

                return (
                  <Form.Label
                    key={op.text}
                    htmlFor={`${formIdentifier}_${index}`}
                    className="control-label d-block"
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
                    <input
                      type="radio"
                      className={inputClass}
                      id={`${formIdentifier}_${index}`}
                      name={`${formIdentifier}_${index}`}
                      value={op.text}
                      disabled={disabled}
                      required={required}
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
                      {...radioInput}
                      onChange={(e) => {
                        radioInput.onChange(e);
                        if (extraOnChange) {
                          extraOnChange(e);
                        }
                        if (!touched) {
                          setTouched(true);
                        }
                      }}
                    />
                    <span className="ms-2">{op.text}</span>
                    {required ? (
                      <span className="text-danger">&nbsp;*</span>
                    ) : null}
                    {tooltipKey || tooltipText ? (
                      <span className="text-info">
                        <FontAwesomeIcon icon={faCheckCircle} />
                      </span>
                    ) : null}
                    {errors[`${formIdentifier}_${index}`] &&
                    !hideInvalidTooltip ? (
                      <InputGroup.Text className="text-start text-wrap" role="alert">
                        <span className="text-danger">
                          {errors[`${formIdentifier}_${index}`].message}
                        </span>
                      </InputGroup.Text>
                    ) : null}
                  </Form.Label>
                );
              })
          : null}
      </InputGroup>
    </Form.Group>
  );
};
