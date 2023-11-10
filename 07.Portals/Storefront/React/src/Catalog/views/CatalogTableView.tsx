import React, { UIEvent, useState } from "react";
import { useTranslation } from "react-i18next";
import {
  AddToCartButton,
  AddToCartQuantitySelector
} from "../../Cart/controls";
import { ProductModel } from "../../_api/cvApi.shared";
import { currencyFormatter } from "../../_shared/common/Formatters";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { ProductCardControlsWidget } from "../controls/ProductCardControlsWidget/ProductCardControlsWidget";
import { Col, Row, Table } from "react-bootstrap";

let timeoutHolder: any;

interface ICatalogTableViewProps {
  products: Array<any>;
  onChangeProductQuantity: Function;
  onScrollToBottom?: Function;
  useInfinite?: boolean;
  parentRunning?: boolean;
}

export const CatalogTableView = (
  props: ICatalogTableViewProps
): JSX.Element => {
  const {
    products,
    onChangeProductQuantity,
    onScrollToBottom,
    useInfinite,
    parentRunning
  } = props;

  const [canCallScrollFn, setCanCallScrollFn] = useState(true);
  const { t } = useTranslation();

  let style: React.CSSProperties = {};
  if (useInfinite) {
    style.maxHeight = "30vh";
    style.overflowY = "scroll";
    style.overflowX = "hidden";
  }

  const showOutOfStock = (product: ProductModel): boolean => {
    return (
      product.readInventory &&
      product.readInventory().IsOutOfStock &&
      !product.readInventory().AllowBackOrder
    );
  };

  return (
    <div
      className="table-body"
      style={style}
      onScroll={(event: UIEvent<HTMLDivElement>) => {
        const { currentTarget } = event;
        const { scrollHeight, scrollTop, clientHeight } = currentTarget;
        const bottom = scrollHeight - Math.ceil(scrollTop) - 50 < clientHeight;
        if (useInfinite && bottom && onScrollToBottom && canCallScrollFn) {
          onScrollToBottom();
          setCanCallScrollFn(false);
          timeoutHolder = setTimeout(() => {
            setCanCallScrollFn(true);
            clearTimeout(timeoutHolder);
          }, 2000);
        }
      }}>
      <Row id="results-table">
        <Col xs="12">
          <div className="table-responsive">
            <Table striped hover className="table-condensed mb-0 table-sm">
              <thead>
                <tr>
                  <th className="w-16">{t("ui.storefront.common.Name")}</th>
                  <th className="w-16">{t("ui.storefront.cart.SKU")}</th>
                  <th className="w-16">{t("ui.storefront.cart.Stock")}</th>
                  <th className="w-8">{t("ui.storefront.common.Price")}</th>
                  <th className="w-auto">
                    {t("ui.storefront.product.catalog.results.grid.addToCart")}
                  </th>
                </tr>
              </thead>
              <tbody>
                {products.map((product: any, index: number): JSX.Element => {
                  const {
                    Name,
                    ID,
                    Quantity,
                    CustomKey,
                    readPrices,
                    readInventory,
                    SeoUrl
                  } = product;
                  let salePrice: number;
                  let basePrice: number;
                  if (readPrices) {
                    const { base, sale } = readPrices();
                    basePrice = base;
                    salePrice = sale;
                  }
                  return (
                    <tr key={ID}>
                      <td className="w-16">
                        <a
                          href={`/product?seoUrl=${SeoUrl}`}
                          className="card-title text-body"
                          id={`productCatalogDetailsLinkFor_${index}`}>
                          <span
                            className="rows-2 fw-bolder fs-6"
                            id={`cardProductName_${index}`}>
                            {Name}
                          </span>
                        </a>
                      </td>
                      <td className="w-16">
                        <div className="product-sku rows-1">
                          <span className="fw-bold small">{CustomKey}</span>
                        </div>
                      </td>
                      <td className="w-auto">
                        <div
                          className="product-stock rows-1"
                          style={{ overflowWrap: "break-word" }}>
                          {showOutOfStock(product) ? (
                            <span className="alert alert-danger p-1">
                              <small>
                                {t("ui.storefront.common.OutOfStock")}
                              </small>
                            </span>
                          ) : (
                            <span className="alert alert-success p-1">
                              {readInventory?.IsUnlimitedStock ? (
                                <small>
                                  {t("ui.storefront.common.Unlimited")}
                                </small>
                              ) : (
                                <>
                                  <small>
                                    {readInventory?.QuantityOnHand || 0}{" "}
                                  </small>
                                  <small>
                                    {t("ui.storefront.common.InStock")}
                                  </small>
                                </>
                              )}
                            </span>
                          )}
                        </div>
                      </td>
                      <td className="w-8">
                        <Row className="product-pricing">
                          <Col
                            xs="12"
                            className="bold very-big text-center order-1">
                            {salePrice
                              ? currencyFormatter.format(salePrice)
                              : "$--.--"}
                          </Col>
                        </Row>
                      </td>
                      <td>
                        <div className="d-flex w-auto">
                          <div
                            className="d-inline-block"
                            style={{ width: "calc(100% - 46px)" }}>
                            <AddToCartQuantitySelector
                              id={ID}
                              onChange={(val: number): void =>
                                onChangeProductQuantity(ID, val)
                              }
                            />
                          </div>
                          <AddToCartButton
                            product={product}
                            quantity={Quantity}
                            classes="btn btn-success text-white"
                            excludeQuoteCart={true}
                            hideOutOfStock={true}
                            icon={true}
                          />
                          <ProductCardControlsWidget
                            product={product}
                            render="horizontal"
                          />
                        </div>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </Table>
          </div>
        </Col>
      </Row>
      {parentRunning ? <LoadingWidget /> : null}
    </div>
  );
};
