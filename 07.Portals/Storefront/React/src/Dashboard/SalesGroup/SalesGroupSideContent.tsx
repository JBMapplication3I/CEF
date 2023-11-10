import { ISalesGroupSideContentProps } from "./_salesGroupTypes";
import {
  faCoffee,
  faFileInvoiceDollar,
  faQuoteRight,
  faReceipt
} from "@fortawesome/free-solid-svg-icons";
import { IconProp } from "@fortawesome/fontawesome-svg-core";
import { useTranslation } from "react-i18next";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  SalesInvoiceModel,
  SalesOrderModel,
  SalesQuoteModel
} from "../../_api/cvApi._DtoClasses";
import { SalesTab } from "./SalesTab";
import { Card, Col, ListGroup } from "react-bootstrap";

export const SalesGroupSideContent = (props: ISalesGroupSideContentProps) => {
  const {
    type,
    salesGroup,
    selectedSalesTab,
    onSelectedSalesTabChange,
    params
  } = props;

  const { t } = useTranslation();

  if (!salesGroup || !salesGroup.ID) {
    return null;
  }

  let icon: IconProp = faCoffee;
  let translation: string = "";
  let masterKey: string = "";
  let subKey: string = "";

  let masters: SalesOrderModel[] | SalesInvoiceModel[] | SalesQuoteModel[];
  let subs: SalesOrderModel[] | SalesQuoteModel[];

  switch (type) {
    case "sales-order":
      icon = faReceipt;
      translation = "ui.storefront.common.Order.Plural";
      masters = salesGroup.SalesOrderMasters;
      subs = salesGroup.SubSalesOrders;
      subKey =
        "ui.storefront.userDashboard.controls.salesGroupDetail.SubOrders";
      break;
    case "sales-invoice":
      icon = faFileInvoiceDollar;
      translation = "ui.storefront.common.Invoice.Plural";
      masters = salesGroup.SalesInvoices;
      masterKey = "ui.storefront.common.SalesInvoice.Plural";
      break;
    case "sales-quote":
      icon = faQuoteRight;
      translation = "ui.storefront.common.Quote.Plural";
      masters = salesGroup.SalesQuoteRequestMasters;
      subs = salesGroup.SalesQuoteRequestSubs;
      masterKey = "ui.storefront.common.SalesQuote.Plural";
      subKey =
        "ui.storefront.userDashboard.controls.salesGroupDetail.SubOrders";
      break;
  }
  return (
    <Col xs="auto" className="d-print-none">
      <h4 className="mt-0 mb-3">
        <FontAwesomeIcon icon={icon} className="fa-fw" />
        &nbsp;
        <span>{t(translation)}</span>
      </h4>
      <Card className="border-0">
        <Card.Body className="p-1">
          <b
            className="card-subtitle text-muted"
            id="IndividualSalesGroupOrderMasterText">
            {t(masterKey)}
          </b>
        </Card.Body>
        {masters.map(
          (
            master: SalesOrderModel | SalesInvoiceModel | SalesQuoteModel,
            index: number
          ) => {
            // @ts-ignore
            const { ID, Totals } = master;
            return (
              <SalesTab
                key={ID}
                ID={ID}
                classes="border-bottom-0"
                Totals={Totals}
                index={index}
                selectedSalesTab={selectedSalesTab}
                onSelectedSalesTabChange={onSelectedSalesTabChange}
                params={params}
              />
            );
          }
        )}
        {subs && (
          <>
            <Card.Body className="p-1">
              <b className="card-subtitle text-muted">{t(subKey)}</b>
            </Card.Body>
            <ListGroup variant="flush" className="pr-2 side-tabs">
              {subs.map(
                (sub: SalesOrderModel | SalesQuoteModel, index: number) => {
                  // @ts-ignore
                  const { ID, Totals } = sub;
                  return (
                    <SalesTab
                      key={ID}
                      ID={ID}
                      classes="border-bottom-0"
                      Totals={Totals}
                      index={index}
                      selectedSalesTab={selectedSalesTab}
                      onSelectedSalesTabChange={onSelectedSalesTabChange}
                      params={params}
                    />
                  );
                }
              )}
            </ListGroup>
          </>
        )}
      </Card>
    </Col>
  );
};
