import { useState, useEffect } from "react";
import { connect } from "react-redux";
import { AddressBlock } from "../../../Dashboard/AddressBook/AddressBlock";
import cvApi from "../../../_api/cvApi";
import { AccountContactModel } from "../../../_api/cvApi._DtoClasses";
import { LoadingWidget } from "../../../_shared/common/LoadingWidget";
import { useViewState } from "../../../_shared/customHooks/useViewState";
import { PurchaseRateQuotesManagerWidget } from "../../PurchaseRateQuotesManagerWidget";
import { ICheckoutShippingStepProps } from "../../_checkoutTypes";
import { Row, Col, Button, Form } from "react-bootstrap";
import { ContactSwitch } from "../../../Dashboard/AddressBook/ContactSwitch";

const mapStateToProps = (state: any) => {
  return {
    cart: state.cart
  };
};

export const CheckoutShippingStep = connect(mapStateToProps)(
  (props: ICheckoutShippingStepProps): JSX.Element => {
    const {
      onCompleteCheckoutShippingStep,
      cartBillingContact,
      accountContacts,
      cart,
      continueText
    } = props;

    const [sameAsBilling, setSameAsBilling] = useState<boolean>(false);
    const [estimatedShippingCost, setEstimatedShippingCost] = useState<any | null>(null);
    const [cartShippingContact, setCartShippingContact] = useState<AccountContactModel>(null);
    const [shippingRateQuotes, setShippingRateQuotes] = useState<any | null>(null);

    const { setRunning, finishRunning, viewState } = useViewState();

    useEffect(() => {
      onChangeWithRatesReset();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [sameAsBilling]);

    const onChangeWithRatesReset = () => {
      setRunning();
      cvApi.shopping
        .ClearCurrentCartShippingRateQuote({ TypeName: "Cart" })
        .then((_res: any) => {
          finishRunning();
        })
        .catch((err: any) => {
          finishRunning(true, err.message || "Failed to clear current cart shipping rate quote");
        });
    };

    function getShippingRateQuotes() {
      setRunning();
      cvApi.shopping
        .GetCurrentCartShippingRateQuotes({
          Expedited: false,
          TypeName: "Cart"
        })
        .then((res: any) => {
          if (res.data.ActionSucceeded) {
            setShippingRateQuotes(res.data.Result);
            finishRunning(false);
          } else {
            finishRunning(true, "Failed to get shipping rate quotes");
          }
        })
        .catch((err: any) => {
          finishRunning(true, err.message || "Failed to get shipping rate quotes");
        });
    }

    function toggleShippingSameAsBilling() {
      setRunning();
      setShippingRateQuotes(null);
      cvApi.shopping
        .CurrentCartSetSetSameAsBilling({
          TypeName: "Cart",
          IsSameAsBilling: !sameAsBilling
        })
        .then((res: any) => {
          if (res.data.ActionSucceeded) {
            setSameAsBilling(!sameAsBilling);
            if (!sameAsBilling) {
              setCartShippingContact(null);
            }
            finishRunning(false);
          } else {
            finishRunning(true, "Failed to mark cart shipping address to be same as billing");
          }
        })
        .catch((err: any) => {
          finishRunning(
            true,
            err.message || "Failed to mark cart shipping address to be same as billing"
          );
        });
    }

    function updateCartShippingContact(contact: AccountContactModel) {
      setShippingRateQuotes(null);
      setRunning();
      cvApi.shopping
        .CurrentCartSetShippingContact({
          TypeName: "Cart",
          ShippingContact: contact.Slave
        })
        .then((res) => {
          if (res.data.ActionSucceeded) {
            setCartShippingContact(contact);
            finishRunning(false);
          } else {
            finishRunning(true, " failed to update cart shipping contact");
          }
        })
        .catch((err: any) => {
          console.log(err);
          finishRunning(true, err.message || " failed to update cart shipping contact");
        });
    }

    return (
      <Row>
        <Col xs={12}>
          <h4 className="mt-2">
            <span
              id="CheckoutBillingStageText"
              data-translate="ui.storefront.checkout.billingInfo.whichAddy">
              Which shipping address would you like to use? {viewState.running}
            </span>
            &nbsp;<span className="text-danger">*</span>
          </h4>
          <Form.Check type="checkbox" className="mb-0">
            <Form.Check.Label className="mb-0">
              <Form.Check.Input type="checkbox" onClick={toggleShippingSameAsBilling} />
              &nbsp;
              <span data-translate="ui.storefront.checkout.shippingBilling">Same as Billing</span>
            </Form.Check.Label>
          </Form.Check>
          <hr />
        </Col>
        <Col xs={12}>
          <Row>
            <Col xs={12}>
              {sameAsBilling ? (
                <AddressBlock
                  address={{
                    name: cartBillingContact.Slave.CustomKey,
                    ...cartBillingContact.Slave,
                    ...cartBillingContact.Slave.Address
                  }}
                />
              ) : (
                <ContactSwitch
                  accountContacts={accountContacts}
                  onChange={updateCartShippingContact}
                  onAddressAdded={(addressData) => {}}
                />
              )}
            </Col>
            {viewState.running && <LoadingWidget />}
            {shippingRateQuotes && (
              <Col xs={12} className="my-4">
                <PurchaseRateQuotesManagerWidget
                  quotes={shippingRateQuotes}
                  onRateSelected={(quote: any) => setEstimatedShippingCost(quote.Rate)}
                />
              </Col>
            )}
            <Col xs={12}>
              <div className="d-flex mb-4">
                <Button
                  variant="outline-primary"
                  disabled={!cartShippingContact && !sameAsBilling}
                  onClick={getShippingRateQuotes}>
                  Estimate Shipping Cost
                </Button>
                <Button
                  variant="primary"
                  disabled={estimatedShippingCost === null ? true : false}
                  onClick={() => {
                    if (estimatedShippingCost !== null) {
                      onCompleteCheckoutShippingStep(cartShippingContact, sameAsBilling);
                    }
                  }}>
                  {continueText}
                </Button>
              </div>
            </Col>
          </Row>
        </Col>
      </Row>
    );
  }
);
