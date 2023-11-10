import { useState, UIEvent } from "react";
import { useTranslation } from "react-i18next";
import { ProductModel } from "../../_api/cvApi._DtoClasses";
import { currencyFormatter } from "../../_shared/common/Formatters";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { ProductCardActionButtonWidget } from "../controls/ProductCardActionButtonWidget";
import { ProductCardControlsWidget } from "../controls/ProductCardControlsWidget/ProductCardControlsWidget";
import { Row, Col, Card, Table } from "react-bootstrap";

let timeoutHolder: any;

interface ICatalogListViewProps {
  products: ProductModel[];
  onScrollToBottom?: Function;
  useInfinite?: boolean;
  parentRunning?: boolean;
}

export const CatalogListView = (props: ICatalogListViewProps) => {
  const { products, onScrollToBottom, useInfinite, parentRunning } = props;
  const [canCallScrollFn, setCanCallScrollFn] = useState(true);
  const { t } = useTranslation();
  let style: React.CSSProperties = {};
  if (useInfinite) {
    style.maxHeight = "40vh";
    style.overflowY = "scroll";
    style.overflowX = "hidden";
  }

  return (
    <div
      className="list-body"
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
      <Row id="results-list">
        {products.map((product: any, index: number) => {
          const {
            Name,
            ID,
            CustomKey,
            ShortDescription,
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
          let quantityAvailable = 0;

          if (readInventory) {
            quantityAvailable = readInventory().QuantityOnHand;
          }

          return (
            <div className="catalog-result-card col-12 mb-3 product" key={ID}>
              <Card>
                <Row className="no-gutters">
                  <Col sm="3">
                    <a
                      href={`/product?seoUrl=${SeoUrl}`}
                      className="d-flex justify-content-center product-image">
                      <img className="img-fluid d-block" alt={Name} />
                    </a>
                  </Col>
                  <Col sm="9">
                    <Card.Body>
                      <Card.Title>
                        <a
                          href={`/product?seoUrl=${SeoUrl}`}
                          className="card-title"
                          id={`productCatalogDetailsLinkFor_${index}`}>
                          <span
                            className="rows-2"
                            id={`cardProductName_${index}`}>
                            {Name}
                          </span>
                        </a>
                      </Card.Title>
                      <Card.Text className="my-1">
                        <div className="product-sku rows-1">
                          <span className="code">
                            {t("ui.storefront.cart.SKU")}: {CustomKey}
                          </span>
                        </div>
                      </Card.Text>
                      <Card.Text className="my-1">
                        <span
                          className="rows-2 small"
                          id="cardProductShortDesc_">
                          {ShortDescription}
                        </span>
                      </Card.Text>
                      <Card.Text className="my-1">
                        <Row className="product-pricing">
                          <Col
                            xs="12"
                            className="bold order-1 text-center very-big">
                            {salePrice
                              ? currencyFormatter.format(salePrice)
                              : "$--.--"}
                          </Col>
                        </Row>
                      </Card.Text>
                      <Card.Text className="my-1">
                        <ProductCardActionButtonWidget
                          product={product}
                          quantityAvailable={quantityAvailable}
                        />
                      </Card.Text>
                      <Card.Text className="my-1">
                        <ProductCardControlsWidget
                          product={product}
                          render="horizontal"
                          hideShoppingList={true}
                        />
                      </Card.Text>
                    </Card.Body>
                  </Col>
                </Row>
              </Card>
            </div>
          );
        })}
      </Row>
      {parentRunning ? <LoadingWidget /> : null}
    </div>
  );
};
