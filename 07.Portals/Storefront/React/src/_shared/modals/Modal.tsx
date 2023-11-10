import React, { Fragment, useState } from "react";
import { faTimes } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import classes from "./Modal.module.scss";
import { LoadingWidget } from "../common/LoadingWidget";
import { useTranslation } from "react-i18next";
import { Button } from "react-bootstrap";

interface IModalProps {
  size?: string;
  title: string;
  showHeader?: boolean;
  onCancel: Function;
  show: boolean;
  children?: React.ReactNode;
  loading?: boolean;
}

export const Modal = (props: IModalProps): JSX.Element => {
  const { title, onCancel, size, show, loading, showHeader } = props;

  const [canClickBackdropForClose, setCanClickBackdropForClose] =
    useState(false);

  const { t } = useTranslation();

  const onCancelButtonPressed = () => {
    if (onCancel) {
      onCancel();
    }
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
                !showHeader && "d-none"
              }`}>
              <h5 className="modal-title text-capitalize mb-0">{title}</h5>
              <Button
                variant="dark"
                className="close d-inline-block"
                aria-label={t("ui.storefront.common.Close")}
                onClick={onCancelButtonPressed}>
                <FontAwesomeIcon icon={faTimes} className="text-light" />
              </Button>
              <span className="sr-only">{t("ui.storefront.common.Close")}</span>
            </div>
            <div className="modal-body">
              {loading ? <LoadingWidget /> : props.children}
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

Modal.defaultProps = {
  showHeader: true
};
