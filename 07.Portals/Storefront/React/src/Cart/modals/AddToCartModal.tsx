import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useHistory } from "react-router-dom";
import { ConfirmationModal } from "../../_shared/modals";
import ImageWithFallback from "../../_shared/common/ImageWithFallback";
import { Row, Col, Alert } from "react-bootstrap";

interface IAddToCartModalProps {
  product: any;
  show: boolean;
  onCancel: Function;
}

export const AddToCartModal = (props: IAddToCartModalProps): JSX.Element => {
  const { product, show, onCancel } = props;

  const [messages, setMessages] = useState([]);

  const { t } = useTranslation();
  const history = useHistory();

  const onViewCartClicked = () => {
    history.push("/cart");
    window.location.reload();
  };

  const onContinueShoppingClicked = () => {
    onCancel();
  };

  if (!product) {
    return <div className="d-none"></div>;
  }

  return (
    <ConfirmationModal
      title={t("ui.storefront.carts.AddToCartModalTitle")}
      show={show}
      confirmBtnLabel={t("ui.storefront.cart.ViewCart")}
      cancelBtnLabel={t("ui.storefront.cart.continueShopping")}
      onConfirm={onViewCartClicked}
      onCancel={onContinueShoppingClicked}
      size="md">
      <Row>
        <div className={product ? "col-12" : "hide"}>
          <Row>
            <div className="col-sm-4">
              <ImageWithFallback
                className="img-fluid d-block mx-auto"
                alt={product.Name || product.name}
                src={product.PrimaryImageFileName}
                style={{
                  maxWidth: "150px",
                  maxHeight: "150px",
                  scale: "both"
                }}
              />
            </div>
            <div className="col-sm-8">
              <h4
                className="cef-confirmation-add-cart-title"
                id="AddToCartModalProductText">
                {product.Name}
              </h4>
              {/*
                  {!product.readPrices().isSale && <div>
                    <p className="h3 cef-confirmation-add-cart-price">
                    {product.readPrices().base}
                    </p>
                    <p className="h3 cef-confirmation-add-cart-price"
                    {product.readPrices().sale}
                    </p> 
                  </div>}
                */}
            </div>
          </Row>
        </div>
        {/*
            <div className={`text-center col-sm-${product ? '4' : '12'}`}>
              <p className="bold">Cart Total</p>
              <p id="productDetailAddToCartModalCartTotal"
                className="h3 cef-confirmation-add-cart-price"
                ng-bind="addToCartModalCtrl.cvCartService.carts[addToCartModalCtrl.type].Totals.Total | globalizedCurrency">
              </p>
            </div>
          */}
        {messages.length ? (
          <div className="col-12">
            {messages.map((msg: string, index) => {
              let alertClass;
              const firstWord = msg.split(" ")[0];
              switch (firstWord.toUpperCase()) {
                case "ERROR":
                  alertClass = "alert-danger";
                  break;
                case "WARNING":
                  alertClass = "alert-warning";
                  break;
                default:
                  alertClass = "alert-info";
              }
              return (
                <div
                  role="alert"
                  key={msg}
                  id={`AddToCartModalText_${index}`}
                  className={`alert ${alertClass} ${
                    index === messages.length - 1 ? "mb-0" : ""
                  }
                    `}>
                  {msg}
                </div>
              );
            })}
          </div>
        ) : null}
      </Row>
    </ConfirmationModal>
  );
};
