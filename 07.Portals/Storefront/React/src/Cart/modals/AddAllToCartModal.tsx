import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useHistory } from "react-router-dom";
import { ConfirmationModal } from "../../_shared/modals";
import { Row, Col, Alert } from "react-bootstrap";
interface IAddAllToCartModalProps {
  count: number;
  show: boolean;
  onConfirm?: Function;
  onCancel: Function;
}

export const AddAllToCartModal = (
  props: IAddAllToCartModalProps
): JSX.Element => {
  const { count, show, onConfirm, onCancel } = props;
  const [messages, setMessages] = useState([
    `${count} unique items were added to the cart`
  ]);

  const { t } = useTranslation();
  const history = useHistory();

  const onViewCartClicked = () => {
    history.push("/cart");
    if (onConfirm) {
      onConfirm();
    }
  };

  const onContinueShoppingClicked = () => {
    onCancel();
  };

  return (
    <ConfirmationModal
      title="Add to Cart Result"
      show={show}
      confirmBtnLabel={t("ui.storefront.cart.ViewCart")}
      cancelBtnLabel={t("ui.storefront.cart.continueShopping")}
      onConfirm={onViewCartClicked}
      onCancel={onContinueShoppingClicked}
      size="md">
      <Row>
        {messages.length ? (
          <Col xs={12}>
            {messages.map((msg: string, index) => {
              let alertClass;
              const firstWord = msg.split(" ")[0];
              switch (firstWord.toUpperCase()) {
                case "ERROR":
                  alertClass = "danger";
                  break;
                case "WARNING":
                  alertClass = "warning";
                  break;
                default:
                  alertClass = "info";
              }
              return (
                <Alert
                  variant={alertClass}
                  key={msg}
                  id={`AddAllToCartModalText_${index}`}
                  className={index === messages.length - 1 ? "mb-0" : ""}>
                  {msg}
                </Alert>
              );
            })}
          </Col>
        ) : null}
      </Row>
    </ConfirmationModal>
  );
};
