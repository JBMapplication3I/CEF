import { Fragment } from "react";
import { useTranslation } from "react-i18next";
import { Row, Alert, Col } from "react-bootstrap";
interface IRegistrationCompletedStepProps {
  emailRegisteredWith: string;
}

export const RegistrationCompletedStep = (
  props: IRegistrationCompletedStepProps
): JSX.Element => {
  const { emailRegisteredWith } = props;
  const { t } = useTranslation();

  return (
    <Fragment>
      <Row
        as={Alert}
        variant="success"
        className="text-center align-items-center pt-4">
        <Col xs={12} className="mb-3">
          <p className="mb-0">
            <strong>{t("ui.storefront.common.Success.Exclamation")}</strong>
            <span>
              {t("ui.storefront.user.registration.Success.UsernameMessage")}
            </span>
            &nbsp;
            <strong>{emailRegisteredWith}</strong>
            <span>{t("ui.storefront.user.registration.Success1.Message")}</span>
          </p>
        </Col>
        <Col xs={12} className="mb-2">
          <span className="d-block">{"What's next?"}</span>
        </Col>
        <Col md={4} xl={12}>
          <span>
            {"Save time at checkout by creating your"}&nbsp;
            <a href="/dashboard/wallet">
              {t("ui.storefront.userDashboard2.userDashboard.Wallet")}
            </a>
          </span>
        </Col>
        <Col md={4} xl={12}>
          <span>{"OR"}</span>
        </Col>
        <Col md={4} xl={12}>
          <div className="d-flex flex-column justify-content-center align-items-center">
            <span>{"Get right to shopping!"}</span>
            <a href="/catalog">{t("ui.storefront.cart.continueShopping")}</a>
          </div>
        </Col>
      </Row>
    </Fragment>
  );
};
