﻿import { useCefConfig } from "../_shared/customHooks/useCefConfig";
import { useInventoryFactory } from "../_shared/customHooks/useInventoryFactory";
import { useLocation } from "react-router-dom";
import { usePricingFactory } from "../_shared/customHooks/usePricingFactory";
import { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useViewState } from "../_shared/customHooks/useViewState";
import { CEFConfig } from "../_redux/_reduxTypes";
import { CalculatedInventories, CalculatedPrices, ProductModel } from "../_api/cvApi.shared";
import cvApi from "../_api/cvApi";
import { corsLink } from "../_shared/common/Cors";
import { AddToCartButton, AddToCartQuantitySelector } from "../Cart/controls";
import { LoadingWidget } from "../_shared/common/LoadingWidget";
import Price from "./controls/Price";
import Stock from "./controls/Stock";
import { ProductCardControlsWidget } from "./controls/ProductCardControlsWidget/ProductCardControlsWidget";
import { RelatedProductsList } from "./views/RelatedProductsList";
import ImageWithFallback from "../_shared/common/ImageWithFallback";
import { Row, Col, Container, ListGroup, Breadcrumb } from "react-bootstrap";
import { ProductDetailsTabs } from "./views/ProductDetailsTabs/ProductDetailsTabs";
import { VariantSwatches } from "./controls/VariantSwatches";
import { ProductAssociationModel } from "../_api/cvApi._DtoClasses";

export const ProductDetails = (): JSX.Element => {
  const [masterProduct, setMasterProduct] = useState<ProductModel>(null);
  const [productVariant, setProductVariant] = useState<ProductModel>(null);
  const [listOfVariants, setListOfVariants] = useState<ProductAssociationModel[]>(null);

  const [quantity, setQuantity] = useState<number>(1);

  const { setRunning, finishRunning, viewState } = useViewState();
  const cefConfig = useCefConfig() as CEFConfig;
  const inventoryFactory = useInventoryFactory();
  const pricingFactory = usePricingFactory();

  const { t } = useTranslation();
  const location = useLocation();

  const params = new URLSearchParams(location.search);
  const seoUrl = params.get("seoUrl");

  useEffect(() => {
    if (
      !seoUrl ||
      !cefConfig ||
      (cefConfig.featureSet.pricing.enabled && !pricingFactory) ||
      (cefConfig.featureSet.inventory.enabled && !inventoryFactory)
    ) {
      return;
    }
    initializeProduct();
  }, [seoUrl, cefConfig, inventoryFactory, pricingFactory]);

  /* Retrieve variant as ProductModel */
  const updateProductVariant = async (variantSeoUrl: string) => {
    const productVariant = await getProduct(variantSeoUrl, true);
    finishRunning();
    setProductVariant(productVariant);
  };

  const initializeProduct = async () => {
    setRunning();
    const product = await getProduct(seoUrl);
    finishRunning();
    setMasterProduct(product);
  };

  useEffect(() => {
    if (masterProduct && masterProduct.ProductAssociations) {
      var newListOfVariants: ProductAssociationModel[] = masterProduct.ProductAssociations
        ? masterProduct.ProductAssociations.filter(
            (childProduct) => childProduct.TypeKey === "VARIANT-OF-MASTER"
          ).sort((a, b) => Number(a.SlaveKey) - Number(b.SlaveKey))
        : null;
    }
    setListOfVariants(newListOfVariants);
  }, [masterProduct]);

  async function getProduct(seoUrl: string, force: boolean = false) {
    try {
      const productID =
        !force && masterProduct
          ? masterProduct.ID
          : (await cvApi.products.CheckProductExistsBySeoUrl({ SeoUrl: seoUrl })).data;
      let productData =
        !force && masterProduct
          ? masterProduct
          : ((await cvApi.products.GetProductByID(productID)).data as ProductModel);
      if (cefConfig.featureSet.inventory.enabled && inventoryFactory) {
        // Assign inventory
        productData = await inventoryFactory.factoryAssign(productData);
      }
      // Assign prices
      if (cefConfig.featureSet.pricing.enabled && pricingFactory) {
        productData = await pricingFactory.factoryAssign(productData);
      }
      return productData;
    } catch (err) {
      console.log(err);
      finishRunning(true, "Failed to get product");
    }
  }

  const getProductView = (): JSX.Element => {
    if (
      viewState.running ||
      !masterProduct ||
      !cefConfig ||
      (cefConfig.featureSet.pricing.enabled && !masterProduct.readPrices) ||
      (cefConfig.featureSet.inventory.enabled && !masterProduct.readInventory)
    ) {
      return (
        <section>
          <div className="container">
            <LoadingWidget />
          </div>
        </section>
      );
    }

    let prices: CalculatedPrices, inventory: CalculatedInventories;
    if (cefConfig.featureSet.pricing.enabled) {
      prices = masterProduct.readPrices();
    }

    if (cefConfig.featureSet.pricing.enabled && cefConfig.featureSet.inventory.enabled) {
      inventory = masterProduct.readInventory();
    }

    let shownProduct = productVariant ? productVariant : masterProduct;
    if (!shownProduct) {
      return null;
    }

    return (
      <section>
        {/* For DNN:cef:ProductMetaService */}
        <input id="productID" type="hidden" value={shownProduct.ID} />
        {prices && prices.base && <input id="productBasePrice" type="hidden" value={prices.base} />}
        <Container className="px-0">
          <div className="product-one">
            <Row>
              <Col xs={12} md={4}>
                <div className="image-preview text-center">
                  <ImageWithFallback
                    className="img-fluid"
                    src={shownProduct.PrimaryImageFileName}
                    alt={shownProduct.Name}
                  />
                </div>
              </Col>
              <Col xs={12} md={8}>
                <div className="head-title">
                  <Row>
                    <h1 className="title">{shownProduct.Name}</h1>
                  </Row>
                  <hr />
                  <Row>
                    <Col xs={12} md={8}>
                      <div className="product-info">
                        {cefConfig.featureSet.pricing.enabled && <Price prices={prices} />}
                        {cefConfig.featureSet.inventory.enabled && (
                          <Stock inventory={inventory} productID={shownProduct.ID} />
                        )}
                        <ListGroup as="ul" className="list-unstyled">
                          <ListGroup.Item>
                            <span className="dt">
                              {t("ui.storefront.product.detail.productDetails.SKU")}:{" "}
                              {shownProduct?.CustomKey}
                            </span>
                          </ListGroup.Item>
                          <ListGroup.Item>
                            <span className="dt">
                              {t("ui.storefront.common.Unit")}: {shownProduct?.UnitOfMeasure}
                            </span>
                          </ListGroup.Item>
                        </ListGroup>
                      </div>
                      <hr />
                      <div>
                        <h5>{t("ui.storefront.product.detail.productDetails.quickDescription")}</h5>
                        <p>{shownProduct?.ShortDescription}</p>
                      </div>
                    </Col>
                    <Col xs={12} sm={4} className="text-right">
                      <div className="d-block mb-1">
                        {listOfVariants && listOfVariants.length > 0 && (
                          <VariantSwatches
                            listOfVariants={listOfVariants}
                            onUpdateProductVariant={updateProductVariant}
                          />
                        )}
                      </div>
                      <hr /> {/* temporary */}
                      <div className="d-block mb-1">
                        <AddToCartQuantitySelector id={shownProduct.ID} onChange={setQuantity} />
                      </div>
                      <div className="d-block mb-1">
                        <AddToCartButton
                          classes="btn btn-block btn-lg btn-primary w-100"
                          quantity={quantity}
                          product={shownProduct}
                        />
                      </div>
                      <ProductCardControlsWidget
                        product={shownProduct}
                        inventory={inventory}
                        render="vertical"
                      />
                    </Col>
                  </Row>
                </div>
              </Col>
            </Row>
          </div>
          {shownProduct && (
            <Row>
              <Col xs={12}>
                <ProductDetailsTabs product={shownProduct} />
              </Col>
            </Row>
          )}
          <RelatedProductsList
            products={masterProduct?.ProductAssociations ? masterProduct.ProductAssociations : []}
          />
        </Container>
      </section>
    );
  };

  return (
    <div>
      <Container>
        <Row>
          <Col xs={12}>
            <Breadcrumb className="bg-light mb-2" listProps={{ className: "p-2 mb-0" }}>
              <Breadcrumb.Item className="breadcrumbs-title" href="/">
                {t("ui.storefront.common.Home")}
              </Breadcrumb.Item>
              <Breadcrumb.Item href={corsLink(cefConfig, cefConfig?.routes?.catalog?.root)}>
                {t("ui.storefront.product.detail.productDetails.Catalog")}
              </Breadcrumb.Item>
              <Breadcrumb.Item>
                <span>{masterProduct ? masterProduct.Name : null}</span>
              </Breadcrumb.Item>
            </Breadcrumb>
          </Col>
        </Row>
      </Container>
      {getProductView()}
    </div>
  );
};
