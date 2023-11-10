import { IToggleFormGroupFormGroupProps } from "./_formGroupTypes";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { useState } from "react";
import classes from "./ToggleFormGroup.module.scss";
import { useTranslation } from "react-i18next";
import { Form, InputGroup } from "react-bootstrap";

export const ToggleFormGroup = (props: IToggleFormGroupFormGroupProps) => {
  const {
    register,
    errors,
    formIdentifier,
    disabled,
    required,
    requiredMessage,
    labelKey,
    labelText,
    trueStyle,
    falseStyle,
    trueText,
    falseText,
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
    failsOn
  } = props;

  const [touched, setTouched] = useState<boolean>(false);
  const [tooltipIsOpen, setTooltipIsOpen] = useState<boolean>(false);
  const [toggledOn, setToggledOn] = useState(false);

  const { t } = useTranslation();

  const toggleInput = register(formIdentifier, {
    required: {
      value: typeof required === "boolean" ? required : null,
      message: requiredMessage
    }
  });

  return (
    <Form.Group
      className={`position-relative lg-text-left md-text-left sm-text-left ${formClasses ?? ""} ${
        errors[formIdentifier] ? "has-error" : ""
      }`}>
      <Form.Label
        htmlFor={formIdentifier}
        className="w-100 form-check-label"
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
        <InputGroup className={classes.switch}>
          <input
            className={`toggle-input w-100 ${inputClass ?? ""}`}
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
            onChange={(e) => {
              toggleInput.onChange(e);
              setToggledOn(!toggledOn);
              if (extraOnChange) {
                extraOnChange(e);
              }
              if (!touched) {
                setTouched(true);
              }
            }}
            id={formIdentifier}
            name={formIdentifier}
          />
          <span
            className={`d-flex justify-content-center align-items-center border border-danger ${
              toggledOn ? `bg-success ${trueStyle}` : `bg-danger ${falseStyle}`
            } ${classes.slider}`}>
            {toggledOn ? trueText : falseText}
          </span>
        </InputGroup>
        {errors[formIdentifier] && !hideInvalidTooltip ? (
          <Form.Control.Feedback type="invalid">
            {errors[formIdentifier].message}
          </Form.Control.Feedback>
        ) : null}
        {/* TODO: add success messages */}
      </Form.Label>
      {tooltipIsOpen ? (
        <div
          className={`rounded border text-center p-2 w-25 small bg-dark text-light form-group-tooltip`}>
          {tooltipText}
        </div>
      ) : null}
    </Form.Group>
  );
};
