import { Card, Col, Row, Form } from "react-bootstrap";
import { WalletModel } from "../../_api/cvApi._DtoClasses";
import { useTranslation } from "react-i18next";
import { useViewState } from "../../_shared/customHooks/useViewState";
import cvApi from "../../_api/cvApi";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCcDiscover,
  faCcMastercard,
  faCcVisa
} from "@fortawesome/free-brands-svg-icons";
import { faCreditCard } from "@fortawesome/free-regular-svg-icons";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";

interface IDefaultWalletEntryCardProps {
  afterDefaultSet?: Function;
  wallet: WalletModel[];
}

export const DefaultWalletEntryCard = (props: IDefaultWalletEntryCardProps) => {
  const { afterDefaultSet, wallet } = props;

  const { setRunning, finishRunning, viewState } = useViewState();
  const { t } = useTranslation();

  function setDefaultWalletEntryForCurrentUser(ID: number): void {
    setRunning();
    cvApi.payments
      .CurrentUserMarkWalletEntryAsDefault(ID)
      .then((res) => {
        if (afterDefaultSet) {
          afterDefaultSet();
        }
        finishRunning();
      })
      .catch((err) => {
        finishRunning(true, err);
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

  const defaultWalletEntry = wallet ? wallet.find((w) => w.IsDefault) : null;

  return (
    <Card className="h-100 border-info position-relative">
      {viewState.running ? <LoadingWidget overlay={true} /> : null}
      <Card.Header className="alert-info">
        {t("ui.storefront.userDashboard.wallet.DefaultEntry")}
      </Card.Header>
      <Card.Body className="p-2">
        <Form.Select
          className="form-control custom-select"
          onChange={(e) => {
            setDefaultWalletEntryForCurrentUser(+e.target.value);
          }}
          value={defaultWalletEntry ? defaultWalletEntry.ID : null}
          id="ddlDefaultEntry"
          name="ddlDefaultEntry">
          <option value={null} disabled={defaultWalletEntry != null}>
            {t("ui.storefront.userDashboard.wallet.PleaseSelectADefaultEntry")}
          </option>
          {wallet.map((wallet: WalletModel) => {
            const { ID, Name } = wallet;
            return (
              <option key={ID} label={Name} value={ID}>
                {Name}
              </option>
            );
          })}
        </Form.Select>
      </Card.Body>
      {defaultWalletEntry ? (
        <Card.Body className="p-2">
          <div className={`w-100 wallet-panel`}>
            <div className={`w-100 p-2`}>
              <Row className="align-items-center">
                <Col>
                  <p className="h3 mt-0" id="WalletEntryCardNumber">
                    &bull;&bull;&bull;&bull;&nbsp;
                    &bull;&bull;&bull;&bull;&nbsp;
                    &bull;&bull;&bull;&bull;&nbsp;
                    {defaultWalletEntry.CreditCardNumber}
                  </p>
                </Col>
                <Col xs="auto">
                  <p className="h3 mt-0">
                    <FontAwesomeIcon
                      icon={getIconByCardType(defaultWalletEntry.CardType)}
                    />
                    <span className="sr-only">
                      {defaultWalletEntry.CardType}
                    </span>
                  </p>
                </Col>
              </Row>
              <Row>
                <Col>
                  <p id="WalletCardHolderName" className="small mb-1">
                    {t("ui.storefront.wallet.walletCard.CardHolderName")}
                  </p>
                  <p className="mb-1">{defaultWalletEntry.CardHolderName}</p>
                </Col>
                <Col xs="auto">
                  <p id="CheckoutCardValidThrough" className="small mb-1">
                    {t("ui.storefront.checkout.checkoutCard.ValidThrough")}
                  </p>
                  <p className="mb-1">
                    {defaultWalletEntry.ExpirationMonth.toString().padStart(
                      2,
                      "0"
                    )}
                    &#47;
                    {defaultWalletEntry.ExpirationYear}
                  </p>
                </Col>
              </Row>
            </div>
          </div>
        </Card.Body>
      ) : null}
    </Card>
  );
};
