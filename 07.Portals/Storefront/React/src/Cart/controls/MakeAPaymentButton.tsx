import { useState } from "react";
import { useTranslation } from "react-i18next";
import { PayInvoiceModal } from "../../Dashboard/Invoices/PayInvoiceModal";
import {
  SalesInvoiceModel,
  SalesOrderModel,
  SalesQuoteModel
} from "../../_api/cvApi._DtoClasses";
import { Button, ButtonGroup } from "react-bootstrap";

interface IMakeAPaymentProps {
  classes?: string;
  icon?: boolean;
  excludeQuoteCart?: boolean;
  label?: string;
  disabled?: boolean;
  product?: any;
  quantity?: number;
  balanceIsPastDue: boolean;
  salesObject: SalesOrderModel | SalesInvoiceModel | SalesQuoteModel;
  onPaymentConfirmed?: Function;
}

export const MakeAPaymentButton = (props: IMakeAPaymentProps): JSX.Element => {
  const {
    classes,
    disabled,
    balanceIsPastDue,
    salesObject,
    onPaymentConfirmed
  } = props;

  const [showPayInvoiceModal, setShowPayInvoiceModal] =
    useState<boolean>(false);

  const { t } = useTranslation();

  return (
    <div className="position-relative">
      <PayInvoiceModal
        show={showPayInvoiceModal}
        invoice={salesObject}
        onConfirm={() => {
          setShowPayInvoiceModal(false);
          if (onPaymentConfirmed) {
            onPaymentConfirmed();
          }
        }}
        onCancel={() => setShowPayInvoiceModal(false)}
      />
      <ButtonGroup className="w-100">
        <Button
          variant={balanceIsPastDue ? "danger" : "warning"}
          className={classes ?? "btn-block btn-md rounded-start text-nowrap"}
          disabled={disabled || false}
          type="button"
          onClick={() => setShowPayInvoiceModal(true)}>
          {t(
            "ui.storefront.userDashboard.controls.salesGroupDetail.MakeAPayment"
          )}
        </Button>
      </ButtonGroup>
    </div>
  );
};
