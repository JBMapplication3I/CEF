import React, { Fragment, useEffect, useState } from "react";
import { faChevronLeft, faChevronRight, faTrash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Row, Col, Button, Table } from "react-bootstrap";
import { Link } from "react-router-dom";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { camelCaseToHumanReadable, currencyFormatter } from "../../_shared/common/Formatters";
import { ProductCardActionButtonWidget } from "../controls/ProductCardActionButtonWidget";
import Interweave from "interweave";
import classes from "./Compare.module.scss";
import { connect } from "react-redux";
import { useTranslation } from "react-i18next";
import cvApi from "../../_api/cvApi";
import { IReduxStore } from "../../_redux/_reduxTypes";

const mapStateToProps = (state: IReduxStore) => {
  return {
    compareCartItems: state.staticCarts.compareList
  };
};

export const Compare = connect(mapStateToProps)((props: any) => {
  const { compareCartItems, removeItemFromCompareCart } = props;

  const [compareProducts, setCompareProducts] = useState([]);
  const [pages, setPages] = useState(null);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    if (compareCartItems && compareCartItems.length) {
      getCompareCartItemProductsByIDs();
    }
  }, [compareCartItems]);

  async function getCompareCartItemProductsByIDs() {
    setRunning();
    try {
      const products = (
        await cvApi.products.GetProductsByIDs({
          IDs: compareCartItems.map((item: { ProductID: number | string }) => item.ProductID)
        })
      ).data;
      const priceResult = (
        await cvApi.pricing.CalculatePricesForProducts({
          ProductIDs: products.map((p) => p.ID)
        })
      ).data;
      const pricesObject = priceResult.Result;
      for (let i = 0; i < products.length; i++) {
        const inventoryCallData = (await cvApi.providers.CalculateInventory(products[i].ID)).data;
        const inventory = inventoryCallData.Result;
        products[i] = {
          ...products[i],
          ...inventory,
          ...pricesObject[products[i].ID]
        };
      }
      setCompareProducts(products);
      finishRunning();
    } catch (err) {
      console.log(err);
      finishRunning(true, "Failed to get compare cart item products");
    }
  }

  function getRowTitles() {
    const rows: Array<any> = [];
    const skipKeys = [
      // Explicitly called out so don't iterate over
      "CustomKey",
      "Name",
      "Description",
      "ShortDescription",
      "Width",
      "WidthUnitOfMeasure",
      "Depth",
      "DepthUnitOfMeasure",
      "Height",
      "HeightUnitOfMeasure",
      "Weight",
      "WeightUnitOfMeasure",
      // General Skips
      "ID",
      "Active",
      "CreatedDate",
      "UpdatedDate",
      "Hash",
      "JsonAttributes",
      "jsonAttributes",
      "Type",
      "TypeID",
      "TypeKey",
      "TypeName",
      "TypeDisplayName",
      "TypeSortOrder",
      "Status",
      "StatusID",
      "StatusKey",
      "StatusName",
      "StatusDisplayName",
      "StatusSortOrder",
      "Accounts",
      "Vendors",
      "Manufacturers",
      "ProductAssociations",
      "ProductInventoryLocationSections",
      "ProductPricePoints",
      "ProductsAssociatedWith",
      "ProductSubscriptionTypes",
      "ProductFiles",
      "ProductImages",
      "PrimaryImageFileName",
      "ImageFileName",
      "IsEligibleForReturn",
      "Stores",
      "Images",
      "StoredFiles",
      "SortOrder",
      "KitQuantityOfParent",
      "KitCapacity",
      "KitBaseQuantityPriceMultiplier",
      "IsFreeShipping",
      "IsShippingRestricted",
      "ProductCategories",
      "SeoUrl",
      "SeoDescription",
      "SeoKeywords",
      "SeoPageTitle",
      "Package",
      "PackageID",
      "PackageKey",
      "PackageName",
      "MasterPack",
      "MasterPackID",
      "MasterPackKey",
      "MasterPackName",
      "Pallet",
      "PalletID",
      "PalletKey",
      "PalletName",
      "ProductRestrictions",
      "IsVisible",
      "IsTaxable",
      "IsDiscontinued",
      "AllowBackOrder",
      "IsUnlimitedStock",
      "StockQuantity",
      "StockQuantityAllocated",
      "StockQuantityPreSold",
      "TotalPurchasedAmount",
      "TotalPurchasedQuantity",
      "DocumentRequiredForPurchaseOverrideFeeIsPercent",
      "MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent",
      "ProductNotifications",
      "AllowPreSale",
      "DropShipOnly",
      "NothingToShip",
      "ShippingLeadTimeIsCalendarDays",
      "RequiresRolesList",
      "RequiresRolesListAlt",
      // Properties and functions that extend in Angular
      "inventoryObject",
      "isOutOfStock",
      "countStock",
      "quantity",
      "CurrentShipOption",
      "readPrices",
      "$_rawPrices",
      "readInventory",
      "$_rawInventory"
    ];
    compareProducts.forEach((product: { SerializableAttributes: any }) => {
      const keys = Object.keys(product);
      keys.forEach((key) => {
        if (skipKeys.indexOf(key) > -1) {
          return;
        }
        if (rows.indexOf(key) > -1) {
          return;
        }
        if (key === "SerializableAttributes") {
          const attrKeys = Object.keys(product.SerializableAttributes);
          attrKeys.forEach((attrKey) => {
            if (!product.SerializableAttributes[attrKey].Value) {
              return;
            }
            if (attrKey.endsWith("_UOM")) {
              return;
            } // TAL-Specific
            if (product.SerializableAttributes[attrKey].Value === "000000000000") {
              return;
            } // TAL-Specific
            if (product.SerializableAttributes[attrKey].Value === "0000000") {
              return;
            } // TAL-Specific
            /* if (this.attributes.every((x) => x.CustomKey !== attrKey)) {
              return;
            } */ // Not a comparable attribute
            if (rows.indexOf(`SerializableAttributes.${attrKey}`) > -1) {
              return;
            }
            rows.push(`SerializableAttributes.${attrKey}`);
          });
          return;
        }
        rows.push(key);
      });
    });
    return rows;
  }

  if (
    !compareCartItems ||
    compareCartItems.length === 0 ||
    !compareProducts ||
    !compareProducts.length
  ) {
    return (
      <Row>
        <Col sm={12}>
          <p>
            {t("ui.storefront.product.widgets.compareViewer.thereAreNoItemsInTheProductCompare")}
          </p>
        </Col>
      </Row>
    );
  }

  return (
    <Fragment>
      <Row>
        <Col>
          <h2>{t("ui.storefront.searchCatalog.compareProduct.Plural")}</h2>
        </Col>
        <Col sm="auto">
          <Link className="btn btn-secondary" to="/catalog">
            {t("ui.storefront.searchCatalog.BackToResults")}
          </Link>
        </Col>
      </Row>
      <Row>
        <Col sm={12}>
          <Row className="no-gutters flex-nowrap">
            <Col sm="auto">
              <div className="paging-left">
                <Button
                  variant="outline-dark border-0"
                  id="btnCompareCartPageLeft">
                  <FontAwesomeIcon icon={faChevronLeft} className="fa-2x" />
                  <span className="sr-only">Page Left</span>
                </Button>
              </div>
            </Col>
            <Col>
              <Table striped hover size="sm" className={classes.compareTable}>
                <thead>
                  <tr>
                    <th
                      className="w-28 sm-w-28 md-w-20 lg-w-20 xl-w-20 tk-w-20 fk-w-20"
                      id="CompareSpecificationsText">
                      {t("ui.storefront.product.detail.productDetails.Specifications")}
                    </th>
                    {compareCartItems.map(
                      (
                        item: {
                          ID: number | string;
                          ProductSeoUrl: string;
                          ProductName: string;
                        },
                        index: number
                      ) => {
                        const { ID, ProductSeoUrl, ProductName } = item;
                        return (
                          <th
                            key={ID}
                            className="w-33 sm-w-33 md-w-25 lg-w-25 xl-w-25 tk-w-25 fk-w-25 compare-text compare-title">
                            <a
                              id={`btnCompareCartProductCard_${index}`}
                              href={`/product?seoUrl=${ProductSeoUrl}`}
                              rel="noopener noreferrer"
                              target="_blank">
                              {ProductName}
                            </a>
                          </th>
                        );
                      }
                    )}
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td id="CompareImageText">{t("ui.storefront.searchCatalog.compare.Image")}</td>
                    {compareCartItems.map(
                      (item: {
                        ID: number | string;
                        ProductSeoUrl: string;
                        ProductName: string;
                      }) => {
                        const { ID, ProductSeoUrl, ProductName } = item;
                        return (
                          <td key={ID}>
                            <div>
                              <a
                                href={`/product?seoUrl=${item.ProductSeoUrl}`}
                                className="d-flex justify-content-center product-image">
                                <img
                                  alt={"Image of" + ProductName}
                                  src={`./images/${ProductSeoUrl}`}
                                  width={250}
                                  height={250}
                                />
                              </a>
                            </div>
                          </td>
                        );
                      }
                    )}
                  </tr>
                  <tr>
                    <td id="CompareSummaryText">
                      {t("ui.storefront.userDashboard2.controls.salesDetail.Summary")}
                    </td>
                    {compareCartItems.map(
                      (item: {
                        ID: number | string;
                        ProductSeoUrl: string;
                        ProductName: string;
                        ProductShortDescription: string;
                      }) => {
                        return (
                          <td key={item.ID}>
                            <em className="compare-text">{item.ProductShortDescription}</em>
                          </td>
                        );
                      }
                    )}
                  </tr>
                  <tr>
                    <td id="CompareDescriptionText">{t("ui.storefront.common.Description")}</td>
                    {compareCartItems.map(
                      (item: { ID: number | string; ProductDescription: string }) => {
                        return (
                          <td key={item.ID}>
                            <em className="compare-text">
                              <Interweave content={item.ProductDescription} />
                            </em>
                          </td>
                        );
                      }
                    )}
                  </tr>
                  <tr>
                    <td id="CompareSkuText">{t("ui.storefront.cart.SKU")}</td>
                    {compareCartItems.map((item: { ID: number | string; CustomKey: string }) => {
                      return (
                        <td key={item.ID} className="compare-text">
                          {item.CustomKey}
                        </td>
                      );
                    })}
                  </tr>
                  <tr>
                    <td id="CompareDimensionsText">
                      {t("ui.storefront.searchCatalog.compare.Dimensions")}
                    </td>
                    {compareProducts.map((product: any) => {
                      const { ID } = product;
                      const widthExists = !isNaN(product.Width);
                      const depthExists = !isNaN(product.Depth);
                      const weightExists = !isNaN(product.Weight);
                      if (widthExists || depthExists || weightExists) {
                        return (
                          <td key={ID} className="compare-text">
                            <table className="table table-bordered mb-0">
                              <tbody>
                                {/* account for 0 */}
                                {widthExists ? (
                                  <tr>
                                    <th>
                                      {t("ui.storefront.storeDashboard.storeProductEditor.Width")}
                                    </th>
                                    <th>{`${product.Width} ${product.WidthUnitOfMeasure}`}</th>
                                  </tr>
                                ) : null}
                                {depthExists ? (
                                  <tr>
                                    <th>
                                      {t("ui.storefront.storeDashboard.storeProductEditor.Depth")}
                                    </th>
                                    <th>{`${product.Depth} ${product.DepthUnitOfMeasure}`}</th>
                                  </tr>
                                ) : null}
                                {weightExists ? (
                                  <tr>
                                    <th>
                                      {t("ui.storefront.storeDashboard.storeProductEditor.Weight")}
                                    </th>
                                    <th>{`${product.Weight} ${product.WeightUnitOfMeasure}`}</th>
                                  </tr>
                                ) : null}
                              </tbody>
                            </table>
                          </td>
                        );
                      } else {
                        return (
                          <td key={ID}>
                            <table className="table table-bordered mb-0">
                              <tbody>
                                <tr>
                                  <td>{t("ui.storefront.searchCatalog.filters.None")}</td>
                                </tr>
                              </tbody>
                            </table>
                          </td>
                        );
                      }
                    })}
                  </tr>
                  {getRowTitles()
                    .sort()
                    .map((title) => {
                      const titleNoPrefix = title.replace("SerializableAttributes.", "");
                      return (
                        <tr key={title}>
                          <td>
                            {camelCaseToHumanReadable(title.replace("SerializableAttributes.", ""))}
                          </td>
                          {compareProducts.map((product: any) => {
                            return (
                              <td key={product.ID}>
                                {
                                  <Interweave
                                    content={
                                      title.startsWith("SerializableAttributes.") &&
                                      product.SerializableAttributes[titleNoPrefix]
                                        ? product.SerializableAttributes[titleNoPrefix].Value +
                                          " " +
                                          product.SerializableAttributes[titleNoPrefix].UofM
                                        : product[title] != null
                                        ? product[title].toString()
                                        : ""
                                    }
                                  />
                                }
                              </td>
                            );
                          })}
                        </tr>
                      );
                    })}
                </tbody>
                <tbody>
                  <tr>
                    <td id="CompareCartStockText">
                      {t("ui.storefront.searchCatalog.compare.Stock")}
                    </td>
                    {compareProducts.map((product: any) => {
                      const { IsUnlimitedStock, IsOutOfStock, QuantityOnHand } = product;
                      let innerContent;
                      let classes = "";
                      if (IsUnlimitedStock) {
                        innerContent = "Unlimited";
                        classes = "text-success";
                      } else if (IsOutOfStock) {
                        innerContent = "Out of Stock";
                        classes = "text-danger";
                      } else if (QuantityOnHand || QuantityOnHand === 0) {
                        innerContent = QuantityOnHand;
                      }
                      return (
                        <td className="compare-text" key={product.ID}>
                          <span className={classes}>{innerContent}</span>
                        </td>
                      );
                    })}
                  </tr>
                  <tr>
                    <td id="ComparePriceText">{t("ui.storefront.common.Price")}</td>
                    {compareProducts.map((product: any) => {
                      let pricingDisplayStyle = "sideBySide";
                      let returnContent;
                      const { BasePrice, SalePrice } = product;
                      switch (pricingDisplayStyle) {
                        case "sideBySide":
                          returnContent = (
                            <p className="text-center">
                              {SalePrice !== BasePrice ? (
                                <span>
                                  <del className="text-danger small">
                                    {currencyFormatter.format(BasePrice)}
                                  </del>
                                  &nbsp;
                                  <span className="text-success very-big">
                                    {currencyFormatter.format(SalePrice)}
                                  </span>
                                </span>
                              ) : (
                                <span className="text-body very-big">
                                  {currencyFormatter.format(SalePrice)}
                                </span>
                              )}
                            </p>
                          );
                          break;
                        default:
                          returnContent = <div></div>;
                      }
                      return <td key={product.ID}>{returnContent}</td>;
                    })}
                  </tr>
                  <tr>
                    <td></td>
                    {compareProducts.map((product: any) => {
                      return (
                        <td key={product.ID}>
                          <ProductCardActionButtonWidget product={product} quantity={1} />
                        </td>
                      );
                    })}
                  </tr>
                  <tr>
                    <td></td>
                    {compareProducts.map((product: any) => {
                      return (
                        <td key={product.ID} className="text-center">
                          <Button
                            variant="link"
                            className="btn-block text-danger compare-cart-product-remove"
                            onClick={() =>
                              removeItemFromCompareCart(product.ID)
                            }>
                            <FontAwesomeIcon
                              icon={faTrash}
                              className="fa-lg me-2"
                              aria-hidden="true"
                            />
                            <span id="btnRemoveFromCompareCart">
                              {t("ui.storefront.common.Compare.RemoveFrom")}
                            </span>
                          </Button>
                        </td>
                      );
                    })}
                  </tr>
                </tbody>
              </Table>
            </Col>
            <Col sm="auto">
              <div className="paging-right">
                <Button
                  variant="outline-dark border-0"
                  type="button"
                  id="btnCompareCartPageRight">
                  <FontAwesomeIcon icon={faChevronRight} className="fa-2x" />
                  <span className="sr-only">Page Right</span>
                </Button>
              </div>
            </Col>
          </Row>
        </Col>
      </Row>
    </Fragment>
  );
});
