import { useState, useEffect, useRef, Fragment } from "react";
import { Link, Route, Switch } from "react-router-dom";
import { Column } from "@material-table/core";
import { ErrorView } from "../../_shared/common/ErrorView";
import cvApi from "../../_api/cvApi";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { CVGrid } from "../../_shared/common/CVGrid";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faAngleDoubleRight,
  faFileInvoiceDollar
} from "@fortawesome/free-solid-svg-icons";
import { useTranslation } from "react-i18next";
import { Button, Row, Col } from "react-bootstrap";

/* incomplete */
export interface ISalesInvoice {
  ID: number;
  SalesGroupID: number;
  CustomKey: string;
  DueDate: string;
  ItemQuantity: number;
  Status: {
    Active: boolean;
    CreatedDate: Date;
    DisplayName: string;
  };
  Totals: {
    Discounts: number;
    Fees: number;
    Handling: number;
    Shipping: number;
    SubTotal: number;
    Tax: number;
    Total: number;
  };
  CreatedDate: Date;
  StatusKey: string;
  TypeName: string;
}

interface IToolbarFilterColumn {
  toolbarFilterType:
    | "input"
    | "autocomplete"
    | "attribute"
    | "id"
    | "select"
    | "boolean"
    | "minmaxnumber"
    | "minmaxdate";
  toolbarFilterOptions?: Array<any>;
  toolbarFilterTitle: string;
  toolbarFilterPlaceholder?: string;
  toolbarFilterMin?: number;
  toolbarFilterMax?: number;
}

interface IObjectKeys {
  [key: string]: any;
}

interface IInvoicesFilterData extends IObjectKeys {
  ID: string;
  Total: string;
  "Invoice Date": {
    MinDate: string;
    MaxDate: string;
  };
  Status: string;
}

export const Invoices = (props: any) => {
  const { isDashboardMain } = props;
  let pageSize, pageSizeOptions;
  if (isDashboardMain) {
    pageSize = 5;
    pageSizeOptions = [5, 10, 15];
  } else {
    pageSize = 8;
    pageSizeOptions = [8, 16, 24];
  }
  const [error, setError] = useState<any>(null);
  const [startIndex, setStartIndex] = useState<number>(1);
  const [size, setSize] = useState<number>(pageSize);
  const [sortDirection, setSortDirection] = useState<string>("desc");
  const [sortField, setSortField] = useState<string>("CreatedDate");
  const [order, setOrder] = useState<number>(1);
  const [invoices, setInvoices] = useState<Array<any>>([]);
  const [statuses, setStatuses] = useState<Array<any>>([]);
  const [totalInvoicesCount, setTotalInvoicesCount] = useState<number>(0);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  const tableRef = useRef(null);

  useEffect(() => {
    getInvoiceStatuses();
  }, []);

  useEffect(() => {
    getInvoices();
  }, [startIndex, size, sortDirection, sortField]);

  async function getInvoices(data: any = {}) {
    setRunning();
    let sendData: any = {
      Paging: {
        StartIndex: startIndex,
        Size: size
      },
      Sorts: [
        {
          dir: sortDirection,
          field: sortField,
          order: order
        }
      ],
      ...data
    };
    cvApi.providers
      .GetCurrentAccountSalesInvoices(sendData)
      .then((result: any) => {
        const invoicesWithReadableDate = result.data.Results.map((r: any) => {
          if (!r.CreatedDate) {
            return r;
          }
          const date = new Date(r.CreatedDate);
          return {
            ...r,
            CreatedDate: `${date.toDateString()} ${date.toLocaleTimeString()}`
          };
        });
        setInvoices(invoicesWithReadableDate);
        setTotalInvoicesCount(result.data.TotalCount);
        finishRunning(false);
        // @ts-ignore
        tableRef.current.onQueryChange();
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || "failed to get current account sales invoices"
        );
      });
  }

  function getInvoiceStatuses(): void {
    setRunning();
    cvApi.invoicing
      .GetSalesInvoiceStatuses()
      .then((result: any) => {
        setStatuses(result.data.Results);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "Failed to get invoices statuses");
      });
  }

  function ViewDetailsButton(rowData: ISalesInvoice): JSX.Element {
    const { ID, SalesGroupID } = rowData;
    return (
      <Button
        as="a"
        variant="outline-primary"
        size="sm"
        disabled={!SalesGroupID ? true : false}
        href={`/dashboard/sales-group/${SalesGroupID}/sales-invoice/${ID}`}>
        {t("ui.storefront.common.View")}
      </Button>
    );
  }

  const columns: Array<Column<ISalesInvoice>> = [
    {
      title: "ID",
      field: "ID",
      width: "5%",
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" },
      emptyValue: "N/A",
      type: "string"
    },
    {
      title: t("ui.storefront.common.Total"),
      field: "Totals.Total",
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" },
      emptyValue: "N/A",
      filterPlaceholder: "1.99",
      type: "currency"
    },
    {
      title: t("ui.storefront.common.Date"),
      field: "CreatedDate",
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" },
      emptyValue: "N/A",
      type: "datetime"
    },
    {
      title: t("ui.storefront.userDashboard2.controls.salesDetail.Status"),
      field: "Status.DisplayName",
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" },
      emptyValue: "N/A",
      type: "string"
    },
    {
      width: "fit-content",
      render: ViewDetailsButton,
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" }
    }
  ];

  const filterColumns: IToolbarFilterColumn[] = [
    {
      toolbarFilterTitle: "Status",
      toolbarFilterType: "select",
      toolbarFilterOptions: statuses
    },
    {
      toolbarFilterTitle: "ID",
      toolbarFilterType: "id"
    },
    {
      toolbarFilterTitle: "Invoice Date",
      toolbarFilterType: "minmaxdate"
    }
  ];

  const formatFiltersObjectForGetInvoices = (
    filterData: IInvoicesFilterData
  ): any => {
    const formatted: any = {};
    for (const filterCol of filterColumns) {
      const filterValue = filterData[filterCol.toolbarFilterTitle];
      if (filterValue) {
        switch (filterCol.toolbarFilterTitle) {
          case "Total":
            formatted.Totals = { Total: filterValue };
            break;
          case "Status":
            if (filterCol.toolbarFilterOptions && filterValue !== "All") {
              formatted.StatusID = filterCol.toolbarFilterOptions.find(
                (op) => op.CustomKey === filterValue
              ).ID;
              break;
            }
            formatted.StatusID = "";
            break;
          case "ID":
            formatted.ID = filterValue;
            break;
          case "Invoice Date":
            if (typeof filterValue !== "string" && filterValue.MinDate) {
              formatted.MinDate = new Date(filterValue.MinDate).toISOString();
            }
            if (typeof filterValue !== "string" && filterValue.MaxDate) {
              formatted.MaxDate = new Date(filterValue.MaxDate).toISOString();
            }
            break;
        }
      }
    }
    return formatted;
  };

  return (
    <Row>
      <Col>
        <section className="section-invoices">
          <div className="section-title d-flex align-items-center justify-content-between">
            {isDashboardMain ? (
              <Fragment>
                <h4 className="title">
                  <FontAwesomeIcon icon={faFileInvoiceDollar} className="me-2" />
                  <span>Recent {t("ui.storefront.common.Invoice.Plural")}</span>
                </h4>
                <p className="text-right">
                  <Link
                    to="/dashboard/invoices"
                    className="btn btn-link"
                    id="userDashboard_salesInvoice_viewAll">
                    <span className="mr-1">View All</span>
                    <FontAwesomeIcon icon={faAngleDoubleRight} />
                  </Link>
                </p>
              </Fragment>
            ) : (
              <h1 className="title mr-2">
                <FontAwesomeIcon icon={faFileInvoiceDollar} className="me-2" />
                <span>{t("ui.storefront.common.Invoice.Plural")}</span>
              </h1>
            )}
          </div>
          <CVGrid
            columns={columns}
            filterColumns={isDashboardMain ? [] : filterColumns}
            data={invoices}
            defaultPageSize={pageSize}
            defaultPageSizeOptions={pageSizeOptions}
            ref={tableRef}
            page={startIndex - 1}
            totalCount={totalInvoicesCount}
            onRefreshClicked={(data: any) => getInvoices(formatFiltersObjectForGetInvoices(data))}
            onFilterClicked={(data: IInvoicesFilterData) =>
              getInvoices(formatFiltersObjectForGetInvoices(data))
            }
            onPageChange={(page: number, pageSize: number) => {
              setStartIndex(page + 1);
              setSize(pageSize);
            }}
            onOrderChange={(invoiceBy: number, invoiceDirection: string) => {
              let sortFieldToApply = "CreatedDate";
              let sortDirectionToApply = "desc";
              if (invoiceBy >= 0) {
                // @ts-ignore possibly 'undefined'
                sortFieldToApply = columns[invoiceBy].field;
                if (invoiceDirection.length) {
                  sortDirectionToApply = invoiceDirection;
                }
              }
              setSortField(sortFieldToApply);
              setSortDirection(sortDirectionToApply);
            }}
            isLoading={viewState.running}
          />
        </section>
        <ErrorView error={error} />
      </Col>
    </Row>
  );
};
