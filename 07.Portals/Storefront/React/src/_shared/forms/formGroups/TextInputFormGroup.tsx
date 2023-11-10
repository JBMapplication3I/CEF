import { ITextInputFormGroupProps } from "./_formGroupTypes";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { faAt, faFax, faPhone } from "@fortawesome/free-solid-svg-icons";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { Form, InputGroup } from "react-bootstrap";
import { Input } from "@material-ui/core";

export const TextInputFormGroup = (props: ITextInputFormGroupProps) => {
  const {
    register,
    errors,
    pattern,
    patternMessage,
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
    leftIcon,
    rightIcon,
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
    isEmail,
    isPhone,
    isFax,
    isUsername
  } = props;

  const [touched, setTouched] = useState<boolean>(false);
  const [tooltipIsOpen, setTooltipIsOpen] = useState<boolean>(false);

  const { t } = useTranslation();

  const textInput = register(formIdentifier, {
    required: {
      value: typeof required === "boolean" ? required : null,
      message:
        requiredMessage ??
        t("ui.storefront.common.validation.ThisFieldIsRequired")
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
    pattern: {
      value: pattern != null ? pattern : null,
      message: patternMessage ?? "Value does not match required pattern"
    },
    validate: validateObject ?? null
  });

  let inputAutoComplete;
  if (isUsername) {
    inputAutoComplete = "username";
  } else if (isEmail) {
    inputAutoComplete = "email";
  } else if (isPhone) {
    inputAutoComplete = "tel";
  } else if (isFax) {
    inputAutoComplete = "fax";
  } else if (autoComplete) {
    inputAutoComplete = autoComplete;
  }

  const applyValidClass = (): string => {
    if (!touched) {
      return "";
    }
    return errors[formIdentifier] ? "is-invalid" : "is-valid";
  };

  const renderLeftIcon = () => {
    if (!isEmail && !isFax && !isPhone && !leftIcon) {
      return null;
    }
    return (
      <InputGroup.Text>
        {isEmail ? <FontAwesomeIcon icon={faAt} /> : null}
        {isPhone ? <FontAwesomeIcon icon={faPhone} /> : null}
        {isFax ? <FontAwesomeIcon icon={faFax} /> : null}
        {leftIcon ? <FontAwesomeIcon icon={leftIcon} /> : null}
      </InputGroup.Text>
    );
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
        {renderLeftIcon()}
        <input
          className={`form-control ${
            !rightIcon ? "rounded-right" : ""
          } ${applyValidClass()} ${inputClass ?? ""}`}
          type={isPhone || isFax ? "tel" : "text"}
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
          autoComplete={inputAutoComplete ?? "off"}
          {...textInput}
          onChange={(e) => {
            textInput.onChange(e);
            if (extraOnChange) {
              extraOnChange(e);
            }
            if (!touched) {
              setTouched(true);
            }
          }}
          id={formIdentifier}
          placeholder={placeholderKey ? t(placeholderKey) : placeholderText}
        />
        {rightIcon ? (
          <InputGroup.Text className="rounded-right">
            <FontAwesomeIcon icon={rightIcon} />
          </InputGroup.Text>
        ) : null}
        {errors[formIdentifier] && !hideInvalidTooltip ? (
          <Form.Control.Feedback type="invalid">
            {errors[formIdentifier].message}
          </Form.Control.Feedback>
        ) : null}
        {/* TODO: add success messages */}
      </InputGroup>
    </Form.Group>
  );
};
