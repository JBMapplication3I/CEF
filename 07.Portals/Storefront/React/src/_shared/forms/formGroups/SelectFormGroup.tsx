import { useEffect, useState } from "react";
import { ISelectFormGroupProps } from "./_formGroupTypes";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { useTranslation } from "react-i18next";
import { Form, InputGroup } from "react-bootstrap";

export const SelectFormGroup = (props: ISelectFormGroupProps) => {
  const [touched, setTouched] = useState<boolean>(false);
  const [nullOptionDisabled, setNullOptionDisabled] = useState<boolean>(false);
  const [tooltipIsOpen, setTooltipIsOpen] = useState<boolean>(false);
  const [selectedValue, setSelectedValue] = useState<string>("");

  const { t } = useTranslation();

  const {
    register,
    errors,
    formIdentifier,
    formClasses,
    labelKey,
    labelText,
    tooltipKey,
    tooltipText,
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
    disabled,
    showValidTooltip,
    hideInvalidTooltip,
    failsOn,
    forceTooltipWithFocus,
    required,
    requiredMessage,
    options,
    autocomplete,
    leftIcon,
    rightIcon,
    selectClass,
    isAutofocus,
    optionsAsValue,
    includeNull,
    nullKey,
    nullText,
    hideOptionKey,
    initialOption
  } = props;

  const applyValidClass = (): string => {
    if (!touched) {
      return "";
    }
    return errors[formIdentifier] ? "is-invalid" : "is-valid";
  };

  let nullString: string = "";
  if (nullKey) {
    nullString = t(nullKey);
  } else if (nullText) {
    nullString = nullText;
  }
  useEffect(() => {
    if (initialOption && options.length) {
      const initialValueOption = options.find(o => o.option === initialOption.option || o.value === initialOption.value);
      if (initialValueOption) {
        setSelectedValue(initialValueOption.value ?? initialValueOption.option);
      } else {
        setSelectedValue(options[0].option);
      }
      return;
    }
    if (hideOptionKey && options.length && selectedValue === "") {
      setSelectedValue(options[0].option);
    } else if (!hideOptionKey && !options && selectedValue === "") {
      setSelectedValue(nullString);
    }
  }, []);

  const select = register(formIdentifier, {
    required: {
      value: typeof required === "boolean" ? required : null,
      message: requiredMessage ?? "This field is required"
    },
    validate: {
      isNotNull: (value) => {
        if (!value || value === nullString || value === nullText) {
          return "This field is required";
        } else {
          return true;
        }
      }
    }
  });

  return (
    <Form.Group
      className={`form-group position-relative ${formClasses ?? ""} ${
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
      <InputGroup className="input-group">
        {leftIcon ? (
          <InputGroup.Text className="rounded-left">
            <FontAwesomeIcon icon={leftIcon} className="fa-fw" />
          </InputGroup.Text>
        ) : null}
        <Form.Select
          className={`custom-select ${
            !rightIcon ? "rounded-right" : ""
          } ${applyValidClass()} ${selectClass ?? ""}`}
          id={formIdentifier}
          name={formIdentifier}
          disabled={disabled}
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
          value={selectedValue}
          {...select}
          onChange={(e) => {
            setSelectedValue(e.target.value);
            select.onChange(e);
            if (!nullOptionDisabled) {
              setNullOptionDisabled(true);
            }
            if (extraOnChange) {
              extraOnChange(e);
            }
            if (!touched) {
              setTouched(true);
            }
          }}>
          {includeNull ? (
            <option value={nullString} disabled={nullOptionDisabled}>
              {nullString}
            </option>
          ) : null}
          {options && Array.isArray(options)
            ? options.map((opt) => {
                return (
                  <option
                    key={opt.value ?? opt.option}
                    value={
                      optionsAsValue
                        ? opt.option
                        : opt.value
                        ? opt.value
                        : opt.option
                    }>
                    {opt.option}
                  </option>
                );
              })
            : null}
        </Form.Select>
        {rightIcon ? (
          <InputGroup.Text className="rounded-right">
            <FontAwesomeIcon icon={rightIcon} className="fa-fw" />
          </InputGroup.Text>
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
