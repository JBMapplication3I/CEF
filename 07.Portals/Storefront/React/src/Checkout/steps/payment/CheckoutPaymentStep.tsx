import { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { ServiceStrings } from "../../../_shared/ServiceStrings";
import { PaymentMethodCreditCardBody } from "../../PaymentMethodCreditCardBody";
import { PaymentMethodInvoiceBody } from "../../PaymentMethodInvoiceBody";
import { PaymentMethodQuoteBody } from "../../PaymentMethodQuoteBody";
import { ICheckoutPaymentStepProps } from "../../_checkoutTypes";
import { Row, Col, Form, Alert } from "react-bootstrap";
import { useViewState } from "../../../_shared/customHooks/useViewState";
import { useAddressFactory } from "../../../_shared/customHooks/useAddressFactory";
import cvApi from "../../../_api/cvApi";
import { AccountContactModel } from "../../../_api/cvApi._DtoClasses";
import { connect } from "react-redux";
import { IReduxStore } from "../../../_redux/_reduxTypes";
import { ContactSwitch } from "../../../Dashboard/AddressBook/ContactSwitch";

type TPaymentMethod = "Credit Card" | "Invoice" | "Quote";

const mapStateToProps = (state: IReduxStore) => {
  return {
    currentAccountAddressBook: state.currentAccountAddressBook
  };
};

export const CheckoutPaymentStep = connect(mapStateToProps)(
  (props: ICheckoutPaymentStepProps): JSX.Element => {
    const {
      onCompleteCheckoutPaymentStep,
      continueText,
      currentAccountAddressBook,
      existingPaymentData,
      initialBillingContact
    } = props;

    const [cartBillingContact, setCartBillingContact] = useState(initialBillingContact ?? null);
    const [selectedPaymentMethod, setSelectedPaymentMethod] =
      useState<TPaymentMethod>("Credit Card");

    const { setRunning, finishRunning, viewState } = useViewState();
    const { t } = useTranslation();
    const addressFactory = useAddressFactory();

    useEffect(() => {
      if (!addressFactory) {
        return;
      }
      addressFactory.getAddressBook();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [addressFactory]);

    function updateCartBillingContact(contact: AccountContactModel): void {
      setRunning();
      cvApi.shopping
        .CurrentCartSetBillingContact({
          TypeName: "Cart",
          BillingContact: contact.Slave
        })
        .then((res) => {
          if (res.data.ActionSucceeded) {
            setCartBillingContact(contact);
            finishRunning();
          } else {
            finishRunning(true, "Failed to update cart billing contact");
          }
        })
        .catch((err) => {
          finishRunning(true, err.message || "Failed to update cart billing contact");
        });
    }

    return (
      <>
        <Row className="mb-3">
          <Col xs={12}>
            <h4 className="mt-2 mb-3">
              <span id="CheckoutPaymentStageWhichAddressText">
                {t("ui.storefront.checkout.billingInfo.whichAddy")}
              </span>
              &nbsp;<span className="text-danger">*</span>
            </h4>
          </Col>
          <Col xs={12}>
            <Row>
              <Col xs={12}>
                <ContactSwitch
                  accountContacts={currentAccountAddressBook}
                  onChange={updateCartBillingContact}
                  classes="input-group"
                  initialContact={cartBillingContact}
                  allowAdd={true}
                  useAddButton={true}
                  onAddressAdded={(newAddressData) => {
                    addressFactory.createNewAddress(newAddressData).then((newContact) => {
                      updateCartBillingContact(newContact);
                    });
                  }}
                />
              </Col>
            </Row>
          </Col>
        </Row>
        {viewState.hasError ? (
          <Row>
            <Col xs={12}>
              <Alert variant="danger">{viewState.errorMessage}</Alert>
            </Col>
          </Row>
        ) : null}
        <Row>
          <Col xs={12}>
            <h4 className="mt-2 mb-3">
              <span id="CheckoutPaymentStageHowPayText">
                {/* {t("ui.storefront.checkout.paymentInfo.howPay")} */}
                How would you like to pay?
              </span>
              &nbsp;<span className="text-danger">*</span>
            </h4>
          </Col>
          <Col xs={12} sm={3}>
            <Form.Check
              type="radio"
              id="rd_purchasePaymentMethodCreditCard"
              name="rdPaymentModel"
              label={t("ui.storefront.common.CreditCard")}
              className="custom-control custom-radio"
              checked={selectedPaymentMethod === "Credit Card"}
              onChange={() => setSelectedPaymentMethod("Credit Card")}
            />
            <Form.Check
              type="radio"
              id="rd_purchasePaymentMethodInvoice"
              name="rdPaymentModel"
              label={t("ui.storefront.common.Invoice")}
              className="custom-control custom-radio"
              checked={selectedPaymentMethod === "Invoice"}
              onChange={() => setSelectedPaymentMethod("Invoice")}
            />
            <Form.Check
              type="radio"
              id="rd_purchasePaymentMethodQuote"
              name="rdPaymentModel"
              label={t("ui.storefront.common.SalesQuote")}
              className="custom-control custom-radio"
              checked={selectedPaymentMethod === "Quote"}
              onChange={() => setSelectedPaymentMethod("Quote")}
            />
          </Col>
          <Col xs={12} sm={9}>
            {selectedPaymentMethod === "Credit Card" && (
              <PaymentMethodCreditCardBody
                continueText={continueText}
                onSubmit={(type: "PayByCreditCard" | "PayByWalletEntry", creditCardData: any) => {
                  onCompleteCheckoutPaymentStep(
                    {
                      [type]: {
                        ...creditCardData
                      }
                    },
                    ServiceStrings.checkout.paymentMethods.creditCard,
                    cartBillingContact
                  );
                }}
                submitDisabled={!cartBillingContact}
                initialWalletID={
                  existingPaymentData && existingPaymentData.PayByWalletEntry
                    ? existingPaymentData.PayByWalletEntry.WalletID
                    : null
                }
              />
            )}
            {selectedPaymentMethod === "Invoice" && (
              <PaymentMethodInvoiceBody
                continueText={continueText}
                onSubmit={(invoiceFormData: any) => {
                  onCompleteCheckoutPaymentStep(
                    {
                      PayByBillMeLater: {
                        ...invoiceFormData
                      }
                    },
                    ServiceStrings.checkout.paymentMethods.invoice,
                    cartBillingContact
                  );
                }}
                submitDisabled={!cartBillingContact}
              />
            )}
            {selectedPaymentMethod === "Quote" && (
              <PaymentMethodQuoteBody
                continueText={continueText}
                onSubmit={() => {
                  onCompleteCheckoutPaymentStep(
                    {},
                    ServiceStrings.checkout.paymentMethods.quoteMe,
                    cartBillingContact
                  );
                }}
                submitDisabled={!cartBillingContact}
              />
            )}
          </Col>
        </Row>
      </>
    );
  }
);
