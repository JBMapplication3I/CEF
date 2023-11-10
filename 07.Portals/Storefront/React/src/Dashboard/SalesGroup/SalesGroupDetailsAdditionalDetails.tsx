import { Fragment } from "react";
import { useTranslation } from "react-i18next";
import { MakeAPaymentButton } from "../../Cart/controls/MakeAPaymentButton";
import {
  SalesInvoiceModel,
  SalesOrderModel,
  SalesQuoteModel
} from "../../_api/cvApi._DtoClasses";
import { AddressBlock } from "../AddressBook/AddressBlock";
import { Col, Table } from "react-bootstrap";
interface ISalesGroupDetailsAdditionalDetailsProps {
  type: string;
  salesObject: SalesOrderModel | SalesInvoiceModel | SalesQuoteModel;
}

export const SalesGroupDetailsAdditionalDetails = (props: any): JSX.Element => {
  const { type, salesObject, onPaymentConfirmed } = props;

  const { t } = useTranslation();

  function today(): string {
    return new Date().toDateString();
  }

  function hasDueDate(): boolean {
    if (salesObject.StatusKey !== "Unpaid" && salesObject.BalanceDue <= 0) {
      return false;
    }
    return "DueDate" in salesObject;
  }

  const balanceIsPastDue = !hasDueDate()
    ? false
    : Date.parse(salesObject.DueDate) < Date.parse(today());

  return (
    <Fragment>
      <Col sm={6} md={4} className="order-2 order-md-1">
        <hr className="my-2 border-bottom" />
        <h4 className="bold" id="SalesOrderDetailsText">
          {t("ui.storefront.common.Detail.Plural")}
        </h4>
        <hr className="my-2 border-bottom" />
        <Table className="w-100 mb-3" bsPrefix="">
          <tbody>
            <tr>
              <td>
                <b id="SalesOrderDateText">{t("ui.storefront.common.Date")}</b>
              </td>
              <td>
                <span id="sales-order-salesDate">
                  {/* @ts-ignore */}
                  {new Date(salesObject.CreatedDate).toDateString()}
                </span>
              </td>
            </tr>
            {type === "sales-order" ? (
              <tr>
                <td>
                  <b id="SalesOrderBalanceDueText">
                    {t("ui.storefront.userDashboard2.controls.salesDetail.BalanceDue")}
                  </b>
                </td>
                <td>
                  <span>{salesObject.BalanceDue ? salesObject.BalanceDue : "Paid"}</span>
                </td>
              </tr>
            ) : null}
            <tr>
              <td>
                <b id="SalesOrderStatusText">
                  {t("ui.storefront.userDashboard2.controls.salesDetail.Status")}
                </b>
              </td>
              <td>
                <span>{salesObject.StatusKey}</span>
              </td>
            </tr>
          </tbody>
        </Table>
        {type === "sales-invoice" ? (
          <>
            <div className="d-flex flex-column w-100 mb-3">
              <b id="SalesOrderBalanceDueText">
                {t("ui.storefront.userDashboard2.controls.salesDetail.BalanceDue")}
              </b>
              <div
                className={`p-2 alert d-flex justify-content-between align-items-center ${
                  salesObject.StatusKey === "Paid" || salesObject.BalanceDue <= 0
                    ? "alert-success"
                    : balanceIsPastDue
                    ? "alert-danger"
                    : "alert-warning"
                }`}>
                {salesObject.BalanceDue && salesObject.BalanceDue > 0
                  ? "$" + salesObject.BalanceDue
                  : "Paid"}
                {salesObject.StatusKey !== "Paid" && salesObject.BalanceDue > 0 ? (
                  <MakeAPaymentButton
                    balanceIsPastDue={balanceIsPastDue}
                    salesObject={salesObject}
                    onPaymentConfirmed={onPaymentConfirmed}
                  />
                ) : null}
              </div>
              {balanceIsPastDue && salesObject.BalanceDue > 0 ? (
                <div className="alert alert-danger">
                  <span>Your payment is passed due.</span>
                </div>
              ) : null}
            </div>
          </>
        ) : null}
      </Col>
      <Col sm={6} md={4} className="order-2 order-md-1">
        {type === "sales-order" && salesObject && salesObject.ShippingContact && (
          <Fragment>
            <hr className="my-2 border-bottom" />
            <h4 className="bold" id="SalesOrderShippingInfoText">
              {t("ui.storefront.common.ShippingInfo")}
            </h4>
            <hr className="my-2 border-bottom" />
            <AddressBlock
              address={{
                Name: salesObject.ShippingContact.FullName,
                ...salesObject.ShippingContact,
                ...salesObject.ShippingContact.Address
              }}
            />
          </Fragment>
        )}
      </Col>
    </Fragment>
  );
};
