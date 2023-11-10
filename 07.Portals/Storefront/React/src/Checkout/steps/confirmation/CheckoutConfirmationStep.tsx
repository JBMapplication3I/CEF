import { useTranslation } from "react-i18next";
import { ICheckoutConfirmationStepProps } from "../../_checkoutTypes";
import { Row, Col, Button } from "react-bootstrap";
import { Fragment } from "react";

export const CheckoutConfirmationStep = (props: ICheckoutConfirmationStepProps) => {
  const { masterOrderID, subOrderIDs, orderID, quoteIDs } = props;
  const { t } = useTranslation();

  return (
    <div className="step step-three">
      <div className="checkout-box">
        <Row>
          <Col xs={12} className="text-center">
            <h4 className="mb-4">
              {t("ui.storefront.checkout.views.orderConfirmation.thankYouForYouPurchase") + "!"}
            </h4>
          </Col>
        </Row>
        <Row className="mb-4">
          <div className="col-12 col-sm-6">
            <p id="PurchaseSuccessText">
              {t(
                "ui.storefront.checkout.views.orderConfirmation.yourPurchaseWasSuccessfulAndIsInTheProcessOfBeing"
              )}
            </p>
          </div>
          {quoteIDs ? (
            <Col xs={12} sm={6}>
              <h4>
                {t("ui.storefront.submitQuote.confirmation.MasterQuote")}&nbsp;
                <strong>{quoteIDs.master}</strong>
              </h4>
              <p className="small font-weight-bold">
                {t("ui.storefront.submitQuote.confirmation.MasterQuote.Message")}
              </p>
              <h4>
                {t("ui.storefront.submitQuote.confirmation.SubQuote.OptionalPlural")}
                <strong>{quoteIDs.slave}</strong>
              </h4>
              <p className="small font-weight-bold">
                {t("ui.storefront.submitQuote.confirmation.SubQuote.Message")}
              </p>
            </Col>
          ) : masterOrderID ? (
            <Col xs={12} sm={6}>
              <div className="col-12">
                <h5>
                  <span>{t("ui.storefront.checkout.views.orderConfirmation.MasterOrder")}</span>
                  &nbsp;
                  <b>{masterOrderID}</b>
                </h5>
                <p className="small bold">
                  {t("ui.storefront.checkout.views.orderConfirmation.MasterOrder.Message")}
                </p>
                <h5>
                  <span>
                    {t("ui.storefront.checkout.views.orderConfirmation.SubOrder.OptionalPlural")}
                  </span>
                  &nbsp;
                  {subOrderIDs.map((subOrderID: number) => {
                    return (
                      <Fragment key={subOrderID}>
                        <b>
                          {subOrderID +
                            `${subOrderID === subOrderIDs[subOrderIDs.length - 1] ? "" : ", "}`}
                        </b>
                      </Fragment>
                    );
                  })}
                </h5>
                <p className="small">
                  {t("ui.storefront.checkout.views.orderConfirmation.SubOrder.Message")}
                </p>
              </div>
            </Col>
          ) : (
            <Col xs={12} sm={6}>
              <h4>
                {t("ui.storefront.checkout.views.orderConfirmation.OrderNumber")}
                &#58;&nbsp;
                <span className="number text-uppercase mr-2">{orderID}</span>
              </h4>
            </Col>
          )}
        </Row>
        <Row className="mb-3">
          <Col xs={12}>
            <div className="d-flex justify-content-end">
              <a className="btn btn-outline-primary" href="/catalog">
                {t("ui.storefront.cart.continueShopping")}
              </a>
              {quoteIDs ? (
                <a className="btn btn-outline-secondary me-2" href="/dashboard/quotes">
                  {t("ui.storefront.submitQuote.confirmation.YourQuoteHistory")}
                </a>
              ) : (
                <a className="btn btn-outline-secondary" href="/dashboard/orders">
                  {t("ui.storefront.checkout.confirmation.YourOrderHistory")}
                </a>
              )}
            </div>
          </Col>
        </Row>
      </div>
    </div>
  );
};
