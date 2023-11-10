/**
 * @file React/src/Catalog/controls/Stock.tsx
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product detail stock widget
 */
import { CEFConfig, IReduxStore } from "../../_redux/_reduxTypes";
import { connect } from "react-redux";
import { CalculatedInventories } from "../../_api/cvApi.shared";
import { useTranslation } from "react-i18next";
import { CartModel } from "../../_api/cvApi._DtoClasses";
import { Alert, Button, Col, Row } from "react-bootstrap";

const Stock = (props: IProductStockProps): JSX.Element => {
  const { inventory, productID, notifyMe, cefConfig } = props;
  const { t } = useTranslation();
  const debugMsg = "Catalog/controls/Stock render";

  const showNotifyMe = (): boolean => {
    if (!inventory || !notifyMe) {
      return false;
    }
    return (
      !inventory.IsUnlimitedStock &&
      inventory.QuantityOnHand <= 0 &&
      !notifyMe.SalesItems.some((nmsi) => nmsi.ProductID === productID)
    );
  };

  const showDontNotifyMe = (): boolean => {
    if (!inventory || !notifyMe) {
      return true;
    }
    return (
      !inventory.IsUnlimitedStock &&
      inventory.QuantityOnHand <= 0 &&
      notifyMe.SalesItems.some((nmsi) => nmsi.ProductID === productID)
    );
  };

  const click = (add: boolean): void => {
    // TODO: require login for add to notifyMe
  };

  return (
    <Row className="cef-product-stock" id="pnlProductDetailsStock">
      {inventory.IsUnlimitedStock && (
        <Col xs="auto">
          <Alert variant="success" className="py-1 px-2 mb-3">
            {t("ui.storefront.common.Unlimited")}
          </Alert>
        </Col>
      )}
      {!inventory.IsUnlimitedStock && inventory.QuantityOnHand > 0 && (
        <Col xs="auto">
          <Alert variant="success" className="py-1 px-2 mb-3">
            <span>{inventory.QuantityOnHand}</span>
            &nbsp;<span>{t("ui.storefront.common.InStock")}</span>
          </Alert>
        </Col>
      )}
      {!inventory.IsUnlimitedStock &&
        inventory.AllowBackOrder &&
        inventory.QuantityOnHand <= 0 && (
          <Col xs="auto">
            <Alert variant="warning" className="py-1 px-2 mb-3">
              {t("ui.storefront.common.OnBackorder")}
            </Alert>
          </Col>
        )}
      {!inventory.IsUnlimitedStock &&
        !inventory.AllowBackOrder &&
        inventory.QuantityOnHand <= 0 && (
          <Col xs="auto">
            <Alert variant="danger" className="py-1 px-2 mb-3">
              {t("ui.storefront.common.OutOfStock")}
            </Alert>
          </Col>
        )}
      {cefConfig.featureSet.carts.notifyMeWhenInStock.enabled &&
        !inventory.IsUnlimitedStock && (
          <Col xs="auto">
            {showNotifyMe() && (
              <Button
                variant="link"
                className="d-inline-block"
                id="btnProductDetailNotifyMe"
                name="btnProductDetailNotifyMe"
                title={t("ui.storefront.common.NotifyMeWhenInStock")}
                aria-label={t("ui.storefront.common.NotifyMeWhenInStock")}
                onClick={() => click(true)}>
                <i className="far fa-bell"></i>
                <span className="sr-only">
                  {t("ui.storefront.common.NotifyMeWhenInStock")}
                </span>
              </Button>
            )}
            {showDontNotifyMe() && (
              <Button
                variant="link"
                className="d-inline-block"
                id="btnProductDetailDontNotifyMe"
                name="btnProductDetailDontNotifyMe"
                title={t("ui.storefront.common.DontNotifyMeWhenInStock")}
                aria-label={t("ui.storefront.common.DontNotifyMeWhenInStock")}
                onClick={() => click(false)}>
                <i className="fas fa-bell"></i>
                <span className="sr-only">
                  {t("ui.storefront.common.DontNotifyMeWhenInStock")}
                </span>
              </Button>
            )}
          </Col>
        )}
    </Row>
  );
};

interface IProductStockProps {
  inventory: CalculatedInventories;
  productID: number;
  notifyMe: CartModel; // From Redux
  cefConfig: CEFConfig; // From Redux
}

const mapStateToProps = (state: IReduxStore) => {
  return {
    cefConfig: state.cefConfig,
    notifyMe: state.staticCarts.notifyMeList
  };
};

export default connect(mapStateToProps)(Stock);
