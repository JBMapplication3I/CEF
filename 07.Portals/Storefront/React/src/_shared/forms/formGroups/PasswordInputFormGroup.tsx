import { IPasswordInputFormGroupProps } from "./_formGroupTypes";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { faEye, faEyeSlash } from "@fortawesome/free-solid-svg-icons";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { Form, InputGroup, Button, ListGroup } from "react-bootstrap";
import { Input } from "@material-ui/core";

export const PasswordInputFormGroup = (props: IPasswordInputFormGroupProps) => {
  const {
    register,
    errors,
    pattern,
    patternMessage,
    formIdentifier,
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
    startTouched,
    placeholderKey,
    placeholderText,
    minLength,
    minLengthMessage,
    maxLength,
    maxLengthMessage
  } = props;

  const [touched, setTouched] = useState<boolean>(false);
  const [tooltipIsOpen, setTooltipIsOpen] = useState<boolean>(false);
  const [showPassword, setShowPassword] = useState(false);

  const { t } = useTranslation();

  const toggleShowPassword = (): void => {
    setShowPassword(!showPassword);
  };

  const reqd = typeof required === "boolean" ? required : true;

  const passwordInput = register(formIdentifier, {
    required: {
      value: reqd,
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
    pattern: {
      value: pattern != null ? pattern : null,
      message: patternMessage ?? "Value does not match required pattern"
    },
    validate: reqd
      ? {
          // hasNumber: value => value.match(/\d+/g),
          hasLowerCase: (value) => {
            if (!value.match(/[a-z]/g)) {
              return "Password must contain at least one lowercase character";
            } else {
              return true;
            }
          },
          hasUpperCase: (value) => {
            if (!value.match(/[A-Z]/g)) {
              return "Password must contain at least one uppercase character";
            } else {
              return true;
            }
          },
          hasNumber: (value) => {
            if (!value.match(/\d+/g)) {
              return "Password must contain at least one number";
            } else {
              return true;
            }
          }
        }
      : null
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
        onMouseEnter={(_e) => {
          if (tooltipKey || tooltipText) {
            setTooltipIsOpen(true);
          }
        }}
        onMouseLeave={(_e) => {
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
          className={`form-control ${applyValidClass()} ${inputClass ?? ""}`}
          type={showPassword ? "text" : "password"}
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
          {...passwordInput}
          onChange={(e) => {
            passwordInput.onChange(e);
            if (extraOnChange) {
              extraOnChange(e);
            }
            if (!touched) {
              setTouched(true);
            }
          }}
          id={formIdentifier}
          placeholder={placeholderKey ? t(placeholderKey) : placeholderText ? placeholderText : ""}
        />
        <InputGroup.Text className="p-0">
          <Button
            variant="outline-secondary"
            className="rounded-right"
            onClick={toggleShowPassword}>
            <FontAwesomeIcon icon={showPassword ? faEyeSlash : faEye} />
          </Button>
        </InputGroup.Text>
        {errors[formIdentifier] && !hideInvalidTooltip ? (
          <div className="w-100">
            <ListGroup className="list-unstyled">
              {errors[formIdentifier] &&
                Object.values<string>(errors[formIdentifier].types).map(
                  (msg): JSX.Element => {
                    return (
                      <Form.Control.Feedback type="invalid" key={msg}>
                        {msg}
                      </Form.Control.Feedback>
                    );
                  }
                )}
            </ListGroup>
          </div>
        ) : null}
        {/* TODO: add success messages */}
      </InputGroup>
    </Form.Group>
  );
};
