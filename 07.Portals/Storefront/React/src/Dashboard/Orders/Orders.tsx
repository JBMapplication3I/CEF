import React, { useState, useEffect, useRef, Fragment } from "react";
import { Link, Route, Switch } from "react-router-dom";
import { Column } from "@material-table/core";
import { ErrorView } from "../../_shared/common/ErrorView";
import cvApi from "../../_api/cvApi";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { CVGrid } from "../../_shared/common/CVGrid";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faAngleDoubleRight,
  faReceipt
} from "@fortawesome/free-solid-svg-icons";
import { useTranslation } from "react-i18next";
import { Row, Col } from "react-bootstrap";
/* incomplete */
interface ISalesOrder {
  ID: number;
  CustomKey: string;
  ItemQuantity: number;
  SalesGroupAsMasterID?: number;
  SalesGroupAsSubID?: number;
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

interface IOrdersFilterData extends IObjectKeys {
  ID: string;
  Total: string;
  "Order Date": {
    MinDate: string;
    MaxDate: string;
  };
  Status: string;
}

export const Orders = (props: any) => {
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
  const [orders, setOrders] = useState([]);
  const [statuses, setStatuses] = useState([]);
  const [totalOrdersCount, setTotalOrdersCount] = useState(0);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  const tableRef = useRef(null);

  useEffect(() => {
    getOrderStatuses();
  }, []);

  useEffect(() => {
    setError(null);
    getOrders();
  }, [startIndex, size, sortDirection, sortField]);

  async function getOrders(data: any = {}) {
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
    cvApi.ordering
      .GetCurrentAccountSalesOrders(sendData)
      .then((result: any) => {
        const ordersWithReadableDate = result.data.Results.map((r: any) => {
          if (!r.CreatedDate) {
            return r;
          }
          const date = new Date(r.CreatedDate);
          return {
            ...r,
            CreatedDate: `${date.toDateString()} ${date.toLocaleTimeString()}`
          };
        });
        setOrders(ordersWithReadableDate);
        setTotalOrdersCount(result.data.TotalCount);
        finishRunning(false);
        // @ts-ignore
        tableRef.current.onQueryChange();
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || "Failed to get current account sales orders"
        );
      });
  }

  function getOrderStatuses(): void {
    cvApi.ordering
      .GetSalesOrderStatuses()
      .then((result: any) => {
        setStatuses(result.data.Results);
      })
      .catch((err: any) => {
        setError(err);
      });
  }

  function ViewDetailsButton(rowData: ISalesOrder): JSX.Element {
    const { SalesGroupAsMasterID, SalesGroupAsSubID, ID } = rowData;
    return (
      <Link
        to={`/dashboard/sales-group/${
          SalesGroupAsMasterID || SalesGroupAsSubID
        }/sales-order/${ID}`}
        className={`btn btn-outline-primary btn-sm ${
          !SalesGroupAsMasterID && !SalesGroupAsSubID ? "disabled" : ""
        }`}
        type="button">
        {t("ui.storefront.common.View")}
      </Link>
    );
  }

  const columns: Array<Column<ISalesOrder>> = [
    {
      title: "ID",
      field: "ID",
      width: "5%",
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" },
      emptyValue: "N/A",
      type: "string"
    },
    {
      title: "Total",
      field: "Totals.Total",
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" },
      emptyValue: "N/A",
      filterPlaceholder: "1.99",
      type: "currency"
    },
    {
      title: "Order Date",
      field: "CreatedDate",
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" },
      emptyValue: "N/A",
      type: "datetime"
    },
    {
      title: "Status",
      field: "Status.DisplayName",
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" },
      emptyValue: "N/A",
      type: "string"
    },
    {
      width: "fit-content",
      render: ViewDetailsButton,
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" }
    },
    {
      title: "Order Key",
      field: "CustomKey",
      hidden: true,
      cellStyle: { paddingTop: "0.4em", paddingBottom: "0.4em" },
      emptyValue: "N/A",
      type: "string"
    },
    { title: "Quantity", field: "ItemQuantity", type: "numeric", hidden: true }
  ];

  const filterColumns: Array<IToolbarFilterColumn> = [
    {
      toolbarFilterTitle: "ID",
      toolbarFilterType: "id"
    },
    {
      toolbarFilterTitle: "Total",
      toolbarFilterPlaceholder: "1.99",
      toolbarFilterType: "input"
    },
    {
      toolbarFilterTitle: "Quantity",
      toolbarFilterType: "minmaxnumber",
      toolbarFilterMin: 1,
      toolbarFilterMax: 100
    },
    {
      toolbarFilterTitle: "Order Date",
      toolbarFilterType: "minmaxdate"
    },
    {
      toolbarFilterTitle: "Status",
      toolbarFilterType: "select",
      toolbarFilterOptions: statuses
    }
  ];

  const formatFiltersObjectForGetOrders = (
    filterData: IOrdersFilterData
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
          case "Order Date":
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
        <section className="section-orders">
          <div className="section-title d-flex align-items-center justify-content-between">
            {isDashboardMain ? (
              <Fragment>
                <h4 className="title">
                  <FontAwesomeIcon icon={faReceipt} className="me-2" />
                  <span>Recent Orders</span>
                </h4>
                <p className="text-right">
                  <Link
                    to="/dashboard/orders"
                    className="btn btn-link"
                    id="userDashboard_salesOrder_viewAll">
                    <span className="mr-1">View All</span>
                    <FontAwesomeIcon icon={faAngleDoubleRight} />
                  </Link>
                </p>
              </Fragment>
            ) : (
              <h1 className="title">
                <FontAwesomeIcon icon={faReceipt} className="me-2" />
                <span>Orders</span>
              </h1>
            )}
          </div>
          <CVGrid
            columns={columns}
            filterColumns={isDashboardMain ? [] : filterColumns}
            data={orders}
            defaultPageSize={pageSize}
            defaultPageSizeOptions={pageSizeOptions}
            ref={tableRef}
            page={startIndex - 1}
            totalCount={totalOrdersCount}
            onRefreshClicked={(data: any) => getOrders(formatFiltersObjectForGetOrders(data))}
            onFilterClicked={(data: IOrdersFilterData) =>
              getOrders(formatFiltersObjectForGetOrders(data))
            }
            onPageChange={(page: number, pageSize: number) => {
              setStartIndex(page + 1);
              setSize(pageSize);
            }}
            onOrderChange={(orderBy: number, orderDirection: string) => {
              let sortFieldToApply = "CreatedDate";
              let sortDirectionToApply = "desc";
              if (orderBy >= 0) {
                // @ts-ignore possibly 'undefined'
                sortFieldToApply = columns[orderBy].field;
                if (orderDirection.length) {
                  sortDirectionToApply = orderDirection;
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
