/* CONTENT/LOGIC MOVED TO PAYMENT STEP, HERE FOR REFERENCE */

import { useEffect, useState } from "react";
import { Alert } from "react-bootstrap";
import { connect } from "react-redux";
import { ContactSwitch } from "../../../Dashboard/AddressBook/ContactSwitch";
import cvApi from "../../../_api/cvApi";
import { AccountContactModel } from "../../../_api/cvApi._DtoClasses";
import { IReduxStore } from "../../../_redux/_reduxTypes";
import { useViewState } from "../../../_shared/customHooks/useViewState";
import { ICheckoutBillingStepProps } from "../../_checkoutTypes";
import { useTranslation } from "react-i18next";
import { Row, Col, Button } from "react-bootstrap";
import { useAddressFactory } from "../../../_shared/customHooks/useAddressFactory";

const mapStateToProps = (state: IReduxStore) => {
  return {
    currentUser: state.currentUser,
    currentAccountAddressBook: state.currentAccountAddressBook
  };
};

export const CheckoutBillingStep = connect(mapStateToProps)(
  (props: ICheckoutBillingStepProps): JSX.Element => {
    const [cartBillingContact, setCartBillingContact] = useState(null);

    const { setRunning, finishRunning, viewState } = useViewState();
    const { t } = useTranslation();
    const addressFactory = useAddressFactory();

    const { onCompleteCheckoutBillingStep, continueText, currentUser, currentAccountAddressBook } = props;

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
        .catch((err: any) => {
          finishRunning(true, err.message || "Failed to update cart billing contact");
        });
    }

    return (
      <Row>
        <Col xs={12}>
          <h4 className="mt-2">
            <span id="CheckoutBillingStageText">
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
            {viewState.hasError ? (
              <Col xs={12}>
                <Alert variant="danger">{viewState.errorMessage}</Alert>
              </Col>
            ) : null}
            <Col xs={12}>
              <Button
                variant="primary"
                disabled={cartBillingContact ? false : true}
                className="mb-4"
                onClick={() => {
                  if (cartBillingContact) {
                    onCompleteCheckoutBillingStep(cartBillingContact);
                  }
                }}>
                {continueText}
              </Button>
            </Col>
          </Row>
        </Col>
      </Row>
    );
  }
);
