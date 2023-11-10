import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useTranslation } from "react-i18next";
import {
  faCcDiscover,
  faCcMastercard,
  faCcVisa
} from "@fortawesome/free-brands-svg-icons";
import { faCreditCard } from "@fortawesome/free-regular-svg-icons";
import { Card, Form, Col, InputGroup } from "react-bootstrap";

interface IWalletBlockProps {
  walletItem: any;
  walletCVV: string;
  setWalletCVV: Function;
}

export const WalletBlock = (props: IWalletBlockProps) => {
  const { walletItem, walletCVV, setWalletCVV } = props;
  const { t } = useTranslation();

  let icon = faCreditCard;
  if (walletItem && walletItem.CardType) {
    switch (walletItem.CardType.toLowerCase()) {
      case "visa":
        icon = faCcVisa;
        break;
      case "mastercard":
        icon = faCcMastercard;
        break;
      case "discover":
        icon = faCcDiscover;
        break;
    }
  }

  return (
    <Col xs={12}>
      <Card className="form-vertical wallet-card-panel h-100">
        <Card.Header className="d-flex justify-content-between align-items-center">
          <h4 className="m-0 bold d-inline">{walletItem.Name}</h4>
          <FontAwesomeIcon icon={icon} className="fa-2x" />
          <span className="sr-only">{walletItem.CardType}</span>
        </Card.Header>
        <Card.Body className="p-2">
          <p className="font-weight-bold">{walletItem.CardHolderName}</p>
          <p id="CardNumberText">
            &bull;&bull;&bull;&bull;&nbsp;&bull;&bull;&bull;&bull;&nbsp;&bull;&bull;&bull;&bull;&nbsp;
            {walletItem.CreditCardNumber}
          </p>
          <div
            className="mb-0 d-flex align-items-center justify-content-between"
            id="WalletEntryExpirationText">
            <div>
              <span>
                {t("ui.storefront.wallet.walletCard.Expiration.Abbreviation")}
              </span>
              <span>
                {walletItem.ExpirationMonth}/{walletItem.ExpirationYear}
              </span>
            </div>
            <Form.Group>
              <Form.Label htmlFor="txtWalletCVV">
                <span>
                  {t("ui.storefront.checkout.views.paymentInformation.CVV")}
                </span>
              </Form.Label>
              <InputGroup>
                <input
                  value={walletCVV}
                  onChange={(e) => setWalletCVV(e.target.value)}
                  style={{ width: "60px" }}
                  id="txtWalletCVV"
                />
              </InputGroup>
            </Form.Group>
          </div>
        </Card.Body>
      </Card>
    </Col>
  );
};
