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
  faQuoteRight
} from "@fortawesome/free-solid-svg-icons";
import { useTranslation } from "react-i18next";
import { Button, Row, Col } from "react-bootstrap";

/* incomplete */
interface ISalesQuote {
  ID: number;
  SalesGroupAsRequestMasterID?: number;
  SalesGroupAsRequestSubID?: number;
  CustomKey: string;
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

interface IQuotesFilterData extends IObjectKeys {
  ID: string;
  Total: string;
  "Sales Quote Date": {
    MinDate: string;
    MaxDate: string;
  };
  Status: string;
}

export const Quotes = (props: any) => {
  const { isDashboardMain } = props;
  let pageSize, pageSizeOptions;
  if (isDashboardMain) {
    pageSize = 5;
    pageSizeOptions = [5, 10, 15];
  } else {
    pageSize = 8;
    pageSizeOptions = [8, 16, 24];
  }
  const [error, setError] = useState(null);
  const [startIndex, setStartIndex] = useState(1);
  const [size, setSize] = useState<number>(pageSize);
  const [sortDirection, setSortDirection] = useState<string>("desc");
  const [sortField, setSortField] = useState<string>("CreatedDate");
  const [order, setOrder] = useState<number>(1);
  const [quotes, setQuotes] = useState([]);
  const [statuses, setStatuses] = useState([]);
  const [totalQuotesCount, setTotalQuotesCount] = useState(0);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  const tableRef = useRef(null);

  useEffect(() => {
    getQuoteStatuses();
  }, []);

  useEffect(() => {
    getQuotes();
  }, [startIndex, size, sortDirection, sortField]);

  async function getQuotes(data: any = {}) {
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
      .GetCurrentAccountSalesQuotes(sendData)
      .then((result: any) => {
        const quotesWithReadableDate = result.data.Results.map((r: any) => {
          if (!r.CreatedDate) {
            return r;
          }
          const date = new Date(r.CreatedDate);
          return {
            ...r,
            CreatedDate: `${date.toDateString()} ${date.toLocaleTimeString()}`
          };
        });
        setQuotes(quotesWithReadableDate);
        setTotalQuotesCount(result.data.TotalCount);
        finishRunning(false);
        // @ts-ignore
        tableRef.current.onQueryChange();
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || "Failed to get current account sales quotes"
        );
      });
  }

  function getQuoteStatuses() {
    setRunning();
    cvApi.quoting
      .GetSalesQuoteStatuses()
      .then((result: any) => {
        setStatuses(result.data.Results);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || "failed to get sales quote statuses"
        );
      });
  }

  function ViewDetailsButton(rowData: ISalesQuote): JSX.Element {
    const { ID, SalesGroupAsRequestMasterID, SalesGroupAsRequestSubID } =
      rowData;
    return (
      <Button
        as="a"
        variant="outline-primary"
        size="sm"
        disabled={
          !SalesGroupAsRequestMasterID && !SalesGroupAsRequestSubID
            ? true
            : false
        }
        href={`/dashboard/sales-group/${
          SalesGroupAsRequestMasterID ?? SalesGroupAsRequestSubID
        }/sales-quote/${ID}`}>
        {t("ui.storefront.common.View")}
      </Button>
    );
  }

  const columns: Array<Column<ISalesQuote>> = [
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

  const filterColumns: Array<IToolbarFilterColumn> = [
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
      toolbarFilterTitle: "Sales Quote Date",
      toolbarFilterType: "minmaxdate"
    }
  ];

  const formatFiltersObjectForGetQuotes = (
    filterData: IQuotesFilterData
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
          case "Sales Quote Date":
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
        <section className="section-quotes">
          <div className="section-title d-flex align-items-center justify-content-between">
            {isDashboardMain ? (
              <Fragment>
                <h4 className="title mr-2">
                  <FontAwesomeIcon icon={faQuoteRight} className="me-2" />
                  <span>Recent {t("ui.storefront.common.Quote.Plural")}</span>
                </h4>
                <p className="text-right">
                  <Link
                    to="/dashboard/quotes"
                    className="btn btn-link"
                    id="userDashboard_salesQuote_viewAll">
                    <span className="mr-1">View All</span>
                    <FontAwesomeIcon icon={faAngleDoubleRight} />
                  </Link>
                </p>
              </Fragment>
            ) : (
              <h1 className="title mr-2">
                <FontAwesomeIcon icon={faQuoteRight} className="me-2" />
                <span>{t("ui.storefront.common.Quote.Plural")}</span>
              </h1>
            )}
          </div>
          <CVGrid
            columns={columns}
            filterColumns={isDashboardMain ? [] : filterColumns}
            data={quotes}
            defaultPageSize={pageSize}
            defaultPageSizeOptions={pageSizeOptions}
            ref={tableRef}
            page={startIndex - 1}
            totalCount={totalQuotesCount}
            onRefreshClicked={(data: any) => getQuotes(formatFiltersObjectForGetQuotes(data))}
            onFilterClicked={(data: IQuotesFilterData) =>
              getQuotes(formatFiltersObjectForGetQuotes(data))
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
