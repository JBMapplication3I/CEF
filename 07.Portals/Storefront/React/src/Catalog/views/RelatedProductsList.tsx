import { Fragment, useEffect, useState } from "react";
import { Carousel } from "../../_shared/common/Carousel";
import { ProductCard } from "./ProductCard";
import { CarouselItem } from "../../_shared/common/CarouselItem";
import { currencyFormatter } from "../../_shared/common/Formatters";
import ImageWithFallback from "../../_shared/common/ImageWithFallback";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { useTranslation } from "react-i18next";
import cvApi from "../../_api/cvApi";
import { usePricingFactory } from "../../_shared/customHooks/usePricingFactory";
import { useInventoryFactory } from "../../_shared/customHooks/useInventoryFactory";
import { CEFConfig } from "../../_redux/_reduxTypes";
import { useCefConfig } from "../../_shared/customHooks/useCefConfig";
import { Row, Col, Card } from "react-bootstrap";

interface IRelatedProductsListProps {
  products: Array<any>;
}

export const RelatedProductsList = (props: IRelatedProductsListProps) => {
  const { products } = props;

  const [fullProducts, setFullProducts] = useState([]);

  const { t } = useTranslation();
  const pricingFactory = usePricingFactory();
  const inventoryFactory = useInventoryFactory();
  const cefConfig: CEFConfig = useCefConfig();
  // TODO: use the viewState.running
  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    if (!cefConfig) {
      return;
    }
    let withPricing, withInventory;
    if (cefConfig.featureSet.pricing.enabled) {
      withPricing = Boolean(pricingFactory);
    }
    if (cefConfig.featureSet.inventory.enabled) {
      withInventory = Boolean(inventoryFactory);
    }
    getProductsByProductIDs(withPricing, withInventory);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [cefConfig, pricingFactory, inventoryFactory]);

  if (!products || !products.length) {
    return <div className="d-none"></div>;
  }

  // Similar function in Catalog.tsx, consider refactoring
  async function getProductsByProductIDs(
    withPricing: boolean,
    withInventory: boolean
  ) {
    const debugMsg =
      "Catalog/views/RelatedProductsList getProductsByProductIDs";
    setRunning();
    const productIDs = products.map((product: any) => {
      return product.SlaveID;
    });
    if (!productIDs.length) {
      return finishRunning(true, debugMsg + " no productIDs");
    }
    try {
      let newProducts = (
        await cvApi.products.GetProductsByIDs({ IDs: productIDs })
      ).data;
      if (withPricing) {
        // Assign prices
        newProducts = await pricingFactory.bulkFactoryAssign(newProducts);
      }
      if (withInventory) {
        // Assign inventory
        newProducts = await inventoryFactory.bulkFactoryAssign(newProducts);
      }
      setFullProducts(newProducts);
    } catch (err) {
      if (cefConfig.debug) {
        console.debug(debugMsg, err);
      }
      finishRunning(true, debugMsg + ": Failed to get products by product ids");
    }
  }

  return (
    <Fragment>
      <Row className="d-none d-lg-block">
        <Carousel>
          {Array(Math.ceil(fullProducts.length / 4))
            .fill(null)
            .map((_n: null, index: number): JSX.Element => {
              return (
                <CarouselItem key={index}>
                  <Row>
                    {fullProducts
                      .slice(index * 4, index * 4 + 4)
                      .map((product: any) => {
                        return (
                          <ProductCard
                            product={product}
                            customClasses="col-6 col-xl-3"
                            key={product.ID}
                          />
                        );
                      })}
                  </Row>
                </CarouselItem>
              );
            })}
        </Carousel>
      </Row>
      <Row className="d-block d-lg-none">
        <Col sm={12}>
          <h2 className="mt-4">
            {t("ui.storefront.product.widgets.relatedProducts.relatedProducts")}
          </h2>
          <Carousel>
            {Array(Math.ceil(fullProducts.length / 4))
              .fill(null)
              .map((_n: null, index: number): JSX.Element => {
                return (
                  <CarouselItem key={index}>
                    <Row>
                      <Col sm={12}>
                        {fullProducts
                          .slice(index * 4, index * 4 + 4)
                          .map((product: any) => {
                            const {
                              ID,
                              SeoUrl,
                              Name,
                              CustomKey,
                              SalePrice,
                              BasePrice
                            } = product;
                            return (
                              <Card key={ID}>
                                <Card.Body className="p-3">
                                  <div className="d-flex">
                                    <div className="img-holder me-3">
                                      {/* //TODO: replaced direct import of placeholder image with fallback w/ no src, needs to be real image */}
                                      <ImageWithFallback
                                        src={""}
                                        alt={"NULLIMG"}
                                        width="100"
                                        height="100"
                                      />
                                    </div>
                                    <div>
                                      <a
                                        href={`/product?seoUrl=${SeoUrl}`}
                                        className="card-title">
                                        <span className="rows-2">{Name}</span>
                                      </a>
                                      <Card.Text className="my-1">
                                        <div className="product-sku rows-1">
                                          <span className="code">
                                            {t("ui.storefront.cart.SKU")}:{" "}
                                            {CustomKey}
                                          </span>
                                        </div>
                                      </Card.Text>
                                      <div className="my-1">
                                        {SalePrice !== BasePrice ? (
                                          <span>
                                            <del className="text-danger small">
                                              {BasePrice != null
                                                ? currencyFormatter.format(
                                                    BasePrice
                                                  )
                                                : "Price not found"}
                                            </del>
                                            &nbsp;
                                            <span className="text-success very-big">
                                              {SalePrice != null
                                                ? currencyFormatter.format(
                                                    SalePrice
                                                  )
                                                : "Price not found"}
                                            </span>
                                          </span>
                                        ) : (
                                          <span className="text-body very-big">
                                            {SalePrice != null
                                              ? currencyFormatter.format(
                                                  SalePrice
                                                )
                                              : "Price not found"}
                                          </span>
                                        )}
                                      </div>
                                    </div>
                                  </div>
                                </Card.Body>
                              </Card>
                            );
                          })}
                      </Col>
                    </Row>
                  </CarouselItem>
                );
              })}
          </Carousel>
        </Col>
      </Row>
    </Fragment>
  );
};
