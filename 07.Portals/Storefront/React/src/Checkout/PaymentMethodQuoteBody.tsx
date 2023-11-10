import { IPaymentMethodQuoteBodyProps } from "./_checkoutTypes";
import { useTranslation } from "react-i18next";
import { Row, Col, Button } from "react-bootstrap";
export const PaymentMethodQuoteBody = (props: IPaymentMethodQuoteBodyProps): JSX.Element => {
  const { onSubmit, continueText, submitDisabled } = props;
  const { t } = useTranslation();

  return (
    <Row className="mb-3">
      <Col xs={12}>
        <p>{t("ui.storefront.checkout.views.paymentInformation.QuoteMeContent")}</p>
      </Col>
      <Col xs={12}>
        <Button
          variant="primary"
          className={`my-3`}
          id="btnSubmit_purchaseStepPayment"
          title={continueText ?? "Confirm Order and Purchase"}
          disabled={submitDisabled}
          onClick={() => onSubmit()}>
          {continueText ??
            t("ui.storefront.checkout.views.paymentInformation.confirmOrderAndPurchase")}
        </Button>
      </Col>
    </Row>
  );
};
