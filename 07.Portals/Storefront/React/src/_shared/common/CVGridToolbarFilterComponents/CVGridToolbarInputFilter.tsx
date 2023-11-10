import { ICVGridToolbarInputFilterProps } from "./_CVGridToolbarFilterTypes";
import { Col, Form } from "react-bootstrap";

export const CVGridToolbarInputFilter = (
  props: ICVGridToolbarInputFilterProps
): JSX.Element => {
  const {
    uniqueFieldID,
    toolbarFilterTitle,
    onCallFilterClicked,
    onInputChange,
    inputValue
  } = props;

  return (
    <Col key={toolbarFilterTitle}>
      <Form.Label htmlFor={uniqueFieldID}>{toolbarFilterTitle}</Form.Label>
      <Form.Control
        id={uniqueFieldID}
        type="text"
        placeholder={toolbarFilterTitle}
        aria-label={uniqueFieldID}
        onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>) => {
          if (e.key === "Enter") {
            onCallFilterClicked();
          }
        }}
        onChange={(e) => onInputChange(e.target.value)}
        value={inputValue}
      />
    </Col>
  );
};
