import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { connect } from "react-redux";
import { setWallet } from "../_redux/actions/walletActions";
import {
  IPaymentMethodCreditCardBodyProps,
  ICreditCardFormData,
  IMapStateToPaymentMethodCreditCardBodyProps
} from "./_checkoutTypes";
import { useTranslation } from "react-i18next";
import { SelectFormGroup } from "../_shared/forms/formGroups";
import { useViewState } from "../_shared/customHooks/useViewState";
import { WalletBlock } from "../Dashboard/Wallet/WalletBlock";
import cvApi from "../_api/cvApi";
import { IReduxStore } from "../_redux/_reduxTypes";
import { Form, Row, Col, Button, InputGroup } from "react-bootstrap";
import { CheckoutPayByCreditCard, WalletModel } from "../_api/cvApi._DtoClasses";
import { NewWalletModal } from "../Dashboard/Wallet/NewWalletModal";

const thisYear = new Date().getFullYear();

const availableExpirationYears: number[] = [];
for (let i = 0; i < 11; i++) {
  availableExpirationYears.push(thisYear + i);
}

const mapStateToProps = (state: IReduxStore): IMapStateToPaymentMethodCreditCardBodyProps => {
  return {
    wallet: state.wallet
  };
};

export const PaymentMethodCreditCardBody = connect(mapStateToProps)(
  (props: IPaymentMethodCreditCardBodyProps): JSX.Element => {
    const {
      wallet,
      onSubmit,
      onSelectedWalletChanged,
      continueText,
      submitDisabled,
      initialWalletID
    } = props;

    const [selectedWalletItem, setSelectedWalletItem] = useState(
      initialWalletID && Array.isArray(wallet) ? wallet.find((w) => w.ID === initialWalletID) : null
    );
    const [walletCVV, setWalletCVV] = useState<string>("");
    const [showNewWalletModal, setShowNewWalletModal] = useState(false);
    const { t } = useTranslation();
    const { setRunning, finishRunning, viewState } = useViewState();

    const {
      register,
      handleSubmit,
      formState: { errors }
    } = useForm({
      reValidateMode: "onBlur"
    });

    useEffect(() => {
      if (onSelectedWalletChanged) {
        onSelectedWalletChanged(selectedWalletItem);
      }
    }, [selectedWalletItem]);

    const onSubmitCreditCardForm = (data: ICreditCardFormData): void => {
      const {
        txtCVV,
        txtCardHolderName,
        txtCardNumber,
        txtReferenceName,
        ddlExpirationMonth,
        ddlExpirationYear
      } = data;
      if (selectedWalletItem && walletCVV?.length > 2) {
        onSubmit("PayByWalletEntry", {
          WalletCVV: walletCVV,
          WalletID: selectedWalletItem.ID
        });
        return;
      }
      onSubmit("PayByCreditCard", {
        CVV: txtCVV,
        CardHolderName: txtCardHolderName,
        CardNumber: txtCardNumber,
        CardType: "Visa",
        ExpirationMonth: +ddlExpirationMonth,
        ExpirationYear: ddlExpirationYear
      }); //CheckoutPayByCreditCard
    };

    useEffect(() => {
      if (!wallet) {
        getUserWallet();
      }
    }, []);

    function getUserWallet(): void {
      setRunning();
      cvApi.payments
        .GetCurrentUserWallet()
        .then((res) => {
          if (!res.data?.ActionSucceeded) {
            finishRunning(true, " Failed to get current user wallet");
            return;
          }
          setWallet(res.data.Result);
          finishRunning();
        })
        .catch((err) => {
          finishRunning(true, err);
        });
    }

    function addUserWalletItem(data: any): void {
      setRunning();
      const { CreditCardNumber, CardNickName, CardHolderName, ExpirationMonth, ExpirationYear } =
        data;
      const walletData = {
        CreditCardNumber,
        // AccountNumber: "",
        // RoutingNumber: "",
        // BankName: "",
        Name: CardNickName,
        CardHolderName,
        ExpirationMonth,
        ExpirationYear
        // Token: "",
        // CardType: "",
        // UserID: 1,
        // UserKey: "",
        // User: UserModel,
        // CurrencyID: 1,
        // CurrencyKey: "USD",
        // CurrencyName: ""
        // Currency: CurrencyModel
      } as WalletModel;
      cvApi.payments
        .CreateCurrentUserWalletEntry(walletData)
        .then((res) => {
          if (res.data.ActionSucceeded) {
            setShowNewWalletModal(false);
            getUserWallet();
            finishRunning();
          } else {
            alert("Failed to add wallet item.");
            finishRunning(true, "Failed to add wallet item.");
          }
        })
        .catch((err) => {
          finishRunning(true, err.message || "Failed to add wallet item.");
        });
    }

    let initialOption = null;
    if (initialWalletID && Array.isArray(wallet)) {
      const foundWallet = wallet.find((w) => w.ID === initialWalletID);
      if (foundWallet != null) {
        initialOption = {
          option: foundWallet.ID.toString(),
          value: foundWallet.ID.toString()
        };
      }
    }

    return (
      <>
        <Row as={Form} className="mb-3" onSubmit={handleSubmit(onSubmitCreditCardForm)}>
          <Col xs={12}>
            <Row>
              <Col xs={12}>
                <InputGroup className="d-flex align-items-end">
                  <SelectFormGroup
                    register={register}
                    errors={errors}
                    formIdentifier="ddlSelectedCard"
                    formClasses="flex-grow-1"
                    options={
                      wallet && Array.isArray(wallet)
                        ? wallet.map((card) => {
                            return {
                              ...card,
                              option: card.Name,
                              value: card.ID.toString()
                            };
                          })
                        : []
                    }
                    initialOption={initialOption}
                    includeNull={true}
                    nullKey="ui.storefront.checkout.views.paymentInformation.PleaseSelectACardOrEnterANewOne"
                    labelKey="ui.storefront.checkout.views.paymentInformation.selectACard"
                    extraOnChange={(e) => {
                      if (e.target.value.match(/^\d+$/)) {
                        setSelectedWalletItem(
                          wallet.find((w) => w.ID.toString() === e.target.value)
                        );
                      } else {
                        setSelectedWalletItem(null);
                      }
                    }}
                  />
                  <Button variant="secondary" onClick={() => setShowNewWalletModal(true)}>
                    {t("ui.storefront.checkout.views.paymentInformation.AddCardToWallet")}
                  </Button>
                </InputGroup>
              </Col>
              {selectedWalletItem && (
                <WalletBlock
                  walletItem={selectedWalletItem}
                  walletCVV={walletCVV}
                  setWalletCVV={setWalletCVV}
                />
              )}
            </Row>
            <Row>
              <Col xs={12}>
                <Button
                  variant="primary"
                  className="my-3"
                  id="btnSubmit_purchaseStepPayment"
                  disabled={walletCVV.length < 3 || submitDisabled} // TODO: Replace with allowing button click but trigger validation when cvv is empty
                  title="Confirm Order and Purchase"
                  type="submit">
                  {continueText ??
                    t("ui.storefront.checkout.views.paymentInformation.confirmOrderAndPurchase")}
                </Button>
              </Col>
            </Row>
          </Col>
        </Row>
        <NewWalletModal
          show={showNewWalletModal}
          onConfirm={addUserWalletItem}
          onCancel={() => setShowNewWalletModal(false)}
        />
      </>
    );
  }
);
