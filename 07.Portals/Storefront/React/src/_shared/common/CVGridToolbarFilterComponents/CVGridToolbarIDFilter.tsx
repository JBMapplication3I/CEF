import { Col, Form } from "react-bootstrap";
import { ICVGridToolbarIDFilterProps } from "./_CVGridToolbarFilterTypes";

export const CVGridToolbarIDFilter = (
  props: ICVGridToolbarIDFilterProps
): JSX.Element => {
  const {
    showLabels,
    toolbarFilterTitle,
    uniqueFieldID,
    onCallFilterClicked,
    onIdChanged,
    idValue
  } = props;

  return (
    <Col key={toolbarFilterTitle}>
      {showLabels ? (
        <Form.Label htmlFor={uniqueFieldID}>{toolbarFilterTitle}</Form.Label>
      ) : null}
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
        onChange={(e) => onIdChanged(e.target.value)}
        value={idValue}
      />
    </Col>
  );
};
