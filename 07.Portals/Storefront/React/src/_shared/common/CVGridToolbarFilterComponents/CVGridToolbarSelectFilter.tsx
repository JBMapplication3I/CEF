import { Col, Form } from "react-bootstrap";
import { ICVGridToolbarSelectFilterProps } from "./_CVGridToolbarFilterTypes";

export const CVGridToolbarSelectFilter = (
  props: ICVGridToolbarSelectFilterProps
) => {
  const {
    toolbarFilterTitle,
    uniqueFieldID,
    optionValue,
    onSelectChanged,
    toolbarFilterOptions
  } = props;

  return (
    <Col key={toolbarFilterTitle}>
      <Form.Label id="UserDashboardSelectFilterText" htmlFor={uniqueFieldID}>
        {toolbarFilterTitle}
      </Form.Label>
      <Form.Select
        id={uniqueFieldID}
        onChange={(e) => onSelectChanged(e.target.value)}
        value={optionValue}>
        <option value="All">All</option>
        {toolbarFilterOptions.map((option: any) => {
          const { CustomKey } = option;
          return (
            <option key={CustomKey} value={CustomKey}>
              {CustomKey}
            </option>
          );
        })}
      </Form.Select>
    </Col>
  );
};
