import React from "react";

export interface IToolbarValuesState {
  [key: string]: any;
}

export interface ICVGridToolbarFilterProps {
  uniqueFieldID: string;
  toolbarFilterTitle: string;
  toolbarValuesState?: IToolbarValuesState;
  setToolbarValuesState?: React.Dispatch<any>;
}

export interface ICVGridToolbarIDFilterProps extends ICVGridToolbarFilterProps {
  showLabels: boolean;
  onIdChanged: (id: string) => void;
  idValue: string;
  onCallFilterClicked: VoidFunction;
}

export interface ICVGridToolbarInputFilterProps
  extends ICVGridToolbarFilterProps {
  onInputChange: (input: string) => void;
  inputValue: string;
  onCallFilterClicked: VoidFunction;
}

export interface ICVGridToolbarAutoCompleteFilterProps
  extends ICVGridToolbarFilterProps {
  onTextAndSuggestionChange: (text: string) => void;
  onCallFilterClicked: VoidFunction;
  filterColumns: Array<any>;
}

export interface ICVGridToolbarAttributesFilterProps
  extends ICVGridToolbarFilterProps {
  onRemoveAttributeChange: (index: number) => void;
  onAddAttributeChange: (attribute: string) => void;
  attributesState: string;
}

export interface ICVGridToolbarSelectFilterProps
  extends ICVGridToolbarFilterProps {
  optionValue: string;
  onSelectChanged: (option: string) => void;
  toolbarFilterOptions: Array<any>;
}

export interface ICVGridToolbarBooleanFilterProps
  extends ICVGridToolbarFilterProps {
  onBooleanChange: (boolean: string) => void;
}

export interface ICVGridToolbarMinMaxNumFilterProps
  extends ICVGridToolbarFilterProps {
  onNumChange: (num: string) => void;
  onCallFilterClicked: VoidFunction;
  toolbarFilterMin: number;
  toolbarFilterMax: number;
  toolbarFilterPlaceholder: string;
}

export interface ICVGridToolbarMinMaxDateFilterProps
  extends ICVGridToolbarFilterProps {
  onMinDateChange: (date: string) => void;
  onMaxDateChange: (date: string) => void;
}

export interface ICVGridToolbarFilterBtns {
  onCallFilterClicked: VoidFunction;
  onRefreshClicked: Function;
  isFilterColumnsAvailable: boolean;
}
