import {
  SalesGroupModel,
  SalesInvoiceModel,
  SalesOrderModel,
  SalesQuoteModel
} from "../../_api/cvApi._DtoClasses";

type ISalesGroupType = "sales-order" | "sales-quote" | "sales-invoice";

export interface ISalesGroupParams {
  salesGroupId: string;
  type: ISalesGroupType;
  typeId: string;
}

export interface ISalesGroupDetailsProps {
  params: ISalesGroupParams;
  salesGroup: SalesGroupModel;
  salesObject: SalesOrderModel | SalesInvoiceModel | SalesQuoteModel;
  onPaymentConfirmed?: Function;
}

export interface ISalesGroupParams {
  salesGroupId: string;
  type: ISalesGroupType;
  typeId: string;
}

export interface ISalesGroupSideContentProps {
  type: ISalesGroupType;
  salesGroup: SalesGroupModel;
  selectedSalesTab: number;
  onSelectedSalesTabChange: Function;
  params: ISalesGroupParams;
}
