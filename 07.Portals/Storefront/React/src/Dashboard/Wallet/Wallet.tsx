import { useEffect, useState } from "react";
import {
  faCcDiscover,
  faCcMastercard,
  faCcVisa
} from "@fortawesome/free-brands-svg-icons";
import { faCreditCard } from "@fortawesome/free-regular-svg-icons";
import { faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { ConfirmationModal } from "../../_shared/modals";
import cvApi from "../../_api/cvApi";
import { connect } from "react-redux";
import { setWallet } from "../../_redux/actions/walletActions";
import { NewWalletModal } from "./NewWalletModal";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { useTranslation } from "react-i18next";
import { Card, Col, Row, Button, ButtonGroup } from "react-bootstrap";
import { WalletModel } from "../../_api/cvApi._DtoClasses";
import { DefaultWalletEntryCard } from "./DefaultWalletEntryCard";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { IReduxStore } from "../../_redux/_reduxTypes";

const thisYear = new Date().getFullYear();

const availableExpirationYears: number[] = [];
for (let i = 0; i < 11; i++) {
  availableExpirationYears.push(thisYear + i);
}

interface IWalletProps {
  wallet?: WalletModel[]; // redux
}

const mapStateToProps = (state: IReduxStore) => {
  return {
    wallet: state.wallet
  };
};

export const Wallet = connect(mapStateToProps)((props: IWalletProps): JSX.Element => {
  const { wallet } = props;

  const [showAddCard, setShowAddCard] = useState<boolean>(false);
  const [deleteWalletItem, setDeleteWalletItem] = useState<WalletModel>(null);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

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
        if (!res.data.ActionSucceeded) {
          finishRunning(true);
          return;
        }
        if (setWallet) {
          setWallet(res.data.Result);
        }
        finishRunning();
      })
      .catch((err: any) => {
        console.log(err);
      });
  }

  function addUserWalletItem(data: any): void {
    setRunning();
    const {
      CreditCardNumber,
      CardNickName,
      CardHolderName,
      ExpirationMonth,
      ExpirationYear
    } = data;
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
          setShowAddCard(false);
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

  function deactiveUserWalletItem(id: number): void {
    setRunning();
    cvApi.payments
      .DeactivateCurrentUserWalletEntry(id)
      .then((res) => {
        if (!res.data.ActionSucceeded) {
          finishRunning(true, "Failed to deactivate wallet item");
          return;
        }
        getUserWallet();
        setDeleteWalletItem(null);
      })
      .catch((err: any) => {
        console.log(err);
      });
  }

  const getIconByCardType = (type: string) => {
    switch (type) {
      case "visa":
        return faCcVisa;
      case "mastercard":
        return faCcMastercard;
      case "discover":
        return faCcDiscover;
      default:
        return faCreditCard;
    }
  };

  return (
    <Row className="position-relative">
      {viewState.running ? <LoadingWidget overlay={true} /> : null}
      <Col xs={12}>
        <h1>{t("ui.storefront.userDashboard2.userDashboard.Wallet")}</h1>
        <Row className="row-cols-1 row-cols-sm-2 row-cols-md-2 row-cols-lg-3 row-cols-xl-3 row-cols-tk-5 row-cols-fk-6">
          <Col className="mb-3">
            <Card className="h-100">
              <Card.Header className="alert-success">
                {t("ui.storefront.userDashboard.wallet.AddANewEntry")}
              </Card.Header>
              <Card.Body className="p-2">
                <Button
                  variant="outline-secondary"
                  className="btn-block cef-add-card-to-wallet p-4 h-100 w-100 mb-1"
                  onClick={() => setShowAddCard(true)}
                  id="btnAddCreditCardWallet"
                  name="btnAddCreditCardWallet"
                  disabled={showAddCard}
                  style={{ height: "216px" }}>
                  <svg
                    width="30px"
                    height="31px"
                    viewBox="0 0 30 31"
                    version="1.1"
                    xmlns="http://www.w3.org/2000/svg">
                    <g
                      stroke="none"
                      strokeWidth="1"
                      fill="none"
                      fillRule="evenodd">
                      <g
                        transform="translate(-847.000000, -260.000000)"
                        fill="#B6B6B6">
                        <path d="M862,260.370117 C853.720465,260.370117 847,267.090582 847,275.370117 C847,283.649652 853.720465,290.370117 862,290.370117 C870.279535,290.370117 877,283.649652 877,275.370117 C877,267.090582 870.279535,260.370117 862,260.370117 Z M862,261.067792 C869.903256,261.067792 876.302326,267.466861 876.302326,275.370117 C876.302326,283.273373 869.903256,289.672443 862,289.672443 C854.096744,289.672443 847.697674,283.273373 847.697674,275.370117 C847.697674,267.466861 854.096744,261.067792 862,261.067792 Z M861.956279,267.336164 L861.956279,267.336164 C861.776744,267.357559 861.643721,267.513838 861.651163,267.695699 L861.651163,275.02128 L854.325581,275.02128 C854.303721,275.01942 854.28186,275.01942 854.26,275.02128 C853.794884,275.065001 853.860465,275.762675 854.325581,275.718954 L861.651163,275.718954 L861.651163,283.044536 C861.647442,283.238954 861.805581,283.398954 862,283.398954 C862.194419,283.398954 862.352558,283.238954 862.348837,283.044536 L862.348837,275.718954 L869.674419,275.718954 C869.868837,275.722675 870.028837,275.564536 870.028837,275.370117 C870.028837,275.175699 869.868837,275.017559 869.674419,275.02128 L862.348837,275.02128 L862.348837,267.695699 C862.35814,267.47942 862.170698,267.308722 861.956279,267.336164 Z"></path>
                      </g>
                    </g>
                  </svg>
                  <br />
                  <span>{t("ui.storefront.wallet.walletList.AddCard")}</span>
                </Button>
              </Card.Body>
            </Card>
          </Col>
          {wallet && wallet.length ? (
            <Col className="mb-3">
              <DefaultWalletEntryCard
                afterDefaultSet={() => getUserWallet()}
                wallet={wallet}
              />
            </Col>
          ) : null}
          <NewWalletModal
            show={showAddCard}
            onConfirm={addUserWalletItem}
            onCancel={() => setShowAddCard(false)}
            loading={viewState.running}
          />
        </Row>
        <Row className="row-cols-1 row-cols-sm-2 row-cols-md-2 row-cols-lg-3 row-cols-xl-3 row-cols-tk-5 row-cols-fk-6">
          {wallet &&
            wallet.map((walletItem: WalletModel): JSX.Element => {
              const {
                ID,
                Name,
                CardType,
                CardHolderName,
                CreditCardNumber,
                ExpirationMonth,
                ExpirationYear
              } = walletItem;
              return (
                <Col key={ID.toString()} className="mb-3">
                  <Card className="form-vertical wallet-card-panel h-100">
                    <Card.Header className="d-flex justify-content-between align-items-center">
                      <h4 className="m-0 bold d-inline">{Name}</h4>
                      <FontAwesomeIcon
                        icon={getIconByCardType(CardType)}
                        className="fa-2x"
                      />
                      <span className="sr-only">{CardType}</span>
                    </Card.Header>
                    <Card.Body className="p-2">
                      <p className="font-weight-bold">{CardHolderName}</p>
                      <p id="CardNumberText">
                        &bull;&bull;&bull;&bull;&nbsp;&bull;&bull;&bull;&bull;&nbsp;&bull;&bull;&bull;&bull;&nbsp;
                        {CreditCardNumber}
                      </p>
                      <div className="mb-0" id="WalletEntryExpirationText">
                        <span>
                          {t(
                            "ui.storefront.wallet.walletCard.Expiration.Abbreviation"
                          )}
                        </span>
                        <span>
                          {ExpirationMonth.toString().padStart(2, "0")}/
                          {ExpirationYear}
                        </span>
                      </div>
                    </Card.Body>
                    <ButtonGroup as={Card.Footer} className="p-0">
                      {/* TODO- show errors */}
                      <Button
                        variant="link"
                        size="lg"
                        className="text-danger"
                        onClick={() => setDeleteWalletItem(walletItem)}
                        id="btnCardEntryDelete"
                        name="btnCardEntryDelete"
                        title="Delete">
                        <FontAwesomeIcon icon={faTrash} aria-hidden="true" />
                        <span className="ml-1">
                          {t("ui.storefront.common.Remove")}
                        </span>
                      </Button>
                    </ButtonGroup>
                  </Card>
                </Col>
              );
            })}
          <ConfirmationModal
            show={!!deleteWalletItem}
            title="Are you sure you want to remove this card?"
            confirmBtnLabel="Yes"
            cancelBtnLabel="No"
            onConfirm={() => deactiveUserWalletItem(deleteWalletItem.ID)}
            onCancel={() => setDeleteWalletItem(null)}
            size="md">
            <Card.Body className="p-2">
              <p className="font-weight-bold">
                {deleteWalletItem?.CardHolderName}
              </p>
              <p id="CardNumberText">
                &bull;&bull;&bull;&bull;&nbsp;&bull;&bull;&bull;&bull;&nbsp;&bull;&bull;&bull;&bull;&nbsp;
                {deleteWalletItem?.CreditCardNumber}
              </p>
              <div className="mb-0" id="WalletEntryExpirationText">
                <span>
                  {t("ui.storefront.wallet.walletCard.Expiration.Abbreviation")}
                </span>
                <span>
                  {deleteWalletItem?.ExpirationMonth.toString().padStart(
                    2,
                    "0"
                  )}
                  /{deleteWalletItem?.ExpirationYear}
                </span>
              </div>
            </Card.Body>
          </ConfirmationModal>
        </Row>
      </Col>
    </Row>
  );
});
