import { useState, useEffect, useCallback, Fragment, useReducer } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import { CheckoutStepsHeader } from "./steps/_shared/CheckoutStepsHeader";
import { MiniCart } from "../Cart/views/MiniCart";
import cvApi from "../_api/cvApi";
import { ErrorView } from "../_shared/common/ErrorView";
import { LoadingWidget } from "../_shared/common/LoadingWidget";
import { useViewState } from "../_shared/customHooks/useViewState";
import { ICheckoutProps, IMapStateToCheckoutProps, IQuoteIDs } from "./_checkoutTypes";
import { IPurchaseStep, purchaseStepFactory } from "./steps/checkoutSteps";
import { CheckoutMethodStep } from "./steps/method/CheckoutMethodStep";
import { CheckoutShippingStep } from "./steps/shipping/CheckoutShippingStep";
import { CheckoutSplitShippingStep } from "./steps/shipping/CheckoutSplitShippingStep";
import { CheckoutPaymentStep } from "./steps/payment/CheckoutPaymentStep";
import { CheckoutConfirmationStep } from "./steps/confirmation/CheckoutConfirmationStep";
import { AccountContactModel } from "../_api/cvApi._DtoClasses";
import { ServiceStrings } from "../_shared/ServiceStrings";
import { IReduxStore } from "../_redux/_reduxTypes";
import { Container, Row, Col } from "react-bootstrap";
import { ProcessCurrentQuoteCartToTargetQuotesDto } from "../_api/cvApi.Providers";

const mapStateToProps = (state: IReduxStore): IMapStateToCheckoutProps => {
  return {
    cart: state.cart,
    quoteCart: state.quoteCart,
    cefConfig: state.cefConfig,
    currentAccountAddressBook: state.currentAccountAddressBook,
    currentUser: state.currentUser
  };
};

export const Checkout = connect(mapStateToProps)((props: ICheckoutProps) => {
  const {
    currentUser,
    currentAccountAddressBook,
    cart,
    quoteCart, // TODO: Implement Quote Cart checkout
    cartType,
    cefConfig
  } = props;

  //const [cartBillingContact, setCartBillingContact] = useState<AccountContactModel>(null);
  //const [cartShippingContact, setCartShippingContact] = useState<AccountContactModel>(null);
  //const [paymentStepData, setPaymentStepData] = useState<any>(null);
  //const [sameAsBilling, setSameAsBilling] = useState<boolean>(false);
  const [step, setStep] = useState<number>(1);
  const [allSteps, setAllSteps] = useState<Array<IPurchaseStep>>(null);
  const [completedOrderID, setCompletedOrderID] = useState<number>();
  const [completedMasterOrderID, setCompletedMasterOrderID] = useState<number>(null);
  const [completedSubOrderIDs, setCompletedSubOrderIDs] = useState<Array<number>>(null);
  const [completedQuoteIDs, setCompletedQuoteIDs] = useState<IQuoteIDs>(null);

  const { setRunning, finishRunning, viewState } = useViewState();
  const { t } = useTranslation();

  const [state, dispatch] = useReducer(reducer, {
    shippingStepData: {
      sameAsBilling: false,
      cartShippingContact: null
    },
    paymentStepData: {
      paymentStyle: null,
      cartBillingContact: null,
      payBy: null
    }
  });

  useEffect(() => {
    proceedOrPurchase();
  }, [state]);

  const getAddressBook = useCallback(() => {
    let defaultBillingContact;
    if (Array.isArray(currentAccountAddressBook)) {
      defaultBillingContact = currentAccountAddressBook.find(
        (accountContact: AccountContactModel) => accountContact.IsBilling
      );
    }
    if (defaultBillingContact) {
      // setDefaultBilling(defaultBillingContact.Slave);
    }
  }, [currentAccountAddressBook]);

  useEffect(() => {
    getAddressBook();
  }, [currentAccountAddressBook, getAddressBook]);

  useEffect(() => {
    if (!currentUser.AccountID) {
      setStep(1);
    }
  }, [currentUser.AccountID]);

  const proceedOrPurchase = (): void => {
    if (step !== allSteps?.length - 1) {
      setStep((prevStep) => prevStep + 1);
      return;
    }
    if (state.paymentStepData?.paymentStyle && state.paymentStepData?.payBy) {
      confirmAndPurchase();
    }
  };

  function reducer(state: any, action: { type: string; payload: any }) {
    switch (action.type) {
      case "paymentStepComplete":
        return {
          ...state,
          paymentStepData: {
            paymentStyle: action.payload.paymentStyle,
            cartBillingContact: action.payload.billingContact,
            payBy: action.payload.payBy
          }
        };
      case "shippingStepComplete":
        return {
          ...state,
          shippingStepData: {
            cartShippingContact: action.payload.cartShippingContact,
            sameAsBilling: action.payload.sameAsBilling
          }
        };
      case "splitShippingStepComplete":
        return {
          ...state,
          shippingComplete: action.payload.shippingComplete
        };
      default:
        throw new Error();
    }
  }

  async function confirmAndPurchase() {
    if (!state.paymentStepData?.paymentStyle || !state.paymentStepData?.payBy) {
      console.debug(state);
      console.error("Invalid payment or shipping data. Could not proceed to purchase.");
      return;
    }
    setRunning();
    const { paymentStyle, ...paymentData } = state.paymentStepData;
    const order = {
      Billing: state.paymentStepData.cartBillingContact?.Slave,
      IsPartialPayment: false,
      IsSameAsBilling: state.shippingStepData.sameAsBilling,
      ...paymentData,
      PaymentStyle: paymentStyle,
      SpecialInstructions: null,
      WithCartInfo: {
        CartSessionID: cartType && cartType === "Quote" ? quoteCart.SessionID : cart.SessionID,
        CartTypeName: cartType
      },
      WithUserInfo: {
        IsNewAccount: false,
        UserID: currentUser.ID,
        UserName: currentUser.UserName
      }
    };
    if (paymentStyle === ServiceStrings.checkout.paymentMethods.creditCard) {
      order.Shipping = state.shippingStepData.sameAsBilling
        ? state.paymentStepData.cartBillingContact?.Slave
        : state.shippingStepData.cartShippingContact?.Slave;
    }
    if (paymentStyle === ServiceStrings.checkout.paymentMethods.quoteMe) {
      const { Billing, paymentStyle, ...orderDataForQuote } = order;
      purchaseWithQuote(orderDataForQuote);
      return;
    }
    if (cefConfig.featureSet.shipping.splitShipping.enabled) {
      cvApi.providers
        .ProcessCurrentCartToTargetOrders(order)
        .then((result) => {
          if (result.data.Succeeded) {
            setCompletedMasterOrderID(result.data.OrderIDs[0]);
            setCompletedSubOrderIDs([...result.data.OrderIDs.slice(1)]);
            setStep((prevStep) => prevStep + 1);
            finishRunning();
          } else {
            finishRunning(true, null, result.data.ErrorMessages);
          }
        })
        .catch((err: any) => {
          finishRunning(true, err.message || "Failed to process current cart to target orders");
        });
      return;
    }
    cvApi.providers
      .ProcessCurrentCartToSingleOrder(order)
      .then((result) => {
        if (result.data.Succeeded) {
          setCompletedOrderID(result.data.OrderID);
          setStep((prevStep) => prevStep + 1);
          finishRunning();
        } else {
          finishRunning(true, null, result.data.ErrorMessages);
        }
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "Failed to process current cart to target orders");
      });
  }

  function purchaseWithQuote(orderDataForQuote: ProcessCurrentQuoteCartToTargetQuotesDto) {
    cvApi.providers
      .ProcessCurrentQuoteCartToTargetQuotes(orderDataForQuote)
      .then((result) => {
        if (!result.data.Succeeded) {
          finishRunning(true, null, result.data.ErrorMessages);
          return;
        }
        const { QuoteIDs } = result.data;
        const [masterQuoteID, subQuoteID] = QuoteIDs;
        setCompletedQuoteIDs({
          master: masterQuoteID,
          slave: subQuoteID
        });
        setStep((prevStep) => prevStep + 1);
        finishRunning();
      })
      .catch((err) => {
        finishRunning(true, err);
      });
  }

  const onCompleteCheckoutMethodStep = () => {
    proceedOrPurchase();
  };

  const onCompleteCheckoutShippingStep = (
    cartShippingContact: AccountContactModel,
    sameAsBilling: boolean
  ) => {
    /* setCartShippingContact(shippingContact);
    setSameAsBilling(sameAsBilling); */
    dispatch({
      type: "shippingStepComplete",
      payload: { cartShippingContact: cartShippingContact, sameAsBilling: sameAsBilling }
    });
    proceedOrPurchase();
  };

  const onCompleteCheckoutSplitShippingStep = () => {
    dispatch({
      type: "splitShippingStepComplete",
      payload: { shippingComplete: true }
    });
    proceedOrPurchase();
  };

  const onCompleteCheckoutPaymentStep = (
    paymentData: any,
    paymentStyle: string,
    billingContact: AccountContactModel
  ) => {
    // TODO: create promise and/or use callbacks to make sure
    // state is set before calling proceedOrPurchase()
    /* setCartBillingContact(billingContact);
    setPaymentStepData({
      ...paymentData,
      paymentStyle
    }); */
    dispatch({
      type: "paymentStepComplete",
      payload: {
        paymentStyle: paymentStyle,
        cartBillingContact: billingContact,
        payBy: paymentData
      }
    });
    proceedOrPurchase();
  };

  useEffect(() => {
    if (!cefConfig || allSteps) {
      return;
    }
    const stepsToShow = purchaseStepFactory(cefConfig);
    const fullSteps = stepsToShow.map((step, index) => {
      const continueText = t(stepsToShow[index + 1]?.continueTextKey ?? "");
      return {
        ...step,
        continueText
      };
    });
    setAllSteps(fullSteps);
  }, [cefConfig]);

  if (
    (cartType === "Cart" && !cart?.SalesItems?.length) ||
    (cartType === "Quote" && !quoteCart?.SalesItems?.length)
  ) {
    return (
      <section className="section-checkout">
        <div className="container">
          <div className="section-head">
            <h1 className="title">{t("ui.storefront.checkout.checkoutPanels.Checkout")}</h1>
          </div>
          <div className="row">
            <div className="col-12">
              <h2 className="h5">{t("ui.storefront.checkout.checkoutPanels.CartIsEmpty")}</h2>
            </div>
          </div>
        </div>
      </section>
    );
  }

  return (
    <section className="section-checkout">
      <Container>
        <div className="section-head">
          <h1 className="title">{t("ui.storefront.checkout.checkoutPanels.Checkout")}</h1>
        </div>
        <Row>
          <Col xs={12} lg={8}>
            <div className="checkout-steps">
              {allSteps &&
                allSteps.map((stepData, index) => {
                  const { continueText, titleKey } = stepData;
                  let component = null;
                  switch (stepData.name) {
                    case ServiceStrings.checkout.steps.shipping:
                      component = (
                        <CheckoutShippingStep
                          key={stepData.name}
                          continueText={continueText}
                          onCompleteCheckoutShippingStep={onCompleteCheckoutShippingStep}
                          accountContacts={currentAccountAddressBook}
                          cartBillingContact={state.paymentStepData.cartBillingContact}
                        />
                      );
                      break;
                    case ServiceStrings.checkout.steps.confirmation:
                      component = (
                        <CheckoutConfirmationStep
                          key={stepData.name}
                          orderID={completedOrderID}
                          quoteIDs={completedQuoteIDs}
                        />
                      );
                      break;
                    case ServiceStrings.checkout.steps.completed:
                      component = (
                        <CheckoutConfirmationStep
                          key={stepData.name}
                          orderID={completedOrderID}
                          quoteIDs={completedQuoteIDs}
                          masterOrderID={completedMasterOrderID}
                          subOrderIDs={completedSubOrderIDs}
                        />
                      );
                      break;
                    case ServiceStrings.checkout.steps.method:
                      component = (
                        <CheckoutMethodStep
                          key={stepData.name}
                          continueText={continueText}
                          onCompleteCheckoutMethodStep={onCompleteCheckoutMethodStep}
                        />
                      );
                      break;
                    case ServiceStrings.checkout.steps.payment:
                      component = (
                        <CheckoutPaymentStep
                          key={stepData.name}
                          continueText={continueText}
                          onCompleteCheckoutPaymentStep={onCompleteCheckoutPaymentStep}
                          initialBillingContact={state.paymentStepData.cartBillingContact}
                          existingPaymentData={state.paymentStepData.payBy}
                        />
                      );
                      break;
                    case ServiceStrings.checkout.steps.splitShipping:
                      component = (
                        <CheckoutSplitShippingStep
                          key={stepData.name}
                          continueText={continueText}
                          onCompleteCheckoutSplitShippingStep={onCompleteCheckoutSplitShippingStep}
                          accountContacts={currentAccountAddressBook}
                          cartBillingContact={state.paymentStepData.cartBillingContact}
                        />
                      );
                      break;
                    default:
                      console.log("Failed to find step for checkout in cefConfig");
                      break;
                  }
                  return (
                    <Fragment key={titleKey}>
                      <Row>
                        <Col xs={12} sm>
                          <CheckoutStepsHeader
                            valid={step >= index + 2}
                            titleKey={titleKey}
                            index={index + 1}
                            allowEdit={index + 1 < step && step < allSteps.length}
                            onEditClicked={() => setStep(index + 1)}
                          />
                          <hr />
                        </Col>
                      </Row>
                      <Row>
                        <Col xs={12}>
                          {step === index + 1 && !viewState.running ? (
                            component
                          ) : step === index + 1 && viewState.running ? (
                            <LoadingWidget />
                          ) : null}
                          {step === index + 1 && viewState.hasError ? (
                            <ErrorView error={viewState.errorMessage} />
                          ) : null}
                        </Col>
                      </Row>
                    </Fragment>
                  );
                })}
            </div>
          </Col>
          <Col xs={12} lg={4}>
            <MiniCart />
          </Col>
        </Row>
        <ErrorView error={viewState.errorMessage} />
      </Container>
    </section>
  );
});
