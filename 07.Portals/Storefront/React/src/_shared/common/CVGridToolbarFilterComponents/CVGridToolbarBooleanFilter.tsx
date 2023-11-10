import { ICVGridToolbarBooleanFilterProps } from "./_CVGridToolbarFilterTypes";

import { Col, Form } from "react-bootstrap";

export const CVGridToolbarBooleanFilter = (
  props: ICVGridToolbarBooleanFilterProps
) => {
  const { toolbarFilterTitle, uniqueFieldID, onBooleanChange } = props;
  return (
    <Col key={toolbarFilterTitle}>
      <Form.Label id="UserDashboardSelectFilterText" htmlFor={uniqueFieldID}>
        {toolbarFilterTitle}
      </Form.Label>
      <Form.Select
        id={uniqueFieldID}
        onChange={(e) => onBooleanChange(e.target.value)}>
        <option value="Either">Either</option>
        <option value="true">True</option>
        <option value="false">False</option>
      </Form.Select>
    </Col>
  );
};
