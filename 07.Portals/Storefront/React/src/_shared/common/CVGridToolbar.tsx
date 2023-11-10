import { Row } from "react-bootstrap";
import { useState } from "react";
import { ServiceStrings } from "../ServiceStrings";
import { CVGridToolbarIDFilter } from "./CVGridToolbarFilterComponents/CVGridToolbarIDFilter";
import { CVGridToolbarFilterBtns } from "./CVGridToolbarFilterComponents/CVGridToolbarFilterBtns";
import { CVGridToolbarInputFilter } from "./CVGridToolbarFilterComponents/CVGridToolbarInputFilter";
import { CVGridToolbarSelectFilter } from "./CVGridToolbarFilterComponents/CVGridToolbarSelectFilter";
import { CVGridToolbarBooleanFilter } from "./CVGridToolbarFilterComponents/CVGridToolbarBooleanFilter";
import { CVGridToolbarAttributeFilter } from "./CVGridToolbarFilterComponents/CVGridToolbarAttributeFilter";
import { CVGridToolbarMinMaxNumFilter } from "./CVGridToolbarFilterComponents/CVGridToolbarMinMaxNumFilter";
import { CVGridToolbarMinMaxDateFilter } from "./CVGridToolbarFilterComponents/CVGridToolbarMinMaxDateFilter";
import { CVGridToolbarAutoCompleteFilter } from "./CVGridToolbarFilterComponents/CVGridToolbarAutoCompleteFilter";
import { IToolbarValuesState } from "./CVGridToolbarFilterComponents/_CVGridToolbarFilterTypes";

interface ICVGridToolbarProps {
  filterColumns: Array<any>;
  onFilterClicked: Function;
  onRefreshClicked: Function;
  showLabels: boolean;
}
export const CVGridToolbar = (props: ICVGridToolbarProps): JSX.Element => {
  const { filterColumns, onFilterClicked, onRefreshClicked, showLabels } =
    props;

  const initialState: any = {};
  for (let i = 0; i < filterColumns.length; i++) {
    if (filterColumns[i].hidden || !filterColumns[i].toolbarFilterTitle) {
      continue;
    }
    if (filterColumns[i].toolbarFilterType === "minmaxdate") {
      initialState[filterColumns[i].toolbarFilterTitle] = {
        MinDate: "",
        MaxDate: ""
      };
      continue;
    }
    if (filterColumns[i].toolbarFilterType === "attribute") {
      initialState[filterColumns[i].toolbarFilterTitle] = [];
      continue;
    }
    initialState[filterColumns[i].toolbarFilterTitle] = "";
  }

  const [toolbarValuesState, setToolbarValuesState] =
    useState<IToolbarValuesState>(initialState);

  const onCallFilterClicked = (): void => {
    let d: any = {};
    for (let i = 0; i < filterColumns.length; i++) {
      if (filterColumns[i].conversion) {
        const dataToSend = filterColumns[i].conversion(
          toolbarValuesState[filterColumns[i].toolbarFilterTitle]
        );
        d = {
          ...d,
          ...dataToSend
        };
      } else {
        d = {
          ...d,
          [filterColumns[i].toolbarFilterTitle]:
            toolbarValuesState[filterColumns[i].toolbarFilterTitle]
        };
      }
    }
    onFilterClicked(d);
  };

  return (
    <Row className="mb-2">
      {filterColumns.map((col, index) => {
        const uniqueFieldID = `${col.toolbarFilterType}${index}_${col.toolbarFilterTitle}`;
        switch (col.toolbarFilterType) {
          case ServiceStrings.cvgrid.filters.id:
            return (
              <CVGridToolbarIDFilter
                key={uniqueFieldID}
                showLabels={showLabels}
                toolbarFilterTitle={col.toolbarFilterTitle}
                uniqueFieldID={uniqueFieldID}
                onIdChanged={(id) =>
                  setToolbarValuesState({
                    ...toolbarValuesState,
                    [col.toolbarFilterTitle]: id
                  })
                }
                idValue={toolbarValuesState[col.toolbarFilterTitle]}
                onCallFilterClicked={onCallFilterClicked}
              />
            );
          case ServiceStrings.cvgrid.filters.input:
            return (
              <CVGridToolbarInputFilter
                key={uniqueFieldID}
                uniqueFieldID={uniqueFieldID}
                toolbarFilterTitle={col.toolbarFilterTitle}
                onCallFilterClicked={onCallFilterClicked}
                onInputChange={(input: string) =>
                  setToolbarValuesState({
                    ...toolbarValuesState,
                    [col.toolbarFilterTitle]: input
                  })
                }
                inputValue={toolbarValuesState[col.toolbarFilterTitle]}
              />
            );
          case ServiceStrings.cvgrid.filters.autocomplete:
            return (
              <CVGridToolbarAutoCompleteFilter
                key={uniqueFieldID}
                toolbarFilterTitle={col.toolbarFilterTitle}
                uniqueFieldID={uniqueFieldID}
                onCallFilterClicked={onCallFilterClicked}
                toolbarValuesState={toolbarValuesState}
                setToolbarValuesState={setToolbarValuesState}
                onTextAndSuggestionChange={(text: string) =>
                  setToolbarValuesState({
                    ...toolbarValuesState,
                    [col.toolbarFilterTitle]: text
                  })
                }
                filterColumns={filterColumns}
              />
            );
          case ServiceStrings.cvgrid.filters.attribute:
            return (
              <CVGridToolbarAttributeFilter
                key={uniqueFieldID}
                toolbarFilterTitle={col.toolbarFilterTitle}
                uniqueFieldID={uniqueFieldID}
                onRemoveAttributeChange={(index: number) =>
                  setToolbarValuesState({
                    ...toolbarValuesState,
                    [col.toolbarFilterTitle]: [
                      ...toolbarValuesState[col.toolbarFilterTitle].filter(
                        (_: string, i: number) => i !== index
                      )
                    ]
                  })
                }
                onAddAttributeChange={(attribute: string) =>
                  setToolbarValuesState({
                    ...toolbarValuesState,
                    [col.toolbarFilterTitle]: [
                      ...toolbarValuesState[col.toolbarFilterTitle],
                      attribute
                    ]
                  })
                }
                attributesState={toolbarValuesState[col.toolbarFilterTitle]}
              />
            );
          case ServiceStrings.cvgrid.filters.select:
            return (
              <CVGridToolbarSelectFilter
                key={uniqueFieldID}
                toolbarFilterTitle={col.toolbarFilterTitle}
                uniqueFieldID={uniqueFieldID}
                optionValue={toolbarValuesState[col.toolbarFilterTitle]}
                onSelectChanged={(option) =>
                  setToolbarValuesState({
                    ...toolbarValuesState,
                    [col.toolbarFilterTitle]: option
                  })
                }
                toolbarFilterOptions={col.toolbarFilterOptions}
              />
            );
          case ServiceStrings.cvgrid.filters.boolean:
            return (
              <CVGridToolbarBooleanFilter
                key={uniqueFieldID}
                toolbarFilterTitle={col.toolbarFilterTitle}
                uniqueFieldID={uniqueFieldID}
                onBooleanChange={(boolean) =>
                  setToolbarValuesState({
                    ...toolbarValuesState,
                    [col.toolbarFilterTitle]: boolean
                  })
                }
              />
            );
          case ServiceStrings.cvgrid.filters.minmaxnumber:
            return (
              <CVGridToolbarMinMaxNumFilter
                key={uniqueFieldID}
                toolbarFilterTitle={col.toolbarFilterTitle}
                uniqueFieldID={uniqueFieldID}
                toolbarValuesState={toolbarValuesState}
                onNumChange={(num: string) =>
                  setToolbarValuesState({
                    ...toolbarValuesState,
                    [col.toolbarFilterTitle]: num
                  })
                }
                onCallFilterClicked={onCallFilterClicked}
                toolbarFilterMin={col.toolbarFilterMin}
                toolbarFilterMax={col.toolbarFilterMax}
                toolbarFilterPlaceholder={col.toolbarFilterPlaceholder}
              />
            );
          case ServiceStrings.cvgrid.filters.minmaxdate:
            return (
              <CVGridToolbarMinMaxDateFilter
                key={uniqueFieldID}
                toolbarFilterTitle={col.toolbarFilterTitle}
                uniqueFieldID={uniqueFieldID}
                onMinDateChange={(minDate: string) =>
                  setToolbarValuesState({
                    ...toolbarValuesState,
                    [col.toolbarFilterTitle]: {
                      ...toolbarValuesState[col.toolbarFilterTitle],
                      MinDate: minDate
                    }
                  })
                }
                onMaxDateChange={(maxDate: string) =>
                  setToolbarValuesState({
                    ...toolbarValuesState,
                    [col.toolbarFilterTitle]: {
                      ...toolbarValuesState[col.toolbarFilterTitle],
                      MaxDate: maxDate
                    }
                  })
                }
              />
            );
          default:
            return <div key={col.toolbarFilterTitle}></div>;
        }
      })}
      <CVGridToolbarFilterBtns
        isFilterColumnsAvailable={!!filterColumns.length}
        onCallFilterClicked={onCallFilterClicked}
        onRefreshClicked={onRefreshClicked}
      />
    </Row>
  );
};
