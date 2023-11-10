import React, { useEffect, useState } from "react";
import cvApi from "../../_api/cvApi";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch } from "@fortawesome/free-solid-svg-icons";
import { Button, Card, InputGroup, Form, ListGroup } from "react-bootstrap";
import { useHistory } from "react-router";
import { useTranslation } from "react-i18next";

export const ExternalSearchBox = () => {
  const [suggestions, setSuggestions] = useState<Array<any>>([]);
  const [showSuggestions, setShowSuggestions] = useState(false);
  const [text, setText] = useState("");
  const [error, setError] = useState(null);

  const { t } = useTranslation();
  const history = useHistory();

  useEffect(() => {
    if (suggestions && suggestions.length) {
      setShowSuggestions(true);
    }
  }, [suggestions]);

  const handleTextChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    let suggestions: Array<any> = [];
    const value = e.target.value;
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

  const handleSubmit = (e: React.MouseEvent<HTMLFormElement>): void => {
    if (text === "") {
      history.push(`/Catalog`);
      history.go(0);
    } else{
      history.push(`/Catalog?term=${text}`);
      history.go(0);
      setText("");
    }
    e.preventDefault();
  };

  return (
    <div
      role="search"
      className="w-100 sm-w-auto md-w-auto lg-w-auto xl-w-auto tk-w-auto fk-w-auto">
      <Form
        className="form-inline my-2 my-lg-0 position-relative"
        onSubmit={handleSubmit}>
        <InputGroup>
          <Form.Control
            className="search-input p-0 border-right-0"
            type="search"
            placeholder={t(
              "ui.storefront.searchCatalog.controls.externalSearchBox.Search.Ellipses"
            )}
            aria-label="Search"
            onChange={handleTextChange}
            value={text}
          />
          <InputGroup.Text className="p-0">
            <Button
              variant="outline-secondary"
              className="p-1 pl-3 pr-3"
              style={{ borderTopLeftRadius: 0, borderBottomLeftRadius: 0 }}
              type="submit">
              <FontAwesomeIcon icon={faSearch} aria-label="search" />
            </Button>
          </InputGroup.Text>
        </InputGroup>
        {showSuggestions ? (
          <Card.Body
            className="position-absolute border bg-white"
            style={{ top: "105%", zIndex: 1000 }}>
            <ListGroup as="ul" className="list-unstyled mb-0">
              {suggestions.map((suggestion) => (
                <ListGroup.Item key={suggestion.Name} className="p-2">
                  <a
                    href={`/product?seoUrl=${suggestion.SeoUrl}`}
                    onClick={() => setShowSuggestions(false)}>
                    {suggestion.Name}
                  </a>
                </ListGroup.Item>
              ))}
            </ListGroup>
          </Card.Body>
        ) : null}
      </Form>
    </div>
  );
};
