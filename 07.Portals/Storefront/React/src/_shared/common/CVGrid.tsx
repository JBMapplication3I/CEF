import React, { Fragment } from "react";
import { LoadingWidget } from "./LoadingWidget";
import MaterialTable from "@material-table/core";
import variables from "../../_meta/css/exposedToJSVariables.module.scss";
import { CVGridToolbar } from "./CVGridToolbar";
import { ServiceStrings } from "../ServiceStrings";

// type valueof<T> = T[keyof T];
interface ICVGridProps {
  columns: Array<any>;
  filterColumns: IToolbarFilterColumn[];
  data: Array<any>;
  page: number;
  totalCount: number;
  onPageChange: Function;
  onOrderChange: Function;
  isLoading: boolean;
  onFilterClicked: Function;
  onRefreshClicked: Function;
  showLabels?: boolean;
  ref: any;
  defaultPageSize: number;
  defaultPageSizeOptions: number[];
}
interface IToolbarFilterColumn {
  toolbarFilterType: string; //TODO should be a valueof<ServiceStrings.cvgrid.filters>
  toolbarFilterOptions?: Array<any>;
  toolbarFilterTitle: string;
  //TODO Add toolbarFilterStateKey
  toolbarFilterPlaceholder?: string;
  toolbarFilterMin?: number;
  toolbarFilterMax?: number;
}

const CVGridComponent = (props: ICVGridProps, ref: any): JSX.Element => {
  const {
    columns,
    filterColumns,
    data,
    page,
    totalCount,
    onPageChange,
    onOrderChange,
    isLoading,
    onRefreshClicked,
    onFilterClicked,
    defaultPageSize,
    defaultPageSizeOptions
  } = props;

  function TableLoadingWidget() {
    return <LoadingWidget overlay={true} />;
  }

  return (
    <Fragment>
      <CVGridToolbar
        filterColumns={filterColumns}
        onRefreshClicked={onRefreshClicked}
        onFilterClicked={onFilterClicked}
        showLabels={props.showLabels !== undefined ? props.showLabels : true}
      />
      <MaterialTable
        columns={columns}
        data={(q) => {
          return new Promise((resolve, reject) => {
            resolve({
              data,
              page,
              totalCount
            });
          });
        }}
        tableRef={ref}
        options={{
          emptyRowsWhenPaging: false,
          toolbar: false,
          thirdSortClick: false,
          padding: "dense",
          paginationType: "stepped",
          rowStyle: (_rowData, index) => ({
            backgroundColor: index % 2 === 0 ? variables.light : variables.white
          }),
          pageSize: defaultPageSize ?? 8,
          pageSizeOptions: defaultPageSizeOptions ?? [8, 16, 24]
        }}
        onPageChange={(page, pageSize) => {
          onPageChange(page, pageSize);
        }}
        onOrderChange={(orderBy, orderDirection) => {
          onOrderChange(orderBy, orderDirection);
        }}
        components={{
          OverlayLoading: TableLoadingWidget
        }}
        isLoading={isLoading}
        // detailPanel={(rowData) => {
        //   return (
        //     <div className="px-4 py-2">
        //       <ul className="list-unstyled">
        //         <li>Quantity: {rowData.rowData.ItemQuantity}</li>
        //         <li>CustomKey: {rowData.rowData.CustomKey}</li>
        //       </ul>
        //     </div>
        //   );
        // }}
      />
    </Fragment>
  );
};

export const CVGrid = React.forwardRef(CVGridComponent);
