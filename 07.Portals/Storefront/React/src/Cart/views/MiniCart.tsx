import { faPencilAlt } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import { CEFConfig, IReduxStore } from "../../_redux/_reduxTypes";
import { currencyFormatter } from "../../_shared/common/Formatters";
import { TotalsWidget } from "./TotalsWidget";
import { SalesItemBaseModel } from "../../_api/cvApi.shared";
import { AppliedCartItemDiscountModel, CartModel } from "../../_api/cvApi._DtoClasses";
import ImageWithFallback from "../../_shared/common/ImageWithFallback";
import { corsLink } from "../../_shared/common/Cors";
import { Card, Row, Col, ListGroup, Button } from "react-bootstrap";

interface IMiniCartProps {
  cefConfig?: CEFConfig; // redux
  cart?: CartModel; // redux
}

interface IMapStateToMiniCartProps {
  cefConfig: CEFConfig;
  cart: CartModel;
}

const mapStateToProps = (state: IReduxStore): IMapStateToMiniCartProps => {
  return {
    cefConfig: state.cefConfig,
    cart: state.cart
  };
};

export const MiniCart = connect(mapStateToProps)((props: IMiniCartProps): JSX.Element => {
  const { cart, cefConfig } = props;
  const { t } = useTranslation();

  if (!cart || !cart.SalesItems || !cart.SalesItems.length) {
    return (
      <Card className="mb-3">
        <Card.Header className="p-4">
          <h4>{t("ui.storefront.checkout.cartEmpty")}</h4>
        </Card.Header>
      </Card>
    );
  }

  return (
    <Card className="mb-3">
      <Card.Header className="p-4">
        <Row>
          <Col>
            <h4 className="my-0">{t("ui.storefront.common.OrderSummary")}</h4>
          </Col>
          <Col xs="auto">
            <Button
              as="a"
              variant="link"
              href="/cart"
              className="px-1 py-0"
              id="btnEditCart">
              <FontAwesomeIcon icon={faPencilAlt} aria-hidden={true} />
              &nbsp;
              <span>{t("ui.storefront.checkout.checkoutPanels.editCart")}</span>
            </Button>
          </Col>
        </Row>
      </Card.Header>
      <Card.Body className="py-1 bg-light text-dark font-weight-bold">
        <span className="card-subtitle">
          {t("ui.storefront.product.catalog.results.quickView.Items")}
        </span>
      </Card.Body>
      <ListGroup>
        {cart.SalesItems.map(
          (salesItem: SalesItemBaseModel<AppliedCartItemDiscountModel>, index: number) => {
            const {
              ID,
              ProductPrimaryImage,
              ProductName,
              UnitSoldPrice,
              UnitCorePrice,
              Quantity,
              QuantityBackOrdered,
              QuantityPreSold
            } = salesItem;
            const price = UnitSoldPrice ?? UnitCorePrice;
            const totalQuantity = Quantity + (QuantityBackOrdered || 0) + (QuantityPreSold || 0);
            return (
              <ListGroup.Item key={ID} className="py-0">
                <Row>
                  <Col
                    xs={3}
                    className="d-none d-xl-flex xl-pl-2 xl-py-2 tk-py-2 fk-py-2">
                    <div
                      id={`miniCartProductImageThumbnail${index}`}
                      className="card-img">
                      <a
                        href={corsLink(cefConfig, null, "site", "primary", false, {
                          seoUrl: salesItem.ProductSeoUrl
                        })}
                        id={`cartProductImageThumbnail${index}`}
                        className="product-image d-flex justify-content-center">
                        <ImageWithFallback
                          className="img-fluid"
                          alt={ProductName}
                          src={ProductPrimaryImage}
                        />
                      </a>
                    </div>
                  </Col>
                  <Col className="py-2 pl-3 xl-pl-2 tk-pl-2 fk-pl-3 pr-3">
                    <span
                      className="cart-item-label"
                      id={`miniCartProductName_${index}`}>
                      {ProductName}
                    </span>
                    <Row>
                      <Col sm="auto">
                        <div className="text-left font-weight-bold">
                          {t("ui.storefront.common.Price")}
                        </div>
                        <div className="text-left" id={`miniCartProductPrice_${index}`}>
                          {currencyFormatter.format(price)}
                        </div>
                      </Col>
                      <Col sm="auto">
                        <div
                          className="text-left font-weight-bold"
                          id={`MiniCartProductQuantityText_${index}`}>
                          {t("ui.storefront.common.Quantity")}
                        </div>
                        <div className="text-center">{totalQuantity}</div>
                      </Col>
                      <Col sm="auto">
                        <div className="text-right font-weight-bold">
                          {t("ui.storefront.checkout.views.checkoutLabelItemTotal.avd")}
                        </div>
                        <div className="text-right" id={`MiniCartItemTotal_${index}`}>
                          {currencyFormatter.format(totalQuantity * price)}
                        </div>
                      </Col>
                    </Row>
                  </Col>
                </Row>
              </ListGroup.Item>
            );
          }
        )}
      </ListGroup>
      <Card.Body className=" py-1 bg-light text-dark">
        <span
          className="card-subtitle font-weight-bold"
          id="MiniCartTotalsText">
          {t("ui.storefront.cart.widgets.totalsWidget.Totals")}
        </span>
      </Card.Body>
      <Card.Body className="p-0">
        <TotalsWidget Totals={cart.Totals} />
      </Card.Body>
    </Card>
  );
});
