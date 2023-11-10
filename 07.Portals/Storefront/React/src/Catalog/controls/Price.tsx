/**
 * @file React/src/Catalog/controls/Price.tsx
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product detail price widget
 */
import { CEFConfig, IReduxStore } from "../../_redux/_reduxTypes";
import { connect } from "react-redux";
import { currencyFormatter } from "../../_shared/common/Formatters";
import { CalculatedPrices } from "../../_api/cvApi.shared";
import { useTranslation } from "react-i18next";
import { Row, Col } from "react-bootstrap";
const Price = (props: IProductPriceProps): JSX.Element => {
  const { prices } = props;
  const { t } = useTranslation();
  const debugMsg = "Catalog/controls/Price render";
  return (
    <Row id="pnlProductPrice">
      {/* Non-Kit setups  */}
      <Col className="h1 mt-0 mb-3">
        {/*
        <span ng-if="pdpCtrl.prices.isSale"
          className="price text-success">
          <span data-translate="ui.storefront.product.detail.productDetails.onSale"></span>
          <br />
        </span>
        */}
        {prices.haveBase && !prices.haveSale && (
          <span className="price text-success">
            {t("ui.storefront.common.Free")}
          </span>
        )}
        {prices.haveBase && prices.isSale ? (
          <del
            className={"price very-small text-muted"}
            data-sid="ProductDetailsPagePrice">
            {currencyFormatter.format(prices.base)}
          </del>
        ) : (
          <span className="price" data-sid="ProductDetailsPagePrice">
            {currencyFormatter.format(prices.base)}
          </span>
        )}
        {prices.isSale && (
          <span className="price text-success">
            {" "}
            <span data-sid="ProductDetailsPageSalePrice">
              {currencyFormatter.format(prices.sale)}
            </span>
          </span>
        )}
        {prices.isSale && (
          <span className="price-savings text-muted very-small">
            {" "}
            {currencyFormatter.format(prices.amountOff) +
              " (" +
              Math.trunc(prices.percentOff) +
              "%) " +
              t("ui.storefront.product.detail.productDetails.Off")}
          </span>
        )}
      </Col>
    </Row>
  );
};

interface IProductPriceProps {
  prices: CalculatedPrices;
  cefConfig: CEFConfig; // From Redux
}

const mapStateToProps = (state: IReduxStore) => {
  return {
    cefConfig: state.cefConfig
  };
};

export default connect(mapStateToProps)(Price);
