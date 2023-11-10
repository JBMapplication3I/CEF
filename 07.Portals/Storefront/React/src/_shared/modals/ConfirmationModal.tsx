import React, { useState, Fragment } from "react";
import { faTimes } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import classes from "./Modal.module.scss";
import { useTranslation } from "react-i18next";

interface IConfirmationModalProps {
  confirmBtnLabel?: string;
  confirmBtnClass?: string;
  cancelBtnLabel?: string;
  cancelBtnClass?: string;
  confirmDisabled?: boolean;
  showHeader?: boolean;
  size?: string;
  title?: string;
  show: boolean;
  onConfirm: Function;
  onCancel: Function;
  children?: React.ReactNode;
}

export const ConfirmationModal = (
  props: IConfirmationModalProps
): JSX.Element => {
  const {
    title,
    showHeader,
    confirmBtnLabel,
    confirmBtnClass,
    cancelBtnLabel,
    cancelBtnClass,
    onConfirm,
    confirmDisabled,
    onCancel,
    size,
    show
  } = props;

  const [canClickBackdropForClose, setCanClickBackdropForClose] =
    useState(false);

  const { t } = useTranslation();

  const onConfirmButtonPressed = (): void => {
    onConfirm();
  };

  const onCancelButtonPressed = (): void => {
    onCancel();
  };

  return (
    <Fragment>
      <div
        className={`modal fade ${show ? "show" : ""}`}
        style={{ display: show ? "block" : "none" }}
        onClick={() => {
          if (canClickBackdropForClose) {
            onCancelButtonPressed();
          }
        }}
        tabIndex={-1}
        aria-hidden="true">
        <div className={`modal-dialog modal-${size ?? "lg"}`}>
          <div
            className="modal-content border-0"
            onMouseEnter={() => setCanClickBackdropForClose(false)}
            onMouseLeave={() => setCanClickBackdropForClose(true)}>
            <div
              className={`modal-header bg-dark text-light rounded-0 ${
                showHeader === false ? "d-none" : ""
              }`}>
              <h5 className="modal-title text-capitalize mb-0">{title}</h5>
              <button
                type="button"
                className="btn close d-inline-block"
                aria-label={t("ui.storefront.common.Close")}
                onClick={onCancelButtonPressed}>
                <FontAwesomeIcon icon={faTimes} className="text-light" />
              </button>
              <span className="sr-only">{t("ui.storefront.common.Close")}</span>
            </div>
            <div className="modal-body">{props.children}</div>
            <div className="modal-footer">
              <button
                type="button"
                className={`btn btn-outline-secondary ${cancelBtnClass}`}
                onClick={onCancelButtonPressed}>
                {cancelBtnLabel ?? t("ui.storefront.common.Cancel")}
              </button>
              <button
                type="button"
                className={`btn btn-primary ml-2 text-capitalize ${confirmBtnClass}`}
                disabled={confirmDisabled || false}
                onClick={onConfirmButtonPressed}>
                {confirmBtnLabel ?? "Confirm"}
              </button>
            </div>
          </div>
        </div>
      </div>
      <div
        className={`fade modal-backdrop ${
          show ? `${classes.zHigh} show` : `${classes.zLow} d-none`
        }`}
        onClick={onCancelButtonPressed}></div>
    </Fragment>
  );
};
