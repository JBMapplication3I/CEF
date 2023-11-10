import { useEffect, useState } from "react";
import { faObjectGroup } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useRouteMatch } from "react-router";
import { AddressBlock } from "../AddressBook/AddressBlock";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { Link } from "react-router-dom";
import { SalesGroupDetails } from "../SalesGroup/SalesGroupDetails";
import { SalesGroupSideContent } from "./SalesGroupSideContent";
import cvApi from "../../_api/cvApi";
import {
  SalesGroupModel,
  SalesInvoiceModel,
  SalesOrderModel,
  SalesQuoteModel
} from "../../_api/cvApi._DtoClasses";
import { useTranslation } from "react-i18next";
import { ErrorView } from "../../_shared/common/ErrorView";
import { ISalesGroupParams } from "./_salesGroupTypes";
import { Nav, Row, Col, Table } from "react-bootstrap";

export const SalesGroup = () => {
  const [salesGroup, setSalesGroup] = useState<SalesGroupModel>(null);
  const [salesOrder, setSalesOrder] = useState<SalesOrderModel>(null);
  const [cachedSalesOrders, setCachedSalesOrders] = useState<
    Array<SalesOrderModel>
  >([]);
  const [cachedSalesInvoices, setCachedSalesInvoices] = useState<
    Array<SalesInvoiceModel>
  >([]);
  const [cachedSalesQuotes, setCachedSalesQuotes] = useState<
    Array<SalesQuoteModel>
  >([]);
  const [salesInvoice, setSalesInvoice] = useState<SalesInvoiceModel>(null);
  const [salesQuote, setSalesQuote] = useState<any>(null);
  const [selectedSalesTab, setSelectedSalesTab] = useState(0);

  const { params } = useRouteMatch<ISalesGroupParams>(
    "/dashboard/sales-group/:salesGroupId/:type/:typeId"
  );

  const salesGroupId = params.salesGroupId;
  const type = params.type;
  const typeId = params.typeId;

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();
  //const { salesGroupId, type, typeId } = breadCrumbs<ISalesGroupParams>();

  useEffect(() => {
    getSalesGroup();
    getSalesObject();
  }, []);

  function getSalesGroup(): void {
    setRunning();
    cvApi.sales
      .GetSecureSalesGroup(+salesGroupId)
      .then((res) => {
        setSalesGroup(res.data);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  function getSalesOrder(id: number): void {
    setRunning();
    if (cachedSalesOrders.length) {
      let cachedOrder = cachedSalesOrders.find((order) => {
        return order.ID === +id;
      });
      if (cachedOrder) {
        setSalesOrder(cachedOrder);
        setSelectedSalesTab(cachedOrder.ID);
        finishRunning();
        return;
      }
    }
    cvApi.ordering
      .GetSecureSalesOrder(id)
      .then((res) => {
        setSalesOrder(res.data);
        setCachedSalesOrders((cachedSalesOrders) => [
          ...cachedSalesOrders,
          res.data
        ]);
        setSelectedSalesTab(res.data.ID);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  function getSalesQuote(id: number): void {
    setRunning();
    cvApi.providers
      .GetSecureSalesQuote(id)
      .then((res) => {
        setSalesQuote(res.data);
        setSelectedSalesTab(res.data.ID);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  function getSalesInvoice(id: number): void {
    if (cachedSalesInvoices.length) {
      let cachedInvoice = cachedSalesInvoices.find((invoice) => {
        return invoice.ID === +id;
      });
      if (cachedInvoice) {
        setSalesOrder(cachedInvoice);
        setSelectedSalesTab(cachedInvoice.ID);
        return;
      }
    }
    setRunning();
    cvApi.providers
      .GetSecureSalesInvoice(id)
      .then((res) => {
        setSalesInvoice(res.data);
        // TODO: setCachedSalesInvoices needs an array, res is a single
        // setCachedSalesInvoices(res.data);
        setSelectedSalesTab(res.data.ID);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  function getSalesObject(id = parseInt(typeId)): void {
    switch (type) {
      case "sales-order":
        getSalesOrder(id);
        break;
      case "sales-invoice":
        getSalesInvoice(id);
        break;
      case "sales-quote":
        getSalesQuote(id);
        break;
    }
  }

  function salesObjectInSalesGroup(): boolean {
    if (!salesGroup) {
      return false;
    }
    switch (type) {
      case "sales-order":
        return (
          [
            ...salesGroup.SalesOrderMasters,
            ...salesGroup.SubSalesOrders
          ].filter((order) => {
            return order.ID === +typeId;
          }).length > 0
        );
      case "sales-invoice":
        return (
          salesGroup.SalesInvoices.filter((invoice) => {
            return invoice.ID === +typeId;
          }).length > 0
        );
      case "sales-quote":
        return (
          [
            ...salesGroup.SalesQuoteRequestMasters,
            ...salesGroup.SalesQuoteRequestSubs
          ].filter((quote) => {
            return quote.ID === +typeId;
          }).length > 0
        );
    }
  }

  function handleNewTab(tabName: "order" | "invoice" | "quote"): void {
    switch (tabName) {
      case "order":
        getSalesOrder(salesGroup.SubSalesOrders[0].ID);
        setSelectedSalesTab(salesGroup.SubSalesOrders[0].ID);
        break;
      case "invoice":
        getSalesInvoice(salesGroup.SalesInvoices[0].ID);
        setSelectedSalesTab(salesGroup.SalesInvoices[0].ID);
        break;
    }
  }

  function onSelectedSalesTabChange(id: number): void {
    getSalesObject(id);
  }

  const refreshForSalesInvoice = () => {
    getSalesInvoice(+typeId);
  };

  const renderTabMainContent = () => {
    if (viewState.running) {
      return <LoadingWidget />;
    }
    if (salesGroup) {
      switch (type) {
        case "sales-order":
          if (!salesOrder) {
            getSalesOrder(parseInt(typeId));
          }
          return (
            <SalesGroupDetails
              params={params}
              salesGroup={salesGroup}
              salesObject={salesOrder}
            />
          );
        case "sales-invoice":
          if (!salesInvoice) {
            getSalesInvoice(parseInt(typeId));
          }
          return (
            <SalesGroupDetails
              params={params}
              salesGroup={salesGroup}
              salesObject={salesInvoice}
              onPaymentConfirmed={refreshForSalesInvoice}
            />
          );

        case "sales-quote":
          if (!salesQuote) {
            getSalesQuote(parseInt(typeId));
          }
          return (
            <SalesGroupDetails
              params={params}
              salesGroup={salesGroup}
              salesObject={salesQuote}
            />
          );
      }
    }
  };

  if (!salesGroup && viewState.running) {
    return <LoadingWidget />;
  }

  if (!salesGroup || !salesObjectInSalesGroup()) {
    return (
      <ErrorView error={viewState.errorMessage ?? "Invalid sales group"} />
    );
  }

  return (
    <Row>
      <Col xs={12}>
        <div className="mb-3">
          <Row className="align-items-center">
            <Col>
              <h1 className="mt-0">
                <FontAwesomeIcon icon={faObjectGroup} className="me-2" />
                <span id="IndividualSalesGroupTest">
                  {t("ui.storefront.common.SalesGroup")}
                </span>
                &nbsp;
                <span>{t("ui.storefront.common.Number.Symbol")}</span>
                <span>{salesGroup && salesGroup.ID ? salesGroup.ID : ""}</span>
              </h1>
            </Col>
          </Row>
          <Row>
            {salesGroup && salesGroup.BillingContact && (
              <Col xs={12} sm={6}>
                <hr className="my-2 border-bottom" />
                <h4
                  className="bold px-3"
                  id="IndividualSalesGroupBillingInformation">
                  {t(
                    "ui.storefront.storeDashboard.storeOrderDetail.BillingInformation"
                  )}
                </h4>
                <hr className="my-2 border-bottom" />
                <AddressBlock
                  address={{
                    Name: salesGroup.BillingContact.FullName,
                    ...salesGroup.BillingContact,
                    ...salesGroup.BillingContact.Address
                  }}
                />
              </Col>
            )}
            {/* <Col xs={12} sm={6}>
              <hr className="my-2 border-bottom" />
              <h4 className="bold px-3" id="IndividualSalesGroupEvents">
                {t("ui.storefront.common.Events")}
              </h4>
              <hr className="my-2 border-bottom" />
              <Table
                size="sm"
                striped
                hover
                className={classes.fixedHeightTable}>
                <thead>
                  <tr>
                    <th className="border-medium w-35">
                      {t("ui.storefront.common.Date")}
                    </th>
                    <th className="border-medium">
                      {t("ui.storefront.common.Detail.Plural")}
                    </th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td colSpan={2} className="border-medium">
                      {t("ui.storefront.common.NoEvents")}
                    </td>
                  </tr>
                </tbody>
              </Table>
            </Col> */}
          </Row>
          <Row>
            <Col xs={12} className="d-print-none">
              <Nav className="nav-tabs mb-3">
                <Nav.Item>
                  <Link
                    to={`/dashboard/sales-group/${salesGroupId}/sales-quote/${
                      salesGroup.SalesQuoteRequestSubs?.length
                        ? /* @ts-ignore */
                          salesGroup.SalesQuoteRequestSubs[0].ID
                        : ""
                    }`}
                    className={`btn nav-link ${
                      type === "sales-quote" ? "active" : ""
                    } ${
                      salesGroup.SalesQuoteRequestSubs?.length ? "" : "disabled"
                    }`}>
                    <span>{t("ui.storefront.common.Quote.Plural")}</span>
                    <span>
                      ({salesGroup.SalesQuoteRequestSubs?.length ?? "0"})
                    </span>
                  </Link>
                </Nav.Item>
                <Nav.Item>
                  <Link
                    to={`/dashboard/sales-group/${salesGroupId}/sales-order/${
                      salesGroup.SubSalesOrders?.length
                        ? /* @ts-ignore */
                          salesGroup.SubSalesOrders[0].ID
                        : ""
                    }`}
                    onClick={() => handleNewTab("order")}
                    className={`btn nav-link ${
                      type === "sales-order" ? "active" : ""
                    } ${salesGroup.SubSalesOrders?.length ? "" : "disabled"}`}>
                    <span>{t("ui.storefront.common.Order.Plural")}</span>
                    <span>({salesGroup.SubSalesOrders?.length ?? "0"})</span>
                  </Link>
                </Nav.Item>
                <Nav.Item>
                  <Link
                    to={`/dashboard/sales-group/${salesGroupId}/sales-invoice/${
                      salesGroup.SalesInvoices?.length
                        ? /* @ts-ignore */
                          salesGroup.SalesInvoices[0].ID
                        : ""
                    }`}
                    onClick={() => handleNewTab("invoice")}
                    className={`btn nav-link ${
                      type === "sales-invoice" ? "active" : ""
                    } ${salesGroup.SalesInvoices?.length ? "" : "disabled"}`}>
                    <span>{t("ui.storefront.common.Invoice.Plural")}</span>
                    <span>({salesGroup.SalesInvoices?.length ?? "0"})</span>
                  </Link>
                </Nav.Item>
              </Nav>
            </Col>
            {salesGroup && (salesOrder || salesInvoice || salesQuote) ? (
              <SalesGroupSideContent
                type={type}
                salesGroup={salesGroup}
                selectedSalesTab={selectedSalesTab}
                onSelectedSalesTabChange={onSelectedSalesTabChange}
                params={params}
              />
            ) : (
              <LoadingWidget />
            )}
            {salesGroup && (salesOrder || salesInvoice || salesQuote) ? (
              renderTabMainContent()
            ) : (
              <LoadingWidget />
            )}
          </Row>
        </div>
      </Col>
    </Row>
  );
};
