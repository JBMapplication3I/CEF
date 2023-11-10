import { Col, Form } from "react-bootstrap";
import { ICVGridToolbarMinMaxNumFilterProps } from "./_CVGridToolbarFilterTypes";

export const CVGridToolbarMinMaxNumFilter = (
  props: ICVGridToolbarMinMaxNumFilterProps
) => {
  const {
    toolbarFilterTitle,
    uniqueFieldID,
    toolbarValuesState,
    onNumChange,
    onCallFilterClicked,
    toolbarFilterMin,
    toolbarFilterMax,
    toolbarFilterPlaceholder
  } = props;

  return (
    <Col key={toolbarFilterTitle}>
      <Form.Label htmlFor={uniqueFieldID}>{toolbarFilterTitle}</Form.Label>
      <Form.Control
        className="form-control"
        id={uniqueFieldID}
        type="number"
        min={toolbarFilterMin}
        max={toolbarFilterMax}
        placeholder={toolbarFilterPlaceholder || toolbarFilterTitle}
        aria-label={uniqueFieldID}
        onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>) => {
          if (e.key === "Enter") {
            onCallFilterClicked();
          }
        }}
        onChange={(e) => onNumChange(e.target.value)}
        value={toolbarValuesState[toolbarFilterTitle]}
      />
    </Col>
  );
};
