import { Fragment, useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import cvApi from "../../_api/cvApi";
import { SalesGroupDetailsAdditionalDetails } from "./SalesGroupDetailsAdditionalDetails";
import { Table } from "../../_shared/common/Table";
import { TotalsWidget } from "../../Cart/views/TotalsWidget";
import { AddToCartButton } from "../../Cart/controls";
import { AddAllToCartButton } from "../../Cart/controls";
import { Link } from "react-router-dom";
import { Column } from "@material-table/core";
import { convertSalesItemToBasicProductData } from "../../_shared/common/Formatters";
import { currencyFormatter } from "../../_shared/common/Formatters";
import {
  faChevronDown,
  faChevronUp,
  faPrint
} from "@fortawesome/free-solid-svg-icons";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { useTranslation } from "react-i18next";
import { ISalesGroupDetailsProps } from "./_salesGroupTypes";
import { ButtonGroup, Button, Row, Col } from "react-bootstrap";
const ColumnRenderSalesItemSku = (salesItem: any): JSX.Element => (
  <Link to={`/product-detail/${salesItem.ProductSeoUrl}`}>{salesItem.Sku}</Link>
);

const ColumnRenderProductName = (salesItem: any): JSX.Element => (
  <Link to={`/product-detail/${salesItem.ProductSeoUrl}`}>
    {salesItem.Name}
  </Link>
);

const ColumnRenderAddToCart = (salesItem: any) => (
  <AddToCartButton
    product={convertSalesItemToBasicProductData(salesItem)}
    quantity={salesItem.Quantity}
  />
);

const ColumnRenderPrice = (salesItem: any) => {
  const { UnitCorePrice, UnitSoldPrice, DiscountTotal } = salesItem;
  const index = salesItem.tableData.id;
  return (
    <Fragment>
      <div
        className={`form-control-static cart-price price text-right text-muted py-0 ${
          UnitCorePrice && UnitSoldPrice > 0 && UnitCorePrice !== UnitSoldPrice
            ? "strike-through small text-disabled pb-0"
            : ""
        }`}
        id={`ProductLineItemCorePrice_${index}`}>
        {currencyFormatter.format(UnitSoldPrice)}
      </div>
      {UnitSoldPrice && UnitCorePrice !== UnitSoldPrice && UnitSoldPrice > 0 ? (
        <div
          className={`form-control-static cart-price price text-right text-danger py-0 ${
            DiscountTotal && DiscountTotal < 0
              ? "strike-through small text-disabled pb-0"
              : ""
          }`}
          id={`ProductLineItemSalePrice_${index}`}>
          {currencyFormatter.format(UnitSoldPrice)}
        </div>
      ) : null}
      {DiscountTotal && DiscountTotal < 0 ? (
        <div className="form-control-static cart-price price text-right text-success py-0">
          {currencyFormatter.format(UnitSoldPrice || UnitCorePrice || 0)}
        </div>
      ) : null}
    </Fragment>
  );
};

const ColumnRenderSubtotal = (salesItem: any) => {
  const {
    UnitCorePrice,
    UnitSoldPrice,
    DiscountTotal,
    ExtendedPrice,
    TotalQuantity
  } = salesItem;
  const index = salesItem.tableData.id;
  return (
    <Fragment>
      <div
        id={`ProductLineItemPriceSubtotal_${index}`}
        className={`form-control-static font-weight-normal text-right text-muted py-0 ${
          (UnitCorePrice &&
            UnitCorePrice !== UnitSoldPrice &&
            UnitSoldPrice > 0) ||
          (DiscountTotal && DiscountTotal < 0)
            ? "strike-through small text-disabled pb-0"
            : null
        }`}>
        {currencyFormatter.format(UnitCorePrice * TotalQuantity)}
      </div>
      {UnitCorePrice !== UnitSoldPrice && UnitSoldPrice > 0 ? (
        <div
          id={`ProductLineItemSalePriceSubtotal_${index}`}
          className={`form-control-static font-weight-normal text-right text-danger py-0 ${
            DiscountTotal && DiscountTotal < 0
              ? "strike-through small text-disabled pb-0"
              : ""
          }`}>
          {currencyFormatter.format(ExtendedPrice)}
        </div>
      ) : null}
      {DiscountTotal && DiscountTotal < 0 ? (
        <div
          id={`cartProductSubtotalWithDiscounts${index}`}
          className={`form-control-static font-weight-normal text-right text-success py-0`}>
          {currencyFormatter.format(ExtendedPrice + DiscountTotal)}
        </div>
      ) : null}
    </Fragment>
  );
};

const columns: Array<Column<any>> = [
  {
    title: "SKU",
    width: "fit-content",
    render: ColumnRenderSalesItemSku
  },
  {
    title: "Name",
    render: ColumnRenderProductName,
    cellStyle: {
      width: "100%"
    }
  },
  {
    title: "Price",
    width: "fit-content",
    render: ColumnRenderPrice
  },
  {
    title: "Quantity",
    field: "Quantity",
    width: "fit-content"
  },
  {
    title: "Subtotal",
    width: "fit-content",
    render: ColumnRenderSubtotal
  },
  {
    width: "fit-content",
    render: ColumnRenderAddToCart
  }
];

export const SalesGroupDetails = (props: ISalesGroupDetailsProps) => {
  const { params, salesGroup, salesObject, onPaymentConfirmed } = props;

  const [showActionsDropdown, setShowActionsDropdown] =
    useState<boolean>(false);
  const [allSalesItems, setAllSalesItems] = useState([]);

  const type = params.type;

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    if (
      type === "sales-invoice" &&
      salesGroup &&
      salesGroup.SalesOrderMasters &&
      salesGroup.SubSalesOrders &&
      (salesGroup.SalesOrderMasters.length || salesGroup.SubSalesOrders.length)
    ) {
      setAllSalesItems([]);
      let allOrders = [
        ...salesGroup.SalesOrderMasters,
        ...salesGroup.SubSalesOrders
      ];
      allOrders.forEach((order) => {
        getSalesOrderItems(order.ID);
      });
    }
  }, [type, salesObject]);

  function getSalesOrderItems(id: number): void {
    setRunning();
    cvApi.ordering
      .GetSecureSalesOrder(id)
      .then((res) => {
        let items = res.data?.SalesItems.filter((item) => {
          return item.Quantity > 0;
        });
        setAllSalesItems((allSalesItems) => [...allSalesItems, ...items]);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  if (!salesObject) {
    return null;
  }

  return (
    <Fragment>
      <Col>
        <div className="sales-group-inner-detail">
          <Row className="align-items-center mb-3">
            <Col className="col-pr-12">
              <h4 className="my-0">
                <span id="UserDashboardSalesOrderText">
                  {t("ui.storefront.userDashboard2.userDashboard.Sales")}{" "}
                  {type === "sales-order"
                    ? "Order"
                    : type === "sales-invoice"
                    ? "Invoice"
                    : "Quote"}
                </span>
                &nbsp;
                <span>{t("ui.storefront.common.Number.Symbol")}</span>
                {/* @ts-ignore */}
                <span>
                  {salesObject && salesObject.ID ? salesObject.ID : ""}{" "}
                  {type === "sales-invoice" &&
                  salesObject &&
                  salesObject.CustomKey
                    ? "[" + salesObject.CustomKey + "]"
                    : ""}
                </span>
              </h4>
            </Col>
            <Col xs="auto" className="d-print-none">
              <Button
                variant="outline-secondary"
                className="btn d-none d-md-inline-block"
                onClick={() => {}}
                id="btnPrint"
                title="Print"
                aria-label="Print">
                <FontAwesomeIcon icon={faPrint} aria-hidden="true" />
                &nbsp;
                <span>{t("ui.storefront.common.Print")}</span>
              </Button>
              <ButtonGroup className="d-inline-block d-md-none dropdown">
                <Button
                  variant="outline-primary"
                  className="rounded no-caret dropdown-toggle"
                  onClick={() => setShowActionsDropdown(!showActionsDropdown)}
                  id="btnActions">
                  <span>
                    {t("ui.storefront.messaging.messaging-folder.Actions")}
                  </span>
                  <FontAwesomeIcon
                    icon={showActionsDropdown ? faChevronUp : faChevronDown}
                    className="pl-2"
                  />
                </Button>
                <ul className="dropdown-menu dropdown-menu-right cef-dropdown">
                  <li role="menuitem">
                    <Button
                      className="btn-link-dropdown-item wit-icon w-100 text-left"
                      id="btnPrintMenuItem"
                      title="Print"
                      aria-label="Print">
                      <FontAwesomeIcon icon={faPrint} aria-hidden="true" />
                      &nbsp;
                      <span>{t("ui.storefront.common.Print")}</span>
                    </Button>
                  </li>
                </ul>
              </ButtonGroup>
            </Col>
          </Row>
          <Row className="mb-3">
            <Col xs={12}>
              <Table
                data={
                  type === "sales-invoice"
                    ? allSalesItems
                    : salesObject.SalesItems.filter(
                        (item: any) => item.Quantity > 0
                      )
                }
                columns={columns}
              />
            </Col>
          </Row>
          <Row className="mb-3">
            <Col xs={12} sm={6} md={4} className="order-1 order-md-2">
              <Row>
                <Col>
                  {/* @ts-ignore object is possibly 'null' */}
                  <TotalsWidget Totals={salesObject.Totals} />
                </Col>
              </Row>
              <Row className=" d-print-none">
                <Col>
                  <ButtonGroup className="w-100 dropdown">
                    <AddAllToCartButton
                      products={
                        type !== "sales-invoice"
                          ? salesObject.SalesItems
                          : allSalesItems
                      }
                      useConfirmModal={true}
                    />
                  </ButtonGroup>
                </Col>
              </Row>
            </Col>
            <SalesGroupDetailsAdditionalDetails
              type={type}
              salesObject={salesObject}
              onPaymentConfirmed={() => {
                if (onPaymentConfirmed) {
                  onPaymentConfirmed();
                }
              }}
            />
          </Row>
        </div>
      </Col>
    </Fragment>
  );
};
