import { useState, useEffect } from "react";
import { Col, Form, InputGroup, ListGroup, Card } from "react-bootstrap";
import cvApi from "../../../_api/cvApi";
import { ICVGridToolbarAutoCompleteFilterProps } from "./_CVGridToolbarFilterTypes";

export const CVGridToolbarAutoCompleteFilter = (
  props: ICVGridToolbarAutoCompleteFilterProps
) => {
  const {
    toolbarFilterTitle,
    uniqueFieldID,
    onCallFilterClicked,
    onTextAndSuggestionChange
  } = props;

  const [suggestions, setSuggestions] = useState<Array<any>>([]);
  const [showSuggestions, setShowSuggestions] = useState(false);
  const [error, setError] = useState(null);
  const [text, setText] = useState("");

  useEffect(() => {
    if (suggestions && suggestions.length) {
      setShowSuggestions(true);
    }
  }, [suggestions]);

  const handleTextChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    let suggestions: Array<any> = [];
    const value = e.target.value;
    onTextAndSuggestionChange(value);
    if (value.length > 0) {
      cvApi.providers
        .SuggestProductCatalogWithProvider({
          Query: value,
          Page: 1,
          PageSetSize: 8,
          PageSize: 8,
          Sort: 0
        })
        .then((result) => {
          setSuggestions(result.data);
        })
        .catch((err: any) => {
          setError(err);
        });
    }
    setSuggestions(suggestions);
    setText(value);
  };

  const onClickSuggestion = (itemName: string) => {
    setText(itemName);
    onTextAndSuggestionChange(itemName);
    setShowSuggestions(false);
  };

  return (
    <Col key={toolbarFilterTitle}>
      <Form.Label htmlFor={uniqueFieldID}>{toolbarFilterTitle}</Form.Label>
      <InputGroup>
        <Form.Control
          id={uniqueFieldID}
          type="text"
          placeholder={toolbarFilterTitle}
          aria-label={uniqueFieldID}
          onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>) => {
            if (e.key === "Enter") {
              onCallFilterClicked();
            }
            if (e.key === "Backspace") {
              setShowSuggestions(false);
            }
          }}
          onChange={handleTextChange}
          value={text}
        />
        {showSuggestions ? (
          <Card.Body
            className="position-absolute border bg-white"
            style={{ top: "105%", zIndex: 1000 }}>
            <ListGroup as="ul" className="list-unstyled mb-0">
              {suggestions.map((suggestion) => (
                <ListGroup.Item
                  key={suggestion.Name}
                  className="p-2"
                  onClick={() => onClickSuggestion(suggestion.Name)}>
                  {suggestion.Name}
                </ListGroup.Item>
              ))}
            </ListGroup>
          </Card.Body>
        ) : null}
      </InputGroup>
    </Col>
  );
};
