import { ITextAreaFormGroupProps } from "./_formGroupTypes";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { Form, InputGroup } from "react-bootstrap";

export const TextAreaFormGroup = (props: ITextAreaFormGroupProps) => {
  const {
    register,
    errors,
    validateObject,
    formIdentifier,
    disabled,
    required,
    requiredMessage,
    autoComplete,
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
    startTouched,
    placeholderKey,
    placeholderText,
    minLength,
    minLengthMessage,
    maxLength,
    maxLengthMessage,
    showCharacterCounter,
    rows
  } = props;

  const [touched, setTouched] = useState<boolean>(false);
  const [tooltipIsOpen, setTooltipIsOpen] = useState<boolean>(false);
  const [letterCount, setLetterCount] = useState<number>(0);

  const { t } = useTranslation();

  const textArea = register(formIdentifier, {
    required: {
      value: typeof required === "boolean" ? required : null,
      message: requiredMessage ?? "This field is required"
    },
    minLength: {
      value: typeof minLength === "number" ? minLength : null,
      message: minLengthMessage ?? `Must reach minimum length of ${minLength}`
    },
    maxLength: {
      value: typeof maxLength === "number" ? maxLength : null,
      message:
        maxLengthMessage ?? `Must not exceed maximum length of ${maxLength}`
    },
    validate: validateObject ?? null
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
        <textarea
          className={`w-100 p-1 form-control ${applyValidClass()} ${
            inputClass ?? ""
          }`}
          style={{
            maxWidth: "100%",
            minHeight: 24 * (rows || 4) + "px"
          }}
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
          rows={rows}
          disabled={disabled}
          {...textArea}
          onChange={(e) => {
            textArea.onChange(e);
            if (extraOnChange) {
              extraOnChange(e);
            }
            if (!touched) {
              setTouched(true);
            }
            setLetterCount(e.target.value.length);
          }}
          id={formIdentifier}
          placeholder={t(placeholderKey)}
        />
        {maxLength > 0 && showCharacterCounter ? (
          <div>{`${letterCount} of ${maxLength}`}</div>
        ) : null}
        {errors[formIdentifier] && !hideInvalidTooltip ? (
          <div className="input-group-append w-100 pt-1 pl-1" role="alert">
            <Form.Control.Feedback type="invalid">
              {errors[formIdentifier].message}
            </Form.Control.Feedback>
          </div>
        ) : null}
        {/* TODO: add success messages */}
      </InputGroup>
    </Form.Group>
  );
};
