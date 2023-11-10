import MaterialTable, { Column } from "@material-table/core";
import { createRef, forwardRef, useEffect } from "react";
import scssVariables from "../../_meta/css/exposedToJSVariables.module.scss";

interface ITableProps {
  data: Array<any>;
  columns: Column<any>[];
  headerStyle?: IRowStyle;
  setTableRef?(ref: any): void;
  DetailPanelContent?(rowData: any): JSX.Element;
}
interface IRowStyle {
  borderStyle?: "solid" | "dashed" | "none" | "hidden";
  fontWeight?: "bold" | "normal" | "lighter";
  textAlign?: "left" | "center" | "right";
}

export const Table = (props: ITableProps): JSX.Element => {
  const { data, columns, headerStyle, setTableRef, DetailPanelContent } = props;
  const matTableRef: any = createRef();

  const columnCellStyle = {
    borderWidth: `1px`,
    borderColor: scssVariables.tableBorderColor,
    borderStyle: "solid",
    padding: "0.5rem 1rem"
  };

  const columnsWithCellStyles = columns.map((col) => {
    return {
      ...col,
      ...(col.cellStyle
        ? {
            ...col.cellStyle,
            ...columnCellStyle
          }
        : columnCellStyle)
    };
  });

  const TABLE_ICONS = {
    // eslint-disable-next-line react/display-name
    DetailPanel: forwardRef((_props, _ref) => null)
  };

  useEffect(() => {
    if (setTableRef) {
      setTableRef(matTableRef);
    }
  }, [matTableRef]);

  return (
    <div>
      <MaterialTable
        tableRef={matTableRef}
        data={data}
        columns={columnsWithCellStyles}
        options={{
          detailPanelColumnStyle: { display: "none" },
          showDetailPanelIcon: false,
          emptyRowsWhenPaging: false,
          toolbar: false,
          thirdSortClick: false,
          padding: "dense",
          pageSize: 8,
          pageSizeOptions: [8, 16, 24],
          rowStyle: (_rowData, index) => {
            return {
              backgroundColor:
                index % 2 === 0 ? scssVariables.light : scssVariables.white,
              border: "1px solid " + scssVariables.tableBorderColor
            };
          },
          editCellStyle: {
            border: "none"
          },
          headerStyle: {
            borderWidth: "0 1px",
            borderColor: scssVariables.tableBorderColor,
            padding: "0.5rem 1rem",
            ...(headerStyle && {
              borderStyle: headerStyle.borderStyle || "none",
              fontWeight: headerStyle.fontWeight || "normal",
              textAlign: headerStyle.textAlign || "center"
            })
          }
        }}
        icons={TABLE_ICONS}
        detailPanel={
          DetailPanelContent
            ? (obj: any) => {
                const rowData: any = obj.rowData;
                return DetailPanelContent(rowData);
              }
            : undefined
        }
      />
    </div>
  );
};
